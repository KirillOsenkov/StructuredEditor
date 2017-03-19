using System.Collections.Generic;

namespace GuiLabs.Canvas.Controls
{
	public delegate bool CharFilter(char toTest);

	public static class CommonCharFilters
	{
		public static bool AcceptAll(char c)
		{
			return true;
		}

		public static bool AcceptNoWhitespace(char c)
		{
			return !char.IsWhiteSpace(c);
		}
	}

	public partial class TextBox
	{
		#region CharFilter

		private List<CharFilter> mCharFilters = new List<CharFilter>();
		public ICollection<CharFilter> CharFilters
		{
			get
			{
				return mCharFilters;
			}
		}

		/// <summary>
		/// Tests if all the filters in CharFilters list
		/// accept the char toTest 
		/// (which is, return true when passed the char as parameter).
		/// </summary>
		/// <param name="toTest">char to be tested</param>
		/// <returns>true if all filters returned true or no filters added. 
		/// false if there was at least one filter that returned false.</returns>
		public bool IsCharAcceptable(char toTest)
		{
			foreach (CharFilter filter in CharFilters)
			{
				bool result = filter(toTest);
				if (!result)
				{
					return false;
				}
			}
			return true;
		}

		#endregion
	}
}