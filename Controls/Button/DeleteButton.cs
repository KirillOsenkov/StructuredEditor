using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Controls
{
	public class DeleteButton : Button
	{
		public DeleteButton()
			: base(PictureLibrary.Instance.Delete2)
		{
			this.Box.Margins.SetAll(3);
		}

		protected override void DrawBorder(GuiLabs.Canvas.Renderer.IRenderer Renderer)
		{
			
		}

		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
				Layout();
			}
		}
	}
}
