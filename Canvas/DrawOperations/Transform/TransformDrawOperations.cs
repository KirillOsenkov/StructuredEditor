using System.Collections.Generic;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.DrawOperations
{
	public class TransformDrawOperations : AbstractDrawOperations, IDrawOperations
	{
		#region SourceDrawOperations

		private IDrawOperations mSourceDrawOperations;
		public IDrawOperations SourceDrawOperations
		{
			get
			{
				return mSourceDrawOperations;
			}
			set
			{
				if (value != null && value != this)
				{
					mSourceDrawOperations = value;
					if (Viewport.Size != SourceDrawOperations.Viewport.Size)
					{
						Viewport.Size = SourceDrawOperations.Viewport.Size;
					}
				}
			}
		}

		#endregion

		#region Transform

		protected Rect R = new Rect();
		private Point P1 = new Point();
		private Point P2 = new Point();
		private Point P3 = new Point();

		protected virtual void TransformRect(Rect src)
		{
			Point p = R.Location;
			TransformPoint(src.Location, p);
			R.Size.Set(src.Size);
		}

		protected virtual void TransformPoint(Point src, Point dst)
		{
			dst.Set(src);
		}

		protected void TransformPoint(Point toTransform)
		{
			TransformPoint(toTransform, toTransform);
		}

		public virtual void DeTransformPoint(Point src, Point dst)
		{
			dst.Set(src);
		}

		protected void DeTransformPoint(Point toDetransform)
		{
			DeTransformPoint(toDetransform, toDetransform);
		}

		private void TransformPointList(IList<Point> src)
		{
			foreach (Point p in src)
			{
				Point t = p;
				TransformPoint(p, t);
			}
		}

		#endregion

		#region IDrawOperations Members

		public IDrawInfoFactory Factory
		{
			get
			{
				if (SourceDrawOperations == null)
				{
					return null;
				}
				return SourceDrawOperations.Factory;
			}
			set
			{
				if (SourceDrawOperations != null)
				{
					SourceDrawOperations.Factory = value;
				}
			}
		}

		#region Rectangle

		public void DrawRectangle(Rect theRect, ILineStyleInfo theStyle)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawRectangle(R, theStyle);
		}

		public void DrawRectangle(Rect theRect, System.Drawing.Color lineColor)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawRectangle(R, lineColor);
		}

		public void DrawDotRectangle(Rect theRect, System.Drawing.Color lineColor)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawDotRectangle(R, lineColor);
		}

		public void DrawRectangle(Rect theRect, System.Drawing.Color lineColor, int lineWidth)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawRectangle(R, lineColor, lineWidth);
		}

		public void FillRectangle(Rect theRect, IFillStyleInfo theStyle)
		{
			TransformRect(theRect);
			SourceDrawOperations.FillRectangle(R, theStyle);
		}

		public void FillRectangle(Rect theRect, System.Drawing.Color fillColor)
		{
			TransformRect(theRect);
			SourceDrawOperations.FillRectangle(R, fillColor);
		}

		public void DrawFilledRectangle(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawFilledRectangle(R, Line, Fill);
		}

		public void GradientFillRectangle(Rect theRect, System.Drawing.Color Color1, System.Drawing.Color Color2, System.Drawing.Drawing2D.LinearGradientMode GradientType)
		{
			TransformRect(theRect);
			SourceDrawOperations.GradientFillRectangle(R, Color1, Color2, GradientType);
		}

		public void GradientFill4(Rect rect, System.Drawing.Color leftTop, System.Drawing.Color rightTop, System.Drawing.Color leftBottom, System.Drawing.Color rightBottom)
		{
			TransformRect(rect);
			SourceDrawOperations.GradientFill4(
				rect, 
				leftTop, rightTop, leftBottom, rightBottom);
		}

		public void GradientFill4(Rect rect, System.Drawing.Color leftTop, System.Drawing.Color rightTop, System.Drawing.Color leftBottom, System.Drawing.Color rightBottom, int steps)
		{
			TransformRect(rect);
			SourceDrawOperations.GradientFill4(
				rect,
				leftTop, rightTop, leftBottom, rightBottom, steps);
		}

		public void DrawShadow(Rect theRect)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawShadow(R);
		}

		#endregion

		#region Ellipse

		public void DrawEllipse(Rect theRect, ILineStyleInfo theStyle)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawEllipse(R, theStyle);
		}

		public void DrawFilledEllipse(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawFilledEllipse(R, Line, Fill);
		}

		public void DrawPie(
			Rect theRect,
			double startAngleDegrees,
			double endAngleDegrees,
			System.Drawing.Color borderColor,
			System.Drawing.Color fillColor)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawPie(
				R,
				startAngleDegrees,
				endAngleDegrees,
				borderColor,
				fillColor);
		}

		#endregion

		#region Line

		public override void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor, int lineWidth)
		{
			P1.Set(x1, y1);
			P2.Set(x2, y2);
			TransformPoint(P1);
			TransformPoint(P2);
			SourceDrawOperations.DrawLine(P1.X, P1.Y, P2.X, P2.Y, lineColor, lineWidth);
		}

		public override void GradientLine(int x1, int y1, int x2, int y2, System.Drawing.Color startColor, System.Drawing.Color endColor, int lineWidth)
		{
			P1.Set(x1, y1);
			P2.Set(x2, y2);
			TransformPoint(P1);
			TransformPoint(P2);
			SourceDrawOperations.GradientLine(
				P1.X, 
				P1.Y, 
				P2.X, 
				P2.Y, 
				startColor, 
				endColor, 
				lineWidth);
		}

		#endregion

		#region Polygons

		public void DrawTriangle(
			int x1,
			int y1,
			int x2,
			int y2,
			int x3,
			int y3,
			System.Drawing.Color borderColor,
			System.Drawing.Color fillColor)
		{
			P1.Set(x1, y1);
			P2.Set(x2, y2);
			P3.Set(x3, y3);
			TransformPoint(P1, P1);
			TransformPoint(P2, P2);
			TransformPoint(P3, P3);
			SourceDrawOperations.DrawTriangle(
				P1.X,
				P1.Y,
				P2.X,
				P2.Y,
				P3.X,
				P3.Y,
				borderColor,
				fillColor);
		}

		public void FillPolygon(IList<Point> Points, ILineStyleInfo LineStyle, IFillStyleInfo FillStyle)
		{
			TransformPointList(Points);
			SourceDrawOperations.FillPolygon(Points, LineStyle, FillStyle);
		}

		#endregion

		#region Text

		public void DrawString(string Text, Rect theRect, IFontStyleInfo theFont)
		{
			TransformRect(theRect);
			SourceDrawOperations.DrawString(Text, R, theFont);
		}

		public Point StringSize(string Text, IFontInfo theFont)
		{
			return SourceDrawOperations.StringSize(Text, theFont);
		}

		public void DrawStringWithSelection(Rect Block, int StartSelPos, int CaretPosition, string Text, IFontStyleInfo FontStyle)
		{
			TransformRect(Block);
			SourceDrawOperations.DrawStringWithSelection(R, StartSelPos, CaretPosition, Text, FontStyle);
		}

		#endregion

		#region Image

		public void DrawImage(IPicture picture, Point p)
		{
			TransformPoint(p, P1);
			SourceDrawOperations.DrawImage(picture, P1);
		}

		#endregion

		//Rect oldCaretRect = new Rect();
		//public override void DrawCaret(Caret caret)
		//{
		//    if (!caret.Visible)
		//    {
		//        return;
		//    }

		//    // backup old caret coordinates
		//    oldCaretRect.Set(caret.Bounds);

		//    // transform the caret coordinates
		//    TransformRect(caret.Bounds);
		//    caret.SetNewBounds(R.Location.X, R.Location.Y, R.Size.Y);

		//    // draw the caret with transformed coordinates
		//    Source.DrawCaret(caret);

		//    // restore caret bounds
		//    caret.SetNewBounds(
		//        oldCaretRect.Location.X, 
		//        oldCaretRect.Location.Y, 
		//        oldCaretRect.Size.Y);
		//}

		#endregion
	}
}