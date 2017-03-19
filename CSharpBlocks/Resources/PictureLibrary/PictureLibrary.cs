using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Canvas.Controls;
using System.Drawing;

namespace GuiLabs.Editor.CSharp
{
	public class CSharpPictureLibrary : PictureLibrary
	{
        public CSharpPictureLibrary() : base()
        {

        }

		private static CSharpPictureLibrary mInstance;
		public new static CSharpPictureLibrary Instance
		{
			get
			{
				if (mInstance == null) Initialize();
				return mInstance;
			}
			protected set
			{
				mInstance = value;
			}
		}

		private static void Initialize()
		{
			mInstance = new CSharpPictureLibrary();
		}

        public IPicture TypeClass = 
            RendererSingleton.StyleFactory.ProduceNewTransparentPicture(
                Icons.TypeClass, Color.White);
	}
}
