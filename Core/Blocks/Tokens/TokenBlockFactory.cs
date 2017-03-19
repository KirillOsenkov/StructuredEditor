using GuiLabs.Utils;
namespace GuiLabs.Editor.Blocks
{
	public class TokenBlockFactory
	{
		public virtual TokenLineBlock CreateNewLine()
		{
			return new TokenLineBlock();
		}

		public virtual TokenSeparatorBlock CreateNewSeparator()
		{
			return new TokenSeparatorBlock();
		}

		public virtual TokenBlock CreateNewToken()
		{
			return new TokenBlock();
		}

		public virtual TokenBlock CreateNewTextCompletionToken()
		{
			return new TokenCompletionBlock();
		}

		public TokenBlock CreateNewTextToken(string text, int cursorPosition)
		{
			TokenBlock newToken = CreateNewToken();
			newToken.SetText(text, ActionOptions.NoRedrawNoUndo);
			newToken.MyTextBox.SetCaretPosition(cursorPosition);
			return newToken;
		}
	}
}
