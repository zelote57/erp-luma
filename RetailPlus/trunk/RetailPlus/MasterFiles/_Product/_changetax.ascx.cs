using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._Product
{
	public partial  class __ChangeTax : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer.ToString();
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

		}
		#endregion

		#region Web Control Methods

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}
        protected void cmdProductGroup_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                LoadProductGroup();
                cboProductGroup_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboProductGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                LoadSubGroup();
                if (cboProductGroup.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    ProductGroup clsProductGroup = new ProductGroup();
                    ProductGroupDetails clsProductGroupDetails = clsProductGroup.Details(long.Parse(cboProductGroup.SelectedItem.Value));
                    clsProductGroup.CommitAndDispose();
                    txtVAT.Text = clsProductGroupDetails.VAT.ToString("#,##0.#0");
                    txtEVAT.Text = clsProductGroupDetails.EVAT.ToString("#,##0.#0");
                    txtLocalTax.Text = clsProductGroupDetails.LocalTax.ToString("#,##0.#0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmdProductSubGroup_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadSubGroup();
            cboProductSubGroup_SelectedIndexChanged(null, null);
        }
        protected void cboProductSubGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                LoadProduct();
                if (cboProductSubGroup.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    ProductSubGroup clsProductSubGroup = new ProductSubGroup();
                    ProductSubGroupDetails clsProductSubGroupDetails = clsProductSubGroup.Details(long.Parse(cboProductSubGroup.SelectedItem.Value));
                    clsProductSubGroup.CommitAndDispose();
                    txtVAT.Text = clsProductSubGroupDetails.VAT.ToString("#,##0.#0");
                    txtEVAT.Text = clsProductSubGroupDetails.EVAT.ToString("#,##0.#0");
                    txtLocalTax.Text = clsProductSubGroupDetails.LocalTax.ToString("#,##0.#0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmdProductCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadProduct();
        }
        protected void cboProductCode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cboProductCode.SelectedItem.Value != Constants.ZERO_STRING)
                {
                    Products clsProduct = new Products();
                    ProductDetails clsProductDetails = clsProduct.Details(long.Parse(cboProductCode.SelectedItem.Value));
                    clsProduct.CommitAndDispose();
                    txtVAT.Text = clsProductDetails.VAT.ToString("#,##0.#0");
                    txtEVAT.Text = clsProductDetails.EVAT.ToString("#,##0.#0");
                    txtLocalTax.Text = clsProductDetails.LocalTax.ToString("#,##0.#0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void imgSaveTax_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord(); 
        }
        protected void cmdSaveTax_Click(object sender, EventArgs e)
        {
            SaveRecord(); 
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            cboProductGroup.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductGroup.SelectedIndex = 0;
            cboProductSubGroup.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductSubGroup.SelectedIndex = 0;

            string strproductcode = string.Empty;
            try { strproductcode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
            catch { }

            if (strproductcode == string.Empty)
            {
                cboProductCode.Items.Clear();
                cboProductCode.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
                cboProductCode.SelectedIndex = 0;
            }
            else{
                txtProductCode.Text = strproductcode;
                cmdProductCode_Click(null, null);
            }

            lblProductGroupID.Text = Constants.ZERO_STRING;
            lblProductSubGroup.Text = Constants.ZERO_STRING;
            lblProductID.Text = Constants.ZERO_STRING;
		}
        private void LoadProductGroup()
        {
            Data.ProductGroup clsProductGroup = new Data.ProductGroup();
            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";

            string stSearchKey = "%" + txtProductGroup.Text;
            cboProductGroup.DataSource = clsProductGroup.SearchDataTable(stSearchKey, "ProductGroupName", SortOption.Ascending, 100);
            cboProductGroup.DataBind();
            clsProductGroup.CommitAndDispose();

            if (cboProductGroup.Items.Count == 0) cboProductGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductGroup.SelectedIndex = 0;
        }
        private void LoadSubGroup()
        {
            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupID = true;
            clsProductSubGroupColumns.ProductSubGroupName = true;

            ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
            clsSearchKeys.ProductSubGroupCode = txtProductSubGroup.Text;
            clsSearchKeys.ProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);

            Data.ProductSubGroup clsProductSubGroup = new Data.ProductSubGroup();
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsProductSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0, System.Data.SqlClient.SortOrder.Ascending, 0, Data.ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder.Ascending);
            cboProductSubGroup.DataBind();
            clsProductSubGroup.CommitAndDispose();

            if (cboProductSubGroup.Items.Count == 0) cboProductSubGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductSubGroup.SelectedIndex = 0;
        }
        private void LoadProduct()
        {
            string strSearchKey = txtProductCode.Text.Trim();
            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = strSearchKey;
            clsSearchKeys.BarCode2 = strSearchKey;
            clsSearchKeys.BarCode3 = strSearchKey;
            clsSearchKeys.ProductCode = strSearchKey;
            clsSearchKeys.ProductSubGroupID = long.Parse(cboProductSubGroup.SelectedItem.Value);
            clsSearchKeys.ProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);

            Data.Products clsProduct = new Data.Products();
            cboProductCode.DataTextField = "ProductCode";
            cboProductCode.DataValueField = "ProductID";
            cboProductCode.DataSource = clsProduct.ListAsDataTable(clsSearchKeys, ProductListFilterType.ShowInactiveOnly, 0, System.Data.SqlClient.SortOrder.Ascending, 100, false, "ProductCode", SortOption.Ascending);
            cboProductCode.DataBind();
            clsProduct.CommitAndDispose();

            if (cboProductCode.Items.Count == 0) cboProductCode.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboProductCode.SelectedIndex = 0;
        }
		private void SaveRecord()
		{
            long lngProductGroupID = long.Parse(cboProductGroup.SelectedItem.Value);
            long lngProductSubGroupID = long.Parse(cboProductSubGroup.SelectedItem.Value);
            long lngProductID = long.Parse(cboProductCode.SelectedItem.Value);
            decimal decVAT = 0; decimal decEVAT = 0; decimal decLocalTax = 0;
            string stScript;
            try
            { decVAT = decimal.Parse(txtVAT.Text); }
            catch 
            {
                stScript = "<Script>";
                stScript += "window.alert('Please enter a valid VAT.')";
                stScript += "</Script>";
                Response.Write(stScript);	
                return; 
            }
            try
            { decEVAT = decimal.Parse(txtEVAT.Text); }
            catch
            {
                stScript = "<Script>";
                stScript += "window.alert('Please enter a valid EVAT.')";
                stScript += "</Script>";
                Response.Write(stScript);
                return;
            }
            try
            { decLocalTax = decimal.Parse(txtLocalTax.Text); }
            catch
            {
                stScript = "<Script>";
                stScript += "window.alert('Please enter a valid LocalTax.')";
                stScript += "</Script>";
                Response.Write(stScript);
                return;
            }
            if (lngProductID != 0)
            {
                Products clsProduct = new Products();
                clsProduct.ChangeTax(0, 0, lngProductID, decVAT, decEVAT, decLocalTax);
                clsProduct.CommitAndDispose();
            }
            else if (lngProductSubGroupID != 0)
            {
                ProductSubGroup clsProductSubGroup = new ProductSubGroup();
                clsProductSubGroup.ChangeTax(0, lngProductSubGroupID, decVAT, decEVAT, decLocalTax);
                clsProductSubGroup.CommitAndDispose();
            }
            else
            {
                ProductGroup clsProductGroup = new ProductGroup();
                clsProductGroup.ChangeTax(lngProductGroupID, decVAT, decEVAT, decLocalTax);
                clsProductGroup.CommitAndDispose();
            }

            txtVAT.Text = decVAT.ToString("#,##0.#0");
            txtEVAT.Text = decEVAT.ToString("#,##0.#0");
            txtLocalTax.Text = decLocalTax.ToString("#,##0.#0");

            stScript = "<Script>";
            stScript += "window.alert('Reward points has been updated.')";
            stScript += "</Script>";
            Response.Write(stScript);
            
		}

		#endregion
        
    }
}
