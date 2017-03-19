namespace GuiLabs.Canvas.Events
{
	public interface IKeyHandler
	{
		IKeyHandler DefaultKeyHandler { get; set; }
		//bool NextHandlerValid(IKeyHandler nextHandler);
		void OnKeyDown(System.Windows.Forms.KeyEventArgs e);
		void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e);
		void OnKeyUp(System.Windows.Forms.KeyEventArgs e);
	}
}