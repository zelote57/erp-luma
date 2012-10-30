using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AceSoft.RetailPlus.VersionChecker
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
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWnd());
        }
    }
}