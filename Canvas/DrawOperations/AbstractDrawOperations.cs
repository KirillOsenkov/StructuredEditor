using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using System;
using System.Drawing;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawOperations
{
	public abstract class AbstractDrawOperations
	{
		#region Viewport

		private Rect mViewport = new Rect();
		public Rect Viewport
		{
			get
			{
				return mViewport;
			}
		}

		#endregion

		#region Misc functions

		public enum EdgeType
		{
			Single,
			Raised,
			Sunken,
			Etched
		}

		// TODO: Kirill: add a function to draw raised and sunken edges,
		// similar to the Win32 API function DrawEdge
		public void DrawEdge()
		{
		}

		#endregion

		#region Line

		public abstract void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor, int lineWidth);

		public void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor)
		{
			DrawLine(x1, y1, x2, y2, lineColor, 1);
		}

		public void DrawLine(int x1, int y1, int x2, int y2, ILineStyleInfo theStyle)
		{
			DrawLine(x1, y1, x2, y2, theStyle.ForeColor, theStyle.Width);
		}

		public void DrawLine(Point p1, Point p2, ILineStyleInfo theStyle)
		{
			DrawLine(p1.X, p1.Y, p2.X, p2.Y, theStyle.ForeColor, theStyle.Width);
		}

		#endregion

		#region Gradients

		public virtual void GradientLine(
			int x1,
			int y1,
			int x2,
			int y2,
			System.Drawing.Color startColor,
			System.Drawing.Color endColor,
			int lineWidth)
		{
			const int NumberOfSteps = 128; // number of steps

			int ColorR1 = startColor.R;
			int ColorG1 = startColor.G;
			int ColorB1 = startColor.B;

			int ColorR2 = endColor.R - ColorR1;
			int ColorG2 = endColor.G - ColorG1;
			int ColorB2 = endColor.B - ColorB1;

			Common.SwapIfGreater(ref x1, ref x2);
			Common.SwapIfGreater(ref y1, ref y2);

			int deltaX = x2 - x1;
			int deltaY = y2 - y1;

			int x = x1;
			int y = y1;
			int ox = x;
			int oy = y;

			double Coeff;
			int StepSize;

			if (Math.Abs(deltaX) > Math.Abs(deltaY))
			{
				StepSize = deltaX / NumberOfSteps;
				if (StepSize < 1)
				{
					StepSize = 1;
				}
				for (int i = x1; i <= x2; i += StepSize)
				{
					Coeff = (i - x1) / (double)deltaX;

					x = i;
					if (x > x2)
					{
						x = x2;
					}
					y = (int)(y1 + deltaY * Coeff);

					DrawLine(
						ox,
						oy,
						x,
						y,
						Color.FromArgb(
							(int)(ColorR1 + (double)ColorR2 * Coeff),
							(int)(ColorG1 + (double)ColorG2 * Coeff),
							(int)(ColorB1 + (double)ColorB2 * Coeff)),
							lineWidth);

					ox = x;
					oy = y;
				}
			}
			else if (Math.Abs(deltaY) > Math.Abs(deltaX))
			{
				StepSize = deltaY / NumberOfSteps;
				if (StepSize < 1)
				{
					StepSize = 1;
				}
				for (int j = y1; j <= y2; j += StepSize)
				{
					Coeff = (j - y1) / (double)deltaY;

					y = j;
					if (y > y2)
					{
						y = y2;
					}
					x = (int)(x1 + deltaX * Coeff);

					DrawLine(
						ox,
						oy,
						x,
						y,
						Color.FromArgb(
							(int)(ColorR1 + (double)ColorR2 * Coeff),
							(int)(ColorG1 + (double)ColorG2 * Coeff),
							(int)(ColorB1 + (double)ColorB2 * Coeff)),
							lineWidth);

					ox = x;
					oy = y;
				}
			}
		}

		#endregion

		#region DrawCaret

		public virtual void DrawCaret(Caret caret)
		{
			// if (caret.Visible)
				DrawLine(
					caret.Bounds.Location.X,
					caret.Bounds.Location.Y,
					caret.Bounds.Location.X,
					caret.Bounds.Location.Y + caret.Bounds.Size.Y,
					caret.CaretStyle);
		}

		public void DrawCaret(int x, int y, int height)
		{
			DrawLine(x, y, x, y + height, RendererSingleton.MyCaret.CaretStyle);
		}

		#endregion
	}
}