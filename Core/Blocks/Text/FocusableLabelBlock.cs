using System;
using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.Blocks
{
	public class FocusableLabelBlock : LabelBlock
	{
		#region ctors

		public FocusableLabelBlock(string text)
			: base(text)
		{
			MyLabel.Focusable = true;
		}

		public FocusableLabelBlock()
			: this("")
		{
		}

		#endregion

		#region OnEvents

		#region Process keys
		/// <summary>
		/// Happens when the user presses Backspace key
		/// </summary>
		/// <returns>true if the KeyDown should raise KeyDown event further down the chain</returns>
		protected virtual void ProcessBackspace(System.Windows.Forms.KeyEventArgs e)
		{
			//if (this.Prev == null)
			//{
			//    TokenActions.AppendSecondLineToFirst(this);
			//}
			//else if (e.Control)
			//{
			//    if (this.Prev.Prev != null)
			//    {
			//        this.Prev.Prev.SetFocus(false);
			//    }
			//    BlockActions.Delete(this.Prev, this);
			//}
			//else
			//{
			//    TokenActions.SetCursorToTheEndOfPreviousToken(this);
			//}

			//e.Handled = true;
		}

		protected virtual void ProcessDelete(System.Windows.Forms.KeyEventArgs e)
		{
			//if (e.Shift)
			//{
			//    TokenActions.DeleteCurrentLine(this);
			//}
			//else if (e.Control)
			//{
			//    BlockActions.Delete(this, this.Next);
			//}
			//else if (this.Next == null)
			//{
			//    if (this.Prev == null)
			//    {
			//        TokenActions.DeleteCurrentLine(this);
			//    }
			//    else
			//    {
			//        TokenActions.AppendLineBelowToCurrentLine(this);
			//    }
			//}
			//else
			//{
			//    if (this.Prev == null)
			//    {
			//        TokenActions.SetCursorToTheBeginningOfNextToken(this);
			//    }
			//    else
			//    {
			//        TokenActions.DeleteSeparatorAndJoinNeighbours(this);
			//    }
			//}

			//e.Handled = true;
		}

		protected virtual void ProcessLeft(System.Windows.Forms.KeyEventArgs e)
		{
			//if (!e.Control || !TokenActions.SetFocusToPrevious<TokenSeparatorBlock>(this))
			//{
			//    Block prev = this.FindPrevFocusableBlock();
			//    if (prev != null)
			//    {
			//        prev.SetCursorToTheEnd(true);
			//    }
			//    else
			//    {
			//        e.Handled = false;
			//        return;
			//    }
			//}

			//e.Handled = true;
		}

		protected virtual void ProcessRight(System.Windows.Forms.KeyEventArgs e)
		{
			//if (!e.Control || !TokenActions.SetFocusToNext<TokenSeparatorBlock>(this))
			//{
			//    Block next = this.FindNextFocusableBlock();
			//    if (next != null)
			//    {
			//        next.SetCursorToTheBeginning(true);
			//    }
			//    else
			//    {
			//        e.Handled = false;
			//        return;
			//    }
			//}

			//e.Handled = true;
		}

		protected virtual void ProcessUp(System.Windows.Forms.KeyEventArgs e)
		{
			//if (this.Prev == null && this.Parent.Prev != null)
			//{
			//    if (this.Parent.Prev.SetCursorToTheBeginning(true))
			//    {
			//        e.Handled = true;
			//    }
			//    // TokenActions.SetCursorToTheBeginningOfLine((this.Parent.Prev as ContainerBlock).Children.Head);
			//}
		}

		protected virtual void ProcessHome(System.Windows.Forms.KeyEventArgs e)
		{
			//TokenActions.SetCursorToTheBeginningOfLine(this);
			//e.Handled = true;
		}

		protected virtual void ProcessEnd(System.Windows.Forms.KeyEventArgs e)
		{
			//TokenActions.SetCursorToTheEndOfLine(this);
			//e.Handled = true;
		}

		protected virtual void ProcessReturn(System.Windows.Forms.KeyEventArgs e)
		{
			//TokenActions.InsertNewLineFromCurrent(this, TokenFactory.CreateNewLine());
			//e.Handled = true;
		}

		protected virtual void ProcessTab(System.Windows.Forms.KeyEventArgs e)
		{

		}

		#endregion

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Up:
					ProcessUp(e);
					break;
				case System.Windows.Forms.Keys.Left:
					ProcessLeft(e);
					break;
				case System.Windows.Forms.Keys.Right:
					ProcessRight(e);
					break;
				case System.Windows.Forms.Keys.Home:
					ProcessHome(e);
					break;
				case System.Windows.Forms.Keys.End:
					ProcessEnd(e);
					break;
				case System.Windows.Forms.Keys.Back:
					ProcessBackspace(e);
					break;
				case System.Windows.Forms.Keys.Delete:
					ProcessDelete(e);
					break;
				case System.Windows.Forms.Keys.Return:
					ProcessReturn(e);
					break;
				case System.Windows.Forms.Keys.Tab:
					ProcessTab(e);
					break;
				default:
					break;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "FocusableLabelBlock";
		}

		#endregion
	}
}
