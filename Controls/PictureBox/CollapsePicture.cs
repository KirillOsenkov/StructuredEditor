using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Controls
{
	public class CollapsePicture : PictureChangeBox
	{
		public CollapsePicture(IPicture expandedPicture, IPicture collapsedPicture) 
			: base(expandedPicture, collapsedPicture)
		{
			this.Box.Margins.SetLeftAndTop(3);
			this.Box.Margins.Right = 8;
			this.Box.Margins.Bottom = 3;
			this.Box.MouseSensitivityArea = this.Box.Margins;
		}
	}
}
