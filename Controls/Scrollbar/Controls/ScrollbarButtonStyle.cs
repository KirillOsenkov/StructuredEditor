using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	public class ScrollbarButtonStyle
	{
		public ScrollbarButtonStyle()
		{
			InitStyles();
		}

		private void InitStyles()
		{
			LightBorder = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(System.Drawing.Color.GhostWhite, 1);
			DarkBorder = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(System.Drawing.Color.Black, 1);
			Background = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFillStyleInfo(System.Drawing.Color.LightGray);
		}

		public virtual void Draw(ScrollbarButton Button, IRenderer Renderer)
		{
            ILineStyleInfo TopLeft;
            ILineStyleInfo BottomRight;

            if (Button.State == ScrollbarButton.ButtonState.Normal)
            {
                TopLeft = LightBorder;
                BottomRight = DarkBorder;
            }
            else
            {
                TopLeft = DarkBorder;
                BottomRight = LightBorder;
            }

            Rect R = Button.Bounds;
            int x1 = R.Location.X;
            int y1 = R.Location.Y;
            int x2 = R.Right;
            int y2 = R.Bottom;

            DrawBackground(Button, Renderer);

            Renderer.DrawOperations.DrawLine(x1, y1, x2, y1, TopLeft);
            Renderer.DrawOperations.DrawLine(x1, y1, x1, y2, TopLeft);
            Renderer.DrawOperations.DrawLine(x2, y1, x2, y2, BottomRight);
            Renderer.DrawOperations.DrawLine(x1, y2, x2 + 1, y2, BottomRight);
		}

		protected virtual void DrawBackground(ScrollbarButton button, IRenderer Renderer)
		{
			Renderer.DrawOperations.FillRectangle(button.Bounds, Background);
		}

		private ILineStyleInfo mLightBorder;
		protected ILineStyleInfo LightBorder
		{
			get
			{
				return mLightBorder;
			}
			set
			{
				mLightBorder = value;
			}
		}

		private ILineStyleInfo mDarkBorder;
		protected ILineStyleInfo DarkBorder
		{
			get
			{
				return mDarkBorder;
			}
			set
			{
				mDarkBorder = value;
			}
		}

		private IFillStyleInfo mBackground;
		protected IFillStyleInfo Background
		{
			get
			{
				return mBackground;
			}
			set
			{
				mBackground = value;
			}
		}
	}
}
