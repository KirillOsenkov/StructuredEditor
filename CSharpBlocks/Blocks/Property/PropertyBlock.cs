using System.Collections.Generic;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Undo;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("property")]
	public class PropertyBlock : 
		CodeBlock, 
		ICSharpBlock,
		IClassLevel,
		IHasName, 
		IHasModifiers
	{
		#region ctors

		public PropertyBlock()
			: base()
		{
			GetAccessor = new PropertyGetBlock();
			SetAccessor = new PropertySetBlock();

			Init();
		}

		public PropertyBlock(PropertyGetBlock getBlock, PropertySetBlock setBlock)
			: base()
		{
			GetAccessor = getBlock;
			SetAccessor = setBlock;

			Init();
		}

		private void Init()
		{
			this.MyUniversalControl.OpenCurlyHasNegativeLowerMargin = false;
			NameBlock.MyControl.Box.Margins.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			NameBlock.MyControl.Box.SetMouseSensitivityToMargins();
			NameBlock.MyControl.Layout();
			MyUniversalControl.CanOffsetCurlies = true;

			this.HMembers.Add(Modifiers);
			this.HMembers.Add(NameBlock);
		}

		#endregion

		#region ReplaceWithField

		public void ReplaceWithField()
		{
			using (Transaction.Create(Root.ActionManager))
			{
				FieldBlock field = new FieldBlock();
				field.Modifiers.SetMany(
					this.Modifiers.GetModifierString());
				field.Name = this.Name;
				this.Replace(field);
			}
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

		private TextBoxBlock mNameBlock = new MemberNameBlock();
		public TextBoxBlock NameBlock
		{
			get
			{
				return mNameBlock;
			}
			set
			{
				mNameBlock = value;
			}
		}

		#endregion

		#region Create

		public static PropertyBlock Create(
			string propertyModifiers,
			string propertyType,
			string propertyName)
		{
			PropertyBlock result = new PropertyBlock();
			if (!string.IsNullOrEmpty(propertyModifiers))
			{
				result.Modifiers.SetMany(propertyModifiers);
			}
			if (!string.IsNullOrEmpty(propertyType))
			{
				result.Modifiers.Set(propertyType);
			}
			if (!string.IsNullOrEmpty(propertyName))
			{
				result.Name = propertyName;
			}
			return result;
		}

		#endregion

		#region Parent

		public ClassOrStructBlock ParentClassOrStruct
		{
			get
			{
				return this.ParentParent as ClassOrStructBlock;
			}
		}

		#endregion

		#region Modifiers

		private MemberModifierContainerBlock mModifiers = new MemberModifierContainerBlock();
		public MemberModifierContainerBlock Modifiers
		{
			get
			{
				return mModifiers;
			}
			set
			{
				mModifiers = value;
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
				MemberModifierContainerBlock mm = Modifiers as MemberModifierContainerBlock;
				if (mm != null)
				{
					return mm.TypeBlock;
				}
				return null;
			}
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

		public string TypeText
		{
			get
			{
				return TypeBlock.Text;
			}
		}

		#endregion

		#region Get

		private PropertyGetBlock mGetAccessor;
		public PropertyGetBlock GetAccessor
		{
			get
			{
				return mGetAccessor;
			}
			set
			{
				if (mGetAccessor == value)
				{
					return;
				}
				if (this.Root == null)
				{
					AssignGetAccessor(value);
				}
				else
				{
					using (Transaction.Create(this.Root.ActionManager))
					{
						AssignGetAccessor(value);
					}
				}
			}
		}

		private void AssignGetAccessor(PropertyGetBlock value)
		{
			if (value == null && mGetAccessor != null)
			{
				if (SetAccessor == null)
				{
					this.ReplaceWithField();
				}
				else
				{
					BlockActions.DeleteBlock(mGetAccessor);
				}
			}
			else if (mGetAccessor == null && value != null)
			{
				this.VMembers.AddToBeginning(value);
			}
			else if (mGetAccessor != null && value != null)
			{
				mGetAccessor.Replace(value);
			}
			mGetAccessor = value;
		}

		#endregion

		#region Set

		private PropertySetBlock mSetAccessor;
		public PropertySetBlock SetAccessor
		{
			get
			{
				return mSetAccessor;
			}
			set
			{
				if (mSetAccessor == value)
				{
					return;
				}
				if (this.Root == null)
				{
					AssignSetAccessor(value);
				}
				else
				{
					using (Transaction.Create(this.Root.ActionManager))
					{
						AssignSetAccessor(value);
					}
				}
			}
		}

		private void AssignSetAccessor(PropertySetBlock value)
		{
			if (value == null && mSetAccessor != null)
			{
				if (GetAccessor == null)
				{
					this.ReplaceWithField();
				}
				else
				{
					BlockActions.DeleteBlock(mSetAccessor);
				}
			}
			else if (mSetAccessor == null && value != null)
			{
				this.VMembers.Add(value);
			}
			else if (mSetAccessor != null && value != null)
			{
				mSetAccessor.Replace(value);
			}
			mSetAccessor = value;
		}

		#endregion

		#region DefaultFocusableControl

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			return Modifiers.DefaultFocusableControl();
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Name = storage["name"];
			TypeBlock.Text = storage["type"];
			this.Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = Name;
			storage["type"] = TypeBlock.Text;
			storage["modifiers"] = GetModifierOnlyString();
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			foreach (Block child in restoredChildren)
			{
				if (child is PropertyGetBlock)
				{
					GetAccessor = child as PropertyGetBlock;
				}
				if (child is PropertySetBlock)
				{
					SetAccessor = child as PropertySetBlock;
				}
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "PropertyBlock";
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
