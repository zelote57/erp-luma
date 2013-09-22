namespace AceSoft.RetailPlus.MasterFiles._Product
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using AceSoft.RetailPlus.Data;
    using System.IO;
    using System.Xml;

    public partial class __Synchronize : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);
            this.imgSynchronize.Click += new System.Web.UI.ImageClickEventHandler(this.imgSynchronize_Click);

		}
		#endregion

		#region Web Control Methods

		private void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void imgSynchronize_Click(object sender, ImageClickEventArgs e)
        {
            SynchronizeToBranch();
        }
		protected void cmdSynchronize_Click(object sender, System.EventArgs e)
		{
			SynchronizeToBranch();
		}
        protected void imgSynchronizeFromBranch_Click(object sender, ImageClickEventArgs e)
        {
            SynchronizeFromBranch();
        }
        protected void cmdSynchronizeFromBranch_Click(object sender, EventArgs e)
        {
            SynchronizeFromBranch();
        }
        protected void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }
        protected void imgUpload_Click(object sender, ImageClickEventArgs e)
        {
            Upload1();
        }
        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            Upload();
        }
        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            Download();
        }
        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            Download();
        }
        
		#endregion

		#region Private Methods

        private void LoadOptions()
        {
            Branch clsBranch = new Branch();
            cboSynchronizeToBranch.DataTextField = "BranchCode";
            cboSynchronizeToBranch.DataValueField = "BranchID";
            cboSynchronizeToBranch.DataSource = clsBranch.ListAsDataTable("BranchCode", SortOption.Ascending).DefaultView;
            cboSynchronizeToBranch.DataBind();
            cboSynchronizeToBranch.SelectedIndex = 0;
            clsBranch.CommitAndDispose();

            cboSynchronizeFromBranch.DataTextField = "BranchCode";
            cboSynchronizeFromBranch.DataValueField = "BranchID";
            cboSynchronizeFromBranch.DataSource = cboSynchronizeToBranch.DataSource;
            cboSynchronizeFromBranch.DataBind();
            cboSynchronizeFromBranch.SelectedIndex = 0;

            cboBranchUpload.DataTextField = "BranchCode";
            cboBranchUpload.DataValueField = "BranchID";
            cboBranchUpload.DataSource = cboSynchronizeToBranch.DataSource;
            cboBranchUpload.DataBind();
            cboBranchUpload.SelectedIndex = 0;

            cboBranchDownload.DataTextField = "BranchCode";
            cboBranchDownload.DataValueField = "BranchID";
            cboBranchDownload.DataSource = cboSynchronizeToBranch.DataSource;
            cboBranchDownload.DataBind();
            cboBranchDownload.SelectedIndex = 0;

            if (cboSynchronizeToBranch.Items.Count == 0)
            {
                imgSynchronizeFromBranch.Visible = false;
                cmdSynchronizeFromBranch.Enabled = false;
                cboSynchronizeFromBranch.Items.Add(new ListItem("No Branch", "0"));

                imgSynchronize.Visible = false;
                cmdSynchronize.Enabled = false;
                cboSynchronizeToBranch.Items.Add(new ListItem("No Branch", "0"));
                
                txtPath.Enabled = false;
                imgUpload.Enabled = false;
                cmdUpload.Enabled = false;
                cboBranchUpload.Items.Add(new ListItem("No Branch", "0"));
            }
            
        }
        private void SynchronizeToBranch()
        {
            try 
            {
                lblError.Text = string.Empty;
                
                Branch clsBranch = new Branch();
                BranchDetails clsBranchDetails = clsBranch.Details(Convert.ToInt16(cboSynchronizeToBranch.SelectedItem.Value.ToString()));
                clsBranch.CommitAndDispose();

                if (IPAddress.IsOpen(clsBranchDetails.DBIP, int.Parse(clsBranchDetails.DBPort)) == false)
                {
                    lblError.Text = "Sorry cannot connect to Branch '" + cboSynchronizeToBranch.SelectedItem.Text + "'. Please check you connection to IP Address :" + clsBranchDetails.DBIP + ". <br><br>";
                    lblError.Text += "HOW TO CHECK : <br><br>";
                    lblError.Text += "  1. Open command prompt<br>";
                    lblError.Text += "  2. Type ping[space][IP Address]<br><br>";
                    lblError.Text += "If the answer is 'Request timed out.' then contact system administrator.<br>";
                    lblError.Text += "Else if the answer is 'Reply...' Follow the next steps.<br><br>";
                    lblError.Text += "  3. Type telnet[space][IP Address][sapce][IP Port]<br><br>";

                    return;
                }

                Session.Timeout = 60 * 60 * 30;

                Products clsProduct = new Products();
                ProductDetails[] arrProductDetails = clsProduct.List();

                ContactGroup clsContactGroup = new ContactGroup(clsProduct.Connection, clsProduct.Transaction);
                Contacts clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
                Data.Unit clsUnit = new Data.Unit(clsProduct.Connection, clsProduct.Transaction);
                Data.ProductGroup clsProductGroup = new Data.ProductGroup(clsProduct.Connection, clsProduct.Transaction);
                Data.ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup(clsProduct.Connection, clsProduct.Transaction);
                Data.Variation clsVariation = new Variation(clsProduct.Connection, clsProduct.Transaction);

                RemoteBranchInventory clsBranchInventory = new RemoteBranchInventory();
                clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP, clsBranchDetails.DBPort);

                ContactGroup clsBranchContactGroup = new ContactGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Contacts clsBranchContact = new Contacts(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.Unit clsBranchUnit = new Data.Unit(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.ProductGroup clsBranchProductGroup = new Data.ProductGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.ProductSubGroup clsBranchProductSubGroup = new Data.ProductSubGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.Variation clsBranchVariation = new Variation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Products clsBranchProduct = new Products(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductDetails clsBranchProductDetails;

                foreach (ProductDetails clsProductDetails in arrProductDetails)
                {
                    clsBranchProductDetails = clsProductDetails;
                    try
                    {
                        clsBranchProductDetails.ProductID = clsBranchProduct.Details(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.BarCode).ProductID;
                        if (clsBranchProductDetails.ProductID != 0)
                        {
                            lblError.Text += clsBranchProductDetails.BarCode + " already exist.<br><br>";
                            clsBranchProduct.UpdatePurchasing(clsBranchProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsBranchProductDetails.SupplierID, clsBranchProductDetails.BaseUnitID, clsBranchProductDetails.PurchasePrice);
                            clsBranchProduct.UpdateSellingPrice(clsBranchProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsBranchProductDetails.SupplierID, clsBranchProductDetails.BaseUnitID, clsBranchProductDetails.Price);
                        }
                        else
                        {
                            clsBranchProductDetails.ProductID = clsBranchProduct.DetailsByCode(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.ProductCode).ProductID;
                            if (clsBranchProductDetails.ProductID != 0)
                            {
                                lblError.Text += clsBranchProductDetails.ProductCode + " already exist.<br><br>";
                                clsBranchProduct.UpdateBarcode(clsBranchProductDetails.ProductID, clsBranchProductDetails.BarCode);
                                clsBranchProduct.UpdatePurchasing(clsBranchProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsBranchProductDetails.SupplierID, clsBranchProductDetails.BaseUnitID, clsBranchProductDetails.PurchasePrice);
                                clsBranchProduct.UpdateSellingPrice(clsBranchProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsBranchProductDetails.SupplierID, clsBranchProductDetails.BaseUnitID, clsBranchProductDetails.Price);
                            }
                            else
                            {
                                clsBranchProductDetails.SupplierID = clsBranchContact.Details(clsBranchProductDetails.SupplierCode).ContactID;
                                if (clsBranchProductDetails.SupplierID == 0)
                                {
                                    ContactDetails clsContactDetails = clsContact.Details(clsBranchProductDetails.SupplierCode);
                                    if (clsBranchContactGroup.Details(clsContactDetails.ContactGroupID).ContactGroupID == 0)
                                    {
                                        ContactGroupDetails clsContactGroupDetails = clsContactGroup.Details(clsContactDetails.ContactGroupID);
                                        clsContactDetails.ContactGroupID = clsBranchContactGroup.Insert(clsContactGroupDetails);
                                    }
                                    clsBranchProductDetails.SupplierID = clsBranchContact.Insert(clsContactDetails);
                                }

                                clsBranchProductDetails.BaseUnitID = clsBranchUnit.Details(clsBranchProductDetails.BaseUnitCode).UnitID;
                                if (clsBranchProductDetails.BaseUnitID == 0)
                                {
                                    UnitDetails clsUnitDetails = clsUnit.Details(clsProductDetails.BaseUnitID);
                                    clsBranchProductDetails.BaseUnitID = clsBranchUnit.Insert(clsUnitDetails);
                                }

                                clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Details(clsBranchProductDetails.ProductGroupCode).ProductGroupID;
                                if (clsBranchProductDetails.ProductGroupID == 0)
                                {
                                    ProductGroupDetails clsProductGroupDetails = clsProductGroup.Details(clsProductDetails.ProductGroupID);
                                    clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Insert(clsProductGroupDetails);
                                }

                                clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Details(clsBranchProductDetails.ProductSubGroupCode).ProductSubGroupID;
                                if (clsBranchProductDetails.ProductSubGroupID == 0)
                                {
                                    ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(clsProductDetails.ProductSubGroupID);
                                    clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Insert(clsProductSubGroupDetails);
                                }

                                clsBranchProductDetails.Quantity = 0;
                                clsBranchProductDetails.QuantityIN = 0;
                                clsBranchProductDetails.QuantityOUT = 0;

                                clsBranchProductDetails.ProductID = clsBranchProduct.Insert(clsBranchProductDetails);
                                lblError.Text += clsBranchProductDetails.ProductCode + " inserted.<br><br>";
                            }
                        }
                    }
                    catch {
                        lblError.Text += "<div class=ms-alternating> ERROR INSERTING ITEM: " + clsBranchProductDetails.ProductCode + "</div><br><br>";
                        if (clsBranchInventory.Connection.State == ConnectionState.Closed)
                        {
                            clsBranchInventory = new RemoteBranchInventory();
                            clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP);
                            clsBranchContactGroup = new ContactGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchContact = new Contacts(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchUnit = new Data.Unit(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchProductGroup = new Data.ProductGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchProductSubGroup = new Data.ProductSubGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchVariation = new Variation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                            clsBranchProduct = new Products(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                        }
                    }
                }

                clsProduct.CommitAndDispose();
                clsBranchInventory.CommitAndDispose();

                lblError.Text = "Done synchronizing products of Branch: " + clsBranchDetails.BranchCode + "<br><br>" + lblError.Text;
            }
            catch (Exception ex)
            {
                lblError.Text += "ERROR WHILE CREATING INSERT STATEMENT: " + ex.Message;
            }
        }
        private void SynchronizeFromBranch()
        {
            try
            {
                lblError.Text = string.Empty;

                Branch clsBranch = new Branch();
                BranchDetails clsBranchDetails = clsBranch.Details(Convert.ToInt16(cboSynchronizeFromBranch.SelectedItem.Value.ToString()));
                clsBranch.CommitAndDispose();

                if (IPAddress.IsOpen(clsBranchDetails.DBIP, int.Parse(clsBranchDetails.DBPort)) == false)
                {
                    lblError.Text = "Sorry cannot connect to Branch '" + cboSynchronizeFromBranch.SelectedItem.Text + "'. Please check you connection to IP Address :" + clsBranchDetails.DBIP + ". <br><br>";
                    lblError.Text += "HOW TO CHECK : <br><br>";
                    lblError.Text += "  1. Open command prompt<br>";
                    lblError.Text += "  2. Type ping[space][IP Address]<br><br>";
                    lblError.Text += "If the answer is 'Request timed out.' then contact system administrator.<br>";
                    lblError.Text += "Else if the answer is 'Reply...' Follow the next steps.<br><br>";
                    lblError.Text += "  3. Type telnet[space][IP Address][sapce][IP Port]<br><br>";

                    return;
                }

                Session.Timeout = 60 * 60 * 30;

                Products clsProduct = new Products();
                clsProduct.GetConnection();
                ProductDetails clsProductDetails;
                ContactGroup clsContactGroup = new ContactGroup(clsProduct.Connection, clsProduct.Transaction);
                Contacts clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
                Data.Unit clsUnit = new Data.Unit(clsProduct.Connection, clsProduct.Transaction);
                Data.ProductGroup clsProductGroup = new Data.ProductGroup(clsProduct.Connection, clsProduct.Transaction);
                Data.ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup(clsProduct.Connection, clsProduct.Transaction);
                Data.Variation clsVariation = new Variation(clsProduct.Connection, clsProduct.Transaction);

                RemoteBranchInventory clsBranchInventory = new RemoteBranchInventory();
                clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP, clsBranchDetails.DBPort);

                Products clsBranchProduct = new Products(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductDetails[] arrBranchProductDetails = clsBranchProduct.List();

                ContactGroup clsBranchContactGroup = new ContactGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Contacts clsBranchContact = new Contacts(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.Unit clsBranchUnit = new Data.Unit(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.ProductGroup clsBranchProductGroup = new Data.ProductGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.ProductSubGroup clsBranchProductSubGroup = new Data.ProductSubGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Data.Variation clsBranchVariation = new Variation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                
                foreach (ProductDetails clsBranchProductDetails in arrBranchProductDetails)
                {
                    clsProductDetails = clsBranchProductDetails;
                    try
                    {
                        clsProductDetails.ProductID = clsProduct.Details(clsProductDetails.BarCode).ProductID;
                        if (clsProductDetails.ProductID != 0)
                        {
                            lblError.Text += clsProductDetails.BarCode + " already exist.<br><br>";
                            clsProduct.UpdatePurchasing(clsProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsProductDetails.SupplierID, clsProductDetails.BaseUnitID, clsProductDetails.PurchasePrice);
                            clsProduct.UpdateSellingPrice(clsProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsProductDetails.SupplierID, clsProductDetails.BaseUnitID, clsProductDetails.Price);
                        }
                        else
                        {
                            clsProductDetails.ProductID = clsProduct.DetailsByCode(Constants.BRANCH_ID_MAIN, clsProductDetails.BarCode).ProductID;
                            if (clsProductDetails.ProductID != 0)
                            {
                                lblError.Text += clsProductDetails.ProductCode + " already exist.<br><br>";
                                clsProduct.UpdateBarcode(clsProductDetails.ProductID, clsProductDetails.BarCode);
                                clsProduct.UpdatePurchasing(clsProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsProductDetails.SupplierID, clsProductDetails.BaseUnitID, clsProductDetails.PurchasePrice);
                                clsProduct.UpdateSellingPrice(clsProductDetails.ProductID, clsBranchProductDetails.MatrixID, clsProductDetails.SupplierID, clsProductDetails.BaseUnitID, clsProductDetails.Price);
                            }
                            else
                            {
                                clsProductDetails.SupplierID = clsContact.Details(clsProductDetails.SupplierCode).ContactID;
                                if (clsProductDetails.SupplierID == 0)
                                {
                                    ContactDetails clsBranchContactDetails = clsBranchContact.Details(clsProductDetails.SupplierCode);
                                    if (clsContactGroup.Details(clsBranchContactDetails.ContactGroupID).ContactGroupID == 0)
                                    {
                                        ContactGroupDetails clsBranchContactGroupDetails = clsBranchContactGroup.Details(clsBranchContactDetails.ContactGroupID);
                                        clsBranchContactDetails.ContactGroupID = clsContactGroup.Insert(clsBranchContactGroupDetails);
                                    }
                                    clsProductDetails.SupplierID = clsContact.Insert(clsBranchContactDetails);
                                }

                                clsProductDetails.BaseUnitID = clsUnit.Details(clsProductDetails.BaseUnitCode).UnitID;
                                if (clsProductDetails.BaseUnitID == 0)
                                {
                                    UnitDetails clsBranchUnitDetails = clsBranchUnit.Details(clsBranchProductDetails.BaseUnitID);
                                    clsProductDetails.BaseUnitID = clsUnit.Insert(clsBranchUnitDetails);
                                }

                                clsProductDetails.ProductGroupID = clsProductGroup.Details(clsProductDetails.ProductGroupCode).ProductGroupID;
                                if (clsProductDetails.ProductGroupID == 0)
                                {
                                    ProductGroupDetails clsBranchProductGroupDetails = clsBranchProductGroup.Details(clsBranchProductDetails.ProductGroupID);
                                    clsProductDetails.ProductGroupID = clsProductGroup.Insert(clsBranchProductGroupDetails);
                                }

                                clsProductDetails.ProductSubGroupID = clsProductSubGroup.Details(clsProductDetails.ProductSubGroupCode).ProductSubGroupID;
                                if (clsProductDetails.ProductSubGroupID == 0)
                                {
                                    ProductSubGroupDetails clsBranchProductSubGroupDetails = clsBranchProductSubGroup.Details(clsBranchProductDetails.ProductSubGroupID);
                                    clsProductDetails.ProductSubGroupID = clsProductSubGroup.Insert(clsBranchProductSubGroupDetails);
                                }

                                clsProductDetails.Quantity = 0;
                                clsProductDetails.QuantityIN = 0;
                                clsProductDetails.QuantityOUT = 0;

                                try
                                {
                                    clsProductDetails.ProductID = clsProduct.Insert(clsProductDetails);
                                    lblError.Text += clsProductDetails.ProductCode + " inserted.<br><br>";
                                }
                                catch (Exception exProduct){
                                    lblError.Text += "<div class=ms-alternating> ERROR INSERTING ITEM: " + clsProductDetails.ProductCode + " err: " + exProduct.Message + ".</div><br><br>";
                                    if (clsProduct.Connection.State == ConnectionState.Closed)
                                    {
                                        clsProduct = new Products();
                                        clsProduct.GetConnection();
                                        clsContactGroup = new ContactGroup(clsProduct.Connection, clsProduct.Transaction);
                                        clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
                                        clsUnit = new Data.Unit(clsProduct.Connection, clsProduct.Transaction);
                                        clsProductGroup = new Data.ProductGroup(clsProduct.Connection, clsProduct.Transaction);
                                        clsProductSubGroup = new Data.ProductSubGroup(clsProduct.Connection, clsProduct.Transaction);
                                        clsVariation = new Variation(clsProduct.Connection, clsProduct.Transaction);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exProducts)
                    {
                        lblError.Text += "<div class=ms-alternating> ERROR INSERTING ITEM: " + clsProductDetails.ProductCode + " err: " + exProducts.Message + ".</div><br><br>";
                        if (clsProduct.Connection.State == ConnectionState.Closed)
                        {
                            clsProduct = new Products();
                            clsProduct.GetConnection();
                            clsContactGroup = new ContactGroup(clsProduct.Connection, clsProduct.Transaction);
                            clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
                            clsUnit = new Data.Unit(clsProduct.Connection, clsProduct.Transaction);
                            clsProductGroup = new Data.ProductGroup(clsProduct.Connection, clsProduct.Transaction);
                            clsProductSubGroup = new Data.ProductSubGroup(clsProduct.Connection, clsProduct.Transaction);
                            clsVariation = new Variation(clsProduct.Connection, clsProduct.Transaction);
                        }
                    }
                }

                clsProduct.CommitAndDispose();
                clsBranchInventory.CommitAndDispose();

                lblError.Text = "Done synchronizing products from Branch: " + clsBranchDetails.BranchCode + "<br><br>" + lblError.Text;
            }
            catch (Exception ex)
            {
                lblError.Text += "ERROR WHILE CREATING INSERT STATEMENT: " + ex.Message;
            }
        }
        private void Download()
        {
            DateTime dteProcessingDate = DateTime.Now;

            Products clsProduct = new Products();
            ProductDetails[] arrProductDetails = clsProduct.List();

            Contacts clsContact = new Contacts(clsProduct.Connection, clsProduct.Transaction);
            ContactDetails clsContactDetails ;

            ContactGroup clsContactGroup = new ContactGroup(clsProduct.Connection, clsProduct.Transaction);
            ContactGroupDetails clsContactGroupDetails;

            ProductVariation clsProductVariation = new ProductVariation(clsProduct.Connection, clsProduct.Transaction);
			DataTable dtaProductVariation;

            try{
                lblError.Text += "Creating xml file...<br><br>";
                string xmlFileName = Server.MapPath(@"\RetailPlus\temp\ExportedProductList_" + CompanyDetails.CompanyCode + "_" + dteProcessingDate.ToString("yyyyMMddHHmm") + ".xml");
                XmlTextWriter writer = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);
                
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteComment("This file represents the latest list of " + CompanyDetails.CompanyName + " products as of '" + dteProcessingDate.ToString("yyyyMMddHHmm")  + "'.");
                writer.WriteComment("Save this in your local file. Goto 'File', click 'Save As', select the location in your local directory, click 'Save'.");
                writer.WriteStartElement("Products");
                lblError.Text += "Creating Products...<br><br>";
                foreach(ProductDetails clsProductDetails in arrProductDetails)
                {
                    writer.WriteStartElement("Item");
                    lblError.Text += "Writing " + clsProductDetails.ProductCode + "...";
                    writer.WriteAttributeString("ProductID", XmlConvert.ToString(clsProductDetails.ProductID));
                    writer.WriteAttributeString("ProductCode", clsProductDetails.ProductCode);
                    writer.WriteAttributeString("BarCode", clsProductDetails.BarCode);
                    writer.WriteAttributeString("ProductDesc", clsProductDetails.ProductDesc);
                    writer.WriteAttributeString("ProductGroupID", XmlConvert.ToString(clsProductDetails.ProductGroupID));
                    writer.WriteAttributeString("ProductGroupCode", clsProductDetails.ProductGroupCode);
                    writer.WriteAttributeString("ProductGroupName", clsProductDetails.ProductGroupName);
                    writer.WriteAttributeString("ProductSubGroupID", XmlConvert.ToString(clsProductDetails.ProductSubGroupID));
                    writer.WriteAttributeString("ProductSubGroupCode", clsProductDetails.ProductSubGroupCode);
                    writer.WriteAttributeString("ProductSubGroupName", clsProductDetails.ProductSubGroupName);
                    writer.WriteAttributeString("BaseUnitID", XmlConvert.ToString(clsProductDetails.BaseUnitID));
                    writer.WriteAttributeString("BaseUnitCode", clsProductDetails.BaseUnitCode);
                    writer.WriteAttributeString("BaseUnitName", clsProductDetails.BaseUnitName);
                    writer.WriteAttributeString("DateCreated", clsProductDetails.DateCreated.ToString("MM/dd/yyyy HH:mm:ss"));
                    writer.WriteAttributeString("Deleted",  XmlConvert.ToString(clsProductDetails.Deleted));
                    writer.WriteAttributeString("Price", XmlConvert.ToString(clsProductDetails.Price));
                    writer.WriteAttributeString("PurchasePrice", XmlConvert.ToString(clsProductDetails.PurchasePrice));
                    writer.WriteAttributeString("IncludeInSubtotalDiscount", XmlConvert.ToString(clsProductDetails.IncludeInSubtotalDiscount));
                    writer.WriteAttributeString("VAT", XmlConvert.ToString(clsProductDetails.VAT));
                    writer.WriteAttributeString("EVAT", XmlConvert.ToString(clsProductDetails.EVAT));
                    writer.WriteAttributeString("LocalTax", XmlConvert.ToString(clsProductDetails.LocalTax));
                    writer.WriteAttributeString("Quantity", XmlConvert.ToString(clsProductDetails.Quantity));
                    writer.WriteAttributeString("MinThreshold", XmlConvert.ToString(clsProductDetails.MinThreshold));
                    writer.WriteAttributeString("MaxThreshold", XmlConvert.ToString(clsProductDetails.MaxThreshold));

                    //Get the SUpplier Details
                    clsContactDetails = clsContact.Details(clsProductDetails.SupplierID);

                    writer.WriteAttributeString("ContactID", XmlConvert.ToString(clsContactDetails.ContactID));
                    writer.WriteAttributeString("ContactCode", clsContactDetails.ContactCode);
                    writer.WriteAttributeString("ContactName", clsContactDetails.ContactName);
                    
                    clsContactGroupDetails = clsContactGroup.Details(clsContactDetails.ContactGroupID);
                    writer.WriteAttributeString("ContactGroupCode", clsContactGroupDetails.ContactGroupCode);
                    writer.WriteAttributeString("ContactGroupCategory", clsContactGroupDetails.ContactGroupCategory.ToString("G"));
                    writer.WriteAttributeString("ContactGroupName", clsContactDetails.ContactGroupName);

                    writer.WriteAttributeString("ModeOfTerms", clsContactDetails.ModeOfTerms.ToString("G"));
                    writer.WriteAttributeString("Terms", clsContactDetails.Terms.ToString());
                    writer.WriteAttributeString("Address", clsContactDetails.Address);
                    writer.WriteAttributeString("BusinessName", clsContactDetails.BusinessName);
                    writer.WriteAttributeString("TelephoneNo", clsContactDetails.TelephoneNo);
                    writer.WriteAttributeString("Remarks", clsContactDetails.Remarks);
                    writer.WriteAttributeString("Debit", "0");
                    writer.WriteAttributeString("Credit", "0");
                    writer.WriteAttributeString("CreditLimit", clsContactDetails.CreditLimit.ToString());
                    writer.WriteAttributeString("IsCreditAllowed", clsContactDetails.IsCreditAllowed.ToString());
                    writer.WriteAttributeString("ContactDateCreated", clsContactDetails.DateCreated.ToString("MM/dd/yyyy HH:mm:ss"));

                    writer.WriteAttributeString("OrderSlipPrinter", clsProductDetails.OrderSlipPrinter.ToString("G"));
                    writer.WriteAttributeString("ChartOfAccountIDPurchase", XmlConvert.ToString(clsProductDetails.ChartOfAccountIDPurchase));
                    writer.WriteAttributeString("ChartOfAccountIDSold", XmlConvert.ToString(clsProductDetails.ChartOfAccountIDSold));
                    writer.WriteAttributeString("ChartOfAccountIDInventory", XmlConvert.ToString(clsProductDetails.ChartOfAccountIDInventory));
                    writer.WriteAttributeString("ChartOfAccountIDTaxPurchase", XmlConvert.ToString(clsProductDetails.ChartOfAccountIDTaxPurchase));
                    writer.WriteAttributeString("ChartOfAccountIDTaxSold", XmlConvert.ToString(clsProductDetails.ChartOfAccountIDTaxSold));
                    writer.WriteAttributeString("IsItemSold", XmlConvert.ToString(clsProductDetails.IsItemSold));
                    writer.WriteAttributeString("WillPrintProductComposition", XmlConvert.ToString(clsProductDetails.WillPrintProductComposition));
                    writer.WriteAttributeString("UpdatedBy", XmlConvert.ToString(clsProductDetails.UpdatedBy));
                    writer.WriteAttributeString("UpdatedOn", clsProductDetails.UpdatedOn.ToString("MM/dd/yyyy HH:mm:ss"));
                    writer.WriteAttributeString("PercentageCommision", XmlConvert.ToString(clsProductDetails.PercentageCommision));
                    writer.WriteAttributeString("QuantityIN", XmlConvert.ToString(clsProductDetails.QuantityIN));
                    writer.WriteAttributeString("QuantityOUT", XmlConvert.ToString(clsProductDetails.QuantityOUT));

                    //Get Variations
                    dtaProductVariation = clsProductVariation.ListAsDataTable(clsProductDetails.ProductID, null, System.Data.SqlClient.SortOrder.Ascending);
                    foreach (DataRow rowVariation in dtaProductVariation.Rows)
                    {
                        writer.WriteStartElement("Variation", null);
                        writer.WriteAttributeString("VariationCode", rowVariation["VariationCode"].ToString());
                        writer.WriteAttributeString("VariationType", rowVariation["VariationType"].ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    lblError.Text += " Done.<br><br>";
                }
                writer.WriteEndElement();

                //Write the XML to file and close the writer
                writer.Flush();
                writer.Close();
                lblError.Text += "Done creating XML file. <br><br>";
                //lblError.Text = "/RetailPlus/temp/ExportedProductList_" + CompanyDetails.CompanyCode + "_" + dteProcessingDate.ToString("yyyyMMddHHmm") + ".xml<br><br>" + lblError.Text;
                clsProduct.CommitAndDispose();

                string stScript = "<Script>";
                stScript += "window.open('../../temp/ExportedProductList_" + CompanyDetails.CompanyCode + "_" + dteProcessingDate.ToString("yyyyMMddHHmm") + ".xml');";
                stScript += "</Script>";
                Response.Write(stScript);
            }
            catch (Exception ex)
            {
                lblError.Text += "ERROR WHILE CREATING xml FILE: " + ex.Message;
            }
        }
        private void Upload()
        {
            if (txtPath.HasFile)
            {
                //string fn = System.IO.Path.GetFileName(txtPath.PostedFile.FileName);
                //string SaveLocation = "/RetailPlus/temp/uploaded_" + fn;

                //txtPath.PostedFile.SaveAs(SaveLocation);
                XmlTextReader xmlReader = new XmlTextReader(txtPath.PostedFile.ToString());
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;

                Branch clsBranch = new Branch();
                BranchDetails clsBranchDetails = clsBranch.Details(Convert.ToInt16(cboBranchUpload.SelectedItem.Value.ToString()));
                clsBranch.CommitAndDispose();

                RemoteBranchInventory clsBranchInventory = new RemoteBranchInventory();
                clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP);

                Contacts clsBranchContact = new Contacts(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ContactDetails clsBranchContactDetails;

                ContactGroup clsBranchContactGroup = new ContactGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ContactGroupDetails clsContactGroupDetails;

                Data.Unit clsBranchUnit = new Data.Unit(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                UnitDetails clsUnitDetails;

                ProductGroup clsBranchProductGroup = new Data.ProductGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductGroupDetails clsBranchProductGroupDetails;
 
                ProductSubGroup clsBranchProductSubGroup = new Data.ProductSubGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductSubGroupDetails clsBranchProductSubGroupDetails;

                //Data.Variation clsBranchVariation = new Variation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                Products clsBranchProduct = new Products(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductDetails clsBranchProductDetails;

                ProductVariation clsBranchProductVariation = new ProductVariation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
                ProductVariationDetails clsBranchProductVariationDetails;

                long lngBranchProductID = 0; long lngProductCtr = 0; long lngProductInserted = 0;

                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            
                            if (xmlReader.Name == "Item")
                            {
                                lngProductCtr++;

                                clsBranchProductDetails = new ProductDetails();
                                clsBranchProductDetails.BarCode = xmlReader.GetAttribute("BarCode");
                                clsBranchProductDetails.ProductCode = xmlReader.GetAttribute("ProductCode");
                                lblError.Text += "Checking <b>" + clsBranchProductDetails.ProductCode + "</b> if exist... ";

                                //check product by barcode
                                clsBranchProductDetails.ProductID = clsBranchProduct.Details(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.BarCode).ProductID;
                                lngBranchProductID = clsBranchProductDetails.ProductID;
                                if (clsBranchProductDetails.ProductID != 0)
                                {
                                    lblError.Text += " [Y] barcode exist... next item...<br>";
                                    break;
                                }

                                //check product by product code
                                clsBranchProductDetails.ProductID = clsBranchProduct.Details(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.ProductCode).ProductID;
                                lngBranchProductID = clsBranchProductDetails.ProductID;
                                if (clsBranchProductDetails.ProductID != 0)
                                {
                                    clsBranchProduct.UpdateBarcode(clsBranchProductDetails.ProductID, clsBranchProductDetails.BarCode);
                                    lblError.Text += " [Y] barcode not exist, product code exist. barcode updated. next item...<br>";
                                    break;
                                }

                                lblError.Text += " [N] inserting product... ";

                                clsBranchProductDetails.BarCode = xmlReader.GetAttribute("BarCode");
                                clsBranchProductDetails.ProductDesc = xmlReader.GetAttribute("ProductDesc");
                                clsBranchProductDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                                clsBranchProductDetails.ProductGroupName = xmlReader.GetAttribute("ProductGroupName");
                                clsBranchProductDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                                clsBranchProductDetails.ProductSubGroupName = xmlReader.GetAttribute("ProductSubGroupName");
                                clsBranchProductDetails.BaseUnitCode = xmlReader.GetAttribute("BaseUnitCode");
                                clsBranchProductDetails.BaseUnitName = xmlReader.GetAttribute("BaseUnitName");
                                clsBranchProductDetails.DateCreated = DateTime.Now;
                                clsBranchProductDetails.Price = Convert.ToDecimal(xmlReader.GetAttribute("Price"));
                                clsBranchProductDetails.PurchasePrice = Convert.ToDecimal(xmlReader.GetAttribute("PurchasePrice"));
                                clsBranchProductDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(xmlReader.GetAttribute("IncludeInSubtotalDiscount"));
                                clsBranchProductDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("VAT"));
                                clsBranchProductDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("EVAT"));
                                clsBranchProductDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("LocalTax"));
                                clsBranchProductDetails.Quantity = 0;
                                clsBranchProductDetails.MinThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MinThreshold"));
                                clsBranchProductDetails.MaxThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MaxThreshold"));
                                clsBranchProductDetails.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), xmlReader.GetAttribute("OrderSlipPrinter"));
                                clsBranchProductDetails.ChartOfAccountIDPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDPurchase"));
                                clsBranchProductDetails.ChartOfAccountIDSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDSold"));
                                clsBranchProductDetails.ChartOfAccountIDInventory = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDInventory"));
                                clsBranchProductDetails.ChartOfAccountIDTaxPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxPurchase"));
                                clsBranchProductDetails.ChartOfAccountIDTaxSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxSold"));
                                clsBranchProductDetails.IsItemSold = Convert.ToBoolean(int.Parse(xmlReader.GetAttribute("IsItemSold")));
                                clsBranchProductDetails.WillPrintProductComposition = Convert.ToBoolean(int.Parse(xmlReader.GetAttribute("WillPrintProductComposition")));
                                clsBranchProductDetails.UpdatedBy = long.Parse(xmlReader.GetAttribute("UpdatedBy"));
                                clsBranchProductDetails.UpdatedOn = Convert.ToDateTime(xmlReader.GetAttribute("UpdatedOn"));
                                clsBranchProductDetails.PercentageCommision = decimal.Parse(xmlReader.GetAttribute("PercentageCommision"));
                                clsBranchProductDetails.QuantityIN = decimal.Parse(xmlReader.GetAttribute("QuantityIN"));
                                clsBranchProductDetails.QuantityOUT = decimal.Parse(xmlReader.GetAttribute("QuantityOUT"));

                                clsBranchProductDetails.SupplierCode = xmlReader.GetAttribute("ContactCode");
                                clsBranchProductDetails.SupplierID = clsBranchContact.Details(clsBranchProductDetails.SupplierCode).ContactID;
                                if (clsBranchProductDetails.SupplierID == 0)
                                {
                                    clsBranchContactDetails = new ContactDetails();
                                    clsBranchContactDetails.ContactGroupID = clsBranchContactGroup.Details(xmlReader.GetAttribute("ContactGroupCode")).ContactGroupID;
                                    if (clsBranchContactDetails.ContactGroupID == 0)
                                    {
                                        clsContactGroupDetails = new ContactGroupDetails();
                                        clsContactGroupDetails.ContactGroupCode = xmlReader.GetAttribute("ContactCode");
                                        clsContactGroupDetails.ContactGroupName = xmlReader.GetAttribute("ContactCode");
                                        clsContactGroupDetails.ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), xmlReader.GetAttribute("ContactGroupCategory"));
                                        clsBranchContactDetails.ContactGroupID = clsBranchContactGroup.Insert(clsContactGroupDetails);
                                    }

                                    clsBranchContactDetails.ContactCode = xmlReader.GetAttribute("ContactCode");
                                    clsBranchContactDetails.ContactName = xmlReader.GetAttribute("ContactName");
                                    
                                    clsBranchContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), xmlReader.GetAttribute("ModeOfTerms"));
                                    clsBranchContactDetails.Terms = Convert.ToInt32(xmlReader.GetAttribute("Terms"));
                                    clsBranchContactDetails.Address = xmlReader.GetAttribute("Address");
                                    clsBranchContactDetails.BusinessName = xmlReader.GetAttribute("BusinessName");
                                    clsBranchContactDetails.TelephoneNo = xmlReader.GetAttribute("TelephoneNo");
                                    clsBranchContactDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                    clsBranchContactDetails.Debit = Convert.ToDecimal(xmlReader.GetAttribute("Debit"));
                                    clsBranchContactDetails.Credit = Convert.ToDecimal(xmlReader.GetAttribute("Credit"));
                                    clsBranchContactDetails.IsCreditAllowed = Convert.ToBoolean(xmlReader.GetAttribute("IsCreditAllowed"));
                                    clsBranchContactDetails.CreditLimit = Convert.ToDecimal(xmlReader.GetAttribute("CreditLimit"));
                                    clsBranchContactDetails.ContactID = clsBranchContact.Insert(clsBranchContactDetails);
                                }

                                clsBranchProductDetails.BaseUnitCode = xmlReader.GetAttribute("BaseUnitCode");
                                clsBranchProductDetails.BaseUnitID = clsBranchUnit.Details(clsBranchProductDetails.BaseUnitCode).UnitID;
                                if (clsBranchProductDetails.BaseUnitID == 0)
                                {
                                    clsUnitDetails = new UnitDetails();
                                    clsUnitDetails.UnitCode = xmlReader.GetAttribute("BaseUnitCode");
                                    clsUnitDetails.UnitName = xmlReader.GetAttribute("BaseUnitName");
                                    clsBranchProductDetails.BaseUnitID = clsBranchUnit.Insert(clsUnitDetails);
                                }


                                clsBranchProductDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                                clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Details(clsBranchProductDetails.ProductGroupCode).ProductGroupID;
                                if (clsBranchProductDetails.ProductGroupID == 0)
                                {
                                    lblError.Text += "inserting product group....";
                                    clsBranchProductGroupDetails = new ProductGroupDetails();
                                    clsBranchProductGroupDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                                    clsBranchProductGroupDetails.ProductGroupName = xmlReader.GetAttribute("ProductGroupName");
                                    clsBranchProductGroupDetails.BaseUnitID = clsBranchProductDetails.BaseUnitID;
                                    clsBranchProductGroupDetails.Price = clsBranchProductDetails.Price;
                                    clsBranchProductGroupDetails.PurchasePrice = clsBranchProductDetails.PurchasePrice;
                                    clsBranchProductGroupDetails.IncludeInSubtotalDiscount = clsBranchProductDetails.IncludeInSubtotalDiscount;
                                    clsBranchProductGroupDetails.VAT = clsBranchProductDetails.VAT;
                                    clsBranchProductGroupDetails.EVAT = clsBranchProductDetails.EVAT;
                                    clsBranchProductGroupDetails.LocalTax = clsBranchProductDetails.LocalTax;
                                    clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Insert(clsBranchProductGroupDetails);
                                }

                                clsBranchProductDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                                clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Details(clsBranchProductDetails.ProductSubGroupCode).ProductSubGroupID;
                                if (clsBranchProductDetails.ProductSubGroupID == 0)
                                {
                                    lblError.Text += "inserting product sub-group....";
                                    clsBranchProductSubGroupDetails = new ProductSubGroupDetails();
                                    clsBranchProductSubGroupDetails.ProductGroupID = clsBranchProductDetails.ProductGroupID;
                                    clsBranchProductSubGroupDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                                    clsBranchProductSubGroupDetails.ProductSubGroupName = xmlReader.GetAttribute("ProductSubGroupName");
                                    clsBranchProductSubGroupDetails.BaseUnitID = clsBranchProductDetails.BaseUnitID;
                                    clsBranchProductSubGroupDetails.Price = clsBranchProductDetails.Price;
                                    clsBranchProductSubGroupDetails.PurchasePrice = clsBranchProductDetails.PurchasePrice;
                                    clsBranchProductSubGroupDetails.IncludeInSubtotalDiscount = clsBranchProductDetails.IncludeInSubtotalDiscount;
                                    clsBranchProductSubGroupDetails.VAT = clsBranchProductDetails.VAT;
                                    clsBranchProductSubGroupDetails.EVAT = clsBranchProductDetails.EVAT;
                                    clsBranchProductSubGroupDetails.LocalTax = clsBranchProductDetails.LocalTax;
                                    clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Insert(clsBranchProductSubGroupDetails);
                                }

                                clsBranchProductDetails.ProductID = clsBranchProduct.Insert(clsBranchProductDetails);
                                lngBranchProductID = clsBranchProductDetails.ProductID;
                                lngProductInserted++;

                                lblError.Text += " [done]. next item...<br>";
                            }
                            else if (xmlReader.Name == "Variation")
                            {
                                if (lngBranchProductID != 0)
                                {
                                    clsBranchProductVariationDetails = new ProductVariationDetails();

                                    clsBranchProductVariationDetails.VariationID = clsBranchProductVariation.Details(lngBranchProductID, xmlReader.GetAttribute("VariationCode")).VariationID;
                                    if (clsBranchProductVariationDetails.VariationID == 0)
                                    {
                                        clsBranchProductVariationDetails.ProductID = lngBranchProductID;
                                        clsBranchProductVariationDetails.VariationCode = xmlReader.GetAttribute("VariationCode");
                                        clsBranchProductVariationDetails.VariationType = xmlReader.GetAttribute("VariationType");

                                        clsBranchProductVariation.Insert(clsBranchProductVariationDetails);
                                    }
                                }
                            }
                            else
                            {
                                lblError.Text += "<b>" + xmlReader.Name + ":</b>" + xmlReader.Value + "<br>";
                            }
                            break;
                        case XmlNodeType.Text:
                            lblError.Text += "<b>" + xmlReader.LocalName + ":</b>" + xmlReader.Value + "<br>";
                            break;
                    }
                }
                xmlReader.Close();

                clsBranchInventory.CommitAndDispose();
                lblError.Text = "<b>" + lngProductInserted.ToString() + " out of " + lngProductCtr.ToString() + " has been successfully transferred.</b><br><br>" + lblError.Text;
            }
            else
            {
                Response.Write("Please select a file to upload. " + txtPath.FileName);
            }

        }

        private void Upload1()
        {
            //string fn = System.IO.Path.GetFileName(TextBox1.Text);
            //string SaveLocation = "/RetailPlus/temp/uploaded_" + fn;

            //System.IO.File.Copy(TextBox1.Text, SaveLocation);
            //txtPath.PostedFile.SaveAs(SaveLocation);
            XmlTextReader xmlReader = new XmlTextReader(TextBox1.Text);
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;

            Branch clsBranch = new Branch();
            BranchDetails clsBranchDetails = clsBranch.Details(Convert.ToInt16(cboBranchUpload.SelectedItem.Value.ToString()));
            clsBranch.CommitAndDispose();

            RemoteBranchInventory clsBranchInventory = new RemoteBranchInventory();
            clsBranchInventory.GetConnectionToBranch(clsBranchDetails.DBIP);

            Contacts clsBranchContact = new Contacts(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ContactDetails clsBranchContactDetails;

            ContactGroup clsBranchContactGroup = new ContactGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ContactGroupDetails clsContactGroupDetails;

            Data.Unit clsBranchUnit = new Data.Unit(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            UnitDetails clsUnitDetails;

            ProductGroup clsBranchProductGroup = new Data.ProductGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ProductGroupDetails clsBranchProductGroupDetails;

            ProductSubGroup clsBranchProductSubGroup = new Data.ProductSubGroup(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ProductSubGroupDetails clsBranchProductSubGroupDetails;

            //Data.Variation clsBranchVariation = new Variation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            Products clsBranchProduct = new Products(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ProductDetails clsBranchProductDetails;

            ProductVariation clsBranchProductVariation = new ProductVariation(clsBranchInventory.Connection, clsBranchInventory.Transaction);
            ProductVariationDetails clsBranchProductVariationDetails;

            long lngBranchProductID = 0; long lngProductCtr = 0; long lngProductInserted = 0;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:

                        if (xmlReader.Name == "Item")
                        {
                            lngProductCtr++;

                            clsBranchProductDetails = new ProductDetails();
                            clsBranchProductDetails.BarCode = xmlReader.GetAttribute("BarCode");
                            clsBranchProductDetails.ProductCode = xmlReader.GetAttribute("ProductCode");
                            lblError.Text += "Checking <b>" + clsBranchProductDetails.ProductCode + "</b> if exist... ";

                            //check product by barcode
                            clsBranchProductDetails.ProductID = clsBranchProduct.Details(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.BarCode).ProductID;
                            lngBranchProductID = clsBranchProductDetails.ProductID;
                            if (clsBranchProductDetails.ProductID != 0)
                            {
                                lblError.Text += " [Y] barcode exist... next item...<br>";
                                break;
                            }

                            //check product by product code
                            clsBranchProductDetails.ProductID = clsBranchProduct.Details(Constants.BRANCH_ID_MAIN, clsBranchProductDetails.ProductCode).ProductID;
                            lngBranchProductID = clsBranchProductDetails.ProductID;
                            if (clsBranchProductDetails.ProductID != 0)
                            {
                                clsBranchProduct.UpdateBarcode(clsBranchProductDetails.ProductID, clsBranchProductDetails.BarCode);
                                lblError.Text += " [Y] barcode not exist, product code exist. barcode updated. next item...<br>";
                                break;
                            }

                            lblError.Text += " [N] inserting product... ";

                            clsBranchProductDetails.BarCode = xmlReader.GetAttribute("BarCode");
                            clsBranchProductDetails.ProductDesc = xmlReader.GetAttribute("ProductDesc");
                            clsBranchProductDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                            clsBranchProductDetails.ProductGroupName = xmlReader.GetAttribute("ProductGroupName");
                            clsBranchProductDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                            clsBranchProductDetails.ProductSubGroupName = xmlReader.GetAttribute("ProductSubGroupName");
                            clsBranchProductDetails.BaseUnitCode = xmlReader.GetAttribute("BaseUnitCode");
                            clsBranchProductDetails.BaseUnitName = xmlReader.GetAttribute("BaseUnitName");
                            clsBranchProductDetails.DateCreated = DateTime.Now;
                            clsBranchProductDetails.Price = Convert.ToDecimal(xmlReader.GetAttribute("Price"));
                            clsBranchProductDetails.PurchasePrice = Convert.ToDecimal(xmlReader.GetAttribute("PurchasePrice"));
                            clsBranchProductDetails.IncludeInSubtotalDiscount = Convert.ToBoolean(xmlReader.GetAttribute("IncludeInSubtotalDiscount"));
                            clsBranchProductDetails.VAT = Convert.ToDecimal(xmlReader.GetAttribute("VAT"));
                            clsBranchProductDetails.EVAT = Convert.ToDecimal(xmlReader.GetAttribute("EVAT"));
                            clsBranchProductDetails.LocalTax = Convert.ToDecimal(xmlReader.GetAttribute("LocalTax"));
                            clsBranchProductDetails.Quantity = 0;
                            clsBranchProductDetails.MinThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MinThreshold"));
                            clsBranchProductDetails.MaxThreshold = Convert.ToDecimal(xmlReader.GetAttribute("MaxThreshold"));
                            clsBranchProductDetails.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), xmlReader.GetAttribute("OrderSlipPrinter"));
                            clsBranchProductDetails.ChartOfAccountIDPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDPurchase"));
                            clsBranchProductDetails.ChartOfAccountIDSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDSold"));
                            clsBranchProductDetails.ChartOfAccountIDInventory = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDInventory"));
                            clsBranchProductDetails.ChartOfAccountIDTaxPurchase = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxPurchase"));
                            clsBranchProductDetails.ChartOfAccountIDTaxSold = int.Parse(xmlReader.GetAttribute("ChartOfAccountIDTaxSold"));
                            clsBranchProductDetails.IsItemSold = Convert.ToBoolean(xmlReader.GetAttribute("IsItemSold"));
                            clsBranchProductDetails.WillPrintProductComposition = Convert.ToBoolean(xmlReader.GetAttribute("WillPrintProductComposition"));
                            clsBranchProductDetails.UpdatedBy = long.Parse(xmlReader.GetAttribute("UpdatedBy"));
                            clsBranchProductDetails.UpdatedOn = Convert.ToDateTime(xmlReader.GetAttribute("UpdatedOn"));
                            clsBranchProductDetails.PercentageCommision = decimal.Parse(xmlReader.GetAttribute("PercentageCommision"));
                            clsBranchProductDetails.QuantityIN = decimal.Parse(xmlReader.GetAttribute("QuantityIN"));
                            clsBranchProductDetails.QuantityOUT = decimal.Parse(xmlReader.GetAttribute("QuantityOUT"));

                            clsBranchProductDetails.SupplierCode = xmlReader.GetAttribute("ContactCode");
                            clsBranchProductDetails.SupplierID = clsBranchContact.Details(clsBranchProductDetails.SupplierCode).ContactID;
                            if (clsBranchProductDetails.SupplierID == 0)
                            {
                                clsBranchContactDetails = new ContactDetails();
                                clsBranchContactDetails.ContactGroupID = clsBranchContactGroup.Details(xmlReader.GetAttribute("ContactGroupCode")).ContactGroupID;
                                if (clsBranchContactDetails.ContactGroupID == 0)
                                {
                                    clsContactGroupDetails = new ContactGroupDetails();
                                    clsContactGroupDetails.ContactGroupCode = xmlReader.GetAttribute("ContactCode");
                                    clsContactGroupDetails.ContactGroupName = xmlReader.GetAttribute("ContactCode");
                                    clsContactGroupDetails.ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), xmlReader.GetAttribute("ContactGroupCategory"));
                                    clsBranchContactDetails.ContactGroupID = clsBranchContactGroup.Insert(clsContactGroupDetails);
                                }

                                clsBranchContactDetails.ContactCode = xmlReader.GetAttribute("ContactCode");
                                clsBranchContactDetails.ContactName = xmlReader.GetAttribute("ContactName");

                                clsBranchContactDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), xmlReader.GetAttribute("ModeOfTerms"));
                                clsBranchContactDetails.Terms = Convert.ToInt32(xmlReader.GetAttribute("Terms"));
                                clsBranchContactDetails.Address = xmlReader.GetAttribute("Address");
                                clsBranchContactDetails.BusinessName = xmlReader.GetAttribute("BusinessName");
                                clsBranchContactDetails.TelephoneNo = xmlReader.GetAttribute("TelephoneNo");
                                clsBranchContactDetails.Remarks = xmlReader.GetAttribute("Remarks");
                                clsBranchContactDetails.Debit = Convert.ToDecimal(xmlReader.GetAttribute("Debit"));
                                clsBranchContactDetails.Credit = Convert.ToDecimal(xmlReader.GetAttribute("Credit"));
                                clsBranchContactDetails.IsCreditAllowed = Convert.ToBoolean(xmlReader.GetAttribute("IsCreditAllowed"));
                                clsBranchContactDetails.CreditLimit = Convert.ToDecimal(xmlReader.GetAttribute("CreditLimit"));
                                clsBranchContactDetails.ContactID = clsBranchContact.Insert(clsBranchContactDetails);
                            }

                            clsBranchProductDetails.BaseUnitCode = xmlReader.GetAttribute("BaseUnitCode");
                            clsBranchProductDetails.BaseUnitID = clsBranchUnit.Details(clsBranchProductDetails.BaseUnitCode).UnitID;
                            if (clsBranchProductDetails.BaseUnitID == 0)
                            {
                                clsUnitDetails = new UnitDetails();
                                clsUnitDetails.UnitCode = xmlReader.GetAttribute("BaseUnitCode");
                                clsUnitDetails.UnitName = xmlReader.GetAttribute("BaseUnitName");
                                clsBranchProductDetails.BaseUnitID = clsBranchUnit.Insert(clsUnitDetails);
                            }


                            clsBranchProductDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                            clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Details(clsBranchProductDetails.ProductGroupCode).ProductGroupID;
                            if (clsBranchProductDetails.ProductGroupID == 0)
                            {
                                lblError.Text += "inserting product group....";
                                clsBranchProductGroupDetails = new ProductGroupDetails();
                                clsBranchProductGroupDetails.ProductGroupCode = xmlReader.GetAttribute("ProductGroupCode");
                                clsBranchProductGroupDetails.ProductGroupName = xmlReader.GetAttribute("ProductGroupName");
                                clsBranchProductGroupDetails.BaseUnitID = clsBranchProductDetails.BaseUnitID;
                                clsBranchProductGroupDetails.Price = clsBranchProductDetails.Price;
                                clsBranchProductGroupDetails.PurchasePrice = clsBranchProductDetails.PurchasePrice;
                                clsBranchProductGroupDetails.IncludeInSubtotalDiscount = clsBranchProductDetails.IncludeInSubtotalDiscount;
                                clsBranchProductGroupDetails.VAT = clsBranchProductDetails.VAT;
                                clsBranchProductGroupDetails.EVAT = clsBranchProductDetails.EVAT;
                                clsBranchProductGroupDetails.LocalTax = clsBranchProductDetails.LocalTax;
                                clsBranchProductDetails.ProductGroupID = clsBranchProductGroup.Insert(clsBranchProductGroupDetails);
                            }

                            clsBranchProductDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                            clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Details(clsBranchProductDetails.ProductSubGroupCode).ProductSubGroupID;
                            if (clsBranchProductDetails.ProductSubGroupID == 0)
                            {
                                lblError.Text += "inserting product sub-group....";
                                clsBranchProductSubGroupDetails = new ProductSubGroupDetails();
                                clsBranchProductSubGroupDetails.ProductGroupID = clsBranchProductDetails.ProductGroupID;
                                clsBranchProductSubGroupDetails.ProductSubGroupCode = xmlReader.GetAttribute("ProductSubGroupCode");
                                clsBranchProductSubGroupDetails.ProductSubGroupName = xmlReader.GetAttribute("ProductSubGroupName");
                                clsBranchProductSubGroupDetails.BaseUnitID = clsBranchProductDetails.BaseUnitID;
                                clsBranchProductSubGroupDetails.Price = clsBranchProductDetails.Price;
                                clsBranchProductSubGroupDetails.PurchasePrice = clsBranchProductDetails.PurchasePrice;
                                clsBranchProductSubGroupDetails.IncludeInSubtotalDiscount = clsBranchProductDetails.IncludeInSubtotalDiscount;
                                clsBranchProductSubGroupDetails.VAT = clsBranchProductDetails.VAT;
                                clsBranchProductSubGroupDetails.EVAT = clsBranchProductDetails.EVAT;
                                clsBranchProductSubGroupDetails.LocalTax = clsBranchProductDetails.LocalTax;
                                clsBranchProductDetails.ProductSubGroupID = clsBranchProductSubGroup.Insert(clsBranchProductSubGroupDetails);
                            }

                            clsBranchProductDetails.ProductID = clsBranchProduct.Insert(clsBranchProductDetails);
                            lngBranchProductID = clsBranchProductDetails.ProductID;
                            lngProductInserted++;

                            lblError.Text += " [done]. next item...<br>";
                        }
                        else if (xmlReader.Name == "Variation")
                        {
                            if (lngBranchProductID != 0)
                            {
                                clsBranchProductVariationDetails = new ProductVariationDetails();

                                clsBranchProductVariationDetails.VariationID = clsBranchProductVariation.Details(lngBranchProductID, xmlReader.GetAttribute("VariationCode")).VariationID;
                                if (clsBranchProductVariationDetails.VariationID == 0)
                                {
                                    clsBranchProductVariationDetails.ProductID = lngBranchProductID;
                                    clsBranchProductVariationDetails.VariationCode = xmlReader.GetAttribute("VariationCode");
                                    clsBranchProductVariationDetails.VariationType = xmlReader.GetAttribute("VariationType");

                                    clsBranchProductVariation.Insert(clsBranchProductVariationDetails);
                                }
                            }
                        }
                        else
                        {
                            lblError.Text += "<b>" + xmlReader.Name + ":</b>" + xmlReader.Value + "<br>";
                        }
                        break;
                    case XmlNodeType.Text:
                        lblError.Text += "<b>" + xmlReader.LocalName + ":</b>" + xmlReader.Value + "<br>";
                        break;
                }
            }
            xmlReader.Close();

            clsBranchInventory.CommitAndDispose();
            lblError.Text = "<b>" + lngProductInserted.ToString() + " out of " + lngProductCtr.ToString() + " has been successfully transferred.</b><br><br>" + lblError.Text;
        }

		#endregion


        protected void imgUpload_Click1(object sender, ImageClickEventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Upload1();
        }
}
}
