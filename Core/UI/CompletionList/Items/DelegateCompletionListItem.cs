using GuiLabs.Utils.Delegates;

namespace GuiLabs.Editor.UI
{
	public class DelegateCompletionListItem : CompletionListItem
	{
		public DelegateCompletionListItem(string text, EmptyHandler func)
			: base(text)
		{
			this.Function = func;
		}

		private EmptyHandler Function;

		public override void Click(CompletionFunctionality hostItemList)
		{
			if (Function != null)
			{
				Function();
			}
		}
	}
}
