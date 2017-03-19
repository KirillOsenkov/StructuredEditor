using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierList : List<string>, IModifierList
	{
		#region ctor

		public ModifierList(string kind)
		{
			Kind = kind;
		}

		public ModifierList(string kind, IEnumerable<string> modifiers)
			: this(kind)
		{
			Add(modifiers);
		}

		#endregion

		#region Add

		public void Add(IEnumerable<string> modifiers)
		{
			AddRange(modifiers);
		}

		public void Add(string modifier, ModifierVisibilityCondition condition)
		{
			Add(modifier);
			AddVisibilityCondition(modifier, condition);
		}

		#endregion

		private string mKind;
		public string Kind
		{
			get
			{
				return mKind;
			}
			set
			{
				mKind = value;
			}
		}

		private Dictionary<string, ModifierVisibilityCondition> mVisibilityConditions = new Dictionary<string, ModifierVisibilityCondition>();
		public Dictionary<string, ModifierVisibilityCondition> VisibilityConditions
		{
			get
			{
				return mVisibilityConditions;
			}
			set
			{
				mVisibilityConditions = value;
			}
		}
		
		public ModifierVisibilityCondition GetVisibilityCondition(string modifierName)
		{
			ModifierVisibilityCondition c;
			VisibilityConditions.TryGetValue(modifierName, out c);
			return c;
		}

		public void AddVisibilityCondition(string modifierName, ModifierVisibilityCondition condition)
		{
			VisibilityConditions.Add(modifierName, condition);
		}
	}
}
