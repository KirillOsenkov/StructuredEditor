using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	public class CodeLineBlock : LineBlock
	{
		public CodeLineBlock()
		{
			CanMoveUpDown = true;
		}

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				Delete();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion
	}
}
