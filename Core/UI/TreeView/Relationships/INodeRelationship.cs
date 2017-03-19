using System;
namespace GuiLabs.Editor.UI
{
    public interface INodeRelationship
    {
        System.Collections.Generic.List<GuiLabs.Editor.Blocks.Block> Receivers { get; set; }
        GuiLabs.Editor.Blocks.Block Sender { get; set; }
		
		/* true: sender to receiver, false: receiver to sender */
		bool Direction { get; set; }
    }
}
