using System.Collections.Generic;
using GuiLabs.Editor.Actions;
using GuiLabs.Utils;
using GuiLabs.Undo;
using System.Windows.Forms;

namespace GuiLabs.Editor.Blocks
{
	public abstract partial class Block
	{
		#region Append

		public virtual void AppendBlocks(IEnumerable<Block> blocksToAppend)
		{
			if (this.Parent == null && !CanAppendBlocks)
			{
				return;
			}

			if (this.ActionManager != null)
			{
				AddBlocksAction Action = new AddBlocksAction(this.Parent, this);
				Action.PrepareBlocks(blocksToAppend);
				this.ActionManager.RecordAction(Action);
			}
			else
			{
				this.Parent.Children.Append(this, blocksToAppend);
			}
		}

		public void AppendBlocks(params Block[] blocksToAppend)
		{
			AppendBlocks(blocksToAppend as IEnumerable<Block>);
		}

		private bool mCanAppendBlocks = true;
		public bool CanAppendBlocks
		{
			get
			{
				return mCanAppendBlocks;
			}
			set
			{
				mCanAppendBlocks = value;
			}
		}

		private bool mCanPrependBlocks = true;
		public bool CanPrependBlocks
		{
			get
			{
				return mCanPrependBlocks;
			}
			set
			{
				mCanPrependBlocks = value;
			}
		}

		#endregion

		#region Prepend

		/// <summary>
		/// Prepends a list of blocks before current block.
		/// Changes the Undo stack.
		/// </summary>
		/// <param name="blocksToPrepend">The order of blocksToPrepend is preserved.</param>
		public void PrependBlocks(IEnumerable<Block> blocksToPrepend)
		{
			if (this.Parent == null && !CanPrependBlocks)
			{
				return;
			}

			if (this.ActionManager != null)
			{
				IAction action = ActionFactory.PrependBlocks(this, blocksToPrepend);
				this.ActionManager.RecordAction(action);
			}
			else
			{
				this.Parent.Children.Prepend(this, blocksToPrepend);
			}
		}

		/// <summary>
		/// Prepends a list of blocks before current block.
		/// Changes the Undo stack.
		/// </summary>
		/// <param name="blocksToPrepend">The order of blocksToPrepend is preserved.</param>
		public void PrependBlocks(params Block[] blocksToPrepend)
		{
			PrependBlocks(blocksToPrepend as IEnumerable<Block>);
		}

		#endregion

		#region Replace

		public void Replace(Block toReplaceWith)
		{
			if (this.ActionManager != null)
			{
				IAction a = ActionFactory.ReplaceBlock(this, toReplaceWith);
				this.ActionManager.RecordAction(a);
			}
			else
			{
				this.Parent.Children.Replace(this, toReplaceWith);
			}
		}

		public void Replace(IEnumerable<Block> blocksToReplaceWith)
		{
			if (this.ActionManager != null)
			{
				IAction a = ActionFactory.ReplaceBlock(this, blocksToReplaceWith);
				this.ActionManager.RecordAction(a);
			}
			else
			{
				this.Parent.Children.Replace(this, blocksToReplaceWith);
			}
		}

		public void Replace(params Block[] blocksToReplaceWith)
		{
			Replace((IEnumerable<Block>)blocksToReplaceWith);
		}

		#endregion

		#region Delete

		/// <summary>
		/// Deletes the current block.
		/// Changes the Undo stack.
		/// </summary>
		public virtual void Delete()
		{
			BlockActions.DeleteBlock(this);
			//if (this.Parent == null)
			//{
			//    return;
			//}

			//if (this.Root != null && this.Root.ActionManager != null)
			//{
			//    RemoveBlocksAction Action = new RemoveBlocksAction(this.Parent);
			//    Action.PrepareBlocks(GetBlocksToDelete());
			//    this.Root.ActionManager.RecordAction(Action);
			//}
			//else
			//{
			//    this.Parent.Children.Delete(GetBlocksToDelete());
			//}
		}

		internal virtual void OnAfterDelete()
		{
			RaiseDeleted();
		}

		/// <summary>
		/// Override PrepareBlocksToDelete to return all blocks
		/// you want to be deleted.
		/// </summary>
		/// <example>
		/// Override this method like this to delete current block and next block:
		///		yield return this;
		///		yield return this.Next;
		/// </example>
		/// <remarks>
		/// By default, marks this block and the next block to be deleted.
		/// </remarks>
		/// <param name="deleteAction">A delete transaction to call PrepareBlocks on</param>
		public virtual IEnumerable<Block> GetBlocksToDelete()
		{
			yield return this;
			if (this.Next != null && this.Next is ISeparatorBlock)
			{
				yield return this.Next;
			}
		}

		#endregion

		#region Move

		public void Move(ContainerBlock newParent)
		{
			BlockActions.MoveBlocks(newParent, PrepareBlocksToMove());
		}

		public void MoveAfterBlock(Block afterChild)
		{
			BlockActions.MoveBlocksAfterBlock(PrepareBlocksToMove(), afterChild);
		}

		public void MoveBeforeBlock(Block beforeChild)
		{
			BlockActions.MoveBlocksBeforeBlock(PrepareBlocksToMove(), beforeChild);
		}

		public IEnumerable<Block> PrepareBlocksToMove()
		{
			yield return this;
			if (this.Next != null && this.Next is ISeparatorBlock)
			{
				yield return this.Next;
			}
		}

		#endregion

		#region MoveUp/MoveDown
		
		public void MoveUp()
		{
			if (!CanMoveUpDown)
			{
				return;
			}
			Block previous = this.Prev;
			if (previous == null) return;
			if (previous is ISeparatorBlock)
			{
				previous = this.GetNeighborBlock(-2);
				if (previous == null || (previous is ISeparatorBlock)) return;
			}
			if (previous.CanPrependBlocks)
			{
				MoveBeforeBlock(previous);
			}
		}

		public bool CanMoveUp
		{
			get
			{
				if (!CanMoveUpDown)
				{
					return false;
				}
				Block previous = this.Prev;
				if (previous == null) return false;
				if (previous is ISeparatorBlock)
				{
					previous = this.GetNeighborBlock(-2);
					if (previous == null || (previous is ISeparatorBlock)) return false;
				}
				return previous.CanPrependBlocks;
			}
		}
		
		public void MoveDown()
		{
			if (!CanMoveUpDown)
			{
				return;
			}
			Block next = this.Next;
			if (next == null) return;
			if (next is ISeparatorBlock)
			{
				next = this.GetNeighborBlock(3);
				if (next == null || !(next is ISeparatorBlock)) return;
			}
			if (next.CanAppendBlocks)
			{
				MoveAfterBlock(next);
			}
		}

		public bool CanMoveDown
		{
			get
			{
				if (!CanMoveUpDown)
				{
					return false;
				}
				Block next = this.Next;
				if (next == null) return false;
				if (next is ISeparatorBlock)
				{
					next = this.GetNeighborBlock(3);
					if (next == null || !(next is ISeparatorBlock)) return false;
				}
				return next.CanAppendBlocks;
			}
		}
		
		#endregion
		
		#region Clipboard

		public virtual void Cut()
		{
			CutCore();
		}

		protected void CutCore()
		{
			Copy();
			Delete();
		}

		public virtual void Copy()
		{
			CopyCore();
		}

		protected void CopyCore()
		{
			string toCopy = this.Serialize();
			System.Windows.Forms.Clipboard.SetData(Block.ClipboardFormat, toCopy);
		}

		public virtual void Paste()
		{
			PasteCore();
		}

		protected void PasteCore()
		{
			IEnumerable<Block> blocksInClipboard = BlockFactory.Instance.GetBlocksFromClipboard();
			if (blocksInClipboard != null && !Common.Empty(blocksInClipboard))
			{
				PasteBlocks(blocksInClipboard);
			}
		}

		public virtual void PasteBlocks(IEnumerable<Block> blocksToPaste)
		{
			AppendBlocks(blocksToPaste);
		}

		public void PasteBlocks(params Block[] blocksToPaste)
		{
			PasteBlocks((IEnumerable<Block>)blocksToPaste);
		}

		#endregion
	}
}
