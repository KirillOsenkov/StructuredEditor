using GuiLabs.Canvas;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;

namespace GuiLabs.Editor.CSharp
{
	public class Settings
	{
		protected Settings()
		{
		}

		private static Settings mCurrent = null;
		public static Settings Current
		{
			get
			{
				if (mCurrent == null)
				{
					mCurrent = new Settings();
				}
				return mCurrent;
			}
			set
			{
				if (value != null)
				{
					mCurrent = value;
				}
			}
		}

		private UniversalControl.UniversalControlDesign mDesignOfUniversalControl = UniversalControl.Design;
		public UniversalControl.UniversalControlDesign DesignOfUniversalControl
		{
			get
			{
				return mDesignOfUniversalControl;
			}
			set
			{
				mDesignOfUniversalControl = value;
				UniversalControl.Design = value;
			}
		}

		private UniversalControl.TypeOfCurlies mCurliesInUniversalControl = UniversalControl.DefaultCurlyType;
		public UniversalControl.TypeOfCurlies CurliesInUniversalControl
		{
			get
			{
				return mCurliesInUniversalControl;
			}
			set
			{
				mCurliesInUniversalControl = value;
				UniversalControl.DefaultCurlyType = value;
			}
		}

		private int mFontSize = ShapeStyle.DefaultFontSize;
		public int FontSize
		{
			get
			{
				return mFontSize;
			}
			set
			{
				mFontSize = value;
			}
		}

		//private VisibilityFilterKind mBlockFilter = VisibilityFilterKind.ShowAll;
		//public VisibilityFilterKind BlockFilter
		//{
		//    get
		//    {
		//        return mBlockFilter;
		//    }
		//    set
		//    {
		//        mBlockFilter = value;
		//    }
		//}

	}
}
