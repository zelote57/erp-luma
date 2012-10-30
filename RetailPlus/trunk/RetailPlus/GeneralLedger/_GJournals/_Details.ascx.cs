namespace AceSoft.RetailPlus.GeneralLedger._GJournals
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
			this.lstGJournalsDebit.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstGJournalsDebit_ItemDataBound);
			this.lstGJournalsCredit.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.lstGJournalsCredit_ItemDataBound);
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
		private void lstGJournalsDebit_ItemDataBound(object sender, DataListItemEventArgs e)
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
		private void lstGJournalsCredit_ItemDataBound(object sender, DataListItemEventArgs e)
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

		#endregion

		#region Private Methods

		private void LoadOptions()
		{

        }
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["GJournalid"],Session.SessionID));
			GJournals clsGJournals = new GJournals();
			GJournalsDetails clsDetails = clsGJournals.Details(iID);
			clsGJournals.CommitAndDispose();

			lblGJournalID.Text = clsDetails.GJournalID.ToString();
			lblRemarks.Text = clsDetails.Particulars;
			lblTotalDebitAmount.Text = clsDetails.TotalDebitAmount.ToString("#,##0.#0");
			lblTotalCreditAmount.Text = clsDetails.TotalCreditAmount.ToString("#,##0.#0");
			lblTotalAmount.Text = Convert.ToDecimal(clsDetails.TotalDebitAmount - clsDetails.TotalCreditAmount).ToString("#,##0.#0");

			LoadItems();
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

		#endregion

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }
        protected void cmdPrint_Click(object sender, EventArgs e)
        {

        }
}
}
