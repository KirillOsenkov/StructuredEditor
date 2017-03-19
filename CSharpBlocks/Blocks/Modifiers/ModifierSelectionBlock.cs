using System.Collections.Generic;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierSelectionBlock : TextSelectionBlock
	{
		#region ctors

		public ModifierSelectionBlock(ModifierContainer container, IModifierList modifiers)
			: base()
		{
			PossibleModifierList = modifiers;
			Init(container);
		}

		public ModifierSelectionBlock(ModifierContainer container)
			: base()
		{
			PossibleModifierList = ModifierListFactory.Empty;
			Init(container);
		}

		private void Init(ModifierContainer container)
		{
			this.Hidden = true;
			this.ParentContainer = container;
		}

		#endregion

		#region Completion

		public override void FillItems(CustomItemsRequestEventArgs e)
		{
			AddItems(e.Items, PossibleModifierList);
		}

		protected override CompletionListItem CreateItem(string text)
		{
			SetModifierCompletionListItem item =
				new SetModifierCompletionListItem(
					this.ParentContainer,
					text,
					PossibleModifierList.GetVisibilityCondition(text));
			item.Picture = Icons.Keyword;
			return item;
		}

		private IModifierList mPossibleModifierList;
		private IModifierList PossibleModifierList
		{
			get
			{
				return mPossibleModifierList;
			}
			set
			{
				Param.CheckNotNull(value, "value");
				mPossibleModifierList = value;
			}
		}

		#endregion

		#region ParentContainer

		private ModifierContainer mParentContainer;
		public ModifierContainer ParentContainer
		{
			get
			{
				return mParentContainer;
			}
			set
			{
				mParentContainer = value;
			}
		}

		#endregion

		#region OnEvents

		protected override void OnTextChanging(string oldText, string newText)
		{
			UpdateVisible(newText);
		}

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(sender, e);
			if (e.Handled)
				return;
			
			if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				Delete();
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Menu)
			{
				ShouldShowCompletion();
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (!char.IsLetterOrDigit(e.KeyChar))
			{
				return;
			}

			using (Redrawer r = new Redrawer(this.Root))
			{
				ModifierSeparatorBlock prev = this.Prev as ModifierSeparatorBlock;
				this.Delete();
				if (prev != null)
				{
					prev.MyControl.OnKeyPress(e);
				}
			}
		}

		#endregion

		#region API

		public void Set(string modifier)
		{
			this.Text = modifier;
		}

		public bool IsSet(string modifier)
		{
			return this.Text == modifier;
		}

		public bool IsSet()
		{
			return !string.IsNullOrEmpty(this.Text);
		}

		public void Clear()
		{
			this.Text = "";
		}
		
		public virtual bool Contains(string modifier)
		{
			if (PossibleModifierList != null)
			{
				return PossibleModifierList.Contains(modifier);
			}
			return false;
		}

		protected override void SetDefaultText()
		{

		}

		#endregion

		#region Delete

		public override void Delete()
		{
			if (this.Text != null)
			{
				ParentContainer.ClearModifier(this.Text);
			}
		}

		#endregion

		#region Visible

		/// <summary>
		/// Shows or hides this ModifierSelectionBlock based on
		/// if the new text is valid for it.
		/// </summary>
		/// <param name="newText"></param>
		protected virtual void UpdateVisible(string newText)
		{
			bool shouldBeHidden = ShouldBeHidden(newText);
			if (shouldBeHidden != this.Hidden)
			{
				this.Hidden = shouldBeHidden;
				if (this.Next is ModifierSeparatorBlock)
				{
					this.Next.Hidden = shouldBeHidden;
				}
				this.Parent.CheckVisibility();
				if (shouldBeHidden)
				{
					this.RemoveFocus(MoveFocusDirection.SelectPrev);
				}
			}
			if (!shouldBeHidden)
			{
				this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
			}
		}

		protected virtual bool ShouldBeHidden(string newText)
		{
			return !PossibleModifierList.Contains(newText);
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "ModifierSelectionBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"To change it, just right-click, or press [Enter] or just start typing over it.",
			"Press [Delete] or [Backspace] to delete it."
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				yield return Description;
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
		protected override string Description
		{
			get
			{
				return "This is a modifier.";
			}
		}

		#endregion
	}
}
