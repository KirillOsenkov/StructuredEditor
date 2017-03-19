using System.Collections.Generic;
using System.Collections.ObjectModel;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Utils.Collections
{
	public class CollectionWithEvents<T> : Collection<T>, ICollectionWithEvents<T>
	{
		#region Events

		public event ElementAddedHandler<T> ElementAdded;
		public event ElementRemovedHandler<T> ElementRemoved;
		public event ElementReplacedHandler<T> ElementReplaced;
		public event EmptyHandler CollectionChanged;

		protected void RaiseElementAdded(T newItem)
		{
			if (ElementAdded != null && !SuspendEvents)
			{
				ElementAdded(newItem);
			}
		}

		protected void RaiseElementRemoved(T oldItem)
		{
			if (ElementRemoved != null && !SuspendEvents)
			{
				ElementRemoved(oldItem);
			}
		}

		protected void RaiseElementReplaced(T oldElement, T newElement)
		{
			if (ElementReplaced != null && !SuspendEvents)
			{
				ElementReplaced(oldElement, newElement);
			}
		}
		
		protected void RaiseCollectionChanged()
		{
			if (CollectionChanged != null && !SuspendEvents)
			{
				CollectionChanged();
			}
		}

		#endregion

		public IEnumerable<T> Reversed
		{
			get 
			{
				for (int i = this.Count - 1; i >= 0; i--)
				{
					yield return this[i];
				}
			}
		}

		public T Head
		{
			get 
			{
				if (this.Count > 0)
				{
					return this[0];
				}
				else
				{
					return default(T);
				}
			}
		}

		public T Tail
		{
			get 
			{
				if (this.Count > 0)
				{
					return this[this.Count - 1];
				}
				else
				{
					return default(T);
				}
			}
		}

		public void Prepend(T item)
		{
			this.Insert(0, item);
		}

		protected override void InsertItem(int index, T item)
		{
			base.InsertItem(index, item);
			RaiseElementAdded(item);
			RaiseCollectionChanged();
		}

		protected override void SetItem(int index, T item)
		{
			T oldItem = this[index];
			base.SetItem(index, item);
			RaiseElementReplaced(oldItem, item);
			RaiseCollectionChanged();
		}

		protected override void RemoveItem(int index)
		{
			T item = this[index];
			base.RemoveItem(index);
			RaiseElementRemoved(item);
			RaiseCollectionChanged();
		}

		// TODO: WARNING: Linear search complexity O(n)
		// Should be constant.
		public T GetPrevElement(T element)
		{
			int position = this.IndexOf(element);
			if (position > 0)
			{
				position--;
				return this[position];
			}
			return default(T);
		}

		// TODO: WARNING: Linear search complexity O(n)
		// Should be constant.
		public T GetNextElement(T element)
		{
			int position = this.IndexOf(element);
			if (position >= 0 && position < this.Count - 1)
			{
				position++;
				return this[position];
			}
			return default(T);
		}

		private bool mSuspendEvents = false;
		public bool SuspendEvents
		{
			get
			{
				return mSuspendEvents;
			}
			set
			{
				mSuspendEvents = value;
			}
		}
	}
}