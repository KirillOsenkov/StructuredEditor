using System;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Canvas.Controls
{
	public partial class Control : ShapeWithEvents
	{
		#region Events

		public event ChangeHandler<Control> VisibleChanged;
		protected void RaiseVisibleChanged()
		{
			if (VisibleChanged != null)
			{
				VisibleChanged(this);
			}
		}

		#endregion

		#region Visibility

		public bool IsVisible(Rect Window)
		{
			return this.Bounds.IntersectsRect(Window);
		}

		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				if (mVisible != value)
				{
					mVisible = value;
					RaiseVisibleChanged();
				}
			}
		}

		public bool IsVisible()
		{
			Control current = this;
			while (current != null)
			{
				if (!current.Visible)
				{
					return false;
				}
				//if (current != this && current.Collapsed)
				//{
				//    return false;
				//}
				current = current.Parent;
			}
			return true;
		}

		/// <summary>
		/// Returns itself if visible,
		/// otherwise finds closest parent which is visible
		/// </summary>
		/// <returns>Returns closest parent which is visible, 
		/// returns this if itself is visible</returns>
		public Control FindFirstVisibleParent()
		{
			Control current = this;
			while (!current.IsVisible())
			{
				current = current.Parent;
			}
			return current;
		}

		#endregion
	}
}
