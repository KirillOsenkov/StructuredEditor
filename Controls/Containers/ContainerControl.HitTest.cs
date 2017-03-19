using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public abstract partial class ContainerControl : Control
	{
		#region HitTest

		public override Control FindControlAtPoint(int x, int y)
		{
			Control childAtPoint = FindDirectChildAtPoint(x, y);
			if (childAtPoint != null)
			{
				return childAtPoint.FindControlAtPoint(x, y);
			}

			if (this.HitTest(x, y))
			{
				return this;
			}

			return null;
		}

		public virtual Control FindDirectChildAtPoint(int x, int y)
		{
			foreach (Control child in this.Children.Reversed)
			{
				if (child.HitTest(x, y))
				{
					return child;
				}
			}

			return null;
		}

		//public override IShape HitTestOld(int x, int y)
		//{
		//    IShape result = List.HitTest(x, y);
		//    if (result == List)
		//    {
		//        result = this;
		//        if (!ShouldHittestItself)
		//        {
		//            result = null;
		//        }
		//    }
		//    return result;
		//}

		/// <summary>
		/// Just a shortcut for "Enabled AND Visible" condition
		/// </summary>
		protected virtual bool ShouldHittestItself
		{
			get
			{
				return Enabled && Visible;
			}
		}

		private bool mRedirectHitTestToNearestChild = false;
		/// <summary>
		/// A property of ContainerControl which determines,
		/// if a click on the control, which doesn't hit any child,
		/// should anyways be redirected to the nearest child.
		/// </summary>
		public bool RedirectHitTestToNearestChild
		{
			get
			{
				return mRedirectHitTestToNearestChild;
			}
			set
			{
				mRedirectHitTestToNearestChild = value;
			}
		}

		/// <summary>
		///		Tests if a point is inside this shape or its children
		/// </summary>
		/// <param name="HitPoint"></param>
		///		The point to check
		/// <returns>
		///		Reference to a child in which the point is (recursive),
		///		this, if the point is inside this shape but inside none of the children
		///		null else
		/// </returns>
		//public override IShape HitTestShapeList(int x, int y)
		//{
		//    if (!this.Bounds.HitTest(x, y)
		//        || !this.Visible
		//        || !this.Enabled)
		//    {
		//        return null;
		//    }

		//    IShape foundChild = HitTestChildrenOnly(x, y);
		//    return foundChild != null ? foundChild : this;
		//}

		/// <summary>
		/// Finds a subchild that contains a point
		/// </summary>
		/// <param name="x">x-coordinate of a hit test point</param>
		/// <param name="y">y-coordinate of a hit test point</param>
		/// <returns>Found child. null if none found.</returns>
		//public IShape HitTestChildrenOnlyShapeList(int x, int y)
		//{
		//    foreach (T s in Children.Reversed)
		//    {
		//        IShape found = s.HitTest(x, y);
		//        if (found != null) //  && found.Visible && found.Enabled
		//        {
		//            return found;
		//        }
		//    }

		//    return null;
		//}

		/// <summary>
		/// Different from HitTestChildrenOnly,
		/// it returns not the deepest child actually found
		/// but immediate child of this control which has the mouse
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private IShape ShapeToForwardMouseEventTo(MouseWithKeysEventArgs e)
		{
			return FindDirectChildAtPoint(e.X, e.Y);
		}

		#endregion
	}
}
