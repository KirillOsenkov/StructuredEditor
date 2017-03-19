using System;
using System.Drawing;
using System.ComponentModel;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIFillStyle : IFillStyleInfo
	{
		public GDIFillStyle(Color fillColor)
		{
			this.FillColor = fillColor;
		}

		public GDIFillStyle(Color fillColor, Color gradientColor, FillMode mode)
		{
			this.FillColor = fillColor;
			this.GradientColor = gradientColor;
			this.Mode = mode;
		}

		private Color mFillColor = new Color();
		public Color FillColor
		{
			get
			{
				return mFillColor;
			}
			set
			{
				mFillColor = value;
				mWin32FillColor = ColorTranslator.ToWin32(value);
			}
		}

		private int mWin32FillColor = 0;
		[Browsable(false)]
		public int Win32FillColor
		{
			get
			{
				return mWin32FillColor;
			}
			set
			{
				mWin32FillColor = value;
				this.FillColor = ColorTranslator.FromWin32(value);
			}
		}

		private Color mGradientColor = Color.White;
		public Color GradientColor
		{
			get
			{
				return mGradientColor;
			}
			set
			{
				mGradientColor = value;
				mWin32GradientColor = ColorTranslator.ToWin32(value);
			}
		}

		private int mWin32GradientColor = ColorTranslator.ToWin32(Color.White);
		[Browsable(false)]
		public int Win32GradientColor
		{
			get
			{
				return mWin32GradientColor;
			}
			set
			{
				mWin32GradientColor = value;
				this.GradientColor = ColorTranslator.FromWin32(value);
			}
		}
		
		private FillMode mMode = FillMode.Solid;
		public FillMode Mode
		{
			get
			{
				return mMode;
			}
			set
			{
				mMode = value;
			}
		}
		
		public void Dispose()
		{
		}

		public override string ToString()
		{
			string result = "Fill " + FillColor.ToString();
			if (Mode != FillMode.Solid)
			{
				result += " " + Mode.ToString() + " " + GradientColor.ToString();
			}
			return result;
		}

		public Memento CreateSnapshot()
		{
			Memento result = new Memento();
			result.NodeType = "FillStyle";
			result.Put("color", this.FillColor);
			if (Mode != FillMode.Solid)
			{
				result.Put("gradientColor", this.GradientColor);
				result["mode"] = this.Mode.ToString();
			}
			return result;
		}
	}
}
