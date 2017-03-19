using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Editor.UI;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("field")]
	public class FieldBlock : 
		CodeLineBlock, 
		IClassLevel,
		ICSharpBlock,
		IHasModifiers, 
		IHasType, 
		IHasName
	{
		#region ctor

		public FieldBlock()
			: base()
		{
			Init();
		}

		private void Init()
		{
			InitModifiers();
			this.Draggable = true;

			NameBlock = new MemberNameBlock();
			this.Children.Add(Modifiers);
			this.Children.Add(NameBlock);
			this.Children.Add(" ");

			FillItems();

			this.UserDeletable = true;
			this.MyControl.Focusable = true;
			this.MyListControl.Box.Margins.SetAll(1);
			this.MyListControl.Layout();
		}

		#endregion

		#region Completion

		protected virtual void FillItems()
		{
			AddItem<ClassBlock>("class");
			AddItem<EnumBlock>("enum");
			AddItem<StructBlock>("struct");
			AddItem<DelegateBlock>("delegate");
			AddItem<InterfaceBlock>("interface");

			AddItem<MethodBlock>("method");
			AddItem<MethodBlock>("void");
			AddItem<ConstructorBlock>("ctor");
			AddItem<PropertyBlock>("prop");
		}

		public void AddItem<T>(string s)
		{
			ReplaceMemberEmptyBlockItem item = new ReplaceMemberEmptyBlockItem(
				s,
				BlockActivatorFactory.Types<T>(),
				this);
			item.Picture = Icons.CodeSnippet;
			Modifiers.Completion.AddItem(item);
		}

		#endregion

		#region Name

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

		private TextBoxBlock mNameBlock;
		public TextBoxBlock NameBlock
		{
			get
			{
				return mNameBlock;
			}
			set
			{
				Param.CheckNotNull(value, "NameBlock");
				if (mNameBlock != null)
				{
					mNameBlock.MyTextBox.PreviewKeyPress -= NameBlock_PreviewKeyPress;
				}
				mNameBlock = value;
				mNameBlock.MyTextBox.PreviewKeyPress += NameBlock_PreviewKeyPress;
			}
		}

		void NameBlock_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			bool canReplace = CanReplace && NameBlock.MyTextBox.CaretIsAtEnd;

			if (canReplace)
			{
				if (e.KeyChar == '(')
				{
					e.Handled = true;
					ReplaceWithMethod();
				}

				if (e.KeyChar == '{')
				{
					e.Handled = true;
					ReplaceWithProperty();
				}
			}
		}

		#endregion

		#region Modifiers

		protected virtual void InitModifiers()
		{
			Modifiers = new MemberModifierContainerBlock();
		}

		public string GetModifierOnlyString()
		{
			List<ModifierSelectionBlock> modBlocks = new List<ModifierSelectionBlock>();
			foreach (ModifierSelectionBlock m in this.Modifiers.ModifierBlocks)
			{
				if (m != this.TypeBlock)
				{
					modBlocks.Add(m);
				}
			}
			return this.Modifiers.GetModifierString(modBlocks);
		}

		private MemberModifierContainerBlock mModifiers;
		public MemberModifierContainerBlock Modifiers
		{
			get
			{
				return mModifiers;
			}
			set
			{
				if (mModifiers != null)
				{
					mModifiers.MyControl.DoubleClick -= MyControl_DoubleClick;
				}
				mModifiers = value;
				if (mModifiers != null)
				{
					mModifiers.MyControl.DoubleClick += MyControl_DoubleClick;
				}
			}
		}

		public bool IsEmptyField()
		{
			return string.IsNullOrEmpty(this.Modifiers.GetModifierString())
								&& string.IsNullOrEmpty(this.Name);
		}

		void MyControl_DoubleClick(GuiLabs.Canvas.Events.MouseWithKeysEventArgs e)
		{
			if (Root != null && Root.ActiveBlock is ModifierSeparatorBlock)
			{
				this.SetFocus();
			}
		}

		ModifierContainer IHasModifiers.Modifiers
		{
			get { return Modifiers; }
		}

		public virtual TypeSelectionBlock TypeBlock
		{
			get
			{
				return Modifiers != null ? Modifiers.TypeBlock : null;
			}
		}

		#endregion

		#region Replacing

		public bool CanReplace
		{
			get
			{
				string name = this.NameBlock.Text;
				bool result = !name.Contains("=");
				result = result && !name.Contains(",");
				return result;
			}
		}

		public void TrimName()
		{
			this.Name = this.Name.Trim();
		}

		public void ReplaceWithMethod()
		{
			TrimName();
			MethodBlock newMethod =
				MethodBlock.Create(
					Modifiers.GetModifierString(),
					this.TypeBlock.Text,
					this.NameBlock.Text);

			this.Replace(newMethod);
		}

		public void ReplaceWithProperty()
		{
			TrimName();
			PropertyBlock newProperty =
				PropertyBlock.Create(
					Modifiers.GetModifierString(),
					this.TypeBlock.Text,
					this.NameBlock.Text);
			using (Redrawer r = new Redrawer(this.Root))
			{
				this.Replace(newProperty);
				Block toFocus = newProperty.GetAccessor.FindFirstFocusableChild();
				if (toFocus != null)
				{
					toFocus.SetFocus();
				}
			}
		}

		#endregion

		#region SetDefaultFocus

		public override void SetDefaultFocus()
		{
			if (TypeBlock.IsSet())
			{
				this.NameBlock.SetCursorToTheBeginning();
			}
			else
			{
				this.TypeBlock.RemoveFocus(MoveFocusDirection.SelectPrev);
			}
		}

		#endregion

		public void SetInitialText(string text)
		{
			Modifiers.SetMany(text);
		}

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			this.Name = storage["name"];
			this.TypeString = storage["type"];
			this.Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = this.Name;
			storage["type"] = this.TypeString;
			storage["modifiers"] = GetModifierOnlyString();
		}

		public override IEnumerable<Block> GetChildrenToSerialize()
		{
			return null;
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "FieldBlock";
		}

		#endregion

		#region TypeString

		public string TypeString
		{
			get
			{
				return TypeBlock.Text;
			}
			set
			{
				Modifiers.Set(value);
			}
		}

		#endregion

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
