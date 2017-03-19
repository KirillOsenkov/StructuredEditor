using GuiLabs.Utils;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("class")]
	public class ClassBlock : ClassOrStructBlock
	{
		#region ctor

		public ClassBlock()
			: base("class")
		{
		}

		#endregion

		#region Create

		public static ClassBlock Create(string className, string classModifiers)
		{
			ClassBlock newClass = new ClassBlock();
			newClass.Name = className;
			newClass.Modifiers.SetMany(classModifiers);
			return newClass;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "ClassBlock";
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
