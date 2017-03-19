namespace GuiLabs.Canvas.Events
{
	/// <summary>
	/// Everything that can SEND mouse events
	/// </summary>
	public interface IMouseEvents
	{
		event MouseWithKeysEventHandler Click;
		event MouseWithKeysEventHandler DoubleClick;
		event MouseWithKeysEventHandler MouseDown;
		event MouseWithKeysEventHandler MouseHover;
		event MouseWithKeysEventHandler MouseMove;
		event MouseWithKeysEventHandler MouseUp;
		event MouseWithKeysEventHandler MouseWheel;
	}
}