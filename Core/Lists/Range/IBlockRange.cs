using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Lists
{
	public interface IBlockRange
	{
		IBlockRange FromChild
		{
			get;
			set;
		}
		Block Node
		{
			get;
			set;
		}
		IBlockRange ToChild
		{
			get;
			set;
		}

		// IBlockRange Clone();
	}
}
