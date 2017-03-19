//#define UseOldContextMenu

using System;
using System.ComponentModel;
using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Editor.UI
{
	public class ViewWindow : DrawWindow
	{
		public ViewWindow()
			: base()
		{
			MainShape = new ShapeWithScrollbar();
			this.Repaint += OnWindowRepaint;
			//this.RegionRepaint += OnWindowRegionRepaint;
			this.Resize += OnWindowResize;
		}

		#region Menu

#if UseOldContextMenu
		private System.Windows.Forms.ContextMenu strip = new System.Windows.Forms.ContextMenu();
#else
		private System.Windows.Forms.ContextMenuStrip strip = new System.Windows.Forms.ContextMenuStrip();
#endif

		void MyRootControl_ShouldShowPopupMenu(ICommandList menu, System.Drawing.Point location)
		{
#if UseOldContextMenu
			strip.MenuItems.Clear();
#else
			strip.Items.Clear();
#endif

			foreach (ICommand item in menu)
			{
#if UseOldContextMenu
				OldStyleMenuCommand i = new OldStyleMenuCommand(item);
				strip.MenuItems.Add(i);
#else
				MenuCommand i = new MenuCommand(item);
				strip.Items.Add(i);
#endif
			}

			location.X += -MainShape.Scrollable.DeltaX;
			location.Y += -MainShape.Scrollable.DeltaY;

			strip.Show(this, location);
		}

		#endregion

		#region OnEvents

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if (this.RootBlock == null || this.RootBlock.MyRootControl == null)
			{
				return;
			}
			using (Redrawer a = new Redrawer(this.RootBlock))
			{
				base.OnMouseDown(e);
			}
		}

		//protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		//{
		//    if (this.RootBlock == null || this.RootBlock.MyRootControl == null)
		//    {
		//        return;
		//    }
		//    using (RedrawAccumulator a = new RedrawAccumulator(this.RootBlock.MyRootControl))
		//    {
		//        base.OnMouseMove(e);
		//    }
		//}

		//protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		//{
		//    if (this.RootBlock == null || this.RootBlock.MyRootControl == null)
		//    {
		//        return;
		//    }
		//    using (RedrawAccumulator a = new RedrawAccumulator(this.RootBlock.MyRootControl))
		//    {
		//        base.OnMouseUp(e);
		//    }
		//}

		#endregion

		#region RootBlock

		private RootBlock mRootBlock;
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RootBlock RootBlock
		{
			get
			{
				return mRootBlock;
			}
			set
			{
				DefaultKeyHandler = null;
				if (mRootBlock != null)
				{
					mRootBlock.MyRootControl.DetachView(this);
					mRootBlock.MyRootControl.ShouldScrollTo -= MyRootControl_ShouldScrollTo;
					mRootBlock.MyRootControl.ShouldDisplayCompletionList -= MyRootControl_ShouldDisplayCompletionList;
					mRootBlock.MyRootControl.ShouldShowPopupMenu -= MyRootControl_ShouldShowPopupMenu;
				}
				mRootBlock = value;
				if (mRootBlock != null)
				{
					mRootBlock.MyRootControl.AttachView(this);
					mRootBlock.MyRootControl.ShouldScrollTo += MyRootControl_ShouldScrollTo;
					mRootBlock.MyRootControl.ShouldDisplayCompletionList += MyRootControl_ShouldDisplayCompletionList;
					mRootBlock.MyRootControl.ShouldShowPopupMenu += MyRootControl_ShouldShowPopupMenu;

					MainShape.Scrollable.Content = mRootBlock.MyRootControl;

					//MainShape = new ShapeWithScrollbar(mRootBlock.MyRootControl);
					//MainShape.ShouldHandleScrollRequestsFromContent = false;
					RecalcBounds();
					this.Redraw();
					DefaultKeyHandler = mRootBlock.MyRootControl;
				}
			}
		}

		#region Event handlers

		//void MyRootControl_ShapeNeedsRedraw(IDrawableRect ShapeToRedraw)
		//{
		//    this.Redraw(ShapeToRedraw);
		//}

		void MyRootControl_ShouldDisplayCompletionList(IHasBounds nearToShape)
		{
			System.Drawing.Rectangle R;

			R = new System.Drawing.Rectangle
			(
				nearToShape.Bounds.Location.X - MainShape.Scrollable.DeltaX + this.Left,
				nearToShape.Bounds.Location.Y - MainShape.Scrollable.DeltaY + this.Top,
				nearToShape.Bounds.Size.X,
				nearToShape.Bounds.Size.Y
			);

			UIManager.DropDownList.Show(R, this);
		}

		private void MyRootControl_ShouldScrollTo(Rect shape)
		{
			const int horizBorder = 150;
			const int verticBorder = 50;

			Rect.RectangleRelativePosition RelativePosition = shape.RelativeToRectY(MainShape.Scrollable.ViewPort);
			int x = shape.Location.X;
			int y = shape.Location.Y;
			int totalHeight = RootBlock.MyRootControl.Bounds.Size.Y;
			int viewportLeft = MainShape.Scrollable.ViewPort.Location.X;
			int viewportRight = MainShape.Scrollable.ViewPort.Right;
			int viewportHeight = MainShape.Scrollable.ViewPort.Size.Y;
			int viewportTop = MainShape.Scrollable.ViewPort.Top;

			bool shouldScrollX = false;
			bool shouldScrollY = false;
			int scrollToX = 0;
			int scrollToY = 0;

			switch (RelativePosition)
			{
				case Rect.RectangleRelativePosition.FullyContaining:
				case Rect.RectangleRelativePosition.IntersectingBeginning:
				case Rect.RectangleRelativePosition.FullyBefore:
				case Rect.RectangleRelativePosition.IntersectingEnd:
				case Rect.RectangleRelativePosition.FullyAfter:
					shouldScrollY = true;
					scrollToY = y - verticBorder;
					break;
				case Rect.RectangleRelativePosition.FullyInside:
				default:
					if (totalHeight > viewportHeight
						&& viewportHeight > 300
						&& totalHeight - y < 300)
					{
						shouldScrollY = true;
						scrollToY = totalHeight;
					}
					else if (y > viewportTop && y < viewportTop + verticBorder)
					{
						shouldScrollY = true;
						scrollToY = y - verticBorder;
					}
					break;
			}

			if (x < viewportLeft + horizBorder)
			{
				shouldScrollX = true;
				scrollToX = x - horizBorder;
			}

			if (x > viewportRight - horizBorder)
			{
				shouldScrollX = true;
				scrollToX = x + horizBorder;
			}

			if (shouldScrollX && shouldScrollY)
			{
				MainShape.ScrollContentXY(scrollToX, scrollToY);
			}
			else if (shouldScrollX)
			{
				MainShape.ScrollContentX(scrollToX);
			}
			else if (shouldScrollY)
			{
				MainShape.ScrollContentY(scrollToY);
			}
		}

		#endregion

		#endregion

		#region MainShape

		private ShapeWithScrollbar mMainShape;
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ShapeWithScrollbar MainShape
		{
			get
			{
				return mMainShape;
			}
			set
			{
				this.DefaultMouseHandler = null;
				if (mMainShape != null)
				{
					mMainShape.NeedRedraw -= mMainShape_NeedRedraw;
				}
				mMainShape = value;
				if (mMainShape != null)
				{
					mMainShape.NeedRedraw += mMainShape_NeedRedraw;
					this.DefaultMouseHandler = mMainShape;
					mMainShape.ScrollbarsVisible = mShowScrollbars;
					mMainShape.ShouldHandleScrollRequestsFromContent = false;
				}
			}
		}

		private bool mShowScrollbars = true;
		[Description("Determines if the control displays or hides the two scrollbars. "
			+ "If true, the scrollbars will be shown all the time, "
			+ "even if the content doesn't need to be scrolled. "
			+ "If false, the scrollbars won't be shown even if the content "
			+ "requires scrolling.")]
		public bool ShowScrollbars
		{
			get
			{
				if (MainShape != null && MainShape.ScrollbarsVisible != mShowScrollbars)
				{
					mShowScrollbars = MainShape.ScrollbarsVisible;
				}
				return mShowScrollbars;
			}
			set
			{
				if (MainShape != null)
				{
					MainShape.ScrollbarsVisible = value;
				}
				mShowScrollbars = value;
			}
		}

		#endregion

		#region Repaint

		protected void mMainShape_NeedRedraw(IDrawableRect ShapeToRedraw)
		{
			Redraw();
		}

		/// <summary>
		/// Happens when the Window asks to repaint itself
		/// </summary>
		/// <param name="Renderer"></param>
		protected void OnWindowRepaint(IRenderer Renderer)
		{
			if (MainShape != null)
			{
				MainShape.Draw(Renderer);
				//Renderer.DrawOperations.DrawPie(
				//    MainShape.Bounds,
				//    72, 260,
				//    System.Drawing.Color.Bisque,
				//    System.Drawing.Color.AntiqueWhite);
			}
		}

		//protected void OnWindowRegionRepaint(IRenderer Renderer, IDrawableRect RegionToRedraw)
		//{
		//    if (MainShape != null)
		//    {
		//        MainShape.Scrollable.DrawShape(Renderer, RegionToRedraw);
		//    }
		//}

		#endregion

		#region Resize

		private void OnWindowResize(object sender, EventArgs e)
		{
			RecalcBounds();
			this.Redraw();
		}

		public override Point GetClientSize()
		{
			return MainShape.Scrollable.Bounds.Size;
		}

		public void RecalcBounds()
		{
			if (MainShape != null && RootBlock != null)
			{
				if (RootBlock.MyRootControl.StretchToWindow)
				{
					bool showScrollbars = ShowScrollbars;
					int VerticalScrollbarWidth = showScrollbars ? MainShape.VScroll.Bounds.Size.X : 0;
					int HorizontalScrollbarHeight = showScrollbars ? MainShape.HScroll.Bounds.Size.Y : 0;

					RootBlock.MyRootControl.Bounds.Size.X = this.ClientSize.Width - VerticalScrollbarWidth;
					RootBlock.MyRootControl.Bounds.Size.Y = this.ClientSize.Height - HorizontalScrollbarHeight;
					RootBlock.MyRootControl.LayoutDock();
				}

				MainShape.Bounds.Size.Set(this.Bounds.Size);
				MainShape.LayoutCore();
			}
		}

		#endregion
	}
}
