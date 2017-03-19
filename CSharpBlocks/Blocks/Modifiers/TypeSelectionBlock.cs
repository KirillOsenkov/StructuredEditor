using System.Collections.Generic;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;
using GuiLabs.Utils.Collections;

namespace GuiLabs.Editor.CSharp
{
	public class TypeSelectionBlock : ModifierSelectionBlock
	{
		#region ctor

		public TypeSelectionBlock(ModifierContainer parent)
			: base(parent)
		{

		}

		#endregion

		public override void FillItems(CustomItemsRequestEventArgs e)
		{
			LanguageService ls = LanguageService.Get(this);
			ClassOrStructBlock parent = ClassNavigator.FindContainingClassOrStruct(this);

			if (ls != null && parent != null)
			{
				ListSet<TextPictureInfo> result = new ListSet<TextPictureInfo>();
				AddItems(e.Items, ItemStrings);

				ls.FillTypeList(parent, result);
				foreach (TextPictureInfo s in result)
				{
					CompletionListItem item = CreateItem(s.Text);
					if (item.ShouldShow(this.Completion))
					{
						item.Picture = s.Picture;
						e.Items.Add(item);
					}
				}
			}
		}

		protected override bool ShouldBeHidden(string newText)
		{
			return string.IsNullOrEmpty(newText);
		}

		public override bool Contains(string modifier)
		{
			return true;
		}

		#region Style

		protected override string StyleName()
		{
			return "TypeSelection";
		}

		#endregion

		#region Help

		protected override string Description
		{
			get
			{
				return "This is a return type or type name.";
			}
		}

		#endregion
	}
}
