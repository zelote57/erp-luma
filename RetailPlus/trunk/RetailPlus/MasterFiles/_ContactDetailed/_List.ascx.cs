using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._ContactDetailed
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

                HyperLink lnkBusinessName = (HyperLink)e.Item.FindControl("lnkBusinessName");
                lnkBusinessName.Text = dr["BusinessName"].ToString();
                lnkBusinessName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                HyperLink lnkContactGroupName = (HyperLink)e.Item.FindControl("lnkContactGroupName");
                lnkContactGroupName.Text = dr["ContactGroupName"].ToString();
                lnkContactGroupName.NavigateUrl =  Constants.ROOT_DIRECTORY + "/MasterFiles/_ContactGroup/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr["ContactGroupID"].ToString(), Session.SessionID);
                
                Label lblRewardCardNo = (Label)e.Item.FindControl("lblRewardCardNo");
                lblRewardCardNo.Text = dr["RewardCardNo"].ToString();

                if (!string.IsNullOrEmpty(lblRewardCardNo.Text)) {
                    Label lblRewardCardStatus = (Label)e.Item.FindControl("lblRewardCardStatus");
                    lblRewardCardStatus.Text = Enum.Parse(typeof(RewardCardStatus), dr["RewardCardStatus"].ToString()).ToString();

                    if (Convert.ToBoolean(dr["RewardActive"].ToString()))
                    {
                        lblRewardCardStatus.Text += "(Active)";
                    }else {lblRewardCardStatus.Text += "(InActive)";}

                    Label lblRewardExpiryDate = (Label)e.Item.FindControl("lblRewardExpiryDate");
                    lblRewardExpiryDate.Text = string.IsNullOrEmpty(dr["ExpiryDate"].ToString()) ? "" : Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                DateTime dteBirthDate = Constants.C_DATE_MIN_VALUE;
                DateTime dteSpouseBirthDate = Constants.C_DATE_MIN_VALUE;
                DateTime dteAnniversaryDate = Constants.C_DATE_MIN_VALUE;

                dteBirthDate = DateTime.TryParse(dr["BirthDate"].ToString(), out dteBirthDate) ? dteBirthDate : Constants.C_DATE_MIN_VALUE;
                dteSpouseBirthDate = DateTime.TryParse(dr["SpouseBirthDate"].ToString(), out dteSpouseBirthDate) ? dteSpouseBirthDate : Constants.C_DATE_MIN_VALUE;
                dteAnniversaryDate = DateTime.TryParse(dr["AnniversaryDate"].ToString(), out dteAnniversaryDate) ? dteAnniversaryDate : Constants.C_DATE_MIN_VALUE;

                Label lblBirthDate = (Label)e.Item.FindControl("lblBirthDate");
                lblBirthDate.Text = dteBirthDate == Constants.C_DATE_MIN_VALUE ? "" : dteBirthDate.ToString("dd-MMM-yyyy");

                Label lblSpouseName = (Label)e.Item.FindControl("lblSpouseName");
                lblSpouseName.Text = dr["SpouseName"].ToString();

                Label lblSpouseBirthDate = (Label)e.Item.FindControl("lblSpouseBirthDate");
                lblSpouseBirthDate.Text = dteSpouseBirthDate == Constants.C_DATE_MIN_VALUE ? "" : dteSpouseBirthDate.ToString("dd-MMM-yyyy");

                Label lblAnniversaryDate = (Label)e.Item.FindControl("lblAnniversaryDate");
                lblAnniversaryDate.Text = dteAnniversaryDate == Constants.C_DATE_MIN_VALUE ? "" : dteAnniversaryDate.ToString("dd-MMM-yyyy");

            }
        }
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemDelete":
                    Contacts clsContact = new Contacts();
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

        #endregion

		#region Private Methods

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

			HyperLink SortByContactCode = (HyperLink) e.Item.FindControl("SortByContactCode");
			HyperLink SortByContactName = (HyperLink) e.Item.FindControl("SortByContactName");
            HyperLink SortByBusinessName = (HyperLink)e.Item.FindControl("SortByBusinessName");
            HyperLink SortByContactGroupName = (HyperLink)e.Item.FindControl("SortByContactGroupName");
			HyperLink SortByRewardCardNo = (HyperLink) e.Item.FindControl("SortByRewardCardNo");
            HyperLink SortByRewardExpiryDate = (HyperLink)e.Item.FindControl("SortByRewardExpiryDate");
            HyperLink SortByBirthDate = (HyperLink)e.Item.FindControl("SortByBirthDate");
            HyperLink SortBySpouseName = (HyperLink)e.Item.FindControl("SortBySpouseName");
            HyperLink SortBySpouseBirthDate = (HyperLink)e.Item.FindControl("SortBySpouseBirthDate");
            HyperLink SortByAnniversaryDate = (HyperLink)e.Item.FindControl("SortByAnniversaryDate");

			SortByContactCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactCode", Session.SessionID);
			SortByContactName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactName", Session.SessionID);
            SortByBusinessName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BusinessName", Session.SessionID);
            SortByContactGroupName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactGroupName", Session.SessionID);
            SortByRewardCardNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RewardCardNo", Session.SessionID);
            SortByRewardExpiryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ExpiryDate", Session.SessionID);
            SortByBirthDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("addon.BirthDate", Session.SessionID);
            SortBySpouseName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SpouseName", Session.SessionID);
            SortBySpouseBirthDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SpouseBirthDate", Session.SessionID);
            SortByAnniversaryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("AnniversaryDate", Session.SessionID);
		}

        private void LoadOptions()
        {
            ContactGroup clsContactGroup = new ContactGroup();

            cboGroup.DataTextField = "ContactGroupName";
            cboGroup.DataValueField = "ContactGroupID";
            cboGroup.DataSource = clsContactGroup.ListAsDataTable(ContactGroupCategory.CUSTOMER).DefaultView;
            cboGroup.DataBind();
            cboGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboGroup.SelectedIndex = 0;

            cboBirthMonth.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboBirthMonth.Items.Add(new ListItem("January", "1"));
            cboBirthMonth.Items.Add(new ListItem("February", "2"));
            cboBirthMonth.Items.Add(new ListItem("March", "3"));
            cboBirthMonth.Items.Add(new ListItem("April", "4"));
            cboBirthMonth.Items.Add(new ListItem("May", "5"));
            cboBirthMonth.Items.Add(new ListItem("June", "6"));
            cboBirthMonth.Items.Add(new ListItem("July", "7"));
            cboBirthMonth.Items.Add(new ListItem("August", "8"));
            cboBirthMonth.Items.Add(new ListItem("September", "9"));
            cboBirthMonth.Items.Add(new ListItem("October", "10"));
            cboBirthMonth.Items.Add(new ListItem("November", "11"));
            cboBirthMonth.Items.Add(new ListItem("December", "12"));
            cboBirthMonth.SelectedIndex = DateTime.Now.Month;

            cboAnniversaryMonth.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboAnniversaryMonth.Items.Add(new ListItem("January", "1"));
            cboAnniversaryMonth.Items.Add(new ListItem("February", "2"));
            cboAnniversaryMonth.Items.Add(new ListItem("March", "3"));
            cboAnniversaryMonth.Items.Add(new ListItem("April", "4"));
            cboAnniversaryMonth.Items.Add(new ListItem("May", "5"));
            cboAnniversaryMonth.Items.Add(new ListItem("June", "6"));
            cboAnniversaryMonth.Items.Add(new ListItem("July", "7"));
            cboAnniversaryMonth.Items.Add(new ListItem("August", "8"));
            cboAnniversaryMonth.Items.Add(new ListItem("September", "9"));
            cboAnniversaryMonth.Items.Add(new ListItem("October", "10"));
            cboAnniversaryMonth.Items.Add(new ListItem("November", "11"));
            cboAnniversaryMonth.Items.Add(new ListItem("December", "12"));
            cboAnniversaryMonth.SelectedIndex = 0;

        }
		private void LoadList()
		{	
			Contacts clsContact = new Contacts();
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
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            string SearchKey = txtSearch.Text;
            string ContactGroupCode = "";
            ContactGroupCode = cboGroup.SelectedIndex == 0 ? "" : cboGroup.SelectedItem.Text;

            DateTime dteBirthDateFrom = Constants.C_DATE_MIN_VALUE;
            DateTime dteBirthDateTo = Constants.C_DATE_MIN_VALUE;
            DateTime dteAnniversaryDateFrom = Constants.C_DATE_MIN_VALUE;
            DateTime dteAnniversaryDateTo = Constants.C_DATE_MIN_VALUE;

            dteBirthDateFrom = DateTime.TryParse(txtBirthStartDate.Text, out dteBirthDateFrom) ? dteBirthDateFrom : Constants.C_DATE_MIN_VALUE;
            dteBirthDateTo = DateTime.TryParse(txtBirthEndDate.Text, out dteBirthDateTo) ? dteBirthDateTo : Constants.C_DATE_MIN_VALUE;
            dteAnniversaryDateFrom = DateTime.TryParse(txtAnnivStartDate.Text, out dteAnniversaryDateFrom) ? dteAnniversaryDateFrom : Constants.C_DATE_MIN_VALUE;
            dteAnniversaryDateTo = DateTime.TryParse(txtAnnivEndDate.Text, out dteAnniversaryDateTo) ? dteAnniversaryDateTo : Constants.C_DATE_MIN_VALUE;

            //PageData.DataSource = clsContact.Customers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsSearchColumns, SearchKey, 0, false, null, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            PageData.DataSource = clsContact.ListAsDataTable(ContactGroupCategory.CUSTOMER, ContactCode: SearchKey, ContactName: SearchKey, ContactGroupCode: ContactGroupCode, SortField: SortField, SortOrder: sortoption, BirthDateFrom: dteBirthDateFrom.ToString("yyyy-MM-dd"), BirthDateTo: dteBirthDateTo.ToString("yyyy-MM-dd"), AnniversaryDateFrom: dteAnniversaryDateFrom.ToString("yyyy-MM-dd"), AnniversaryDateTo: dteAnniversaryDateTo.ToString("yyyy-MM-dd"), BirthMonth: cboBirthMonth.SelectedIndex, AnniversaryMonth: cboAnniversaryMonth.SelectedIndex).DefaultView;
                //clsContact.CustomersDataTable(SearchKey, SortField: SortField, SortOrder: sortoption).DefaultView;

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
				Contacts clsContact = new Contacts();
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

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
        }
    }
}
