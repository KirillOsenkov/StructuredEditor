using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using System.Collections.Generic;

namespace GuiLabs.Editor.Blocks
{
	public class TextBoxBlockWithCompletion : TextBoxBlock, IHasCompletion
	{
		#region ctors

		public TextBoxBlockWithCompletion()
			: base()
		{
			Init();
		}

		public TextBoxBlockWithCompletion(int minWidth)
			: base(minWidth)
		{
			Init();
		}

		public TextBoxBlockWithCompletion(string initialText)
			: base(initialText)
		{
			Init();
		}

		public TextBoxBlockWithCompletion(CompletionFunctionality completion)
			: base()
		{
			Completion = completion;
		}

		private void InitCompletion()
		{
			Completion = new CompletionFunctionality();
		}

		private void Init()
		{
			InitCompletion();
		}

		#endregion

		#region Events
		
		public event CustomItemsRequestHandler CustomItemsRequested;
		protected void RaiseCustomItemsRequested(CustomItemsRequestEventArgs e)
		{
			if (CustomItemsRequested != null)
			{
				CustomItemsRequested(e);
			}
		}
		
		#endregion
		
		#region OnEvents

		protected override void OnAfterKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Back
				|| e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				if (Completion.Visible)
				{
					TryShowCompletionList();
					e.Handled = true;
				}
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);
			if (e.Handled)
				return;

			if (this.MyControl.IsFocused && Strings.CanBeStartOfVariableName(e.KeyChar))
			{
				TryShowCompletionList();
				e.Handled = true;
			}
		}

		protected override void OnMouseUp(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		{
			if (e.IsRightButtonPressed)
			{
				OnMouseUpRight(e);
			}
			if (!e.Handled)
			{
				base.OnMouseUp(e);
			}
		}

		protected virtual void OnMouseUpRight(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		{
			//MyTextBox.OnShowPopupMenu(e);
			//e.Handled = true;
		}

		#endregion

		#region Completion

		private CompletionFunctionality mCompletion;
		public virtual CompletionFunctionality Completion
		{
			get
			{
				return mCompletion;
			}
			set
			{
				if (mCompletion != null)
				{
					mCompletion.VisibleChanged -= OnCompletionVisibleChanged;
					//mCompletion.BeforeShowingCompletionList -= OnBeforeShowingCompletionList;
					mCompletion.CustomItemsRequested -= PrepareItemsToShowCore;
				}
				mCompletion = value;
				if (mCompletion != null)
				{
					mCompletion.VisibleChanged += OnCompletionVisibleChanged;
					//mCompletion.BeforeShowingCompletionList += OnBeforeShowingCompletionList;
					mCompletion.CustomItemsRequested += PrepareItemsToShowCore;
				}
			}
		}
		
		protected void PrepareItemsToShowCore(CustomItemsRequestEventArgs e)
		{
			RaiseCustomItemsRequested(e);
			if (e.ShouldCallFillItems)
			{
				FillItems(e);
			}
		}

		protected virtual void FillItems(CustomItemsRequestEventArgs e)
		{

		}
		
		//protected virtual void OnBeforeShowingCompletionList(BeforeShowingCompletionListEventArgs e)
		//{

		//}

		protected virtual void OnCompletionVisibleChanged(bool isCompletionListNowVisible)
		{
			if (Completion.HostBlock == this)
			{
				DisplayContextHelp();
			}
		}

		#region Items

		public void ClearItems()
		{
			Completion.ClearItems();
		}

		public virtual TextCompletionItem AddTextItem(string completionTextString)
		{
			return Completion.AddTextCompletionItem(completionTextString);
		}

		public virtual TextCompletionItem AddTextItem(string completionTextString, System.Drawing.Image picture)
		{
			return Completion.AddTextCompletionItem(completionTextString, picture);
		}

		#endregion

		public virtual void TryShowCompletionList()
		{
			string beforeCaret = this.MyTextBox.TextBeforeCaret;
			string lastWord = Strings.GetLastWord(beforeCaret);

			if (lastWord.Length > 0)
			{
				Completion.ShowCompletionList(this, lastWord);
			}
			else
			{
				Completion.HideCompletionList();
			}
		}

		public bool CompletionIsShown
		{
			get
			{
				return Completion != null
					&& Completion.Visible
					&& Completion.HostBlock == this;
			}
		}

		public bool CompletionItemIsSelected
		{
			get
			{
				return CompletionIsShown
					&& Completion.ItemIsSelected;
			}
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{

		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string completionHint in GetCompletionHelp())
				{
					yield return completionHint;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}
		protected virtual IEnumerable<string> GetCompletionHelp()
		{
			if (Completion.Visible)
			{
				yield return HelpBase.CommitCompletion;
				yield return HelpBase.CancelCompletion;
			}
			else
			{
				yield return HelpBase.StartTypingForCompletion;
			}
		}

		#endregion
	}
}
