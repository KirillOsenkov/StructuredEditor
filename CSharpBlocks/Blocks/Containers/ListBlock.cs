using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class ListBlock : CodeBlock
	{
		#region ctor

		public ListBlock()
		{
			this.MyUniversalControl.HCompartment.Remove(this.MyUniversalControl.MyCollapseButton);
			this.MyUniversalControl.Add(this.MyUniversalControl.MyCollapseButton);
			this.MyUniversalControl.LinearLayoutStrategy.Orientation = GuiLabs.Canvas.Controls.OrientationType.Horizontal;
			this.MyUniversalControl.VMembers.Box.Margins.Left = 0;
			this.MyUniversalControl.HMembers.Stretch = GuiLabs.Canvas.Controls.StretchMode.None;
			this.MyUniversalControl.Layout();
		}

		#endregion

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			Block head = this.VMembers.Children.Head;
			if (head != null)
			{
				return head.MyControl;
			}
			else
			{
				return this.MyUniversalControl;
			}
		}
	}
}
