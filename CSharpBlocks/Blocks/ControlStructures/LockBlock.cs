using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	using GuiLabs.Utils;
	using GuiLabs.Canvas.DrawStyle;

	[BlockSerialization("lock")]
	public class LockBlock : ControlStructureBlock
	{
		#region ctor

		public LockBlock()
			: base("lock")
		{
			LockObject.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			LockObject.MyControl.Box.SetMouseSensitivityToMargins();
			LockObject.Context = CompletionContext.ObjectExpression;
			this.HMembers.Add(LockObject);
		}

		#endregion

		private ExpressionBlock mLockObject = new ExpressionBlock();
		public ExpressionBlock LockObject
		{
			get
			{
				return mLockObject;
			}
			set
			{
				mLockObject = value;
			}
		}

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			LockObject.Text = storage["object"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["object"] = LockObject.Text;
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
