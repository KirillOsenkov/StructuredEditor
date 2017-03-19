using GuiLabs.Editor.Blocks;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Actions
{
	public enum SetFocusOptions
	{
		General,
		ToBeginning,
		ToEnd
	}

	public class SetFocusAction : AbstractAction
	{
		#region ctors

		public SetFocusAction(Block toFocus, SetFocusOptions options)
			: base()
		{
			Options = options;
			ToFocus = toFocus;
		}

		protected SetFocusOptions Options;
		protected Block ToFocus;

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
		}

		protected override void UnExecuteCore()
		{
			
		}
	}
}