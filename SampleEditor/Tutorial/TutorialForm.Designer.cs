namespace GuiLabs.Editor.Sample
{
	partial class TutorialForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.RootList = new GuiLabs.Editor.UI.ViewWindow();
			this.BlockView = new GuiLabs.Editor.UI.ViewWindow();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.RootList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.BlockView);
			this.splitContainer1.Size = new System.Drawing.Size(920, 656);
			this.splitContainer1.SplitterDistance = 197;
			this.splitContainer1.TabIndex = 0;
			// 
			// RootList
			// 
			this.RootList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RootList.Location = new System.Drawing.Point(0, 0);
			this.RootList.Name = "RootList";
			this.RootList.ShowScrollbars = false;
			this.RootList.Size = new System.Drawing.Size(197, 656);
			this.RootList.TabIndex = 0;
			// 
			// BlockView
			// 
			this.BlockView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BlockView.Location = new System.Drawing.Point(0, 0);
			this.BlockView.Name = "BlockView";
			this.BlockView.ShowScrollbars = true;
			this.BlockView.Size = new System.Drawing.Size(719, 656);
			this.BlockView.TabIndex = 1;
			// 
			// TutorialForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 656);
			this.Controls.Add(this.splitContainer1);
			this.Name = "TutorialForm";
			this.Text = "TutorialForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private GuiLabs.Editor.UI.ViewWindow RootList;
		private GuiLabs.Editor.UI.ViewWindow BlockView;

	}
}