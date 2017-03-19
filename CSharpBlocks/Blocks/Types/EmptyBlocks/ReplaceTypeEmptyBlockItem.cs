using System;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using System.Collections.ObjectModel;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class ReplaceTypeEmptyBlockItem : ReplaceBlocksItem
	{
		#region ctors

		public ReplaceTypeEmptyBlockItem(
			string text,
			IEnumerable<Type> blockTypes,
			TypeEmptyBlock empty)
			: base(text, blockTypes)
		{
			emptyBlock = empty;
		}

		public ReplaceTypeEmptyBlockItem(
			string text,
			IBlockFactory factory,
			TypeEmptyBlock empty)
			: base(text, factory)
		{
			emptyBlock = empty;
		}

		#endregion

		private TypeEmptyBlock emptyBlock;

		public override void Click(CompletionFunctionality hostItemList)
		{
			Block reference = emptyBlock;

			ReadOnlyCollection<Block> blocksToInsert = Factory.CreateBlocks();

			if (reference != null 
				&& blocksToInsert != null
				&& blocksToInsert.Count > 0)
			{
				string modifiers = emptyBlock.GetModifierString();
				IHasModifiers result = blocksToInsert[0] as IHasModifiers;
				if (result != null)
				{
					result.Modifiers.SetMany(modifiers);
				}

				reference.Replace(blocksToInsert);
			}
		}
	}
}
