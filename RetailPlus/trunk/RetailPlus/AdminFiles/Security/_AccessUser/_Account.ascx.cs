using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessUser
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Account : System.Web.UI.UserControl
	{
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();
					LoadRecord();
				}
			}
		}

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

        private void LoadOptions()
        {
            DataClass clsDataClass = new DataClass();
            Country clsCountry = new Country();

            cboCountry.DataTextField = "CountryName";
            cboCountry.DataValueField = "CountryID";
            cboCountry.DataSource = clsDataClass.DataReaderToDataTable(clsCountry.List("CountryName", SortOption.Ascending)).DefaultView;
            cboCountry.DataBind();
            cboCountry.SelectedIndex = cboCountry.Items.Count - 1;

            clsCountry.CommitAndDispose();

            AccessGroup clsAccessGroup = new AccessGroup();

            cboGroup.DataTextField = "GroupName";
            cboGroup.DataValueField = "GroupID";
            cboGroup.DataSource = clsDataClass.DataReaderToDataTable(clsAccessGroup.List("GroupName", SortOption.Ascending)).DefaultView;
            cboGroup.DataBind();
            cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

            clsAccessGroup.CommitAndDispose();

        }
		private void LoadRecord()
		{

			Int64 iID = Convert.ToInt64(Session["UID"].ToString());
			AccessUser clsAccessUser = new AccessUser();
			AccessUserDetails clsDetails = clsAccessUser.Details(iID);

			clsAccessUser.CommitAndDispose();

			lblUID.Text = clsDetails.UID.ToString();
			txtUserName.Text = clsDetails.UserName;
			txtPassword.Text = clsDetails.Password;
			txtConfirm.Text = clsDetails.Password;
			txtName.Text = clsDetails.Name;

			cboCountry.SelectedIndex = cboCountry.Items.IndexOf(cboCountry.Items.FindByValue(clsDetails.CountryID.ToString()));

			txtAddress1.Text	=	clsDetails.Address1;
			txtAddress2.Text	=	clsDetails.Address2;
			txtCity.Text		=	clsDetails.City;
			txtState.Text		=	clsDetails.State;
			txtOfficePhone.Text	=	clsDetails.OfficePhone;
			txtDirectPhone.Text	=	clsDetails.DirectPhone;
			txtHomePhone.Text	=	clsDetails.HomePhone;
			txtFaxNumber.Text	=	clsDetails.FaxPhone;
			txtMobile.Text		=	clsDetails.MobilePhone;
			txtEmail.Text		=	clsDetails.EmailAddress.ToString();

			cboGroup.SelectedIndex = cboGroup.Items.IndexOf(cboGroup.Items.FindByValue(clsDetails.GroupID.ToString()));

			txtPageSize.Text	=	clsDetails.PageSize.ToString();
}
		private void SaveRecord()
		{
			AccessUser clsAccessUser = new AccessUser();
			AccessUserDetails clsDetails = new AccessUserDetails();

			clsDetails.UID = Convert.ToInt64(lblUID.Text);
			clsDetails.UserName  = txtUserName.Text;
			clsDetails.Password  = txtPassword.Text;
			clsDetails.Name = txtName.Text;
			clsDetails.CountryID = Convert.ToInt32(cboCountry.SelectedItem.Value); 
			clsDetails.Address1  = txtAddress1.Text;
			clsDetails.Address2  = txtAddress2.Text;
			clsDetails.City  = txtCity.Text;
			clsDetails.State = txtState.Text;
			clsDetails.OfficePhone = txtOfficePhone.Text;
			clsDetails.DirectPhone = txtDirectPhone.Text;
			clsDetails.HomePhone = txtHomePhone.Text;
			clsDetails.FaxPhone = txtFaxNumber.Text;
			clsDetails.MobilePhone = txtMobile.Text;
			clsDetails.EmailAddress = txtEmail.Text;
			clsDetails.GroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
			clsDetails.PageSize = Convert.ToInt32(txtPageSize.Text);

			clsAccessUser.Update(clsDetails);
			clsAccessUser.CommitAndDispose();

			AssignUserSession(clsDetails);
		}
		private void AssignUserSession(AccessUserDetails clsDetails)
		{
            Session["AccessUserDetails"] = clsDetails;

//			Session.RemoveAll();
			Session.Add("PageSize",clsDetails.PageSize);
			Session.Add("UID", clsDetails.UID);
			Session.Add("UserName", clsDetails.UserName);
			Session.Add("Password", clsDetails.Password);
			Session.Add("Name",clsDetails.Name);

			Session.Add("CountryID", clsDetails.CountryID);

			Session.Add("Addres1", clsDetails.Address1);
			Session.Add("Addres2", clsDetails.Address2);
			Session.Add("City", clsDetails.City);
			Session.Add("State", clsDetails.State);
			Session.Add("OfficePhone", clsDetails.OfficePhone);
			Session.Add("DirectPhone", clsDetails.DirectPhone);
			Session.Add("HomePhone", clsDetails.HomePhone);
			Session.Add("FaxPhone", clsDetails.FaxPhone);
			Session.Add("MobilePhone", clsDetails.MobilePhone);
			Session.Add("EmailAddress", clsDetails.EmailAddress);
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
