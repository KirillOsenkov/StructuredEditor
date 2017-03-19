using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class BlockStatementBlock : VContainerBlock, ICSharpBlock
	{
		#region AcceptVisitor

		public virtual void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
