using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using System.IO;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Undo;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public partial class EditorForm : Form
	{
		#region ctor

		public EditorForm()
		{
			InitializeComponent();
			viewWindow1.Repaint += viewWindow1_Repaint;

			CodeUnit = new CodeUnitBlock();

			ToggleContextHelp();

			ButtonContextHelp.Checked = !SplitterMain.Panel2Collapsed;
			DisplayContextHelp();
			EnableUI();
		}

		#endregion

		#region Generated code

		private PrettyPrinter pp = new PrettyPrinter();
		private GeneratedCodeViewer generatedCode = new GeneratedCodeViewer();
		private GeneratedCodeViewer SerializationViewer = new GeneratedCodeViewer();

		public void UpdateGeneratedCode()
		{
			pp.Clear();
			pp.Visit(CodeUnit);
			generatedCode.Code = pp.ToString();
		}

		public void UpdateSerializationViewer()
		{
			if (SerializationViewer == null || SerializationViewer.IsDisposed)
			{
				return;
			}

			Block active = CodeUnit.ActiveBlock;
			if (active != null)
			{
				SerializationViewer.Code = active.Serialize();
			}
		}

		#endregion

		#region Context help

		void CodeUnit_ShouldDisplayContextHelp(Block itemChanged)
		{
			DisplayContextHelp(itemChanged);
		}

		void CodeUnit_ActiveBlockChanged(Block oldBlock, Block newBlock)
		{
			if (CodeUnit != null
				&& CodeUnit.ActionManager != null
				&& CodeUnit.ActionManager.ActionIsExecuting)
			{
				return;
			}
			//StatusBar.Text = CodeUnit.ActiveBlock.ToString();
			FillBreadcrumb();
			DisplayContextHelp(newBlock);
			UpdateSerializationViewer();
		}

		void FillBreadcrumb()
		{
			Block block = CodeUnit.ActiveBlock;
			List<string> classes = new List<string>();
			foreach (ClassOrStructBlock c in ClassNavigator.FindContainingClassOrStructs(block))
			{
				classes.Insert(0, c.Name);
			}
			string[] classesArray = classes.ToArray();
			string classesString = "";
			if (classesArray.Length > 0)
			{
				classesString = string.Join(".", classesArray);
			}
			MethodBlock method = ClassNavigator.FindContainingMethod(block);
			string methodString = "";
			if (method != null)
			{
				methodString = method.Name;
			}

			Breadcrumb.Text = classesString + "." + methodString;
		}

		void DisplayContextHelp()
		{
			if (CodeUnit != null)
			{
				DisplayContextHelp(CodeUnit.ActiveBlock);
			}
		}

		void DisplayContextHelp(Block block)
		{
			if (block == null)
			{
				return;
			}

			TextContextHelp.ShowHelp(block.HelpStrings);
		}

		public void ToggleContextHelp()
		{
			SplitterMain.Panel2Collapsed = !SplitterMain.Panel2Collapsed;
			ButtonContextHelp.Checked = !SplitterMain.Panel2Collapsed;
			DisplayContextHelp();
		}

		#endregion

		#region CodeUnit

		private CodeUnitBlock mCodeUnit;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CodeUnitBlock CodeUnit
		{
			get
			{
				return mCodeUnit;
			}
			set
			{
				if (mCodeUnit != null)
				{
					viewWindow1.RootBlock = null;
					mCodeUnit.ShouldShowStatus -= CodeUnit_ShouldShowStatus;
					mCodeUnit.ActionManager.CollectionChanged -= CodeUnit_UndoBufferChanged;
					mCodeUnit.ActiveBlockChanged -= CodeUnit_ActiveBlockChanged;
					mCodeUnit.ShouldDisplayContextHelp -= CodeUnit_ShouldDisplayContextHelp;
				}
				mCodeUnit = value;
				if (mCodeUnit != null)
				{
					viewWindow1.RootBlock = CodeUnit;
					mCodeUnit.ShouldShowStatus += CodeUnit_ShouldShowStatus;
					mCodeUnit.ActionManager.CollectionChanged += CodeUnit_UndoBufferChanged;
					mCodeUnit.ActiveBlockChanged += CodeUnit_ActiveBlockChanged;
					mCodeUnit.ShouldDisplayContextHelp += CodeUnit_ShouldDisplayContextHelp;
					EnableUI();
				}
			}
		}

		#endregion

		#region CodeUnit event handlers

		void CodeUnit_ShouldShowStatus(string param)
		{
			StatusBar.Text = param;
		}

		#endregion

		#region Repaint

		private int repaintCount = 0;
		void viewWindow1_Repaint(GuiLabs.Canvas.Renderer.IRenderer Renderer)
		{
			repaintCount++;
			RedrawCounterLabel.Text = repaintCount.ToString();
		}

		#endregion

		#region Undo/Redo

		void CodeUnit_UndoBufferChanged(object sender, EventArgs e)
		{
			EnableUI();
			DisplayContextHelp();
			UpdateGeneratedCode();
		}

		public void EnableUI()
		{
			bool canUndo = CodeUnit.ActionManager.CanUndo;
			bool canRedo = CodeUnit.ActionManager.CanRedo;

			//Options.ButtonUndo.Enabled = canUndo;
			//Options.ButtonRedo.Enabled = canRedo;
			ButtonUndo.Enabled = canUndo;
			ButtonRedo.Enabled = canRedo;
		}

		#endregion

		#region Hello World

		private void HelloWorld()
		{
            using (new Redrawer(CodeUnit))
			using (Transaction.Create(CodeUnit.ActionManager))
			{
				CodeUnit.AddUsings(
					"System",
					"System.Collections.Generic",
					"System.Text"
				);

				NamespaceBlock n = CodeUnit.AddNamespace("GuiLabs.Editor.Test");
				ClassBlock c = n.AddClass("Program", "public static");
				MethodBlock m = c.AddMethod("Main", "public static", "void");
				m.VMembers.Add(new ForeachBlock());
				//BlockStatementBlock b = new BlockStatementBlock();
				//b.Add(new ForeachBlock());
				//m.VMembers = b;

				// AddEnum(n);
				// AddInterface(n);
			}
			DisplayContextHelp();
		}

		private void AddNamespaces(int howMany)
		{
			for (int i = 0; i < howMany; i++)
			{
				NamespaceBlock n = CodeUnit.AddNamespace("namespace " + i.ToString());
				ClassBlock c1 = n.AddClass("class " + i.ToString(), "public");
				ClassBlock c2 = n.AddClass("class (2) " + i.ToString(), "");
			}
		}

		#endregion

		#region Events

		private void EditorForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				ToggleContextHelp();
			}
		}

		private void ButtonHelloWorld_MouseDown(object sender, MouseEventArgs e)
		{
			HelloWorld();
		}

		private void ButtonContextHelp_MouseDown(object sender, MouseEventArgs e)
		{
			ToggleContextHelp();
		}

		private void ButtonUndo_MouseDown(object sender, MouseEventArgs e)
		{
            using (new Redrawer(CodeUnit))
            {
                CodeUnit.ActionManager.Undo();
            }
			
		}

		private void ButtonRedo_MouseDown(object sender, MouseEventArgs e)
		{
            using (new Redrawer(CodeUnit))
            {
                CodeUnit.ActionManager.Redo();
            }
		}

		#endregion

		private void ButtonNew_Click(object sender, EventArgs e)
		{
			CodeUnit.Clear();
		}

		private void ButtonOpen_Click(object sender, EventArgs e)
		{
			string fileName = AskOpenFileName();
			if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
			{
				CodeUnit = LoadFromString(File.ReadAllText(fileName));
			}
		}

		public CodeUnitBlock LoadFromString(string contents)
		{
			Memento snapshot = Memento.ReadFromString(contents);
			return BlockFactory.Instance.CreateBlock(snapshot) as CodeUnitBlock;
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			string fileName = AskSaveFileName();
			if (!string.IsNullOrEmpty(fileName))
			{
				CodeUnit.SaveToFile(fileName);
			}
		}

		public static readonly string FileFilter = "Blocks files (*.blocks)|*.blocks|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

		public string AskOpenFileName()
		{
			OpenFileDialog o = new OpenFileDialog();
			o.InitialDirectory = Application.StartupPath;
			o.Filter = FileFilter;
			o.DefaultExt = "blocks";
			DialogResult result = o.ShowDialog();
			if (result == DialogResult.OK)
			{
				return o.FileName;
			}
			return null;
		}

		public string AskSaveFileName()
		{
			SaveFileDialog s = new SaveFileDialog();
			s.InitialDirectory = Application.StartupPath;
			s.Filter = FileFilter;
			s.DefaultExt = "blocks";
			DialogResult result = s.ShowDialog();
			if (result == DialogResult.OK)
			{
				return s.FileName;
			}
			return null;
		}

		private void PrettyPrinter_Click(object sender, EventArgs e)
		{
			generatedCode.Visible = ButtonPrettyPrinter.Checked;
		}

		private void ButtonSerializationViewer_Click(object sender, EventArgs e)
		{
			SerializationViewer.Visible = ButtonSerializationViewer.Checked;
		}

        private void ButtonOptions_Click(object sender, EventArgs e)
        {
            CodeUnit.ShowOptionsDialog();
        }
	}
}