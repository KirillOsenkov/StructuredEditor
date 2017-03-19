using System;
using System.Windows.Forms;
using GuiLabs.Canvas.DrawOperations;

namespace GuiLabs.Canvas.Renderer
{
	public interface IRenderer : IDisposable
	{
		IDrawOperations DrawOperations
		{
			get;
			set;
		}

		//Point ClientSize
		//{
		//    get;
		//    //set;
		//}
		System.Drawing.Color BackColor
		{
			get;
			set;
		}

		void Clear();
		//void Clear(Rect Area);

		void RenderBuffer(Control DestinationControl, Rect ToRedraw);
		//void RenderBuffer(Control DestinationControl, System.Drawing.Rectangle r);
	}
}