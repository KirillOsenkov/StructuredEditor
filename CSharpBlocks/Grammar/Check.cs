using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public static class Check
	{
		public static bool ValidTypeName(string typeName)
		{
			return ValidIdentifier(typeName);
		}
		
		public static bool ValidIdentifier(string identifier)
		{
			if (string.IsNullOrEmpty(identifier))
			{
				return false;
			}
			foreach(char c in identifier)
			{
				if(!Strings.CanBePartOfVariableName(c))
				{
					return false;
				}
			}
			return true;
		}
		
		public static bool ValidTypeReference(string typeReference)
		{
			return !string.IsNullOrEmpty(typeReference);
		}
	}
}
