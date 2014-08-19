namespace AceSoft.RetailPlus.MasterFiles._ProductGroup._VariationsMatrix
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
		protected System.Web.UI.WebControls.DropDownList cboVariationType;
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
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

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdBack_Click(object sender, System.EventArgs e)
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
				
				ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix();
				TextBox txtDescription = (TextBox) e.Item.FindControl("txtDescription");
				txtDescription.Text = clsProductGroupVariationsMatrix.Details(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(chkList.Value)).Description;
				clsProductGroupVariationsMatrix.CommitAndDispose();

			}
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            DataClass clsDataClass = new DataClass();

			lblGroupID.Text = Common.Decrypt((string)Request.QueryString["groupid"],Session.SessionID);
			lblMatrixID.Text = Common.Decrypt(Request.QueryString["id"],Session.SessionID);

			ProductGroupVariations clsProductGroupVariation = new ProductGroupVariations();
			lstItem.DataSource = clsDataClass.DataReaderToDataTable(clsProductGroupVariation.List(Convert.ToInt32(lblGroupID.Text),"VariationType",SortOption.Ascending)).DefaultView;
			lstItem.DataBind();
			clsProductGroupVariation.CommitAndDispose();

            Data.Unit clsUnit = new Data.Unit();
			cboUnit.DataTextField = "UnitName";
			cboUnit.DataValueField = "UnitID";
			cboUnit.DataSource = clsUnit.ListAsDataTable(SortField: "UnitName").DefaultView;
			cboUnit.DataBind();
			cboUnit.SelectedIndex = cboUnit.Items.Count - 1;
			clsUnit.CommitAndDispose();	
	
			ProductGroup clsProductGroup = new ProductGroup();
			ProductGroupDetails clsProductGroupDetails = clsProductGroup.Details(Convert.ToInt32(lblGroupID.Text));
			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsProductGroupDetails.UnitDetails.UnitID.ToString()));
			txtProductPrice.Text = clsProductGroupDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsProductGroupDetails.PurchasePrice.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsProductGroupDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsProductGroupDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsProductGroupDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsProductGroupDetails.LocalTax.ToString("#,##0.#0");
		}
		private void LoadRecord()
		{
			ProductGroupVariationsMatrix clsProductGroupVariationsMatrix = new ProductGroupVariationsMatrix();
			ProductGroupBaseVariationsMatrixDetails clsBaseDetails = clsProductGroupVariationsMatrix.BaseDetails(Convert.ToInt32(lblMatrixID.Text), Convert.ToInt32(lblGroupID.Text));
			clsProductGroupVariationsMatrix.CommitAndDispose();

			cboUnit.SelectedIndex = cboUnit.Items.IndexOf(cboUnit.Items.FindByValue(clsBaseDetails.UnitID.ToString()));
			txtProductPrice.Text = clsBaseDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsBaseDetails.PurchasePrice.ToString("#,##0.#0");
            decimal decMargin = clsBaseDetails.Price - clsBaseDetails.PurchasePrice;
            try { decMargin = decMargin / clsBaseDetails.PurchasePrice; }
            catch { decMargin = 1; }
            decMargin = decMargin * 100;
            txtMargin.Text = decMargin.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsBaseDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsBaseDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsBaseDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsBaseDetails.LocalTax.ToString("#,##0.#0");
			
		}

		#endregion

    }
}
