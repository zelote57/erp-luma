namespace AceSoft.RetailPlus.Inventory._StockType
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

        #region Web Form Controls

        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        #endregion

        #region Private methods

        private void LoadOptions()
        {
            cboDirection.Items.Clear();
            foreach (int direction in Enum.GetValues(typeof(StockDirections)))
            {
                cboDirection.Items.Add(new ListItem(Enum.GetName(typeof(StockDirections), direction), direction.ToString()));
            }
            cboDirection.SelectedIndex = 1;
        }
        private void LoadRecord()
        {
            Int16 iID = Convert.ToInt16(Common.Decrypt(Request.QueryString["id"], Session.SessionID));
            StockTypes clsStockType = new StockTypes();
            StockTypesDetails clsDetails = clsStockType.Details(iID);
            clsStockType.CommitAndDispose();

            lblStockTypeID.Text = clsDetails.StockTypeID.ToString();
            txtStockTypeCode.Text = clsDetails.StockTypeCode;
            txtDescription.Text = clsDetails.Description;
            cboDirection.SelectedIndex = cboDirection.Items.IndexOf(cboDirection.Items.FindByValue(clsDetails.StockDirection.ToString("d")));
        }

        #endregion

    }
}
