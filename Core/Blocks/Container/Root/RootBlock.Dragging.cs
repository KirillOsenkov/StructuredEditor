using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Delegates;
using GuiLabs.Editor.Clipboard;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	partial class RootBlock
	{
		private DragInfo mDragState;
		public DragInfo DragState
		{
			get
			{
				return mDragState;
			}
			set
			{
				mDragState = value;
			}
		}

		public Block FindDraggableBlock(int x, int y)
		{
			Block result = FindBlockAtPoint(x, y);
			while (result != null && !result.Draggable && !result.CanGetFocus)
			{
				result = result.Parent;
			}
			if (result != null && !result.Draggable)
			{
				return null;
			}
			return result;
		}

		public DragQueryResult FindDropTarget()
		{
			Block current = FindBlockAtPoint(
				DragState.Query.DropPoint.X, 
				DragState.Query.DropPoint.Y);
			if (current == null)
			{
				return null;
			}
			if (IsTargetWithinSource(current))
			{
				return null;
			}
			DragQueryResult result = CanDropAt(current);

			while (current != null && result == null)
			{
				current = current.Parent;
				result = CanDropAt(current);
			}

			return result;
		}

		public bool IsTargetWithinSource(Block target)
		{
			foreach (Block source in DragState.Query)
			{
				ContainerBlock sourceContainer = source as ContainerBlock;
				if (sourceContainer != null)
				{
					if (target.IsInSubtreeOf(sourceContainer))
					{
						return true;
					}
				}
			}
			return false;
		}

		private DragQueryResult CanDropAt(Block current)
		{
			IPotentialDropTarget target = current as IPotentialDropTarget;
			if (target == null)
			{
				return null;
			}
			DragQueryResult result = target.CanAcceptBlocks(DragState.Query);
			if (result != null)
			{
				result.DropTargetContainer = target;
			}
			return result;
		}

		protected override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			if (e.Handled == true)
			{
				return;
			}
			Block block = FindDraggableBlock(e.X, e.Y);

			if (e.IsRightButtonPressed
				&& block != null
				&& block.Draggable)
			{
				DragState = new DragInfo(block, e.X, e.Y);
				block.SetFocus();
			}
			else
			{
				DragState = null;
			}
		}

		protected void OnCtrlPressed(System.Windows.Forms.KeyEventArgs e)
		{
			if (DragState != null
				&& DragState.Query != null
				&& DragState.Query.ShouldCopyInsteadOfMove != (e.Control))
			{
				DragState.Query.ShouldCopyInsteadOfMove = (e.Control);

				if (DragState.DragStarted)
				{
					DragState.Result = FindDropTarget();
				}
				Redraw();
			}
		}

		protected override void OnMouseMove(MouseWithKeysEventArgs e)
		{
			if (DragState != null)
			{
				if (!e.IsRightButtonPressed)
				{
					DragState = null;
					Redraw();
					return;
				}

				DragState.Query.DropPoint.Set(e.X, e.Y);
				DragState.Query.ShouldCopyInsteadOfMove = e.IsCtrlPressed;

				if (!DragState.Query.DragStartpoint.PointWithinDragSize(e.X, e.Y)
					|| DragState.DragStarted)
				{
					DragState.Result = FindDropTarget();
					DragState.DragStarted = true;
				}
				Redraw();
			}
		}

		protected override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			if (e.Handled == true)
			{
				return;
			}

			bool shouldShowPopupMenu = e.IsRightButtonPressed;

			using (Redrawer r = new Redrawer(Root))
			{
				if (DragState != null)
				{
					if (DragState.Result != null)
					{
						DragState.Query.ShouldCopyInsteadOfMove = e.IsCtrlPressed;
						DragState.Result.DropTargetContainer.AcceptBlocks(
							DragState.Query,
							DragState.Result);
						shouldShowPopupMenu = false;
						e.Handled = true;
					}
					else
					{
						if (DragState.DragStarted)
						{
							shouldShowPopupMenu = false;
							e.Handled = true;
						}
					}
					DragState = null;
				}

				if (shouldShowPopupMenu && e.IsRightButtonPressed)
				{
					Block rightClicked = FindBlockAtPoint(e.X, e.Y);
					if (rightClicked != null
						&& rightClicked.Menu != null
						&& rightClicked.MyControl.HitTest(e.X, e.Y))
					{
						if (rightClicked.CanGetFocus)
						{
							rightClicked.SetFocus();
						}
						MyRootControl.ShowPopupMenu(rightClicked.Menu, e.Location);
						e.Handled = true;
					}
				}
			}
		}
	}
}