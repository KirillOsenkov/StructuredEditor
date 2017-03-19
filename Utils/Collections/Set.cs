using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public class Set<T> : 
		Dictionary<T, Object>, 
		IEnumerable<T>,
		ISet<T>,
		IFillable<T>,
		IClearable,
		ICountable
	{
		#region ctors

		public Set()
			: base()
		{

		}

		public Set(IEnumerable<T> initializeWithList)
			: base()
		{
			foreach (T item in initializeWithList)
			{
				this.Add(item, null);
			}
		}

		#endregion

		private bool mAllowDuplicates = true;
		public bool AllowDuplicates
		{
			get
			{
				return mAllowDuplicates;
			}
			set
			{
				mAllowDuplicates = value;
			}
		}

		public void Add(T newItem)
		{
			if (!AllowDuplicates 
				&& this.Contains(newItem))
			{
				return;
			}
			this.Add(newItem, null);
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				Add(item);
			}
		}

		public bool Contains(T item)
		{
			return this.ContainsKey(item);
		}

		public new IEnumerator<T> GetEnumerator()
		{
			return this.Keys.GetEnumerator();
		}
	}
}
