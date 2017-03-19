namespace GuiLabs.Editor.CSharp
{
	public interface ITypeDeclaration : 
		INamespaceLevel,
		IClassLevel,
		ICSharpBlock, 
		IHasName, 
		IHasModifiers
	{
	}
}
