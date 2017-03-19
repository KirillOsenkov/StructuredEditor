using GuiLabs.Utils.Collections;
namespace GuiLabs.Canvas.Controls
{
	public class LinearContainerControl : ContainerControl
	{
		#region ctors

		public LinearContainerControl()
			: base()
		{
			InitLayoutStrategy();
			Init();
		}

		public LinearContainerControl(
			ICollectionWithEvents<Control> bindToCollection)
			: base(bindToCollection)
		{
			InitLayoutStrategy();
			Init();
		}

		public LinearContainerControl(ILinearLayout layout)
			: base()
		{
			this.LinearLayoutStrategy = layout;
			Init();
		}

		private void Init()
		{
			Layout();
			this.Stretch = StretchMode.Both;
		}

		protected virtual void InitLayoutStrategy()
		{
			this.LinearLayoutStrategy = new LinearLayout(OrientationType.Vertical);
		}

		#endregion

		#region LinearLayoutStrategy

		private ILinearLayout mLinearLayoutStrategy;
		public ILinearLayout LinearLayoutStrategy
		{
			get
			{
				return mLinearLayoutStrategy;
			}
			private set
			{
				this.mLinearLayoutStrategy = value;
				this.LayoutStrategy = value;
			}
		}

		#endregion

		#region HitTest

		public override Control FindDirectChildAtPoint(int x, int y)
		{
			if (!this.HitTest(x, y))
			{
				return null;
			}

			Control result = null;
			if (this.LinearLayoutStrategy.Orientation == OrientationType.Horizontal)
			{
				result = FindChildHorizontal(x, y);
			}
			else
			{
				result = FindChildVertical(x, y);
			}

			// if we really found someone exactly under (x,y),
			// of course just return it
			if (result != null)
			{
				return result;
			}

			// the following happens only if we haven't found anything suitable so far

			Control candidate = Children.Tail;
			if (candidate != null
				&& candidate.Enabled
				&& candidate.Visible
				//&& candidate.Stretch != StretchMode.None
				&& this.RedirectHitTestToNearestChild
				)
			{
				return candidate;
			}

			return null;
		}

		public Control FindChildHorizontal(int x, int y)
		{
			foreach (Control child in Children)
			{
				if (this.RedirectHitTestToNearestChild)
				{
					if (child.Bounds.HitTestWithMarginX(x, child.Box.MouseSensitivityArea)
						&& child.Enabled
						&& child.Visible)
					{
						return child;
					}
				}
				else
				{
					if (child.HitTest(x, y))
					{
						return child;
					}
				}
			}

			return null;
		}

		public Control FindChildVertical(int x, int y)
		{
			foreach (Control child in Children)
			{
				if (this.RedirectHitTestToNearestChild)
				{
					if (child.Bounds.HitTestWithMarginY(y, child.Box.MouseSensitivityArea)
						&& child.Enabled
						&& child.Visible)
					{
						return child;
					}
				}
				else
				{
					if (child.HitTest(x, y))
					{
						return child;
					}
				}
			}

			return null;
		}

		#endregion
	}
}
