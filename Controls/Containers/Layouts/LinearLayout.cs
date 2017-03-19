using System.Collections.Generic;

namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// How to align controls with smaller width/height
	/// </summary>
	public enum AlignmentType
	{
		LeftOrTopEdge,
		Center,
		RightOrBottomEdge,
		Justify
	}

	/// <summary>
	/// How to layout: in a row (horizontally) or in a column (vertically)
	/// </summary>
	public enum OrientationType
	{
		Horizontal,
		Vertical
	}

	/// <summary>
	/// Represents a custom container layout strategy
	/// that aligns all children of a container control
	/// horizontally or vertically, using specified options.
	/// </summary>
	public class LinearLayout : ILinearLayout
	{
		#region ctors

		public LinearLayout()
		{

		}

		public LinearLayout(
			OrientationType orientation,
			AlignmentType alignment,
			int xSpacing,
			int ySpacing)
		{
			XSpacing = xSpacing;
			YSpacing = ySpacing;
			Orientation = orientation;
			Alignment = alignment;
		}

		public LinearLayout(
			int xSpacing,
			int ySpacing)
		{
			XSpacing = xSpacing;
			YSpacing = ySpacing;
		}

		public LinearLayout(OrientationType orientation)
		{
			Orientation = orientation;
		}

		public LinearLayout(AlignmentType alignment)
		{
			Alignment = alignment;
		}

		public LinearLayout(OrientationType orientation, AlignmentType alignment)
		{
			Orientation = orientation;
			Alignment = alignment;
		}

		#endregion

		#region Layout

		/// <summary>
		/// Layouts all children of an ContainerControl
		/// with current layout parameters (Orientation, Alignment, etc.)
		/// </summary>
		/// <remarks>
		/// At the end, sets the control's own size
		/// based on summary size and bounds of all children.
		/// </remarks>
		/// <param name="control">An ContainerControl whose children need to be aligned</param>
		public void Layout(ContainerControl control)
		{
			if (Orientation == OrientationType.Vertical)
			{
				VerticalLayout(control);
			}
			else
			{
				HorizontalLayout(control);
			}

			// LayoutDock(control);
		}

		#endregion

		#region Algorithms

		/// <summary>
		/// Internal helper to align vertically
		/// </summary>
		/// <param name="control">control whose child controls are to be aligned</param>
		protected virtual void VerticalLayout(ContainerControl control)
		{
			bool someChildAlreadyProcessed = false;

			int ox = control.Bounds.Location.X;
			int oy = control.Bounds.Location.Y;

			int x = ox + control.Box.Borders.Left + control.Box.Padding.Left;
			int y = oy + control.Box.Borders.Top + control.Box.Padding.Top;

			Point maxSize = control.BiggestMinimumChildSize();
			Point childSize = new Point();

			foreach (Control child in control.Children)
			{
				if (child.Visible)
				{
					if (!someChildAlreadyProcessed)
					{
						someChildAlreadyProcessed = true;
					}
					else
					{
						y += YSpacing;
					}

					int moveToX = x;

					child.CalcSizeWithMargins(child.MinimumRequiredSize, childSize);

					switch (this.Alignment)
					{
						case AlignmentType.Center:
							moveToX = x + (maxSize.X - childSize.X) / 2;
							break;
						case AlignmentType.RightOrBottomEdge:
							moveToX = x + (maxSize.X - childSize.X);
							break;
						case AlignmentType.Justify:
						//child.Bounds.Size.X = maxSize;
						//moveToX = x;
						//break;
						case AlignmentType.LeftOrTopEdge:
						default:
							break;
					}

					child.MoveTo(moveToX + child.Box.Margins.Left, y + child.Box.Margins.Top);

					y += childSize.Y;
				}
			}

			x += maxSize.X + control.Box.Borders.Right + control.Box.Padding.Right;
			y += control.Box.Borders.Bottom + control.Box.Padding.Bottom;

			control.Bounds.Size.Set(x - ox, y - oy);
		}

		protected virtual void HorizontalLayout(ContainerControl control)
		{
			bool someChildAlreadyProcessed = false;

			int ox = control.Bounds.Location.X;
			int oy = control.Bounds.Location.Y;

			int x = ox + control.Box.Borders.Left + control.Box.Padding.Left;
			int y = oy + control.Box.Borders.Top + control.Box.Padding.Top;

			Point maxSize = control.BiggestMinimumChildSize();
			Point childSize = new Point();

			foreach (Control child in control.Children)
			{
				if (child.Visible)
				{
					if (!someChildAlreadyProcessed)
					{
						someChildAlreadyProcessed = true;
						if (child.Box.Margins.Right > 0)
						{

						}
					}
					else
					{
						x += XSpacing;
					}

					int moveToY = y;

					child.CalcSizeWithMargins(child.MinimumRequiredSize, childSize);

					switch (this.Alignment)
					{
						case AlignmentType.Center:
							moveToY = y + (maxSize.Y - childSize.Y) / 2;
							break;
						case AlignmentType.RightOrBottomEdge:
							moveToY = y + (maxSize.Y - childSize.Y);
							break;
						case AlignmentType.Justify:
							//child.Bounds.Size.X = maxSize;
							//moveToX = x;
							break;
						case AlignmentType.LeftOrTopEdge:
						default:
							break;
					}

					child.MoveTo(x + child.Box.Margins.Left, moveToY + child.Box.Margins.Top);

					x += childSize.X;
				}
			}

			x += control.Box.Borders.Right + control.Box.Padding.Right;
			y += maxSize.Y + control.Box.Borders.Bottom + control.Box.Padding.Bottom;

			control.Bounds.Size.Set(x - ox, y - oy);
		}

		#region HorizontalLayoutComplex

		/// <summary>
		/// Internal helper to align horizontally
		/// </summary>
		/// <param name="control">control whose children are to be aligned</param>
		protected virtual void HorizontalLayoutComplex(ContainerControl control)
		{
			int ox = control.Bounds.Location.X;
			int oy = control.Bounds.Location.Y;

			int XMargin = control.Box.Padding.Left;
			int YMargin = control.Box.Padding.Top;

			List<Point> LayerSizes = new List<Point>();
			LayerSizes.Add(new Point());

			bool someChildAlreadyProcessed = false;
			bool startingLayer = true;

			int x = ox + XMargin;
			int y = oy + YMargin;
			int maxX = 0;

			int j = 0;

			foreach (Control child in control.Children)
			{
				if (child.Visible)
				{
					if (!someChildAlreadyProcessed)
					{
						someChildAlreadyProcessed = true;
					}
					else
					{
						x += XSpacing;
						LayerSizes[j].X += XSpacing;
					}

					if (WrapMaxSize > 0 && x + child.MinimumRequiredSize.X + XMargin > ox + WrapMaxSize)
					{
						if (!startingLayer)
						{
							x = ox + control.Box.Padding.Left;
							LayerSizes.Add(new Point());
							j++;
							startingLayer = true;
						}
					}

					if (child.Bounds.Size.Y > LayerSizes[j].Y)
					{
						LayerSizes[j].Y = child.MinimumRequiredSize.Y;
					}

					LayerSizes[j].X += child.MinimumRequiredSize.X;

					x += child.MinimumRequiredSize.X;
					if (x > maxX)
					{
						maxX = x;
					}
					startingLayer = false;
				}
			}

			someChildAlreadyProcessed = false;
			startingLayer = true;

			switch (LayerAlignment)
			{
				case AlignmentType.LeftOrTopEdge:
					x = ox + XMargin;
					break;
				case AlignmentType.Center:
					x = ox + XMargin + (maxX - ox - XMargin - LayerSizes[0].X) / 2;
					break;
				case AlignmentType.RightOrBottomEdge:
					x = maxX - LayerSizes[0].X;
					break;
				case AlignmentType.Justify:
				default:
					x = ox + control.Box.Padding.Left;
					break;
			}

			y = oy + control.Box.Padding.Top;

			j = 0;

			foreach (Control child in control.Children)
			{
				if (child.Visible)
				{
					if (!someChildAlreadyProcessed)
					{
						someChildAlreadyProcessed = true;
					}
					else
					{
						x += XSpacing;
					}

					if (WrapMaxSize > 0
						&& x + child.MinimumRequiredSize.X + XMargin > ox + WrapMaxSize)
					{
						if (!startingLayer)
						{
							switch (LayerAlignment)
							{
								case AlignmentType.LeftOrTopEdge:
									x = ox + XMargin;
									break;
								case AlignmentType.Center:
									x = ox + XMargin + (maxX - ox - XMargin - LayerSizes[j].X) / 2;
									break;
								case AlignmentType.RightOrBottomEdge:
									x = maxX - LayerSizes[j].X;
									break;
								case AlignmentType.Justify:
								default:
									x = ox + XMargin;
									break;
							}

							y += LayerSizes[j].Y + YSpacing;
							j++;
							startingLayer = true;
						}
					}

					int moveToY = y;

					switch (this.Alignment)
					{
						case AlignmentType.Center:
							moveToY = y + (LayerSizes[j].Y - child.MinimumRequiredSize.Y) / 2;
							break;
						case AlignmentType.RightOrBottomEdge:
							moveToY = y + LayerSizes[j].Y - child.MinimumRequiredSize.Y;
							break;
						case AlignmentType.Justify:
							child.Bounds.Size.Y = LayerSizes[j].Y;
							break;
						case AlignmentType.LeftOrTopEdge:
						default:
							break;
					}

					child.MoveTo(x, moveToY);

					x += child.MinimumRequiredSize.X;
					startingLayer = false;
				}
			}

			x = maxX + XMargin;
			y += LayerSizes[j].Y + YMargin;

			control.Bounds.Size.Set(x - ox, y - oy);
		}

		#endregion

		#endregion

		public void LayoutDock(ContainerControl control)
		{
			if (Orientation == OrientationType.Vertical)
			{
				VerticalLayoutDock(control);
			}
			else
			{
				HorizontalLayoutDock(control);
			}
		}

		#region LayoutDock

		protected virtual void VerticalLayoutDock(ContainerControl control)
		{
			if (WrapMaxSize <= 0)
			{
				Point minSize = control.BiggestMinimumChildSize();
				int proposedSize = control.Bounds.Size.X - control.Box.Padding.LeftAndRight - control.Box.Borders.LeftAndRight;
				if (proposedSize < minSize.X) proposedSize = minSize.X;

				foreach (Control child in control.Children)
				{
					child.SetBoundsToMinimumSize();
					if (child.ShouldStretchHorizontally)
					{
						//child.Bounds.Location.X = control.Bounds.Location.X + XMargin;
						child.Bounds.Size.X = proposedSize - child.Box.Margins.LeftAndRight;
					}
					child.LayoutDock();
				}
			}
		}

		protected virtual void HorizontalLayoutDock(ContainerControl control)
		{
			if (WrapMaxSize <= 0)
			{
				Point minSize = control.BiggestMinimumChildSize();
				int proposedSize = control.Bounds.Size.Y - control.Box.Padding.TopAndBottom - control.Box.Borders.TopAndBottom;
				if (proposedSize < minSize.Y) proposedSize = minSize.Y;

				foreach (Control child in control.Children)
				{
					child.SetBoundsToMinimumSize();
					if (child.ShouldStretchHorizontally && child == control.Children.Tail)
					{
						//child.Bounds.Location.X = control.Bounds.Location.X + XMargin;
						child.Bounds.Size.X = control.Bounds.Right - child.Bounds.Location.X;
					}
					if (child.ShouldStretchVertically)
					{
						child.Bounds.Size.Y = proposedSize;
					}
					if (child.Stretch != StretchMode.None)
					{
						child.LayoutDock();
					}
				}
			}
		}

		#endregion

		#region Props

		private AlignmentType mAlignment = AlignmentType.LeftOrTopEdge;
		/// <summary>
		/// How to position controls which are smaller than neighbour controls:
		/// align them to the left (top), center them, or align them to the right (bottom)
		/// You can also justify (resize all controls to be the same width/height)
		/// </summary>
		public AlignmentType Alignment
		{
			get
			{
				return mAlignment;
			}
			set
			{
				mAlignment = value;
			}
		}

		private AlignmentType mLayerAlignment = AlignmentType.LeftOrTopEdge;
		public AlignmentType LayerAlignment
		{
			get
			{
				return mLayerAlignment;
			}
			set
			{
				mLayerAlignment = value;
			}
		}

		private OrientationType mOrientation = OrientationType.Vertical;
		/// <summary>
		/// How to layout controls - horizontally or vertically
		/// (in a row or in a column)
		/// </summary>
		public OrientationType Orientation
		{
			get
			{
				return mOrientation;
			}
			set
			{
				mOrientation = value;
			}
		}

		//private int mXMargin = 0;
		///// <summary>
		///// Distance in pixels between first control and parent's left edge
		///// (same as between last control and parent's right edge)
		///// </summary>
		//public int XMargin
		//{
		//    get
		//    {
		//        return mXMargin;
		//    }
		//    set
		//    {
		//        mXMargin = value;
		//    }
		//}

		//private int mYMargin = 0;
		///// <summary>
		///// Distance in pixels between first control and parent's top edge
		///// (same as between last control and parent's bottom edge)
		///// </summary>
		//public int YMargin
		//{
		//    get
		//    {
		//        return mYMargin;
		//    }
		//    set
		//    {
		//        mYMargin = value;
		//    }
		//}

		private int mXSpacing = 0;
		/// <summary>
		/// Distance in pixels between two neighbour controls (horizontally)
		/// </summary>
		public int XSpacing
		{
			get
			{
				return mXSpacing;
			}
			set
			{
				mXSpacing = value;
			}
		}

		private int mYSpacing = 0;
		/// <summary>
		/// Distance in pixels between two neighbour controls (vertically)
		/// </summary>
		public int YSpacing
		{
			get
			{
				return mYSpacing;
			}
			set
			{
				mYSpacing = value;
			}
		}

		private int mWrapMaxSize = 0;
		/// <summary>
		/// The max boundary around which shapes should be wrapped.
		/// </summary>
		public int WrapMaxSize
		{
			get
			{
				return mWrapMaxSize;
			}
			set
			{
				mWrapMaxSize = value;
			}
		}

		#endregion

		public System.Windows.Forms.Keys PrevKey
		{
			get
			{
				if (this.Orientation == OrientationType.Horizontal)
				{
					return System.Windows.Forms.Keys.Left;
				}
				else
				{
					return System.Windows.Forms.Keys.Up;
				}
			}
		}

		public System.Windows.Forms.Keys NextKey
		{
			get
			{
				if (this.Orientation == OrientationType.Horizontal)
				{
					return System.Windows.Forms.Keys.Right;
				}
				else
				{
					return System.Windows.Forms.Keys.Down;
				}
			}
		}
	}
}
