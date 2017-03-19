using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Controls
{
	public class PictureBox : Control
	{
		public PictureBox(IPicture picture)
		{
            mMyPicture = picture;
			Layout();
		}

		private IPicture mMyPicture;
		public IPicture MyPicture
		{
			get { return mMyPicture; }
			set {
				if (value == null)
				{
					return;
				}
				mMyPicture = value;
			}
		}

		public override void LayoutCore()
		{
			if (MyPicture != null)
			{
				this.Bounds.Size = MyPicture.Size;
			}
		}

		public override void DrawCore(GuiLabs.Canvas.Renderer.IRenderer Renderer)
		{
			if (MyPicture != null)
			{
				Renderer.DrawOperations.DrawImage(MyPicture, this.Bounds.Location);
			}
		}
	}
}
