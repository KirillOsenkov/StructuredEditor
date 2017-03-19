using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Actions
{
	public class ReplaceBlocksAction : RootBlockAction
	{
		#region ctor

		public ReplaceBlocksAction (Block afterChild)
			: base(afterChild.Root)
		{
			Parent = afterChild.Parent;
			AddAction = new AddBlocksAction(afterChild.Parent, afterChild);
			RemoveAction = new RemoveBlocksAction(Parent);
		}

		#endregion

		private AddBlocksAction AddAction;
		private RemoveBlocksAction RemoveAction;
		private ContainerBlock Parent;

		public void PrepareBlockToRemove(Block block)
		{
			RemoveAction.PrepareBlocks(block);
		}

		public void PrepareBlockToAdd(Block block)
		{
			AddAction.PrepareBlocks(block);
		}

		public void PrepareBlockToAdd(IEnumerable<Block> blocks)
		{
			AddAction.PrepareBlocks(blocks);
		}

		protected override void ExecuteCore()
		{
			AddAction.BlockToFocus = this.BlockToFocus;
			AddAction.Execute();
			RemoveAction.Execute();
		}

		protected override void UnExecuteCore()
		{
			RemoveAction.BlockToFocusAfterUndo = this.BlockToFocusAfterUndo;
			RemoveAction.UnExecute();
			AddAction.UnExecute();
		}
	}
}
