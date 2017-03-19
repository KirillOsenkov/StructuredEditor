using System.Collections.ObjectModel;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Sample
{
	public class TutorialEmptyBlock : EmptyBlock
	{
		public TutorialEmptyBlock()
		{
			Completion.AddCreateBlocksItem<TutorialUniversalBlock, TutorialEmptyBlock>("universal");

			CreateBlocksItem createText = Completion.AddCreateBlocksItem<TextBoxBlock>("text");
			MoreThan3TextBlocksCondition createTextVisible = new MoreThan3TextBlocksCondition(this);
			createText.VisibilityConditions.Add(createTextVisible);
		}

		private class MoreThan3TextBlocksCondition : ICondition
		{
			public MoreThan3TextBlocksCondition(EmptyBlock parent)
			{
				Parent = parent;
			}

			private EmptyBlock Parent;

			public bool IsTrue()
			{
				ReadOnlyCollection<TextBoxBlock> textBoxes =
					Parent.Root.FindChildren<TextBoxBlock>();
				return textBoxes.Count < 3;
			}
		}
	}
}
