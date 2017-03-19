using System;

namespace GuiLabs.Canvas.Controls
{
	internal interface INumberRange<T> where T : IComparable<T>
	{
		T Pos
		{
			get;
			set;
		}

		T Size
		{
			get;
			set;
		}
	}
}