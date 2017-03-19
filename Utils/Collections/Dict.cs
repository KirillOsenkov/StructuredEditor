using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public class Dict<T> : Dictionary<string, T>
	{
		public new T this[string index]
		{
			get 
			{
				T result = default(T);
				this.TryGetValue(index, out result);
				return result;
			}
			set
			{
				if (this.ContainsKey(index))
				{
					this.Remove(index);
				}
				this.Add(index, value);
			}
		}
	}
}
