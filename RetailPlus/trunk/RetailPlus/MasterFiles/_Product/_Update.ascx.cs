using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	/// <summary>
	///		Summary description for __Insert.
	/// </summary>
	public partial  class __Update : System.Web.UI.UserControl
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
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupID = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupColumns clsSearchColumns = new ProductSubGroupColumns();
            clsSearchColumns.ProductGroupID = true;

            ProductSubGroup clsProductSubGroup = new ProductSubGroup();
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchColumns, cboProductGroup.SelectedItem.Value, 0, System.Data.SqlClient.SortOrder.Ascending, 0, string.Empty, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;
            clsProductSubGroup.CommitAndDispose();

            // do not load the default to override
            //cboProductSubGroup_SelectedIndexChanged(sender, e);
		}

        //protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    //if (cboProductSubGroup.Items.Count != 0)
        //    //{
        //    //    ProductSubGroup clsProductSubGroup = new ProductSubGroup();
        //    //    ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(Convert.ToInt32(cboProductSubGroup.SelectedItem.Value));
        //    //    cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf( cboProductUnit.Items.FindByValue(clsProductSubGroupDetails.BaseUnitID.ToString()));
        //    //    txtProductPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
        //    //    clsProductSubGroup.CommitAndDispose();	
        //    //}
        //}

		
		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));

			ProductGroup clsProductGroup = new ProductGroup();
			cboProductGroup.DataTextField = "ProductGroupName";
			cboProductGroup.DataValueField = "ProductGroupID";
			cboProductGroup.DataSource = clsDataClass.DataReaderToDataTable(clsProductGroup.List("ProductGroupName",SortOption.Ascending)).DefaultView;
			cboProductGroup.DataBind();
			cboProductGroup.SelectedIndex = cboProductGroup.Items.Count - 1;

            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupID = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupColumns clsSearchColumns = new ProductSubGroupColumns();

            ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsProductGroup.Connection, clsProductGroup.Transaction);
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchColumns, cboProductGroup.SelectedItem.Value, 0, System.Data.SqlClient.SortOrder.Ascending, 0, string.Empty, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;
            clsProductSubGroup.CommitAndDispose();

			UnitMeasurements clsUnit = new UnitMeasurements(clsProductGroup.Connection, clsProductGroup.Transaction);
			cboProductUnit.DataTextField = "UnitName";
			cboProductUnit.DataValueField = "UnitID";
			cboProductUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List("UnitName",SortOption.Ascending)).DefaultView;
			cboProductUnit.DataBind();
			cboProductUnit.SelectedIndex = cboProductUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();

            ContactColumns clsContactColumns = new ContactColumns();
            clsContactColumns.ContactID = true;
            clsContactColumns.ContactName = true;

            ContactColumns clsContactSearchColumns = new ContactColumns();

            Contacts clsContact = new Contacts(clsProductGroup.Connection, clsProductGroup.Transaction);
            cboSupplier.DataTextField = "ContactName";
            cboSupplier.DataValueField = "ContactID";
            cboSupplier.DataSource = clsContact.Suppliers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsContactSearchColumns, string.Empty, 0, false, "ContactName", System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboSupplier.DataBind();
            cboSupplier.SelectedIndex = cboSupplier.Items.Count - 1;

			ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix(clsProductGroup.Connection, clsProductGroup.Transaction);
			ProductUnitsMatrixDetails clsUnitDetails = clsUnitMatrix.LastDetails(iID);

			if (clsUnitDetails.BottomUnitName == null)
			{
				cboProductUnit.Enabled = true;
			}

            // Added July 9, 2010
            Terminal clsTerminal = new Terminal(clsProductGroup.Connection, clsProductGroup.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(1);
            txtWSPriceMarkUp.Text = clsTerminalDetails.WSPriceMarkUp.ToString();
            txtMargin.Text = clsTerminalDetails.RETPriceMarkUp.ToString();

			clsProductGroup.CommitAndDispose();	
		}

		private void LoadRecord()
		{
            Int64 prdID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
			Products clsProduct = new Products();
            ProductDetails clsDetails = clsProduct.Details(prdID);
			clsProduct.CommitAndDispose();

			lblProductID.Text = clsDetails.ProductID.ToString();
			txtProductCode.Text = clsDetails.ProductCode;
			txtBarcode.Text = clsDetails.BarCode;
            txtBarcode2.Text = clsDetails.BarCode2;
            txtBarcode3.Text = clsDetails.BarCode3;
			txtProductDesc.Text = clsDetails.ProductDesc;
			cboProductGroup.SelectedIndex = cboProductGroup.Items.IndexOf(cboProductGroup.Items.FindByValue(clsDetails.ProductGroupID.ToString()));
			cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.IndexOf(cboProductSubGroup.Items.FindByValue(clsDetails.ProductSubGroupID.ToString()));
			txtProductDesc.Text = clsDetails.ProductDesc;
			cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf(cboProductUnit.Items.FindByValue(clsDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
            txtWSPrice.Text = clsDetails.WSPrice.ToString("#,##0.#0");
            txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
            txtPercentageCommision.Text = clsDetails.PercentageCommision.ToString("#,##0.#0");
            decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");

            decMargin = clsDetails.WSPrice - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtWSPriceMarkUp.Text = decMargin.ToString("#,##0.#0");

            chkIncludeInSubtotalDiscount.Checked = clsDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
			txtQuantity.Text = clsDetails.Quantity.ToString("#,##0.#0");
			txtMinThreshold.Text = clsDetails.MinThreshold.ToString("#,##0.#0");
			txtMaxThreshold.Text = clsDetails.MaxThreshold.ToString("#,##0.#0");
			cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(cboSupplier.Items.FindByValue(clsDetails.SupplierID.ToString()));
            chkIsItemSold.Checked = clsDetails.IsItemSold;
            chkWillPrintProductComposition.Checked = clsDetails.WillPrintProductComposition;

            txtRID.Text = clsDetails.RID.ToString("###0");

            cboProductUnit.Enabled = clsDetails.Quantity != 0 ? false : true;
		}

		private void SaveRecord()
		{
			ProductDetails clsDetails = new ProductDetails();

			clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.ProductCode  = txtProductCode.Text;
			clsDetails.BarCode  = txtBarcode.Text;
            clsDetails.BarCode2 = txtBarcode2.Text;
            clsDetails.BarCode3 = txtBarcode3.Text;
			clsDetails.ProductDesc = txtProductDesc.Text;
			clsDetails.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value); 
			clsDetails.ProductSubGroupID  = Convert.ToInt64(cboProductSubGroup.SelectedItem.Value);
			clsDetails.ProductDesc  = txtProductDesc.Text;
			clsDetails.BaseUnitID  = Convert.ToInt32(cboProductUnit.SelectedItem.Value); 
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
            clsDetails.WSPrice = Convert.ToDecimal(txtWSPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
            clsDetails.PercentageCommision = Convert.ToDecimal(txtPercentageCommision.Text);
			clsDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked;
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsDetails.MinThreshold = Convert.ToDecimal(txtMinThreshold.Text);
			clsDetails.MaxThreshold = Convert.ToDecimal(txtMaxThreshold.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
            clsDetails.IsItemSold = Convert.ToBoolean(chkIsItemSold.Checked);
            clsDetails.WillPrintProductComposition = Convert.ToBoolean(chkWillPrintProductComposition.Checked);
            clsDetails.UpdatedBy = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.UpdatedOn = DateTime.Now;
            clsDetails.RID = Convert.ToInt64(txtRID.Text);

			Products clsProduct = new Products();
			clsProduct.Update(clsDetails);

            // Aug 26, 2011 : Lemu 
            // Update Requried Inventory Days (RID)
            clsProduct.UpdateRID(clsDetails.ProductID, clsDetails.RID);

			clsProduct.CommitAndDispose();
		}

		#endregion
	}
}
