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
        static void Main(string[] args)
        {
            string steExecutableSender = "RetailPlus.exe";

            if (args.Length > 0)
            {
                steExecutableSender = args[0].Trim();

                if (steExecutableSender != "RetailPlus.exe" && steExecutableSender != "RestoPlus.exe")
                    steExecutableSender = "RetailPlus.exe";
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWnd appmain = new MainWnd();
            appmain.ExecutableSender = steExecutableSender;
            Application.Run(appmain);
        }
    }
}