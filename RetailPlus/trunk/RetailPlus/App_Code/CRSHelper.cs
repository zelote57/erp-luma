using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Web.SessionState;

using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace AceSoft.RetailPlus
{
    public class CRSHelper
    {
        public static void OpenExportedReport(string FileName)
        {
            try
            {
                //System.Net.WebClient Client = new System.Net.WebClient();
                //Client.DownloadFile(Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName), FileName);

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName);
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                p.Start();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void GenerateReport(string FileName, ReportDocument rpt, System.Web.UI.Control ClientScriptBlockControl, ExportFormatType pvtExportFormatType)
        {
            try
            {
                ExportOptions exportop = new ExportOptions();
                DiskFileDestinationOptions dest = new DiskFileDestinationOptions();

                string strFileExtensionName = ".pdf";
                switch (pvtExportFormatType)
                {
                    case ExportFormatType.PortableDocFormat: strFileExtensionName = ".pdf"; exportop.ExportFormatType = ExportFormatType.PortableDocFormat; break;
                    case ExportFormatType.WordForWindows: strFileExtensionName = ".doc"; exportop.ExportFormatType = ExportFormatType.WordForWindows; break;
                    case ExportFormatType.Excel: strFileExtensionName = ".xls"; exportop.ExportFormatType = ExportFormatType.Excel; break;
                }

                string strPath = System.Web.HttpContext.Current.Server.MapPath(@"\retailplus\temp\");
                string strFileName = FileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;

                if (System.IO.File.Exists(strPath + strFileName))
                    System.IO.File.Delete(strPath + strFileName);

                dest.DiskFileName = strPath + strFileName;
                exportop.DestinationOptions = dest;
                exportop.ExportDestinationType = ExportDestinationType.DiskFile;
                rpt.Export(exportop); //rpt.Close(); rpt.Dispose();
                // remove the error
                if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
                {
                    // the maximum report processing jobs limit configured by your system administrator has been reached.
                    rpt.Close(); rpt.Dispose();
                }

                if (pvtExportFormatType == ExportFormatType.PortableDocFormat)
                {
                    string newWindowUrl = Constants.ROOT_DIRECTORY + "/temp/" + strFileName;
                    string javaScript = "window.open('" + newWindowUrl + "');";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(ClientScriptBlockControl, ClientScriptBlockControl.GetType(), "openwindow", javaScript, true);
                }
                else
                {
                    string newWindowUrl = Constants.ROOT_DIRECTORY + "/temp/" + strFileName;
                    string javaScript = "window.open('" + newWindowUrl + "','_self');";
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(ClientScriptBlockControl, ClientScriptBlockControl.GetType(), "openwindow", javaScript, true);

                    //System.Diagnostics.Process p = new System.Diagnostics.Process();
                    //p.StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + strFileName);
                    //p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    //p.Start();
                }
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
