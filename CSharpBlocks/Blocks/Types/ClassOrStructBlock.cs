using System.Collections.Generic;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using System.Drawing;

namespace GuiLabs.Editor.CSharp
{
	public class ClassOrStructBlock : CodeBlock, IClassOrStruct, INamespaceLevel
	{
		#region ctor

		public ClassOrStructBlock(string keyword)
			: base()
		{
			NameBlock = new TypeNameBlock(16);

            //HMembers.Add(new PictureBlock(CSharpPictureLibrary.Instance.TypeClass));
            InitModifiers();
			HMembers.Add(Modifiers);
            HMembers.Add(new KeywordLabel(keyword));
			HMembers.Add(NameBlock);
			VMembers.Children.Add(new EmptyClassMember());

			MyUniversalControl.CanOffsetCurlies = true;
		}

		#endregion

		#region Methods

		public MethodBlock AddMethod(
			string methodName,
			string modifiers,
			string returnType)
		{
			MethodBlock newMethod = new MethodBlock();
			newMethod.Name = methodName;
			newMethod.Modifiers.SetMany(modifiers);
			newMethod.TypeBlock.Text = returnType;
			AddMethod(newMethod);
			return newMethod;
		}

		public void AddMethod(MethodBlock method)
		{
			this.VMembers.Add(method, new EmptyClassMember());
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			Name = storage["name"];
			this.Modifiers.SetMany(storage["modifiers"]);
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["name"] = Name;
			storage["modifiers"] = this.Modifiers.GetModifierString();
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child, new EmptyClassMember());
			}
		}

		#endregion

		#region Modifiers

		protected virtual void InitModifiers()
		{
			Modifiers = new ClassModifierContainerBlock();
		}

		private ClassModifierContainerBlock mModifiers;
		public ClassModifierContainerBlock Modifiers
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

		public string GetFullName()
		{
			string path = ClassNavigator.GetFullPath(this);
			if (!string.IsNullOrEmpty(path))
			{
				path += ".";
			}
			path += Name;
			return path;
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
				mNameBlock = value;
			}
		}

		/// <summary>
		/// Gets or returns the name of a class or struct.
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
				VMembers.AddAcceptableBlockTypes<IClassLevel>();
				VMembers.AddSeparatorType<EmptyClassMember>();
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
				return this.ParentParent as StructBlock;
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
				return this.ParentParent as NamespaceBlock;
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
	}
}
