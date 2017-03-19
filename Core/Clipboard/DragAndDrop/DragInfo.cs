using GuiLabs.Canvas;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Clipboard
{
	public class DragInfo
	{
		public DragInfo(Block b, int x, int y)
		{
			Query = new DragQuery(b, x, y);
		}

		private DragQuery mQuery;
		public DragQuery Query
		{
			get
			{
				return mQuery;
			}
			set
			{
				mQuery = value;
			}
		}

		private DragQueryResult mResult;
		public DragQueryResult Result
		{
			get
			{
				return mResult;
			}
			set
			{
				mResult = value;
			}
		}

		private bool mDragStarted = false;
		public bool DragStarted
		{
			get
			{
				return mDragStarted;
			}
			set
			{
				mDragStarted = value;
			}
		}
	}
}