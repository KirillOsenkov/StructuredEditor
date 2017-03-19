namespace GuiLabs.Canvas.DrawStyle
{
	public class BoxLayoutParameters
	{
		private FourSideValues mMouseSensitivityArea = new FourSideValues();
		/// <summary>
		/// Additional area around this.Bounds where mouse clicks get HitTested
		/// by this control. Similar to Margins, but independent of it.
		/// Margins control visual Margins, and MouseSensitivity control mouse behaviour.
		/// </summary>
		public FourSideValues MouseSensitivityArea
		{
			get
			{
				return mMouseSensitivityArea;
			}
			set
			{
				mMouseSensitivityArea = value;
			}
		}

		public void SetMouseSensitivityToMargins()
		{
			MouseSensitivityArea.Set(Margins);
		}

		private FourSideValues mMargins = new FourSideValues();
		/// <summary>
		/// Margins are outer spacing between the border and neighbor shapes.
		/// Margins are external to the box's bounds.
		/// </summary>
		public FourSideValues Margins
		{
			get
			{
				return mMargins;
			}
			set
			{
				mMargins = value;
			}
		}

		private FourSideValues mPadding = new FourSideValues();
		/// <summary>
		/// Padding is inner spacing between the border and child shapes.
		/// </summary>
		public FourSideValues Padding
		{
			get
			{
				return mPadding;
			}
			set
			{
				mPadding = value;
			}
		}

		private FourSideValues mBorders = new FourSideValues(1,1,1,1);
		/// <summary>
		/// Borders specify the thickness of the borders of this box.
		/// Borders are considered to belong to the box within its bounds.
		/// </summary>
		public FourSideValues Borders
		{
			get
			{
				return mBorders;
			}
			set
			{
				mBorders = value;
			}
		}
	}
}