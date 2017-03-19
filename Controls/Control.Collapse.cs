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

		public event ChangeHandler<Control> CollapseChanged;

		protected void RaiseCollapseChanged()
		{
			if (CollapseChanged != null)
			{
				CollapseChanged(this);
			}
		}

		#endregion

		#region Collapse

		public virtual void Collapse(bool collapse, bool shouldRedraw)
		{
			if (this.Collapsed == collapse
				|| !this.Collapsable)
			{
				return;
			}

			using (RedrawAccumulator a = new RedrawAccumulator(this.Root))
			{
				this.Collapsed = collapse;
				Layout();
				RaiseCollapseChanged();

				// now the logic of removing focus
				// from the collapsed children
				if (this.Root == null)
				{
					return;
				}

				// check if the focus was inside this control
				// and now is hidden
				if (this.Collapsed && Root.IsFocusInsideControl(this))
				{
					this.SetFocus();
				}
			}
		}

		/// <summary>
		/// If the control is currently collapsed.
		/// </summary>
		public bool Collapsed { get; set; }

		/// <summary>
		/// If the control can theoretically be collapsed.
		/// </summary>
		public virtual bool Collapsable { get; set; }

		public virtual void ToggleCollapse(bool shouldRedraw)
		{
			this.Collapse(!this.Collapsed, shouldRedraw);
		}

		/// <summary>
		/// Collapses or expands the current control
		/// and all children recursively
		/// </summary>
		/// <param name="collapse">
		/// true to collapse the whole subtree
		/// false to expand the whole subtree
		/// </param>
		/// <param name="shouldRedraw">
		/// If everything should be redrawn after collapsing/expanding
		/// </param>
		/// <remarks>
		/// Will be overriden in ContainerControl
		/// </remarks>
		public virtual void CollapseAll(bool collapse, bool shouldRedraw)
		{
			Collapse(collapse, shouldRedraw);
		}

		#endregion
	}
}
