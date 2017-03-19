namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Events

		public event TextChangedEventHandler TextChanged;
		protected void RaiseTextChanged(string oldText, string newText)
		{
			if (TextChanged != null)
			{
				TextChanged(this, oldText, newText);
			}
		}

		#endregion

		#region Changed

		/// <summary>
		/// Should be called anytime after text is changed.
		/// </summary>
		protected virtual void AfterTextChanged()
		{
			VerifyCaretPosition();
			Layout();

			ShouldScroll();
			Redraw();
		}

		private Rect scrollCenter = new Rect();
		private void ShouldScroll()
		{
			if (this.Root != null)
			{
				UpdateCaretCoords();
				scrollCenter.Location.Set(this.Bounds.Location.X + this.Box.Padding.Left + TextSize.X, this.Bounds.Location.Y + this.Box.Padding.Top);
				scrollCenter.Size.Set(1);
				this.Root.RaiseScrollTo(scrollCenter);
			}
		}

		#endregion

		private void AfterCaretChanged()
		{
			VerifyCaretPosition();
			ShouldScroll();
			Redraw();
		}
	}
}