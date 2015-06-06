using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __GeneralLedger : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboContactName;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "gl";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                //if (Session["ReportType"].ToString() == "gl")
                    //try { CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"]; } catch { }
            }
        }


		private void LoadOptions()
		{
			txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
			txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

			cboReportType.Items.Clear();
			
			cboReportType.Items.Add(new ListItem("Select Report Type", "0"));
			cboReportType.Items.Add(new ListItem("Posted PO", "Posted PO"));
			cboReportType.Items.Add(new ListItem("Posted PO Returns", "Posted PO Returns"));
			cboReportType.Items.Add(new ListItem("Posted Debit Memo", "Posted Debit Memo"));
			cboReportType.Items.Add(new ListItem("By Vendor", "By Vendor"));
			
			cboReportType.SelectedIndex = 0;
			
			if (Request.QueryString["reporttype"] != null)
			{
				string stReportType = Common.Decrypt(Request.QueryString["reporttype"],Session.SessionID);
				switch (stReportType)
				{
					case "Posted PO":
						cboReportType.SelectedIndex = 1;
						break;
					case "Posted PO Returns":
						cboReportType.SelectedIndex = 2;
						break;
					case "Posted Debit Memo":
						cboReportType.SelectedIndex = 3;
						break;
					case "By Vendor":
						cboReportType.SelectedIndex = 4;
						break;
				}

				fraViewer.Visible = true;
				GenerateHTML();
			}
		}


		#region GeneratePDF
		private void GeneratePDF()
		{
			ReportDocument rpt = new ReportDocument();

			switch (cboReportType.SelectedItem.Text)
			{
                case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPO.rpt"));
                    break;
                case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPOReturns.rpt"));
                    break;
                case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedDebitMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PurchaseAnalysis.rpt"));
                    break;
                default:
                    return;
					
			}

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\retailplus\temp\");

			string strFileName = cboReportType.SelectedItem.Text.Replace(" ", "").ToLower() + "_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".pdf";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.PortableDocFormat;
			rpt.Export();   rpt.Close();    rpt.Dispose();

			fraViewer.Attributes.Add("src",Constants.ROOT_DIRECTORY + "/temp/" + strFileName);
		}

		#endregion

		#region GenerateWord

		private void GenerateWord()
		{
			ReportDocument rpt = new ReportDocument();

			switch (cboReportType.SelectedItem.Text)
			{
                case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPO.rpt"));
                    break;
                case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPOReturns.rpt"));
                    break;
                case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedDebitMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PurchaseAnalysis.rpt"));
                    break;
                default:
                    return;
					
			}

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\retailplus\temp\");

			string strFileName = cboReportType.SelectedItem.Text.Replace(" ", "").ToLower() + "_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".doc";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.WordForWindows;
			rpt.Export();   rpt.Close();    rpt.Dispose();
			
			fraViewer.Attributes.Add("src",Constants.ROOT_DIRECTORY + "/temp/" + strFileName);
		}

		#endregion

		#region GenerateExcel

		private void GenerateExcel()
		{
			ReportDocument rpt = new ReportDocument();

			switch (cboReportType.SelectedItem.Text)
			{
                case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPO.rpt"));
                    break;
                case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPOReturns.rpt"));
                    break;
                case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedDebitMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PurchaseAnalysis.rpt"));
                    break;
                default:
                    return;
					
			}

			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\retailplus\temp\");

			string strFileName = cboReportType.SelectedItem.Text.Replace(" ", "").ToLower() + "_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".xls";
			if (System.IO.File.Exists(strPath + strFileName))
				System.IO.File.Delete(strPath + strFileName);

			dest.DiskFileName = strPath + strFileName;

			exportop = rpt.ExportOptions;
	
			SetDataSource(rpt);

			exportop.DestinationOptions = dest;
			exportop.ExportDestinationType = ExportDestinationType.DiskFile;
			exportop.ExportFormatType = ExportFormatType.Excel;
			rpt.Export();   rpt.Close();    rpt.Dispose();
			
			fraViewer.Attributes.Add("src",Constants.ROOT_DIRECTORY + "/temp/" + strFileName);
		}

		#endregion

		#region GenerateHTML

		private void GenerateHTML()
		{
			ReportDocument rpt = new ReportDocument();

			switch (cboReportType.SelectedItem.Text)
			{
				case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPO.rpt"));
					break;
				case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedPOReturns.rpt"));
					break;
				case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PostedDebitMemo.rpt"));
					break;
				case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/PurchaseAnalysis.rpt"));
					break;
				default:
					return;
					
			}

			HTMLFormatOptions htmlOpts = new HTMLFormatOptions();
 
			ExportOptions exportop = new ExportOptions();
			DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
			
			string strPath = Server.MapPath(@"\retailplus\temp\html\");

			string strFileName = cboReportType.SelectedItem.Text.Replace(" ", "").ToLower() + "_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + ".htm";
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

            Session["ReportDocument"] = rpt;

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


		#endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();

			DateTime PostingDateFrom = DateTime.MinValue;
			try
			{	PostingDateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text);	}
			catch{}
			DateTime PostingDateTo = DateTime.MinValue;
			try
			{	PostingDateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text);	}
			catch{}

			System.Data.DataTable dt;
			string ReportType = cboReportType.SelectedItem.Text;

			switch (ReportType)
			{
				case "Posted PO":
					Data.PO clsPO = new Data.PO();
                    dt = clsPO.ListAsDataTable(POStatus.Posted, new Data.PODetails(), Constants.C_DATE_MIN_VALUE, Constants.C_DATE_MIN_VALUE, PostingDateFrom, PostingDateTo);
					clsPO.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drPO = rptds.PO.NewRow();

						foreach (DataColumn dc in rptds.PO.Columns)
							drPO[dc] = dr[dc.ColumnName];

						rptds.PO.Rows.Add(drPO);
					}
					break;
				case "Posted PO Returns":
					Data.POReturns clsPOReturns = new Data.POReturns();
					dt =  clsPOReturns.ListAsDataTable(POReturnStatus.Posted, PostingStartDate: PostingDateFrom, PostingEndDate: PostingDateTo);
					clsPOReturns.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drPOReturns = rptds.POReturns.NewRow();

						foreach (DataColumn dc in rptds.POReturns.Columns)
							drPOReturns[dc] = dr[dc.ColumnName];

						rptds.POReturns.Rows.Add(drPOReturns);
					}
					break;
				case "Posted Debit Memo":
					Data.DebitMemos clsDebitMemos = new Data.DebitMemos();
					dt =  clsDebitMemos.ListAsDataTable(DebitMemoStatus.Posted, PostingStartDate: PostingDateFrom, PostingEndDate: PostingDateTo);
					clsDebitMemos.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drDebitMemos = rptds.DebitMemo.NewRow();

						foreach (DataColumn dc in rptds.DebitMemo.Columns)
							drDebitMemos[dc] = dr[dc.ColumnName];

						rptds.DebitMemo.Rows.Add(drDebitMemos);
					}
					break;
				case "By Vendor":
					Data.PurchaseAnalysis clsPurchaseAnalysis = new Data.PurchaseAnalysis();
					dt = clsPurchaseAnalysis.ByVendor(PostingDateFrom, PostingDateTo);
					clsPurchaseAnalysis.CommitAndDispose();

					foreach(System.Data.DataRow dr in dt.Rows)
					{
						DataRow drPAByVendor = rptds.PurchaseAnalysisPerVendor.NewRow();

						foreach (DataColumn dc in rptds.PurchaseAnalysisPerVendor.Columns)
							drPAByVendor[dc] = dr[dc.ColumnName];

						rptds.PurchaseAnalysisPerVendor.Rows.Add(drPAByVendor);
					}
					break;

				default:
					break;
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
	}
}
