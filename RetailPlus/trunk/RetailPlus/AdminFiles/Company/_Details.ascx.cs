using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus._Company
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer.ToString();
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Private Modifiers

		private void LoadRecord()
		{

			txtCompanyCode.Text =	CompanyDetails.CompanyCode;
			txtCompanyName.Text =	CompanyDetails.CompanyName;
			txtAddress1.Text	=	CompanyDetails.Address1;
			txtAddress2.Text	=	CompanyDetails.Address2;
			txtCity.Text		=	CompanyDetails.City;
			txtState.Text		=	CompanyDetails.State;
			txtZip.Text			=	CompanyDetails.Zip;
			txtOfficePhone.Text	=	CompanyDetails.OfficePhone;
			txtDirectPhone.Text	=	CompanyDetails.DirectPhone;
			txtFaxNumber.Text	=	CompanyDetails.FaxPhone;
			txtMobile.Text		=	CompanyDetails.MobilePhone;
			txtEmail.Text		=	CompanyDetails.EmailAddress;
			txtWebSite.Text		=	CompanyDetails.WebSite;
			txtDateCreated.Text	=	CompanyDetails.DateCreated;
			txtTIN.Text			=	CompanyDetails.TIN;
		}


		#endregion
	}
}
