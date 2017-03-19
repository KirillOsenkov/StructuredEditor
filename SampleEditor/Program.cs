using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GuiLabs.Editor.Blocks;
using SampleEditor.DiplomSampleCode;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Tests suite = new Tests();
			suite.Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			StyleFactory.Instance = new Skins.SampleStyleFactory();

            Application.Run(new TutorialForm());
        }
    }
}