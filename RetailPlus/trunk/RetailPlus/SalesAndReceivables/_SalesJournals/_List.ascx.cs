using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.SalesAndReceivables._SalesJournals
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
			this.imgGRN.Click += new System.Web.UI.ImageClickEventHandler(this.imgGRN_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Common Common = new Common();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			Common Common = new Common();
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
				chkList.Value = dr["SOID"].ToString();

				HyperLink lnkSONo = (HyperLink) e.Item.FindControl("lnkSONo");
				lnkSONo.Text = dr["SONo"].ToString();
				Common Common = new Common();
				string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&poid=" + Common.Encrypt(chkList.Value.ToString(),Session.SessionID);
				lnkSONo.NavigateUrl = "Default.aspx" + stParam;

				Label lblSODate = (Label) e.Item.FindControl("lblSODate");
				lblSODate.Text = Convert.ToDateTime(dr["SODate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");

				Label lblCustomerID = (Label) e.Item.FindControl("lblCustomerID");
				lblCustomerID.Text = dr["CustomerID"].ToString();

				HyperLink lblCustomerCode = (HyperLink) e.Item.FindControl("lblCustomerCode");
				lblCustomerCode.Text = dr["CustomerCode"].ToString();
				stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(lblCustomerID.Text,Session.SessionID);	
				lblCustomerCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/SalessAndPayables/_Vendor/Default.aspx" + stParam;
				
				Label lblReqDeliveryDate = (Label) e.Item.FindControl("lblReqDeliveryDate");
				lblReqDeliveryDate.Text = Convert.ToDateTime(dr["RequiredDeliveryDate"].ToString()).ToString("yyyy-MM-dd");

				Label lblBranchID = (Label) e.Item.FindControl("lblBranchID");
				lblBranchID.Text = dr["BranchID"].ToString();
				Label lblBranchCode = (Label) e.Item.FindControl("lblBranchCode");
				lblBranchCode.Text = dr["BranchCode"].ToString();

				Label lblSOSubTotal = (Label) e.Item.FindControl("lblSOSubTotal");
				lblSOSubTotal.Text = Convert.ToDecimal(dr["SOSubTotal"].ToString()).ToString("#,##0.#0");

				Label lblSORemarks = (Label) e.Item.FindControl("lblSORemarks");
				lblSORemarks.Text = dr["SORemarks"].ToString();

				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}				

		private void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Delete())
				LoadList();
		}

		private void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadList();
		}

		private void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}

		private void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
		
		private void imgAdditem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Additem();
		}

		private void cmdAdditem_Click(object sender, System.EventArgs e)
		{
			Additem();
		}

		private void imgGRN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
				SO clsSO = new SO();
				clsSO.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsSO.CommitAndDispose();
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
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&poid=" + Common.Encrypt(stID,Session.SessionID);	
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

		private void Additem()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					Common Common = new Common();
					string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&poid=" + Common.Encrypt(stID,Session.SessionID);	
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
					string stParam = "?task=" + Common.Encrypt("issuegrn",Session.SessionID) + "&poid=" + Common.Encrypt(stID,Session.SessionID);	
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
//			Int64 UID = Convert.ToInt64(Session["UID"]);
//			AccessRights clsAccessRights = new AccessRights(); 
//			AccessRightsDetails clsDetails = new AccessRightsDetails();
//
//			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.SalesOrders); 
//
//			clsAccessRights.CommitAndDispose();
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

			HyperLink SortBySONo = (HyperLink) e.Item.FindControl("SortBySONo");
			HyperLink SortBySODate = (HyperLink) e.Item.FindControl("SortBySODate");
			HyperLink SortByCustomerCode = (HyperLink) e.Item.FindControl("SortByCustomerCode");
			HyperLink SortByReqDeliveryDate = (HyperLink) e.Item.FindControl("SortByReqDeliveryDate");
			HyperLink SortByBranchCode = (HyperLink) e.Item.FindControl("SortByBranchCode");
			HyperLink SortBySOSubTotal = (HyperLink) e.Item.FindControl("SortBySOSubTotal");
			HyperLink SortBySORemarks = (HyperLink) e.Item.FindControl("SortBySORemarks");

			SortBySONo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SONo", Session.SessionID);
			SortBySODate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SODate", Session.SessionID);
			SortByCustomerCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CustomerCode", Session.SessionID);
			SortByReqDeliveryDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("RequiredDeliveryDate", Session.SessionID);
			SortByBranchCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("BranchID", Session.SessionID);
			SortBySOSubTotal.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SOSubTotal", Session.SessionID);
			SortBySORemarks.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("SORemarks", Session.SessionID);
		}

		private void LoadList()
		{	
			SO clsSO = new SO();
			DataClass clsDataClass = new DataClass();
			Common Common = new Common();

			string SortField = "SOID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}

			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsSO.List(SOStatus.Posted, SortField, sortoption)).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsSO.Search(SOStatus.Posted, SearchKey, SortField, sortoption)).DefaultView;
			}

			clsSO.CommitAndDispose();

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
