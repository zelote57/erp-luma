using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Inventory._Stock
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
//			ManageSecurity();

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
        protected void imgTransfer_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Transfer();
		}
		protected void cmdTransfer_Click(object sender, System.EventArgs e)
		{
			Transfer();
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
                chkList.Value = dr["StockID"].ToString();

                imgItemDelete.Enabled = cmdDelete.Visible; if (!imgItemDelete.Enabled) imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                imgItemEdit.Enabled = cmdEdit.Visible; if (!imgItemEdit.Enabled) imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                if (imgItemDelete.Enabled) imgItemDelete.Attributes.Add("onClick", "return confirm_item_delete();");

                ImageButton imgItemTransfer = (ImageButton)e.Item.FindControl("imgItemTransfer");
                if (dr["StockTypeID"].ToString() != Constants.STOCK_TYPE_TRANSFER_TO_BRANCH_ID)
                { imgItemTransfer.Enabled = false; imgItemTransfer.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif"; imgItemTransfer.ToolTip = ""; }
                else
                { imgItemTransfer.Enabled = cmdTransfer.Visible; if (!imgItemTransfer.Enabled) { imgItemTransfer.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif"; imgItemTransfer.ToolTip = ""; } }

                HyperLink lnkTransactionNo = (HyperLink)e.Item.FindControl("lnkTransactionNo");
                lnkTransactionNo.Text = dr["TransactionNo"].ToString();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&stockid=" + Common.Encrypt(chkList.Value.ToString(), Session.SessionID);
                lnkTransactionNo.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Stock/Default.aspx" + stParam;

                Label lblStockTypeCode = (Label)e.Item.FindControl("lblStockTypeCode");
                lblStockTypeCode.Text = dr["StockTypeDescription"].ToString();

                Label lblStockTypeID = (Label)e.Item.FindControl("lblStockTypeID");
                lblStockTypeID.Text = dr["StockTypeID"].ToString();

                Label lblStockDirection = (Label)e.Item.FindControl("lblStockDirection");
                StockDirections stockdirection = (StockDirections)Enum.Parse(typeof(StockDirections), Convert.ToInt16(dr["StockDirection"]).ToString());
                lblStockDirection.Text = stockdirection.ToString("G");

                Label lblStockDate = (Label)e.Item.FindControl("lblStockDate");
                lblStockDate.Text = Convert.ToDateTime(dr["StockDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");

                Label lblRemarks = (Label)e.Item.FindControl("lblRemarks");
                lblRemarks.Text = dr["Remarks"].ToString();

                ImageButton imgTransactionTag = (ImageButton)e.Item.FindControl("imgTransactionTag");
                if (Convert.ToBoolean(dr["Active"]))
                {
                    imgTransactionTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/prodtagact.gif";
                    imgTransactionTag.ToolTip = "Close this transaction to prevent user for adding new items.";
                    
                }
                else //if (clsProductListFilterType == ProductListFilterType.ShowInactiveOnly)
                {
                    imgTransactionTag.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgTransactionTag.ToolTip = "Transaction is already close.";
                    imgTransactionTag.Enabled = false;
                    chkList.Attributes.Add("disabled", "false");

                    imgItemDelete.Enabled = false; imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                    imgItemEdit.Enabled = false; imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
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
                    Stock clsStock = new Stock();
                    clsStock.Delete(chkList.Value);
                    clsStock.CommitAndDispose();

                    LoadList();
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("additem", Session.SessionID) + "&stockid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgItemTransfer":
                    stParam = "?task=" + Common.Encrypt("transfer", Session.SessionID) + "&stockid=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
                case "imgTransactionTag":
                    {
                        Stock clsStock1 = new Stock();
                        clsStock1.TagInactive(long.Parse(chkList.Value));
                        clsStock1.CommitAndDispose();
                        LoadList();
                    }
                    break;
            }
        }
        protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LoadList();
        }
        protected void rdoShowAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void rdoShowActiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void rdoShowInactiveOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadList();
        }

		#endregion

		#region Private Methods

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
				Stock clsStock = new Stock();
				clsStock.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsStock.CommitAndDispose();
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
					string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&stockid=" + Common.Encrypt(stID,Session.SessionID);	
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
		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.StockTransactions); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			imgEdit.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.BranchTransfer); 
			lblSeparator3.Visible = clsDetails.Read;
			cmdTransfer.Visible  = clsDetails.Read;

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

			HyperLink SortByTransactionNo = (HyperLink) e.Item.FindControl("SortByTransactionNo");
			HyperLink SortByStockType = (HyperLink) e.Item.FindControl("SortByStockType");
			HyperLink SortByStockDirection = (HyperLink) e.Item.FindControl("SortByStockDirection");
			HyperLink SortByStockDate = (HyperLink) e.Item.FindControl("SortByStockDate");
			HyperLink SortByRemarks = (HyperLink) e.Item.FindControl("SortByRemarks");

			SortByTransactionNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TransactionNo", Session.SessionID);
			SortByStockType.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Description", Session.SessionID);
			SortByStockDirection.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("StockDirection", Session.SessionID);
			SortByStockDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("StockDate", Session.SessionID);
			SortByRemarks.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("a.Remarks", Session.SessionID);
		}
		private void LoadList()
		{	
			Stock clsStock = new Stock();
			DataClass clsDataClass = new DataClass();

			string SortField = "StockID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

			SortOption sortoption = SortOption.Desscending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            TransactionListFilterType clsTransactionListFilterType = TransactionListFilterType.ShowActiveAndInactive;
            if (rdoShowActiveOnly.Checked == true) clsTransactionListFilterType = TransactionListFilterType.ShowActiveOnly;
            if (rdoShowInactiveOnly.Checked == true) clsTransactionListFilterType = TransactionListFilterType.ShowInactiveOnly;

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsStock.ListAsDataTableActiveInactive(clsTransactionListFilterType, SortField, sortoption).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsStock.SearchDataTableActiveInactive(clsTransactionListFilterType, SearchKey, SortField, sortoption).DefaultView;
			}

			clsStock.CommitAndDispose();

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
		private void Transfer()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Int64 iID = Convert.ToInt64(stID);
					Stock clsStock = new Stock();
					StockDetails clsDetails = clsStock.Details(iID);
					clsStock.CommitAndDispose();

					if (clsDetails.StockTypeID.ToString() == Constants.STOCK_TYPE_TRANSFER_TO_BRANCH_ID)
					{
						string stParam = "?task=" + Common.Encrypt("transfer",Session.SessionID) + "&stockid=" + Common.Encrypt(stID,Session.SessionID);	
						Response.Redirect("Default.aspx" + stParam);
					}
					else
					{
						string stScript = "<Script>";
						stScript += "window.alert('Sorry you cannot transfer this transaction. Please select a TRANSFER TO BRANCH transaction.')";
						stScript += "</Script>";
						Response.Write(stScript);	
					}
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot transfer more than one record. Please select at least one record to transfer.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}

		#endregion
        
    }
}
