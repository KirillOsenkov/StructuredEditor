using System;
using System.Collections.Generic;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class ReplaceBlocksItem : CreateBlocksItem
	{
		#region ctors

		public ReplaceBlocksItem(
			string text,
			IEnumerable<Type> blockTypes)
			: base(text, blockTypes)
		{
		}

		public ReplaceBlocksItem(
			string text,
			IBlockFactory factory)
			: base(text, factory)
		{
		}

		#endregion

		new public static ReplaceBlocksItem Create<T>(string text)
		{
			ReplaceBlocksItem result = new ReplaceBlocksItem(
				text,
				BlockActivatorFactory.Types<T>()
			);
			return result;
		}

		new public static CreateBlocksItem Create<T1, T2>(string text)
		{
			return new ReplaceBlocksItem(text, BlockActivatorFactory.Types<T1, T2>());
		}

		public override void Click(CompletionFunctionality hostItemList)
		{
			Block reference = hostItemList.HostBlock;
			if (ReferenceBlock != null)
			{
				reference = ReferenceBlock(reference);
			}
			if (reference != null)
			{
				reference.Replace(Factory.CreateBlocks());
			}
		}
	}
}
