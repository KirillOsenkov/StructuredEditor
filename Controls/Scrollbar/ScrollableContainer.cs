using GuiLabs.Canvas.DrawOperations;
using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// ScrollableContainer contains a Content, that may possibly have 
	/// much bigger dimensions as the container ScrollableContainer itself.
	/// 
	/// ScrollableContainer uses a TranslateTransform "Scroller" to map
	/// drawing and user input to the original Content, so that the
	/// Content thinks it draws itself with its normal coordinates
	/// and the user clicks directly on it.
	/// </summary>
	/// <remarks>
	/// 1. The size of Content can change.
	/// 2. The size of ScrollableContainer can change independently.
	/// 3. Content may ask to scroll it arbitrarily.
	/// </remarks>
	public class ScrollableContainer : Control, IScrollableContainer
	{
		#region ctors

		public ScrollableContainer()
			: base()
		{

		}

		public ScrollableContainer(IScrollableContainer Source)
			: base()
		{
			Content = Source;
		}

		#endregion

		#region Events

		public event SizeChangedHandler ContentSizeChanged;
		public event ScrollToHandler ShouldScrollTo;
		//public event NeedRedrawHandler ShapeNeedsRedraw;

		public void RaiseContentSizeChanged(IShape ResizedShape, Point OldSize)
		{
			if (ContentSizeChanged != null)
			{
				ContentSizeChanged(ResizedShape, OldSize);
			}
		}

		public virtual void RaiseScrollTo(Rect shape)
		{
			if (ShouldScrollTo != null)
			{
				ShouldScrollTo(shape);
			}
		}

		//protected void RaiseShapeNeedsRedraw(IDrawableRect shapeToRedraw)
		//{
		//    if (ShapeNeedsRedraw != null)
		//    {
		//        ShapeNeedsRedraw(shapeToRedraw);
		//    }
		//}

		#endregion

		#region Content
		private IScrollableContainer mContent;
		public IScrollableContainer Content
		{
			get
			{
				return mContent;
			}
			set
			{
				if (mContent != null)
				{
					mContent.NeedRedraw -= Content_NeedRedraw;
					mContent.SizeChanged -= Content_SizeChanged;
					mContent.ShouldScrollTo -= Content_ShouldScrollTo;
					//mContent.ShapeNeedsRedraw -= mContent_ShapeNeedsRedraw;
				}
				mContent = value;
				if (mContent != null)
				{
					mContent.NeedRedraw += Content_NeedRedraw;
					mContent.SizeChanged += Content_SizeChanged;
					mContent.ShouldScrollTo += Content_ShouldScrollTo;
					//mContent.ShapeNeedsRedraw += mContent_ShapeNeedsRedraw;
				}
			}
		}

		#endregion

		#region Transform & MouseHandler

		private TranslateTransform Scroller = new TranslateTransform();

		public int DeltaX
		{
			get
			{
				return Scroller.DeltaX;
			}
			set
			{
				Scroller.DeltaX = value;
				mScrollPosition.X = value;
			}
		}

		public int DeltaY
		{
			get
			{
				return Scroller.DeltaY;
			}
			set
			{
				Scroller.DeltaY = value;
				mScrollPosition.Y = value;
			}
		}

		private Point mScrollPosition = new Point();
		public Point ScrollPosition
		{
			get
			{
				return mScrollPosition;
			}
		}

		private Rect mViewPort = new Rect();
		public Rect ViewPort
		{
			get
			{
				mViewPort.Location = ScrollPosition;
				mViewPort.Size = Bounds.Size;
				return mViewPort;
			}
		}

		#endregion

		#region IMouseHandler Members

		public override void OnClick(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnClick(DeTransform(e));
			}
		}

		public override void OnDoubleClick(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnDoubleClick(DeTransform(e));
			}
		}

		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnMouseDown(DeTransform(e));
			}
		}

		public override void OnMouseHover(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnMouseHover(DeTransform(e));
			}
		}

		public override void OnMouseMove(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnMouseMove(DeTransform(e));
			}
		}

		public override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				MouseWithKeysEventArgs wrapped = DeTransform(e);
				Content.OnMouseUp(wrapped);
				e.Handled = wrapped.Handled;
			}
		}

		public override void OnMouseWheel(MouseWithKeysEventArgs e)
		{
			if (Content != null)
			{
				Content.OnMouseWheel(DeTransform(e));
			}
		}

		private static Point p = new Point();
		private MouseWithKeysEventArgs DeTransform(MouseWithKeysEventArgs e)
		{
			p.X = e.X;
			p.Y = e.Y;
			Scroller.DeTransformPoint(p, p);
			MouseWithKeysEventArgs Result = new MouseWithKeysEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta);
			Result.Handled = e.Handled;
			return Result;
		}

		#endregion

		#region IDrawableRect Members

		/// <summary>
		/// Draws the Content on Renderer, translating it DeltaX pixels to the right
		/// and DeltaY pixels down.
		/// </summary>
		/// <param name="Renderer">Source Renderer to draw on</param>
		public override void DrawCore(IRenderer Renderer)
		{
			if (Content != null)
			{
				DrawShape(Renderer, Content);
			}
		}

		public void DrawShape(IRenderer Renderer, IDrawableRect ShapeToDraw)
		{
			// Substitute the Renderer'child DrawOperations with our own,
			// smart, coordinate translating DrawOperations 
			// (Scroller : TranslateTransform : DrawOperations)
			Scroller.SourceDrawOperations = Renderer.DrawOperations;
			Renderer.DrawOperations = Scroller;

			// Draw on modified Renderer:
			// all coordinates are recalculated while drawing
			ShapeToDraw.Draw(Renderer);

			// restore the Renderer's child default DrawOperations
			Renderer.DrawOperations = Scroller.SourceDrawOperations;
		}

		private void Content_NeedRedraw(IDrawableRect ShapeToRedraw)
		{
			RaiseNeedRedraw(this);
		}

		//void mContent_ShapeNeedsRedraw(IDrawableRect ShapeToRedraw)
		//{
		//    RaiseShapeNeedsRedraw(ShapeToRedraw);
		//}

		//public IDrawableRect DefaultDrawHandler
		//{
		//    get
		//    {
		//        if (Content != null)
		//        {
		//            return Content.DefaultDrawHandler;
		//        }
		//        return null;
		//    }
		//    set
		//    {
		//        if (Content != null)
		//        {
		//            Content.DefaultDrawHandler = value;
		//        }
		//    }
		//}

		#endregion

		#region Scrolling

		private CompositeRange mVRange = new CompositeRange();
		public CompositeRange VRange
		{
			get
			{
				return mVRange;
			}
			set
			{
				mVRange = value;
			}
		}

		private CompositeRange mHRange = new CompositeRange();
		public CompositeRange HRange
		{
			get
			{
				return mHRange;
			}
			set
			{
				mHRange = value;
			}
		}

		public override void LayoutCore()
		{
			UpdateVRange();
			UpdateHRange();
		}

		private Point OldContentSize = new Point();

		/// <summary>
		/// 1. Content size changed
		/// </summary>
		/// <param name="ResizedShape">IShape of Content</param>
		/// <param name="OldSize">Old content size</param>
		protected void Content_SizeChanged(IShape ResizedShape, Point OldSize)
		{
			UpdateVRange();
			UpdateHRange();
			OldContentSize.Set(this.Bounds.Size);
			RaiseContentSizeChanged(ResizedShape, OldContentSize);
		}

		protected void Content_ShouldScrollTo(Rect shape)
		{
			RaiseScrollTo(shape);
		}

		public void UpdateVRange()
		{
			if (Content == null)
			{
				return;
			}

			int OldDeltaY = DeltaY;

			VRange.Pos = Content.Bounds.Location.Y;
			VRange.Size = Content.Bounds.Size.Y;

			VRange.Span.Pos = DeltaY;
			VRange.Span.Size = this.Bounds.Size.Y;

			VRange.CheckSpan();

			if (VRange.Span.Pos != OldDeltaY)
			{
				DeltaY = (int)VRange.Span.Pos;
			}
		}

		public void UpdateHRange()
		{
			if (Content == null)
			{
				return;
			}

			int OldDeltaX = DeltaX;

			HRange.Pos = Content.Bounds.Location.X;
			HRange.Size = Content.Bounds.Size.X;

			HRange.Span.Pos = DeltaX;
			HRange.Span.Size = this.Bounds.Size.X;

			HRange.CheckSpan();

			if (HRange.Span.Pos != OldDeltaX)
			{
				DeltaX = (int)HRange.Span.Pos;
			}
		}

		public void ScrollToY(int NewY)
		{
			DeltaY = NewY;
			UpdateVRange();
		}

		public void ScrollToX(int NewX)
		{
			DeltaX = NewX;
			UpdateHRange();
		}

		#endregion

		#region Keyboard

		public override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (Content != null)
			{
				Content.OnKeyDown(e);
			}
		}

		public override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if (Content != null)
			{
				Content.OnKeyPress(e);
			}
		}

		public override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			if (Content != null)
			{
				Content.OnKeyUp(e);
			}
		}

		#endregion
	}
}
