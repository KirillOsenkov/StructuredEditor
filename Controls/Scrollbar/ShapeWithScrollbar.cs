using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public class ShapeWithScrollbar : ContainerControl
	{
		#region ctors

		public ShapeWithScrollbar()
			: this(null)
		{
		}

		public ShapeWithScrollbar(IScrollableContainer SourceShape)
			: base()
		{
			Scrollable = new ScrollableContainer(SourceShape);
			VScroll = ScrollbarFactory.Instance.CreateVScrollbar(Scrollable.VRange);
			HScroll = ScrollbarFactory.Instance.CreateHScrollbar(Scrollable.HRange);
			BottomRightCorner = ScrollbarFactory.Instance.CreateRectangle();

			SuspendLayout = true;
			Add(Scrollable);
			Add(VScroll);
			Add(HScroll);
			Add(BottomRightCorner);
			SuspendLayout = false;
		}

		#endregion

		#region ScrollbarsVisible

		private bool mScrollbarsVisible = true;
		public bool ScrollbarsVisible
		{
			get
			{
				return mScrollbarsVisible;
			}
			set
			{
				if (mScrollbarsVisible != value)
				{
					mScrollbarsVisible = value;
					VScroll.Visible = value;
					HScroll.Visible = value;
					BottomRightCorner.Visible = value;
					Layout();
				}
			}
		}

		#endregion

		#region Scrollable content

		private ScrollableContainer mScrollable;
		public ScrollableContainer Scrollable
		{
			get
			{
				return mScrollable;
			}
			set
			{
				if (mScrollable != null)
				{
					mScrollable.NeedRedraw -= Scrollable_NeedRedraw;
					mScrollable.ContentSizeChanged -= Scrollable_ContentSizeChanged;
					mScrollable.ShouldScrollTo -= Scrollable_ShouldScrollTo;
				}
				mScrollable = value;
				if (mScrollable != null)
				{
					mScrollable.NeedRedraw += Scrollable_NeedRedraw;
					mScrollable.ContentSizeChanged += Scrollable_ContentSizeChanged;
					mScrollable.ShouldScrollTo += Scrollable_ShouldScrollTo;
				}
			}
		}

		private bool mShouldHandleScrollRequestsFromContent = true;
		public bool ShouldHandleScrollRequestsFromContent
		{
			get
			{
				return mShouldHandleScrollRequestsFromContent;
			}
			set
			{
				mShouldHandleScrollRequestsFromContent = value;
			}
		}

		protected void Scrollable_ShouldScrollTo(Rect shape)
		{
			if (!ShouldHandleScrollRequestsFromContent)
			{
				return;
			}

			//Rect absolute = new Rect(shape);
			//absolute.Location.Add(-Scrollable.DeltaX, -Scrollable.DeltaY);

			Rect.RectangleRelativePosition relativePosition = shape.RelativeToRectY(Scrollable.ViewPort);
			if (relativePosition != Rect.RectangleRelativePosition.FullyInside 
				&& relativePosition != Rect.RectangleRelativePosition.FullyContaining)
			{
				ScrollContentXY(shape.Location.X, shape.Location.Y);
			}
		}

		#endregion

		#region Scrollbars

		private VScrollbar mVScroll;
		public VScrollbar VScroll
		{
			get
			{
				return mVScroll;
			}
			set
			{
				if (mVScroll != null)
				{
					mVScroll.NeedRedraw -= VScroll_NeedRedraw;
					mVScroll.Scrolled -= VScroll_Scrolled;
					mVScroll.ButtonDownPushed -= VScroll_ButtonDownPushed;
					mVScroll.ButtonUpPushed -= VScroll_ButtonUpPushed;
				}
				mVScroll = value;
				if (mVScroll != null)
				{
					mVScroll.NeedRedraw += VScroll_NeedRedraw;
					mVScroll.Scrolled += VScroll_Scrolled;
					mVScroll.ButtonDownPushed += VScroll_ButtonDownPushed;
					mVScroll.ButtonUpPushed += VScroll_ButtonUpPushed;
				}
			}
		}

		private HScrollbar mHScroll;
		public HScrollbar HScroll
		{
			get
			{
				return mHScroll;
			}
			set
			{
				if (mHScroll != null)
				{
					mHScroll.NeedRedraw -= HScroll_NeedRedraw;
					mHScroll.Scrolled -= HScroll_Scrolled;
					mHScroll.ButtonDownPushed -= HScroll_ButtonDownPushed;
					mHScroll.ButtonUpPushed -= HScroll_ButtonUpPushed;
				}
				mHScroll = value;
				if (mHScroll != null)
				{
					mHScroll.NeedRedraw += HScroll_NeedRedraw;
					mHScroll.Scrolled += HScroll_Scrolled;
					mHScroll.ButtonDownPushed += HScroll_ButtonDownPushed;
					mHScroll.ButtonUpPushed += HScroll_ButtonUpPushed;
				}
			}
		}

		private RectShape mBottomRightCorner;
		public RectShape BottomRightCorner
		{
			get { return mBottomRightCorner; }
			set { mBottomRightCorner = value; }
		}

		#endregion

		public override void LayoutCore()
		{
			int ScrollbarSize = Scrollbar.InitialSize;

			Scrollable.Bounds.Location = this.Bounds.Location;
			int ScrollbarsMargin = (this.ScrollbarsVisible ? ScrollbarSize : 0);
			Scrollable.Bounds.Size.X = this.Bounds.Size.X - ScrollbarsMargin;
			Scrollable.Bounds.Size.Y = this.Bounds.Size.Y - ScrollbarsMargin;
			Scrollable.LayoutCore();

			VScroll.Bounds.Location.Set(Scrollable.Bounds.Right, Scrollable.Bounds.Location.Y);
			VScroll.Bounds.Size.Set(ScrollbarSize, Scrollable.Bounds.Size.Y);
			VScroll.LayoutCore();

			HScroll.Bounds.Location.Set(Scrollable.Bounds.Location.X, Scrollable.Bounds.Bottom);
			HScroll.Bounds.Size.Set(Scrollable.Bounds.Size.X, ScrollbarSize);
			HScroll.LayoutCore();

			BottomRightCorner.Bounds.Location.Set(VScroll.Bounds.Location.X, HScroll.Bounds.Location.Y);
			BottomRightCorner.Bounds.Size.Set(VScroll.Bounds.Size.X, HScroll.Bounds.Size.Y);

			VScroll.SyncToView();
			HScroll.SyncToView();
		}

		protected void Scrollable_ContentSizeChanged(IShape ResizedShape, Point OldSize)
		{
			VScroll.SyncToView();
			HScroll.SyncToView();
			// RaiseNeedRedraw(this);
		}

		protected void Scrollable_NeedRedraw(IDrawableRect ShapeToRedraw)
		{
			RaiseNeedRedraw();
		}

		protected void VScroll_NeedRedraw(IDrawableRect ShapeToRedraw)
		{
			RaiseNeedRedraw();
		}

		protected void HScroll_NeedRedraw(IDrawableRect ShapeToRedraw)
		{
			RaiseNeedRedraw();
		}

		protected void VScroll_Scrolled(int OldPosition, int NewPosition)
		{
			Scrollable.ScrollToY(NewPosition);
			RaiseNeedRedraw(this);
		}

		protected void HScroll_Scrolled(int OldPosition, int NewPosition)
		{
			Scrollable.ScrollToX(NewPosition);
			RaiseNeedRedraw(this);
		}

		public override void OnMouseWheel(MouseWithKeysEventArgs e)
		{
			if (VScroll.Scrolling)
			{
				return;
			}

			// size of one logical "line" in Windows
			const int WheelAmount = 25;

			// number of logical units in one mouse wheel rotation, typically 120
			int Delta = System.Windows.Forms.SystemInformation.MouseWheelScrollDelta;

			// number of lines to scroll with each mouse wheel rotation
			int Lines = System.Windows.Forms.SystemInformation.MouseWheelScrollLines;

			// scroll WheelAmount * Lines pixels
			int FinalAmount = (int)((double)-e.Delta * WheelAmount * Lines / Delta);
			ScrollContentSmooth(
				(int)Scrollable.VRange.Span.Pos + FinalAmount, 
				ScrollContentY);
		}

		// ========================================

		private const int ScrollAmount = 24;

		// ========================================

		public delegate void ScrollContentDelegate(int NewPosition);

		#region Public API

		public void ScrollContentY(int NewPosition)
		{
			if (!VScroll.Thumb.Visible)
			{
				return;
			}
			Scrollable.ScrollToY(NewPosition);
			VScroll.SyncToView();
		}

		public void ScrollContentYSmooth(int newPosition)
		{
			ScrollContentSmooth(
				newPosition,
				ScrollContentY);
		}

		public void ScrollContentX(int NewPosition)
		{
			if (!HScroll.Thumb.Visible)
			{
				return;
			}
			Scrollable.ScrollToX(NewPosition);
			HScroll.SyncToView();
		}

		public void ScrollContentXSmooth(int newPosition)
		{
			ScrollContentSmooth(
				newPosition,
				ScrollContentX);
		}

		public void ScrollContentXY(int x, int y)
		{
			if (VScroll.Thumb.Visible)
			{
				Scrollable.ScrollToY(y);
				VScroll.SyncToView();
			}
			if (HScroll.Thumb.Visible)
			{
				Scrollable.ScrollToX(x);
				HScroll.SyncToView();
			}
			// RaiseNeedRedraw(this);
		}

		public void ScrollContentXYSmooth(int newX, int newY)
		{
			//double OldPosition = Scrollable.VRange.Span.Pos;
			//double Position = OldPosition;

			//double Duration = 400; // milliseconds
			//const double AnimationSteps = 6; // animation steps

			//Timer timer = new Timer();
			//double StartTime = timer.Milliseconds();
			//double EndTime = StartTime + Duration;
			//double StepDuration = Duration / AnimationSteps;

			//for (int i = 0; i < AnimationSteps; i++)
			//{
			//    Position = OldPosition + (NewPosition - OldPosition) * (double)i / AnimationSteps;
			//    if ((timer.Milliseconds() - StartTime) / Duration <= (double)i / AnimationSteps)
			//    {
			//        ScrollFunc((int)Position);
			//        RaiseNeedRedraw();
			//    }
			//}
			//ScrollFunc(NewPosition);
			//RaiseNeedRedraw();
		}

		#endregion

		public void ScrollContentSmooth(int NewPosition, ScrollContentDelegate ScrollFunc)
		{
			double OldPosition = Scrollable.VRange.Span.Pos;
			double Position = OldPosition;

			double Duration = 400; // milliseconds
			const double AnimationSteps = 1; // animation steps

			Timer timer = new Timer();
			double StartTime = timer.Milliseconds();
			double EndTime = StartTime + Duration;
			double StepDuration = Duration / AnimationSteps;

			for (int i = 0; i < AnimationSteps; i++)
			{
				Position = OldPosition + (NewPosition - OldPosition) * (double)i / AnimationSteps;
				if ((timer.Milliseconds() - StartTime) / Duration <= (double)i / AnimationSteps)
				{
					ScrollFunc((int)Position);
					RaiseNeedRedraw();
				}
			}
			ScrollFunc(NewPosition);
			RaiseNeedRedraw();
		}

		public void ScrollDeltaY(int Delta)
		{
			ScrollContentY((int)(Scrollable.VRange.Span.Pos + Delta));
		}

		public void ScrollDeltaX(int Delta)
		{
			ScrollContentX((int)(Scrollable.HRange.Span.Pos + Delta));
		}

		public void ScrollUp()
		{
			ScrollDeltaY(-ScrollAmount);
		}

		public void ScrollDown()
		{
			ScrollDeltaY(ScrollAmount);
		}

		public void ScrollLeft()
		{
			ScrollDeltaX(-ScrollAmount);
		}

		public void ScrollRight()
		{
			ScrollDeltaX(ScrollAmount);
		}

		public void ScrollPageUp()
		{
			ScrollDeltaY((int)-Scrollable.VRange.Span.Size);
		}

		public void ScrollPageDown()
		{
			ScrollDeltaY((int)Scrollable.VRange.Span.Size);
		}

		#region Physical scroll request (Button pushed)

		private void VScroll_ButtonDownPushed()
		{
			ScrollDown();
			RaiseNeedRedraw();
		}

		private void VScroll_ButtonUpPushed()
		{
			ScrollUp();
			RaiseNeedRedraw();
		}

		private void HScroll_ButtonUpPushed()
		{
			ScrollLeft();
			RaiseNeedRedraw();
		}

		private void HScroll_ButtonDownPushed()
		{
			ScrollRight();
			RaiseNeedRedraw();
		}

		#endregion
	}
}
