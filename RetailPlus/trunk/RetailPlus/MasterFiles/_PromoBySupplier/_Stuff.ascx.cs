namespace AceSoft.RetailPlus.MasterFiles._PromoBySupplier
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Stuff : System.Web.UI.UserControl
	{
		
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadOptions();	
					LoadRecord();
					cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
					imgDelete.Attributes.Add("onClick", "return confirm_delete();");
				}
			}
		}

		private void LoadOptions()
		{
			Contacts clsContact = new Contacts();
			cboContact.DataTextField = "ContactName";
			cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text).DefaultView;
			cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
			cboContact.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsContact.Connection, clsContact.Transaction);
			cboProductGroup.DataTextField = "ProductGroupName";
			cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroupCode.Text, "ProductGroupName").DefaultView;
			cboProductGroup.DataBind();
			cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
			cboProductGroup.SelectedIndex = 0;

            clsContact.CommitAndDispose();

			cboProductGroup_SelectedIndexChanged(null, System.EventArgs.Empty);

            txtPromoBySupplierValue.Text = "0";

            txtCouponRemarks.Text =  "{DateNow}: Congratulations you qualified for our annual raffle promo brought to you by HOUSEWARE PLAZA SUPERSTORE.";
            txtCouponRemarks.Text += Environment.NewLine + Environment.NewLine + "Please proceed to our customer service for more info.";
            txtCouponRemarks.Text += Environment.NewLine + Environment.NewLine + "Total Amount Puchased: {Amount}";
            txtCouponRemarks.Text += Environment.NewLine + "OR No: {ORNo}";
            txtCouponRemarks.Text += Environment.NewLine + "Customer Name: {CustomerName}";
		}

		private void LoadList()
		{	
			Int64 PromoBySupplierID =  Convert.ToInt64(lblPromoBySupplierID.Text);
			PromoBySupplierItems clsPromoBySupplierItems = new PromoBySupplierItems();
			lstStuff.DataSource = clsPromoBySupplierItems.ListAsDataTable(PromoBySupplierID).DefaultView;
			lstStuff.DataBind();
			clsPromoBySupplierItems.CommitAndDispose();
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			PromoBySupplier clsPromoBySupplier = new PromoBySupplier();
			PromoBySupplierDetails clsDetails = clsPromoBySupplier.Details(iID);
			clsPromoBySupplier.CommitAndDispose();

			lblPromoBySupplierID.Text = clsDetails.PromoBySupplierID.ToString();
			txtPromoBySupplierCode.Text = clsDetails.PromoBySupplierCode;
			txtPromoBySupplierName.Text = clsDetails.PromoBySupplierName;
			txtStartDate.Text = clsDetails.StartDate.ToString("yyyy-MM-dd HH:mm");
			txtEndDate.Text = clsDetails.EndDate.ToString("yyyy-MM-dd HH:mm");

			LoadList();

		}

		private void SaveRecord()
		{
			PromoBySupplierItemsDetails clsDetails = new PromoBySupplierItemsDetails();

			clsDetails.PromoBySupplierID = Convert.ToInt64(lblPromoBySupplierID.Text);
			clsDetails.ContactID = Convert.ToInt64(cboContact.SelectedItem.Value);
			clsDetails.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
			clsDetails.ProductSubGroupID = Convert.ToInt64(cboSubGroup.SelectedItem.Value);
			clsDetails.ProductID = Convert.ToInt64(cboProducts.SelectedItem.Value);
			clsDetails.VariationMatrixID = Convert.ToInt64(cboProductVariation.SelectedItem.Value);
			clsDetails.PromoBySupplierValue = Convert.ToDecimal(txtPromoBySupplierValue.Text);
			clsDetails.CouponRemarks = txtCouponRemarks.Text;

			PromoBySupplierItems clsPromoBySupplierItems = new PromoBySupplierItems();
			clsPromoBySupplierItems.Insert(clsDetails);
			clsPromoBySupplierItems.CommitAndDispose();

			LoadList();
		}

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();			
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
		}

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

        protected void lstStuff_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["PromoBySupplierItemsID"].ToString();

				Label lblContactName = (Label) e.Item.FindControl("lblContactName");
                if (dr["ContactID"].ToString() == Constants.ZERO_STRING) lblContactName.Text = "All Suppliers"; else lblContactName.Text = dr["ContactName"].ToString();

				Label lblProductGroup = (Label) e.Item.FindControl("lblProductGroup");
				if (string.IsNullOrEmpty(dr["ProductGroupName"].ToString())) lblProductGroup.Text = "All Groups"; else lblProductGroup.Text = dr["ProductGroupName"].ToString();

				Label lblProductSubGroup = (Label) e.Item.FindControl("lblProductSubGroup");
				if (string.IsNullOrEmpty(dr["ProductSubGroupName"].ToString())) lblProductSubGroup.Text = "All SubGroups"; else lblProductSubGroup.Text = dr["ProductSubGroupName"].ToString();

				Label lblProduct = (Label) e.Item.FindControl("lblProduct");
				if (string.IsNullOrEmpty(dr["ProductDesc"].ToString()))  lblProduct.Text = "All Products";  else lblProduct.Text = dr["ProductDesc"].ToString();

				Label lblVariation = (Label) e.Item.FindControl("lblVariation");
				if (string.IsNullOrEmpty(dr["Description"].ToString())) lblVariation.Text = "All Variations"; else lblVariation.Text = dr["Description"].ToString();

				Label lblPromoBySupplierValue = (Label) e.Item.FindControl("lblPromoBySupplierValue");
				lblPromoBySupplierValue.Text = Convert.ToDecimal(dr["PromoBySupplierValue"].ToString()).ToString("#,##0.#");

                Label lblCouponRemarks = (Label)e.Item.FindControl("lblCouponRemarks");
                lblCouponRemarks.Text = dr["CouponRemarks"].ToString();

				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
//				
//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}

        protected void lstStuff_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");

            switch (e.CommandName)
            {
                case "imgItemDelete":
                    {
                        PromoBySupplierItems clsPromoBySupplierItems = new PromoBySupplierItems();
                        clsPromoBySupplierItems.Delete(chkList.Value);
                        clsPromoBySupplierItems.CommitAndDispose();
                        LoadList();
                    }
                    break;

            }
        }

		protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ColumnsNameID = true;

            ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
            clsSearchKeys.ProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);
            clsSearchKeys.ProductSubGroupCode = txtSubGroupCode.Text;

            ProductSubGroup clsSubGroup = new ProductSubGroup();
            cboSubGroup.DataTextField = "ProductSubGroupName";
            cboSubGroup.DataValueField = "ProductSubGroupID";
            cboSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0);
            cboSubGroup.DataBind();
            cboSubGroup.Items.Insert(0, new ListItem(Constants.ALL,Constants.ZERO_STRING));
            if (cboSubGroup.Items.Count > 1 && txtSubGroupCode.Text.Trim() != string.Empty) cboSubGroup.SelectedIndex = 1; else cboSubGroup.SelectedIndex = 0;
            clsSubGroup.CommitAndDispose();

			cboProductSubGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
		}

		protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataClass clsDataClass = new DataClass();
			Products clsProduct = new Products();

			cboProducts.DataTextField = "ProductCode";
			cboProducts.DataValueField = "ProductID";
            cboProducts.DataSource = clsProduct.ProductIDandCodeDataTable(ProductListFilterType.ShowActiveAndInactive, txtProductCode.Text.Trim(), 0, Convert.ToInt64(cboProductGroup.SelectedItem.Value), string.Empty, Convert.ToInt64(cboSubGroup.SelectedItem.Value));
			cboProducts.DataBind();
            cboProducts.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProducts.Items.Count > 1 && txtProductCode.Text.Trim() != string.Empty) cboProducts.SelectedIndex = 1; else cboProducts.SelectedIndex = 0;
			clsProduct.CommitAndDispose();

			cboProducts_SelectedIndexChanged(null, System.EventArgs.Empty);
		}

		protected void cboProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{

            long ProductID = Convert.ToInt64(cboProducts.SelectedItem.Value);
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();

			cboProductVariation.DataTextField = "VariationDesc";
			cboProductVariation.DataValueField = "MatrixID";
            cboProductVariation.DataSource = clsProductVariationsMatrix.BaseListSimpleAsDataTable(ProductID, SortField: "VariationDesc").DefaultView; 
			cboProductVariation.DataBind();
            cboProductVariation.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
			cboProductVariation.SelectedIndex = 0;

			clsProductVariationsMatrix.CommitAndDispose();
		}

        protected void imgSubGroupCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            cboProductGroup_SelectedIndexChanged(null, null);
        }

        protected void imgProductGroupCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            ProductGroup clsProductGroup = new ProductGroup();

            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(txtProductGroupCode.Text).DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboProductGroup.Items.Count > 1 && txtProductGroupCode.Text.Trim() != string.Empty) cboProductGroup.SelectedIndex = 1; else cboProductGroup.SelectedIndex = 0;
            clsProductGroup.CommitAndDispose();

            cboProductGroup_SelectedIndexChanged(null, null);
        }

        protected void imgContactCodeSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Contacts clsContact = new Contacts();
            cboContact.DataTextField = "ContactName";
            cboContact.DataValueField = "ContactID";
            cboContact.DataSource = clsContact.SuppliersAsDataTable(txtContactCode.Text).DefaultView;
            cboContact.DataBind();
            clsContact.CommitAndDispose();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContact.Items.Count > 1) cboContact.SelectedIndex = 1; else cboContact.SelectedIndex = 0;
        }

        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            cboProductSubGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
        }

        protected void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Delete())
				LoadList();
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadList();
		}	

		private bool Delete()
		{
			bool boRetValue = false;
			string stIDs = "";

			foreach(DataListItem item in lstStuff.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						stIDs += chkList.Value + ",";		
						boRetValue = true;
					}
				}
			}
			if (boRetValue)
			{
				PromoBySupplierItems clsPromoBySupplierItems = new PromoBySupplierItems();
				clsPromoBySupplierItems.Delete(stIDs.Substring(0,stIDs.Length-1));
				clsPromoBySupplierItems.CommitAndDispose();
			}

			return boRetValue;
		}
		
	}
}
