using GuiLabs.Editor.Blocks;
using System.Collections.Generic;

namespace GuiLabs.Editor.CSharp
{
	public class NameBlock : TextBoxBlock
	{
		#region ctors

		public NameBlock()
			: base()
		{
			Init();
		}

		public NameBlock(int minWidth)
			: base(minWidth)
		{
		}

		public NameBlock(string defaultText)
			: base(defaultText)
		{
			Init();
		}

		private void Init()
		{
			MyTextBox.MinWidth = 16;
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDownBack(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.MyTextBox.CaretIsAtBeginning)
			{
				Block prev = this.FindPrevFocusableBlockInChain();
				if (prev != null)
				{
					prev.SetFocus();
					e.Handled = true;
					return;
				}
			}
			base.OnKeyDownBack(e);
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{
			"This is the block name."
		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				yield return "Press [Enter] to go to the block's children.";
				if (!this.MyTextBox.CaretIsAtBeginning)
				{
					yield return HelpBase.PressHome;
				}
				else
				{
					yield return "Press [Home] to select the block itself.";

				}
				if (!this.MyTextBox.CaretIsAtEnd)
				{
					yield return HelpBase.PressEnd;
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
