using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class TextLikeCodeBlock : VContainerBlock
	{
		public TextLikeCodeBlock()
			: base()
		{
			this.MyControl.Focusable = true;

			HMembers = new HContainerBlock();
			VMembers = new VContainerBlock();

			HMembers.CanBeSelected = false;
			VMembers.CanBeSelected = false;

			HeaderLine = new HContainerBlock();
			HeaderLine.Children.Add(new PictureBlock(CSharpPictureLibrary.Instance.Minus));
			HeaderLine.Children.Add(HMembers);

			this.Children.Add(HeaderLine);
			this.Children.Add("{");

			HContainerBlock body = new HContainerBlock();
			body.CanBeSelected = false;
			this.Children.Add(body);
			body.Children.Add("    ");
			body.Children.Add(VMembers);

			this.Children.Add("}");
		}

		public HContainerBlock HeaderLine { get; set; }
		public HContainerBlock HMembers { get; set; }
		public VContainerBlock VMembers { get; set; }
	}
}
