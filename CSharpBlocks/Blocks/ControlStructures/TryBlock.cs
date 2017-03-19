using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("try")]
	public class TryBlock : ControlStructureBlock
	{
		#region ctor

		public TryBlock()
			: base("try")
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
