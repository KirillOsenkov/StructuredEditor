using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.Blocks
{
	public abstract partial class Block
	{
		/// <summary>
		/// Tries to set focus to the block.
		/// </summary>
		/// <param name="shouldRedraw">If the draw window should be repainted.</param>
		/// <remarks>The draw window is only repainted when the focus was set successfully</remarks>
		/// <returns>true if the focus was set, otherwise false.</returns>
		public virtual bool SetFocus()
		{
			return SetFocusCore();
		}

		protected bool SetFocusCore()
		{
			if (Root == null)
			{
				return false;
			}
			Block toFocus = FindFirstFocusableBlock();
			if (toFocus != null && toFocus.CanGetFocus)
			{
				if (this.ActionManager != null && this.ActionManager.RecordingTransaction != null)
				{
					// was: 
					//this.
					//    ActionManager.
					//    RecordingTransaction.
					//    AccumulatingAction.
					//    BlockToFocus = toFocus;
                    toFocus.SetFocus(SetFocusOptions.General);
				}
				else
				{
					return toFocus.MyControl.SetFocus();
				}
			}
			return false;
		}

		/// <summary>
		/// Tries to set the focus to the default control inside current block.
		/// Normally equivalent to SetFocus, but can be redefined to set the focus
		/// to arbitrary control.
		/// </summary>
		/// <param name="shouldRedraw">If the screen should be repainted.</param>
		public virtual void SetDefaultFocus()
		{
			Control toFocus = this.DefaultFocusableControl();
			if (toFocus != null)
			{
				toFocus.SetFocus();
			}
			else
			{
				SetFocus();
			}
		}

		public virtual Control DefaultFocusableControl()
		{
			return MyControl;
		}

		public virtual bool CanGetFocus
		{
			get
			{
				return MyControl != null
					&& MyControl.CanGetFocus;
			}
		}

		/// <summary>
		/// Searches this block depth-first top-to-bottom 
		/// and all children recursively
		/// </summary>
		/// <returns>First found focusable block (may be self). null if none found.</returns>
		public virtual Block FindFirstFocusableBlock()
		{
			if (CanGetFocus)
			{
				return this;
			}
			return null;
		}

		/// <summary>
		/// Searches this block depth-first bottom-to-top
		/// and all children recursively
		/// </summary>
		/// <returns>Last found focusable block (may be self). null if none found.</returns>
		public virtual Block FindLastFocusableBlock()
		{
			if (CanGetFocus)
			{
				return this;
			}
			return null;
		}

		/// <summary>
		/// Searches depth-first recursively, without considering the current block.
		/// Can only be non-null for containers.
		/// </summary>
		/// <returns>The first focusable child block of the current block, 
		/// if such a block exists, null otherwise.</returns>
		public virtual Block FindFirstFocusableChild()
		{
			return null;
		}

		public virtual Block FindLastFocusableChild()
		{
			return null;
		}

		public enum MoveFocusDirection
		{
			SelectPrev,
			SelectNext,
			SelectPrevInChain,
			SelectNextInChain,
			SelectParent
		}

		/// <summary>
		/// Puts focus to the next available block.
		/// </summary>
		/// <param name="shouldRedraw">If the screen should be repainted afterwards</param>
		public bool RemoveFocus()
		{
			return RemoveFocus(MoveFocusDirection.SelectNext);
		}

		/// <summary>
		/// Puts focus to some other block.
		/// </summary>
		/// <returns>true if successful, false if the method had no effect</returns>
		/// <remarks>Normally called before deleting a block.</remarks>
		public bool RemoveFocus(MoveFocusDirection direction)
		{
			Block neighbour = null;

			if (direction == MoveFocusDirection.SelectPrev
				|| direction == MoveFocusDirection.SelectPrevInChain)
			{
				if (direction == MoveFocusDirection.SelectPrev)
				{
					neighbour = this.FindPrevFocusableBlock();
				}
				else
				{
					neighbour = this.FindPrevFocusableBlockInChain();
				}
				
				if (neighbour == null)
				{
					// if there is no prev block, 
					// try to select next block
					neighbour = this.FindNextFocusableBlock();
				}
			}
			else if (direction == MoveFocusDirection.SelectNext
				|| direction == MoveFocusDirection.SelectNextInChain)
			{
				if (direction == MoveFocusDirection.SelectNext)
				{
					neighbour = this.FindNextFocusableBlock();
				}
				else
				{
					neighbour = this.FindNextFocusableBlockInChain();
				}
				
				if (neighbour == null)
				{
					// if there is no next block, 
					// try to select previous block
					neighbour = this.FindPrevFocusableBlock();
				}
			}

			if (neighbour == null)
			{
				// if toRemoveFocusFrom is the only block,
				// try to select parent
				neighbour = this.FindNearestFocusableParent();
			}

			if (neighbour != null)
			{
				// if found someone, select it
				neighbour.SetFocus();
			}

			return neighbour != null;
		}

		/// <summary>
		/// Searches a "brother" block upwards of startFrom,
		/// that is above startFrom and is focusable.
		/// If no such block is found, returns null.
		/// </summary>
		/// <param name="startFrom">Block, above which to search (upwards)</param>
		/// <returns>Focusable sibling block, if found; otherwise null.</returns>
		//public Block FindPrevFocusableSibling()
		//{
		//    Block current = this.Prev;
		//    while (current != null && !current.CanGetFocus)
		//    {
		//        current = current.Prev;
		//    }
		//    return current;
		//}

		/// <summary>
		/// Searches a "brother" block downwards of startFrom,
		/// that is below startFrom and is focusable.
		/// If no such block is found, returns null.
		/// </summary>
		/// <param name="startFrom">Block, down from which to search (downwards)</param>
		/// <returns>Focusable sibling block, if found; otherwise null.</returns>
		//public Block FindNextFocusableSibling()
		//{
		//    Block current = this.Next;
		//    while (current != null && !current.CanGetFocus)
		//    {
		//        current = current.Next;
		//    }
		//    return current;
		//}

		/// <summary>
		/// Searches for a previous block "deep"
		/// (includes children of previous blocks recursively)
		/// </summary>
		/// <returns>Focusable block if found; otherwise null</returns>
		public Block FindPrevFocusableBlock()
		{
			Block current = this.Prev;

			while (current != null)
			{
				Block foundFocusableBlock = current.FindLastFocusableBlock();
				if (foundFocusableBlock != null)
				{
					return foundFocusableBlock;
				}
				current = current.Prev;
			}

			return null;
		}

		/// <summary>
		/// Searches for a next block "deep"
		/// (includes children of following blocks recursively)
		/// </summary>
		/// <returns></returns>
		public Block FindNextFocusableBlock()
		{
			Block current = this.Next;

			while (current != null)
			{
				Block foundFocusableBlock = current.FindFirstFocusableBlock();
				if (foundFocusableBlock != null)
				{
					return foundFocusableBlock;
				}
				current = current.Next;
			}

			return null;
		}

		/// <summary>
		/// Searches for a previous focused block in the same parent, no recursion.
		/// </summary>
		/// <returns>Nearest previous sibling, doesn't go deep.</returns>
		public Block FindPrevFocusableSibling()
		{
			Block current = this.Prev;

			while (current != null)
			{
				if (current.CanGetFocus)
				{
					return current;
				}
				current = current.Prev;
			}

			return null;
		}

		/// <summary>
		/// Searches for the next focused block in the same parent, no recursion.
		/// </summary>
		/// <returns>Nearest next sibling, doesn't go deep.</returns>
		public Block FindNextFocusableSibling()
		{
			Block current = this.Next;

			while (current != null)
			{
				if (current.CanGetFocus)
				{
					return current;
				}
				current = current.Next;
			}

			return null;
		}
		/// <summary>
		/// Searches for a previous block "deep"
		/// (includes children of previous blocks recursively)
		/// </summary>
		/// <returns>Focusable block if found; otherwise null</returns>
		public Block FindPrevFocusableBlockInChain()
		{
			Block current = this.Prev;

			while (current != null)
			{
				Block foundFocusableBlock = current.FindLastFocusableBlock();
				if (foundFocusableBlock != null && foundFocusableBlock.CanGetFocus)
				{
					return foundFocusableBlock;
				}
				current = current.Prev;
			}

			if (this.Parent != null)
			{
				if (this.Parent.CanGetFocus)
				{
					return this.Parent;
				}
				else
				{
					return this.Parent.FindPrevFocusableBlockInChain();
				}
			}

			return null;
		}

		public virtual Block MoveFocusToPrevInChain()
		{
			Block prev = this.FindPrevFocusableBlockInChain();
			if (prev != null)
			{
				prev.SetFocus();
			}
			return prev;
		}

		public virtual Block MoveFocusToNextInChain()
		{
			Block next = this.FindNextFocusableBlockInChain();
			if (next != null)
			{
				next.SetFocus();
			}
			return next;
		}

		/// <summary>
		/// Searches for a next block "deep"
		/// (includes children of following blocks recursively)
		/// </summary>
		/// <returns></returns>
		public Block FindNextFocusableBlockInChain()
		{
			Block current = this.Next;

			while (current != null)
			{
				Block foundFocusableBlock = current.FindFirstFocusableBlock();
				if (foundFocusableBlock != null && foundFocusableBlock.CanGetFocus)
				{
					return foundFocusableBlock;
				}
				current = current.Next;
			}

			if (this.Parent != null)
			{
				return this.Parent.FindNextFocusableBlockInChain();
			}

			return null;
		}

		/// <summary>
		/// Searches for a first container block,
		/// which is focusable
		/// </summary>
		/// <returns>Nearest focusable parent or null if none found.</returns>
		public ContainerBlock FindNearestFocusableParent()
		{
			ContainerBlock current = this.Parent;
			while (current != null
				&& !(current.CanGetFocus))
			{
				current = current.Parent;
			}
			return current;
		}

		public virtual bool SetCursorToTheBeginning()
		{
			return this.SetFocusCore();
		}

		public virtual bool SetCursorToTheEnd()
		{
			return SetCursorToTheBeginning();
		}
	}
}