using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	public class ArrowButtonStyle : ScrollbarButtonStyle
	{
		public enum ArrowType
		{
			Up,
			Down,
			Left,
			Right
		}

		public ArrowButtonStyle(ArrowType ButtonType)
		{
			MyType = ButtonType;
			p[0] = new Point();
			p[1] = new Point();
			p[2] = new Point();
			//p.Add(new Point(10, 10));
			//p.Add(new Point(20, 20));
			//p.Add(new Point(15, 5));
			//BlackLine = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(System.Drawing.Color.Black, 1);
			//BlackFill = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(System.Drawing.Color.Black);
		}

		private ArrowType MyType;

		//private ILineStyleInfo BlackLine;
		//private IFillStyleInfo BlackFill;
		private Point[] p = new Point[3];

		public override void Draw(ScrollbarButton Button, IRenderer Renderer)
		{
			base.Draw(Button, Renderer);

			const int cx = 3;
			const int cy = cx / 2;
			const int d = cx - cy;
			int x1 = Button.Bounds.Location.X + (int)(Button.Bounds.Size.X / 2);
			int y1 = Button.Bounds.Location.Y + (int)(Button.Bounds.Size.Y / 2) + cy;

			if (Button.State == ScrollbarButton.ButtonState.Pushed)
			{
				x1++;
				y1++;
			}

			if (MyType == ArrowType.Up)
			{
				p[0].Set(x1 - cx, y1);
				p[1].Set(x1, y1 - cx);
				p[2].Set(x1 + cx, y1);
			}
			else if (MyType == ArrowType.Left)
			{
				p[0].Set(x1 - d, y1);
				p[1].Set(x1 + cy, y1 - cx);
				p[2].Set(x1 + cy, y1 + cx);
			}
			else if (MyType == ArrowType.Right)
			{
				p[0].Set(x1 - cy, y1 - cx);
				p[1].Set(x1 - cy, y1 + cx);
				p[2].Set(x1 + d, y1);
			}
			else // if (MyType == ArrowType.Down)
			{
				p[0].Set(x1 - cx, y1 - d);
				p[1].Set(x1, y1 + cy);
				p[2].Set(x1 + cx, y1 - d);
			}

			// Renderer.DrawOperations.FillPolygon(p, this.BlackLine, this.BlackFill);
            Renderer.DrawOperations.DrawTriangle(
                p[0].X,
                p[0].Y,
                p[1].X,
                p[1].Y,
                p[2].X,
                p[2].Y,
                System.Drawing.Color.Black,
                System.Drawing.Color.Black);
		}
	}
}