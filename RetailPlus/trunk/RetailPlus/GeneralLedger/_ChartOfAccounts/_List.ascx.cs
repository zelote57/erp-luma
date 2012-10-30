using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.GeneralLedger._ChartOfAccounts
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data; 

	/// <summary>
	///		Summary description for __List.
	/// </summary>
	public partial  class __List : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

			if (!IsPostBack)
				if (Visible)
				{
					LoadAccountSummaryList();
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
			this.lstAccountSummary.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstAccountSummary_ItemDataBound);
		}
		#endregion  

		#region Web Control Methods

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}
		
		private void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Delete())
				LoadAccountSummaryList();
		}
		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadAccountSummaryList();
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
			LoadAccountSummaryList();
		}

		private void lstAccountSummary_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

                Label lblAccountSummaryCode = (Label) e.Item.FindControl("lblAccountSummaryCode");
				lblAccountSummaryCode.Text = dr["AccountSummaryCode"].ToString();

				Label lblAccountSummaryName = (Label) e.Item.FindControl("lblAccountSummaryName");
				lblAccountSummaryName.Text = dr["AccountSummaryName"].ToString();

                DataList lstAccountCategory = (DataList) e.Item.FindControl("lstAccountCategory");
                lstAccountCategory.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstAccountCategory_ItemDataBound);
                int AccountSummaryID = Convert.ToInt16(dr["AccountSummaryID"].ToString());
                LoadAccountCategoryList(lstAccountCategory, AccountSummaryID);
			}
		}
        private void lstAccountCategory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                Label lblAccountCategoryCode = (Label)e.Item.FindControl("lblAccountCategoryCode");
                lblAccountCategoryCode.Text = dr["AccountCategoryCode"].ToString();

                Label lblAccountCategoryName = (Label)e.Item.FindControl("lblAccountCategoryName");
                lblAccountCategoryName.Text = dr["AccountCategoryName"].ToString();

                DataList lstChartOfAccounts = (DataList)e.Item.FindControl("lstChartOfAccounts");
                lstChartOfAccounts.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstChartOfAccounts_ItemDataBound);
                int AccountCategoryID = Convert.ToInt16(dr["AccountCategoryID"].ToString());
                LoadChartOfAccountList(lstChartOfAccounts, AccountCategoryID);
            }
        }
        private void lstChartOfAccounts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["ChartOfAccountID"].ToString();

                Label lblAccountCode = (Label) e.Item.FindControl("lblAccountCode");
                lblAccountCode.Text = dr["ChartOfAccountCode"].ToString();

                Label lblAccountName = (Label) e.Item.FindControl("lblAccountName");
                lblAccountName.Text = dr["ChartOfAccountName"].ToString();

                Label lblDebit = (Label) e.Item.FindControl("lblDebit");
                lblDebit.Text = Convert.ToDecimal(dr["Debit"].ToString()).ToString("#,##0.#0");

                Label lblCredit = (Label) e.Item.FindControl("lblCredit");
                lblCredit.Text = Convert.ToDecimal(dr["Credit"].ToString()).ToString("#,##0.#0");

                //For anchor
                //HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

                //HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";

            }
        }				

		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ChartOfAccounts); 
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
            //string stParam = null;		

            //SortOption sortoption = SortOption.Ascending;
            //if (Request.QueryString["sortoption"]!=null)
            //    sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);

            //if (sortoption == SortOption.Ascending)
            //    stParam += "?sortoption=" + Common.Encrypt(SortOption.Desscending.ToString("G"), Session.SessionID);
            //else if (sortoption == SortOption.Desscending)
            //    stParam += "?sortoption=" + Common.Encrypt(SortOption.Ascending.ToString("G"), Session.SessionID);

            //System.Collections.Specialized.NameValueCollection querystrings = Request.QueryString;;
            //foreach(string querystring in querystrings.AllKeys)
            //{
            //    if (querystring.ToLower() != "sortfield" && querystring.ToLower() != "sortoption") 
            //        stParam += "&" + querystring + "=" + querystrings[querystring].ToString();
            //}

            //HyperLink SortByAccountSummaryName = (HyperLink) e.Item.FindControl("SortByAccountSummaryName");
            //HyperLink SortByAccountCategoryName = (HyperLink) e.Item.FindControl("SortByAccountCategoryName");
            //HyperLink SortByAccountCode = (HyperLink) e.Item.FindControl("SortByAccountCode");
            //HyperLink SortByDebit = (HyperLink) e.Item.FindControl("SortByDebit");
            //HyperLink SortByCredit = (HyperLink) e.Item.FindControl("SortByCredit");

            //SortByAccountSummaryName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("AccountSummaryName", Session.SessionID);
            //SortByAccountCategoryName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("AccountCategoryName", Session.SessionID);
            //SortByAccountCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("ChartOfAccountCode", Session.SessionID);
            //SortByDebit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Debit", Session.SessionID);
            //SortByCredit.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Credit", Session.SessionID);
		}

		private void LoadAccountSummaryList()
		{
            AccountSummary clsAccountSummary = new AccountSummary();
			DataClass clsDataClass = new DataClass();

            string SortField = "AccountSummaryCode";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
                PageData.DataSource = clsDataClass.DataReaderToDataTable(clsAccountSummary.List(SortField, sortoption)).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
                PageData.DataSource = clsDataClass.DataReaderToDataTable(clsAccountSummary.Search(SearchKey, SortField, sortoption)).DefaultView;
			}

            clsAccountSummary.CommitAndDispose();

			int iPageSize = Convert.ToInt16(Session["PageSize"]) ;
			
			PageData.AllowPaging = true;
			PageData.PageSize = iPageSize;
			try
			{
				PageData.CurrentPageIndex = Convert.ToInt16(cboCurrentPage.SelectedItem.Value) - 1;				
				lstAccountSummary.DataSource = PageData;
				lstAccountSummary.DataBind();
			}
			catch
			{
				PageData.CurrentPageIndex = 1;
				lstAccountSummary.DataSource = PageData;
				lstAccountSummary.DataBind();
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
        private void LoadAccountCategoryList(DataList lstAccountCategory, int AccountSummaryID)
        {
            AccountCategory clsAccountCategory = new AccountCategory();
            DataClass clsDataClass = new DataClass();
            System.Data.DataTable dt = clsDataClass.DataReaderToDataTable(clsAccountCategory.List(AccountSummaryID, "AccountCategoryCode", SortOption.Ascending));
            clsAccountCategory.CommitAndDispose();
            lstAccountCategory.DataSource = dt.DefaultView;
            lstAccountCategory.DataBind();
        }
        private void LoadChartOfAccountList(DataList lstChartOfAccount, int AccountCategoryID)
        {
            ChartOfAccount clsChartOfAccount = new ChartOfAccount();
            DataClass clsDataClass = new DataClass();
            System.Data.DataTable dt = clsDataClass.DataReaderToDataTable(clsChartOfAccount.List(AccountCategoryID, "ChartOfAccountCode", SortOption.Ascending));
            clsChartOfAccount.CommitAndDispose();
            lstChartOfAccount.DataSource = dt.DefaultView;
            lstChartOfAccount.DataBind();
        }

		private bool Delete()
		{
			bool boRetValue = false;
			string stIDs = "";

			foreach(DataListItem item in lstAccountSummary.Items)
			{
                DataList lstAccountCategory = (DataList)item.FindControl("lstAccountCategory");
                foreach (DataListItem itemAccountCategory in lstAccountCategory.Items)
                {
                    DataList lstChartOfAccounts = (DataList)itemAccountCategory.FindControl("lstChartOfAccounts");
                    foreach (DataListItem itemChartOfAccounts in lstChartOfAccounts.Items)
                    {
                        HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemChartOfAccounts.FindControl("chkList");
                        if (chkList != null)
                        {
                            if (chkList.Checked == true)
                            {
                                stIDs += chkList.Value + ",";
                                boRetValue = true;
                            }
                        }
                    }
                }
			}
			if (boRetValue)
			{
				ChartOfAccount clsChartOfAccount = new ChartOfAccount();
				clsChartOfAccount.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsChartOfAccount.CommitAndDispose();
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
            foreach (DataListItem item in lstAccountSummary.Items)
            {
                DataList lstAccountCategory = (DataList) item.FindControl("lstAccountCategory");
                foreach (DataListItem itemAccountCategory in lstAccountCategory.Items)
                {
                    DataList lstChartOfAccounts = (DataList)itemAccountCategory.FindControl("lstChartOfAccounts");
                    foreach (DataListItem itemChartOfAccounts in lstChartOfAccounts.Items)
                    {
                        HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemChartOfAccounts.FindControl("chkList");
                        if (chkList != null)
                        {
                            if (chkList.Checked == true)
                            {
                                return chkList.Value;
                            }
                        } 
                    }
                }
            }

			return null;
		}
		private bool isChkListSingle()
		{
			bool boChkListSingle = true;
			int iCount = 0;
			
			foreach(DataListItem item in lstAccountSummary.Items)
			{
                DataList lstAccountCategory = (DataList)item.FindControl("lstAccountCategory");
                foreach (DataListItem itemAccountCategory in lstAccountCategory.Items)
                {
                    DataList lstChartOfAccounts = (DataList)itemAccountCategory.FindControl("lstChartOfAccounts");
                    foreach (DataListItem itemChartOfAccounts in lstChartOfAccounts.Items)
                    {
                        HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemChartOfAccounts.FindControl("chkList");
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
                }
			}
			return boChkListSingle;
		}


		#endregion


        //#region TreeView


        //#endregion

        //protected void tvList_OnTreeNodePopulate(object sender, TreeNodeEventArgs e)
        //{
        //    // Call the appropriate method to populate a node at a particular level.
        //    switch (e.Node.Depth)
        //    {
        //        case 0:
        //            // Populate the first-level nodes.
        //            LoadAccountSummaryList(e.Node);
        //            break;
        //        case 1:
        //            // Populate the second-level nodes.
        //            LoadAccountCategoryList(e.Node);
        //            break;
        //        case 2:
        //            // Populate the second-level nodes.
        //            LoadChartOfAccountsList(e.Node);
        //            break;
        //        default:
        //            // Do nothing.
        //            break;
        //    }
        //}

        //private void LoadAccountSummaryList(TreeNode node)
        //{
        //    // Query for the product categories. These are the values
        //    // for the second-level nodes.
        //    AccountSummary clsAccountSummary = new AccountSummary();
        //    DataClass clsDataClass = new DataClass();
            
        //    DataTable dt = clsDataClass.DataReaderToDataTable(clsAccountSummary.List("AccountSummaryID", SortOption.Ascending));

        //    clsAccountSummary.CommitAndDispose();

        //    // Create the second-level nodes.

        //    // Iterate through and create a new node for each row in the query results.
        //    // Notice that the query results are stored in the table of the DataSet.
        //    foreach (DataRow dtRow in dt.Rows)
        //    {

        //        // Create the new node. Notice that the CategoryId is stored in the Value property 
        //        // of the node. This will make querying for items in a specific category easier when
        //        // the third-level nodes are created. 
        //        TreeNode newNode = new TreeNode();
        //        newNode.Text = dtRow["AccountSummaryName"].ToString();
        //        newNode.Value = dtRow["AccountSummaryID"].ToString();

        //        // Set the PopulateOnDemand property to true so that the child nodes can be 
        //        // dynamically populated.
        //        newNode.PopulateOnDemand = true;

        //        // Set additional properties for the node.
        //        newNode.SelectAction = TreeNodeSelectAction.Expand;

        //        // Add the new node to the ChildNodes collection of the parent node.
        //        node.ChildNodes.Add(newNode);

        //    }
        //}
        //private void LoadAccountCategoryList(TreeNode node)
        //{

        //    // Query for the product categories. These are the values
        //    // for the second-level nodes.
        //    AccountCategory clsAccountCategory = new AccountCategory();
        //    DataClass clsDataClass = new DataClass();

        //    int iAccountSummaryID = Convert.ToInt32(node.Value);
        //    DataTable dt = clsDataClass.DataReaderToDataTable(clsAccountCategory.List(iAccountSummaryID, "a.AccountCategoryID", SortOption.Ascending));

        //    clsAccountCategory.CommitAndDispose();

        //    // Create the second-level nodes.

        //    // Iterate through and create a new node for each row in the query results.
        //    // Notice that the query results are stored in the table of the DataSet.
        //    foreach (DataRow dtRow in dt.Rows)
        //    {

        //        // Create the new node. Notice that the CategoryId is stored in the Value property 
        //        // of the node. This will make querying for items in a specific category easier when
        //        // the third-level nodes are created. 
        //        TreeNode newNode = new TreeNode();
        //        newNode.Text = dtRow["AccountCategoryName"].ToString();
        //        newNode.Value = dtRow["AccountCategoryID"].ToString();

        //        // Set the PopulateOnDemand property to true so that the child nodes can be 
        //        // dynamically populated.
        //        newNode.PopulateOnDemand = true;

        //        // Set additional properties for the node.
        //        newNode.SelectAction = TreeNodeSelectAction.Expand;

        //        // Add the new node to the ChildNodes collection of the parent node.
        //        node.ChildNodes.Add(newNode);

        //    }
        //}
        //private void LoadChartOfAccountsList(TreeNode node)
        //{

        //    // Query for the product categories. These are the values
        //    // for the second-level nodes.
        //    ChartOfAccount clsChartOfAccount = new ChartOfAccount();
        //    DataClass clsDataClass = new DataClass();

        //    int iAccountCategoryID = Convert.ToInt32(node.Value);
        //    DataTable dt = clsDataClass.DataReaderToDataTable(clsChartOfAccount.List(iAccountCategoryID, "ChartOfAccountID", SortOption.Ascending));

        //    clsChartOfAccount.CommitAndDispose();

        //    // Create the second-level nodes.

        //    // Iterate through and create a new node for each row in the query results.
        //    // Notice that the query results are stored in the table of the DataSet.
        //    foreach (DataRow dtRow in dt.Rows)
        //    {

        //        // Create the new node. Notice that the CategoryId is stored in the Value property 
        //        // of the node. This will make querying for items in a specific category easier when
        //        // the third-level nodes are created. 
        //        TreeNode newNode = new TreeNode();
        //        newNode.Text = dtRow["ChartOfAccountName"].ToString();
        //        newNode.Value = dtRow["ChartOfAccountID"].ToString();

        //        // Set the PopulateOnDemand property to true so that the child nodes can be 
        //        // dynamically populated.
        //        newNode.PopulateOnDemand = false;

        //        // Set additional properties for the node.
        //        newNode.SelectAction = TreeNodeSelectAction.None;

        //        // Add the new node to the ChildNodes collection of the parent node.
        //        node.ChildNodes.Add(newNode);

        //    }
        //}
    }
}
