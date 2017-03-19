using System.Diagnostics;
using System;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas
{
	[DebuggerStepThrough]
	public class Rect : IHasBounds
	{
		#region ctors

		public Rect()
		{
			Location = new Point(0, 0);
			Size = new Point(0, 0);
		}

		public Rect(Point NewLocation, Point NewSize)
		{
			Location = NewLocation;
			Size = NewSize;
			if (Location == null)
				Location = new Point(0, 0);
			if (Size == null)
				Size = new Point(0, 0);
		}

		public Rect(System.Drawing.Rectangle r)
		{
			Location = new Point(r.Left, r.Top);
			Size = new Point(r.Width, r.Height);
		}

		public Rect(Rect r)
		{
			Location = new Point(r.Location);
			Size = new Point(r.Size);
		}

		public Rect(int x, int y, int width, int height)
		{
			Location = new Point(x, y);
			Size = new Point(width, height);
		}

		#endregion

		public Rect Clone()
		{
			return new Rect(this);
		}

		#region Location & Size

		private Point mLocation;
		public Point Location
		{
			get
			{
				return mLocation;
			}
			set
			{
				mLocation = value;
			}
		}

		private Point mSize;
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

		#endregion

		public Rect AddMargins(FourSideValues margins)
		{
			return new Rect(
				this.Left - margins.Left,
				this.Top - margins.Top,
				this.Width + margins.LeftAndRight,
				this.Height + margins.TopAndBottom);
		}

		public int Width
		{
			get
			{
				return Size.X;
			}
			set
			{
				Size.X = value;
			}
		}

		public int Height
		{
			get
			{
				return Size.Y;
			}
			set
			{
				Size.Y = value;
			}
		}

		#region Infos: Left, Top, Right, Bottom, CenterX, CenterY

		public int Left
		{
			get
			{
				return mLocation.X;
			}
			set
			{
				mLocation.X = value;
			}
		}

		public int Top
		{
			get
			{
				return mLocation.Y;
			}
			set
			{
				mLocation.Y = value;
			}
		}

		public int Right
		{
			get
			{
				return mLocation.X + mSize.X;
			}
		}

		public int Bottom
		{
			get
			{
				return mLocation.Y + mSize.Y;
			}
		}

		public int CenterX
		{
			get
			{
				return Location.X + Size.X / 2;
			}
		}

		public int CenterY
		{
			get
			{
				return Location.Y + Size.Y / 2;
			}
		}

		public int HalfSizeX
		{
			get
			{
				return Size.X / 2;
			}
		}

		public int HalfSizeY
		{
			get
			{
				return Size.Y / 2;
			}
		}

		#endregion

		#region Set

		public void Set(Rect R)
		{
			this.Location.Set(R.Location);
			this.Size.Set(R.Size);
		}

		public void Set(Point location, Point size)
		{
			this.Location.Set(location);
			this.Size.Set(size);
		}

		public void Set(int x, int y, int width, int height)
		{
			this.Location.Set(x, y);
			this.Size.Set(width, height);
		}

		public void Set(System.Drawing.Rectangle dotnetRect)
		{
			this.Location.X = dotnetRect.Left;
			this.Location.Y = dotnetRect.Top;
			this.Size.X = dotnetRect.Width;
			this.Size.Y = dotnetRect.Height;
		}

		public void Set0()
		{
			this.Location.Set0();
			this.Size.Set0();
		}

		#endregion

		#region HitTest

		public bool HitTest(Point HitPoint)
		{
			return HitTest(HitPoint.X, HitPoint.Y);
		}

		public bool HitTest(int x, int y)
		{
			return (
				(x >= this.Location.X) &&
				(x <= this.Right) &&
				(y >= this.Location.Y) &&
				(y <= this.Bottom)
				);
		}

		public bool HitTestX(int x)
		{
			return (
				(x >= this.Location.X) &&
				(x <= this.Right)
				);
		}

		public bool HitTestY(int y)
		{
			return (
				(y >= this.Location.Y) &&
				(y <= this.Bottom)
				);
		}

		public bool HitTestWithMargin(int x, int y, GuiLabs.Canvas.DrawStyle.FourSideValues margins)
		{
			return (
				(x >= this.Left - margins.Left) &&
				(x <= this.Right + margins.Right) &&
				(y >= this.Top - margins.Top) &&
				(y <= this.Bottom + margins.Bottom)
				);
		}

		public bool HitTestWithMarginX(int x, GuiLabs.Canvas.DrawStyle.FourSideValues margins)
		{
			return (
				(x >= this.Location.X - margins.Left) &&
				(x <= this.Right + margins.Right)
				);
		}

		public bool HitTestWithMarginY(int y, GuiLabs.Canvas.DrawStyle.FourSideValues margins)
		{
			return (
				(y >= this.Location.Y - margins.Top) &&
				(y <= this.Bottom + margins.Bottom)
				);
		}

		#endregion

		public bool Contains(Rect inner)
		{
			return
				inner.Location.X >= this.Location.X
				&& inner.Location.Y >= this.Location.Y
				&& inner.Right <= this.Right
				&& inner.Bottom <= this.Bottom;
		}

		public enum RectangleRelativePosition
		{
			FullyContaining = 0,
			FullyBefore = 1,
			IntersectingBeginning = 2,
			FullyInside = 3,
			IntersectingEnd = 4,
			FullyAfter = 5
		}

		public RectangleRelativePosition RelativeToRectY(Rect Reference)
		{
			int y1 = Reference.Location.Y;
			int y2 = Reference.Bottom;
			int top = this.Location.Y;
			int bottom = this.Bottom;

			if (top >= y2) return RectangleRelativePosition.FullyAfter;
			if (bottom <= y1) return RectangleRelativePosition.FullyBefore;

			if (top < y1)
			{
				if (bottom <= y2) return RectangleRelativePosition.IntersectingBeginning;
				return RectangleRelativePosition.FullyContaining;
			}
			else // if (top >= y1)
			{
				if (bottom > y2) return RectangleRelativePosition.IntersectingEnd;
				return RectangleRelativePosition.FullyInside;
			}
		}

		public bool IntersectsRectX(Rect Reference)
		{
			int x1 = Reference.Location.X;
			int x2 = Reference.Right;
			int left = this.Location.X;
			int right = this.Right;

			return (right > x1 && left < x2);
		}

		public bool IntersectsRectY(Rect Reference)
		{
			int y1 = Reference.Location.Y;
			int y2 = Reference.Bottom;
			int top = this.Location.Y;
			int bottom = this.Bottom;

			return (bottom > y1 && top < y2);
		}

		public bool IntersectsRect(Rect Reference)
		{
			return IntersectsRectX(Reference) && IntersectsRectY(Reference);
		}

		public void EnsurePositiveSize()
		{
			EnsurePositiveSizeX();
			EnsurePositiveSizeY();
		}

		public void EnsurePositiveSizeX()
		{
			int width = this.Size.X;
			if (width < 0)
			{
				this.Location.X += width;
				this.Size.X = -width;
			}
		}

		public void EnsurePositiveSizeY()
		{
			int height = this.Size.Y;
			if (height < 0)
			{
				this.Location.Y += height;
				this.Size.Y = -height;
			}
		}

		public void Unite(Rect ToContain)
		{
			int x1 = ToContain.mLocation.X;
			int y1 = ToContain.mLocation.Y;
			int x2 = ToContain.Right;
			int y2 = ToContain.Bottom;

			if (x1 < this.Location.X)
			{
				this.Size.X += this.Location.X - x1;
				this.Location.X = x1;
			}

			if (y1 < this.Location.Y)
			{
				this.Size.Y += this.Location.Y - y1;
				this.Location.Y = y1;
			}

			if (x2 > this.Right)
			{
				this.Size.X = x2 - this.Location.X;
			}

			if (y2 > this.Bottom)
			{
				this.Size.Y = y2 - this.Location.Y;
			}
		}

		public System.Drawing.Rectangle GetRectangle()
		{
			return new System.Drawing.Rectangle(mLocation.X, mLocation.Y, mSize.X, mSize.Y);
		}

		[CLSCompliant(false)]
		public GuiLabs.Utils.API.RECT ToRECT()
		{
			GuiLabs.Utils.API.RECT result;
			result.Left = this.Location.X;
			result.Top = this.Location.Y;
			result.Right = this.Right;
			result.Bottom = this.Bottom;
			return result;
		}

		public static Rect NullRect
		{
			get
			{
				return new Rect();
			}
		}

		Rect IHasBounds.Bounds
		{
			get
			{
				return this;
			}
			set
			{
				this.Set(value);
			}
		}
	}
}