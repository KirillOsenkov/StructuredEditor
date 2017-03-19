using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using System.Collections.Generic;
using System;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("method")]
	public class MethodBlock : 
		MethodOrPropertyAccessor, 
		IHasName,
		IHasModifiers, 
		ICSharpBlock,
		IClassLevel
	{
		#region ctors

		public MethodBlock()
			: base()
		{
			Init();
		}

		public MethodBlock(VContainerBlock vMembers)
			: base(vMembers)
		{
			Init();
		}

		private void Init()
		{
			InitModifiers();
			MyUniversalControl.CanOffsetCurlies = true;

			NameBlock = new MemberNameBlock();

			HMembers.Add(Modifiers);
			HMembers.Add(NameBlock);
			HMembers.Add(Parameters);
		}

		#endregion

		#region Create

		public static MethodBlock Create(
			string methodModifiers,
			string returnType,
			string methodName)
		{
			MethodBlock newMethod = new MethodBlock();
			if (!string.IsNullOrEmpty(methodName))
			{
				newMethod.Name = methodName;
			}
			if (!string.IsNullOrEmpty(methodModifiers))
			{
				newMethod.Modifiers.SetMany(methodModifiers);
			}
			if (!string.IsNullOrEmpty(returnType))
			{
				newMethod.Modifiers.Set(returnType);
			}
			return newMethod;
		}

		protected override VContainerBlock CreateVMembers()
		{
			BlockStatementBlock result = new BlockStatementBlock();
			result.Add(new StatementLine());
			return result;
		}

		#endregion

		#region Modifiers

		protected virtual void InitModifiers()
		{
			MemberModifierContainerBlock m = new MemberModifierContainerBlock();
			m.TypeBlock.ItemStrings = new string[] { "void" };
			Modifiers = m;
		}

		private ModifierContainer mModifiers;
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

		#endregion

		#region Name

		public virtual string Name
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
			if ((e.KeyChar == '(' && NameBlock.MyTextBox.CaretIsAtEnd)
				 || e.KeyChar == ' ')
			{
				e.Handled = true;
				Parameters.SetCursorToTheBeginning();
			}
		}

		#endregion

		#region Parameters

		private ParameterListBlock mParameters = new ParameterListBlock();
		public ParameterListBlock Parameters
		{
			get
			{
				return mParameters;
			}
			set
			{
				mParameters = value;
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Name = storage["name"];
			TypeBlock.Text = storage["type"];
			this.Modifiers.SetMany(storage["modifiers"]);
			this.Parameters.Text = storage["parameters"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = Name;
			storage["type"] = TypeBlock.Text;
			storage["modifiers"] = GetModifierOnlyString();
			storage["parameters"] = Parameters.Text;
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			StatementLine firstStatement = this.VMembers.Children.Head as StatementLine;
			if (this.VMembers.Children.Count == 1 
				&& firstStatement != null)
			{
				this.VMembers.Children.Delete(firstStatement);
			}
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region Validation
		
		public bool IsValid()
		{
			return Check.ValidIdentifier(this.Name)
				&& this.TypeBlock != null
				&& Check.ValidTypeReference(this.TypeBlock.Text);
		}
		
		#endregion

		#region SetDefaultFocus

		public override void SetDefaultFocus()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				Parameters.SetCursorToTheBeginning();
				return;
			}

			if (this.TypeBlock != null && !string.IsNullOrEmpty(this.TypeBlock.Text))
			{
				NameBlock.SetCursorToTheBeginning();
				return;
			}

			Modifiers.SetCursorToTheEnd();
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "MethodBlock";
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
