namespace AceSoft.RetailPlus.MasterFiles._Product
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using AceSoft.RetailPlus.Data;

    public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Master Files: System prerequisite setups";
            SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.NotApplicable;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
				Common Common = new Common();
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
                        stHeading = "Create new product to buy, sell, or use as raw material.";
						SearchIndex = SearchCategoryID.Products;
						ctrlInsert.Visible = true;
						LargeHeading.Text = stHeading;
						break;
					case "edit":
                        stHeading = "Update existing product to buy, sell, or use as raw material.";
						SearchIndex = SearchCategoryID.Products;
						ctrlUpdate.Visible = true;
						break;
					case "compose":
                        stHeading = "Compose product using another product as raw material.";
						SearchIndex = SearchCategoryID.Products;
						ctrlCompose.Visible = true;
						break;
                    case "det":
                        stHeading = "Product detailed information.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlDetails.Visible = true;
                        ctrlUnitsMatrixList.Visible = true;
                        break;  
					case "details":
                        stHeading = "Product detailed information.";
						SearchIndex = SearchCategoryID.Products;
						ctrlDetails.Visible = true;
                        ctrlUnitsMatrixList.Visible = true;
						break;   
					case "list":
                        string strView = string.Empty;
                        try { strView = Common.Decrypt(Request.QueryString["view"].ToString(), Session.SessionID); }
                        catch { }

                        if (strView == "compacked")
                            ctrlList.Visible = true;
                        else
                            ctrlListDetailed.Visible = true;

						stHeading = "Products List";
						SearchIndex = SearchCategoryID.Products;
						break;	
					case "ProductPrice":
                        stHeading = "Product's price list";
						SearchIndex = SearchCategoryID.Products;
						ctrlList.Visible = true;
						break;
                    case "finance":
                        stHeading = "Update the information of product to be used for accounting purposes.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlFinance.Visible = true;
                        break;
                    case "changetax":
                        stHeading = "Change the existing VAT, EVAT, Local Tax of products.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlChangeTax.Visible = true;
                        break;	
                    case "addproductvariation":
                        stHeading = "Add Product Variation.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlAddProductVariation.Visible = true;
                        break;
                    case "changerewardpoints":
                        stHeading = "Change the Reward Points of Products.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlChangeRewardPoints.Visible = true;
                        break;
                    case "changeprice":
                        stHeading = "Change the price of specific product.";
                        SearchIndex = SearchCategoryID.Products;
                        ctrlChangePrice.Visible = true;
                        break;
                    case "synchronize":
                        stHeading = "Synchronize branch database to this master database.";
                        SearchIndex = SearchCategoryID.NotApplicable;
                        ctrlSynchronize.Visible = true;
                        break;
	                    
					default:	
						break;
				}
                LargeHeading.Text = stHeading;
				RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
