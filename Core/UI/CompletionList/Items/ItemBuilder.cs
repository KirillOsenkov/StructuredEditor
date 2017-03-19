using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class ItemBuilder
	{
		#region ctor
		
		public ItemBuilder(ICompletionListBuilder items)
		{
			Items = items;
		}
		
		protected ICompletionListBuilder Items;

		#endregion
		
		public void Add(CompletionListItem item)
		{
			Items.Add(item);
		}

		#region TextCompletion

		public TextCompletionItem AddText(string text)
		{
			TextCompletionItem result = new TextCompletionItem(text);
			Add(result);
			return result;
		}

		public TextCompletionItem AddText(string text, System.Drawing.Image picture)
		{
			TextCompletionItem result = new TextCompletionItem(text, picture);
			Add(result);
			return result;
		}

		#endregion
	}
}
