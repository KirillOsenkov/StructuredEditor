using System.Drawing;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public partial class UniversalControl
	{
		#region Design

		public enum UniversalControlDesign
		{
			NoBackground,
            VerticalLine,
            LightHorizontalGradient,
            Rectangle,
			BlockLike,
            Gamma,
			TableLike
		}

		private static UniversalControlDesign mDesign = DefaultDesign;
		public static UniversalControlDesign Design
		{
			get
			{
				return mDesign;
			}
			set
			{
				mDesign = value;
			}
		}

		#endregion

		#region Draw

		private Rect rect1 = new Rect();
		private Rect rect2 = new Rect();

		protected override void DrawBorder(IRenderer Renderer)
		{
			if (Design == UniversalControlDesign.VerticalLine 
				&& !this.IsFocused)
			{
				return;
			}
			base.DrawBorder(Renderer);
		}

		public override void DrawSelectionBorder(IRenderer renderer)
		{
			if (Design == UniversalControlDesign.VerticalLine)
			{
				return;
			}
			base.DrawSelectionBorder(renderer);
		}

		protected override void DrawRectangleLike(IRenderer renderer)
		{
			if (this.LinearLayoutStrategy.Orientation == OrientationType.Horizontal)
			{
				rect1.Size.Y = 0;
				rect2.Set(
					Bounds.Location.X + Box.Borders.Left,
					Bounds.Location.Y + Box.Borders.Top,
					HCompartment.Bounds.Size.X,
					Bounds.Size.Y - Box.Borders.TopAndBottom);
			}
			else
			{
				rect1.Set(
					VMembers.Bounds.Location.X,
					Bounds.Location.Y + Box.Borders.Top,
					Bounds.Right - VMembers.Bounds.Location.X - Box.Borders.Right,
					HCompartment.Bounds.Size.Y + 2);
				rect2.Set(
					Bounds.Location.X + Box.Borders.Left,
					Bounds.Location.Y + Box.Borders.Top,
					rect1.Location.X - Bounds.Location.X - Box.Borders.Left,
					Bounds.Size.Y - Box.Borders.TopAndBottom);
			}

			switch (Design)
			{
				case UniversalControlDesign.BlockLike:
					DrawBlockLike(renderer);
					break;
				case UniversalControlDesign.TableLike:
					DrawTableLike(renderer);
					break;
				case UniversalControlDesign.Rectangle:
					base.DrawRectangleLike(renderer);
					break;
				case UniversalControlDesign.LightHorizontalGradient:
					DrawLightHorizontalGradient(renderer);
					break;
				case UniversalControlDesign.VerticalLine:
					DrawVerticalLine(renderer);
					break;
				case UniversalControlDesign.Gamma:
					DrawGamma(renderer);
					break;
				default:
					break;
			}
		}

		private void DrawBlockLike(IRenderer renderer)
		{
			if (this.Collapsed)
			{
				rect2.Size.Y = rect1.Size.Y;
			}

			rect2.Location.Y += rect1.Size.Y;
			rect2.Size.Y -= rect1.Size.Y;

			renderer.DrawOperations.FillRectangle(
				rect2,
				CurrentStyle.FillStyleInfo);
			Color darker = Colors.ScaleColor(CurrentStyle.FillColor, 0.85f);
			renderer.DrawOperations.DrawRectangle(
				rect2,
				darker
				);
		}

		private void DrawTableLike(IRenderer renderer)
		{
			rect1.Location.X = Bounds.Location.X;
			rect1.Location.Y = Bounds.Location.Y;
			rect1.Size.X = Bounds.Size.X;

			renderer.DrawOperations.DrawFilledRectangle(
				rect1,
				CurrentStyle.LineStyleInfo,
				CurrentStyle.FillStyleInfo);
		}

		private void DrawLightHorizontalGradient(IRenderer renderer)
		{
			rect2.Size.X += 50;
			renderer.DrawOperations.GradientFillRectangle(
				rect2,
				CurrentStyle.FillStyleInfo.FillColor,
				System.Drawing.Color.White,
				System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
		}

		private void DrawVerticalLine(IRenderer renderer)
		{
			if (this.Collapsed)
			{
				return;
			}
			int x = rect2.Location.X;
			int y1 = HCompartment.Bounds.Bottom + HCompartment.Box.Margins.Bottom;
			int y2 = rect2.Bottom;
			if (OpenCurly.Visible)
			{
				y1 += OpenCurly.Bounds.Height;
			}
			if (CloseCurly.Visible)
			{
				y2 -= CloseCurly.Bounds.Height;
			}

			if (OffsetCurlies)
			{
				x += CurrentStyle.FontStyleInfo.Font.SpaceCharSize.X;
			}
			renderer.DrawOperations.DrawLine(
				x,
				y1,
				x,
				y2,
				Color.Gainsboro,
				CurrentStyle.LineWidth);
		}

		private void DrawGamma(IRenderer renderer)
		{
			if (this.LinearLayoutStrategy.Orientation == OrientationType.Vertical)
			{
				rect1.Location.X = this.Bounds.Location.X + this.Box.Borders.Left;
				rect1.Location.Y = this.Bounds.Location.Y + this.Box.Borders.Top;
				rect1.Size.X = this.Bounds.Size.X - this.Box.Borders.LeftAndRight;
				rect1.Size.Y = rect1.Size.Y - this.Box.Borders.TopAndBottom;
				renderer.DrawOperations.FillRectangle(
					rect1,
					Style.FillStyleInfo);
			}

			if (this.Collapsed)
			{
				rect2.Size.Y = rect1.Size.Y;
			}

			rect2.Location.Y += rect1.Size.Y;
			rect2.Size.Y -= rect1.Size.Y;

			renderer.DrawOperations.FillRectangle(
				rect2,
				CurrentStyle.FillStyleInfo.FillColor);
			//Color darker = Colors.ScaleColor(CurrentStyle.FillColor, 0.85f);
			//renderer.DrawOperations.DrawRectangle(
			//    rect2,
			//    darker
			//    );
		}

		#endregion
	}
}