using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using GuiLabs.Canvas.Events;
using GuiLabs.Utils;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.UI
{
	public partial class CompletionList : Form, ICompletionList
	{
		#region ctor

		public CompletionList()
		{
			InitializeComponent();
			InitCommittingChars();
			this.Leave += CompletionList_Leave;
		}

		#endregion

		private CompletionFunctionality mCurrentList;
		public CompletionFunctionality CurrentList
		{
			get
			{
				return mCurrentList;
			}
			set
			{
				mCurrentList = value;
				if (mCurrentList != null)
				{
					DefaultKeyHandler = value.HostBlock.MyControl;
				}
			}
		}

		#region Items

		#region ICompletionListBuilder
		
		private Set<string> DuplicateHash = new Set<string>();
		
		public void Add(CompletionListItem itemToAdd)
		{
			if (itemToAdd.ShouldShow(CurrentList)
			    && !DuplicateHash.Contains(itemToAdd.Text)
			   )
			{
				lstItems.Items.Add(itemToAdd);
				DuplicateHash.Add(itemToAdd.Text);
			}
		}
		
		public void AddRange(IEnumerable<CompletionListItem> itemsToAdd)
		{
			foreach(CompletionListItem item in itemsToAdd)
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

		public int Count
		{
			get
			{
				return lstItems.Items.Count;
			}
		}
		
		public void StartAddingItems()
		{
			lstItems.BeginUpdate();
			lstItems.Items.Clear();
			DuplicateHash.Clear();
		}
		
		public void FinishAddingItems()
		{
			lstItems.EndUpdate();
		}
		
		#endregion
		
		public IEnumerable<CompletionListItem> Items
		{
			get
			{
				return CurrentList.Items;
			}
		}

		public bool ShouldSortItems
		{
			get
			{
				return lstItems.Sorted;
			}
			set
			{
				lstItems.Sorted = value;
			}
		}

		public int MaxVisibleItems
		{
			get { return 10; }
		}

		private void FillItems()
		{
//			lstItems.BeginUpdate();
//			lstItems.Items.Clear();
//			foreach (CompletionListItem Item in this.CurrentList.ItemsToShow)
//			{
//				lstItems.Items.Add(Item);
//			}
//			lstItems.EndUpdate();
		}

		/// <summary>
		/// Selects the first matching item found,
		/// that has text starting with matchBeginning
		/// </summary>
		/// <param name="matchBeginning">Beginning of the item text</param>
		public void SelectItem(string matchBeginning)
		{
			matchFound = false;
			for (int i = 0; i <= lstItems.Items.Count - 1; i++)
			{
				CompletionListItem CurrentItem = lstItems.Items[i] as CompletionListItem;
				if (CurrentItem.Text.StartsWith(matchBeginning, CurrentList.TextComparison))
				{
					lstItems.SelectedIndex = i;
					matchFound = true;
					return;
				}
			}
			lstItems.SelectedIndex = -1;
		}

		private bool matchFound = false;

		//public CompletionListItem FindItem(string matchBeginning)
		//{
		//    for (int i = 0; i <= lstItems.Items.Count - 1; i++)
		//    {
		//        CompletionListItem CurrentItem = lstItems.Items[i] as CompletionListItem;
		//        if (CurrentItem.Text.ToLower().StartsWith(matchBeginning.ToLower()))
		//        {
		//            return CurrentItem;
		//        }
		//    }
		//    return null;
		//}

		public CompletionListItem SelectedItem()
		{
			return (lstItems.SelectedItem) as CompletionListItem;
		}

		private CompletionListItem GetActiveItem()
		{
			if (lstItems.SelectedIndex > -1)
			{
				return lstItems.Items[lstItems.SelectedIndex] as CompletionListItem;
			}
			else
			{
				return null;
			}
		}

		#endregion

		#region Click

		private void Item_MouseClick(object sender, MouseEventArgs e)
		{
			matchFound = true;
			PerformClick(new ItemClickReason());
		}

		private void PerformClick(ItemClickReason reason)
		{
			using (Redrawer r = new Redrawer(CurrentList.HostBlock.Root))
			{
				CompletionListItem ActiveItem = GetActiveItem();
				HideList();
				if (ActiveItem != null && matchFound)
				{
					ActiveItem.Reason = reason;
					ActiveItem.Click(CurrentList);
					if (CurrentList != null)
					{
						CurrentList.RaiseItemClicked(ActiveItem);
					}
				}
			}
		}

		#endregion

		#region Window

		#region Size and position

		private Control mParentControl;
		/// <summary>
		/// NearRectangle parameter is specified
		/// in the coordinate system of ParentControl
		/// </summary>
		public Control ParentControl
		{
			get
			{
				return mParentControl;
			}
			set
			{
				mParentControl = value;
			}
		}

		/// <summary>
		/// Calculate the size of the popup based on the number of items
		/// Uses MaxVisibleItems property
		/// </summary>
		public void CalcSize()
		{
			int itemCount = Common.Min(lstItems.Items.Count, MaxVisibleItems);
			if (itemCount <= 0)
			{
				return;
			}

			int contentHeight = lstItems.GetRequiredContentHeight();
			int contentWidth = lstItems.GetRequiredContentWidth();

			lstItems.Height = lstItems.ItemHeight * itemCount;

			int width = lstItems.GetRequiredListBoxWidth(contentWidth, contentHeight);
			lstItems.Width = width;

			// HACK: this hack somehow prevents that the scrollbars
			// are shown when they are not necessary at all
			lstItems.HorizontalExtent = 1; //  contentWidth - SystemInformation.VerticalScrollBarWidth;
			lstItems.HorizontalExtent = contentWidth;

			this.ClientSize = lstItems.Size;
		}

		/// <summary>
		/// Calculate and set the left-top-corner position
		/// where to show the popup
		/// </summary>
		/// <param name="nearRectangle"></param>
		private void CalcStartPosition(Rectangle nearRectangle, Control parent)
		{
			#region Width

			// width and height of the popup list
			int w = this.Width;

			// calculate X coordinate, where to show the popup
			int x = nearRectangle.Left;

			// if it exceeds the right border of the screen,
			// show the popup to the left of the nearRectangle
			//int screenWidth = Screen.FromControl(this).Bounds.Width;
			int pW = parent.Width;
			if (x + w > pW)
			{
				x = nearRectangle.Left - w;
				if (x < 0)
				{
					x = 0;
				}
			}

			// assign the coordinates
			this.Left = x;

			#endregion

			#region Height

			int h = this.Height;

			// calculate Y coordinate, where to show the popup
			int y = nearRectangle.Bottom;

			// int screenHeight = Screen.FromControl(this).Bounds.Height;
			int pH = parent.Height;
			if (y + h > pH)
			{
				y = nearRectangle.Top - h;
				if (y < 0)
				{
					y = 0;
				}
			}

			this.Top = y;

			#endregion
		}

		#endregion

		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}

		#endregion

		#region Show

		/// <summary>
		/// Shows the completion list popup at a given point on screen
		/// </summary>
		/// <param name="NearRectangle">Normally, the popup list will be shown
		/// right under this block (under NearRectangle)</param>
		/// <param name="parentControl">DrawWindow that causes this popup to show</param>
		public void Show(Rectangle nearRectangle, Control parent)
		{
			if (this.Visible)
			{
				return;
			}
			ParentControl = parent;

			FillItems();
			if (lstItems.Items.Count == 0)
			{
				return;
			}

			this.Parent = parent.Parent;
			this.BringToFront();

			CalcSize();
			CalcStartPosition(nearRectangle, parent);

			this.Show();

			this.lstItems.Focus();

			if (CurrentList != null)
			{
				CurrentList.RaiseVisibleChanged(true);
			}
		}

		private bool hidingList = false; // guard
		public void HideList()
		{
			if (hidingList || CurrentList == null)
			{
				return;
			}
			hidingList = true;

			using (Redrawer r = new Redrawer(CurrentList.HostBlock.Root))
			{
				ViewWindow v = ParentControl as ViewWindow;

				if (v != null)
				{
					v.ShouldRedrawOnWindowPaint = false;
				}

				ParentControl.Focus();
				Hide();
				Application.DoEvents();

				if (v != null)
				{
					v.ShouldRedrawOnWindowPaint = true;
				}

				if (CurrentList != null)
				{
					CurrentList.RaiseVisibleChanged(false);
				}
			}
			
//			lstItems.BeginUpdate();
//			lstItems.Items.Clear();
//			lstItems.EndUpdate();

			hidingList = false;
		}

		void CompletionList_Leave(object sender, EventArgs e)
		{
			HideList();
		}

		#endregion

		#region Keyboard

		#region Events

		private void lstItems_KeyDown(object sender, KeyEventArgs e)
		{
			bool shouldRaiseEvent = false;
			bool needToHide = false;

			switch (e.KeyCode)
			{
				case Keys.Tab:
				case Keys.Return:
					PerformClick(new ItemClickReason(e.KeyCode));
					break;
				case Keys.Up:
				case Keys.Down:
				case Keys.PageUp:
				case Keys.PageDown:
					matchFound = true;
					break;
				case Keys.Escape:
					HideList();
					break;
				case Keys.Back:
				case Keys.Delete:
					shouldRaiseEvent = true;
					break;
				case Keys.Left:
				case Keys.Right:
				case Keys.Home:
				case Keys.End:
					shouldRaiseEvent = true;
					needToHide = true;
					break;
				default:
					break;
			}

			if (shouldRaiseEvent && DefaultKeyHandler != null)
			{
				using (Redrawer r = new Redrawer(CurrentList.HostBlock.Root))
				{
					if (needToHide)
					{
						Hide();
					}
					DefaultKeyHandler.OnKeyDown(e);
				}
			}
		}

		private void lstItems_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.CommittingChars.Contains(e.KeyChar))
			{
				using (Redrawer r = new Redrawer(CurrentList.HostBlock.Root))
				{
					PerformClick(new ItemClickReason(e.KeyChar));
					DefaultKeyHandler.OnKeyPress(e);
				}
			}
			else if (DefaultKeyHandler != null && !char.IsControl(e.KeyChar))
			{
				DefaultKeyHandler.OnKeyPress(e);
			}
			e.Handled = true;
		}

		#endregion

		private IKeyHandler mDefaultKeyHandler;
		public IKeyHandler DefaultKeyHandler
		{
			get
			{
				return mDefaultKeyHandler;
			}
			set
			{
				mDefaultKeyHandler = value;
			}
		}

		#region Committing

		private Set<char> mCommittingChars = new Set<char>();
		public Set<char> CommittingChars
		{
			get
			{
				return mCommittingChars;
			}
			set
			{
				mCommittingChars = value;
			}
		}

		private void InitCommittingChars()
		{
			foreach (char c in GetCommittingChars())
			{
				this.CommittingChars.Add(c);
			}
		}

		private IEnumerable<char> GetCommittingChars()
		{
			char[] result = new char[]
			{
			'{',
			'}',
			'[',
			']',
			'(',
			')',
			'.',
			',',
			':',
			';',
			'+',
			'-',
			'*',
			'/',
			'%',
			'&',
			'|',
			'^',
			'!',
			'~',
			'=',
			'<',
			'>',
			'?',
			'@',
			'#',
			'\'',
			'\"',
			'\\',
			' '
			};

			return result;
		}

		#endregion

		#endregion

		IEnumerable<CompletionListItem> ICompletionList.Items
		{
			get
			{
				foreach (object o in lstItems.Items)
				{
					CompletionListItem result = o as CompletionListItem;
					if (result != null)
					{
						yield return result;
					}
				}
			}
		}
	}

	#region ListBox2

	/// <summary>
	/// HACK: A hack list box that accepts all keyboard keys.
	/// Necessary to catch the Tab key.
	/// </summary>
	public class ListBox2 : IconListBox
	{
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}

		protected override Image GetIconForItem(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				return null;
			}
			CompletionListItem item = this.Items[index] as CompletionListItem;
			if (item == null)
			{
				return null;
			}

			return item.Picture;
		}
	}

	#endregion
}
