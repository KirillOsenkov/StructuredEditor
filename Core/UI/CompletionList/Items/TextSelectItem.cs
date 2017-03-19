using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.UI
{
	public class TextSelectItem : CompletionListItem
	{
		public TextSelectItem(string text)
			: base(text)
		{
		}

		public override void Click(CompletionFunctionality hostItemList)
		{
			IHasText host = hostItemList.HostBlock as IHasText;
			if (host != null)
			{
				host.Text = this.Text;
			}
		}
	}
}
