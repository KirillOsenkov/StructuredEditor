using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Utils;
using GuiLabs.Canvas.Events;

namespace GuiLabs.Editor.Blocks
{
	public class UniversalBlock : ContainerBlock
	{
		#region ctors

		public UniversalBlock()
			: base()
		{
			HMembers = CreateHMembers();
			VMembers = CreateVMembers();

			InitControl();
		}

		public UniversalBlock(VContainerBlock vMembers)
			: base()
		{
			HMembers = CreateHMembers();
			if (vMembers == null)
			{
				VMembers = CreateVMembers();
			}
			else
			{
				VMembers = vMembers;
			}

			InitControl();
		}

		#endregion

		#region Control

		public override Control MyControl
		{
			get { return MyUniversalControl; }
		}

		protected virtual void InitControl()
		{
			MyUniversalControl = new UniversalControl(
				HMembers.MyListControl,
				VMembers.MyListControl);
		}

		private UniversalControl mMyUniversalControl;
		public UniversalControl MyUniversalControl
		{
			get
			{
				return mMyUniversalControl;
			}
			set
			{
				if (mMyUniversalControl != null)
				{
					UnSubscribeControl();
				}
				mMyUniversalControl = value;
				if (mMyUniversalControl != null)
				{
					SubscribeControl();
				}
			}
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;
			
			if (IsMoveUpDown(e))
			{
				e.Handled = true;
				return;
			}

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Right:
					nextFocusable = this.FindFirstFocusableChild();
					if (nextFocusable == null)
					{
						nextFocusable = this.FindNextFocusableBlockInChain();
					}
					nextFocusable.SetCursorToTheBeginning();
					e.Handled = true;
					return;
				case System.Windows.Forms.Keys.Down:
					if (VMembers.Visible
						&& this.MyUniversalControl.LinearLayoutStrategy.Orientation == OrientationType.Vertical)
					{
						nextFocusable = VMembers.FindFirstFocusableBlock();
						if (nextFocusable != null)
						{
							nextFocusable.SetFocus();
							e.Handled = true;
							return;
						}
					}
					break;
				case System.Windows.Forms.Keys.End:
					nextFocusable = HMembers.FindLastFocusableBlock();
					if (nextFocusable != null)
					{
						nextFocusable.SetCursorToTheEnd();
						e.Handled = true;
						return;
					}
					break;
				default:
					break;
			}

			if (nextFocusable != null && nextFocusable.CanGetFocus)
			{
				nextFocusable.SetFocus();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnDoubleClick(MouseWithKeysEventArgs e)
		{
			if (this.HasPoint(e.X, e.Y))
			{
				this.MyUniversalControl.ToggleCollapse(true);
                e.Handled = true;
			}
		}

		#endregion

		#region Memento

		public override IEnumerable<Block> GetChildrenToSerialize()
		{
			return this.VMembers.Children;
		}

		#endregion

		#region HMembers

		private HContainerBlock mHMembers;
		public virtual HContainerBlock HMembers
		{
			get { return mHMembers; }
			private set
			{
				if (mHMembers != null)
				{
					mHMembers.KeyDown -= HMembers_KeyDown;
					this.Children.Delete(mHMembers);
				}
				mHMembers = value;
				if (mHMembers != null)
				{
					this.Children.Prepend(mHMembers);
					mHMembers.CanBeSelected = false;
					mHMembers.KeyDown += HMembers_KeyDown;
				}
			}
		}

		protected virtual HContainerBlock CreateHMembers()
		{
			HContainerBlock result = new HContainerBlock();
			return result;
		}

		void HMembers_KeyDown(Block Block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Return:
				case System.Windows.Forms.Keys.Down:
				case System.Windows.Forms.Keys.Right:
					if (VMembers != null && this.VMembers.MyControl.Visible)
					{
						nextFocusable = this.VMembers.FindFirstFocusableBlock();
					}
					break;
				case System.Windows.Forms.Keys.Left:
				case System.Windows.Forms.Keys.Home:
					nextFocusable = this;
					break;
				case System.Windows.Forms.Keys.End:
					nextFocusable = this.HMembers.FindLastFocusableChild();
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

		#region VMembers

		private VContainerBlock mVMembers;
		public virtual VContainerBlock VMembers
		{
			get { return mVMembers; }
			set
			{
				if (mVMembers != null)
				{
					mVMembers.KeyDown -= VMembers_KeyDown;
					this.Children.Delete(mVMembers);
				}
				mVMembers = value;
				if (mVMembers != null)
				{
					mVMembers.CanBeSelected = false;
					mVMembers.KeyDown += VMembers_KeyDown;
					this.Children.Add(mVMembers);
					if (this.MyUniversalControl != null)
					{
						this.MyUniversalControl.VMembers = this.VMembers.MyListControl;
					}
				}
			}
		}

		protected virtual VContainerBlock CreateVMembers()
		{
			VContainerBlock result = new VContainerBlock();
			return result;
		}

		void VMembers_KeyDown(Block Block, System.Windows.Forms.KeyEventArgs e)
		{
			Block nextFocusable = null;

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Left:
					nextFocusable = VMembers.FindPrevFocusableBlockInChain();
					if (nextFocusable != null && nextFocusable.IsInStrictSubtreeOf(this))
					{
						nextFocusable.SetCursorToTheEnd();
						e.Handled = true;
						return;
					}
					break;
				case System.Windows.Forms.Keys.Up:
				//Let's select ourselves each time we're going up
				//if (HMembers != null && HMembers.GetFirstFocusableChild() != null)
				//{
				//    nextFocusable = HMembers.GetFirstFocusableChild();
				//}
				case System.Windows.Forms.Keys.Home:
					nextFocusable = this;
					if (!this.CanGetFocus)
					{
						nextFocusable = this.FindNearestFocusableParent();
					}
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

		#region Help

		protected override void OnCollapseChanged(Control itemChanged)
		{
			this.DisplayContextHelp();
		}

		private static string[] mHelpStrings = new string[]
		{
			HelpBase.Delete
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				yield return HelpBase.SelectFirstChild(this);
				foreach (string pageDownUp in HelpBase.PressPageDownAndOrUp(this))
				{
					yield return pageDownUp;
				}
				if (this.ParentParent != null)
				{
					yield return HelpBase.PressHomeToSelectParent;
				}
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				yield return HelpBase.SpaceCollapse(this.MyControl.Collapsed);
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}

		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion
	}
}
