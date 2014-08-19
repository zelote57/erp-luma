using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __GeneralLedger : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadOptions();
                    GenerateHTML();
				}	
			}
		}

		private void LoadOptions()
		{
			
		}
 
		#region GeneratePDF

		private void GeneratePDF()
		{
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/GeneralLedger.rpt"));

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\RetailPlus\temp\");

			string strFileName = "generalledger_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".pdf";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.PortableDocFormat;
			rpt.Export();   rpt.Close();    rpt.Dispose();
			
			fraViewer.Attributes.Add("src","/RetailPlus/temp/" + strFileName);
		}

		#endregion

		#region GenerateWord

		private void GenerateWord()
		{
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/GeneralLedger.rpt"));

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\retailplus\temp\");

			string strFileName = "generalledger_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".doc";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.WordForWindows;
			rpt.Export();   rpt.Close();    rpt.Dispose();
			
			fraViewer.Attributes.Add("src","/retailplus/temp/" + strFileName);
		}

		#endregion

		#region GenerateExcel

		private void GenerateExcel()
		{
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/GeneralLedger.rpt"));

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\RetailPlus\temp\");

			string strFileName = "generalledger_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".xls";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.Excel;
			rpt.Export();   rpt.Close();    rpt.Dispose();
			
			fraViewer.Attributes.Add("src","/RetailPlus/temp/" + strFileName);
		}

		#endregion

		#region GenerateHTML

		private void GenerateHTML()
		{
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/GeneralLedger.rpt"));

			HTMLFormatOptions htmlOpts = new HTMLFormatOptions();
 
			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\RetailPlus\temp\html\");
//			DeleteTempDirectory(strPath);

			string strFileName = "generalledger_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".htm";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			htmlOpts.HTMLFileName = strFileName;
			htmlOpts.HTMLEnableSeparatedPages = true;;
			htmlOpts.HTMLHasPageNavigator = true;
			htmlOpts.HTMLBaseFolderName = strPath;
			rpt.ExportOptions.FormatOptions = htmlOpts;

			exportop = rpt.ExportOptions;

			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.HTML40;
			
			dest.DiskFileName = strFileName.ToString();
			exportop.DestinationOptions = dest;

			SetDataSource(rpt);

			rpt.Export();   rpt.Close();    rpt.Dispose();

			strFileName = "//" + Request.ServerVariables["SERVER_NAME"].ToString() + FindHTMLFile(strPath,strFileName);	
			
			fraViewer.Attributes.Add("src",strFileName);
		}

		private string FindHTMLFile(string Root,string HTMLFile)
		{
			string strFile = string.Empty;

			string[] dirs = Directory.GetDirectories(Root);
			foreach (string dir in dirs)
			{									
				string[] strFiles = Directory.GetFiles(dir);

				foreach( string file in strFiles)
				{
					if (file.ToLower()==dir.ToLower() + "\\" + HTMLFile.ToLower())
					{
						int lngIndex = file.ToLower().IndexOf(@"\retailplus\temp\html\");
						string filepath = file.Substring(lngIndex); 
						filepath = filepath.Replace("\\","/");
						return filepath;
					}
				}
			}

			return strFile;
		}

//		private void DeleteTempDirectory(string Path)
//		{
//			if (Directory.Exists(Path)) 
//			{
//				DirectoryInfo dir = new DirectoryInfo(Path);
//				dir.Delete(true);
//			}
//			
//			Directory.CreateDirectory(Path); 
//
//		}

		#endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();

            ChartOfAccounts clsChartOfAccount = new ChartOfAccounts();
            MySqlDataReader myreader = clsChartOfAccount.List("ChartOfAccountID", SortOption.Ascending);
			clsChartOfAccount.CommitAndDispose();

			while(myreader.Read())
			{
				DataRow drNew = rptds.ChartOfAccount.NewRow();
				
				foreach (DataColumn dc in rptds.ChartOfAccount.Columns)
					drNew[dc] = "" + myreader[dc.ColumnName]; 
				
				rptds.ChartOfAccount.Rows.Add(drNew);
			}

			Report.SetDataSource(rptds); 
			SetParameters(Report);
		}

		#endregion

		#region SetParameters
		private void SetParameters (ReportDocument Report)
		{
			ParameterFieldDefinition paramField;
			ParameterValues currentValues;
			ParameterDiscreteValue discreteParam;

			paramField = Report.DataDefinition.ParameterFields["CompanyName"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = CompanyDetails.CompanyName;
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

			paramField = Report.DataDefinition.ParameterFields["PrintedBy"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = Session["Name"].ToString();
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["IncludeInActive"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cnkInActive.Checked;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void cmdView_Click(object sender, System.EventArgs e)
		{
			fraViewer.Visible = true;

			switch (Convert.ToInt16(cboReportOptions.SelectedItem.Value))
			{
				case 0:
					GenerateHTML();
					break;
				case 1:
					GeneratePDF();
					break;
				case 2:
					GenerateWord();
					break;
				case 3:
					GenerateExcel();
					break;
			}
		}

        protected void imgView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            fraViewer.Visible = true;

            switch (Convert.ToInt16(cboReportOptions.SelectedItem.Value))
            {
                case 0:
                    GenerateHTML();
                    break;
                case 1:
                    GeneratePDF();
                    break;
                case 2:
                    GenerateWord();
                    break;
                case 3:
                    GenerateExcel();
                    break;
            }
        }
}
}
