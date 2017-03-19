using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierBlockCollection : List<ModifierSelectionBlock>
	{
		//public string GetModifierKind(string modifier)
		//{
		//    foreach (ModifierSelectionBlock block in this)
		//    {
		//        if (block.Contains(modifier))
		//        {
		//            return block.PossibleModifierList.Kind;
		//        }
		//    }
		//    return "";
		//}

		public ModifierSelectionBlock FindBlockForModifier(string modifier)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Contains(modifier))
				{
					return this[i];
				}
			}
			return null;
		}

		//public ModifierSelectionBlock FindBlockByModifierKind(string kind)
		//{
		//    foreach (ModifierSelectionBlock block in this)
		//    {
		//        if (block.PossibleModifierList != null &&
		//            block.PossibleModifierList.Kind == kind)
		//        {
		//            return block;
		//        }
		//    }
		//    return null;
		//}

		public bool IsValidModifier(string modifier)
		{
			foreach (ModifierSelectionBlock block in this)
			{
				if (block.Contains(modifier))
				{
					return true;
				}
			}
			return false;
		}
	}
}
