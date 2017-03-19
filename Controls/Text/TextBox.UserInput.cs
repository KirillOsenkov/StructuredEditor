using GuiLabs.Utils;
namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region OnActivate, OnDeactivate

		public override void OnDeactivate()
		{
			ResetSelection();
			base.OnDeactivate();
			Layout();
		}

		public override void OnActivate()
		{
			ResetSelection();
			base.OnActivate();
			Layout();
		}

		#endregion

		//protected virtual void OnUserTextInsert(string newSelection)
		//{
		//    if (string.IsNullOrEmpty(newSelection))
		//    {
		//        return;
		//    }
		//    string oldText = this.Text;
		//    BeforeTextChanged();
		//    InsertTextCore(newSelection);
		//    AfterTextChanged();
		//    RaiseTextChanged(oldText, this.Text);
		//}

		//protected virtual void OnUserTextDelete()
		//{
		//    if (string.IsNullOrEmpty(this.Text))
		//    {
		//        return;
		//    }
		//    string oldText = this.Text;
		//    BeforeTextChanged();
		//    DeleteSelectionCore();
		//    AfterTextChanged();
		//    RaiseTextChanged(oldText, this.Text);
		//}

		public virtual void OnUserPasteCommand()
		{
			string toInsert = GetTextToInsertFromClipboard();
			if (!string.IsNullOrEmpty(toInsert))
			{
				InsertText(toInsert);
			}
		}

		public virtual void OnUserCutCommand()
		{
			OnUserCopyCommand();
			DeleteSelection();
		}

		public virtual void OnUserCopyCommand()
		{
			string toCopy = SelectionText;
			if (string.IsNullOrEmpty(toCopy))
			{
				toCopy = this.Text;
			}
			CopyText(toCopy);
		}
	}
}