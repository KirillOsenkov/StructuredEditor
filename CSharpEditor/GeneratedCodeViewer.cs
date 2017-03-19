using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GuiLabs.Editor.CSharp
{
	public partial class GeneratedCodeViewer : Form
	{
		public GeneratedCodeViewer()
		{
			InitializeComponent();
		}

		public string Code
		{
			get
			{
				return TextCode.Text;
			}
			set
			{
				TextCode.Text = value;
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				this.Hide();
				e.Cancel = true;
				return;
			}
			base.OnFormClosing(e);
		}
	}
}