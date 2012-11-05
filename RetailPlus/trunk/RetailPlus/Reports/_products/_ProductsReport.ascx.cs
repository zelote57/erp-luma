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

	public partial  class __ProductsReport : System.Web.UI.UserControl
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
            cboReportType.Items.Add(new ListItem(ReportTypes.ProductList, ReportTypes.ProductList));
            cboReportType.Items.Add(new ListItem(ReportTypes.ProductPriceList, ReportTypes.ProductPriceList));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.WeightedProductsForWeighingScale, ReportTypes.WeightedProductsForWeighingScale));
            cboReportType.Items.Add(new ListItem(ReportTypes.CountedProductsForWeighingScale, ReportTypes.CountedProductsForWeighingScale));
            cboReportType.SelectedIndex = 0;

            Branch clsBranch = new Branch();
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranch.DataBind();
            cboBranch.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboBranch.SelectedIndex = 0;

            Contact clsContact = new Contact(clsBranch.Connection, clsBranch.Transaction);
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text, 100).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboContact.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsBranch.Connection, clsBranch.Transaction);
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.SearchDataTable(txtProductGroupCode.Text).DefaultView;
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
            else if (strReportType == ReportTypes.ProductList)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_products/_ProductsReport.rpt"));
            else if (strReportType == ReportTypes.ProductPriceList)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_products/_ProductsReportPriceSummary.rpt"));
            else if (strReportType == ReportTypes.WeightedProductsForWeighingScale)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_products/_ProductsReportPricesForWeighingScale.rpt"));
            else if (strReportType == ReportTypes.CountedProductsForWeighingScale)
                rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_products/_ProductsReportPricesForWeighingScale.rpt"));
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
            string strFileName = "prodrep_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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

            //long lngProductGroupName = long.Parse(cboProductGroup.SelectedItem.Value);
            //long lngSubGroupName = long.Parse(cboSubGroup.SelectedItem.Value);

            string ProductGroupName = string.Empty;
            if (cboProductGroup.SelectedItem.Value != Constants.ZERO_STRING) ProductGroupName = cboProductGroup.SelectedItem.Value;
            string SubGroupName = string.Empty;
            if (cboSubGroup.SelectedItem.Value != Constants.ZERO_STRING) SubGroupName = cboSubGroup.SelectedItem.Value;

            string ReportType = cboReportType.SelectedItem.Text;

            ProductColumns clsProductColumns = new ProductColumns();
            #region clsProductColumns
            clsProductColumns.ProductCode = true;
            clsProductColumns.BarCode = true;
            clsProductColumns.BarCode2 = true;
            clsProductColumns.BarCode3 = true;
            clsProductColumns.ProductDesc = true;
            clsProductColumns.ProductSubGroupName = true;
            clsProductColumns.BaseUnitName = true;
            clsProductColumns.UnitName = true;
            clsProductColumns.ProductGroupName = true;
            clsProductColumns.DateCreated = true;
            clsProductColumns.Price = true;
            clsProductColumns.MainQuantity = true;
            clsProductColumns.MinThreshold = true;
            clsProductColumns.MaxThreshold = true;
            clsProductColumns.PurchasePrice = true;
            clsProductColumns.SupplierName = true;
            clsProductColumns.MainQuantityIN = true;
            clsProductColumns.MainQuantityOUT = true;
            clsProductColumns.RIDMinThreshold = true;
            clsProductColumns.RIDMaxThreshold = true;
            clsProductColumns.RID = true;
            //clsProductColumns.BranchActualQuantity = true;
            //clsProductColumns.BranchQuantity = true;
            //clsProductColumns.BranchQuantityIN = true;
            //clsProductColumns.BranchQuantityOUT = true;
            #endregion

            ProductDetails clsSearchKey = new ProductDetails();
            #region Search Key
            clsSearchKey.BranchID = Convert.ToInt32(cboBranch.SelectedItem.Value);
            clsSearchKey.SupplierID = Convert.ToInt32(cboContact.SelectedItem.Value);
            clsSearchKey.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
            clsSearchKey.ProductSubGroupID = Convert.ToInt64(cboSubGroup.SelectedItem.Value);
            clsSearchKey.ProductCode = txtProductCode.Text;
            #endregion

            Product clsProduct = new Product();
            DataTable dt;
            string ProductIDs = null;

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.ProductList:
                    #region Products List
                    dt = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKey, ProductListFilterType.ShowActiveAndInactive, 0, System.Data.SqlClient.SortOrder.Ascending, 0, false, ProductColumnNames.ProductCode, SortOption.Ascending);

                    ProductVariationsMatrix clsMatrix = new ProductVariationsMatrix(clsProduct.Connection, clsProduct.Transaction);
                    System.Data.DataTable dtMatrixInventoryReport = clsMatrix.InventoryReport(ProductGroupName, SubGroupName, txtProductCode.Text);
                    clsProduct.CommitAndDispose();

                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Products.NewRow();

                        foreach (DataColumn dc in rptds.Products.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Products.Rows.Add(drNew);
                    }
                    foreach (System.Data.DataRow dr in dtMatrixInventoryReport.Rows)
                    {
                        DataRow drNew = rptds.ProductVariations.NewRow();

                        foreach (DataColumn dc in rptds.ProductVariations.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.ProductVariations.Rows.Add(drNew);
                    }
                    break;
                    #endregion

                case ReportTypes.ProductPriceList:
                    #region Products Price List
                    dt = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKey, ProductListFilterType.ShowActiveAndInactive, 0, System.Data.SqlClient.SortOrder.Ascending, 0, false, ProductColumnNames.ProductCode, SortOption.Ascending);
                    clsProduct.CommitAndDispose();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drNew = rptds.Products.NewRow();

                        foreach (DataColumn dc in rptds.Products.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.Products.Rows.Add(drNew);
                        ProductIDs += dr["ProductID"].ToString() + ",";
                    }
                    break;
                    #endregion

                case ReportTypes.WeightedProductsForWeighingScale:
                case ReportTypes.CountedProductsForWeighingScale:
                    #region Weighted and Counted Products For Weighing Scale
                    dt = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKey, ProductListFilterType.ShowActiveAndInactive, 0, System.Data.SqlClient.SortOrder.Ascending, 0, false, ProductColumnNames.ProductCode, SortOption.Ascending);
                    clsProduct.CommitAndDispose();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[ProductColumnNames.BarCode].ToString() != null && dr[ProductColumnNames.BarCode].ToString() != string.Empty)
                        {
                            DataRow drNew = rptds.Products.NewRow();

                            foreach (DataColumn dc in rptds.Products.Columns)
                                drNew[dc] = dr[dc.ColumnName];

                            rptds.Products.Rows.Add(drNew);
                            ProductIDs += dr["ProductID"].ToString() + ",";
                        }
                    }
                    break;
                    #endregion

                default:
                    return;

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
                case ReportTypes.ProductList:
                    paramField = Report.DataDefinition.ParameterFields["BranchName"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = cboBranch.SelectedItem.Text;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    
                    break;

                case ReportTypes.ProductPriceList:
                    paramField = Report.DataDefinition.ParameterFields["IncludeBuying"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = chkIncludeBuying.Checked;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["IncludeMargin"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = chkIncludeMargin.Checked;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;

                case ReportTypes.WeightedProductsForWeighingScale:
                    paramField = Report.DataDefinition.ParameterFields["Mode"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = "0";
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;

                case ReportTypes.CountedProductsForWeighingScale:
                    paramField = Report.DataDefinition.ParameterFields["Mode"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = "1";
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;

                default:
                    return;

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
            cboProductGroup.DataSource = clsProductGroup.SearchDataTable(txtProductGroupCode.Text).DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProductGroup.Items.Count > 1 && txtProductGroupCode.Text.Trim() != string.Empty) cboProductGroup.SelectedIndex = 1; else cboProductGroup.SelectedIndex = 0;
            clsProductGroup.CommitAndDispose();

            cboProductGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
        }

        protected void imgContactCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Contact clsContact = new Contact();
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

            if (strReportType == ReportTypes.CountedProductsForWeighingScale
                || strReportType == ReportTypes.WeightedProductsForWeighingScale)
                holderWeighted.Visible = true;
            else
                holderWeighted.Visible = false;
        }

        #endregion

    }
}