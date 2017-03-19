using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Commands;
using GuiLabs.Editor.UI.Commands;

namespace GuiLabs.Editor.CSharp
{
	public class CodeBlock : UniversalBlock, ICSharpBlock
	{
		#region ctors

		public CodeBlock()
			: base()
		{
			Init();
		}

		public CodeBlock(VContainerBlock vMembers)
			: base(vMembers)
		{
			Init();
		}

		private void Init()
		{
			CanMoveUpDown = true;
			Draggable = true;
			if (Menu == null)
			{
				Menu = DeleteCommand.CreateDeleteMenu(this);
			}
			this.HMembers.MyListControl.Box.Padding.Bottom = 3;
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//if (e.KeyCode == System.Windows.Forms.Keys.PageUp)
			//{
			//    Block prev = this.GetNeighborBlock(-2);
			//    if (prev == null)
			//    {
			//        prev = this.FindPrevFocusableBlock();
			//    }
			//    if (prev != null)
			//    {
			//        prev.SetFocus(true);
			//        e.Handled = true;
			//    }
			//}
			//if (e.KeyCode == System.Windows.Forms.Keys.PageDown)
			//{
			//    Block next = this.GetNeighborBlock(2);
			//    if (next == null)
			//    {
			//        next = this.FindNextFocusableBlock();
			//    }
			//    if (next != null)
			//    {
			//        next.SetFocus(true);
			//        e.Handled = true;
			//    }
			//}

			if (IsMoveUpDown(e))
			{
				e.Handled = true;
				return;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				Block b = this.FindFirstFocusableChild();
				if (b != null)
				{
					b.SetFocus();
					e.Handled = true;
				}
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Insert
			|| (e.KeyCode == System.Windows.Forms.Keys.Return && e.Control))
			{
				Block nextBlock = this.FindNextFocusableBlock();
				if (nextBlock != null)
				{
					nextBlock.SetFocus();
					e.Handled = true;
				}
			}
			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);
			if (e.Handled)
			{
				return;
			}
			if (char.IsLetterOrDigit(e.KeyChar))
			{
				Block b = this.FindFirstFocusableChild();
				if (b != null)
				{
					using (Redrawer r = new Redrawer(this.Root))
					{
						b.SetCursorToTheEnd();
						b.MyControl.OnKeyPress(e);
					}
					e.Handled = true;
				}
			}
		}

		#endregion

		#region Visitor

		public virtual void AcceptVisitor(IVisitor visitor)
		{
			visitor.Visit(this);
		}

		#endregion
	}
}
