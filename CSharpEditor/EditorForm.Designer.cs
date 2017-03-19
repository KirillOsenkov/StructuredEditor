namespace GuiLabs.Editor.CSharp
{
	partial class EditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.RedrawCounterLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.Breadcrumb = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripMain = new System.Windows.Forms.ToolStrip();
            this.ButtonNew = new System.Windows.Forms.ToolStripButton();
            this.ButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.ButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonHelloWorld = new System.Windows.Forms.ToolStripButton();
            this.ButtonContextHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.ButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.ButtonPrettyPrinter = new System.Windows.Forms.ToolStripButton();
            this.ButtonSerializationViewer = new System.Windows.Forms.ToolStripButton();
            this.ButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.SplitterMain = new System.Windows.Forms.SplitContainer();
            this.viewWindow1 = new GuiLabs.Editor.UI.ViewWindow();
            this.PanelContextHelp = new System.Windows.Forms.Panel();
            this.TextContextHelp = new GuiLabs.Editor.CSharp.DynamicHelpControl();
            this.statusStrip1.SuspendLayout();
            this.ToolStripMain.SuspendLayout();
            this.SplitterMain.Panel1.SuspendLayout();
            this.SplitterMain.Panel2.SuspendLayout();
            this.SplitterMain.SuspendLayout();
            this.PanelContextHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RedrawCounterLabel,
            this.StatusBar,
            this.Breadcrumb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 743);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // RedrawCounterLabel
            // 
            this.RedrawCounterLabel.Name = "RedrawCounterLabel";
            this.RedrawCounterLabel.Size = new System.Drawing.Size(113, 20);
            this.RedrawCounterLabel.Text = "Redraw counter";
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(37, 20);
            this.StatusBar.Text = "";
            // 
            // Breadcrumb
            // 
            this.Breadcrumb.Name = "Breadcrumb";
            this.Breadcrumb.Size = new System.Drawing.Size(90, 20);
            this.Breadcrumb.Text = "Breadcrumb";
            // 
            // ToolStripMain
            // 
            this.ToolStripMain.AllowItemReorder = true;
            this.ToolStripMain.CanOverflow = false;
            this.ToolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonNew,
            this.ButtonOpen,
            this.ButtonSave,
            this.toolStripSeparator2,
            this.ButtonHelloWorld,
            this.ButtonContextHelp,
            this.toolStripSeparator1,
            this.ButtonUndo,
            this.ButtonRedo,
            this.ButtonPrettyPrinter,
            this.ButtonSerializationViewer,
            this.ButtonOptions});
            this.ToolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ToolStripMain.Location = new System.Drawing.Point(0, 0);
            this.ToolStripMain.Name = "ToolStripMain";
            this.ToolStripMain.ShowItemToolTips = false;
            this.ToolStripMain.Size = new System.Drawing.Size(792, 27);
            this.ToolStripMain.TabIndex = 8;
            this.ToolStripMain.Text = "toolStrip1";
            // 
            // ButtonNew
            // 
            this.ButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonNew.Image = global::GuiLabs.Editor.CSharp.Resources.New;
            this.ButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonNew.Name = "ButtonNew";
            this.ButtonNew.Size = new System.Drawing.Size(23, 20);
            this.ButtonNew.Text = "toolStripButton1";
            this.ButtonNew.Click += new System.EventHandler(this.ButtonNew_Click);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonOpen.Image = global::GuiLabs.Editor.CSharp.Resources.Open;
            this.ButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(23, 20);
            this.ButtonOpen.Text = "toolStripButton1";
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ButtonSave.Image = global::GuiLabs.Editor.CSharp.Resources.Save;
            this.ButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(23, 20);
            this.ButtonSave.Text = "toolStripButton2";
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // ButtonHelloWorld
            // 
            this.ButtonHelloWorld.Image = ((System.Drawing.Image)(resources.GetObject("ButtonHelloWorld.Image")));
            this.ButtonHelloWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonHelloWorld.Name = "ButtonHelloWorld";
            this.ButtonHelloWorld.Size = new System.Drawing.Size(110, 24);
            this.ButtonHelloWorld.Text = "Hello World";
            this.ButtonHelloWorld.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonHelloWorld_MouseDown);
            // 
            // ButtonContextHelp
            // 
            this.ButtonContextHelp.Image = ((System.Drawing.Image)(resources.GetObject("ButtonContextHelp.Image")));
            this.ButtonContextHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonContextHelp.Name = "ButtonContextHelp";
            this.ButtonContextHelp.Size = new System.Drawing.Size(116, 24);
            this.ButtonContextHelp.Text = "Context Help";
            this.ButtonContextHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonContextHelp_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // ButtonUndo
            // 
            this.ButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("ButtonUndo.Image")));
            this.ButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonUndo.Name = "ButtonUndo";
            this.ButtonUndo.Size = new System.Drawing.Size(65, 24);
            this.ButtonUndo.Text = "Undo";
            this.ButtonUndo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonUndo_MouseDown);
            // 
            // ButtonRedo
            // 
            this.ButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRedo.Image")));
            this.ButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonRedo.Name = "ButtonRedo";
            this.ButtonRedo.Size = new System.Drawing.Size(64, 24);
            this.ButtonRedo.Text = "Redo";
            this.ButtonRedo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonRedo_MouseDown);
            // 
            // ButtonPrettyPrinter
            // 
            this.ButtonPrettyPrinter.CheckOnClick = true;
            this.ButtonPrettyPrinter.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPrettyPrinter.Image")));
            this.ButtonPrettyPrinter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonPrettyPrinter.Name = "ButtonPrettyPrinter";
            this.ButtonPrettyPrinter.Size = new System.Drawing.Size(115, 24);
            this.ButtonPrettyPrinter.Text = "Pretty printer";
            this.ButtonPrettyPrinter.Click += new System.EventHandler(this.PrettyPrinter_Click);
            // 
            // ButtonSerializationViewer
            // 
            this.ButtonSerializationViewer.CheckOnClick = true;
            this.ButtonSerializationViewer.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSerializationViewer.Image")));
            this.ButtonSerializationViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonSerializationViewer.Name = "ButtonSerializationViewer";
            this.ButtonSerializationViewer.Size = new System.Drawing.Size(158, 24);
            this.ButtonSerializationViewer.Text = "Serialization viewer";
            this.ButtonSerializationViewer.Click += new System.EventHandler(this.ButtonSerializationViewer_Click);
            // 
            // ButtonOptions
            // 
            this.ButtonOptions.Image = ((System.Drawing.Image)(resources.GetObject("ButtonOptions.Image")));
            this.ButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonOptions.Name = "ButtonOptions";
            this.ButtonOptions.Size = new System.Drawing.Size(81, 24);
            this.ButtonOptions.Text = "Options";
            this.ButtonOptions.Click += new System.EventHandler(this.ButtonOptions_Click);
            // 
            // SplitterMain
            // 
            this.SplitterMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitterMain.Location = new System.Drawing.Point(0, 27);
            this.SplitterMain.Name = "SplitterMain";
            this.SplitterMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitterMain.Panel1
            // 
            this.SplitterMain.Panel1.Controls.Add(this.viewWindow1);
            // 
            // SplitterMain.Panel2
            // 
            this.SplitterMain.Panel2.Controls.Add(this.PanelContextHelp);
            this.SplitterMain.Size = new System.Drawing.Size(792, 716);
            this.SplitterMain.SplitterDistance = 588;
            this.SplitterMain.TabIndex = 10;
            // 
            // viewWindow1
            // 
            this.viewWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewWindow1.Location = new System.Drawing.Point(0, 0);
            this.viewWindow1.Name = "viewWindow1";
            this.viewWindow1.ShouldRedrawOnWindowPaint = true;
            this.viewWindow1.ShowScrollbars = true;
            this.viewWindow1.Size = new System.Drawing.Size(792, 588);
            this.viewWindow1.TabIndex = 1;
            // 
            // PanelContextHelp
            // 
            this.PanelContextHelp.Controls.Add(this.TextContextHelp);
            this.PanelContextHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContextHelp.Location = new System.Drawing.Point(0, 0);
            this.PanelContextHelp.Name = "PanelContextHelp";
            this.PanelContextHelp.Size = new System.Drawing.Size(792, 124);
            this.PanelContextHelp.TabIndex = 10;
            // 
            // TextContextHelp
            // 
            this.TextContextHelp.BackColor = System.Drawing.Color.LightYellow;
            this.TextContextHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextContextHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextContextHelp.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextContextHelp.Location = new System.Drawing.Point(0, 0);
            this.TextContextHelp.Name = "TextContextHelp";
            this.TextContextHelp.ReadOnly = true;
            this.TextContextHelp.Size = new System.Drawing.Size(792, 124);
            this.TextContextHelp.TabIndex = 0;
            this.TextContextHelp.TabStop = false;
            this.TextContextHelp.Text = "";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 768);
            this.Controls.Add(this.SplitterMain);
            this.Controls.Add(this.ToolStripMain);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditorForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditorForm_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ToolStripMain.ResumeLayout(false);
            this.ToolStripMain.PerformLayout();
            this.SplitterMain.Panel1.ResumeLayout(false);
            this.SplitterMain.Panel2.ResumeLayout(false);
            this.SplitterMain.ResumeLayout(false);
            this.PanelContextHelp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel StatusBar;
		private System.Windows.Forms.ToolStripStatusLabel RedrawCounterLabel;
		private System.Windows.Forms.ToolStrip ToolStripMain;
		private System.Windows.Forms.ToolStripButton ButtonHelloWorld;
		private System.Windows.Forms.ToolStripButton ButtonContextHelp;
		private System.Windows.Forms.SplitContainer SplitterMain;
		private GuiLabs.Editor.UI.ViewWindow viewWindow1;
		private System.Windows.Forms.Panel PanelContextHelp;
		private GuiLabs.Editor.CSharp.DynamicHelpControl TextContextHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton ButtonUndo;
		private System.Windows.Forms.ToolStripButton ButtonRedo;
		private System.Windows.Forms.ToolStripStatusLabel Breadcrumb;
		private System.Windows.Forms.ToolStripButton ButtonNew;
		private System.Windows.Forms.ToolStripButton ButtonOpen;
		private System.Windows.Forms.ToolStripButton ButtonSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton ButtonPrettyPrinter;
		private System.Windows.Forms.ToolStripButton ButtonSerializationViewer;
		private System.Windows.Forms.ToolStripButton ButtonOptions;
	}
}