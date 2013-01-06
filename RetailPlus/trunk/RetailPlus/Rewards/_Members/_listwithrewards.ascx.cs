using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Rewards._Members
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 

	public partial  class __ListWithRewards : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack && Visible)
            {
                ManageSecurity();
                LoadOptions();
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
        private void InitializeComponent()
        {

        }

        #endregion

        #region Web Control Methods

        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
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
        protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                LoadSortFieldOptions(e);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ImageButton imgItemDelete = (ImageButton)e.Item.FindControl("imgItemDelete");
                ImageButton imgItemEdit = (ImageButton)e.Item.FindControl("imgItemEdit");

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["ContactID"].ToString();
                if (chkList.Value == "1" || chkList.Value == "2")
                {
                    chkList.Attributes.Add("disabled", "false");
                    imgItemDelete.Enabled = false; imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }
                else
                {
                    imgItemDelete.Enabled = cmdDelete.Visible; if (!imgItemDelete.Enabled) imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = cmdEdit.Visible; if (!imgItemEdit.Enabled) imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    if (imgItemDelete.Enabled) imgItemDelete.Attributes.Add("onClick", "return confirm_item_delete();");
                }

                HyperLink lnkContactCode = (HyperLink)e.Item.FindControl("lnkContactCode");
                lnkContactCode.Text = dr["ContactCode"].ToString();
                lnkContactCode.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                HyperLink lnkContactName = (HyperLink)e.Item.FindControl("lnkContactName");
                lnkContactName.Text = dr["ContactName"].ToString();
                lnkContactName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                Label lblBirthDate = (Label)e.Item.FindControl("lblBirthDate");
                lblBirthDate.Text = Convert.ToDateTime(dr["BirthDate"].ToString()).ToString("dd-MMM-yyyy");

                Label lblRewardCardNo = (Label)e.Item.FindControl("lblRewardCardNo");
                lblRewardCardNo.Text = dr["RewardCardNo"].ToString();

                Label lblRewardCardStatus = (Label)e.Item.FindControl("lblRewardCardStatus");
                lblRewardCardStatus.Text = Enum.Parse(typeof(RewardCardStatus), dr["RewardCardStatus"].ToString()).ToString();

                if (Convert.ToBoolean(dr["RewardActive"].ToString()))
                {
                    lblRewardCardStatus.Text += "(Active)";
                }else {lblRewardCardStatus.Text += "(InActive)";}

                Label lblRewardExpiryDate = (Label)e.Item.FindControl("lblRewardExpiryDate");
                lblRewardExpiryDate.Text = Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");

                Label lblRewardPoints = (Label)e.Item.FindControl("lblRewardPoints");
                lblRewardPoints.Text = dr["RewardPoints"].ToString();

                Label lblRewardRedeemedPoints = (Label)e.Item.FindControl("lblRewardRedeemedPoints");
                lblRewardRedeemedPoints.Text = dr["RedeemedPoints"].ToString();

                Label lblRewardTotalPurchases = (Label)e.Item.FindControl("lblRewardTotalPurchases");
                lblRewardTotalPurchases.Text = dr["TotalPurchases"].ToString();
            }
        }
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemDelete":
                    Contact clsContact = new Contact();
                    clsContact.Delete(chkList.Value);
                    clsContact.CommitAndDispose();

                    LoadList();
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
            }
        }

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
        }

        #endregion

		#region Private Methods

        private void LoadOptions()
        {
            cboRewardCardStatus.Items.Clear();

            foreach (RewardCardStatus selection in Enum.GetValues(typeof(RewardCardStatus)))
            {
                cboRewardCardStatus.Items.Add(new ListItem(selection.ToString("G"), selection.ToString("d")));
            }
            cboRewardCardStatus.Items.Insert(0, new ListItem(Constants.ALL, Constants.ALL));
            cboRewardCardStatus.SelectedIndex = 0;

            foreach (Constants.DateSelectionString selection in Enum.GetValues(typeof(Constants.DateSelectionString)))
            {
                cboBirthDate.Items.Add(new ListItem(selection.ToString("G"), selection.ToString("d")));
            }
            cboBirthDate.SelectedIndex = 0;
            
        }
		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Contacts); 
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

            System.Data.SqlClient.SortOrder sortoption = System.Data.SqlClient.SortOrder.Ascending;
			if (Request.QueryString["sortoption"]!=null)
                sortoption = (System.Data.SqlClient.SortOrder)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

            if (sortoption == System.Data.SqlClient.SortOrder.Ascending)
                stParam += "?sortoption=" + Common.Encrypt(System.Data.SqlClient.SortOrder.Descending.ToString("G"), Session.SessionID);
            else if (sortoption == System.Data.SqlClient.SortOrder.Descending)
                stParam += "?sortoption=" + Common.Encrypt(System.Data.SqlClient.SortOrder.Ascending.ToString("G"), Session.SessionID);

			System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString;;
			foreach(string querystring in querystrings.AllKeys)
			{
				if (querystring.ToLower() != "sortfield" && querystring.ToLower() != "sortoption") 
					stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
			}

			HyperLink SortByContactCode = (HyperLink) e.Item.FindControl("SortByContactCode");
			HyperLink SortByContactName = (HyperLink) e.Item.FindControl("SortByContactName");
            HyperLink SortByBirthDate = (HyperLink)e.Item.FindControl("SortByBirthDate");
			HyperLink SortByRewardCardNo = (HyperLink) e.Item.FindControl("SortByRewardCardNo");
            HyperLink SortByRewardCardStatus = (HyperLink)e.Item.FindControl("SortByRewardCardNo");
            HyperLink SortByRewardExpiryDate = (HyperLink)e.Item.FindControl("SortByRewardExpiryDate");
            HyperLink SortByRewardPoints = (HyperLink)e.Item.FindControl("SortByRewardPoints");
            HyperLink SortByRewardRedeemedPoints = (HyperLink)e.Item.FindControl("SortByRewardRedeemedPoints");
            HyperLink SortByRewardTotalPurchases = (HyperLink)e.Item.FindControl("SortByRewardTotalPurchases");

			SortByContactCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactCode", Session.SessionID);
			SortByContactName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactName", Session.SessionID);
            SortByBirthDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BirthDate", Session.SessionID);
            SortByRewardCardNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RewardCardNo", Session.SessionID);
            SortByRewardCardStatus.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RewardCardStatus", Session.SessionID);
            SortByRewardExpiryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ExpiryDate", Session.SessionID);
            SortByRewardPoints.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RewardPoints", Session.SessionID);
            SortByRewardRedeemedPoints.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RedeemedPoints", Session.SessionID);
            SortByRewardTotalPurchases.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TotalPurchases", Session.SessionID);
		}
		private void LoadList()
		{	
			Contact clsContact = new Contact();
			DataClass clsDataClass = new DataClass();
            ContactColumns clsContactColumns = new ContactColumns();
            clsContactColumns.ContactID = true;
            clsContactColumns.ContactCode = true;
            clsContactColumns.ContactName = true;
            clsContactColumns.RewardDetails = true;

            ContactColumns clsSearchColumns = new ContactColumns();
            clsSearchColumns.ContactCode = true;
            clsSearchColumns.ContactName = true;
            clsSearchColumns.RewardDetails = true;

			string SortField = "ContactID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			System.Data.SqlClient.SortOrder sortoption = System.Data.SqlClient.SortOrder.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (System.Data.SqlClient.SortOrder) Enum.Parse(typeof(System.Data.SqlClient.SortOrder), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            string SearchKey = string.Empty;
			if (Request.QueryString["Search"]!=null)
			{
                SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
                txtSearch.Text = SearchKey;
			}

            string strSearch = txtSearch.Text.Trim();

            Constants.DateSelectionString BirthDate = (Constants.DateSelectionString)Enum.Parse(typeof(Constants.DateSelectionString), cboBirthDate.SelectedItem.Value);
            
            DateTime dteRewardExpiryDateFrom = !DateTime.TryParse(txtRewardExpiryDateFrom.Text, out dteRewardExpiryDateFrom) ? dteRewardExpiryDateFrom : DateTime.MinValue;            
            DateTime dteRewardExpiryDateTo = !DateTime.TryParse(txtRewardExpiryDateTo.Text, out dteRewardExpiryDateTo) ? dteRewardExpiryDateTo : DateTime.MinValue;
            Int16 intRewardCardStatus = (cboRewardCardStatus.SelectedItem.Value == Constants.ALL) ? Int16.Parse("-1") : Int16.Parse(cboRewardCardStatus.SelectedItem.Value);

            PageData.DataSource = clsContact.CustomersWithRewards(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, 0, strSearch, dteRewardExpiryDateFrom, dteRewardExpiryDateTo, BirthDate, intRewardCardStatus, SortField, sortoption).DefaultView;

			clsContact.CommitAndDispose();

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
				Contact clsContact = new Contact();
				clsContact.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsContact.CommitAndDispose();
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
