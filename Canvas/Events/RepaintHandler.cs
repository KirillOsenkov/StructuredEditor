using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Events
{
	public delegate void RepaintHandler(IRenderer Renderer);
	public delegate void RegionRepaintHandler(IRenderer Renderer, IDrawableRect RegionToRedraw);
}
