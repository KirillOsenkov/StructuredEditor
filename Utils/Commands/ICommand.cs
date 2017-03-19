namespace GuiLabs.Utils.Commands
{
	public interface ICommand
	{
		string Text { get; }
		void Click();
		System.Drawing.Image Picture { get; }
	}
}
