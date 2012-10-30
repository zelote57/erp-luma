namespace AceSoft.RetailPlus.Inventory._BranchTransfer
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
				lblReferrer.Text = Request.UrlReferrer.ToString();
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
			Int64 BranchTransferID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(BranchTransferID.ToString(),Session.SessionID) + "#itemsection";	
			Response.Redirect("Default.aspx" + stParam);
		}

        protected void cmdSaveAddItem_Click(object sender, EventArgs e)
		{
			Int64 BranchTransferID = SaveRecord();
			string stParam = "?task=" + Common.Encrypt("additem",Session.SessionID) + "&BranchTransferID=" + Common.Encrypt(BranchTransferID.ToString(),Session.SessionID) + "#itemsection";	
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
            cboBranchFrom.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboBranchFrom.DataBind();

            cboBranchTo.DataTextField = "BranchCode";
            cboBranchTo.DataValueField = "BranchID";
            cboBranchTo.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
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
			lblBranchTransferDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = Convert.ToDateTime(lblBranchTransferDate.Text).AddHours(12).ToString("yyyy-MM-dd");

			lblBranchTransferNo.Text = "WILL BE ASSIGNED AFTER SAVING";
		}

		private Int64 SaveRecord()
		{
			BranchTransfer clsBranchTransfer = new BranchTransfer();
			clsBranchTransfer.GetConnection();
			lblBranchTransferNo.Text = Constants.BRANCH_TRANSFER_CODE + CompanyDetails.CompanyCode + DateTime.Now.Year.ToString() + clsBranchTransfer.LastTransactionNo();

			BranchTransferDetails clsDetails = new BranchTransferDetails();

			clsDetails.BranchTransferNo = lblBranchTransferNo.Text;
			clsDetails.BranchTransferDate = Convert.ToDateTime(lblBranchTransferDate.Text);
            clsDetails.BranchIDFrom = Convert.ToInt16(cboBranchFrom.SelectedItem.Value);
            clsDetails.BranchIDTo = Convert.ToInt16(cboBranchTo.SelectedItem.Value);
            clsDetails.RequiredDeliveryDate = Convert.ToDateTime(txtRequiredDeliveryDate.Text);
			clsDetails.TransferrerID = Convert.ToInt64(Session["UID"].ToString());
            clsDetails.TransferrerName = Session["Name"].ToString();
            clsDetails.RequestedBy = txtRequestedBy.Text;
			clsDetails.Status = BranchTransferStatus.Open;
			clsDetails.Remarks = txtRemarks.Text;
			
			Int64 id = clsBranchTransfer.Insert(clsDetails);
			clsBranchTransfer.CommitAndDispose();

			return id;
		}


		#endregion

    }
}
