using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public interface ISupportRemove<T>
	{
		bool Remove(T item);
	}

	public interface ISupportAddRemove<T> : IFillable<T>, ISupportRemove<T>
	{

	}
}
