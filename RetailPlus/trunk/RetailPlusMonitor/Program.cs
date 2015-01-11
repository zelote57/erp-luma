using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceSoft.RetailPlus.Client;
using System.Data;

namespace AceSoft.RetailPlus.Monitor
{
    class MainModule
    {
        #region Application Main

		[STAThread]
        static void Main(string[] args)
        {
            try
            {
                WriteProcessToMonitor("Starting RetailPlus monitoring tool...");
                WriteProcessToMonitor("   ok");
            back:
                WriteProcessToMonitor("Checking connections to server.");
                if (IPAddress.IsOpen(AceSoft.RetailPlus.DBConnection.ServerIP(), DBConnection.DBPort()) == false)
                {
                    WriteProcessToMonitor("   cannot connect to server please check.");
                    goto exit;
                }
                WriteProcessToMonitor("   ok");
                WriteProcessToMonitor("Checking connections to database.");
                
                Data.Database clsDatabase = new Data.Database();
                try
                {
                    bool boIsDBAlive = clsDatabase.IsAlive();
                    WriteProcessToMonitor("   connected to '" + clsDatabase.Connection.ConnectionString.Split(';')[0].ToString().Replace("Data Source=", "") + "'");
                }
                catch (Exception ex)
                {
                    WriteProcessToMonitor("   ERROR connecting to database. Exception: " + ex.ToString());
                }
                WriteProcessToMonitor("Checking timed-out process.");
                System.Data.DataTable dtProcessList = clsDatabase.getProcessList();
                foreach (DataRow dr in dtProcessList.Rows)
                {
                    int iID = int.Parse(dr["ID"].ToString());
                    string strHost = dr["Host"].ToString();
                    string strDB = dr["db"].ToString();
                    int iTime = int.Parse(dr["Time"].ToString());
                    string strInfo = dr["Info"].ToString();
                    if (strInfo.ToUpper() != "SHOW PROCESSLIST" || strDB == "pos")
                    {
                        WriteProcessToMonitor("      id:" + iID.ToString() + "  host:" + strHost + "  Time:" + iTime.ToString() + "  Info:" + strInfo);
                        if (iTime > Constants.C_DEFAULT_MYSQL_PROCESS_TIMEOUT && strDB == "pos")
                        {
                            WriteProcessToMonitor("          status not ok... killing process id: " + iID.ToString());
                            clsDatabase.killProcess(iID);
                            WriteProcessToMonitor("      done.");
                        }
                        else { WriteProcessToMonitor("          status ok"); }
                    }
                }
                clsDatabase.CommitAndDispose();

                // audit
                clsDatabase = new Data.Database();
                clsDatabase.GetConnection(username: "POSAuditUser", password: "posauditpwd");
                try
                {
                    bool boIsDBAlive = clsDatabase.IsAlive();
                    WriteProcessToMonitor("   connected to '" + clsDatabase.Connection.ConnectionString.Split(';')[0].ToString().Replace("Data Source=", "") + "'");
                }
                catch (Exception ex)
                {
                    WriteProcessToMonitor("   ERROR connecting to database. Exception: " + ex.ToString());
                }
                WriteProcessToMonitor("Checking Audit timed-out process.");
                dtProcessList = clsDatabase.getProcessList();
                foreach (DataRow dr in dtProcessList.Rows)
                {
                    int iID = int.Parse(dr["ID"].ToString());
                    string strHost = dr["Host"].ToString();
                    string strDB = dr["db"].ToString();
                    int iTime = int.Parse(dr["Time"].ToString());
                    string strInfo = dr["Info"].ToString();
                    if (strInfo.ToUpper() != "SHOW PROCESSLIST" || strDB == "pos")
                    {
                        WriteProcessToMonitor("      audit id:" + iID.ToString() + "  host:" + strHost + "  Time:" + iTime.ToString() + "  Info:" + strInfo);
                        if (iTime > Constants.C_DEFAULT_MYSQL_PROCESS_TIMEOUT && strDB == "pos")
                        {
                            WriteProcessToMonitor("          status not ok... killing process id: " + iID.ToString());
                            clsDatabase.killProcess(iID);
                            WriteProcessToMonitor("      done.");
                        }
                        else { WriteProcessToMonitor("          status ok"); }
                    }
                }
                clsDatabase.CommitAndDispose();

                WriteProcessToMonitor("   done checking...");

                WriteProcessToMonitor("Waiting for next process...");

                System.Threading.Thread.Sleep(20000);
                goto back;
            exit:
                WriteProcessToMonitor("Sytem terminated.");
            }
            catch (Exception ex) 
            {
                WriteProcessToMonitor("PLEASE CALL RETAILPLUS IMMEDIATELY... error:" + ex.ToString());
            }
        }

        #endregion

        #region Private Modifiers

        private static void WriteProcessToMonitor(string ProcessToWrite)
        {
            Event clsEvent = new Event();
            clsEvent.AddEventLn(ProcessToWrite, true);
            Console.WriteLine(ConsoleMonitor() + ProcessToWrite);
        }
        private static string ConsoleMonitor()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": ";
        }
        #endregion

    }
}
