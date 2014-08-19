namespace AceSoft.RetailPlus.GeneralLedger._Payments
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
        protected void imgDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (DeleteItems())
			{
				LoadItems();
				SaveRecord("delete");
			}
		}
		protected void cmdDelete_Click(object sender, System.EventArgs e)
		{
			if (DeleteItems())
			{
				LoadItems();
				SaveRecord("delete");
			}
		}
        protected void imgClear_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LoadAccount();
		}
		protected void cmdClear_Click(object sender, System.EventArgs e)
		{
			LoadAccount();
		}
        protected void imgSaveDebit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Convert.ToDecimal(txtAmount.Text) != 0)
			{
				SaveRecord("debit");
				SaveDebit();
				LoadItems();
				LoadAccount();
				txtAmount.Text = "0.00";
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot save an account with ZERO amount. Please select enter amount.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		protected void cmdSaveDebit_Click(object sender, System.EventArgs e)
		{
			if (Convert.ToDecimal(txtAmount.Text) != 0)
			{
				SaveRecord("debit");
				SaveDebit();
				LoadItems();
				LoadAccount();
				txtAmount.Text = "0.00";
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot save an account with ZERO amount. Please select enter amount.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
        protected void imgSaveCredit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Convert.ToDecimal(txtAmount.Text) != 0)
			{
				SaveRecord("credit");
				SaveCredit();
				LoadItems();
				LoadAccount();
				txtAmount.Text = "0.00";
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot save an account with ZERO amount. Please select enter amount.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		protected void cmdSaveCredit_Click(object sender, System.EventArgs e)
		{
			if (Convert.ToDecimal(txtAmount.Text) != 0)
			{
				SaveRecord("credit");
				SaveCredit();
				LoadItems();
				LoadAccount();
				txtAmount.Text = "0.00";
			}
			else
			{
				string stScript = "<Script>";
				stScript += "window.alert('Cannot save an account with ZERO amount. Please select enter amount.')";
				stScript += "</Script>";
				Response.Write(stScript);	
			}
		}
		private void lstPaymentsDebit_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkListDebit = (HtmlInputCheckBox) e.Item.FindControl("chkListDebit");
				chkListDebit.Value = dr["PaymentDebitID"].ToString();

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

				HtmlInputCheckBox chkListCredit = (HtmlInputCheckBox) e.Item.FindControl("chkListCredit");
				chkListCredit.Value = dr["PaymentCreditID"].ToString();

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

                HtmlInputCheckBox chkList = (HtmlInputCheckBox)e.Item.FindControl("chkList");
                chkList.Value = dr["POID"].ToString();

                POPaymentStatus POPaymentStatus = (POPaymentStatus)Enum.Parse(typeof(POPaymentStatus), dr["PaymentStatus"].ToString());
                if (POPaymentStatus == POPaymentStatus.ForProcessing || POPaymentStatus == POPaymentStatus.Partially)
                { chkList.Attributes.Add("checked", "checked"); }

                HyperLink lnkPONo = (HyperLink)e.Item.FindControl("lnkPONo");
                lnkPONo.Text = dr["PONo"].ToString();
                Common Common = new Common();
                string stParam = "?task=" + Common.Encrypt("details", Session.SessionID) + "&poid=" + Common.Encrypt(chkList.Value.ToString(), Session.SessionID);
                lnkPONo.NavigateUrl = AceSoft.RetailPlus.Constants.ROOT_DIRECTORY + "/PurchasesAndPayables/_PO/Default.aspx" + stParam;

                Label lblPODate = (Label)e.Item.FindControl("lblPODate");
                lblPODate.Text = Convert.ToDateTime(dr["PODate"].ToString()).ToString("MM-dd-yyyy");

                Label lblDeliveryDate = (Label)e.Item.FindControl("lblDeliveryDate");
                lblDeliveryDate.Text = Convert.ToDateTime(dr["DeliveryDate"].ToString()).ToString("MM-dd-yyyy");

                Label lblSupplierDRNo = (Label)e.Item.FindControl("lblSupplierDRNo");
                lblSupplierDRNo.Text = dr["SupplierDRNo"].ToString();

                Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                lblAmount.Text = Convert.ToDecimal(dr["SubTotal"].ToString()).ToString("#,##0.#0");

                Label lblPaidAmount = (Label)e.Item.FindControl("lblPaidAmount");
                lblPaidAmount.Text = Convert.ToDecimal(dr["PaidAmount"].ToString()).ToString("#,##0.#0");

                Label lblUnpaidAmount = (Label)e.Item.FindControl("lblUnpaidAmount");
                lblUnpaidAmount.Text = Convert.ToDecimal(dr["UnpaidAmount"].ToString()).ToString("#,##0.#0");

                //				//For anchor
                //				HtmlGenericControl divExpCollAsst = (HtmlGenericControl) e.Item.FindControl("divExpCollAsst");
                //
                //				HtmlAnchor anchorDown = (HtmlAnchor) e.Item.FindControl("anchorDown");
                //				anchorDown.HRef = "javascript:ToggleDiv('" +  divExpCollAsst.ClientID + "')";
            }
		}
		protected void cboPayee_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			long payeeid = Convert.ToInt64(cboPayee.SelectedItem.Value);

			Data.Contacts clsContact = new Data.Contacts();
			Data.ContactDetails clsDetails = clsContact.Details(payeeid);
			clsContact.CommitAndDispose();
			
			txtPayeeName.Text = clsDetails.ContactName;

			DataClass clsDataClass = new DataClass();

			PO clsPO = new PO();
			lstPO.DataSource = clsDataClass.DataReaderToDataTable(clsPO.ListForPayment(payeeid, "POID", SortOption.Ascending)).DefaultView;
			lstPO.DataBind();
			clsPO.CommitAndDispose();

            Label lblAmount;
            Label lblPaidAmount;
            Label lblUnpaidAmount;

            decimal decAmount = 0;
            decimal decPaidAmount = 0;
            decimal decUnpaidAmount = 0;

            foreach (DataListItem item in lstPO.Items)
            {
                lblAmount = (Label)item.FindControl("lblAmount");
                lblPaidAmount = (Label)item.FindControl("lblPaidAmount");
                lblUnpaidAmount = (Label)item.FindControl("lblUnpaidAmount");

                decAmount += Convert.ToDecimal(lblAmount.Text);
                decPaidAmount += Convert.ToDecimal(lblPaidAmount.Text);
                decUnpaidAmount += Convert.ToDecimal(lblUnpaidAmount.Text);
            }

            lblPOTotalAmount.Text = decAmount.ToString("#,##0.#0");
            lblPOTotalPaidAmount.Text = decPaidAmount.ToString("#,##0.#0");
            lblPOTotalUnpaidAmount.Text = decUnpaidAmount.ToString("#,##0.#0");
		}
        protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bankid = Convert.ToInt32(cboBank.SelectedItem.Value);

            Banks clsBank = new Banks();
            txtChequeNo.Text = clsBank.getChequeNo(bankid);
            clsBank.CommitAndDispose();
        }
        protected void imgPost_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PostInvoice();
        }
        protected void cmdPost_Click(object sender, System.EventArgs e)
        {
            PostInvoice();
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

            Banks clsBank = new Banks();
            cboBank.DataTextField = "BankCode";
            cboBank.DataValueField = "BankID";
            cboBank.DataSource = clsBank.ListAsDataTable().DefaultView;
            cboBank.DataBind();
            clsBank.CommitAndDispose();
            cboBank.SelectedIndex = 0;
            cboBank_SelectedIndexChanged(null, null);

			Contacts clsContact = new Contacts();
			cboPayee.DataTextField = "ContactCode";
			cboPayee.DataValueField = "ContactID";
			cboPayee.DataSource = clsDataClass.DataReaderToDataTable(clsContact.Suppliers(null, 0, "ContactName", SortOption.Ascending)).DefaultView;
			cboPayee.DataBind();
			clsContact.CommitAndDispose();
			cboPayee.SelectedIndex = 0;

			txtChequeDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			lblPaymentID.Text = "0";
			lblTotalDebitAmount.Text = "0.00";
			lblTotalCreditAmount.Text = "0.00";
			lblTotalAmount.Text = "0.00";

			txtPostingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			LoadAccount();
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["paymentid"],Session.SessionID));
			Payments clsPayments = new Payments();
			PaymentsDetails clsDetails = clsPayments.Details(iID);
			clsPayments.CommitAndDispose();

			lblPaymentID.Text = clsDetails.PaymentID.ToString();
            cboBank.SelectedIndex = cboBank.Items.IndexOf(cboBank.Items.FindByValue(clsDetails.BankID.ToString()));
			txtChequeNo.Text = clsDetails.ChequeNo;
			txtChequeDate.Text = clsDetails.ChequeDate.ToString("yyyy-MM-dd");
			cboPayee.SelectedIndex = cboPayee.Items.IndexOf(cboPayee.Items.FindByValue(clsDetails.PayeeID.ToString()));
            cboPayee_SelectedIndexChanged(null, null);
			txtPayeeName.Text = clsDetails.PayeeName;
			txtRemarks.Text = clsDetails.Particulars;
			lblTotalDebitAmount.Text = clsDetails.TotalDebitAmount.ToString("#,##0.#0");
			lblTotalCreditAmount.Text = clsDetails.TotalCreditAmount.ToString("#,##0.#0");
			lblTotalAmount.Text = Convert.ToDecimal(clsDetails.TotalDebitAmount - clsDetails.TotalCreditAmount).ToString("#,##0.#0");

			LoadItems();
		}
		private void LoadAccount()
		{
			DataClass clsDataClass = new DataClass();

			ChartOfAccounts clsChartOfAccount = new ChartOfAccounts();
			cboAccount.DataTextField = "ChartOfAccountName";
			cboAccount.DataValueField = "ChartOfAccountID";
			cboAccount.DataSource = clsDataClass.DataReaderToDataTable(clsChartOfAccount.List("ChartOfAccountName", SortOption.Ascending)).DefaultView;
			cboAccount.DataBind();
			clsChartOfAccount.CommitAndDispose();
			cboAccount.SelectedIndex = 0;

			txtAmount.Text = "0.00";
		}

		private void SaveRecord(string Sender)
		{
			ComputePayment(Sender);

			PaymentsDetails clsDetails = new PaymentsDetails();

            clsDetails.PaymentID = Convert.ToInt64(lblPaymentID.Text);
            clsDetails.BankID = Convert.ToInt32(cboBank.SelectedItem.Value);
            clsDetails.BankCode = cboBank.SelectedItem.Text;
            clsDetails.ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
            clsDetails.ChequeNo = txtChequeNo.Text;
            clsDetails.PayeeID = Convert.ToInt64(cboPayee.SelectedItem.Value);
            clsDetails.PayeeCode = cboPayee.SelectedItem.Text;
            clsDetails.PayeeName = txtPayeeName.Text;
            clsDetails.Particulars = txtRemarks.Text;
            clsDetails.TotalDebitAmount = Convert.ToDecimal(lblTotalDebitAmount.Text);
            clsDetails.TotalCreditAmount = Convert.ToDecimal(lblTotalCreditAmount.Text);

			Payments clsPayments = new Payments();
            clsPayments.Update(clsDetails);

            Banks clsBank = new Banks(clsPayments.Connection, clsPayments.Transaction);
            clsBank.UpdateChequeCounter(clsDetails.BankID, clsDetails.ChequeNo);

			clsPayments.CommitAndDispose();
		}
		private void SaveDebit()
		{
			PaymentsDebitDetails clsDetails = new PaymentsDebitDetails();
			
			clsDetails.PaymentID = Convert.ToInt64(lblPaymentID.Text);
			clsDetails.ChartOfAccountID = Convert.ToInt32(cboAccount.SelectedItem.Value);
			clsDetails.Amount = Convert.ToDecimal(txtAmount.Text);

			PaymentsDebit clsPaymentsDebit = new PaymentsDebit();
			clsPaymentsDebit.Insert(clsDetails);
			clsPaymentsDebit.CommitAndDispose();
		}
		private void SaveCredit()
		{
			PaymentsCreditDetails clsDetails = new PaymentsCreditDetails();
			
			clsDetails.PaymentID = Convert.ToInt64(lblPaymentID.Text);
			clsDetails.ChartOfAccountID = Convert.ToInt32(cboAccount.SelectedItem.Value);
			clsDetails.Amount = Convert.ToDecimal(txtAmount.Text);

			PaymentsCredit clsPaymentsCredit = new PaymentsCredit();
			clsPaymentsCredit.Insert(clsDetails);
			clsPaymentsCredit.CommitAndDispose();
		}
        private void SavePO()
        {
            bool boRetValuePO = false;
            string stIDs = "";

            foreach (DataListItem item in lstPO.Items)
            {
                HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                if (chkList != null)
                {
                    if (chkList.Checked == true)
                    {
                        stIDs += chkList.Value + ",";
                        boRetValuePO = true;
                    }
                }
            }
            if (boRetValuePO)
            {
                PO clsPO = new PO();
                clsPO.UpdatePaymentStatus(POPaymentStatus.ForProcessing, stIDs.Substring(0, stIDs.Length - 1));
                clsPO.CommitAndDispose();
            }
        }
		
		private bool DeleteItems()
		{
			bool boRetValueDebit = false;
			string stIDs = "";

			foreach(DataListItem item in lstPaymentsDebit.Items)
			{
				HtmlInputCheckBox chkListDebit = (HtmlInputCheckBox) item.FindControl("chkListDebit");
				if (chkListDebit!=null)
				{
					if (chkListDebit.Checked == true)
					{
						stIDs += chkListDebit.Value + ",";		
						boRetValueDebit = true;
					}
				}
			}
			if (boRetValueDebit)
			{
				PaymentsDebit clsPaymentsDebit = new PaymentsDebit();
				clsPaymentsDebit.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsPaymentsDebit.CommitAndDispose();
			}

			bool boRetValueCredit = false;
			stIDs = "";

			foreach(DataListItem item in lstPaymentsCredit.Items)
			{
				HtmlInputCheckBox chkListCredit = (HtmlInputCheckBox) item.FindControl("chkListCredit");
				if (chkListCredit!=null)
				{
					if (chkListCredit.Checked == true)
					{
						stIDs += chkListCredit.Value + ",";		
						boRetValueCredit = true;
					}
				}
			}
			if (boRetValueCredit)
			{
				PaymentsCredit clsPaymentsCredit = new PaymentsCredit();
				clsPaymentsCredit.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsPaymentsCredit.CommitAndDispose();
			}

			bool boRetValue = false;
			if (boRetValueDebit)
				return true;
			
			if (boRetValueCredit)
				return true;

			return boRetValue;
		}
		private void ComputePayment(string Sender)
		{
			decimal TotalDebitAmount = 0;
			decimal TotalCreditAmount = 0;

			foreach(DataListItem item in lstPaymentsDebit.Items)
			{
				Label lblDebitAmountDebit = (Label) item.FindControl("lblDebitAmountDebit");
				Label lblCreditAmountDebit = (Label) item.FindControl("lblCreditAmountDebit");
				TotalDebitAmount += Convert.ToDecimal(lblDebitAmountDebit.Text);	
				TotalCreditAmount += Convert.ToDecimal(lblCreditAmountDebit.Text);
			}

			foreach(DataListItem item in lstPaymentsCredit.Items)
			{
				Label lblDebitAmountCredit = (Label) item.FindControl("lblDebitAmountCredit");
				Label lblCreditAmountCredit = (Label) item.FindControl("lblCreditAmountCredit");
				TotalDebitAmount += Convert.ToDecimal(lblDebitAmountCredit.Text);	
				TotalCreditAmount += Convert.ToDecimal(lblCreditAmountCredit.Text);
			}

			switch(Sender)
			{
				case "credit":
					TotalCreditAmount += Convert.ToDecimal(txtAmount.Text);
					break;
				case "debit":
					TotalDebitAmount += Convert.ToDecimal(txtAmount.Text);
					break;
				default:
					break;
			}
			lblTotalDebitAmount.Text = TotalDebitAmount.ToString("#,##0.#0");
			lblTotalCreditAmount.Text = TotalCreditAmount.ToString("#,##0.#0");
			lblTotalAmount.Text = Convert.ToDecimal(TotalDebitAmount - TotalCreditAmount).ToString("#,##0.#0");
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
        private void PostInvoice()
        {
            try
            {
                decimal decTotalAmount = Convert.ToDecimal(lblTotalAmount.Text);

                if (decTotalAmount == 0)
                {
                    decimal decTotalDebitAmount = Convert.ToDecimal(lblTotalDebitAmount.Text);

                    foreach (DataListItem item in lstPO.Items)
                    {
                        HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                        if (chkList != null)
                        {
                            if (chkList.Checked == true)
                            {
                                Label lblUnpaidAmount = (Label)item.FindControl("lblUnpaidAmount");
                                decimal decUnpaidAmount = Convert.ToDecimal(lblUnpaidAmount.Text);

                                if (decTotalDebitAmount == 0)
                                { break; }
                                else if (decTotalDebitAmount >= decUnpaidAmount)
                                { decTotalDebitAmount -= decUnpaidAmount; }
                                else if (decTotalDebitAmount != 0)
                                { decTotalDebitAmount = 0; break; }
                            }
                        }
                    }
                    if (decTotalDebitAmount > 0)
                    {
                        lblReferrer.Text = "Warning!!! Cannot save the transaction, the Total Amount is greater than the payable amount.";
                        string stScript = "<Script>";
                        stScript += "window.alert('Warning!!! Cannot save the transaction, the Total Amount is greater than the payable amount.\n Check more Purchase Order(s) to be paid or decrease the Total debit/credit Amount.')";
                        stScript += "</Script>";
                        Response.Write(stScript);
                    }
                    else
                    {
                        decTotalDebitAmount = Convert.ToDecimal(lblTotalDebitAmount.Text);
                        DateTime PostingDate = Convert.ToDateTime(txtPostingDate.Text + " " + DateTime.Now.ToString("HH:mm"));
                        long lPaymentID = Convert.ToInt64(lblPaymentID.Text);
                        Payments clsPayments = new Payments();
                        clsPayments.GetConnection();

                        PaymentPODetailDetails clsPaymentPODetailDetails;

                        PaymentPODetails clsPaymentPODetails = new PaymentPODetails(clsPayments.Connection, clsPayments.Transaction);
                        PO clsPO = new PO(clsPayments.Connection, clsPayments.Transaction);

                        foreach (DataListItem item in lstPO.Items)
                        {
                            HtmlInputCheckBox chkList = (HtmlInputCheckBox)item.FindControl("chkList");
                            if (chkList != null)
                            {
                                 if (chkList.Checked == true)
                                {
                                    long lPOID = Convert.ToInt64(chkList.Value);
                                    clsPaymentPODetailDetails = new PaymentPODetailDetails();
                                    clsPaymentPODetailDetails.PaymentID = lPaymentID;
                                    clsPaymentPODetailDetails.POID = lPOID;

                                    Label lblUnpaidAmount = (Label)item.FindControl("lblUnpaidAmount");
                                    decimal decUnpaidAmount = Convert.ToDecimal(lblUnpaidAmount.Text);

                                    if (decTotalDebitAmount == 0)
                                    { break; }
                                    else if (decTotalDebitAmount >= decUnpaidAmount)
                                    {
                                        clsPO.UpdatePayment(lPOID, decUnpaidAmount, POPaymentStatus.FullyPaid);

                                        clsPaymentPODetailDetails.Amount = decUnpaidAmount;
                                        clsPaymentPODetailDetails.PaymentStatus = POPaymentStatus.FullyPaid;
                                        clsPaymentPODetails.Insert(clsPaymentPODetailDetails);

                                        decTotalDebitAmount -= decUnpaidAmount;
                                    }
                                    else if (decTotalDebitAmount != 0)
                                    {
                                        clsPO.UpdatePayment(lPOID, decTotalDebitAmount, POPaymentStatus.Partially);

                                        clsPaymentPODetailDetails.Amount = decTotalDebitAmount;
                                        clsPaymentPODetailDetails.PaymentStatus = POPaymentStatus.Partially;
                                        clsPaymentPODetails.Insert(clsPaymentPODetailDetails);

                                        decTotalDebitAmount = 0;
                                    }

                                }
                            }
                        }

                        clsPayments.Post(lPaymentID, PostingDate);
                        clsPayments.CommitAndDispose();

                        Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
                    }
                }
                else
                {
                    //lblReferrer.Text = "Cannot post invoice, the debit/credit amount should be equal. Please check the posted accounts.";
                    string stScript = "<Script>";
                    stScript += "window.alert('Cannot post invoice, the debit/credit amount should be equal. Please check the posted accounts.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                }
            }
            catch (Exception ex) {
                lblReferrer.Text= "'Cannot post invoice. unexpected error occurred: " + ex.ToString();
                string stScript = "<Script>";
                stScript += "window.alert('Cannot post invoice. unexpected error occurred: " + ex.ToString() + "')" ;
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }

		private string GetFirstID()
		{
			foreach(DataListItem item in lstPaymentsDebit.Items)
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
			
			foreach(DataListItem item in lstPaymentsDebit.Items)
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
