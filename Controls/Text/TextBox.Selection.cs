using System;
namespace GuiLabs.Canvas.Controls
{
	#region CaretPositionChanged event

	public delegate void CaretPositionChangedHandler(CaretPositionChangedEventArgs e);
	public class CaretPositionChangedEventArgs : EventArgs
	{
		public CaretPositionChangedEventArgs(
			TextBox textControl,
			int oldPosition,
			int newPosition)
		{
			TextControl = textControl;
			OldPosition = oldPosition;
			NewPosition = newPosition;
		}

		private int mOldPosition;
		public int OldPosition
		{
			get
			{
				return mOldPosition;
			}
			set
			{
				mOldPosition = value;
			}
		}

		private int mNewPosition;
		public int NewPosition
		{
			get
			{
				return mNewPosition;
			}
			set
			{
				mNewPosition = value;
			}
		}

		private TextBox mTextControl;
		public TextBox TextControl
		{
			get
			{
				return mTextControl;
			}
			set
			{
				mTextControl = value;
			}
		}
	}

	#endregion

	public partial class TextBox
	{
		#region Events

		public event CaretPositionChangedHandler CaretPositionChanged;
		protected void RaiseCaretPositionChanged(int oldPosition, int newPosition)
		{
			if (CaretPositionChanged != null)
			{
				CaretPositionChangedEventArgs e = new CaretPositionChangedEventArgs(
					this,
					oldPosition,
					newPosition);
				CaretPositionChanged(e);
			}
		}

		#endregion

		#region Selection

		private int SelectionStartPos = 0;

		public int SelectionStart
		{
			get { return System.Math.Min(SelectionStartPos, CaretPosition); }
		}

		public int SelectionLength
		{
			get { return System.Math.Abs(SelectionStartPos - CaretPosition); }
		}

		public int SelectionEnd
		{
			get { return System.Math.Max(SelectionStartPos, CaretPosition); }
		}

		public void SetSelection(int selStart, int selLength)
		{
			int length = this.TextLength;
			if (
				selStart >= 0
				&& selStart < length
				&& selLength > 0
				&& selStart + selLength <= length)
			{
				SelectionStartPos = selStart;
				CaretPosition = selStart + selLength;
			}
		}

		/// <summary>
		/// The selection is between SelectionStartPos and CaretPosition
		/// </summary>
		public void ResetSelection()
		{
			SelectionStartPos = CaretPosition;
		}

		public bool HasSelection
		{
			get
			{
				return SelectionStartPos != CaretPosition;
			}
		}

		#endregion

		#region Caret position

		public void SetCaretPosition(int newPosition)
		{
			CaretPosition = newPosition;
			ResetSelection();
		}

		private int mCaretPosition = 0;
		/// <summary>
		/// Can be 0 for the leftmost position, and 
		/// equal to Text.Length for the rightmost position.
		/// </summary>
		public int CaretPosition
		{
			get
			{
				return mCaretPosition;
			}
			set
			{
				if (mCaretPosition != value)
				{
					int oldValue = mCaretPosition;
					mCaretPosition = value;
					VerifyCaretPosition();
					if (mCaretPosition != oldValue)
					{
						RaiseCaretPositionChanged(oldValue, mCaretPosition);
					}
				}
			}
		}

		protected void VerifyCaretPosition()
		{
			if (mCaretPosition > TextLength)
			{
				mCaretPosition = TextLength;
			}
			else if (mCaretPosition < 0)
			{
				mCaretPosition = 0;
			}
		}

		/// <summary>
		/// Returns the substring from the beginning of the textbox to the caret.
		/// </summary>
		public string TextBeforeCaret
		{
			get
			{
				return this.Text.Substring(0, CaretPosition);
			}
		}

		/// <summary>
		/// Returns the substring from the caret to the end of the textbox.
		/// </summary>
		public string TextAfterCaret
		{
			get
			{
				return this.Text.Substring(CaretPosition);
			}
		}

		/// <summary>
		/// The text from the beginning of the textbox to the starting point of the selection.
		/// If nothing is selected, from the beginning to the current caret position.
		/// </summary>
		public string TextBeforeSelection
		{
			get
			{
				if (this.SelectionLength == 0)
				{
					return TextBeforeCaret;
				}
				else
				{
					return this.Text.Substring(0, SelectionStart);
				}
			}
		}

		/// <summary>
		/// The text from the end of the selection to the end of the textbox.
		/// If nothing is selected, from the caret position to the end.
		/// </summary>
		public string TextAfterSelection
		{
			get
			{
				if (this.SelectionLength == 0)
				{
					return TextAfterCaret;
				}
				else
				{
					return this.Text.Substring(SelectionEnd);
				}
			}
		}

		/// <summary>
		/// Returns the current selected text or string.Empty if none is selected.
		/// The text can be selected even if the textbox doesn't have the focus.
		/// </summary>
		public string SelectionText
		{
			get
			{
				if (this.SelectionLength == 0)
				{
					return string.Empty;
				}
				else
				{
					return this.Text.Substring(this.SelectionStart, this.SelectionLength);
				}
			}
		}

		/// <summary>
		/// Returns if the caret is currently at the beginning of the textbox.
		/// </summary>
		public bool CaretIsAtBeginning
		{
			get
			{
				return this.CaretPosition == 0;
			}
		}

		/// <summary>
		/// Returns if the caret is currently at the end of the textbox.
		/// </summary>
		public bool CaretIsAtEnd
		{
			get
			{
				return this.CaretPosition == this.TextLength;
			}
		}

		public void SetCaretToBeginning()
		{
			this.SetCaretPosition(0);
		}

		public void SetCaretToEnd()
		{
			this.SetCaretPosition(this.TextLength);
		}

		#endregion
	}
}