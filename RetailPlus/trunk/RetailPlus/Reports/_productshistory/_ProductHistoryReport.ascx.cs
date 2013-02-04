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

	public partial  class __ProductHistoryReport : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null ;
			}
		}

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null)
            {
                CRViewer.ReportSource = (ReportDocument) Session["ReportDocument"];
            }
        }

		private void LoadOptions()
		{
            Int64 UID = Convert.ToInt64(Session["UID"]);
            Security.AccessRights clsAccessRights = new Security.AccessRights();

            cboReportType.Items.Clear();
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION, ReportTypes.REPORT_SELECTION));
            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            cboReportType.Items.Add(new ListItem(ReportTypes.ProductHistoryMovement, ReportTypes.ProductHistoryMovement));

            if (clsAccessRights.Details(UID, (int)AccessTypes.PricesReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.ProductHistoryPrice, ReportTypes.ProductHistoryPrice));

            cboReportType.Items.Add(new ListItem(ReportTypes.REPORT_SELECTION_SEPARATOR, ReportTypes.REPORT_SELECTION_SEPARATOR));
            if (clsAccessRights.Details(UID, (int)AccessTypes.MostSalableItemsReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.ProductHistoryMostSaleable, ReportTypes.ProductHistoryMostSaleable));

            if (clsAccessRights.Details(UID, (int)AccessTypes.LeastSalableItemsReport).Read)
                cboReportType.Items.Add(new ListItem(ReportTypes.ProductHistoryLeastSaleable, ReportTypes.ProductHistoryLeastSaleable));

            clsAccessRights.CommitAndDispose();

            cboReportType.SelectedIndex = 0;

			txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
			txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string strProductCode = string.Empty;
            try
            {
                try
                {
                    if (Request.QueryString["sender"].ToString().ToLower() == "direct" && Request.QueryString["productcode"].ToString() != null)
                        strProductCode = Request.QueryString["productcode"].ToString();
                }
                catch { strProductCode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
                lblReferrer.ToolTip = strProductCode;
            }
            catch { strProductCode = lblReferrer.ToolTip; }

            txtProductCode.Text = strProductCode;

			Data.Products clsProduct = new Data.Products();
			cboProductCode.DataTextField = "ProductCode";
			cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(ProductListFilterType.ShowActiveAndInactive, txtProductCode.Text, 0, 0, string.Empty, 0, string.Empty, 100, false, false, ProductColumnNames.ProductCode, SortOption.Ascending);
            cboProductCode.DataBind();
			clsProduct.CommitAndDispose();
			
			if (cboProductCode.Items.Count == 0)
				cboProductCode.Items.Add(new ListItem("No product", "0"));

			cboProductCode.SelectedIndex = 0;
            cboProductCode_SelectedIndexChanged(null, null);
            try
            {
                if (strProductCode != string.Empty && cboProductCode.SelectedItem.Value != "0")
                    GenerateHTML();
            }
            catch { }
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.ProductHistoryMovement:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_producthistoryreportmovement.rpt"));
                    break;
                case ReportTypes.ProductHistoryPrice:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_producthistoryreportprice.rpt"));
                    break;
                case ReportTypes.ProductHistoryMostSaleable:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_MostSalableItemsReportGrouped.rpt"));
                    break;
                    //bool boIsGrouped = false;
                    //if (boIsGrouped)
                    //    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_MostSalableItemsReportGrouped.rpt"));
                    //else
                    //    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_MostSalableItemsReport.rpt"));
                    //break;
                case ReportTypes.ProductHistoryLeastSaleable:
                    rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_productshistory/_LeastSalableItemsReportGrouped.rpt"));
                    break;
                default:
                    return null;
            }

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
            string strFileName = "prodhist_" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssff") + strFileExtensionName;
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

            long lngProductID = long.Parse(cboProductCode.SelectedItem.Value);

			DateTime DateFrom = DateTime.MinValue;
			try
			{	DateFrom = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text);	}
			catch{}
			DateTime DateTo = DateTime.MinValue;
			try
			{	DateTo = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text);	}
			catch{}
            Int32 Limit = 0;
            try
            { Limit = Convert.ToInt32(txtLimit.Text); }
            catch { }

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.ProductHistoryMovement:
                    #region Product History Movement
                    StockItem clsStockItem = new StockItem();
                    System.Data.DataTable dtProductHistoryMovement = clsStockItem.ProductMovementReport(lngProductID, DateFrom, DateTo);
                    clsStockItem.CommitAndDispose();
                    foreach (DataRow dr in dtProductHistoryMovement.Rows)
                    {
                        DataRow drNew = rptds.ProductMovement.NewRow();

                        foreach (DataColumn dc in rptds.ProductMovement.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.ProductMovement.Rows.Add(drNew);
                    }
                    break;
                    #endregion
                case ReportTypes.ProductHistoryPrice:
                    #region Product price history
                    ProductPackagePriceHistory clsProductPackagePriceHistory = new ProductPackagePriceHistory();
                    clsProductPackagePriceHistory.GetConnection();
                    MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(clsProductPackagePriceHistory.Connection, clsProductPackagePriceHistory.Transaction);
                    System.Data.DataTable dtProductList = clsProductPackagePriceHistory.List(DateFrom, DateTo, lngProductID);
                    System.Data.DataTable dtMatrixList = clsMatrixPackagePriceHistory.List(DateFrom, DateTo, lngProductID);
                    clsProductPackagePriceHistory.CommitAndDispose();

                    foreach (DataRow dr in dtProductList.Rows)
                    {
                        DataRow drNew = rptds.ProductPriceHistory.NewRow();

                        foreach (DataColumn dc in rptds.ProductPriceHistory.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.ProductPriceHistory.Rows.Add(drNew);
                    }
                    foreach (DataRow dr in dtMatrixList.Rows)
                    {
                        DataRow drNew = rptds.ProductPriceHistory.NewRow();

                        foreach (DataColumn dc in rptds.ProductPriceHistory.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.ProductPriceHistory.Rows.Add(drNew);
                    }
                    break;
                    #endregion
                case ReportTypes.ProductHistoryMostSaleable:
			        SalesTransactionItems clsSalesTransactionItemsMost = new SalesTransactionItems();
                    System.Data.DataTable dtMostSaleable = clsSalesTransactionItemsMost.MostSalableItems(DateFrom, DateTo, Limit);
                    clsSalesTransactionItemsMost.CommitAndDispose();
                    foreach (DataRow dr in dtMostSaleable.Rows)
                    {
                        DataRow drNew = rptds.MostSalableItems.NewRow();

                        foreach (DataColumn dc in rptds.MostSalableItems.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.MostSalableItems.Rows.Add(drNew);
                    }
                    break;
                case ReportTypes.ProductHistoryLeastSaleable:
                    SalesTransactionItems clsSalesTransactionItemsLeast = new SalesTransactionItems();
                    System.Data.DataTable dtLeastSaleable = clsSalesTransactionItemsLeast.LeastSalableItems(DateFrom, DateTo, Limit);
                    clsSalesTransactionItemsLeast.CommitAndDispose();
                    foreach (DataRow dr in dtLeastSaleable.Rows)
                    {
                        DataRow drNew = rptds.LeastSalableItems.NewRow();

                        foreach (DataColumn dc in rptds.LeastSalableItems.Columns)
                            drNew[dc] = dr[dc.ColumnName];

                        rptds.LeastSalableItems.Rows.Add(drNew);
                    }
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

            DateTime StartTransactionDate = DateTime.MinValue;
            try
            { StartTransactionDate = Convert.ToDateTime(txtStartDate.Text + " " + txtStartTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["StartTransactionDate"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = StartTransactionDate;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            DateTime EndTransactionDate = DateTime.MinValue;
            try
            { EndTransactionDate = Convert.ToDateTime(txtEndDate.Text + " " + txtEndTime.Text); }
            catch { }
            paramField = Report.DataDefinition.ParameterFields["EndTransactionDate"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = EndTransactionDate;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.ProductHistoryMovement:
                    #region Product History Movement
                    Products clsProduct = new Products();
                    ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(cboProductCode.SelectedItem.Value));
                    clsProduct.CommitAndDispose();

                    paramField = Report.DataDefinition.ParameterFields["ProductCode"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsDetails.ProductCode;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["Description"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsDetails.ProductDesc;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["Quantity"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsDetails.Quantity.ToString("#,###.#0");
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["UnitCode"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsDetails.BaseUnitCode;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = Report.DataDefinition.ParameterFields["ConvertedQuantity"];
                    discreteParam = new ParameterDiscreteValue();
                    discreteParam.Value = clsDetails.ConvertedMainQuantity;
                    currentValues = new ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);
                    break;
                    #endregion
                case ReportTypes.ProductHistoryMostSaleable:
                case ReportTypes.ProductHistoryLeastSaleable:
                    paramField = Report.DataDefinition.ParameterFields["GroupItems"];
			        discreteParam = new ParameterDiscreteValue();
			        discreteParam.Value = chkGroupItems.Checked;
			        currentValues = new ParameterValues();
			        currentValues.Add(discreteParam);
			        paramField.ApplyCurrentValues(currentValues);
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
		private void InitializeComponent()
		{

		}

		#endregion

        #region Web Control Methods

        protected void cmdView_Click(object sender, System.EventArgs e)
		{
			if (cboProductCode.SelectedItem.Value == "0")
			{
				string stScript = "<Script>";
				stScript += "window.alert('Please select at least one record to print.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
			else 
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
		}
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DataClass clsDataClass = new DataClass();

			Data.Products clsProduct = new Data.Products();
			cboProductCode.DataTextField = "ProductCode";
			cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ProductIDandCodeDataTable(ProductListFilterType.ShowActiveAndInactive, txtProductCode.Text, 0, 0, string.Empty, 0, string.Empty, 100, false, false, ProductColumnNames.ProductCode, SortOption.Ascending);
			cboProductCode.DataBind();
			clsProduct.CommitAndDispose();
			
			if (cboProductCode.Items.Count == 0) cboProductCode.Items.Add(new ListItem("No product", "0"));
			cboProductCode.SelectedIndex = 0;
            cboProductCode_SelectedIndexChanged(null, null);
		}
        protected void cboProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProductCode.SelectedItem.Text == "No product")
                {
                    imgProductHistory.Visible = false;
                    imgProductPriceHistory.Visible = false;
                    imgInventoryAdjustment.Visible = false;
                    imgEditNow.Visible = false;
                }
                else
                {
                    imgProductHistory.Visible = true;
                    imgProductPriceHistory.Visible = true;
                    imgInventoryAdjustment.Visible = true;
                    imgEditNow.Visible = true;
                    txtProductCode.Text = cboProductCode.SelectedItem.Text;
                }
            }
            catch { }
        }
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        protected void imgProductHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("producthistory", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgProductPriceHistory_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("pricehistory", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Reports/Default.aspx" + stParam);
        }
        protected void imgInventoryAdjustment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("invadjustment", Session.SessionID) +
                        "&productcode=" + Common.Encrypt(cboProductCode.SelectedItem.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/Inventory/Default.aspx" + stParam);
        }
        protected void imgEditNow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) +
                                "&id=" + Common.Encrypt(cboProductCode.SelectedItem.Value, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }

        protected void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            holderProductCode.Visible = false;
            holderMostSaleable.Visible = false;
            switch (cboReportType.SelectedValue)
            {
                case ReportTypes.ProductHistoryMovement:
                case ReportTypes.ProductHistoryPrice:
                    holderProductCode.Visible = true;
                    break;
                case ReportTypes.ProductHistoryMostSaleable:
                case ReportTypes.ProductHistoryLeastSaleable:
                    holderMostSaleable.Visible = true;
                    break;
            }
        }

        #endregion

    }
}
