using System.Windows.Forms;
using System.Drawing;
using GuiLabs.Canvas.Events;
using System.Collections.Generic;

namespace GuiLabs.Editor.UI
{
	public interface ICompletionList : ICompletionListBuilder
	{
		CompletionFunctionality CurrentList { get; set; }

		IEnumerable<CompletionListItem> Items { get; }
		// bool ShouldSortItems { get; set; }

		void SelectItem(string MatchBeginning);
		CompletionListItem SelectedItem();
		
		void StartAddingItems();
		void FinishAddingItems();

		//event CompletionListItemClickedDelegate ItemClicked;
		//event System.EventHandler VisibleChanged;

		void Show(Rectangle NearRectangle, Control ParentControl);
		void HideList();
		bool Visible { get; }

		// IKeyHandler DefaultKeyHandler { get; set; }

		string ToString();
	}
}
