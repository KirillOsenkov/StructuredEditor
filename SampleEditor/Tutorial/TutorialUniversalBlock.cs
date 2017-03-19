using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Sample
{
	public class TutorialUniversalBlock : UniversalBlock
	{
		public TutorialUniversalBlock() : base()
		{
			this.HMembers.Add("Universal block: ");
			this.HMembers.Add(new TextBoxBlock());
			this.VMembers.Add(new TutorialEmptyBlock());
		}
	}
}
