using System;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIDrawInfoFactory : IDrawInfoFactory
	{
		#region Line

		public ILineStyleInfo ProduceNewLineStyleInfo(Color theColor, int theWidth)
		{
			ILineStyleInfo NewLineStyle = new GDILineStyle(theColor, theWidth);
			return NewLineStyle;
		}

		#endregion

		#region Fill

		public IFillStyleInfo ProduceNewFillStyleInfo(Color FillColor)
		{
			IFillStyleInfo NewFillStyle = new GDIFillStyle(FillColor);
			return NewFillStyle;
		}

		public IFillStyleInfo ProduceNewFillStyleInfo(
			Color FillColor,
			Color GradientColor,
			FillMode mode)
		{
			IFillStyleInfo NewFillStyle = new GDIFillStyle(
				FillColor, GradientColor, mode);
			return NewFillStyle;
		}

		#endregion

		#region Font

		public IFontStyleInfo ProduceNewFontStyleInfo(
			string FamilyName,
			float size,
			System.Drawing.FontStyle theStyle)
		{
			GDIFontStyle result = new GDIFontStyle();

			string sig = GetSignature(FamilyName, size, theStyle);
			GDIFont found = FindFont(sig);
			if (found == null)
			{
				found = new GDIFont(FamilyName, size, theStyle);
				AddFont(found);
			}
			result.Font = found;
			
			return result;
		}

		public void AddFont(GDIFont font)
		{
			string sig = GetSignature(font.Name, font.Size, font.FontStyle);
			if (!fontCache.ContainsKey(sig))
			{
				fontCache.Add(sig, font);
			}
		}

		public GDIFont FindFont(string signature)
		{
			GDIFont found = null;
			fontCache.TryGetValue(signature, out found);
			return found;
		}

		private Dictionary<string, GDIFont> fontCache
			= new Dictionary<string, GDIFont>();

		public string GetSignature(
			string FamilyName,
			float size,
			System.Drawing.FontStyle theStyle)
		{
			return FamilyName + size.ToString() + theStyle.ToString();
		}

		#endregion

		#region Picture

		public IPicture ProduceNewPicture(System.Drawing.Image image)
		{
			IPicture picture = new Picture(image);
			return picture;
		}

		public IPicture ProduceNewTransparentPicture(
			System.Drawing.Image image,
			System.Drawing.Color transparentColor
		)
		{
			Picture picture = new Picture(image);
			picture.Transparent = true;
			picture.TransparentColor = transparentColor;
			return picture;
		}

		#endregion
	}
}
