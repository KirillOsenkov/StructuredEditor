using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Lists
{
	public interface IChildrenList : IBlockList
	{
		event BlockKeyDownEventHandler KeyDown;
	}
}
