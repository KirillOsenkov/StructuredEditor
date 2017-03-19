using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.UI;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class EmptyBlockItem : CreateBlocksItem
	{
		public EmptyBlockItem(string displayName, IBlockFactory factory)
			: base(displayName, factory)
		{
			
		}

		public override void Click(CompletionFunctionality hostItemList)
		{
			List<Block> blocks = new List<Block>(this.Factory.CreateBlocks());
			IHasModifiers newEmpty = blocks[0] as IHasModifiers;
			if (newEmpty != null)
			{
				newEmpty.Modifiers.SetMany(this.Text);
			}
			hostItemList.HostBlock.AppendBlocks(blocks);
		}
	}
}
