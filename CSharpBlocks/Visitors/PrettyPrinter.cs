using System.Text;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class PrettyPrinter : BaseVisitor
	{
		#region ctor

		public PrettyPrinter()
		{
			Builder = new StringBuilder();
		}

		#endregion

		public static string Print(CodeUnitBlock block)
		{
			PrettyPrinter visitor = new PrettyPrinter();
			block.AcceptVisitor(visitor);
			return visitor.ToString();
		}

		#region StringBuilder

		public void Clear()
		{
			Builder.Remove(0, Builder.Length);
		}

		public override string ToString()
		{
			return Builder.ToString();
		}

		private StringBuilder mBuilder;
		public StringBuilder Builder
		{
			get
			{
				return mBuilder;
			}
			set
			{
				mBuilder = value;
			}
		}

		#endregion

		#region Visit

		public override void Visit(CodeUnitBlock block)
		{
			VisitContainer(block);
		}

		#region Namespace

		public override void Visit(NamespaceBlock block)
		{
			WriteIndent();
			WriteLine("namespace " + block.Name);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(EmptyNamespaceBlock block)
		{
			VisitEmptyBlock(block);
		}

		public override void Visit(UsingBlock block)
		{
			foreach (UsingDirective dir in block.VMembers.Children)
			{
				WriteIndent();
				Visit(dir);
				NewLine();
			}
		}

		public override void Visit(UsingDirective block)
		{
			if (string.IsNullOrEmpty(block.Text))
			{
				return;
			}
			string text = "using " + block.Text.TrimEnd(';', ' ') + ";";
			Write(text);
		}

		#endregion

		#region Types

		public override void Visit(ClassBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			WriteLine("class " + block.Name);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(EmptyClassMember block)
		{
			VisitEmptyBlock(block);
		}

		public override void Visit(StructBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			WriteLine("struct " + block.Name);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		#region Interface

		public override void Visit(InterfaceBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			WriteLine("interface " + block.Name);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(InterfaceMemberDeclarationBlock block)
		{
			WriteIndent();
			Visit(block.Text);
			if (block.Parameters != null)
			{
				Visit(block.Parameters);
			}
			if (block.ThisParameters != null)
			{
				Visit(block.ThisParameters);
			}
			if (block.PropertyAccessors != null)
			{
				Visit(block.PropertyAccessors);
			}
			else
			{
				if (!string.IsNullOrEmpty(block.Text.Text))
				{
					WriteSemicolon();
				}
			}
			NewLine();
		}

		public override void Visit(InterfaceMemberTextBlock block)
		{
			Write(block.Text);
		}

		public override void Visit(InterfaceAccessorsBlock block)
		{
			Write(" {");
			if (block.Getter != null)
			{
				Write(" get;");
			}
			if (block.Setter != null)
			{
				Write(" set;");
			}
			Write(" }");
		}

		#endregion

		public override void Visit(EnumBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			WriteLine("enum " + block.Name);
			StartBlock();

			foreach (ICSharpBlock b in block.VMembers.Children)
			{
				WriteIndent();
				b.AcceptVisitor(this);
				NewLine();
			}

			EndBlock();
		}

		public override void Visit(EnumValue block)
		{
			if (block.IsValue)
			{
				Write(block.Text);
				if (!block.IsLastValue)
				{
					Write(",");
				}
			}
		}

		public override void Visit(DelegateBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			Write("delegate ");
			Write(block.TypeBlock.Text);
			WriteSpace();
			Write(block.Name);
			Visit(block.Parameters);
			WriteSemicolon();
			NewLine();
		}

		#endregion

		#region Members

		public override void Visit(MethodBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			Write(block.Name);
			Visit(block.Parameters);
			NewLine();
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(ConstructorBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			Write(block.Name);
			Visit(block.Parameters);
			NewLine();
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(PropertyBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			Write(block.Name);
			NewLine();
			StartBlock();
			if (block.GetAccessor != null)
			{
				Visit(block.GetAccessor);
			}
			if (block.SetAccessor != null)
			{
				Visit(block.SetAccessor);
			}
			EndBlock();
		}

		public override void Visit(PropertyAccessorBlock block)
		{
			WriteIndent();
			WriteLine(block.Keyword.Text);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(FieldBlock block)
		{
			WriteIndent();
			Write(block.Modifiers);
			Write(block.Name.TrimEnd(';', ' '));
			WriteSemicolon();
			NewLine();
		}

		#endregion

		#region ControlStructures

		public override void Visit(ForBlock block)
		{
			WriteIndent();
			Write(block.Keyword.Text);
			Write("(");
			Write(block.ForInitializer.Text);
			Write("; ");
			Write(block.ForCondition.Text);
			Write("; ");
			Write(block.ForIncrementStep.Text);
			WriteLine(")");
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(ForeachBlock block)
		{
			WriteIndent();
			Write(block.Keyword.Text);
			Write("(");
			Write(block.IteratorType.Text);
			Write(" ");
			Write(block.IteratorName.Text);
			Write(" in ");
			Write(block.EnumeratedExpression.Text);
			WriteLine(")");
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(IfBlock block)
		{
			WriteControlStructureWithString(block, block.Condition.Text);
		}

		public override void Visit(ElseBlock block)
		{
			WriteIndent();
			Write(block.Keyword.Text);
			if (!string.IsNullOrEmpty(block.Condition))
			{
				Write(" if (");
				Write(block.Condition);
				Write(")");
			}
			NewLine();
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(WhileBlock block)
		{
			WriteControlStructureWithString(block, block.Condition.Text);
		}

		public override void Visit(DoWhileBlock block)
		{
			WriteIndent();
			WriteLine("do");
			StartBlock();
			VisitContainer(block.DoPart.VMembers);
			EndBlock();
			WriteIndent();
			Write("while (");
			Write(block.Condition.Text);
			WriteLine(");");
		}

		public override void Visit(UsingStatementBlock block)
		{
			WriteControlStructureWithString(block, block.Resource.Text.TrimEnd(' ', ';'));
		}

		public override void Visit(TryBlock block)
		{
			WriteControlStructure(block);
		}

		public override void Visit(CatchBlock block)
		{
			WriteIndent();
			Write(block.Keyword.Text);
			if (!string.IsNullOrEmpty(block.ExceptionBlock.Text))
			{
				Write("(");
				Write(block.ExceptionBlock.Text);
				Write(")");
			}
			NewLine();
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(FinallyBlock block)
		{
			WriteControlStructure(block);
		}

		public override void Visit(BreakStatement block)
		{
			WriteIndent();
			WriteLine("break;");
		}

		public override void Visit(ContinueStatement block)
		{
			WriteIndent();
			WriteLine("continue;");
		}

		public override void Visit(LockBlock block)
		{
			WriteControlStructureWithString(block, block.LockObject.Text);
		}

		public void WriteControlStructure(ControlStructureBlock block)
		{
			WriteIndent();
			WriteLine(block.Keyword.Text);
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public void WriteControlStructureWithString(ControlStructureBlock block, string title)
		{
			WriteIndent();
			Write(block.Keyword.Text);
			Write("(");
			Write(title);
			WriteLine(")");
			StartBlock();
			VisitContainer(block.VMembers);
			EndBlock();
		}

		public override void Visit(BlockStatementBlock block)
		{
			StartBlock();
			VisitContainer(block);
			EndBlock();
		}

		#endregion

		public override void Visit(CodeLine block)
		{
			if (!string.IsNullOrEmpty(block.Text))
			{
				WriteIndent();
				string text = block.Text.TrimEnd(';', ' ');
				Write(text + ";");
			}
			NewLine();
		}

		public override void Visit(ParameterListBlock block)
		{
			Write(block.OpeningBrace + block.Text + block.ClosingBrace);
		}

		private void VisitEmptyBlock(EmptyBlock block)
		{
			if (block.Prev != null && block.Next != null)
			{
				NewLine();
			}
		}

		#endregion

		#region Write

		public void WriteSpace()
		{
			Write(" ");
		}

		public void WriteSemicolon()
		{
			Write(";");
		}

		public void Write(ModifierContainer modifiers)
		{
			string mods = modifiers.ToString();
			if (!string.IsNullOrEmpty(mods))
			{
				Write(mods);
				WriteSpace();
			}
		}

		public void Write(string s)
		{
			Builder.Append(s);
		}

		public void WriteLine(string s)
		{
			Builder.AppendLine(s);
		}

		public void NewLine()
		{
			Builder.AppendLine();
		}

		public void WriteFormat(string s, params object[] parameters)
		{
			Builder.AppendFormat(s, parameters);
		}

		public void WriteIndent()
		{
			Write(CurrentIndentString);
		}

		public void Tab()
		{
			Write(IndentString);
		}

		public void StartBlock()
		{
			WriteIndent();
			WriteLine("{");
			Indent();
		}

		public void EndBlock()
		{
			UnIndent();
			WriteIndent();
			WriteLine("}");
		}

		#endregion

		#region Indent

		private string mIndentString = "\t";
		public string IndentString
		{
			get
			{
				return mIndentString;
			}
			set
			{
				mIndentString = value;
			}
		}

		private string mCurrentIndentString = "";
		public string CurrentIndentString
		{
			get
			{
				return mCurrentIndentString;
			}
			set
			{
				mCurrentIndentString = value;
			}
		}

		public string GetIndents(int timesToIndent)
		{
			int lengthOfASingleSegment = IndentString.Length;
			StringBuilder sb = new StringBuilder(timesToIndent * lengthOfASingleSegment);
			for (int i = 0; i < lengthOfASingleSegment; i++)
			{
				sb.Append(IndentString);
			}
			return sb.ToString();
		}

		public void Indent()
		{
			CurrentIndentString += IndentString;
		}

		public void UnIndent()
		{
			CurrentIndentString = CurrentIndentString.Substring(0, CurrentIndentString.Length - IndentString.Length);
		}

		#endregion
	}
}
