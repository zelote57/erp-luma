using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.SalesAndReceivables._Returns
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
                LoadList();
                cmdDelete.Attributes.Add("onClick", "return confirm_cancel();");
                imgDelete.Attributes.Add("onClick", "return confirm_cancel();");
                cmdEdit.Attributes.Add("onClick", "return confirm_select();");
                imgEdit.Attributes.Add("onClick", "return confirm_select();");
                cmdPost.Attributes.Add("onClick", "return confirm_select();");
                imgPost.Attributes.Add("onClick", "return confirm_select();");
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

        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
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
        protected void imgPost_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            IssueGRN();
        }
        protected void cmdPost_Click(object sender, System.EventArgs e)
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
                chkList.Value = dr["CreditMemoID"].ToString();

                HyperLink lnkReturnNo = (HyperLink)e.Item.FindControl("lnkReturnNo");
                lnkReturnNo.Text = dr["CNNo"].ToString();
                Common Common = new Common();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value.ToString(), Session.SessionID);
                lnkReturnNo.NavigateUrl = "Default.aspx" + stParam;

                Label lblReturnDate = (Label)e.Item.FindControl("lblReturnDate");
                lblReturnDate.Text = Convert.ToDateTime(dr["CNDate"]).ToString("yyyy-MM-dd HH:mm:ss");

                Label lblCustomerID = (Label)e.Item.FindControl("lblCustomerID");
                lblCustomerID.Text = dr["CustomerID"].ToString();

                HyperLink lblCustomerCode = (HyperLink)e.Item.FindControl("lblCustomerCode");
                lblCustomerCode.Text = dr["CustomerCode"].ToString();
                stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblCustomerID.Text, Session.SessionID);
                lblCustomerCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalesAndReceivables/_Customer/Default.aspx" + stParam;

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

                //For anchor
                //				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

                //				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
            }
        }
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemDelete":
                    stParam = "?task=" + Common.Encrypt("cancel", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#cancelsection";
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#itemsection";
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemPost":
                    stParam = stParam = "?task=" + Common.Encrypt("post", Session.SessionID) + "&retid=" + Common.Encrypt(chkList.Value, Session.SessionID) + "#postsection";
                    Response.Redirect("Default.aspx" + stParam);
                    break;

            }
        }

        #endregion

        #region Private Methods

        private void Delete()
        {
            if (isChkListSingle() == true)
            {
                string stID = GetFirstID();
                if (stID != null)
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
                if (stID != null)
                {
                    Common Common = new Common();
                    string stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&retid=" + Common.Encrypt(stID, Session.SessionID) + "#itemsection";
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
                if (stID != null)
                {
                    Common Common = new Common();
                    string stParam = "?task=" + Common.Encrypt("post", Session.SessionID) + "&retid=" + Common.Encrypt(stID, Session.SessionID) + "#postsection";
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

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.SalesReturns);
            imgAdd.Visible = clsDetails.Write;
            cmdAdd.Visible = clsDetails.Write;
            imgDelete.Visible = clsDetails.Write;
            cmdDelete.Visible = clsDetails.Write;
            cmdEdit.Visible = clsDetails.Write;
            imgEdit.Visible = clsDetails.Write;
            lblSeparator1.Visible = clsDetails.Write;
            lblSeparator2.Visible = clsDetails.Write;
            //			lblSeparator3.Visible = clsDetails.Write;

            clsAccessRights.CommitAndDispose();
        }
        private void LoadSortFieldOptions(DataListItemEventArgs e)
        {
            Common Common = new Common();
            string stParam = "?task=" + Common.Encrypt("list", Session.SessionID); // +"&status=" + cboStatus.SelectedIndex.ToString();

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

            HyperLink SortByReturnNo = (HyperLink)e.Item.FindControl("SortByReturnNo");
            HyperLink SortByReturnDate = (HyperLink)e.Item.FindControl("SortByReturnDate");
            HyperLink SortByCustomerCode = (HyperLink)e.Item.FindControl("SortByCustomerCode");
            HyperLink SortByReqReturnDate = (HyperLink)e.Item.FindControl("SortByReqReturnDate");
            HyperLink SortByBranchCode = (HyperLink)e.Item.FindControl("SortByBranchCode");
            HyperLink SortBySubTotal = (HyperLink)e.Item.FindControl("SortBySubTotal");
            HyperLink SortByRemarks = (HyperLink)e.Item.FindControl("SortByRemarks");

            SortByReturnNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CNNo", Session.SessionID);
            SortByReturnDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CNDate", Session.SessionID);
            SortByCustomerCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CustomerCode", Session.SessionID);
            SortByReqReturnDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RequiredPostingDate", Session.SessionID);
            SortByBranchCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchID", Session.SessionID);
            SortBySubTotal.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SubTotal", Session.SessionID);
            SortByRemarks.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Remarks", Session.SessionID);
        }
        private void LoadList()
        {
            SOReturns clsSOReturns = new SOReturns();
            DataClass clsDataClass = new DataClass();
            Common Common = new Common();

            string SortField = "CreditMemoID";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            if (Request.QueryString["Search"] == null)
            {
                PageData.DataSource = clsSOReturns.ListAsDataTable(SOReturnStatus.Open, SortField, sortoption).DefaultView;
            }
            else
            {
                string SearchKey = Common.Decrypt((string)Request.QueryString["search"], Session.SessionID);
                PageData.DataSource = clsSOReturns.SearchAsDataTable(SOReturnStatus.Open, SearchKey, SortField, sortoption).DefaultView;
            }

            clsSOReturns.CommitAndDispose();

            int iPageSize = Convert.ToInt16(Session["PageSize"]);

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
            for (int i = 0; i < PageData.PageCount; i++)
            {
                int iValue = i + 1;
                cboCurrentPage.Items.Add(new ListItem(iValue.ToString(), iValue.ToString()));
                if (PageData.CurrentPageIndex == i)
                { cboCurrentPage.Items[i].Selected = true; }
                else
                { cboCurrentPage.Items[i].Selected = false; }
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

            foreach (DataListItem item in lstItem.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
                {
                    if (chkList.Checked == true)
                    {
                        iCount += 1;
                        if (iCount >= 2)
                        { return false; }
                    }
                }
            }
            return boChkListSingle;
        }


        #endregion

    }
}
