using GuiLabs.Canvas.DrawOperations;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Canvas.Renderer
{
	public class RendererSingleton
	{
		protected static IRenderer mInstance;
		public static IRenderer Instance
		{
			get
			{
				if (mInstance == null)
				{
					Initialize();
				}
				return mInstance;
			}
		}

		public static void Initialize()
		{
			Dispose();
			mInstance = new GDIRenderer();
				// new GDIPlusRendererGDIBackBuffer();
		}

		public static void Dispose()
		{
			if (mInstance != null)
			{
				mInstance.Dispose();
				mInstance = null;
			}
		}

		public static IDrawInfoFactory StyleFactory
		{
			get
			{
				return Instance.DrawOperations.Factory;
			}
		}

		public static IDrawOperations DrawOperations
		{
			get
			{
				return Instance.DrawOperations;
			}
		}

		private static Caret mMyCaret = new Caret();
		public static Caret MyCaret
		{
			get { return mMyCaret; }
		}
	}
}