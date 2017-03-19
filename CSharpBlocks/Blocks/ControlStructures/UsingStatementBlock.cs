using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("usingStatement")]
	public class UsingStatementBlock : ControlStructureBlock
	{
		#region ctor

		public UsingStatementBlock()
			: base("using")
		{
			Resource.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			Resource.MyControl.Box.SetMouseSensitivityToMargins();
			Resource.Context = CompletionContext.ObjectExpression;
			this.HMembers.Add(Resource);
		}

		#endregion

		#region Resource

		private ExpressionBlock mResource = new ExpressionBlock();
		public ExpressionBlock Resource
		{
			get
			{
				return mResource;
			}
			set
			{
				mResource = value;
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Resource.Text = storage["resource"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["resource"] = Resource.Text;
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
