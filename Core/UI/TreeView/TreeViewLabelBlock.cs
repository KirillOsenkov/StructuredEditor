using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class TreeViewLabelBlock : LabelBlock
	{
		public TreeViewLabelBlock()
			: base()
		{

		}

		public TreeViewLabelBlock(string text)
			: base(text)
		{

		}

		#region Style

		protected override string StyleName()
		{
			return "TreeViewLabelBlock";
		}

		#endregion
	}
}
