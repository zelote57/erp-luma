namespace AceSoft.RetailPlus.Inventory._Stock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();	
					LoadRecord();	
				}
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
			this.imgPrint.Click += new System.Web.UI.ImageClickEventHandler(this.imgPrint_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["StockItemID"].ToString();

				HyperLink lnkProduct = (HyperLink) e.Item.FindControl("lnkProduct");
				lnkProduct.Text = dr["ProductCode"].ToString();
				lnkProduct.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				HyperLink lnkVariation = (HyperLink) e.Item.FindControl("lnkVariation");
				if (dr["BaseVariationDescription"].ToString() == string.Empty || dr["BaseVariationDescription"].ToString() == null)
					lnkVariation.Text = "_";
				else
				{
					lnkVariation.Text = dr["BaseVariationDescription"].ToString();
					lnkVariation.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
				}
				
				Label lblProductUnit = (Label) e.Item.FindControl("lblProductUnit");
				lblProductUnit.Text = dr["UnitName"].ToString();

				Label lblStockType = (Label) e.Item.FindControl("lblStockType");
				lblStockType.Text = dr["StockTypeDescription"].ToString();

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = dr["Quantity"].ToString();

				Label lblRemarks = (Label) e.Item.FindControl("lblRemarks");
				lblRemarks.Text = dr["Remarks"].ToString();

				//For anchor
				//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Print();
        }
        protected void cmdPrint_Click(object sender, System.EventArgs e)
        {
            Print();
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{

		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["stockid"],Session.SessionID));
			Stock clsStock = new Stock();
			StockDetails clsDetails = clsStock.Details(iID);
			clsStock.CommitAndDispose();

			lblStockID.Text = clsDetails.StockID.ToString();
			lblTransactionNo.Text = clsDetails.TransactionNo;
			lblStockDate.Text = clsDetails.StockDate.ToString("MMM. dd, yyy HH:mm:ss");
			lblSupplier.Text = clsDetails.SupplierName;
			lblSupplierID.Text = clsDetails.SupplierID.ToString();
			lblStockTypeCode.Text = clsDetails.StockTypeCode; 
			lblStockTypeCode.ToolTip = clsDetails.StockTypeID.ToString(); 
			lblStockDirection.Text = clsDetails.StockDirection.ToString("G");

			LoadItems();
		}
		private void LoadItems()
		{
			DataClass clsDataClass = new DataClass();

			StockItem clsStockItem = new StockItem();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsStockItem.List(Convert.ToInt64(lblStockID.Text), "StockItemID",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsStockItem.CommitAndDispose();
		}
        private void Print()
        {
            Session.Remove("tranno");
            Session.Add("tranno", lblTransactionNo.Text);

            string stParam = "?task=" + Common.Encrypt("reports", Session.SessionID) + "&target=" + Common.Encrypt("stocktransaction", Session.SessionID) + "&tranno=" + Common.Encrypt(lblTransactionNo.Text, Session.SessionID);
            string newWindowUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx" + stParam;
            string javaScript = "window.open('" + newWindowUrl + "');";

            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updPrint, this.updPrint.GetType(), "openwindow", javaScript, true);
        }

		#endregion
    }
}
