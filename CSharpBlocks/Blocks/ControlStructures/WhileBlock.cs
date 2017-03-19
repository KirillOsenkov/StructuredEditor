using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	using GuiLabs.Utils;
	using GuiLabs.Canvas.DrawStyle;

	[BlockSerialization("while")]
	public class WhileBlock : ControlStructureBlock
	{
		#region ctor

		public WhileBlock()
			: base("while")
		{
            Condition = new ExpressionBlock();
			Condition.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			Condition.MyControl.Box.SetMouseSensitivityToMargins();
			Condition.Context = CompletionContext.BooleanExpression;
			this.HMembers.Add(Condition);
		}

		#endregion

		public ExpressionBlock Condition { get; set; }

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Condition.Text = storage["condition"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["condition"] = Condition.Text;
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
