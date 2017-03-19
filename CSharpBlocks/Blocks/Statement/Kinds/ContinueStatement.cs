using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("continue")]
	public class ContinueStatement : OneWordStatement, ICSharpBlock, IMethodLevel
	{
		#region ctor

		public ContinueStatement()
			: base("continue")
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
