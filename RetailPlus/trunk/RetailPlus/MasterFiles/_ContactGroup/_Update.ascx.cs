namespace AceSoft.RetailPlus.MasterFiles._ContactGroup
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

		private void LoadOptions()
		{
						
		}

		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ContactGroup clsContactGroup = new ContactGroup();
			ContactGroupDetails clsDetails = clsContactGroup.Details(iID);
			clsContactGroup.CommitAndDispose();

			lblContactGroupID.Text = clsDetails.ContactGroupID.ToString();
			txtContactGroupCode.Text = clsDetails.ContactGroupCode;
			txtContactGroupName.Text = clsDetails.ContactGroupName;
			cboContactGroupCategory.SelectedIndex = Convert.ToInt16(clsDetails.ContactGroupCategory) - 1;
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

		private void SaveRecord()
		{
			ContactGroupDetails clsDetails = new ContactGroupDetails();

			clsDetails.ContactGroupID = Convert.ToInt16(lblContactGroupID.Text);
			clsDetails.ContactGroupCode = txtContactGroupCode.Text;
			clsDetails.ContactGroupName = txtContactGroupName.Text;
			clsDetails.ContactGroupCategory = (ContactGroupCategory) Enum.Parse(typeof(ContactGroupCategory), cboContactGroupCategory.SelectedItem.Value);
			
			ContactGroup clsContactGroup = new ContactGroup();
			clsContactGroup.Update(clsDetails);
			clsContactGroup.CommitAndDispose();
		}

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

	}
}
