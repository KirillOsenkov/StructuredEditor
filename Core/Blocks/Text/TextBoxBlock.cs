using GuiLabs.Canvas.Controls;
using GuiLabs.Utils;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Blocks
{
	public class TextBoxBlock : Block, IHasText, ITextProvider
	{
		#region ctors

		public TextBoxBlock()
		{
			MyTextBox = new TextBox();
			Init();
		}

		public TextBoxBlock(int minWidth)
			: this()
		{
			MyTextBox.MinWidth = minWidth;
		}

		public TextBoxBlock(string defaultText)
		{
			MyTextBox = new TextBox(defaultText);
			Init();
		}

		private void Init()
		{
			InitBox();
		}

		protected virtual void InitBox()
		{
			//this.MyControl.Box.Borders.SetAll(0);
			//this.MyControl.Box.Margins.SetAll(1);
		}

		#endregion

		#region Events

		public event TextChangedEventHandler TextChanged;
		protected void RaiseTextChanged(string oldText, string newText)
		{
			if (TextChanged != null)
			{
				TextChanged(this, oldText, newText);
			}
		}

		#endregion

		#region Control

		public override Control MyControl
		{
			get { return mMyTextBox; }
		}

		private TextBox mMyTextBox;
		public TextBox MyTextBox
		{
			get { return mMyTextBox; }
			set
			{
				if (mMyTextBox != null)
				{
					UnSubscribeControl();
				}
				mMyTextBox = value;
				if (mMyTextBox != null)
				{
					SubscribeControl();
				}
			}
		}

		protected override void SubscribeControl()
		{
			base.SubscribeControl();
			mMyTextBox.TextChanged += OnTextHasChanged;
			mMyTextBox.CaretPositionChanged += OnCaretPositionChanged;
			mMyTextBox.PreviewShowPopupMenu += OnShowingPopupMenu;
			mMyTextBox.PreviewKeyDown += OnPreviewKeyDown;
			mMyTextBox.AfterKeyDown += OnAfterKeyDown;
			mMyTextBox.TextBoxChangePending += mMyTextBox_TextBoxChangePending;
		}

		protected override void UnSubscribeControl()
		{
			base.UnSubscribeControl();
			mMyTextBox.TextChanged -= OnTextHasChanged;
			mMyTextBox.CaretPositionChanged -= OnCaretPositionChanged;
			mMyTextBox.PreviewShowPopupMenu -= OnShowingPopupMenu;
			mMyTextBox.PreviewKeyDown -= OnPreviewKeyDown;
			mMyTextBox.AfterKeyDown -= OnAfterKeyDown;
			mMyTextBox.TextBoxChangePending -= mMyTextBox_TextBoxChangePending;
		}

		#endregion

		#region Text

		public override string ToString()
		{
			return MyTextBox.Text;
		}

		public string Text
		{
			get
			{
				return MyTextBox.Text;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				MyTextBox.Text = value;
			}
		}

		public override bool SetCursorToTheBeginning()
		{
			if (!this.CanGetFocus)
			{
				return false;
			}
			this.MyTextBox.SetCaretToBeginning();
			return this.SetFocusCore();
		}

		public override bool SetCursorToTheEnd()
		{
			if (!this.CanGetFocus)
			{
				return false;
			}
			this.MyTextBox.SetCaretToEnd();
			return this.SetFocusCore();
		}

		#endregion

		#region Lexer

		public string GetWordBeforeCaret()
		{
			return this.MyTextBox.GetWordBeforeCaret();
		}

		#endregion

		#region ShouldRecordActions

		private bool mShouldRecordActions = true;
		public bool ShouldRecordActions
		{
			get
			{
				return mShouldRecordActions;
			}
			set
			{
				mShouldRecordActions = value;
			}
		}

		#endregion

		#region OnEvents

		#region OnKeyDown

		protected virtual void OnPreviewKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		protected virtual void OnAfterKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Handled)
			{
				return;
			}
			bool handled = false;

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Left:
					OnKeyDownLeft(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Right:
					OnKeyDownRight(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Up:
					OnKeyDownUp(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Down:
					OnKeyDownDown(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Home:
					OnKeyDownHome(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.End:
					OnKeyDownEnd(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Back:
					OnKeyDownBack(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Return:
					OnKeyDownReturn(e);
					handled = e.Handled;
					break;
				case System.Windows.Forms.Keys.Tab:
					OnKeyDownTab(e);
					handled = e.Handled;
					break;
				default:
					break;
			}

			if (!handled)
			{
				base.OnKeyDown(sender, e);
			}

			lastKey = e;
		}

		protected static System.Windows.Forms.KeyEventArgs lastKey = new System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.None);

		protected virtual void OnKeyDownLeft(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownRight(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownUp(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownDown(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownEnd(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownHome(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownTab(System.Windows.Forms.KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
		}

		/// <summary>
		/// We need to do nothing here because our TextBox already
		/// handles it. We don't want to delete this TextBoxBlock each time
		/// the user presses delete on it.
		/// </summary>
		protected override void OnKeyDownDelete(System.Windows.Forms.KeyEventArgs e)
		{
			// left empty on purpose
		}

		#endregion

		protected virtual void OnCaretPositionChanged(CaretPositionChangedEventArgs e)
		{
			this.DisplayContextHelp();
		}

		private void mMyTextBox_TextBoxChangePending(TextBox sender, TextBoxChangeEventArgs e)
		{
			if (this.ActionManager != null && ShouldRecordActions)
			{
				IAction a = e.Action;
				e.Handled = true;
				this.ActionManager.RecordAction(a);
			}
		}

		protected virtual void OnTextHasChanged(
			ITextProvider changedControl,
			string oldText,
			string newText)
		{
			RaiseTextChanged(oldText, newText);
			if (this.Text == "")
			{
				OnTextIsEmpty(oldText);
			}
			DisplayContextHelp();
		}

		protected virtual void OnShowingPopupMenu(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
		{
		}

		protected virtual void OnTextIsEmpty(string oldText)
		{

		}

		#endregion

		#region Clipboard

		public override void Cut()
		{
			if (MyTextBox.SelectionLength > 0)
			{
				MyTextBox.OnUserCutCommand();
			}
			else
			{
				base.Cut();
			}
		}

		public override void Copy()
		{
			if (MyTextBox.SelectionLength > 0)
			{
				MyTextBox.OnUserCopyCommand();
			}
			else
			{
				base.Copy();
			}
		}

		public override void Paste()
		{
			if (System.Windows.Forms.Clipboard.ContainsData(ClipboardFormat))
			{
				base.Paste();
			}
			else
			{
				MyTextBox.OnUserPasteCommand();
			}
		}

		#endregion

		#region Delete

		private bool mCanBeDeletedByUser = false;
		public bool CanBeDeletedByUser
		{
			get
			{
				return mCanBeDeletedByUser;
			}
			set
			{
				mCanBeDeletedByUser = value;
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			this.Text = storage["text"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["text"] = this.Text;
		}

		#endregion
	}
}
