namespace GuiLabs.Editor.Sample
{
	partial class TreeViewTest
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
            this.treeView1 = new System.Windows.Forms.TreeView();
			this.treeViewControl1 = new GuiLabs.Editor.UI.TreeViewControl();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.treeView1.Location = new System.Drawing.Point(12, 12);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(422, 621);
			this.treeView1.TabIndex = 0;
			// 
			// treeViewControl1
			// 
			this.treeViewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.treeViewControl1.Location = new System.Drawing.Point(441, 13);
			this.treeViewControl1.Name = "treeViewControl1";
			this.treeViewControl1.Size = new System.Drawing.Size(403, 620);
			this.treeViewControl1.TabIndex = 1;
			// 
			// TreeViewTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(856, 645);
			this.Controls.Add(this.treeViewControl1);
			this.Controls.Add(this.treeView1);
			this.Name = "TreeViewTest";
			this.Text = "TreeViewTest";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private GuiLabs.Editor.UI.TreeViewControl treeViewControl1;
	}
}