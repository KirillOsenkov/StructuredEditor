using GuiLabs.Canvas.Events;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		public event MouseWithKeysEventHandler PreviewShowPopupMenu;
		protected void RaisePreviewShowPopupMenu(MouseWithKeysEventArgs e)
		{
			if (PreviewShowPopupMenu != null)
			{
				PreviewShowPopupMenu(e);
			}
		}

		#region Mouse events

		public override void OnDoubleClick(MouseWithKeysEventArgs e)
		{
			if (this.IsFocused)
			{
				this.SetSelection(0, this.TextLength);
				AfterCaretChanged();
			}

			RaiseDoubleClick(e);
		}

		private bool WasMouseDown = false;

		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			WasMouseDown = true;

			bool shouldPositionCaret =
				e.Button == System.Windows.Forms.MouseButtons.Left
				|| (e.Button == System.Windows.Forms.MouseButtons.Right
					&& this.SelectionLength == 0);

			if (shouldPositionCaret)
			{
				int newCaretPosition = CursorStringPosition(e.X);
				if (CaretPosition != newCaretPosition)
				{
					CaretPosition = newCaretPosition;

					if (!e.IsShiftPressed)
					{
						ResetSelection();
					}

					AfterCaretChanged();
					SetFocus();
					//e.Handled = true;
				}
				else if (this.SelectionLength != 0)
				{
					ResetSelection();
					AfterCaretChanged();
					SetFocus();
					//e.Handled = true;
				}
			}

			// commenting this out because we always want the 
			// event to propagate to the root
			// (for drag & drop purposes)
			//if (!e.Handled)
			//{
				RaiseMouseDown(e);
			//}
		}

		public override void OnMouseMove(MouseWithKeysEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left
				&& WasMouseDown)
			{
				int newCaretPosition = CursorStringPosition(e.X);
				if (CaretPosition != newCaretPosition)
				{
					CaretPosition = newCaretPosition;
					AfterCaretChanged();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				RaiseMouseMove(e);
			}
		}

		public override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			WasMouseDown = false;
			RaiseMouseUp(e);
			if (!e.Handled)
			{
				OnShowPopupMenu(e);
			}
		}

		private bool CanShowPopupMenu(MouseWithKeysEventArgs e)
		{
			return ShouldShowPopupMenu 
				&& e.IsRightButtonPressed
				&& this.Root != null
				&& this.Menu != null
				&& this.Menu.Count > 0;
		}

		private bool mShouldShowPopupMenu = true;
		public bool ShouldShowPopupMenu
		{
			get
			{
				return mShouldShowPopupMenu;
			}
			set
			{
				mShouldShowPopupMenu = value;
			}
		}

		public virtual void OnShowPopupMenu(MouseWithKeysEventArgs e)
		{
			if (!ShouldShowPopupMenu 
				|| e.Handled
				|| !CanShowPopupMenu(e) 
				|| !this.HitTest(e.X, e.Y)
			)
			{
				return;
			}
			RaisePreviewShowPopupMenu(e);
			if (!e.Handled)
			{
				this.Root.ShowPopupMenu(Menu, e.Location);
				e.Handled = true;
			}
		}

		#endregion
	}
}