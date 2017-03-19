using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.Actions;
using GuiLabs.Undo;
using GuiLabs.Utils.Delegates;
using GuiLabs.Utils.Commands;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	/// <summary>
	/// Base class for all blocks
	/// </summary>
	public abstract partial class Block
	{
		#region Events

		public event BlockKeyDownEventHandler KeyDown;
		protected void RaiseKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (KeyDown != null && !e.Handled)
			{
				KeyDown(this, e);
			}
		}

		public event EmptyHandler ParentChanged;
		protected void RaiseParentChanged()
		{
			if (ParentChanged != null)
			{
				ParentChanged();
			}
		}

		public event EmptyHandler RootChanged;
		protected void RaiseRootChanged()
		{
			if (RootChanged != null)
			{
				RootChanged();
			}
		}

		public event ChangeHandler<Block> VisibleChanged;
		protected void RaiseVisibleChanged()
		{
			if (VisibleChanged != null)
			{
				VisibleChanged(this);
			}
		}

		public event ChangeHandler<Block> Deleted;
		protected void RaiseDeleted()
		{
			if (Deleted != null)
			{
				Deleted(this);
			}
		}

		#endregion

		#region Parent & Root

		private ContainerBlock mParent;
		/// <summary>
		/// Parent Block of this Block.
		/// </summary>
		public ContainerBlock Parent
		{
			get
			{
				return mParent;
			}
			internal set
			{
				mParent = value;
			}
		}

		internal void NotifyParentChanged(ContainerBlock oldParent)
		{
			OnParentChanged(oldParent, mParent);
			RaiseParentChanged();
		}
		
		internal void NotifyRootChanged(RootBlock oldRoot)
		{
			if (oldRoot == mRoot)
			{
				return;
			}
			OnRootChanged(oldRoot, mRoot);
			RaiseRootChanged();
		}

		/// <summary>
		/// Returns this.Parent.Parent; can be null.
		/// </summary>
		/// <remarks>Doesn't throw; just returns null.</remarks>
		/// <exception cref="Doesn't throw"></exception>
		public ContainerBlock ParentParent
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.Parent;
				}
				return null;
			}
		}

		private RootBlock mRoot;
		public virtual RootBlock Root
		{
			get
			{
				return mRoot;
			}
			internal set
			{
				mRoot = value;
			}
		}
		
		public bool IsInSubtreeOf(ContainerBlock someParent)
		{
			Block current = this;
			while (current != null)
			{
				if (current == someParent)
				{
					return true;
				}
				current = current.Parent;
			}
			return false;
		}

		public bool IsInStrictSubtreeOf(ContainerBlock someParent)
		{
			ContainerBlock current = this.Parent;
			while (current != null)
			{
				if (current == someParent)
				{
					return true;
				}
				current = current.Parent;
			}
			return false;
		}

		#endregion

		public IEnumerable<T> FindAllContainingBlocks<T>()
			where T : class
		{
			ContainerBlock current = this.Parent;
			while (current != null)
			{
				T nextFound = current as T;
				if (nextFound != null)
				{
					yield return nextFound;
				}
				current = current.Parent;
			}
		}

		#region ActionManagerProvider

		public virtual ActionManager ActionManager
		{
			get
			{
				if (Root != null)
				{
					return Root.ActionManager;
				}
				return null;
			}
		}

		/// <summary>
		/// Records and runs the action if this.Root != null
		/// and simply runs it when this.Root == null
		/// </summary>
		/// <param name="action">An action to (record and) execute</param>
		public void RunAction(IAction action)
		{
			if (Root != null)
			{
				Root.ActionManager.RecordAction(action);
			}
			else
			{
				action.Execute();
			}
		}

		#endregion

		#region Linked List Node (Prev, Next...)

		private Block mPrev;
		public Block Prev
		{
			get
			{
				return mPrev;
			}
			set
			{
				mPrev = value;
			}
		}

		private Block mNext;
		public Block Next
		{
			get
			{
				return mNext;
			}
			set
			{
				mNext = value;
			}
		}

		#region GetNeighborBlocks

		public IEnumerable<Block> GetNeighborBlocks(int from, int to)
		{
			Block current = this.GetNeighborBlock(from);
			while (from <= to && current != null)
			{
				yield return current;
				current = current.Next;
				from++;
			}
		}

		public Block GetNeighborBlock(int offsetFromCurrentBlock)
		{
			Block current = this;

			while (offsetFromCurrentBlock < 0 && current != null)
			{
				current = current.Prev;
				offsetFromCurrentBlock++;
			}
			while (offsetFromCurrentBlock > 0 && current != null)
			{
				current = current.Next;
				offsetFromCurrentBlock--;
			}

			return current;
		}

		#endregion

		/// <summary>
		/// Establishes a two-way link between
		/// this node and its previous node,
		/// if the previous node exists
		/// </summary>
		/// <param name="PrevNode"></param>
		internal void LinkToPrev(Block prevNode)
		{
			this.Prev = prevNode;
			if (prevNode != null)
			{
				prevNode.Next = this;
			}
		}

		/// <summary>
		/// Establishes a two-way link between
		/// this node and its next node
		/// if the next node exists
		/// </summary>
		/// <param name="NextNode"></param>
		internal void LinkToNext(Block NextNode)
		{
			this.Next = NextNode;
			if (NextNode != null)
			{
				NextNode.Prev = this;
			}
		}

		#endregion

		#region Control

		/// <summary>
		/// abstract means no implementation provided
		/// Implementation must be provided in derived classes
		/// </summary>
		public abstract Control MyControl
		{
			get;
		}

		protected virtual void SubscribeControl()
		{
			MyControl.ControlActivated += OnActivated;
			MyControl.ControlDeactivated += OnDeactivated;
			MyControl.Click += OnClick;
			MyControl.DoubleClick += OnDoubleClick;
			MyControl.MouseDown += OnMouseDown;
			MyControl.MouseMove += OnMouseMove;
			MyControl.MouseUp += OnMouseUp;
			MyControl.KeyDown += OnKeyDown;
			MyControl.KeyPress += OnKeyPress;
			MyControl.KeyUp += OnKeyUp;
			MyControl.VisibleChanged += OnVisibleChanged;
			MyControl.CollapseChanged += OnCollapseChanged;
			InitStyle();
		}

		protected virtual void OnCollapseChanged(Control itemChanged)
		{

		}

		protected virtual void UnSubscribeControl()
		{
			MyControl.ControlActivated -= OnActivated;
			MyControl.ControlDeactivated -= OnDeactivated;
			MyControl.Click -= OnClick;
			MyControl.DoubleClick -= OnDoubleClick;
			MyControl.MouseDown -= OnMouseDown;
			MyControl.MouseMove -= OnMouseMove;
			MyControl.MouseUp -= OnMouseUp;
			MyControl.KeyDown -= OnKeyDown;
			MyControl.KeyPress -= OnKeyPress;
			MyControl.KeyUp -= OnKeyUp;
			MyControl.VisibleChanged -= OnVisibleChanged;
			MyControl.CollapseChanged -= OnCollapseChanged;
		}

		#endregion

		#region OnEvents

		protected virtual void OnClick(MouseWithKeysEventArgs MouseInfo)
		{
		}

		protected virtual void OnDoubleClick(MouseWithKeysEventArgs MouseInfo)
		{
		}

		protected virtual void OnMouseDown(MouseWithKeysEventArgs e)
		{
		}

		protected virtual void OnMouseMove(MouseWithKeysEventArgs e)
		{

		}

		protected virtual void OnMouseUp(MouseWithKeysEventArgs e)
		{
			//if (Menu != null
			//    && e.IsRightButtonPressed
			//    && this.HasPoint(e.X, e.Y)
			//    //&& (Root != null && Root.DragState == null)
			//    )
			//{
			//    ShowPopupMenu(e.Location);
			//    e.Handled = true;
			//}
		}

		private bool mCanMoveUpDown = false;
		public bool CanMoveUpDown
		{
			get
			{
				return mCanMoveUpDown;
			}
			set
			{
				mCanMoveUpDown = value;
			}
		}

		protected bool IsMoveUpDown(System.Windows.Forms.KeyEventArgs e)
		{
			if (!CanMoveUpDown)
			{
				return false;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Up 
			    && e.Control)
			{
				MoveUp();
				return true;
			}

			if (e.KeyCode == System.Windows.Forms.Keys.Down
			    && e.Control)
			{
				MoveDown();
				return true;
			}
			
			return false;
		}

		protected virtual void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Handled)
			{
				return;
			}

			if (IsMoveUpDown(e))
			{
				e.Handled = true;
				return;
			}

			RaiseKeyDown(e);

			if (e.Handled)
			{
				return;
			}

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.F11:
					ObjectViewer.Show(this);
					break;
				case System.Windows.Forms.Keys.Delete:
					if (e.Shift)
					{
						Cut();
					}
					else
					{
						OnKeyDownDelete(e);
					}
					break;
				case System.Windows.Forms.Keys.PageDown:
					OnKeyDownPageDown(e);
					break;
				case System.Windows.Forms.Keys.PageUp:
					OnKeyDownPageUp(e);
					break;
				case System.Windows.Forms.Keys.C:
					if (e.Control)
					{
						Copy();
					}
					break;
				case System.Windows.Forms.Keys.X:
					if (e.Control)
					{
						Cut();
					}
					break;
				case System.Windows.Forms.Keys.V:
					if (e.Control)
					{
						Paste();
					}
					break;
				case System.Windows.Forms.Keys.Insert:
					if (e.Control)
					{
						Copy();
					}
					else if (e.Shift)
					{
						Paste();
					}
					break;
				default:
					break;
			}
		}

		protected virtual void OnKeyDownDelete(System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Handled)
			{
				return;
			}
			this.Delete();
            e.Handled = true;
		}

		protected virtual void OnKeyDownPageUp(System.Windows.Forms.KeyEventArgs e)
		{
			Block prev = this.FindPrevFocusableSibling();
			if (prev != null)
			{
				prev.SetFocus();
				e.Handled = true;
			}
		}

		protected virtual void OnKeyDownPageDown(System.Windows.Forms.KeyEventArgs e)
		{
			Block next = this.FindNextFocusableSibling();
			if (next != null)
			{
				next.SetFocus();
				e.Handled = true;
			}
		}

		protected virtual void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{

		}

		protected virtual void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{

		}

		protected virtual void OnParentChanged(ContainerBlock oldParent, ContainerBlock newParent)
		{

		}

		protected virtual void OnRootChanged(RootBlock oldRoot, RootBlock newRoot)
		{

		}

		protected virtual void OnActivated(Control control)
		{
			if (this.Root != null) this.Root.ActiveBlock = this;
		}

		protected virtual void OnDeactivated(Control control)
		{

		}

		protected virtual void OnVisibleChanged(Control itemChanged)
		{

		}

		#endregion

		#region Style

		protected internal void InitStyle()
		{
			if (StyleFactory.Instance == null || MyControl == null)
			{
				return;
			}

			string styleName = StyleName();
			if (!string.IsNullOrEmpty(styleName))
			{
				IShapeStyle newStyle = StyleFactory.Instance.GetStyle(styleName);
				if (newStyle != null)
				{
					MyControl.Style = newStyle;
				}
			}

			string selectedStyleName = SelectedStyleName();
			if (!string.IsNullOrEmpty(selectedStyleName))
			{
				IShapeStyle newSelectedStyle = StyleFactory.Instance.GetStyle(selectedStyleName);
				if (newSelectedStyle != null)
				{
					MyControl.SelectedStyle = newSelectedStyle;
				}
			}
		}

		//private static IStyleFactory mCurrentSkin;
		//public static IStyleFactory CurrentSkin
		//{
		//    get
		//    {
		//        return mCurrentSkin;
		//    }
		//    set
		//    {
		//        mCurrentSkin = value;
		//    }
		//}

		protected virtual string StyleName()
		{
			return "";
		}

		protected string SelectedStyleName()
		{
			return StyleName() + "_selected";
		}

		protected string ValidatedStyleName()
		{
			return StyleName() + "_validated";
		}

		#endregion

		#region CanBeSelected

		private bool mCanBeSelected = true;
		public bool CanBeSelected
		{
			get { return mCanBeSelected; }
			set { mCanBeSelected = value; }
		}

		#endregion

		#region Dragging

		private bool mDraggable = false;
		public virtual bool Draggable
		{
			get { return mDraggable; }
			set { mDraggable = value; }
		}

		public virtual void BeforeStartDrag()
		{

		}

		#endregion

		#region HitTest

		public virtual Block FindBlockAtPoint(int x, int y)
		{
			return this;
		}

		public virtual bool HasPoint(int x, int y)
		{
			if (!this.MyControl.HitTest(x, y))
			{
				return false;
			}
			Control c = this.MyControl.FindControlAtPoint(x, y);
			if (c is CollapsePicture)
			{
				return false;
			}
			if (c == null || c == this.MyControl)
			{
				return true;
			}
			if (!c.CanGetFocus)
			{
				c = c.FindNearestFocusableParent();
				if (c == null || c == this.MyControl)
				{
					return true;
				}
			}
			return false;
		}

		#endregion

		#region Menu

		private CommandList mMenu;
		public CommandList Menu
		{
			get
			{
				return mMenu;
			}
			set
			{
				mMenu = value;
			}
		}

		public void ShowPopupMenu(System.Drawing.Point location)
		{
			if (this.Menu != null && this.Root != null)
			{
				this.Root.MyRootControl.ShowPopupMenu(Menu, location);
			}
		}

		#endregion

		public static Block[] EmptyArray = new Block[] { };

		public static string ClipboardFormat = "blocks";
	}

	public delegate void BlockKeyDownEventHandler(Block block, System.Windows.Forms.KeyEventArgs e);
}
