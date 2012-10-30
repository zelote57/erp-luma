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
	public class FilePrinter
	{
		private System.IO.StreamWriter writer;
		private string dateNow;

		private string mstFileName;
		private string mstLogFile;

		public string FileName
		{
			get { return mstLogFile; }
			set { mstFileName = value; }
		}
		public FilePrinter()
		{
		
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

				if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
				{
					Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
				}
				string logFile = logsdir + logdate.ToString("MMM") + "/" + mstFileName + ".prn";
				if (Counter >= 1)
                    logFile = logsdir + logdate.ToString("MMM") + "/" + mstFileName + "_" + Counter.ToString("0#").ToString() + ".prn";

				mstLogFile = logFile;

				writer = new StreamWriter(logFile, false);
				// writer = File.AppendText(logFile);
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
		
		public void Write(string Message)
		{
			InitializeWriter();
			writer.Write(Message);
			writer.Flush();
			writer.Close();
			writer.Dispose();
		}
		public void DeleteFile()
		{
			try
			{
				if (File.Exists(mstLogFile))
				{
					FileStream fs = File.Open(mstLogFile, FileMode.Open, FileAccess.Read, FileShare.None);
					fs.Close();
					fs.Dispose();
					File.Delete(mstLogFile); }
			}
			catch (Exception ex) { }
		}
	}
}
