using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using GuiLabs.Undo;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierContainer : HContainerBlock, IHasModifiers
	{
		#region ctor

		public ModifierContainer()
		{
			Completion = new CompletionFunctionality();
			this.Add(new ModifierSeparatorBlock());
		}

		#endregion

		#region Add modifier kinds

		public void AddModifierKind(IModifierList list)
		{
			AddModifierBlock(list);
			AddModifierSeparator();
			// AddCompletionItems(list);
		}

		protected virtual void AddModifierBlock(IModifierList list)
		{
			AddModifierBlock(CreateModifierSelectionBlock(list));
		}

		protected virtual void AddModifierBlock(ModifierSelectionBlock modifierBlock)
		{
			ModifierBlocks.Add(modifierBlock);
			this.Add(modifierBlock);
		}
		
		protected virtual void RemoveModifierBlock(ModifierSelectionBlock modifierBlock)
		{
			ModifierBlocks.Remove(modifierBlock);
			modifierBlock.Delete();
		}

		protected virtual ModifierSelectionBlock CreateModifierSelectionBlock(IModifierList list)
		{
			return new ModifierSelectionBlock(this, list);
		}

		protected virtual void AddModifierSeparator()
		{
			ModifierSeparatorBlock sep = new ModifierSeparatorBlock();
			sep.Hidden = true;
			this.Add(sep);
		}

		public void AddModifierKind(string kind)
		{
			IModifierList list = ModifierListFactory.Get(kind);
			AddModifierKind(list);
		}

		public void AddModifierKinds(params string[] kinds)
		{
			foreach (string k in kinds)
			{
				AddModifierKind(k);
			}
		}

		#endregion

		#region API

		#region Set

		public virtual void Set(string modifier)
		{
			if (string.IsNullOrEmpty(modifier))
			{
				return;
			}
			// Param.CheckNonEmptyString(modifier, "modifier");

			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifier);

			if (block == null || block.IsSet(modifier))
			{
				return;
			}

			if (this.Root != null)
			{
				IAction action = new SetModifierAction(this, modifier);
				Root.ActionManager.RecordAction(action);
			}
			else
			{
				SetCore(modifier);
			}
		}

		public void SetCore(string modifier)
		{
			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifier);

			if (block == null || block.IsSet(modifier))
			{
				return;
			}

			block.Set(modifier);
		}

		public void SetMany(string modifierList)
		{
			if (string.IsNullOrEmpty(modifierList))
			{
				return;
			}

			List<string> modifiersToSet = new List<string>();
			bool protectedFound = false;
			bool internalFound = false;
			bool sealedFound = false;
			bool overrideFound = false;

			string[] modifiers = modifierList.Split(' ');
			foreach (string modifier in modifiers)
			{
				if (this.ModifierBlocks.IsValidModifier(modifier))
				{
					modifiersToSet.Add(modifier);
				}

				// special case protected internal -> make one modifier out of two
				if (modifier == Keywords.Protected)
				{
					protectedFound = true;
				}
				else if (modifier == Keywords.Internal)
				{
					internalFound = true;
				}
				// special case sealed override -> make one modifier out of two
				else if (modifier == Keywords.Sealed)
				{
					sealedFound = true;
				}
				else if (modifier == Keywords.Override)
				{
					overrideFound = true;
				}
			}

			if (protectedFound && internalFound)
			{
				modifiersToSet.Remove(Keywords.Protected);
				modifiersToSet.Remove(Keywords.Internal);
				if (this.ModifierBlocks.IsValidModifier(Keywords.ProtectedInternal))
				{
					modifiersToSet.Add(Keywords.ProtectedInternal);
				}
			}

			if (sealedFound && overrideFound)
			{
				modifiersToSet.Remove(Keywords.Sealed);
				modifiersToSet.Remove(Keywords.Override);
				if (this.ModifierBlocks.IsValidModifier(Keywords.SealedOverride))
				{
					modifiersToSet.Add(Keywords.SealedOverride);
				}
			}

			SetMany(modifiersToSet);
		}

		public void SetMany(IEnumerable<string> modifierList)
		{
			if (this.Root != null)
			{
				using (Transaction.Create(this.Root.ActionManager))
				{
					foreach (string m in modifierList)
					{
						Set(m);
					}
				}
			}
			else
			{
				foreach (string m in modifierList)
				{
					Set(m);
				}
			}
		}

		#endregion

		#region Clear

		public void ClearModifier(string modifier)
		{
			Param.CheckNonEmptyString(modifier, "modifier");

			if (!this.IsSet(modifier))
			{
				return;
			}

			if (ActionManager != null)
			{
				ActionManager.RecordAction(new ClearModifierAction(this, modifier));
			}
			else
			{
				ClearModifierCore(modifier);
			}
		}

		public void ClearModifierCore(string modifier)
		{
			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifier);
			if (block != null)
			{
				block.Clear();
			}
		}

		public void ClearModifierGroup(string modifierKind)
		{
			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifierKind);
			if (block != null && block.Visible)
			{
				ClearModifier(block.Text);
			}
		}

		#endregion

		#region Query

		public bool IsSet(string modifier)
		{
			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifier);
			if (block != null)
			{
				return block.IsSet(modifier);
			}
			return false;
		}

		//public bool IsModifierKindSet(string modifierKind)
		//{
		//    ModifierSelectionBlock block = ModifierBlocks.FindBlockByModifierKind(modifierKind);
		//    if (block != null)
		//    {
		//        return block.Visible;
		//    }
		//    return false;
		//}

		public bool IsModifierGroupSet(string modifier)
		{
			ModifierSelectionBlock block = ModifierBlocks.FindBlockForModifier(modifier);
			if (block != null)
			{
				return block.Visible;
			}
			return false;
		}

		public virtual string GetModifierString()
		{
			return GetModifierString(this.ModifierBlocks);
		}

		public string GetModifierString(IEnumerable<ModifierSelectionBlock> modifierBlocks)
		{
			StringBuilder sb = new StringBuilder();
			bool wasSomeoneAlreadyAdded = false;

			foreach (ModifierSelectionBlock modBlock in modifierBlocks)
			{
				if (modBlock.Visible)
				{
					if (wasSomeoneAlreadyAdded)
					{
						sb.Append(" ");
					}
					sb.Append(modBlock.Text);
					wasSomeoneAlreadyAdded = true;
				}
			}

			return sb.ToString();
		}

		#endregion

		#endregion

		#region ModifierBlocks

		private ModifierBlockCollection mModifierBlocks = new ModifierBlockCollection();
		public ModifierBlockCollection ModifierBlocks
		{
			get
			{
				return mModifierBlocks;
			}
			set
			{
				mModifierBlocks = value;
			}
		}

		#endregion

		#region Completion

		private CompletionFunctionality mCompletion;
		public CompletionFunctionality Completion
		{
			get
			{
				return mCompletion;
			}
			set
			{
				if (mCompletion != null)
				{
					mCompletion.CustomItemsRequested -= FillItems;
				}
				mCompletion = value;
				if (mCompletion != null)
				{
					mCompletion.CustomItemsRequested += FillItems;
				}
			}
		}
		
		public virtual void FillItems(CustomItemsRequestEventArgs e)
		{
			foreach (ModifierSelectionBlock block in ModifierBlocks)
			{
				block.FillItems(e);
			}
		}
		
		//public virtual void AddCompletionItems(IModifierList list, ICompletionListBuilder results)
		//{
		//    foreach (string modifier in list)
		//    {
		//        results.Add(CreateItem(list, modifier));
		//    }
		//}

		//protected CompletionListItem CreateItem(IModifierList list, string modifier)
		//{
		//    SetModifierCompletionListItem item = new SetModifierCompletionListItem(
		//        this, modifier);
		//    item.VisibilityCondition = list.GetVisibilityCondition(modifier);
		//    item.Picture = Icons.Keyword;
		//    return item;
		//}

		#endregion

		#region ToString

		public override string ToString()
		{
			return GetModifierString();
		}

		#endregion

		ModifierContainer IHasModifiers.Modifiers
		{
			get
			{
				return this;
			}
		}
	}
}
