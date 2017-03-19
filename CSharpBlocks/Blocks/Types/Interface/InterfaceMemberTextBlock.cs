using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class InterfaceMemberTextBlock : TextBoxBlockWithCompletion, ICSharpBlock
	{
		public InterfaceMemberTextBlock()
		{
			this.MyTextBox.MinWidth = 16;
		}

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
