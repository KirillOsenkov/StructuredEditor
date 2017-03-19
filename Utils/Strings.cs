using System;
using System.Text;

namespace GuiLabs.Utils
{
	public static class Strings
	{
		public static string[] EmptyArray = new string[] { };

		public static bool IsAlphaNumeric(char c)
		{
			return char.IsLetterOrDigit(c);
		}

		public delegate bool CompareStringsDelegate(string s1, string s2);

		public static int WordsOverlapLength(string word1, string word2, CompareStringsDelegate comparer)
		{
			int length1 = word1.Length;
			int length2 = word2.Length;

			int maxOverlap = System.Math.Min(length1, length2);

			for (int i = maxOverlap; i > 0; i--)
			{
				string suffixOf1 = word1.Substring(length1 - i, i);
				string prefixOf2 = word2.Substring(0, i);

				if (comparer(suffixOf1, prefixOf2))
				{
					return i;
				}
			}

			return 0;
		}

		public static string GetLastWord(string text)
		{
			return GetLastWord(text, CanBePartOfVariableName);
		}

		public static bool CanBePartOfVariableName(char c)
		{
			return char.IsLetterOrDigit(c)
				|| c == '_';
		}

		public static bool CanBeStartOfVariableName(char c)
		{
			return char.IsLetter(c)
				|| c == '_';
		}

		public static string GetLastWord(string text, Predicate<char> allowedPrefixChar)
		{
			int length = text.Length;
			for (int i = length - 1; i >= 0; i--)
			{
				if (!allowedPrefixChar(text[i]))
				{
					if (i < length - 1)
					{
						return text.Substring(i + 1);
					}
					else
					{
						return "";
					}
				}
			}

			return text;
		}

		public static int WordsOverlapLength(string word1, string word2)
		{
			return WordsOverlapLength(word1, word2, EqualIgnoreCase);
		}

		public static bool EqualIgnoreCase(string s1, string s2)
		{
			return s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool Equal(string s1, string s2)
		{
			return s1.Equals(s2, StringComparison.InvariantCulture);
		}

		public static string CutRightChars(string s, int numberOfCharsToCutFromTheRight)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= numberOfCharsToCutFromTheRight)
			{
				return "";
			}
			return s.Substring(0, s.Length - numberOfCharsToCutFromTheRight);
		}

		public static string CutLeftChars(string s, int numberOfCharsToCutFromTheLeft)
		{
			if (string.IsNullOrEmpty(s) || s.Length <= numberOfCharsToCutFromTheLeft)
			{
				return "";
			}
			return s.Substring(numberOfCharsToCutFromTheLeft);
		}

		public static string Left(string s, int numberOfCharsToTake)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			if (numberOfCharsToTake > s.Length)
			{
				return s;
			}
			return s.Substring(0, numberOfCharsToTake);
		}

		public static string Right(string s, int numberOfCharsToTake)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			if (numberOfCharsToTake > s.Length)
			{
				return s;
			}
			return s.Substring(s.Length - numberOfCharsToTake);
		}

		/// <summary>
		/// Determines if the first string is the beginning of the second string
		/// </summary>
		/// <param name="s1">prefix</param>
		/// <param name="s2">whole string</param>
		/// <returns>true if s1 is prefix of s2</returns>
		public static bool IsPrefix(string prefixToTest, string wholeString)
		{
			if (prefixToTest.Length > wholeString.Length)
			{
				return false;
			}

			return EqualIgnoreCase(
				prefixToTest,
				wholeString.Substring(
					0,
					prefixToTest.Length
				)
			);
		}

		#region RandomStrings

		public static string GetRandomString(int len)
		{
			StringBuilder result = new StringBuilder(len);
			for (int i = 0; i < len; i++)
			{
				result.Append(GetRandomChar());
			}
			return result.ToString();
		}

		public static Random Rnd = new Random();

		private static char[] mChars;
		public static char[] Chars
		{
			get
			{
				if (mChars == null)
				{
					mChars = new char[62];
					for (int i = 0; i < 26; i++)
					{
						mChars[i] = Convert.ToChar(Convert.ToInt16('a') + i);
						mChars[i + 26] = Convert.ToChar(Convert.ToInt16('A') + i);
					}
					for (int i = 0; i < 10; i++)
					{
						mChars[i + 52] = Convert.ToChar(Convert.ToInt16('0') + i);
					}
				}
				return mChars;
			}
		}

		public static char GetRandomChar()
		{
			return Chars[Rnd.Next(62)];
		}

		#endregion
	}
}
