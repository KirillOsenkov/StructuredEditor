using System;
using System.Collections.Generic;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Delegates;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	public class TextSelectionBlock : FocusableLabelBlock
	{
		#region ctors

		public TextSelectionBlock()
			: base()
		{
			Init();
		}

		public TextSelectionBlock(string text)
			: base(text)
		{
			Init();
		}

		public TextSelectionBlock(params string[] itemStrings)
			: this((IEnumerable<string>)itemStrings)
		{
		}

		public TextSelectionBlock(IEnumerable<string> itemStrings)
			: base()
		{
			ItemStrings = new List<string>(itemStrings);
			SetDefaultText();
			Init();
		}

		public TextSelectionBlock(Enum enumeration)
			: base(enumeration.ToString())
		{
			this.ItemStrings = Enum.GetNames(enumeration.GetType());
			Init();
		}

		private void Init()
		{
			mCompletion = new CompletionFunctionality();
			this.Completion.CustomItemsRequested += FillItems;
		}

		#endregion

		#region Text

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				string oldText = this.Text;
				if (oldText != value)
				{
					OnTextChanging(oldText, value);
					base.Text = value;
					OnTextChanged(oldText, value);
					RaiseTextChanged(oldText, value);
				}
			}
		}

		protected virtual void SetDefaultText()
		{
			string firstItem = Common.Head(ItemStrings);
			if (!string.IsNullOrEmpty(firstItem))
			{
				this.Text = firstItem;
			}
		}

		protected virtual void OnTextChanging(string oldText, string newText)
		{

		}

		protected virtual void OnTextChanged(string oldText, string newText)
		{

		}

		#endregion

		#region Completion

		private CompletionFunctionality mCompletion;
		public CompletionFunctionality Completion
		{
			get
			{
				return mCompletion;
			}
		}

		private IEnumerable<string> mItemStrings = new List<string>();
		public IEnumerable<string> ItemStrings
		{
			get
			{
				return mItemStrings;
			}
			set
			{
				mItemStrings = value;
			}
		}

		#region Add items

		public virtual void FillItems(CustomItemsRequestEventArgs e)
		{
			AddItems(e.Items, ItemStrings);
		}

		protected void AddItems(ICompletionListBuilder items, IEnumerable<string> itemsToAdd)
		{
			foreach (string s in itemsToAdd)
			{
				CompletionListItem item = CreateItem(s);
				if (item.ShouldShow(this.Completion))
				{
					items.Add(item);
				}
			}
		}

		public void AddTextItem(string text)
		{
			Completion.AddItem(CreateItem(text));
		}

		protected virtual CompletionListItem CreateItem(string text)
		{
			TextSelectItem item = new TextSelectItem(text);
			return item;
		}

		#endregion

		#endregion

		#region OnEvents

		protected override void OnDoubleClick(MouseWithKeysEventArgs MouseInfo)
		{
			ShouldShowCompletion();
		}

		protected override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			if (e.IsRightButtonPressed)
			{
				ShouldShowCompletion();
				SetFocus();
				e.Handled = true;
			}
		}

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return
				|| e.KeyCode == System.Windows.Forms.Keys.Space)
			{
				ShouldShowCompletion();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			foreach (CompletionListItem item in this.Completion.Items)
			{
				if (item.Text.ToLower().StartsWith(e.KeyChar.ToString().ToLower()))
				{
					Completion.ShowCompletionList(this, e.KeyChar.ToString());
					return;
				}
			}
		}

		public void ShouldShowCompletion()
		{
			Completion.ShowCompletionList(this, Text);
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "TextSelectionBlock";
		}

		#endregion
	}
}
