namespace AceSoft.RetailPlus.Inventory._WBranchTransfer
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
    /// <summary>
    /// Branch Transfer creation
    /// </summary>
    public partial  class __Insert : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
					LoadOptions();			
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

		}
		#endregion

		#region Web Control Methods

        protected void imgSaveAddItem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Int64 WBranchTransferID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&WBranchTransferID=" + Common.Encrypt(WBranchTransferID.ToString(),Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);
		}

        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
		{
			Int64 WBranchTransferID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&WBranchTransferID=" + Common.Encrypt(WBranchTransferID.ToString(),Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);	
		}

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);	
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
			Response.Redirect("Default.aspx" + stParam);			
		}

        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			
			Response.Redirect(lblReferrer.Text);
		}

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			Branch clsBranch = new Branch();
			cboBranchFrom.DataTextField = "BranchCode";
            cboBranchFrom.DataValueField = "BranchID";
            cboBranchFrom.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranchFrom.DataBind();

            cboBranchTo.DataTextField = "BranchCode";
            cboBranchTo.DataValueField = "BranchID";
            cboBranchTo.DataSource = clsBranch.ListAsDataTable().DefaultView;
            cboBranchTo.DataBind();

            clsBranch.CommitAndDispose();

            try { cboBranchFrom.SelectedIndex = 0; }
            catch { }
            try { cboBranchTo.SelectedIndex = cboBranchTo.Items.Count - 1; }
            catch { }
			NewTransaction();
		}

		private void NewTransaction()
		{
			lblWBranchTransferDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = Convert.ToDateTime(lblWBranchTransferDate.Text).AddHours(12).ToString("yyyy-MM-dd");

			lblWBranchTransferNo.Text = "WILL BE ASSIGNED AFTER SAVING";
		}

		private Int64 SaveRecord()
		{
			WBranchTransfer clsWBranchTransfer = new WBranchTransfer();
			clsWBranchTransfer.GetConnection();
			lblWBranchTransferNo.Text = Constants.BRANCH_TRANSFER_CODE + CompanyDetails.BECompanyCode + DateTime.Now.Year.ToString() + clsWBranchTransfer.LastTransactionNo();

			WBranchTransferDetails clsDetails = new WBranchTransferDetails();

			clsDetails.WBranchTransferNo = lblWBranchTransferNo.Text;
			clsDetails.WBranchTransferDate = Convert.ToDateTime(lblWBranchTransferDate.Text);
            clsDetails.BranchIDFrom = Convert.ToInt16(cboBranchFrom.SelectedItem.Value);
            clsDetails.BranchIDTo = Convert.ToInt16(cboBranchTo.SelectedItem.Value);
            clsDetails.RequiredDeliveryDate = Convert.ToDateTime(txtRequiredDeliveryDate.Text);
			clsDetails.TransferrerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.TransferrerName = Session["Name"].ToString();
            clsDetails.RequestedBy = txtRequestedBy.Text;
			clsDetails.Status = WBranchTransferStatus.Open;
			clsDetails.Remarks = txtRemarks.Text;
			
			Int64 id = clsWBranchTransfer.Insert(clsDetails);
			clsWBranchTransfer.CommitAndDispose();

			return id;
		}


		#endregion

    }
}
