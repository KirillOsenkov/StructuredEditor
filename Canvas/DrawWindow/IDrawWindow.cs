using System;
using GuiLabs.Canvas.Events;

namespace GuiLabs.Canvas
{
	public interface IDrawWindow // : IMouseEvents
	{
		event RepaintHandler Repaint;
		event EventHandler Resize;
		event EventHandler GotFocus;

		//event System.Windows.Forms.KeyEventHandler KeyDown;
		//event System.Windows.Forms.KeyPressEventHandler KeyPress;
		//event System.Windows.Forms.KeyEventHandler KeyUp;

		void Redraw();
		void Redraw(Rect ToRedraw);
		//void Redraw(IDrawableRect ShapeToRedraw);

		//Rect Bounds
		//{
		//    get;
		//}
		Point GetClientSize();
	}
}
