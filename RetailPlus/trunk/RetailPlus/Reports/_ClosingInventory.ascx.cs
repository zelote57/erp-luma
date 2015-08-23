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

	public partial  class __ClosingInventory : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "closinginv";

                if (Request.QueryString["refno"] != null)
                    GeneratePDF();

                if (Request.QueryString["type"] != null)
                    GeneratePDF();
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "closinginv")
                    try { CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"]; } catch { }
            }
        }

		private void LoadOptions()
		{
            txtStartTransactionDate.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtEndTransactionDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            Contacts clsContact = new Contacts();
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(Limit: 100).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboContact.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsContact.Connection, clsContact.Transaction);
            cboGroup.DataTextField = "ProductGroupName";
            cboGroup.DataValueField = "ProductGroupID";
            cboGroup.DataSource = clsProductGroup.ListAsDataTable();
            cboGroup.DataBind();
            cboGroup.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT,Constants.ZERO_STRING));
            cboGroup.SelectedIndex = 0;

            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns() { ColumnsNameID = true };

            ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsContact.Connection, clsContact.Transaction);
            cboSubGroup.DataTextField = "ProductSubGroupName";
            cboSubGroup.DataValueField = "ProductSubGroupID";
            cboSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns);
            cboSubGroup.DataBind();
            cboSubGroup.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            cboSubGroup.SelectedIndex = 0;

            Data.Inventory clsInventory = new Data.Inventory(clsContact.Connection, clsContact.Transaction);
            cboInventoryNo.DataTextField = "ReferenceNo";
            cboInventoryNo.DataValueField = "PostingReference";
            cboInventoryNo.DataSource = clsInventory.ClosingInventoryReferenceNos(Convert.ToDateTime(txtStartTransactionDate.Text), Convert.ToDateTime(txtEndTransactionDate.Text));
            cboInventoryNo.DataBind();
            cboInventoryNo.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.PLEASE_SELECT + DateTime.MinValue.ToString("yyyy-MM-dd")));
            cboInventoryNo.SelectedIndex = 0;

            clsContact.CommitAndDispose();

            lblType.Text = ""; lblType.ToolTip = "";
            if (Request.QueryString["type"] != null)
            {
                lblType.Text = Common.Decrypt(Request.QueryString["type"].ToString(), Session.SessionID);
            }
            if (Request.QueryString["typedet"] != null)
            {
                lblType.ToolTip = Common.Decrypt(Request.QueryString["typedet"].ToString(), Session.SessionID);
            }
            lblBranchID.Text = "0";
            if (Request.QueryString["branchid"] != null)
            {
                lblBranchID.Text = Common.Decrypt(Request.QueryString["branchid"].ToString(), Session.SessionID);
            }

            if (Request.QueryString["refno"] != null)
            {
                string strRefNo = "refno";
                strRefNo = Common.Decrypt(Request.QueryString["refno"].ToString(), Session.SessionID);
                cboInventoryNo.SelectedIndex = cboInventoryNo.Items.IndexOf(cboInventoryNo.Items.FindByText(strRefNo));
            }

            if (Request.QueryString["prdgrpid"] != null)
            {
                string strPrdGrpID = "prdgrpid";
                strPrdGrpID = Common.Decrypt(Request.QueryString["prdgrpid"].ToString(), Session.SessionID);
                cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(strPrdGrpID));
            }

            if (Request.QueryString["contactid"] != null)
            {
                string strContactID = "contactid";
                strContactID = Common.Decrypt(Request.QueryString["contactid"].ToString(), Session.SessionID);
                cboContact.SelectedIndex = cboContact.Items.IndexOf(cboContact.Items.FindByValue(strContactID));
            }
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();
            rpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal;
            rpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;

            switch (lblType.Text)
            {
                case "invcount":
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_ClosingInventoryCount.rpt"));
                    break;
                default:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_ClosingInventory.rpt"));
                    break;
            }

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_closinginventory";
                CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
            }
            else
            {
                CRViewer.ReportSource = rpt;
                Session["ReportDocument"] = rpt;
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
            ReportDataset rptds = new ReportDataset();

            string ContactCode = string.Empty;
            if (cboContact.SelectedItem.Value != Constants.ZERO_STRING) ContactCode = cboContact.SelectedItem.Text;

            string ProductGroupName = string.Empty;
            if (cboGroup.SelectedItem.Value != Constants.ZERO_STRING) ProductGroupName = cboGroup.SelectedItem.Text;

            string ProductSubGroupName = string.Empty;
            if (cboSubGroup.SelectedItem.Value != Constants.ZERO_STRING) ProductSubGroupName = cboSubGroup.SelectedItem.Text;

            System.Data.DataTable dt = null;
            if (lblType.Text == "invcount")
            {
                Int64 lngSupplierID = Convert.ToInt64(cboContact.SelectedItem.Value);

                Int64 lngProductgroupID = Convert.ToInt64(cboGroup.SelectedItem.Value);

                Int64 lngProductSubGroupID = Convert.ToInt64(cboSubGroup.SelectedItem.Value);

                ProductInventories clsProductInventories = new ProductInventories();

                dt = clsProductInventories.ListAsDataTable(BranchID: int.Parse(lblBranchID.Text), SupplierID: lngSupplierID, ProductGroupID: lngProductgroupID, ProductSubGroupID: lngProductSubGroupID, clsProductListFilterType: ProductListFilterType.ShowActiveOnly, SortField: "ProductCode ASC, MatrixDescription ASC, BarCode1", SortOrder: SortOption.Desscending);

                //Contacts clsContacts = new Contacts(clsProductInventories.Connection, clsProductInventories.Transaction);
                //ContactDetails clsContactDetails = clsContacts.Details(lngSupplierID);

                clsProductInventories.CommitAndDispose();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    DataRow drInventory = rptds.ProductInventory.NewRow();

                    foreach (DataColumn dc in rptds.ProductInventory.Columns)
                        drInventory[dc] = dr[dc.ColumnName];

                    rptds.ProductInventory.Rows.Add(drInventory);
                }
            }
            else
            {
                Data.Inventory clsInventory = new Data.Inventory();
                dt = clsInventory.DataList(cboInventoryNo.SelectedItem.Text, chkIncludeShortOverProducts.Checked, long.Parse(cboContact.SelectedItem.Value), long.Parse(cboGroup.SelectedItem.Value), SortField: "InventoryID", SortOrder: SortOption.Ascending);
                clsInventory.CommitAndDispose();

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    DataRow drInventory = rptds.Inventory.NewRow();

                    foreach (DataColumn dc in rptds.Inventory.Columns)
                        drInventory[dc] = dr[dc.ColumnName];

                    rptds.Inventory.Rows.Add(drInventory);
                }
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

            paramField = Report.DataDefinition.ParameterFields["ClosingInventoryID"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cboInventoryNo.SelectedItem.Text;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["PostingDate"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = Convert.ToDateTime(cboInventoryNo.SelectedItem.Value.Replace(cboInventoryNo.SelectedItem.Text, "")).ToString("yyyy-MM-dd");
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ProductGroupName"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cboGroup.SelectedItem.Value == Constants.ZERO_STRING ? "ALL" : cboGroup.SelectedItem.Text;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ContactCode"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cboContact.SelectedItem.Value == Constants.ZERO_STRING ? "ALL" : cboContact.SelectedItem.Text;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            if (lblType.Text == "invcount")
            {
                paramField = Report.DataDefinition.ParameterFields["TypeDet"];
                discreteParam = new ParameterDiscreteValue();
                discreteParam.Value = lblType.ToolTip; // byprod means by productdesc
                currentValues = new ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);
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
        
        protected void cmdView_Click(object sender, System.EventArgs e)
		{
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
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Data.Inventory clsInventory = new Data.Inventory();
            cboInventoryNo.DataTextField = "ReferenceNo";
            cboInventoryNo.DataValueField = "PostingReference";
            cboInventoryNo.DataSource = clsInventory.ClosingInventoryReferenceNos(Convert.ToDateTime(txtStartTransactionDate.Text), Convert.ToDateTime(txtEndTransactionDate.Text));
            cboInventoryNo.DataBind();
            cboInventoryNo.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.PLEASE_SELECT + DateTime.MinValue.ToString("yyyy-MM-dd")));
            cboInventoryNo.SelectedIndex = 0;

            clsInventory.CommitAndDispose();
        }

        protected void cboInventoryNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cboContact.SelectedIndex = 0;
            cboGroup.SelectedIndex = 0;

            Data.Inventory clsInventory = new Data.Inventory();

            System.Data.DataTable dt = clsInventory.ClosingInventoryContactCodes(cboInventoryNo.SelectedItem.Text);
            if (dt.Rows.Count == 1)
            {
                cboContact.SelectedIndex = cboContact.Items.IndexOf(cboContact.Items.FindByValue(dt.Rows[0]["ContactID"].ToString()));
            }

            dt = clsInventory.ClosingInventoryGroupCodes(cboInventoryNo.SelectedItem.Text);
            if (dt.Rows.Count == 1)
            {
                cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(dt.Rows[0]["ProductGroupID"].ToString()));
            }

            clsInventory.CommitAndDispose();
        }

        #endregion
    }
}
