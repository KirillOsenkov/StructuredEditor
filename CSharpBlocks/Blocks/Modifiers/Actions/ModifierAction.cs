using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.CSharp
{
	public abstract class ModifierAction : RootBlockAction
	{
		#region ctor

		protected ModifierAction(ModifierContainer container, string modifier)
			: base(container.Root)
		{
			Container = container;
			Modifier = modifier;
		}

		#endregion

		private string mModifier;
		public string Modifier
		{
			get
			{
				return mModifier;
			}
			set
			{
				mModifier = value;
			}
		}
		
		private ModifierContainer mContainer;
		public ModifierContainer Container
		{
			get
			{
				return mContainer;
			}
			set
			{
				mContainer = value;
			}
		}
	}
}
