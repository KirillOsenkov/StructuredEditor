using System.Windows.Forms;
using System;
using GuiLabs.Editor.Blocks;
using System.Collections.ObjectModel;
using GuiLabs.Editor.UI;
using System.Reflection;
using System.Collections.Generic;

namespace GuiLabs.Editor.Sample
{
	public partial class TutorialForm : Form
	{
		private ReadOnlyCollection<RootBlock> documents;
		private RootBlock rootList = new RootBlock();

		public TutorialForm()
		{
			InitializeComponent();

			documents = InitDocuments();

			BlockView.RootBlock = documents[0];
			RootList.RootBlock = rootList;

			FillRootList();
		}

		private void FillRootList()
		{
			foreach (RootBlock root in documents)
			{
				ListItem label = new ListItem(BlockView, root);
				rootList.Add(label);
			}
		}

		private ReadOnlyCollection<RootBlock> InitDocuments()
		{
			List<RootBlock> result = new List<RootBlock>();

			Assembly ass = Assembly.GetCallingAssembly();
			foreach (Type t in ass.GetTypes())
			{
				if (t.FullName.Contains("TutorialRootBlock"))
				{
					result.Add(Activator.CreateInstance(t) as RootBlock);
				}
			}

			return result.AsReadOnly();
		}

		class ListItem : FocusableLabelBlock
		{
			public ListItem(ViewWindow _view, RootBlock _root) : base(_root.GetType().Name)
			{
				root = _root;
				view = _view;
			}

			protected override void OnClick(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
			{
				base.OnClick(MouseInfo);
				view.RootBlock = root;
			}

			private ViewWindow view;
			private RootBlock root;
		}

	}
}