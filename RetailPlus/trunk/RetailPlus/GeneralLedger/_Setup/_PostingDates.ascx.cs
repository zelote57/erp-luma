namespace AceSoft.RetailPlus.GeneralLedger._Setup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __PostingDates : System.Web.UI.UserControl
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
			ERPConfig clsERPConfig = new ERPConfig();
			ERPConfigDetails clsDetails = clsERPConfig.Details();

			clsERPConfig.CommitAndDispose();

			txtDateFrom.Text = clsDetails.PostingDateFrom.ToString("yyyy-MM-dd");
			txtDateTo.Text = clsDetails.PostingDateTo.ToString("yyyy-MM-dd");
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
			this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		private void SaveRecord()
		{
			
			ERPConfigDetails clsDetails = new ERPConfigDetails();

			clsDetails.PostingDateFrom = Convert.ToDateTime(txtDateFrom.Text);
			clsDetails.PostingDateTo = Convert.ToDateTime(txtDateTo.Text);

			ERPConfig clsERPConfig = new ERPConfig();
			clsERPConfig.UpdatePostingDate(clsDetails.PostingDateFrom, clsDetails.PostingDateTo);

			clsERPConfig.CommitAndDispose();
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
