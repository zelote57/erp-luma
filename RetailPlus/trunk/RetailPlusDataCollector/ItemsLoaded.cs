using System;
using System.IO;
using System.Security.Permissions;

namespace AceSoft.RetailPlus.DataCollector
{
    public class InvLoadedLog
    {
        private System.IO.StreamWriter writer;
        private string dateNow;

        public Data.BranchDetails BranchDetails { get; set; }
        public string LogFile { get; set; }

        public InvLoadedLog()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private void InitializeWriter()
        {
            WriteToWriter();

        }
        private void WriteToWriter()
        {
            try
            {
                DateTime logdate = DateTime.Now;
                dateNow = logdate.ToShortDateString();

                string logsdir = "invfiles";

                logsdir += logsdir.EndsWith("/") ? "" : "/";
                if (!Directory.Exists(logsdir))
                {
                    Directory.CreateDirectory(logsdir);
                }
                string logFile = logsdir + BranchDetails.BranchCode + logdate.ToString("yyyyMMdd") + "_saved.inv";
                LogFile = logFile;

                if (!File.Exists(logFile))
                {
                    writer = File.AppendText(logFile);

                    writer.WriteLine("This is an auto-generated logfile for RetailPlus inventory Data Collector InvLoadedLog.");
                    writer.WriteLine("Best viewed in notepad and textpad using Courier 10 as Font.");
                    writer.WriteLine("Creation Date: {0}", logdate.ToString("MMMM dd, yyyy"));
                    writer.WriteLine();
                }
                else { writer = File.AppendText(logFile); }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AddItem(string Barcode, decimal Quantity, string BaseUnitCode, string ProductCode)
        {
            InitializeWriter();
            writer.WriteLine("{0}|{1}|{2}|{3}", Barcode, Quantity.ToString("########0.#0"), BaseUnitCode, ProductCode);
            writer.Flush();
            writer.Close();
        }

    }
}
