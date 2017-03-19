using System.Drawing;

namespace GuiLabs.Editor.UI
{
	public struct TextPictureInfo
	{
		public TextPictureInfo(string text)
		{
			Text = text;
			Picture = null;
		}

		public TextPictureInfo(string text, Image picture)
		{
			Text = text;
			Picture = picture;
		}

		public string Text;
		public Image Picture;
	}
}
