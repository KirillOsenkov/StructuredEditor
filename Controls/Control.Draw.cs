using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public partial class Control : ShapeWithEvents
	{
		#region Draw

		public bool CanDraw
		{
			get
			{
				return this.Visible && this.CurrentStyle != null;
			}
		}

		/// <summary>
		/// The basic control draw routine.
		/// Draws background and border, if necessary.
		/// </summary>
		/// <remarks>
		/// Use Control.ShouldDrawBackground and
		/// Control.ShouldDrawBorder to allow/prohibit
		/// drawing of background/border
		/// </remarks>
		/// <param name="Renderer">Renderer to draw on.</param>
		public override void DrawCore(IRenderer renderer)
		{
			if (!CanDraw) return;
			DrawRectangleLike(renderer);
			DrawBorder(renderer);
		}

		#region Shadow

		private static bool mShouldDrawSelectionShadow = true;
		public static bool ShouldDrawSelectionShadow
		{
			get
			{
				return mShouldDrawSelectionShadow;
			}
			set
			{
				mShouldDrawSelectionShadow = value;
			}
		}

		public virtual void DrawSelectionBorder(IRenderer renderer)
		{
			if (this.IsFocused && ShouldDrawSelectionShadow)
			{
				//Renderer.DrawOperations.DrawCaret(
				//    this.Bounds.Location.X,
				//    this.Bounds.Location.Y,
				//    Bounds.Size.Y);
				renderer.DrawOperations.DrawShadow(this.Bounds);
			}
		}

		#endregion

		#region Background

		private Rect backgroundRect = new Rect();
		protected virtual void DrawRectangleLike(IRenderer Renderer)
		{
			if (CurrentStyle.FillStyleInfo == null 
				|| !ShouldDrawBackground
				|| CurrentStyle.FillColor == System.Drawing.Color.Transparent)
			{
				return;
			}

			backgroundRect.Set(
				Bounds.Location.X + Box.Borders.Left,
				Bounds.Location.Y + Box.Borders.Top,
				Bounds.Size.X - Box.Borders.LeftAndRight,
				Bounds.Size.Y - Box.Borders.TopAndBottom);
			Renderer.DrawOperations.FillRectangle(
				backgroundRect,
				CurrentStyle.FillStyleInfo);
		}

		private bool mShouldDrawBackground = true;
		public bool ShouldDrawBackground
		{
			get
			{
				return mShouldDrawBackground;
			}
			set
			{
				mShouldDrawBackground = value;
			}
		}

		#endregion

		#region Border

		private bool mShouldDrawBorder = true;
		public bool ShouldDrawBorder
		{
			get
			{
				return mShouldDrawBorder;
			}
			set
			{
				mShouldDrawBorder = value;
			}
		}

		protected virtual void DrawBorder(IRenderer Renderer)
		{
			if (CurrentStyle.LineStyleInfo == null 
				|| CurrentStyle.LineColor == System.Drawing.Color.Transparent
				|| !ShouldDrawBorder)
			{
				return;
			}

			Renderer.DrawOperations.DrawRectangle(
				this.Bounds,
				this.CurrentStyle.LineStyleInfo);
		}

		#endregion

		#region Redraw

		public virtual void Redraw()
		{
			if (this.Root != null)
			{
				this.Root.Redraw();
			}
			else if (this.Parent != null)
			{
				this.Parent.Redraw();
			}
			else
			{
				this.RaiseNeedRedraw();
			}
		}

		#endregion

		#endregion

		#region Box

		private readonly BoxLayoutParameters mBox = new BoxLayoutParameters();
		public BoxLayoutParameters Box
		{
			get
			{
				return mBox;
			}
		}

		public void CalcSizeWithMargins(Point originalSize, Point result)
		{
			result.Set(
				originalSize.X + Box.Margins.LeftAndRight,
				originalSize.Y + Box.Margins.TopAndBottom);
		}

		#endregion

		#region HitTest

		public override bool HitTest(int x, int y)
		{
			return this.Visible
				&& this.Bounds.HitTestWithMargin(x, y, this.Box.MouseSensitivityArea)
				&& this.Enabled;
		}

		public virtual Control FindControlAtPoint(int x, int y)
		{
			return this;
			//if (this.HitTest(x, y))
			//{
			//    return this;
			//}

			//return null;
		}


		#endregion
	}
}
