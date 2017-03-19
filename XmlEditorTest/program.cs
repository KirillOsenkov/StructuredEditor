using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XMLEditor
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.Run(new frmMain());
			//try
			//{
			//    Application.Run(new frmMain());
			//}
			//catch (Exception e)
			//{
			//    MessageBox.Show("Application level exception:" + e.ToString());
			//}
		}
	}
}