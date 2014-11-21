namespace AceSoft.RetailPlus.Credits._Guarantors
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
            cboGroup.DataSource = clsContactGroup.ListAsDataTable(ContactGroupCategory.CUSTOMER).DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = 0; //cboGroup.Items.Count - 1;

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

            clsContactGroup.CommitAndDispose();
        }
        private Int64 SaveRecord()
        {

            ContactDetails clsDetails = new ContactDetails();

            clsDetails.ContactCode = txtContactCode.Text;
            clsDetails.ContactName = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text;
            clsDetails.ContactGroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
            clsDetails.ModeOfTerms = ModeOfTerms.Days;
            clsDetails.Terms = Convert.ToInt32("0");
            clsDetails.Address = txtAddress.Text;
            clsDetails.BusinessName = txtBusinessName.Text;
            clsDetails.TelephoneNo = txtTelephoneNo.Text;
            clsDetails.Remarks = txtRemarks.Text;
            clsDetails.Debit = Convert.ToDecimal("0");
            clsDetails.Credit = Convert.ToDecimal("0");
            clsDetails.IsCreditAllowed = true;
            clsDetails.CreditLimit = Convert.ToDecimal(0);
            clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
            clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

            ContactAddOnDetails clsAddOnDetails = new ContactAddOnDetails();
            clsAddOnDetails.ContactID = clsDetails.ContactID;
            clsAddOnDetails.Salutation = cboSalutation.SelectedItem.Value;
            clsAddOnDetails.FirstName = txtFirstName.Text;
            clsAddOnDetails.MiddleName = txtMiddleName.Text;
            clsAddOnDetails.LastName = txtLastName.Text;
            clsAddOnDetails.SpouseName = "";
            DateTime dteBirthDate = Constants.C_DATE_MIN_VALUE;
            dteBirthDate = DateTime.TryParse(txtBirthDate.Text, out dteBirthDate) ? dteBirthDate : Constants.C_DATE_MIN_VALUE;
            clsAddOnDetails.BirthDate = dteBirthDate;
            clsAddOnDetails.SpouseBirthDate = Constants.C_DATE_MIN_VALUE;
            clsAddOnDetails.AnniversaryDate = Constants.C_DATE_MIN_VALUE;
            clsAddOnDetails.Address1 = txtAddress.Text;
            clsAddOnDetails.Address2 = string.Empty;
            clsAddOnDetails.City = string.Empty;
            clsAddOnDetails.State = string.Empty;
            clsAddOnDetails.ZipCode = string.Empty;
            clsAddOnDetails.CountryID = Constants.C_DEF_COUNTRY_ID;
            clsAddOnDetails.CountryCode = Constants.C_DEF_COUNTRY_CODE;
            clsAddOnDetails.BusinessPhoneNo = txtTelephoneNo.Text;
            clsAddOnDetails.HomePhoneNo = string.Empty;
            clsAddOnDetails.MobileNo = txtMobileNo.Text;
            clsAddOnDetails.FaxNo = string.Empty;
            clsAddOnDetails.EmailAddress = string.Empty;

            clsDetails.AdditionalDetails = clsAddOnDetails;

            Contacts clsContact = new Contacts();
            Int64 id = clsContact.Insert(clsDetails);
            clsContact.CommitAndDispose();

            return id;
        }

        #endregion

    }
}
