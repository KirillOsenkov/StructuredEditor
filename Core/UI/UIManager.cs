namespace GuiLabs.Editor.UI
{
	/// <summary>
	/// Access point for all User Interface goodies, like e.g. CompletionList
	/// </summary>
	public class UIManager
	{
		#region Singleton

		//protected UIManager()
		//{
		//    mDropDownList = new CompletionList();
		//}

		//private static UIManager mInstance = null;
		//public static UIManager Instance
		//{
		//    get
		//    {
		//        if (mInstance == null)
		//        {
		//            Initialize();
		//            if (mInstance == null) // double-check just to be sure
		//            {
		//                mInstance = new UIManager();
		//            }
		//        }
		//        return mInstance;
		//    }
		//}

		//private static void Initialize()
		//{
		//    mInstance = new UIManager();
		//}

		#endregion

		//public static ICompletionList CompletionList
		//{
		//    get
		//    {
		//        return Instance.DropDownList;
		//    }
		//}

		private static ICompletionList mDropDownList = new CompletionList();
		public static ICompletionList DropDownList
		{
			get
			{
				return mDropDownList;
			}
			set
			{
				mDropDownList = value;
			}
		}

	}
}
