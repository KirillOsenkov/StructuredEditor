using System.Collections.Generic;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class ParameterList : IEnumerable<Parameter>
	{
		public Parameter this[string paramName]
		{
			get
			{
				Parameter result;
				vars.TryGetValue(paramName, out result);
				return result;
			}
		}
		
		private Dictionary<string, Parameter> vars = new Dictionary<string, Parameter>();

		public virtual IEnumerable<string> GetParameterNames()
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

		public void Add(string parameterName, Parameter parameter)
		{
			Param.CheckNonEmptyString(parameterName, "parameterName");
			Param.CheckNotNull(parameter, "parameter");
			vars.Add(parameterName, parameter);
		}

		public void Add(Parameter parameter)
		{
			Param.CheckNotNull(parameter, "parameter");
			if (vars.ContainsKey(parameter.Name))
			{
				return;
			}
			vars.Add(parameter.Name, parameter);
		}

		public IEnumerator<Parameter> GetEnumerator()
		{
			return vars.Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
