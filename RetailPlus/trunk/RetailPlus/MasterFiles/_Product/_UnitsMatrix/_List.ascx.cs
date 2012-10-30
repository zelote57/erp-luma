

namespace AceSoft.RetailPlus.MasterFiles._Product._UnitsMatrix
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

        public string ProductID
        { get;  set; }

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ManageSecurity();

            if (!IsPostBack && Visible)
			{
                if (Request.QueryString["prodid"] != null)
				    lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);
                else if (Request.QueryString["id"] != null) 
                    lblProductID.Text = Common.Decrypt((string)Request.QueryString["id"], Session.SessionID); ;

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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);			
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
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
        protected void idViewProductDetails_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);

		}
        protected void cmdViewProductDetails_Click(object sender, System.EventArgs e)
		{
            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(lblProductID.Text, Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx" + stParam);
		}
        protected void idBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
		}
		protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
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
				chkList.Value = dr["MatrixID"].ToString();

				TextBox txtBaseUnit = (TextBox) e.Item.FindControl("txtBaseUnit");
				txtBaseUnit.Text = dr["BaseUnitName"].ToString();

				TextBox txtBaseUnitValue = (TextBox) e.Item.FindControl("txtBaseUnitValue");
				txtBaseUnitValue.Text = dr["BaseUnitValue"].ToString();

				TextBox txtBottomUnit = (TextBox) e.Item.FindControl("txtBottomUnit");
				txtBottomUnit.Text = dr["BottomUnitName"].ToString();

				TextBox txtBottomUnitValue = (TextBox) e.Item.FindControl("txtBottomUnitValue");
				txtBottomUnitValue.Text = dr["BottomUnitValue"].ToString();
			}
		}
        protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            string stParam = string.Empty;
            HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");

            switch (e.CommandName)
            {
                case "imgEditNowClick":
                    stParam = "?task=" + Common.Encrypt("edit", Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text, Session.SessionID) + "&id=" + Common.Encrypt(chkList.Value, Session.SessionID);
                    Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_UnitsMatrix/Default.aspx" + stParam);
                    break;
            }
        }

		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductVariations); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdViewProductDetails.Visible = clsDetails.Write; 
			idViewProductDetails.Visible = clsDetails.Write; 
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

		}
		private void LoadList()
		{	
			ProductUnitsMatrix clsProductUnitsMatrix = new ProductUnitsMatrix();
			DataClass clsDataClass = new DataClass();

			string SortField = "a.MatrixID";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductUnitsMatrix.List(Convert.ToInt64(lblProductID.Text), SortField, sortoption)).DefaultView;
			}
			else
			{	
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductUnitsMatrix.Search(lblProductID.Text, SortField, sortoption)).DefaultView;
			}

			clsProductUnitsMatrix.CommitAndDispose();

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
				ProductUnitsMatrix clsUnitMatrix = new ProductUnitsMatrix();
				clsUnitMatrix.Delete(stIDs.Substring(0,stIDs.Length-1));
				clsUnitMatrix.CommitAndDispose();
			}

			return boRetValue;
		}

		#endregion
    }
}
