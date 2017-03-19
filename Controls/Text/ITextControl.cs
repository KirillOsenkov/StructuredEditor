namespace GuiLabs.Canvas.Controls
{
	public interface ITextControl : IHasText, ITextProvider
	{
		void SetCaretPosition(int newPosition);
	}
}
