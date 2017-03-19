using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("if")]
	public class IfBlock : ControlStructureBlock
	{
		#region ctor

		public IfBlock()
			: base("if")
		{
			Condition.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			Condition.MyControl.Box.MouseSensitivityArea = Condition.MyControl.Box.Margins;
			Condition.Context = CompletionContext.BooleanExpression;
			this.HMembers.Add(Condition);
		}

		#endregion

		private ExpressionBlock mCondition = new ExpressionBlock();
		public ExpressionBlock Condition
		{
			get
			{
				return mCondition;
			}
			set
			{
				mCondition = value;
			}
		}

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
