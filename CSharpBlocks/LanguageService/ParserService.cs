namespace GuiLabs.Editor.CSharp
{
	public class ParserService
	{
		public virtual IStatement ParseStatement(string statementText)
		{
			return null;
		}

		public virtual ParameterList ParseParameters(string parametersText)
		{
			return null;
		}
	}
}
