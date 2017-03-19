using System.Windows.Forms;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Events

		public event KeyPressEventHandler PreviewKeyPress;
		protected void RaisePreviewKeyPress(KeyPressEventArgs e)
		{
			if (PreviewKeyPress != null)
			{
				PreviewKeyPress(this, e);
			}
		}

		public event KeyEventHandler PreviewKeyDown;
		protected void RaisePreviewKeyDown(KeyEventArgs e)
		{
			if (PreviewKeyDown != null)
			{
				PreviewKeyDown(this, e);
			}
		}

		public event KeyEventHandler AfterKeyDown;
		protected void RaiseAfterKeyDown(KeyEventArgs e)
		{
			if (AfterKeyDown != null)
			{
				AfterKeyDown(this, e);
			}
		}

		#endregion

		#region KeyBrake

		private KeyboardBrakeSystem mKeyBrake = new KeyboardBrakeSystem();
		public KeyboardBrakeSystem KeyBrake
		{
			get
			{
				return mKeyBrake;
			}
			set
			{
				mKeyBrake = value;
			}
		}

		#endregion

		/// <summary>
		/// Called when this TextBox has focus and user presses a keyboard key.
		/// </summary>
		/// <remarks>
		/// Raises KeyDown event.
		/// </remarks>
		/// <param name="e">Information about the key pressed.</param>
		public override void OnKeyDown(KeyEventArgs e)
		{
			RaisePreviewKeyDown(e);
			if (e.Handled)
			{
				return;
			}

			if (e.KeyCode == Keys.Left)
			{
				OnKeyDownLeft(e);
			}
			else if (e.KeyCode == Keys.Right)
			{
				OnKeyDownRight(e);
			}
			else if (e.KeyCode == Keys.Down)
			{
				OnKeyDownDown(e);
			}
			else if (e.KeyCode == Keys.Home)
			{
				OnKeyDownHome(e);
			}
			else if (e.KeyCode == Keys.End)
			{
				OnKeyDownEnd(e);
			}
			else if (e.KeyCode == Keys.Back)
			{
				OnKeyDownBack(e);
			}
			else if (e.KeyCode == Keys.Delete)
			{
				OnKeyDownDelete(e);
			}
			//else if (e.KeyCode == Keys.C && e.Control)
			//{
			//    OnUserCopyCommand();
			//}
			//else if (e.KeyCode == Keys.V && e.Control)
			//{
			//    OnUserPasteCommand();
			//}
			//else if (e.KeyCode == Keys.X && e.Control)
			//{
			//    OnUserCutCommand();
			//}
			else
			{
				// Only if the key pressed wasn't handled by this TextBox,
				// notify the listeners by raising KeyDown event.
				// 
				// If we can handle the event by ourselves
				// (e.g. user pressed the "left" button and the cursor
				// can be moved to the left, just move the cursor,
				// no need to inform the listeners.
				RaiseKeyDown(e);
			}

			RaiseAfterKeyDown(e);
		}

		protected virtual void OnKeyDownLeft(KeyEventArgs e)
		{
			bool ShouldRaiseEvent = false;
			bool CaretChanged = true;

			if (CaretPosition > 0)
			{
				if (!e.Shift && !e.Alt && !e.Control)
				{
					KeyBrake.SetBrake();
				}

				if (e.Shift)
				{
					CaretPosition = GoToLeft(e.Control);
				}
				else
				{
					if (!HasSelection)
					{
						CaretPosition = GoToLeft(e.Control);
					}
					else
					{
						CaretPosition = SelectionStart;
					}
					ResetSelection();
				}
			}
			else
			{
				if (HasSelection && !e.Shift)
				{
					ResetSelection();
				}
				else
				{
					ShouldRaiseEvent = true;
					CaretChanged = false;
				}
				if (e.Shift || KeyBrake.QueryAndDecreaseCounter())
				{
					ShouldRaiseEvent = false;
				}
			}

			if (CaretChanged)
			{
				this.AfterCaretChanged();
			}
			if (ShouldRaiseEvent)
			{
				RaiseKeyDown(e);
			}
		}

		protected virtual void OnKeyDownRight(KeyEventArgs e)
		{
			bool ShouldRaiseEvent = false;
			bool CaretChanged = true;

			if (CaretPosition < TextLength)
			{
				if (!e.Shift && !e.Alt && !e.Control)
				{
					KeyBrake.SetBrake();
				}

				if (e.Shift)
				{
					CaretPosition = GoToRight(e.Control);
				}
				else
				{
					if (!HasSelection)
					{
						CaretPosition = GoToRight(e.Control);
					}
					else
					{
						CaretPosition = SelectionEnd;
					}
					ResetSelection();
				}
			}
			else
			{
				if (HasSelection && !e.Shift)
				{
					ResetSelection();
				}
				else
				{
					ShouldRaiseEvent = true;
					CaretChanged = false;
				}
				if (e.Shift || KeyBrake.QueryAndDecreaseCounter())
				{
					ShouldRaiseEvent = false;
				}
			}

			if (CaretChanged)
			{
				this.AfterCaretChanged();
			}
			if (ShouldRaiseEvent)
			{
				RaiseKeyDown(e);
			}
		}

		protected virtual void OnKeyDownDown(KeyEventArgs e)
		{
			if (CaretPosition == 0
				&& TextLength > 0
				&& e.Shift)
			{
				SelectionStartPos = TextLength;
				AfterCaretChanged();
			}
			else
			{
				RaiseKeyDown(e);
			}
		}

		protected virtual void OnKeyDownHome(KeyEventArgs e)
		{
			bool ShouldRaiseEvent = false;
			bool CaretChanged = true;

			if (CaretPosition > 0)
			{
				CaretPosition = 0;
				if (!e.Shift)
				{
					ResetSelection();
				}
			}
			else
			{
				if (HasSelection && !e.Shift)
				{
					CaretPosition = 0;
					ResetSelection();
				}
				else
				{
					CaretChanged = false;
					ShouldRaiseEvent = true;
				}
			}

			if (CaretChanged)
			{
				this.AfterCaretChanged();
			}
			if (ShouldRaiseEvent)
			{
				RaiseKeyDown(e);
			}
		}

		protected virtual void OnKeyDownEnd(KeyEventArgs e)
		{
			bool ShouldRaiseEvent = false;
			bool CaretChanged = true;

			if (CaretPosition < TextLength)
			{
				CaretPosition = TextLength;
				if (!e.Shift)
				{
					ResetSelection();
				}
			}
			else
			{
				if (HasSelection && !e.Shift)
				{
					CaretPosition = TextLength;
					ResetSelection();
				}
				else
				{
					CaretChanged = false;
					ShouldRaiseEvent = true;
				}
			}

			if (CaretChanged)
			{
				this.AfterCaretChanged();
			}
			if (ShouldRaiseEvent)
			{
				RaiseKeyDown(e);
			}
		}

		protected virtual void OnKeyDownBack(KeyEventArgs e)
		{
			if (e.Handled)
			{
				return;
			}

            using (RedrawAccumulator a = new RedrawAccumulator(this.Root, false))
            {
                if (HasSelection)
                {
                    DeleteSelection();
                    a.ShouldRedrawAtTheEnd = true;
                    e.Handled = true;
                }
                else if (!CaretIsAtBeginning)
                {
                    this.CaretPosition = GoToLeft(e.Control);
                    DeleteSelection();
                    a.ShouldRedrawAtTheEnd = true;
                    e.Handled = true;
                }

                if (!e.Handled)
                {
                    RaiseKeyDown(e);
                    if (e.Handled)
                    {
                        a.ShouldRedrawAtTheEnd = true;
                    }
                }
            }
		}

		protected virtual void OnKeyDownDelete(KeyEventArgs e)
		{
			if (e.Handled)
			{
				return;
			}

            using (RedrawAccumulator a = new RedrawAccumulator(this.Root, false))
            {
                if (!e.Shift && !e.Alt)
                {
                    if (HasSelection)
                    {
                        DeleteSelection();
                        a.ShouldRedrawAtTheEnd = true;
                        e.Handled = true;
                    }
                    else if (!CaretIsAtEnd)
                    {
                        this.CaretPosition = GoToRight(e.Control);
                        DeleteSelection();
                        a.ShouldRedrawAtTheEnd = true;
                        e.Handled = true;
                    }
                }

                if (!e.Handled)
                {
                    RaiseKeyDown(e);
                    if (e.Handled)
                    {
                        a.ShouldRedrawAtTheEnd = true;
                    }
                }
            }
		}

		public override void OnKeyPress(KeyPressEventArgs e)
		{
			RaisePreviewKeyPress(e);
			if (e.Handled)
			{
				return;
			}

			using (RedrawAccumulator a = new RedrawAccumulator(this.Root, false))
			{
				if (IsCharAcceptable(e.KeyChar) && !char.IsControl(e.KeyChar))
				{
					InsertText(e.KeyChar.ToString());
					a.ShouldRedrawAtTheEnd = true;
				}

				RaiseKeyPress(e);
				if (e.Handled)
				{
					a.ShouldRedrawAtTheEnd = true;
				}
			}
		}

		public override void OnKeyUp(KeyEventArgs e)
		{
			RaiseKeyUp(e);
			KeyBrake.ReleaseBrake();
		}
	}
}