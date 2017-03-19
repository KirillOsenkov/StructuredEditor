using System.Windows.Forms;

namespace GuiLabs.Canvas.Events
{
	public interface IKeyEvents
	{
		event KeyEventHandler KeyDown;
		event KeyPressEventHandler KeyPress;
		event KeyEventHandler KeyUp;
		void RaiseKeyDown(KeyEventArgs e);
	}
}
