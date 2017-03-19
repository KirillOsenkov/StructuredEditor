using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class Redrawer : RedrawAccumulator
	{
		#region ctors

		public Redrawer(RootBlock rootBlock)
			: this(rootBlock, true)
		{
		}

		public Redrawer(RootBlock rootBlock, bool shouldRedrawAtTheEnd)
			: base()
		{
			this.ShouldRedrawAtTheEnd = shouldRedrawAtTheEnd;
			RootBlock = rootBlock;
			if (RootBlock != null)
			{
				Root = RootBlock.MyRootControl;
				Root.RedrawRequestsCount++;
			}
		}

		#endregion

        public RootBlock RootBlock { get; set; }

		public override void Dispose()
		{
			if (RootBlock != null)
			{
				Root.RedrawRequestsCount--;
				if (Root.RedrawRequestsCount == 0 && ShouldRedrawAtTheEnd)
				{
					Root.Redraw();
				}
			}
		}
	}
}
