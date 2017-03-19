using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using System.Collections.Generic;
using System;
using GuiLabs.Utils;
using GuiLabs.Utils.Delegates;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Drawing;

namespace GuiLabs.Editor.UI
{
	public delegate void CustomItemsRequestHandler(CustomItemsRequestEventArgs e);
	public delegate void BeforeShowingCompletionListHandler(BeforeShowingCompletionListEventArgs e);
	public class CustomItemsRequestEventArgs : CancelEventArgs
	{
		public CustomItemsRequestEventArgs(ICompletionListBuilder items)
		{
			mItems = items;
		}
		
		private ICompletionListBuilder mItems;
		public ICompletionListBuilder Items
		{
			get
			{
				return mItems;
			}
		}
		
		private bool mShowOnlyCustomItems = false;
		public bool ShowOnlyCustomItems
		{
			get
			{
				return mShowOnlyCustomItems;
			}
			set
			{
				mShowOnlyCustomItems = value;
			}
		}
		
		private bool mShouldCallFillItems = true;
		public bool ShouldCallFillItems
		{
			get
			{
				return mShouldCallFillItems;
			}
			set
			{
				mShouldCallFillItems = value;
			}
		}
	}
	
	public class BeforeShowingCompletionListEventArgs : CancelEventArgs
	{
		private Block mHostBlock;
		public Block HostBlock
		{
			get
			{
				return mHostBlock;
			}
			set
			{
				mHostBlock = value;
			}
		}

		private string mItemToPreselect;
		public string ItemToPreselect
		{
			get
			{
				return mItemToPreselect;
			}
			set
			{
				mItemToPreselect = value;
			}
		}
	}

	public class CompletionFunctionality
	{
		#region Events

		public event CustomItemsRequestHandler CustomItemsRequested;
		protected CustomItemsRequestEventArgs RaiseCustomItemsRequested(ICompletionListBuilder items)
		{
			if (CustomItemsRequested != null)
			{
				CustomItemsRequestEventArgs e = new CustomItemsRequestEventArgs(items);
				CustomItemsRequested(e);
				return e;
			}
			return null;
		}

		public event BeforeShowingCompletionListHandler BeforeShowingCompletionList;
		/// <summary>
		/// 
		/// </summary>
		/// <returns>true if should cancel showing the list, default is false.</returns>
		protected bool RaiseBeforeShowingCompletionList(Block hostBlock)
		{
			if (BeforeShowingCompletionList != null)
			{
				BeforeShowingCompletionListEventArgs e = new BeforeShowingCompletionListEventArgs();
				e.HostBlock = hostBlock;
				BeforeShowingCompletionList(e);
				return e.Cancel;
			}
			return false;
		}

		public event CompletionListVisibleChangedDelegate VisibleChanged;
		internal void RaiseVisibleChanged(bool isNowVisible)
		{
			if (VisibleChanged != null)
			{
				VisibleChanged(isNowVisible);
			}
		}

		public event CompletionListItemClickedDelegate ItemClicked;
		internal void RaiseItemClicked(CompletionListItem item)
		{
			if (ItemClicked != null)
			{
				ItemClicked(item);
			}
		}

		#endregion

		#region Host

		private Block mHostBlock;
		public Block HostBlock
		{
			get
			{
				return mHostBlock;
			}
			set
			{
				mHostBlock = value;
			}
		}

		#endregion

		#region Items

		/// <summary>
		/// List of items to display in the completion list.
		/// Items can be of various types and different actions
		/// can be executed, when you hit Enter on the item.
		/// </summary>
		/// <example>
		/// Most common type of item is CreateBlocksItem.
		/// When user selects CreateBlocksItem from the list,
		/// it inserts new blocks after current EmptyBlock.
		/// </example>
		/// <seealso cref="\Core\Blocks\Empty\CreateBlocksItem.cs"/>
		private ICompletionListItems mItems = new CompletionListItems();
		public ICompletionListItems Items
		{
			get
			{
				return mItems;
			}
		}
		
		public void ClearItems()
		{
			Items.Clear();
		}

		#region Add items

		#region AddItem

		public void AddItem(CompletionListItem item)
		{
			Items.Add(item);
		}

		public void AddItems(params CompletionListItem[] items)
		{
			AddItems((IEnumerable<CompletionListItem>)items);
		}

		public void AddItems(IEnumerable<CompletionListItem> items)
		{
			foreach (CompletionListItem item in items)
			{
				AddItem(item);
			}
		}

		#endregion

		#region CreateBlocksItem

		public CreateBlocksItem AddCreateBlocksItem(
			string itemTitle,
			params System.Type[] blocksToCreate)
		{
			CreateBlocksItem item = new CreateBlocksItem(
				itemTitle,
				blocksToCreate
			);
			Items.Add(item);
			return item;
		}

		public CreateBlocksItem AddCreateBlocksItem<T1>(
			string itemTitle)
		{
			CreateBlocksItem item = new CreateBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1>()
			);
			Items.Add(item);
			return item;
		}

        public CreateBlocksItem AddCreateBlocksItem<T1>(
            string itemTitle, Image image)
        {
            CreateBlocksItem item = new CreateBlocksItem(
                itemTitle,
                BlockActivatorFactory.Types<T1>()
            );
            item.Picture = image;
            Items.Add(item);
            return item;
        }

		public CreateBlocksItem AddCreateBlocksItem<T1, T2>(
			string itemTitle)
		{
			CreateBlocksItem item = new CreateBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1, T2>()
			);
			Items.Add(item);
			return item;
		}

		public CreateBlocksItem AddCreateBlocksItem<T1, T2>(
			string itemTitle, Image image)
		{
			CreateBlocksItem item = new CreateBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1, T2>()
			);
			item.Picture = image;
			Items.Add(item);
			return item;
		}
		
		public CreateBlocksItem AddCreateBlocksItem<T1, T2, T3>(
			string itemTitle)
		{
			CreateBlocksItem item = new CreateBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1, T2, T3>()
			);
			Items.Add(item);
			return item;
		}

		#endregion

		#region ReplaceBlocksItem

		public ReplaceBlocksItem AddReplaceBlocksItem(
			string itemTitle,
			IEnumerable<Type> typesOfBlocksToInsert)
		{
			ReplaceBlocksItem item = new ReplaceBlocksItem(
				itemTitle,
				typesOfBlocksToInsert
			);
			Items.Add(item);
			return item;
		}

		public ReplaceBlocksItem AddReplaceBlocksItem<T1>(
			string itemTitle)
		{
			ReplaceBlocksItem item = new ReplaceBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1>()
			);
			Items.Add(item);
			return item;
		}

		public ReplaceBlocksItem AddReplaceBlocksItem<T1, T2>(
			string itemTitle)
		{
			ReplaceBlocksItem item = new ReplaceBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1, T2>()
			);
			Items.Add(item);
			return item;
		}

		public ReplaceBlocksItem AddReplaceBlocksItem<T1, T2, T3>(
			string itemTitle)
		{
			ReplaceBlocksItem item = new ReplaceBlocksItem(
				itemTitle,
				BlockActivatorFactory.Types<T1, T2, T3>()
			);
			Items.Add(item);
			return item;
		}

		#endregion

		#region TextCompletion

		public TextCompletionItem AddTextCompletionItem(string text)
		{
			TextCompletionItem result = new TextCompletionItem(text);
			Items.Add(result);
			return result;
		}

		public TextCompletionItem AddTextCompletionItem(string text, System.Drawing.Image picture)
		{
			TextCompletionItem result = new TextCompletionItem(text, picture);
			Items.Add(result);
			return result;
		}

		#endregion

		#endregion

		public bool ItemIsSelected
		{
			get
			{
				return DropDownBox.SelectedItem() != null;
			}
		}

		public CompletionListItem SelectedItem
		{
			get
			{
				return DropDownBox.SelectedItem();
			}
		}
		
		public ICompletionList DropDownBox
		{
			get
			{
				return UIManager.DropDownList;
			}
		}

		private StringComparison mTextComparison = StringComparison.InvariantCultureIgnoreCase;
		public StringComparison TextComparison
		{
			get
			{
				return mTextComparison;
			}
			set
			{
				mTextComparison = value;
			}
		}

		#endregion

		#region Show & Hide

		public bool CanShow()
		{
			SimpleCompletionListBuilder items = new SimpleCompletionListBuilder();
			FillItems(items);
			return items.Count > 0;
		}

		public bool CanShowForPrefix(string prefix)
		{
			SimpleCompletionListBuilder items = new SimpleCompletionListBuilder();
			FillItems(items);
			if (items.Count == 0)
			{
				return false;
			}
			return items.Exists(delegate (CompletionListItem i) 
			{
				return i.Text.StartsWith(prefix, TextComparison);
			});
		}

		public bool ExistingItemsHavePrefix(string prefix)
		{
			if (DropDownBox == null || DropDownBox.Items == null)
			{
				return false;
			}
			foreach (CompletionListItem item in DropDownBox.Items)
			{
				if (item.Text.StartsWith(prefix))
				{
					return true;
				}
			}
			return false;
		}

		public void FillItems(ICompletionListBuilder items)
		{
			CustomItemsRequestEventArgs e = RaiseCustomItemsRequested(items);
			if (e != null && e.ShowOnlyCustomItems)
			{
				return;
			}
			items.AddRange(this.Items);
		}

		/// <summary>
		/// Physically displays the completion list on the screen.
		/// </summary>
		/// <param name="hostBlock"></param>
		/// <returns>true if successfully shown, false otherwise.</returns>
		public bool ShowCompletionList(Block hostBlock)
		{
			if (this.Visible)
			{
				return true;
			}
			Param.CheckNotNull(hostBlock, "hostBlock");
			this.HostBlock = hostBlock;

			bool shouldCancel = RaiseBeforeShowingCompletionList(hostBlock);
			if (shouldCancel)
			{
				return false;
			}

			DropDownBox.CurrentList = this;
			
			DropDownBox.StartAddingItems();
			FillItems(DropDownBox);
			DropDownBox.FinishAddingItems();

			if (DropDownBox.Count == 0)
			{
				return false;
			}
			
			HostBlock.MyControl.DisplayCompletionList();
			
//			string e = Timer.ElapsedSince(l);
//			if (this.HostBlock.Root != null)
//			{
//				this.HostBlock.Root.ShowStatus(e);
//			}
			return true;
		}
		
		public void ShowCompletionList(Block hostBlock, string itemToPreselect)
		{
			if (this.Visible || ShowCompletionList(hostBlock))
			{
				UIManager.DropDownList.SelectItem(itemToPreselect);
			}
		}

		public void HideCompletionList()
		{
			if (Visible)
			{
				UIManager.DropDownList.HideList();
			}
		}

		public bool Visible
		{
			get
			{
				return UIManager.DropDownList.Visible;
			}
		}

		#endregion
	}
}
