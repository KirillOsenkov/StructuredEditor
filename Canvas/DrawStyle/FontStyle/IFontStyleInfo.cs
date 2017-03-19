using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface IFontStyleInfo : ISupportMemento
	{
		IFontInfo Font
		{
			get;
			set;
		}
		System.Drawing.Color ForeColor
		{
			get;
			set;
		}
	}
}
