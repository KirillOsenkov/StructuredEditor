using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Actions
{
	/// <summary>
	/// A recordable Action for adding several blocks before a block
	/// or to the beginning of the block
	/// </summary>
	public class PrependBlocksAction : RootBlockAction
	{
		#region ctors

		internal PrependBlocksAction(ContainerBlock parent, Block beforeChild)
			: base(beforeChild.Root)
		{
			Param.CheckNotNull(beforeChild, "beforeChild");
			BeforeChild = beforeChild;
			Parent = parent;
		}

		internal PrependBlocksAction(
			Block beforeChild,
			IEnumerable<Block> blocksToAdd
		)
			: base(beforeChild.Root)
		{
			Param.CheckNotNull(blocksToAdd, "blocksToAdd");
			Parent = beforeChild.Parent;
			foreach (Block b in blocksToAdd)
			{
				if (b != null)
				{
					list.AddLast(b);
				}
			}
		}

		#endregion

		private ContainerBlock Parent;
		private Block BeforeChild;

		#region list of blocks

		private System.Collections.Generic.LinkedList<Block> list = new LinkedList<Block>();

		public void PrepareBlocks(Block block)
		{
			if (block != null)
			{
				list.AddLast(block);
			}
		}

		public void PrepareBlocks(IEnumerable<Block> blocks)
		{
			foreach (Block block in blocks)
			{
				PrepareBlocks(block);
			}
		}

//		private IEnumerable<Block> ReversedList()
//		{
//			LinkedListNode<Block> current = this.list.Last;
//			while (current != null)
//			{
//				yield return current.Value;
//				current = current.Previous;
//			}
//		}

		#endregion

		#region Execute

		protected override void ExecuteCore()
		{
			BlockActions.EnsureBlocksHangInTheAir(list);
			Parent.Children.Prepend(BeforeChild, list);

			if (!BlockToFocusIsValid)
			{
				BlockToFocus = BlockActions.FindFirstFocusableBlock(list);
			}
		}

		#endregion

		#region UnExecute

		protected override void UnExecuteCore()
		{
			RootControl root = Root.MyRootControl;

			foreach (Block b in this.list)
			{
				if (root.IsFocusInsideControl(b.MyControl))
				{
					b.RemoveFocus();
				}
				Parent.Children.Delete(b);
			}
		}

		#endregion
	}
}
