using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("namespace")]
	public class NamespaceBlock : CodeBlock, INamespaceLevel
	{
		#region ctor

		public NamespaceBlock()
			: base()
		{
			this.NameBlock = new NameBlock(16);
			this.NameBlock.MyTextBox.Box.Padding.SetLeftAndRight(ShapeStyle.DefaultFontSize);

			this.HMembers.Add(new KeywordLabel("namespace"));
			this.HMembers.Add(NameBlock);

			this.HMembers.CanBeSelected = false;

			// Add a default initial child to Members (empty text block)
			this.VMembers.Add(new EmptyNamespaceBlock());
		}

		#endregion

		#region VMembers

		public override VContainerBlock VMembers
		{
			get
			{
				return base.VMembers;
			}
			set
			{
				base.VMembers = value;
				this.VMembers.CanBeSelected = false;
				this.VMembers.AddAcceptableBlockTypes<INamespaceLevel>();
				this.VMembers.AddSeparatorType<EmptyNamespaceBlock>();
			}
		}

		#endregion

		#region API

		public static NamespaceBlock Create(string namespaceName)
		{
			NamespaceBlock newNamespace = new NamespaceBlock();
			newNamespace.Name = namespaceName;
			return newNamespace;
		}

		public void AddClass(ClassBlock newClass)
		{
			this.VMembers.Add(newClass, new EmptyNamespaceBlock());
		}

		public ClassBlock AddClass(string className, string modifiers)
		{
			ClassBlock newClass = new ClassBlock();
			newClass.Name = className;
			newClass.Modifiers.SetMany(modifiers);
			AddClass(newClass);
			return newClass;
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Name = storage["name"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = Name;
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child, new EmptyNamespaceBlock());
			}
		}

		#endregion

		private TextBoxBlock mNameBlock;
		public TextBoxBlock NameBlock
		{
			get
			{
				return mNameBlock;
			}
			set
			{
				if (mNameBlock != null)
				{
					mNameBlock.KeyDown -= mNameBlock_KeyDown;
				}
				mNameBlock = value;
				if (mNameBlock != null)
				{
					mNameBlock.KeyDown += mNameBlock_KeyDown;
				}
			}
		}

		void mNameBlock_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Tab)
			{
				this.SetFocus();
			}
		}

		/// <summary>
		/// Gets or returns the namespace name.
		/// The setter neither changes the Undo stack nor redraws.
		/// </summary>
		public string Name
		{
			get
			{
				return NameBlock.Text;
			}
			set
			{
				NameBlock.Text = value;
			}
		}

		#region Focus

		public override Control DefaultFocusableControl()
		{
			return NameBlock.DefaultFocusableControl();
		}

		#endregion

		//internal static System.Type[] InsertionList =
		//{
		//    // typeof(EmptyNamespaceBlock),
		//    typeof(NamespaceBlock), 
		//    typeof(EmptyNamespaceBlock)
		//};

		#region Style

		protected override string StyleName()
		{
			return "NamespaceBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"Namespace declaration.",
			Help.PressKeyToSelect("the namespace name", "RightArrow", "Tab")
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string s in mHelpStrings)
				{
					yield return s;
				}
				foreach (string parentHelpStrings in GetOldHelpStrings())
				{
					yield return parentHelpStrings;
				}
			}
		}

		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion

		#region AcceptVisitor

		public override void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
