using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.CSharp
{
	public interface IStatement
	{
		VariableDeclaration LocalVariableDeclaration { get; }
	}
}
