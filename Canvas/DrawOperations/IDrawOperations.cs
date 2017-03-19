using System.Collections.Generic;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.DrawStyle;
using System.Drawing;

namespace GuiLabs.Canvas.DrawOperations
{
	public interface IDrawOperations
	{
		IDrawInfoFactory Factory
		{
			get;
			set;
		}

		Rect Viewport { get; }

		void DrawLine(Point p1, Point p2, ILineStyleInfo theStyle);
		void DrawLine(int x1, int y1, int x2, int y2, ILineStyleInfo theStyle);
		void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor);
		void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor, int lineWidth);

		void GradientLine(
			int x1,
			int y1,
			int x2,
			int y2,
			System.Drawing.Color startColor,
			System.Drawing.Color endColor,
			int lineWidth);

		void DrawTriangle(
			int x1,
			int y1,
			int x2,
			int y2,
			int x3,
			int y3,
			System.Drawing.Color borderColor,
			System.Drawing.Color fillColor);

		void DrawRectangle(Rect theRect, ILineStyleInfo theStyle);
		void DrawRectangle(Rect theRect, System.Drawing.Color lineColor);
		// stefan
		void DrawDotRectangle(Rect theRect, System.Drawing.Color lineColor);
		// stefan
		void DrawRectangle(Rect theRect, System.Drawing.Color lineColor, int lineWidth);
		void DrawEllipse(Rect theRect, ILineStyleInfo theStyle);
		void FillRectangle(Rect theRect, IFillStyleInfo theStyle);
		void FillRectangle(Rect theRect, System.Drawing.Color fillColor);
		void DrawFilledRectangle(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill);
		void DrawFilledEllipse(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill);

		void DrawShadow(Rect theRect);

		void DrawPie(
			Rect theRect,
			double startAngleDegrees,
			double endAngleDegrees,
			System.Drawing.Color borderColor,
			System.Drawing.Color fillColor);

		void FillPolygon(IList<Point> Points, ILineStyleInfo LineStyle, IFillStyleInfo FillStyle);

		void GradientFillRectangle(
			Rect theRect,
			System.Drawing.Color Color1,
			System.Drawing.Color Color2,
			System.Drawing.Drawing2D.LinearGradientMode GradientType);

		void GradientFill4(
			Rect rect,
			Color leftTop,
			Color rightTop,
			Color leftBottom,
			Color rightBottom);

		void GradientFill4(
			Rect rect,
			Color leftTop,
			Color rightTop,
			Color leftBottom,
			Color rightBottom,
			int steps);

		void DrawString(string Text, Rect theRect, IFontStyleInfo theFont);
		Point StringSize(string Text, IFontInfo theFont);
		void DrawStringWithSelection(Rect Block, int StartSelPos, int CaretPosition, string Text, IFontStyleInfo FontStyle);
		void DrawCaret(Caret Car);
		void DrawCaret(int x, int y, int height);
		void DrawImage(IPicture picture, Point point);
	}
}
