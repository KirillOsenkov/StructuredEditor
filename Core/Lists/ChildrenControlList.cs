using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Utils.Collections;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Editor.Lists
{
	public class ChildrenControlList : ICollectionWithEvents<Control>
	{
		public ChildrenControlList(IBlockList blocks)
		{
			Param.CheckNotNull(blocks, "blocks");
			Blocks = blocks;
			Blocks.ElementAdded += Blocks_ElementAdded;
			Blocks.ElementReplaced += Blocks_ElementReplaced;
			Blocks.ElementRemoved += Blocks_ElementRemoved;
			Blocks.CollectionChanged += RaiseCollectionChanged;
		}

		#region Events

		public event ElementAddedHandler<Control> ElementAdded;
		public event ElementRemovedHandler<Control> ElementRemoved;
		public event ElementReplacedHandler<Control> ElementReplaced;
		public event EmptyHandler CollectionChanged;

		protected void RaiseElementAdded(Control element)
		{
			if (ElementAdded != null && !SuspendEvents)
			{
				ElementAdded(element);
			}
		}

		protected void RaiseElementReplaced(Control oldElement, Control newElement)
		{
			if (ElementReplaced != null && !SuspendEvents)
			{
				ElementReplaced(oldElement, newElement);
			}
		}

		protected void RaiseElementRemoved(Control element)
		{
			if (ElementRemoved != null && !SuspendEvents)
			{
				ElementRemoved(element);
			}
		}

		protected void RaiseCollectionChanged()
		{
			if (CollectionChanged != null && !SuspendEvents)
			{
				CollectionChanged();
			}
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

		#endregion

		#region Forwarding events

		void Blocks_ElementAdded(Block element)
		{
			RaiseElementAdded(element.MyControl);
		}

		void Blocks_ElementReplaced(Block oldElement, Block newElement)
		{
			RaiseElementReplaced(oldElement.MyControl, newElement.MyControl);
		}

		void Blocks_ElementRemoved(Block element)
		{
			RaiseElementRemoved(element.MyControl);
		}

		#endregion

		private IBlockList mBlocks;
		public IBlockList Blocks
		{
			[DebuggerStepThrough]
			get
			{
				return mBlocks;
			}
			[DebuggerStepThrough]
			set
			{
				mBlocks = value;
			}
		}

		#region Enumerators

		public IEnumerable<Control> Reversed
		{
			get
			{
				Block Current = Blocks.Tail;
				while (Current != null)
				{
					yield return Current.MyControl;
					Current = Current.Prev;
				}
			}
		}

		public IEnumerator<Control> GetEnumerator()
		{
			Block Current = Blocks.Head;
			while (Current != null)
			{
				yield return Current.MyControl;
				Current = Current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			Block Current = Blocks.Head;
			while (Current != null)
			{
				yield return Current.MyControl;
				Current = Current.Next;
			}
		}

		#endregion

		public int Count
		{
			get { return Blocks.Count; }
		}

		public Control Head
		{
			get
			{
				if (Blocks != null && Blocks.Head != null)
				{
					return Blocks.Head.MyControl;
				}
				else
				{
					return null;
				}
			}
		}

		public Control Tail
		{
			get
			{
				if (Blocks != null && Blocks.Tail != null)
				{
					return Blocks.Tail.MyControl;
				}
				else
				{
					return null;
				}
			}
		}
	}
}
