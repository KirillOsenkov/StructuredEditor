using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Delegates;
using GuiLabs.Canvas.Renderer;
using System.Drawing;
using GuiLabs.Editor.Clipboard;
using GuiLabs.Utils;
using GuiLabs.Undo;
using System;

namespace GuiLabs.Editor.Blocks
{
	/// <summary>
	/// HOWTO: inherit from RootBlock:
	/// 1. Constructor should call base()
	/// 2. Constructor should set FocusBlock
	/// </summary>
	public partial class RootBlock : LinearContainerBlock //, IRootBlock, IDragDrop
	{
		#region ctor

		public RootBlock()
			: base()
		{
			InitActionManager();
			Root = this;
		}

		#endregion

		#region Events

		public event Action UndoBufferChanged;
		protected void RaiseUndoBufferChanged(object sender, EventArgs e)
		{
			if (UndoBufferChanged != null)
			{
				UndoBufferChanged();
			}
			if (ActiveBlock != null)
			{
				RaiseShouldDisplayContextHelp(ActiveBlock);
			}
		}

		public event ActiveBlockChangedHandler ActiveBlockChanged;
		private void RaiseActiveBlockChanged(Block oldActiveBlock)
		{
			if (ActiveBlockChanged != null)
			{
				ActiveBlockChanged(oldActiveBlock, mActiveBlock);
			}
		}

		public event ChangeHandler<Block> ShouldDisplayContextHelp;
		public void RaiseShouldDisplayContextHelp(Block helpForBlock)
		{
			if (ShouldDisplayContextHelp != null)
			{
				ShouldDisplayContextHelp(helpForBlock);
			}
		}

		public event ChangeHandler<string> ShouldShowStatus;
		public void ShowStatus(string statusText)
		{
			if (ShouldShowStatus != null)
			{
				ShouldShowStatus(statusText);
			}
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Z && e.Control)
			{
				this.ActionManager.Undo();
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Y && e.Control)
			{
				this.ActionManager.Redo();
			}
			OnCtrlPressed(e);
			// Do nothing. This is important.
			// Because otherwise this handler will be fired twice for the RootBlock.
			// 
			// See ShapeWithEvents.cs:
			// public override void OnKeyDown(KeyEventArgs e)
			// {
			//     base.OnKeyDown(e);	// fire keydown once
			//     RaiseKeyDown(e);		// fire keydown for the second time
			// }
		}

		protected override void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(sender, e);
			OnCtrlPressed(e);
		}

		#endregion

		#region ActionManager

		private void InitActionManager()
		{
			mActionManager = new ActionManager();
			mActionManager.CollectionChanged += RaiseUndoBufferChanged;
		}

		private ActionManager mActionManager;
		public override ActionManager ActionManager
		{
			get
			{
				return mActionManager;
			}
		}

		#endregion

		#region Control

		protected override void InitControl(OrientationType orientation)
		{
			MyRootControl = new RootControl(this.Children.Controls);
			MyRootControl.LinearLayoutStrategy.Orientation = orientation;
		}

		private RootControl mMyRootControl;
		public virtual RootControl MyRootControl
		{
			get
			{
				return mMyRootControl;
			}
			protected set
			{
				if (mMyRootControl != null)
				{
					UnSubscribeControl();
					mMyRootControl.AfterDraw -= Draw;
				}
				mMyRootControl = value;
				if (mMyRootControl != null)
				{
					SubscribeControl();
					mMyRootControl.AfterDraw += Draw;
				}
			}
		}

		public override LinearContainerControl MyListControl
		{
			get
			{
				return MyRootControl;
			}
			protected set
			{
				// Should never be called
			}
		}

		public sealed override Control MyControl
		{
			get
			{
				return MyRootControl;
			}
		}

		#endregion

		#region Draw

		private static Rect dragRect = new Rect(0, 0, 64, 32);
		private static Rect dropRect = new Rect();
		private void Draw(IRenderer renderer)
		{
			//if (this.CurrentSelection != null)
			//{
			//    Renderer.DrawOperations.DrawRectangle(
			//        CurrentSelection.Bounds,
			//        Color.Blue, 
			//        3);
			//}

			if (DragState != null)
			{
				DragQuery query = DragState.Query;
				if (DragState.DragStarted)
				{
					dragRect.Location.Set(
						DragState.Query.DropPoint + 24);
					renderer.DrawOperations.DrawDotRectangle(
						dragRect,
						Color.DarkGray);
				}

				if (DragState.Result != null)
				{
					DragQueryResult result = DragState.Result;
					dropRect.Set(result.DropTarget.MyControl.Bounds.AddMargins(
						result.DropTarget.MyControl.Box.MouseSensitivityArea));
					if (result.HowToDrop == DropTargetInfo.DropBeforeTarget)
					{
						dropRect.Size.Y = 3;
					}
					else if (result.HowToDrop == DropTargetInfo.DropAfterTarget)
					{
						dropRect.Location.Y += dropRect.Size.Y;
						dropRect.Size.Y = 3;
					}
					renderer.DrawOperations.DrawRectangle(
						dropRect,
						Color.DeepSkyBlue, 2);
				}
			}
		}

		public void Redraw()
		{
			if (MyRootControl != null)
			{
				MyRootControl.Redraw();
			}
		}

		#endregion

		#region Styles

		public void ReloadAllStyles()
		{
			foreach (Block b in EnumAllBlocks())
			{
				b.InitStyle();
			}
		}

		#endregion

		#region ActiveBlock

		private Block mActiveBlock;
		/// <summary>
		/// Use Block.SetFocus to set the ActiveBlock property.
		/// </summary>
		public Block ActiveBlock
		{
			get
			{
				return mActiveBlock;
			}
			internal set
			{
				Block oldActiveBlock = mActiveBlock;
				mActiveBlock = value;
				//this.MyRootControl.ActiveControl = value.MyControl;
				RaiseActiveBlockChanged(oldActiveBlock);
			}
		}

		#endregion

		#region Visibility

		private VisibilityFilter mVisibilityManager;
		public VisibilityFilter VisibilityManager
		{
			get
			{
				return mVisibilityManager;
			}
			set
			{
				mVisibilityManager = value;
				CheckVisibility();
				this.MyRootControl.Redraw();
			}
		}

		#endregion
	}
}
