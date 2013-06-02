namespace AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
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

				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();

				TextBox txtDescription = (TextBox) e.Item.FindControl("txtDescription");
				txtDescription.Text = clsProductVariationsMatrix.Details(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(chkList.Value)).Description;

				clsProductVariationsMatrix.CommitAndDispose();

			}
		}


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);
			lblMatrixID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductVariation clsProductVariation = new ProductVariation();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariation.List(Convert.ToInt32(lblProductID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductVariation.CommitAndDispose();	

			ProductUnitsMatrix clsUnit = new ProductUnitsMatrix();
			cboUnit.DataTextField = "BottomUnitName";
			cboUnit.DataValueField = "BottomUnitID";
			cboUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List(Convert.ToInt64(lblProductID.Text),"MatrixID",SortOption.Ascending)).DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();	

			Products clsProduct = new Products();
            ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
			cboUnit.Items.Add(new ListItem(clsDetails.BaseUnitName, clsDetails.BaseUnitID.ToString()));
			clsProduct.CommitAndDispose();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
		}
		private void LoadRecord()
		{
            long ProductID = Int64.Parse(lblProductID.Text);
            long MatrixID = Int64.Parse(lblMatrixID.Text);

            Products clsProducts = new Products();
            ProductDetails clsDetails = clsProducts.Details(ProductID: ProductID, MatrixID: MatrixID);
            clsProducts.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
            txtWSPrice.Text = clsDetails.WSPrice.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
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
		}
		private bool SaveRecord()
		{
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
			ProductVariationsMatrixDetails clsDetails = new ProductVariationsMatrixDetails();

			string stringVariationDesc = null;

			foreach (DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				TextBox txtDescription = (TextBox) item.FindControl("txtDescription");
				
				clsDetails = new ProductVariationsMatrixDetails();
				clsDetails.MatrixID = Convert.ToInt32(lblMatrixID.Text);
				clsDetails.ProductID = Convert.ToInt32(lblProductID.Text);
				clsDetails.VariationID = Convert.ToInt32(chkList.Value);
				clsDetails.Description = txtDescription.Text;
				
				clsProductVariationsMatrix.Update(clsDetails);

				Label lblVariationType = (Label) item.FindControl("lblVariationType");
				//stringVariationDesc += lblVariationType.Text + ":" + txtDescription.Text + "; ";
                stringVariationDesc += txtDescription.Text + "; ";
			}
			
			//update base variation matrix 
			ProductBaseMatrixDetails clsBaseDetails = new ProductBaseMatrixDetails();
			clsBaseDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
			clsBaseDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsBaseDetails.Description = stringVariationDesc;
			clsBaseDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsBaseDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
            clsBaseDetails.WSPrice = Convert.ToDecimal(txtWSPrice.Text);
			clsBaseDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsBaseDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked;
			clsBaseDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsBaseDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsBaseDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			clsBaseDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsBaseDetails.MinThreshold = Convert.ToDecimal(txtMinThreshold.Text);
			clsBaseDetails.MaxThreshold = Convert.ToDecimal(txtMaxThreshold.Text);
            clsBaseDetails.UpdatedBy = Convert.ToInt64(Session["UID"].ToString());
            clsBaseDetails.UpdatedOn = DateTime.Now;

			clsProductVariationsMatrix.UpdateBaseVariation(clsBaseDetails);

			clsProductVariationsMatrix.CommitAndDispose();

			return true;
		}

		#endregion
    }
}
