using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Inventory._VariationsMatrix
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();

					ManageSecurity();
					lblProductID.Text = Common.Decrypt((string)Request.QueryString["prodid"],Session.SessionID);

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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductVariations); 
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
            string stSearchKey = string.Empty;
            if (Request.QueryString["Search"] != null)
            { stSearchKey = Server.UrlDecode(Common.Decrypt((string)Request.QueryString["search"], Session.SessionID)); }
            else if (Session["Search"] != null)
            { stSearchKey = Server.UrlDecode(Common.Decrypt(Session["Search"].ToString(), Session.SessionID)); }

            try { Session.Remove("Search"); }
            catch { }
            if (stSearchKey == null) { stSearchKey = string.Empty; }
            else if (stSearchKey != string.Empty) { Session.Add("Search", Common.Encrypt(stSearchKey, Session.SessionID)); }

            ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
            System.Data.DataTable dt = clsProductVariationsMatrix.BaseListAsDataTable(Int64.Parse(lblProductID.Text), MatrixDescription:  stSearchKey);
            clsProductVariationsMatrix.CommitAndDispose();
            
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
			this.idBack.Click += new System.Web.UI.ImageClickEventHandler(this.idBack_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		private void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);			
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID);
			Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
		}

		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["MatrixID"].ToString();

				HyperLink lnkVariation = (HyperLink) e.Item.FindControl("lnkVariation");
				if (dr["Description"].ToString() == string.Empty || dr["Description"].ToString() == null)
					lnkVariation.Text = "_";
				else
				{
					lnkVariation.Text = dr["Description"].ToString();
					lnkVariation.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["MatriXID"].ToString(), Session.SessionID);
				}

				Label lblUnitName = (Label) e.Item.FindControl("lblUnitName");
				lblUnitName.Text = dr["UnitName"].ToString();

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

				Label lblMinThreshold = (Label) e.Item.FindControl("lblMinThreshold");
				lblMinThreshold.Text = Convert.ToDecimal(dr["MinThreshold"].ToString()).ToString("#,##0.#0");

				Label lblMaxThreshold = (Label) e.Item.FindControl("lblMaxThreshold");
				lblMaxThreshold.Text = Convert.ToDecimal(dr["MaxThreshold"].ToString()).ToString("#,##0.#0");
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
                // Aug 1, 2011 : Lemu
                // Remove the entire code and replaced with the code below to cater InventoryAdjustment and ProductMovement 
                // See _Product/_variationsMatrix/_List.cs.Delete
                // 
                // ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                // clsProductVariationsMatrix.Delete(stIDs.Substring(0,stIDs.Length-1));
                // clsProductVariationsMatrix.SynchronizeQuantity(Convert.ToInt64(lblProductID.Text));
                // clsProductVariationsMatrix.CommitAndDispose();

                Security.AccessUserDetails clsAccessUserDetails = (Security.AccessUserDetails)Session["AccessUserDetails"];
                ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix();
                clsProductVariationsMatrix.GetConnection();

                Products clsProducts = new Products(clsProductVariationsMatrix.Connection, clsProductVariationsMatrix.Transaction);

                long ProductID = Int64.Parse(lblProductID.Text);

                string[] strIDs = stIDs.Substring(0, stIDs.Length - 1).Split(',');
                foreach (string ID in strIDs)
                {
                    long MatrixID = long.Parse(ID);
                    ProductDetails clsDetails = clsProducts.Details(ProductID: ProductID, MatrixID: MatrixID);

                    InvAdjustmentDetails clsInvAdjustmentDetails = new InvAdjustmentDetails();
                    clsInvAdjustmentDetails.UID = clsAccessUserDetails.UID;
                    clsInvAdjustmentDetails.InvAdjustmentDate = DateTime.Now;
                    clsInvAdjustmentDetails.ProductID = clsDetails.ProductID;
                    clsInvAdjustmentDetails.ProductCode = clsDetails.ProductCode;
                    clsInvAdjustmentDetails.Description = clsDetails.ProductDesc;
                    clsInvAdjustmentDetails.VariationMatrixID = clsDetails.MatrixID;
                    clsInvAdjustmentDetails.MatrixDescription = clsDetails.MatrixDescription;
                    clsInvAdjustmentDetails.UnitID = clsDetails.BaseUnitID;
                    clsInvAdjustmentDetails.UnitCode = clsDetails.BaseUnitCode;
                    clsInvAdjustmentDetails.QuantityBefore = clsDetails.Quantity;
                    clsInvAdjustmentDetails.QuantityNow = 0;
                    clsInvAdjustmentDetails.MinThresholdBefore = clsDetails.MinThreshold;
                    clsInvAdjustmentDetails.MinThresholdNow = 0;
                    clsInvAdjustmentDetails.MaxThresholdBefore = clsDetails.MaxThreshold;
                    clsInvAdjustmentDetails.MaxThresholdNow = 0;
                    clsInvAdjustmentDetails.Remarks = "deleted item.";

                    InvAdjustment clsInvAdjustment = new InvAdjustment(clsProducts.Connection, clsProducts.Transaction);
                    clsInvAdjustment.Insert(clsInvAdjustmentDetails);

                    clsProducts.SubtractQuantity(Constants.BRANCH_ID_MAIN, clsDetails.ProductID, clsDetails.MatrixID, clsDetails.Quantity, Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_PRODUCT_VARIATION_DELETE) + " : " + clsDetails.MatrixDescription, clsInvAdjustmentDetails.InvAdjustmentDate, "SYS-VARDEL" + clsInvAdjustmentDetails.InvAdjustmentDate.ToString("yyyyMMddHHmmss"), clsAccessUserDetails.Name);

                    clsProductVariationsMatrix.Delete(long.Parse(ID));
                }
                clsProductVariationsMatrix.CommitAndDispose();
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
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&prodid=" + Common.Encrypt(lblProductID.Text,Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx" + stParam);
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

		private void idBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

	}
}
