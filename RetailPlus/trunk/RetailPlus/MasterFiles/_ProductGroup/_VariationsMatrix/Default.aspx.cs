namespace AceSoft.RetailPlus.MasterFiles._ProductGroup._VariationsMatrix
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
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Product Group Variation Combinations";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ProductGroupVariations;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
                ProductGroup clsProductGroup = new ProductGroup();
                ProductGroupDetails clsDetails = new ProductGroupDetails();

                long id = Convert.ToInt64(Common.Decrypt(Request.QueryString["groupid"].ToString(), Session.SessionID));
                clsDetails = clsProductGroup.Details(id);

                clsProductGroup.CommitAndDispose();

                string groupCode = " for Product Group Code : " + clsDetails.ProductGroupName;

				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Product Group Variation Combination";	
						SearchIndex = SearchCategoryID.ProductGroupVariationsMatrix;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Product Group Variation Combination";
						SearchIndex = SearchCategoryID.ProductGroupVariationsMatrix;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Product Group Variation Combinations List";		
						SearchIndex = SearchCategoryID.ProductGroupVariationsMatrix;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Product Group Variation Combination details";
                        SearchIndex = SearchCategoryID.ProductGroupVariationsMatrix;
                        ctrlDetails.Visible = true;
                        break;  
					default:	
						break;
				}

				LargeHeading.Text = stHeading + groupCode;
				RightBodySectionSearch.SearchIDSelectedItem = SearchIndex;
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

	}
}
