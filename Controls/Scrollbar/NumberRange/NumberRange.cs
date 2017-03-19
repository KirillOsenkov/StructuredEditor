using System;

namespace GuiLabs.Canvas.Controls
{
	internal class NumberRange<T> where T : IComparable<T>
	{
		public NumberRange()
		{

		}

		private T mPos;
		public virtual T Pos
		{
			get
			{
				return mPos;
			}
			set
			{
				mPos = value;
			}
		}

		private T mSize;
		public virtual T Size
		{
			get
			{
				return mSize;
			}
			set
			{
				mSize = value;
			}
		}
	}
}