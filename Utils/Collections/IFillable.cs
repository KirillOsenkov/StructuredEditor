using System.Collections.Generic;

namespace GuiLabs.Utils.Collections
{
	public interface IFillable<T>
	{
		void Add(T item);
		void AddRange(IEnumerable<T> items);
	}
}
