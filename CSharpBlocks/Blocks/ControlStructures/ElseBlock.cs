using GuiLabs.Editor.Blocks;
using System.Windows.Forms;

namespace GuiLabs.Editor.CSharp
{
	using Line = TextBoxBlock;
	using GuiLabs.Utils;
	using GuiLabs.Canvas.DrawStyle;

	[BlockSerialization("else")]
	public class ElseBlock : ControlStructureBlock
	{
		#region ctor

		public ElseBlock()
			: base("else")
		{
			ElseIfSection = new IfPart(this);
			EmptyIf.MyTextBox.PreviewKeyPress += delegate(object sender, KeyPressEventArgs e)
			{
				if (char.IsLetter(e.KeyChar))
				{
					EmptyIf.Replace(ElseIfSection);
					e.Handled = true;
				}
			};
			EmptyIf.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			EmptyIf.MyControl.Box.SetMouseSensitivityToMargins();
			this.HMembers.Add(EmptyIf);
		}

		#endregion
		
		#region elseif section
		
		private TextBoxBlock mEmptyIf = new TextBoxBlock();
		public TextBoxBlock EmptyIf
		{
			get
			{
				return mEmptyIf;
			}
		}
		
		public IfPart ElseIfSection { get; set; }

		#endregion
		
		public string Condition
		{
			get
			{
				if (ElseIfSection.Parent != null)
				{
					return ElseIfSection.Condition.Text;
				}
				return "";
			}
			set
			{
				ElseIfSection.Condition.Text = value;
				if (ElseIfSection.Parent == null)
				{
					EmptyIf.Replace(ElseIfSection);
				}
			}
		}

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Condition = storage["condition"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["condition"] = Condition;
		}

		#endregion

		#region AcceptVisitor

		public override void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
	
	public class IfPart : HContainerBlock
	{
		#region ctor
		
		public IfPart(ElseBlock parent)
			: base()
		{
			ParentElse = parent;
			this.MyControl.Focusable = true;
			this.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			this.MyControl.Box.SetMouseSensitivityToMargins();
			this.MyControl.Style = parent.MyControl.Style;
			this.MyControl.SelectedStyle = parent.MyControl.SelectedStyle;
			this.MyControl.ShouldDrawBackground = false;
			Condition.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			Condition.MyControl.Box.SetMouseSensitivityToMargins();
			Condition.MyControl.KeyDown += delegate(object sender, KeyEventArgs e) 
			{
				if (e.KeyCode == Keys.Back && string.IsNullOrEmpty(Condition.Text))
				{
					this.Delete();
                    e.Handled = true;
				}
			};
			this.Add(Keyword, Condition);
		}
		
		#endregion
		
		private ElseBlock ParentElse;
		
		private KeywordLabel Keyword = new KeywordLabel("if");

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
		
		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			return Condition.DefaultFocusableControl();
		}
		
		public override void Delete()
		{
			this.Replace(ParentElse.EmptyIf);
		}
	}
}
