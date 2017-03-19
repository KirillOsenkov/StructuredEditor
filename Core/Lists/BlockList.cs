using System.Collections;
using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Utils.Collections;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Editor.Lists
{
	public class BlockList : IBlockList
	{
		#region ctor

		public BlockList()
		{
			mControls = new ChildrenControlList(this);
		}

		#endregion

		#region indexer

		public Block this[int index]
		{
			get
			{
				return GetBlockFromIndex(index);
			}
			set
			{
				Block existing = GetBlockFromIndex(index);
				if (existing != null && existing != value)
				{
					Replace(existing, value);
				}
			}
		}

		public int GetIndex(Block block)
		{
			Block current = Head;
			int index = 0;

			while (current != null)
			{
				if (current == block)
					return index;

				index++;
				current = current.Next;
			}

			return -1;
		}

		public Block GetBlockFromIndex(int index)
		{
			Block current = Head;
			int currentIndex = 0;

			while (current != null)
			{
				if (currentIndex == index)
					return current;

				currentIndex++;
				current = current.Next;
			}

			return null;
		}

		#endregion

		#region Events

		public event ElementAddedHandler<Block> ElementAdded;
		public event ElementRemovedHandler<Block> ElementRemoved;
		/// <summary>
		/// We need ElementReplaced because if we use Added + Removed instead,
		/// there might be double update somewhere on the client:
		/// first an update during Remove and then another update during Add.
		/// 
		/// And we want our client to be updated only once during the replace operation
		/// (we want replace to be atomic and not to consist of two separate actions)
		/// </summary>
		public event ElementReplacedHandler<Block> ElementReplaced;
		public event EmptyHandler CollectionChanged;

		protected void RaiseElementAdded(Block element)
		{
			if (ElementAdded != null && !SuspendEvents)
			{
				ElementAdded(element);
			}
		}

		protected void RaiseElementReplaced(Block oldElement, Block newElement)
		{
			if (ElementReplaced != null && !SuspendEvents)
			{
				ElementReplaced(oldElement, newElement);
			}
		}

		protected void RaiseElementRemoved(Block element)
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

		#region Head, Tail

		private Block mTail;
		public Block Tail
		{
			get
			{
				return mTail;
			}
			private set
			{
				mTail = value;
			}
		}

		private Block mHead;
		public Block Head
		{
			get
			{
				return mHead;
			}
			private set
			{
				mHead = value;
			}
		}

		private void UpdateHeadTail(Block node)
		{
			if (node.Prev == null && this.Head != node)
			{
				this.Head = node;
			}
			if (node.Next == null && this.Tail != node)
			{
				this.Tail = node;
			}
		}

		#endregion

		#region Count, IsEmpty

		public bool IsEmpty()
		{
			return Head == null;
		}

		private int mCount = 0;
		public int Count
		{
			get
			{
				return mCount;
			}
			private set
			{
				mCount = value;
			}
		}

		#endregion

		#region On Added and Removed

		protected virtual void OnBlockAdded(Block newBlock)
		{
			Count++;
			RaiseElementAdded(newBlock);
			RaiseCollectionChanged();
		}

		protected virtual void OnBlockRemoved(Block removedBlock)
		{
			Count--;
			RaiseElementRemoved(removedBlock);
			RaiseCollectionChanged();
		}

		protected virtual void OnBlockReplaced(Block oldBlock, Block newBlock)
		{
			RaiseElementReplaced(oldBlock, newBlock);
			RaiseCollectionChanged();
		}

		#endregion

		#region Operations

		#region Add

		public void Add(string labelText)
		{
			Add(new LabelBlock(labelText));
		}
		
		public void Add(Block newBlock)
		{
			Param.CheckNotNull(newBlock);

			if (this.Tail == null)
			{
				this.Head = newBlock;
				this.Tail = this.Head;

				OnBlockAdded(newBlock);
			}
			else
			{
				Append(this.Tail, newBlock);
			}
		}

		public void Add(params Block[] blocksToAdd)
		{
			Add((IEnumerable<Block>)blocksToAdd);
		}

		public void Add(IEnumerable<Block> blocksToAdd)
		{
			foreach (Block block in blocksToAdd)
			{
				Add(block);
			}
		}

		#endregion

		#region Prepend

		public void Prepend(Block newHead)
		{
			Param.CheckNotNull(newHead);

			if (this.Head != null)
			{
				Prepend(this.Head, newHead);
			}
			else
			{
				Add(newHead);
			}
		}

		public void Prepend(Block oldBlock, Block newBlock)
		{
			Param.CheckNotNull(oldBlock, "oldBlock");
			Param.CheckNotNull(newBlock, "newBlock");

			newBlock.LinkToPrev(oldBlock.Prev);
			newBlock.LinkToNext(oldBlock);
			UpdateHeadTail(newBlock);

			OnBlockAdded(newBlock);
		}

		public void Prepend(IEnumerable<Block> blocksAtTheBeginning)
		{
			Param.CheckNotNull(blocksAtTheBeginning, "blocksAtTheBeginning");

			if (this.Head == null)
			{
				this.Add(blocksAtTheBeginning);
			}
			else
			{
				Prepend(this.Head, blocksAtTheBeginning);
			}
		}

		public void Prepend(Block oldBlock, IEnumerable<Block> blocksToPrepend)
		{
			Param.CheckNotNull(blocksToPrepend, "blocksToAppend");

			if (oldBlock == null)
			{
				if (this.Head == null)
				{
					this.Add(blocksToPrepend);
					return;
				}
				else
				{
					oldBlock = this.Head;
				}
			}

			bool alreadyPrependedOne = false;

			Block current = oldBlock;
			foreach (Block block in blocksToPrepend)
			{
				if (block != null)
				{
					if (current != null)
					{
						// we do this to preserve the order
						// in which we receive blocksToAppend
						if (alreadyPrependedOne)
						{
							Append(current, block);
						}
						else
						{
							Prepend(current, block);
							alreadyPrependedOne = true;
						}
					}
					current = block;
				}
			}
		}

		public void Prepend(Block oldBlock, params Block[] blocksToPrepend)
		{
			this.Prepend(oldBlock, (IEnumerable<Block>)blocksToPrepend);
		}

		#endregion

		#region Append

		public void Append(Block oldBlock, Block newBlock)
		{
			Param.CheckNotNull(oldBlock, "oldBlock");
			Param.CheckNotNull(newBlock, "newBlock");

			newBlock.LinkToNext(oldBlock.Next);
			newBlock.LinkToPrev(oldBlock);
			UpdateHeadTail(newBlock);

			OnBlockAdded(newBlock);
		}

		public void Append(Block oldBlock, IEnumerable<Block> blocksToAppend)
		{
			Param.CheckNotNull(oldBlock, "oldBlock");
			Param.CheckNotNull(blocksToAppend, "blocksToAppend");

			Block current = oldBlock;
			foreach (Block block in blocksToAppend)
			{
				if (block != null)
				{
					Append(current, block);
					current = block;
				}
			}
		}

		public void Append(Block oldBlock, params Block[] blocksToAppend)
		{
			Append(oldBlock, (IEnumerable<Block>)blocksToAppend);
		}

		#endregion

		#region Replace

		public void Replace(Block oldBlock, Block newBlock)
		{
			Param.CheckNotNull(oldBlock, "oldBlock");
			Param.CheckNotNull(newBlock, "newBlock");

			newBlock.LinkToPrev(oldBlock.Prev);
			newBlock.LinkToNext(oldBlock.Next);
			UpdateHeadTail(newBlock);

			OnBlockReplaced(oldBlock, newBlock);
		}

		public void Replace(Block oldBlock, params Block[] blocksToReplaceWith)
		{
			this.Replace(oldBlock, (IEnumerable<Block>)blocksToReplaceWith);
		}

		public void Replace(Block oldBlock, IEnumerable<Block> blocksToReplaceWith)
		{
			Append(oldBlock, blocksToReplaceWith);
			Delete(oldBlock);
		}

		#endregion

		#region Delete

		public void Delete(Block oldBlock)
		{
			Param.CheckNotNull(oldBlock);

			if (oldBlock.Prev != null)
			{
				oldBlock.Prev.LinkToNext(oldBlock.Next);
				UpdateHeadTail(oldBlock.Prev);
			}
			else if (oldBlock.Next != null)
			{
				oldBlock.Next.Prev = null;
				Head = oldBlock.Next;
			}
			else
			{
				Head = null;
				Tail = null;
			}

			OnBlockRemoved(oldBlock);
		}

		public void Delete(IEnumerable<Block> blocksToDelete)
		{
			foreach (Block b in blocksToDelete)
			{
				this.Delete(b);
			}
		}

		public void Delete(params Block[] blocksToDelete)
		{
			this.Delete((IEnumerable<Block>)blocksToDelete);
		}

		#endregion

		#endregion

		#region Insert, Remove (per index)

		#region Insert

		/// <summary>
		/// Fuegt einen BlockNode in eine Bestehende Liste ein.		
		/// </summary>
		/// <param name="newBlock">Einzufuegender Knoten.</param>
		/// <param name="nIndex">index bei welchem diese Liste eingefuegt werden soll</param>
		/// <param name="shouldReplace">falls true, wird der Block an der Stelle nIndex mit der Blockliste ersetzt\nsnsonsten danach eingefuegt</param>
		/// <returns></returns>
		public void Insert(Block newBlock, int index, bool shouldReplace)
		{
			Block existing = GetBlockFromIndex(index);
			if (existing != null)
			{
				if (shouldReplace)
				{
					Replace(existing, newBlock);
				}
				else
				{
					Append(existing, newBlock);
				}
			}
			else
			{
				Add(newBlock);
			}

			////RaiseElementAdded(newBlock);
			////Block current = Head;
			//Block ActiveNode = Head;
			//int ActiveIndex = 0;
			////Count++;

			////noch kein Element in der Liste vorhanden!
			//if (ActiveNode == null)
			//{
			//    Add(newBlock);
			//}

			////Position zum einfuegen suchen
			//while (ActiveNode != null)
			//{
			//    if (ActiveIndex == index)
			//    {
			//        if (shouldReplace)
			//        {
			//            Replace(ActiveNode, newBlock);

			//            #region old Code

			//            /*
			//            if (current == mHead)
			//            {
			//                mHead = newBlock;
			//                newBlock.Prev = null;
			//            }
			//            else
			//            {
			//                current.Prev.Next = newBlock;
			//                newBlock.Prev = current.Prev;
			//            }

			//            if (current == mTail)
			//            {
			//                mTail = newBlock;
			//                newBlock.Next = null;
			//            }
			//            else
			//            {
			//                current.Next.Prev = newBlock;
			//                newBlock = current.Next;
			//            }

			//            current.Next = null;
			//            current.Prev = null;
			//            current = null;

			//            RaiseElementAdded(newBlock);
			//            RaiseCollectionChanged();
			//            */

			//            #endregion

			//            return 0;
			//        }
			//        else
			//        {
			//            Append(ActiveNode, newBlock);

			//            #region old Code

			//            /*
			//            if (current == mTail)
			//            {
			//                mTail = newBlock;
			//                newBlock.Next = null;
			//            }
			//            else
			//            {
			//                current.Next.Prev = newBlock;
			//                newBlock.Next = current.Next;
			//            }

			//            current.Next = newBlock;
			//            newBlock.Prev = current;

			//            RaiseElementAdded(newBlock);
			//            RaiseCollectionChanged();
			//            */

			//            #endregion

			//            return 0;
			//        }
			//    }

			//    ActiveIndex++;
			//    ActiveNode = ActiveNode.Next;
			//}

			//return -1; //Fehler, waere etwas ersetzt worden, waere er nicht bis hier gekommen
		}

		/// <summary>
		///   Fuegt eine Liste von Bloecken in eine Bestehende Liste ein.		///
		/// </summary>
		/// <param name="blLst">Einzufuegende Liste.</param>
		///		
		/// <param name="nIndex">index bei welchem diese Liste eingefuegt werden soll</param>
		///		
		/// <param name="shouldReplace">falls true, wird der Block an der Stelle nIndex mit der Blockliste ersetzt\nsnsonsten danach eingefuegt</param>
		///
		/// <returns></returns>
		//public virtual int Insert(BlockList blLst, int nIndex, bool shouldReplace)
		//{
		//    Block blnActive = Head;
		//    int nAktIndex = 0;

		//    //noch kein Element in der Liste vorhanden!
		//    if (blnActive == null)
		//    {
		//        mHead = blLst.mHead;
		//        mTail = blLst.mTail;
		//    }

		//    //Position zum einfuegen suchen
		//    while (blnActive != null)
		//    {
		//        Count++;
		//        if (nAktIndex == nIndex)
		//        {
		//            if (shouldReplace)
		//            {

		//                if (blnActive == mHead)
		//                {
		//                    mHead = blLst.mHead;
		//                }
		//                else
		//                {
		//                    blnActive.Prev.Next = blLst.mHead;
		//                    blLst.mHead.Prev = blnActive.Prev;
		//                }

		//                if (blnActive == mTail)
		//                {
		//                    mTail = blLst.mTail;
		//                }
		//                else
		//                {
		//                    blnActive.Next.Prev = blLst.mTail;
		//                    blLst.mTail.Next = blnActive.Next;
		//                }

		//                blnActive.Next = null;
		//                blnActive.Prev = null;
		//                blnActive = null;

		//                RaiseCollectionChanged();

		//                return 0;
		//            }
		//            else
		//            {
		//                if (blnActive == mTail)
		//                {
		//                    mTail = blLst.mTail;
		//                }
		//                else
		//                {
		//                    blnActive.Next.Prev = blLst.mTail;
		//                    blLst.mTail.Next = blnActive.Next;
		//                }

		//                blnActive.Next = blLst.mHead;
		//                blLst.mHead.Prev = blnActive;

		//                RaiseCollectionChanged();

		//                return 0;
		//            }
		//        }

		//        nAktIndex++;
		//        blnActive = blnActive.Next;
		//    }

		//    return -1; //Fehler, waere etwas ersetzt worden, waere er nicht bis hier gekommen
		//}

		#endregion

		#region Remove

		/// <summary>
		///	Entfernt ein Listenelement
		/// </summary>
		/// <param name="nIndex">zu entfernendes Element</param>
		public void Remove(int index)
		{
			Block existing = GetBlockFromIndex(index);
			if (existing != null)
			{
				Delete(existing);
			}

			//int nAktIndex = 0;

			//// Count--;

			//Block blnActive = Head;
			////Position zum einfuegen suchen
			//while (blnActive != null)
			//{
			//    if (nAktIndex == nIndex)
			//    {
			//        Delete(blnActive);

			//        #region old Code
			//        /*
			//        if ((blnActive == mTail) && (blnActive == mHead))
			//        {
			//            mTail = null;
			//            mHead = null;
			//        }
			//        else
			//        {
			//            if (blnActive.Next == null)
			//            {
			//                blnActive.Prev.Next = null;
			//                mTail = blnActive.Prev;
			//            }
			//            else
			//            {
			//                blnActive.Next.Prev = blnActive.Prev;
			//            }

			//            if (blnActive.Prev == null)
			//            {
			//                mHead = blnActive.Next;
			//                mHead.Prev = null;
			//            }
			//            else
			//            {
			//                blnActive.Prev.Next = blnActive.Next;
			//            }
			//        }
			//        blnActive.Prev = null;
			//        blnActive.Next = null;
			//        */
			//        #endregion
			//    }

			//    nAktIndex++;
			//    blnActive = blnActive.Next;
			//}
			//// RaiseCollectionChanged();
		}

		#endregion

		#endregion

		#region Controls

		private ICollectionWithEvents<Control> mControls;
		public ICollectionWithEvents<Control> Controls
		{
			get
			{
				return mControls;
			}
		}

		#endregion

		#region Enumerators

		/// <returns>New enumerator for this list.</returns>
		IEnumerator<Block> IEnumerable<Block>.GetEnumerator()
		{
			Block Current = this.Head;
			while (Current != null)
			{
				// C# 2.0 has this new wonderful ability - yield return
				// each time execution encounters a yield return,
				// the returned element is added to the IEnumerator being returned
				// by the function
				yield return Current;
				Current = Current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			Block Current = this.Head;
			while (Current != null)
			{
				yield return Current;
				Current = Current.Next;
			}
		}

		public IEnumerable<Block> Reversed
		{
			get
			{
				Block Current = this.Tail;
				while (Current != null)
				{
					yield return Current;
					Current = Current.Prev;
				}
			}
		}

		#endregion

		#region GetPrevElement, GetNextElement, ToArray

		public Block GetPrevElement(Block element)
		{
			return element.Prev;
		}

		public Block GetNextElement(Block element)
		{
			return element.Next;
		}

		public Block[] ToArray()
		{
			Block[] result = new Block[this.Count];
			int i = 0;
			foreach (Block b in this)
			{
				result[i++] = b;
			}
			return result;
		}

		#endregion
	}
}
