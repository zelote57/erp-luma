using System;
using System.IO;
using System.Security.Permissions;

namespace AceSoft.RetailPlus.DataCollector
{
    public class Event
    {
        private System.IO.StreamWriter writer;
        private string dateNow;

        public Event()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private void InitializeWriter()
        {
            WriteToWriter(0);

        }
        private void WriteToWriter(int Counter)
        {
            try
            {
                DateTime logdate = DateTime.Now;
                dateNow = logdate.ToShortDateString();

                string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                logsdir += logsdir.EndsWith("/") ? "" : "/";
                if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                {
                    Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                }
                string logFile = logsdir + logdate.ToString("MMM") + "/" + logdate.Month.ToString("0#") + logdate.Day.ToString("0#") + logdate.Year.ToString() + "_" + Counter.ToString("0#").ToString() + ".log";

                if (!File.Exists(logFile))
                {
                    writer = File.AppendText(logFile);

                    writer.WriteLine("This is an auto-generated logfile for RetailPlus inventory Data Collector event logs.");
                    writer.WriteLine("Best viewed in notepad and textpad using Courier 10 as Font.");
                    writer.WriteLine("Log Date: {0}", logdate.ToString("MMMM dd, yyyy"));
                    writer.WriteLine();
                }
                else { writer = File.AppendText(logFile); }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("being used by another process"))
                {
                    Counter += 1;
                    WriteToWriter(Counter);
                }
            }
        }

        public void AddEvent(string Message)
        {
            AddEvent(Message, true);
        }

        public void AddErrorEvent(Exception ex)
        {
            AddEventLn("Error...", false);
            AddEvent("Exception  : " + ex.ToString(), true);
            AddEvent("Message    : " + ex.Message, false);
            AddEvent("Source     : " + ex.Source, false);
            AddEvent("Stack trace: " + ex.StackTrace, false);
        }

        public void AddEvent(string Message, bool IncludeDateLog)
        {
            InitializeWriter();
            if (IncludeDateLog == true)
            {
                writer.Write(DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + " : " + Message);
            }
            else
            {
                writer.WriteLine("                                           " + Message);
            }

            writer.Flush();
            writer.Close();
        }

        public void AddEventReceipt(string Message)
        {
            InitializeWriter();
            writer.Write(Message);
            writer.Flush();
            writer.Close();
        }

        //public void AddEventLn(string Message)
        //{		
        //    AddEventLn(Message, false);
        //}

        public void AddErrorEventLn(Exception ex)
        {
            AddEventLn("Error!", true);
            AddEvent("Exception  : " + ex.ToString(), true);
            AddEvent("Message    : " + ex.Message, false);
            AddEvent("Source     : " + ex.Source, false);
            AddEvent("Stack trace: " + ex.StackTrace, false);
        }

        public void AddEventLn(string Message, bool IncludeDateLog = false, bool willLogThis = true)
        {
            if (willLogThis)
            {
                InitializeWriter();
                if (IncludeDateLog == true)
                {
                    writer.WriteLine(DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + " : " + Message);
                }
                else
                {
                    writer.WriteLine(" " + Message);
                }

                writer.Flush();
                writer.Close();
            }
        }
    }
}
