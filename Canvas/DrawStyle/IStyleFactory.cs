using System.Collections.Generic;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public interface IStyleFactory : IEnumerable<IShapeStyle>, ISupportMemento
	{
		void Add(string styleName, IShapeStyle newStyle);
		IShapeStyle GetStyle(string styleName);
	}
}
