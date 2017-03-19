using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Editor.UI
{
	public class TreeViewNodeControl : ContainerControl
	{
		public TreeViewNodeControl(
			ContainerControl hMembersControl,
			ContainerControl vMembersControl)
			: base()
		{
			HMembers = hMembersControl;
			VMembers = vMembersControl;

			this.CollapseButton = new PictureChangeBox(
				PictureLibrary.Instance.Minus,
				PictureLibrary.Instance.Plus);
			this.CollapseButton.Visible = false;
			this.CollapseButton.Box.Margins.SetAll(3);
			this.CollapseButton.Box.MouseSensitivityArea.SetAll(8);
			this.Add(CollapseButton);

			this.Add(HMembers);
			this.Add(VMembers);

			TreeLineStyle = DefaultLineStyle;

			this.Focusable = true;
			this.Collapsable = true;

			this.CollapseChanged += TreeViewNodeControl_CollapseChanged;
		}

		#region CollapseButton

		void TreeViewNodeControl_CollapseChanged(Control itemChanged)
		{
			this.CollapseButton.Toggle();
		}

		private PictureChangeBox mCollapseButton;
		public PictureChangeBox CollapseButton
		{
			get
			{
				return mCollapseButton;
			}
			set
			{
				if (mCollapseButton != null)
				{
					mCollapseButton.MouseDown -= CollapseButton_MouseDown;
				}
				mCollapseButton = value;
				if (mCollapseButton != null)
				{
					mCollapseButton.MouseDown += CollapseButton_MouseDown;
				}
			}
		}

		private void CollapseButton_MouseDown(MouseWithKeysEventArgs MouseInfo)
		{
			ToggleCollapse(true);
		}

		public override void Collapse(bool collapse, bool shouldRedraw)
		{
			if (collapse && this.VMembers.Children.Count == 0)
			{
				return;
			}

			base.Collapse(collapse, shouldRedraw);
		}

		#endregion

		#region Layout

		public override void LayoutCore()
		{
			const int indent = 18;

			Layouter.PutAt(this, CollapseButton, CollapseButton.Box.Margins.Left);
			Layouter.PutRight(CollapseButton, HMembers, indent - CollapseButton.Bounds.Size.X);
			CollapseButton.Bounds.Location.Y += (HMembers.Bounds.Size.Y - CollapseButton.Bounds.Size.Y) / 2;

			if (VMembers.Children.Count > 0)
			{
				CollapseButton.Visible = true;

				if (this.Collapsed)
				{
					VMembers.Visible = false;
					this.Bounds.Size.X = HMembers.Bounds.Right - this.Bounds.Location.X;
					this.Bounds.Size.Y = HMembers.Bounds.Bottom - this.Bounds.Location.Y;
				}
				else
				{
					VMembers.Visible = true;
					Layouter.PutUnder(HMembers, VMembers);
					this.Bounds.Size.X = HMembers.Bounds.Right - this.Bounds.Location.X;
					this.Bounds.Size.Y = VMembers.Bounds.Bottom - this.Bounds.Location.Y;
					Layouter.GrowToInclude(this, HMembers);
					Layouter.GrowToInclude(this, VMembers);
					this.Bounds.Size.Add(1);
				}
			}
			else
			{
				CollapseButton.Visible = false;
				VMembers.Visible = false;
				Layouter.PutAround(CollapseButton, HMembers, this);
				this.Bounds.Size.X = HMembers.Bounds.Right - this.Bounds.Location.X;
				this.Bounds.Size.Y = HMembers.Bounds.Bottom - this.Bounds.Location.Y;
			}
		}

		#endregion

		#region LineStyle

		private static ILineStyleInfo mDefaultLineStyle;
		private ILineStyleInfo DefaultLineStyle
		{
			get
			{
				if (mDefaultLineStyle == null)
				{
					mDefaultLineStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(System.Drawing.Color.LightGray, 1);
				}
				return mDefaultLineStyle;
			}
		}

		private ILineStyleInfo mTreeLineStyle;
		public ILineStyleInfo TreeLineStyle
		{
			get
			{
				return mTreeLineStyle;
			}
			set
			{
				mTreeLineStyle = value;
			}
		}

		#endregion

		#region Draw

		public override void DrawCore(IRenderer Renderer)
		{
			if (Style.FillStyleInfo != null
				&& (Style.FillStyleInfo.FillColor != System.Drawing.Color.Transparent
				|| Style.FillStyleInfo.GradientColor != System.Drawing.Color.Transparent))
			{
				Renderer.DrawOperations.FillRectangle(this.HMembers.Bounds, Style.FillStyleInfo);
			}

			if (Style.LineStyleInfo != null
				&& Style.LineStyleInfo.ForeColor != System.Drawing.Color.Transparent)
			{
				Renderer.DrawOperations.DrawRectangle(this.HMembers.Bounds, Style.LineStyleInfo);
			}

			DrawLines(Renderer);
			DrawChildren(Renderer);
		}

		Point topPoint = new Point();
		Point bottomPoint = new Point();
		Point rightPoint = new Point();

		protected void DrawLines(IRenderer Renderer)
		{
			if (this.VMembers == null
				|| this.VMembers.Children.Count == 0
				|| this.Collapsed)
			{ return; }

			const int nonExistent = int.MinValue;

			topPoint.Set(
				nonExistent,
				this.HMembers.Bounds.Bottom);

			foreach (Control child in this.VMembers.Children)
			{
				TreeViewNodeControl node = child as TreeViewNodeControl;
				if (node != null)
				{
					if (topPoint.X == nonExistent)
					{
						topPoint.X = node.CollapseButton.Bounds.CenterX;
					}

					bottomPoint.Set(
						topPoint.X,
						node.CollapseButton.Bounds.CenterY);

					rightPoint.Set(
						node.HMembers.Bounds.Location.X - 3,
						bottomPoint.Y);

					Renderer.DrawOperations.DrawLine(
						topPoint, bottomPoint, this.TreeLineStyle);

					Renderer.DrawOperations.DrawLine(
						bottomPoint, rightPoint, this.TreeLineStyle);
				}
			}
		}

		#endregion

		#region HMembers

		private ContainerControl mHMembers;
		public ContainerControl HMembers
		{
			get
			{
				return mHMembers;
			}
			set
			{

				if (mHMembers != null)
				{
					mHMembers.DoubleClick -= mHMembers_DoubleClick;
				}
				mHMembers = value;
				if (mHMembers != null)
				{
					mHMembers.DoubleClick += mHMembers_DoubleClick;
				}
			}
		}

		void mHMembers_DoubleClick(MouseWithKeysEventArgs MouseInfo)
		{
			this.ToggleCollapse(true);
		}

		#endregion

		#region VMembers

		private ContainerControl mVMembers;
		public ContainerControl VMembers
		{
			get
			{
				return mVMembers;
			}
			set
			{
				if (mVMembers != null)
				{
					mVMembers.Children.CollectionChanged -= Children_CollectionChanged;
				}
				mVMembers = value;
				if (mVMembers != null)
				{
					mVMembers.Children.CollectionChanged += Children_CollectionChanged;
				}
			}
		}

		void Children_CollectionChanged()
		{
			// Layout();
		}

		#endregion
	}
}
