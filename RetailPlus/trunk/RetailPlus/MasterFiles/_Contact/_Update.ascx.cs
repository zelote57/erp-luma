namespace AceSoft.RetailPlus.MasterFiles._Contact
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

        #region Web Control Methods
        
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
		private void InitializeComponent()
		{

		}

		#endregion

        #region Web Control Methods

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
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
            ContactGroup clsContactGroup = new ContactGroup();

            cboGroup.DataTextField = "ContactGroupName";
            cboGroup.DataValueField = "ContactGroupID";
            cboGroup.DataSource = clsContactGroup.ListAsDataTable().DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

            Department clsDepartment = new Department(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboDepartment.DataTextField = "DepartmentName";
            cboDepartment.DataValueField = "DepartmentID";
            cboDepartment.DataSource = clsDepartment.ListAsDataTable("DepartmentName", SortOption.Ascending, 0).DefaultView;
            cboDepartment.DataBind();
            cboDepartment.SelectedIndex = 0;

            Position clsPosition = new Position(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboPosition.DataTextField = "PositionName";
            cboPosition.DataValueField = "PositionID";
            cboPosition.DataSource = clsPosition.ListAsDataTable("PositionName", SortOption.Ascending, 0).DefaultView;
            cboPosition.DataBind();
            cboPosition.SelectedIndex = 0;

            clsContactGroup.CommitAndDispose();
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            Contact clsContact = new Contact();
            ContactDetails clsDetails = clsContact.Details(iID);

            clsContact.CommitAndDispose();

            lblContactID.Text = clsDetails.ContactID.ToString();
            txtContactCode.Text = clsDetails.ContactCode;
            txtContactName.Text = clsDetails.ContactName;
            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(clsDetails.ContactGroupID.ToString()));
            cboModeOfTerms.SelectedIndex = cboModeOfTerms.Items.IndexOf(cboModeOfTerms.Items.FindByValue(clsDetails.ModeOfTerms.ToString("d")));
            txtTerms.Text = clsDetails.Terms.ToString("###0");
            txtAddress.Text = clsDetails.Address;
            txtBusinessName.Text = clsDetails.BusinessName;
            txtTelephoneNo.Text = clsDetails.TelephoneNo;
            txtRemarks.Text = clsDetails.Remarks;
            txtDebit.Text = clsDetails.Debit.ToString("###0.#0");
            txtCredit.Text = clsDetails.Credit.ToString("###0.#0");
            if (clsDetails.IsCreditAllowed == 0)
                chkIsCreditAllowed.Checked = false;
            else
                chkIsCreditAllowed.Checked = true;
            txtCreditLimit.Text = clsDetails.CreditLimit.ToString("###0.#0");
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(clsDetails.DepartmentID.ToString()));
            cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(clsDetails.PositionID.ToString()));
        }
        private void SaveRecord()
		{
			Contact clsContact = new Contact();
			ContactDetails clsDetails = new ContactDetails();

			clsDetails.ContactID = Convert.ToInt32(lblContactID.Text);
			clsDetails.ContactCode = txtContactCode.Text;
			clsDetails.ContactName = txtContactName.Text;
			clsDetails.ContactGroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
            clsDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), cboModeOfTerms.SelectedItem.Value);
			clsDetails.Terms = Convert.ToInt32(txtTerms.Text);
			clsDetails.Address = txtAddress.Text;
			clsDetails.BusinessName = txtBusinessName.Text;
			clsDetails.TelephoneNo = txtTelephoneNo.Text;
			clsDetails.Remarks = txtRemarks.Text;
			clsDetails.Debit = Convert.ToDecimal(txtDebit.Text);
			clsDetails.Credit = Convert.ToDecimal(txtCredit.Text);
			if (chkIsCreditAllowed.Checked == false)
				clsDetails.IsCreditAllowed = 0;
			else
				clsDetails.IsCreditAllowed = 1;
			clsDetails.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text);
            clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
            clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

			clsContact.Update(clsDetails);

			clsContact.CommitAndDispose();
        }

        #endregion

    }
}
