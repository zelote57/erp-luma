using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessUser
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Details : System.Web.UI.UserControl
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

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();
			Country clsCountry = new Country();
			
			cboCountry.DataTextField = "CountryName";
			cboCountry.DataValueField = "CountryID";
			cboCountry.DataSource = clsDataClass.DataReaderToDataTable(clsCountry.List("CountryName",SortOption.Ascending)).DefaultView;
			cboCountry.DataBind();
			cboCountry.SelectedIndex = cboCountry.Items.Count - 1;

			clsCountry.CommitAndDispose();

			AccessGroup clsAccessGroup = new AccessGroup();
			
			cboGroup.DataTextField = "GroupName";
			cboGroup.DataValueField = "GroupID";
			cboGroup.DataSource = clsDataClass.DataReaderToDataTable(clsAccessGroup.List("GroupName",SortOption.Ascending)).DefaultView;
			cboGroup.DataBind();
			cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

			clsAccessGroup.CommitAndDispose();

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

		private void LoadRecord()
		{

            long iID = long.Parse(Common.Decrypt(Request.QueryString["id"].ToString(), Session.SessionID));
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

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
}
}
