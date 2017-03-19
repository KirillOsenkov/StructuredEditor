using GuiLabs.Canvas;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Shapes
{
	public delegate void NeedRedrawHandler(IDrawableRect ShapeToRedraw);

	/// <summary>
	/// Alles was auf Canvas gezeichnet werden kann,
	/// muss diese Schnittstelle implementieren.
	/// </summary>
	public interface IDrawableRect : IDrawable, IHasBounds, IMovable
	{
		/// <summary>
		/// Benachrichtigung: "man muss mich neu zeichnen"
		/// </summary>
		event NeedRedrawHandler NeedRedraw;
		void RaiseNeedRedraw();
		void RaiseNeedRedraw(IDrawableRect ShapeToRedraw);
	}
}
