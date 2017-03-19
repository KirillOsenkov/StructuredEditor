using System;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("constructor")]
	public class ConstructorBlock : MethodBlock
	{
		#region ctors

		public ConstructorBlock()
			: base()
		{
			Init();
		}

		public ConstructorBlock(VContainerBlock members)
			: base(members)
		{
			Init();
		}

		private void Init()
		{
			// dirty hack: hide the NameBlock because a constructor
			// has a predefined name: ConstructorName
			this.NameBlock.Hidden = true;
			this.NameBlock.Parent.CheckVisibility();
			this.NameBlock.Parent.Children.Append(this.NameBlock, ConstructorName);
			this.ParentChanged += ConstructorBlock_ParentChanged;
		}

		void ConstructorBlock_ParentChanged()
		{
			if (this.ParentClassOrStruct != null)
			{
				this.ConstructorName.MyLabel.TextProvider = this.ParentClassOrStruct.NameBlock;
			}
			else
			{
				this.ConstructorName.MyLabel.TextProvider = null;
			}
		}

		#endregion

		#region Modifiers

		protected override void InitModifiers()
		{
			Modifiers = new ConstructorModifierContainerBlock();
		}

		#endregion

		#region Name

		private LabelBlock ConstructorName = new LabelBlock();

		public override string Name
		{
			get
			{
				return ConstructorName.Text;
			}
			set
			{
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			this.Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["modifiers"] = this.Modifiers.GetModifierString();
		}

		#endregion

		#region Visitor

		public override void AcceptVisitor(IVisitor visitor)
		{
			visitor.Visit(this);
		}

		#endregion
	}
}
