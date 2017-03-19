namespace GuiLabs.Canvas.DrawStyle
{
	public class GDIPlusPicture : IPicture
	{
		public GDIPlusPicture(System.Drawing.Image image)
		{
			Image = image;
		}

		private System.Drawing.Image mImage;
		public System.Drawing.Image Image
		{
			get
			{
				return mImage;
			}
			set
			{
				mImage = value;
			}
		}

		private Point mSize = new Point();
		public Point Size
		{
			get
			{
				mSize.Set(Image.Size);
				return mSize;
			}
		}

		public void Dispose()
		{
			if (Image != null)
			{
				Image.Dispose();
			}
			Image = null;
		}
	}
}
