using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Product._VariationsMatrix
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 

	public partial  class __List : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.ImageButton imgSave;
		protected System.Web.UI.WebControls.LinkButton cmdSave;
		protected System.Web.UI.WebControls.ImageButton imgCancel;
		protected System.Web.UI.WebControls.LinkButton cmdCancel;
		protected System.Web.UI.WebControls.Label lblReferrer;
		protected PagedDataSource PageData = new PagedDataSource();

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

			if (!IsPostBack)
				if (Visible)
				{
					lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);

					LoadList();
					cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
					imgDelete.Attributes.Add("onClick", "return confirm_delete();");
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

        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
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


        protected void idEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}

		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}


        protected void idBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
		}

		protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}


		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}

        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["MatrixID"].ToString();

				HyperLink lnkVariation = (HyperLink) e.Item.FindControl("lnkVariation");
				lnkVariation.Text = dr["Description"].ToString();
				lnkVariation.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["MatrixID"].ToString(), Session.SessionID);

				Label lblUnitName = (Label) e.Item.FindControl("lblUnitName");
				lblUnitName.Text = dr["UnitName"].ToString();

				Label lblPrice = (Label) e.Item.FindControl("lblPrice");
				lblPrice.Text = Convert.ToDecimal(dr["Price"].ToString()).ToString("#,##0.#0");

				Label lblPurchasePrice = (Label) e.Item.FindControl("lblPurchasePrice");
				lblPurchasePrice.Text = Convert.ToDecimal(dr["PurchasePrice"].ToString()).ToString("#,##0.#0");

				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";

				Label lblVAT = (Label) e.Item.FindControl("lblVAT");
				lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0") + " %"; 

				Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
				lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0") + " %"; 

				Label lblLocalTax = (Label) e.Item.FindControl("lblLocalTax");
				lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0") + " %"; 

			}
		}				
		
		protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			HtmlInputCheckBox chkList = null;
			string stParam = null;

			chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
			stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
				"&matrixid=" + Common.Encrypt(chkList.Value,Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID) ;

			switch(e.CommandName)
			{
				case "imgPackageMatrixClick":
					Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_MatrixPackage/Default.aspx" + stParam);
					break;
			}
		}

		
		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductVariations); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			idEdit.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;

			clsAccessRights.CommitAndDispose();
		}

		private void LoadSortFieldOptions(DataListItemEventArgs e)
		{
			string stParam = null;		

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
				sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

			if (sortoption == SortOption.Ascending)
				stParam += "?sortoption=" + Common.Encrypt(SortOption.Desscending.ToString("G"), Session.SessionID);
			else if (sortoption == SortOption.Desscending)
				stParam += "?sortoption=" + Common.Encrypt(SortOption.Ascending.ToString("G"), Session.SessionID);

			System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString;;
			foreach(string querystring in querystrings.AllKeys)
			{
				if (querystring.ToLower() != "sortfield" && querystring.ToLower() != "sortoption") 
					stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
			}

			HyperLink SortByVariation = (HyperLink) e.Item.FindControl("SortByVariation");
			HyperLink SortByUnit = (HyperLink) e.Item.FindControl("SortByUnit");
			HyperLink SortByPurchasePrice = (HyperLink) e.Item.FindControl("SortByPurchasePrice");
			HyperLink SortByPrice = (HyperLink) e.Item.FindControl("SortByPrice");

			SortByVariation.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Description", Session.SessionID);
			SortByUnit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("UnitName", Session.SessionID);
			SortByPurchasePrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PurchasePrice", Session.SessionID);
			SortByPrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Price", Session.SessionID);
		}

		private void LoadList()
		{	
			ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
			DataClass clsDataClass = new DataClass();

			string SortField = "MatriXID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationsMatrix.BaseList(Convert.ToInt64(lblProductID.Text), SortField, sortoption)).DefaultView;
			}
			else
			{	
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductVariationsMatrix.Search(Convert.ToInt64(lblProductID.Text), SearchKey, SortField, sortoption)).DefaultView;
			}

			clsProductVariationsMatrix.CommitAndDispose();

			int iPageSize = Convert.ToInt16(Session["PageSize"]) ;
			
			PageData.AllowPaging = true;
			PageData.PageSize = iPageSize;
			try
			{
				PageData.CurrentPageIndex = Convert.ToInt16(cboCurrentPage.SelectedItem.Value) - 1;				
				lstItem.DataSource = PageData;
				lstItem.DataBind();
			}
			catch
			{
				PageData.CurrentPageIndex = 1;
				lstItem.DataSource = PageData;
				lstItem.DataBind();
			}			
			
			cboCurrentPage.Items.Clear();
			for (int i=0; i < PageData.PageCount;i++)
			{
				int iValue = i + 1;
				cboCurrentPage.Items.Add(new ListItem(iValue.ToString(),iValue.ToString()));
				if (PageData.CurrentPageIndex == i)
				{	cboCurrentPage.Items[i].Selected = true;}
				else
				{	cboCurrentPage.Items[i].Selected = false;}
			}
			lblDataCount.Text = " of " + " " + PageData.PageCount;
		}

		private bool Delete()
		{
			bool boRetValue = false;
			string stIDs = "";

			foreach(DataListItem item in lstItem.Items)
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
                Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails) Session["AccessUserDetails"];
				ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                clsProductVariationsMatrix.GetConnection();

                Products clsProduct = new Products(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);
                ProductDetails clsProductDetails = clsProduct.Details(Convert.ToInt64(lblProductID.Text));

                string[] strIDs = stIDs.Substring(0,stIDs.Length-1).Split(',');
                foreach (string ID in strIDs)
                {
                    ProductBaseMatrixDetails clsBaseDetails = clsProductVariationsMatrix.BaseDetailsByMatrixID(long.Parse(ID));
                    
                    InvAdjustmentDetails clsInvAdjustmentDetails = new InvAdjustmentDetails();
                    clsInvAdjustmentDetails.UID = clsAccessUserDetails.UID;
                    clsInvAdjustmentDetails.InvAdjustmentDate = DateTime.Now;
                    clsInvAdjustmentDetails.ProductID = clsProductDetails.ProductID;
                    clsInvAdjustmentDetails.ProductCode = clsProductDetails.ProductCode;
                    clsInvAdjustmentDetails.Description = clsProductDetails.ProductDesc;
                    clsInvAdjustmentDetails.VariationMatrixID = clsBaseDetails.MatrixID;
                    clsInvAdjustmentDetails.MatrixDescription = clsBaseDetails.Description;
                    clsInvAdjustmentDetails.UnitID = clsBaseDetails.UnitID;
                    clsInvAdjustmentDetails.UnitCode = clsProductDetails.BaseUnitCode;
                    clsInvAdjustmentDetails.QuantityBefore = clsBaseDetails.Quantity;
                    clsInvAdjustmentDetails.QuantityNow = 0;
                    clsInvAdjustmentDetails.MinThresholdBefore = clsBaseDetails.MinThreshold;
                    clsInvAdjustmentDetails.MinThresholdNow = 0;
                    clsInvAdjustmentDetails.MaxThresholdBefore = clsBaseDetails.MaxThreshold;
                    clsInvAdjustmentDetails.MaxThresholdNow = 0;
                    clsInvAdjustmentDetails.Remarks = "deleted item.";

                    InvAdjustment clsInvAdjustment = new InvAdjustment(clsProduct.Connection, clsProduct.Transaction);
                    clsInvAdjustment.Insert(clsInvAdjustmentDetails);

                    // Aug 1, 2011 : Lemu
                    // Replaced clsProductVariationsMatrix.SynchronizeQuantity(Convert.ToInt64(lblProductID.Text));
                    clsProduct.SubtractQuantity(Constants.BRANCH_ID_MAIN, clsProductDetails.ProductID, clsBaseDetails.MatrixID, clsBaseDetails.Quantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PRODUCT_VARIATION_DELETE) + " : " + clsBaseDetails.Description, clsInvAdjustmentDetails.InvAdjustmentDate, "SYS-VARDEL" + clsInvAdjustmentDetails.InvAdjustmentDate.ToString("yyyyMMddHHmmss"), clsAccessUserDetails.Name);

                    clsProductVariationsMatrix.Delete(long.Parse(ID));
                }

                // Aug 1, 2011 : Lemu
                // Deleted and replaced by subtracting each matrix in the product to include in the ProductMovement report
                // clsProduct.SubtractQuantity(clsProductDetails.ProductID, 0, clsBaseDetails.Quantity, Product.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PRODUCT_VARIATION_DELETE) + " : " + clsBaseDetails.Description, clsInvAdjustmentDetails.InvAdjustmentDate, "SYS-VARDEL" + clsInvAdjustmentDetails.InvAdjustmentDate.ToString(""), clsAccessUserDetails.Name);
				// clsProductVariationsMatrix.SynchronizeQuantity(Convert.ToInt64(lblProductID.Text));

				clsProductVariationsMatrix.CommitAndDispose();
			}

			return boRetValue;
		}


		private void Update()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot update more than one record. Please select at least one record to update.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}

		private string GetFirstID()
		{
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						return chkList.Value;
					}
				}
			}
			return null;
		}
		private bool isChkListSingle()
		{
			bool boChkListSingle = true;
			int iCount = 0;
			
			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						iCount += 1;
						if (iCount >= 2)
						{	return false;	}
					}
				}
			}
			return boChkListSingle;
		}


		#endregion
        
}
}
