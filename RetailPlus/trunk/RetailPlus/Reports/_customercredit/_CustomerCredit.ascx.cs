using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __CustomerCredit : System.Web.UI.UserControl
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
            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.CustomerCredit, ReportTypes.CustomerCredit));
            cboReportType.Items.Add(new ListItem(ReportTypes.CustomerCreditBill, ReportTypes.CustomerCreditBill));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.CustomerCreditListWCredit, ReportTypes.CustomerCreditListWCredit));
            cboReportType.Items.Add(new ListItem(ReportTypes.CustomerCreditListLatestBill, ReportTypes.CustomerCreditListLatestBill));
            cboReportType.SelectedIndex = 0;

            Customer clsCustomer = new Customer();
			cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContactName.Items.Count > 1 && txtContactName.Text.Trim() != string.Empty) cboContactName.SelectedIndex = 1; else cboContactName.SelectedIndex = 0;

            ContactGroup clsContactGroup = new ContactGroup(clsCustomer.Connection, clsCustomer.Transaction);
            cboCustomerGroup.DataTextField = "ContactGroupName";
            cboCustomerGroup.DataValueField = "ContactGroupCode";
            cboCustomerGroup.DataSource = clsContactGroup.ListAsDataTable(ContactGroupCategory.CUSTOMER);
            cboCustomerGroup.DataBind();
            cboCustomerGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboCustomerGroup.Items.Count > 1 && txtCustomerGroup.Text.Trim() != string.Empty) cboCustomerGroup.SelectedIndex = 1; else cboCustomerGroup.SelectedIndex = 0;

            clsCustomer.CommitAndDispose();

		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            string strReportType = cboReportType.SelectedValue;

            if (strReportType == ReportTypes.REPORT_SELECTION)
                return null;
            else if (strReportType == ReportTypes.REPORT_SELECTION_SEPARATOR)
                return null;
            else if (strReportType == ReportTypes.CustomerCredit)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_customercredit/_CustomerCredit.rpt"));
            else if (strReportType == ReportTypes.CustomerCreditBill)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_customercredit/_CustomerCreditBill.rpt"));
            else if (strReportType == ReportTypes.CustomerCreditListWCredit)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_customercredit/_CustomerCreditListWCredit.rpt"));
            else if (strReportType == ReportTypes.CustomerCreditListLatestBill)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_customercredit/_CustomerCreditListLatestBill.rpt"));
            else return null;

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

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
            string strFileName = "cuscred_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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
            ReportDocument rpt = getReportDocument();
            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;
        }

        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
			ReportDataset rptds = new ReportDataset();
            DataTable dt;

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.CustomerCredit:
                    #region Customer Credit
                    Customer clsCustomer = new Customer();
                    dt = clsCustomer.CustomersDataTable(cboContactName.SelectedItem.Text);

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.CustomerDetails.NewRow();

                        foreach (DataColumn dc in rptds.CustomerDetails.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.CustomerDetails.Rows.Add(drNew);
                    }

                    SalesTransactions clsSalesTransactions = new SalesTransactions(clsCustomer.Connection, clsCustomer.Transaction);
                    dt = clsSalesTransactions.ListForPaymentDataTable(Convert.ToInt64(cboContactName.SelectedItem.Value));
                    clsCustomer.CommitAndDispose();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.CustomerCredit.NewRow();

                        foreach (DataColumn dc in rptds.CustomerCredit.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.CustomerCredit.Rows.Add(drNew);
                    }

                    break;
                    #endregion
                
                case ReportTypes.CustomerCreditListWCredit:
                    #region  Customers List With Credit
                    Contacts clsContact = new Contacts();
                    MySqlDataReader myreader = clsContact.CustomerAdvanceSearch(null, cboContactName.Text, cboCustomerGroup.SelectedValue, true, "ContactID", SortOption.Ascending);
			        while(myreader.Read())
			        {
				        DataRow drNew = rptds.Contacts.NewRow();
				
				        foreach (DataColumn dc in rptds.Contacts.Columns)
					        drNew[dc] = myreader[dc.ColumnName]; 
				
				        rptds.Contacts.Rows.Add(drNew);
			        }
                    myreader.Close();
                    clsContact.CommitAndDispose();
                    break;
                    #endregion
                
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

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.CustomerCredit:
                    #region Customer Credit
                    

                    break;
                    #endregion

                case ReportTypes.CustomerCreditListWCredit:
                    #region  Customers List With Credit
                    paramField = Report.DataDefinition.ParameterFields["ReportName"];
			        discreteParam = new ParameterDiscreteValue();
			        discreteParam.Value = cboCustomerGroup.SelectedItem.Text + " w/ Credit";
			        currentValues = new ParameterValues();
			        currentValues.Add(discreteParam);
			        paramField.ApplyCurrentValues(currentValues);

                    break;
                    #endregion

            }
		}

		#endregion

		#region Web Form Designer generated code
		
        override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}

		#endregion

        #region Web Control Methods

        protected void cmdView_Click(object sender, System.EventArgs e)
		{
            //if (cboContactName.SelectedItem.Value == "0")
            //{
            //    string stScript = "<Script>";
            //    stScript += "window.alert('Please select customer to view credit records for printing.')";
            //    stScript += "</Script>";
            //    Response.Write(stScript);
            //    return;
            //}
            //fraViewer.Visible = true;

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

        protected void imgContactNameSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Customer clsCustomer = new Customer();
            cboContactName.DataTextField = "ContactName";
            cboContactName.DataValueField = "ContactID";
            cboContactName.DataSource = clsCustomer.CustomersDataTable(txtContactName.Text, 0, false, "ContactName", SortOption.Ascending);
            cboContactName.DataBind();
            cboContactName.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContactName.Items.Count > 1 && txtContactName.Text.Trim() != string.Empty) cboContactName.SelectedIndex = 1; else cboContactName.SelectedIndex = 0;
            clsCustomer.CommitAndDispose();
        }

        protected void cboContactName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdView_Click(null, null);
        }

        protected void imgCustomerGroupSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ContactGroup clsContactGroup = new ContactGroup();
            cboCustomerGroup.DataTextField = "ContactGroupName";
            cboCustomerGroup.DataValueField = "ContactGroupCode";
            cboCustomerGroup.DataSource = clsContactGroup.ListAsDataTable(ContactGroupCategory.CUSTOMER, txtCustomerGroup.Text.TrimEnd());
            cboCustomerGroup.DataBind();
            cboCustomerGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboCustomerGroup.Items.Count > 1 && txtCustomerGroup.Text.Trim() != string.Empty) cboCustomerGroup.SelectedIndex = 1; else cboCustomerGroup.SelectedIndex = 0;
            clsContactGroup.CommitAndDispose();
        }

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReportType.SelectedValue == ReportTypes.CustomerCreditListWCredit)
                holderCustomerGroup.Visible = true;
            else
                holderCustomerGroup.Visible = false;
        }

        #endregion

    }
}
