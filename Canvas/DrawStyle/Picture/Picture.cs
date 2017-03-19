using System;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public class Picture : IPicture
	{
		public Picture(String path)
		{
			InitBitmap(System.Drawing.Image.FromFile(path));
		}

		public Picture(System.Drawing.Image image)
		{
			InitBitmap(image);
		}

		private void InitBitmap(System.Drawing.Image image)
		{
			bool needToDisposeBitmap = false;

			System.Drawing.Bitmap Bitmap = image as System.Drawing.Bitmap;
			if (Bitmap == null && image != null)
			{
				Bitmap = new System.Drawing.Bitmap(image);
				needToDisposeBitmap = true;
			}

			mSize = new Point(Bitmap.Size.Width, Bitmap.Size.Height);
			hBitmap = Bitmap.GetHbitmap();

			if (needToDisposeBitmap)
			{
				Bitmap.Dispose();
			}
		}

		private Point mSize;
		public Point Size
		{
			get { return mSize; }
		}

		private bool mTransparent = false;
		public bool Transparent
		{
			get
			{
				return mTransparent;
			}
			set
			{
				mTransparent = value;
			}
		}

		private System.Drawing.Color mTransparentColor;
		public int Win32TransparentColor = 0;
		public System.Drawing.Color TransparentColor
		{
			get
			{
				return mTransparentColor;
			}
			set
			{
				mTransparentColor = value;
				Win32TransparentColor = System.Drawing.ColorTranslator.ToWin32(value);
			}
		}

		private IntPtr mhBitmap;
		public IntPtr hBitmap
		{
			get
			{
				return mhBitmap;
			}
			set
			{
				mhBitmap = value;
			}
		}

		public void Dispose()
		{
			if (hBitmap != System.IntPtr.Zero)
			{
				API.DeleteObject(hBitmap);
				hBitmap = System.IntPtr.Zero;
			}
		}

		~Picture()
		{
			Dispose();
		}
	}
}
