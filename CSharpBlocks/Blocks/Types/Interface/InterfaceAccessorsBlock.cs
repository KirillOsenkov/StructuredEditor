using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.UI;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Undo;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("accessors")]
	public class InterfaceAccessorsBlock : HContainerBlock
	{
		public InterfaceAccessorsBlock()
		{
			Empty = new TextBoxBlock();
			Empty.MyTextBox.MinWidth = 1;
			Empty.MyTextBox.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize / 2);
			Empty.MyTextBox.Box.SetMouseSensitivityToMargins();
			this.MyControl.Focusable = true;
			this.MyControl.Stretch = GuiLabs.Canvas.Controls.StretchMode.None;
			this.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			this.MyControl.Box.SetMouseSensitivityToMargins();

			InternalContainer = new HContainerBlock();
			this.Add("{");
			this.Add(InternalContainer);
			this.Add("}");

			InternalContainer.Add(Empty);
		}

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.G)
			{
				AddGetter();
				e.Handled = true;
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.S)
			{
				AddSetter();
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		private HContainerBlock mInternalContainer;
		public HContainerBlock InternalContainer
		{
			get
			{
				return mInternalContainer;
			}
			set
			{
				if (mInternalContainer != null)
				{
					mInternalContainer.Children.CollectionChanged -= Children_CollectionChanged;
				}
				mInternalContainer = value;
				if (mInternalContainer != null)
				{
					mInternalContainer.Children.CollectionChanged += Children_CollectionChanged;
				}
			}
		}

		void Children_CollectionChanged()
		{
			UpdateGetterAndSetter();
		}

		private void UpdateGetterAndSetter()
		{
			bool getterFound = false;
			bool setterFound = false;

			foreach (Block b in InternalContainer.Children)
			{
				InterfacePropertyAccessor accessor = b as InterfacePropertyAccessor;
				if (accessor != null)
				{
					if (accessor.Text == Keywords.Get)
					{
						getterFound = true;
						if (mGetter != accessor)
						{
							mGetter = accessor;
						}
					}
					else if (accessor.Text == Keywords.Set)
					{
						setterFound = true;
						if (mSetter != accessor)
						{
							mSetter = accessor;
						}
					}
				}
			}

			if (!getterFound && mGetter != null)
			{
				mGetter = null;
			}
			if (!setterFound && mSetter != null)
			{
				mSetter = null;
			}

			Empty.Hidden = Getter != null && Setter != null;
			InternalContainer.CheckVisibility();
			InternalContainer.MyControl.Redraw();
		}

		private TextBoxBlock mEmpty;
		public TextBoxBlock Empty
		{
			get
			{
				return mEmpty;
			}
			set
			{
				if (mEmpty != null)
				{
					mEmpty.MyTextBox.PreviewKeyPress -= MyTextBox_PreviewKeyPress;
					mEmpty.MyTextBox.KeyDown -= MyTextBox_KeyDown;
				}
				mEmpty = value;
				if (mEmpty != null)
				{
					mEmpty.MyTextBox.PreviewKeyPress += MyTextBox_PreviewKeyPress;
					mEmpty.MyTextBox.KeyDown += MyTextBox_KeyDown;
				}
			}
		}

		void MyTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				if (Getter != null)
				{
					Getter = null;
					e.Handled = true;
				}
				else if (Setter == null)
				{
					this.Delete();
					e.Handled = true;
				}
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				if (Setter != null)
				{
					Setter = null;
					e.Handled = true;
				}
				else if (Getter == null)
				{
					this.Delete();
					e.Handled = true;
				}
			}
		}

		void MyTextBox_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 's')
			{
				AddSetter();
			}
			else if (e.KeyChar == 'g')
			{
				AddGetter();
			}
			e.Handled = true;
		}

		#region API

		public void AddGetter()
		{
			if (Getter == null)
			{
				Getter = new InterfacePropertyAccessor(Keywords.Get);
			}
		}

		public void AddSetter()
		{
			if (Setter == null)
			{
				Setter = new InterfacePropertyAccessor(Keywords.Set);
			}
		}

		#endregion

		private InterfacePropertyAccessor mGetter;
		public InterfacePropertyAccessor Getter
		{
			get
			{
				return mGetter;
			}
			set
			{
				if (ActionManager != null)
				{
					using (Transaction.Create(this.ActionManager))
					{
						if (mGetter != null)
						{
							mGetter.KeyDown -= mGetter_KeyDown;
							mGetter.Delete();
						}
						mGetter = value;
						if (mGetter != null)
						{
							if (mSetter != null)
							{
								Empty.Hidden = false;
							}
							InternalContainer.AddToBeginning(mGetter);
							if (!Empty.Hidden)
							{
								Empty.SetFocus();
							}
							mGetter.Deleted += mGetter_Deleted;
							mGetter.KeyDown += mGetter_KeyDown;
						}
					}
				}
				else
				{
					if (mGetter != null)
					{
						mGetter.KeyDown -= mGetter_KeyDown;
						mGetter.Delete();
					}
					mGetter = value;
					if (mGetter != null)
					{
						if (mSetter != null)
						{
							Empty.Hidden = false;
						}
						InternalContainer.AddToBeginning(mGetter);
						if (!Empty.Hidden)
						{
							Empty.SetFocus();
						}
						mGetter.Deleted += mGetter_Deleted;
						mGetter.KeyDown += mGetter_KeyDown;
					}
				}
			}
		}

		void mGetter_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Delete || e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				Getter = null;
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.S)
			{
				AddSetter();
			}
		}

		void mGetter_Deleted(Block itemChanged)
		{
			using (Redrawer r = new Redrawer(this.Root))
			{
				if (mGetter != null)
				{
					mGetter.Deleted -= mGetter_Deleted;
					mGetter = null;
				}
				Empty.Hidden = false;
				Empty.SetFocus();
			}
		}

		private InterfacePropertyAccessor mSetter;
		public InterfacePropertyAccessor Setter
		{
			get
			{
				return mSetter;
			}
			set
			{
				if (ActionManager != null)
				{
					using (ActionManager.CreateTransaction())
					{
						if (mSetter != null)
						{
							mSetter.KeyDown -= mSetter_KeyDown;
							mSetter.Delete();
						}
						mSetter = value;
						if (mSetter != null)
						{
							if (mGetter != null)
							{
								Empty.Hidden = true;
							}
							InternalContainer.Add(mSetter);
							if (Empty.IsVisible)
							{
								Empty.SetFocus();
							}
							mSetter.Deleted += mSetter_Deleted;
							mSetter.KeyDown += mSetter_KeyDown;
						}
					}
				}
				else
				{
					if (mSetter != null)
					{
						mSetter.KeyDown -= mSetter_KeyDown;
						mSetter.Delete();
					}
					mSetter = value;
					if (mSetter != null)
					{
						if (mGetter != null)
						{
							Empty.Hidden = true;
						}
						InternalContainer.Add(mSetter);
						if (Empty.IsVisible)
						{
							Empty.SetFocus();
						}
						mSetter.Deleted += mSetter_Deleted;
						mSetter.KeyDown += mSetter_KeyDown;
					}
				}
			}
		}

		void mSetter_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Back || e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				Setter = null;
				e.Handled = true;
			}
			if (e.KeyCode == System.Windows.Forms.Keys.G)
			{
				AddGetter();
			}
		}

		void mSetter_Deleted(Block itemChanged)
		{
			using (Redrawer r = new Redrawer(this.Root))
			{
				if (mSetter != null)
				{
					mSetter.Deleted -= mSetter_Deleted;
					mSetter = null;
				}
				Empty.Hidden = false;
				Empty.SetFocus();
			}
		}

		#region DefaultFocusableBlock

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			if (Empty.IsVisible)
			{
				return Empty.MyTextBox;
			}
			if (Getter != null)
			{
				return Getter.MyControl;
			}
			if (Setter != null)
			{
				return Setter.MyControl;
			}
			return this.MyControl;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "InterfaceAccessorsBlock";
		}

		#endregion
	}
}
