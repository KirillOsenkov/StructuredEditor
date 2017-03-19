using System;
using System.Text;

namespace GuiLabs.Canvas.Controls
{
	public class ChildNumberRange<T> : INumberRange<T> 
		where T : IComparable<T>
	{
		public ChildNumberRange(CompositeNumberRange<T> ParentRange)
		{
			Parent = ParentRange;
		}

		private CompositeNumberRange<T> mParent;
		public CompositeNumberRange<T> Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent = value;
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
			if (Parent != null)
				Parent.CheckSpan();
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
			if (Parent != null)
				Parent.CheckSpan();
		}

		public override string ToString()
		{
			StringBuilder Result = new StringBuilder();
			Result.Append("(");
			Result.Append(this.Pos.ToString());
			Result.Append(" / ");
			Result.Append(this.Size.ToString());
			Result.Append(")");
			return Result.ToString();
		}
	}
}