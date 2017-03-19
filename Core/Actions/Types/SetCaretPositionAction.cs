using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Actions
{
	public class SetCaretPositionAction : SetFocusAction
	{
		#region ctors

		public SetCaretPositionAction(
			RootBlock root, 
			TextBoxBlock toFocus, 
			SetFocusOptions options,
			int caretPosition)
			: base(toFocus, options)
		{
			CaretPosition = caretPosition;
			Text = toFocus;
		}

		TextBoxBlock Text;
		int CaretPosition;

		#endregion

		protected override void ExecuteCore()
		{
			if (ToFocus == null || ToFocus.Root == null)
			{
				return;
			}

			switch (Options)
			{
				case SetFocusOptions.ToBeginning:
					ToFocus.SetCursorToTheBeginning();
					break;
				case SetFocusOptions.ToEnd:
					ToFocus.SetCursorToTheEnd();
					break;
				default:
					ToFocus.SetFocus();
					break;
			}

			Text.MyTextBox.SetCaretPosition(CaretPosition);
		}
	}
}