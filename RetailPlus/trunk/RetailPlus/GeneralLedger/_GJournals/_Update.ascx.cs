namespace AceSoft.RetailPlus.GeneralLedger._GJournals
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
		
		private void InitializeComponent()
		{
			
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
        protected void lstGJournalsDebit_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkListDebit = (HtmlInputCheckBox) e.Item.FindControl("chkListDebit");
				chkListDebit.Value = dr["GJournalDebitID"].ToString();

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
        protected void lstGJournalsCredit_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView) e.Item.DataItem;				

				HtmlInputCheckBox chkListCredit = (HtmlInputCheckBox) e.Item.FindControl("chkListCredit");
				chkListCredit.Value = dr["GJournalCreditID"].ToString();

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
        protected void imgPost_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Postjournal();
        }
        protected void cmdPost_Click(object sender, System.EventArgs e)
        {
            Postjournal();
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			lblGJournalID.Text = "0";
			lblTotalDebitAmount.Text = "0.00";
			lblTotalCreditAmount.Text = "0.00";
			lblTotalAmount.Text = "0.00";

			txtPostingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			LoadAccount();
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["GJournalid"],Session.SessionID));
			GJournals clsGJournals = new GJournals();
			GJournalsDetails clsDetails = clsGJournals.Details(iID);
			clsGJournals.CommitAndDispose();

			lblGJournalID.Text = clsDetails.GJournalID.ToString();
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
			ComputeGJournal(Sender);

			GJournalsDetails clsDetails = new GJournalsDetails();

            clsDetails.GJournalID = Convert.ToInt64(lblGJournalID.Text);
            clsDetails.Particulars = txtRemarks.Text;
            clsDetails.TotalDebitAmount = Convert.ToDecimal(lblTotalDebitAmount.Text);
            clsDetails.TotalCreditAmount = Convert.ToDecimal(lblTotalCreditAmount.Text);

			GJournals clsGJournals = new GJournals();
            clsGJournals.Update(clsDetails);

			clsGJournals.CommitAndDispose();
		}
		private void SaveDebit()
		{
			GJournalsDebitDetails clsDetails = new GJournalsDebitDetails();
			
			clsDetails.GJournalID = Convert.ToInt64(lblGJournalID.Text);
			clsDetails.ChartOfAccountID = Convert.ToInt32(cboAccount.SelectedItem.Value);
			clsDetails.Amount = Convert.ToDecimal(txtAmount.Text);

			GJournalsDebit clsGJournalsDebit = new GJournalsDebit();
			clsGJournalsDebit.Insert(clsDetails);
			clsGJournalsDebit.CommitAndDispose();
		}
		private void SaveCredit()
		{
			GJournalsCreditDetails clsDetails = new GJournalsCreditDetails();
			
			clsDetails.GJournalID = Convert.ToInt64(lblGJournalID.Text);
			clsDetails.ChartOfAccountID = Convert.ToInt32(cboAccount.SelectedItem.Value);
			clsDetails.Amount = Convert.ToDecimal(txtAmount.Text);

			GJournalsCredit clsGJournalsCredit = new GJournalsCredit();
			clsGJournalsCredit.Insert(clsDetails);
			clsGJournalsCredit.CommitAndDispose();
		}
		
		private bool DeleteItems()
		{
			bool boRetValueDebit = false;
			string stIDs = "";

			foreach(DataListItem item in lstGJournalsDebit.Items)
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
				GJournalsDebit clsGJournalsDebit = new GJournalsDebit();
				clsGJournalsDebit.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsGJournalsDebit.CommitAndDispose();
			}

			bool boRetValueCredit = false;
			stIDs = "";

			foreach(DataListItem item in lstGJournalsCredit.Items)
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
				GJournalsCredit clsGJournalsCredit = new GJournalsCredit();
				clsGJournalsCredit.Delete( stIDs.Substring(0,stIDs.Length-1));
				clsGJournalsCredit.CommitAndDispose();
			}

			bool boRetValue = false;
			if (boRetValueDebit)
				return true;
			
			if (boRetValueCredit)
				return true;

			return boRetValue;
		}
		private void ComputeGJournal(string Sender)
		{
			decimal TotalDebitAmount = 0;
			decimal TotalCreditAmount = 0;

			foreach(DataListItem item in lstGJournalsDebit.Items)
			{
				Label lblDebitAmountDebit = (Label) item.FindControl("lblDebitAmountDebit");
				Label lblCreditAmountDebit = (Label) item.FindControl("lblCreditAmountDebit");
				TotalDebitAmount += Convert.ToDecimal(lblDebitAmountDebit.Text);	
				TotalCreditAmount += Convert.ToDecimal(lblCreditAmountDebit.Text);
			}

			foreach(DataListItem item in lstGJournalsCredit.Items)
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
			long GJournalID = Convert.ToInt64(lblGJournalID.Text);
			DataClass clsDataClass = new DataClass();

			GJournalsDebit clsGJournalsDebit = new GJournalsDebit();
			lstGJournalsDebit.DataSource = clsDataClass.DataReaderToDataTable(clsGJournalsDebit.List(GJournalID, "GJournalDebitID", SortOption.Ascending)).DefaultView;
			lstGJournalsDebit.DataBind();

			GJournalsCredit clsGJournalsCredit = new GJournalsCredit(clsGJournalsDebit.Connection, clsGJournalsDebit.Transaction);
			lstGJournalsCredit.DataSource = clsDataClass.DataReaderToDataTable(clsGJournalsCredit.List(GJournalID, "GJournalCreditID", SortOption.Ascending)).DefaultView;
			lstGJournalsCredit.DataBind();
			clsGJournalsDebit.CommitAndDispose();
		}
        private void Postjournal()
        {
            try
            {
                decimal decTotalAmount = Convert.ToDecimal(lblTotalAmount.Text);

                if (decTotalAmount == 0)
                {
                    decimal decTotalDebitAmount = Convert.ToDecimal(lblTotalDebitAmount.Text);

                    DateTime PostingDate = Convert.ToDateTime(txtPostingDate.Text + " " + DateTime.Now.ToString("HH:mm"));
                    long lGJournalID = Convert.ToInt64(lblGJournalID.Text);
                    GJournals clsGJournals = new GJournals();
                    clsGJournals.GetConnection();

                    clsGJournals.Post(lGJournalID, PostingDate);
                    clsGJournals.CommitAndDispose();

                    Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
                }
                else
                {
                    //lblReferrer.Text = "Cannot post journal, the debit/credit amount should be equal. Please check the posted accounts.";
                    string stScript = "<Script>";
                    stScript += "window.alert('Cannot post journal, the debit/credit amount should be equal. Please check the posted accounts.')";
                    stScript += "</Script>";
                    Response.Write(stScript);
                }
            }
            catch (Exception ex) {
                lblReferrer.Text= "'Cannot post journal. unexpected error occurred: " + ex.ToString();
                string stScript = "<Script>";
                stScript += "window.alert('Cannot post journal. unexpected error occurred: " + ex.ToString() + "')" ;
                stScript += "</Script>";
                Response.Write(stScript);
            }
        }

		private string GetFirstID()
		{
			foreach(DataListItem item in lstGJournalsDebit.Items)
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
			
			foreach(DataListItem item in lstGJournalsDebit.Items)
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
