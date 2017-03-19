using System;

namespace GuiLabs.Editor.CSharp
{
	public class Parameter : Variable
	{
		#region ctors

		public Parameter()
			: base()
		{

		}

		public Parameter(string name, string type)
			: base(name, type)
		{

		}

		#endregion
	
		private ParameterType mKind;
		public ParameterType Kind
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

		public static Parameter CreateValueParameter()
		{
			Parameter result = new Parameter("value", "");
			return result;
		}

		public static Parameter CreateValueParameter(string type)
		{
			Parameter result = new Parameter("value", type);
			return result;
		}
	}
	
	[Flags]
	public enum ParameterType
	{
		None = 0,
		In = 1,
		Out = 2,
		Ref = 4,
		Params = 8,
		Optional = 16
	}
}
