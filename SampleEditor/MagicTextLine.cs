using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Sample
{
    public class MagicTextLine : TextBoxBlock
    {
        public MagicTextLine()
            : base()
        {
            this.MyTextBox.KeyDown += MyTextBox_KeyDown;
        }

        public MagicTextLine(string initialText) 
            : this()
        {
			this.Text = initialText;
        }

        private void ProcessReturnKey()
        {
            string LeftText = this.MyTextBox.Text.Substring(0, this.MyTextBox.CaretPosition);
            string RightText = this.MyTextBox.Text.Substring(this.MyTextBox.CaretPosition);
            this.MyTextBox.Text = LeftText;

            MagicTextLine newLine = new MagicTextLine(RightText);
            this.AppendBlocks(new Block[] { newLine });
            newLine.SetFocus();
        }

        private void ProcessBackspace()
        {
            if (this.MyTextBox.CaretPosition != 0)
            {
                return;
            }

            string RightText = this.Text;

            MagicTextLine previous = this.Prev as MagicTextLine;
            if (previous != null)
            {
                int textLength = previous.Text.Length;
                previous.Text = previous.Text + RightText;
                previous.MyTextBox.CaretPosition = textLength;
                previous.SetFocus();
                this.Delete();
            }
        }

        void MyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                ProcessReturnKey();
            }

            if (e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                ProcessBackspace();
            }
        }
    }
}
