using System;
using GuiLabs.Editor.Blocks;
using GuiLabs.Undo;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Actions
{
    public static class Extensions
    {
        public static void SetFocus(this Block toFocus, SetFocusOptions options)
        {
            SetFocusAction action = new SetFocusAction(toFocus, options);
            toFocus.ActionManager.RecordAction(action);
        }

        public static void SetFocus(this Block toFocus, SetFocusOptions options, ActionManager actionManager)
        {
            SetFocusAction action = new SetFocusAction(toFocus, options);
            actionManager.RecordAction(action);
        }

        public static SingleRedrawTransaction Transaction(this Block block)
        {
            return new SingleRedrawTransaction(block);
        }

        public static SingleRedrawTransaction Transaction(this Block block, bool isDelayed)
        {
            return new SingleRedrawTransaction(block, isDelayed);
        }
    }

    public class SingleRedrawTransaction : Transaction
	{
        public SingleRedrawTransaction(Block block) : this(block, true)
        {
        }

        public SingleRedrawTransaction(Block block, bool isDelayed) : base(block.ActionManager, isDelayed)
        {
            redrawer = new Redrawer(block.Root, true);
        }

        Redrawer redrawer;

        public override void Dispose()
        {
            base.Dispose();
            redrawer.Dispose();
        }
	}
}
