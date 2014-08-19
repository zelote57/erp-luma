namespace AceSoft.RetailPlus.Inventory._BranchTransfer
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
            if (!IsPostBack && Visible)
			{
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
			}
		}
		
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{

		}
		#endregion
		
		#region Web Control Methods

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
		}
		private void LoadRecord()
		{
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["BranchTransferID"], Session.SessionID));
            BranchTransfer clsBranchTransfer = new BranchTransfer();
            BranchTransferDetails clsDetails = clsBranchTransfer.Details(iID);
            clsBranchTransfer.CommitAndDispose();

			lblBranchTransferID.Text = clsDetails.BranchTransferID.ToString();
			lblBranchTransferNo.Text = clsDetails.BranchTransferNo;
            lblBranchTransferDate.Text = clsDetails.BranchTransferDate.ToString("yyyy-MM-dd HH:mm:ss");
			txtRequiredDeliveryDate.Text = clsDetails.RequiredDeliveryDate.ToString("yyyy-MM-dd");
            cboBranchFrom.SelectedIndex = cboBranchFrom.Items.IndexOf(cboBranchFrom.Items.FindByValue(clsDetails.BranchIDFrom.ToString()));
            cboBranchTo.SelectedIndex = cboBranchTo.Items.IndexOf(cboBranchTo.Items.FindByValue(clsDetails.BranchIDTo.ToString()));
            txtRequestedBy.Text = clsDetails.RequestedBy;
			txtRemarks.Text = clsDetails.Remarks;
		}
		private void SaveRecord()
		{
            BranchTransferDetails clsDetails = new BranchTransferDetails();

            clsDetails.BranchTransferID = long.Parse(lblBranchTransferID.Text);
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

            BranchTransfer clsBranchTransfer = new BranchTransfer();
            clsBranchTransfer.Update(clsDetails);
            clsBranchTransfer.CommitAndDispose();
		}

		#endregion

    }
}
