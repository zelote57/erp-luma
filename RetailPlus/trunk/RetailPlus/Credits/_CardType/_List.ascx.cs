using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Credits._CardType
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
			ManageSecurity();

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
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdAdd_Click(object sender, System.EventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
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
        protected void idEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ImageButton imgItemDelete = (ImageButton)e.Item.FindControl("imgItemDelete");
                ImageButton imgItemEdit = (ImageButton)e.Item.FindControl("imgItemEdit");

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["CardTypeID"].ToString();
                imgItemDelete.Enabled = cmdDelete.Visible; if (!imgItemDelete.Enabled) imgItemDelete.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                imgItemEdit.Enabled = cmdEdit.Visible; if (!imgItemEdit.Enabled) imgItemEdit.ImageUrl = Constants.ROOT_DIRECTORY + "/_layouts/images/blank.gif";
                if (imgItemDelete.Enabled) imgItemDelete.Attributes.Add("onClick", "return confirm_item_delete();");

				Label lblCardTypeCode = (Label) e.Item.FindControl("lblCardTypeCode");
				lblCardTypeCode.Text = dr["CardTypeCode"].ToString();

                HyperLink lnkCardTypeName = (HyperLink)e.Item.FindControl("lnkCardTypeName");
                lnkCardTypeName.Text = dr["CardTypeName"].ToString();
                lnkCardTypeName.NavigateUrl = "Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);

                Label lblCreditFinanceCharge = (Label)e.Item.FindControl("lblCreditFinanceCharge");
                lblCreditFinanceCharge.Text = decimal.Parse(dr["CreditFinanceCharge"].ToString()).ToString("#,##0.#0");

                Label lblCreditLatePenaltyCharge = (Label)e.Item.FindControl("lblCreditLatePenaltyCharge");
                lblCreditLatePenaltyCharge.Text = decimal.Parse(dr["CreditLatePenaltyCharge"].ToString()).ToString("#,##0.#0");

                Label lblCreditMinimumAmountDue = (Label)e.Item.FindControl("lblCreditMinimumAmountDue");
                lblCreditMinimumAmountDue.Text = decimal.Parse(dr["CreditMinimumAmountDue"].ToString()).ToString("#,##0.#0");

                Label lblCreditMinimumPercentageDue = (Label)e.Item.FindControl("lblCreditMinimumPercentageDue");
                lblCreditMinimumPercentageDue.Text = decimal.Parse(dr["CreditMinimumPercentageDue"].ToString()).ToString("#,##0.#0");

                CheckBox chkWithGuarantor = (CheckBox)e.Item.FindControl("chkWithGuarantor");
                chkWithGuarantor.Checked = bool.Parse(dr["WithGuarantor"].ToString());

                CheckBox chkExemptInTerminalCharge = (CheckBox)e.Item.FindControl("chkExemptInTerminalCharge");
                chkExemptInTerminalCharge.Checked = bool.Parse(dr["ExemptInTerminalCharge"].ToString());
			}
		}
        protected void lstItem_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
            string stParam = string.Empty;
            switch (e.CommandName)
            {
                case "imgItemDelete":
                    CardType clsCardType = new CardType();
                    clsCardType.Delete(chkList.Value);
                    clsCardType.CommitAndDispose();

                    LoadList();
                    break;
                case "imgItemEdit":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect("Default.aspx" + stParam);
                    break;
            }
        }
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}

		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (int)AccessTypes.CardType); 
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

			HyperLink SortByCardTypeCode = (HyperLink) e.Item.FindControl("SortByCardTypeCode");
			HyperLink SortByCardTypeName = (HyperLink) e.Item.FindControl("SortByCardTypeName");

			SortByCardTypeCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CardTypeCode", Session.SessionID);
			SortByCardTypeName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("CardTypeName", Session.SessionID);
		}
		private void LoadList()
		{	
			CardType clsCardType = new CardType();
			DataClass clsDataClass = new DataClass();

			string SortField = "CardTypeID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

            Data.CardTypeDetails SearchKeys = new CardTypeDetails(CreditCardTypes.Internal);
            if (Request.QueryString["Search"]!=null)
			{
                SearchKeys.CardTypeCode = Common.Decrypt((string)Request.QueryString["search"], Session.SessionID);
                SearchKeys.CardTypeName = Common.Decrypt((string)Request.QueryString["search"], Session.SessionID);
            }

            PageData.DataSource = clsCardType.ListAsDataTable(SearchKeys, SortField, sortoption).DefaultView;	
			clsCardType.CommitAndDispose();

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
				CardType clsCardType = new CardType();
				clsCardType.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsCardType.CommitAndDispose();
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
