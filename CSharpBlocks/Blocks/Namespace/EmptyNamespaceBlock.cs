using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class EmptyNamespaceBlock :
		EmptyBlock,
		ICSharpBlock,
		INamespaceLevel
	{
		#region ctor

		public EmptyNamespaceBlock()
			: base()
		{
			FillItems();
		}

		#endregion

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

            base.OnKeyPress(sender, e);
        }

        //bool attributesAdded = false;

        //protected override void OnRootChanged(RootBlock oldRoot, RootBlock newRoot)
        //{
        //    base.OnRootChanged(oldRoot, newRoot);
        //    if (newRoot != null && !attributesAdded)
        //    {
        //        attributesAdded = true;
        //        AttributeBlock.AddAttributes(this);
        //    }
        //}

		#region Items

		protected virtual void FillItems()
		{
			ReplaceBlocksItem usingItem = Completion.AddReplaceBlocksItem<UsingBlock, EmptyNamespaceBlock>("using");
			usingItem.Picture = Icons.CodeSnippet;
			usingItem.VisibilityConditions.Add
			(
				new DelegateCondition(
					delegate
					{
						return this.Prev == null;
					}
				)
			);

			AddItem<NamespaceBlock>("namespace");

			AddItem<ClassBlock>("class");
			AddItem<StructBlock>("struct");
			AddItem<InterfaceBlock>("interface");
			AddItem<EnumBlock>("enum");
			AddItem<DelegateBlock>("delegate");

			AddEmptyItem("public");
			AddEmptyItem("internal");
			AddEmptyItem("abstract");
			AddEmptyItem("sealed");
			AddEmptyItem("static");
			AddEmptyItem("partial");

            AttributeBlock.AddAttributes(this);
		}

		protected void AddItem<T>(string keyword)
		{
			Completion.AddCreateBlocksItem<T, EmptyNamespaceBlock>(keyword, Icons.CodeSnippet);
		}

		protected void AddEmptyItem(string keyword)
		{
			EmptyBlockItem item = new EmptyBlockItem(keyword, emptyBlockFactory);
			item.Picture = Icons.Keyword;
			Completion.AddItem(item);
		}

		private static BlockActivatorFactory emptyBlockFactory
			= BlockActivatorFactory.CreateFactory
				<TypeEmptyBlock,
				EmptyNamespaceBlock>();

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			Help.ThisIsEmptyNamespaceBlock
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
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
