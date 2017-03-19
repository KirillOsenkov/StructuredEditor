namespace GuiLabs.Editor.CSharp
{
	public class ModifierVisibilityCondition
	{
		public virtual bool IsVisible(ModifierContainer modifiers, SetModifierCompletionListItem item)
		{
			return !modifiers.IsSet(item.Text);
		}

		public static NestedClassModifierAllowedRule NestedClass = new NestedClassModifierAllowedRule();
		public static ModifierVisibilityCondition Unique = new ModifierVisibilityCondition();
		public static StaticConstructorModifierAllowedRule StaticCtor = new StaticConstructorModifierAllowedRule();
	}

	public class StaticConstructorModifierAllowedRule : ModifierVisibilityCondition
	{
		public override bool IsVisible(ModifierContainer modifiers, SetModifierCompletionListItem item)
		{
			if (modifiers.IsSet("static"))
			{
				return false;
			}
			else
			{
				return Unique.IsVisible(modifiers, item);
			}
		}
	}

	public class NestedClassModifierAllowedRule : ModifierVisibilityCondition
	{
		public override bool IsVisible(ModifierContainer modifiers, SetModifierCompletionListItem item)
		{
			ClassOrStructBlock c = modifiers.ParentParent as ClassOrStructBlock;
			if (c != null)
			{
				return c.IsNested && base.IsVisible(modifiers, item);
			}
			return false;
		}
	}

	public class PartialClassModifierAllowedRule : ModifierVisibilityCondition
	{
		public override bool IsVisible(ModifierContainer modifiers, SetModifierCompletionListItem item)
		{
			ClassOrStructBlock c = modifiers.ParentParent as ClassOrStructBlock;
			if (c != null)
			{
				return !c.IsNested && base.IsVisible(modifiers, item);
			}
			return false;
		}
	}

	public class PrivateModifierAllowedRule : ModifierVisibilityCondition
	{
		public override bool IsVisible(ModifierContainer modifiers, SetModifierCompletionListItem item)
		{
			return base.IsVisible(modifiers, item);
		}
	}
}
