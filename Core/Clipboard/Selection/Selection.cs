using System;
using System.Collections.Generic;
using GuiLabs.Canvas;

namespace GuiLabs.Editor.Blocks
{
	public class Selection
	{
		#region Selection Start and End

		private Block mSelectionStart;
		public Block SelectionStart
		{
			get { return mSelectionStart; }
			set { mSelectionStart = value; }
		}

		private Block mSelectionEnd;
		public Block SelectionEnd
		{
			get { return mSelectionEnd; }
			set { mSelectionEnd = value; }
		}

		public void Clear()
		{
			MouseStart = null;
			MouseEnd = null;
			SelectionEnd = null;
			SelectionStart = null;
		}

		#endregion

		#region Mouse Position

		private Block mMouseStart;
		public Block MouseStart
		{
			get { return mMouseStart; }
			set { mMouseStart = value; }
		}

		private Block mMouseEnd;
		public Block MouseEnd
		{
			get { return mMouseEnd; }
			set { mMouseEnd = value; }
		}

		#endregion

		#region Selection Define

		public bool DefineSelectionBlocks()
		{
			if (MouseStart.Parent != MouseEnd.Parent)
			{
				List<Block> ListStart = ParentList(MouseStart);
				List<Block> ListEnd = ParentList(MouseEnd);
				bool CanFind = FindCommonParent(ListStart, ListEnd);
				if (!CanFind)
				{
					return false;
				}
			}
			else
			{
				SelectionEnd = MouseEnd;
				SelectionStart = MouseStart;
			}
			if (!CanBeSelected(SelectionEnd) || !CanBeSelected(SelectionStart)) return false;
			bool NeedSwap = SelectBlocksOrder();
			if (NeedSwap)
			{
				Block tmp = SelectionStart;
				SelectionStart = SelectionEnd;
				SelectionEnd = tmp;
			}
			return true;
		}

		private bool CanBeSelected(Block block)
		{
			if (!block.CanBeSelected)
			{
				while (!block.CanBeSelected)
				{
					if (block.Parent != null) block = block.Parent;
				}
				block.MyControl.SetFocus();
				return false;
			}
			else
			{
				return true;
			}
		}

		private List<Block> ParentList(Block SelBlock)
		{
			List<Block> BlockList = new List<Block>();
			while (SelBlock != null)
			{
				BlockList.Add(SelBlock);
				SelBlock = SelBlock.Parent;
			}
			BlockList.Reverse();
			return BlockList;
		}

		private bool FindCommonParent(List<Block> ListStart, List<Block> ListEnd)
		{
			for (int i = 0; i < Math.Min(ListStart.Count, ListEnd.Count); i++)
			{
				if (ListStart[i] != ListEnd[i])
				{
					SelectionEnd = ListEnd[i];
					SelectionStart = ListStart[i];
					return true;
				}
			}
			return false;
		}

		private bool SelectBlocksOrder()
		{
			Block tmp = SelectionStart;
			while (tmp != null)
			{
				if (tmp == SelectionEnd) return false;
				if (tmp.Next != null)
				{
					tmp = tmp.Next;
				}
				else
				{
					return true;
				}
			}
			return true;
		}

		public void FindSelection(Block mouseStart, Block mouseEnd)
		{
			if (mouseEnd != null && mouseStart != null && mouseEnd != mouseStart)
			{
				MouseEnd = mouseEnd;
				MouseStart = mouseStart;
				if (DefineSelectionBlocks())
				{
					RecalcBounds();
				}
			}
		}

		#endregion

		#region Bounds

		private void RecalcBounds()
		{
			Bounds.Location.Set(SelectionStart.MyControl.Bounds.Location);
			Bounds.Size.Set(
				Math.Max(
					SelectionEnd.MyControl.Bounds.Size.X,
					SelectionStart.MyControl.Bounds.Size.X),
				SelectionEnd.MyControl.Bounds.Location.Y
				- SelectionStart.MyControl.Bounds.Location.Y
				+ SelectionEnd.MyControl.Bounds.Size.Y);
		}

		private Rect mBounds = new Rect();
		public Rect Bounds
		{
			get
			{
				return mBounds;
			}
		}

		#endregion
	}
}
