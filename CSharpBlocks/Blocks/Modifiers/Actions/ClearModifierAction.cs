using GuiLabs.Editor.Actions;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class ClearModifierAction : ModifierAction
	{
		#region ctor

		public ClearModifierAction(ModifierContainer container, string modifier)
			: base(container, modifier)
		{
		}

		#endregion

		protected override void ExecuteCore()
		{
			Container.ClearModifierCore(Modifier);
		}

		protected override void UnExecuteCore()
		{
			Container.SetCore(Modifier);
		}
	}
}
