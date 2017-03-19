namespace GuiLabs.Editor.Blocks
{
	public class SeparatorBlock : FocusableLabelBlock
	{
		public SeparatorBlock()
			: base(" ")
		{

		}

		#region OnEvents

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);

			if (this.CanStartNewToken(e.KeyChar))
			{
				this.AppendNewToken(e.KeyChar.ToString());
			}

			if (e.KeyChar == ' ')
			{
				this.MoveFocusToNextInChain();
			}
		}

		#endregion

		#region Starting new token

		protected virtual void AppendNewToken(string prefix)
		{
		}

		protected virtual bool CanStartNewToken(char beginning)
		{
			return
				!char.IsControl(beginning)
				&& !char.IsWhiteSpace(beginning)
				;
		}

		#endregion
	}
}
