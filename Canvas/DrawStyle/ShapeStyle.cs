using System.Drawing;
using GuiLabs.Canvas.Renderer;
using System.ComponentModel;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.DrawStyle
{
	public class ShapeStyle : IShapeStyle
	{
		#region ctors

		/// <summary>
		/// ShapeStyle Constructor initializes the three objects with their default values.
		/// </summary>
		public ShapeStyle()
		{
			FillStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFillStyleInfo(Color.Transparent);
			LineStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(Color.Transparent, 1);
			FontStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFontStyleInfo(DefaultFont, DefaultFontSize, FontStyle.Regular);
		}

		public ShapeStyle(Color fillColor)
		{
			FillStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFillStyleInfo(fillColor);
			LineStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(Color.Transparent, 1);
			FontStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFontStyleInfo(DefaultFont, DefaultFontSize, FontStyle.Regular);
		}

		public ShapeStyle(Color borderColor, Color fillColor)
		{
			FillStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFillStyleInfo(fillColor);
			LineStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(borderColor, 1);
			FontStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFontStyleInfo(DefaultFont, DefaultFontSize, FontStyle.Regular);
		}

		public ShapeStyle(Color borderColor, IFillStyleInfo fill)
		{
			FillStyleInfo = fill;
			LineStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(borderColor, 1);
			FontStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFontStyleInfo(DefaultFont, DefaultFontSize, FontStyle.Regular);
		}

		public ShapeStyle(Color borderColor, Color fillColor, Color gradientColor, FillMode gradientMode)
		{
			FillStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFillStyleInfo(fillColor, gradientColor, gradientMode);
			LineStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(borderColor, 1);
			FontStyleInfo = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewFontStyleInfo(DefaultFont, DefaultFontSize, FontStyle.Regular);
		}

		#endregion

		private string mName;
		[Browsable(false)]
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		#region Defaults

		public const string DefaultFont = "Consolas";
		public const int DefaultFontSize = 10;

		#endregion

		#region Fill

		private IFillStyleInfo mFillStyleInfo;
		[Browsable(false)]
		public IFillStyleInfo FillStyleInfo
		{
			get
			{
				return mFillStyleInfo;
			}
			set
			{
				mFillStyleInfo = value;
			}
		}

		#endregion

		#region Line

		private ILineStyleInfo mLineStyleInfo;
		[Browsable(false)]
		public ILineStyleInfo LineStyleInfo
		{
			get
			{
				return mLineStyleInfo;
			}
			set
			{
				mLineStyleInfo = value;
			}
		}

		#endregion

		#region Font

		private IFontStyleInfo mFontStyleInfo;
		[Browsable(false)]
		public IFontStyleInfo FontStyleInfo
		{
			get
			{
				return mFontStyleInfo;
			}
			set
			{
				mFontStyleInfo = value;
			}
		}

		#endregion

		#region Shortcuts

		// The following properties are just shortcuts to the
		// corresponding properties of the 3 basic Canvas objects.

		#region Shortcuts to properties of Canvas objects

		/// <summary>
		/// Shortcut to LineStyle.ForeColor
		/// </summary>
		[Category("Line")]
		public Color LineColor
		{
			get
			{
				return this.LineStyleInfo.ForeColor;
			}
			set
			{
				this.LineStyleInfo.ForeColor = value;
			}
		}

		/// <summary>
		/// Shortcut to FillStyle.FillColor
		/// </summary>
		[Category("Fill")]
		public Color FillColor
		{
			get
			{
				return this.FillStyleInfo.FillColor;
			}
			set
			{
				this.FillStyleInfo.FillColor = value;
			}
		}

		/// <summary>
		/// Shortcut to FillStyle.FillColor
		/// </summary>
		[Category("Fill")]
		public Color GradientColor
		{
			get
			{
				return this.FillStyleInfo.GradientColor;
			}
			set
			{
				this.FillStyleInfo.GradientColor = value;
			}
		}

		[Category("Fill")]
		[DefaultValue(FillMode.Solid)]
		public FillMode Mode
		{
			get
			{
				return this.FillStyleInfo.Mode;
			}
			set
			{
				this.FillStyleInfo.Mode = value;
			}
		}

		/// <summary>
		/// Shortcut to LineStyle.Width
		/// </summary>
		[Category("Line")]
		[DefaultValue(1)]
		public int LineWidth
		{
			get
			{
				return this.LineStyleInfo.Width;
			}
			set
			{
				this.LineStyleInfo.Width = value;
			}
		}

		/// <summary>
		/// Shortcut to MyFontStyle.Font.Name
		/// </summary>
		[Category("Font")]
		public string FontName
		{
			get
			{
				return this.FontStyleInfo.Font.Name;
			}
		}

		/// <summary>
		/// Shortcut to MyFontStyle.Font.Size
		/// </summary>
		[Category("Font")]
		[DefaultValue(10)]
		[ReadOnly(true)]
		public int FontSize
		{
			get
			{
				return (int)this.FontStyleInfo.Font.Size;
			}
			set
			{
				this.FontStyleInfo.Font.Size = value;
			}
		}


		[Category("Font")]
		public Color FontColor
		{
			get
			{
				return this.FontStyleInfo.ForeColor;
			}
			set
			{
				this.FontStyleInfo.ForeColor = value;
			}
		}

		#endregion

		#endregion

		public override string ToString()
		{
			return LineStyleInfo.ToString() +
				" " +
				FillStyleInfo.ToString() +
				" " +
				FontStyleInfo.ToString();
		}

		public Memento CreateSnapshot()
		{
			Memento result = new Memento();
			result.NodeType = "ShapeStyle";
			result["name"] = this.Name;
			result.Add(LineStyleInfo);
			result.Add(FillStyleInfo);
			result.Add(FontStyleInfo);
			return result;
		}

		public static ShapeStyle CreateFromMemento(Memento snapshot)
		{
			//<ShapeStyle name="TypeName">
			//  <LineStyle color="16777215" width="1" />
			//  <FillStyle color="16777215" gradientColor="-1" mode="Solid" />
			//  <FontStyle color="-13921873" fontName="Consolas" size="10" bold="False" italic="False" underline="False" />
			//</ShapeStyle>

			ShapeStyle result = new ShapeStyle();
			result.Name = snapshot["name"];

			Memento line = snapshot.FindChild("LineStyle");
			if (line != null)
			{
				Color foreColor = line.GetColor("color");
				int width = line.GetInt("width");
				if (width == 0)
				{
					width = 1;
				}
				result.LineStyleInfo = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(foreColor, width);
			}

			Memento fill = snapshot.FindChild("FillStyle");
			if (fill != null)
			{
				Color fillColor = fill.GetColor("color");
				Color gradientColor = Color.Transparent;
				FillMode mode = FillMode.Solid;
				if (!string.IsNullOrEmpty(fill["mode"]))
				{
					gradientColor = fill.GetColor("gradientColor");
					mode = fill.Get<FillMode>("mode");
				}
				result.FillStyleInfo
					= RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
					fillColor, gradientColor, mode);
			}

			Memento font = snapshot.FindChild("FontStyle");
			if (font != null)
			{
				Color fontColor = font.GetColor("color");
				string fontName = font["fontName"];
				int size = font.GetInt("size");
				bool bold = font.GetBool("bold");
				bool italic = font.GetBool("italic");
				bool underline = font.GetBool("underline");
				FontStyle style = FontStyle.Regular;
				if (bold)
				{
					style |= FontStyle.Bold;
				}
				if (italic)
				{
					style |= FontStyle.Italic;
				}
				if (underline)
				{
					style |= FontStyle.Underline;
				}
				result.FontStyleInfo
					= RendererSingleton.StyleFactory.ProduceNewFontStyleInfo(
					fontName, size, style);
				result.FontColor = fontColor;
			}

			return result;
		}
	}
}
