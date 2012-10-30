namespace AceSoft.RetailPlus.MasterFiles._ProductGroup
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
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
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
        protected void cmdBack_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string stParam = "?task=" + Common.Encrypt("add", Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx" + stParam);
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{

			DataClass clsDataClass = new DataClass();

			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));

			UnitMeasurements clsUnit = new UnitMeasurements();
			
			cboProductGroupUnit.DataTextField = "UnitName";
			cboProductGroupUnit.DataValueField = "UnitID";
			cboProductGroupUnit.DataSource = clsDataClass.DataReaderToDataTable(clsUnit.List("UnitName",SortOption.Ascending)).DefaultView;
			cboProductGroupUnit.DataBind();
			cboProductGroupUnit.SelectedIndex = cboProductGroupUnit.Items.Count - 1;

			clsUnit.CommitAndDispose();		

            cboOrderSlipPrinter.Items.Add(new ListItem(OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G"),OrderSlipPrinter.RetailPlusOSPrinter1.ToString("d")));
            cboOrderSlipPrinter.Items.Add(new ListItem(OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G"),OrderSlipPrinter.RetailPlusOSPrinter2.ToString("d")));
            cboOrderSlipPrinter.Items.Add(new ListItem(OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G"),OrderSlipPrinter.RetailPlusOSPrinter3.ToString("d")));
            cboOrderSlipPrinter.Items.Add(new ListItem(OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G"),OrderSlipPrinter.RetailPlusOSPrinter4.ToString("d")));
            cboOrderSlipPrinter.Items.Add(new ListItem(OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G"),OrderSlipPrinter.RetailPlusOSPrinter5.ToString("d")));
		}
		private void LoadRecord()
		{
			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ProductGroup clsProductGroup = new ProductGroup();
			ProductGroupDetails clsDetails = clsProductGroup.Details(iID);
			clsProductGroup.CommitAndDispose();

			lblProductGroupID.Text = clsDetails.ProductGroupID.ToString();
			txtProductGroupCode.Text = clsDetails.ProductGroupCode;
			txtProductGroupName.Text = clsDetails.ProductGroupName;
			cboProductGroupUnit.SelectedIndex = cboProductGroupUnit.Items.IndexOf( cboProductGroupUnit.Items.FindByText(clsDetails.BaseUnitName));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
			if (clsDetails.IncludeInSubtotalDiscount == 0)
				chkIncludeInSubtotalDiscount.Checked = false;
			else
				chkIncludeInSubtotalDiscount.Checked = true;
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
            cboOrderSlipPrinter.SelectedIndex = cboOrderSlipPrinter.Items.IndexOf(cboOrderSlipPrinter.Items.FindByValue(clsDetails.OrderSlipPrinter.ToString("d")));
		}

		#endregion

    }
}
