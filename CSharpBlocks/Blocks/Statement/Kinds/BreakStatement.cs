using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("break")]
	public class BreakStatement : OneWordStatement, ICSharpBlock, IMethodLevel
	{
		#region ctor

		public BreakStatement() : base("break")
		{
			CanAppendBlocks = false;
		}

		#endregion

		#region AcceptVisitor

		public virtual void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}