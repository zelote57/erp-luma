using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Product._ProductPackage
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

        public string ProductCode
        { 
            set { lblProductCode.Text = value; }
        }

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
			this.imgAdd.Click += new System.Web.UI.ImageClickEventHandler(this.imgAdd_Click);
			this.imgDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgDelete_Click);
			this.idEdit.Click += new System.Web.UI.ImageClickEventHandler(this.idEdit_Click);
			this.idBack.Click += new System.Web.UI.ImageClickEventHandler(this.idBack_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}
		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["PackageID"].ToString();

                Label lblDescription = (Label)e.Item.FindControl("lblDescription");
                lblDescription.Text = dr["Description"].ToString();

				Label lblUnitName = (Label) e.Item.FindControl("lblUnitName");
				lblUnitName.Text = dr["UnitName"].ToString();

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

				Label lblPurchasePrice = (Label) e.Item.FindControl("lblPurchasePrice");
				lblPurchasePrice.Text = Convert.ToDecimal(dr["PurchasePrice"].ToString()).ToString("#,##0.#0");

				Label lblPrice = (Label) e.Item.FindControl("lblPrice");
				lblPrice.Text = Convert.ToDecimal(dr["Price"].ToString()).ToString("#,##0.#0");

				Label lblVAT = (Label) e.Item.FindControl("lblVAT");
				lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0") + " %"; 

				Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
				lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0") + " %"; 

				Label lblLocalTax = (Label) e.Item.FindControl("lblLocalTax");
				lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0") + " %"; 

//				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
//
//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";

			}
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
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}
		private void idBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
		}
		protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
        protected void imgChangePrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(lblProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }
        protected void cmdChangePrice_Click(object sender, EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("changeprice", Session.SessionID) +
                                "&productcode=" + Common.Encrypt(lblProductCode.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
        }
        protected void imgCopyToAllMatrix_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CopyToMatrixPackage();
        }
        protected void cmdCopyToAllMatrix_Click(object sender, EventArgs e)
        {
            CopyToMatrixPackage();
        }

		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductPackage); 
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

			HyperLink SortByUnit = (HyperLink) e.Item.FindControl("SortByUnit");
			HyperLink SortByQuantity = (HyperLink) e.Item.FindControl("SortByQuantity");
			HyperLink SortByPurchasePrice = (HyperLink) e.Item.FindControl("SortByPurchasePrice");
			HyperLink SortByPrice = (HyperLink) e.Item.FindControl("SortByPrice");
			HyperLink SortByVAT = (HyperLink) e.Item.FindControl("SortByVAT");
			HyperLink SortByEVAT = (HyperLink) e.Item.FindControl("SortByEVAT");
			HyperLink SortByLocalTax = (HyperLink) e.Item.FindControl("SortByLocalTax");

			SortByUnit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("c.UnitName", Session.SessionID);
			SortByQuantity.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.Quantity", Session.SessionID);
			SortByPurchasePrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.PurchasePrice", Session.SessionID);
			SortByPrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.Price", Session.SessionID);
			SortByVAT.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.VAT", Session.SessionID);
			SortByEVAT.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.EVAT", Session.SessionID);
			SortByLocalTax.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.LocalTax", Session.SessionID);
		}
		private void LoadList()
		{	
			string SortField = "PackageID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

            SortOption SortOrder = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
            { SortOrder = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            ProductPackage clsProductPackage = new ProductPackage();
            System.Data.DataTable dt = clsProductPackage.ListAsDataTable(Convert.ToInt64(lblProductID.Text), SortField: SortField, SortOrder: SortOrder);
			clsProductPackage.CommitAndDispose();

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
				ProductPackage clsProductPackage = new ProductPackage();
				clsProductPackage.Delete(stIDs.Substring(0,stIDs.Length-1));
				clsProductPackage.CommitAndDispose();

				Security.AuditTrailDetails clsAuditDetails = new Security.AuditTrailDetails();

				clsAuditDetails.ActivityDate = DateTime.Now;
				clsAuditDetails.User = Convert.ToString(Session["Name"]);
				clsAuditDetails.IPAddress = Request.UserHostAddress;
				clsAuditDetails.Activity = "Product Package";
				clsAuditDetails.Remarks = "Delete Product Package(s). ProductID:'" + lblProductID.Text + "' IDs:'" + stIDs + "'";

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
        private void CopyToMatrixPackage()
        {
            //ProductPackage clsProductPackage = new ProductPackage();
            //clsProductPackage.CopyToMatrixPackage(long.Parse(lblProductID.Text));
            //clsProductPackage.CommitAndDispose();
        }

		#endregion

    }
}
