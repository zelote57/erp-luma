namespace AceSoft.RetailPlus.GeneralLedger._Payments
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
				lblReferrer.Text = Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadOptions();
					LoadRecord();
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
			this.lstPO.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstPO_ItemDataBound);
			this.lstPaymentsDebit.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstPaymentsDebit_ItemDataBound);
			this.lstPaymentsCredit.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstPaymentsCredit_ItemDataBound);
		}
		#endregion

		#region Web Control Methods

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Default.aspx?task=" + Common.Encrypt("list",Session.SessionID));
		}
		private void lstPaymentsDebit_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				Label lblChartOfAccountCodeDebit = (Label) e.Item.FindControl("lblChartOfAccountCodeDebit");
				lblChartOfAccountCodeDebit.Text = dr["ChartOfAccountCode"].ToString();

				Label lblChartOfAccountNameDebit = (Label) e.Item.FindControl("lblChartOfAccountNameDebit");
				lblChartOfAccountNameDebit.Text = dr["ChartOfAccountName"].ToString();

				Label lblDebitAmountDebit = (Label) e.Item.FindControl("lblDebitAmountDebit");
				lblDebitAmountDebit.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

				Label lblCreditAmountDebit = (Label) e.Item.FindControl("lblCreditAmountDebit");
				lblCreditAmountDebit.Text = "0.00";

//				//For anchor
//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
//
//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
		private void lstPaymentsCredit_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				Label lblChartOfAccountCodeCredit = (Label) e.Item.FindControl("lblChartOfAccountCodeCredit");
				lblChartOfAccountCodeCredit.Text = dr["ChartOfAccountCode"].ToString();

				Label lblChartOfAccountNameCredit = (Label) e.Item.FindControl("lblChartOfAccountNameCredit");
				lblChartOfAccountNameCredit.Text = dr["ChartOfAccountName"].ToString();

				Label lblDebitAmountCredit = (Label) e.Item.FindControl("lblDebitAmountCredit");
				lblDebitAmountCredit.Text = "0.00";

				Label lblCreditAmountCredit = (Label) e.Item.FindControl("lblCreditAmountCredit");
				lblCreditAmountCredit.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

				//				//For anchor
				//				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
				//
				//				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
				//				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
			}
		}
		private void lstPO_ItemDataBound(object sender, DataListItemEventArgs e)
		{
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                HyperLink lnkPONo = (HyperLink)e.Item.FindControl("lnkPONo");
                lnkPONo.Text = dr["PONo"].ToString();
                Common Common = new Common();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(dr["POID"].ToString(), Session.SessionID);
                lnkPONo.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx" + stParam;

                Label lblPODate = (Label)e.Item.FindControl("lblPODate");
                lblPODate.Text = Convert.ToDateTime(dr["PODate"].ToString()).ToString("MM-dd-yyyy");

                Label lblDeliveryDate = (Label)e.Item.FindControl("lblDeliveryDate");
                lblDeliveryDate.Text = Convert.ToDateTime(dr["DeliveryDate"].ToString()).ToString("MM-dd-yyyy");

                Label lblSupplierDRNo = (Label)e.Item.FindControl("lblSupplierDRNo");
                lblSupplierDRNo.Text = dr["SupplierDRNo"].ToString();

                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

                //				//For anchor
                //				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
                //
                //				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
            }
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{

        }
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["paymentid"],Session.SessionID));
			Payments clsPayments = new Payments();
			PaymentsDetails clsDetails = clsPayments.Details(iID);
			clsPayments.CommitAndDispose();

			lblPaymentID.Text = clsDetails.PaymentID.ToString();
            lblBank.Text = clsDetails.BankCode;
            lblBank.NavigateUrl = Constants.ROOT_DIRECTORY + "/GeneralLedger/_Bank/Default.aspx?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.BankID.ToString(), Session.SessionID);

			lblChequeNo.Text = clsDetails.ChequeNo;
			lblChequeDate.Text = clsDetails.ChequeDate.ToString("yyyy-MM-dd");
            lblPayeeCode.Text = clsDetails.PayeeCode;

            string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&id=" + Common.Encrypt(clsDetails.PayeeID.ToString(), Session.SessionID);
            lblPayeeCode.NavigateUrl = Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_Vendor/Default.aspx" + stParam;
            lblPayeeName.Text = clsDetails.PayeeName;
			
			lblRemarks.Text = clsDetails.Particulars;
			lblTotalDebitAmount.Text = clsDetails.TotalDebitAmount.ToString("#,##0.#0");
			lblTotalCreditAmount.Text = clsDetails.TotalCreditAmount.ToString("#,##0.#0");
			lblTotalAmount.Text = Convert.ToDecimal(clsDetails.TotalDebitAmount - clsDetails.TotalCreditAmount).ToString("#,##0.#0");

            LoadPO();
			LoadItems();
		}
		private void LoadAccount()
		{

        }
		
        private void LoadPO()
        {
            long PaymentID = Convert.ToInt64(lblPaymentID.Text);

            DataClass clsDataClass = new DataClass();

            PaymentPODetails clsPaymentPODetails = new PaymentPODetails();
            lstPO.DataSource = clsDataClass.DataReaderToDataTable(clsPaymentPODetails.List(PaymentID, "PaymentDetailID", SortOption.Ascending)).DefaultView;
            lstPO.DataBind();
            clsPaymentPODetails.CommitAndDispose();

            Label lblAmount;

            decimal decAmount = 0;

            foreach (DataListItem item in lstPO.Items)
            {
                lblAmount = (Label)item.FindControl("lblAmount");
                decAmount += Convert.ToDecimal(lblAmount.Text);
            }

            lblPOTotalAmount.Text = decAmount.ToString("#,##0.#0");
        }
		private void LoadItems()
		{
			long PaymentID = Convert.ToInt64(lblPaymentID.Text);
			DataClass clsDataClass = new DataClass();

			PaymentsDebit clsPaymentsDebit = new PaymentsDebit();
			lstPaymentsDebit.DataSource = clsDataClass.DataReaderToDataTable(clsPaymentsDebit.List(PaymentID, "PaymentDebitID", SortOption.Ascending)).DefaultView;
			lstPaymentsDebit.DataBind();

			PaymentsCredit clsPaymentsCredit = new PaymentsCredit(clsPaymentsDebit.Connection, clsPaymentsDebit.Transaction);
			lstPaymentsCredit.DataSource = clsDataClass.DataReaderToDataTable(clsPaymentsCredit.List(PaymentID, "PaymentCreditID", SortOption.Ascending)).DefaultView;
			lstPaymentsCredit.DataBind();
			clsPaymentsDebit.CommitAndDispose();
		}

		#endregion

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }
        protected void cmdPrint_Click(object sender, EventArgs e)
        {

        }
}
}
