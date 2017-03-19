using GuiLabs.Canvas.Events;

namespace GuiLabs.Canvas.Shapes
{
	/// <summary>
	/// Ein visuelles Objekt, das gezeichnet werden kann
	/// und das auf Maus und Tastatur reagieren kann
	/// </summary>
	public interface IShape : IDrawableRect, IMouseHandler, IKeyHandler
	{
		Point MinimumRequiredSize { get; }
		void SetBoundsToMinimumSize();
		event SizeChangedHandler SizeChanged;
		bool HitTest(int x, int y);
		bool Visible { get; set; }
		bool Enabled { get; set; }
		void Layout();
		void LayoutDock();
	}
}
