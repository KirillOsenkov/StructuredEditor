namespace GuiLabs.Editor.CSharp
{
	public class ClassModifierContainerBlock : ModifierContainer
	{
		public ClassModifierContainerBlock()
		{
			this.AddModifierKinds(
				"ClassAccess",
				"ClassInherit",
				"Partial"
			);
		}
	}
}
