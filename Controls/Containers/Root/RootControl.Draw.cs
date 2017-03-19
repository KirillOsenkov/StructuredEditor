using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	partial class RootControl
	{
		#region Events

		public event RepaintHandler AfterDraw;
		protected void RaiseAfterDraw(IRenderer renderer)
		{
			if (AfterDraw != null)
			{
				AfterDraw(renderer);
			}
		}

		#endregion

		public override void DrawCore(GuiLabs.Canvas.Renderer.IRenderer renderer)
		{
			base.DrawCore(renderer);
			RaiseAfterDraw(renderer);
		}

		#region Redraw

		public override void Redraw()
		{
			if (RedrawRequestsCount == 0)
			{
				RaiseNeedRedraw();
			}
		}

		private int mRedrawRequestsCount = 0;
		public int RedrawRequestsCount
		{
			get
			{
				return mRedrawRequestsCount;
			}
			set
			{
				mRedrawRequestsCount = value;
			}
		}

		#endregion
	}
}