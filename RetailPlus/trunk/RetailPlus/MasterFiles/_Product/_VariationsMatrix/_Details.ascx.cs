namespace AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix
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
			this.imgBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}


		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["VariationID"].ToString();

				Label lblVariationType = (Label) e.Item.FindControl("lblVariationType");
				lblVariationType.Text = dr["VariationType"].ToString();

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

			ProductUnitsMatrix clsUnit = new ProductUnitsMatrix(clsProductVariation.Connection, clsProductVariation.Transaction);
			cboUnit.DataTextField = "BottomUnitName";
			cboUnit.DataValueField = "BottomUnitID";
			cboUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List(Convert.ToInt64(lblProductID.Text),"MatrixID",SortOption.Ascending)).DefaultView;
			cboUnit.DataBind();

			Product clsProduct = new Product(clsProductVariation.Connection, clsProductVariation.Transaction);
			ProductDetails clsDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));
			cboUnit.Items.Add(new ListItem(clsDetails.BaseUnitName, clsDetails.BaseUnitID.ToString()));
			clsProductVariation.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
		}

		private void LoadRecord()
		{
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
			ProductBaseMatrixDetails clsProductBaseMatrixDetails = clsProductVariationsMatrix.BaseDetails(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(lblProductID.Text));
			clsProductVariationsMatrix.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsProductBaseMatrixDetails.UnitID.ToString()));
			txtProductPrice.Text = clsProductBaseMatrixDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsProductBaseMatrixDetails.PurchasePrice.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsProductBaseMatrixDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsProductBaseMatrixDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsProductBaseMatrixDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsProductBaseMatrixDetails.LocalTax.ToString("#,##0.#0");
			txtQuantity.Text = clsProductBaseMatrixDetails.Quantity.ToString("#,##0.#0");
			txtMinThreshold.Text = clsProductBaseMatrixDetails.MinThreshold.ToString("#,##0.#0");
			txtMaxThreshold.Text = clsProductBaseMatrixDetails.MaxThreshold.ToString("#,##0.#0");
		}


		#endregion
	}
}
