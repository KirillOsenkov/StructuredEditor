using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.Xml
{
	public class AttributeList : HContainerBlock
	{
		public AttributeList()
		{
			MyControl.Box.Margins.Left = 10;
			MyControl.Box.SetMouseSensitivityToMargins();
			this.Add(new TextBoxBlock());
		}
	}
}
