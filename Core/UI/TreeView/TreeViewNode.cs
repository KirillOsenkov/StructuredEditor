using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class TreeViewNode : ContainerBlock
	{
		#region ctors

		public TreeViewNode()
			: base()
		{
			HMembers = new HContainerBlock();
			VMembers = new VContainerBlock();

			this.Add(HMembers);
			this.Add(VMembers);

			InitControl();
		}

		public TreeViewNode(string Caption)
			: this()
		{
			HMembers.Add(new TreeViewLabelBlock(Caption));
		}

		#endregion

		#region AddNode

		public TreeViewNode AddNode(string Caption)
		{
			TreeViewNode childToAdd = new TreeViewNode(Caption);
			this.VMembers.Add(childToAdd);
			return childToAdd;
		}

		#endregion

		#region Control

		protected virtual void InitControl()
		{
			MyNodeControl = new TreeViewNodeControl(
				HMembers.MyListControl,
				VMembers.MyListControl);
		}

		private TreeViewNodeControl mMyNodeControl;
		public TreeViewNodeControl MyNodeControl
		{
			get
			{
				return mMyNodeControl;
			}
			protected set
			{
				if (mMyNodeControl != null)
				{
					UnSubscribeControl();
				}
				mMyNodeControl = value;
				if (mMyNodeControl != null)
				{
					SubscribeControl();
				}
			}
		}

		public override Control MyControl
		{
			get { return MyNodeControl; }
		}

		#endregion

		#region OnKeyDown

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Left:
					if (!this.MyControl.Collapsed && this.VMembers.Children.Count > 0)
					{
						e.Handled = true;
						this.MyNodeControl.ToggleCollapse(true);
					}
					break;
				case System.Windows.Forms.Keys.Right:
					if (this.MyControl.Collapsed)
					{
						e.Handled = true;
						this.MyNodeControl.ToggleCollapse(true);
					}
					else
					{
						nextFocusable = VMembers.FindFirstFocusableBlock();
					}
					break;
				case System.Windows.Forms.Keys.Down:
					if (VMembers.MyControl.Visible && !VMembers.MyControl.Collapsed)
					{
						nextFocusable = VMembers.FindFirstFocusableBlock();
					}
					break;
				case System.Windows.Forms.Keys.End:
					nextFocusable = HMembers.FindLastFocusableBlock();
					break;
				case System.Windows.Forms.Keys.Add:
					if (this.MyControl.Collapsed)
					{
						e.Handled = true;
						this.MyNodeControl.ToggleCollapse(true);
					}
					break;
				case System.Windows.Forms.Keys.Subtract:
					if (!this.MyControl.Collapsed && this.VMembers.Children.Count > 0)
					{
						e.Handled = true;
						this.MyNodeControl.ToggleCollapse(true);
					}
					break;
				case System.Windows.Forms.Keys.Space:
					e.Handled = true;
					this.MyNodeControl.ToggleCollapse(true);
					break;
				case System.Windows.Forms.Keys.Multiply:
					e.Handled = true;
					this.MyNodeControl.CollapseAll(false, true);
					break;
				case System.Windows.Forms.Keys.Divide:
					e.Handled = true;
					this.MyNodeControl.CollapseAll(true, true);
					break;
				case System.Windows.Forms.Keys.Delete:
					this.Delete();
					e.Handled = true;
					break;
				default:
					break;
			}

			if (nextFocusable != null && nextFocusable.CanGetFocus)
			{
				nextFocusable.SetFocus();
				e.Handled = true;
			}

			RaiseKeyDown(e);
		}

		#endregion

		#region HMembers

		private HContainerBlock mHMembers;
		public HContainerBlock HMembers
		{
			get { return mHMembers; }
			set
			{
				if (mHMembers != null)
				{
					mHMembers.KeyDown -= HMembers_KeyDown;
				}
				mHMembers = value;
				if (mHMembers != null)
				{
					mHMembers.KeyDown += HMembers_KeyDown;
				}
			}
		}

		void HMembers_KeyDown(Block Block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Return:
				case System.Windows.Forms.Keys.Down:
					if (VMembers != null && this.VMembers.MyControl.Visible)
					{
						nextFocusable = this.VMembers.FindFirstFocusableBlock();
					}
					break;
				case System.Windows.Forms.Keys.Left:
				case System.Windows.Forms.Keys.Home:
					nextFocusable = this;
					break;
				default:
					break;
			}

			if (nextFocusable != null)
			{
				nextFocusable.SetFocus();
				e.Handled = true;
			}

			RaiseKeyDown(e);
		}

		#endregion

		#region VMembers

		private VContainerBlock mVMembers;
		public VContainerBlock VMembers
		{
			get { return mVMembers; }
			set
			{
				if (mVMembers != null)
				{
					mVMembers.KeyDown -= VMembers_KeyDown;
				}
				mVMembers = value;
				if (mVMembers != null)
				{
					mVMembers.KeyDown += VMembers_KeyDown;
				}
			}
		}

		void VMembers_KeyDown(Block Block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			switch (e.KeyCode)
			{
				// Let's select ourselves each time we're going up
				case System.Windows.Forms.Keys.Up:
				case System.Windows.Forms.Keys.Left:
				case System.Windows.Forms.Keys.Home:
					nextFocusable = this;
					break;
				default:
					break;
			}

			if (nextFocusable != null && nextFocusable.CanGetFocus)
			{
				nextFocusable.SetFocus();
				e.Handled = true;
			}

			RaiseKeyDown(e);
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "TreeViewNode";
		}

		#endregion
	}
}
