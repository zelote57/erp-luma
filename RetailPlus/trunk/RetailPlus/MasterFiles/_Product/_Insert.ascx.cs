using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	/// <summary>
	///		Summary description for __Insert.
	/// </summary>
	public partial  class __Insert : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
                    try { lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString(); }
                    catch { }
					LoadOptions();
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

		#region Web Control Methods

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
		private void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdSaveBack_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
			Response.Redirect(lblReferrer.Text);
		}
		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            string strWindowAction = string.Empty;
            try { strWindowAction = Common.Decrypt(Request.QueryString["windowaction"].ToString(), Session.SessionID); }
            catch { }

            if (strWindowAction != string.Empty)
            {
                string javaScript =
                 "<script type='text/javascript'>\n" +
                 "<!--\n" +
                 "window.close();\n" +
                 "// -->\n" +
                 "</script>\n";
                this.Page.ClientScript.RegisterStartupScript(GetType(),"closewindow", javaScript);
            }
            else
            { Response.Redirect(lblReferrer.Text); }
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
            string strWindowAction = string.Empty;
            try { strWindowAction = Common.Decrypt(Request.QueryString["windowaction"].ToString(), Session.SessionID); }
            catch { }

            if (strWindowAction != string.Empty)
            {
                string javaScript =
                 "<script type='text/javascript'>\n" +
                 "<!--\n" +
                 "window.close();\n" +
                 "// -->\n" +
                 "</script>\n";
                this.Page.ClientScript.RegisterStartupScript(GetType(), "closewindow", javaScript);
            }
            else
            { Response.Redirect(lblReferrer.Text); }
		}
		protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns() { ColumnsNameID = true };

            ProductSubGroupDetails clsSearchKey = new ProductSubGroupDetails() { ProductGroupID = Int64.Parse(cboProductGroup.SelectedItem.Value) } ;

            ProductSubGroup clsProductSubGroup = new ProductSubGroup();
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKey, SortField: "ProductSubGroupName").DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;
            clsProductSubGroup.CommitAndDispose();

            cboProductSubGroup_SelectedIndexChanged(null, null);
		}
		protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cboProductSubGroup.Items.Count != 0)
			{
				ProductSubGroup clsProductSubGroup = new ProductSubGroup();
				ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(Convert.ToInt32(cboProductSubGroup.SelectedItem.Value));
				cboProductUnit.SelectedIndex = cboProductUnit.Items.IndexOf( cboProductUnit.Items.FindByValue(clsProductSubGroupDetails.BaseUnitID.ToString()));
				txtProductPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
                txtWSPrice.Text = clsProductSubGroupDetails.Price.ToString("#,##0.#0");
				txtPurchasePrice.Text = clsProductSubGroupDetails.PurchasePrice.ToString("#,##0.#0");
                chkIncludeInSubtotalDiscount.Checked = clsProductSubGroupDetails.IncludeInSubtotalDiscount;
				txtVAT.Text = clsProductSubGroupDetails.VAT.ToString("#,##0.#0");
				txtEVAT.Text = clsProductSubGroupDetails.EVAT.ToString("#,##0.#0");
				txtLocalTax.Text = clsProductSubGroupDetails.LocalTax.ToString("#,##0.#0");
				clsProductSubGroup.CommitAndDispose();	
			}
		}

        protected void imgCreateBarCode1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            txtBarcode.Text = CreateBarCode();
        }

        protected void imgCreateBarCode2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            txtBarcode2.Text = CreateBarCode();
        }

        protected void imgCreateBarCode3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            txtBarcode3.Text = CreateBarCode();
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			ProductGroup clsProductGroup = new ProductGroup();
			cboProductGroup.DataTextField = "ProductGroupName";
			cboProductGroup.DataValueField = "ProductGroupID";
			cboProductGroup.DataSource = clsProductGroup.ListAsDataTable(SortField:"ProductGroupName").DefaultView;
			cboProductGroup.DataBind();
			cboProductGroup.SelectedIndex = cboProductGroup.Items.Count - 1;

            Data.Unit clsUnit = new Data.Unit(clsProductGroup.Connection, clsProductGroup.Transaction);
			cboProductUnit.DataTextField = "UnitName";
			cboProductUnit.DataValueField = "UnitID";
			cboProductUnit.DataSource = clsUnit.ListAsDataTable(SortField:"UnitName").DefaultView;
			cboProductUnit.DataBind();
			cboProductUnit.SelectedIndex = cboProductUnit.Items.Count - 1;

            ContactColumns clsContactColumns = new ContactColumns();
            clsContactColumns.ContactID = true;
            clsContactColumns.ContactName = true;

            ContactColumns clsContactSearchColumns = new ContactColumns();

			Contacts clsContact = new Contacts(clsProductGroup.Connection , clsProductGroup.Transaction);
			cboSupplier.DataTextField = "ContactName";
			cboSupplier.DataValueField = "ContactID";
            cboSupplier.DataSource = clsContact.Suppliers(clsContactColumns, 0, System.Data.SqlClient.SortOrder.Ascending, clsContactSearchColumns, string.Empty, 0, false, "ContactName", System.Data.SqlClient.SortOrder.Ascending).DefaultView;
			cboSupplier.DataBind();
			cboSupplier.SelectedIndex = cboSupplier.Items.Count - 1;

            //// Added July 9, 2010
            // Remove Nov 22, 2011 - overwritten when a subgroup is selected
            //Terminal clsTerminal = new Terminal(clsProductGroup.Connection, clsProductGroup.Transaction);
            //TerminalDetails clsTerminalDetails = clsTerminal.Details(1);
            //txtWSPriceMarkUp.Text = clsTerminalDetails.WSPriceMarkUp.ToString();
            //txtMargin.Text = clsTerminalDetails.RETPriceMarkUp.ToString();
            //txtVAT.Text = clsTerminalDetails.VAT.ToString("###.#0");
            //txtEVAT.Text = clsTerminalDetails.EVAT.ToString("###.#0");
            //txtLocalTax.Text = clsTerminalDetails.LocalTax.ToString("###.#0");

			clsProductGroup.CommitAndDispose();	

			cboProductGroup_SelectedIndexChanged(null, null);
		}

		private Int64 SaveRecord()
		{
			ProductDetails clsDetails = new ProductDetails();

			clsDetails.ProductCode  = txtProductCode.Text;
			clsDetails.BarCode  = txtBarcode.Text;
            clsDetails.BarCode2 = txtBarcode2.Text;
            clsDetails.BarCode3 = txtBarcode3.Text;
			clsDetails.ProductDesc = txtProductDesc.Text;
			clsDetails.ProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value); 
			clsDetails.ProductSubGroupID  = Convert.ToInt64(cboProductSubGroup.SelectedItem.Value);
			clsDetails.BaseUnitID = Convert.ToInt32(cboProductUnit.SelectedItem.Value); 
			clsDetails.Price = Convert.ToDecimal(txtProductPrice.Text);
            clsDetails.WSPrice = Convert.ToDecimal(txtWSPrice.Text); 
			clsDetails.PurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);
            clsDetails.PercentageCommision = Convert.ToDecimal(txtPercentageCommision.Text);
            clsDetails.IncludeInSubtotalDiscount = chkIncludeInSubtotalDiscount.Checked; 
			clsDetails.VAT = Convert.ToDecimal(txtVAT.Text); 
			clsDetails.EVAT = Convert.ToDecimal(txtEVAT.Text); 
			clsDetails.LocalTax = Convert.ToDecimal(txtLocalTax.Text); 
			clsDetails.Quantity = Convert.ToDecimal(txtQuantity.Text);
			clsDetails.MinThreshold = Convert.ToDecimal(txtMinThreshold.Text);
			clsDetails.MaxThreshold = Convert.ToDecimal(txtMaxThreshold.Text);
			clsDetails.SupplierID = Convert.ToInt64(cboSupplier.SelectedItem.Value);
            clsDetails.IsItemSold = Convert.ToBoolean(chkIsItemSold.Checked);
            clsDetails.WillPrintProductComposition = Convert.ToBoolean(chkWillPrintProductComposition.Checked);
            
			Products clsProduct = new Products();
			Int64 id = clsProduct.Insert(clsDetails);
			clsDetails.ProductID = id;

            long lngUID = long.Parse(Session["UID"].ToString());
            InvAdjustmentDetails clsInvAdjustmentDetails = new InvAdjustmentDetails();
            clsInvAdjustmentDetails.UID = lngUID;
            clsInvAdjustmentDetails.InvAdjustmentDate = DateTime.Now;
            clsInvAdjustmentDetails.ProductID = id;
            clsInvAdjustmentDetails.ProductCode = clsDetails.ProductCode;
            clsInvAdjustmentDetails.Description = clsDetails.ProductDesc;
            clsInvAdjustmentDetails.VariationMatrixID = 0;
            clsInvAdjustmentDetails.MatrixDescription = null;
            clsInvAdjustmentDetails.UnitID = clsDetails.BaseUnitID;
            clsInvAdjustmentDetails.UnitCode = cboProductUnit.SelectedItem.Text;
            clsInvAdjustmentDetails.QuantityBefore = 0;
            clsInvAdjustmentDetails.QuantityNow = clsDetails.Quantity;
            clsInvAdjustmentDetails.MinThresholdBefore = 0;
            clsInvAdjustmentDetails.MinThresholdNow = clsDetails.MinThreshold;
            clsInvAdjustmentDetails.MaxThresholdBefore = 0;
            clsInvAdjustmentDetails.MaxThresholdNow = clsDetails.MaxThreshold;
            clsInvAdjustmentDetails.Remarks = "newly added. beginning balance.";

            InvAdjustment clsInvAdjustment = new InvAdjustment(clsProduct.Connection, clsProduct.Transaction);
            clsInvAdjustment.Insert(clsInvAdjustmentDetails);

			if (chkVariations.Checked == true)
			{
				clsProduct.InheritSubGroupVariations(clsDetails.ProductSubGroupID, clsDetails.ProductID);
			}

			if (chkVariationsMatrix.Checked == true)
			{
				if (chkVariations.Checked == false)
				{
					clsProduct.InheritSubGroupVariations(clsDetails.ProductSubGroupID, clsDetails.ProductID);
				}
				clsProduct.InheritSubGroupVariationsMatrix(clsDetails.ProductSubGroupID, clsDetails.ProductID, clsDetails);
			}
			if (chkUnitMatrix.Checked == true)
			{
				clsProduct.InheritSubGroupUnitMatrix(clsDetails.ProductSubGroupID, clsDetails.ProductID);
			}

            // Aug 26, 2011 : Lemu
            // Update Required Inventory Days (RID)
            clsDetails.RID = Convert.ToInt64(txtRID.Text);
            clsProduct.UpdateRID(clsDetails.ProductID, clsDetails.RID);

			clsProduct.CommitAndDispose();

			return 0;
		}

        private string CreateBarCode()
        {
            string strRetValue = "";

            Data.ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup();
            string strProductCode = clsProductSubGroup.getBarCodeCounter(Int64.Parse(cboProductSubGroup.SelectedItem.Value)).ToString().PadLeft(13 - (cboProductSubGroup.SelectedItem.Value.Length + 2), '0');
            clsProductSubGroup.CommitAndDispose();

            BarcodeHelper ean13 = new BarcodeHelper("99", cboProductSubGroup.SelectedItem.Value, strProductCode);
            strRetValue = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

            return strRetValue;
        }

		#endregion

        
	}
}
