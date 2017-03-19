using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("enum")]
	public class EnumBlock : CodeBlock, ITypeDeclaration
	{
		#region ctor

		public EnumBlock()
		{
			NameBlock.MyControl.Box.Padding.SetLeftAndRight(ShapeStyle.DefaultFontSize);
			MyUniversalControl.CanOffsetCurlies = true;

			this.HMembers.Add(Modifiers);
			this.HMembers.Add(new KeywordLabel("enum"));
			this.HMembers.Add(NameBlock);
			this.VMembers.Add(new EnumValue());
		}

		#endregion

		#region API

		public new void Add(string enumValue)
		{
			EnumValue firstValue = this.VMembers.Children.Head as EnumValue;

			if (firstValue != null
				&& firstValue.Prev == null
				&& firstValue.Next == null
				&& string.IsNullOrEmpty(firstValue.Text))
			{
				firstValue.Text = enumValue;
				return;
			}

			this.VMembers.Add(new EnumValue(enumValue));
		}

		#endregion

		#region DefaultFocusableControl

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			return NameBlock.DefaultFocusableControl();
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			this.Name = storage["name"];
			this.Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = this.Name;
			storage["modifiers"] = this.Modifiers.GetModifierString();
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			foreach (Block b in restoredChildren)
			{
				if (b is EnumValue)
				{
					Add((b as EnumValue).Text);
				}
			}
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
                this.VMembers.AddAcceptableBlockTypes<EnumValue>();
            }
        }

        #endregion

        #region Modifiers

        private ModifierContainer mModifiers = new TypeModifierContainerBlock();
		public ModifierContainer Modifiers
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

		private TypeNameBlock mNameBlock = new TypeNameBlock(16);
		public TypeNameBlock NameBlock
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

		#region Style

		protected override string StyleName()
		{
			return "EnumBlock";
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
