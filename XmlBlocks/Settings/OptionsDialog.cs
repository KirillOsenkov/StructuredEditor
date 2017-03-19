using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GuiLabs.Utils;
using GuiLabs.Canvas.Controls;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Editor.Xml
{
	public partial class OptionsDialog : Form
	{
		public event EmptyHandler ShouldRedraw;
		protected void RaiseShouldRedraw()
		{
			if (ShouldRedraw != null)
			{
				ShouldRedraw();
			}
		}

		public OptionsDialog()
		{
			InitializeComponent();
			NodeDesign.Value = UniversalControl.Design;
		}

		private void NodeDesign_SelectedIndexChanged(object sender, EventArgs e)
		{
			UniversalControl.Design = NodeDesign.Value;
			RaiseShouldRedraw();
		}

		private void ButtonOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}

	public class UniversalControlDesignCombo : EnumSelectorCombo<GuiLabs.Canvas.Controls.UniversalControl.UniversalControlDesign>
	{

	}
}