using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("struct")]
	public class StructBlock : ClassOrStructBlock
	{
		#region ctor

		public StructBlock()
			: base("struct")
		{

		}

		#endregion

		#region Create

		public static StructBlock Create(string structName, string structModifiers)
		{
			StructBlock newStruct = new StructBlock();
			newStruct.Name = structName;
			newStruct.Modifiers.SetMany(structModifiers);
			return newStruct;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "StructBlock";
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
