using System;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface ILineStyleInfo : IDisposable, ISupportMemento
	{
		System.Drawing.Color ForeColor
		{
			get;
			set;
		}
		int Width
		{
			get;
			set;
		}
	}
}
