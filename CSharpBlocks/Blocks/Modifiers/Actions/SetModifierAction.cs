using GuiLabs.Editor.Actions;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class SetModifierAction : ModifierAction
	{
		#region ctor

		public SetModifierAction(ModifierContainer container, string modifier)
			: base(container, modifier)
		{
		}

		#endregion

		private string oldModifier = "";

		protected override void ExecuteCore()
		{
			ModifierSelectionBlock block = Container.ModifierBlocks.FindBlockForModifier(Modifier);
			if (block == null)
			{
				return;
			}

			if (block.Visible)
			{
				oldModifier = block.Text;
			}

			if (oldModifier != Modifier)
			{
				Container.SetCore(Modifier);
			}
		}

		protected override void UnExecuteCore()
		{
			if (oldModifier != "")
			{
				Container.SetCore(oldModifier);
			}
			else
			{
				Container.ClearModifierCore(Modifier);
			}
		}
	}
}
