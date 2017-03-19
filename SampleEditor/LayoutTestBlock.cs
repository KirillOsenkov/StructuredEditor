using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Sample
{
	public class LayoutTestBlock : VContainerBlock
	{
		public LayoutTestBlock() : base()
		{
			this.MyControl.Focusable = true;

			this.Children.Add(new FocusableLabelBlock("aaa"));
			VContainerBlock v = new VContainerBlock();
			v.Children.Add(new FocusableLabelBlock("bbbbbbb"));
			v.Children.Add(new FocusableLabelBlock("bbbbbbb"));
			this.Children.Add(v);
			this.Children.Add(new FocusableLabelBlock("cc"));
			this.Children.Add(new FocusableLabelBlock("ddddddddd"));
		}

		#region Style

		protected override string StyleName()
		{
			return "LayoutTestBlock";
		}

		#endregion
		
	}
}
