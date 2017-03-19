using System;

namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// Guarantees that the screen is redrawn exactly one time
	/// when the RedrawAccumulator is disposed or the using block is finished.
	/// </summary>
	/// <example>
	/// using(RedrawAccumulator ... ) { ... }
	/// </example>
	public class RedrawAccumulator : IDisposable
	{
		#region ctors

		protected RedrawAccumulator()
		{

		}

		public RedrawAccumulator(RootControl rootControl)
			: this(rootControl, true)
		{

		}

		public RedrawAccumulator(RootControl rootControl, bool shouldRedrawAtTheEnd)
		{
			Root = rootControl;
			if (Root != null)
			{
				Root.RedrawRequestsCount++;
			}
			this.ShouldRedrawAtTheEnd = shouldRedrawAtTheEnd;
		}

		#endregion

		private bool mShouldRedrawAtTheEnd = true;
		public bool ShouldRedrawAtTheEnd
		{
			get
			{
				return mShouldRedrawAtTheEnd;
			}
			set
			{
				mShouldRedrawAtTheEnd = value;
			}
		}

		private RootControl mRoot;
		public RootControl Root
		{
			get
			{
				return mRoot;
			}
			set
			{
				mRoot = value;
			}
		}

		public virtual void Dispose()
		{
			if (Root == null)
			{
				return;
			}
			Root.RedrawRequestsCount--;
			if (Root.RedrawRequestsCount == 0 && ShouldRedrawAtTheEnd)
			{
				Root.Redraw();
			}
		}
	}
}
