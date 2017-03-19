using System;
using System.Text;

namespace GuiLabs.Canvas.Controls
{
	public class CompositeRange : CompositeNumberRange<double>
	{
		private const double Epsilon = 0.000000001;

		public void SetProportionalSpan(CompositeRange Sample)
		{
			if (Sample.Size == 0 || this.Size == 0)
			{
				return;
			}

			double Factor = this.Size / Sample.Size;
			this.Span.Pos = Math.Round(Sample.Span.Pos * Factor, 6);
			this.Span.Size = Math.Round(Sample.Span.Size * Factor, 6);
			if (this.Span.Size != this.Size 
				&& Math.Abs(this.Span.Size - this.Size) < Epsilon)
			{
				this.Span.Size = this.Size;
			}

			this.CheckSpan();
		}

		public override void CheckSpan()
		{
			// check if the scrollbar has a nonzero size
			if (this.Size <= 0)
			{
				// if not, there'child nothing to talk about
				return;
			}

			// check if the thumb is bigger than the entire scrollbar
			if (this.Span.Size > this.Size)
			{
				this.Span.Pos = 0;
				return;
			}

			// ensure that the thumb isn't too high
			if (this.Span.Pos < Epsilon)
			{
				this.Span.Pos = 0;
			}

			// ensure that the thumb isn't too low
			if (this.Span.Pos + this.Span.Size > this.Size)
			{
				this.Span.Pos = this.Size - this.Span.Size;
			}
		}

		public override string ToString()
		{
			StringBuilder Result = new StringBuilder();
			Result.Append("{");

			Result.Append(this.Pos.ToString());
			Result.Append(", ");
			Result.Append(this.Span.ToString());
			Result.Append(", ");
			Result.Append(this.Size.ToString());
			Result.Append("}");

			return Result.ToString();
		}
	}
}