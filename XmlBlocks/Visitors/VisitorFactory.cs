using System.IO;

namespace GuiLabs.Editor.Xml
{
	public class VisitorFactory
	{
		//private static DrawVisitor mDrawingVisitor = new DrawVisitor();
		//public static DrawVisitor DrawingVisitor
		//{
		//    get
		//    {
		//        return mDrawingVisitor;
		//    }
		//}

		//private static LayoutVisitor mLayoutingVisitor = new LayoutVisitor();
		//public static LayoutVisitor LayoutingVisitor
		//{
		//    get
		//    {
		//        return mLayoutingVisitor;
		//    }
		//}


		//public static void PrepareXMLVisitor(StreamWriter Writer)
		//{
		//    mXMLVisitor = new XMLVisitor(Writer);
		//}

		//private static XMLVisitor mXMLVisitor = new XMLVisitor(null);
		//public static XMLVisitor XMLVisitor
		//{
		//    get
		//    {
		//        return mXMLVisitor;
		//    }
		//}
		protected VisitorFactory()
		{
		}

		private static VisitorFactory mInstance = null;
		public static VisitorFactory Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new VisitorFactory();
				}
				return mInstance;
			}
		}


	}
}

