using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Contact
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 

	public partial  class __PriceLevel : System.Web.UI.UserControl
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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
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
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
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

                PriceLevel enumPriceLevel = (PriceLevel) Enum.Parse(typeof(PriceLevel), dr["PriceLevel"].ToString());
                RadioButton rdoPrice = (RadioButton)e.Item.FindControl("rdoPrice");
                RadioButton rdoWSPrice = (RadioButton)e.Item.FindControl("rdoWSPrice");
                RadioButton rdoLevel1 = (RadioButton)e.Item.FindControl("rdoLevel1");
                RadioButton rdoLevel2 = (RadioButton)e.Item.FindControl("rdoLevel2");
                RadioButton rdoLevel3 = (RadioButton)e.Item.FindControl("rdoLevel3");
                RadioButton rdoLevel4 = (RadioButton)e.Item.FindControl("rdoLevel4");
                RadioButton rdoLevel5 = (RadioButton)e.Item.FindControl("rdoLevel5");

                switch (enumPriceLevel)
                {
                    case PriceLevel.SRP: rdoPrice.Checked = true; break;
                    case PriceLevel.WSPrice: rdoWSPrice.Checked = true; break;
                    case PriceLevel.One: rdoLevel1.Checked = true; break;
                    case PriceLevel.Two: rdoLevel2.Checked = true; break;
                    case PriceLevel.Three: rdoLevel3.Checked = true; break;
                    case PriceLevel.Four: rdoLevel4.Checked = true; break;
                    case PriceLevel.Five: rdoLevel5.Checked = true; break;
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

        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
        }

        protected void rdoPriceLevelAll_CheckedChanged(Object sender, EventArgs e)
        {
            HtmlInputCheckBox chkList = null;
            RadioButton rdoPriceLevelAll = (RadioButton)sender;
            RadioButton rdoPrice = null;
            Int64 iContactID = 0;

            Contacts clsContacts = new Contacts();
            PriceLevel enumPriceLevel = PriceLevel.SRP;

            foreach (DataListItem item in lstItem.Items)
            {
                chkList = (HtmlInputCheckBox)item.FindControl("chkList");

                iContactID = Int64.Parse(chkList.Value);

                switch (rdoPriceLevelAll.ID)
                {
                    case "rdoPriceAll": enumPriceLevel = PriceLevel.SRP; rdoPrice = (RadioButton)item.FindControl("rdoPrice"); break;
                    case "rdoWSPriceAll": enumPriceLevel = PriceLevel.WSPrice; rdoPrice = (RadioButton)item.FindControl("rdoWSPrice"); break;
                    case "rdoLevel1All": enumPriceLevel = PriceLevel.One; rdoPrice = (RadioButton)item.FindControl("rdoLevel1"); break;
                    case "rdoLevel2All": enumPriceLevel = PriceLevel.Two; rdoPrice = (RadioButton)item.FindControl("rdoLevel2"); break;
                    case "rdoLevel3All": enumPriceLevel = PriceLevel.Three; rdoPrice = (RadioButton)item.FindControl("rdoLevel3"); break;
                    case "rdoLevel4All": enumPriceLevel = PriceLevel.Four; rdoPrice = (RadioButton)item.FindControl("rdoLevel4"); break;
                    case "rdoLevel5All": enumPriceLevel = PriceLevel.Five; rdoPrice = (RadioButton)item.FindControl("rdoLevel5"); break;
                }
                clsContacts.UpdatePriceLevel(iContactID, enumPriceLevel);
                rdoPrice.Checked = rdoPriceLevelAll.Checked;
            }
            clsContacts.CommitAndDispose();
        }

        protected void grpPriceLevel_OnCheckedChanged(Object sender, EventArgs e)
        {
            RadioButton rdoPrice = (RadioButton)sender;
            DataListItem item = (DataListItem)rdoPrice.NamingContainer;

            HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
            Int64 iContactID = Int64.Parse(chkList.Value);

            PriceLevel enumPriceLevel = PriceLevel.SRP;
            switch (rdoPrice.ID)
            {
                case "rdoPrice": enumPriceLevel = PriceLevel.SRP; break;
                case "rdoWSPrice": enumPriceLevel = PriceLevel.WSPrice; break;
                case "rdoLevel1": enumPriceLevel = PriceLevel.One; break;
                case "rdoLevel2": enumPriceLevel = PriceLevel.Two; break;
                case "rdoLevel3": enumPriceLevel = PriceLevel.Three; break;
                case "rdoLevel4": enumPriceLevel = PriceLevel.Four; break;
                case "rdoLevel5": enumPriceLevel = PriceLevel.Five; break;
            }

            Contacts clsContacts = new Contacts();
            clsContacts.UpdatePriceLevel(iContactID, enumPriceLevel);
            clsContacts.CommitAndDispose();
        }


		#endregion

		#region Private Methods

        private void LoadOptions()
        {
            ContactGroups clsContactGroup = new ContactGroups();

            cboGroup.DataTextField = "ContactGroupName";
            cboGroup.DataValueField = "ContactGroupID";
            cboGroup.DataSource = clsContactGroup.ListAsDataTable(ContactGroupCategory.CUSTOMER).DefaultView;
            cboGroup.DataBind();
            cboGroup.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "0"));
            cboGroup.SelectedIndex = 0;

            clsContactGroup.CommitAndDispose();

            cboModeOfTerms.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, "-1"));
            cboModeOfTerms.SelectedIndex = 0;
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
            HyperLink SortByPrice = (HyperLink)e.Item.FindControl("SortByPrice");
            HyperLink SortByWSPrice = (HyperLink)e.Item.FindControl("SortByWSPrice");
            HyperLink SortByPriceLevel1 = (HyperLink)e.Item.FindControl("SortByPriceLevel1");
            HyperLink SortByPriceLevel2 = (HyperLink)e.Item.FindControl("SortByPriceLevel2");
            HyperLink SortByPriceLevel3 = (HyperLink)e.Item.FindControl("SortByPriceLevel3");
            HyperLink SortByPriceLevel4 = (HyperLink)e.Item.FindControl("SortByPriceLevel4");
            HyperLink SortByPriceLevel5 = (HyperLink)e.Item.FindControl("SortByPriceLevel5");

			SortByContactCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactCode", Session.SessionID);
			SortByContactName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ContactName", Session.SessionID);
            SortByPrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Price", Session.SessionID);
            SortByWSPrice.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("WSPrice", Session.SessionID);
            SortByPriceLevel1.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PriceLevel", Session.SessionID);
            SortByPriceLevel2.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PriceLevel", Session.SessionID);
            SortByPriceLevel3.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PriceLevel", Session.SessionID);
            SortByPriceLevel4.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PriceLevel", Session.SessionID);
            SortByPriceLevel5.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PriceLevel", Session.SessionID);
		}

		private void LoadList()
		{	
			Contacts clsContact = new Contacts();
            
			string SortField = "ContactCode";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            string strContactCode = txtContactCode.Text;
            string strContactName = txtContactName.Text;
            string strContactGroupCode = cboGroup.SelectedItem.Text;
            Int16 intModeOfTerms = Int16.Parse(cboModeOfTerms.SelectedItem.Value);

            PageData.DataSource = clsContact.ListAsDataTable(ContactGroupCategory.CUSTOMER, ContactCode: strContactCode, ContactName: strContactName, ContactGroupCode: strContactGroupCode, SortField: SortField, SortOrder: sortoption, ModeOfTerms: intModeOfTerms).DefaultView;

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
