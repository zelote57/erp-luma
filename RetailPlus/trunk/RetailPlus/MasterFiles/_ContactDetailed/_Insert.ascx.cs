namespace AceSoft.RetailPlus.MasterFiles._ContactDetailed
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Insert : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
					LoadOptions();			
			}
		}

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
            cboDepartment.DataSource = clsDepartment.ListAsDataTable().DefaultView;
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
        private Int64 SaveRecord()
        {
            ContactDetails clsDetails = new ContactDetails();

            ERPConfig clsERPConfig = new ERPConfig();
            BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.CustomerCode_Country_Code, BarcodeHelper.CustomerCode_ManufacturerCode, clsERPConfig.get_LastCustomerCode());
            clsDetails.ContactCode = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
            clsERPConfig.CommitAndDispose();

            clsDetails.ContactName = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text;
            clsDetails.ContactGroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
            clsDetails.ModeOfTerms = ModeOfTerms.Months;
            clsDetails.Terms = 0;
            clsDetails.Address = txtAddress1.Text + " " + txtAddress2.Text + " " + txtCity.Text + " " + txtState.Text + " " + txtZipCode.Text;
            clsDetails.BusinessName = txtBusinessName.Text;
            clsDetails.TelephoneNo = txtBusinessPhoneNo.Text;
            clsDetails.Remarks = txtRemarks.Text;
            clsDetails.Debit = 0;
            clsDetails.Credit = 0;
            clsDetails.IsCreditAllowed = false;
            clsDetails.CreditLimit = 0;
            clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
            clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

            DateTime dteBirthDate = Constants.C_DATE_MIN_VALUE;
            DateTime dteSpouseBirthDate = Constants.C_DATE_MIN_VALUE;
            DateTime dteAnniversaryDate = Constants.C_DATE_MIN_VALUE;

            dteBirthDate = DateTime.TryParse(txtBirthDate.Text, out dteBirthDate) ? dteBirthDate : Constants.C_DATE_MIN_VALUE;
            dteSpouseBirthDate = DateTime.TryParse(txtSpouseBirthDate.Text, out dteSpouseBirthDate) ? dteSpouseBirthDate : Constants.C_DATE_MIN_VALUE;
            dteAnniversaryDate = DateTime.TryParse(txtAnniversaryDate.Text, out dteAnniversaryDate) ? dteAnniversaryDate : Constants.C_DATE_MIN_VALUE;

            ContactAddOnDetails clsAddOnDetails = new ContactAddOnDetails();
            clsAddOnDetails.ContactID = clsDetails.ContactID;
            clsAddOnDetails.Salutation = cboSalutation.SelectedItem.Value;
            clsAddOnDetails.FirstName = txtFirstName.Text;
            clsAddOnDetails.MiddleName = txtMiddleName.Text;
            clsAddOnDetails.LastName = txtLastName.Text;
            clsAddOnDetails.SpouseName = txtSpouseName.Text;
            clsAddOnDetails.BirthDate = dteBirthDate;
            clsAddOnDetails.SpouseBirthDate = dteSpouseBirthDate;
            clsAddOnDetails.AnniversaryDate = dteAnniversaryDate;
            clsAddOnDetails.Address1 = txtAddress1.Text;
            clsAddOnDetails.Address2 = txtAddress2.Text;
            clsAddOnDetails.City = txtCity.Text;
            clsAddOnDetails.State = txtState.Text;
            clsAddOnDetails.ZipCode = txtZipCode.Text;
            clsAddOnDetails.CountryID = int.Parse(cboCountry.SelectedItem.Value);
            clsAddOnDetails.CountryCode = cboCountry.SelectedItem.Text;
            clsAddOnDetails.BusinessPhoneNo = txtBusinessPhoneNo.Text;
            clsAddOnDetails.HomePhoneNo = txtHomePhoneNo.Text;
            clsAddOnDetails.MobileNo = txtMobileNo.Text;
            clsAddOnDetails.FaxNo = txtFaxNo.Text;
            clsAddOnDetails.EmailAddress = txtEmailAddress.Text;

            clsDetails.AdditionalDetails = clsAddOnDetails;

            Contacts clsContact = new Contacts();
            Int64 id = clsContact.Insert(clsDetails);
            clsContact.CommitAndDispose();

            return id;
        }

        #endregion

    }
}
