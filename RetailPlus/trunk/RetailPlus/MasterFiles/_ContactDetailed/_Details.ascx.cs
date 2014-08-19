namespace AceSoft.RetailPlus.MasterFiles._ContactDetailed
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
            ContactGroups clsContactGroup = new ContactGroups();

            cboGroup.DataTextField = "ContactGroupName";
            cboGroup.DataValueField = "ContactGroupID";
            cboGroup.DataSource = clsContactGroup.ListAsDataTable().DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = 0; //cboGroup.Items.Count - 1;
            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(ContactGroupCategory.CUSTOMER.ToString("d")));

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

            Salutation clsSalutation = new Salutation(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboSalutation.DataTextField = "SalutationName";
            cboSalutation.DataValueField = "SalutationCode";
            cboSalutation.DataSource = clsSalutation.ListAsDataTable().DefaultView;
            cboSalutation.DataBind();
            cboSalutation.SelectedIndex = 0;
            cboSalutation.SelectedIndex = cboSalutation.Items.IndexOf(cboSalutation.Items.FindByValue("MR"));

            Contacts clsContacts = new Contacts(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboSoldBy.DataTextField = "ContactName";
            cboSoldBy.DataValueField = "ContactCode";
            cboSoldBy.DataSource = clsContacts.AgentsAsDataTable(SortField: "ContactName").DefaultView;
            cboSoldBy.DataBind();
            cboSoldBy.SelectedIndex = 0;

            Security.AccessUser clsAccessUser = new Security.AccessUser(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboConfirmedBy.DataTextField = "Name";
            cboConfirmedBy.DataValueField = "Name";
            cboConfirmedBy.DataSource = clsAccessUser.ListAsDataTable(SortField: "Name").DefaultView;
            cboConfirmedBy.DataBind();
            cboConfirmedBy.SelectedIndex = 0;

            Security.Country clsCountry = new Security.Country(clsContactGroup.Connection, clsContactGroup.Transaction);
            cboCountry.DataTextField = "CountryName";
            cboCountry.DataValueField = "CountryID";
            cboCountry.DataSource = clsCountry.ListAsDataTable().DefaultView;
            cboCountry.DataBind();
            cboCountry.SelectedIndex = 0;

            clsContactGroup.CommitAndDispose();
        }
        private void LoadRecord()
        {
            Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            Contacts clsContact = new Contacts();
            ContactDetails clsDetails = clsContact.Details(iID);

            clsContact.CommitAndDispose();

            lblContactID.Text = clsDetails.ContactID.ToString();
            lblCustomerCode.Text = clsDetails.ContactCode;

            txtFirstName.Text = clsDetails.ContactCode;
            txtLastName.Text = clsDetails.ContactName;
            cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(clsDetails.ContactGroupID.ToString()));
            txtBusinessName.Text = clsDetails.BusinessName;
            txtRemarks.Text = clsDetails.Remarks;
            cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(clsDetails.DepartmentID.ToString()));
            cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(clsDetails.PositionID.ToString()));

            cboSalutation.SelectedIndex = cboSalutation.Items.IndexOf(cboSalutation.Items.FindByValue(clsDetails.AdditionalDetails.Salutation));
            txtFirstName.Text = clsDetails.AdditionalDetails.FirstName;
            txtMiddleName.Text = clsDetails.AdditionalDetails.MiddleName;
            txtLastName.Text = clsDetails.AdditionalDetails.LastName;
            txtSpouseName.Text = clsDetails.AdditionalDetails.SpouseName;
            txtBirthDate.Text = clsDetails.AdditionalDetails.BirthDate == Constants.C_DATE_MIN_VALUE ? "" : clsDetails.AdditionalDetails.BirthDate.ToString("yyyy-MM-dd");
            txtSpouseBirthDate.Text = clsDetails.AdditionalDetails.SpouseBirthDate == Constants.C_DATE_MIN_VALUE ? "" : clsDetails.AdditionalDetails.SpouseBirthDate.ToString("yyyy-MM-dd");
            txtAnniversaryDate.Text = clsDetails.AdditionalDetails.AnniversaryDate == Constants.C_DATE_MIN_VALUE ? "" : clsDetails.AdditionalDetails.AnniversaryDate.ToString("yyyy-MM-dd");
            txtAddress1.Text = clsDetails.AdditionalDetails.Address1;
            txtAddress2.Text = clsDetails.AdditionalDetails.Address2;
            txtCity.Text = clsDetails.AdditionalDetails.City;
            txtState.Text = clsDetails.AdditionalDetails.State;
            txtZipCode.Text = clsDetails.AdditionalDetails.ZipCode;
            cboCountry.SelectedIndex = cboCountry.Items.IndexOf(cboCountry.Items.FindByValue(clsDetails.AdditionalDetails.CountryID.ToString()));
            txtBusinessPhoneNo.Text = clsDetails.AdditionalDetails.BusinessPhoneNo;
            txtHomePhoneNo.Text = clsDetails.AdditionalDetails.HomePhoneNo;
            txtMobileNo.Text = clsDetails.AdditionalDetails.MobileNo;
            txtFaxNo.Text = clsDetails.AdditionalDetails.FaxNo;
            txtEmailAddress.Text = clsDetails.AdditionalDetails.EmailAddress;
        }

        #endregion

	}
}
