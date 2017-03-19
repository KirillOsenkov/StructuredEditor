using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	using Line = ExpressionBlock;
	using GuiLabs.Utils;
	using System.Drawing;
	using GuiLabs.Canvas.DrawStyle;

	[BlockSerialization("for")]
	public class ForBlock : ControlStructureBlock, ICSharpBlock
	{
		#region ctor

		public ForBlock() : base("for")
		{
			ForInitializer = new StatementLine();
			ForCondition = new ExpressionBlock();
			ForIncrementStep = new ExpressionBlock();

			const int c = ShapeStyle.DefaultFontSize;

			ForInitializer.MyControl.Box.Margins.Left = c;
			ForInitializer.MyControl.Box.Margins.SetTopAndBottom(0);
			ForInitializer.MyControl.Box.MouseSensitivityArea.SetLeftAndRight(c);
			ForInitializer.Draggable = false;
			InitField(ForInitializer);

			ForCondition.MyControl.Box.Margins.Left = c;
			ForCondition.MyControl.Box.MouseSensitivityArea.SetLeftAndRight(c);
			InitField(ForCondition);

			ForIncrementStep.MyControl.Box.Margins.SetLeftAndRight(c);
			ForIncrementStep.MyControl.Box.MouseSensitivityArea.SetLeftAndRight(c);
			InitField(ForIncrementStep);

			LabelBlock firstSep = new LabelBlock(";");
			firstSep.MyControl.Enabled = false;
			LabelBlock secondSep = new LabelBlock(";");
			secondSep.MyControl.Enabled = false;

			this.HMembers.Add(ForInitializer);
			this.HMembers.Add(firstSep);
			this.HMembers.Add(ForCondition);
			this.HMembers.Add(secondSep);
			this.HMembers.Add(ForIncrementStep);
		}

		private void InitField(TextBoxBlock field)
		{
			field.MyTextBox.MinWidth = 16;
		}

		#endregion

		#region OnEvents

		void mForInitializer_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				ForCondition.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				this.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Delete && ForInitializer.MyTextBox.CaretIsAtEnd)
			{
				ForCondition.SetCursorToTheBeginning();
				e.Handled = true;
			}
		}

		void mForCondition_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				ForIncrementStep.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				ForInitializer.SetCursorToTheEnd();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Delete && ForCondition.MyTextBox.CaretIsAtEnd)
			{
				ForIncrementStep.SetCursorToTheBeginning();
				e.Handled = true;
			}
		}

		void mForIncrementStep_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				ForInitializer.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				ForCondition.SetCursorToTheEnd();
				e.Handled = true;
			}
		}

		#endregion

		#region Initializer

		private StatementLine mForInitializer;
		public StatementLine ForInitializer
		{
			get
			{
				return mForInitializer;
			}
			set
			{
				if (mForInitializer != null)
				{
					mForInitializer.KeyDown -= mForInitializer_KeyDown;
					mForInitializer.MyTextBox.PreviewKeyPress -= ForInitializer_PreviewKeyPress;
				}
				mForInitializer = value;
				if (mForInitializer != null)
				{
					mForInitializer.KeyDown += mForInitializer_KeyDown;
					mForInitializer.MyTextBox.PreviewKeyPress += ForInitializer_PreviewKeyPress;
					mForInitializer.Multiline = false;
					mForInitializer.Context = CompletionContext.ForInitializer;
				}
			}
		}

		void ForInitializer_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ';')
			{
				ForCondition.SetCursorToTheBeginning();
				e.Handled = true;
			}
			if (e.KeyChar == ' ' && ForInitializer.MyTextBox.CaretIsAtBeginning)
			{
				e.Handled = true;
			}
		}

		#endregion

		#region Condition

		private Line mForCondition;
		public Line ForCondition
		{
			get
			{
				return mForCondition;
			}
			set
			{
				if (mForCondition != null)
				{
					mForCondition.KeyDown -= mForCondition_KeyDown;
					mForCondition.MyTextBox.PreviewKeyPress -= ForCondition_PreviewKeyPress;
				}
				mForCondition = value;
				if (mForCondition != null)
				{
					mForCondition.KeyDown += mForCondition_KeyDown;
					mForCondition.MyTextBox.PreviewKeyPress += ForCondition_PreviewKeyPress;
					mForCondition.Context = CompletionContext.ForCondition;
				}
			}
		}

		void ForCondition_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ';')
			{
				ForIncrementStep.SetCursorToTheBeginning();
				e.Handled = true;
			}
			if (e.KeyChar == ' ' && ForCondition.MyTextBox.CaretIsAtBeginning)
			{
				e.Handled = true;
			}
		}

		#endregion

		#region IncrementStep

		private Line mForIncrementStep;
		public Line ForIncrementStep
		{
			get
			{
				return mForIncrementStep;
			}
			set
			{
				if (mForIncrementStep != null)
				{
					mForIncrementStep.KeyDown -= mForIncrementStep_KeyDown;
					mForIncrementStep.MyTextBox.PreviewKeyPress -= ForIncrementStep_PreviewKeyPress;
				}
				mForIncrementStep = value;
				if (mForIncrementStep != null)
				{
					mForIncrementStep.KeyDown += mForIncrementStep_KeyDown;
					mForIncrementStep.MyTextBox.PreviewKeyPress += ForIncrementStep_PreviewKeyPress;
					mForIncrementStep.Context = CompletionContext.ForIncrementStep;
				}
			}
		}

		void ForIncrementStep_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ';')
			{
				e.Handled = true;
			}
			if (e.KeyChar == ' ' && ForIncrementStep.MyTextBox.CaretIsAtBeginning)
			{
				e.Handled = true;
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			ForInitializer.Text = storage["initializer"];
			ForCondition.Text = storage["condition"];
			ForIncrementStep.Text = storage["increment"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["initializer"] = ForInitializer.Text;
			storage["condition"] = ForCondition.Text;
			storage["increment"] = ForIncrementStep.Text;
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
