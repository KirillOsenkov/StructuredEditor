using System.Diagnostics;
using System;

namespace GuiLabs.Canvas
{
	[DebuggerStepThrough]
	public class Point
	{
		#region Constructors

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point()
		{
		}

		public Point(Point CloneFrom)
		{
			X = CloneFrom.X;
			Y = CloneFrom.Y;
		}

		public Point(int valueOfBoth)
		{
			X = valueOfBoth;
			Y = valueOfBoth;
		}

		#endregion

		#region X

		private int mx = 0;
		public int X
		{
			get
			{
				return mx;
			}
			set
			{
				mx = value;
			}
		}

		#endregion

		#region Y

		private int my = 0;
		public int Y
		{
			get
			{
				return my;
			}
			set
			{
				my = value;
			}
		}

		#endregion

		#region Add

		public virtual void Add(int x, int y)
		{
			X += x;
			Y += y;
		}

		public void Add(int delta)
		{
			Add(delta, delta);
		}

		public void Add(Point p)
		{
			Add(p.X, p.Y);
		}

		#endregion

		#region Set

		public virtual void Set(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void Set(int sizeOfBoth)
		{
			Set(sizeOfBoth, sizeOfBoth);
		}

		public void Set(Point p)
		{
			Set(p.X, p.Y);
		}

		public void Set(System.Drawing.Point point)
		{
			Set(point.X, point.Y);
		}

		public void Set(System.Drawing.Size point)
		{
			Set(point.Width, point.Height);
		}

		public void Set0()
		{
			Set(0, 0);
		}

		#endregion

		#region Classification

		public enum PointClassification
		{
			Before,
			Inside,
			After
		}

		public PointClassification ClassifyHorizontally(Rect r)
		{
			if (X < r.Left)
			{
				return PointClassification.Before;
			}
			else if (X <= r.Right)
			{
				return PointClassification.Inside;
			}
			else
			{
				return PointClassification.After;
			}
		}

		public PointClassification ClassifyVertically(Rect r)
		{
			if (Y < r.Top)
			{
				return PointClassification.Before;
			}
			else if (Y <= r.Bottom)
			{
				return PointClassification.Inside;
			}
			else
			{
				return PointClassification.After;
			}
		}

		#endregion

		public double DistanceFrom(int otherX, int otherY)
		{
			return Distance(X, Y, otherX, otherY);
		}

		public double DistanceFrom(Point p)
		{
			return Distance(X, Y, p.X, p.Y);
		}

		public bool PointWithinEpsilon(int otherX, int otherY, double epsilon)
		{
			return DistanceFrom(otherX, otherY) <= epsilon;
		}

		private static System.Drawing.Size dragSize = System.Windows.Forms.SystemInformation.DragSize;
		public bool PointWithinDragSize(int otherX, int otherY)
		{
			return Math.Abs(X - otherX) < dragSize.Width
				&& Math.Abs(Y - otherY) < dragSize.Height;
		}

		public static double Distance(int x1, int y1, int x2, int y2)
		{
			return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}

		public void FillPoint(ref System.Drawing.PointF p)
		{
			p.X = X;
			p.Y = Y;
		}

		#region Operators

		public static Point operator +(Point p1, Point p2)
		{
			Point newPoint = new Point(p1.X + p2.X, p1.Y + p2.Y);
			return newPoint;
		}

		public static Point operator -(Point p1, Point p2)
		{
			Point newPoint = new Point(p1.X - p2.X, p1.Y - p2.Y);
			return newPoint;
		}

		public static Point operator +(Point p1, int i)
		{
			Point newPoint = new Point(p1.X + i, p1.Y + i);
			return newPoint;
		}

		public static Point operator -(Point p1, int i)
		{
			Point newPoint = new Point(p1.mx - i, p1.my - i);
			return newPoint;
		}

		#endregion

		public bool EqualTo(Point other)
		{
			if (other == null)
			{
				return false;
			}
			else
			{
				return this.X == other.X && this.Y == other.Y;
			}
		}

		public override string ToString()
		{
			System.Text.StringBuilder s = new System.Text.StringBuilder("(");
			s.Append(X);
			s.Append(", ");
			s.Append(Y);
			s.Append(")");
			return s.ToString();
		}
	}
}