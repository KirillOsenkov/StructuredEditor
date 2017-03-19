namespace GuiLabs.Canvas.DrawOperations
{
	public class TranslateTransform : TransformDrawOperations
	{
		public TranslateTransform()
			: base()
		{
			mDelta = Viewport.Location;
		}

		protected override void TransformPoint(Point src, Point dst)
		{
			dst.X = src.X - mDelta.X;
			dst.Y = src.Y - mDelta.Y;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Has to be public, 
		/// because its called from ScrollableContainer.DeTransform</remarks>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		public override void DeTransformPoint(Point src, Point dst)
		{
			dst.X = src.X + mDelta.X;
			dst.Y = src.Y + mDelta.Y;
		}

		private Point mDelta;
		private Point Delta
		{
			get
			{
				return mDelta;
			}
		}

		public int DeltaX
		{
			get
			{
				return mDelta.X;
			}
			set
			{
				mDelta.X = value;
			}
		}

		public int DeltaY
		{
			get
			{
				return mDelta.Y;
			}
			set
			{
				mDelta.Y = value;
			}
		}
	}
}