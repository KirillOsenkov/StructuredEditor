using GuiLabs.Canvas.Events;

namespace GuiLabs.Canvas.Controls
{
	internal class ArrowButton : ScrollbarButton
	{
		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			base.OnMouseUp(e);
			State = ButtonState.Pushed;
			Redraw();
			RaiseMouseDown(e);
		}

		public override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			base.OnMouseUp(e);
			State = ButtonState.Normal;
			Redraw();
			RaiseMouseUp(e);
		}
	}
}