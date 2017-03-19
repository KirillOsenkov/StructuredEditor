using System;
using GuiLabs.Canvas;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Shapes
{
	/// <summary>
	/// Alles was auf Canvas gezeichnet werden kann,
	/// muss diese Schnittstelle implementieren.
	/// </summary>
	/// 
	public interface IDrawable
	{
		/// <summary>
		/// zeichne mich auf diesem Renderer
		/// </summary>
		void Draw(IRenderer Renderer);

		/// <summary>
		/// Normalerweise null (wird dann ignoriert).
		/// Falls nicht null, jeder Aufruf von Draw()
		/// wird an die Methode Draw() von diesem
		/// DefaultDrawHandler weitergeleitet
		/// </summary>
		//IDrawable DefaultDrawHandler
		//{
		//    get;
		//    set;
		//}
	}
}
