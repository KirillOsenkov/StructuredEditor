using System;
using System.Drawing;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface IFillStyleInfo : IDisposable, ISupportMemento
	{
		Color FillColor
		{
			get;
			set;
		}

		Color GradientColor
		{
			get;
			set;
		}

		FillMode Mode { get; set; }
	}
}
