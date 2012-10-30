namespace AceSoft.RetailPlus.Inventory._StockType
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

        #region Web Form Controls

        protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect("Default.aspx" + stParam);
        }
        protected void cmdSave_Click(object sender, System.EventArgs e)
        {
            SaveRecord();
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
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
        private void SaveRecord()
		{
			StockTypes clsStockType = new StockTypes();
			StockTypesDetails clsDetails = new StockTypesDetails();

			clsDetails.StockTypeID = Convert.ToInt16(lblStockTypeID.Text);
			clsDetails.StockTypeCode = txtStockTypeCode.Text;
			clsDetails.Description = txtDescription.Text;
			clsDetails.StockDirection = (StockDirections) Enum.Parse(typeof(StockDirections), cboDirection.SelectedItem.Value);

			clsStockType.Update(clsDetails);
			clsStockType.CommitAndDispose();
        }

        #endregion

    }
}
