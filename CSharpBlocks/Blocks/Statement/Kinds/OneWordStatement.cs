using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	public abstract class OneWordStatement : FocusableLabelBlock
	{
		#region ctor

		public OneWordStatement(string keyword) : base(keyword)
		{
			this.MyControl.Box.Margins.SetTopAndBottom(2);
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				if (!e.Control)
				{
					this.PrependBlocks(new StatementLine());
					e.Handled = true;
				}
				else
				{
					if (this.ParentParent is IControlStructure)
					{
						TextBoxBlock next = this.ParentParent.Next as TextBoxBlock;
						if (next != null)
						{
							next.SetFocus();
						}
						else
						{
							this.ParentParent.AppendBlocks(new StatementLine());
						}
					}
					else
					{
						this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
					}
				}
			}
			// Don't need this because can't have any statements 
			// immediately after break or continue
			//if (e.KeyCode == System.Windows.Forms.Keys.Insert
			//    || (e.KeyCode == System.Windows.Forms.Keys.Return && e.Control))
			//{
			//    this.AppendBlocks(new StatementLine());
			//    e.Handled = true;
			//}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		protected override void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (Strings.CanBeStartOfVariableName(e.KeyChar))
			{
				StatementLine s = new StatementLine();
				this.Replace(s);
				s.MyTextBox.OnKeyPress(e);
			}
		}

		#endregion

		#region Delete

		public override void Delete()
		{
			if (this.Prev == null && this.Next == null)
			{
				this.Replace(new StatementLine());
			}
			else
			{
				base.Delete();
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "OneWordStatement";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			HelpBase.StartTypingForCompletion,
			HelpBase.Delete,
			HelpBase.PressHomeToSelectParent,
			Help.PressEnterToInsertBefore,
			HelpBase.PressCtrlEnterToJumpOut
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
	}
}
