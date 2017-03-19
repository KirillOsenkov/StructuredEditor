using GuiLabs.Utils.Collections;

namespace GuiLabs.Canvas.Controls
{
	public abstract partial class ContainerControl : Control
	{
		#region ctors

		/// <summary>
		/// Creates a container control with its own children list.
		/// Methods ContainerControl.Add and ContainerControl.Remove
		/// can be used to add or remove child controls.
		/// </summary>
		public ContainerControl()
			: base()
		{
			ChildrenCollection = new CollectionWithEvents<Control>();
			Children = ChildrenCollection;
		}

		/// <summary>
		/// Creates a container control based on a given list of controls.
		/// All controls in the list are treated 
		/// as own children of this ContainerControl.
		/// </summary>
		/// <param name="bindToCollection"></param>
		public ContainerControl(ICollectionWithEvents<Control> bindToCollection)
			: base()
		{
			Children = bindToCollection;
		}

		#endregion

		#region Root

		/// <summary>
		/// When acquiring a reference to the new Root
		/// also recursively update all the children
		/// to point to the same root
		/// </summary>
		public override RootControl Root
		{
			get
			{
				return base.Root;
			}
			set
			{
				base.Root = value;
				foreach (Control child in Children)
				{
					child.Root = value;
				}
			}
		}

		#endregion

		#region Children

		private ICollectionWithEvents<Control> mChildren;
		/// <summary>
		/// Reference to a collection of children to display in this ContainerControl.
		/// It can be our own collection of controls or a collection
		/// provided, for example, by a ContainerBlock.
		/// </summary>
		public ICollectionWithEvents<Control> Children
		{
			get
			{
				return mChildren;
			}
			set
			{
				// release the old children collection before we get the new one
				if (mChildren != null)
				{
					mChildren.ElementAdded -= Children_ElementAdded;
					mChildren.ElementRemoved -= Children_ElementRemoved;
					mChildren.ElementReplaced -= Children_ElementReplaced;
				}

				// acquire the new children collection
				mChildren = value;

				// subscribe to the events of the new children collection
				if (mChildren != null)
				{
					mChildren.ElementAdded += Children_ElementAdded;
					mChildren.ElementRemoved += Children_ElementRemoved;
					mChildren.ElementReplaced += Children_ElementReplaced;
				}
			}
		}

		private void Children_ElementAdded(Control childControl)
		{
			SubscribeItem(childControl);
			OnSizeChanged();
		}

		private void Children_ElementReplaced(Control oldControl, Control newControl)
		{
			UnsubscribeItem(oldControl);
			SubscribeItem(newControl);
			OnSizeChanged();
		}

		private void Children_ElementRemoved(Control childControl)
		{
			UnsubscribeItem(childControl);
			OnSizeChanged();
		}

		private void SubscribeItem(Control childControl)
		{
			childControl.SizeChanged += Child_SizeChanged;
		}

		private void UnsubscribeItem(Control childControl)
		{
			childControl.SizeChanged -= Child_SizeChanged;
		}

		#endregion

		#region ChildrenCollection

		private CollectionWithEvents<Control> mChildrenCollection;
		/// <summary>
		/// Our own list of Controls.
		/// </summary>
		protected CollectionWithEvents<Control> ChildrenCollection
		{
			get
			{
				return mChildrenCollection;
			}
			set
			{
				mChildrenCollection = value;
			}
		}

		#region Add, Remove

		/// <summary>
		/// Add a control to this CompositeControl.
		/// </summary>
		/// <remarks>
		/// CompositeControl isn't resized if SuspendLayout == true.
		/// </remarks>
		/// <param name="newShape">Control to add to this collection.</param>
		public void Add(Control newControl)
		{
			if (ChildrenCollection != null)
			{
				ChildrenCollection.Add(newControl);
				newControl.Parent = this;
				newControl.Root = this.Root;
			}
		}

		public void Prepend(Control newControl)
		{
			if (ChildrenCollection != null)
			{
				ChildrenCollection.Prepend(newControl);
				newControl.Parent = this;
				newControl.Root = this.Root;
			}
		}

		/// <summary>
		/// Removes an existing control from this CompositeControl.
		/// </summary>
		/// <remarks>
		/// CompositeControl isn't resized if SuspendLayout == true.
		/// </remarks>
		/// <param name="newShape">Control to add to this collection.</param>
		public void Remove(Control controlToRemove)
		{
			if (ChildrenCollection != null)
			{
				ChildrenCollection.Remove(controlToRemove);
				controlToRemove.Root = null;
				controlToRemove.Parent = null;
			}
		}

		#endregion

		#endregion

		#region CollapseAll

		public override void CollapseAll(bool collapse, bool shouldRedraw)
		{
			CollapseAllChildren(collapse);

			Collapse(collapse, false);

			if (shouldRedraw)
			{
				this.Redraw();
			}
		}

		private void CollapseAllChildren(bool collapse)
		{
			foreach (Control child in this.Children)
			{
				child.CollapseAll(collapse, false);
			}
		}

		#endregion

		#region Move

		public override void Move(int deltaX, int deltaY)
		{
			if (deltaX == 0 && deltaY == 0)
			{
				return;
			}
			this.Bounds.Location.Add(deltaX, deltaY);
			foreach (Control child in Children)
			{
				child.Move(deltaX, deltaY);
			}
		}

		#endregion
	}
}
