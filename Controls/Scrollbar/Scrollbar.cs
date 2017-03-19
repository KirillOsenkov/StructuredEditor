using System;
using System.Diagnostics;

using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// Abstract scrollbar. Base class for HScrollbar and VScrollbar.
	/// </summary>
	public abstract class Scrollbar : ContainerControl
	{
		public Scrollbar(CompositeRange ExistingViewRange)
			: base()
		{
			#region Scrollbar()

			this.ViewRange = ExistingViewRange;

			Etalon.Pos = 0;
			Etalon.Size = 1000;
			Etalon.Span.Pos = 0;
			Etalon.Span.Size = Etalon.Size;

			ButtonPushTimer.Interval = TimeIntervalLong;
			ButtonPushTimer.Tick += ButtonPushTimer_Tick;

			#endregion
		}

		public static int InitialSize { get { return 18; } }

		#region Events

		public event ButtonPushedHandler ButtonUpPushed;
		public event ButtonPushedHandler ButtonDownPushed;
		public event ScrolledHandler Scrolled;

		public void RaiseButtonUp()
		{
			if (ButtonUpPushed != null)
			{
				ButtonUpPushed();
			}
		}

		public void RaiseButtonDown()
		{
			if (ButtonDownPushed != null)
			{
				ButtonDownPushed();
			}
		}

		protected void RaiseScrolled(int OldPosition, int NewPosition)
		{
			if (Scrolled != null)
			{
				Scrolled(OldPosition, NewPosition);
			}
			else
			{
				this.Redraw();
			}
		}

		#endregion

		#region 4 Components

		private ScrollbarButton mButtonUp;
		public ScrollbarButton ButtonUp
		{
			get
			{
				return mButtonUp;
			}
			set
			{
				if (mButtonUp != null)
				{
					this.Remove(mButtonUp);
					mButtonUp.MouseDown -= OnButtonUpMouseDown;
					mButtonUp.MouseUp -= OnButtonUpMouseUp;
					mButtonUp.NeedRedraw -= base.RaiseNeedRedraw;
				}
				mButtonUp = value;
				if (mButtonUp != null)
				{
					mButtonUp.MouseDown += OnButtonUpMouseDown;
					mButtonUp.MouseUp += OnButtonUpMouseUp;
					mButtonUp.NeedRedraw += base.RaiseNeedRedraw;
					this.Add(value);
				}
			}
		}

		private ScrollbarButton mButtonDown;
		public ScrollbarButton ButtonDown
		{
			get
			{
				return mButtonDown;
			}
			set
			{
				if (mButtonDown != null)
				{
					this.Remove(mButtonDown);
					mButtonDown.MouseDown -= OnButtonDownMouseDown;
					mButtonDown.MouseUp -= OnButtonDownMouseUp;
					mButtonDown.NeedRedraw -= base.RaiseNeedRedraw;
				}
				mButtonDown = value;
				if (mButtonDown != null)
				{
					mButtonDown.MouseDown += OnButtonDownMouseDown;
					mButtonDown.MouseUp += OnButtonDownMouseUp;
					mButtonDown.NeedRedraw += base.RaiseNeedRedraw;
					this.Add(value);
				}
			}
		}

		private ScrollbarButton mThumb;
		public ScrollbarButton Thumb
		{
			get
			{
				return mThumb;
			}
			set
			{
				if (mThumb != null)
				{
					this.Remove(mThumb);
					mThumb.MouseDown -= OnThumbMouseDown;
					mThumb.MouseMove -= OnThumbMouseMove;
					mThumb.MouseUp -= OnThumbMouseUp;
				}
				mThumb = value;
				if (mThumb != null)
				{
					mThumb.MouseDown += OnThumbMouseDown;
					mThumb.MouseMove += OnThumbMouseMove;
					mThumb.MouseUp += OnThumbMouseUp;
					this.Add(value);
				}
			}
		}

		private RectShape mBackground;
		public RectShape Background
		{
			get
			{
				return mBackground;
			}
			set
			{
				if (mBackground != null)
				{
					this.Remove(mBackground);
					mBackground.MouseDown -= OnBackgroundMouseDown;
				}
				mBackground = value;
				if (mBackground != null)
				{
					mBackground.MouseDown += OnBackgroundMouseDown;
					this.Add(mBackground);
				}
			}
		}

		#endregion

		#region WritableList Events

		#region Button press

		private const int TimeIntervalShort = 70;
		private const int TimeIntervalLong = 500;

		private System.Windows.Forms.Timer ButtonPushTimer = new System.Windows.Forms.Timer();

		private bool ButtonUpPressed = false;
		protected void OnButtonUpMouseDown(MouseWithKeysEventArgs e)
		{
			ButtonUpPressed = true;
			RaiseButtonUp();
			ButtonPushTimer.Interval = TimeIntervalLong;
			ButtonPushTimer.Start();
		}

		protected void OnButtonUpMouseUp(MouseWithKeysEventArgs e)
		{
			if (ButtonUpPressed)
			{
				ButtonUpPressed = false;
				ButtonPushTimer.Stop();
			}
		}

		private bool ButtonDownPressed = false;
		protected void OnButtonDownMouseDown(MouseWithKeysEventArgs e)
		{
			ButtonDownPressed = true;
			RaiseButtonDown();
			ButtonPushTimer.Interval = TimeIntervalLong;
			ButtonPushTimer.Start();
		}

		protected void OnButtonDownMouseUp(MouseWithKeysEventArgs e)
		{
			if (ButtonDownPressed)
			{
				ButtonDownPressed = false;
				ButtonPushTimer.Stop();
			}
		}

		private void ButtonPushTimer_Tick(object sender, EventArgs e)
		{
			if (ButtonUpPressed)
			{
				if (Etalon.Span.Pos == 0)
				{
					ButtonPushTimer.Stop();
					ButtonUpPressed = false;
				}
				else
				{
					RaiseButtonUp();
					if (ButtonPushTimer.Interval == TimeIntervalLong)
					{
						ButtonPushTimer.Interval = TimeIntervalShort;
					}
				}
			}
			if (ButtonDownPressed)
			{
				if (Etalon.Span.Pos >= Etalon.Size - Etalon.Span.Size)
				{
					ButtonPushTimer.Stop();
					ButtonDownPressed = false;
				}
				else
				{
					RaiseButtonDown();
					if (ButtonPushTimer.Interval == TimeIntervalLong)
					{
						ButtonPushTimer.Interval = TimeIntervalShort;
					}
				}
			}
		}

		/// <summary>
		/// User clicks on a scrollbar background.
		/// The thumb is expected to jump to this position and stick to the cursor.
		/// </summary>
		/// <param name="e">Cursor info</param>
		protected virtual void OnBackgroundMouseDown(MouseWithKeysEventArgs e)
		{
			if (!Thumb.Visible)
			{
				return;
			}
			MoveThumb(GetMouseCoord(e) - this.GetScrollAreaStart() - ThumbSize / 2);
			this.Capture = null;
			this.OnMouseDown(e);
			// this.List.Capture = Thumb;
			// OnThumbMouseDown(e);
		}

		protected virtual int GetMouseCoord(MouseWithKeysEventArgs e)
		{
			return e.Y;
		}

		protected virtual int ThumbSize
		{
			get
			{
				return Thumb.Bounds.Size.Y;
			}
		}

		#endregion

		#region Thumb change

		private bool mScrolling;
		public bool Scrolling
		{
			get
			{
				return mScrolling;
			}
			set
			{
				mScrolling = value;
			}
		}

		protected int oy = 0;
		protected int OldThumbPos = 0;

		protected virtual void OnThumbMouseDown(MouseWithKeysEventArgs e)
		{
			if (Scrolling)
			{
				Scrolling = false;
				return;
			}

			if (e.IsLeftButtonPressed)
			{
				Scrolling = true;
				oy = GetMouseCoord(e);
				OldThumbPos = (int)ThumbRange.Span.Pos;
			}
		}

		private int oldx = -1;
		private int oldy = -1;
		protected virtual void OnThumbMouseMove(MouseWithKeysEventArgs e)
		{
			if (Scrolling && !(e.X == oldx && e.Y == oldy))
			{
				MoveThumb(OldThumbPos + GetMouseCoord(e) - oy);

				if (!e.IsLeftButtonPressed)
				{
					Scrolling = false;
				}
				oldx = e.X;
				oldy = e.Y;
			}
		}

		private void OnThumbMouseUp(MouseWithKeysEventArgs e)
		{
			Scrolling = false;
		}

		#endregion

		#endregion

		#region Changes

		#region ScrollInfo

		private CompositeRange mEtalon = new CompositeRange();
		public CompositeRange Etalon
		{
			get
			{
				return mEtalon;
			}
			set
			{
				mEtalon = value;
			}
		}

		private CompositeRange mThumbRange = new CompositeRange();
		public CompositeRange ThumbRange
		{
			get
			{
				return mThumbRange;
			}
			set
			{
				mThumbRange = value;
			}
		}

		private CompositeRange mViewRange;
		public CompositeRange ViewRange
		{
			get
			{
				return mViewRange;
			}
			set
			{
				mViewRange = value;
			}
		}

		#endregion

		// =====================================

		public override void LayoutCore()
		{
			FillScrollAreaInfo();
			SetThumbToEtalon();
		}

		protected void FillScrollAreaInfo()
		{
			ThumbRange.Pos = GetScrollAreaStart();
			ThumbRange.Size = GetScrollAreaSize();
		}

		protected abstract int GetScrollAreaStart();
		protected abstract int GetScrollAreaSize();

		// =====================================

		/// <summary>
		/// 
		/// </summary>
		/// <param name="NewPosition">Distance from begin of scroll area to the top of the thumb (in pixels)</param>
		protected void MoveThumb(int NewPosition)
		{
			ThumbRange.Span.SetPos(NewPosition);
			UpdateThumbFromThumbRange();
			SyncToThumb();
		}

		// =====================================

		public void SyncToThumb()
		{
			Etalon.SetProportionalSpan(ThumbRange);
			if (Etalon.Span.Pos + Etalon.Span.Size + Etalon.Size / 1000 > Etalon.Size)
			{
				Etalon.Span.Pos = Etalon.Size - Etalon.Span.Size;
			}
			UpdateButtonVisibility();
			SetViewToEtalon();
		}

		public void SyncToView()
		{
			Etalon.SetProportionalSpan(ViewRange);
			UpdateButtonVisibility();
			SetThumbToEtalon();
		}

		// =====================================

		protected abstract void UpdateThumbFromThumbRange();

		protected void SetThumbToEtalon()
		{
			ThumbRange.SetProportionalSpan(Etalon);
			UpdateThumbFromThumbRange();
		}

		protected void SetViewToEtalon()
		{
			int OldPosition = (int)ViewRange.Span.Pos;
			ViewRange.SetProportionalSpan(Etalon);
			RaiseScrolled(OldPosition, (int)ViewRange.Span.Pos);
		}

		// =====================================

		protected void UpdateButtonVisibility()
		{
			if (Etalon.Span.Size < Etalon.Size)
			{
				SetButtonsVisible(true);
			}
			else
			{
				SetButtonsVisible(false);
			}
		}

		private void SetButtonsVisible(bool AreVisible)
		{
			Thumb.Visible = AreVisible;
			ButtonDown.Visible = AreVisible;
			ButtonUp.Visible = AreVisible;
		}

		// =====================================

		#endregion

		#region Draw

		public override void DrawCore(IRenderer Renderer)
		{
			Background.Draw(Renderer);
			ButtonUp.Draw(Renderer);
			ButtonDown.Draw(Renderer);
			if (GetScrollAreaSize() > 1)
			{
				Thumb.Draw(Renderer);
			}
		}

		#endregion

	}
}

