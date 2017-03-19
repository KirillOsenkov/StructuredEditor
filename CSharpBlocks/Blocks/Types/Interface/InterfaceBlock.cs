using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("interface")]
	public class InterfaceBlock : CodeBlock, ITypeDeclaration
	{
		#region ctor

		public InterfaceBlock()
			: base()
		{
			NameBlock = new TypeNameBlock(2 * ShapeStyle.DefaultFontSize);
			MyUniversalControl.CanOffsetCurlies = true;
			MyUniversalControl.OpenCurlyHasNegativeLowerMargin = false;

			InitModifiers();
			HMembers.Add(Modifiers);

			HMembers.Add(new KeywordLabel("interface"));
			HMembers.Add(NameBlock);
			VMembers.Children.Add(new InterfaceMemberDeclarationBlock());
		}

		#endregion

		#region API

		public void AddMethodDeclaration(
			string modifiers,
			string returnType,
			string name,
			string parameters)
		{
			InterfaceMemberDeclarationBlock newDecl = new InterfaceMemberDeclarationBlock();
			string accum = modifiers;
			if (!string.IsNullOrEmpty(accum))
			{
				accum += " ";
			}
			accum += returnType;
			accum += " " + name;
			newDecl.Text.Text = accum;
			newDecl.AppendParameters(parameters);
			Add(newDecl);
		}

		public void AddPropertyDeclaration(
			string modifiers,
			string propType,
			string name,
			bool hasGetter,
			bool hasSetter)
		{
			InterfaceMemberDeclarationBlock newDecl = new InterfaceMemberDeclarationBlock();
			string accum = modifiers;
			if (!string.IsNullOrEmpty(accum))
			{
				accum += " ";
			}
			accum += propType;
			accum += " " + name;
			newDecl.Text.Text = accum;
			newDecl.AppendPropertyAccessors(hasGetter, hasSetter);
			Add(newDecl);
		}

		public void AddEventDeclaration(
			string modifiers,
			string eventType,
			string name)
		{
			InterfaceMemberDeclarationBlock newDecl = new InterfaceMemberDeclarationBlock();
			string accum = modifiers;
			if (!string.IsNullOrEmpty(accum))
			{
				accum += " ";
			}
			accum += "event";
			accum += " " + eventType;
			accum += " " + name;
			newDecl.Text.Text = accum;
			Add(newDecl);
		}

		public void AddIndexerDeclaration(
			string modifiers,
			string returnType,
			string parameters,
			bool hasGetter,
			bool hasSetter)
		{
			InterfaceMemberDeclarationBlock newDecl = new InterfaceMemberDeclarationBlock();
			string accum = modifiers;
			if (!string.IsNullOrEmpty(accum))
			{
				accum += " ";
			}
			accum += returnType;
			accum += " this";
			newDecl.Text.Text = accum;
			newDecl.AppendThisParameters(parameters);
			newDecl.AppendPropertyAccessors(hasGetter, hasSetter);
			Add(newDecl);
		}

		public void Add(InterfaceMemberDeclarationBlock memberDeclaration)
		{
			InterfaceMemberDeclarationBlock first = this.VMembers.Children.Head as InterfaceMemberDeclarationBlock;
			if (first != null 
				&& first.Prev == null 
				&& first.Next == null
				&& string.IsNullOrEmpty(first.Text.Text))
			{
				first.Replace(memberDeclaration);
				return;
			}
			this.VMembers.Children.Add(memberDeclaration);
		}

		#endregion

		#region Create

		public static InterfaceBlock Create(string interfaceName, string interfaceModifiers)
		{
			InterfaceBlock newInterface = new InterfaceBlock();
			newInterface.Name = interfaceName;
			newInterface.Modifiers.SetMany(interfaceModifiers);
			return newInterface;
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Name = storage["name"];
			Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = Name;
			storage["modifiers"] = Modifiers.GetModifierString();
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			this.VMembers.Clear();
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region Modifiers

		protected virtual void InitModifiers()
		{
			Modifiers = new InterfaceModifierContainerBlock();
		}

		private InterfaceModifierContainerBlock mModifiers;
		public InterfaceModifierContainerBlock Modifiers
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

		public bool IsPartial
		{
			get
			{
				return Modifiers.IsSet("partial");
			}
		}

		#endregion

		#region Name

		private TextBoxBlock mNameBlock;
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

		/// <summary>
		/// Gets or returns the class name.
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

		#endregion

		#region ParentContainer

		public ClassBlock ParentClass
		{
			get
			{
				return ParentParent as ClassBlock;
			}
		}

		public StructBlock ParentStruct
		{
			get
			{
				return ParentParent as StructBlock;
			}
		}

		public ClassOrStructBlock ParentClassOrStruct
		{
			get
			{
				return this.ParentParent as ClassOrStructBlock;
			}
		}

		public NamespaceBlock ParentNamespace
		{
			get
			{
				return ParentParent as NamespaceBlock;
			}
		}

		public bool IsNested
		{
			get
			{
				return ParentClassOrStruct != null;
			}
		}

		#endregion

		#region DefaultFocusableControl

		public override Control DefaultFocusableControl()
		{
			return NameBlock.DefaultFocusableControl();
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "InterfaceBlock";
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
