namespace GuiLabs.Canvas.Events
{
	/// <summary>
	/// Can react not only to mouse, but to keyboard as well
	/// Implements IMouseHandler, IKeyHandler together
	/// </summary>
	public class KeyMouseHandler : MouseHandler, IKeyHandler
	{
		#region KeyHandler

		private IKeyHandler mDefaultKeyHandler;
		/// <summary>
		/// If not null, all keyboard events 
		/// are being redirected to this object
		/// </summary>
		public IKeyHandler DefaultKeyHandler
		{
			get
			{
				return mDefaultKeyHandler;
			}
			set
			{
				if (NextHandlerValid(value))
				{
					mDefaultKeyHandler = value;
				}
				else
				{
					mDefaultKeyHandler = null;
				}
			}
		}

		/// <summary>
		/// Can we set such a DefaultKeyHandler?
		/// Prevents endless recursive loops.
		/// </summary>
		/// <param name="nextHandler">Canditate to test</param>
		/// <returns>true, if setting DefaultKeyHandler to nextHandler causes no recursion.</returns>
		public bool NextHandlerValid(IKeyHandler nextHandler)
		{
			// setting to null is perfectly fine
			// (turning off the redirection)
			if (nextHandler == null)
			{
				return true;
			}

			// setting to itself would cause
			// an infinite recursion
			if (nextHandler == this)
			{
				return false;
			}

			IKeyHandler current = nextHandler;
			while (current != null)
			{
				current = current.DefaultKeyHandler;
				if (current == this)
				{
					return false;
				}
			}
			return true;
		}

		public virtual void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (DefaultKeyHandler != null)
			{
				DefaultKeyHandler.OnKeyDown(e);
			}
		}

		public virtual void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if (DefaultKeyHandler != null)
			{
				DefaultKeyHandler.OnKeyPress(e);
			}
		}

		public virtual void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			if (DefaultKeyHandler != null)
			{
				DefaultKeyHandler.OnKeyUp(e);
			}
		}

		#endregion
	}
}