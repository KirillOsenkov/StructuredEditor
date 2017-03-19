using System;
using System.IO;

namespace GuiLabs.Utils
{
	public class Log
	{
		protected Log()
		{
		}

		private static Log mInstance = null;
		public static Log Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new Log();
				}
				return mInstance;
			}
		}

		private string mLogFileName = "GuiLabs.Utils.Log.txt";
		public string LogFileName
		{
			get
			{
				return mLogFileName;
			}
			set
			{
				mLogFileName = value;
			}
		}

		public void WriteWarning(string text)
		{
			WriteLine("Warning: " + text);
		}

		public void WriteLine(string text)
		{
			using (StreamWriter writer = new StreamWriter(LogFileName, true))
			{
				writer.WriteLine(text);
			}
		}

		public void LogNonCriticalException(Exception e, string additionalInfo)
		{
			WriteLine(
				"Exception: "
				+ e.ToString()
				+ Environment.NewLine
				+ Environment.NewLine
				+ (!string.IsNullOrEmpty(additionalInfo)
					? "Additional info: " + additionalInfo
					: ""));
		}

		public static void NonCriticalException(Exception e, string additionalInfo)
		{
			Instance.LogNonCriticalException(e, additionalInfo);
		}

		public static void Write(string text)
		{
			Instance.WriteLine(text);
		}
	}
}
