using System.Collections.Generic;
using System.Collections.ObjectModel;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.UI
{
	public class CompletionListItems 
		: Collection<CompletionListItem>, ICompletionListItems
	{
		public CompletionListItem FindFirstItemWithPrefix(string prefix)
		{
			foreach (CompletionListItem item in this)
			{
				if (startsWith(item, prefix))
				{
					return item;
				}
			}
			return null;
		}

		private bool startsWith(CompletionListItem item, string prefix)
		{
			return item.Text.ToLower().StartsWith(prefix.ToLower());
		}

		private bool mShouldSortItems = true;
		public bool ShouldSortItems
		{
			get
			{
				return mShouldSortItems;
			}
			set
			{
				mShouldSortItems = value;
			}
		}

		private bool mAllowDuplicateStrings = false;
		public bool AllowDuplicateStrings
		{
			get
			{
				return mAllowDuplicateStrings;
			}
			set
			{
				mAllowDuplicateStrings = value;
			}
		}

		private Set<string> DuplicateHash = new Set<string>();

		protected override void ClearItems()
		{
			base.ClearItems();
			DuplicateHash.Clear();
		}

		protected override void InsertItem(int index, CompletionListItem item)
		{
			if (!AllowDuplicateStrings && DuplicateHash.Contains(item.Text))
			{
				return;
			}
			base.InsertItem(index, item);
			if (!AllowDuplicateStrings)
			{
				DuplicateHash.Add(item.Text);
			}
		}
		
		#region TextCompletion

		public void AddRange(IEnumerable<CompletionListItem> itemsToAdd)
		{
			foreach (CompletionListItem item in itemsToAdd)
			{
				Add(item);
			}
		}
		
		public void AddText(string text)
		{
			TextCompletionItem item = new TextCompletionItem(text);
			Add(item);
		}
		
		public void AddText(string text, System.Drawing.Image icon)
		{
			TextCompletionItem item = new TextCompletionItem(text, icon);
			Add(item);
		}

		public void AddText(IEnumerable<string> textItems)
		{
			foreach (string s in textItems)
			{
				AddText(s);
			}
		}

		public void AddText(IEnumerable<TextPictureInfo> textItems)
		{
			foreach (TextPictureInfo t in textItems)
			{
				AddText(t.Text, t.Picture);
			}
		}
		
		#endregion
	}
}
