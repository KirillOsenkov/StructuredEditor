using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public class ScrollbarFactory
	{
		protected ScrollbarFactory()
		{
		}

		static ScrollbarFactory()
		{
			BackgroundStyle = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(System.Drawing.Color.WhiteSmoke);
			BorderStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(System.Drawing.Color.WhiteSmoke, 1);
			ButtonUpStyle = new ArrowButtonStyle(ArrowButtonStyle.ArrowType.Up);
			ButtonDownStyle = new ArrowButtonStyle(ArrowButtonStyle.ArrowType.Down);
			ButtonLeftStyle = new ArrowButtonStyle(ArrowButtonStyle.ArrowType.Left);
			ButtonRightStyle = new ArrowButtonStyle(ArrowButtonStyle.ArrowType.Right);
			MyThumbStyle = new ScrollbarButtonStyle();
		}

		private static ScrollbarFactory mInstance;
		public static ScrollbarFactory Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new ScrollbarFactory();
				}
				return mInstance;
			}
		}

		public virtual VScrollbar CreateVScrollbar(CompositeRange ViewRange)
		{
			VScrollbar NewScrollbar = new VScrollbar(ViewRange);
			NewScrollbar.SuspendLayout = true;
			NewScrollbar.Background = CreateRectangle();
			NewScrollbar.ButtonUp = CreateButtonUp();
			NewScrollbar.ButtonDown = CreateButtonDown();
			NewScrollbar.Thumb = CreateThumb();
			NewScrollbar.SuspendLayout = false;
			return NewScrollbar;
		}

		public virtual HScrollbar CreateHScrollbar(CompositeRange ViewRange)
		{
			HScrollbar NewScrollbar = new HScrollbar(ViewRange);
			NewScrollbar.SuspendLayout = true;
			NewScrollbar.Background = CreateRectangle();
			NewScrollbar.ButtonUp = CreateButtonLeft();
			NewScrollbar.ButtonDown = CreateButtonRight();
			NewScrollbar.Thumb = CreateThumb();
			NewScrollbar.SuspendLayout = false;
			return NewScrollbar;
		}

		private static ScrollbarButtonStyle ButtonUpStyle;
		private ScrollbarButton CreateButtonUp()
		{
			ArrowButton NewButton = new ArrowButton();
			NewButton.MyButtonStyle = ButtonUpStyle;
			return NewButton;
		}

		private static ScrollbarButtonStyle ButtonDownStyle;
		protected virtual ScrollbarButton CreateButtonDown()
		{
			ArrowButton NewButton = new ArrowButton();
			NewButton.MyButtonStyle = ButtonDownStyle;
			return NewButton;
		}

		private static ScrollbarButtonStyle ButtonLeftStyle;
		private ScrollbarButton CreateButtonLeft()
		{
			ArrowButton NewButton = new ArrowButton();
			NewButton.MyButtonStyle = ButtonLeftStyle;
			return NewButton;
		}

		private static ScrollbarButtonStyle ButtonRightStyle;
		protected virtual ScrollbarButton CreateButtonRight()
		{
			ArrowButton NewButton = new ArrowButton();
			NewButton.MyButtonStyle = ButtonRightStyle;
			return NewButton;
		}

		private static ScrollbarButtonStyle MyThumbStyle;
		protected virtual ScrollbarButton CreateThumb()
		{
			ScrollbarButton NewThumb = new ScrollbarButton();
			NewThumb.MyButtonStyle = MyThumbStyle;
			return NewThumb;
		}

		private static IFillStyleInfo BackgroundStyle;
		private static ILineStyleInfo BorderStyle;
		public virtual RectShape CreateRectangle()
		{
			RectShape newRect = new RectShape();
			newRect.Style.FillStyleInfo = BackgroundStyle;
			newRect.Style.LineStyleInfo = BorderStyle;
			return newRect;
		}
	}
}
