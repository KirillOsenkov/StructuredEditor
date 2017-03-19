using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.Lists
{
	public interface IBlockList : IEnumerable<Block>, ICollectionWithEvents<Block>
	{
		void Add(Block newBlock);
		void Add(string labelText);
		void Add(params Block[] blocksToAdd);
		void Add(IEnumerable<Block> blocksToAdd);
		void Append(Block oldBlock, Block newBlock);
		void Append(Block oldBlock, IEnumerable<Block> blocksToAppend);
		void Append(Block oldBlock, params Block[] blocksToAppend);
		void Prepend(Block newHead);
		void Prepend(IEnumerable<Block> blocksAtTheBeginning);
		void Prepend(Block oldBlock, Block newBlock);
		void Prepend(Block oldBlock, IEnumerable<Block> blocksToPrepend);
		void Prepend(Block oldBlock, params Block[] blocksToPrepend);
		void Replace(Block oldBlock, Block newBlock);
		void Replace(Block oldBlock, IEnumerable<Block> blocksToReplaceWith);
		void Replace(Block oldBlock, params Block[] blocksToReplaceWith);
		void Delete(Block oldBlock);
		void Delete(IEnumerable<Block> blocksToDelete);
		void Delete(params Block[] blocksToDelete);

		void Insert(Block newBlock, int index, bool shouldReplace);
		void Remove(int index);

		int GetIndex(Block block);
		Block GetBlockFromIndex(int index);

		Block[] ToArray();

		ICollectionWithEvents<Control> Controls { get; }

		bool IsEmpty();

		//Block Head
		//{
		//    get;
		//    //set;
		//}
		//Block Tail
		//{
		//    get;
		//    //set;
		//}
	}
}
