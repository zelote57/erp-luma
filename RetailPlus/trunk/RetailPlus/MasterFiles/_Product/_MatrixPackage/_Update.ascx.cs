namespace AceSoft.RetailPlus.MasterFiles._Product._MatrixPackage
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

		}
		#endregion

		#region Web Control Methods

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID) + "&matrixid=" + Common.Encrypt(lblMatrixID.Text, Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID) + "&matrixid=" + Common.Encrypt(lblMatrixID.Text, Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
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


		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			lblMatrixID.Text = Common.Decrypt((string)Request.QueryString["matrixid"],Session.SessionID);
			lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);
			lblPackageID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductUnitsMatrix clsUnit = new ProductUnitsMatrix();
			cboUnit.DataTextField = "BottomUnitName";
			cboUnit.DataValueField = "BottomUnitID";
			cboUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List(Convert.ToInt64(lblProductID.Text),"MatrixID",SortOption.Ascending)).DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();

			Product clsProduct = new Product();
			ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
			cboUnit.Items.Add(new ListItem(clsDetails.BaseUnitName, clsDetails.BaseUnitID.ToString()));
			clsProduct.CommitAndDispose();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
		}

		private void LoadRecord()
		{
			MatrixPackage clsMatrixPackage = new MatrixPackage();
			MatrixPackageDetails clsDetails = clsMatrixPackage.Details(Convert.ToInt64(lblPackageID.Text));
			clsMatrixPackage.CommitAndDispose();

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
			if (clsDetails.Quantity == 1)
				txtQuantity.Enabled = false;
		}
		private bool SaveRecord()
		{
            long lngUID = long.Parse(Session["UID"].ToString());
            DateTime dteChangeDate = DateTime.Now;

			MatrixPackage clsMatrixPackage = new MatrixPackage();
			MatrixPackageDetails clsDetails = new MatrixPackageDetails();
			
			clsDetails.PackageID = Convert.ToInt64(lblPackageID.Text);
			clsDetails.MatrixID = Convert.ToInt64(lblMatrixID.Text);
			clsDetails.UnitID = Convert.ToInt32(cboUnit.SelectedItem.Value);
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);

			clsMatrixPackage.Update(clsDetails, lngUID, dteChangeDate, "Matrix Package update.");
			clsMatrixPackage.CommitAndDispose();

			return true;
		}



		#endregion
    }
}
