using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GuiLabs.Canvas.DrawStyle;
using Graphics = System.Drawing.Graphics;

namespace GuiLabs.Canvas.DrawOperations
{
	internal class GDIPlusDrawOperations : AbstractDrawOperations, IDrawOperations
	{
		private Graphics mGraphics;

		public GDIPlusDrawOperations(Graphics InitialGraphics)
		{
			mGraphics = InitialGraphics;
			throw new NotImplementedException(
				"GDIPlusDrawOperations is still not implemented properly. Contact kirill@guilabs.de");
		}

		// =====================================================================

		public void DrawRectangle(Rect theRect, ILineStyleInfo theStyle)
		{
			mGraphics.DrawRectangle(((GDIPlusLineStyle)theStyle).Pen, theRect.GetRectangle());
		}

		public void DrawRectangle(Rect theRect, Color lineColor)
		{

		}

		public void DrawDotRectangle(Rect theRect, Color lineColor)
		{

		}

		public void DrawRectangle(Rect theRect, Color lineColor, int lineWidth)
		{
		}

		public void DrawTriangle(
			int x1,
			int y1,
			int x2,
			int y2,
			int x3,
			int y3,
			Color borderColor,
			Color fillColor) { }

		public void DrawEllipse(Rect theRect, ILineStyleInfo theStyle)
		{
			mGraphics.DrawEllipse(((GDIPlusLineStyle)theStyle).Pen, theRect.GetRectangle());
		}

		public void FillRectangle(Rect theRect, IFillStyleInfo theStyle)
		{
			switch (theStyle.Mode)
			{
				case GuiLabs.Canvas.DrawStyle.FillMode.Solid:
					mGraphics.FillRectangle(
						((GDIPlusFillStyle)theStyle).Brush,
						theRect.GetRectangle());
					break;
				case GuiLabs.Canvas.DrawStyle.FillMode.HorizontalGradient:
					GradientFillRectangle(
						theRect,
						theStyle.FillColor,
						theStyle.GradientColor,
						LinearGradientMode.Horizontal);
					break;
				case GuiLabs.Canvas.DrawStyle.FillMode.VerticalGradient:
					GradientFillRectangle(
						theRect,
						theStyle.FillColor,
						theStyle.GradientColor,
						LinearGradientMode.Vertical);
					break;
				default:
					break;
			}
		}

		public void FillRectangle(Rect theRect, Color fillColor)
		{
			SolidBrush b = new SolidBrush(fillColor);
			mGraphics.FillRectangle(b, theRect.GetRectangle());
			b.Dispose();
		}

		public void DrawShadow(Rect theRect)
		{
			const int depth = 8;
			const int depth2 = depth * 2;

			Rect Rounding = new Rect();
			for (int i = 1; i < depth; i++)
			{
				Color col = System.Drawing.Color.FromArgb(50 - (int)(i / depth), System.Drawing.Color.Black);
				Rounding.Size.Set(i);

				Rounding.Location.Set(theRect.Right, theRect.Top + depth2 - i);
				DrawPie(Rounding, 0, 90, System.Drawing.Color.Transparent, col);

				Rounding.Location.Set(theRect.Left + depth2 - i, theRect.Bottom);
				DrawPie(Rounding, 180, 270, System.Drawing.Color.Transparent, col);

				Rounding.Location.Set(theRect.Right, theRect.Bottom);
				DrawPie(Rounding, 270, 360, System.Drawing.Color.Transparent, col);

				DrawLine(theRect.Left + depth2, theRect.Bottom + i, theRect.Right, theRect.Bottom + i, col);
				DrawLine(theRect.Right + i, theRect.Top + depth2, theRect.Right + i, theRect.Bottom, col);
			}
		}

		public void FillEllipse(Rect theRect, Color fillColor)
		{
			mGraphics.FillEllipse(new SolidBrush(fillColor), theRect.GetRectangle());
		}

		public void DrawFilledRectangle(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			FillRectangle(theRect, Fill);
			DrawRectangle(theRect, Line);
		}

		public void DrawFilledEllipse(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			FillEllipse(theRect, Fill.FillColor);
			DrawEllipse(theRect, Line);
		}

		public void DrawPie(
			Rect theRect,
			double startAngleDegrees,
			double endAngleDegrees,
			Color borderColor,
			Color fillColor) { }

		#region Gradients

		public void GradientFillRectangle(Rect theRect, Color Color1, Color Color2, LinearGradientMode GradientType)
		{
			Rectangle R = theRect.GetRectangle();
			Brush b = new LinearGradientBrush(R, Color1, Color2, GradientType);
			mGraphics.FillRectangle(b, R);
		}

		#endregion

		public void FillPolygon(IList<Point> Points, ILineStyleInfo LineStyle, IFillStyleInfo FillStyle)
		{
			GDIPlusLineStyle Line = (GDIPlusLineStyle)LineStyle;
			GDIPlusFillStyle Fill = (GDIPlusFillStyle)FillStyle;

			System.Drawing.Point[] P = new System.Drawing.Point[Points.Count];

			for (int i = 0; i < Points.Count; i++)
			{
				P[i].X = Points[i].X;
				P[i].Y = Points[i].Y;
			}

			mGraphics.FillPolygon(Fill.Brush, P);
			mGraphics.DrawPolygon(Line.Pen, P);
		}

		public override void DrawLine(int x1, int y1, int x2, int y2, Color lineColor, int lineWidth)
		{
			Pen p = new Pen(lineColor, lineWidth);
			mGraphics.DrawLine(p, x1, y1, x2, y2);
			p.Dispose();
		}

		public void DrawImage(IPicture picture, Point p)
		{
			GDIPlusPicture pict = picture as GDIPlusPicture;
			mGraphics.DrawImage(pict.Image, new System.Drawing.Point(p.X, p.Y));
		}

		private SolidBrush textBrush = new SolidBrush(Color.Black);
		private PointF textLocation = new PointF();
		public void DrawString(string Text, Rect theRect, IFontStyleInfo theFont)
		{
			if (theFont.ForeColor != textBrush.Color)
			{
				textBrush.Color = theFont.ForeColor;
			}
			theRect.Location.FillPoint(ref textLocation);
			mGraphics.DrawString(
				Text,
				((GDIPlusFontWrapper)(theFont.Font)).Font,
				textBrush,
				textLocation);
		}

		public Point StringSize(string Text, IFontInfo theFont)
		{
			SizeF s = new SizeF();

			s = mGraphics.MeasureString(Text, ((GDIPlusFontWrapper)theFont).Font);

			Point result = new Point((int)s.Width, (int)s.Height);
			return result;
		}

		// =====================================================================

		private IDrawInfoFactory mFactory = new GDIPlusDrawInfoFactory();
		public IDrawInfoFactory Factory
		{
			get
			{
				return mFactory;
			}
			set
			{
				mFactory = value;
			}
		}

		// TODO: Implement DrawStringWithSelection
		public void DrawStringWithSelection(Rect Block, int StartSelPos, int CaretPosition, string Text, IFontStyleInfo FontStyle)
		{
		}

		public void GradientFill4(Rect rect, Color leftTop, Color rightTop, Color leftBottom, Color rightBottom)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void GradientFill4(Rect rect, Color leftTop, Color rightTop, Color leftBottom, Color rightBottom, int steps)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
