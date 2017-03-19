using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Diagnostics;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Xml
{
	[BlockSerialization("text")]
	public class EmptyNodeBlock : TextLine, IXmlBlock
	{
		#region ctor

		public EmptyNodeBlock()
		{
			this.Completion.CustomItemsRequested += new CustomItemsRequestHandler(Completion_CustomItemsRequested);
			this.Completion.BeforeShowingCompletionList += new BeforeShowingCompletionListHandler(Completion_BeforeShowingCompletionList);
			Draggable = true;
		}

		#endregion

		#region Completion

		void Completion_BeforeShowingCompletionList(BeforeShowingCompletionListEventArgs e)
		{
			if (!Completion.CanShowForPrefix(this.Text))
			{
				e.Cancel = true;
			}
		}

		void Completion_CustomItemsRequested(CustomItemsRequestEventArgs e)
		{
			AddItemsFromSchema(e);
		}

		void AddItemsFromSchema(CustomItemsRequestEventArgs e)
		{
			string path = Schema.GetPath(this);
			XmlSchemaElement el = Schema.FindPath(path, RootNode.RootSchemaElement);
			if (el == null)
			{
				return;
			}
			foreach (string s in Schema.GetPossibleTags(el))
			{
				e.Items.Add(new CreateTagItem(s));
			}
		}

		#endregion

		public XMLRootBlock RootNode
		{
			get
			{
				return Root as XMLRootBlock;
			}
		}

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Insert
				|| (e.KeyCode == System.Windows.Forms.Keys.Return && e.Control))
			{
				JumpOut();
				e.Handled = true;
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Back
				&& e.Control)
			{
				UnIndent();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyDownUp(System.Windows.Forms.KeyEventArgs e)
		{
			NodeBlock parent = ParentParent as NodeBlock;
			if (Prev == null && parent != null && parent.Parent is XMLRootBlock)
			{
				parent.HMembers.SetDefaultFocus();
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDownUp(e);
			}
		}

		protected override void OnKeyDownTab(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Shift)
			{
				UnIndent();
				e.Handled = true;
				return;
			}
			if (Indent())
			{
				e.Handled = true;
				return;
			}
			NodeBlock n = new NodeBlock();
			n.NameBlock.Text = this.Text;
			this.Replace(n);
			e.Handled = true;
		}

		protected override void OnTextHasChanged(GuiLabs.Canvas.Controls.ITextProvider changedControl, string oldText, string newText)
		{
			if (Completion.Visible)
			{
				if (!Completion.ExistingItemsHavePrefix(this.Text))
				{
					Completion.HideCompletionList();
				}
				else
				{
					Completion.ShowCompletionList(this, this.Text);
				}
			}
			base.OnTextHasChanged(changedControl, oldText, newText);
		}

		#endregion

		#region Multiline API

		public void JumpOut()
		{
			ContainerBlock p = this.ParentParent;
			if (p != null && p.Parent != Root)
			{
				TextBoxBlock next = p.Next as TextBoxBlock;
				if (next != null && string.IsNullOrEmpty(next.Text))
				{
					next.SetFocus();
				}
				else
				{
					using (Root.Transaction())
					{
						if (string.IsNullOrEmpty(this.Text) && this.Next == null && this.Prev != null)
						{
							this.Delete();
						}
						p.AppendBlocks(new EmptyNodeBlock());
					}
				}
			}
		}

		public bool Indent()
		{
			NodeBlock previous = Prev as NodeBlock;
			if (string.IsNullOrEmpty(this.Text) && previous != null)
			{
				this.Move(previous.VMembers);
				return true;
			}
			return false;
		}

		public bool UnIndent()
		{
			if (this.Next == null && this.Prev != null)
			{
				ContainerBlock p = this.ParentParent;
				if (p != null && p.Parent != Root)
				{
					this.MoveAfterBlock(p);
					return true;
				}
			}
			return false;
		}

		#endregion
	}
}