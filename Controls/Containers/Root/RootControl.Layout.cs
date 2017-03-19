namespace GuiLabs.Canvas.Controls
{
	partial class RootControl
	{
		private bool mStretchToWindow = true;
		public bool StretchToWindow
		{
			get
			{
				return mStretchToWindow;
			}
			set
			{
				mStretchToWindow = value;
			}
		}

		public override void Layout()
		{
			base.Layout();

			if (this.DefaultView != null && this.StretchToWindow)
			{
				Point clientSize = this.DefaultView.GetClientSize();
				this.Bounds.Size.Set(clientSize);
				LayoutDock();
			}
		}

		//public override void LayoutCore()
		//{
		//    base.LayoutCore();
		//    if (this.DefaultView != null)
		//    {
		//        this.Bounds.Size.Set(this.DefaultView.Bounds.Size);
		//        LayoutDock();
		//    }
		//}

		//public override void LayoutDockCore()
		//{
		//    if (DefaultView != null)
		//    {
		//        Point recommendedSize = DefaultView.GetClientSize();

		//        if (recommendedSize.X > this.MinimumRequiredSize.X)
		//        {
		//            this.Bounds.Size.X = recommendedSize.X;
		//        }
		//        else
		//        {
		//            this.Bounds.Size.X = this.MinimumRequiredSize.X;
		//        }
		//        if (recommendedSize.Y > this.MinimumRequiredSize.Y)
		//        {
		//            this.Bounds.Size.Y = recommendedSize.Y;
		//        }
		//        else
		//        {
		//            this.Bounds.Size.X = this.MinimumRequiredSize.Y;
		//        }
		//    }
		//    base.LayoutDockCore();
		//}
	}
}