namespace GuiLabs.Editor.Sample
{
    partial class Form1
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
			this.viewWindow1 = new GuiLabs.Editor.UI.ViewWindow();
			this.SuspendLayout();
			// 
			// viewWindow1
			// 
			this.viewWindow1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.viewWindow1.DefaultKeyHandler = null;
			this.viewWindow1.DefaultMouseHandler = null;
			this.viewWindow1.Location = new System.Drawing.Point(17, 16);
			this.viewWindow1.MainShape = null;
			this.viewWindow1.Margin = new System.Windows.Forms.Padding(4);
			this.viewWindow1.Name = "viewWindow1";
			this.viewWindow1.RootBlock = null;
			this.viewWindow1.Size = new System.Drawing.Size(831, 683);
			this.viewWindow1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(865, 714);
			this.Controls.Add(this.viewWindow1);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

        }

        #endregion

        private GuiLabs.Editor.UI.ViewWindow viewWindow1;
    }
}

