using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using System.Collections.Generic;
using GuiLabs.Utils;
using GuiLabs.Editor.UI;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("foreach")]
	public class ForeachBlock : ControlStructureBlock
	{
		#region ctor

		public ForeachBlock()
			: base("foreach")
		{
			IteratorType = new ExpressionBlock();
            IteratorType.MyControl.Style = StyleFactory.Instance.GetStyle(StyleNames.TypeName);
            IteratorType.MyControl.SelectedStyle = StyleFactory.Instance.GetStyle(StyleNames.TypeNameSel);
			IteratorName = new ExpressionBlock();
			EnumeratedExpression = new ExpressionBlock();

			this.HMembers.Add(IteratorType);
			this.HMembers.Add(IteratorName);
			this.HMembers.Add(new KeywordLabel("in"));
			this.HMembers.Add(EnumeratedExpression);

			const int x = ShapeStyle.DefaultFontSize;
			const int x2 = ShapeStyle.DefaultFontSize / 2;

			InitExpr(IteratorType);
			IteratorType.MyControl.Box.Margins.SetLeftAndRight(x, x2);

			InitExpr(IteratorName);
			IteratorName.MyControl.Box.Margins.SetLeftAndRight(x2, x);

			InitExpr(EnumeratedExpression);
			EnumeratedExpression.MyControl.Box.Margins.SetLeftAndRight(x, x);

			IteratorType.ProvideHelpStrings += new StringsProvider(IteratorType_ProvideHelpStrings);
			IteratorName.ProvideHelpStrings += new StringsProvider(IteratorName_ProvideHelpStrings);
			EnumeratedExpression.ProvideHelpStrings += ProvideHelpStringsForTab;
		}

		private void InitExpr(ExpressionBlock e)
		{
			e.MyControl.Box.MouseSensitivityArea = IteratorType.MyControl.Box.Margins;
			e.MyTextBox.MinWidth = ShapeStyle.DefaultFontSize;
		}

		#endregion

		#region IteratorType

		private ExpressionBlock mIteratorType;
		public ExpressionBlock IteratorType
		{
			get { return mIteratorType; }
			private set
			{
				if (mIteratorType != null)
				{
					mIteratorType.MyTextBox.PreviewKeyPress -= IteratorType_KeyPress;
					mIteratorType.KeyDown -= IteratorType_KeyDown;
					mIteratorType.CustomItemsRequested -= FillTypes;
				}
				mIteratorType = value;
				if (mIteratorType != null)
				{
					mIteratorType.MyTextBox.PreviewKeyPress += IteratorType_KeyPress;
					mIteratorType.KeyDown += IteratorType_KeyDown;
					mIteratorType.CustomItemsRequested += FillTypes;
					mIteratorType.Context = CompletionContext.ForeachType;
				}
			}
		}
		
		public void FillTypes(CustomItemsRequestEventArgs e)
		{
			LanguageService ls = LanguageService.Get(this);
			ClassOrStructBlock parentClass = ClassNavigator.FindContainingClassOrStruct(this);
			if (ls != null)
			{
				ls.FillTypeItems(parentClass, e.Items);
			}
			e.ShouldCallFillItems = false;
			e.ShowOnlyCustomItems = true;
		}

		#endregion

		#region IteratorName

		private ExpressionBlock mIteratorName = new ExpressionBlock();
		public ExpressionBlock IteratorName
		{
			get { return mIteratorName; }
			set
			{
				if (mIteratorName != null)
				{
					mIteratorName.MyTextBox.PreviewKeyPress -= IteratorName_KeyPress;
					mIteratorName.KeyDown -= IteratorName_KeyDown;
				}
				mIteratorName = value;
				if (mIteratorName != null)
				{
					mIteratorName.MyTextBox.PreviewKeyPress += IteratorName_KeyPress;
					mIteratorName.KeyDown += IteratorName_KeyDown;
					mIteratorName.Context = null;
				}
			}
		}

		#endregion

		#region EnumeratedExpression

		private ExpressionBlock mCollectionName = new ExpressionBlock();
		public ExpressionBlock EnumeratedExpression
		{
			get { return mCollectionName; }
			set
			{
				if (mCollectionName != null)
				{
					mCollectionName.MyControl.KeyPress -= EnumeratedExpression_KeyPress;
					mCollectionName.KeyDown -= CollectionName_KeyDown;
				}
				mCollectionName = value;
				if (mCollectionName != null)
				{
					mCollectionName.MyControl.KeyPress += EnumeratedExpression_KeyPress;
					mCollectionName.KeyDown += CollectionName_KeyDown;
					mCollectionName.Context = CompletionContext.ForeachCollection;
				}
			}
		}
		
		#endregion

		#region Variable
		
		private Variable mVariable = new Variable();
		public Variable IterationVariable
		{
			get
			{
				mVariable.Name = this.IteratorName.Text;
				mVariable.Type = this.IteratorType.Text;
				return mVariable;
			}
		}

		#endregion

		#region Keyboard

		void IteratorType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				IteratorName.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				this.SetFocus();
				e.Handled = true;
			}
		}

		void IteratorName_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				EnumeratedExpression.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				IteratorType.SetCursorToTheEnd();
				e.Handled = true;
			}
		}

		void CollectionName_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				IteratorType.SetFocus();
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				IteratorName.SetCursorToTheEnd();
				e.Handled = true;
			}
		}

		void IteratorType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' '
				//&& IteratorType.MyTextBox.CaretIsAtEnd
				)
			{
				IteratorName.SetCursorToTheBeginning();
				e.Handled = true;
			}
		}

		void IteratorName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' '
				//&& IteratorName.MyTextBox.CaretIsAtEnd
				)
			{
				EnumeratedExpression.SetCursorToTheBeginning();
				e.Handled = true;
			}
		}

		void EnumeratedExpression_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{

		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			IteratorType.Text = storage["type"];
			IteratorName.Text = storage["name"];
			EnumeratedExpression.Text = storage["collection"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["type"] = IteratorType.Text;
			storage["name"] = IteratorName.Text;
			storage["collection"] = EnumeratedExpression.Text;
		}

		#endregion

		#region AcceptVisitor

		public override void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion

		#region Help

		private static string PressHomeOrLeft = "Press [Home] or [LeftArrow] to select the foreach block.";
		private static string PressHome = "Press [Home] to select the foreach block.";
		private static string PressEnter = "Press [Enter] to jump to the body of the foreach block.";

		IEnumerable<string> IteratorType_ProvideHelpStrings()
		{
			yield return "Press [Tab] or [Space] to jump to the iteration variable name.";
			yield return PressHomeOrLeft;
			yield return PressEnter;
		}

		IEnumerable<string> IteratorName_ProvideHelpStrings()
		{
			yield return "Press [Tab] or [Space] to jump to the collection to iterate over.";
			yield return PressHome;
			yield return PressEnter;
		}

		IEnumerable<string> ProvideHelpStringsForTab()
		{
			yield return "Press [Tab] to jump to the type of an iteration.";
			yield return PressHome;
			yield return PressEnter;
		}

		#endregion
	}
}
