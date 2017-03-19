using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public abstract partial class ContainerControl : Control
	{
		#region Layout

		private void OnSizeChanged()
		{
			Layout();
		}

		private void Child_SizeChanged(IShape ResizedShape, Point OldSize)
		{
			OnSizeChanged();
		}

		private ILayout mLayoutStrategy;
		public ILayout LayoutStrategy
		{
			get { return mLayoutStrategy; }
			set
			{
				mLayoutStrategy = value;
			}
		}

		public override void LayoutCore()
		{
			if (LayoutStrategy != null)
			{
				LayoutStrategy.Layout(this);
			}
		}

		public override void LayoutDockCore()
		{
			if (LayoutStrategy != null)
			{
				LayoutStrategy.LayoutDock(this);
			}
		}

		private Point mBiggestMinimumChildSize = new Point();
		public Point BiggestMinimumChildSize()
		{
			int maxX = 0;
			int maxY = 0;

			Point childSize = new Point();

			foreach (Control child in this.Children)
			{
				if (child.Visible)
				{
					child.CalcSizeWithMargins(child.MinimumRequiredSize, childSize);

					if (childSize.X > maxX)
					{
						maxX = childSize.X;
					}
					if (childSize.Y > maxY)
					{
						maxY = childSize.Y;
					}
				}
			}

			mBiggestMinimumChildSize.Set(maxX, maxY);

			return mBiggestMinimumChildSize;
		}

		public override void LayoutAll()
		{
			foreach (Control child in this.Children)
			{
				child.LayoutAll();
			}
			base.LayoutAll();
		}

		#region SuspendLayout

		private bool mSuspendLayout = false;
		/// <summary>
		/// Set SuspendLayout to true, when you add new controls
		/// and don't want Layout() to be called, before 
		/// all the controls are added.
		/// </summary>
		/// <remarks>
		/// When you set it back to false at the end,
		/// Layout() is called.
		/// </remarks>
		public bool SuspendLayout
		{
			get
			{
				return mSuspendLayout;
			}
			set
			{
				mSuspendLayout = value;

				if (ChildrenCollection != null)
				{
					ChildrenCollection.SuspendEvents = value;
				}
			}
		}

		#endregion

		#endregion
	}
}
