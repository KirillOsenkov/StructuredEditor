using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Clipboard;

namespace GuiLabs.Editor.Blocks
{
	public partial class LinearContainerBlock : ContainerBlock, IPotentialDropTarget
	{
		#region ctors

		public LinearContainerBlock()
			: this(OrientationType.Vertical)
		{
		}

		public LinearContainerBlock(OrientationType orientation)
			: base()
		{
			InitControl(orientation);

			this.MyControl.Box.Borders.SetAll(0);

			Children.KeyDown += Children_KeyDown;
		}

		#endregion

		#region Control

		protected virtual void InitControl(OrientationType orientation)
		{
			MyListControl = new LinearContainerControl(this.Children.Controls);
			MyListControl.LinearLayoutStrategy.Orientation = orientation;
		}

		private LinearContainerControl mMyListControl;
		public virtual LinearContainerControl MyListControl
		{
			get
			{
				return mMyListControl;
			}
			protected set
			{
				if (mMyListControl != null)
				{
					UnSubscribeControl();
				}
				mMyListControl = value;
				if (mMyListControl != null)
				{
					SubscribeControl();
				}
			}
		}

		public override Control MyControl
		{
			get { return MyListControl; }
		}

		#region LayoutStrategy

		public ILinearLayout LinearLayoutStrategy
		{
			get
			{
				return this.MyListControl.LinearLayoutStrategy;
			}
		}

		public System.Windows.Forms.Keys PrevKey
		{
			get
			{
				return LinearLayoutStrategy.PrevKey;
			}
		}

		public System.Windows.Forms.Keys NextKey
		{
			get
			{
				return LinearLayoutStrategy.NextKey;
			}
		}

		public OrientationType Orientation
		{
			get
			{
				return LinearLayoutStrategy.Orientation;
			}
			set
			{
				LinearLayoutStrategy.Orientation = value;
			}
		}

		#endregion

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			if (e.KeyCode == NextKey)
			{
				nextFocusable = FindFirstFocusableChild();
				if (nextFocusable != null)
				{
					nextFocusable.SetFocus();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.End)
			{
				nextFocusable = FindLastFocusableChild();
				if (nextFocusable != null)
				{
					nextFocusable.SetCursorToTheEnd();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		/// <summary>
		/// One of the children of this LinearContainerBlock has received a key click.
		/// </summary>
		/// <param name="Block">Which child has been clicked?</param>
		/// <param name="e">Key event info</param>
		protected virtual void Children_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			bool isLeft = e.KeyCode == System.Windows.Forms.Keys.Left;
			bool isUp = e.KeyCode == System.Windows.Forms.Keys.Up;
			bool isDown = e.KeyCode == System.Windows.Forms.Keys.Down;
			bool isRight = e.KeyCode == System.Windows.Forms.Keys.Right;

			bool isPrev = e.KeyCode == PrevKey;
			bool isNext = e.KeyCode == NextKey;
			bool prevIsUp = PrevKey == System.Windows.Forms.Keys.Up;
			bool nextIsDown = NextKey == System.Windows.Forms.Keys.Down;

			if (isPrev || (isLeft && prevIsUp))
			{
				nextFocusable = block.FindPrevFocusableBlock();
				if (nextFocusable != null)
				{
					if (isLeft)
					{
						nextFocusable.SetCursorToTheEnd();
					}
					else
					{
						nextFocusable.SetFocus();
					}
					e.Handled = true;
				}
				else
				{
					if (this.CanGetFocus)
					{
						this.SetFocus();
						e.Handled = true;
					}
				}
			}
			else if (isNext || (isRight && nextIsDown))
			{
				nextFocusable = block.FindNextFocusableBlock();
				if (nextFocusable != null)
				{
					if (isRight)
					{
						nextFocusable.SetCursorToTheBeginning();
					}
					else
					{
						nextFocusable.SetFocus();
					}
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Home)
			{
				if (this.CanGetFocus)
				{
					this.SetFocus();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				this.RaiseKeyDown(e);
			}
		}

		#endregion

		#region Focus

		public override bool SetCursorToTheBeginning()
		{
			if (!this.Visible)
			{
				return false;
			}

			if (this.CanGetFocus)
			{
				this.SetFocus();
				return true;
			}

			foreach (Block child in this.Children)
			{
				if (child.SetCursorToTheBeginning())
				{
					return true;
				}
			}

			return false;
		}

		public override bool SetCursorToTheEnd()
		{
			if (!this.Visible)
			{
				return false;
			}

			foreach (Block child in this.Children.Reversed)
			{
				if (child.SetCursorToTheEnd())
				{
					return true;
				}
			}

			if (this.CanGetFocus)
			{
				this.SetFocus();
				return true;
			}

			return false;
		}

		#endregion

		#region Delete

		private bool mUserDeletable = false;
		public bool UserDeletable
		{
			get
			{
				return mUserDeletable;
			}
			set
			{
				mUserDeletable = value;
			}
		}

		#endregion

		#region FindDirectChildAtPoint

		public override Block FindDirectChildAtPoint(int x, int y)
		{
			if (!this.MyControl.HitTest(x, y))
			{
				return null;
			}

			Block result = null;
			if (this.MyListControl.LinearLayoutStrategy.Orientation == OrientationType.Horizontal)
			{
				result = FindChildHorizontal(x, y);
			}
			else
			{
				result = FindChildVertical(x, y);
			}

			if (result != null)
			{
				return result;
			}

			// nur falls result == null


			Block candidate = Children.Tail;
			if (candidate != null
				&& candidate.MyControl.Enabled
				&& candidate.MyControl.Visible
				//&& candidate.Stretch != StretchMode.None
				)
			{
				// return candidate;
			}

			return null;
		}

		public Block FindChildHorizontal(int x, int y)
		{
			return FindChildVertical(x, y);
		}

		public Block FindChildVertical(int x, int y)
		{
			foreach (Block child in Children)
			{
				if (child.MyControl.HitTest(x, y))
				{
					return child;
				}
			}

			return null;
		}

		#endregion

		#region Separator

		private IBlockFactory SeparatorFactory;
		public void AddSeparatorType<T>()
		{
			SeparatorFactory = BlockActivatorFactory.CreateFactory<T>();
		}

		public bool HasSeparators
		{
			get
			{
				return SeparatorFactory != null;
			}
		}

		public Block CreateSeparator()
		{
			if (SeparatorFactory == null)
			{
				return null;
			}
			return SeparatorFactory.CreateBlock();
		}

		#endregion
	}
}
