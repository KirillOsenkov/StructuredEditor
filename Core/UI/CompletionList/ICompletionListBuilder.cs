using System.Collections.Generic;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.UI
{
	public interface ICompletionListBuilder : IFillable<CompletionListItem>
	{
		void AddText(string text);
		void AddText(string text, System.Drawing.Image icon);
		void AddText(IEnumerable<string> textItems);
		void AddText(IEnumerable<TextPictureInfo> textItems);
		int Count { get; }
	}
}
