using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Public interface

		public void SetText(string newValue)
		{
			if (newValue == Text || newValue == null)
			{
				return;
			}

			InsertTextAction action = new InsertTextAction(
				this, newValue, 0, this.TextLength);

			RunAction(action);

			//using (RedrawAccumulator a = new RedrawAccumulator(this.Root, options.Redraw))
			//{
			//    int oldCaretPosition = this.CaretPosition;
			//    this.Text = newValue;
			//    this.SetCaretPosition(oldCaretPosition);
			//}
		}

		public void InsertText(string textToInsert)
		{
			InsertTextAction action = new InsertTextAction(
				this, textToInsert, this.SelectionStart, this.SelectionLength);

			RunAction(action);

			//string oldText = this.Text;
			//this.BeforeTextChanged();
			//InsertTextCore(
			//    this, 
			//    textToInsert, 
			//    this.SelectionStart, 
			//    this.SelectionLength);
			//this.AfterTextChanged();
			//RaiseTextChanged(oldText, this.Text);
		}

		public void DeleteSelection()
		{
			DeleteTextAction action = new DeleteTextAction(
				this, this.SelectionStart, this.SelectionLength);

			RunAction(action);
		}

		#endregion

		#region Private implementation

		private static void InsertTextCore(
			TextBox host, 
			string textToInsert,
			int selectionStart,
			int selectionLength)
		{
			// delete if overwriting something
			if (selectionLength > 0)
			{
				DeleteSelectionCore(host, selectionStart, selectionLength);
			}

			// modify the string
			if (selectionStart == host.DynamicString.Length)
			{
				host.DynamicString.Append(textToInsert);
			}
			else
			{
				host.DynamicString.Insert(selectionStart, textToInsert);
			}

			// update caret and selection
			host.SetCaretPosition(selectionStart + textToInsert.Length);
		}

		private static void DeleteSelectionCore(
			TextBox host,
			int selectionStart,
			int selectionLength)
		{
			if (selectionLength <= 0)
			{
				return;
			}

			// modify the string
			host.DynamicString.Remove(selectionStart, selectionLength);

			// update caret and selection
			host.SetCaretPosition(selectionStart);
		}

		#endregion
	}
}