using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace GuiLabs.Utils
{
	public struct Pair<T>
	{
		public Pair(T first, T second)
		{
			First = first;
			Second = second;
		}

		public T First;
		public T Second;
	}

	public static class Common
	{
		public static bool BitSet<T>(T enumValue, T bitValue)
		{
			int val = Convert.ToInt32(enumValue);
			int bit = Convert.ToInt32(bitValue);
			return (val & bit) == bit;
		}

		public static void Swap<T>(ref T Value1, ref T Value2)
		{
			T Temp = Value1;
			Value1 = Value2;
			Value2 = Temp;
		}

		public static T Max<T>(T Value1, T Value2)
			where T : IComparable
		{
			if (Value1.CompareTo(Value2) < 0)
			{
				return Value2;
			}
			else
			{
				return Value1;
			}
		}

		public static T Min<T>(T Value1, T Value2)
			where T : IComparable
		{
			if (Value1.CompareTo(Value2) < 0)
			{
				return Value1;
			}
			else
			{
				return Value2;
			}
		}

		public static void EnsureGreater<T>(ref T Value, T ComparedWith)
			where T : IComparable
		{
			if (Value.CompareTo(ComparedWith) < 0)
			{
				Value = ComparedWith;
			}
		}

		public static void SwapIfGreater<T>(ref T LValue, ref T RValue)
			where T : IComparable
		{
			if (LValue.CompareTo(RValue) > 0)
			{
				T temp = LValue;
				LValue = RValue;
				RValue = temp;
			}
		}

        public static string DumpEnvironmentPaths()
        {
            var paths = Enum.GetValues(typeof(Environment.SpecialFolder))
                .Cast<Environment.SpecialFolder>()
                .Select(folder => folder + " = " + Environment.GetFolderPath(folder))
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);
            return paths;
        }

		public static T GetFirstItem<T>(IEnumerable<T> list)
		{
			T result = default(T);

			if (list != null)
			{
				IEnumerator<T> enumerator = list.GetEnumerator();
				if (enumerator != null && enumerator.MoveNext())
				{
					result = enumerator.Current;
				}
			}

			return result;
		}

		public static T Head<T>(IEnumerable<T> list)
		{
			return GetFirstItem(list);
		}

		public static bool Empty<T>(IEnumerable<T> list)
		{
			return GetFirstItem(list) == null;
		}

		public static IEnumerable<T> Tail<T>(IEnumerable<T> list)
		{
			if (list != null)
			{
				IEnumerator<T> enumerator = list.GetEnumerator();
				if (enumerator != null && enumerator.MoveNext())
				{
					while (enumerator.MoveNext())
					{
						yield return enumerator.Current;
					}
				}
			}
		}

        public static double MeasureExecutionTime(Action code)
        {
            long startTime = Stopwatch.GetTimestamp();
            code();
            long stopTime = Stopwatch.GetTimestamp();
            return (stopTime - startTime) / (double)Stopwatch.Frequency;
        }

		public static bool Contains<T>(IEnumerable<T> list, T value)
			where T : IEquatable<T>
		{
			foreach (T item in list)
			{
				if (item.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		public static IEnumerable<T> Filter<T>(ArrayList arrayList)
			where T : class
		{
			foreach (object o in arrayList)
			{
				T found = o as T;
				if (found != null)
				{
					yield return found;
				}
			}
		}

		#region File dialogs

		public static string FilterAllFiles = "All files (*.*) |*.*";

		public static string GetSaveFileName()
		{
			return GetSaveFileName(FilterAllFiles);
		}

		public static string GetOpenFileName()
		{
			return GetOpenFileName(FilterAllFiles);
		}

		public static string GetSaveFileName(string filter)
		{
			if (string.IsNullOrEmpty(filter))
			{
				filter = FilterAllFiles;
			}
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.InitialDirectory = Application.StartupPath;
			dialog.Filter = filter;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				return dialog.FileName;
			}
			return string.Empty;
		}

		public static string GetOpenFileName(string filter)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = filter;
			dialog.InitialDirectory = Application.StartupPath;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				return dialog.FileName;
			}
			return string.Empty;
		}

		#endregion
	}

	public static class Param
	{
		public static void CheckNotNull(object param)
		{
			if (param == null)
			{
				throw new ArgumentNullException();
			}
		}

		public static void CheckNotNull(object param, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		public static void CheckNonEmptyString(string param)
		{
			if (string.IsNullOrEmpty(param))
			{
				throw new ArgumentException("String parameter cannot be null.");
			}
		}

		public static void CheckNonEmptyString(string param, string paramName)
		{
			if (string.IsNullOrEmpty(param))
			{
				throw new ArgumentException(string.Format("String parameter '{0}' cannot be null.", paramName));
			}
		}
	}
}
