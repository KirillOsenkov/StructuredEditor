using System.Collections.Generic;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.CSharp
{
	public interface IModifierList : 
		IFillable<string>, 
		IEnumerable<string>,
		GuiLabs.Utils.Collections.ISet<string>
	{
		string Kind { get; }
		ModifierVisibilityCondition GetVisibilityCondition(string modifierName);
	}
}
