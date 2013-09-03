namespace AceSoft.RetailPlus.SalesAndReceivables._SalesJournals
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadRecord();	
					LoadItems();
				}
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
			this.imgPrint.Click += new System.Web.UI.ImageClickEventHandler(this.imgPrint_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
			this.lstItem.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstItem_ItemDataBound);

		}
		#endregion

		#region Web Control Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));		
		}
		private void lstItem_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkList = (HtmlInputCheckBox) e.Item.FindControl("chkList");
				chkList.Value = dr["SOItemID"].ToString();

				HyperLink lnkDescription = (HyperLink) e.Item.FindControl("lnkDescription");
				lnkDescription.Text = dr["Description"].ToString();
				lnkDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&id=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID);

				HyperLink lnkMatrixDescription = (HyperLink) e.Item.FindControl("lnkMatrixDescription");
				if (dr["MatrixDescription"].ToString() == string.Empty || dr["MatrixDescription"].ToString() == null)
					lnkMatrixDescription.Text = "_";
				else
				{
					lnkMatrixDescription.Text = dr["MatrixDescription"].ToString();
					lnkMatrixDescription.NavigateUrl = Constants.ROOT_DIRECTORY + "/MasterFiles/_Product/_VariationsMatrix/Default.aspx?task=" + Common.Encrypt("det", Session.SessionID) + "&prodid=" + Common.Encrypt(dr["ProductID"].ToString(), Session.SessionID) + "&id=" + Common.Encrypt(dr["VariationMatrixID"].ToString(), Session.SessionID);
				}
				
				Label lblQuantity = (Label) e.Item.FindControl("lblQuantity");
				lblQuantity.Text = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

				Label lblProductUnitID = (Label) e.Item.FindControl("lblProductUnitID");
				lblProductUnitID.Text = dr["ProductUnitID"].ToString();

				Label lblProductUnitCode = (Label) e.Item.FindControl("lblProductUnitCode");
				lblProductUnitCode.Text = dr["ProductUnitCode"].ToString();

				Label lblUnitCost = (Label) e.Item.FindControl("lblUnitCost");
				lblUnitCost.Text = Convert.ToDecimal(dr["UnitCost"].ToString()).ToString("#,##0.#0");

				Label lblDiscount = (Label) e.Item.FindControl("lblDiscount");
				lblDiscount.Text = Convert.ToDecimal(dr["Discount"].ToString()).ToString("#,##0.#0");

                if (Convert.ToBoolean(dr["InPercent"].ToString()))
				{
					Label lblPercent = (Label) e.Item.FindControl("lblPercent");
					lblPercent.Visible = true;
				}

				decimal amount = Convert.ToDecimal(dr["Amount"].ToString()) - Convert.ToDecimal(dr["VAT"].ToString());
				Label lblAmount = (Label) e.Item.FindControl("lblAmount");
				lblAmount.Text = amount.ToString("#,##0.#0");

				Label lblVAT = (Label) e.Item.FindControl("lblVAT");
				lblVAT.Text = Convert.ToDecimal(dr["VAT"].ToString()).ToString("#,##0.#0");

				Label lblEVAT = (Label) e.Item.FindControl("lblEVAT");
				lblEVAT.Text = Convert.ToDecimal(dr["EVAT"].ToString()).ToString("#,##0.#0");

				Label lblLocalTax = (Label) e.Item.FindControl("lblLocalTax");
				lblLocalTax.Text = Convert.ToDecimal(dr["LocalTax"].ToString()).ToString("#,##0.#0");

				Label lblRemarks = (Label) e.Item.FindControl("lblRemarks");
				lblRemarks.Text = dr["Remarks"].ToString();

				//For anchor
				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");

				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}


		#endregion

		#region Private Methods

		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["poid"],Session.SessionID));
			SO clsSO = new SO();
			SODetails clsDetails = clsSO.Details(iID);
			clsSO.CommitAndDispose();

			lblSOID.Text = clsDetails.SOID.ToString();
			lblSONo.Text = clsDetails.SONo;
			lblSODate.Text = clsDetails.SODate.ToString("yyyy-MM-dd HH:mm:ss");
			lblRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
			lblCustomerID.Text = clsDetails.CustomerID.ToString();

			lblCustomerCode.Text = clsDetails.CustomerCode.ToString();
			string stParam = "?task=" + Common.Encrypt("details",Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.CustomerID.ToString(),Session.SessionID);	
			lblCustomerCode.NavigateUrl = "/RetailPlus/SalesAndReceivables/_Vendor/Default.aspx" + stParam;

			lblCustomerContact.Text = clsDetails.CustomerContact;
			lblCustomerTelephoneNo.Text = clsDetails.CustomerTelephoneNo;
			lblTerms.Text = clsDetails.CustomerTerms.ToString("##0");
			switch (clsDetails.CustomerModeOfTerms)
			{
				case 0: 
					lblModeOfterms.Text = "Days"; 
					break;
				case 1:
					lblModeOfterms.Text = "Months"; 
					break;
				case 2:
					lblModeOfterms.Text = "Years"; 
					break;
			}
			lblCustomerAddress.Text = clsDetails.CustomerAddress;
			lblBranchID.Text = clsDetails.BranchID.ToString();
			lblBranchCode.Text = clsDetails.BranchCode;
			lblBranchAddress.Text = clsDetails.BranchAddress;
			lblSORemarks.Text = clsDetails.Remarks;

			lblSOSubTotal.Text = Convert.ToDecimal(clsDetails.SubTotal - clsDetails.VAT).ToString("#,##0.#0");
			lblSOVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			lblSOTotal.Text = clsDetails.SubTotal.ToString("#,##0.#0");
		}

		private void LoadItems()
		{
			SOItem clsSOItem = new SOItem();
			lstItem.DataSource = clsSOItem.ListAsDataTable(Convert.ToInt64(lblSOID.Text)).DefaultView;
			lstItem.DataBind();
			clsSOItem.CommitAndDispose();
		}


		#endregion

		private void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			PrintSO();
		}

		protected void cmdPrint_Click(object sender, System.EventArgs e)
		{
			PrintSO();
		}

		private void PrintSO()
		{
			string stParam = "?task=" + Common.Encrypt("reports",Session.SessionID) + "&target=" + Common.Encrypt("po",Session.SessionID) + "&poid=" + Common.Encrypt(lblSOID.Text,Session.SessionID);	
			Response.Redirect("Default.aspx" + stParam);
		}
	}
}
