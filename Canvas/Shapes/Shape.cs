using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Shapes
{
	public delegate void DrawHandler(DrawHandlerEventArgs e);
	public class DrawHandlerEventArgs : System.EventArgs
	{
		public DrawHandlerEventArgs(IRenderer renderer, IDrawableRect shape)
		{
			Renderer = renderer;
			Shape = shape;
		}

		private IRenderer mRenderer;
		public IRenderer Renderer
		{
			get
			{
				return mRenderer;
			}
			set
			{
				mRenderer = value;
			}
		}

		private IDrawableRect mShape;
		public IDrawableRect Shape
		{
			get
			{
				return mShape;
			}
			set
			{
				mShape = value;
			}
		}
	}

	/// <summary>
	/// Base implementation for IShape
	/// </summary>
	public class Shape : KeyMouseHandler, IShape
	{
		#region ctors
		
		public Shape()
		{

		}

		#endregion

		#region Events

		public event NeedRedrawHandler NeedRedraw;
		public void RaiseNeedRedraw(IDrawableRect ShapeToRedraw)
		{
			if (NeedRedraw != null)
			{
				NeedRedraw(ShapeToRedraw);
			}
		}

		public void RaiseNeedRedraw()
		{
			RaiseNeedRedraw(this);
		}

		public event SizeChangedHandler SizeChanged;
		public void RaiseSizeChanged(Point OldSize)
		{
			//Point newSize = this.Bounds.Size;
			
			// Kirill: Unfortunately, there is something wrong with this check.
			// This optimization doesn't work for some reason
			// UniversalControls are being resized with the text 
			// and not stretched to fit the screen.

			//if (   newSize.EqualTo(OldSize)
			////	&& newSize.EqualTo(this.MinimumRequiredSize)
			//)
			//{
			//    return;
			//}

			if (SizeChanged != null)
			{
				SizeChanged(this, OldSize);
			}
		}

		#endregion

		#region Draw

		private DrawHandler mCustomDrawHandler;
		public DrawHandler CustomDrawHandler
		{
			get
			{
				return mCustomDrawHandler;
			}
			set
			{
				mCustomDrawHandler = value;
			}
		}

		/// <summary>
		/// A function to draw current shape
		/// on the specified renderer.
		/// </summary>
		/// <remarks>Override to define custom look for the shape</remarks>
		/// <param name="Renderer"></param>
		public void Draw(IRenderer renderer)
		{
			if (CustomDrawHandler != null)
			{
				CustomDrawHandler(new DrawHandlerEventArgs(renderer, this));
			}
			else
			{
				DrawCore(renderer);
			}
		}

		public virtual void DrawCore(IRenderer renderer)
		{

		}

		#endregion

		#region Bounds & Layout

		protected Rect mBounds = new Rect();
		/// <summary>
		/// Rectangular shape boundary.
		/// </summary>
		/// <remarks>Consists of two Points: Location and Size.</remarks>
		/// <example>
		/// this.Bounds.Location.Set(otherShape.Bounds.Location);
		/// this.Bounds.Location = alignWith.Bounds.Location;
		/// this.Bounds.Size.Set(160);
		/// this.Bounds.Size.Set(200, 32);
		/// this.Bounds.Size = MyPoint;
		/// </example>
		public virtual Rect Bounds
		{
			get
			{
				return mBounds;
			}
			set
			{
				mBounds = value;
			}
		}

		public bool IsVisibleInViewport(IRenderer renderer)
		{
			return this.Bounds.IntersectsRect(renderer.DrawOperations.Viewport);
		}

		private Point mMinimumRequiredSize = new Point();
		/// <summary>
		/// Actual size this shape requires and can be shrinked to.
		/// Bounds.Size can be larger than MinimumRequiredSize
		/// but it can never be smaller than MinimumRequiredSize 
		/// </summary>
		public virtual Point MinimumRequiredSize
		{
			get
			{
				return mMinimumRequiredSize;
			}
			protected set
			{
				mMinimumRequiredSize = value;
			}
		}

		private Point oldSize = new Point();
		/// <summary>
		/// Tells the shape to calculate its size itself.
		/// May be based on sizes of child shapes.
		/// </summary>
		/// <remarks>
		/// Layout doesn't set the Position of the shape,
		/// it only changes the shape's Size.
		/// Position of a shape is always changed by its parent,
		/// in parent's Layout.
		/// Layout is a template method and cannot be overriden as a whole.
		/// Inheritors can only override LayoutCore().
		/// </remarks>
		public virtual void Layout()
		{
			oldSize.Set(this.Bounds.Size);

			LayoutCore();

			// CalcMinimumRequiredSize();
			MinimumRequiredSize.Set(this.Bounds.Size);

			//LayoutDock();

			RaiseSizeChanged(oldSize);
		}

		public void SetBoundsToMinimumSize()
		{
			this.Bounds.Size.Set(this.MinimumRequiredSize);
		}

		/// <summary>
		/// Override LayoutCore to change the logic
		/// how the shape calculates its size
		/// and sets Bounds.Size
		/// </summary>
		public virtual void LayoutCore()
		{
		}

		public void LayoutDock()
		{
			if (this.Bounds.Size.X < this.MinimumRequiredSize.X)
			{
				this.Bounds.Size.X = this.MinimumRequiredSize.X;
			}
			if (this.Bounds.Size.Y < this.MinimumRequiredSize.Y)
			{
				this.Bounds.Size.Y = this.MinimumRequiredSize.Y;
			}
			LayoutDockCore();
		}

		public virtual void LayoutDockCore()
		{
			this.Bounds.Size.Set(this.MinimumRequiredSize);
		}

		#endregion

		#region HitTest

		public virtual bool HitTest(int x, int y)
		{
			return this.Visible && this.Bounds.HitTest(x, y) && this.Enabled;
		}

		#endregion

		#region Move

		public virtual void Move(int deltaX, int deltaY)
		{
			this.Bounds.Location.Add(deltaX, deltaY);
		}

		public void MoveTo(int x, int y)
		{
			Move(x - this.Bounds.Location.X, y - this.Bounds.Location.Y);
		}

		public void MoveTo(Point point)
		{
			Move(point.X - this.Bounds.Location.X, point.Y - this.Bounds.Location.Y);
		}

		#endregion

		#region Enabled

		private bool mEnabled = true;
		public virtual bool Enabled
		{
			get
			{
				return mEnabled;
			}
			set
			{
				mEnabled = value;
			}
		}

		#endregion

		#region Visible

		protected bool mVisible = true;
		public virtual bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				mVisible = value;
			}
		}

		#endregion
	}
}
