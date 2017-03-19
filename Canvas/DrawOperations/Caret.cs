using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Renderer
{
	public class Caret : IHasBounds
	{
		public Caret()
		{
			CaretStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(System.Drawing.Color.Black, 1);
		}

		private ILineStyleInfo mCaretStyle;
		public ILineStyleInfo CaretStyle
		{
			get { return mCaretStyle; }
			set { mCaretStyle = value; }
		}

		private Rect mBounds = new Rect();
		public Rect Bounds
		{
			get { return mBounds; }
			set { mBounds = value; }
		}

		public void SetNewPosition(int x, int y)
		{
			Bounds.Location.X = x;
			Bounds.Location.Y = y;
		}

		public void SetNewSize(int height)
		{
			Bounds.Size.Y = height;
		}

		public void SetNewBounds(int x, int y, int height)
		{
			Bounds.Location.X = x;
			Bounds.Location.Y = y;
			Bounds.Size.Y = height;
		}

		private bool mVisible = false;
		public bool Visible
		{
			get
			{
				return mVisible;
			}
			set
			{
				mVisible = value;
			}
		}
	}
}
