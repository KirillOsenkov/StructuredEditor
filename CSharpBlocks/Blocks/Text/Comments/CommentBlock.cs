using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
    public class CommentBlock : BaseCommentBlock
    {
        protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                this.PrependBlocks(new StatementLine());
                e.Handled = true;
                return;
            }

            base.OnKeyDown(sender, e);
        }
    }
}