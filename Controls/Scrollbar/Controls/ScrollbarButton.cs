using GuiLabs.Canvas.Shapes;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.DrawStyle;


namespace GuiLabs.Canvas.Controls
{
	public class ScrollbarButton : Control
	{
		public ScrollbarButton()
			: base()
		{
			MyButtonStyle = new ScrollbarButtonStyle();
			this.Bounds.Size.Set(16, 16);
		}

		private ScrollbarButtonStyle mMyButtonStyle;
		public ScrollbarButtonStyle MyButtonStyle
		{
			get { return mMyButtonStyle; }
			set { mMyButtonStyle = value; }
		}

		public override void DrawCore(IRenderer Renderer)
		{
			if (this.Visible) MyButtonStyle.Draw(this, Renderer);
		}

		#region State

		public enum ButtonState
		{
			Normal,
			Pushed
		}

		private ButtonState mState = ButtonState.Normal;
		public ButtonState State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
			}
		}

		#endregion
	}
}
