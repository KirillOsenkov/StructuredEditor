using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public partial class Control
	{
		#region Events

		public event ActivatedHandler ControlActivated;
		protected void RaiseControlActivated(Control control)
		{
			if (ControlActivated != null)
			{
				ControlActivated(control);
			}
		}

		public event ActivatedHandler ControlDeactivated;
		protected void RaiseControlDeactivated(Control control)
		{
			if (ControlDeactivated != null)
			{
				ControlDeactivated(control);
			}
		}

		#endregion

		#region Focus

		private bool mFocusable = false;
		public virtual bool Focusable
		{
			get
			{
				return mFocusable;
			}
			set
			{
				mFocusable = value;
			}
		}

		public bool IsFocused
		{
			get
			{
				if (Root == null)
				{
					return false;
				}
				return Root.FocusControl == this;
			}
		}

		/// <summary>
		/// Tries to set focus to the control.
		/// </summary>
		/// <param name="shouldRedraw">If the draw window should be repainted.</param>
		/// <remarks>The draw window is only repainted when the focus was set successfully</remarks>
		/// <returns>true if the focus was set, otherwise false.</returns>
		public bool SetFocus()
		{
			if (Root == null)
			{
				return false;
			}
			bool result = false;
			// RedrawAccumulator doesn't help here for some reason
			//
			// since SetFocus is an often-called operation
			// using RedrawAccumulator might be expensive here,
			// so I'll leave the straight-forward Root.Redraw() for a while

			//using (RedrawAccumulator r = new RedrawAccumulator(Root, false))
			//{
				result = Root.SetFocus(this);
				if (result)
				{
					//r.ShouldRedrawAtTheEnd = true;
					Root.Redraw();  // comment this when switching to the accumulator
				}
			//}
			return result;
		}

		public bool CanGetFocus
		{
			get
			{
				return Focusable
					&& Visible
					&& Enabled
					&& Root != null;
			}
		}

		public virtual void OnActivate()
		{
			RaiseControlActivated(this);
		}

		public virtual void OnDeactivate()
		{
			RaiseControlDeactivated(this);
		}

		/// <summary>
		/// Searches for a first container control,
		/// which is focusable
		/// </summary>
		/// <returns>Nearest focusable parent or null if none found.</returns>
		public ContainerControl FindNearestFocusableParent()
		{
			ContainerControl current = this.Parent;
			while (current != null
				&& !(current.CanGetFocus)
				)
			{
				current = current.Parent;
			}
			return current;
		}

		#endregion
	}
}
