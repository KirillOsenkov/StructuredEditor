namespace GuiLabs.Canvas.Events
{
	/// <summary>
	/// Default implementation for IMouseHandler
	/// Implements "Chain of responsibility" design pattern
	/// </summary>
	public class MouseHandler : IMouseHandler
	{
		#region MouseHandler

		protected IMouseHandler mDefaultMouseHandler;
		/// <summary>
		/// If not null, all mouse events are being redirected to this object
		/// </summary>
		public virtual IMouseHandler DefaultMouseHandler
		{
			get
			{
				return mDefaultMouseHandler;
			}
			set
			{
				if (NextHandlerValid(value))
				{
					mDefaultMouseHandler = value;
				}
				else
				{
					mDefaultMouseHandler = null;
				}
			}
		}

		/// <summary>
		/// Can we set such a DefaultMouseHandler?
		/// Prevents endless recursive loops.
		/// </summary>
		/// <param name="nextHandler">Canditate to test</param>
		/// <returns>true, if setting DefaultMouseHandler to nextHandler causes no recursion.</returns>
		public bool NextHandlerValid(IMouseHandler nextHandler)
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
			
			IMouseHandler current = nextHandler;
			while (current != null)
			{
				current = current.DefaultMouseHandler;
				if (current == this)
				{
					return false;
				}
			}

			return true;
		}

		public virtual void OnClick(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnClick(e);
			}
		}

		public virtual void OnDoubleClick(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnDoubleClick(e);
			}
		}

		public virtual void OnMouseDown(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseDown(e);
			}
		}

		/// <summary>
		/// Occures when the user hovers the mouse over the block.
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnMouseHover(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseHover(e);
			}
		}

		public virtual void OnMouseMove(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseMove(e);
			}
		}

		public virtual void OnMouseUp(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseUp(e);
			}
		}

		public virtual void OnMouseWheel(MouseWithKeysEventArgs e)
		{
			if (DefaultMouseHandler != null)
			{
				DefaultMouseHandler.OnMouseWheel(e);
			}
		}

		#endregion
	}
}