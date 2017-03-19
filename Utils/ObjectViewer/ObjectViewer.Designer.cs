namespace GuiLabs.Utils
{
	partial class ObjectViewer
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
			this.ListObjects = new System.Windows.Forms.ListBox();
			this.GridProperties = new System.Windows.Forms.PropertyGrid();
			this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ListFields = new System.Windows.Forms.ListBox();
			this.ButtonBack = new System.Windows.Forms.Button();
			this.LabelTypeName = new System.Windows.Forms.Label();
			this.SplitContainer1.Panel1.SuspendLayout();
			this.SplitContainer1.Panel2.SuspendLayout();
			this.SplitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListObjects
			// 
			this.ListObjects.Dock = System.Windows.Forms.DockStyle.Top;
			this.ListObjects.FormattingEnabled = true;
			this.ListObjects.ItemHeight = 16;
			this.ListObjects.Location = new System.Drawing.Point(0, 0);
			this.ListObjects.Name = "ListObjects";
			this.ListObjects.Size = new System.Drawing.Size(265, 308);
			this.ListObjects.TabIndex = 0;
			this.ListObjects.SelectedIndexChanged += new System.EventHandler(this.ListObjects_SelectedIndexChanged);
			// 
			// GridProperties
			// 
			this.GridProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.GridProperties.Location = new System.Drawing.Point(0, 32);
			this.GridProperties.Name = "GridProperties";
			this.GridProperties.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.GridProperties.Size = new System.Drawing.Size(527, 588);
			this.GridProperties.TabIndex = 1;
			// 
			// SplitContainer1
			// 
			this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer1.Name = "SplitContainer1";
			// 
			// SplitContainer1.Panel1
			// 
			this.SplitContainer1.Panel1.Controls.Add(this.ListFields);
			this.SplitContainer1.Panel1.Controls.Add(this.ListObjects);
			// 
			// SplitContainer1.Panel2
			// 
			this.SplitContainer1.Panel2.Controls.Add(this.ButtonBack);
			this.SplitContainer1.Panel2.Controls.Add(this.LabelTypeName);
			this.SplitContainer1.Panel2.Controls.Add(this.GridProperties);
			this.SplitContainer1.Size = new System.Drawing.Size(796, 620);
			this.SplitContainer1.SplitterDistance = 265;
			this.SplitContainer1.TabIndex = 2;
			// 
			// ListFields
			// 
			this.ListFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListFields.FormattingEnabled = true;
			this.ListFields.ItemHeight = 16;
			this.ListFields.Location = new System.Drawing.Point(0, 308);
			this.ListFields.Name = "ListFields";
			this.ListFields.Size = new System.Drawing.Size(265, 308);
			this.ListFields.Sorted = true;
			this.ListFields.TabIndex = 1;
			this.ListFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListFields_MouseClick);
			// 
			// ButtonBack
			// 
			this.ButtonBack.Enabled = false;
			this.ButtonBack.Location = new System.Drawing.Point(0, 0);
			this.ButtonBack.Name = "ButtonBack";
			this.ButtonBack.Size = new System.Drawing.Size(64, 32);
			this.ButtonBack.TabIndex = 3;
			this.ButtonBack.Text = "Back";
			this.ButtonBack.UseVisualStyleBackColor = true;
			this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
			// 
			// LabelTypeName
			// 
			this.LabelTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LabelTypeName.BackColor = System.Drawing.Color.LightYellow;
			this.LabelTypeName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LabelTypeName.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LabelTypeName.Location = new System.Drawing.Point(64, 0);
			this.LabelTypeName.Name = "LabelTypeName";
			this.LabelTypeName.Size = new System.Drawing.Size(463, 32);
			this.LabelTypeName.TabIndex = 2;
			// 
			// ObjectViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(796, 620);
			this.Controls.Add(this.SplitContainer1);
			this.Name = "ObjectViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ObjectViewer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.SplitContainer1.Panel1.ResumeLayout(false);
			this.SplitContainer1.Panel2.ResumeLayout(false);
			this.SplitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ListObjects;
		private System.Windows.Forms.PropertyGrid GridProperties;
		private System.Windows.Forms.SplitContainer SplitContainer1;
		private System.Windows.Forms.ListBox ListFields;
		private System.Windows.Forms.Label LabelTypeName;
		private System.Windows.Forms.Button ButtonBack;
	}
}