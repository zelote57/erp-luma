using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Credits._Guarantors
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

                Label lblCreditType = (Label)e.Item.FindControl("lblCreditType");
                lblCreditType.Text = dr["CardTypeCode"].ToString().ToString();

                Label lblCreditCardNo = (Label)e.Item.FindControl("lblCreditCardNo");
                lblCreditCardNo.Text = dr["CreditCardNo"].ToString();

                Label lblCreditCardStatus = (Label)e.Item.FindControl("lblCreditCardStatus");
                lblCreditCardStatus.Text = Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString()).ToString();

                Label lblExpiryDate = (Label)e.Item.FindControl("lblExpiryDate");
                lblExpiryDate.Text = Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");

                decimal decCreditLimit = Convert.ToDecimal(dr["CreditLimit"].ToString());
                decimal decCredit = Convert.ToDecimal(dr["Credit"].ToString());
                decimal decAvailableCredit = decCreditLimit - decCredit;

                Label lblCreditLimit = (Label)e.Item.FindControl("lblCreditLimit");
                lblCreditLimit.Text = decCreditLimit.ToString("#,##0.#");

                Label lblCredit = (Label)e.Item.FindControl("lblCredit");
                lblCredit.Text = decCredit.ToString("#,##0.#");

                Label lblAvailableCredit = (Label)e.Item.FindControl("lblAvailableCredit");
                lblAvailableCredit.Text = decAvailableCredit.ToString("#,##0.#");

                Label lblTotalPurchases = (Label)e.Item.FindControl("lblTotalPurchases");
                lblTotalPurchases.Text = Convert.ToDecimal(dr["TotalPurchases"].ToString()).ToString("#,##0.#");

                Label lblLastBillingDate = (Label)e.Item.FindControl("lblLastBillingDate");
                lblLastBillingDate.Text = Convert.ToDateTime(dr["LastBillingDate"].ToString()).ToString("dd-MMM-yyyy");

                
                Contacts clsContact = new Contacts();
                DataClass clsDataClass = new DataClass();
                ContactColumns clsContactColumns = new ContactColumns();
                clsContactColumns.ContactID = true;
                clsContactColumns.ContactCode = true;
                clsContactColumns.ContactName = true;
                clsContactColumns.CreditDetails = true;

                ContactColumns clsSearchColumns = new ContactColumns();
                clsSearchColumns.ContactCode = true;
                clsSearchColumns.ContactName = true;
                clsSearchColumns.CreditDetails = true;

                Int64 iGuarantorID = Int64.Parse(dr["ContactID"].ToString());
                DataList lstItemCustomer = (DataList)e.Item.FindControl("lstItemCustomer");

                if (iGuarantorID != 0)
                {
                    string strSearch = txtSearch.Text.Trim();
                    DateTime dteExpiryDateFrom = DateTime.TryParse(txtExpiryDateFrom.Text, out dteExpiryDateFrom) ? dteExpiryDateFrom : DateTime.MinValue;
                    DateTime dteExpiryDateTo = DateTime.TryParse(txtExpiryDateTo.Text, out dteExpiryDateTo) ? dteExpiryDateTo : DateTime.MinValue;
                    CreditCardStatus enumCreditCardStatus = (CreditCardStatus)Enum.Parse(typeof(CreditCardStatus), cboCreditCardStatus.SelectedItem.Value);
                    System.Data.DataTable dt = clsContact.CustomersWithCredits(clsContactColumns, iGuarantorID, strSearch, dteExpiryDateFrom, dteExpiryDateTo, enumCreditCardStatus, Int32.Parse(cboCreditType.SelectedItem.Value));
                    clsContact.CommitAndDispose();
                    lstItemCustomer.DataSource = dt.DefaultView;
                    lstItemCustomer.DataBind();
                    lstItemCustomer.Visible = true;
                }
                else
                {
                    lstItemCustomer.Visible = false;
                }
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
        
        protected void lstItemCustomer_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //LoadSortFieldOptions(e);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ImageButton imgItemDelete = (ImageButton)e.Item.FindControl("imgItemDelete");
                ImageButton imgItemEdit = (ImageButton)e.Item.FindControl("imgItemEdit");
                ImageButton imgPrintBilling = (ImageButton)e.Item.FindControl("imgPrintBilling");

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["ContactID"].ToString();
                if (chkList.Value == "1" || chkList.Value == "2")
                {
                    chkList.Attributes.Add("disabled", "false");
                    imgItemDelete.Enabled = false; imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgPrintBilling.Enabled = false; ; imgPrintBilling.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }
                else
                {
                    imgItemDelete.Enabled = cmdDelete.Visible; if (!imgItemDelete.Enabled) imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = cmdEdit.Visible; if (!imgItemEdit.Enabled) imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    if (imgItemDelete.Enabled) imgItemDelete.Attributes.Add("onClick", "return confirm_item_delete();");

                    if (DateTime.Parse(dr["LastBillingDate"].ToString()) != DateTime.MinValue && DateTime.Parse(dr["LastBillingDate"].ToString()) != Constants.C_DATE_MIN_VALUE)
                    {
                        imgPrintBilling.Enabled = imgPrintBilling.Visible; if (!imgPrintBilling.Enabled) imgPrintBilling.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/print.gif";
                        imgPrintBilling.ToolTip = DateTime.Parse(dr["LastBillingDate"].ToString()).ToString("yyyy-MMM-dd");
                    }
                    else
                    {
                        imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                        imgPrintBilling.Enabled = false; ; imgPrintBilling.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    }
                }

                HyperLink lnkContactName = (HyperLink)e.Item.FindControl("lnkContactName");
                lnkContactName.Text = dr["ContactName"].ToString();
                lnkContactName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                Label lblCreditType = (Label)e.Item.FindControl("lblCreditType");
                lblCreditType.Text = dr["CardTypeCode"].ToString().ToString();

                Label lblCreditCardNo = (Label)e.Item.FindControl("lblCreditCardNo");
                lblCreditCardNo.Text = dr["CreditCardNo"].ToString();

                Label lblCreditCardStatus = (Label)e.Item.FindControl("lblCreditCardStatus");
                lblCreditCardStatus.Text = Enum.Parse(typeof(CreditCardStatus), dr["CreditCardStatus"].ToString()).ToString();

                Label lblExpiryDate = (Label)e.Item.FindControl("lblExpiryDate");
                lblExpiryDate.Text = Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");

                decimal decCreditLimit = Convert.ToDecimal(dr["CreditLimit"].ToString());
                decimal decCredit = Convert.ToDecimal(dr["Credit"].ToString());
                decimal decAvailableCredit = decCreditLimit - decCredit;

                Label lblCreditLimit = (Label)e.Item.FindControl("lblCreditLimit");
                lblCreditLimit.Text = decCreditLimit.ToString("#,##0.#");

                Label lblCredit = (Label)e.Item.FindControl("lblCredit");
                lblCredit.Text = decCredit.ToString("#,##0.#");

                Label lblAvailableCredit = (Label)e.Item.FindControl("lblAvailableCredit");
                lblAvailableCredit.Text = decAvailableCredit.ToString("#,##0.#");

                Label lblTotalPurchases = (Label)e.Item.FindControl("lblTotalPurchases");
                lblTotalPurchases.Text = Convert.ToDecimal(dr["TotalPurchases"].ToString()).ToString("#,##0.#");

                Label lblLastBillingDate = (Label)e.Item.FindControl("lblLastBillingDate");
                lblLastBillingDate.Text = Convert.ToDateTime(dr["LastBillingDate"].ToString()).ToString("dd-MMM-yyyy");
            }
        }

        protected void lstItemCustomer_ItemCommand(object sender, DataListCommandEventArgs e)
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

                case "imgPrintBilling":
                    ImageButton imgPrintBilling = (ImageButton)e.Item.FindControl("imgPrintBilling");
                    if (DateTime.Parse(imgPrintBilling.ToolTip) != DateTime.MinValue && DateTime.Parse(imgPrintBilling.ToolTip) != Constants.C_DATE_MIN_VALUE)
                    {
                        Billing clsBilling = new Billing();
                        System.Data.DataTable dt = clsBilling.ListBillingDateAsDataTable(long.Parse(chkList.Value), DateTime.Parse(imgPrintBilling.ToolTip));
                        clsBilling.CommitAndDispose();

                        if (dt.Rows.Count > 0)
                        {
                            string newWindowUrl = Constants.ROOT_DIRECTORY + "/billings/" + dt.Rows[0]["BillingFile"].ToString();
                            string javaScript = "window.open('" + newWindowUrl + "','_blank');";
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.lstItem, this.lstItem.GetType(), "openwindow", javaScript, true);
                        }
                    }
                    else
                    {
                        string javaScript = "window.alert('Sorry there is no billing file to print.');";
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.lstItem, this.lstItem.GetType(), "openwindow", javaScript, true);
                    }
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
            cboCreditCardStatus.Items.Clear();
            foreach (CreditCardStatus selection in Enum.GetValues(typeof(CreditCardStatus)))
            {
                cboCreditCardStatus.Items.Add(new ListItem(selection.ToString("G"), selection.ToString("d")));
            }
            cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(CreditCardStatus.All.ToString("d")));

            Data.CardType clsCardType = new Data.CardType();
            cboCreditType.Items.Clear();
            cboCreditType.DataTextField = "CardTypeCode";
            cboCreditType.DataValueField = "CardTypeID";
            cboCreditType.DataSource = clsCardType.ListAsDataTable(new CardTypeDetails(CreditCardTypes.Internal, true)).DefaultView;
            cboCreditType.DataBind();
            cboCreditType.SelectedIndex = 0;
            clsCardType.CommitAndDispose();

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
                sortoption = (System.Data.SqlClient.SortOrder)Enum.Parse(typeof(System.Data.SqlClient.SortOrder), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

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
            HyperLink SortByCreditType = (HyperLink)e.Item.FindControl("SortByCreditType");
            HyperLink SortByCreditCardNo = (HyperLink)e.Item.FindControl("SortByCreditCardNo");
            HyperLink SortByCreditCardStatus = (HyperLink)e.Item.FindControl("SortByCreditCardStatus");
            HyperLink SortByExpiryDate = (HyperLink)e.Item.FindControl("SortByExpiryDate");
            HyperLink SortByCreditLimit = (HyperLink)e.Item.FindControl("SortByCreditLimit");
            HyperLink SortByCredit = (HyperLink)e.Item.FindControl("SortByCredit");
            HyperLink SortByTotalPurchases = (HyperLink)e.Item.FindControl("SortByTotalPurchases");
            HyperLink SortByLastBillingDate = (HyperLink)e.Item.FindControl("SortByLastBillingDate");

			SortByContactCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactCode", Session.SessionID);
			SortByContactName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactName", Session.SessionID);
            SortByCreditType.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CreditType", Session.SessionID);
            SortByCreditCardNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CreditCardNo", Session.SessionID);
            SortByCreditCardStatus.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CreditCardStatus", Session.SessionID);
            SortByExpiryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ExpiryDate", Session.SessionID);
            SortByCreditLimit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CreditLimit", Session.SessionID);
            SortByCredit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Credit", Session.SessionID);
            SortByTotalPurchases.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TotalPurchases", Session.SessionID);
            SortByLastBillingDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("LastBillingDate", Session.SessionID);
		}
		private void LoadList()
		{	
			Contacts clsContact = new Contacts();
			DataClass clsDataClass = new DataClass();
            ContactColumns clsContactColumns = new ContactColumns();
            clsContactColumns.ContactID = true;
            clsContactColumns.ContactCode = true;
            clsContactColumns.ContactName = true;
            clsContactColumns.CreditDetails = true;

            ContactColumns clsSearchColumns = new ContactColumns();
            clsSearchColumns.ContactCode = true;
            clsSearchColumns.ContactName = true;

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

            DateTime dteExpiryDateFrom = DateTime.TryParse(txtExpiryDateFrom.Text, out dteExpiryDateFrom) ? dteExpiryDateFrom : DateTime.MinValue;
            DateTime dteExpiryDateTo = DateTime.TryParse(txtExpiryDateTo.Text, out dteExpiryDateTo) ? dteExpiryDateTo : DateTime.MinValue;
            Int16 intCreditCardStatus = (cboCreditCardStatus.SelectedItem.Value == Constants.ALL) ? Int16.Parse("-1") : Int16.Parse(cboCreditCardStatus.SelectedItem.Value);

            PageData.DataSource = clsContact.Guarantors(clsContactColumns, SortField, sortoption).DefaultView;

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
}
}
