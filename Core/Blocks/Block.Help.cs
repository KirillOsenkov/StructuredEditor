using System.Collections.Generic;
using System.Text;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.Blocks
{
	public partial class Block
	{
		public void DisplayContextHelp()
		{
			if (this.Root != null)
			{
				Root.RaiseShouldDisplayContextHelp(this);
			}
		}

		#region Help

		private static string[] mHelpStrings = new string[] 
		{
		};
		public virtual IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string s in HelpBase.PressCtrlDownOrUp(this))
				{
					yield return s;
				}
				foreach (string s2 in RaiseProvideHelpStrings())
				{
					yield return s2;
				}
			}
		}
		protected virtual string Description
		{
			get
			{
				return "";
			}
		}

		public event StringsProvider ProvideHelpStrings;
		protected virtual IEnumerable<string> RaiseProvideHelpStrings()
		{
			if (ProvideHelpStrings != null)
			{
				return ProvideHelpStrings();
			}
			return mHelpStrings;
		}

		#endregion
	}

	public class HelpBase
	{
		#region Empty

		public static string ThisIsEmptyNamespaceBlock =
			"This is an empty space at the namespace level.";

		#endregion

		#region Keyboard navigation

		public static string PressRightToSelect(string whatToSelect)
		{
			return PressKeyToSelect(whatToSelect, "RightArrow");
		}

		public static string PressLeftToSelect(string whatToSelect)
		{
			return PressKeyToSelect(whatToSelect, "LeftArrow");
		}

		public static string PressUpToSelect(string whatToSelect)
		{
			return PressKeyToSelect(whatToSelect, "UpArrow");
		}

		public static string PressDownToSelect(string whatToSelect)
		{
			return PressKeyToSelect(whatToSelect, "DownArrow");
		}

		public static string PressKeyToSelect(string whatToSelect, params string[] alternativeKeys)
		{
			return string.Format(
				"Press {0} to select {1}.",
				GetKeyList(alternativeKeys),
				whatToSelect);
		}

		public static string GetKeyList(params string[] keyCombinations)
		{
			StringBuilder sb = new StringBuilder();

			if (keyCombinations.Length > 0)
			{
				sb.Append("[");
				sb.Append(keyCombinations[0]);
				sb.Append("]");
				if (keyCombinations.Length > 1)
				{
					if (keyCombinations.Length > 2)
					{
						for (int i = 1; i < keyCombinations.Length - 1; i++)
						{
							sb.Append(", ");
							sb.Append("[");
							sb.Append(keyCombinations[i]);
							sb.Append("]");
						}
					}
					sb.Append(" or ");
					sb.Append("[");
					sb.Append(keyCombinations[keyCombinations.Length - 1]);
					sb.Append("]");
				}
			}

			return sb.ToString();
		}

		public static string SelectFirstChild(UniversalBlock block)
		{
			Block firstChild = block.FindFirstFocusableChild();
			string keyForChild = "DownArrow";

			if (block.MyUniversalControl.LinearLayoutStrategy.Orientation == GuiLabs.Canvas.Controls.OrientationType.Horizontal)
			{
				keyForChild = "RightArrow";
			}
			else
			{
				firstChild = block.VMembers.FindFirstFocusableBlock();
			}

			if (firstChild != null)
			{
				return PressKeyToSelect("the first child block", keyForChild);
			}
			else
			{
				return PressKeyToSelect("the next block", keyForChild);
			}
		}

		public static string PressPageUp =
			"Press [PageUp] to move to the previous block.";

		public static string PressPageDown =
			"Press [PageDown] to move to the next block.";

		public static string PressCtrlUp =
			"Press [Ctrl+UpArrow] to move this block up.";

		public static string PressCtrlDown =
			"Press [Ctrl+DownArrow] to move this block down.";

		public static IEnumerable<string> PressPageDownAndOrUp(Block current)
		{
			if (current.FindNextFocusableSibling() != null)
			{
				yield return HelpBase.PressPageDown;
			}
			if (current.FindPrevFocusableSibling() != null)
			{
				yield return HelpBase.PressPageUp;
			}
		}

		public static IEnumerable<string> PressCtrlDownOrUp(Block current)
		{
			if (current.CanMoveDown)
			{
				yield return HelpBase.PressCtrlDown;
			}
			if (current.CanMoveUp)
			{
				yield return HelpBase.PressCtrlUp;
			}
		}

		public static string PressHome =
			"Press [Home] to move to the beginning of the line.";

		public static string PressHomeToSelectParent =
			"Press [Home] to select the parent block.";

		public static string PressEnd =
			"Press [End] to move to the end of the line.";

		public static string PressCtrlEnterToJumpOut =
			"Press [Ctrl+Enter] to jump out of the current block.";

		#endregion

		#region Completion

		public static string StartTypingForCompletion =
			"Start typing to popup the completion list.";

		public static string PopupCompletion =
			@"To popup, you can also right-click to the right of the caret, or press [Enter], [Tab] or [ContextMenu].";

		public static string CommitCompletion =
			@"Press [Space], [Enter] or [Tab] to commit completion.";

		public static string CancelCompletion =
			@"Press [Escape] to hide the completion list.";

		#endregion

		public static string SpaceCollapse(bool isCollapsed)
		{
			if (isCollapsed)
			{
				return "Press [Space] to expand the block.";
			}
			else
			{
				return "Press [Space] to collapse the block.";
			}
		}

		public static string Delete = "Press [Delete] to delete current block.";
	}

	public delegate IEnumerable<string> StringsProvider();
}
