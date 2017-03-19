namespace GuiLabs.Canvas.Controls
{
	/// <summary>
	/// Slows down the cursor motion (key repeat rate) in important places
	/// to give the user a chance to release the arrow key
	/// exactly where he/she wants it.
	/// </summary>
	/// <remarks>Consumes 8 KeyDown events with no effect
	/// to give the user a chance to release the key
	/// in an important place.</remarks>
	public class KeyboardBrakeSystem
	{
		public KeyboardBrakeSystem() : this(8)
		{
		}

		public KeyboardBrakeSystem(int delay)
		{
			DefaultDelay = delay;
		}

		private int mCounter = 0;
		/// <summary>
		/// Number of KeyDown events to ignore at an important place
		/// before continuing
		/// </summary>
		public int Counter
		{
			get
			{
				return mCounter;
			}
			set
			{
				mCounter = value;
			}
		}

		private int mDefaultDelay = 0;
		/// <summary>
		/// Default value to initialize the Counter.
		/// </summary>
		/// <remarks>
		/// Number of key clicks to ignore.</remarks>
		public int DefaultDelay
		{
			get
			{
				return mDefaultDelay;
			}
			set
			{
				mDefaultDelay = value;
			}
		}

		/// <summary>
		/// Sets the counter to DefaultDelay,
		/// if it isn't already counting.
		/// </summary>
		public void SetBrake()
		{
			if (Counter == 0)
			{
				Counter = DefaultDelay;
			}
		}

		/// <summary>
		/// Immediately stops the break.
		/// </summary>
		public void ReleaseBrake()
		{
			Counter = 0;
		}

		/// <summary>
		/// Consumes one KeyDown event
		/// Counter--
		/// </summary>
		public void DecreaseCounter()
		{
			if (Counter > 0)
			{
				Counter--;
			}
		}

		/// <summary>
		/// Counter != 0?
		/// </summary>
		private bool BrakeEnabled
		{
			get
			{
				return Counter != 0;
			}
		}

		/// <summary>
		/// Decreases the counter and returns the new value.
		/// </summary>
		/// <returns>New value of the counter.</returns>
		public bool QueryAndDecreaseCounter()
		{
			DecreaseCounter();
			return BrakeEnabled;
		}
	}
}
