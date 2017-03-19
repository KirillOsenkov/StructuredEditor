namespace GuiLabs.Editor.Xml
{
	partial class OptionsDialog
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
			this.NodeDesign = new GuiLabs.Editor.Xml.UniversalControlDesignCombo();
			this.label1 = new System.Windows.Forms.Label();
			this.ButtonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// NodeDesign
			// 
			this.NodeDesign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.NodeDesign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NodeDesign.FormattingEnabled = true;
			this.NodeDesign.Location = new System.Drawing.Point(152, 16);
			this.NodeDesign.Name = "NodeDesign";
			this.NodeDesign.Size = new System.Drawing.Size(376, 24);
			this.NodeDesign.TabIndex = 0;
			this.NodeDesign.SelectedIndexChanged += new System.EventHandler(this.NodeDesign_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Design of nodes";
			// 
			// ButtonOK
			// 
			this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonOK.Location = new System.Drawing.Point(432, 96);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.Size = new System.Drawing.Size(96, 32);
			this.ButtonOK.TabIndex = 2;
			this.ButtonOK.Text = "OK";
			this.ButtonOK.UseVisualStyleBackColor = true;
			this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.ButtonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonOK;
			this.ClientSize = new System.Drawing.Size(540, 138);
			this.Controls.Add(this.ButtonOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.NodeDesign);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "XML editor options";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private UniversalControlDesignCombo NodeDesign;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ButtonOK;
	}
}