namespace AceSoft.RetailPlus.MasterFiles._ProductGroup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __Insert : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				if (Visible)
					LoadOptions();			
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
        protected void imgAdd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID);
            Response.Redirect(Constants.ROOT_DIRECTORY + "/MasterFiles/_Unit/Default.aspx" + stParam);
		}

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            Data.Unit clsUnit = new Data.Unit();
			
			cboProductGroupUnit.DataTextField = "UnitName";
			cboProductGroupUnit.DataValueField = "UnitID";
			cboProductGroupUnit.DataSource = clsUnit.ListAsDataTable(SortField: "UnitName").DefaultView;
			cboProductGroupUnit.DataBind();
			cboProductGroupUnit.SelectedIndex = cboProductGroupUnit.Items.Count - 1;

            Terminal clsTerminal = new Terminal(clsUnit.Connection, clsUnit.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(Int32.Parse(Session["BranchID"].ToString()), Session["TerminalNo"].ToString());
            txtVAT.Text = clsTerminalDetails.VAT.ToString("###.#0");
            txtEVAT.Text = clsTerminalDetails.EVAT.ToString("###.#0");
            txtLocalTax.Text = clsTerminalDetails.LocalTax.ToString("###.#0");

			clsUnit.CommitAndDispose();
		}
		private Int64 SaveRecord()
		{
			ProductGroupDetails clsDetails = new ProductGroupDetails();

			clsDetails.ProductGroupCode = txtProductGroupCode.Text;
			clsDetails.ProductGroupName = txtProductGroupName.Text;
            clsDetails.UnitDetails = new UnitDetails
            {
                UnitID = Convert.ToInt32(cboProductGroupUnit.SelectedItem.Value)
            };
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked;
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);

			ProductGroup clsProductGroup = new ProductGroup();
			Int64 id = clsProductGroup.Insert(clsDetails);
			clsProductGroup.CommitAndDispose();

			return id;
		}

		#endregion

    }
}
