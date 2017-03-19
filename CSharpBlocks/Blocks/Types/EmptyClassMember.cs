using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using System.Drawing;
using System;

namespace GuiLabs.Editor.CSharp
{
	public class EmptyClassMember : EmptyBlock, ICSharpBlock, IClassLevel
	{
		public EmptyClassMember()
		{
			AddItem<ClassBlock>("class");
			AddItem<EnumBlock>("enum");
			AddItem<StructBlock>("struct");
			AddItem<DelegateBlock>("delegate");
			AddItem<InterfaceBlock>("interface");

			AddItem<MethodBlock>("method");
			AddItem<ConstructorBlock>("ctor");
			AddItem<PropertyBlock>("prop");

			AddEmptyItem("public");
			AddEmptyItem("protected");
			AddEmptyItem("private");
			AddEmptyItem("internal");
			AddEmptyItem("protected internal");

			AddEmptyItem("abstract");
			AddEmptyItem("virtual");
			AddEmptyItem("override");
			AddEmptyItem("sealed override");
			AddEmptyItem("static");

			AddVoidItem();
		}

        bool attributesAdded = false;

        protected override void OnRootChanged(RootBlock oldRoot, RootBlock newRoot)
        {
            base.OnRootChanged(oldRoot, newRoot);
            if (newRoot != null && !attributesAdded)
            {
                attributesAdded = true;
                AttributeBlock.AddAttributes(this);
            }
        }

        protected override void FillItems(CustomItemsRequestEventArgs e)
		{
			LanguageService ls = LanguageService.Get(this);
			ClassOrStructBlock parentClass = ClassNavigator.FindContainingClassOrStruct(this);
			if (ls != null && parentClass != null)
			{
				IEnumerable<TextPictureInfo> types = ls.GetTypeList(parentClass);
				foreach (TextPictureInfo t in types)
				{
					AddEmptyItem(t.Text, t.Picture, e.Items);
				}
			}
		}

		private void AddItem<T>(string itemName)
		{
			Completion.AddCreateBlocksItem<T, EmptyClassMember>(itemName, Icons.CodeSnippet);
		}

		private void AddVoidItem()
		{
			EmptyBlockItem item = new EmptyBlockItem(
				"void",
				emptyMethodFactory);
			item.Picture = Icons.MethodPublic;
			Completion.AddItem(item);
		}

		protected void AddEmptyItem(string keyword)
		{
			AddEmptyItem(keyword, Icons.Keyword, Completion.Items);
		}

		protected void AddEmptyItem(string keyword, Image picture, ICompletionListBuilder items)
		{
			EmptyBlockItem item = new EmptyBlockItem(
				keyword,
				emptyBlockFactory);
			item.Picture = picture;
			items.Add(item);
		}

		private static BlockActivatorFactory emptyMethodFactory
			= BlockActivatorFactory.CreateFactory
			<MethodBlock,
			EmptyClassMember>();

		private static BlockActivatorFactory emptyBlockFactory
			= BlockActivatorFactory.CreateFactory
			<FieldBlock,
			EmptyClassMember>();

		#region OnEvents

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
            if (e.KeyChar == '/')
            {
                if (this.Next != null)
                {
                    this.AppendBlocks(new XmlCommentBlock());
                    e.Handled = true;
                    return;
                }
            }

			if (e.KeyChar == '{')
			{
				FieldBlock prevField = this.Prev as FieldBlock;
				if (prevField != null)
				{
					prevField.ReplaceWithProperty();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				base.OnKeyPress(sender, e);
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
