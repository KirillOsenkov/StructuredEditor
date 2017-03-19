using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.UI;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.Actions;
using GuiLabs.Undo;

namespace GuiLabs.Editor.Xml
{
	public class CreateTagItem : ReplaceBlocksItem
	{
		public CreateTagItem(string text) 
			: base(text, BlockActivatorFactory.Types<NodeBlock>())
		{

		}

		public override void Click(CompletionFunctionality hostItemList)
		{
			NodeBlock node = new NodeBlock();
			node.NameBlock.Text = this.Text;
			using (hostItemList.HostBlock.Transaction())
			{
				hostItemList.HostBlock.Replace(node);
				if (Reason.Type == ItemClickSource.KeyPress && Reason.KeyChar == ' ')
				{
					node.Attributes.SetFocus(SetFocusOptions.ToBeginning);
				}
			}
		}
	}
}
