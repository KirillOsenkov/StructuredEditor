namespace GuiLabs.Canvas.Controls
{
	public class HContainerControl : LinearContainerControl
	{
		public HContainerControl()
			: base()
		{
			this.LinearLayoutStrategy.Orientation = OrientationType.Horizontal;
		}
	}
}
