using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __List : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

			if (!IsPostBack)
				if (Visible)
				{
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
			this.imgAdd.Click += new System.Web.UI.ImageClickEventHandler(this.imgAdd_Click);
			this.imgDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgDelete_Click);
			this.idEdit.Click += new System.Web.UI.ImageClickEventHandler(this.idEdit_Click);
			this.idCompose.Click += new System.Web.UI.ImageClickEventHandler(this.idCompose_Click);
			this.lstItem.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.lstItem_ItemCommand);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
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

		
		private void idEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}

		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
		
		private void idCompose_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Compose();
		}

		protected void cmdCompose_Click(object sender, System.EventArgs e)
		{
			Compose();
		}

        protected void idFinance_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SetupFinance();
        }
        protected void cmdFinance_Click(object sender, EventArgs e)
        {
            SetupFinance();
        }

		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}

		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Common Common = new Common();
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["ProductID"].ToString();

				Label lnkProductCode = (Label) e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr["ProductCode"].ToString();

				Label lnkBarCode = (Label) e.Item.FindControl("lnkBarCode");
				lnkBarCode.Text = dr["BarCode"].ToString();

				HyperLink lnkDescription = (HyperLink) e.Item.FindControl("lnkDescription");
				lnkDescription.Text = dr["ProductDesc"].ToString();
				lnkDescription.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				Label lnkGroup = (Label) e.Item.FindControl("lnkGroup");
				lnkGroup.Text = dr["ProductGroupName"].ToString() + " / " + dr["ProductSubGroupName"].ToString();

				Label lnkUnit = (Label) e.Item.FindControl("lnkUnit");
				lnkUnit.Text = dr["BaseUnitName"].ToString();

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

		private void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			Common Common = new Common();
			HtmlInputCheckBox chkList = null;
			string stParam = null;

			chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
			Common = new Common();
			stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
				"&prodid=" + Common.Encrypt(chkList.Value,Session.SessionID);

			switch(e.CommandName)
			{
				case "imgVariationsClick":
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Variations/Default.aspx" + stParam);
					break;
				case "imgVariationsMatrixClick":
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
					break;
				case "imgUnitsMatrixClick":
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
					break;
				case "imgPackageMatrixClick":
					Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_Package/Default.aspx" + stParam);
					break;
                case "imgFinanceClick":
                    stParam = "?task=" + Common.Encrypt("finance", Session.SessionID) +
                                "&prodid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
                    break;
			}
		}


		private void imgPrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateProductPrice();
		}

		private void cmdProductPriceUpdate_Click(object sender, System.EventArgs e)
		{
			UpdateProductPrice();
		}

		
		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Products); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			idEdit.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductComposition); 
			cmdCompose.Visible = clsDetails.Write; 
			idCompose.Visible = clsDetails.Write; 
			lblSeparator3.Visible = clsDetails.Write;

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.ItemSetupFinancial);
            cmdFinance.Visible = clsDetails.Write;
            idFinance.Visible = clsDetails.Write;
            lblSeparator4.Visible = clsDetails.Write;

			clsAccessRights.CommitAndDispose();
		}

		private void LoadSortFieldOptions(DataListItemEventArgs e)
		{
			Common Common = new Common();
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

			HyperLink SortByProductCode = (HyperLink) e.Item.FindControl("SortByProductCode");
			HyperLink SortByBarCode = (HyperLink) e.Item.FindControl("SortByBarCode");
			HyperLink SortByDescription = (HyperLink) e.Item.FindControl("SortByDescription");
			HyperLink SortByGroupName = (HyperLink) e.Item.FindControl("SortByGroupName");
			HyperLink SortByUnit = (HyperLink) e.Item.FindControl("SortByUnit");

			SortByProductCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductCode", Session.SessionID);
			SortByBarCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BarCode", Session.SessionID);
			SortByDescription.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductDesc", Session.SessionID);
			SortByGroupName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ProductGroupName", Session.SessionID);
			SortByUnit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("c.UnitName", Session.SessionID);
		}

		private void LoadList()
		{
            string SortField = "ProductDesc";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            string SearchKey = string.Empty;
            if (Request.QueryString["Search"] != null)
            { SearchKey = Server.UrlDecode(Common.Decrypt((string)Request.QueryString["search"], Session.SessionID)); }
            else if (Session["Search"] != null)
            { SearchKey = Server.UrlDecode(Common.Decrypt(Session["Search"].ToString(), Session.SessionID)); }

            try { Session.Remove("Search"); }
            catch { }
            if (SearchKey == null) { SearchKey = string.Empty; }
            else if (SearchKey != string.Empty) { Session.Add("Search", Common.Encrypt(SearchKey, Session.SessionID)); }

            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = SearchKey;
            clsSearchKeys.BarCode2 = SearchKey;
            clsSearchKeys.BarCode3 = SearchKey;
            clsSearchKeys.ProductCode = SearchKey;

            Products clsProduct = new Products();
            System.Data.DataTable dt = clsProduct.ListAsDataTable(clsSearchKeys: clsSearchKeys, limit: 100, SortField: SortField, SortOrder: SortOption.Ascending);
            clsProduct.CommitAndDispose();

            PageData.DataSource = dt.DefaultView;

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
				Products clsProduct = new Products();
                clsProduct.Delete(stIDs.Substring(0, stIDs.Length - 1), Convert.ToString(Session["Name"]));
				clsProduct.CommitAndDispose();

				Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();

				clsAuditDetails.ActivityDate = DateTime.Now;
				clsAuditDetails.User = Convert.ToString(Session["Name"]);
				clsAuditDetails.IPAddress = Request.UserHostAddress;
				clsAuditDetails.Activity = "Products";
				clsAuditDetails.Remarks = "Delete Product(s). IDs:'" + stIDs + "'";

				Security.AuditTrail clsAuditTrail = new Security.AuditTrail();
				clsAuditTrail.Insert(clsAuditDetails);
				clsAuditTrail.CommitAndDispose();
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
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
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

		private void Compose()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("compose",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot update product composition of more than one record. Please select at least one record to compose.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}

        private void SetupFinance()
        {
            if (isChkListSingle() == true)
            {
                string stID = GetFirstID();
                if (stID != null)
                {
                    Common Common = new Common();
                    string stParam = "?task=" + Common.Encrypt("finance", Session.SessionID) + "&id=" + Common.Encrypt(stID, Session.SessionID);
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


		private void UpdateProductPrice()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("ProductPrice",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);
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


		#endregion

        
    }
}
