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

	public partial  class __Default : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "reportsdefault";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "reportsdefault")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
            if (Request.QueryString["task"] != null)
            {
                string strUnparsedTask = Request.QueryString["task"].ToString();
                lblReferrer.ToolTip = strUnparsedTask;

                if (strUnparsedTask.ToLower() == "transaction")
                {
                    //stHeading = "View Transaction Report";
                }
                else
                {
                    string task = Common.Decrypt(strUnparsedTask, Session.SessionID);
                    switch (task)
                    {
                        case "products":
                            LoadOptionsProducts(); holderProductGroup.Visible = true;
                            break;
                        case "producthistory":
                            LoadOptionsProductHistory(); holderProductHistory.Visible = true;
                            break;
                    }
                }
            }
			
		}
        private void LoadOptionsProducts()
        {
            ProductGroup clsProductGroup = new ProductGroup();
            cboGroup.DataTextField = "ProductGroupName";
            cboGroup.DataValueField = "ProductGroupID";
            cboGroup.DataSource = clsProductGroup.ListAsDataTable();
            cboGroup.DataBind();
            cboGroup.Items.Insert(0, new ListItem(Constants.ALL,Constants.ZERO_STRING));
            cboGroup.SelectedIndex = 0;

            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupDetails clsSearchKey = new ProductSubGroupDetails();

            ProductSubGroup clsSubGroup = new ProductSubGroup(clsProductGroup.Connection, clsProductGroup.Transaction);
            cboSubGroup.DataTextField = "ProductSubGroupName";
            cboSubGroup.DataValueField = "ProductSubGroupID";
            cboSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKey, 0, System.Data.SqlClient.SortOrder.Ascending, 0, ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder.Ascending);
            cboSubGroup.DataBind();
            cboSubGroup.Items.Insert(0, new ListItem(Constants.ALL,Constants.ZERO_STRING));
            cboSubGroup.SelectedIndex = 0;
            clsProductGroup.CommitAndDispose();
        }
        private void LoadOptionsProductHistory()
        {
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            DataClass clsDataClass = new DataClass();

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";

            try
            {
                if (Request.QueryString["productcode"].ToString() != null)
                    txtProductCode.Text = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID);
            }
            catch { }
            if (!string.IsNullOrEmpty(txtProductCode.Text))
            {
                string stSearchKey = txtProductCode.Text;
                cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.Search(stSearchKey, "ProductCode", SortOption.Ascending));
            }
            else
            {
                cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.List("ProductCode", SortOption.Ascending)).DefaultView;
            }
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0)
                cboProductCode.Items.Add(new ListItem("No product", "0"));

            cboProductCode.SelectedIndex = 0;

            try
            {
                if (Request.QueryString["productcode"].ToString() != null && cboProductCode.SelectedItem.Value != "0")
                    GenerateHTML();
            }
            catch { }
        }

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            if (!string.IsNullOrEmpty(lblReferrer.ToolTip))
            {
                string strUnparsedTask = lblReferrer.ToolTip;
                string task = strUnparsedTask;

                if (strUnparsedTask.ToLower() != "transaction")
                {
                    task = Common.Decrypt(strUnparsedTask, Session.SessionID);
                    switch (task)
                    {
                        case "products":
                            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/ProductsReport.rpt"));
                            break;
                        case "producthistory":
                            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/ProductHistoryReport.rpt"));
                            break;
                    }
                }
                
            }

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            if (string.IsNullOrEmpty(lblReferrer.ToolTip)) return;

            ReportDocument rpt = getReportDocument();

            string strUnparsedTask = lblReferrer.ToolTip;
            string task = strUnparsedTask;
            if (strUnparsedTask.ToLower() != "transaction")
                task = Common.Decrypt(strUnparsedTask, Session.SessionID);

            SetDataSource(rpt, task);
            CRViewer.ReportSource = rpt;
            Session["ReportDocument"] = rpt;

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_products";
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

		private void SetDataSource(ReportDocument pvtReport, string pvtTask)
		{
            switch (pvtTask)
            {
                /*************************************************Products******************************************/
                case "products":
                    SetDataSourceProducts(pvtReport);
                    break;
                case "producthistory":
                    SetDataSourceProductHistory(pvtReport);
                    break;
                /*************************************************Products******************************************/
            }
            SetParameters(pvtReport);
		}
        private void SetDataSourceProducts(ReportDocument Report)
        {
            ReportDataset rptds = new ReportDataset();
            string ProductGroupName = string.Empty;
            if (cboGroup.SelectedItem.Value != Constants.ZERO_STRING) ProductGroupName = cboGroup.SelectedItem.Text;
            string SubGroupName = string.Empty;
            if (cboSubGroup.SelectedItem.Value != Constants.ZERO_STRING) SubGroupName = cboSubGroup.SelectedItem.Text;

            Products clsProduct = new Products();
            System.Data.DataTable dtProductInventoryReport = clsProduct.InventoryReport(ProductGroupName, SubGroupName, txtProductCode.Text);

            //ProductVariationsMatrix clsMatrix = new ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
            //System.Data.DataTable dtMatrixInventoryReport = clsMatrix.InventoryReport(ProductGroupName, SubGroupName, txtProductCode.Text);
            clsProduct.CommitAndDispose();

            foreach (System.Data.DataRow dr in dtProductInventoryReport.Rows)
            {
                DataRow drNew = rptds.Products.NewRow();

                foreach (DataColumn dc in rptds.Products.Columns)
                    drNew[dc] = dr[dc.ColumnName];

                rptds.Products.Rows.Add(drNew);
            }

            Report.SetDataSource(rptds);

        }
        private void SetDataSourceProductHistory(ReportDocument Report)
        {
            ReportDataset rptds = new ReportDataset();

            DateTime DateFrom = DateTime.MinValue;
            try
            { DateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text); }
            catch { }
            DateTime DateTo = DateTime.MinValue;
            try
            { DateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text); }
            catch { }

            StockItem clsStockItem = new StockItem();
            MySqlDataReader myreader = clsStockItem.ProductHistoryReport(Convert.ToInt64(cboProductCode.SelectedItem.Value), DateFrom, DateTo);
            clsStockItem.CommitAndDispose();

            while (myreader.Read())
            {
                DataRow drStockItem = rptds.ProductHistory.NewRow();

                foreach (DataColumn dc in rptds.ProductHistory.Columns)
                    drStockItem[dc] = myreader[dc.ColumnName];

                rptds.ProductHistory.Rows.Add(drStockItem);
            }

            Report.SetDataSource(rptds);
        }

		#endregion

		#region SetParameters
        private void SetParameters(ReportDocument pvtReport)
		{
			ParameterFieldDefinition paramField;
			ParameterValues currentValues;
			ParameterDiscreteValue discreteParam;

            paramField = pvtReport.DataDefinition.ParameterFields["CompanyName"];
			discreteParam = new ParameterDiscreteValue();
			discreteParam.Value = CompanyDetails.CompanyName;
			currentValues = new ParameterValues();
			currentValues.Add(discreteParam);
			paramField.ApplyCurrentValues(currentValues);

            paramField = pvtReport.DataDefinition.ParameterFields["PrintedBy"];
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
        protected void cboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupCode = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
            clsSearchKeys.ProductGroupID = long.Parse(cboGroup.SelectedItem.Value);

            ProductSubGroup clsSubGroup = new ProductSubGroup();
            cboSubGroup.DataTextField = "ProductSubGroupName";
            cboSubGroup.DataValueField = "ProductSubGroupID";
            cboSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0, System.Data.SqlClient.SortOrder.Ascending, 0, ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder.Ascending);
            cboSubGroup.DataBind();
            cboSubGroup.Items.Insert(0, new ListItem(Constants.ALL,Constants.ZERO_STRING));
            cboSubGroup.SelectedIndex = 0;
            clsSubGroup.CommitAndDispose();
        }
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DataClass clsDataClass = new DataClass();

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";

            if (!string.IsNullOrEmpty(txtProductCode.Text))
            {
                string stSearchKey = txtProductCode.Text;
                cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.Search(stSearchKey, "ProductCode", SortOption.Ascending));
            }
            else
            {
                cboProductCode.DataSource = clsDataClass.DataReaderToDataTable(clsProduct.List("ProductCode", SortOption.Ascending)).DefaultView;
            }
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0)
                cboProductCode.Items.Add(new ListItem("No product", "0"));

            cboProductCode.SelectedIndex = 0;
        }
        protected void cboProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtProductCode.Text = cboProductCode.SelectedItem.Text;
            }
            catch { }
        }
        
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        #endregion

    }
}
