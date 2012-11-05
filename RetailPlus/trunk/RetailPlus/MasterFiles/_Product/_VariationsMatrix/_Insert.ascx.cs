namespace AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix
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
            if (!IsPostBack && Visible)
			{
			    lblReferrer.Text = Request.UrlReferrer.ToString();
			    LoadOptions();			
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

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
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
		protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["VariationID"].ToString();

                HyperLink lnkVariationType = (HyperLink)e.Item.FindControl("lnkVariationType");
                lnkVariationType.Text = dr["VariationType"].ToString(); //VariationID
                lnkVariationType.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Variation/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationID"].ToString(), Session.SessionID);
			}
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);

			ProductVariation clsProductVariation = new ProductVariation();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariation.List(Convert.ToInt64(lblProductID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductVariation.CommitAndDispose();
			
			ProductUnitsMatrix clsUnit = new ProductUnitsMatrix();
			cboUnit.DataTextField = "BottomUnitName";
			cboUnit.DataValueField = "BottomUnitID";
			cboUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List(Convert.ToInt64(lblProductID.Text),"MatrixID",SortOption.Ascending)).DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();	

			Product clsProduct = new Product();
			ProductDetails clsProductDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
			cboUnit.Items.Add(new ListItem(clsProductDetails.BaseUnitName, clsProductDetails.BaseUnitID.ToString()));
			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsProductDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsProductDetails.Price.ToString("#,##0.#0");
            txtWSPrice.Text = clsProductDetails.WSPrice.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsProductDetails.PurchasePrice.ToString("#,##0.#0");
            decimal decMargin = clsProductDetails.Price - clsProductDetails.PurchasePrice;
            try { decMargin = decMargin / clsProductDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");

            decMargin = clsProductDetails.WSPrice - clsProductDetails.PurchasePrice;
            try { decMargin = decMargin / clsProductDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtWSPriceMarkUp.Text = decMargin.ToString("#,##0.#0");

            if (clsProductDetails.IncludeInSubtotalDiscount == 0)
				chkIncludeInSubtotalDiscount.Checked = false;
			else
				chkIncludeInSubtotalDiscount.Checked = true;
			txtVAT.Text = clsProductDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsProductDetails.EVAT.ToString("#,##0.#0");
			clsProduct.CommitAndDispose();

			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
			bool IsFirstBaseVariation = clsProductVariationsMatrix.IsFirstBaseVariation(Convert.ToInt64(lblProductID.Text));
			if (IsFirstBaseVariation)
			{
				txtQuantity.Text = clsProductDetails.Quantity.ToString("##0");
			}
			clsProductVariationsMatrix.CommitAndDispose();
			
		}
		private bool SaveRecord()
		{
            Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];

			ProductBaseMatrixDetails clsBaseDetails = new ProductBaseMatrixDetails();
			ProductVariationsMatrixDetails clsDetails;
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
			
			clsBaseDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsBaseDetails.Description = "";
			clsBaseDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsBaseDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
            clsBaseDetails.WSPrice = Convert.ToDecimal(txtWSPrice.Text); 
            clsBaseDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsBaseDetails.IncludeInSubtotalDiscount = Convert.ToInt16(chkIncludeInSubtotalDiscount.Checked);
			clsBaseDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsBaseDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsBaseDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsBaseDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsBaseDetails.MinThreshold = Convert.ToDecimal(txtMinThreshold.Text);
			clsBaseDetails.MaxThreshold = Convert.ToDecimal(txtMaxThreshold.Text);
            clsBaseDetails.CreatedBy = clsAccessUserDetails.Name;
			clsBaseDetails.MatrixID = clsProductVariationsMatrix.InsertBaseVariation(clsBaseDetails);

            string stringVariationDesc = null;

			foreach (DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				TextBox txtDescription = (TextBox) item.FindControl("txtDescription");

				clsDetails = new ProductVariationsMatrixDetails();
				clsDetails.MatrixID = clsBaseDetails.MatrixID;
				clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
				clsDetails.VariationID = Convert.ToInt32(chkList.Value);
				clsDetails.Description = txtDescription.Text;
				
				clsProductVariationsMatrix.InsertVariation(clsDetails);

                //Label lblVariationType = (Label) item.FindControl("lblVariationType");
                //stringVariationDesc += lblVariationType.Text + ":" + txtDescription.Text + "; ";
                stringVariationDesc += txtDescription.Text + "; ";
			}
			
			//Insert as single description 
			clsBaseDetails.Description = stringVariationDesc;
			clsProductVariationsMatrix.UpdateVariationDesc(clsBaseDetails);

            Product clsProduct = new Product(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
            ProductDetails clsProductDetails = clsProduct.Details(clsBaseDetails.ProductID);

            InvAdjustmentDetails clsInvAdjustmentDetails = new InvAdjustmentDetails();
            clsInvAdjustmentDetails.UID = clsAccessUserDetails.UID;
            clsInvAdjustmentDetails.InvAdjustmentDate = DateTime.Now;
            clsInvAdjustmentDetails.ProductID = clsBaseDetails.ProductID;
            clsInvAdjustmentDetails.ProductCode = clsProductDetails.ProductCode;
            clsInvAdjustmentDetails.Description = clsProductDetails.ProductDesc;
            clsInvAdjustmentDetails.VariationMatrixID = clsBaseDetails.MatrixID;
            clsInvAdjustmentDetails.MatrixDescription = clsBaseDetails.Description;
            clsInvAdjustmentDetails.UnitID = clsBaseDetails.UnitID;
            clsInvAdjustmentDetails.UnitCode = cboUnit.SelectedItem.Text;
            clsInvAdjustmentDetails.QuantityBefore = 0;
            clsInvAdjustmentDetails.QuantityNow = 0; // Aug 1, 2011 : Lemu Change the clsBaseDetails.Quantity to ZERO and add in the clsProduct.AddQuantity
            clsInvAdjustmentDetails.MinThresholdBefore = 0;
            clsInvAdjustmentDetails.MinThresholdNow = clsBaseDetails.MinThreshold;
            clsInvAdjustmentDetails.MaxThresholdBefore = 0;
            clsInvAdjustmentDetails.MaxThresholdNow = clsBaseDetails.MaxThreshold;
            clsInvAdjustmentDetails.Remarks = "newly added. beginning balance.";

            InvAdjustment clsInvAdjustment = new InvAdjustment(clsProduct.Connection, clsProduct.Transaction);

            clsInvAdjustment.DeleteMainProduct(clsProductDetails.ProductID);
            clsInvAdjustment.Insert(clsInvAdjustmentDetails);

            // Aug 1, 2011 : Lemu
            // Deleted and replaced by adding the quantity in the product to include in the ProductMovement report
            // clsProduct.AddQuantity is already included in the clsProductVariationsMatrix.InsertBaseVariation
            // 
			// clsProductVariationsMatrix.SynchronizeQuantity(long.Parse(lblProductID.Text));

			clsProductVariationsMatrix.CommitAndDispose();

			return true;
		}

		#endregion
    }
}