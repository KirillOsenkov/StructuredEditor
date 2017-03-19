using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public abstract partial class ContainerControl : Control
	{
		#region IMouseHandler Members

		private IShape mCapture = null;
		/// <summary>
		/// A child shape which has captured the mouse input
		/// and becomes all mouse events from this ContainerControl
		/// until it releases capture
		/// </summary>
		/// <remarks>
		/// Similar to keyboard focus
		/// </remarks>
		public IShape Capture
		{
			get
			{
				return mCapture;
			}
			set
			{
				mCapture = value;
			}
		}

		public override void OnClick(MouseWithKeysEventArgs e)
		{
			DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnClick(e);
			}

			if (e.Handled)
			{
				return;
			}

			RaiseClick(e);
		}

		public override void OnDoubleClick(MouseWithKeysEventArgs e)
		{
			if (Capture != null)
			{
				Capture = null;
			}

			DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnDoubleClick(e);
			}

			if (e.Handled)
			{
				return;
			}

			RaiseDoubleClick(e);
		}

		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			if (Capture != null)
			{
				Capture.OnMouseDown(e);
			}
			else
			{
				IShape clicked = ShapeToForwardMouseEventTo(e);
				DefaultMouseHandler = clicked;
				if (DefaultMouseHandler != null)
				{
					Capture = clicked;
					DefaultMouseHandler.OnMouseDown(e);
				}
			}

			if (e.Handled)
			{
				return;
			}

			RaiseMouseDown(e);
		}

		public override void OnMouseHover(MouseWithKeysEventArgs e)
		{
			DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseHover(e);
			}

			if (e.Handled)
			{
				return;
			}

			RaiseMouseHover(e);
		}

		public override void OnMouseMove(MouseWithKeysEventArgs e)
		{
			if (Capture != null)
			{
				Capture.OnMouseMove(e);
			}
			else
			{
				DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
				if (DefaultMouseHandler != null)
				{
					DefaultMouseHandler.OnMouseMove(e);
				}
			}

			if (e.Handled)
			{
				return;
			}

			RaiseMouseMove(e);
		}

		public override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			if (Capture != null)
			{
				Capture.OnMouseUp(e);
				Capture = null;
			}
			else
			{
				DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
				if (DefaultMouseHandler != null)
				{
					DefaultMouseHandler.OnMouseUp(e);
				}
			}

			if (e.Handled)
			{
				return;
			}

			RaiseMouseUp(e);
		}

		public override void OnMouseWheel(MouseWithKeysEventArgs e)
		{
			if (Capture != null)
			{
				Capture = null;
			}

			DefaultMouseHandler = ShapeToForwardMouseEventTo(e);
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseWheel(e);
			}

			if (e.Handled)
			{
				return;
			}

			RaiseMouseWheel(e);
		}

		#endregion
	}
}
