using System.Collections.ObjectModel;

namespace GuiLabs.Editor.Blocks
{
	public interface IBlockProvider
	{
		ReadOnlyCollection<Block> GetBlocks();
	}
}
