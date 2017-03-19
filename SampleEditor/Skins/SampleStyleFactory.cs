using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using System.Drawing;

namespace GuiLabs.Editor.Sample.Skins
{
	public class SampleStyleFactory : StyleFactory
	{
		public SampleStyleFactory()
		{
			AddStyle("TextBoxBlock", Color.White, Color.FloralWhite);
			AddStyle("TextBoxBlock_selected", Color.LightYellow, Color.LightYellow);

			AddStyle("SampleBlock", Color.White, Color.White);
			AddStyle("SampleBlock_selected", Color.LightYellow, Color.LightYellow);

			AddStyle("ButtonBlock",
				Color.DarkRed,
				Color.Linen,
				Color.PeachPuff,
				FillMode.VerticalGradient);

			AddStyle("ButtonBlock_selected",
				Color.DarkRed,
				Color.Linen,
				Color.SandyBrown,
				FillMode.VerticalGradient);

			AddStyle("LabelBlock", Color.White);
			AddStyle("LabelBlock_selected", Color.Wheat, Color.LightYellow);

			AddStyle("TextSelectionBlock", Color.White);
			AddStyle("SelectionLabelBlock_selected", Color.Wheat, Color.LightYellow);

			AddStyle("FocusableLabelBlock", Color.White);
			AddStyle("FocusableLabelBlock_selected", Color.Wheat, Color.LightYellow);

			AddStyle("TreeViewNode", Color.Transparent, Color.Transparent);
			AddStyle("TreeViewNode_selected", Color.Wheat, Color.LightYellow);
			
			AddStyle("LayoutTestBlock", Color.Black, Color.LightGray);
			AddStyle("LayoutTestBlock_selected", Color.Black, Color.Gray);

			ShapeStyle TreeViewStyle = new ShapeStyle(Color.Transparent);
			TreeViewStyle.FontStyleInfo =
				RendererSingleton.StyleFactory.ProduceNewFontStyleInfo
				("Tahoma",
				(float) 9,
				FontStyle.Regular);
			//TreeViewStyle.FontStyleInfo =
			//    RendererSingleton.StyleFactory.ProduceNewFontStyleInfo
			//    (System.Drawing.SystemFonts.DialogFont.Name,
			//    System.Drawing.SystemFonts.DialogFont.SizeInPoints,
			//    System.Drawing.SystemFonts.DialogFont.Style);
			Add("TreeViewLabelBlock", TreeViewStyle);
		}
	}
}
