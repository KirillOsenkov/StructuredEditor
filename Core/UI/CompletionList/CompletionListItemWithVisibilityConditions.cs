using System.Collections.Generic;
using GuiLabs.Utils;

namespace GuiLabs.Editor.UI
{
	public class CompletionListItemWithVisibilityConditions : CompletionListItem
	{
		#region ctor

		public CompletionListItemWithVisibilityConditions(string text)
			: base(text)
		{

		}

		#endregion

		#region Visibility conditions

		private List<ICondition> mVisibilityConditions = new List<ICondition>();
		public List<ICondition> VisibilityConditions
		{
			get
			{
				return mVisibilityConditions;
			}
			set
			{
				mVisibilityConditions = value;
			}
		}

		public override bool ShouldShow(CompletionFunctionality hostItemList)
		{
			foreach (ICondition condition in VisibilityConditions)
			{
				if (!condition.IsTrue())
				{
					return false;
				}
			}
			return true;
		}

		#endregion
	}
}
