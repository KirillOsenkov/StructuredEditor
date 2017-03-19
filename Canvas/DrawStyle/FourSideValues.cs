namespace GuiLabs.Canvas.DrawStyle
{
	public class FourSideValues
	{
		public FourSideValues()
		{

		}

		public FourSideValues(int left, int top, int right, int bottom)
		{
			mLeft = left;
			mTop = top;
			mRight = right;
			mBottom = bottom;
		}

		private int mLeft;
		public int Left
		{
			get
			{
				return mLeft;
			}
			set
			{
				mLeft = value;
			}
		}

		private int mTop;
		public int Top
		{
			get
			{
				return mTop;
			}
			set
			{
				mTop = value;
			}
		}

		private int mRight;
		public int Right
		{
			get
			{
				return mRight;
			}
			set
			{
				mRight = value;
			}
		}

		private int mBottom;
		public int Bottom
		{
			get
			{
				return mBottom;
			}
			set
			{
				mBottom = value;
			}
		}

		public int LeftAndRight
		{
			get
			{
				return Left + Right;
			}
		}

		public int TopAndBottom
		{
			get
			{
				return Top + Bottom;
			}
		}

		public void Set(FourSideValues copyFrom)
		{
			Left = copyFrom.Left;
			Top = copyFrom.Top;
			Right = copyFrom.Right;
			Bottom = copyFrom.Bottom;
		}

		public void SetAll(int value)
		{
			Left = value;
			Top = value;
			Right = value;
			Bottom = value;
		}

		public void SetAll(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public void SetLeftAndRight(int value)
		{
			Left = value;
			Right = value;
		}

		public void SetTopAndBottom(int value)
		{
			Top = value;
			Bottom = value;
		}

		public void SetLeftAndTop(int value)
		{
			Left = value;
			Top = value;
		}

		public void SetRightAndBottom(int value)
		{
			Right = value;
			Bottom = value;
		}

		public void SetLeftAndRight(int left, int right)
		{
			Left = left;
			Right = right;
		}

		public void SetTopAndBottom(int top, int bottom)
		{
			Top = top;
			Bottom = bottom;
		}
	}
}