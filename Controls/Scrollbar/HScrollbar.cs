namespace GuiLabs.Canvas.Controls
{
	public class HScrollbar : Scrollbar
	{
		public HScrollbar(CompositeRange ViewRange)
			: base(ViewRange)
		{
			Bounds.Size.Y = InitialSize;
		}

		protected override int GetScrollAreaStart()
		{
			return ButtonUp.Bounds.Right + 1;
		}

		protected override int GetScrollAreaSize()
		{
			return this.Bounds.Size.X - (ButtonUp.Bounds.Size.X + ButtonDown.Bounds.Size.X + 2);
		}

		protected override void UpdateThumbFromThumbRange()
		{
			if (!Thumb.Visible)
				return;

			Thumb.Bounds.Location.Set(
				(int)(ThumbRange.Pos + ThumbRange.Span.Pos),
				ButtonUp.Bounds.Location.Y
				);

			Thumb.Bounds.Size.Set(
				(int)(ThumbRange.Span.Size),
				ButtonUp.Bounds.Size.Y
				);
		}

		public override void LayoutCore()
		{
			int x = this.Bounds.Location.X;
			int y = this.Bounds.Location.Y;
			int sx = this.Bounds.Size.X;
			int sy = this.Bounds.Size.Y;
			int vsy = sy;

			int MinSize = 2 * sy + 2;

			if (sx < MinSize)
			{
				vsy = (int)(sx / 2) - 1;
				Thumb.Visible = false;
			}
			else
			{
				UpdateButtonVisibility();
			}

			Background.Bounds.Set(this.Bounds);
			ButtonUp.Bounds.Location.Set(x, y);
			ButtonUp.Bounds.Size.Set(sy, vsy - 1);
			ButtonDown.Bounds.Location.Set(x + sx - vsy - 1, y);
			ButtonDown.Bounds.Size.Set(ButtonUp.Bounds.Size);

			base.LayoutCore();
		}

		protected override int GetMouseCoord(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		{
			return e.X;
		}

		protected override int ThumbSize
		{
			get 
			{
				return Thumb.Bounds.Size.X;
			}
		}
	}
}
