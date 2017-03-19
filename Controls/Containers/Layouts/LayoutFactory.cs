namespace GuiLabs.Canvas.Controls
{
	public class LayoutFactory
	{
		#region Horizontal

		private static ILayout mHorizontal;
		public static ILayout Horizontal
		{
			get
			{
				if (mHorizontal == null)
				{
					mHorizontal = new LinearLayout(OrientationType.Horizontal);
				}
				return mHorizontal;
			}
		}

		private static ILayout mHorizontalIndented;
		public static ILayout HorizontalIndented
		{
			get
			{
				if (mHorizontalIndented == null)
				{
					mHorizontalIndented = new LinearLayout(OrientationType.Horizontal, AlignmentType.LeftOrTopEdge, 8, 0);
				}
				return mHorizontalIndented;
			}
		}

		#endregion

		#region Vertical

		private static ILayout mVertical;
		public static ILayout Vertical
		{
			get
			{
				if (mVertical == null)
				{
					mVertical = new LinearLayout(OrientationType.Vertical);
				}
				return mVertical;
			}
		}

		private static ILayout mVerticalIndented;
		public static ILayout VerticalIndented
		{
			get
			{
				if (mVerticalIndented == null)
				{
					mVerticalIndented = new LinearLayout(OrientationType.Vertical, AlignmentType.LeftOrTopEdge, 0, 8);
				}
				return mVerticalIndented;
			}
		}

		#endregion
	}
}
