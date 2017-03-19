using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GuiLabs.Utils
{
	[CLSCompliant(false)]
	public static class API
	{
		#region GDI

		#region Bitmaps

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern bool BitBlt(IntPtr hDestDC, int X, int Y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int StretchBlt(IntPtr hDC, int X, int Y, int nWidth, int nHeight, int hSrcDC, int xSrc, int ySrc, int nSrcWidth, int nSrcHeight, int dwRop);

		[DllImport("msimg32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern bool TransparentBlt(
			IntPtr hDestDC, 
			int X, 
			int Y, 
			int nWidth, 
			int nHeight, 
			IntPtr hSrcDC, 
			int xSrc, 
			int ySrc,
			int nWidthSrc,
			int nHeightSrc,
			int nTransparentColor);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

		#endregion 

		#region DC

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
		
		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int DeleteDC(IntPtr hDC);
		
		#endregion

		#region Objects

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int DeleteObject(IntPtr hObject);

		public const int WHITE_BRUSH = 0;
		public const int LTGRAY_BRUSH = 1;
		public const int GRAY_BRUSH = 2;
		public const int DKGRAY_BRUSH = 3;
		public const int BLACK_BRUSH = 4;
		public const int NULL_BRUSH = 5;
		public const int HOLLOW_BRUSH = NULL_BRUSH;
		public const int WHITE_PEN = 6;
		public const int BLACK_PEN = 7;
		public const int NULL_PEN = 8;
		public const int OEM_FIXED_FONT = 10;
		public const int ANSI_FIXED_FONT = 11;
		public const int ANSI_VAR_FONT = 12;
		public const int SYSTEM_FONT = 13;
		public const int DEVICE_DEFAULT_FONT = 14;
		public const int DEFAULT_PALETTE = 15;
		public const int SYSTEM_FIXED_FONT = 16;
		public const int DEFAULT_GUI_FONT = 17;
		public const int DC_BRUSH = 18;
		public const int DC_PEN = 19;

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr GetStockObject(int hObject);

		[DllImport("gdi32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, int fdwItalic, int fdwUnderline, int fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr CreatePen(PenStyle nPenStyle, int nWidth, int crColor);

		public enum PenStyle : int
		{
			PS_SOLID = 0, //The pen is solid. 
			PS_DASH = 1, //The pen is dashed. 
			PS_DOT = 2, //The pen is dotted. 
			PS_DASHDOT = 3, //The pen has alternating dashes and dots. 
			PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots. 
			PS_NULL = 5, //The pen is invisible. 
			PS_INSIDEFRAME = 6
		};

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr CreateSolidBrush(int crColor);

		#endregion

		#region GDI Drawing

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int SetPixelV(IntPtr hDC, int X, int Y, int crColor);

		[DllImport("gdi32.dll")]
		public static extern int MoveToEx(IntPtr hDC, int X, int Y, ref POINT lpPoint);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int LineTo(IntPtr hDC, int X, int Y);

		#region Rectangle

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int DrawFocusRect(IntPtr hDC, ref RECT lpRect);

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int FillRect(IntPtr hDC, ref RECT lpRect, IntPtr hBrush);

		public static void FillRectangle(IntPtr DC, Color Col, Rectangle r)
		{
			IntPtr hBrush = API.CreateSolidBrush(ColorTranslator.ToWin32(Col));
			RECT rect1 = ToRECT(r);
			FillRect(DC, ref rect1, hBrush);
			DeleteObject(hBrush);
		}

		public static void FillRectangle(IntPtr dc, Color col, RECT rectStruct)
		{
			IntPtr hBrush = API.CreateSolidBrush(ColorTranslator.ToWin32(col));
			FillRect(dc, ref rectStruct, hBrush);
			DeleteObject(hBrush);
		}

		//public static void FillRectangle(IntPtr DC, int Col, Rect r)
		//{
		//    IntPtr hBrush = API.CreateSolidBrush(Col);
		//    RECT rect1 = ToRECT(r);
		//    FillRect(DC, ref rect1, hBrush);
		//    DeleteObject(hBrush);
		//}

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int Rectangle(IntPtr hDC, int X1, int Y1, int X2, int Y2);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int RoundRect(IntPtr hDC, int X1, int Y1, int X2, int Y2, int X3, int Y3);

		#endregion

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int Ellipse(IntPtr hDC, int X1, int Y1, int X2, int Y2);

		[DllImport("gdi32.dll")]
		public static extern bool Pie(
			IntPtr hDC, 
			int nLeftRect, 
			int nTopRect, 
			int nRightRect,
			int nBottomRect, 
			int nXRadial1, 
			int nYRadial1, 
			int nXRadial2, 
			int nYRadial2);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int Polygon(IntPtr hDC, POINT[] lpPoint, int nCount);
	

		#region Surface properties

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int SetBkColor(IntPtr hDC, int crColor);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int SetBkMode(IntPtr hDC, int nBkMode);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int SetROP2(IntPtr hDC, System.Int32 nDrawMode);

		#endregion

		#region Text

		[DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
		public static extern int DrawText(IntPtr hDC, string lpStr, int nCount, ref RECT lpRect, int wFormat);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
		//[CLSCompliant(false)]
		public static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions,
		   [In] ref RECT lprc, string lpString, uint cbCount, int[] lpDx);

		[DllImport("gdi32.dll")]
		public static extern bool GetTextExtentPoint32(
			IntPtr hdc, 
			string lpString,
			int cbString, 
			out SIZE lpSize);

		//[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int cbString, out SIZE lpSize);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int SetTextColor(IntPtr hDC, int crColor);

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int TextOut(int hDC, int X, int Y, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, int nCount);

		#endregion

		public const int SRCCOPY = 0xcc0020;

		public static int RGB(int R, int G, int B)
		{
			return ((R | (G * 0x100)) | (B * 0x10000));
		}

		[DllImport("gdi32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

		#endregion

		#region POINT RECT SIZE Structs

		public static RECT ToRECT(Rectangle r)
		{
			RECT rect1;
			rect1.Left = r.Left;
			rect1.Top = r.Top;
			rect1.Right = r.Right;
			rect1.Bottom = r.Bottom;
			return rect1;
		}



		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SIZE
		{
			public int x;
			public int y;
		}

		#endregion

		#region Fonts

		public enum FontWeight
		{
			FW_DONTCARE = 0,
			FW_THIN = 100,
			FW_EXTRALIGHT = 200,
			FW_ULTRALIGHT = 200,
			FW_LIGHT = 300,
			FW_NORMAL = 400,
			FW_REGULAR = 400,
			FW_MEDIUM = 500,
			FW_SEMIBOLD = 600,
			FW_DEMIBOLD = 600,
			FW_BOLD = 700,
			FW_EXTRABOLD = 800,
			FW_ULTRABOLD = 800,
			FW_BLACK = 900,
			FW_HEAVY = 900
		}

		public enum CHARSET
		{
			ANSI_CHARSET = 0,
			DEFAULT_CHARSET = 1,
			SYMBOL_CHARSET = 2,
			SHIFTJIS_CHARSET = 128,
			OEM_CHARSET = 255
		}

		public enum OutputPrecision
		{
			OUT_DEFAULT_PRECIS = 0,
			OUT_STRING_PRECIS = 1,
			OUT_CHARACTER_PRECIS = 2,
			OUT_STROKE_PRECIS = 3,
			OUT_TT_PRECIS = 4,
			OUT_DEVICE_PRECIS = 5,
			OUT_RASTER_PRECIS = 6,
			OUT_TT_ONLY_PRECIS = 7,
			OUT_OUTLINE_PRECIS = 8,
			OUT_SCREEN_OUTLINE_PRECIS = 9,
			OUT_PS_ONLY_PRECIS = 10
		}

		public enum ClipPrecision
		{
			CLIP_DEFAULT_PRECIS = 0,
			CLIP_CHARACTER_PRECIS = 1,
			CLIP_STROKE_PRECIS = 2,
			CLIP_MASK = 0xf,
			CLIP_LH_ANGLES = (1 << 4),
			CLIP_TT_ALWAYS = (2 << 4),
			CLIP_EMBEDDED = (8 << 4)
		}

		public enum Quality
		{
			DEFAULT_QUALITY = 0,
			DRAFT_QUALITY = 1,
			PROOF_QUALITY = 2,
			NONANTIALIASED_QUALITY = 3,
			ANTIALIASED_QUALITY = 4,
			CLEARTYPE_QUALITY = 5,
			CLEARTYPE_NATURAL_QUALITY = 6
		}

		public enum PitchAndFamily
		{
			DEFAULT_PITCH = 0,
			FIXED_PITCH = 1,
			VARIABLE_PITCH = 2,
			MONO_FONT = 8
		}

		#endregion

		#endregion

		#region Windows

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, enumShowWindow nCmdShow);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		//[CLSCompliant(false)]
		public static extern bool SetWindowPos(
			IntPtr hWnd, 
			IntPtr hWndInsertAfter, 
			int X,
			int Y, 
			int cx, 
			int cy, 
			uint uFlags);

		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		public static readonly IntPtr HWND_TOP = new IntPtr(0);
		public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr GetDesktopWindow();

		public enum enumShowWindow : int
		{
			Hide = 0,
			Normal = 1,
			ShowMinimized = 2,
			ShowMaximized = 3,
			ShowNoActivate = 4,
			Show = 5,
			Minimize = 6,
			ShowMinNoActive = 7,
			ShowNA = 8,
			Restore = 9,
			ShowDefault = 10,
			ForceMinimize = 11,
			Max = 11
		}

		[DllImport("user32.dll")]
		public static extern bool LockWindowUpdate(IntPtr hwndLock);

		public const UInt32 WM_SETREDRAW = 0x000B;
		public const UInt32 WM_VSCROLL = 0x0115;
		public const UInt32 WM_ERASEBKGND = 0x0014;
		public const UInt32 WM_MOUSEWHEEL = 0x020A;
		public const UInt32 WM_KEYDOWN = 0x0100;

		internal static void SetRedraw(IntPtr hWnd, bool shouldRedraw)
		{
			if (hWnd == IntPtr.Zero)
			{
				return;
			}
			SendMessage(hWnd, WM_SETREDRAW, shouldRedraw ? 1 : 0, 0); 
		}

		public static void SetRedraw(Control control, bool shouldRedraw)
		{
			if (control == null || !control.IsHandleCreated || control.Handle == IntPtr.Zero)
			{
				return;
			}
			SetRedraw(control.Handle, shouldRedraw);
		}

		public const int SPI_SETDESKWALLPAPER = 0X14;
		public const int SPIF_UPDATEINIFILE = 0X1;
		public const int SPIF_SENDWININICHANGE = 0X2;

		[DllImport("USER32.DLL", EntryPoint = "SystemParametersInfo", SetLastError = true)]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		public static void SetDesktopWallpaper(string fileName)
		{
			SystemParametersInfo(
				SPI_SETDESKWALLPAPER,
				0,
				fileName,
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}

		#endregion

		#region Misc

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

		#region Timers

		public static long Ticks()
		{
			long num1 = 0;
			QueryPerformanceCounter(ref num1);
			return num1;
		}

		public static double Milliseconds()
		{
			return Ticks() / PerformanceCounterFrequency() * 1000;
		}

		[DllImport("WinMM.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int timeGetTime();

		public static long PerformanceCounterFrequency()
		{
			long num2 = 0;
			QueryPerformanceFrequency(ref num2);
			return num2;
		}

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int QueryPerformanceCounter(ref long PC);

		[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		public static extern int QueryPerformanceFrequency(ref long PC);

		#endregion

		#endregion
	}
}