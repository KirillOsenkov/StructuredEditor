using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Editor.UI.Commands
{
	public class DeleteCommand : BlockCommand
	{
		public DeleteCommand(Block toDelete)
			: base("Delete")
		{
			BlockToDelete = toDelete;
			this.Picture = PictureLibrary.Instance.ImageMenuDelete;
		}

		private Block BlockToDelete;

		public override void Click()
		{
			BlockToDelete.Delete();
		}

		public static CommandList CreateDeleteMenu(Block toDelete)
		{
			CommandList result = new CommandList();
			result.Add(new DeleteCommand(toDelete));
			return result;
		}

		public static void Add(CommandList menu, Block toDelete)
		{
			menu.Add(new DeleteCommand(toDelete));
		}
	}
}
