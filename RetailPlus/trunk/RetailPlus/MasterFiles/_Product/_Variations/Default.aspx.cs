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

namespace AceSoft.RetailPlus.MasterFiles._Product._Variations
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{


		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "Product Variation Types";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Products;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
                Products clsProduct = new Products();
                ProductDetails clsDetails = new ProductDetails();

                long id = long.Parse(Common.Decrypt(Request.QueryString["prodid"].ToString(), Session.SessionID));
                clsDetails = clsProduct.Details(Constants.BRANCH_ID_MAIN, id);

                clsProduct.CommitAndDispose();

                string productCode = " for : " + clsDetails.ProductCode;

				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Variation";	
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Variation";
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Variations List ";		
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlList.Visible = true;
						break;		
					default:	
						break;
				}

				LargeHeading.Text = stHeading + productCode;
				RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
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
