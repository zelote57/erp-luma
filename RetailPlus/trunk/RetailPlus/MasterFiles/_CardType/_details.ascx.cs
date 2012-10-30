namespace AceSoft.RetailPlus.MasterFiles._CardType
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
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

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
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
        }

        #endregion
        
    }
}
