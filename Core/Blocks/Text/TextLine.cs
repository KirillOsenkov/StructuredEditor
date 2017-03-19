using System;
using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.UI;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Blocks
{
	public class TextLine : TextBoxBlockWithCompletion
	{
		#region ctors

		public TextLine()
			: base()
		{
			Init();
		}

		public TextLine(string initialText)
			: base(initialText)
		{
			Init();
		}

		private void Init()
		{
			LastCaretPosition = 0;
			CanMoveUpDown = true;
		}

		protected override void InitBox()
		{
			int margins = Multiline ? 2 : 0;
			this.MyControl.Box.Margins.SetTopAndBottom(margins);
			this.MyControl.Box.SetMouseSensitivityToMargins();
		}

		#endregion

		#region OnEvents

		#region Arrows move focus

		/// <summary>
		/// Set caret to the end of the previous line, if multiline mode enabled
		/// </summary>
		protected override void OnKeyDownLeft(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.MyTextBox.CaretPosition == 0 && Multiline)
			{
				// e.Handled = true is necessary to prevent parent UniversalBlock
				// from receiving focus
				// because otherwise TextBox will raise KeyDown
				// and parent UniversalBlock would catch it and steal focus
				TextLine prev = this.Prev as TextLine;
				if (prev != null)
				{
					prev.SetCursorToTheEnd();
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Set caret to the beginning of the next line, if multiline mode enabled
		/// </summary>
		protected override void OnKeyDownRight(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.MyTextBox.CaretPosition == this.MyTextBox.Text.Length && Multiline)
			{
				TextLine next = this.Next as TextLine;
				if (next != null)
				{
					next.SetCursorToTheBeginning();
				}
			}
		}

		/// <summary>
		/// Set caret to the previous line, if multiline mode enabled
		/// </summary>
		protected override void OnKeyDownUp(System.Windows.Forms.KeyEventArgs e)
		{
			TextLine previous = this.Prev as TextLine;
			if (previous != null && Multiline)
			{
				ChangingFromNeighborLine = true;
				previous.MyTextBox.SetCaretPosition(LastCaretPosition);
				ChangingFromNeighborLine = false;
				previous.SetFocus();
				// set this to true to prevent standart code 
				// to move focus up in LinearContainerBlock
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDownUp(e);
			}
		}

		/// <summary>
		/// Set caret to the next line, if multiline mode enabled
		/// </summary>
		protected override void OnKeyDownDown(System.Windows.Forms.KeyEventArgs e)
		{
			TextLine next = this.Next as TextLine;
			if (Multiline && next != null)
			{
				ChangingFromNeighborLine = true;
				next.MyTextBox.SetCaretPosition(LastCaretPosition);
				ChangingFromNeighborLine = false;
				next.SetFocus();
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDownDown(e);
			}
		}

		#endregion

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline && IsMoveUpDown(e))
			{
				e.Handled = true;
				return;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline)
			{
				LineBreak(e.Control);
				e.Handled = true;
			}
		}

		protected override void OnKeyDownDelete(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline)
			{
				if (e.Shift 
					&& (this.Prev != null || this.Next != null)
					&& this.CanBeDeletedByUser)
				{
					this.Delete();
					e.Handled = true;
					return;
				}

				if (this.Text == ""
					&& this.Next == null
					&& this.Prev != null
					&& CanBeDeletedByUser)
				{
					LastCaretPosition = 0;
					this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
					this.Delete();
                    e.Handled = true;
				}
				else if (this.MyTextBox.CaretPosition == this.MyTextBox.Text.Length)
				{
					JoinWithNext();
                    e.Handled = true;
				}
			}
		}

		protected override void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.MyTextBox.CaretPosition == 0 && Multiline)
			{
				TextLine previous = this.Prev as TextLine;

				if (previous != null)
				{
					previous.JoinWithNext();
					e.Handled = true;
				}
				else if (this.Prev != null)
				{
					using (Redrawer a = new Redrawer(this.Root))
					{
						Block p = this.FindPrevFocusableBlock();
						if (p != null)
						{
							p.SetCursorToTheEnd();
						}
						else
						{
							this.RemoveFocus(MoveFocusDirection.SelectPrev);
						}
						if (this.Text == "")
						{
							base.Delete();
						}
						e.Handled = true;
					}
				}
                else
                {
                    using (new Redrawer(Root, true))
                    {
                        Block p = this.FindPrevFocusableBlockInChain();
                        if (p != null)
                        {
                            p.SetCursorToTheEnd();
                            e.Handled = true;
                        }
                    }
                }
			}
			if (!e.Handled)
			{
				base.OnKeyDownBack(e);
			}
		}

		#region Caret position

		private static int mLastCaretPosition = 0;
		protected static int LastCaretPosition
		{
			get
			{
				return mLastCaretPosition;
			}
			set
			{
				mLastCaretPosition = value;
			}
		}

		private static bool ChangingFromNeighborLine = false;

		protected override void OnCaretPositionChanged(CaretPositionChangedEventArgs e)
		{
			base.OnCaretPositionChanged(e);
			if (!ChangingFromNeighborLine)
			{
				LastCaretPosition = e.NewPosition;
			}
		}

		#endregion

		#endregion

		#region Multiline

		private bool mMultiline = true;
		public bool Multiline
		{
			get
			{
				return mMultiline;
			}
			set
			{
				mMultiline = value;
				CanMoveUpDown = value;
				InitBox();
			}
		}

		protected virtual TextLine CreateNewTextLine()
		{
			TextLine result = null;
			try
			{
				Type t = this.GetType();
				result = (TextLine)Activator.CreateInstance(t);
			}
			catch (Exception)
			{
				result = new TextLine();
			}
			return result;
		}

		public virtual void JoinWithNext()
		{
			TextLine next = this.Next as TextLine;
			if (next != null)
			{
				using (this.Transaction())
				{
					int caretPosThis = this.Text.Length;
					LastCaretPosition = caretPosThis;

					this.Text += next.Text;
					this.MyTextBox.SetCaretPosition(caretPosThis);
					this.SetFocus(SetFocusOptions.General);
					next.Delete();
					SetCaretPositionAction action = new SetCaretPositionAction(
						this.Root,
						this,
						SetFocusOptions.General,
						caretPosThis);
					this.ActionManager.RecordAction(action);
				}
			}
			else if (this.Text == "" && (this.Prev != null || this.Next != null))
			{
				base.Delete();
			}
		}

		public void LineBreak(bool stayOnTheOldLine)
		{
			string oldLineText = this.MyTextBox.TextBeforeSelection;
			string newLineText = this.MyTextBox.TextAfterSelection;
			TextLine newLine = CreateNewTextLine();

			using (this.Transaction())
			{
				if (newLineText != "")
				{
					newLine.Text = newLineText;
					this.Text = oldLineText;
				}
				this.AppendBlocks(newLine);
				if (stayOnTheOldLine)
				{
					this.SetFocus(SetFocusOptions.ToEnd, Root.ActionManager);
				}
				else
				{
					newLine.SetFocus(SetFocusOptions.ToBeginning, Root.ActionManager);
				}
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "TextLine";
		}

		#endregion

		#region Help

		public override IEnumerable<string> HelpStrings
		{
			get
			{
				if (Multiline 
					&& this.Prev != null
					&& this.MyTextBox.CaretIsAtBeginning)
				{
					yield return "Press [Backspace] to move to the previous line.";
				}
				if (!this.MyTextBox.CaretIsAtBeginning && !Completion.Visible)
				{
					yield return HelpBase.PressHome;
				}
				if (!this.MyTextBox.CaretIsAtEnd && !Completion.Visible)
				{
					yield return HelpBase.PressEnd;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion
	}
}
