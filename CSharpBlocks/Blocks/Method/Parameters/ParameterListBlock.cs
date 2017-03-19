using GuiLabs.Editor.Blocks;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("parameters")]
	public class ParameterListBlock : HContainerBlock, ICSharpBlock, IEnumerable<Parameter>, IReparsable
	{
		#region ctor

		public ParameterListBlock()
			: this("")
		{
		}

		public ParameterListBlock(string initialText)
			: base()
		{
			List = new ExpressionBlock(initialText);
			Init();
		}

		private void Init()
		{
			LeftBrace = new LabelBlock(OpeningBrace);
			RightBrace = new LabelBlock(ClosingBrace);
			LeftBrace.MyControl.Enabled = false;
			RightBrace.MyControl.Enabled = false;

			Children.Add(LeftBrace);
			Children.Add(List);
			Children.Add(RightBrace);

			List.CanBeDeletedByUser = false;
			List.MyTextBox.MinWidth = 2;
			List.MyControl.Box.MouseSensitivityArea.SetLeftAndRight(List.MyTextBox.StringSize(OpeningBrace).X);
		}

		#endregion

		#region Parentheses

		public LabelBlock LeftBrace { get; set; }
		public LabelBlock RightBrace { get; set; }

		private void UpdateBraces()
		{
			LeftBrace.Text = OpeningBrace;
			RightBrace.Text = ClosingBrace;
		}

		public string OpeningBrace
		{
			get
			{
				switch (TypeOfBraces)
				{
					case TypeOfParentheses.Parentheses:
						return "(";
					case TypeOfParentheses.SquareBrackets:
						return "[";
					case TypeOfParentheses.CurlyBraces:
						return "{";
					default:
						return "";
				}
			}
		}

		public string ClosingBrace
		{
			get
			{
				switch (TypeOfBraces)
				{
					case TypeOfParentheses.Parentheses:
						return ")";
					case TypeOfParentheses.SquareBrackets:
						return "]";
					case TypeOfParentheses.CurlyBraces:
						return "}";
					default:
						return "";
				}
			}
		}
		private TypeOfParentheses mTypeOfBraces = TypeOfParentheses.Parentheses;
		public TypeOfParentheses TypeOfBraces
		{
			get
			{
				return mTypeOfBraces;
			}
			set
			{
				mTypeOfBraces = value;
				UpdateBraces();
			}
		}

		public enum TypeOfParentheses
		{
			Parentheses,
			SquareBrackets,
			CurlyBraces
		}

		#endregion

		#region List

		private ExpressionBlock mList = new ExpressionBlock();
		public ExpressionBlock List
		{
			get
			{
				return mList;
			}
			set
			{
				if (mList != null)
				{
					mList.MyTextBox.PreviewKeyPress -= MyTextBox_PreviewKeyPress;
					mList.TextChanged -= MyTextBox_TextChanged;
				}
				mList = value;
				if (mList != null)
				{
					mList.MyTextBox.PreviewKeyPress += MyTextBox_PreviewKeyPress;
					mList.TextChanged += MyTextBox_TextChanged;
					mList.Context = CompletionContext.MethodParameters;
				}
			}
		}

		#endregion
		
		#region API
		
		public Parameter this[string paramName]
		{
			get
			{
				return Parameters[paramName];
			}
		}
		
		#endregion

		private ParameterList mParameters = new ParameterList();
		public ParameterList Parameters
		{
			get
			{
				return mParameters;
			}
			set
			{
				mParameters = value;
			}
		}

		#region OnEvents

		void MyTextBox_TextChanged(GuiLabs.Canvas.Controls.ITextProvider sender, string oldText, string newText)
		{
			Reparse();
		}

		void MyTextBox_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (List.MyTextBox.CaretIsAtBeginning && e.KeyChar == ')')
			{
                Block child = this.FindNextFocusableBlockInChain();
                if (child != null)
                {
                    child.SetFocus();
                }
				e.Handled = true;
			}
		}

		#endregion

		public void Reparse()
		{
			Parameters = LanguageService.ParseParameters(this.List);
		}

		#region Text

		public string Text
		{
			get
			{
				return List.Text;
			}
			set
			{
				List.Text = value;
			}
		}

		#endregion

		#region IEnumerable

		public IEnumerator<Parameter> GetEnumerator()
		{
			if (Parameters != null)
			{
				return Parameters.GetEnumerator();
			}
			else
			{
				return (new List<Parameter>()).GetEnumerator();
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
