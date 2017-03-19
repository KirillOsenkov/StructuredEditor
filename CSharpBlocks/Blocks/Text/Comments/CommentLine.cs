using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public class CommentLine : TextLine, ICSharpBlock
	{
		#region ctors

		public CommentLine()
			: base()
		{
            this.MyTextBox.Stretch = StretchMode.None;
            this.MyTextBox.MinWidth = 40;
		}

        public CommentLine(string initialText)
			: this()
		{
			this.Text = initialText;
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline 
				&& e.KeyCode == System.Windows.Forms.Keys.Insert
				&& CtrlEnter(false))
			{
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		/// <summary>
		/// Indent / unindent (only for multiline)
		/// </summary>
		protected override void OnKeyDownTab(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline)
			{
				if (e.Shift)
				{
					if (UnIndent())
					{
						e.Handled = true;
						return;
					}
				}
				else
				{
					if (Indent())
					{
						e.Handled = true;
						return;
					}
				}
			}
			if (!e.Handled)
			{
				base.OnKeyDownTab(e);
			}
		}

		/// <summary>
		/// Indent / unindent (only for multiline)
		/// </summary>
		protected override void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline && e.Control && CtrlEnter(LastCtrlReturn))
			{
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDownReturn(e);
			}
		}

		protected override void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
			if (Multiline && e.Control && UnIndent())
			{
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDownBack(e);
			}
		}

		#endregion

		#region Multiline API

		#region CtrlEnter

		protected bool CtrlEnter(bool shouldDeleteCurrent)
		{
			Block ParentParentNext = ParentParent != null ? ParentParent.Next : null;
			TextBoxBlock NextText = ParentParentNext as TextBoxBlock;
			shouldDeleteCurrent = shouldDeleteCurrent && this.Text == "" && this.Prev != null;

			if (this.Next != null || ParentParent == null)
			{
				return false;
			}

			if (NextText != null && NextText.Text == "")
			{
				using (Redrawer r = new Redrawer(this.Root))
				{
					NextText.SetFocus();
					if (shouldDeleteCurrent)
					{
						this.Delete();
					}
				}
				return true;
			}
			else
			{
				ContainerBlock containingControlStructure = FindNearestControlStructureParent();
				if (shouldDeleteCurrent)
				{
					this.MoveAfterBlock(ParentParent);
					return true;
				}
				else if (containingControlStructure != null)
				{
					containingControlStructure.AppendBlocks(CreateNewTextLine());
					return true;
				}
			}

			return false;
		}

		private ContainerBlock FindNearestControlStructureParent()
		{
			return ClassNavigator.FindContainingControlStructure(this);
		}

		protected bool CanCtrlEnter()
		{
			Block ParentParentNext = ParentParent != null ? ParentParent.Next : null;
			TextBoxBlock NextText = ParentParentNext as TextBoxBlock;
			bool shouldDeleteCurrent = LastCtrlReturn && this.Text == "" && this.Prev != null;

			if (this.Next != null || ParentParent == null)
			{
				return false;
			}

			if ((NextText != null && NextText.Text == "")
				|| LastCtrlReturn
				|| ParentParent is IControlStructure)
			{
				return true;
			}

			return false;
		}

		protected bool LastCtrlReturn
		{
			get
			{
				return lastKey.KeyCode == System.Windows.Forms.Keys.Return && lastKey.Control;
			}
		}

		#endregion

		#region Indenting

		/// <summary>
		/// Tries to raise the current textbox and to append it to the end
		/// of previous ControlStructureBlock
		/// </summary>
		/// <returns>true if succeeded, false if couldn't do anything</returns>
		protected bool Indent()
		{
			Block moveAfter = CanIndent();
			if (moveAfter != null)
			{
				this.MoveAfterBlock(moveAfter);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Tries to "indent" the current line - which is,
		/// if a previous block is a ControlStructureBlock, it tries to 
		/// append this line to the end of the previous block, thus "raising" it.
		/// </summary>
		/// <returns>A block after which the current block must be placed,
		/// to be indented. null if no such block exists.</returns>
		protected Block CanIndent()
		{
			ControlStructureBlock prev = this.Prev as ControlStructureBlock;
			if (prev == null)
			{
				return null;
			}

			return prev.VMembers.Children.Tail;
		}

		/// <summary>
		/// If the current textbox is the last in the block, 
		/// move it out and after the block
		/// </summary>
		/// <returns>true if succeeded, false if couldn't do anything</returns>
		protected virtual bool UnIndent()
		{
			Block par = CanUnIndent();
			if (par != null)
			{
				this.MoveAfterBlock(par);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Tries to UnIndent the current line - 
		/// if this line is inside some ControlStructureBlock,
		/// move this line out and after its parent.
		/// </summary>
		/// <returns>Parent control structure if UnIndent is possible, null otherwise.</returns>
		protected virtual Block CanUnIndent()
		{
			ControlStructureBlock par = ParentParent as ControlStructureBlock;
			if (par == null || this.Prev == null || this.Next != null)
			{
				return null;
			}
			return par;
		}

		#endregion

		#endregion

		#region Help

		public override IEnumerable<string> HelpStrings
		{
			get
			{
				if (Multiline)
				{
					yield return "Press [Enter] to insert a new line.";
				}
				if (Multiline && CanCtrlEnter())
				{
					ControlStructureBlock controlStructure = ParentParent as ControlStructureBlock;
					if (controlStructure != null)
					{
						yield return "Press [Ctrl+Enter] to jump out of the " + controlStructure.Keyword + " block and to move to the next block.";
						yield return "Do not release [Ctrl] and repeat pressing [Enter] to jump out of multiple blocks.";
					}
					else
					{
						yield return "Press [Ctrl+Enter] to jump out of the current block.";
					}
				}
				else if (Multiline)
				{
					yield return "Press [Ctrl+Enter] to insert a line while staying at the current line.";
				}
				if (Multiline && CanIndent() != null)
				{
					yield return "Press [Tab] to indent the current line.";
				}
				if (Multiline && CanUnIndent() != null)
				{
					yield return "Press [Shift+Tab] to unindent the current line.";
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

        protected override string StyleName()
        {
            return "CommentLine";
        }

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
