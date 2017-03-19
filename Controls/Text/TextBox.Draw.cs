using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	public partial class TextBox
	{
		#region Draw

		private Point TextSize = new Point();
		private Rect ContentBounds = new Rect();
		public override void DrawCore(IRenderer Renderer)
		{
			if (ShouldDrawBackground)
			{
				DrawRectangleLike(Renderer);
			}

			if (ShouldDrawBorder)
			{
				DrawBorder(Renderer);
			}

			ContentBounds.Set(
				this.Bounds.Location.X + this.Box.Padding.Left,
				this.Bounds.Location.Y + this.Box.Padding.Top,
				this.Bounds.Size.X - this.Box.Padding.LeftAndRight,
				this.Bounds.Size.Y - this.Box.Padding.TopAndBottom);

			if (!string.IsNullOrEmpty(this.Text))
			{
				Renderer.DrawOperations.DrawStringWithSelection(
					ContentBounds,
					this.SelectionStart,
					this.SelectionEnd,
					this.Text,
					this.CurrentStyle.FontStyleInfo);
			}

			if (this.IsFocused)
			{
				UpdateCaretCoords();

				RendererSingleton.MyCaret.SetNewBounds(
					this.Bounds.Location.X + this.Box.Padding.Left + TextSize.X,
					this.Bounds.Location.Y + this.Box.Padding.Top,
					this.Bounds.Size.Y - this.Box.Padding.TopAndBottom
					// this.Style.FontStyleInfo.Font.SpaceCharSize.Y
					);

				Renderer.DrawOperations.DrawCaret(RendererSingleton.MyCaret);

				// RendererSingleton.MyCaret.Visible = true;
			}
		}

		private void UpdateCaretCoords()
		{
			TextSize = StringSize(this.Text.Substring(0, this.CaretPosition));

			if (TextSize.X > 0)
			{
				TextSize.X -= 1;
			}
		}

		#endregion

		#region String size calculations

		/// <summary>
		/// Determine a char number in a string
		/// which is closest to the clicked point
		/// </summary>
		/// <param name="x">x in pixels</param>
		/// <returns>Char number where to put the cursor</returns>
		private int CursorStringPosition(int x)
		{
			string subString = this.Text;

			int point = x - this.Bounds.Location.X - this.Box.Padding.Left;
			int cursorPosition = 0;

			if (point <= 0)
			{
				return 0;
			}

			if (point > TextWidthInPixels(subString))
			{
				return subString.Length;
			}

			while (subString.Length > 1)
			{
				int midString = subString.Length / 2;
				string firstHalfString = subString.Substring(0, midString);
				string secondHalfString = subString.Substring(midString);
				int firstHalfPixels = TextWidthInPixels(firstHalfString);

				if (point > firstHalfPixels)
				{
					subString = secondHalfString;
					point -= firstHalfPixels;
					cursorPosition += firstHalfString.Length;
				}
				else
				{
					subString = firstHalfString;
				}
			}

			if (point > TextWidthInPixels(subString) / 2)
			{
				cursorPosition++;
			}

			return cursorPosition;
		}

		private int TextWidthInPixels(string text)
		{
			return StringSize(text).X;
		}

		public Point StringSize(string text)
		{
			return RendererSingleton.DrawOperations.StringSize(text, this.CurrentFontStyle.Font);
		}

		#endregion

		#region Size & Layout

		private IFontStyleInfo CurrentFontStyle
		{
			get
			{
				return this.CurrentStyle.FontStyleInfo;
			}
		}

		/// <summary>
		/// Recalculates new size for the textbox
		/// </summary>
		/// <remarks>
		/// Raises SizeChanged event.
		/// </remarks>
		public override void LayoutCore()
		{
			string text = Text;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			Point p = StringSize(text);
			p.X += this.Box.Padding.LeftAndRight;
			p.Y += this.Box.Padding.TopAndBottom;

			if (p.X < MinWidth + this.Box.Padding.LeftAndRight)
			{
				p.X = MinWidth + this.Box.Padding.LeftAndRight;
			}

			if (p.Y < MinHeight + this.Box.Padding.TopAndBottom)
			{
				p.Y = MinHeight + this.Box.Padding.TopAndBottom;
			}

			Bounds.Size.Set(p);
		}

		public override void LayoutDockCore()
		{
			this.Bounds.Size.Y = this.MinimumRequiredSize.Y;
			int proposedX = this.Bounds.Size.X;
			if (proposedX < this.MinimumRequiredSize.X)
			{
				proposedX = this.MinimumRequiredSize.X;
			}
			if (this.Stretch == StretchMode.None || this.Stretch == StretchMode.Vertical)
			{
				proposedX = this.MinimumRequiredSize.X;
			}
			this.Bounds.Size.X = proposedX;
		}

		private int mMinWidth = 150;
		public int MinWidth
		{
			get
			{
				return mMinWidth;
			}
			set
			{
				mMinWidth = value;
				Layout();
			}
		}

		private int mMinHeight = 0;
		public int MinHeight
		{
			get
			{
				return mMinHeight;
			}
			set
			{
				mMinHeight = value;
				Layout();
			}
		}

		#endregion

		#region Style

		protected override void InitStyle()
		{
			Style.LineColor = System.Drawing.Color.Transparent;
			Style.FillColor = System.Drawing.Color.Transparent;
		}

		protected override void InitSelectedStyle()
		{
			SelectedStyle.LineColor = System.Drawing.Color.Transparent;
			SelectedStyle.FillColor = System.Drawing.Color.White;
		}

		protected override string StyleName
		{
			get
			{
				return "TextBox";
			}
		}

		#endregion
	}
}