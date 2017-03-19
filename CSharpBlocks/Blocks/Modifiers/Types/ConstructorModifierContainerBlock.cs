using GuiLabs.Editor.Actions;
using GuiLabs.Undo;
namespace GuiLabs.Editor.CSharp
{
	public class ConstructorModifierContainerBlock : ModifierContainer
	{
		public ConstructorModifierContainerBlock()
			: base()
		{
			this.AddModifierKinds(
				"ConstructorAccess",
				"MethodStatic"
			);
		}

		public override void Set(string modifier)
		{
			if (modifier == "static")
			{
                if (this.ActionManager != null)
                {
                    using (this.Transaction())
                    {
                        if (IsModifierGroupSet("public"))
                        {
                            ClearModifierGroup("public");
                        }
                        base.Set(modifier);
                    }
                }
                else
                {
                    if (IsModifierGroupSet("public"))
                    {
                        ClearModifierGroup("public");
                    }
                    base.Set(modifier);
                }
			}
			else
			{
				base.Set(modifier);
			}
		}
	}
}
