namespace GuiLabs.Editor.Clipboard
{
	public interface IPotentialDropTarget
	{
		DragQueryResult CanAcceptBlocks(DragQuery draggedBlocks);
		void AcceptBlocks(DragQuery draggedBlocks, DragQueryResult whereTo);
	}
}
