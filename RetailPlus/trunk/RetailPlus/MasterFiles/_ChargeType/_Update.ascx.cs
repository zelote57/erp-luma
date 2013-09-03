namespace AceSoft.RetailPlus.MasterFiles._ChargeType
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

        #region Web Form Methods
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
				{
					LoadOptions();	
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
		private void InitializeComponent()
		{

		}

		#endregion

        #region Web Form Control

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

        }
        private void SaveRecord()
        {
            ChargeType clsChargeType = new ChargeType();
            ChargeTypeDetails clsDetails = new ChargeTypeDetails();

            clsDetails.ChargeTypeCode = txtChargeTypeCode.Text;
            clsDetails.ChargeType = txtChargeType.Text;
            clsDetails.ChargeAmount = Convert.ToDecimal(txtChargeAmount.Text);
            clsDetails.InPercent = Convert.ToByte(chkInPercent.Checked);
            clsDetails.ChargeTypeID = Convert.ToInt32(lblChargeTypeID.Text);

            clsChargeType.Update(clsDetails);
            clsChargeType.CommitAndDispose();
        }
        private void LoadRecord()
        {
            Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            ChargeType clsChargeType = new ChargeType();
            ChargeTypeDetails clsDetails = clsChargeType.Details(iID);
            clsChargeType.CommitAndDispose();

            lblChargeTypeID.Text = clsDetails.ChargeTypeID.ToString();
            txtChargeTypeCode.Text = clsDetails.ChargeTypeCode;
            txtChargeType.Text = clsDetails.ChargeType;
            txtChargeAmount.Text = clsDetails.ChargeAmount.ToString("#,##0.#0");
            chkInPercent.Checked = Convert.ToBoolean(clsDetails.InPercent);
        }

        #endregion

    }
}
