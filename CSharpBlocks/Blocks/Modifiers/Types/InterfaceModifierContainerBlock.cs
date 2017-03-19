namespace GuiLabs.Editor.CSharp
{
	public class InterfaceModifierContainerBlock : ModifierContainer
	{
		public InterfaceModifierContainerBlock()
		{
			this.AddModifierKinds(
				"ClassAccess",
				"Partial"
			);
		}
	}
}
