namespace AceSoft.RetailPlus.GeneralLedger._Bank
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;

    public partial class __Details : System.Web.UI.UserControl
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
			Bank clsBank = new Bank();
			BankDetails clsDetails = clsBank.Details(iID);
			clsBank.CommitAndDispose();

			lblBankID.Text = clsDetails.BankID.ToString();
            txtBankCode.Text = clsDetails.BankCode;
            txtBankName.Text = clsDetails.BankName;
            txtChequeCode.Text = clsDetails.ChequeCode;
            txtChequeCounter.Text = clsDetails.ChequeCounter;
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
			this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

        protected void imgPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }
        protected void cmdPrint_Click(object sender, EventArgs e)
        {

        }
}
}
