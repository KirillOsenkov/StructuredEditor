using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public class ArrayRange<T> : IArray<T>
		where T: IEquatable<T>
	{
		public ArrayRange(T[] elements, int fromElement, int toElement)
		{
			els = elements;
			from = fromElement;
			to = toElement;
		}

		private T[] els;
		private int from;
		private int to;

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = from; i < to; i++)
			{
				if (i <= els.Length - 1)
				{
					yield return els[i];
				}
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count
		{
			get 
			{
				return to - from + 1; 
			}
		}

		public T this[int index]
		{
			get
			{
				return els[index + from];
			}
			set
			{
				els[index + from] = value;
			}
		}

		public bool Contains(T item)
		{
			for (int i = from; i < to; i++)
			{
				if (i <= els.Length - 1)
				{
					if (els[i].Equals(item))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
