namespace GuiLabs.Canvas.Controls
{
	public class VScrollbar : Scrollbar
	{
		public VScrollbar(CompositeRange ViewRange)
			: base(ViewRange)
		{
			Bounds.Size.X = InitialSize;
		}

		protected override int GetScrollAreaStart()
		{
			return ButtonUp.Bounds.Bottom + 1;
		}

		protected override int GetScrollAreaSize()
		{
			return this.Bounds.Size.Y - (ButtonUp.Bounds.Size.Y + ButtonDown.Bounds.Size.Y + 2);
		}

		protected override void UpdateThumbFromThumbRange()
		{
			if (!Thumb.Visible)
				return;

			Thumb.Bounds.Location.Set(
				ButtonUp.Bounds.Location.X,
				(int)(ThumbRange.Pos + ThumbRange.Span.Pos));

			Thumb.Bounds.Size.Set(
				ButtonUp.Bounds.Size.X,
				(int)(ThumbRange.Span.Size));
		}

		public override void LayoutCore()
		{
			int x = this.Bounds.Location.X;
			int y = this.Bounds.Location.Y;
			int sx = this.Bounds.Size.X;
			int sy = this.Bounds.Size.Y;
			int vsy = sx;

			int MinHeight = 2 * sx + 2;

			if (sy < MinHeight)
			{
				vsy = (int)(sy / 2) - 1;
				Thumb.Visible = false;
			}
			else
			{
				UpdateButtonVisibility();
			}

			Background.Bounds.Set(this.Bounds);
			ButtonUp.Bounds.Location.Set(x, y);
			ButtonUp.Bounds.Size.Set(sx - 1, vsy);
			ButtonDown.Bounds.Location.Set(x, y + sy - vsy - 1);
			ButtonDown.Bounds.Size.Set(ButtonUp.Bounds.Size);

			base.LayoutCore();
		}
	}
}
