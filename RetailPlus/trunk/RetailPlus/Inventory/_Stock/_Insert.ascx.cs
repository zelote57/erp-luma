namespace AceSoft.RetailPlus.Inventory._Stock
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Insert : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();			
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
		private void InitializeComponent()
		{

		}

		#endregion

		#region Web Control Methods

        protected void cmdSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Int64 StockID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&stockid=" + Common.Encrypt(StockID.ToString(), Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}
		protected void imgSaveAddItem_Click(object sender, System.EventArgs e)
		{
			Int64 StockID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&stockid=" + Common.Encrypt(StockID.ToString(), Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}
        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);			
		}
        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			
			Response.Redirect(lblReferrer.Text);
		}
        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cboStockTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Data.StockTypes clsStockTypes = new Data.StockTypes();
			Data.StockTypesDetails clsDetails = clsStockTypes.Details(Convert.ToInt16(cboStockTypes.SelectedItem.Value));
			clsStockTypes.CommitAndDispose();
			
			txtStockTypeDescription.Text = clsDetails.Description;
			txtStockDirection.Text = clsDetails.StockDirection.ToString("G");
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			StockTypes clsStockTypes = new StockTypes();
			cboStockTypes.DataTextField = "StockTypeCode";
			cboStockTypes.DataValueField = "StockTypeID";
			cboStockTypes.DataSource = clsDataClass.DataReaderToDataTable(clsStockTypes.List("StockTypeCode", SortOption.Ascending)).DefaultView;
			cboStockTypes.DataBind();
			cboStockTypes.SelectedIndex = cboStockTypes.Items.Count - 1;

            Contact clsContact = new Contact(clsStockTypes.Connection, clsStockTypes.Transaction);
			cboSupplier.DataTextField = "ContactName";
			cboSupplier.DataValueField = "ContactID";
			cboSupplier.DataSource = clsDataClass.DataReaderToDataTable(clsContact.Suppliers(null, 0, "ContactName", SortOption.Ascending)).DefaultView;
			cboSupplier.DataBind();

            Branch clsBranch = new Branch(clsStockTypes.Connection, clsStockTypes.Transaction);
            cboBranch.DataTextField = "BranchCode";
            cboBranch.DataValueField = "BranchID";
            cboBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranch.DataBind();
            
            clsStockTypes.CommitAndDispose();

            cboStockTypes_SelectedIndexChanged(null, null);
            cboSupplier.SelectedIndex = 0;
            cboBranch.SelectedIndex = cboBranch.Items.IndexOf(cboBranch.Items.FindByValue(Constants.BRANCH_ID_MAIN.ToString()));

			NewTransaction();
		}
		private void NewTransaction()
		{
			lblStockDate.Text = DateTime.Now.ToString("MMM. dd, yyyy HH:mm:ss");
			lblTransactionNo.Text = Convert.ToDateTime(lblStockDate.Text).ToString("yyyyMMddhhmmss");
		}
		private Int64 SaveRecord()
		{
			
			StockDetails clsDetails = new StockDetails();

			clsDetails.TransactionNo = lblTransactionNo.Text;
            clsDetails.BranchID = Convert.ToInt32(cboBranch.SelectedItem.Value);
			clsDetails.StockTypeID = Convert.ToInt16(cboStockTypes.SelectedItem.Value);
			clsDetails.StockDate = Convert.ToDateTime(lblStockDate.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
			clsDetails.Remarks = txtRemarks.Text;
			
			StockItemDetails[] itemDetails = new StockItemDetails[0];
			clsDetails.StockItems = itemDetails;

			Stock clsStock = new Stock();
			Int64 id = clsStock.Insert(clsDetails);
			clsStock.CommitAndDispose();

			NewTransaction();

			return id;
		}

		#endregion
    }
}
