using GuiLabs.Editor.Blocks;
using GuiLabs.Canvas.Controls;
using GuiLabs.Editor.UI;
using System.Collections.Generic;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.CSharp
{
	public class ModifierSeparatorBlock : SeparatorBlock
	{
		#region ctor

		public ModifierSeparatorBlock()
			: base()
		{

		}

		#endregion

		#region OnEvents

		protected override void OnMouseUp(GuiLabs.Canvas.Events.MouseWithKeysEventArgs MouseInfo)
		{
			base.OnMouseDown(MouseInfo);
			if (MouseInfo.IsRightButtonPressed)
			{
				AppendNewToken("");
			}
		}

		protected override void ProcessBackspace(System.Windows.Forms.KeyEventArgs e)
		{
			Block prev = this.FindPrevFocusableBlockInChain();
			FieldBlock field = prev as FieldBlock;
			if (prev != null)
			{
				if (prev is ModifierSelectionBlock)
				{
					prev.Delete();
				}
				else if (field != null && field.IsEmptyField())
				{
					prev.Delete();
				}
				else
				{
					if (Container != null && Container.UserDeletable)
					{
						Container.Delete();
					}
					else
					{
						prev.SetFocus();
					}
				}
				e.Handled = true;
			}
		}

		protected override void ProcessDelete(System.Windows.Forms.KeyEventArgs e)
		{
			Block next = this.FindNextFocusableBlockInChain();
			if (next != null)
			{
				if (next is ModifierSelectionBlock)
				{
					next.Delete();
				}
				else
				{
					if (Container != null && Container.UserDeletable)
					{
						Container.Delete();
					}
					else
					{
						next.SetFocus();
					}
				}
			}
			e.Handled = true;
		}

		protected override void ProcessTab(System.Windows.Forms.KeyEventArgs e)
		{
			Block next = this.FindNextFocusableBlockInChain();
			if (next != null)
			{
				next.SetCursorToTheBeginning();
			}
			e.Handled = true;
		}

		#endregion

		protected ModifierContainer Container
		{
			get
			{
				return this.Parent as ModifierContainer;
			}
		}

		protected override void AppendNewToken(string prefix)
		{
			if (!Container.Completion.CanShow())
			{
				return;
			}

			using (Redrawer a = new Redrawer(this.Root))
			{
				TemporaryCompletionBlock empty = new TemporaryCompletionBlock(Container.Completion);
				empty.MyControl.Box.Padding.Right = ShapeStyle.DefaultFontSize;

				this.Parent.Children.Append(this, empty);

				empty.Text = prefix;
				empty.SetCursorToTheEnd();
				empty.Completion.ShowCompletionList(empty, prefix);
			}
		}

		#region Style

		protected override string StyleName()
		{
			return "ModifierSeparatorBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"This is a space to insert modifiers. Start typing or right-click it for a completion list.",
			"Press [Home] to select the parent block."
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}

				ModifierSelectionBlock prev = this.FindPrevFocusableBlock() as ModifierSelectionBlock;
				if (prev != null)
				{
					yield return string.Format("Press [Backspace] to delete the word '{0}'.", prev.Text);
				}

				ModifierSelectionBlock next = this.FindNextFocusableBlock() as ModifierSelectionBlock;
				if (next != null)
				{
					yield return string.Format("Press [Delete] to delete the word '{0}'.", next.Text);
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
	}
}
