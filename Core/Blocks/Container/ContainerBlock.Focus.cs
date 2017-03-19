using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.Blocks
{
	partial class ContainerBlock
	{
		public override Control DefaultFocusableControl()
		{
			Block toFocus = this.FindFirstFocusableBlock();
			if (toFocus != null && toFocus.MyControl != null && toFocus.MyControl.CanGetFocus)
			{
				return toFocus.MyControl;
			}
			else
			{
				return null;
			}
		}

		public override Block FindFirstFocusableBlock()
		{
			if (this.CanGetFocus)
			{
				return this;
			}
			else
			{
				return FindFirstFocusableChild();
			}
		}

		public override Block FindLastFocusableBlock()
		{
			Block result = FindLastFocusableChild();

			if (result != null)
			{
				return result;
			}
			else
			{
				if (CanGetFocus)
				{
					return this;
				}
				else
				{
					return null;
				}
			}
		}

		public override Block FindFirstFocusableChild()
		{
			if (!Visible)
			{
				return null;
			}

			foreach (Block child in this.Children)
			{
				if (child.Visible)
				{
					Block foundFocusableBlock = child.FindFirstFocusableBlock();
					if (foundFocusableBlock != null)
					{
						return foundFocusableBlock;
					}
				}
			}

			return null;
		}

		public override Block FindLastFocusableChild()
		{
			if (MyControl.Collapsed || !Visible)
			{
				return null;
			}

			foreach (Block child in this.Children.Reversed)
			{
				Block foundFocusableBlock = child.FindLastFocusableBlock();
				if (foundFocusableBlock != null)
				{
					return foundFocusableBlock;
				}
			}

			return null;
		}

		#region Old

		#region LastActiveBlock

		//private Block mLastActiveBlock;
		//public Block LastActiveBlock
		//{
		//    get
		//    {
		//        if (mLastActiveBlock == null)
		//        {
		//            mLastActiveBlock = DefaultFocusedBlock;
		//        }
		//        return mLastActiveBlock;
		//    }
		//    set
		//    {
		//        mLastActiveBlock = value;
		//    }
		//}

		#endregion

		#region Navigation

		//public override Block GetRightNeighbour()
		//{
		//    if (LastActiveBlock != null)
		//    {
		//        return LastActiveBlock;
		//    }
		//    else
		//    {
		//        return null; // base.GetRightNeighbour();
		//    }
		//}

		//public override Block GetFirstFocusableChild()
		//{
		//    Block Current = this.Children.Head;
		//    if (Current != null)
		//    {
		//        if (Current.Focusable)
		//        {
		//            return Current;
		//        }
		//        else
		//        {
		//            return Current.GetLowerNeighbour();
		//        }
		//    }
		//    else
		//    {
		//        return null;
		//    }
		//}

		#endregion

		//public virtual void HandleResizedChild(Block ResizedNode)
		//{
		//    const int Margin = 0;

		//    if (ResizedNode.Bounds.Size.X + 2 * Margin > this.Bounds.Size.X)
		//    {
		//        this.Bounds.Size.X = ResizedNode.Bounds.Size.X + 2 * Margin;
		//    }

		//    Block Current = ResizedNode.Next;

		//    while (Current != null)
		//    {
		//        Current.Move(0, Current.Prev.Bounds.Bottom + Margin - Current.Bounds.Location.Y);
		//        Current = Current.Next;
		//    }

		//    if (this.Children.Tail != null)
		//    {
		//        this.Bounds.Size.Y = Children.Tail.Bounds.Bottom + Margin - this.Bounds.Location.Y;
		//    }
		//    else
		//    {
		//        this.Layout();
		//    }

		//    if (Parent != null)
		//    {
		//        Parent.HandleResizedChild(this);
		//    }
		//}

		//public override IBlockRange GetVisibleRange(Rect Window)
		//{
		//    int i = Bounds.RelativeToRectY(Window);

		//    if (i == 3)
		//        return this.Range;

		//    if (i == 1 || i == 5)
		//        return null;

		//    IBlockRange ResultRange = new BlockRange();
		//    ResultRange.Node = this.Node;

		//    IBlockNode child;

		//    if (this.Children.Head != null && this.Children.Tail != null)
		//    {
		//        switch (i)
		//        {
		//            case 0:
		//                IBlockNode firstChild = Children.GetFirstVisibleChild(Window);
		//                IBlockNode lastChild = Children.GetLastVisibleChild(Window);
		//                if (firstChild != null && lastChild != null)
		//                {
		//                    ResultRange.FromChild = firstChild.Block.GetVisibleRange(Window);
		//                    if (firstChild != lastChild)
		//                    {
		//                        ResultRange.ToChild = lastChild.Block.GetVisibleRange(Window);
		//                    }
		//                    else
		//                    {
		//                        ResultRange.ToChild = ResultRange.FromChild;
		//                    }
		//                }
		//                break;
		//            case 2:
		//                child = Children.GetFirstVisibleChild(Window);
		//                if (child != null)
		//                {
		//                    ResultRange.FromChild = child.Block.GetVisibleRange(Window);
		//                    if (child.Next != null)
		//                    {
		//                        ResultRange.ToChild = this.Children.Tail.Block.Range;
		//                    }
		//                    else
		//                    {
		//                        ResultRange.ToChild = ResultRange.FromChild;
		//                    }
		//                }
		//                break;
		//            case 4:
		//                child = Children.GetLastVisibleChild(Window);
		//                if (child != null)
		//                {
		//                    ResultRange.ToChild = child.Block.GetVisibleRange(Window);
		//                    if (child.Prev != null)
		//                    {
		//                        ResultRange.FromChild = this.Children.Head.Block.Range;
		//                    }
		//                    else
		//                    {
		//                        ResultRange.FromChild = ResultRange.ToChild;
		//                    }
		//                }
		//                break;
		//        }
		//    }

		//    return ResultRange;
		//}

		//public override IBlockRange Range
		//{
		//    get
		//    {
		//        if (mRange == null)
		//        {
		//            mRange = new BlockRange();
		//            mRange.Node = this;
		//            mRange.FromChild = (this.Children.IsEmpty()) ? null : this.Children.Head.Range;
		//            mRange.ToChild = (this.Children.IsEmpty()) ? null : this.Children.Tail.Range;
		//        }
		//        return mRange;
		//    }
		//    protected set
		//    {
		//        mRange = value;
		//    }
		//}
		#endregion
	}
}
