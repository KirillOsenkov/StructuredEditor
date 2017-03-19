using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("catch")]
	public class CatchBlock : ControlStructureBlock
	{
		#region ctor

		public CatchBlock()
			: base("catch")
		{
            ExceptionBlock = new ExpressionBlock();
			ExceptionBlock.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			ExceptionBlock.MyControl.Box.SetMouseSensitivityToMargins();
			ExceptionBlock.Context = CompletionContext.ObjectExpression;
			this.HMembers.Add(ExceptionBlock);
		}

		#endregion

		public ExpressionBlock ExceptionBlock { get; set; }

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			ExceptionBlock.Text = storage["exception"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["exception"] = ExceptionBlock.Text;
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
