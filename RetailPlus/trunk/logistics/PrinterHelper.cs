using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

namespace AceSoft
{
    public class PrinterHelper
    {
        public static object GetField(Object obj, String fieldName)
        {
            System.Reflection.FieldInfo fi = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            return fi.GetValue(obj);
        }

        public static void PrintProps(ManagementObject o, string prop)
        {
            try { Console.WriteLine(prop + "|" + o[prop]); }
            catch (Exception e) { Console.Write(e.ToString()); }
        }

        public static bool isPrinterOnline(string objPrinterName)
        {
            bool boretValue = false;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    string printerName = printer["Name"].ToString().ToLower();

                    if (printerName == objPrinterName.ToLower())
                    {
                        boretValue = true;
                        break;
                    }
                    //Console.WriteLine("Printer".PadRight(15) + ":" + printerName);

                    //PrintProps(printer, "Caption");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "Availability");
                    //PrintProps(printer, "Default");
                    //PrintProps(printer, "DetectedErrorState");
                    //PrintProps(printer, "ExtendedDetectedErrorState");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "LastErrorCode");
                    //PrintProps(printer, "PrinterState");
                    //PrintProps(printer, "PrinterStatus");
                    //PrintProps(printer, "Status");
                    //PrintProps(printer, "WorkOffline");
                    //PrintProps(printer, "Local");
                }
            }
            catch { }
            return boretValue;
        }

        public static Boolean PrintFile(string FileName)
        {
            try
            {
                string printerName = "RetailPlusBilling";

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.Verb = "print"; //printto

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                p.StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath(FileName);
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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
