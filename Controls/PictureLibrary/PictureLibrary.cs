using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Canvas.Controls;

namespace GuiLabs.Canvas.Controls
{
	public class PictureLibrary
	{
		protected PictureLibrary()
		{
            Plus = RendererSingleton.StyleFactory.ProduceNewPicture(
                Resources.plus);
			Minus = RendererSingleton.StyleFactory.ProduceNewPicture(
				Resources.minus);
            Delete = RendererSingleton.StyleFactory.ProduceNewPicture(
                Resources.delete);
            Delete2 = RendererSingleton.StyleFactory.ProduceNewTransparentPicture(
                Resources.delete1,
                System.Drawing.Color.Magenta
                );
            Delete2 = RendererSingleton.StyleFactory.ProduceNewTransparentPicture(
                Resources.delete2,
                System.Drawing.Color.Magenta
                );
			NotCollapsed = RendererSingleton.StyleFactory.ProduceNewTransparentPicture(
				Resources.expanded,
				System.Drawing.Color.Magenta
				);
			Collapsed = RendererSingleton.StyleFactory.ProduceNewTransparentPicture(
				Resources.collapsed,
				System.Drawing.Color.Magenta
				);
		}

		private static PictureLibrary mInstance;
		public static PictureLibrary Instance
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
			mInstance = new PictureLibrary();
		}

		private IPicture mPlus;
		public IPicture Plus
		{
			get { return mPlus; }
			set { mPlus = value; }
		}

		private IPicture mMinus;
		public IPicture Minus
		{
			get { return mMinus; }
			set { mMinus = value; }
		}

        private IPicture mDelete;
        public IPicture Delete
        {
            get { return mDelete; }
            set { mDelete = value; }
        }

        private IPicture mDelete2;
        public IPicture Delete2
        {
            get { return mDelete2; }
            set { mDelete2 = value; }
        }

		private IPicture mCollapsed;
		public IPicture Collapsed
		{
			get
			{
				return mCollapsed;
			}
			set
			{
				mCollapsed = value;
			}
		}

		private IPicture mNotCollapsed;
		public IPicture NotCollapsed
		{
			get
			{
				return mNotCollapsed;
			}
			set
			{
				mNotCollapsed = value;
			}
		}

		#region Images

		public System.Drawing.Image ImagePlus
		{
			get
			{
				return Resources.plus;
			}
		}

		public System.Drawing.Image ImageMinus
		{
			get
			{
				return Resources.minus;
			}
		}

		public System.Drawing.Image ImageMenuDelete
		{
			get
			{
				return Resources.MenuDelete;
			}
		}

		public System.Drawing.Image ImageCut
		{
			get
			{
				return Resources.Cut;
			}
		}

		public System.Drawing.Image ImageCopy
		{
			get
			{
				return Resources.Copy;
			}
		}

		public System.Drawing.Image ImagePaste
		{
			get
			{
				return Resources.Paste;
			}
		}

		#endregion
	}
}
