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

	public partial  class __ProductInventoryReport : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cboContactName;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "inventory";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "inventory")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
            txtExpiryDate.Text = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd");

            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.DetailedInventory, ReportTypes.DetailedInventory));
            cboReportType.Items.Add(new ListItem(ReportTypes.DetailedInventoryWQtyInOut, ReportTypes.DetailedInventoryWQtyInOut));
            cboReportType.Items.Add(new ListItem(ReportTypes.SummarizedInventory, ReportTypes.SummarizedInventory));
            cboReportType.Items.Add(new ListItem(ReportTypes.SummarizedInventoryWQtyInOut, ReportTypes.SummarizedInventoryWQtyInOut));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.ForPhysicalInventory, ReportTypes.ForPhysicalInventory));
            cboReportType.Items.Add(new ListItem(ReportTypes.TotalStockInventoryDetailed, ReportTypes.TotalStockInventoryDetailed));
            cboReportType.Items.Add(new ListItem(ReportTypes.TotalStockInventorySummarized, ReportTypes.TotalStockInventorySummarized));
            cboReportType.Items.Add(new ListItem(ReportTypes.TotalStockInventoryWSupplier, ReportTypes.TotalStockInventoryWSupplier));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));

            cboMonth.Items.Clear();
            cboMonth.Items.Add(new ListItem("Jan", "01"));
            cboMonth.Items.Add(new ListItem("Feb", "02"));
            cboMonth.Items.Add(new ListItem("Mar", "03"));
            cboMonth.Items.Add(new ListItem("Apr", "04"));
            cboMonth.Items.Add(new ListItem("May", "05"));
            cboMonth.Items.Add(new ListItem("Jun", "06"));
            cboMonth.Items.Add(new ListItem("Jul", "07"));
            cboMonth.Items.Add(new ListItem("Aug", "08"));
            cboMonth.Items.Add(new ListItem("Sep", "09"));
            cboMonth.Items.Add(new ListItem("Oct", "10"));
            cboMonth.Items.Add(new ListItem("Nov", "11"));
            cboMonth.Items.Add(new ListItem("Dec", "12"));
            cboMonth.SelectedIndex = DateTime.Now.Month-1;

            cboYear.Items.Clear();
            for (int year = 2013; year <= DateTime.Now.Year; year++)
            {
                cboYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            cboYear.SelectedIndex = cboYear.Items.Count - 1;

            Branch clsBranch = new Branch();
            clsBranch.GetConnection();

            Int64 UID = Convert.ToInt64(Session["UID"]);
            Security.AccessRights clsAccessRights = new Security.AccessRights(clsBranch.Connection, clsBranch.Transaction);
            if (clsAccessRights.Details(UID, (int)AccessTypes.ReorderReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.ItemsForReOrder, ReportTypes.ItemsForReOrder));

            if (clsAccessRights.Details(UID, (int)AccessTypes.OverStockReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.OverStockItems, ReportTypes.OverStockItems));

            if (clsAccessRights.Details(UID, (int)AccessTypes.InventoryReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.ExpiredInventory, ReportTypes.ExpiredInventory));

            cboReportType.SelectedIndex = 0;

            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            cboBranch.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboBranch.SelectedIndex = 0;

            Contacts clsContact = new Contacts(clsBranch.Connection, clsBranch.Transaction);
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text, 100).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboContact.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsBranch.Connection, clsBranch.Transaction);
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroupCode.Text, "ProductGroupName").DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductGroup.SelectedIndex = 0;

            clsBranch.CommitAndDispose();

            cboProductGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            string strReportType = cboReportType.SelectedValue;

            if (strReportType == ReportTypes.REPORT_SELECTION)
                return null;
            else if (strReportType == ReportTypes.REPORT_SELECTION_SEPARATOR)
                return null;
            //else if (strReportType == ReportTypes.InventoryPerBranch)
            //    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_ProductInventoryReportPerBranch.rpt"));
            else if (strReportType == ReportTypes.DetailedInventory)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReport.rpt"));
            else if (strReportType == ReportTypes.DetailedInventoryWQtyInOut)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportQTYINOUT.rpt"));
            else if (strReportType == ReportTypes.SummarizedInventory)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportSummarized.rpt"));
            else if (strReportType == ReportTypes.SummarizedInventoryWQtyInOut)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportSummarizedQTYINOUT.rpt"));
            else if (strReportType == ReportTypes.ForPhysicalInventory)
            {
                //rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportPhysicalCount.rpt"));
                string javaScript = "window.alert('This is report is obsolete. Please print the report from Closing Inventory Module.')";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
                return null;
            }
            else if (strReportType == ReportTypes.TotalStockInventoryDetailed)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportTotalStock.rpt"));
            else if (strReportType == ReportTypes.TotalStockInventorySummarized)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportTotalStock.rpt"));
            else if (strReportType == ReportTypes.TotalStockInventoryWSupplier)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportTotalStockWithSupplier.rpt"));
            else if (strReportType == ReportTypes.ItemsForReOrder)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportForReorder.rpt"));
            else if (strReportType == ReportTypes.OverStockItems)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportOverStock.rpt"));
            else if (strReportType == ReportTypes.ExpiredInventory)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_inventory/_ProductInventoryReportExpired.rpt"));
            else return null;

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            if (rpt != null)
            {
                SetDataSource(rpt);

                if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
                {
                    string strFileName = Session["UserName"].ToString() + "_inventory";
                    CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
                }
                else
                {
                    CRViewer.ReportSource = rpt;
                    Session["ReportDocument"] = rpt;
                }
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
            string strReportType = cboReportType.SelectedValue;

            Int32 ForReorder = 0;
            Int32 OverStock = 0;
            if (strReportType == ReportTypes.ItemsForReOrder)
                ForReorder = 1;
            else if (strReportType == ReportTypes.OverStockItems)
                OverStock = 1;

            ReportDataset rptds = new ReportDataset();

            #region Search Key
            Int32 intBranchID = Convert.ToInt32(cboBranch.SelectedItem.Value);
            Int64 lngSupplierID = Convert.ToInt32(cboContact.SelectedItem.Value);
            Int64 lngProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
            Int64 lngProductSubGroupID = Convert.ToInt64(cboSubGroup.SelectedItem.Value);
            string stProductCode = txtProductCode.Text;
            #endregion

            string ExpirationDate = Constants.C_DATE_MIN_VALUE_STRING;
            if (strReportType == ReportTypes.ExpiredInventory)
                ExpirationDate = txtExpiryDate.Text;

            int isSummary = intBranchID == 0 ? 1 : 0;
            ProductInventories clsProductInventories = new ProductInventories();
            System.Data.DataTable dt;
            if (cboMonth.SelectedItem.Value == DateTime.Now.Month.ToString("0#") && cboYear.SelectedItem.Value == DateTime.Now.Year.ToString())
            {
                dt = clsProductInventories.ListAsDataTable(BranchID: intBranchID, ProductCode: stProductCode, ProductGroupID: lngProductGroupID, ProductSubGroupID: lngProductSubGroupID, SupplierID: lngSupplierID, isSummary : isSummary, ExpirationDate: ExpirationDate, ForReorder: ForReorder, OverStock: OverStock);
            }else {
                dt = clsProductInventories.ListAsDataTable(Month: int.Parse(cboMonth.SelectedItem.Value), Year: int.Parse(cboYear.SelectedItem.Value.ToString()), BranchID: intBranchID, ProductCode: stProductCode, ProductGroupID: lngProductGroupID, ProductSubGroupID: lngProductSubGroupID, SupplierID: lngSupplierID, isSummary: isSummary, ExpirationDate: ExpirationDate, ForReorder: ForReorder, OverStock: OverStock);
            }
            clsProductInventories.CommitAndDispose();
            
            foreach (DataRow dr in dt.Rows)
            {
                //if (dr[ProductColumnNames.BarCode].ToString() != null && dr[ProductColumnNames.BarCode].ToString() != string.Empty)
                //{
                    DataRow drNew = rptds.Products.NewRow();

                    foreach (DataColumn dc in rptds.Products.Columns)
                    {
                        drNew[dc] = dr[dc.ColumnName];
                    }
                    rptds.Products.Rows.Add(drNew);
                //}
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

            paramField = Report.DataDefinition.ParameterFields["BranchName"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = cboBranch.SelectedItem.Text;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            string strReportType = cboReportType.SelectedValue;

            if (strReportType == ReportTypes.TotalStockInventoryDetailed || strReportType == ReportTypes.TotalStockInventorySummarized || strReportType == ReportTypes.TotalStockInventoryWSupplier)
            {
                paramField = Report.DataDefinition.ParameterFields["InDetails"];
                discreteParam = new ParameterDiscreteValue();
                if (strReportType == ReportTypes.TotalStockInventoryDetailed || strReportType == ReportTypes.TotalStockInventoryWSupplier)
                    discreteParam.Value = true;
                else
                    discreteParam.Value = false;
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

        protected void cboProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
                clsProductSubGroupColumns.ProductSubGroupCode = true;
                clsProductSubGroupColumns.ProductSubGroupName = true;

                ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
                clsSearchKeys.ProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);
                clsSearchKeys.ProductSubGroupCode = txtSubGroupCode.Text;

                ProductSubGroup clsSubGroup = new ProductSubGroup();
                cboSubGroup.DataTextField = "ProductSubGroupName";
                cboSubGroup.DataValueField = "ProductSubGroupID";
                cboSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0);
                cboSubGroup.DataBind();
                cboSubGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
                if (cboSubGroup.Items.Count > 1 && txtSubGroupCode.Text.Trim() != string.Empty) cboSubGroup.SelectedIndex = 1; else cboSubGroup.SelectedIndex = 0;
                clsSubGroup.CommitAndDispose();

            }
            catch { }
        }

        protected void imgSubGroupCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            cboProductGroup_SelectedIndexChanged(null, null);
        }

        protected void imgProductGroupCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ProductGroup clsProductGroup = new ProductGroup();
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroupCode.Text).DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProductGroup.Items.Count > 1 && txtProductGroupCode.Text.Trim() != string.Empty) cboProductGroup.SelectedIndex = 1; else cboProductGroup.SelectedIndex = 0;
            clsProductGroup.CommitAndDispose();

            cboProductGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
        }

        protected void imgContactCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Contacts clsContact = new Contacts();
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text, 100).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContact.Items.Count > 1 && txtContactCode.Text.Trim() != string.Empty) cboContact.SelectedIndex = 1; else cboContact.SelectedIndex = 0;
            clsContact.CommitAndDispose();
        }

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strReportType = cboReportType.SelectedValue;

            if (strReportType == ReportTypes.ExpiredInventory)
            {
                holderExpiry.Visible = true;
            }
            else
            {
                holderExpiry.Visible = false;
            }
        }

        #endregion
    }
}
