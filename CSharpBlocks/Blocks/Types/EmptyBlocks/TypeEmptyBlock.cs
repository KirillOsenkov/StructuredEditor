using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public class TypeEmptyBlock : ClassModifierContainerBlock
	{
		public TypeEmptyBlock()
			: base()
		{
			PrepareItems();
			this.UserDeletable = true;
			this.MyListControl.Box.Margins.SetAll(1);
			this.MyListControl.Layout();
		}
		
		private void PrepareItems()
		{
			ICompletionListBuilder CustomItems = Completion.Items;
			AddItem<ClassBlock>("class", CustomItems);
			AddItem<StructBlock>("struct", CustomItems);
			AddItem<InterfaceBlock>("interface", CustomItems);
			AddItem<EnumBlock>("enum", CustomItems);
			AddItem<DelegateBlock>("delegate", CustomItems);
		}

		public override void FillItems(CustomItemsRequestEventArgs e)
		{
			base.FillItems(e);
			e.ShowOnlyCustomItems = false;
		}
		
		public void AddItem<T>(string s, ICompletionListBuilder itemsToAdd)
		{
			ReplaceTypeEmptyBlockItem item = new ReplaceTypeEmptyBlockItem(
				s, 
				BlockActivatorFactory.Types<T>(),
				this);
			item.Picture = Icons.CodeSnippet;
			itemsToAdd.Add(item);
		}

		public override System.Collections.Generic.IEnumerable<Block> GetBlocksToDelete()
		{
			yield return this;
			yield return this.Next;
		}

		public void SetInitialText(string text)
		{
			this.SetMany(text);
		}

		public override void SetDefaultFocus()
		{
			this.SetCursorToTheEnd();
		}
	}
}
