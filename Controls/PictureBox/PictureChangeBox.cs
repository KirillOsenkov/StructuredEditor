using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Controls
{
	public class PictureChangeBox : PictureBox
	{
		public PictureChangeBox(IPicture defaultPicture, IPicture togglePicture)
			: base(defaultPicture)
		{
            TogglePicture = togglePicture;
		}

		private IPicture mTogglePicture;
		public IPicture TogglePicture
		{
			get { return mTogglePicture; }
			set { mTogglePicture = value; }
		}

		//public override void RaiseMouseDown(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		//{
		//    // We need to explicitly call Toggle() each time 
		//    // the thing is toggled

		//    // No need to call it here
		//    //Toggle();
		//    this.RaiseMouseDown(e);
		//}

		public void Toggle()
		{
			IPicture tmp = MyPicture;
			MyPicture = TogglePicture;
			TogglePicture = tmp;
		}
	}
}
