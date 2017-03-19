using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// A rectangle that can be drawn on the Canvas
	/// </summary>
	public class RectShape : Control
	{
		#region ctors

		public RectShape()
			: base()
		{
			this.Stretch = StretchMode.None;
		}

		public RectShape(int sizeX, int sizeY)
			: this()
		{
			Size.Set(sizeX, sizeY);
		}

		public RectShape(IFillStyleInfo back)
			: this()
		{
			CurrentStyle.FillStyleInfo = back;
		}

		#endregion

		#region Layout

		public override void LayoutCore()
		{
			this.Bounds.Size.Set(Size.X, Size.Y);
			//this.Box.Margins.Right = 0;
		}

		public override void LayoutDockCore()
		{
			//if (!this.ShouldStretchHorizontally)
			//{
			//    this.Box.Margins.Right = 0;
			//}
			//else
			//{
			//    this.Box.Margins.Right = this.Bounds.Size.X - this.MinimumRequiredSize.X;
			//    if (this.Box.Margins.Right < 0)
			//    {
			//        this.Box.Margins.Right = 0;
			//    }
			//}
			//this.Bounds.Size.X = this.MinimumRequiredSize.X;

			if (!this.ShouldStretchHorizontally)
			{
				this.Bounds.Size.X = this.MinimumRequiredSize.X;
			}

			if (!this.ShouldStretchVertically)
			{
				this.Bounds.Size.Y = this.MinimumRequiredSize.Y;
			}
		}

		//public override void SetBoundsToMinimumSize()
		//{
		//    this.Bounds.Size.Set(this.MinimumRequiredSize);
		//    this.Box.Margins.Right = 0;
		//}

		#endregion

		private Point mSize = new Point();
		public Point Size
		{
			get
			{
				return mSize;
			}
			set
			{
				mSize = value;
			}
		}

		#region Style

		protected override string StyleName
		{
			get
			{
				return "RectShape";
			}
		}

		#endregion
	}
}
