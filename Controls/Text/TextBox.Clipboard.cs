using System.Windows.Forms;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Clipboard wrappers

		private string GetTextToInsertFromClipboard()
		{
			if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
			{
				return Clipboard.GetText(TextDataFormat.UnicodeText);
			}
			else if (Clipboard.ContainsText(TextDataFormat.Text))
			{
				return Clipboard.GetText(TextDataFormat.Text);
			}
			else
			{
				return Clipboard.GetText();
			}
		}

		private void CopyText(string textToCopyToClipboard)
		{
			if (string.IsNullOrEmpty(textToCopyToClipboard))
			{
				return;
			}
			Clipboard.SetText(textToCopyToClipboard, TextDataFormat.UnicodeText);
		}

		#endregion
	}

	public class CutCommand : Command
	{
		public CutCommand(TextBox parent)
			: base("Cut")
		{
			ParentTextBox = parent;
			this.Picture = PictureLibrary.Instance.ImageCut;
		}

		private TextBox ParentTextBox;

		public override void Click()
		{
			ParentTextBox.OnUserCutCommand();
		}
	}

	public class CopyCommand : Command
	{
		public CopyCommand(TextBox parent)
			: base("Copy")
		{
			ParentTextBox = parent;
			this.Picture = PictureLibrary.Instance.ImageCopy;
		}

		private TextBox ParentTextBox;

		public override void Click()
		{
			ParentTextBox.OnUserCopyCommand();
		}
	}

	public class PasteCommand : Command
	{
		public PasteCommand(TextBox parent)
			: base("Paste")
		{
			ParentTextBox = parent;
			this.Picture = PictureLibrary.Instance.ImagePaste;
		}

		private TextBox ParentTextBox;

		public override void Click()
		{
			ParentTextBox.OnUserPasteCommand();
		}
	}
}