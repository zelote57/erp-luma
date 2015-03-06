using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AceSoft
{
    public class PdfHelper
    {
        public static Boolean PrintPDFs(string pdfFileName)
        {
            try
            {
                //string printerName = "RetailPlusBilling";

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.Verb = "print"; //printto

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                p.StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath(pdfFileName);
                //p.StartInfo.Arguments = String.Format("/p /h \"{0}\" \"{1}\"", System.Web.HttpContext.Current.Server.MapPath(pdfFileName));
                //p.StartInfo.Arguments = String.Format("/p /h \"{0}\" \"{1}\"", System.Web.HttpContext.Current.Server.MapPath(pdfFileName), printerName);
                //p.StartInfo.Arguments = String.Format(@"/p /h {0}", System.Web.HttpContext.Current.Server.MapPath(pdfFileName));
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.CreateNoWindow = true;

                p.Start();
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                if (p.HasExited == false)
                {
                    p.WaitForExit(2000);
                }

                p.EnableRaisingEvents = true;

                p.Close();

                try { KillAdobe("AcroRd32"); }
                catch { }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //For whatever reason, sometimes adobe likes to be a stage 5 clinger.
        //So here we kill it with fire.
        private static bool KillAdobe(string name)
        {
            foreach (System.Diagnostics.Process clsProcess in System.Diagnostics.Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }
    }//END Class
}
