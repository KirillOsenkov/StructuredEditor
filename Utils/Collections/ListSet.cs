using System.Collections.Generic;

namespace GuiLabs.Utils.Collections
{
	public class ListSet<T> : List<T>,
		ISet<T>,
		IFillable<T>,
		IClearable,
		ICountable,
		ISupportRemove<T>,
		Indexable<T>
	{
	}
}
