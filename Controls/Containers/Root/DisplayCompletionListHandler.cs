using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Canvas.Controls
{
	public delegate void DisplayCompletionListHandler(IHasBounds nearToShape);
	public delegate void ShowPopupMenuHandler(ICommandList menu, System.Drawing.Point location);
}