using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Actions
{
	/// <summary>
	/// A recordable Action for adding one or several blocks to a block
	/// </summary>
	public class AddBlocksAction : RootBlockAction
	{
		#region ctors

		internal AddBlocksAction(ContainerBlock parent, Block afterChild)
			: base(afterChild.Root)
		{
			Param.CheckNotNull(afterChild);

			AfterChild = afterChild;
			Parent = parent;
		}

		internal AddBlocksAction(ContainerBlock parent)
			: base(parent.Root)
		{
			Param.CheckNotNull(parent);

			AfterChild = null;
			Parent = parent;
		}

		#endregion

		private ContainerBlock Parent;
		private Block AfterChild;

		#region blocks to add

		private LinkedList<Block> list = new LinkedList<Block>();

		public void PrepareBlocks(Block block)
		{
			Param.CheckNotNull(block, "block");
			
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

		#endregion

		#region Execute

		protected override void ExecuteCore()
		{
			BlockActions.EnsureBlocksHangInTheAir(list);
			if (AfterChild != null)
			{
				Parent.Children.Append(AfterChild, list);
			}
			else
			{
				Parent.Children.Add(list);
			}
			
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
