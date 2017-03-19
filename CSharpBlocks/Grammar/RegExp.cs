using System.Text;
using System.Text.RegularExpressions;

namespace GuiLabs.Editor.CSharp
{
	public class RegExp
	{
		private static string ident = @"([A-Za-z_][A-Za-z0-9_]*)";
		private static string type = @"((" + ident + @")(\." + ident + @")*(\[,*\])*)";
		private static string param = @"(\s?((ref|out)\s)?" + type + @"\s" + ident + @"\s?)";
		private static string parameterList = ident + @"\s?\((.*,\s?)*$";

		public static void PrintToFile(string fileName)
		{
			System.IO.File.WriteAllLines(fileName, new string[] { 
				ident,
				type,
				param,
				parameterList
			});
		}

		public static Regex VarTypeAndName = new Regex(
			@"^" + type + @"\s" + ident + @"$"
		);
		
		public static Regex TypeRef = new Regex(
			type
		);
		
		public static Regex Identifier = new Regex(
			ident
		);
		
		public static Regex NotInsideStringLiteral = new Regex(
			@"^[^""]*([^""]*(""([^""]|""""|\\"")*""|@""[^""]*"")[^""]*)*[^""]*$"
		);
		
		public static Regex InsideCharLiteral = new Regex(
			@"^([^']*'\w')*[^']*'(\w)*$"
		);
		
		public static Regex FirstWord = new Regex(
			@"^" + ident + @"\s"
		);
		
		public static Regex FirstWordIsStatementKeyword = new Regex(
			@"^(return|throw)\s"
		);
		
		public static Regex LastWord = new Regex(
			@"[^w]" + ident + @"$"
		);
		
		public static Regex ParameterModifierAllowed = new Regex(
			parameterList
		);
		
		public static Regex Parameter = new Regex(
			param
		);
		
		public static Regex AtMethodParameterName = new Regex(
			"^(" + param + @",)*\s?" + @"((ref|out|params)\s)?" + type + @"\s" + ident + "$"
		);
		
		public static bool IsIdentifier(string str)
		{
			return Identifier.IsMatch(str);
		}
		
		public static bool IsVarTypeAndName(string str)
		{
			return VarTypeAndName.IsMatch(str);
		}
		
		public static bool InsideStringLiteral(string str)
		{
			return !NotInsideStringLiteral.IsMatch(str) || InsideCharLiteral.IsMatch(str);
		}
		
		public static bool IsAtMethodParameterName(string str)
		{
			return AtMethodParameterName.IsMatch(str);
		}
		
		public static bool IsFirstWordStatementKeyword(string str)
		{
			return FirstWordIsStatementKeyword.IsMatch(str);
		}
		
		public static bool IsParameterModifierAllowed(string str)
		{
			return ParameterModifierAllowed.IsMatch(str);
		}
		
		public static string GetFirstWord(string str)
		{
			return FirstMatch(FirstWord, str);
		}
		
		public static string GetLastWord(string str)
		{
			return FirstMatch(LastWord, str);
		}
		
		public static string FirstMatch(Regex re, string str)
		{
			Match m = re.Match(str);
			if (m != null && m.Success)
				return m.Groups[1].Value;
			return "";
		}
	}
}
