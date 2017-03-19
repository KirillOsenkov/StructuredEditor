using System.Collections.Generic;

namespace GuiLabs.Editor.UI
{
	public class SimpleCompletionListBuilder : List<CompletionListItem>, ICompletionListBuilder
	{
		public void Add(IEnumerable<CompletionListItem> items)
		{
			AddRange(items);
		}

		public void AddText(string text)
		{
			TextCompletionItem item = new TextCompletionItem(text);
			Add(item);
		}
		
		public void AddText(string text, System.Drawing.Image icon)
		{
			TextCompletionItem item = new TextCompletionItem(text, icon);
			Add(item);
		}

		public void AddText(IEnumerable<string> textItems)
		{
			foreach (string s in textItems)
			{
				AddText(s);
			}
		}

		public void AddText(IEnumerable<TextPictureInfo> textItems)
		{
			foreach (TextPictureInfo t in textItems)
			{
				AddText(t.Text, t.Picture);
			}
		}
	}
}
