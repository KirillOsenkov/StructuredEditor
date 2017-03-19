using System;
using System.Drawing;
using System.Windows.Forms;
using GuiLabs.Canvas.DrawOperations;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Renderer
{
	internal class GDIRenderer : IRenderer
	{
		protected IntPtr hDC;
		protected IntPtr BackBitmap;
		protected IntPtr OldBitmap;

		public GDIRenderer()
		{
			// Subscribe to this event to update the back buffer
			// if the screen resolution changes
			Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

			InitGDIBuffer();
			InitDrawOperations();
		}

		#region SystemEvents_DisplaySettingsChanged

		/// <summary>
		/// Screen resolution changed.
		/// </summary>
		void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			// Release the old buffer
			DisposeGDIBuffer();

			// And create a new buffer, maybe with different size
			InitGDIBuffer();
			InitDrawOperations();

			// TODO:
			// ("Canvas adapted itself to the changed display settings. If you experience any problems, you can restart the application.", "Canvas");
		}

		#endregion

		#region Init

		protected void InitGDIBuffer()
		{
			IntPtr hWnd = API.GetDesktopWindow();

			IntPtr hControlDC = API.GetDC(hWnd);

			hDC = API.CreateCompatibleDC(hControlDC);
			BackBitmap = API.CreateCompatibleBitmap(hControlDC, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			OldBitmap = API.SelectObject(hDC, BackBitmap);

			API.ReleaseDC(hWnd, hControlDC);
		}

		protected virtual void InitDrawOperations()
		{
			mDrawOperations = new GDIDrawOperations(hDC);
		}

		#endregion

		#region DrawOperations

		protected IDrawOperations mDrawOperations;
		public IDrawOperations DrawOperations
		{
			get
			{
				return mDrawOperations;
			}
			set
			{
				mDrawOperations = value;
			}
		}

		#endregion

		#region RenderBuffer

		public void RenderBuffer(Control DestinationControl, Rect ToRedraw)
		{
			IntPtr hWnd = DestinationControl.Handle;
			IntPtr hDestDC = API.GetDC(hWnd);
			API.BitBlt(hDestDC, ToRedraw.Location.X, ToRedraw.Location.Y, ToRedraw.Size.X, ToRedraw.Size.Y, hDC, ToRedraw.Location.X, ToRedraw.Location.Y, API.SRCCOPY);
			API.ReleaseDC(hWnd, hDestDC);
		}

		//public void RenderBuffer(Control DestinationControl, Rectangle r)
		//{
		//    IntPtr hWnd = DestinationControl.Handle;
		//    IntPtr hDestDC = API.GetDC(hWnd);
		//    API.BitBlt(hDestDC, r.Left, r.Top, r.Right, r.Bottom, hDC, r.Left, r.Top, API.SRCCOPY);
		//    API.ReleaseDC(hWnd, hDestDC);
		//}

		#endregion

		#region BackColor, ClientSize, Clear

		protected Color mBackColor = Color.White;
		public Color BackColor
		{
			get
			{
				return mBackColor;
			}
			set
			{
				mBackColor = value;
			}
		}

		//public Size ClientSize
		//{
		//    get
		//    {
		//        return mClientRectangle.Size;
		//    }
		//    //set
		//    //{
		//    //    mClientRectangle.Size = value;
		//    //}
		//}

		private Rectangle mClientRectangle;
		public void Clear()
		{
			mClientRectangle.Width = DrawOperations.Viewport.Size.X;
			mClientRectangle.Height = DrawOperations.Viewport.Size.Y;
			API.FillRectangle(hDC, mBackColor, mClientRectangle);
		}

		//public void Clear(Rect Area)
		//{
		//    API.FillRectangle(hDC, mBackColor, Area.GetRectangle());
		//}

		#endregion

		#region Dispose

		protected void DisposeGDIBuffer()
		{
			if (hDC != IntPtr.Zero)
			{
				API.SelectObject(hDC, OldBitmap);
				API.DeleteObject(BackBitmap);
				API.DeleteDC(hDC);

				hDC = IntPtr.Zero;
			}
		}

		public virtual void Dispose()
		{
			DisposeGDIBuffer();
		}

		~GDIRenderer()
		{
			Dispose();
		}

		#endregion
	}
}