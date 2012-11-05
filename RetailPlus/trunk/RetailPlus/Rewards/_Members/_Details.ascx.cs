namespace AceSoft.RetailPlus.Rewards._Members
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
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {

        }
        #endregion

        #region Web Control Methods

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, System.EventArgs e)
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
            txtTerms.Text = clsDetails.Terms.ToString("#,##0");
            txtAddress.Text = clsDetails.Address;
            txtBusinessName.Text = clsDetails.BusinessName;
            txtTelephoneNo.Text = clsDetails.TelephoneNo;
            txtRemarks.Text = clsDetails.Remarks;
            txtDebit.Text = clsDetails.Debit.ToString("#,##0.#0");
            txtCredit.Text = clsDetails.Credit.ToString("#,##0.#0");
            if (clsDetails.IsCreditAllowed == 0)
                chkIsCreditAllowed.Checked = false;
            else
                chkIsCreditAllowed.Checked = true;
            txtCreditLimit.Text = clsDetails.CreditLimit.ToString("#,##0.#0");
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(clsDetails.DepartmentID.ToString()));
            cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(clsDetails.PositionID.ToString()));
        }

        #endregion

	}
}
