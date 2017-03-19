#region Using directives

using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using GuiLabs.Utils;
using System.Collections.Generic;

#endregion

namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIFont : IFontInfo
	{
		#region Constructors

		public GDIFont(String FamilyName, float size)
			: this(FamilyName, size, System.Drawing.FontStyle.Regular)
		{
		}

		public GDIFont(GDIFont ExistingFont)
		{
			this.mName = ExistingFont.Name;
			this.mSize = ExistingFont.Size;
			this.mBold = ExistingFont.Bold;
			this.mItalic = ExistingFont.Italic;
			this.mUnderline = ExistingFont.Underline;
			this.hFont = ExistingFont.hFont;
		}

		public GDIFont(String FamilyName, float size, System.Drawing.FontStyle style)
		{
			Init();

			this.mName = FamilyName;
			this.mSize = (int)size;
			this.mBold = (style & System.Drawing.FontStyle.Bold) != 0;
			this.mItalic = (style & System.Drawing.FontStyle.Italic) != 0;
			this.mUnderline = (style & System.Drawing.FontStyle.Underline) != 0;

			AssignHandle();
		}

		#endregion

		#region Init, Exit

		private static bool WasInit = false;

		private void Init()
		{
			if (!WasInit)
			{
				System.Windows.Forms.Application.ApplicationExit += OnApplicationExit;
				WasInit = true;
			}
		}

		private void OnApplicationExit(object sender, System.EventArgs e)
		{
			if (WasInit)
			{
				System.Windows.Forms.Application.ApplicationExit -= OnApplicationExit;
				foreach (IntPtr h in FontHandles.Values)
				{
					API.DeleteObject(h);
				}
				WasInit = false;
			}
		}

		#endregion

		private void AssignHandle()
		{
			IntPtr handle = FindHandle(GetSignature());

			if (handle == IntPtr.Zero)
			{
				CreateHandle();
				AddHandle(hFont);
			}
			else
			{
				hFont = handle;
			}
		}

		#region CreateHandle

		private void CreateHandle()
		{
			hFont = API.CreateFont(FontSize(this.Size), 0, 0, 0,
				(this.Bold) ? 700 : 400, (this.Italic) ? 1 : 0,
				(this.Underline) ? 1 : 0, 0,
				// (int)API.CHARSET.DEFAULT_CHARSET
				1
				, 0, 0, 2, 0, this.Name);
		}

		private int FontSize(int Size)
		{
			const int LOGPIXELSY = 90;

			IntPtr hDC = API.GetDC(API.GetDesktopWindow());
			int result = -API.MulDiv(Size, API.GetDeviceCaps(hDC, LOGPIXELSY), 72);
			API.ReleaseDC(API.GetDesktopWindow(), hDC);

			return result;
		}

		#endregion

		private static Dictionary<string, IntPtr> FontHandles
			= new Dictionary<string, IntPtr>();

		#region AddHandle, FindHandle

		private void AddHandle(IntPtr h)
		{
			FontHandles[GetSignature()] = h;
		}

		private IntPtr FindHandle(string Signature)
		{
			IntPtr o = IntPtr.Zero;
			FontHandles.TryGetValue(Signature, out o);
			return o;
		}

		private string GetSignature()
		{
			return GetSignature(
				this.Name,
				this.Size.ToString(),
				this.GetFontStyle().ToString());
		}

		private string GetSignature(string name, string size, string style)
		{
			StringBuilder s = new StringBuilder();
			s.Append(name);
			s.Append(" ");
			s.Append(size);
			s.Append(", ");
			s.Append(style);
			return s.ToString();
		}

		#endregion

		#region Properties

		private IntPtr mhFont;
		public IntPtr hFont
		{
			get
			{
				return mhFont;
			}
			set
			{
				mhFont = value;
			}
		}

		private string mName;
		public string Name
		{
			get
			{
				return mName;
			}
		}

		private int mSize;
		public int Size
		{
			get
			{
				return mSize;
			}
			set
			{
				if (mSize == value
					|| value < 4
					|| value > 100)
				{
					return;
				}

				this.mSize = value;
				mSpaceCharSize.Set0();

				string newSignature = GetSignature();
				IntPtr existing = FindHandle(newSignature);
				if (existing == IntPtr.Zero)
				{
					CreateHandle();
					AddHandle(hFont);
				}
				else
				{
					hFont = existing;
				}
			}
		}

		#region Style

		private System.Drawing.FontStyle GetFontStyle()
		{
			System.Drawing.FontStyle result = System.Drawing.FontStyle.Regular;

			if (this.Bold)
			{
				result |= System.Drawing.FontStyle.Bold;
			}

			if (this.Italic)
			{
				result |= System.Drawing.FontStyle.Italic;
			}

			if (this.Underline)
			{
				result |= System.Drawing.FontStyle.Underline;
			}

			return result;
		}

		private bool mBold;
		public bool Bold
		{
			get
			{
				return mBold;
			}
		}

		private bool mItalic;
		public bool Italic
		{
			get
			{
				return mItalic;
			}
		}

		private bool mUnderline;
		public bool Underline
		{
			get
			{
				return mUnderline;
			}
		}

		public System.Drawing.FontStyle FontStyle
		{
			get
			{
				System.Drawing.FontStyle result = System.Drawing.FontStyle.Regular;
				if (Bold)
				{
					result |= System.Drawing.FontStyle.Bold;
				}
				if (Italic)
				{
					result |= System.Drawing.FontStyle.Italic;
				}
				if (Underline)
				{
					result |= System.Drawing.FontStyle.Underline;
				}
				return result;
			}
		}

		private readonly Point mSpaceCharSize = new Point();
		public Point SpaceCharSize
		{
			get
			{
				if (mSpaceCharSize.X == 0 || mSpaceCharSize.Y == 0)
				{
					mSpaceCharSize.Set(Renderer.RendererSingleton.DrawOperations.StringSize(" ", this));
				}
				return mSpaceCharSize;
			}
		}

		#endregion

		#endregion

		public override string ToString()
		{
			return GetSignature();
		}
	}
}