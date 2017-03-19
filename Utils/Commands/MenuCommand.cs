using System.Windows.Forms;
using GuiLabs.Utils.Commands;

namespace GuiLabs.Utils.Commands
{
	public class MenuCommand : ToolStripMenuItem
	{
		public MenuCommand(ICommand wrappedItem)
			: base(wrappedItem.Text)
		{
			wrapped = wrappedItem;
			if (wrappedItem.Picture != null)
			{
				this.Image = wrappedItem.Picture;
				this.ImageTransparentColor = System.Drawing.Color.Magenta;
			}
		}

		private ICommand wrapped;

		protected override void OnClick(System.EventArgs e)
		{
			base.OnClick(e);
			wrapped.Click();
		}
	}

	public class OldStyleMenuCommand : MenuItem
	{
		public OldStyleMenuCommand(ICommand wrappedItem)
			: base(wrappedItem.Text)
		{
			wrapped = wrappedItem;
		}

		private ICommand wrapped;

		protected override void OnClick(System.EventArgs e)
		{
			base.OnClick(e);
			wrapped.Click();
		}
	}

}
