using System;
using System.Collections.Generic;
using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Clipboard;
using GuiLabs.Undo;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.Blocks
{
	partial class LinearContainerBlock
	{
		#region Query

		public DragQueryResult CanAcceptBlocks(DragQuery draggedBlocks)
		{
			if (!CanTheoreticallyAcceptBlocks(draggedBlocks)
				|| !this.MyControl.Bounds.HitTest(draggedBlocks.DropPoint))
			{
				return null;
			}
			DragQueryResult result = FindAPlaceToDrop(draggedBlocks);
			if (result != null && draggedBlocks.IsTargetNearSource(result))
			{
				return null;
			}
			return result;
		}

		#region Can theoretically accept blocks?

		public virtual bool CanTheoreticallyAcceptBlocks(DragQuery draggedBlocks)
		{
			if (AcceptableBlockTypes == null
				|| AcceptableBlockTypes.Count == 0)
			{
				return false;
			}
			if (!draggedBlocks.ShouldCopyInsteadOfMove)
			{
				foreach (Block dragged in draggedBlocks)
				{
					ContainerBlock draggedContainer = dragged as ContainerBlock;
					if (dragged != null && this.IsInSubtreeOf(draggedContainer))
					{
						return false;
					}
				}
			}
			foreach (Block dragged in draggedBlocks)
			{
				bool foundAssignable = false;
				foreach (Type acceptableType in AcceptableBlockTypes)
				{
					if (acceptableType.IsAssignableFrom(dragged.GetType()))
					{
						foundAssignable = true;
					}
				}
				if (!foundAssignable)
				{
					return false;
				}
			}
			return true;
		}

		#region Acceptable block types

		private Set<Type> AcceptableBlockTypes = new Set<Type>();

		public void AddAcceptableBlockTypes(params Type[] acceptableBlockTypes)
		{
			foreach (Type t in acceptableBlockTypes)
			{
				if (!AcceptableBlockTypes.Contains(t))
				{
					AcceptableBlockTypes.Add(t);
				}
			}
		}

		public void AddAcceptableBlockTypes<T1>()
		{
			AddAcceptableBlockTypes(typeof(T1));
		}

		public void AddAcceptableBlockTypes<T1, T2>()
		{
			AddAcceptableBlockTypes(typeof(T1), typeof(T2));
		}

		public void AddAcceptableBlockTypes<T1, T2, T3>()
		{
			AddAcceptableBlockTypes(typeof(T1), typeof(T2), typeof(T3));
		}

		#endregion

		#endregion

		public virtual DragQueryResult FindAPlaceToDrop(DragQuery draggedBlocks)
		{
			Point dropPoint = draggedBlocks.DropPoint;
			Block foundReferenceBlock = null;
			DragQueryResult result;
			int minDistance = int.MaxValue;
			int distance;

			if (this.Orientation == OrientationType.Horizontal)
			{
				foreach (Block child in this.Children)
				{
					if (!child.MyControl.Bounds.HitTestWithMarginX(
						dropPoint.X,
						child.MyControl.Box.MouseSensitivityArea)) continue;

					if (child is ISeparatorBlock)
					{
						return new DragQueryResult(child, DropTargetInfo.DropAfterSeparator);
					}
					else
					{
						distance = child.MyControl.Bounds.CenterX - dropPoint.X;
						if (Math.Abs(distance) < Math.Abs(minDistance))
						{
							minDistance = distance;
							foundReferenceBlock = child;
						}
					}
				}
			}
			else
			{
				foreach (Block child in this.Children)
				{
					if (!child.MyControl.Bounds.HitTestWithMarginY(
						dropPoint.Y,
						child.MyControl.Box.MouseSensitivityArea)) continue;

					if (child is ISeparatorBlock)
					{
						return new DragQueryResult(child, DropTargetInfo.DropAfterSeparator);
					}
					else
					{
						distance = child.MyControl.Bounds.CenterY - dropPoint.Y;
						if (Math.Abs(distance) < Math.Abs(minDistance))
						{
							minDistance = distance;
							foundReferenceBlock = child;
						}
					}
				}
			}

			if (foundReferenceBlock == null)
			{
				return null;
			}

			result = new DragQueryResult(foundReferenceBlock);
			result.IsDropPointInTheUpperHalfOfDropTarget = minDistance > 0;
			AdjustPotentialDropTarget(draggedBlocks, result);
			if (result.DropTarget == null)
			{
				return null;
			}

			return result;
		}

		private void AdjustPotentialDropTarget(
			DragQuery request,
			DragQueryResult result)
		{
			if (result.IsDropPointInTheUpperHalfOfDropTarget)
			{
				result.HowToDrop = DropTargetInfo.DropBeforeTarget;
			}
			else
			{
				result.HowToDrop = DropTargetInfo.DropAfterTarget;
			}
			result.AdjustDropOnSeparator();

			if (request.CanDropHere(result))
			{
				return;
			}

			if (request.IsDropPointAboveDragStartPoint)
			{
				result.HowToDrop = DropTargetInfo.DropBeforeTarget;
				result.AdjustDropOnSeparator();
				while (result.DropTarget != null
					&& !request.CanDropHere(result))
				{
					result.DropTarget = result.DropTarget.Prev;
					result.AdjustDropOnSeparator();
				}
				if (result.DropTarget == null)
				{
					return;
				}
			}
			else
			{
				result.HowToDrop = DropTargetInfo.DropAfterTarget;
				result.AdjustDropOnSeparator();
				while (result.DropTarget != null
					&& !request.CanDropHere(result))
				{
					result.DropTarget = result.DropTarget.Next;
					result.AdjustDropOnSeparator();
				}
				if (result.DropTarget == null)
				{
					return;
				}
			}
		}

		#endregion

		#region Do accept

		public void AcceptBlocks(DragQuery draggedBlocks, DragQueryResult whereTo)
		{
			if (whereTo.DropTargetContainer.CanAcceptBlocks(draggedBlocks) == null)
			{
				return;
			}

			IEnumerable<Block> toDrop;

			if (draggedBlocks.ShouldCopyInsteadOfMove)
			{
				toDrop = BlockActions.Clone(draggedBlocks);
				toDrop = IntersperseWithSeparators(toDrop, whereTo);
				if (whereTo.HowToDrop == DropTargetInfo.DropBeforeTarget)
				{
					whereTo.DropTarget.PrependBlocks(toDrop);
				}
				else
				{
					whereTo.DropTarget.AppendBlocks(toDrop);
				}
			}
			else
			{
				using (Root.Transaction())
				{
					foreach (Block b in draggedBlocks)
					{
						b.Delete();
					}
					toDrop = IntersperseWithSeparators(draggedBlocks, whereTo);
					if (whereTo.HowToDrop == DropTargetInfo.DropBeforeTarget)
					{
						whereTo.DropTarget.PrependBlocks(toDrop);
					}
					else
					{
						whereTo.DropTarget.AppendBlocks(toDrop);
					}
				}
			}
		}

		public IEnumerable<Block> IntersperseWithSeparators(IEnumerable<Block> draggedBlocks, DragQueryResult whereTo)
		{
			List<Block> blocks = new List<Block>();
			bool weHaveSeparators = HasSeparators;

			foreach (Block dragged in draggedBlocks)
			{
				if (dragged is ISeparatorBlock)
				{
					if (weHaveSeparators)
					{
						blocks.Add(CreateSeparator());
					}
				}
				else
				{
					if (blocks.Count > 0
						&& !(blocks[blocks.Count - 1] is ISeparatorBlock)
						&& weHaveSeparators)
					{
						blocks.Add(CreateSeparator());
					}
					blocks.Add(dragged);
				}
			}

			if (blocks.Count > 0
				&& !(blocks[blocks.Count - 1] is ISeparatorBlock)
				&& weHaveSeparators)
			{
				blocks.Add(CreateSeparator());
			}

			return blocks;
		}

		#endregion
	}
}
