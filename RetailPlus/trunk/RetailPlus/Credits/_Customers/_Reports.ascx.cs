using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AceSoft.RetailPlus.Credits._Customers
{
	public partial  class __Reports : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
                if (Request.QueryString["reporttype"] != null)
                    lblReportType.Text = Common.Decrypt(Request.QueryString["reporttype"].ToString(), Session.SessionID);

                LoadOptions();
                if (Request.QueryString["target"] != null)
                    GeneratePDF();
                else
                    GenerateHTML();

                Session["ReportDocument"] = null;
                Session["ReportType"] = "credits";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "credits")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.CREDITS_Purchases, ReportTypes.CREDITS_Purchases));
            cboReportType.Items.Add(new ListItem(ReportTypes.CREDITS_Payments, ReportTypes.CREDITS_Payments));
            cboReportType.Items.Add(new ListItem(ReportTypes.CREDITS_CreditorsLedgerSummary, ReportTypes.CREDITS_CreditorsLedgerSummary));
            cboReportType.SelectedIndex = 0;

            if (Request.QueryString["reporttype"] != null)
            {
                lblReportType.Text = Common.Decrypt(Request.QueryString["reporttype"].ToString(), Session.SessionID);

                switch (lblReportType.Text.ToLower())
                {
                    case "purchases": cboReportType.SelectedIndex = cboReportType.Items.IndexOf(cboReportType.Items.FindByValue(ReportTypes.CREDITS_Purchases)); break;
                    case "payments": cboReportType.SelectedIndex = cboReportType.Items.IndexOf(cboReportType.Items.FindByValue(ReportTypes.CREDITS_Payments)); break;
                    case "ledger": cboReportType.SelectedIndex = cboReportType.Items.IndexOf(cboReportType.Items.FindByValue(ReportTypes.CREDITS_CreditorsLedgerSummary)); break;
                    default:
                        break;
                }
            }

            Data.CardType clsCardType = new Data.CardType();
            cboCreditType.Items.Clear();
            cboCreditType.DataTextField = "CardTypeCode";
            cboCreditType.DataValueField = "CardTypeID";
            cboCreditType.DataSource = clsCardType.ListAsDataTable(new CardTypeDetails(CreditCardTypes.Internal, false)).DefaultView;
            cboCreditType.DataBind();
            cboCreditType.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboCreditType.SelectedIndex = cboCreditType.Items.Count >= 2 ? 1 : 0;


            Branch clsBranch = new Branch(clsCardType.Connection, clsCardType.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            cboBranch.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboBranch.SelectedIndex = 0;

            Billing clsBilling = new Billing(clsCardType.Connection, clsCardType.Transaction);
            cboBillingDate.DataTextField = "BillingDate";
            cboBillingDate.DataValueField = "BillingDate";
            cboBillingDate.DataSource = clsBilling.ListBillingDateAsDataTable(CreditType.Individual, CreditCardTypeID: Int16.Parse(cboCreditType.SelectedItem.Value)).DefaultView;
            cboBillingDate.DataBind();
            cboBillingDate.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.C_DATE_MIN_VALUE_STRING));
            cboBillingDate.SelectedIndex = cboBillingDate.Items.Count >= 2 ? 1 : 0;

            clsCardType.CommitAndDispose();

            txtTrxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtTrxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            cboReportType_SelectedIndexChanged(null, null);
        }

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            switch (cboReportType.SelectedItem.Value)
            {
                case ReportTypes.CREDITS_Purchases:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_credits/woutguarantor/purchaseswoutg.rpt"));
                    break;
                case ReportTypes.CREDITS_Payments:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_credits/woutguarantor/paymentswoutg.rpt"));
                    break;
                case ReportTypes.CREDITS_CreditorsLedgerSummary:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_credits/woutguarantor/creditledgerwoutg.rpt"));
                    break;
                default:
                    break;
            }
            
            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_credit";

                switch (cboReportType.SelectedItem.Value)
                {
                    case ReportTypes.CREDITS_Purchases:
                        strFileName += "purchases";
                        break;
                    case ReportTypes.CREDITS_Payments:
                        strFileName += "payments";
                        break;
                    case ReportTypes.CREDITS_CreditorsLedgerSummary:
                        strFileName += "ledger";
                        break;
                    default:
                        break;
                }

                CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
            }
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
        private void GenerateHTML() { Export(ExportFormatType.HTML40); }
        #endregion

		#region SetDataSource

		private void SetDataSource(ReportDocument Report)
		{
            DateTime dteRetValue = Constants.C_DATE_MIN_VALUE;
            ReportDataset rptds = new ReportDataset();
            Data.Contacts clsContacts;
            System.Data.DataTable dt;

            switch (cboReportType.SelectedItem.Value)
            {
                case ReportTypes.CREDITS_Purchases:
                    #region purchases

                    CreditPaymentDetails clsCreditPaymentDetails = new CreditPaymentDetails();
                    clsCreditPaymentDetails.BranchDetails = new BranchDetails() { BranchID = Int32.Parse(cboBranch.SelectedItem.Value) };
                    clsCreditPaymentDetails.TerminalNo = txtTerminalNo.Text.Trim();
                    clsCreditPaymentDetails.PurchaseDateFrom = DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : Constants.C_DATE_MIN_VALUE;
                    clsCreditPaymentDetails.PurchaseDateTo = DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : Constants.C_DATE_MIN_VALUE;
                    clsCreditPaymentDetails.CreditType = CreditType.Individual;
                    clsCreditPaymentDetails.CreditCardTypeID = Int16.Parse(cboCreditType.SelectedItem.Value);
                    clsCreditPaymentDetails.CreditorLastnameFrom = txtLastNameFrom.Text;
                    clsCreditPaymentDetails.CreditorLastnameTo = txtLastNameTo.Text;

                    clsContacts = new Data.Contacts();
                    dt = clsContacts.CreditPurchasesAsDataTable(clsCreditPaymentDetails, "cci.CreditCardNo");
                    clsContacts.CommitAndDispose();

			        foreach(System.Data.DataRow dr in dt.Rows)
			        {
				        DataRow drNew = rptds.ContactCreditPurchases.NewRow();

                        foreach (DataColumn dc in rptds.ContactCreditPurchases.Columns)
					        drNew[dc] = "" + dr[dc.ColumnName];

                        rptds.ContactCreditPurchases.Rows.Add(drNew);
			        }
                    #endregion
                    break;

                case ReportTypes.CREDITS_Payments:
                    #region payments

                    CreditPaymentCashDetails clsCreditPaymentCashDetails = new CreditPaymentCashDetails();
                    clsCreditPaymentCashDetails.BranchDetails = new BranchDetails() { BranchID = Int32.Parse(cboBranch.SelectedItem.Value) };
                    clsCreditPaymentCashDetails.TerminalNo = txtTerminalNo.Text.Trim();
                    clsCreditPaymentCashDetails.PaymentDateFrom = DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : Constants.C_DATE_MIN_VALUE;
                    clsCreditPaymentCashDetails.PaymentDateTo = DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : Constants.C_DATE_MIN_VALUE;
                    clsCreditPaymentCashDetails.CreditType = CreditType.Individual;
                    clsCreditPaymentCashDetails.CreditCardTypeID = Int16.Parse(cboCreditType.SelectedItem.Value);
                    clsCreditPaymentCashDetails.CreditorLastnameFrom = txtLastNameFrom.Text;
                    clsCreditPaymentCashDetails.CreditorLastnameTo = txtLastNameTo.Text;

                    clsContacts = new Data.Contacts();
                    dt = clsContacts.CreditPaymentCashAsDataTable(clsCreditPaymentCashDetails, "cci.CreditCardNo");
                    clsContacts.CommitAndDispose();

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.ContactCreditPaymentCash.NewRow();

                        foreach (DataColumn dc in rptds.ContactCreditPaymentCash.Columns)
                            drNew[dc] = "" + dr[dc.ColumnName];

                        rptds.ContactCreditPaymentCash.Rows.Add(drNew);
                    }
                    #endregion
                    break;

                case ReportTypes.CREDITS_CreditorsLedgerSummary:
                    #region ledger summary
                    Data.Billing clsBilling = new Data.Billing();
                    dt = clsBilling.ListAsDataTable(GuarantorID: 0, CreditCardTypeID: Int16.Parse(cboCreditType.SelectedItem.Value), CreditType: CreditType.Individual, BillingDate: DateTime.Parse(cboBillingDate.SelectedItem.Value), CheckIsBillPrinted: false,  SortField: "GUA.ContactName, CUS.ContactName", SortOrder: System.Data.SqlClient.SortOrder.Descending);
                    clsBilling.CommitAndDispose();

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        System.Data.DataRow drNew = rptds.CreditBillHeader.NewRow();

                        foreach (System.Data.DataColumn dc in rptds.CreditBillHeader.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.CreditBillHeader.Rows.Add(drNew);
                    }
                    #endregion
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
            DateTime dteRetValue = Constants.C_DATE_MIN_VALUE;

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

            paramField = Report.DataDefinition.ParameterFields["CreditCardTypeName"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cboCreditType.SelectedItem.Text;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            switch (cboReportType.SelectedItem.Value)
            {
                case ReportTypes.CREDITS_Purchases:
                    #region purchases
                    paramField = Report.DataDefinition.ParameterFields["PurchaseStartDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = DateTime.TryParse(txtTrxStartDate.Text, out dteRetValue) ? dteRetValue.ToString("yyyy-MM-dd") : Constants.C_DATE_MIN_VALUE.ToString("yyyy-MM-dd");
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["PurchaseEndDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = DateTime.TryParse(txtTrxEndDate.Text, out dteRetValue) ? dteRetValue.ToString("yyyy-MM-dd") : Constants.C_DATE_MIN_VALUE.ToString("yyyy-MM-dd");
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    #endregion
                    break;

                case ReportTypes.CREDITS_Payments:
                    #region payments
                    paramField = Report.DataDefinition.ParameterFields["PaymentStartDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = DateTime.TryParse(txtTrxStartDate.Text, out dteRetValue) ? dteRetValue.ToString("yyyy-MM-dd") : Constants.C_DATE_MIN_VALUE.ToString("yyyy-MM-dd");
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["PaymentEndDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = DateTime.TryParse(txtTrxEndDate.Text, out dteRetValue) ? dteRetValue.ToString("yyyy-MM-dd") : Constants.C_DATE_MIN_VALUE.ToString("yyyy-MM-dd");
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    #endregion
                    break;

                case ReportTypes.CREDITS_CreditorsLedgerSummary:
                    #region ledger summary

                    Data.CreditBills clsCreditBills = new Data.CreditBills();
                    Data.CreditBillDetails clsCreditBillDetails = clsCreditBills.Details(CreditType.Individual, DateTime.Parse(cboBillingDate.SelectedItem.Value), Int16.Parse(cboCreditType.SelectedItem.Value));
                    clsCreditBills.CommitAndDispose();

                    paramField = Report.DataDefinition.ParameterFields["CreditPurcStartDateToProcess"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsCreditBillDetails.CreditPurcStartDateToProcess;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["CreditPurcEndDateToProcess"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsCreditBillDetails.CreditPurcEndDateToProcess;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["PaymentDueDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsCreditBillDetails.CreditPaymentDueDate;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["BillingDate"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = DateTime.Parse(cboBillingDate.SelectedItem.Value);
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["ShowDetails"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = true;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    
                    #endregion
                    break;
                default:
                    break;
            }
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

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboReportType.SelectedItem.Text)
            {
                case ReportTypes.CREDITS_Purchases:
                    lblTrxStartDate.Text = "Purchase Date From";
                    lblTrxEndDate.Text = "Purchase Date To";
                    divDates.Visible = true; divBilingDate.Visible = false;
                    break;
                case ReportTypes.CREDITS_Payments:
                    divDates.Visible = true; divBilingDate.Visible = false;
                    lblTrxStartDate.Text = "Payment Date From";
                    lblTrxEndDate.Text = "Payment Date To";
                    break;
                case ReportTypes.CREDITS_CreditorsLedgerSummary:
                    divDates.Visible = false; divBilingDate.Visible = true;
                    break;
                default:
                    return;

            }
        }


        #endregion
    }
}
