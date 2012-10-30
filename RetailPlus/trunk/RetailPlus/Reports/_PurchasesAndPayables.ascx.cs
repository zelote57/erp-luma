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

	public partial  class __PurchasesAndPayables : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null)
            {
                CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
			txtStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
			txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            LoadRecord();
		}

        private void LoadContact()
        {
            Data.ContactColumns clsContactColumns = new Data.ContactColumns();
            clsContactColumns.ContactName = true;

            Data.ContactColumns clsSearchColumns = new Data.ContactColumns();
            clsContactColumns.ContactName = true;
            clsContactColumns.ContactCode = true;

            Data.Contact clsContact = new Data.Contact();
            cboSupplier.DataTextField = Data.ContactColumnNames.ContactName;
            cboSupplier.DataValueField = Data.ContactColumnNames.ContactID;
            cboSupplier.DataSource = clsContact.Suppliers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsSearchColumns, txtSupplier.Text, 100, false, Data.ContactColumnNames.ContactName, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboSupplier.DataBind();
            clsContact.CommitAndDispose();

            cboSupplier.Items.Add(new ListItem("All Suppliers", "0"));
            cboSupplier.SelectedIndex = cboSupplier.Items.Count - 1;
        }
        private void LoadRecord()
        {
            LoadContact();

            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem("Select Report Type", "0"));
            cboReportType.Items.Add(new ListItem("Posted PO", "Posted PO"));
            cboReportType.Items.Add(new ListItem("Posted PO Returns", "Posted PO Returns"));
            cboReportType.Items.Add(new ListItem("Posted Debit Memo", "Posted Debit Memo"));
            cboReportType.Items.Add(new ListItem("By Vendor", "By Vendor"));
            cboReportType.SelectedIndex = 0;

            if (Request.QueryString["reporttype"] != null)
            {
                string stReportType = Common.Decrypt(Request.QueryString["reporttype"], Session.SessionID);
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

                GenerateHTML();
            }
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = new ReportDocument();
            switch (cboReportType.SelectedItem.Text)
            {
                case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedPO.rpt"));
                    break;
                case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedPOReturns.rpt"));
                    break;
                case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedDebitMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPurchaseAnalysis.rpt"));
                    break;
                default:
                    return;

            }

            SetDataSource(rpt);

            ExportOptions exportop = new ExportOptions();
            DiskFileDestinationOptions dest = new DiskFileDestinationOptions();
            string strPath = Server.MapPath(@"\retailplus\temp\");
            string strFileExtensionName = ".pdf";
            switch (pvtExportFormatType)
            {
                case ExportFormatType.PortableDocFormat: strFileExtensionName = ".pdf"; exportop.ExportFormatType = ExportFormatType.PortableDocFormat; break;
                case ExportFormatType.WordForWindows: strFileExtensionName = ".doc"; exportop.ExportFormatType = ExportFormatType.WordForWindows; break;
                case ExportFormatType.Excel: strFileExtensionName = ".xls"; exportop.ExportFormatType = ExportFormatType.Excel; break;
            }
            string strFileName = "purchases_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
            if (System.IO.File.Exists(strPath + strFileName))
                System.IO.File.Delete(strPath + strFileName);

            dest.DiskFileName = strPath + strFileName;
            exportop.DestinationOptions = dest;
            exportop.ExportDestinationType = ExportDestinationType.DiskFile;
            rpt.Export(exportop); //rpt.Close(); rpt.Dispose();

            if (pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                rpt.Close(); rpt.Dispose();
                Response.Redirect(Constants.ROOT_DIRECTORY + "/temp/" + strFileName, false);
            }
            else
            {
                CRViewer.ReportSource = rpt;
                Session["ReportDocument"] = rpt;
                OpenExportedReport(strFileName);
            }

        }

        private void OpenExportedReport(string FileName)
        {
            try
            {
                System.Net.WebClient Client = new System.Net.WebClient();
                Client.DownloadFile(Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName), @"c:\" + FileName);

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"c:\" + FileName; //Server.MapPath(Constants.ROOT_DIRECTORY + "/temp/" + FileName);
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                p.Start();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region GeneratePDF
        private void GeneratePDF() { Export(ExportFormatType.PortableDocFormat); }
        #endregion

        #region GenerateWord
        private void GenerateWord() { Export(ExportFormatType.WordForWindows); }
        #endregion

        #region GenerateExcel
        private void GenerateExcel() { Export(ExportFormatType.Excel); }
        #endregion

        #region GenerateHTML

        private void GenerateHTML()
        {
            ReportDocument rpt = new ReportDocument();
            switch (cboReportType.SelectedItem.Text)
            {
                case "Posted PO":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedPO.rpt"));
                    break;
                case "Posted PO Returns":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedPOReturns.rpt"));
                    break;
                case "Posted Debit Memo":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPostedDebitMemo.rpt"));
                    break;
                case "By Vendor":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_PurchasesAndPayablesPurchaseAnalysis.rpt"));
                    break;
                default:
                    return;

            }
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
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

			DataClass clsDataClass = new DataClass();
			System.Data.DataTable dt;

			string ReportType = cboReportType.SelectedItem.Text;
            long lngSupplierID = long.Parse(cboSupplier.SelectedValue);
			switch (ReportType)
			{
				case "Posted PO":
					Data.PO clsPO = new Data.PO();
                    if (lngSupplierID==0)
					    dt =  clsDataClass.DataReaderToDataTable(clsPO.List(POStatus.Posted, PostingDateFrom, PostingDateTo));
                    else
                        dt = clsDataClass.DataReaderToDataTable(clsPO.List(POStatus.Posted, lngSupplierID, PostingDateFrom, PostingDateTo));

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
                    if (lngSupplierID == 0)
					    dt =  clsDataClass.DataReaderToDataTable(clsPOReturns.List(POReturnStatus.Posted, PostingDateFrom, PostingDateTo));
                    else
                        dt = clsDataClass.DataReaderToDataTable(clsPOReturns.List(POReturnStatus.Posted, lngSupplierID, PostingDateFrom, PostingDateTo));

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
                    if (lngSupplierID == 0)
					    dt =  clsDataClass.DataReaderToDataTable(clsDebitMemos.List(DebitMemoStatus.Posted, PostingDateFrom, PostingDateTo));
                    else
                        dt = clsDataClass.DataReaderToDataTable(clsDebitMemos.List(DebitMemoStatus.Posted, lngSupplierID, PostingDateFrom, PostingDateTo));

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

            DateTime PostingDateFrom = DateTime.MinValue;
            try
            { PostingDateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["PostingDateFrom"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = PostingDateFrom;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);


            DateTime PostingDateTo = DateTime.MinValue;
            try
            { PostingDateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["PostingDateTo"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = PostingDateTo;
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

        #region Web Control Methods

        protected void imgView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
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
        protected void cmdView_Click(object sender, System.EventArgs e)
        {
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
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        #endregion

        protected void cmdSupplierSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadRecord();
        }
}
}
