namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup
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
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
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
		protected void cboGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboGroup.Items.Count != 0)
			{
				ProductGroup clsProductGroup = new ProductGroup();
				ProductGroupDetails clsProductGroupDetails = clsProductGroup.Details(Convert.ToInt32(cboGroup.SelectedItem.Value));
				cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.IndexOf( cboProductSubGroupUnit.Items.FindByValue(clsProductGroupDetails.UnitDetails.UnitID.ToString()));
				txtProductPrice.Text = clsProductGroupDetails.Price.ToString("#,##0.#0");
				txtPurchasePrice.Text = clsProductGroupDetails.PurchasePrice.ToString("#,##0.#0");
                chkIncludeInSubtotalDiscount.Checked = clsProductGroupDetails.IncludeInSubtotalDiscount;
				txtVAT.Text = clsProductGroupDetails.VAT.ToString("#,##0.#0");
				txtEVAT.Text = clsProductGroupDetails.EVAT.ToString("#,##0.#0");
				txtLocalTax.Text = clsProductGroupDetails.LocalTax.ToString("#,##0.#0");
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
			cboGroup_SelectedIndexChanged(null, System.EventArgs.Empty);

            Data.Unit clsUnit = new Data.Unit();
			
			cboProductSubGroupUnit.DataTextField = "UnitName";
			cboProductSubGroupUnit.DataValueField = "UnitID";
			cboProductSubGroupUnit.DataSource = clsUnit.ListAsDataTable(SortField:"UnitName").DefaultView;
			cboProductSubGroupUnit.DataBind();
			cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.Count - 1;

            Terminal clsTerminal = new Terminal(clsUnit.Connection, clsUnit.Transaction);
            TerminalDetails clsTerminalDetails = clsTerminal.Details(Int32.Parse(Session["BranchID"].ToString()), Session["TerminalNo"].ToString());
            txtVAT.Text = clsTerminalDetails.VAT.ToString("###.#0");
            txtEVAT.Text = clsTerminalDetails.EVAT.ToString("###.#0");
            txtLocalTax.Text = clsTerminalDetails.LocalTax.ToString("###.#0");

			clsUnit.CommitAndDispose();

			if (cboGroup.Items.Count != 0)
			{
				Int32 BaseUnitID= clsProductGroup.Details(Convert.ToInt32(cboGroup.SelectedItem.Value)).UnitDetails.UnitID;
				cboProductSubGroupUnit.SelectedIndex = cboProductSubGroupUnit.Items.IndexOf( cboProductSubGroupUnit.Items.FindByValue(BaseUnitID.ToString()));
			}
			clsProductGroup.CommitAndDispose();	
		}
		private Int64 SaveRecord()
		{
			ProductSubGroupDetails clsDetails = new ProductSubGroupDetails();

			clsDetails.ProductGroupID = Convert.ToInt32(cboGroup.SelectedItem.Value);
			clsDetails.ProductSubGroupCode = txtProductSubGroupCode.Text;
			clsDetails.ProductSubGroupName = txtProductSubGroupName.Text;
			clsDetails.BaseUnitID = Convert.ToInt32(cboProductSubGroupUnit.SelectedItem.Value);
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
			clsDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(chkIncludeInSubtotalDiscount.Checked);
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text);
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text);
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text);
            clsDetails.SequenceNo = Int32.Parse(txtSequenceNo.Text);
            clsDetails.ImagePath = lblImagePath.Text;

            string strfile = "";
            string strfolder = "/RetailPlus/temp/uploaded/subgroupimages/";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile pfile = Request.Files[i];
                if (pfile.ContentLength > 0)
                {
                    strfile = Server.MapPath(strfolder + System.IO.Path.GetFileName(pfile.FileName));

                    if (!System.IO.Directory.Exists(Server.MapPath(strfolder)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(strfolder));

                    if (System.IO.File.Exists(strfile))
                        System.IO.File.Delete(strfile);

                    pfile.SaveAs(strfile);
                    clsDetails.ImagePath = pfile.FileName;
                }
            }

			ProductSubGroup clsProductSubGroup = new ProductSubGroup();
			Int64 id = clsProductSubGroup.Insert(clsDetails);
			clsDetails.ProductSubGroupID = id;

			if (chkVariations.Checked == true)
			{
				clsProductSubGroup.InheritGroupVariations(clsDetails.ProductGroupID, clsDetails.ProductSubGroupID);
			}

			if (chkVariationsMatrix.Checked == true)
			{
				if (chkVariations.Checked == false)
				{
					clsProductSubGroup.InheritGroupVariations(clsDetails.ProductGroupID, clsDetails.ProductSubGroupID);
				}
				clsProductSubGroup.InheritGroupVariationsMatrix(clsDetails.ProductGroupID, clsDetails.ProductSubGroupID);
			}
			if (chkUnitMatrix.Checked == true)
			{
				clsProductSubGroup.InheritGroupUnitMatrix(clsDetails.ProductGroupID, clsDetails.ProductSubGroupID);
			}

			clsProductSubGroup.CommitAndDispose();

			return id;
		}

		#endregion
    }
}
