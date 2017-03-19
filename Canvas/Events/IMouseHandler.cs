namespace GuiLabs.Canvas.Events
{
	/// <summary>
	/// Everything that can RECEIVE mouse events
	/// and react to them
	/// </summary>
	public interface IMouseHandler
	{
		IMouseHandler DefaultMouseHandler { get; set; }
		//bool NextHandlerValid(IMouseHandler nextHandler);
		void OnClick(MouseWithKeysEventArgs e);
		void OnDoubleClick(MouseWithKeysEventArgs e);
		void OnMouseDown(MouseWithKeysEventArgs e);
		void OnMouseHover(MouseWithKeysEventArgs e);
		void OnMouseMove(MouseWithKeysEventArgs e);
		void OnMouseUp(MouseWithKeysEventArgs e);
		void OnMouseWheel(MouseWithKeysEventArgs e);
	}
}