namespace AceSoft.RetailPlus.GeneralLedger._Setup
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;

    public partial class __ProductFinancialSetup : System.Web.UI.UserControl
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
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        protected void imgSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        
        protected void cmdSaveBack_Click(object sender, EventArgs e)
        {
            SaveRecord();
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

        protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }
        
        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?task=" + Common.Encrypt("list", Session.SessionID));
        }

        protected void cboProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
            ProductSubGroup clsProductSubGroup = new ProductSubGroup();
            clsProductSubGroup.GetConnection();
            ProductGroup clsProductGroup = new ProductGroup(clsProductSubGroup.Connection, clsProductSubGroup.Transaction);
            ProductGroupDetails clsDetails;
            //System.Data.DataTable dtProductSubGroup;

            if (lProductGroupID == 0)
            {
                clsDetails = clsProductGroup.Details(DataConstants.DEFAULT_PRODUCT_GROUP);
            }
            else
            {
                clsDetails = clsProductGroup.Details(lProductGroupID);
            }

            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupName = true;
            ProductSubGroupDetails clsSearchKeys = new ProductSubGroupDetails();
            clsSearchKeys.ProductGroupID = lProductGroupID;

            ProductSubGroup clsSubGroup = new ProductSubGroup(clsProductSubGroup.Connection, clsProductSubGroup.Transaction);
            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, clsSearchKeys, 0, System.Data.SqlClient.SortOrder.Ascending, 0, ProductSubGroupColumnNames.ProductSubGroupName, System.Data.SqlClient.SortOrder.Ascending).DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.Items.Insert(0, new ListItem("Do not Apply to Product Sub Groups", "-1"));
            cboProductSubGroup.Items.Insert(1, new ListItem("Apply to all Product Sub Groups", "0"));
            cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;
            
            clsProductSubGroup.CommitAndDispose();

            cboChartOfAccountPurchase.SelectedIndex = cboChartOfAccountPurchase.Items.IndexOf(cboChartOfAccountPurchase.Items.FindByValue(clsDetails.ChartOfAccountIDPurchase.ToString()));
            cboChartOfAccountSold.SelectedIndex = cboChartOfAccountSold.Items.IndexOf(cboChartOfAccountSold.Items.FindByValue(clsDetails.ChartOfAccountIDSold.ToString()));
            cboChartOfAccountInventory.SelectedIndex = cboChartOfAccountInventory.Items.IndexOf(cboChartOfAccountInventory.Items.FindByValue(clsDetails.ChartOfAccountIDInventory.ToString()));
            cboChartOfAccountIDTaxPurchase.SelectedIndex = cboChartOfAccountIDTaxPurchase.Items.IndexOf(cboChartOfAccountIDTaxPurchase.Items.FindByValue(clsDetails.ChartOfAccountIDTaxPurchase.ToString()));
            cboChartOfAccountIDTaxSold.SelectedIndex = cboChartOfAccountIDTaxSold.Items.IndexOf(cboChartOfAccountIDTaxSold.Items.FindByValue(clsDetails.ChartOfAccountIDTaxSold.ToString()));

        }
		#endregion

		#region Private Methods
		private void LoadOptions()
		{
            
            DataClass clsDataClass = new DataClass();
            ChartOfAccounts clsChartOfAccount = new ChartOfAccounts();
            System.Data.DataTable dtList = clsDataClass.DataReaderToDataTable(clsChartOfAccount.List("ChartOfAccountName", SortOption.Ascending));

            ProductGroup clsProductGroup = new ProductGroup(clsChartOfAccount.Connection, clsChartOfAccount.Transaction);
            System.Data.DataTable dtProductGroup = clsProductGroup.ListAsDataTable(SortField:"ProductGroupID");

            ProductSubGroupColumns clsProductSubGroupColumns = new ProductSubGroupColumns();
            clsProductSubGroupColumns.ProductSubGroupName = true;
            ProductSubGroup clsSubGroup = new ProductSubGroup(clsChartOfAccount.Connection, clsChartOfAccount.Transaction);
            System.Data.DataTable dtProductSubGroup = clsSubGroup.ListAsDataTable(clsProductSubGroupColumns, new ProductSubGroupDetails(), 0, System.Data.SqlClient.SortOrder.Ascending, 0, ProductSubGroupColumnNames.ProductSubGroupID, System.Data.SqlClient.SortOrder.Ascending);

            Products clsProduct = new Products(clsChartOfAccount.Connection, clsChartOfAccount.Transaction);
            System.Data.DataTable dtProduct = clsProduct.ListAsDataTable("ProductID", SortOption.Ascending);

            clsChartOfAccount.CommitAndDispose();

            cboProductGroup.DataTextField = "ProductGroupName";
            cboProductGroup.DataValueField = "ProductGroupID";
            cboProductGroup.DataSource = dtProductGroup.DefaultView;
            cboProductGroup.DataBind();
            cboProductGroup.Items.Add(new ListItem("Apply to all Product Groups", "0"));
            cboProductGroup.SelectedIndex = cboProductGroup.Items.Count - 1;

            cboProductSubGroup.DataTextField = "ProductSubGroupName";
            cboProductSubGroup.DataValueField = "ProductSubGroupID";
            cboProductSubGroup.DataSource = dtProductSubGroup.DefaultView;
            cboProductSubGroup.DataBind();
            cboProductSubGroup.Items.Add(new ListItem("Do not Apply to Product Sub Groups", "-1"));
            cboProductSubGroup.Items.Add(new ListItem("Apply to all Product Sub Groups", "0"));
            cboProductSubGroup.SelectedIndex = cboProductSubGroup.Items.Count - 1;

            cboProduct.DataTextField = "ProductCode";
            cboProduct.DataValueField = "ProductID";
            cboProduct.DataSource = dtProduct.DefaultView;
            cboProduct.DataBind();
            cboProduct.Items.Add(new ListItem("Do not Apply to Products", "-1"));
            cboProduct.Items.Add(new ListItem("Apply to all Products", "0"));
            cboProduct.SelectedIndex = cboProduct.Items.Count - 1;

            cboChartOfAccountPurchase.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountPurchase.DataValueField = "ChartOfAccountID";
            cboChartOfAccountPurchase.DataSource = dtList.DefaultView;
            cboChartOfAccountPurchase.DataBind();
            cboChartOfAccountPurchase.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountPurchase.SelectedIndex = cboChartOfAccountPurchase.Items.Count - 1;

            cboChartOfAccountSold.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountSold.DataValueField = "ChartOfAccountID";
            cboChartOfAccountSold.DataSource = dtList.DefaultView;
            cboChartOfAccountSold.DataBind();
            cboChartOfAccountSold.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountSold.SelectedIndex = cboChartOfAccountSold.Items.Count - 1;

            cboChartOfAccountInventory.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountInventory.DataValueField = "ChartOfAccountID";
            cboChartOfAccountInventory.DataSource = dtList.DefaultView;
            cboChartOfAccountInventory.DataBind();
            cboChartOfAccountInventory.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountInventory.SelectedIndex = cboChartOfAccountInventory.Items.Count - 1;

            cboChartOfAccountIDTaxPurchase.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountIDTaxPurchase.DataValueField = "ChartOfAccountID";
            cboChartOfAccountIDTaxPurchase.DataSource = dtList.DefaultView;
            cboChartOfAccountIDTaxPurchase.DataBind();
            cboChartOfAccountIDTaxPurchase.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountIDTaxPurchase.SelectedIndex = cboChartOfAccountIDTaxPurchase.Items.Count - 1;

            cboChartOfAccountIDTaxSold.DataTextField = "ChartOfAccountNameComplete";
            cboChartOfAccountIDTaxSold.DataValueField = "ChartOfAccountID";
            cboChartOfAccountIDTaxSold.DataSource = dtList.DefaultView;
            cboChartOfAccountIDTaxSold.DataBind();
            cboChartOfAccountIDTaxSold.Items.Add(new ListItem("Not Applicable", "0"));
            cboChartOfAccountIDTaxSold.SelectedIndex = cboChartOfAccountIDTaxSold.Items.Count - 1;
		}

		private void LoadRecord()
		{
            cboProductGroup_SelectedIndexChanged(null, null);
		}

		private void SaveRecord()
		{
            long lProductGroupID = Convert.ToInt64(cboProductGroup.SelectedItem.Value);
            long lProductSubGroupID = Convert.ToInt64(cboProductSubGroup.SelectedItem.Value);
            long lProductID = Convert.ToInt64(cboProduct.SelectedItem.Value);

            int iChartOfAccountIDPurchase = Convert.ToInt32(cboChartOfAccountPurchase.SelectedItem.Value);
            int iChartOfAccountIDSold = Convert.ToInt32(cboChartOfAccountSold.SelectedItem.Value);
            int iChartOfAccountIDInventory = Convert.ToInt32(cboChartOfAccountInventory.SelectedItem.Value);
            int iChartOfAccountIDTaxPurchase = Convert.ToInt32(cboChartOfAccountIDTaxPurchase.SelectedItem.Value);
            int iChartOfAccountIDTaxSold = Convert.ToInt32(cboChartOfAccountIDTaxSold.SelectedItem.Value);

            ProductGroup clsProductGroup = new ProductGroup();
            clsProductGroup.GetConnection();
            ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsProductGroup.Connection, clsProductGroup.Transaction);
            Products clsProduct = new Products(clsProductGroup.Connection,clsProductGroup.Transaction);

            if (lProductGroupID == 0)
            {
                clsProductGroup.UpdateFinancialInformation(iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                if (lProductSubGroupID == 0)
                {
                    clsProductSubGroup.UpdateFinancialInformation(iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    if (lProductID == 0)
                    {
                        clsProduct.UpdateFinancialInformation(iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                    else
                    {
                        clsProduct.UpdateFinancialInformation(lProductID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);        
                    }
                }
                else if (lProductSubGroupID > 0)
                {
                    clsProductSubGroup.UpdateFinancialInformation(lProductSubGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    if (lProductID == 0)
                    {
                        clsProduct.UpdateFinancialInformationBySubGroup(lProductSubGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                    else
                    {
                        clsProduct.UpdateFinancialInformation(lProductID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                }
            }
            else
            {
                clsProductGroup.UpdateFinancialInformation(lProductGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                if (lProductSubGroupID == 0)
                {
                    clsProductSubGroup.UpdateFinancialInformationByGroup(lProductGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    if (lProductID == 0)
                    {
                        clsProduct.UpdateFinancialInformationByGroup(lProductGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                    else
                    {
                        clsProduct.UpdateFinancialInformation(lProductID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                }
                else if (lProductSubGroupID > 0)
                {
                    clsProductSubGroup.UpdateFinancialInformation(lProductSubGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    if (lProductID == 0)
                    {
                        clsProduct.UpdateFinancialInformationBySubGroup(lProductSubGroupID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                    else
                    {
                        clsProduct.UpdateFinancialInformation(lProductID, iChartOfAccountIDPurchase, iChartOfAccountIDSold, iChartOfAccountIDInventory, iChartOfAccountIDTaxPurchase, iChartOfAccountIDTaxSold);
                    }
                }
            }
            
            clsProductGroup.CommitAndDispose();
		}
		#endregion

    }
}