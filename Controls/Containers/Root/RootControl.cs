using System.Collections.Generic;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils.Collections;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Canvas.Controls
{
	public partial class RootControl :
		LinearContainerControl,
		IScrollableContainer
	{
		#region ctor

		public RootControl(
			ICollectionWithEvents<Control> bindToCollection)
			: base(bindToCollection)
		{
			this.Box.Borders.SetAll(0);
			this.Box.Padding.SetAll(ShapeStyle.DefaultFontSize);
		}

		#endregion

		#region Ambience

		#region Events

		public event ScrollToHandler ShouldScrollTo;
		public virtual void RaiseScrollTo(Rect shape)
		{
			if (ShouldScrollTo != null)
			{
				ShouldScrollTo(shape);
			}
		}

		public event DisplayCompletionListHandler ShouldDisplayCompletionList;
		protected void RaiseDisplayCompletionList(IHasBounds nearToShape)
		{
			if (ShouldDisplayCompletionList != null)
			{
				ShouldDisplayCompletionList(nearToShape);
			}
		}

		public event ShowPopupMenuHandler ShouldShowPopupMenu;
		protected void RaiseShowPopupMenu(ICommandList menu, System.Drawing.Point location)
		{
			if (ShouldShowPopupMenu != null)
			{
				ShouldShowPopupMenu(menu, location);
			}
		}

		#endregion

		public void ScrollTo(Control TopLeft)
		{
			RaiseScrollTo(TopLeft.Bounds);
		}

		public void DisplayCompletionList(IHasBounds nearToShape)
		{
			RaiseDisplayCompletionList(nearToShape);
		}

		public void ShowPopupMenu(ICommandList menu, System.Drawing.Point location)
		{
			RaiseShowPopupMenu(menu, location);
		}

		#endregion

		#region Styles

		//public void ReloadAllStyles()
		//{
		//    foreach (Control c in AllControls())
		//    {
		//        c.GetStylesFromFactory();
		//    }
		//}

		public IEnumerable<Control> AllControls()
		{
			return AllControls(this);
		}

		public IEnumerable<Control> AllControls(ContainerControl parent)
		{
			foreach (Control c in parent.Children)
			{
				yield return c;
				ContainerControl embedded = c as ContainerControl;
				if (embedded != null)
				{
					foreach (Control c2 in AllControls(embedded))
					{
						yield return c2;
					}
				}
			}
		}

		#endregion

		#region Views

		private List<IDrawWindow> mViews = new List<IDrawWindow>();
		public List<IDrawWindow> Views
		{
			get
			{
				return mViews;
			}
			set
			{
				mViews = value;
			}
		}

		public void AttachView(IDrawWindow view)
		{
			if (DefaultView == null)
			{
				DefaultView = view;
			}
			Views.Add(view);
		}

		public void DetachView(IDrawWindow view)
		{
			Views.Remove(view);
			if (DefaultView == view)
			{
				if (Views.Count > 0)
				{
					DefaultView = Views[0];
				}
				else
				{
					DefaultView = null;
				}
			}
		}

		private IDrawWindow mDefaultView = null;
		public IDrawWindow DefaultView
		{
			get
			{
				return mDefaultView;
			}
			set
			{
				mDefaultView = value;
			}
		}

		#endregion
	}
}
