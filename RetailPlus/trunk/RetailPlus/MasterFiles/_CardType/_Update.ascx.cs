namespace AceSoft.RetailPlus.MasterFiles._CardType
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
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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

        #region Web Control Methods

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
        private void LoadRecord()
        {
            Int16 iID = Convert.ToInt16(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            CardType clsCardType = new CardType();
            CardTypeDetails clsDetails = clsCardType.Details(iID);
            clsCardType.CommitAndDispose();

            lblCardTypeID.Text = clsDetails.CardTypeID.ToString();
            txtCardTypeCode.Text = clsDetails.CardTypeCode;
            txtCardTypeName.Text = clsDetails.CardTypeName;

            lblCreditFinanceCharge.Text = clsDetails.CreditFinanceCharge.ToString("#,##0.#0");
            lblCreditLatePenaltyCharge.Text = clsDetails.CreditLatePenaltyCharge.ToString("#,##0.#0");
            lblCreditMinimumAmountDue.Text = clsDetails.CreditMinimumAmountDue.ToString("#,##0.#0");
            lblCreditMinimumPercentageDue.Text = clsDetails.CreditMinimumPercentageDue.ToString("#,##0.#0");
            chkWithGuarantor.Checked = clsDetails.WithGuarantor;
            chkExemptInTerminalCharge.Checked = clsDetails.ExemptInTerminalCharge;
            lblCreatedOn.Text = clsDetails.CreatedOn.ToString("yyyy-MM-dd hh:mm");
        }
        private void SaveRecord()
        {
            CardType clsCardType = new CardType();
            CardTypeDetails clsDetails = new CardTypeDetails(CreditCardTypes.External);

            clsDetails.CardTypeID = Convert.ToInt16(lblCardTypeID.Text);
            clsDetails.CardTypeCode = txtCardTypeCode.Text;
            clsDetails.CardTypeName = txtCardTypeName.Text;

            clsDetails.CreditFinanceCharge = decimal.Parse(lblCreditFinanceCharge.Text);
            clsDetails.CreditLatePenaltyCharge = decimal.Parse(lblCreditLatePenaltyCharge.Text);
            clsDetails.CreditMinimumAmountDue = decimal.Parse(lblCreditMinimumAmountDue.Text);
            clsDetails.CreditMinimumPercentageDue = decimal.Parse(lblCreditMinimumPercentageDue.Text);
            clsDetails.WithGuarantor = chkWithGuarantor.Checked;
            clsDetails.ExemptInTerminalCharge = chkExemptInTerminalCharge.Checked;

            clsDetails.CreatedOn = DateTime.Parse(lblCreatedOn.Text);
            clsDetails.LastModified = DateTime.Now;

            clsCardType.Update(clsDetails);
            clsCardType.CommitAndDispose();
        }

        #endregion
    }
}
