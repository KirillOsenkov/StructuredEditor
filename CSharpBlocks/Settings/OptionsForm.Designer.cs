using GuiLabs.Canvas.Controls;
namespace GuiLabs.Editor.CSharp
{
	partial class OptionsForm
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
			this.Tabs = new System.Windows.Forms.TabControl();
			this.tabAppearance = new System.Windows.Forms.TabPage();
			this.FontSizeBox = new System.Windows.Forms.NumericUpDown();
			this.CurlyCombo = new GuiLabs.Editor.CSharp.CurlyTypeCombo();
			this.DesignOfUniversalControlCombo = new GuiLabs.Editor.CSharp.UniversalControlDesignCombo();
			this.lblFontSize = new System.Windows.Forms.Label();
			this.lblCurlyType = new System.Windows.Forms.Label();
			this.lblDesignOfUniversalControl = new System.Windows.Forms.Label();
			this.TabStyles = new System.Windows.Forms.TabPage();
			this.LoadFromFileButton = new System.Windows.Forms.Button();
			this.WriteToFileButton = new System.Windows.Forms.Button();
			this.StylesProperties = new GuiLabs.Canvas.DrawStyle.StyleListEditor();
			this.CloseButton = new System.Windows.Forms.Button();
			this.Tabs.SuspendLayout();
			this.tabAppearance.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FontSizeBox)).BeginInit();
			this.TabStyles.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tabs
			// 
			this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.Tabs.Controls.Add(this.tabAppearance);
			this.Tabs.Controls.Add(this.TabStyles);
			this.Tabs.Location = new System.Drawing.Point(13, 13);
			this.Tabs.Name = "Tabs";
			this.Tabs.SelectedIndex = 0;
			this.Tabs.Size = new System.Drawing.Size(667, 571);
			this.Tabs.TabIndex = 0;
			// 
			// tabAppearance
			// 
			this.tabAppearance.Controls.Add(this.FontSizeBox);
			this.tabAppearance.Controls.Add(this.CurlyCombo);
			this.tabAppearance.Controls.Add(this.DesignOfUniversalControlCombo);
			this.tabAppearance.Controls.Add(this.lblFontSize);
			this.tabAppearance.Controls.Add(this.lblCurlyType);
			this.tabAppearance.Controls.Add(this.lblDesignOfUniversalControl);
			this.tabAppearance.Location = new System.Drawing.Point(4, 25);
			this.tabAppearance.Name = "tabAppearance";
			this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
			this.tabAppearance.Size = new System.Drawing.Size(659, 542);
			this.tabAppearance.TabIndex = 1;
			this.tabAppearance.Text = "Appearance";
			this.tabAppearance.UseVisualStyleBackColor = true;
			// 
			// FontSizeBox
			// 
			this.FontSizeBox.Location = new System.Drawing.Point(192, 88);
			this.FontSizeBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.FontSizeBox.Name = "FontSizeBox";
			this.FontSizeBox.Size = new System.Drawing.Size(64, 22);
			this.FontSizeBox.TabIndex = 4;
			this.FontSizeBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.FontSizeBox.ValueChanged += new System.EventHandler(this.FontSizeBox_ValueChanged);
			// 
			// CurlyCombo
			// 
			this.CurlyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurlyCombo.FormattingEnabled = true;
			this.CurlyCombo.Location = new System.Drawing.Point(192, 48);
			this.CurlyCombo.Name = "CurlyCombo";
			this.CurlyCombo.Size = new System.Drawing.Size(232, 24);
			this.CurlyCombo.TabIndex = 3;
			// 
			// DesignOfUniversalControlCombo
			// 
			this.DesignOfUniversalControlCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DesignOfUniversalControlCombo.FormattingEnabled = true;
			this.DesignOfUniversalControlCombo.Location = new System.Drawing.Point(192, 8);
			this.DesignOfUniversalControlCombo.Name = "DesignOfUniversalControlCombo";
			this.DesignOfUniversalControlCombo.Size = new System.Drawing.Size(232, 24);
			this.DesignOfUniversalControlCombo.TabIndex = 2;
			// 
			// lblFontSize
			// 
			this.lblFontSize.AutoSize = true;
			this.lblFontSize.Location = new System.Drawing.Point(8, 88);
			this.lblFontSize.Name = "lblFontSize";
			this.lblFontSize.Size = new System.Drawing.Size(65, 17);
			this.lblFontSize.TabIndex = 1;
			this.lblFontSize.Text = "Font size";
			// 
			// lblCurlyType
			// 
			this.lblCurlyType.AutoSize = true;
			this.lblCurlyType.Location = new System.Drawing.Point(8, 48);
			this.lblCurlyType.Name = "lblCurlyType";
			this.lblCurlyType.Size = new System.Drawing.Size(87, 17);
			this.lblCurlyType.TabIndex = 1;
			this.lblCurlyType.Text = "Curly braces";
			// 
			// lblDesignOfUniversalControl
			// 
			this.lblDesignOfUniversalControl.AutoSize = true;
			this.lblDesignOfUniversalControl.Location = new System.Drawing.Point(8, 8);
			this.lblDesignOfUniversalControl.Name = "lblDesignOfUniversalControl";
			this.lblDesignOfUniversalControl.Size = new System.Drawing.Size(176, 17);
			this.lblDesignOfUniversalControl.TabIndex = 1;
			this.lblDesignOfUniversalControl.Text = "Design of universal control";
			// 
			// TabStyles
			// 
			this.TabStyles.Controls.Add(this.LoadFromFileButton);
			this.TabStyles.Controls.Add(this.WriteToFileButton);
			this.TabStyles.Controls.Add(this.StylesProperties);
			this.TabStyles.Location = new System.Drawing.Point(4, 25);
			this.TabStyles.Name = "TabStyles";
			this.TabStyles.Padding = new System.Windows.Forms.Padding(3);
			this.TabStyles.Size = new System.Drawing.Size(659, 542);
			this.TabStyles.TabIndex = 2;
			this.TabStyles.Text = "Styles";
			this.TabStyles.UseVisualStyleBackColor = true;
			// 
			// LoadFromFileButton
			// 
			this.LoadFromFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LoadFromFileButton.Location = new System.Drawing.Point(168, 496);
			this.LoadFromFileButton.Name = "LoadFromFileButton";
			this.LoadFromFileButton.Size = new System.Drawing.Size(144, 32);
			this.LoadFromFileButton.TabIndex = 1;
			this.LoadFromFileButton.Text = "Read from file...";
			this.LoadFromFileButton.UseVisualStyleBackColor = true;
			this.LoadFromFileButton.Click += new System.EventHandler(this.LoadFromFileButton_Click);
			// 
			// WriteToFileButton
			// 
			this.WriteToFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.WriteToFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.WriteToFileButton.Location = new System.Drawing.Point(8, 496);
			this.WriteToFileButton.Name = "WriteToFileButton";
			this.WriteToFileButton.Size = new System.Drawing.Size(144, 32);
			this.WriteToFileButton.TabIndex = 1;
			this.WriteToFileButton.Text = "Write to file...";
			this.WriteToFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.WriteToFileButton.UseVisualStyleBackColor = true;
			this.WriteToFileButton.Click += new System.EventHandler(this.WriteToFileButton_Click);
			// 
			// StylesProperties
			// 
			this.StylesProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.StylesProperties.Location = new System.Drawing.Point(3, 3);
			this.StylesProperties.Name = "StylesProperties";
			this.StylesProperties.Size = new System.Drawing.Size(643, 485);
			this.StylesProperties.TabIndex = 0;
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(586, 598);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(94, 28);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// OptionsForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.CloseButton;
			this.ClientSize = new System.Drawing.Size(692, 638);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.Tabs);
			this.KeyPreview = true;
			this.Name = "OptionsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "OptionsForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OptionsForm_KeyDown);
			this.Tabs.ResumeLayout(false);
			this.tabAppearance.ResumeLayout(false);
			this.tabAppearance.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.FontSizeBox)).EndInit();
			this.TabStyles.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl Tabs;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.TabPage tabAppearance;
		private System.Windows.Forms.Label lblDesignOfUniversalControl;
		private UniversalControlDesignCombo DesignOfUniversalControlCombo;
		private CurlyTypeCombo CurlyCombo;
		private System.Windows.Forms.Label lblCurlyType;
		private System.Windows.Forms.NumericUpDown FontSizeBox;
		private System.Windows.Forms.Label lblFontSize;
		private System.Windows.Forms.TabPage TabStyles;
		private GuiLabs.Canvas.DrawStyle.StyleListEditor StylesProperties;
		private System.Windows.Forms.Button LoadFromFileButton;
		private System.Windows.Forms.Button WriteToFileButton;
	}
}