using System;
using System.IO;
using System.Security.Permissions;

namespace AceSoft.RetailPlus.Client
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
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

                    writer.WriteLine("This is an auto-generated logfile for RetailPlus event logs.");
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
