using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("finally")]
	public class FinallyBlock : ControlStructureBlock
	{
		#region ctor

		public FinallyBlock()
			: base("finally")
		{
		}

		#endregion

		#region AcceptVisitor

		public override void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
