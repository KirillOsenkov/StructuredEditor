using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIPlusFillStyle : IFillStyleInfo
	{
		public GDIPlusFillStyle(Color fillColor)
		{
			Brush = new SolidBrush(fillColor);
		}

		public GDIPlusFillStyle(Color fillColor, Color gradientColor, FillMode mode)
			: this(fillColor)
		{
			GradientColor = gradientColor;
			Mode = mode;
		}

		private SolidBrush mBrush;
		public SolidBrush Brush
		{
			get
			{
				return mBrush;
			}
			set
			{
				mBrush = value;
			}
		}

		public Color FillColor
		{
			get
			{
				return mBrush.Color;
			}
			set
			{
				mBrush.Color = value;
			}
		}

		private Color mGradientColor;
		public Color GradientColor
		{
			get
			{
				return mGradientColor;
			}
			set
			{
				mGradientColor = value;
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
			if(mBrush != null)
			{
				mBrush.Dispose();
				mBrush = null;
			}
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

		~GDIPlusFillStyle()
		{
			Dispose();
		}
	}
}
