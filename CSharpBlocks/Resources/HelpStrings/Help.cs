using System.Text;
namespace GuiLabs.Editor.CSharp
{
	public class Help
	{
		#region Empty

		public static string ThisIsEmptyNamespaceBlock =
			"This is an empty space at the namespace level.";

		#endregion

		#region Keyboard navigation

		public static string PressEnterToInsertBefore =
			"Press [Enter] to insert a line before current block.";

		public static string PressCtrlEnterToInsertAfter =
			"Press [Ctrl+Enter] or [Insert] to insert a line after current block.";

		public static string PressTabOrRightArrowToEditName =
			"Press [Tab] or [RightArrow] or just start typing to edit the {0} block.";

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

		#endregion

		#region Completion

		public static string StartTypingForCompletion =
			"Start typing to popup the completion list.";

		public static string PopupCompletion =
			@"To popup, you can also right-click to the right of the caret, or press [Enter], [Tab] or [ContextMenu].";

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
	}
}
