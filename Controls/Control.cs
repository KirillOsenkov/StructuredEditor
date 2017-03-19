using System;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Canvas.Controls
{
	public partial class Control : ShapeWithEvents
	{
		#region ctor

		public Control()
			: base()
		{
			GetStylesFromFactory();
		}

		#endregion

		#region Parent & Root

		private ContainerControl mParent;
		public ContainerControl Parent
		{
			get
			{
				return mParent;
			}
			set
			{
				mParent = value;
			}
		}

		private RootControl mRoot;
		public virtual RootControl Root
		{
			get
			{
				return mRoot;
			}
			set
			{
				mRoot = value;
			}
		}

		public virtual void DisplayCompletionList()
		{
			if (this.Root != null)
			{
				this.Root.DisplayCompletionList(this);
			}
		}

		#endregion

		public virtual void LayoutAll()
		{
			Layout();
		}
	}
}
