namespace GuiLabs.Canvas.DrawStyle
{
	public interface IFontInfo
	{
		string Name
		{
			get;
		}
		int Size
		{
			get;
			set;
		}
		bool Bold
		{
			get;
		}
		bool Italic
		{
			get;
		}
		bool Underline
		{
			get;
		}
		Point SpaceCharSize
		{
			get;
		}
	}
}