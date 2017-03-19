using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Canvas.Controls
{
	public partial class UniversalControl : LinearContainerControl
	{
		#region ctor

		public UniversalControl(
			LinearContainerControl declarationControl,
			LinearContainerControl membersControl)
			: base()
		{
			this.CurlyType = DefaultCurlyType;
			UpdateOffsetCurlies();
			this.Collapsable = true;
			InitCollapseButton();
			this.Focusable = true;
			this.Stretch = StretchMode.Horizontal;

			this.Box.Borders.Right = 0;

			this.HMembers = declarationControl;
			this.HCompartment.Box.Borders.SetAll(0);
			this.HCompartment.Add(HMembers);
			this.HCompartment.Add(MyCollapseButton);
			this.HCompartment.Add(OpenCurlyUpper);
			this.Add(HCompartment);

			this.VMembers = membersControl;
		}

		#endregion

		private static TypeOfCurlies mDefaultCurlyType 
			= TypeOfCurlies.None;
		public const UniversalControlDesign DefaultDesign 
			= UniversalControlDesign.Rectangle;

		#region Collapse

		public override void Collapse(bool collapse, bool shouldRedraw)
		{
			MyCollapseButton.Toggle();
			base.Collapse(collapse, shouldRedraw);
		}

		public override bool Collapsable
		{
			get
			{
				return base.Collapsable;
			}
			set
			{
				bool shouldCallLayout = false;

				base.Collapsable = value;
				if (MyCollapseButton != null)
				{
					MyCollapseButton.Visible = value;
					shouldCallLayout = true;
				}
				if (value == false && this.Collapsed)
				{
					ToggleCollapse(true);
				}

				if (shouldCallLayout)
				{
					HCompartment.Layout();
				}
			}
		}

		void MyCollapseButton_MouseDown(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
		{
			ToggleCollapse(true);
		}

		protected virtual void InitCollapsePictures()
		{
			CollapsePictureExpanded = PictureLibrary.Instance.Minus;
			CollapsePictureCollapsed = PictureLibrary.Instance.Plus;
		}

		public IPicture CollapsePictureCollapsed { get; set; }
		public IPicture CollapsePictureExpanded { get; set; }

		protected virtual void InitCollapseButton()
		{
			InitCollapsePictures();
			MyCollapseButton = new CollapsePicture(this.CollapsePictureExpanded, this.CollapsePictureCollapsed);
			MyCollapseButton.MouseDown += MyCollapseButton_MouseDown;
		}

		public CollapsePicture MyCollapseButton { get; set; }

		#endregion

		#region Layout

		public override void LayoutCore()
		{
			if (VMembers != null)
			{
				if (VMembers.Box.Margins.Left != 0)
				{
					VMembers.Box.Margins.Left = Indent;
				}
				if (OpenCurlyHasNegativeLowerMargin)
				{
					OpenCurly.Box.Margins.Bottom = -OpenCurly.Bounds.Height + 1;
				}
				else
				{
					OpenCurly.Box.Margins.Bottom = 0;
				}
				VMembers.Box.Borders.Top = 1;
				VMembers.Visible = !this.Collapsed;
				UpdateCurlyVisibility();
			}
			base.LayoutCore();
		}

		public override void LayoutDockCore()
		{
			base.LayoutDockCore();
			this.MyCollapseButton.Bounds.Location.X =
				this.Bounds.Right
				- MyCollapseButton.Bounds.Size.X
				- this.MyCollapseButton.Box.Margins.Right
				- this.Box.Padding.Right
				- this.Box.Borders.Right;
			//this.MyCollapseButton.Bounds.Location.X = 0;
		}

		#endregion

		#region OnEvents

		public override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == System.Windows.Forms.Keys.Space)
			{
				this.ToggleCollapse(true);
			}
		}

		#endregion

		#region HCompartment

		private LinearContainerControl mHCompartment = new HContainerControl();
		public LinearContainerControl HCompartment
		{
			get
			{
				return mHCompartment;
			}
			set
			{
				mHCompartment = value;
			}
		}

		#endregion

		#region HMembers

		private LinearContainerControl mHMembers;
		public LinearContainerControl HMembers
		{
			get
			{
				return mHMembers;
			}
			set
			{

				mHMembers = value;
				if (mHMembers != null)
				{
					mHMembers.Box.Borders.SetAll(0);
					mHMembers.LinearLayoutStrategy.XSpacing = 0;
				}
			}
		}

		#endregion

		protected int Indent
		{
			get
			{
				return 4 * CurrentStyle.FontSize;
			}
		}

		#region VMembers

		private LinearContainerControl mVMembers;
		public LinearContainerControl VMembers
		{
			get
			{
				return mVMembers;
			}
			set
			{
				if (mVMembers != null)
				{
					this.Remove(OpenCurly);
					this.Remove(mVMembers);
					this.Remove(CloseCurly);
				}
				mVMembers = value;
				if (mVMembers != null)
				{
					mVMembers.Box.Margins.Left = Indent;
					mVMembers.Box.Borders.SetAll(0);
					mVMembers.RedirectHitTestToNearestChild = true;
					this.Add(OpenCurly);
					this.Add(mVMembers);
					this.Add(CloseCurly);
					mVMembers.Box.Borders.Right = 0;
					mVMembers.Layout();
				}
			}
		}

		#endregion

		#region Curlies

		public Label OpenCurlyUpper = new Label("{");
		public Label OpenCurly = new Label("{");
		public Label CloseCurly = new Label("}");

		private bool mCanOffsetCurlies = false;
		public bool CanOffsetCurlies
		{
			get
			{
				return mCanOffsetCurlies;
			}
			set
			{
				mCanOffsetCurlies = value;
				UpdateOffsetCurlies();
			}
		}

		private bool mOffsetCurlies = false;
		public bool OffsetCurlies
		{
			get
			{
				return mOffsetCurlies;
			}
			set
			{
				bool newValue = value && CanOffsetCurlies;
				if (mOffsetCurlies == newValue)
				{
					return;
				}
				mOffsetCurlies = newValue;
				if (mOffsetCurlies)
				{
					OpenCurly.Text = " {";
					CloseCurly.Text = " }";
				}
				else
				{
					OpenCurly.Text = "{";
					CloseCurly.Text = "}";
				}
			}
		}

		public void UpdateOffsetCurlies()
		{
			if (Design == UniversalControlDesign.TableLike)
			{
				OffsetCurlies = false;
			}
			else
			{
				OffsetCurlies = true;
			}
		}

		private bool mHideCurlies = false;
		public bool HideCurlies
		{
			get
			{
				return mHideCurlies;
			}
			set
			{
				mHideCurlies = value;
				if (value)
				{
					CurlyType = TypeOfCurlies.None;
				}
			}
		}

		public enum TypeOfCurlies
		{
			None,
			CSharp,
			Java,
			OnlyClosingCurly
		}

		private TypeOfCurlies mCurlyType;
		public TypeOfCurlies CurlyType
		{
			get
			{
				return mCurlyType;
			}
			set
			{
				if (mCurlyType == value
					|| (HideCurlies && mCurlyType == TypeOfCurlies.None))
				{
					return;
				}
				mCurlyType = value;
				if (HideCurlies)
				{
					mCurlyType = TypeOfCurlies.None;
				}
				UpdateCurlyVisibility();
				this.HCompartment.Layout();
				Layout();
			}
		}

		public static TypeOfCurlies DefaultCurlyType
		{
			get
			{
				return mDefaultCurlyType;
			}
			set
			{
				mDefaultCurlyType = value;
			}
		}

		private bool mOpenCurlyHasNegativeLowerMargin = true;
		public bool OpenCurlyHasNegativeLowerMargin
		{
			get
			{
				return mOpenCurlyHasNegativeLowerMargin;
			}
			set
			{
				mOpenCurlyHasNegativeLowerMargin = value;
				Layout();
			}
		}

		private void UpdateCurlyVisibility()
		{
			TypeOfCurlies curlies = CurlyType;
			if (this.Collapsed)
			{
				curlies = TypeOfCurlies.None;
			}
			switch (curlies)
			{
				case TypeOfCurlies.CSharp:
					OpenCurlyUpper.Visible = false;
					OpenCurly.Visible = true;
					CloseCurly.Visible = true;
					break;
				case TypeOfCurlies.Java:
					OpenCurlyUpper.Visible = true;
					OpenCurly.Visible = false;
					CloseCurly.Visible = true;
					break;
				case TypeOfCurlies.OnlyClosingCurly:
					OpenCurly.Visible = false;
					OpenCurlyUpper.Visible = false;
					CloseCurly.Visible = true;
					break;
				default:
					OpenCurlyUpper.Visible = false;
					OpenCurly.Visible = false;
					CloseCurly.Visible = false;
					break;
			}
		}

		//public void UpdateCurlyType()
		//{
		//    if (Design == UniversalControlDesign.CSharpCurlies
		//        && CurlyType != TypeOfCurlies.CSharp)
		//    {
		//        CurlyType = TypeOfCurlies.CSharp;
		//    }
		//    if (Design == UniversalControlDesign.JavaCurlies
		//        && CurlyType != TypeOfCurlies.Java)
		//    {
		//        CurlyType = TypeOfCurlies.Java;
		//    }
		//    if (Design == UniversalControlDesign.OnlyClosingCurly
		//        && CurlyType != TypeOfCurlies.OnlyClosingCurly)
		//    {
		//        CurlyType = TypeOfCurlies.OnlyClosingCurly;
		//    }
		//    if (Design != UniversalControlDesign.CSharpCurlies
		//        && Design != UniversalControlDesign.JavaCurlies
		//        && Design != UniversalControlDesign.OnlyClosingCurly
		//        && CurlyType != TypeOfCurlies.None)
		//    {
		//        CurlyType = TypeOfCurlies.None;
		//    }
		//}

		#endregion
	}
}
