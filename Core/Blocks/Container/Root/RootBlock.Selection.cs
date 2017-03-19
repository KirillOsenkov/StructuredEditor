using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.Events;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.UI;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Editor.Blocks
{
	partial class RootBlock
	{
		private Selection mCurrentSelection;
		public Selection CurrentSelection
		{
			get { return mCurrentSelection; }
			set { mCurrentSelection = value; }
		}

		//public void OnBlockMouseMove(Block block, MouseWithKeysEventArgs MouseInfo)
		//{
		//    if (MouseInfo.IsLeftButtonPressed)
		//    {
		//        if (block != ActiveBlock)
		//        {
		//            if (CurrentSelection.MouseStart != ActiveBlock ||
		//               CurrentSelection.MouseEnd != block)
		//            {
		//                //CurrentSelection.FindSelection(ActiveBlock, block);
		//            }
		//        }
		//        else
		//        {
		//            if (CurrentSelection.MouseStart != ActiveBlock ||
		//                CurrentSelection.MouseEnd != block)
		//            {
		//                CurrentSelection.Clear();
		//            }

		//        }
		//    }
		//}
	}
}