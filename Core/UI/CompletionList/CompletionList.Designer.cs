namespace GuiLabs.Editor.UI
{
	partial class CompletionList
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
			this.lstItems = new GuiLabs.Editor.UI.ListBox2();
			this.SuspendLayout();
			// 
			// lstItems
			// 
			this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lstItems.HorizontalScrollbar = true;
			this.lstItems.IntegralHeight = true;
			this.lstItems.ItemHeight = 19;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(600, 600);
			this.lstItems.Sorted = true;
			this.lstItems.TabIndex = 0;
			this.lstItems.TabStop = false;
			this.lstItems.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Item_MouseClick);
			this.lstItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstItems_KeyPress);
			this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
			// 
			// CompletionList
			// 
			this.AccessibleDescription = "Completion list";
			this.AccessibleName = "CompletionList";
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.DropList;
			this.ClientSize = new System.Drawing.Size(600, 600);
			this.ControlBox = false;
			this.Controls.Add(this.lstItems);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CompletionList";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.TopLevel = false;
			this.ResumeLayout(false);

		}

		#endregion

		private ListBox2 lstItems;

	}
}
