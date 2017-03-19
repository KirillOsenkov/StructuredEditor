using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("delegate")]
	public class DelegateBlock : CodeLineBlock, ITypeDeclaration
	{
		#region ctor

		public DelegateBlock()
		{
			Init();
		}

		private void Init()
		{
			this.UserDeletable = true;
			this.Draggable = true;
			this.MyControl.Focusable = true;
			this.MyListControl.Box.Margins.SetAll(1);

			const int x = ShapeStyle.DefaultFontSize;
			const int x2 = ShapeStyle.DefaultFontSize / 2;

			TypeBlock = new TypeNameBlock();
			TypeBlock.MyTextBox.MinWidth = x;
			TypeBlock.MyTextBox.Box.Padding.SetLeftAndRight(x, x2);
			TypeBlock.MyTextBox.Layout();
			TypeBlock.MyTextBox.KeyPress += TypeBlock_KeyPress;
			TypeBlock.MyTextBox.CharFilters.Add(CommonCharFilters.AcceptNoWhitespace);

			NameBlock = new TypeNameBlock();
			NameBlock.MyTextBox.MinWidth = x;
			NameBlock.MyTextBox.Box.Padding.SetLeftAndRight(x2, 0);
			NameBlock.MyTextBox.Layout();
			NameBlock.MyTextBox.KeyPress += NameBlock_KeyPress;
			NameBlock.MyTextBox.CharFilters.Add(CommonCharFilters.AcceptNoWhitespace);

			this.Children.Add(Modifiers);
			this.Children.Add(new KeywordLabel("delegate"));
			this.Children.Add(TypeBlock);
			this.Children.Add(NameBlock);
			this.Children.Add(Parameters);
		}

		void TypeBlock_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
				NameBlock.SetCursorToTheBeginning();
			}
		}

		void NameBlock_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
				Parameters.SetCursorToTheBeginning();
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

		#region TypeName

		private TypeNameBlock mTypeBlock;
		public TypeNameBlock TypeBlock
		{
			get
			{
				return mTypeBlock;
			}
			set
			{
				mTypeBlock = value;
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

		private TypeNameBlock mNameBlock;
		public TypeNameBlock NameBlock
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
			this.Name = storage["name"];
			this.TypeBlock.Text = storage["type"];
			this.Modifiers.SetMany(storage["modifiers"]);
			this.Parameters.Text = storage["parameters"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = this.Name;
			storage["type"] = this.TypeBlock.Text;
			storage["modifiers"] = this.Modifiers.GetModifierString();
			storage["parameters"] = this.Parameters.Text;
		}

		public override IEnumerable<Block> GetChildrenToSerialize()
		{
			return null;
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

			if (!string.IsNullOrEmpty(this.TypeBlock.Text))
			{
				NameBlock.SetCursorToTheBeginning();
				return;
			}

			if (!string.IsNullOrEmpty(this.Modifiers.ToString()))
			{
				this.TypeBlock.SetCursorToTheBeginning();
				return;
			}

			Modifiers.SetCursorToTheEnd();
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "DelegateBlock";
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
