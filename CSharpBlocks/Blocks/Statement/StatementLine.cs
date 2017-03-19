using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("statement")]
	public class StatementLine : ExpressionBlock, IReparsable, IMethodLevel
	{
		#region ctors

		public StatementLine()
			: base()
		{
			this.Multiline = true;
			this.Draggable = true;
			Context = CompletionContext.Statement;
		}

		public StatementLine(string initialText)
			: this()
		{
			this.Text = initialText;
			Reparse();
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline 
				&& (e.Control 
					|| (this.Text.StartsWith("return")
						&& this.MyTextBox.CaretIsAtEnd))
				&& CtrlEnter(LastCtrlReturn))
			{
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDownReturn(e);
			}
		}

        protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '/')
            {
                this.Replace(new CommentBlock());
                e.Handled = true;
                return;
            }

            base.OnKeyPress(sender, e);
        }

		protected override void OnRootChanged(RootBlock oldRoot, RootBlock newRoot)
		{
			base.OnRootChanged(oldRoot, newRoot);
			if (newRoot != null)
			{
				Reparse();
			}
		}

		protected override void OnTextHasChanged(ITextProvider changedControl, string oldText, string newText)
		{
			base.OnTextHasChanged(changedControl, oldText, newText);
			Reparse();
		}

		#endregion

		#region DragDrop

		public override bool Draggable
		{
			get
			{
				if (this.Prev == null && this.Next == null)
				{
					return false;
				}
				return base.Draggable;
			}
			set
			{
				base.Draggable = value;
			}
		}

		#endregion

		#region Delete

		public override void Delete()
		{
			if (this.Prev == null && this.Next == null)
			{
				this.Text = "";
			}
			else
			{
				base.Delete();
			}
		}

		#endregion

		#region Statement completion

		protected override void FillItems(CustomItemsRequestEventArgs e)
		{
			LanguageService.GetCompletion(this, e.Items, this.Context);
			AddStatementItems(e.Items);
			e.ShowOnlyCustomItems = true;
		}

		#region Statements

		private void AddStatementItems(ICompletionListBuilder items)
		{
			if (this.Text.Length <= 1 && this.Context == CompletionContext.Statement)
			{
				CompletionAddStatements(items);
			}
		}

		public virtual void CompletionAddStatements(ICompletionListBuilder items)
		{
			items.AddText("return", Icons.Keyword);
			items.AddText("throw", Icons.Keyword);
			CompletionAddControlStructures(items);
		}

		public virtual void CompletionAddControlStructures(ICompletionListBuilder items)
		{
			AddControlStructure<IfBlock>("if", items);
			if (this.Prev is IfBlock || this.Prev is ElseBlock)
			{
				AddControlStructure<ElseBlock>("else", items);
			}
			AddControlStructure<WhileBlock>("while", items);
			AddControlStructure<DoWhileBlock>("do", items);
			AddControlStructure<ForBlock>("for", items);
			AddControlStructure<ForeachBlock>("foreach", items);
			AddControlStructure<LockBlock>("lock", items);
			AddControlStructure<TryBlock>("try", items);
			if (this.Prev is TryBlock || this.Prev is CatchBlock)
			{
				AddControlStructure<CatchBlock>("catch", items);
				AddControlStructure<FinallyBlock>("finally", items);
			}
			AddControlStructure<UsingStatementBlock>("using", items);
			if (this.Next == null && ClassNavigator.FindContainingControlStructure(this) != null)
			{
				AddControlStructure<BreakStatement>("break", items);
				AddControlStructure<ContinueStatement>("continue", items);
			}
		}

		public virtual void AddControlStructure<T>(string text, ICompletionListBuilder items)
		{
			ReplaceBlocksItem item = ReplaceBlocksItem.Create<T>(text);
			item.Picture = Icons.Keyword;
			items.Add(item);
		}

		#endregion

		#endregion

		#region Local variable declaration

		public void Reparse()
		{
			LanguageService ls = LanguageService.Get(this);
			if (ls != null && ls.Parser != null)
			{
				StatementInfo = ls.Parser.ParseStatement(this.Text);
			}
			else
			{
				StatementInfo = null;
			}
		}

		private IStatement mStatementInfo;
		public IStatement StatementInfo
		{
			get
			{
				return mStatementInfo;
			}
			set
			{
				mStatementInfo = value;
			}
		}

		public VariableDeclaration LocalVariableDeclaration
		{
			get
			{
				return StatementInfo != null ? StatementInfo.LocalVariableDeclaration : null;
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "StatementLine";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"This is a statement line."
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion
	}
}
