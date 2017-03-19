using System.Collections.Generic;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.UI
{
	public interface ICompletionListItems :
		ICompletionListBuilder,
		IEnumerable<CompletionListItem>,
		IClearable
	{
		//TextboxWithCompletion ParentTextbox { get; set;}
		//LabelWithCompletion ParentLabel { get; set;}
		CompletionListItem FindFirstItemWithPrefix(string prefix);
		bool ShouldSortItems { get; set; }
		bool AllowDuplicateStrings { get; set; }
	}
}
