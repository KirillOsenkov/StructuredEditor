using System;
using System.Drawing;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface IDrawInfoFactory
	{
		ILineStyleInfo ProduceNewLineStyleInfo(
			Color theColor, int theWidth);
		IFillStyleInfo ProduceNewFillStyleInfo(
			Color theColor);
		IFillStyleInfo ProduceNewFillStyleInfo(
			Color theColor,
			Color gradientColor,
			FillMode mode);
		IFontStyleInfo ProduceNewFontStyleInfo(
			string FamilyName, 
			float size, 
			System.Drawing.FontStyle theStyle);
		IPicture ProduceNewPicture(
			System.Drawing.Image image);
		IPicture ProduceNewTransparentPicture(
			System.Drawing.Image image,
			System.Drawing.Color transparentColor);
	}
}
