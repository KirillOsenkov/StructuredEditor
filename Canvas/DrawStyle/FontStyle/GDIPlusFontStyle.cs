#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Utils;

#endregion

namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIPlusFontStyle : IFontStyleInfo
	{
		public GDIPlusFontStyle(String FamilyName, float size)
		{
			Font = new GDIPlusFontWrapper(FamilyName, size);
		}

		public GDIPlusFontStyle(String FamilyName, float size, System.Drawing.FontStyle style)
		{
			Font = new GDIPlusFontWrapper(FamilyName, size, style);
		}

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
			}
		}

		public Memento CreateSnapshot()
		{
			Memento result = new Memento();
			result.NodeType = "FontStyle";
			
			result.Put("color", this.ForeColor);
			result["fontName"] = this.Font.Name;
			result.Put("size", this.Font.Size);
			result.Put("bold", this.Font.Bold);
			result.Put("italic", this.Font.Italic);
			result.Put("underline", this.Font.Underline);
			return result;
		}
	}
}
