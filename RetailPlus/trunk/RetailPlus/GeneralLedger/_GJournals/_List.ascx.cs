using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.GeneralLedger._GJournals
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
			if (!IsPostBack)
				if (Visible)
				{
					ManageSecurity();

					LoadList();
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
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);
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
		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["GJournalID"].ToString();

				AccountGJournalsStatus status = (AccountGJournalsStatus) Enum.Parse(typeof(AccountGJournalsStatus), dr["Status"].ToString());
				if (status == AccountGJournalsStatus.Cancelled || status == AccountGJournalsStatus.Posted)
                { chkList.Attributes.Add("disabled", "false"); }

                HyperLink lnkParticulars = (HyperLink)e.Item.FindControl("lnkParticulars");
                lnkParticulars.Text = dr["Particulars"].ToString();
				string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&GJournalid=" + Common.Encrypt(chkList.Value.ToString(),Session.SessionID);
				lnkParticulars.NavigateUrl = "Default.aspx" + stParam;

				Label lblTotalDebitAmount = (Label) e.Item.FindControl("lblTotalDebitAmount");
				lblTotalDebitAmount.Text = Convert.ToDecimal(dr["TotalDebitAmount"].ToString()).ToString("#,##0.#0");

				Label lblTotalCreditAmount = (Label) e.Item.FindControl("lblTotalCreditAmount");
				lblTotalCreditAmount.Text = Convert.ToDecimal(dr["TotalCreditAmount"].ToString()).ToString("#,##0.#0");

				Label lblStatus = (Label) e.Item.FindControl("lblStatus");
				lblStatus.Text = status.ToString("G");

				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            if (Cancel())
				LoadList();
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			if (Cancel())
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
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}

		#endregion

		#region Private Methods

		private bool Cancel()
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
				GJournals clsGJournals = new GJournals();
				clsGJournals.Cancel( stIDs.Substring(0,stIDs.Length-1));
				clsGJournals.CommitAndDispose();
			}

			return boRetValue;
		}

		private void Update()
		{
			if (isChkListSingle() == true)
			{
				AccountGJournalsStatus status = GetStatus();
				if (status == AccountGJournalsStatus.Open)
				{
					string stID = GetFirstID();
					if (stID!=null)
					{
						string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&GJournalid=" + Common.Encrypt(stID,Session.SessionID);	
						Response.Redirect("Default.aspx" + stParam);
					}
				} 
				else
				{
					string stScript = "<Script>";
					stScript += "window.alert('Sorry you cannot update a " + status.ToString("G") + " Record. Please select another record to update.')";
					stScript += "</Script>";
					Response.Write(stScript);	
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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.PaymentJournals); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgCancel.Visible = clsDetails.Write; 
			cmdCancel.Visible = clsDetails.Write; 
			imgEdit.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
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

            HyperLink SortByParticulars = (HyperLink)e.Item.FindControl("SortByParticulars");
			HyperLink SortByTotalDebitAmount = (HyperLink) e.Item.FindControl("SortByTotalDebitAmount");
			HyperLink SortByTotalCreditAmount = (HyperLink) e.Item.FindControl("SortByTotalCreditAmount");
			HyperLink SortByStatus = (HyperLink) e.Item.FindControl("SortByStatus");

            SortByParticulars.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Particulars", Session.SessionID);
			SortByTotalDebitAmount.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TotalDebitAmount", Session.SessionID);
			SortByTotalCreditAmount.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TotalCreditAmount", Session.SessionID);
			SortByStatus.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Status", Session.SessionID);
		}

		private void LoadList()
		{	
			GJournals clsGJournals = new GJournals();
			DataClass clsDataClass = new DataClass();

			string SortField = "GJournalID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsGJournals.List(SortField, sortoption)).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsGJournals.Search(SearchKey, SortField, sortoption)).DefaultView;
			}
			
			clsGJournals.CommitAndDispose();
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
		private AccountGJournalsStatus GetStatus()
		{
			AccountGJournalsStatus status = AccountGJournalsStatus.Open;

			foreach(DataListItem item in lstItem.Items)
			{
				HtmlInputCheckBox chkList = (HtmlInputCheckBox) item.FindControl("chkList");
				if (chkList!=null)
				{
					if (chkList.Checked == true)
					{
						Label lblStatus = (Label) item.FindControl("lblStatus");
						status = (AccountGJournalsStatus) Enum.Parse(typeof(AccountGJournalsStatus), lblStatus.Text);
						
						return status;
					}
				}
			}
			return status;
		}

		#endregion
    }
}
