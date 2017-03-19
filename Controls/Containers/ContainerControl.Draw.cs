using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Canvas.Controls
{
	public abstract partial class ContainerControl : Control
	{
		/// <summary>
		/// Draw this container control, including all children recursively
		/// </summary>
		/// <param name="Renderer">Renderer object to draw on</param>
		public override void DrawCore(IRenderer renderer)
		{
			if (!CanDraw) return;

			// This is very important. All the controls which are currently 
			// invisible (i.e. are not within the viewport) are not drawn.
			// This gives us huge speed benefits when viewing large files.
			if (!this.IsVisibleInViewport(renderer))
			{
				return;
			}
			if (IsFocused)
			{
				DrawRectangleLike(renderer);
				DrawSelectionBorder(renderer);
				DrawChildren(renderer);
				DrawBorder(renderer);
			}
			else
			{
				DrawRectangleLike(renderer);
				DrawBorder(renderer);
				DrawChildren(renderer);
			}
		}

		public virtual void DrawChildren(IRenderer renderer)
		{
			foreach (Control child in Children)
			{
				if (child.Visible)
				{
					child.Draw(renderer);
				}
			}
		}
	}
}
