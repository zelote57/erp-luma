using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceSoft.RetailPlus.Client;
using System.Data;
using System.IO;
using System.Xml;

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

                // check if it's already running
                if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
                {
                    WriteProcessToMonitor("   already running. not ok");
                    return;
                }

                WriteProcessToMonitor("   ok");

                Data.Database clsDatabase = new Data.Database();
            back:
                WriteProcessToMonitor("Checking connections to server.");
                if (IPAddress.IsOpen(AceSoft.RetailPlus.DBConnection.ServerIP(), DBConnection.DBPort()) == false)
                {
                    WriteProcessToMonitor("   cannot connect to server please check.");
                    goto exit;
                }
                WriteProcessToMonitor("   ok");
                WriteProcessToMonitor("Checking connections to database.");
                
                clsDatabase.GetConnection(username: "POSUser", password: "pospwd");
                try
                {
                    bool boIsDBAlive = clsDatabase.IsAlive();
                    WriteProcessToMonitor("   connected to '" + clsDatabase.Connection.ConnectionString.Split(';')[0].ToString().Replace("Data Source=", "") + "'");
                }
                catch (Exception ex)
                {
                    WriteProcessToMonitor("   ERROR connecting to database. Exception: " + ex.ToString());
                }

                #region Timed-Out Process
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
                        else if (iTime > 16 && string.IsNullOrEmpty(strInfo))
                        {
                            WriteProcessToMonitor("          status idle... killing process id: " + iID.ToString());
                            clsDatabase.killProcess(iID);
                            WriteProcessToMonitor("      done.");
                        }
                        else if (strInfo.Contains("INSERT INTO tblTransactions") && iTime >= 8)
                        {
                            WriteProcessToMonitor("          status not ok...  flushing table tblTransactions...process id: " + iID.ToString());
                            clsDatabase.FlushTable("tblTransactions");
                            WriteProcessToMonitor("      done.");
                        }
                        else if (strInfo.Contains("UPDATE tblTerminalReport") && iTime >= 8)
                        {
                            WriteProcessToMonitor("          status not ok... flushing table tblTerminalReport...process id: " + iID.ToString());
                            clsDatabase.FlushTable("tblTerminalReport");
                            WriteProcessToMonitor("      done.");
                        }
                        else { WriteProcessToMonitor("          status ok"); }
                    }
                }
                clsDatabase.CommitAndDispose();

                // audit
                //clsDatabase = new Data.Database(clsDatabase.Connection, clsDatabase.Transaction);
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
                #endregion

                string strSyncFunction = ""; 
                try { strSyncFunction = System.Configuration.ConfigurationManager.AppSettings["SyncFunction"].ToString(); }
                catch { }

                if (strSyncFunction.ToLower().Trim() == "export")
                {
                    #region Export Products
                
                    //clsDatabase = new Data.Database(clsDatabase.Connection, clsDatabase.Transaction);
                    clsDatabase.GetConnection(username: "POSUser", password: "pospwd");

                    Data.SysConfig clsSysConfig = new Data.SysConfig(clsDatabase.Connection, clsDatabase.Transaction);
                    DateTime dteLastSyncDateTime = clsSysConfig.get_ProdLastSyncDateTime();
                    DateTime dteLastSyncDateTimeTo = dteLastSyncDateTime.AddMinutes(clsSysConfig.get_ProdSyncInterval());

                    if (dteLastSyncDateTimeTo <= DateTime.Now)
                    {
                        Data.DBSync clsDBSync = new Data.DBSync(clsDatabase.Connection, clsDatabase.Transaction);
                        System.Data.DataTable stSyncItems = clsDBSync.ListAsDataTable("tblProducts", dteLastSyncDateTime, dteLastSyncDateTimeTo);

                        if (stSyncItems.Rows.Count > 0)
                        {
                            string xmlFileName = @"C:\RetailPlus\RetailPlus\RetailPlus\temp\uploaded\prodsync\prod_" + dteLastSyncDateTime.ToString("yyyyMMddHHmmss") + ".xml";

                            if (!System.IO.File.Exists(xmlFileName))
                            {
                                XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);

                                writer.Formatting = Formatting.Indented;
                                writer.WriteStartDocument();
                                writer.WriteComment("This file represents the updated products.");
                                writer.WriteStartElement("Header");
                                writer.WriteAttributeString("TableName", stSyncItems.TableName);
                                writer.WriteAttributeString("LastSyncDateTime", dteLastSyncDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                                writer.WriteAttributeString("LastSyncDateTimeTo", dteLastSyncDateTimeTo.ToString("yyyy-MM-dd HH:mm:ss"));

                                foreach (DataRow dr in stSyncItems.Rows)
                                {
                                    writer.WriteStartElement("Details");

                                    foreach (DataColumn dc in dr.Table.Columns)
                                    {
                                        if (dc.DataType ==  System.Type.GetType("System.DateTime"))
                                            writer.WriteAttributeString(dc.ColumnName, DateTime.Parse(dr[dc.ColumnName].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                                        else 
                                            writer.WriteAttributeString(dc.ColumnName, dr[dc.ColumnName].ToString());
                                    }
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                //Write the XML to file and close the writer
                                writer.Flush();
                                writer.Close();
                            }
                        }

                        clsSysConfig = new Data.SysConfig(clsDatabase.Connection, clsDatabase.Transaction);
                        clsSysConfig.Save(new Data.SysConfigDetails()
                        {
                            ConfigName = "ProdLastSyncDateTime",
                            ConfigValue = dteLastSyncDateTimeTo.ToString("yyyy-MM-dd HH:mm:ss"),
                            Category = "Sync"
                        });
                    }
                    clsDatabase.CommitAndDispose();
                
                    #endregion
                }
                else if (strSyncFunction.ToLower().Trim() == "import")
                {
                    #region Import Products

                    XmlTextReader xmlReader = new XmlTextReader(@"C:\RetailPlus\RetailPlus\RetailPlus\temp\uploaded\prodsync\prod_20150629183000.xml");
                    xmlReader.WhitespaceHandling = WhitespaceHandling.None;

                    ////clsDatabase = new Data.Database(clsDatabase.Connection, clsDatabase.Transaction);
                    clsDatabase.GetConnection(DBName: "poseamirror", username: "POSUser", password: "pospwd");
                    clsDatabase.SetForeignKey(false);

                    Data.SysConfig clsSysConfig = new Data.SysConfig(clsDatabase.Connection, clsDatabase.Transaction);
                    DateTime dteLastSyncDateTime = clsSysConfig.get_ProdLastSyncDateTime();
                    DateTime dteLastSyncDateTimeTo = dteLastSyncDateTime.AddMinutes(clsSysConfig.get_ProdSyncInterval());

                    Data.Products clsProducts = new Data.Products(clsDatabase.Connection, clsDatabase.Transaction);
                    Data.ProductDetails clsProductDetails = new Data.ProductDetails();

                    string strTableName = "";
                    DateTime dteXmlLastSyncDateTime = Constants.C_DATE_MIN_VALUE;
                    DateTime dteXmlLastSyncDateTimeTo = Constants.C_DATE_MIN_VALUE;

                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (xmlReader.Name == "Header")
                                {
                                    strTableName = xmlReader.GetAttribute("TableName").ToString();
                                    dteXmlLastSyncDateTime = DateTime.TryParse(xmlReader.GetAttribute("LastSyncDateTime").ToString(), out dteXmlLastSyncDateTime) ? dteXmlLastSyncDateTime : Constants.C_DATE_MIN_VALUE;
                                    dteXmlLastSyncDateTimeTo = DateTime.TryParse(xmlReader.GetAttribute("LastSyncDateTimeTo").ToString(), out dteXmlLastSyncDateTimeTo) ? dteXmlLastSyncDateTimeTo : Constants.C_DATE_MIN_VALUE;

                                    if (dteLastSyncDateTime >= dteXmlLastSyncDateTime) break;
                                }
                                else if (strTableName == "tblProducts" && xmlReader.Name == "Details")
                                {
                                    clsProductDetails = Data.DBSync.setSyncProductDetails(xmlReader);
                                    clsProducts.Save(clsProductDetails);
                                }
                                break;
                        }
                    }

                    if (dteLastSyncDateTime < dteXmlLastSyncDateTime)
                    {
                        clsSysConfig = new Data.SysConfig(clsDatabase.Connection, clsDatabase.Transaction);
                        clsSysConfig.Save(new Data.SysConfigDetails()
                        {
                            ConfigName = "ProdLastSyncDateTime",
                            ConfigValue = dteXmlLastSyncDateTimeTo.ToString("yyyy-MM-dd HH:mm:ss"),
                            Category = "Sync"
                        });
                    }

                    clsDatabase.SetForeignKey(true);
                    clsDatabase.CommitAndDispose();

                    #endregion
                }

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
