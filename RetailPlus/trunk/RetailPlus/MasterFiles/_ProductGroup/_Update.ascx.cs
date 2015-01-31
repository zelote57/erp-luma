namespace AceSoft.RetailPlus.MasterFiles._ProductGroup
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

			DataClass clsDataClass = new DataClass();

			Int64 iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["id"],Session.SessionID));

            Data.Unit clsUnit = new Data.Unit();
			
			cboProductGroupUnit.DataTextField = "UnitName";
			cboProductGroupUnit.DataValueField = "UnitID";
			cboProductGroupUnit.DataSource = clsUnit.ListAsDataTable().DefaultView;
			cboProductGroupUnit.DataBind();
			cboProductGroupUnit.SelectedIndex = cboProductGroupUnit.Items.Count - 1;

			clsUnit.CommitAndDispose();		

			ProductGroupUnitsMatrix clsUnitMatrix = new ProductGroupUnitsMatrix();
			ProductGroupUnitsMatrixDetails clsUnitDetails = clsUnitMatrix.LastDetails(iID);
			clsUnitMatrix.CommitAndDispose();
			if (clsUnitDetails.BottomUnitName == null)
			{
				cboProductGroupUnit.Enabled = true;
			}
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
			cboProductGroupUnit.SelectedIndex = cboProductGroupUnit.Items.IndexOf( cboProductGroupUnit.Items.FindByText(clsDetails.UnitDetails.UnitName));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
		}
		private void SaveRecord()
		{
			
			ProductGroupDetails clsDetails = new ProductGroupDetails();
			clsDetails.ProductGroupID = Convert.ToInt64(lblProductGroupID.Text);
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
			clsProductGroup.Update(clsDetails);
			clsProductGroup.CommitAndDispose();
		}

		#endregion

    }
}
