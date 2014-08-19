namespace AceSoft.RetailPlus.PurchasesAndPayables._Vendor
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
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();			
			}
		}

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			ContactGroups clsContactGroup = new ContactGroups();
			
			cboGroup.DataTextField = "ContactGroupName";
			cboGroup.DataValueField = "ContactGroupID";
			cboGroup.DataSource = clsDataClass.DataReaderToDataTable(clsContactGroup.List("ContactGroupName",SortOption.Ascending)).DefaultView;
			cboGroup.DataBind();
			cboGroup.SelectedIndex = ((int) ContactGroupCategory.SUPPLIER) - 1; //cboGroup.Items.Count - 1;

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

			clsContactGroup.CommitAndDispose();			
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		private Int64 SaveRecord()
		{
			
			ContactDetails clsDetails = new ContactDetails();

			clsDetails.ContactCode = txtVendorCode.Text;
			clsDetails.ContactName = txtVendorName.Text;
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

            //if (chkIsCreditAllowed.Checked == false)
                
            //else
            //    clsDetails.IsCreditAllowed = 1;
			clsDetails.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text);
            clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
            clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

			Contacts clsContact = new Contacts();
			Int64 id = clsContact.Insert(clsDetails);
			clsContact.CommitAndDispose();

			return id;
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

    }
}
