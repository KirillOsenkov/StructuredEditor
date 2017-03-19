using System;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using System.Collections.ObjectModel;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class ReplaceMemberEmptyBlockItem : ReplaceBlocksItem
	{
		#region ctors

		public ReplaceMemberEmptyBlockItem(
			string text,
			IEnumerable<Type> blockTypes,
			FieldBlock empty)
			: base(text, blockTypes)
		{
			emptyBlock = empty;
		}

		public ReplaceMemberEmptyBlockItem(
			string text,
			IBlockFactory factory,
			FieldBlock empty)
			: base(text, factory)
		{
			emptyBlock = empty;
		}

		#endregion

		private FieldBlock emptyBlock;

		public override void Click(CompletionFunctionality hostItemList)
		{
			Block reference = emptyBlock;

			ReadOnlyCollection<Block> blocksToInsert = Factory.CreateBlocks();

			if (reference != null 
				&& blocksToInsert != null
				&& blocksToInsert.Count > 0)
			{
				string modifiers = emptyBlock.Modifiers.GetModifierString();
				IHasModifiers result = blocksToInsert[0] as IHasModifiers;
				if (result != null)
				{
					result.Modifiers.SetMany(modifiers);
					if (this.Text == "void")
					{
						result.Modifiers.Set("void");
					}
				}
				IHasName named = blocksToInsert[0] as IHasName;
				if (named != null)
				{
					named.Name = emptyBlock.Name;
				}

				reference.Replace(blocksToInsert);
			}
		}
	}
}
