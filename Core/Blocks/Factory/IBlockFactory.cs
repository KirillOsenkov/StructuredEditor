using System;
using System.Collections.ObjectModel;

namespace GuiLabs.Editor.Blocks
{
	public interface IBlockFactory
	{
		ReadOnlyCollection<Block> CreateBlocks();
		Block CreateBlock();
	}
}
