using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Actions
{
	public class RemoveBlocksAction : RootBlockAction
	{
		#region ctors

		internal RemoveBlocksAction(ContainerBlock parent)
			: base(parent.Root)
		{
			Parent = parent;
		}

		#endregion

		private ContainerBlock Parent;
		private Block AfterChild;

		#region blocks to remove

		private System.Collections.Generic.LinkedList<Block> list = new LinkedList<Block>();

		/// <summary>
		/// Adds a block to be deleted to the internal candidate list
		/// </summary>
		/// <param name="Block">Block to delete - can be null</param>
		public void PrepareBlocks(Block block)
		{
			if (block == null) return;

			list.AddLast(block);
			
			// important: if we haven't received a parent from constructor, just get it here
			if (Parent == null && block.Parent != null)
			{
				this.Parent = block.Parent;
			}
		}

		public void PrepareBlocks(params Block[] blocks)
		{
			PrepareBlocks((IEnumerable<Block>)blocks);
		}

		public void PrepareBlocks(IEnumerable<Block> blocksToDelete)
		{
			foreach (Block block in blocksToDelete)
			{
				PrepareBlocks(block);
			}
		}

		#endregion

		#region Execute

		protected override void ExecuteCore()
		{
			if (this.list.First != null && this.list.First.Value != null)
			{
				AfterChild = this.list.First.Value.Prev;
			}
			else
			{
				AfterChild = null;
			}

			foreach (Block b in this.list)
			{
				if (Root.MyRootControl.IsFocusInsideControl(b.MyControl))
				{
					b.RemoveFocus();
				}
				Parent.Children.Delete(b);
			}
		}

		#endregion

		#region UnExecute

		protected override void UnExecuteCore()
		{
			if (AfterChild == null)
			{
				Parent.Children.Prepend(list);
			}
			else
			{
				Parent.Children.Append(AfterChild, list);
			}

			if (!BlockToFocusAfterUndoIsValid)
			{
				BlockToFocusAfterUndo = BlockActions.FindFirstFocusableBlock(list);
			}
		}

		#endregion
	}
}
