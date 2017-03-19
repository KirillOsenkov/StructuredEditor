using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.Sample
{
	//class SampleBlock : HContainerBlock
	//{
	//    public SampleBlock() : base()
	//    {
	//        this.MyControl.Focusable = true;

	//        this.Add(new LabelBlock("Tag("));

	//        text = new TextBoxBlock();
	//        this.Children.Add(text);
	//        text.MyTextBox.KeyUp += MyTextBox_KeyUp;
	//        this.text.MyTextBox.MinWidth = 8;

	//        this.Children.Add(new LabelBlock(")"));
	//    }

	//    public SampleBlock(string initialString) : this()
	//    {
	//        text.SetTextWithoutSideEffects(initialString);
	//    }

	//    public TextBoxBlock text;

	//    void MyTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
	//    {
	//        if (text.MyTextBox.SelectionLength > 0 && e.KeyCode == System.Windows.Forms.Keys.Tab)
	//        {
	//            string t1 = text.MyTextBox.Text.Substring(0, text.MyTextBox.SelectionStart);
	//            string t2 = text.MyTextBox.Text.Substring(text.MyTextBox.SelectionStart, text.MyTextBox.SelectionLength);
	//            string t3 = text.MyTextBox.Text.Substring(text.MyTextBox.SelectionEnd);
	//            ReplaceTextBox(text, t1, t2, t3);
	//        }
	//    }

	//    //void ReplaceTextBox(TextBoxBlock t, string t1, string t2, string t3)
	//    //{
	//    //    text.SetText = t2;

	//    //    ReplaceBlocksAction Action = new ReplaceBlocksAction(t.Parent, t);

	//    //    Action.PrepareBlockToRemove(t);

	//    //    TextBoxBlock links = new TextBoxBlock(4);
	//    //    links.Text = t1;

	//    //    TextBoxBlock rechts = new TextBoxBlock(4);
	//    //    rechts.Text = t3;

	//    //    Action.PrepareBlockToAdd(links);
	//    //    Action.PrepareBlockToAdd(new SampleBlock(t2));
	//    //    Action.PrepareBlockToAdd(rechts);

	//    //    t.Root.ActionManager.RecordAction(Action);
	//    //}

	//    #region Style

	//    protected override string StyleName()
	//    {
	//        return "SampleBlock";
	//    }

	//    #endregion
	
	//}
}
