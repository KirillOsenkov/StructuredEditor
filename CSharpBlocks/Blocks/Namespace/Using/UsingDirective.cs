using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("directive")]
	public class UsingDirective : DraggableTextLine, ICSharpBlock
	{
		#region ctors

		public UsingDirective()
			: base()
		{
			Init();
		}

		public UsingDirective(string text)
			: base(text)
		{
			Init();
		}

		private void Init()
		{
			this.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			this.MyControl.Box.SetMouseSensitivityToMargins();
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDownTab(System.Windows.Forms.KeyEventArgs e)
		{
			if (CanUnIndent())
			{
				UnIndent();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDownTab(e);
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (this.Text == e.KeyChar.ToString() && Strings.CanBeStartOfVariableName(e.KeyChar))
			{
				FillCompletionWithNamespaces(GetNamespaces(""));
				Completion.ShowCompletionList(this, this.Text);
			}
			else if (e.KeyChar == '.')
			{
				FillNamespaceCompletion();
				Completion.ShowCompletionList(this);
			}
			else if (Strings.CanBePartOfVariableName(e.KeyChar))
			{
				if (CompletionIsShown)
				{
					Completion.ShowCompletionList(this, this.GetWordBeforeCaret());
				}
			}
		}

		protected override void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
			if (CanUnIndent())
			{
				UnIndent();
				e.Handled = true;
				return;
			}

			if (e.Control
				&& ParentParent != null
				&& ParentParent.Next != null)
			{
				ParentParent.Next.SetFocus();
				e.Handled = true;
			}
			else
			{
				base.OnKeyDownReturn(e);
			}
		}

		protected override void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
			if (CanUnIndent())
			{
				UnIndent();
				e.Handled = true;
			}
			else if (string.IsNullOrEmpty(this.Text)
				&& this.Prev == null
				&& this.Next == null)
			{
				this.ParentParent.Delete();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDownBack(e);
			}
		}

		protected override void OnDeactivated(Control control)
		{
			base.OnDeactivated(control);
			RecacheCompletion();
		}
		
		#endregion

		public void RecacheCompletion()
		{
			LanguageService service = LanguageService.Get(this);
			if (service != null)
			{
				service.CacheUsing(this);
			}
		}
		
		public IList<object> Cache { get; set; }

		#region Completion

		public void FillNamespaceCompletion()
		{
			FillCompletionWithNamespaces(GetNamespaces());
		}

		public void FillCompletionWithNamespaces(IEnumerable<string> namespaceNames)
		{
			Completion.Items.Clear();
			foreach (string s in namespaceNames)
			{
				Completion.AddTextCompletionItem(s, Icons.Namespace);
			}
		}

		public IEnumerable<string> GetNamespaces()
		{
			return GetNamespaces(GetNamespaceLeftFromCaret());
		}
		
		public IEnumerable<string> GetNamespaces(string ns)
		{
			LanguageService service = LanguageService.Get(this);
			if (service != null)
			{
				return service.GetNamespaces(ns);
			}
			return Strings.EmptyArray;
		}

		#endregion

		#region API

		public string GetNamespaceLeftFromCaret()
		{
			string result = this.MyTextBox.TextBeforeCaret;
			result = result.Trim().TrimEnd('.', ';');
			return result;
		}

		public string Name
		{
			get
			{
				int equalsPos = Text.IndexOf('=');
				if (equalsPos > -1 && equalsPos < Text.Length - 1)
				{
					return Text.Substring(equalsPos + 1).Trim();
				}
				return Text;
			}
		}

		public string Alias
		{
			get
			{
				int equalsPos = Text.IndexOf('=');
				if (equalsPos > -1 && equalsPos < Text.Length - 1)
				{
					return Text.Substring(0, equalsPos).Trim();
				}
				return null;
			}
		}

		#endregion

		#region Indent

		protected bool CanUnIndent()
		{
			return this.Prev != null && this.Next == null && this.Text == "";
		}

		protected void UnIndent()
		{
			using (Redrawer r = new Redrawer(this.Root))
			{
				this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
				this.Delete();
			}
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"This is a using directive.",
			"Using directives are placed inside a single 'using' container.",
			"A semicolon at the end of the line is not required."
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				if (this.MyTextBox.CaretIsAtBeginning)
				{
					if (this.Next != null)
					{
						yield return "Press [Enter] to insert a new using directive before current line.";
					}
					if (this.Prev == null)
					{
						yield return "Press [Home] or [LeftArrow] to select the whole using block.";
					}
					else
					{
						yield return "Press [Home] to select the whole using block.";
						yield return "Press [LeftArrow] to jump to the end of the previous using directive.";
					}
				}
				if ((this.Next != null || this.Text.Length > 0) && this.MyTextBox.CaretIsAtEnd)
				{
					yield return "Press [Enter] to insert a new using directive after the current line.";
				}
				if (this.Prev != null && this.Next == null && string.IsNullOrEmpty(this.Text))
				{
					yield return "Press [Enter] to exit the using block and move to the next block.";
				}
				if (this.MyTextBox.CaretIsAtEnd && this.Next == null)
				{
					yield return "Press [Ctrl+Enter], [DownArrow] or [RightArrow] to exit the using block and move to the next block.";
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
		protected override IEnumerable<string> GetCompletionHelp()
		{
			if (Completion.Visible)
			{
				yield return HelpBase.CommitCompletion;
				yield return HelpBase.CancelCompletion;
			}
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
