using System.Collections.Generic;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.CSharp.Actions;
using System.Windows.Forms;
using GuiLabs.Canvas.Controls;
using GuiLabs.Undo;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("compilationUnit")]
	public class CodeUnitBlock : RootBlock, ICSharpBlock
	{
		#region ctor

		/// <summary>
		/// Only specify the default child block
		/// </summary>
		public CodeUnitBlock()
			: base()
		{
			StyleFactory.Instance = new CSharpStyleFactory();
			UniversalControl.Design = UniversalControl.UniversalControlDesign.NoBackground;
			UniversalControl.DefaultCurlyType = UniversalControl.TypeOfCurlies.CSharp;

			this.SetDefaultFocus();
			//this.ActionManager.UndoBufferChanged += History_UndoBufferChanged;
			this.LanguageService = new LanguageService();
			this.AddAcceptableBlockTypes<INamespaceLevel>();
			this.AddSeparatorType<EmptyNamespaceBlock>();
			Clear();
		}

		#endregion

		public override void Clear()
		{
			using (Transaction.Create(this.ActionManager))
			{
				base.Clear();
				this.Add(new EmptyNamespaceBlock());
			}
		}

		#region OnEvents

        public void ShowOptionsDialog()
        {
            OptionsForm.ShowOptions(this);
        }

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F12)
			{
                ShowOptionsDialog();
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		#region LanguageService

		private LanguageService mLanguageService;
		public LanguageService LanguageService
		{
			get
			{
				return mLanguageService;
			}
			set
			{
				mLanguageService = value;
			}
		}

		#endregion

		#region Usings

		public UsingBlock UsingSection
		{
			get
			{
				return this.Children.Head as UsingBlock;
			}
		}

		public IEnumerable<UsingDirective> UsingDirectives
		{
			get
			{
				UsingBlock u = UsingSection;
				if (u == null) return null;

				return u.VMembers.FindChildren<UsingDirective>();
			}
		}

		public void EnsureUsingBlockExists()
		{
			if (UsingSection == null)
			{
				this.Children.Prepend((Block)new UsingBlock());
			}
		}

		public void AddUsing(UsingDirective usingDirective)
		{
			AddUsingAction action = new AddUsingAction(this, usingDirective);
			this.ActionManager.RecordAction(action);
		}

		public void AddUsing(string usingDirective)
		{
			AddUsing(new UsingDirective(usingDirective));
		}

		public void AddUsings(params string[] usings)
		{
			AddUsingAction action = new AddUsingAction(
				this,
				usings);
			this.ActionManager.RecordAction(action);

			//			UsingBlock b = UsingSection;
			//
			//			List<string> toAdd;
			//			if (b == null)
			//			{
			//				toAdd = new List<string>(usings);
			//			}
			//			else
			//			{
			//				toAdd = new List<string>();
			//				foreach (string u in usings)
			//				{
			//					if (!b.Exists(u))
			//					{
			//						toAdd.Add(u);
			//					}
			//				}
			//			}
			//
			//			if (this.Root != null)
			//			{
			//				using (Transaction t = Transaction.Create(this.Root))
			//				{
			//					if (b == null)
			//					{
			//						b = new UsingBlock();
			//						this.AddToBeginning(b);
			//					}
			//					foreach (string s in toAdd)
			//					{
			//						b.Add(s);
			//					}
			//				}
			//			}
			//			else
			//			{
			//				using (Redrawer r = new Redrawer(this.Root))
			//				{
			//					if (b == null)
			//					{
			//						b = new UsingBlock();
			//						this.Children.Prepend(b);
			//					}
			//					foreach (string s in usings)
			//					{
			//						b.Add(s);
			//					}
			//				}
			//			}
		}

		#endregion

		#region Namespaces

		public NamespaceBlock AddNamespace(string namespaceName)
		{
			NamespaceBlock newNamespace = new NamespaceBlock();
			newNamespace.Name = namespaceName;
			AddNamespace(newNamespace);
			return newNamespace;
		}

		public void AddNamespace(NamespaceBlock newNamespace)
		{
			using (Transaction.Create(Root.ActionManager))
			{
				EnsureNonEmptyContainer();
				this.Add(newNamespace, new EmptyNamespaceBlock());
			}
		}

		public void EnsureNonEmptyContainer()
		{
			if (this.Children.Count == 0)
			{
				this.Add(new EmptyNamespaceBlock());
			}
		}

		#endregion

		void save_Pushed()
		{
			this.SaveToFile(filename.Text);
		}

		public string PrettyPrint()
		{
			return PrettyPrinter.Print(this);
		}

		#region Memento

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			using (Transaction.Create(this.ActionManager))
			{
				EnsureNonEmptyContainer();
				foreach (Block child in restoredChildren)
				{
					if (child is NamespaceBlock)
					{
						this.AddNamespace(child as NamespaceBlock);
					}
					if (child is UsingBlock)
					{
						if (this.UsingSection != null)
						{
							this.UsingSection.Delete();
						}
						this.AddToBeginning(child);
					}
					if (child is ITypeDeclaration)
					{
						this.Add(child, new EmptyNamespaceBlock());
					}
				}
			}
		}

		#endregion

		TextBoxBlock filename = new TextBoxBlock("doc.txt");
		ButtonBlock save = new ButtonBlock("Save");

		//void History_UndoBufferChanged()
		//{
		//    MethodBlock main = ClassNavigator.FindMain(this);
		//    string text;
		//    if (main != null)
		//    {
		//        text = "Main found: " + ClassNavigator.GetFullPath(main) + "." + main.Name;
		//    }
		//    else
		//    {
		//        text = "Main not found";
		//    }
		//    status.Text = text;

		//    //System.Text.StringBuilder s = new System.Text.StringBuilder();
		//    //foreach (Block c in ClassNavigator.FindChildren<ClassBlock>(this, true))
		//    //{
		//    //    s.AppendLine(c.ToString());
		//    //}
		//    //System.Windows.Forms.MessageBox.Show(s.ToString());
		//}

		//private LabelBlock status = new LabelBlock("Add a method called 'Main'");

		public void AcceptVisitor(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}

	//public class HideMethodsVisibilityFilter : VisibilityFilter
	//{
	//    private static HideMethodsVisibilityFilter mInstance = new HideMethodsVisibilityFilter();
	//    public static HideMethodsVisibilityFilter Instance
	//    {
	//        get
	//        {
	//            return mInstance;
	//        }
	//    }

	//    public override bool ShouldBeVisible(Block block)
	//    {
	//        bool visible = true;
	//        if (block is MethodBlock)
	//        {
	//            visible = false;
	//        }
	//        if (block is ISeparatorBlock
	//            && block.Prev != null
	//            && block.Prev is MethodBlock)
	//        {
	//            visible = false;
	//        }
	//        return visible;
	//    }
	//}

	//public enum VisibilityFilterKind
	//{
	//    HideMethods,
	//    ShowAll
	//}
}
