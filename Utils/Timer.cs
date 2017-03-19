// Copyright (C) Kirill Osenkov, www.osenkov.com
// Feel free to use and reuse in any projects.
// Please do not remove this comment.

using System.Runtime.InteropServices;

namespace GuiLabs.Utils
{
	public class Timer
	{
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		long _Freq, _Start, _Finish;
		static double msFreq;

		/// <summary>
		/// The time since some fixed point in the past (in milliseconds).
		/// Usually used twice to measure deltas between two points in ms
		/// </summary>
		public static double ms
		{
			get
			{
				long ticksPerSecond;
				QueryPerformanceFrequency(out ticksPerSecond);
				double ticksPerMillisecond = ticksPerSecond / 1000;
				long elapsedTime;
				QueryPerformanceCounter(out elapsedTime);
				double result = elapsedTime / ticksPerMillisecond;
				return result;
			}
		}

		public static double s
		{
			get
			{
				long ticksPerSecond;
				QueryPerformanceFrequency(out ticksPerSecond);
				long elapsedTime;
				QueryPerformanceCounter(out elapsedTime);
				double result = elapsedTime / ticksPerSecond;
				return result;
			}
		}

		public static string ElapsedSince(double startTime)
		{
			return (ms - startTime).ToString();
		}

		public static string ElapsedSinceInSeconds(double startTime)
		{
			return (s - startTime).ToString();
		}

		public static void ShowElapsed(double startTime)
		{
			System.Windows.Forms.MessageBox.Show(ElapsedSince(startTime));
		}

		static Timer()
		{

		}

		public Timer()
		{
			QueryPerformanceFrequency(out _Freq);
			msFreq = _Freq / 1000;
		}

		public void Start()
		{
			QueryPerformanceCounter(out _Start);
		}

		public void Stop()
		{
			QueryPerformanceCounter(out _Finish);
		}

		long t;
		public double Milliseconds()
		{
			QueryPerformanceCounter(out t);
			return t / msFreq;
		}

		public double TimeElapsed
		{
			get
			{
				return (double)(_Finish - _Start) / _Freq;
			}
		}
	}
}
