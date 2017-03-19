using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.Blocks
{
	public static class TokenActions
	{
		//public static void SetFocusToTheBeginningOfToken(Block token)
		//{
		//    if (token == null)
		//    {
		//        return;
		//    }

		//    token.SetCursorToTheBeginning();
		//    token.SetFocus(true);
		//}

		//public static void SetFocusToTheEndOfToken(Block token)
		//{
		//    if (token == null)
		//    {
		//        return;
		//    }

		//    token.SetCursorToTheEnd();
		//    token.SetFocus(true);
		//}

		public static void SetCursorToTheEndOfPreviousToken(Block block)
		{
			Block prev = block.FindPrevFocusableBlock();
			if (prev != null)
			{
				prev.SetCursorToTheEnd(true);
			}
			//else
			//{
			//    ContainerBlock parent = block.Parent;
			//    if (parent != null)
			//    {
			//        Block prevParent = parent.Prev;
			//        if (prevParent != null)
			//        {
			//            prevParent.SetCursorToTheEnd();
			//            Block toFocus = prevParent.FindLastFocusableBlock();
			//        }
			//    }
			//}
		}

		public static void SetCursorToTheBeginningOfNextToken(Block block)
		{
			Block next = block.FindNextFocusableBlockInChain();
			if (next != null)
			{
				next.SetCursorToTheBeginning(true);
			}
		}

		public static void SetCursorToTheBeginningOfLine(Block currentBlock)
		{
			if (currentBlock == null)
			{
				return;
			}

			ContainerBlock parent = currentBlock.Parent;
			if (parent == null)
			{
				return;
			}

			Block first = parent.FindFirstFocusableBlock();
			if (first == null)
			{
				return;
			}

			first.SetCursorToTheBeginning(true);
		}

		public static void SetCursorToTheEndOfLine(Block currentBlock)
		{
			if (currentBlock == null)
			{
				return;
			}

			ContainerBlock parent = currentBlock.Parent;
			if (parent == null)
			{
				return;
			}

			Block last = parent.FindLastFocusableBlock();
			if (last == null)
			{
				return;
			}

			last.SetCursorToTheEnd(true);
		}

		public static bool SetFocusToPrevious<T>(Block current)
			where T : Block
		{
			while (current != null)
			{
				current = current.FindPrevFocusableBlock();
				if (current != null && current is T)
				{
					current.SetFocus(true);
					return true;
				}
			}
			return false;
		}

		public static bool SetFocusToNext<T>(Block current)
			where T : Block
		{
			while (current != null)
			{
				current = current.FindNextFocusableBlock();
				if (current != null && current is T)
				{
					current.SetFocus(true);
					return true;
				}
			}
			return false;
		}

		public static void DeleteSeparatorAndJoinNeighbours(Block separator)
		{
			TokenBlock prev = separator.Prev as TokenBlock;
			TokenBlock next = separator.Next as TokenBlock;

			if (separator != null
				&& separator.Root != null
				&& prev != null
				&& next != null
			)
			{
				using (ActionBuilder a = new ActionBuilder(separator.Root))
				{
					a.RenameItem(prev, prev.Text + next.Text);
					a.DeleteBlock(separator).BlockToFocus = prev;
					a.DeleteBlock(next);
					a.Run();
				}
			}
		}

		public static void AppendLineBelowToCurrentLine(Block higherSeparator)
		{
			if (higherSeparator == null
				|| higherSeparator.Parent == null
				|| higherSeparator.Parent.Next == null
				|| higherSeparator.Next != null)
			{
				return;
			}

			ContainerBlock newParent = higherSeparator.Parent;
			ContainerBlock oldParent = newParent.Next as ContainerBlock;
			Block newFocus = null;

			if (oldParent == null)
			{
				return;
			}
			else
			{
				newFocus = oldParent.FindFirstFocusableBlock();
			}

			ActionBuilder a = new ActionBuilder(higherSeparator.Root);

			a.DeleteBlock(higherSeparator);

			foreach (Block child in oldParent.Children)
			{
				a.MoveBlock(newParent, child);
			}

			a.DeleteBlock(oldParent);

			a.RunWithoutRedraw();

			if (newFocus != null)
			{
				newFocus.SetFocus(true);
			}
		}

		public static void AppendSecondLineToFirst(Block lowerSeparator)
		{
			if (lowerSeparator == null
				|| lowerSeparator.Parent == null
				|| lowerSeparator.Parent.Prev == null
				|| lowerSeparator.Prev != null)
			{
				return;
			}

			ContainerBlock oldParent = lowerSeparator.Parent;
			ContainerBlock newParent = oldParent.Prev as ContainerBlock;
			Block newFocus = null;

			if (newParent == null)
			{
				return;
			}
			else
			{
				newFocus = newParent.FindLastFocusableBlock();
			}

			ActionBuilder a = new ActionBuilder(lowerSeparator.Root);

			if (lowerSeparator.Next != null)
			{
				foreach (Block child in oldParent.Children)
				{
					if (child != lowerSeparator)
					{
						a.MoveBlock(newParent, child);
					}
				}
			}

			a.DeleteBlock(oldParent);
			
			a.RunWithoutRedraw();

			if (newFocus != null)
			{
				newFocus.SetFocus(true);
			}
		}

		public static void DeleteCurrentLine(Block current)
		{
			if (current == null)
			{
				return;
			}

			ContainerBlock parent = current.Parent;
			if (parent == null)
			{
				return;
			}

			if (parent.Prev == null && parent.Next == null)
			{
				return;
			}

			current.Parent.Delete();
		}

		public static void InsertNewLineFromCurrent(Block current, Block newLine)
		{
			if (current == null)
			{
				return;
			}

			TokenLineBlock parent = current.Parent as TokenLineBlock;
			if (parent == null)
			{
				return;
			}

			if (current.Prev == null && current.Next != null)
			{
				BlockActions.Prepend(parent, newLine);
			}
			else
			{
				parent.AppendBlocks(newLine);
			}
		}
	}
}
