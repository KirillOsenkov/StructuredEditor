using System;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface IPicture : IDisposable
	{
		Point Size { get; }
	}
}
