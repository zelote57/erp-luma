using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Product._Group._Charges
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
					ManageSecurity();
					lblGroupID.Text = Common.Decrypt((string)Request.QueryString["groupid"],Session.SessionID);

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

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.ProductGroups); 
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
			ProductGroupCharge clsProductGroupCharge = new ProductGroupCharge();
			DataClass clsDataClass = new DataClass();

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductGroupCharge.List(Convert.ToInt64(lblGroupID.Text),"ChargeType",SortOption.Ascending)).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsProductGroupCharge.Search(Convert.ToInt64(lblGroupID.Text),SearchKey, "ChargeType", SortOption.Ascending)).DefaultView;
			}

			clsProductGroupCharge.CommitAndDispose();

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
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&groupid=" + Common.Encrypt(lblGroupID.Text,Session.SessionID);			
			Response.Redirect("Default.aspx" + stParam);
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&groupid=" + Common.Encrypt(lblGroupID.Text,Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);
		}

		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["ChargeID"].ToString();

				Label lblChargeType = (Label) e.Item.FindControl("lblChargeType");
				lblChargeType.Text = dr["ChargeType"].ToString();

				Label lblChargeAmount = (Label) e.Item.FindControl("lblChargeAmount");
				lblChargeAmount.Text = Convert.ToDecimal(dr["ChargeAmount"].ToString()).ToString("#,##0.#0");

				HtmlInputCheckBox chkInPercent = (HtmlInputCheckBox) e.Item.FindControl("chkInPercent");
				chkInPercent.Disabled = true;
                if (Convert.ToBoolean(dr["InPercent"].ToString()))
					chkInPercent.Checked = true;
				else
					chkInPercent.Checked = false;

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
				ProductGroupCharge clsProductGroupCharge = new ProductGroupCharge();
				clsProductGroupCharge.Delete(Convert.ToInt64(lblGroupID.Text),stIDs.Substring(0,stIDs.Length-1));
				clsProductGroupCharge.CommitAndDispose();
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
					string stParam = "?task=" + Common.Encrypt("edit",Session.SessionID) + "&groupid=" + Common.Encrypt(lblGroupID.Text,Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
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

		protected void cboCurrentPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadList();
		}

		private void idBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("/RetailPlus/MasterFiles/_ProductGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

		protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("/RetailPlus/MasterFiles/_ProductGroup/Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}

	}
}
