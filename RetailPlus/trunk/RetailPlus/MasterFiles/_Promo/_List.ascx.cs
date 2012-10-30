using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.MasterFiles._Promo
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
			this.idStuff.Click += new System.Web.UI.ImageClickEventHandler(this.idStuff_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);
			this.imgActivate.Click += new System.Web.UI.ImageClickEventHandler(this.imgActivate_Click);
			this.imgDeactivate.Click += new System.Web.UI.ImageClickEventHandler(this.imgDeactivate_Click);

		}
		#endregion

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
				LoadList();
		}

		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (Delete())
				LoadList();
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
			LoadList();
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
				chkList.Value = dr["PromoID"].ToString();

				Label lblPromoCode = (Label) e.Item.FindControl("lblPromoCode");
				lblPromoCode.Text = dr["PromoCode"].ToString();

				Label lblPromoName = (Label) e.Item.FindControl("lblPromoName");
				lblPromoName.Text = dr["PromoName"].ToString();

				Label lblPromoType = (Label) e.Item.FindControl("lblPromoType");
				lblPromoType.Text = dr["PromoTypeCode"].ToString();

				Label lblStartDate = (Label) e.Item.FindControl("lblStartDate");
				lblStartDate.Text = Convert.ToDateTime(dr["StartDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");
				
				Label lblEndDate = (Label) e.Item.FindControl("lblEndDate");
				lblEndDate.Text = Convert.ToDateTime(dr["EndDate"].ToString()).ToString("MM/dd/yyyy HH:mm:ss");

				Label lblStatus = (Label) e.Item.FindControl("lblStatus");
				PromoStatus Status = (PromoStatus) Enum.Parse(typeof(PromoStatus), dr["Status"].ToString());
				lblStatus.Text = Status.ToString("G");

				Int64 PromoID = Convert.ToInt64(dr["PromoID"].ToString());
				DataList lstStuff = (DataList) e.Item.FindControl("lstStuff");
				PromoItems clsPromoItems = new PromoItems();
				DataClass clsDataClass = new DataClass();

				lstStuff.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstStuff_ItemDataBound);
				lstStuff.DataSource = clsDataClass.DataReaderToDataTable(clsPromoItems.List(PromoID, "PromoItemsID",SortOption.Ascending)).DefaultView;
				lstStuff.DataBind();
				clsPromoItems.CommitAndDispose();

				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
				
				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}				


		private void idStuff_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Stuff();
		}

		protected void cmdStuff_Click(object sender, System.EventArgs e)
		{
			Stuff();
		}

		private void lstStuff_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkListStuff = (HtmlInputCheckBox) e.Item.FindControl("chkListStuff");
				chkListStuff.Value = dr["PromoItemsID"].ToString();

				Label lblContactName = (Label) e.Item.FindControl("lblContactName");
				if (dr["ContactName"].ToString()==string.Empty)
				{
					lblContactName.Text = "All Contacts";
				}
				else
				{
					lblContactName.Text = dr["ContactName"].ToString();
				}

				Label lblProductGroup = (Label) e.Item.FindControl("lblProductGroup");
				if (dr["ProductGroupName"].ToString()==string.Empty)
				{
					lblProductGroup.Text = "All Groups";
				}
				else
				{
					lblProductGroup.Text = dr["ProductGroupName"].ToString();
				}

				Label lblProductSubGroup = (Label) e.Item.FindControl("lblProductSubGroup");
				if (dr["ProductSubGroupName"].ToString()==string.Empty)
				{
					lblProductSubGroup.Text = "All SubGroups";
				}
				else
				{
					lblProductSubGroup.Text = dr["ProductSubGroupName"].ToString();
				}

				Label lblProduct = (Label) e.Item.FindControl("lblProduct");
				if (dr["ProductDesc"].ToString()==string.Empty)
				{
					lblProduct.Text = "All Products";
				}
				else
				{
					lblProduct.Text = dr["ProductDesc"].ToString();
				}

				Label lblVariation = (Label) e.Item.FindControl("lblVariation");
				if (dr["Description"].ToString()==string.Empty)
				{
					lblVariation.Text = "All Variations";
				}
				else
				{
					lblVariation.Text = dr["Description"].ToString();
				}

				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#");

				Label lblPromoValue = (Label) e.Item.FindControl("lblPromoValue");
				lblPromoValue.Text = Convert.ToDecimal(dr["PromoValue"].ToString()).ToString("#,##0.#");

				CheckBox chkInPercent = (CheckBox) e.Item.FindControl("chkInPercent");
				chkInPercent.Checked = Convert.ToBoolean(Convert.ToByte(dr["InPercent"].ToString()));

				//For anchor
				//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
				//				
				//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}


		private void imgActivate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Activate())
				LoadList();
		}

		protected void cmdActivate_Click(object sender, System.EventArgs e)
		{
			if (Activate())
				LoadList();
		}

		private void imgDeactivate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (DeActivate())
				LoadList();
		}

		protected void cmdDeactivate_Click(object sender, System.EventArgs e)
		{
			if (DeActivate())
				LoadList();
		}


		#endregion

		#region Private Methods

		private void ManageSecurity()
		{
			Int64 UID = Convert.ToInt64(Session["UID"]);
			AccessRights clsAccessRights = new AccessRights(); 
			AccessRightsDetails clsDetails = new AccessRightsDetails();

			clsDetails = clsAccessRights.Details(UID,(int) AccessTypes.Promos); 
			imgAdd.Visible = clsDetails.Write; 
			cmdAdd.Visible = clsDetails.Write; 
			imgDelete.Visible = clsDetails.Write; 
			cmdDelete.Visible = clsDetails.Write; 
			cmdEdit.Visible = clsDetails.Write; 
			idEdit.Visible = clsDetails.Write; 
			cmdStuff.Visible = clsDetails.Write; 
			idStuff.Visible = clsDetails.Write; 
			lblSeparator1.Visible = clsDetails.Write;
			lblSeparator2.Visible = clsDetails.Write;
			lblSeparator3.Visible = clsDetails.Write;

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

			HyperLink SortByPromoCode = (HyperLink) e.Item.FindControl("SortByPromoCode");
			HyperLink SortByPromoName = (HyperLink) e.Item.FindControl("SortByPromoName");
			HyperLink SortByPromoType = (HyperLink) e.Item.FindControl("SortByPromoType");
			HyperLink SortByStartDate = (HyperLink) e.Item.FindControl("SortByStartDate");
			HyperLink SortByEndDate = (HyperLink) e.Item.FindControl("SortByEndDate");
			HyperLink SortByStatus = (HyperLink) e.Item.FindControl("SortByStatus");

			SortByPromoCode.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PromoCode", Session.SessionID);
			SortByPromoName.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PromoName", Session.SessionID);
			SortByPromoType.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("PromoTypeCode", Session.SessionID);
			SortByStartDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("StartDate", Session.SessionID);
			SortByEndDate.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("EndDate", Session.SessionID);
			SortByStatus.NavigateUrl = "Default.aspx" + stParam + "&sortfield=" + Common.Encrypt("Status", Session.SessionID);
		}

		private void LoadList()
		{	
			Promo clsPromo = new Promo();
			DataClass clsDataClass = new DataClass();

			string SortField = "PromoName";
			if (Request.QueryString["sortfield"]!=null)
			{	SortField = Common.Decrypt(Request.QueryString["sortfield"].ToString(), Session.SessionID);	}
			
			SortOption sortoption = SortOption.Ascending;
			if (Request.QueryString["sortoption"]!=null)
			{	sortoption = (SortOption) Enum.Parse(typeof(SortOption), Common.Decrypt(Request.QueryString["sortoption"], Session.SessionID), true);	}

			if (Request.QueryString["Search"]==null)
			{
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsPromo.List(SortField, sortoption)).DefaultView;
			}
			else
			{						
				string SearchKey = Common.Decrypt((string)Request.QueryString["search"],Session.SessionID);
				PageData.DataSource = clsDataClass.DataReaderToDataTable(clsPromo.Search(SearchKey, SortField, sortoption)).DefaultView;
			}

			clsPromo.CommitAndDispose();

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
				Promo clsPromo = new Promo();
				clsPromo.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsPromo.CommitAndDispose();
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


		private void Stuff()
		{
			if (isChkListSingle() == true)
			{
				string stID = GetFirstID();
				if (stID!=null)
				{
					string stParam = "?task=" + Common.Encrypt("stuff",Session.SessionID) + "&id=" + Common.Encrypt(stID,Session.SessionID);	
					Response.Redirect("Default.aspx" + stParam);
				}
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot stuff more than one record. Please select at least one record to stuff.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}


		private bool Activate()
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
				Promo clsPromo = new Promo();
				clsPromo.Activate( stIDs.Substring(0,stIDs.Length-1));
				clsPromo.CommitAndDispose();
			}

			return boRetValue;
		}

		private bool DeActivate()
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
				Promo clsPromo = new Promo();
				clsPromo.DeActivate( stIDs.Substring(0,stIDs.Length-1));
				clsPromo.CommitAndDispose();
			}

			return boRetValue;
		}


		#endregion
		
	}
}
