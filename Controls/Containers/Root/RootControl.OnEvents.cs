using GuiLabs.Canvas.Events;

namespace GuiLabs.Canvas.Controls
{
	partial class RootControl
	{
		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			using (RedrawAccumulator a = new RedrawAccumulator(this))
			{
				base.OnMouseDown(e);
				FindFocusedControl(e);
			}
		}

		public override void OnMouseMove(MouseWithKeysEventArgs e)
		{
			base.OnMouseMove(e);

			// TODO: Saska: Commenting selection out
			// for a stable build
#if false
			if (e.IsLeftButtonPressed)
			{
				Control clickedControl = this.FindControlAtPoint(e.X, e.Y);
				if (clickedControl != null && !(clickedControl is RootControl)) clickedControl.OnMouseMove(e);
				Redraw();
			}
#endif
		}
	}
}