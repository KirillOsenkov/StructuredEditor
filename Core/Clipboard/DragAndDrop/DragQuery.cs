using GuiLabs.Editor.Blocks;
using System.Collections.Generic;
using GuiLabs.Canvas;

namespace GuiLabs.Editor.Clipboard
{
	public class DragQuery : IEnumerable<Block>
	{
		public DragQuery(Block b, int x, int y)
		{
			DraggedBlock = b;
			DragStartpoint.Set(x, y);
			DropPoint.Set(x, y);
		}

		private Block mDraggedBlock;
		private Block DraggedBlock
		{
			get
			{
				return mDraggedBlock;
			}
			set
			{
				mDraggedBlock = value;
			}
		}

		private Point mDragStartPoint = new Point();
		public Point DragStartpoint
		{
			get
			{
				return mDragStartPoint;
			}
		}

		private Point mDropPoint = new Point();
		public Point DropPoint
		{
			get
			{
				return mDropPoint;
			}
		}

		public bool IsDropPointAboveDragStartPoint
		{
			get
			{
				return DropPoint.Y < DragStartpoint.Y;
			}
		}

		public IEnumerator<Block> GetEnumerator()
		{
			yield return DraggedBlock;
			//if (DraggedBlock.Next is ISeparatorBlock)
			//{
			//    yield return DraggedBlock.Next;
			//}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private bool mShouldCopyInsteadOfMove = false;
		public bool ShouldCopyInsteadOfMove
		{
			get
			{
				return mShouldCopyInsteadOfMove;
			}
			set
			{
				mShouldCopyInsteadOfMove = value;
			}
		}

		public bool CanDropHere(DragQueryResult potentialDropPoint)
		{
			return
				potentialDropPoint.DropTargetValid
				&& !IsTargetNearSource(potentialDropPoint);
		}

		public bool IsTargetNearSource(DragQueryResult targetBlock)
		{
			if (ShouldCopyInsteadOfMove)
			{
				return false;
			}
			Block target = targetBlock.DropTarget;
			foreach (Block dragged in this)
			{
				if (target == dragged)
				{
					return true;
				}
				if (target == dragged.Next || target == dragged.Prev)
				{
					if (target is ISeparatorBlock)
					{
						return true;
					}
					else
					{
						if (target == dragged.Next
							&& targetBlock.HowToDrop == DropTargetInfo.DropBeforeTarget)
						{
							return true;
						}
						if (target == dragged.Prev
							&& targetBlock.HowToDrop != DropTargetInfo.DropBeforeTarget)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}
}
