namespace AceSoft.RetailPlus.MasterFiles._Product._ProductPackage
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
		protected System.Web.UI.WebControls.DropDownList cboVariationType;
		
		#region Web Form methods

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

		#region Web Control methods

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

		
		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);
			lblPackageID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductUnitsMatrix clsUnit = new ProductUnitsMatrix();
			cboUnit.DataTextField = "BottomUnitName";
			cboUnit.DataValueField = "BottomUnitID";
			cboUnit.DataSource = clsUnit.ListAsDataTable(Convert.ToInt64(lblProductID.Text),"MatrixID",SortOption.Ascending).DefaultView;
			cboUnit.DataBind();

            Products clsProduct = new Products(clsUnit.Connection, clsUnit.Transaction);
            ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
            clsUnit.CommitAndDispose();

            cboUnit.Items.Insert(0, new ListItem(clsDetails.BaseUnitName, clsDetails.BaseUnitID.ToString()));
            cboUnit.SelectedIndex = 0;
		}

		private void LoadRecord()
		{
			ProductPackage clsProductPackage = new ProductPackage();
			ProductPackageDetails clsDetails = clsProductPackage.Details(Convert.ToInt64(lblPackageID.Text));

            Products clsProduct = new Products(clsProductPackage.Connection, clsProductPackage.Transaction);
            ProductDetails clsProductDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
            clsProductPackage.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsDetails.UnitID.ToString()));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
            decimal decMargin = clsDetails.Price - clsDetails.PurchasePrice;
            try { decMargin = decMargin / clsDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
			txtQuantity.Text = clsDetails.Quantity.ToString("#,##0.#0");
            txtBarCode1.Text = clsDetails.BarCode1;
            txtBarCode2.Text = clsDetails.BarCode2;
            txtBarCode3.Text = clsDetails.BarCode3;
            if (clsDetails.Quantity == 1 && clsProductDetails.BaseUnitID == clsDetails.UnitID)
            {
                txtQuantity.Enabled = false; cboUnit.Enabled = false;
            }
		}

		private bool SaveRecord()
		{
            long lngUID = long.Parse(Session["UID"].ToString());
            DateTime dteChangeDate = DateTime.Now;

			ProductPackage clsProductPackage = new ProductPackage();
			ProductPackageDetails clsDetails = new ProductPackageDetails();
			
			clsDetails.PackageID = Convert.ToInt64(lblPackageID.Text);
			clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
            clsDetails.BarCode1 = txtBarCode1.Text;
            clsDetails.BarCode2 = txtBarCode2.Text;
            clsDetails.BarCode3 = txtBarCode3.Text;

			clsProductPackage.Update(clsDetails, lngUID, dteChangeDate, "Product Package Update.");
			clsProductPackage.CommitAndDispose();

			return true;
		}


		#endregion
	}
}
