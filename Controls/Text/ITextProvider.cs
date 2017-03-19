using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public interface ITextProvider
	{
		event TextChangedEventHandler TextChanged;
		string Text { get; }
	}
}
