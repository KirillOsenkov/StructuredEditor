using GuiLabs.Utils;
namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		public int FindPrevWhitespacePosition()
		{
			int i = CaretPosition;
			while (i > 0 && IsSeparator(DynamicString[i - 1]))
			{
				i--;
			}
			while (i > 0 && !IsSeparator(DynamicString[i - 1]))
			{
				i--;
			}
			return i;
		}

		public int FindNextWhitespacePosition()
		{
			int i = CaretPosition;
			while (i < TextLength && !IsSeparator(DynamicString[i]))
			{
				i++;
			}
			while (i < TextLength && IsSeparator(DynamicString[i]))
			{
				i++;
			}
			return i;
		}

		public string GetWordBeforeCaret()
		{
			return Strings.GetLastWord(Text);
			//int i = CaretPosition;
			//for (; i > 0 && Strings.IsAlphaNumeric(DynamicString[i - 1]); i--);
			
			//return Text.Substring(i, CaretPosition - 1);
		}

		private bool IsSeparator(char c)
		{
			return
				char.IsWhiteSpace(c)
				|| char.IsPunctuation(c)
				|| char.IsSeparator(c);
		}

		private int GoToLeft(bool ShouldJumpWord)
		{
			if (ShouldJumpWord)
			{
				return FindPrevWhitespacePosition();
			}
			else
			{
				return CaretPosition > 0 ? CaretPosition - 1 : 0;
			}
		}

		private int GoToRight(bool ShouldJumpWord)
		{
			if (ShouldJumpWord)
			{
				return FindNextWhitespacePosition();
			}
			else
			{
				return CaretPosition < TextLength ? CaretPosition + 1 : TextLength;
			}
		}
	}
}