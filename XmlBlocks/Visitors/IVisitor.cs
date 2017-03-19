using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Xml
{
	public interface IVisitor
	{
		void Visit(XMLRootBlock Block);
	}
}