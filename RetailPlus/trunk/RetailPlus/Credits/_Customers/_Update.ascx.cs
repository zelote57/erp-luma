namespace AceSoft.RetailPlus.Credits._Customers
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
            ContactGroups clsContactGroup = new ContactGroups();

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

            Positions clsPosition = new Positions(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboPosition.DataTextField = "PositionName";
            cboPosition.DataValueField = "PositionID";
            cboPosition.DataSource = clsPosition.ListAsDataTable("PositionName", SortOption.Ascending, 0).DefaultView;
            cboPosition.DataBind();
            cboPosition.SelectedIndex = 0;

            cboCreditCardStatus.Items.Clear();
            foreach (CreditCardStatus selection in Enum.GetValues(typeof(CreditCardStatus)))
            {
                cboCreditCardStatus.Items.Add(new ListItem(selection.ToString("G"), selection.ToString("d")));
            }
            cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(CreditCardStatus.All.ToString("d")));

            Data.CardType clsCardType = new Data.CardType(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboCreditCardType.Items.Clear();
            cboCreditCardType.DataTextField = "CardTypeName";
            cboCreditCardType.DataValueField = "CardTypeID";
            cboCreditCardType.DataSource = clsCardType.ListAsDataTable(new CardTypeDetails(CreditCardTypes.Internal)).DefaultView;
            cboCreditCardType.DataBind();
            cboCreditCardType.SelectedIndex = 0;

            clsContactGroup.CommitAndDispose();
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            Contacts clsContact = new Contacts();
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
            chkIsCreditAllowed.Checked = clsDetails.IsCreditAllowed;
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(clsDetails.DepartmentID.ToString()));
            cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(clsDetails.PositionID.ToString()));

            txtCreditCardNo.Text = clsDetails.CreditDetails.CreditCardNo;
            cboCreditCardType.SelectedIndex = cboCreditCardType.Items.IndexOf(cboCreditCardType.Items.FindByValue(clsDetails.CreditDetails.CardTypeDetails.CardTypeID.ToString()));
            txtCreditAwardDate.Text = clsDetails.CreditDetails.CreditAwardDate.ToString("yyyy-MMM-dd");
            txtExpiryDate.Text = clsDetails.CreditDetails.ExpiryDate.ToString("yyyy-MMM-dd");
            cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(clsDetails.CreditDetails.CreditCardStatus.ToString("d")));
            lblCreditCardActive.Text = clsDetails.CreditDetails.CreditActive ? "Active" : "InActive (Hold/Suspended)";
            txtCreditLimit.Text = clsDetails.CreditLimit.ToString("###0.#0");
            txtCredit.Text = clsDetails.Credit.ToString("###0.#0");
            txtPaidAmount.Text = "0.00";
            txtCurrentBalance.Text = (clsDetails.CreditLimit - clsDetails.Credit).ToString("###0.#0");
            
        }
        private void SaveRecord()
        {
            Contacts clsContact = new Contacts();
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
            clsDetails.IsCreditAllowed = chkIsCreditAllowed.Checked;
            clsDetails.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text);
            clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
            clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

            clsContact.Update(clsDetails);

            clsContact.CommitAndDispose();
        }

        #endregion
    }
}
