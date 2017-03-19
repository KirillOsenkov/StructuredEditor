using System;
using System.Windows.Forms;

namespace GuiLabs.Canvas.Events
{
	/// <summary>
	/// Extends mouse event parameter with information
	/// which keys are pressed at the moment
	/// </summary>
	public class MouseWithKeysEventArgs : MouseEventArgs
	{
		public MouseWithKeysEventArgs()
			: base(Control.MouseButtons, 0, 0, 0, 0)
		{
		}

		public MouseWithKeysEventArgs(MouseEventArgs ea)
			: base(ea.Button, ea.Clicks, ea.X, ea.Y, ea.Delta)
		{
		}

		public MouseWithKeysEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
			: base(button, clicks, x, y, delta)
		{
		}

		private Keys mKeysPressed = Control.ModifierKeys;
		public Keys KeysPressed
		{
			get
			{
				return this.mKeysPressed;
			}
			set
			{
				this.mKeysPressed = value;
			}
		}

		public bool IsAltPressed
		{
			get
			{
				return ((this.KeysPressed & Keys.Alt) == Keys.Alt);
			}
		}

		public bool IsCtrlPressed
		{
			get
			{
				return ((this.KeysPressed & Keys.Control) == Keys.Control);
			}
		}

		public bool IsShiftPressed
		{
			get
			{
				return ((this.KeysPressed & Keys.Shift) == Keys.Shift);
			}
		}

		public bool IsLeftButtonPressed
		{
			get
			{
				return ((this.Button & MouseButtons.Left) == MouseButtons.Left);
			}
		}

		public bool IsRightButtonPressed
		{
			get
			{
				return ((this.Button & MouseButtons.Right) == MouseButtons.Right);
			}
		}

		private bool mHandled = false;
		public bool Handled
		{
			get
			{
				return mHandled;
			}
			set
			{
				mHandled = value;
			}
		}
	}
}