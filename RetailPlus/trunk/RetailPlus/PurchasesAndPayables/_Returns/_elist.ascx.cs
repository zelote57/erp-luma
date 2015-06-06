using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.PurchasesAndPayables._Returns
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 

	public partial  class __eList : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();
	

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
                cboStatus.Items.Clear();
                cboStatus.Items.Add(new ListItem("Show All", "0"));
                cboStatus.Items.Add(new ListItem("Show eSales Only", "1"));
                cboStatus.Items.Add(new ListItem("Show Not included in eSales", "2"));
                cboStatus.SelectedIndex = 0;

                txtOrderStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtOrderEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

				ManageSecurity();
				LoadList();
			}
		}


		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{

		}
		#endregion

		#region Web Control Methods

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        protected void cmdCancel_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
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
                chkList.Value = dr["DebitMemoID"].ToString();

                POReturnStatus status = (POReturnStatus)Enum.Parse(typeof(POReturnStatus), dr["POReturnStatus"].ToString());
                if (status == POReturnStatus.Posted || status == POReturnStatus.Cancelled)
                {
                    chkList.Attributes.Add("disabled", "false");
                }

                HyperLink lnkReturnNo = (HyperLink)e.Item.FindControl("lnkReturnNo");
                lnkReturnNo.Text = dr["MemoNo"].ToString();
                Common Common = new Common();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value.ToString(), Session.SessionID);
                lnkReturnNo.NavigateUrl = "Default.aspx" + stParam;

                Label lblReturnDate = (Label)e.Item.FindControl("lblReturnDate");
                lblReturnDate.Text = Convert.ToDateTime(dr["MemoDate"]).ToString("yyyy-MM-dd HH:mm:ss");

                Label lblSupplierID = (Label)e.Item.FindControl("lblSupplierID");
                lblSupplierID.Text = dr["SupplierID"].ToString();

                HyperLink lblSupplierCode = (HyperLink)e.Item.FindControl("lblSupplierCode");
                lblSupplierCode.Text = dr["SupplierCode"].ToString();
                stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblSupplierID.Text, Session.SessionID);
                lblSupplierCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx" + stParam;

                Label lblReqReturnDate = (Label)e.Item.FindControl("lblReqReturnDate");
                lblReqReturnDate.Text = Convert.ToDateTime(dr["RequiredPostingDate"]).ToString("yyyy-MM-dd");

                Label lblBranchID = (Label)e.Item.FindControl("lblBranchID");
                lblBranchID.Text = dr["BranchID"].ToString();
                Label lblBranchCode = (Label)e.Item.FindControl("lblBranchCode");
                lblBranchCode.Text = dr["BranchCode"].ToString();

                Label lblSubTotal = (Label)e.Item.FindControl("lblSubTotal");
                lblSubTotal.Text = Convert.ToDecimal(dr["SubTotal"]).ToString("#,##0.#0");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                CheckBox chkIncludeIneSales = (CheckBox)e.Item.FindControl("chkIncludeIneSales");
                chkIncludeIneSales.Checked = bool.Parse(dr["IncludeIneSales"].ToString());

                //For anchor
                //				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

                //				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
            }
        }
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            //HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            //string stParam = string.Empty;
            //switch (e.CommandName)
            //{
            //    case "imgItemDelete":
            //        stParam = "?task=" + Common.Encrypt("cancel", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#cancelsection";
            //        Response.Redirect("Default.aspx" + stParam);
            //        break;
            //    case "imgItemEdit":
            //        stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#itemsection";	
            //        Response.Redirect("Default.aspx" + stParam);
            //        break;
            //    case "imgItemPost":
            //        stParam = stParam = "?task=" + Common.Encrypt("post", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#postsection";	
            //        Response.Redirect("Default.aspx" + stParam);
            //        break;

            //}
        }
        protected void cmdSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadList();
        }

        protected void chkIncludeIneSalesAll_CheckedChanged(Object sender, EventArgs e)
        {
            HtmlInputCheckBox chkList = null;
            CheckBox chkIncludeIneSalesAll = (CheckBox)sender;
            CheckBox chkIncludeIneSales = null;
            Int64 iDebitMemoID = 0;

            POReturns clsPOReturns = new POReturns();
            foreach (DataListItem item in lstItem.Items)
            {
                chkList = (HtmlInputCheckBox)item.FindControl("chkList");

                iDebitMemoID = Int64.Parse(chkList.Value);

                chkIncludeIneSales = (CheckBox)item.FindControl("chkIncludeIneSales");

                clsPOReturns.UpdateIncludeIneSales(iDebitMemoID, chkIncludeIneSalesAll.Checked);
                chkIncludeIneSales.Checked = chkIncludeIneSalesAll.Checked;
            }
            clsPOReturns.CommitAndDispose();
        }

        protected void chkIncludeIneSales_CheckedChanged(Object sender, EventArgs e)
        {
            CheckBox chkIncludeIneSales = (CheckBox)sender;
            DataListItem item = (DataListItem)chkIncludeIneSales.NamingContainer;

            HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
            Int64 iDebitMemoID = Int64.Parse(chkList.Value);

            POReturns clsPOReturns = new POReturns();
            clsPOReturns.UpdateIncludeIneSales(iDebitMemoID, chkIncludeIneSales.Checked);
            clsPOReturns.CommitAndDispose();
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
                    string stParam = "?task=" + Common.Encrypt("cancel", Session.SessionID) + "&retid=" + Common.Encrypt(stID, Session.SessionID) + "#cancelsection";
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
					string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&retid=" + Common.Encrypt(stID,Session.SessionID) + "#itemsection";	
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
					string stParam = "?task=" + Common.Encrypt("post",Session.SessionID) + "&retid=" + Common.Encrypt(stID,Session.SessionID) + "#postsection";	
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
			
		}
		private void LoadSortFieldOptions(DataListItemEventArgs e)
		{
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID) + "&status=" + POReturnStatus.Posted.ToString("d");

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

			HyperLink SortByReturnNo = (HyperLink) e.Item.FindControl("SortByReturnNo");
			HyperLink SortByReturnDate = (HyperLink) e.Item.FindControl("SortByReturnDate");
			HyperLink SortBySupplierCode = (HyperLink) e.Item.FindControl("SortBySupplierCode");
			HyperLink SortByReqReturnDate = (HyperLink) e.Item.FindControl("SortByReqReturnDate");
			HyperLink SortByBranchCode = (HyperLink) e.Item.FindControl("SortByBranchCode");
			HyperLink SortBySubTotal = (HyperLink) e.Item.FindControl("SortBySubTotal");
			HyperLink SortByRemarks = (HyperLink) e.Item.FindControl("SortByRemarks");

			SortByReturnNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("MemoNo", Session.SessionID);
			SortByReturnDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("MemoDate", Session.SessionID);
			SortBySupplierCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SupplierCode", Session.SessionID);
			SortByReqReturnDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RequiredPostingDate", Session.SessionID);
			SortByBranchCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchID", Session.SessionID);
			SortBySubTotal.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SubTotal", Session.SessionID);
			SortByRemarks.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Remarks", Session.SessionID);
		}
		private void LoadList()
		{	
			POReturns clsPOReturns = new POReturns();
			DataClass clsDataClass = new DataClass();
			Common Common = new Common();

			string SortField = "DebitMemoID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            DateTime dteOrderStartDate = DateTime.MinValue;
            try { if (txtOrderStartDate.Text != string.Empty) dteOrderStartDate = Convert.ToDateTime(txtOrderStartDate.Text + " " + txtOrderStartTime.Text); }
            catch { }

            DateTime dteOrderEndDate = DateTime.MinValue;
            try { if (txtOrderEndDate.Text != string.Empty) dteOrderEndDate = Convert.ToDateTime(txtOrderEndDate.Text + " " + txtOrderEndTime.Text); }
            catch { }

            DateTime dtePostingStartDate = DateTime.MinValue;
            try { if (txtPostingStartDate.Text != string.Empty) dtePostingStartDate = Convert.ToDateTime(txtPostingStartDate.Text + " " + txtPostingStartTime.Text); }
            catch { }

            DateTime dtePostingEndDate = DateTime.MinValue;
            try { if (txtPostingEndDate.Text != string.Empty) dtePostingEndDate = Convert.ToDateTime(txtPostingEndDate.Text + " " + txtPostingEndTime.Text); }
            catch { }

            eSalesFilter clseSalesFilter = new eSalesFilter();
            switch (cboStatus.SelectedIndex)
            {
                case 0:
                    clseSalesFilter.FilterIncludeIneSales = false;
                    break;
                case 1:
                    clseSalesFilter.FilterIncludeIneSales = true;
                    clseSalesFilter.IncludeIneSales = true;
                    break;
                case 2:
                    clseSalesFilter.FilterIncludeIneSales = true;
                    clseSalesFilter.IncludeIneSales = false;
                    break;
            }

            string SearchKey = txtSearch.Text;
            PageData.DataSource = clsPOReturns.SearchAsDataTable(POReturnStatus.Posted, dteOrderStartDate, dteOrderEndDate, dtePostingStartDate, dtePostingEndDate, SearchKey, SortField, sortoption, 0, clseSalesFilter).DefaultView; 

			clsPOReturns.CommitAndDispose();

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
            foreach (DataListItem item in lstItem.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
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
