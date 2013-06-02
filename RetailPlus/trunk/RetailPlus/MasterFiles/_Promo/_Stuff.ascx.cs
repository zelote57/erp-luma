namespace AceSoft.RetailPlus.MasterFiles._Promo
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
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgDelete_Click);
			this.lstStuff.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstStuff_ItemDataBound);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
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
            cboContact.DataSource = clsContact.CustomersDataTable(txtContactCode.Text).DefaultView;
			cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
			cboContact.SelectedIndex = 0;

            ProductGroup clsProductGroup = new ProductGroup(clsContact.Connection, clsContact.Transaction);
			cboProductGroup.DataTextField = "ProductGroupName";
			cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = clsProductGroup.SearchDataTable(txtProductGroupCode.Text).DefaultView;
			cboProductGroup.DataBind();
			cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
			cboProductGroup.SelectedIndex = 0;

            clsContact.CommitAndDispose();

			cboProductGroup_SelectedIndexChanged(null, System.EventArgs.Empty);

            txtQuantity.Text = "1";
            txtPromoValue.Text = "0";
		}

		private void LoadList()
		{	
			Int64 PromoID =  Convert.ToInt64(lblPromoID.Text);
			PromoItems clsPromoItems = new PromoItems();
			DataClass clsDataClass = new DataClass();

			lstStuff.DataSource = clsDataClass.DataReaderToDataTable(clsPromoItems.List(PromoID, "PromoItemsID",SortOption.Ascending)).DefaultView;
			lstStuff.DataBind();
			clsPromoItems.CommitAndDispose();
		}

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			Promo clsPromo = new Promo();
			PromoDetails clsDetails = clsPromo.Details(iID);
			clsPromo.CommitAndDispose();

			lblPromoID.Text = clsDetails.PromoID.ToString();
			txtPromoCode.Text = clsDetails.PromoCode;
			txtPromoName.Text = clsDetails.PromoName;
			txtStartDate.Text = clsDetails.StartDate.ToString("yyyy-MM-dd HH:mm");
			txtEndDate.Text = clsDetails.EndDate.ToString("yyyy-MM-dd HH:mm");
			txtPromoType.Text = clsDetails.PromoTypeName;

			if (clsDetails.PromoTypeID == 1)
			{	chkInPercentage.Checked = false;	}
			else if (clsDetails.PromoTypeID == 2)
			{	chkInPercentage.Checked = true;	}

			LoadList();

		}

		private void SaveRecord()
		{
			PromoItemsDetails clsDetails = new PromoItemsDetails();

			clsDetails.PromoID = Convert.ToInt64(lblPromoID.Text);
			clsDetails.ContactID = Convert.ToInt64(cboContact.SelectedItem.Value);
			clsDetails.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
			clsDetails.ProductSubGroupID = Convert.ToInt64(cboSubGroup.SelectedItem.Value);
			clsDetails.ProductID = Convert.ToInt64(cboProducts.SelectedItem.Value);
			clsDetails.VariationMatrixID = Convert.ToInt64(cboProductVariation.SelectedItem.Value);
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.PromoValue = Convert.ToDecimal(txtPromoValue.Text);
			clsDetails.InPercent = Convert.ToByte(chkInPercentage.Checked);

			PromoItems clsPromoItems = new PromoItems();
			clsPromoItems.Insert(clsDetails);
			clsPromoItems.CommitAndDispose();

			LoadList();
		}

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();			
            //string stID = lblPromoID.Text;
            //string stParam = "?task=" + Common.Encrypt("stuff",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
            //Response.Redirect("Default.aspx" + stParam);
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
            //string stID = lblPromoID.Text;
            //string stParam = "?task=" + Common.Encrypt("stuff",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
            //Response.Redirect("Default.aspx" + stParam);
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

		private void lstStuff_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["PromoItemsID"].ToString();

				Label lblContactName = (Label) e.Item.FindControl("lblContactName");
				if (dr["ContactName"].ToString()==string.Empty)
				{
					lblContactName.Text = "All Contacts";
				}
				else
				{
					lblContactName.Text = dr["ContactName"].ToString();
				}

				Label lblProductGroup = (Label) e.Item.FindControl("lblProductGroup");
				if (dr["ProductGroupName"].ToString()==string.Empty)
				{
					lblProductGroup.Text = "All Groups";
				}
				else
				{
					lblProductGroup.Text = dr["ProductGroupName"].ToString();
				}

				Label lblProductSubGroup = (Label) e.Item.FindControl("lblProductSubGroup");
				if (dr["ProductSubGroupName"].ToString()==string.Empty)
				{
					lblProductSubGroup.Text = "All SubGroups";
				}
				else
				{
					lblProductSubGroup.Text = dr["ProductSubGroupName"].ToString();
				}

				Label lblProduct = (Label) e.Item.FindControl("lblProduct");
				if (dr["ProductDesc"].ToString()==string.Empty)
				{
					lblProduct.Text = "All Products";
				}
				else
				{
					lblProduct.Text = dr["ProductDesc"].ToString();
				}

				Label lblVariation = (Label) e.Item.FindControl("lblVariation");
				if (dr["Description"].ToString()==string.Empty)
				{
					lblVariation.Text = "All Variations";
				}
				else
				{
					lblVariation.Text = dr["Description"].ToString();
				}

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#");

				Label lblPromoValue = (Label) e.Item.FindControl("lblPromoValue");
				lblPromoValue.Text = Convert.ToDecimal(dr["PromoValue"].ToString()).ToString("#,##0.#");

				CheckBox chkInPercent = (CheckBox) e.Item.FindControl("chkInPercent");
				chkInPercent.Checked = Convert.ToBoolean(Convert.ToByte(dr["InPercent"].ToString()));

				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
//				
//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}

		protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupCode = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

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
            cboProductGroup.DataSource = clsProductGroup.SearchDataTable(txtProductGroupCode.Text).DefaultView;
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
            cboContact.DataSource = clsContact.CustomersDataTable(txtContactCode.Text).DefaultView;
            cboContact.DataBind();
            cboContact.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            if (cboContact.Items.Count > 1) cboContact.SelectedIndex = 1; else cboContact.SelectedIndex = 0;
            clsContact.CommitAndDispose();
        }

        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            cboProductSubGroup_SelectedIndexChanged(null, System.EventArgs.Empty);
        }

		private void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
				PromoItems clsPromoItems = new PromoItems();
				clsPromoItems.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsPromoItems.CommitAndDispose();
			}

			return boRetValue;
		}

        

		
	}
}
