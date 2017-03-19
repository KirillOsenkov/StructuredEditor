using System;
using GuiLabs.Undo;

namespace GuiLabs.Canvas.Controls
{
	public delegate void TextBoxChangePendingHandler(TextBox sender, TextBoxChangeEventArgs e);

	public class TextBoxChangeEventArgs : EventArgs
	{
		public bool Handled { get; set; }
		public TextBox.TextBoxAction Action { get; set; }
		public TextBox Textbox { get; set; }
	}

	public partial class TextBox
	{
		public event TextBoxChangePendingHandler TextBoxChangePending;

		/// <summary>
		/// Sends an event to clients telling them an action is ready
		/// to be executed. If they process the action, the textbox
		/// doesn't have to execute the action itself,
		/// so AskClientsToProcessAction returns true.
		/// If noone cares about the action, the textbox should
		/// execute the action itself, so the default value is false.
		/// </summary>
		/// <param name="action">An action which was just recorded and
		/// is about to be executed either by the clients
		/// or by the textbox itself.</param>
		/// <returns>true if the action has already been processed, 
		/// false otherwise (the textbox needs to process the action itself)</returns>
		private bool AskClientsToProcessAction(TextBoxAction action)
		{
			if (TextBoxChangePending != null)
			{
				TextBoxChangeEventArgs e = new TextBoxChangeEventArgs();
				e.Action = action;
				e.Handled = false;
				e.Textbox = this;

				TextBoxChangePending(this, e);

				return e.Handled;
			}

			return false;
		}

		private void RunAction(TextBoxAction action)
		{
			if (!action.WillDoNothing() && !AskClientsToProcessAction(action))
			{
				action.Execute();
			}
		}
	}

	public partial class TextBox
	{
		public abstract class TextBoxAction : AbstractAction
		{
			#region ctors

			public TextBoxAction(TextBox hostTextBox)
			{
				Textbox = hostTextBox;
			}

			#endregion

			public TextBox Textbox { get; set; }

			public override void Execute()
			{
				if (!CanExecute())
				{
					return;
				}

				string oldText = Textbox.Text;

				ExecuteCore();
				ExecuteCount++;

				Textbox.AfterTextChanged();
				Textbox.RaiseTextChanged(oldText, Textbox.Text);
			}

			public override void UnExecute()
			{
				if (!CanUnExecute())
				{
					return;
				}

				string oldText = Textbox.Text;

				UnExecuteCore();
				ExecuteCount--;

				Textbox.AfterTextChanged();
				Textbox.RaiseTextChanged(oldText, Textbox.Text);
			}

			public abstract bool WillDoNothing();
		}

		public class InsertTextAction : TextBoxAction
		{
			#region ctors

			public InsertTextAction(
				TextBox hostTestBox, 
				string text,
				int selStart,
				int selLength
			)
				: base(hostTestBox)
			{
				selectionStart = selStart;
				selectionLength = selLength;
				textToInsert = text;
				deletedText = Textbox.Text.Substring(selectionStart, selectionLength);
			}

			#endregion

			private int selectionStart;
			private int selectionLength;
			private string textToInsert;
			private string deletedText;

			protected override void ExecuteCore()
			{
				InsertTextCore(Textbox, textToInsert, selectionStart, selectionLength);
			}

			protected override void UnExecuteCore()
			{
				InsertTextCore(Textbox, deletedText, selectionStart, textToInsert.Length);
			}

			public override bool WillDoNothing()
			{
				return textToInsert == deletedText;
			}

			public override bool TryToMerge(IAction followingAction)
			{
				InsertTextAction next = followingAction as InsertTextAction;
				if (next != null && next.Textbox == this.Textbox)
				{
					if (next.selectionStart == this.selectionStart + textToInsert.Length)
					{
						this.selectionLength += next.selectionLength;
						this.deletedText += next.deletedText;
						this.textToInsert += next.textToInsert;
						next.Execute();
						return true;
					}
					if (next.selectionStart + next.selectionLength == this.selectionStart)
					{
						this.selectionStart -= next.selectionLength;
						this.selectionLength += next.selectionLength;
						this.deletedText = next.deletedText + this.deletedText;
						this.textToInsert = next.textToInsert + this.textToInsert;
						next.Execute();
						return true;
					}
				}
				return false;
			}
		}

		public class DeleteTextAction : TextBoxAction
		{
			#region ctor

			public DeleteTextAction(
				TextBox hostTextBox, 
				int selStart, 
				int selLength
			) 
				: base(hostTextBox)
			{
				selectionStart = selStart;
				selectionLength = selLength;
				deletedText = Textbox.Text.Substring(selectionStart, selectionLength);
			}

			#endregion

			private int selectionStart;
			private int selectionLength;
			private string deletedText;

			private int selectionEnd
			{
				get
				{
					return selectionStart + selectionLength;
				}
			}

			protected override void ExecuteCore()
			{
				DeleteSelectionCore(Textbox, selectionStart, selectionLength);
			}

			protected override void UnExecuteCore()
			{
				InsertTextCore(Textbox, deletedText, selectionStart, 0);
			}

			public override bool WillDoNothing()
			{
				return selectionLength <= 0;
			}

			public override bool TryToMerge(IAction followingAction)
			{
				DeleteTextAction next = followingAction as DeleteTextAction;
				if (next != null && next.Textbox == this.Textbox)
				{
					if (next.selectionEnd == this.selectionStart)
					{
						this.selectionStart -= next.selectionLength;
						this.selectionLength += next.selectionLength;
						this.deletedText = next.deletedText + this.deletedText;
						next.Execute();
						return true;
					}
					else if (next.selectionStart == this.selectionStart)
					{
						this.selectionLength += next.selectionLength;
						this.deletedText += next.deletedText;
						next.Execute();
						return true;
					}
				}
				return false;
			}
		}
	}
}