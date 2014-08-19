namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup
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
			if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();	
				LoadRecord();	
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
		protected void cboGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboGroup.Items.Count != 0)
			{
				ProductGroup clsProductGroup = new ProductGroup();
				Int32 BaseUnitID= clsProductGroup.Details(Convert.ToInt32(cboGroup.SelectedItem.Value)).UnitDetails.UnitID;
				cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.IndexOf( cboProductSubGroupUnit.Items.FindByValue(BaseUnitID.ToString()));
				clsProductGroup.CommitAndDispose();	
			}
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
			ProductGroup clsProductGroup = new ProductGroup();
			DataClass clsDataClass = new DataClass();
			
			cboGroup.DataTextField = "ProductGroupName";
			cboGroup.DataValueField = "ProductGroupID";
			cboGroup.DataSource = clsProductGroup.ListAsDataTable(SortField:"ProductGroupName").DefaultView;
			cboGroup.DataBind();
			cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

            Data.Unit clsUnit = new Data.Unit();
			
			cboProductSubGroupUnit.DataTextField = "UnitName";
			cboProductSubGroupUnit.DataValueField = "UnitID";
			cboProductSubGroupUnit.DataSource = clsUnit.ListAsDataTable(SortField:"UnitName").DefaultView;
			cboProductSubGroupUnit.DataBind();
			cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.Count - 1;

			clsUnit.CommitAndDispose();		
	
			if (cboGroup.Items.Count != 0)
			{
				int BaseUnitID= clsProductGroup.Details(Convert.ToInt32(cboGroup.SelectedItem.Value)).UnitDetails.UnitID;
				cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.IndexOf( cboProductSubGroupUnit.Items.FindByValue(BaseUnitID.ToString()));
			}
			clsProductGroup.CommitAndDispose();	
		}
		private void LoadRecord()
		{
			Int32 iID = Convert.ToInt32(Common.Decrypt(Request.QueryString["id"],Session.SessionID));
			ProductSubGroup clsProductSubGroup = new ProductSubGroup();
			ProductSubGroupDetails clsDetails = clsProductSubGroup.Details(iID);
			clsProductSubGroup.CommitAndDispose();

			lblProductSubGroupID.Text = clsDetails.ProductSubGroupID.ToString();
			cboGroup.SelectedIndex = cboGroup.Items.IndexOf( cboGroup.Items.FindByValue(clsDetails.ProductGroupID.ToString()));
			txtProductSubGroupCode.Text = clsDetails.ProductSubGroupCode;
			txtProductSubGroupName.Text = clsDetails.ProductSubGroupName;
			cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.IndexOf( cboProductSubGroupUnit.Items.FindByValue(clsDetails.BaseUnitID.ToString()));
			txtProductPrice.Text = clsDetails.Price.ToString("#,##0.#0");
			txtPurchasePrice.Text = clsDetails.PurchasePrice.ToString("#,##0.#0");
            chkIncludeInSubtotalDiscount.Checked = clsDetails.IncludeInSubtotalDiscount;
			txtVAT.Text = clsDetails.VAT.ToString("#,##0.#0");
			txtEVAT.Text = clsDetails.EVAT.ToString("#,##0.#0");
			txtLocalTax.Text = clsDetails.LocalTax.ToString("#,##0.#0");
		}
		private void SaveRecord()
		{
			ProductSubGroupDetails clsDetails = new ProductSubGroupDetails();

			clsDetails.ProductSubGroupID = Convert.ToInt16(lblProductSubGroupID.Text);
			clsDetails.ProductGroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
			clsDetails.ProductSubGroupCode = txtProductSubGroupCode.Text;
			clsDetails.ProductSubGroupName = txtProductSubGroupName.Text;
			clsDetails.BaseUnitID = Convert.ToInt32(cboProductSubGroupUnit.SelectedItem.Value);
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked;
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
			
			ProductSubGroup clsProductSubGroup = new ProductSubGroup();
			clsProductSubGroup.Update(clsDetails);
			clsProductSubGroup.CommitAndDispose();
		}

		#endregion
    }
}
