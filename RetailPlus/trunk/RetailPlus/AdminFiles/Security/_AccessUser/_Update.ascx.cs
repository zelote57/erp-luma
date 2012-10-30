using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Security._AccessUser
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		
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

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
					LoadOptions();
					LoadRecord();
				}
			}
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


		#endregion

		#region Private Methods

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

		private void LoadRecord()
		{

			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
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

			clsDetails.UID = Convert.ToInt32(lblUID.Text);
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
		}


		#endregion
    }
}
