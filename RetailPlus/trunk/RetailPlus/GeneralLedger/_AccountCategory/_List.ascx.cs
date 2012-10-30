using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.GeneralLedger._AccountCategory
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
		protected System.Web.UI.WebControls.ImageButton imgSave;
		protected System.Web.UI.WebControls.LinkButton cmdSave;
		protected System.Web.UI.WebControls.ImageButton imgCancel;
		protected System.Web.UI.WebControls.LinkButton cmdCancel;
		protected System.Web.UI.WebControls.Label lblReferrer;
		protected PagedDataSource PageData = new PagedDataSource();

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
					ManageSecurity();
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
            if (e.Item.ItemType == ListItemType.Header)
            {
                LoadSortFieldOptions(e);
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                Label lblAccountSummaryCode = (Label)e.Item.FindControl("lblAccountSummaryCode");
                lblAccountSummaryCode.Text = dr["AccountSummaryCode"].ToString();

                Label lblAccountSummaryName = (Label)e.Item.FindControl("lblAccountSummaryName");
                lblAccountSummaryName.Text = dr["AccountSummaryName"].ToString();

                DataList lstAccountCategory = (DataList)e.Item.FindControl("lstAccountCategory");
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

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["AccountCategoryID"].ToString();

                Label lblAccountCategoryCode = (Label)e.Item.FindControl("lblAccountCategoryCode");
                lblAccountCategoryCode.Text = dr["AccountCategoryCode"].ToString();

                Label lblAccountCategoryName = (Label)e.Item.FindControl("lblAccountCategoryName");
                lblAccountCategoryName.Text = dr["AccountCategoryName"].ToString();

            }
        }

		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccountCategory); 
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

		}

        private void LoadAccountSummaryList()
        {
            AccountSummary clsAccountSummary = new AccountSummary();
            DataClass clsDataClass = new DataClass();

            string SortField = "AccountSummaryCode";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            if (Request.QueryString["Search"] == null)
            {
                PageData.DataSource = clsDataClass.DataReaderToDataTable(clsAccountSummary.List(SortField, sortoption)).DefaultView;
            }
            else
            {
                string SearchKey = Common.Decrypt((string)Request.QueryString["search"], Session.SessionID);
                PageData.DataSource = clsDataClass.DataReaderToDataTable(clsAccountSummary.Search(SearchKey, SortField, sortoption)).DefaultView;
            }

            clsAccountSummary.CommitAndDispose();

            int iPageSize = Convert.ToInt16(Session["PageSize"]);

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
        private void LoadAccountCategoryList(DataList lstAccountCategory, int AccountSummaryID)
        {
            AccountCategory clsAccountCategory = new AccountCategory();
            DataClass clsDataClass = new DataClass();
            System.Data.DataTable dt = clsDataClass.DataReaderToDataTable(clsAccountCategory.List(AccountSummaryID, "AccountCategoryCode", SortOption.Ascending));
            clsAccountCategory.CommitAndDispose();
            lstAccountCategory.DataSource = dt.DefaultView;
            lstAccountCategory.DataBind();
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
                    HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemAccountCategory.FindControl("chkList");
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
			if (boRetValue)
			{
				AccountCategory clsAccountCategory = new AccountCategory();
				clsAccountCategory.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsAccountCategory.CommitAndDispose();
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
			foreach(DataListItem item in lstAccountSummary.Items)
			{
                DataList lstAccountCategory = (DataList)item.FindControl("lstAccountCategory");
                foreach (DataListItem itemAccountCategory in lstAccountCategory.Items)
                {
                    HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemAccountCategory.FindControl("chkList");
                    if (chkList!=null)
				    {
					    if (chkList.Checked == true)
					    {
						    return chkList.Value;
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
                    HtmlInputCheckBox chkList = (HtmlInputCheckBox)itemAccountCategory.FindControl("chkList");
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
			return boChkListSingle;
		}


		#endregion
	}
}
