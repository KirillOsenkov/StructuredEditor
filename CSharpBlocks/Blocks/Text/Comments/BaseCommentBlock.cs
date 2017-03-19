using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
    public class BaseCommentBlock : VContainerBlock
    {
        public BaseCommentBlock()
        {
            this.Children.Add(new CommentLine());
            this.MyControl.Focusable = true;
        }

        protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
            {
                Block next = this.Next;
                if (next != null)
                {
                    next.SetFocus();
                }
                else
                {
                    SetDefaultFocus();
                }
                e.Handled = true;
                return;
            }

            if (e.KeyCode == System.Windows.Forms.Keys.Right)
            {
                SetDefaultFocus();
                e.Handled = true;
                return;
            }

            base.OnKeyDown(sender, e);
        }

        protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            using (new Redrawer(Root))
            {
                Block child = this.FindFirstFocusableChild();
                if (child != null)
                {
                    child.SetCursorToTheBeginning();
                    child.MyControl.OnKeyPress(e);
                    e.Handled = true;
                    return;
                }
            }

            base.OnKeyPress(sender, e);
        }

        public override void SetDefaultFocus()
        {
            Block child = this.FindFirstFocusableChild();
            if (child != null)
            {
                child.SetCursorToTheBeginning();
            }
            else
            {
                this.SetFocus();
            }
        }

        protected override string StyleName()
        {
            return "CommentBlock";
        }
    }
}