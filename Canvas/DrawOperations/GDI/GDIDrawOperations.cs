using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Utils;
using System.Drawing;
using System.Diagnostics;

namespace GuiLabs.Canvas.DrawOperations
{
	internal class GDIDrawOperations : AbstractDrawOperations, IDrawOperations
	{
		public GDIDrawOperations(IntPtr InitialDC)
		{
			hDC = InitialDC;
			API.SetBkMode(hDC, 1);
		}

		private IntPtr hDC = IntPtr.Zero;
		private API.POINT NULLPOINT;

		#region Rectangle

		public void DrawShadow(Rect theRect)
		{
			API.SetROP2(hDC, 9);

			const int depth = 10;
			const int depth2 = depth * 2;
			const double gray = 0.85;
			System.Drawing.Color white = System.Drawing.Color.White;

			if (theRect.Width < depth2)
			{
				return;
			}

			Rect Rounding = new Rect();
			for (int i = depth; i >= 0; i--)
			{
				System.Drawing.Color col = Colors.ScaleColor(System.Drawing.Color.White, (float)(gray + (1.0 - gray) * ((double)i / depth)));
				Rounding.Size.Set(i * 2);

				// upper right rounding
				Rounding.Location.Set(theRect.Right - i + 1, theRect.Top + depth2 - i + 1);
				API.SetROP2(hDC, 15);
				DrawPie(Rounding, 0, 90, white, white);
				API.SetROP2(hDC, 9);
				DrawPie(Rounding, 0, 90, col, col);

				// lower right rounding
				Rounding.Location.Set(theRect.Right - i + 1, theRect.Bottom - i + 1);
				API.SetROP2(hDC, 15);
				DrawPie(Rounding, 270, 360, white, white);
				API.SetROP2(hDC, 9);
				DrawPie(Rounding, 270, 360, col, col);

				// lower left rounding
				//Rounding.Location.Set(theRect.Left + depth2 - i + 1, theRect.Bottom - i + 1);
				//API.SetROP2(hDC, rop);
				//rop = (rop + 1) % 16;
				////DrawPie(Rounding, 180, 270, white, white);
				//DrawPie(Rounding, 180, 270, col, col);

				API.SetROP2(hDC, 9);

				int y = theRect.Bottom + i;

				GradientLine(
					theRect.Left + depth + i,
					y,
					theRect.Left + depth2,
					y,
					white,
					col,
					1);

				// horizontal 
				DrawLine(
					theRect.Left + depth2,
					y,
					theRect.Right,
					y,
					col);

				// vertical
				DrawLine(
					theRect.Right + i,
					theRect.Top + depth2 + 1,
					theRect.Right + i,
					theRect.Bottom + 1,
					col);
			}

			API.SetROP2(hDC, 13);
		}

		public void DrawFilledRectangle(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			GDILineStyle LineStyle = (GDILineStyle)Line;
			GDIFillStyle FillStyle = (GDIFillStyle)Fill;

			if (FillStyle.Mode != GuiLabs.Canvas.DrawStyle.FillMode.Solid)
			{
				GradientFillRectangle(
					theRect,
					FillStyle.FillColor,
					FillStyle.GradientColor,
					FillStyle.Mode ==
					GuiLabs.Canvas.DrawStyle.FillMode.HorizontalGradient
					? System.Drawing.Drawing2D.LinearGradientMode.Horizontal
					: LinearGradientMode.Vertical);
				DrawRectangle(theRect, Line);
				return;
			}

			IntPtr hPen = API.CreatePen(0, Line.Width, LineStyle.Win32ForeColor);
			IntPtr hOldPen = API.SelectObject(hDC, hPen);
			IntPtr hBrush = API.CreateSolidBrush(FillStyle.Win32FillColor);
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.DeleteObject(hBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void FillRectangle(Rect theRect, IFillStyleInfo theStyle)
		{
			if (theStyle.FillColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			switch (theStyle.Mode)
			{
				case GuiLabs.Canvas.DrawStyle.FillMode.Solid:
					FillRectangle(
						theRect,
						theStyle.FillColor);
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

		public void FillRectangle(Rect theRect, System.Drawing.Color fillColor)
		{
			if (fillColor != System.Drawing.Color.Transparent)
			{
				API.FillRectangle(hDC, fillColor, theRect.GetRectangle());
			}
		}

		public void DrawRectangle(Rect theRect, System.Drawing.Color lineColor)
		{
			if (lineColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			IntPtr hPen = API.CreatePen(API.PenStyle.PS_SOLID, 1, System.Drawing.ColorTranslator.ToWin32(lineColor));
			IntPtr hOldPen = API.SelectObject(hDC, hPen);

			IntPtr hBrush = API.GetStockObject(5); // NULL_BRUSH
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void DrawDotRectangle(Rect theRect, System.Drawing.Color lineColor)
		{
			if (lineColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			IntPtr hPen = API.CreatePen(API.PenStyle.PS_DOT, 1, System.Drawing.ColorTranslator.ToWin32(lineColor));
			IntPtr hOldPen = API.SelectObject(hDC, hPen);

			IntPtr hBrush = API.GetStockObject(5); // NULL_BRUSH
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void DrawRectangle(Rect theRect, System.Drawing.Color lineColor, int lineWidth)
		{
			if (lineColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			SetPenBrush(lineColor, lineWidth, System.Drawing.Color.Transparent);

			//IntPtr hPen = API.CreatePen(API.PenStyle.PS_SOLID, lineWidth, ColorTranslator.ToWin32(lineColor));
			//IntPtr hOldPen = API.SelectObject(hDC, hPen);

			//IntPtr hBrush = API.GetStockObject(5); // NULL_BRUSH
			//IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			ResetPenBrush();

			//API.SelectObject(hDC, hOldBrush);
			//API.SelectObject(hDC, hOldPen);
			//API.DeleteObject(hPen);
		}

		public void DrawRectangle(Rect theRect, ILineStyleInfo theStyle)
		{
			if (theStyle.ForeColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			GDILineStyle Style = (GDILineStyle)theStyle;

			IntPtr hPen = API.CreatePen(0, theStyle.Width, Style.Win32ForeColor);
			IntPtr hOldPen = API.SelectObject(hDC, hPen);

			IntPtr hBrush = API.GetStockObject(5); // NULL_BRUSH
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);

			//			int hPen = API.CreatePen(0, theStyle.Width, theStyle.Win32ForeColor);
			//			int hOldPen = API.SelectObject(hDC, hPen);
			//
			//			int left = theRect.Location.X;
			//			int top = theRect.Location.Y;
			//			int right = theRect.Right;
			//			int bottom = theRect.Bottom;
			//
			//			API.MoveToEx(hDC, left, top, ref NULLPOINTAPI);
			//			API.LineTo(hDC, right, top);
			//			API.LineTo(hDC, right, bottom);
			//			API.LineTo(hDC, left, bottom);
			//			API.LineTo(hDC, left, top);
			//
			//			API.SelectObject(hDC, hOldPen);
			//			API.DeleteObject(hPen);
		}

		#endregion

		#region Ellipse

		public void DrawEllipse(Rect theRect, ILineStyleInfo theStyle)
		{
			GDILineStyle Style = theStyle as GDILineStyle;
			if (Style == null)
			{
				Log.Instance.WriteWarning("DrawEllipse: Style == null");
				return;
			}

			IntPtr hPen = API.CreatePen(0, theStyle.Width, Style.Win32ForeColor);
			IntPtr hOldPen = API.SelectObject(hDC, hPen);

			IntPtr hBrush = API.GetStockObject(5); // NULL_BRUSH
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Ellipse(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void DrawFilledEllipse(Rect theRect, ILineStyleInfo Line, IFillStyleInfo Fill)
		{
			GDILineStyle LineStyle = (GDILineStyle)Line;
			GDIFillStyle FillStyle = (GDIFillStyle)Fill;

			IntPtr hPen = API.CreatePen(0, Line.Width, LineStyle.Win32ForeColor);
			IntPtr hOldPen = API.SelectObject(hDC, hPen);
			IntPtr hBrush = API.CreateSolidBrush(FillStyle.Win32FillColor);
			IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

			API.Ellipse(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);

			API.SelectObject(hDC, hOldBrush);
			API.DeleteObject(hBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void DrawPie(
			Rect theRect,
			double startAngleDegrees,
			double endAngleDegrees,
			System.Drawing.Color borderColor,
			System.Drawing.Color fillColor)
		{
			SetPenBrush(borderColor, 1, fillColor);

			API.Pie(
				hDC,
				theRect.Location.X,
				theRect.Location.Y,
				theRect.Right,
				theRect.Bottom,
				constructRadialX(theRect.CenterX, theRect.HalfSizeX, startAngleDegrees),
				constructRadialY(theRect.CenterY, theRect.HalfSizeY, startAngleDegrees),
				constructRadialX(theRect.CenterX, theRect.HalfSizeX, endAngleDegrees),
				constructRadialY(theRect.CenterY, theRect.HalfSizeY, endAngleDegrees));

			ResetPenBrush();
		}

		#endregion

		private int constructRadialX(int center, int xRadius, double angleDegrees)
		{
			return (int)(center + (double)xRadius * Math.Cos(ToRadians(angleDegrees)));
		}

		private int constructRadialY(int center, int yRadius, double angleDegrees)
		{
			return (int)(center - (double)yRadius * Math.Sin(ToRadians(angleDegrees)));
		}

		public const double PI = 3.141592653589793238462643383;
		public double ToRadians(double degrees)
		{
			return degrees * PI / 180;
		}

		#region Gradients

		public void GradientFillRectangle(Rect theRect, System.Drawing.Color Color1, System.Drawing.Color Color2, LinearGradientMode GradientType)
		{
			int ColorR1 = Color1.R;
			int ColorG1 = Color1.G;
			int ColorB1 = Color1.B;

			int ColorR2 = Color2.R - ColorR1;
			int ColorG2 = Color2.G - ColorG1;
			int ColorB2 = Color2.B - ColorB1;

			int Width = theRect.Size.X;
			int Height = theRect.Size.Y;
			int x0 = theRect.Location.X;
			int y0 = theRect.Location.Y;
			int x1 = theRect.Right;
			int y1 = theRect.Bottom;

			if (Width <= 0 || Height <= 0)
				return;

			double Coeff;
			int StepSize;

			API.RECT R = new API.RECT();

			int NumberOfSteps = 128; // number of steps

			if (GradientType == System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
			{
				double InvWidth = 1.0 / Width;

				StepSize = Width / NumberOfSteps;
				if (StepSize < 1)
					StepSize = 1;

				R.Top = y0;
				R.Bottom = y1;

				for (int i = x0; i <= x1; i += StepSize)
				{
					R.Left = i;
					R.Right = i + StepSize;
					if (R.Right > x1)
					{
						R.Right = x1;
					}

					Coeff = (i - x0) * InvWidth;

					IntPtr hBrush = API.CreateSolidBrush((int)
						(int)(ColorR1 + (double)ColorR2 * Coeff) |
						(int)(ColorG1 + (double)ColorG2 * Coeff) << 8 |
						(int)(ColorB1 + (double)ColorB2 * Coeff) << 16
						);

					API.FillRect(hDC, ref R, hBrush);
					API.DeleteObject(hBrush);
				}
			}
			else
			{
				double InvHeight = 1.0 / Height;

				StepSize = Height / NumberOfSteps;

				if (StepSize < 1)
					StepSize = 1;

				R.Left = x0;
				R.Right = x1;

				for (int i = y0; i <= y1; i += StepSize)
				{
					R.Top = i;
					R.Bottom = i + StepSize;
					if (R.Bottom > y1)
					{

						R.Bottom = y1;

					}

					Coeff = (i - y0) * InvHeight;
					IntPtr hBrush = API.CreateSolidBrush(
						(int)(ColorR1 + (double)ColorR2 * Coeff) |
						(int)(ColorG1 + (double)ColorG2 * Coeff) << 8 |
						(int)(ColorB1 + (double)ColorB2 * Coeff) << 16
					);

					API.FillRect(hDC, ref R, hBrush);
					API.DeleteObject(hBrush);
				}
			}
		}

		public void GradientFill4(
			Rect rect,
			Color leftTop,
			Color rightTop,
			Color leftBottom,
			Color rightBottom)
		{
			GradientFill4(rect, leftTop, rightTop, leftBottom, rightBottom, 128);
		}

		public void GradientFill4(
			Rect rect,
			Color leftTop,
			Color rightTop,
			Color leftBottom,
			Color rightBottom,
			int steps)
		{
			//long startTime = Stopwatch.GetTimestamp();
			GuiLabs.Utils.API.RECT r = new API.RECT();

			Color upper, lower, final;
			final = Color.WhiteSmoke;

			for (int i = 0; i < steps; i++)
			{
				float xratio = (float)i / steps;
				r.Left = (int)(rect.Left + rect.Width * xratio);
				r.Right = (int)(rect.Left + rect.Width * (float)(i + 1) / steps);
				for (int j = 0; j < steps; j++)
				{
					float yratio = (float)j / steps;
					r.Top = (int)(rect.Top + rect.Height * yratio);
					r.Bottom = (int)(rect.Top + rect.Height * (float)(j + 1) / steps);

					upper = Colors.Interpolate(leftTop, rightTop, xratio);
					lower = Colors.Interpolate(leftBottom, rightBottom, xratio);
					final = Colors.Interpolate(upper, lower, yratio);

					API.FillRectangle(hDC, final, r);
				}
			}

			//long stopTime = Stopwatch.GetTimestamp();
			//DrawString(
			//    ((stopTime - startTime) / (double)Stopwatch.Frequency).ToString(),
			//    rect);
		}

		#endregion

		public override void DrawLine(int x1, int y1, int x2, int y2, System.Drawing.Color lineColor, int lineWidth)
		{
			if (lineColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			IntPtr hPen = API.CreatePen(API.PenStyle.PS_SOLID, lineWidth, System.Drawing.ColorTranslator.ToWin32(lineColor));
			IntPtr hOldPen = API.SelectObject(hDC, hPen);

			API.MoveToEx(hDC, x1, y1, ref NULLPOINT);
			API.LineTo(hDC, x2, y2);

			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		public void FillPolygon(IList<Point> Points, ILineStyleInfo LineStyle, IFillStyleInfo FillStyle)
		{
			GDILineStyle Line = (GDILineStyle)LineStyle;
			GDIFillStyle Fill = (GDIFillStyle)FillStyle;

			API.POINT[] P = new API.POINT[Points.Count];

			for (int i = 0; i < Points.Count; i++)
			{
				P[i].x = Points[i].X;
				P[i].y = Points[i].Y;
			}

			SetPenBrush(Line.ForeColor, Line.Width, Fill.FillColor);

			//IntPtr hPen = API.CreatePen(0, Line.Width, Line.Win32ForeColor);
			//IntPtr hOldPen = API.SelectObject(hDC, hPen);
			//IntPtr hBrush = API.CreateSolidBrush(Fill.Win32FillColor);
			//IntPtr hOldBrush = API.SelectObject(hDC, hBrush);

            API.Polygon(hDC, P, Points.Count);

			ResetPenBrush();

			//API.SelectObject(hDC, hOldBrush);
			//API.DeleteObject(hBrush);
			//API.SelectObject(hDC, hOldPen);
			//API.DeleteObject(hPen);
		}

		private API.POINT[] trianglePoints = new API.POINT[3];
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
			trianglePoints[0].x = x1;
			trianglePoints[0].y = y1;
			trianglePoints[1].x = x2;
			trianglePoints[1].y = y2;
			trianglePoints[2].x = x3;
			trianglePoints[2].y = y3;

			SetPenBrush(borderColor, 1, fillColor);

            API.Polygon(hDC, trianglePoints, 3);

			ResetPenBrush();
		}

		#region Pen Brush

		private void SetPenBrush(
			System.Drawing.Color penColor,
			int lineWidth,
			System.Drawing.Color brushColor)
		{
			#region Pen

			if (penColor != System.Drawing.Color.Transparent)
			{
				hPen = API.CreatePen(API.PenStyle.PS_SOLID, lineWidth, System.Drawing.ColorTranslator.ToWin32(penColor));
			}
			else
			{
				hPen = API.CreatePen(API.PenStyle.PS_NULL, 1, 0);
			}
			hOldPen = API.SelectObject(hDC, hPen);

			#endregion

			#region Brush

			if (brushColor != System.Drawing.Color.Transparent)
			{
				hBrush = API.CreateSolidBrush(System.Drawing.ColorTranslator.ToWin32(brushColor));
			}
			else
			{
				hBrush = API.GetStockObject(API.NULL_BRUSH);
			}
			hOldBrush = API.SelectObject(hDC, hBrush);

			#endregion
		}

		private IntPtr hPen;
		private IntPtr hOldPen;
		private IntPtr hBrush;
		private IntPtr hOldBrush;

		private void ResetPenBrush()
		{
			API.SelectObject(hDC, hOldBrush);
			API.DeleteObject(hBrush);
			API.SelectObject(hDC, hOldPen);
			API.DeleteObject(hPen);
		}

		#endregion

		#region Factory

		private IDrawInfoFactory mFactory = new GDIDrawInfoFactory();
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

		#endregion

		#region DrawString

		private int CurrentTextColor = 0;

		public void DrawString(string Text, Rect theRect, IFontStyleInfo theFont)
		{
			GDIFontStyle FontStyle = (GDIFontStyle)theFont;

			if (CurrentTextColor != FontStyle.Win32ForeColor)
			{
				CurrentTextColor = FontStyle.Win32ForeColor;
				API.SetTextColor(hDC, FontStyle.Win32ForeColor);
			}

			IntPtr hOldFont = API.SelectObject(hDC, ((GDIFont)FontStyle.Font).hFont);

			API.RECT r = new API.RECT();

			r.Left = theRect.Location.X;
			r.Top = theRect.Location.Y;
			r.Right = theRect.Right;
			r.Bottom = theRect.Bottom;

			// API.DrawText(hDC, Text, Text.Length, ref r, 2368);

			API.ExtTextOut(hDC, r.Left, r.Top, 4, ref r, Text, (uint)Text.Length, null);

			API.SelectObject(hDC, hOldFont);

			// No need to Delete hFont because we're going to reuse it
			// it is being saved in GDIFontStyle FontStyle
			// API.DeleteObject(hFont);

			// No need to restore old text color 
			// because we're setting it new each time anyway
			// API.SetTextColor(hDC, hOldColor);
		}

		public void DrawString(string text, Rect rect)
		{
			GuiLabs.Utils.API.RECT R = rect.ToRECT();
			API.ExtTextOut(
				hDC,
				rect.Left,
				rect.Top,
				4,
				ref R,
				text,
				(uint)text.Length,
				null);
		}

		private Point stringPos = new Point();
		private Point stringSize = new Point();
		private Rect stringRect = new Rect();

		public void DrawStringWithSelection
		(
			Rect Block,
			int StartSelPos,
			int CaretPosition,
			string Text,
			IFontStyleInfo FontStyle
		)
		{
			// API.SetROP2(hDC, 14);
			// API.Rectangle(hDC, theRect.Location.X, theRect.Location.Y, theRect.Right, theRect.Bottom);
			// FillRectangle(theRect, selectionFillStyle);

			DrawString(Text, Block, FontStyle);

			if (StartSelPos == CaretPosition) return;

			int SelStart, SelEnd;
			if (CaretPosition > StartSelPos)
			{
				SelStart = StartSelPos;
				SelEnd = CaretPosition;
			}
			else
			{
				SelStart = CaretPosition;
				SelEnd = StartSelPos;
			}

			if (SelStart < 0)
			{
				SelStart = 0;
			}

			if (SelEnd > Text.Length)
			{
				SelEnd = Text.Length;
			}

			// Added the if-statement to check if the selection borders 
			// are within the textlength
			string select = "";
			if ((SelStart < Text.Length) && (SelEnd <= Text.Length))
				select = Text.Substring(SelStart, SelEnd - SelStart);

			stringSize.Set(StringSize(Text.Substring(0, SelStart), FontStyle.Font));
			stringPos.Set(Block.Location.X + stringSize.X, Block.Location.Y);
			stringRect.Set(stringPos, StringSize(select, FontStyle.Font));

			FillRectangle(stringRect, System.Drawing.SystemColors.Highlight);

			System.Drawing.Color OldColor = FontStyle.ForeColor;
			FontStyle.ForeColor = System.Drawing.SystemColors.HighlightText;
			DrawString(select, stringRect, FontStyle);
			FontStyle.ForeColor = OldColor;
		}

		public Point StringSize(string Text, IFontInfo theFont)
		{
			IntPtr hOldFont = API.SelectObject(hDC, ((GDIFont)theFont).hFont);

			API.SIZE theSize = new API.SIZE();
			API.GetTextExtentPoint32(hDC, Text, Text.Length, out theSize);
			Point result = new Point(theSize.x, theSize.y);

			API.SelectObject(hDC, hOldFont);

			return result;
		}

		#endregion

		#region DrawImage

		public void DrawImage(IPicture picture, Point p)
		{
			Picture pict = picture as Picture;
			if (pict == null)
			{
				return;
			}

			if (pict.Transparent)
			{
				DrawImageTransparent(pict, p);
				return;
			}

			IntPtr hPictureDC = API.CreateCompatibleDC(hDC);
			IntPtr hOldBitmap = API.SelectObject(hPictureDC, pict.hBitmap);

			API.BitBlt(
				hDC,
				p.X,
				p.Y,
				picture.Size.X,
				picture.Size.Y,
				hPictureDC,
				0,
				0,
				API.SRCCOPY);

			API.SelectObject(hPictureDC, hOldBitmap);
			API.DeleteDC(hPictureDC);
		}

		private void DrawImageTransparent(Picture pict, Point p)
		{
			IntPtr hPictureDC = API.CreateCompatibleDC(hDC);
			IntPtr hOldBitmap = API.SelectObject(hPictureDC, pict.hBitmap);

			API.TransparentBlt(
				hDC,
				p.X,
				p.Y,
				pict.Size.X,
				pict.Size.Y,
				hPictureDC,
				0,
				0,
				pict.Size.X,
				pict.Size.Y,
				pict.Win32TransparentColor
			);

			API.SelectObject(hPictureDC, hOldBitmap);
			API.DeleteDC(hPictureDC);
		}

		#endregion
	}
}
