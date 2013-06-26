using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Inventory
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __List : System.Web.UI.UserControl
	{
		protected PagedDataSource PageData = new PagedDataSource();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
					ManageSecurity();
					LoadList();
					cmdDelete.Attributes.Add("onClick", "return confirm_delete();");
					imgDelete.Attributes.Add("onClick", "return confirm_delete();");
				}
		}

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.AccessGroups); 
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

		private void LoadList()
		{
            string SortField = "ProductDesc";
            if (Request.QueryString["sortfield"] != null)
            { SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID); }

            SortOption sortoption = SortOption.Ascending;
            if (Request.QueryString["sortoption"] != null)
            { sortoption = (SortOption)Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true); }

            string stSearchKey = string.Empty;
            if (Request.QueryString["Search"] != null)
            { stSearchKey = Server.UrlDecode(Common.Decrypt((string)Request.QueryString["search"], Session.SessionID)); }
            else if (Session["Search"] != null)
            { stSearchKey = Server.UrlDecode(Common.Decrypt(Session["Search"].ToString(), Session.SessionID)); }

            try { Session.Remove("Search"); }
            catch { }
            if (stSearchKey == null) { stSearchKey = string.Empty; }
            else if (stSearchKey != string.Empty) { Session.Add("Search", Common.Encrypt(stSearchKey, Session.SessionID)); }

            ProductInventories clsProduct = new ProductInventories();
            System.Data.DataTable dt = clsProduct.ListAsDataTable(BarCode: stSearchKey, ProductCode: stSearchKey, Limit: 100, SortField: SortField, SortOrder: SortOption.Ascending);
            clsProduct.CommitAndDispose();

            PageData.DataSource = dt.DefaultView;

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

		}
		#endregion

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("/RetailPlus/MasterFiles/_Product/Default.aspx" + stParam);
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);			
			Response.Redirect("/RetailPlus/MasterFiles/_Product/Default.aspx" + stParam);
		}

        protected void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr[ProductColumnNames.ProductID].ToString();

                HyperLink lnkBarcode = (HyperLink)e.Item.FindControl("lnkBarcode");
                lnkBarcode.Text = dr[ProductColumnNames.BarCode].ToString();
                lnkBarcode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr[ProductColumnNames.ProductID].ToString(), Session.SessionID);

                HyperLink lnkProductCode = (HyperLink)e.Item.FindControl("lnkProductCode");
				lnkProductCode.Text = dr[ProductColumnNames.ProductCode].ToString();
                if (!string.IsNullOrEmpty(dr["MatrixDescription"].ToString())) {
                    lnkProductCode.Text += " - " + dr["MatrixDescription"].ToString();
                }
                lnkProductCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr[ProductColumnNames.ProductID].ToString(), Session.SessionID);

                Label lblGroup = (Label)e.Item.FindControl("lblGroup");
                lblGroup.Text = dr[ProductColumnNames.ProductGroupCode].ToString() + " / " + dr[ProductColumnNames.ProductSubGroupCode].ToString();

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                // lblQuantity.Text = Convert.ToDecimal(dr[ProductColumnNames.MainQuantity].ToString()).ToString("#,##0.#0") + " " + dr[ProductColumnNames.BaseUnitCode];
                lblQuantity.Text = dr[ProductColumnNames.ConvertedQuantity].ToString();

				Label lblMinThreshold = (Label) e.Item.FindControl("lblMinThreshold");
				lblMinThreshold.Text = Convert.ToDecimal(dr[ProductColumnNames.MinThreshold].ToString()).ToString("#,##0.#0");

				Label lblMaxThreshold = (Label) e.Item.FindControl("lblMaxThreshold");
				lblMaxThreshold.Text = Convert.ToDecimal(dr[ProductColumnNames.MaxThreshold].ToString()).ToString("#,##0.#0");

                Data.BranchInventoryColumns clsBranchInventoryColumns = new Data.BranchInventoryColumns();
                clsBranchInventoryColumns.BranchCode = true;
                clsBranchInventoryColumns.Quantity = true;

                //Data.BranchInventoryColumns clsSearchColumns = new Data.BranchInventoryColumns();

                //Data.BranchInventory clsBranchInventory = new Data.BranchInventory();
                //DataTable dt = clsBranchInventory.ListAsDataTable(clsBranchInventoryColumns, clsSearchColumns, 0, long.Parse(dr[ProductColumnNames.ProductID].ToString()), string.Empty, System.Data.SqlClient.SortOrder.Ascending);
                //clsBranchInventory.CommitAndDispose();

                //if (dt.Rows.Count > 0)
                //{
                //    DataList lstBranchInventory = (DataList)e.Item.FindControl("lstBranchInventory");
                //    lstBranchInventory.DataSource = dt; lstBranchInventory.DataBind(); lstBranchInventory.Visible = true;
                //}
			}
		}				

		protected void lstItem_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			HtmlInputCheckBox chkList = null;
			string stParam = null;

			chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
			stParam = "?task=" + Common.Encrypt("list",Session.SessionID) + 
					"&prodid=" + Common.Encrypt(chkList.Value,Session.SessionID);

			switch(e.CommandName)
			{
				case "imgVariationsMatrixClick":
					Response.Redirect("/RetailPlus/Inventory/_VariationsMatrix/Default.aspx" + stParam);
					break;
			}
		}

        protected void lstBranchInventory_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HyperLink lnkBranchCode = (HyperLink)e.Item.FindControl("lnkBranchCode");
                lnkBranchCode.Text = dr[BranchInventoryColumnNames.BranchCode].ToString();
                lnkBranchCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/Inventory/_Branch/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr[BranchInventoryColumnNames.BranchID].ToString(), Session.SessionID);

                Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
                //lblQuantity.Text = Convert.ToDecimal(dr[BranchInventoryColumnNames.Quantity].ToString()).ToString("#,##0.#0");
                lblQuantity.Text = dr[BranchInventoryColumnNames.ConvertedQuantity].ToString();

            }
        }	
		private void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Delete())
				LoadList();
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadList();
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
				Products clsProduct = new Products();
				clsProduct.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsProduct.CommitAndDispose();
			}

			return boRetValue;
		}

		private void idEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Update();
		}

		protected void cmdEdit_Click(object sender, System.EventArgs e)
		{
			Update();
		}
		private void Update()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("/RetailPlus/MasterFiles/_Product/Default.aspx" + stParam);
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

		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}


		private void UpdateProductPrice()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					string stParam = "?task=" + Common.Encrypt("ProductPrice",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);
					Response.Redirect("/RetailPlus/MasterFiles/_Product/Default.aspx" + stParam);
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

		private void imgPrice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateProductPrice();
		}

		private void cmdProductPriceUpdate_Click(object sender, System.EventArgs e)
		{
			UpdateProductPrice();
		}
	}
}
