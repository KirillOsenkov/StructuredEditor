using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public interface Indexable<T>
	{
		T this[int index] { get; set; }
	}
}
