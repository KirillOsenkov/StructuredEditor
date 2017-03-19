using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public interface IArray<T> : ICountable, IEnumerable<T>, Indexable<T>, ISet<T>
	{
	}
}
