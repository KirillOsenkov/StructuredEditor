using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Lists
{
	/// <summary>
	/// ChildrenList is an intellectual list of blocks, 
	/// that knows about the Parent ContainerBlock.
	/// </summary>
	public class ChildrenList : BlockList, IChildrenList
	{
		#region ctor
		
		public ChildrenList(ContainerBlock parentBlock)
			: base()
		{
			this.Parent = parentBlock;
		}

		#endregion

		#region Events
		
		public event BlockKeyDownEventHandler KeyDown;
		protected void RaiseKeyDown(Block Block, System.Windows.Forms.KeyEventArgs e)
		{
			if (KeyDown != null && !e.Handled)
			{
				KeyDown(Block, e);
			}
		}

		#endregion

		#region Parent

		private ContainerBlock mParent;
		protected ContainerBlock Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent = value;
			}
		}

		#endregion

		#region On Added and Removed

		protected override void OnBlockAdded(Block newBlock)
		{
			base.OnBlockAdded(newBlock);
			SubscribeBlock(newBlock);
		}

		protected override void OnBlockRemoved(Block removedBlock)
		{
			base.OnBlockRemoved(removedBlock);
			UnsubscribeBlock(removedBlock);
		}

		protected override void OnBlockReplaced(Block oldBlock, Block newBlock)
		{
			UnsubscribeBlock(oldBlock);
			base.OnBlockReplaced(oldBlock, newBlock);
			SubscribeBlock(newBlock);
		}

		#endregion

		#region Subscribe children

		private void SubscribeBlock(Block block)
		{
			ContainerBlock oldParent = block.Parent;
			RootBlock oldRoot = block.Root;

			// set block's parent
			block.Parent = this.Parent;
			block.MyControl.Parent = Parent.MyControl as ContainerControl;

			// set block's root
			if (Parent.Root != null)
			{
				block.Root = Parent.Root;
				block.MyControl.Root = Parent.Root.MyRootControl;
			}

			block.NotifyParentChanged(oldParent);
			block.NotifyRootChanged(oldRoot);

			// subscribe to block's events
			block.KeyDown += RaiseKeyDown;
		}

		/// <summary>
		/// Unsubscribe from blocks events
		/// </summary>
		private void UnsubscribeBlock(Block block)
		{
			ContainerBlock oldParent = block.Parent;

			block.KeyDown -= RaiseKeyDown;

			block.Prev = null;
			block.Next = null;

			block.Root = null;
			block.MyControl.Root = null;

			block.Parent = null;
			block.MyControl.Parent = null;

			block.NotifyParentChanged(oldParent);

			block.OnAfterDelete();
		}

		#endregion

		#region Old

		//public INode GetFirstVisibleChild(Rect Window)
		//{
		//    // TODO: Who takes care of checking, if the parent block itself is visible?
		//    // this function or those who call it?
		//    // It would be more efficient if the caller checks it once so that
		//    // we don't need to check it here everytime
		//    //			if (!this.Parent.IsVisible(Window))
		//    //				return null;

		//    Block Current = this.Head;

		//    if (Current == null)
		//        return null;

		//    // Some optimization: if the current block is already below the Window
		//    // no need to dig further down, you will get only deeper
		//    int pos = Current.Bounds.RelativeToRectY(Window);
		//    if (pos == 5)
		//        return null; // so just exit

		//    return Current.FindNextVisible(Window);
		//}

		//public INode GetLastVisibleChild(Rect Window)
		//{
		//    // TODO: Who takes care of checking, if the parent block itself is visible?
		//    // this function or those who call it?
		//    // It would be more efficient if the caller checks it once so that
		//    // we don't need to check it here everytime
		//    //			if (!this.Parent.IsVisible(Window))
		//    //				return null;

		//    Block Current = this.Tail;

		//    if (Current == null)
		//        return null;

		//    int pos = Current.Bounds.RelativeToRectY(Window);
		//    if (pos == 1)
		//        return null;

		//    return Current.FindPreviousVisible(Window);
		//}

		#endregion
	}
}
