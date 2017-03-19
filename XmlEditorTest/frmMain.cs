using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.Xml;

namespace XMLEditor
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();

			viewWindow1.RootBlock = CodeUnit;
		}

		private XMLRootBlock mCodeUnit = new XMLRootBlock();
		public XMLRootBlock CodeUnit
		{
			get
			{
				return mCodeUnit;
			}
		}

		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CodeUnit.ActionManager.Undo();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CodeUnit.ActionManager.Redo();
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			undoToolStripMenuItem.Enabled = CodeUnit.ActionManager.CanUndo;
			redoToolStripMenuItem.Enabled = CodeUnit.ActionManager.CanRedo;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}