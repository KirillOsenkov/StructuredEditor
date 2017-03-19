#region Using directives

using GuiLabs.Editor.Blocks;

#endregion

namespace GuiLabs.Editor.Lists
{
	public class BlockRange : IBlockRange
	{
		private Block mNode;
		public Block Node
		{
			get
			{
				return mNode;
			}
			set
			{
				mNode = value;
			}
		}

		private IBlockRange mFromChild;
		public IBlockRange FromChild
		{
			get
			{
				return mFromChild;
			}
			set
			{
				mFromChild = value;
			}
		}

		private IBlockRange mToChild;
		public IBlockRange ToChild
		{
			get
			{
				return mToChild;
			}
			set
			{
				mToChild = value;
			}
		}

		//		public IBlockRange Clone()
		//		{
		//			IBlockRange Copy = new BlockRange();
		//			Copy.Node = this.Node;
		//			if (this.FromChild != null)
		//			{
		//				Copy.FromChild = this.FromChild.Clone();
		//			}
		//			if (this.ToChild != null)
		//			{
		//				Copy.ToChild = this.ToChild.Clone();
		//			}
		//			return Copy;
		//		}

		// Temporary optimization: reuse existing VRange 
		// instead of calculating a new one each time.
		// Too complicated. Better create a new range each time.
		#region Update visible
		//		public IBlockRange UpdateVisible(Rect Window)
		//		{
		//			int i = this.Node.Block.Bounds.RelativeToRectY(Window);
		//
		//			if (i == 1 || i == 5)
		//			{
		//				return null;
		//			}
		//
		//			ContainerBlock Container = this.Node.Block as ContainerBlock;
		//			if (Container == null)
		//			{
		//				return this;
		//			}
		//
		//			if (this.FromChild == null || this.ToChild == null)
		//			{
		//				IBlockNode From = Container.Children.GetFirstVisibleChild(Window);
		//				if (From == null)
		//				{
		//					return this;
		//				}
		//				IBlockNode To = Container.Children.GetLastVisibleChild(Window);
		//				this.FromChild = From.Block.GetVisibleRange(Window);
		//				if (To != From)
		//				{
		//					this.ToChild = To.Block.GetVisibleRange(Window);
		//				}
		//				else
		//				{
		//					this.ToChild = this.FromChild;
		//				}
		//				return this;
		//			}
		//
		//			if (i == 3)
		//			{
		//				if (this.FromChild.Node.Prev != null)
		//				{
		//					this.FromChild = Container.Children.Head.Block.VRange.Clone();
		//				}
		//				if (this.ToChild.Node.Next != null)
		//				{
		//					this.ToChild = Container.Children.Tail.Block.VRange.Clone();
		//				}
		//				return this;
		//			}
		//
		//			int TopState = this.FromChild.Node.Block.Bounds.RelativeToRectY(Window);
		//			int BottomState = this.ToChild.Node.Block.Bounds.RelativeToRectY(Window);
		//
		//			if (TopState == 2 && BottomState == 4)
		//			{
		//				return this;
		//			}
		//
		//			if (TopState == 1)
		//			{
		//				if (BottomState == 1)
		//				{
		//					IBlockNode NewTop = this.ToChild.Node.FindNextVisible(Window);
		//					if (NewTop == null)
		//					{
		//						this.FromChild = null;
		//						this.ToChild = null;
		//						return this;
		//					}
		//					this.FromChild = NewTop.Block.GetVisibleRange(Window);
		//					IBlockNode NewBottom = Container.Children.GetLastVisibleChild(Window);
		//					this.ToChild = NewBottom.Block.GetVisibleRange(Window);
		//					return this;
		//				}
		//				else
		//				{
		//					
		//				}
		//
		//			}
		//
		//			return null;
		//		}
		#endregion
	}
}
