using System.ComponentModel;

namespace GuiLabs.Editor.UI
{
	public partial class TreeViewControl : ViewWindow
	{
		public TreeViewControl()
			: base()
		{
			this.Root = new TreeViewRootBlock();
			this.RootBlock = Root;
		}

		private TreeViewRootBlock mRoot;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeViewRootBlock Root
		{
			get
			{
				return mRoot;
			}
			protected set
			{
				mRoot = value;
			}
		}

		public TreeViewNode AddNode(string Caption)
		{
			TreeViewNode childToAdd = new TreeViewNode(Caption);
			this.Root.Children.Add(childToAdd);
			return childToAdd;
		}
	}
}
