using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.Blocks
{
	public class PictureBlock : Block
	{
		public PictureBlock()
			: base()
		{
			InitControl();
		}

		public PictureBlock(IPicture picture)
			: base()
		{
			InitControl(picture);
		}

		private void InitControl()
		{
			MyPictureBox = new PictureBox(PictureLibrary.Instance.Plus);
		}

		private void InitControl(IPicture picture)
		{
			MyPictureBox = new PictureBox(picture);
		}

		public override Control MyControl
		{
			get
			{
				return MyPictureBox;
			}
		}

		private PictureBox mMyPictureBox;
		public PictureBox MyPictureBox
		{
			get
			{
				return mMyPictureBox;
			}
			set
			{
				if (mMyPictureBox != null)
				{
					UnSubscribeControl();
				}
				mMyPictureBox = value;
				if (mMyPictureBox != null)
				{
					SubscribeControl();
				}
			}
		}
	}
}
