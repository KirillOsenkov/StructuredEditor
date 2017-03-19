using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.UI
{
	public class TreeViewRootBlock : RootBlock
	{
		public TreeViewRootBlock()
			: base()
		{
			
		}

		protected override void InitControl(OrientationType orientation)
		{
			MyRootControl = new TreeViewRootControl(this);
			MyRootControl.LinearLayoutStrategy.Orientation = orientation;
		}

        private NodeRelationship mRelationship;
        public NodeRelationship Relationship
        {
            get { return mRelationship; }
            set 
			{
				mRelationship = value;
				this.MyRootControl.Layout();
			}
        }
	}
}
