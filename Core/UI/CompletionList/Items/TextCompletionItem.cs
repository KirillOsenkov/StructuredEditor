using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.UI
{
	public class TextCompletionItem : CompletionListItemWithVisibilityConditions
	{
		public TextCompletionItem(string text)
			: base(text)
		{
		}

		public TextCompletionItem(string text, System.Drawing.Image picture)
			: this(text)
		{
			this.Picture = picture;
		}

		private TextBoxBlock Parent;

		public override void Click(CompletionFunctionality hostItemList)
		{
			Parent = hostItemList.HostBlock as TextBoxBlock;
			Param.CheckNotNull(Parent, "Parent");
			
			int toDel = NumOfCharsToDelete();
			if (toDel > 0)
			{
				Parent.MyTextBox.CaretPosition -= toDel;
				Parent.MyTextBox.InsertText(this.Text);
				return;
			}
			Parent.MyTextBox.InsertText(GetSuffixToAdd());
		}

		public int NumOfCharsToDelete()
		{
			string lastWord = Strings.GetLastWord(Parent.MyTextBox.TextBeforeCaret);
			string insertionText = this.Text;

			if (!insertionText.StartsWith(lastWord, System.StringComparison.InvariantCultureIgnoreCase))
			{
				return lastWord.Length;
			}
			return 0;
		}

//		public string GetSuffixToAddOld()
//		{
//			int i = Strings.WordsOverlapLength(Parent.MyTextBox.Text, this.Text, Strings.EqualIgnoreCase);
//			return this.Text.Substring(i);
//		}

		public string GetSuffixToAdd()
		{
			string lastWord = Strings.GetLastWord(Parent.MyTextBox.TextBeforeCaret);
			string insertionText = this.Text;

			if (lastWord.Length > 0
				&& lastWord.Length < insertionText.Length
				&& Strings.EqualIgnoreCase(
					insertionText.Substring(0, lastWord.Length),
					lastWord)
			)
			{
				string textToInsert = insertionText.Substring(lastWord.Length);
				return textToInsert;
			}
			else if (Strings.EqualIgnoreCase(lastWord, insertionText))
			{
				return "";
			}
			else
			{
				return insertionText;
			}
		}
	}
}
