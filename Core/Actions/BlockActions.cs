using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Undo;
using System;

namespace GuiLabs.Editor.Actions
{
	public static class BlockActions
	{
		#region Move

		public static void MoveBlocks(ContainerBlock newParent, IEnumerable<Block> blocksToMove)
		{
            using (newParent.Transaction())
			{
				Delete(blocksToMove);
				newParent.Add(blocksToMove);
			}
		}

		public static void CopyBlocksAfterBlock(IEnumerable<Block> blocksToCopy, Block afterChild)
		{
			IEnumerable<Block> newBlocks = BlockActions.Clone(blocksToCopy);
			afterChild.AppendBlocks(newBlocks);
		}

		public static void CopyBlocksBeforeBlock(IEnumerable<Block> blocksToCopy, Block beforeChild)
		{
			IEnumerable<Block> newBlocks = BlockActions.Clone(blocksToCopy);
			beforeChild.PrependBlocks(newBlocks);
		}

		public static void MoveBlocksAfterBlock(IEnumerable<Block> blocksToMove, Block afterChild)
		{
			using (afterChild.Transaction())
			{
				Delete(blocksToMove);
				afterChild.AppendBlocks(blocksToMove);
				Common.Head(blocksToMove).SetFocus(SetFocusOptions.General, afterChild.ActionManager);
			}
		}

		public static void MoveBlocksBeforeBlock(IEnumerable<Block> blocksToMove, Block beforeChild)
		{
			using (beforeChild.Transaction())
			{
				Block toFocus = Common.Head(blocksToMove);
				Delete(blocksToMove);
				beforeChild.PrependBlocks(blocksToMove);
				toFocus.SetFocus(SetFocusOptions.General);
			}
		}

		public static void MoveBlock(ContainerBlock newParent, Block blockToMove)
		{
			using (newParent.Transaction())
			{
				blockToMove.Delete();
				newParent.AppendBlocks(blockToMove);
			}
		}

		#endregion

		public static void Prepend(Block beforeBlock, params Block[] blocksToInsert)
		{
			if (beforeBlock == null
				|| beforeBlock.Parent == null
				|| blocksToInsert == null
				|| blocksToInsert.Length == 0)
			{
				return;
			}

			beforeBlock.PrependBlocks(blocksToInsert);
			//ActionBuilder a = new ActionBuilder(beforeBlock.Root);
			//a.PrependBlocks(beforeBlock, blocksToInsert);
			//a.Run();
		}

		#region Delete

		public static void DeleteBlock(Block block)
		{
			if (block == null || block.Parent == null)
			{
				return;
			}

			if (block.Root != null && block.Root.ActionManager != null)
			{
				RemoveBlocksAction action = new RemoveBlocksAction(block.Parent);
				action.PrepareBlocks(block.GetBlocksToDelete());
				block.Root.ActionManager.RecordAction(action);
			}
			else
			{
				block.Parent.Children.Delete(block.GetBlocksToDelete());
			}
		}

		public static void Delete(params Block[] blocksToDelete)
		{
			Delete((IEnumerable<Block>)blocksToDelete);
		}

		public static void Delete(IEnumerable<Block> blocksToDelete)
		{
			if (blocksToDelete == null)
			{
				return;
			}
			Block first = Common.Head<Block>(blocksToDelete);
			if (first == null || first.Parent == null)
			{
				return;
			}
			if (first.ActionManager == null)
			{
				first.Parent.Children.Delete(blocksToDelete);
			}
			else
			{
				IAction a = ActionFactory.DeleteBlocks(blocksToDelete);
				first.ActionManager.RecordAction(a);
			}
		}

		#endregion

		/// <summary>
		/// Makes a copy of each block in the list and returns the list of copies.
		/// </summary>
		/// <param name="blocksToClone">A list of blocks to copy.</param>
		/// <remarks>Calls Clone for each block in the blocksToClone list.</remarks>
		/// <returns>A list of copies.</returns>
		public static IEnumerable<Block> Clone(IEnumerable<Block> blocksToClone)
		{
			foreach (Block block in blocksToClone)
			{
				yield return block.Clone();
			}
		}

		#region Sanity checks

		public static bool AreBlocksHangingInTheAir(params Block[] blocks)
		{
			return AreBlocksHangingInTheAir((IEnumerable<Block>)blocks);
		}

		public static bool AreBlocksHangingInTheAir(IEnumerable<Block> blocks)
		{
			foreach (Block b in blocks)
			{
				if (b.Root != null)
				{
					return false;
				}
			}
			return true;
		}

		public static void EnsureBlocksHangInTheAir(params Block[] blocks)
		{
			EnsureBlocksHangInTheAir((IEnumerable<Block>)blocks);
		}
		
		public static void EnsureBlocksHangInTheAir(IEnumerable<Block> blocks)
		{
			if (!AreBlocksHangingInTheAir(blocks))
			{
				throw new Exception("The blocks that are being added cannot belong to any tree.");
			}
		}

		#endregion

		/// <summary>
		/// Recursively searches for a focusable block 
		/// within the list (including children of each block).
		/// </summary>
		/// <param name="blocks">The list of all blocks to search</param>
		/// <returns>First block from the list which either itself can get focus
		/// or has a focusable child</returns>
		public static Block FindFirstFocusableBlock(IEnumerable<Block> blocks)
		{
			foreach (Block b in blocks)
			{
				Block found = b.FindFirstFocusableBlock();
				if (found != null)
				{
					return b;
				}
			}
			return null;
		}
	}
}
