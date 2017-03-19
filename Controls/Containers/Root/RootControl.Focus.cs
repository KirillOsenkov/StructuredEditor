using GuiLabs.Canvas.Events;
namespace GuiLabs.Canvas.Controls
{
	partial class RootControl
	{
		private Control mFocusControl;
		public Control FocusControl
		{
			get
			{
				return mFocusControl;
			}
		}

		public bool SetFocus(Control newFocus)
		{
			bool result = false;

			//using (RedrawAccumulator r = new RedrawAccumulator(this, false))
			{
				result = SetFocusButDontScroll(newFocus);
				if (result)
				{
					ScrollTo(mFocusControl);
					//r.ShouldRedrawAtTheEnd = true;
				}
			}

			return result;
		}

		private bool SetFocusButDontScroll(Control newFocus)
		{
			if (newFocus == null
				|| !newFocus.CanGetFocus
				|| newFocus == mFocusControl
			)
			{
				return false;
			}

			if (mFocusControl != null)
			{
				mFocusControl.OnDeactivate();
			}
			mFocusControl = newFocus;
			mFocusControl.OnActivate();
			DefaultKeyHandler = mFocusControl;

			return true;
		}

		/// <summary>
		/// Finds and selects a new control to focus based on
		/// where the mouse has clicked
		/// </summary>
		/// <returns>true if anything has been changed</returns>
		private bool FindFocusedControl(MouseWithKeysEventArgs e)
		{
			if (!e.IsLeftButtonPressed)
			{
				return false;
			}
			Control clickedControl = this.FindControlAtPoint(e.X, e.Y);

			if (clickedControl != null
				&& clickedControl != this.FocusControl)
			{
				if (clickedControl.CanGetFocus)
				{
					clickedControl.SetFocus();
					return true;
				}
				else
				{
					clickedControl = clickedControl.FindNearestFocusableParent();
					if (clickedControl != null
						&& clickedControl != this.FocusControl
						&& clickedControl.CanGetFocus)
					{
						clickedControl.SetFocus();
						return true;
					}
				}
			}

			return false;
		}

		///<summary>
		/// Determines if the block or any of its children has focus.
		///</summary>
		/// <remarks>
		/// Reverse iteration: from deepest block (Focus) ---> root
		/// on the way to the root iterator is being compared with "this"
		/// </remarks>
		///<returns>
		/// true if the subtree starting with this block has focus
		///</returns>
		public bool IsFocusInsideControl(Control toCheck)
		{
			Control Current = FocusControl;

			while (Current != null)
			{
				if (Current == toCheck) return true;
				Current = Current.Parent;
			}

			return false;
		}
	}
}