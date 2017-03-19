using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Actions
{
	public class ActionFactory
	{
		#region Add

		public static AddBlocksAction AddBlock(
			ContainerBlock parentBlock,
			Block toAdd)
		{
			AddBlocksAction action = new AddBlocksAction(parentBlock);
			action.PrepareBlocks(toAdd);
			return action;
		}

		public static AddBlocksAction AddBlocks(
			ContainerBlock parentBlock,
			IEnumerable<Block> blocksToAdd)
		{
			AddBlocksAction action = new AddBlocksAction(parentBlock);
			action.PrepareBlocks(blocksToAdd);
			return action;
		}

		#endregion

		#region Append

		public static AddBlocksAction AppendBlock(
			Block afterBlock,
			Block toAdd)
		{
			AddBlocksAction action = new AddBlocksAction(
				afterBlock.Parent,
				afterBlock);
			action.PrepareBlocks(toAdd);
			return action;
		}

		public static AddBlocksAction AppendBlocks(
			Block afterBlock,
			params Block[] blocksToAdd)
		{
			AddBlocksAction action = new AddBlocksAction(
				afterBlock.Parent, afterBlock);
			action.PrepareBlocks(blocksToAdd);
			return action;
		}

		public static AddBlocksAction AppendBlocks(
			Block afterBlock,
			IEnumerable<Block> blocksToAdd)
		{
			AddBlocksAction action = new AddBlocksAction(
				afterBlock.Parent,
				afterBlock);
			action.PrepareBlocks(blocksToAdd);
			return action;
		}

		#endregion

		#region Prepend

		public static PrependBlocksAction PrependBlock(
			Block beforeBlock,
			Block toAdd)
		{
			PrependBlocksAction action = new PrependBlocksAction(beforeBlock.Parent, beforeBlock);
			action.PrepareBlocks(toAdd);
			return action;
		}

		public static PrependBlocksAction PrependBlocks(
			Block beforeBlock,
			IEnumerable<Block> blocksToAdd)
		{
			PrependBlocksAction action = new PrependBlocksAction(beforeBlock.Parent, beforeBlock);
			action.PrepareBlocks(blocksToAdd);
			return action;
		}

		#endregion

		#region Delete

		public static RemoveBlocksAction DeleteBlock(Block blockToDelete)
		{
			RemoveBlocksAction action = new RemoveBlocksAction(blockToDelete.Parent);
			action.PrepareBlocks(blockToDelete);
			return action;
		}

		public static RemoveBlocksAction DeleteBlocks(IEnumerable<Block> blocksToDelete)
		{
			Param.CheckNotNull(blocksToDelete, "blocksToDelete");
			Block first = Common.Head<Block>(blocksToDelete);
			Param.CheckNotNull(first, "first");
			if (first.ActionManager == null)
			{
				return null;
			}
			RemoveBlocksAction action = new RemoveBlocksAction(first.Parent);
			action.PrepareBlocks(blocksToDelete);
			return action;
		}

		public static RemoveBlocksAction DeleteBlocks(params Block[] blocksToDelete)
		{
			return DeleteBlocks((IEnumerable<Block>)blocksToDelete);
		}

		#endregion

		#region Replace

		public static ReplaceBlocksAction ReplaceBlock(
			Block toReplace,
			Block replaceWith)
		{
			ReplaceBlocksAction action = new ReplaceBlocksAction(toReplace);
			action.PrepareBlockToRemove(toReplace);
			action.PrepareBlockToAdd(replaceWith);
			return action;
		}

		public static ReplaceBlocksAction ReplaceBlock(
			Block toReplace,
			params Block[] toInsert)
		{
			return ReplaceBlock(toReplace, (IEnumerable<Block>)toInsert);
		}

		public static ReplaceBlocksAction ReplaceBlock(
			Block toReplace,
			IEnumerable<Block> toInsert)
		{
			ReplaceBlocksAction action = new ReplaceBlocksAction(toReplace);
			action.PrepareBlockToRemove(toReplace);
			action.PrepareBlockToAdd(toInsert);
			return action;
		}

		#endregion
	}
}
