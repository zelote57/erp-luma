using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessUser
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	/// <summary>
	///		Summary description for __List.
	/// </summary>
	public partial  class __List : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
					ManageSecurity();
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
        protected void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}
		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
        protected void imgAccessRightsUpdate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateAccessRights();
		}
		protected void cmdAccessRightsUpdate_Click(object sender, System.EventArgs e)
		{
			UpdateAccessRights();
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
                chkList.Value = dr["UID"].ToString();
                if (chkList.Value == "1")
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

                ImageButton imgResetPassword = (ImageButton)e.Item.FindControl("imgResetPassword");
                imgResetPassword.Attributes.Add("onClick", "return confirm_reset_password();");

                Label lblUserName = (Label)e.Item.FindControl("lblUserName");
                lblUserName.Text = dr["UserName"].ToString();

                Label lblPassword = (Label)e.Item.FindControl("lblPassword");
                Label lblPasswordReadable = (Label)e.Item.FindControl("lblPasswordReadable");
                lblPassword.Text = "*****";
                lblPasswordReadable.Text = dr["Password"].ToString();

				HyperLink lnkName = (HyperLink) e.Item.FindControl("lnkName");
				lnkName.Text = dr["Name"].ToString();
                lnkName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

				HyperLink lnkAddress1 = (HyperLink) e.Item.FindControl("lnkAddress1");
				lnkAddress1.Text = dr["Address1"].ToString();
				
				HyperLink lnkEmailAddress = (HyperLink) e.Item.FindControl("lnkEmailAddress");
				lnkEmailAddress.Text = dr["EmailAddress"].ToString();
                lnkEmailAddress.NavigateUrl = "mailto:" + dr["EmailAddress"].ToString();

				HyperLink lnkGroupName = (HyperLink) e.Item.FindControl("lnkGroupName");
				lnkGroupName.Text = dr["GroupName"].ToString();
                lnkGroupName.NavigateUrl = Constants.ROOT_DIRECTORY + "/AdminFiles/Security/_AccessGroup/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(dr["GroupID"].ToString(), Session.SessionID);

                ImageButton imgReloadAccessRights = (ImageButton)e.Item.FindControl("imgReloadAccessRights");
                imgReloadAccessRights.ToolTip = "Reload access rights from " + lnkGroupName.Text + " group to this user.";

				Label lblAddress2 = (Label) e.Item.FindControl("lblAddress2");
				lblAddress2.Text = dr["Address2"].ToString();

				Label lblCity = (Label) e.Item.FindControl("lblCity");
				lblCity.Text = dr["City"].ToString();

				Label lblState = (Label) e.Item.FindControl("lblState");
				lblState.Text = dr["State"].ToString();

				Label lblCountryName = (Label) e.Item.FindControl("lblCountryName");
				lblCountryName.Text = dr["CountryName"].ToString();

				Label lblOfficePhone = (Label) e.Item.FindControl("lblOfficePhone");
				lblOfficePhone.Text = dr["OfficePhone"].ToString();

				Label lblDirectPhone = (Label) e.Item.FindControl("lblDirectPhone");				
				lblDirectPhone.Text = dr["DirectPhone"].ToString();

				Label lblHomePhone = (Label) e.Item.FindControl("lblHomePhone");
				lblHomePhone.Text = dr["HomePhone"].ToString();
				
				Label lblFaxPhone = (Label) e.Item.FindControl("lblFaxPhone");
				lblFaxPhone.Text = dr["FaxPhone"].ToString();
				
				Label lblMobilePhone = (Label) e.Item.FindControl("lblMobilePhone");
				lblMobilePhone.Text = dr["MobilePhone"].ToString();
				    
				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
		{
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
			switch(e.CommandName)
			{
                case "imgItemDelete":
                    AccessUser clsAccessUser = new AccessUser();
                    clsAccessUser.Delete(chkList.Value);
                    clsAccessUser.CommitAndDispose();

                    LoadList();
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemAccessRights":
                    stParam = "?task=" + Common.Encrypt("accessrights", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgResetPassword":
                    ResetPassword(long.Parse(chkList.Value));
                    break;
                case "imgReloadAccessRights":
                    ReloadAccessRights(long.Parse(chkList.Value));
                    stParam = "?task=" + Common.Encrypt("list", Session.SessionID);
                    try { stParam += "&search=" + Common.Encrypt(Request.QueryString["search"].ToString(), Session.SessionID); }
                    catch { }
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgPrintBarCodeAccess":
                    Label lblPasswordReadable = (Label)e.Item.FindControl("lblPasswordReadable");
                    Label lblUserName = (Label)e.Item.FindControl("lblUserName");
                    HyperLink lnkName = (HyperLink)e.Item.FindControl("lnkName");

                    ThermalBarCodePrinter clsThermalBarCodePrinter = new ThermalBarCodePrinter();
                    try { clsThermalBarCodePrinter.PrintUserAccess(lnkName.Text, lblUserName.Text + "|" + lblPasswordReadable.Text); }
                    catch { }
                    break;
			}
		}
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}
		
		#endregion

		#region Private Modifiers

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessUsers); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			imgEdit.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;
			lblSeparator3.Visible = clsDetails.Write; 
			
			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessRights); 
			cmdAccessRightsUpdate.Visible=clsDetails.Write; 
			imgAccessRightsUpdate.Visible = clsDetails.Write;  

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

			HyperLink SortByUserName = (HyperLink) e.Item.FindControl("SortByUserName");
			HyperLink SortByPassword = (HyperLink) e.Item.FindControl("SortByPassword");
			HyperLink SortByName = (HyperLink) e.Item.FindControl("SortByName");
			HyperLink SortByAddress1 = (HyperLink) e.Item.FindControl("SortByAddress1");
			HyperLink SortByEmailAddress = (HyperLink) e.Item.FindControl("SortByEmailAddress");
			HyperLink SortByGroupName = (HyperLink) e.Item.FindControl("SortByGroupName");

			SortByUserName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("UserName", Session.SessionID);
			SortByPassword.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Password", Session.SessionID);
			SortByName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Name", Session.SessionID);
			SortByAddress1.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Address1", Session.SessionID);
			SortByEmailAddress.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("EmailAddress", Session.SessionID);
			SortByGroupName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("GroupName", Session.SessionID);
		}
		private void LoadList()
		{	
			AccessUser  clsAccessUser = new AccessUser();
			DataClass clsDataClass = new DataClass();

			string SortField = "a.UID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            string SearchKey = string.Empty;
			if (Request.QueryString["Search"] != null)
			{					
				SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);	
			}
            PageData.DataSource = clsAccessUser.ListAsDataTable(AccessGroupTypes.All, SearchKey, 0, 0, SortField, sortoption).DefaultView;
			clsAccessUser.CommitAndDispose();

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
				AccessUser clsAccessUser = new AccessUser();
				clsAccessUser.Delete(stIDs.Substring(0,stIDs.Length-1));
				clsAccessUser.CommitAndDispose();
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
		private void UpdateAccessRights()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					string stParam = "?task=" + Common.Encrypt("accessrights",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);
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

        private void ResetPassword(Int64 UserID)
        {

            string plainText = DateTime.Now.ToString("yyyyMMddhhmmss");    // original plaintext
            //string  cipherText = System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"].ToString();	// encrypted text
            string passPhrase = CompanyDetails.TIN; // can be any string
            string initVector = "%@skmelaT3rsh1t!"; // must be 16 bytes

            // Before encrypting data, we will append plain text to a random
            // salt value, which will be between 4 and 8 bytes long (implicitly
            // used defaults).
            AceSoft.Cryptor clsCryptor = new AceSoft.Cryptor(passPhrase, initVector);
            string strPassword = clsCryptor.Encrypt(plainText);
            strPassword = strPassword.Length > 8 ? strPassword.Substring(1, 8) : strPassword;

            AccessUser clsAccessUser = new AccessUser();
            clsAccessUser.UpdatePassword(UserID, strPassword);
            clsAccessUser.CommitAndDispose();

            string stScript = "<Script>";
            stScript += "window.alert('Please advise the user of the new password: " + strPassword + "')";
            stScript += "</Script>";
            Response.Write(stScript);	
        }

        private void ReloadAccessRights(long pvtUserID)
        {
            AccessUser clsAccessUser = new AccessUser();
            clsAccessUser.SynchronizeAccessRightsFromGroup(pvtUserID);
            clsAccessUser.CommitAndDispose();
        }

		#endregion
    }
}
