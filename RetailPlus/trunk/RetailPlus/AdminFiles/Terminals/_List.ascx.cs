using AceSoft.RetailPlus.Security;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Security._Terminals
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

        protected void imgEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}
		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
				LoadSortFieldOptions(e);
			}
			else if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["TerminalID"].ToString();

				HyperLink lnkTerminalNo = (HyperLink) e.Item.FindControl("lnkTerminalNo");
				lnkTerminalNo.Text = dr["TerminalNo"].ToString();

				HyperLink lnkTerminalCode = (HyperLink) e.Item.FindControl("lnkTerminalCode");
				lnkTerminalCode.Text = dr["TerminalCode"].ToString();

				HyperLink lnkTerminalName = (HyperLink) e.Item.FindControl("lnkTerminalName");
				lnkTerminalName.Text = dr["TerminalName"].ToString();

				HyperLink lnkMachineSerialNo = (HyperLink) e.Item.FindControl("lnkMachineSerialNo");
				lnkMachineSerialNo.Text = dr["MachineSerialNo"].ToString();
				
				HyperLink lnkAccreditationNo = (HyperLink) e.Item.FindControl("lnkAccreditationNo");
				lnkAccreditationNo.Text = dr["AccreditationNo"].ToString();

				HyperLink lnkStatus = (HyperLink) e.Item.FindControl("lnkStatus");
				TerminalStatus status = (TerminalStatus) Enum.Parse(typeof(TerminalStatus), dr["Status"].ToString());
				lnkStatus.Text = status.ToString("G");
	
				Label lblDateCreated = (Label) e.Item.FindControl("lblDateCreated");
				lblDateCreated.Text = dr["DateCreated"].ToString();

				Label lblMaxReceiptWidth = (Label) e.Item.FindControl("lblMaxReceiptWidth");
				lblMaxReceiptWidth.Text = dr["MaxReceiptWidth"].ToString();

				CheckBox chkIsPrinterAutoCutter = (CheckBox) e.Item.FindControl("chkIsPrinterAutoCutter");
				chkIsPrinterAutoCutter.Checked = Convert.ToBoolean(dr["IsPrinterAutoCutter"]);

				CheckBox chkAutoPrint = (CheckBox) e.Item.FindControl("chkAutoPrint");
				chkAutoPrint.Checked = Convert.ToBoolean(Convert.ToInt16(dr["AutoPrint"].ToString()));

				Label lblPrinterName = (Label) e.Item.FindControl("lblPrinterName");
				lblPrinterName.Text = dr["PrinterName"].ToString();

				Label lblCashDrawerName = (Label) e.Item.FindControl("lblCashDrawerName");				
				lblCashDrawerName.Text = dr["CashDrawerName"].ToString();

				CheckBox chkItemVoidConfirmation = (CheckBox) e.Item.FindControl("chkItemVoidConfirmation");
				chkItemVoidConfirmation.Checked =Convert.ToBoolean(dr["ItemVoidConfirmation"]);
				
				CheckBox chkEnableEVAT = (CheckBox) e.Item.FindControl("chkEnableEVAT");
				chkEnableEVAT.Checked =Convert.ToBoolean(dr["EnableEVAT"]);

				Label lblFormBehavior = (Label) e.Item.FindControl("lblFormBehavior");
				lblFormBehavior.Text = dr["Form_Behavior"].ToString();

				Label lblMarqueeMessage = (Label) e.Item.FindControl("lblMarqueeMessage");
				lblMarqueeMessage.Text = dr["MarqueeMessage"].ToString();
				
				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
		{
			switch(e.CommandName)
			{
                case "imgItemEdit":
					HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
                    string stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
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
            //Int64 UID = Convert.ToInt64(Session["UID"]);
            //AccessRights clsAccessRights = new AccessRights(); 
            //AccessRightsDetails clsDetails = new AccessRightsDetails();

            //clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Terminal); 
            //cmdEdit.Visible = clsDetails.Write; 
            //imgEdit.Visible = clsDetails.Write; 
			
            //clsAccessRights.CommitAndDispose();
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

			HyperLink SortByTerminalNo = (HyperLink) e.Item.FindControl("SortByTerminalNo");
			HyperLink SortByTerminalCode = (HyperLink) e.Item.FindControl("SortByTerminalCode");
			HyperLink SortByTerminalName = (HyperLink) e.Item.FindControl("SortByTerminalName");
			HyperLink SortByMachineSerialNo = (HyperLink) e.Item.FindControl("SortByMachineSerialNo");
			HyperLink SortByAccreditationNo = (HyperLink) e.Item.FindControl("SortByAccreditationNo");
			HyperLink SortByStatus = (HyperLink) e.Item.FindControl("SortByStatus");

			SortByTerminalNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TerminalNo", Session.SessionID);
			SortByTerminalCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TerminalCode", Session.SessionID);
			SortByTerminalName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("TerminalName", Session.SessionID);
			SortByMachineSerialNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("MachineSerialNo", Session.SessionID);
			SortByAccreditationNo.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("AccreditationNo", Session.SessionID);
			SortByStatus.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Status", Session.SessionID);
		}
		private void LoadList()
		{	
			Terminal clsTerminal = new Terminal();

			string SortField = "TerminalNo";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            string SearchKey = "";
			if (Request.QueryString["Search"]!=null)
			{   SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);    }
            PageData.DataSource = clsTerminal.ListAsDataTable(SearchKey, SortField, sortoption).DefaultView;
			clsTerminal.CommitAndDispose();

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
