namespace GuiLabs.Canvas.DrawStyle
{
	partial class StyleListEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ListStyles = new System.Windows.Forms.ListBox();
			this.PropertiesStyle = new System.Windows.Forms.PropertyGrid();
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
			this.splitContainer1.Panel1.Controls.Add(this.ListStyles);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.PropertiesStyle);
			this.splitContainer1.Size = new System.Drawing.Size(524, 475);
			this.splitContainer1.SplitterDistance = 251;
			this.splitContainer1.TabIndex = 0;
			// 
			// ListStyles
			// 
			this.ListStyles.DisplayMember = "Name";
			this.ListStyles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListStyles.FormattingEnabled = true;
			this.ListStyles.ItemHeight = 16;
			this.ListStyles.Location = new System.Drawing.Point(0, 0);
			this.ListStyles.Name = "ListStyles";
			this.ListStyles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListStyles.Size = new System.Drawing.Size(251, 468);
			this.ListStyles.Sorted = true;
			this.ListStyles.TabIndex = 0;
			this.ListStyles.SelectedIndexChanged += new System.EventHandler(this.ListStyles_SelectedIndexChanged);
			// 
			// PropertiesStyle
			// 
			this.PropertiesStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertiesStyle.HelpVisible = false;
			this.PropertiesStyle.Location = new System.Drawing.Point(0, 0);
			this.PropertiesStyle.Name = "PropertiesStyle";
			this.PropertiesStyle.Size = new System.Drawing.Size(269, 475);
			this.PropertiesStyle.TabIndex = 0;
			this.PropertiesStyle.ToolbarVisible = false;
			this.PropertiesStyle.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.PropertiesStyle_SelectedGridItemChanged);
			this.PropertiesStyle.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertiesStyle_PropertyValueChanged);
			// 
			// StyleListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "StyleListEditor";
			this.Size = new System.Drawing.Size(524, 475);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox ListStyles;
		private System.Windows.Forms.PropertyGrid PropertiesStyle;
	}
}
