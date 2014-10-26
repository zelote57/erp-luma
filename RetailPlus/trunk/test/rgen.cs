using System;
using System.Management;
using AceSoft.RetailPlus;
using System.IO;
using System.Text;

namespace Test
{
    /******************************************************************************
    **		Auth: Lemuel E. Aceron
    **		Date: May 21, 2006
    *******************************************************************************
    **		Change History
    *******************************************************************************
    **		Date:		Author:				Description:
    **		--------		--------				-------------------------------------------
    **    
    *******************************************************************************/

    /// <summary>
    /// Summary description for RandomGen.
    /// </summary>
    public class RandomGen
    {
        

        public static void Main(string[] args)
        {
            try
            {
                Int32 iNoOfRandomNos = 1;
                DateTime logdate = DateTime.Now;

                if (args.Length > 0)
                    iNoOfRandomNos = Int32.TryParse(args[0].ToString(), out iNoOfRandomNos) ? iNoOfRandomNos : 1;

                System.Text.StringBuilder strLine = new System.Text.StringBuilder();
                string chars = "2596181379810510";
                System.Text.StringBuilder pass = new System.Text.StringBuilder();
                Random ran = new Random();

                for (int iCtr = 0; iCtr < iNoOfRandomNos; iCtr++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        pass.Append(chars[ran.Next(0, chars.Length)]);
                    }

                    
                    strLine.Append(pass.ToString());
                    strLine.Append("|");
                    strLine.Append(pass.ToString().Remove(10, 6));
                    strLine.Append("|");
                    strLine.Append(pass.ToString().Remove(0, 10));
                    strLine.Append("|");
                    strLine.Append("INSERT INTO sysAccessUsers(Username, Password, DateCreated, Deleted, Createdon, LastModified)");
                    strLine.Append("VALUES('" + pass.ToString().Remove(10, 6) + "', '" + pass.ToString().Remove(0, 10) + "', '" + logdate.ToString("yyyy-MM-dd HH:mm:ss") + "', 0, '" + logdate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + logdate.ToString("yyyy-MM-dd HH:mm:ss") + "'); ");
                    strLine.Append(" "); // seperator is already the semi-colon ;
                    strLine.Append("INSERT INTO sysAccessUserDetails(UID, Name, CountryID, GroupID, PageSize, Createdon, LastModified)");
                    strLine.Append("VALUES((SELECT UID FROM sysAccessUsers WHERE UserName='" + pass.ToString().Remove(10, 6) + "'), '" + pass.ToString().Remove(10, 6) + "', 1, 1, 10, '" + logdate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + logdate.ToString("yyyy-MM-dd HH:mm:ss") + "'); ");
                    Console.WriteLine(strLine.ToString());
                    WriteToWriter(strLine);

                    strLine = new StringBuilder();
                    pass = new StringBuilder();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }

        private static void WriteToWriter(StringBuilder strpass)
        {
            System.IO.StreamWriter writer;

            DateTime logdate = DateTime.Now;

            string logsdir = System.Windows.Forms.Application.ExecutablePath.ToUpper().Replace("TEST.EXE", "");

            logsdir += logsdir.EndsWith("/") ? "" : "/";
            if (!Directory.Exists(logsdir + "usrs"))
            {
                Directory.CreateDirectory(logsdir + "usrs");
            }
            string logFile = logsdir + "usrs/usr_" + logdate.Month.ToString("0#") + logdate.Day.ToString("0#") + logdate.Year.ToString() + ".usr";

            if (!File.Exists(logFile))
            {
                writer = File.AppendText(logFile);

                writer.WriteLine("This is an auto-generated users for RetailPlus event logs.");
                writer.WriteLine("Best viewed in notepad and textpad using Courier 10 as Font.");
                writer.WriteLine("Log Date: {0}", logdate.ToString("MMMM dd, yyyy"));
                writer.WriteLine();
            }
            else { writer = File.AppendText(logFile); }
            writer.WriteLine(strpass);

            writer.Flush();
            writer.Close();
        }
    }
}
