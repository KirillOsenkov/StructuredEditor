using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	public class TokenBlock : TextBoxBlock, IToken
	{
		#region ctors

		public TokenBlock()
			: base(8)
		{
			Init();
		}

		public TokenBlock(int minWidth)
			: base(minWidth)
		{
			Init();
		}

		public TokenBlock(string initialText)
			: base(8)
		{
			Init();
			this.SetTextWithoutSideEffects(initialText);
		}

		private void Init()
		{
			MyTextBox.CharFilters.Add(CommonCharFilters.AcceptNoWhitespace);
			MyTextBox.Stretch = StretchMode.None;
		}

		#endregion

		public TokenLineBlock ParentLine
		{
			get
			{
				return Parent as TokenLineBlock;
			}
		}

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				Block next = this.Next;
				if (MyTextBox.CaretIsAtEnd && next != null)
				{
					if (next.Next != null)
					{
						TokenActions.DeleteSeparatorAndJoinNeighbours(next);
					}
					else
					{
						TokenActions.AppendLineBelowToCurrentLine(next);
					}
				}
				e.Handled = true;
				//if (MyTextBox.CaretPosition == MyTextBox.Text.Length
				//    && this.Next != null
				//    && this.Next.Next != null)
				//{
				//    using (ActionBuilder a = new ActionBuilder(this.Root))
				//    {
				//        if (this.Next is SpaceBlock && this.Next.Next is TextBoxBlock)
				//        {
				//            string textToAppend = ((TextBoxBlock)this.Next.Next).Text;
				//            a.DeleteBlock(this.Next);
				//            a.DeleteBlock(this.Next.Next);
				//            a.RenameItem(this.MyTextBox, this.Text + textToAppend);
				//        }
				//        a.Run();
				//    }
				//}
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				Block prev = this.Prev;
				if (MyTextBox.CaretIsAtBeginning && prev != null)
				{
					if (prev.Prev != null)
					{
						TokenActions.DeleteSeparatorAndJoinNeighbours(prev);
					}
					else
					{
						TokenActions.AppendSecondLineToFirst(prev);
					}
				}
				e.Handled = true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				// ISeparatorToken next = this.Next as ISeparatorToken;
				if (MyTextBox.CaretIsAtEnd && this.Next != null)
				{
					TokenActions.InsertNewLineFromCurrent(this.Next, TokenFactory.CreateNewLine());
					e.Handled = true;
				}
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Home)
			{
				TokenActions.SetCursorToTheBeginningOfLine(this);
				e.Handled = true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.End)
			{
				TokenActions.SetCursorToTheEndOfLine(this);
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);
			if (e.KeyChar == ' ')
			{
				if (MyTextBox.CaretPosition == MyTextBox.Text.Length)
				{
					Block next = this.Next;
					if (next != null && next is SpaceBlock)
					{
						next.SetFocus(true);
					}
				}
				else if (MyTextBox.CaretPosition > 0)
				{
					string TextBeforeCaret = MyTextBox.TextBeforeCaret;
					string TextAfterCaret = MyTextBox.TextAfterCaret;

					using (ActionBuilder a = new ActionBuilder(this.Root))
					{
						a.RenameItem(MyTextBox, TextBeforeCaret);
						a.AppendBlocks(
							this,
							new TokenSeparatorBlock(), 
							new TokenBlock(TextAfterCaret)
						);
						a.Run();
					}
				}
			}
		}

		protected override void OnTextIsEmpty(string oldText)
		{
			if (this.Text == "")
			{
				this.Delete();
			}
		}

		#endregion

		private static TokenBlockFactory mTokenFactory = new TokenBlockFactory();
		public virtual TokenBlockFactory TokenFactory
		{
			get
			{
				return mTokenFactory;
			}
		}
	}
}
