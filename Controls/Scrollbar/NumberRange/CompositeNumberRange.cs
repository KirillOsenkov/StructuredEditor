using System;

namespace GuiLabs.Canvas.Controls
{
	public abstract class CompositeNumberRange<T> : INumberRange<T> where T : IComparable<T>
	{
		public CompositeNumberRange()
		{
			Span = new ChildNumberRange<T>(this);
		}

		private ChildNumberRange<T> mSpan;
		public ChildNumberRange<T> Span
		{
			get
			{
				return mSpan;
			}
			set
			{
				mSpan = value;
			}
		}

		private T mPos;
		public T Pos
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

		public virtual void SetPos(T NewPos)
		{
			Pos = NewPos;
			CheckSpan();
		}

		protected T mSize;
		public T Size
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

		public virtual void SetSize(T NewSize)
		{
			Size = NewSize;
			CheckSpan();
		}

		public abstract void CheckSpan();

	}
}