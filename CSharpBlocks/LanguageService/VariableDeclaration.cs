using System.Collections.Generic;
using System.Collections.ObjectModel;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class VariableDeclaration : IEnumerable<Variable>
	{
		private Dictionary<string, Variable> vars = new Dictionary<string, Variable>();

		public virtual IEnumerable<string> GetVariableNames()
		{
			return vars.Keys;
		}

		public int Count
		{
			get
			{
				return vars.Count;
			}
		}

		public void Add(Variable variable)
		{
			Param.CheckNotNull(variable, "variable");
			vars.Add(variable.Name, variable);
		}

		public void Add(string variableName, Variable variable)
		{
			Param.CheckNonEmptyString(variableName, "variableName");
			Param.CheckNotNull(variable, "variable");
			vars.Add(variableName, variable);
		}

		public IEnumerator<Variable> GetEnumerator()
		{
			return vars.Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
