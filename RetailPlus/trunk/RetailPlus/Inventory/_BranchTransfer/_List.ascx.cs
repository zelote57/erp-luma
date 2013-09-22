namespace AceSoft.RetailPlus.Inventory._BranchTransfer
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 
	using AceSoft.RetailPlus.Security;

	public partial  class __List : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();
	
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
                    cboStatus.Items.Clear();
                    cboStatus.Items.Add(new ListItem("Show " + BranchTransferStatus.Open.ToString("G").ToUpper() + " Transfers", BranchTransferStatus.Open.ToString("d")));
                    cboStatus.Items.Add(new ListItem("Show " + BranchTransferStatus.Posted.ToString("G").ToUpper() + " Transfers", BranchTransferStatus.Posted.ToString("d")));
                    cboStatus.Items.Add(new ListItem("Show " + BranchTransferStatus.Cancelled.ToString("G").ToUpper() + " Transfers", BranchTransferStatus.Cancelled.ToString("d"))); 
                    cboStatus.SelectedIndex = cboStatus.Items.IndexOf(cboStatus.Items.FindByValue(BranchTransferStatus.Open.ToString("d")));

                    try
                    {   lblStatus.Text = Request.QueryString["status"].ToString();
                        cboStatus.SelectedIndex = cboStatus.Items.IndexOf(cboStatus.Items.FindByValue(Request.QueryString["status"].ToString()));
                    }
                    catch { }

                    txtTransferStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    txtTransferEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

					ManageSecurity();
					LoadList();
					cmdDelete.Attributes.Add("onClick", "return confirm_cancel();");
					imgDelete.Attributes.Add("onClick", "return confirm_cancel();");
					cmdEdit.Attributes.Add("onClick", "return confirm_select();");
					imgEdit.Attributes.Add("onClick", "return confirm_select();");
					cmdGRN.Attributes.Add("onClick", "return confirm_select();");
					imgGRN.Attributes.Add("onClick", "return confirm_select();");

                    
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
		
		protected void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Delete();
		}
		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			Delete();
		}
		protected void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}
		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
		protected void imgGRN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			IssueGRN();
		}
		protected void cmdGRN_Click(object sender, System.EventArgs e)
		{
			IssueGRN();
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

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["BranchTransferID"].ToString();

                BranchTransferStatus status = (BranchTransferStatus)Enum.Parse(typeof(BranchTransferStatus), dr["Status"].ToString());
                if (status == BranchTransferStatus.Posted || status == BranchTransferStatus.Cancelled)
                {
                    chkList.Attributes.Add("disabled", "false");
                    ImageButton imgItemDelete = (ImageButton)e.Item.FindControl("imgItemDelete");
                    ImageButton imgItemEdit = (ImageButton)e.Item.FindControl("imgItemEdit"); 
                    ImageButton imgItemPost = (ImageButton)e.Item.FindControl("imgItemPost"); 
                    imgItemDelete.Enabled = false; imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemPost.Enabled = false; imgItemPost.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                }

                HyperLink lnkBranchTransferNo = (HyperLink)e.Item.FindControl("lnkBranchTransferNo");
                lnkBranchTransferNo.Text = dr["BranchTransferNo"].ToString();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(chkList.Value.ToString(), Session.SessionID);
                lnkBranchTransferNo.NavigateUrl = "Default.aspx" + stParam;

                Label lblBranchTransferDate = (Label)e.Item.FindControl("lblBranchTransferDate");
                lblBranchTransferDate.Text = Convert.ToDateTime(dr["BranchTransferDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                Label lblBranchIDFrom = (Label)e.Item.FindControl("lblBranchIDFrom");
                lblBranchIDFrom.Text = dr["BranchIDFrom"].ToString();

                HyperLink lnkBranchCodeFrom = (HyperLink)e.Item.FindControl("lnkBranchCodeFrom");
                lnkBranchCodeFrom.Text = dr["BranchCodeFrom"].ToString();
                stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblBranchIDFrom.Text, Session.SessionID);
                lnkBranchCodeFrom.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx" + stParam;

                Label lblBranchIDTo = (Label)e.Item.FindControl("lblBranchIDTo");
                lblBranchIDTo.Text = dr["BranchIDTo"].ToString();

                HyperLink lnkBranchCodeTo = (HyperLink)e.Item.FindControl("lnkBranchCodeTo");
                lnkBranchCodeTo.Text = dr["BranchCodeTo"].ToString();
                stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblBranchIDTo.Text, Session.SessionID);
                lnkBranchCodeTo.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx" + stParam;

                Label lblReqDeliveryDate = (Label)e.Item.FindControl("lblReqDeliveryDate");
                lblReqDeliveryDate.Text = Convert.ToDateTime(dr["RequiredDeliveryDate"].ToString()).ToString("yyyy-MM-dd");

                Label lblBranchTransferSubTotal = (Label)e.Item.FindControl("lblBranchTransferSubTotal");
                lblBranchTransferSubTotal.Text = Convert.ToDecimal(dr["SubTotal"].ToString()).ToString("#,##0.#0");

                Label lblBranchTransferRemarks = (Label)e.Item.FindControl("lblBranchTransferRemarks");
                lblBranchTransferRemarks.Text = dr["Remarks"].ToString();

                //For anchor
                HtmlGenericControl divExpCollAsst = (HtmlGenericControl)e.Item.FindControl("divExpCollAsst");

                HtmlAnchor anchorDown = (HtmlAnchor)e.Item.FindControl("anchorDown");
                anchorDown.HRef = "javascript:ToggleDiv('" + divExpCollAsst.ClientID + "')";
            }
        }
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemDelete":
                    stParam = "?task=" + Common.Encrypt("cancel", Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#cancelsection";
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#itemsection";	
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemPost":
                    stParam = "?task=" + Common.Encrypt("issuegrn", Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#postsection";	
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

		private void Delete()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
                    string stParam = "?task=" + Common.Encrypt("cancel", Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(stID, Session.SessionID) + "#cancelsection";	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot cancel more than one record. Please select at least one record to cancel.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		private void Update()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(stID,Session.SessionID) + "#itemsection";	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot put items more than one record. Please select at least one record to put items.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		private void IssueGRN()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("issuegrn",Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(stID,Session.SessionID) + "#postsection";	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot put items more than one record. Please select at least one record to put items.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.BranchTransfer); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			imgEdit.Visible = clsDetails.Write;
            imgGRN.Visible = clsDetails.Write;
            cmdGRN.Visible = clsDetails.Write;
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;
//			lblSeparator3.Visible = clsDetails.Write;

			clsAccessRights.CommitAndDispose();
		}
		private void LoadSortFieldOptions(DataListItemEventArgs e)
		{
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&status=" + cboStatus.SelectedIndex.ToString();

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
                sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

            if (sortoption == SortOption.Ascending)
                stParam += "&sortoption=" + Common.Encrypt(SortOption.Desscending.ToString("G"), Session.SessionID);
            else if (sortoption == SortOption.Desscending)
                stParam += "&sortoption=" + Common.Encrypt(SortOption.Ascending.ToString("G"), Session.SessionID);

            System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString; ;
            foreach (string querystring in querystrings.AllKeys)
            {
                if (querystring.ToLower() != "sortfield" && querystring.ToLower() != "sortoption" && querystring.ToLower() != "task" && querystring.ToLower() != "status")
                    stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
            }

			HyperLink SortByBranchTransferNo = (HyperLink) e.Item.FindControl("SortByBranchTransferNo");
			HyperLink SortByBranchTransferDate = (HyperLink) e.Item.FindControl("SortByBranchTransferDate");
            HyperLink SortByBranchCodeFrom = (HyperLink)e.Item.FindControl("SortByBranchCodeFrom");
            HyperLink SortByBranchCodeTo = (HyperLink)e.Item.FindControl("SortByBranchCodeTo");
			HyperLink SortByReqDeliveryDate = (HyperLink) e.Item.FindControl("SortByReqDeliveryDate");
			HyperLink SortByBranchTransferSubTotal = (HyperLink) e.Item.FindControl("SortByBranchTransferSubTotal");

			SortByBranchTransferNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchTransferNo", Session.SessionID);
			SortByBranchTransferDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchTransferDate", Session.SessionID);
            SortByBranchCodeFrom.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchIDFrom", Session.SessionID);
            SortByBranchCodeTo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchIDTo", Session.SessionID);
			SortByReqDeliveryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RequiredDeliveryDate", Session.SessionID);
			SortByBranchTransferSubTotal.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SubTotal", Session.SessionID);
		}
		private void LoadList()
		{	
			BranchTransfer clsBranchTransfer = new BranchTransfer();
			DataClass clsDataClass = new DataClass();
			Common Common = new Common();

			string SortField = "BranchTransferID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            DateTime dteTransferStartDate = DateTime.MinValue;
            try { if (txtTransferStartDate.Text != string.Empty) dteTransferStartDate = Convert.ToDateTime(txtTransferStartDate.Text + " " + txtTransferStartTime.Text); }
            catch { }

            DateTime dteTransferEndDate = DateTime.MinValue;
            try { if (txtTransferEndDate.Text != string.Empty) dteTransferEndDate = Convert.ToDateTime(txtTransferEndDate.Text + " " + txtTransferEndTime.Text); }
            catch { }

            DateTime dtePostingStartDate = DateTime.MinValue;
            try { if (txtPostingStartDate.Text != string.Empty) dtePostingStartDate = Convert.ToDateTime(txtPostingStartDate.Text + " " + txtPostingStartTime.Text); }
            catch { }

            DateTime dtePostingEndDate = DateTime.MinValue;
            try { if (txtPostingEndDate.Text != string.Empty) dtePostingEndDate = Convert.ToDateTime(txtPostingEndDate.Text + " " + txtPostingEndTime.Text); }
            catch { }

            string SearchKey = txtSearch.Text;
            BranchTransferStatus status = (BranchTransferStatus)Enum.Parse(typeof(BranchTransferStatus), cboStatus.SelectedItem.Value);
            PageData.DataSource = clsBranchTransfer.SearchAsDataTable(status, dteTransferStartDate, dteTransferEndDate, dtePostingStartDate, dtePostingEndDate, SearchKey, SortField, sortoption).DefaultView; 

			clsBranchTransfer.CommitAndDispose();

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
