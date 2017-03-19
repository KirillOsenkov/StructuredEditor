using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierListFactory
	{
		protected ModifierListFactory()
		{

		}

		private static ModifierListFactory mInstance;
		private static ModifierListFactory Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new ModifierListFactory();
				}
				return mInstance;
			}
			set
			{
				mInstance = value;
			}
		}
		
		private static IModifierList mEmpty = Get("EmptyList");
		public static IModifierList Empty
		{
			get
			{
				return mEmpty;
			}
		}

		private Dictionary<string, IModifierList> collection
			= new Dictionary<string, IModifierList>();

		private IModifierList CreateModifiersList(string kind)
		{
			ModifierList list = new ModifierList(kind);

			switch (kind)
			{
				case "MemberAccess":
					list.Add(Keywords.Public, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Protected, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Internal, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.ProtectedInternal, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Private, ModifierVisibilityCondition.Unique);
					break;

				case "ConstructorAccess":
					list.Add(Keywords.Public, ModifierVisibilityCondition.StaticCtor);
					list.Add(Keywords.Protected, ModifierVisibilityCondition.StaticCtor);
					list.Add(Keywords.Internal, ModifierVisibilityCondition.StaticCtor);
					list.Add(Keywords.ProtectedInternal, ModifierVisibilityCondition.StaticCtor);
					list.Add(Keywords.Private, ModifierVisibilityCondition.StaticCtor);
					break;

				case "ClassAccess":
					list.Add(Keywords.Public, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Internal, ModifierVisibilityCondition.Unique);

					list.Add(Keywords.Protected, ModifierVisibilityCondition.NestedClass);
					list.Add(Keywords.ProtectedInternal, ModifierVisibilityCondition.NestedClass);
					list.Add(Keywords.Private, ModifierVisibilityCondition.NestedClass);
					list.Add(Keywords.New, ModifierVisibilityCondition.NestedClass);
					break;

				case "ClassInherit":
					list.Add(Keywords.Abstract, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Sealed, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Static, ModifierVisibilityCondition.Unique);
					break;

				case "Abstract":
					break;

				case "Extern":
					list.Add(Keywords.Extern, ModifierVisibilityCondition.Unique);
					break;

				case "New":
					list.Add(Keywords.New, ModifierVisibilityCondition.Unique);
					break;

				case "MethodInheritance":
					list.Add(Keywords.Abstract, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Virtual, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Override, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.SealedOverride, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.Static, ModifierVisibilityCondition.Unique);
					break;

				case "Partial":
					list.Add(Keywords.Partial, ModifierVisibilityCondition.Unique);
					break;

				case "Static":
					list.Add(Keywords.Static, ModifierVisibilityCondition.Unique);
					break;

				case "Virtual":
					break;

				case "MethodNew":
					list.Add(Keywords.New, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.New, new MethodModifier_New_AllowedRule());
					break;
				case "MethodNonPrivate":
					list.Add(Keywords.Public, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Public, new MethodModifier_nonPrivate_AllowedRule());
					list.Add(Keywords.Protected, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Protected, new MethodModifier_nonPrivate_AllowedRule());
					list.Add(Keywords.Internal, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Internal, new MethodModifier_nonPrivate_AllowedRule());
					list.Add(Keywords.ProtectedInternal, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.ProtectedInternal, new MethodModifier_nonPrivate_AllowedRule());
					break;
				case "MethodPrivate":
					list.Add(Keywords.Private, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Private, new MethodModifier_Private_AllowedRule());
					break;
				case "MethodVirtual":
					list.Add(Keywords.Virtual, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Virtual, new MethodModifier_Virtual_AllowedRule());
					break;
				case "MethodOverride":
					list.Add(Keywords.Override, ModifierVisibilityCondition.Unique);
					list.Add(Keywords.SealedOverride, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.MethodInheritance, new MethodModifier_Override_AllowedRule());
					//list.AddRuleForModifier(Keywords.SealedOverride, new MethodModifier_Override_AllowedRule());
					break;
				case "MethodAbstract":
					list.Add(Keywords.Abstract, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Abstract, new MethodModifier_Abstract_AllowedRule());
					break;
				case "MethodStatic":
					list.Add(Keywords.Static, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Static, new MethodModifier_Static_AllowedRule());
					break;
				case "MethodExtern":
					list.Add(Keywords.Extern, ModifierVisibilityCondition.Unique);
					//list.AddRuleForModifier(Keywords.Extern, new MethodModifier_Extern_AllowedRule());
					break;
					
				default:
					break;
			}

			collection.Add(kind, list);
			return list;
		}

		public static IModifierList Get(string kind)
		{
			return Instance.GetModifiersList(kind);
		}

		private IModifierList GetModifiersList(string kind)
		{
			IModifierList list;
			collection.TryGetValue(kind, out list);

			if (list == null)
			{
				list = CreateModifiersList(kind);
			}

			return list;
		}
	}
}
