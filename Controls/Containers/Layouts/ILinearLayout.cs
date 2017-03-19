namespace GuiLabs.Canvas.Controls
{
	public interface ILinearLayout : ILayout
	{
		AlignmentType Alignment { get; set; }
		AlignmentType LayerAlignment { get; set; }
		OrientationType Orientation { get; set; }
		int WrapMaxSize { get; set; }
		//int XMargin { get; set; }
		//int YMargin { get; set; }
		int XSpacing { get; set; }
		int YSpacing { get; set; }
		System.Windows.Forms.Keys PrevKey { get; }
		System.Windows.Forms.Keys NextKey { get; }
	}
}
