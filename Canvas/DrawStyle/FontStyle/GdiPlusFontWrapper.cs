namespace GuiLabs.Canvas.DrawStyle
{
	internal class GDIPlusFontWrapper : IFontInfo
	{
		public GDIPlusFontWrapper(string FamilyName, float size)
		{
			Font = new System.Drawing.Font(FamilyName, size);
		}

		public GDIPlusFontWrapper(string FamilyName, float size, System.Drawing.FontStyle style)
		{
			Font = new System.Drawing.Font(FamilyName, size, style);
		}

		public GDIPlusFontWrapper(System.Drawing.Font ExistingFont)
		{
			Font = ExistingFont;
		}

		private System.Drawing.Font mFont;
		public System.Drawing.Font Font
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

		#region IFontInfo Members

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
				return (int)Font.Size;
			}
			set
			{
				if (Font.Size == value || value < 4 || value > 100)
				{
					return;
				}
				System.Drawing.Font old = this.Font;
				this.Font = new System.Drawing.Font(
					old.Name,
					value,
					old.Style);
				old.Dispose();
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

		private Point mSpaceCharSize = new Point();
		public Point SpaceCharSize
		{
			get
			{
				if (mSpaceCharSize.X == 0 || mSpaceCharSize.Y == 0)
				{
					mSpaceCharSize = Renderer.RendererSingleton.DrawOperations.StringSize(" ", this);
				}
				return mSpaceCharSize;
			}
		}

		#endregion
	}
}