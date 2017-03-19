namespace GuiLabs.Editor.CSharp
{
	partial class GeneratedCodeViewer
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
			this.TextCode = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// TextCode
			// 
			this.TextCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TextCode.Location = new System.Drawing.Point(0, 0);
			this.TextCode.Name = "TextCode";
			this.TextCode.Size = new System.Drawing.Size(424, 613);
			this.TextCode.TabIndex = 0;
			this.TextCode.Text = "";
			// 
			// GeneratedCodeViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 613);
			this.Controls.Add(this.TextCode);
			this.Location = new System.Drawing.Point(800, 0);
			this.Name = "GeneratedCodeViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "GeneratedCodeViewer";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox TextCode;
	}
}