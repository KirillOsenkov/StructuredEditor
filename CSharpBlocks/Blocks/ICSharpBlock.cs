namespace GuiLabs.Editor.CSharp
{
	public interface ICSharpBlock
	{
		void AcceptVisitor(IVisitor visitor);
	}
}
