#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using GuiLabs.Utils;

#endregion

namespace GuiLabs.Canvas.DrawStyle
{
    internal class GDIFontStyle: IFontStyleInfo
	{
		#region Color

		private System.Drawing.Color mForeColor = System.Drawing.Color.Black;
		public System.Drawing.Color ForeColor
		{
			get
			{
				return mForeColor;
			}
			set
			{
				mForeColor = value;
				mWin32ForeColor = System.Drawing.ColorTranslator.ToWin32(value);
			}
		}

		private int mWin32ForeColor = 0;
		[Browsable(false)]
		public int Win32ForeColor
		{
			get
			{
				return mWin32ForeColor;
			}
			set
			{
				mWin32ForeColor = value;
				this.ForeColor = System.Drawing.ColorTranslator.FromWin32(mWin32ForeColor);
			}
		}

		#endregion

		#region Font

		private IFontInfo mFont;
		public IFontInfo Font
		{
			get
			{
				return mFont;
			}
			set
			{
				mFont = value;
			}
		}

		#endregion

		public string Name
		{
			get
			{
				return Font.Name;
			}
		}
		public int Size
		{
			get
			{
				return Font.Size;
			}
		}
		public bool Bold
		{
			get
			{
				return Font.Bold;
			}
		}
		public bool Italic
		{
			get
			{
				return Font.Italic;
			}
		}
		public bool Underline
		{
			get
			{
				return Font.Underline;
			}
		}

		public Memento CreateSnapshot()
		{
			Memento result = new Memento();
			result.NodeType = "FontStyle";
			result.Put("color", this.ForeColor);
			result["fontName"] = this.Name;
			result.Put("size", this.Size);
			result.Put("bold", this.Bold);
			result.Put("italic", this.Italic);
			result.Put("underline", this.Underline);
			return result;
		}

		public override string ToString()
		{
			return Font.ToString() + " " + ForeColor.ToString();
		}
	}
}