using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public class SetModifierCompletionListItem : CompletionListItem
	{
		#region ctors

		public SetModifierCompletionListItem(
			ModifierContainer container, 
			string modifier)
			: base(modifier)
		{
			Container = container;
		}

		public SetModifierCompletionListItem(
			ModifierContainer container,
			string modifier,
			ModifierVisibilityCondition condition
			)
			: base(modifier)
		{
			Container = container;
			VisibilityCondition = condition;
		}

		#endregion

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

		private ModifierVisibilityCondition mVisibilityCondition;
		public ModifierVisibilityCondition VisibilityCondition
		{
			get
			{
				return mVisibilityCondition;
			}
			set
			{
				mVisibilityCondition = value;
			}
		}

		public override void Click(CompletionFunctionality hostItemList)
		{
			Container.Set(this.Text);
		}

		public override bool ShouldShow(CompletionFunctionality hostItemList)
		{
			if (VisibilityCondition != null)
			{
				return VisibilityCondition.IsVisible(Container, this);
			}
			return true;
		}
	}
}
