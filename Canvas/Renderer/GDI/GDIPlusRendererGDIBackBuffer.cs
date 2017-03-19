using System;
using System.Drawing;
using System.Windows.Forms;
using GuiLabs.Canvas.DrawOperations;

namespace GuiLabs.Canvas.Renderer
{
	internal class GDIPlusRendererGDIBackBuffer : GDIRenderer
	{
		public GDIPlusRendererGDIBackBuffer()
			: base()
		{
		}

		private Graphics graphics;
		protected override void InitDrawOperations()
		{
			graphics = Graphics.FromHdc(hDC);
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			DrawOperations = new GDIPlusDrawOperations(graphics);
		}

		public override void Dispose()
		{
			if (graphics != null)
			{
				graphics.Dispose();
				graphics = null;
			}
			base.Dispose();
		}
	}
}