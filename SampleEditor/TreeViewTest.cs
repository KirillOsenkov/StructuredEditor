using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Sample
{
	public partial class TreeViewTest : Form
	{
		public TreeViewTest()
		{
			InitializeComponent();

			FillStandardTreeView();
			FillOurTreeView();
		}

		private void FillStandardTreeView()
		{
			TreeNode t1 = treeView1.Nodes.Add("test1");
			TreeNode t11 = t1.Nodes.Add("subtest11");
			TreeNode t12 = t1.Nodes.Add("subtest12");
			TreeNode t2 = treeView1.Nodes.Add("test2");
			TreeNode t21 = t2.Nodes.Add("subtest21");
			TreeNode t211 = t21.Nodes.Add("subtest211");
			TreeNode t22 = t2.Nodes.Add("subtest22");
			treeView1.ExpandAll();
		}

		private static ILineStyleInfo mRedLineStyle;
		public ILineStyleInfo RedLineStyle
		{
			get
			{
				if (mRedLineStyle == null)
				{
					mRedLineStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(Color.Red, 1);
				}
				return mRedLineStyle;
			}
		}

		private void FillOurTreeView()
		{
			#region Old way

			//TreeViewNode t1 = new TreeViewNode("test1");
			//treeViewControl1.Root.Children.Add(t1);

			//TreeViewNode t11 = new TreeViewNode("subtest11");
			//t1.VMembers.Children.Add(t11);

			//TreeViewNode t12 = new TreeViewNode("subtest12");
			//t1.VMembers.Children.Add(t12);

			//TreeViewNode t2 = new TreeViewNode("test2");
			//treeViewControl1.Root.Children.Add(t2);
			//t2.MyNodeControl.TreeLineStyle = RedLineStyle;

			//TreeViewNode t21 = new TreeViewNode("subtest21");
			//t2.VMembers.Children.Add(t21);

			//TreeViewNode t22 = new TreeViewNode("subtest22");
			//t2.VMembers.Children.Add(t22);
			////t21.VMembers.Children.Add(new SampleRootBlock());

			//TreeViewNode t211 = new TreeViewNode("subtest211");
			//t21.VMembers.Children.Add(t211);
			//// t21.HMembers.Children.Add(new ButtonBlock("Hello"));

			#endregion

			#region New way

			TreeViewNode t1 = treeViewControl1.AddNode("test1");
			TreeViewNode t11 = t1.AddNode("subtest11");
			TreeViewNode t12 = t1.AddNode("subtest12");
			TreeViewNode t2 = treeViewControl1.AddNode("test2");
			TreeViewNode t21 = t2.AddNode("subtest21");
			TreeViewNode t211 = t21.AddNode("subtest211");
			TreeViewNode t22 = t2.AddNode("subtest22");

			#endregion

			NodeRelationship r = new NodeRelationship();
			r.Sender = t11;
			r.Receivers = new List<Block>();
			r.Receivers.Add(t22);
			r.Receivers.Add(t21);

			treeViewControl1.Root.Relationship = r;
		}
	}
}