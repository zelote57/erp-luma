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

namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._VariationsMatrix
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Product Sub Group Variation Combinations";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ProductSubGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
                ProductGroup clsProductGroup = new ProductGroup();
                ProductGroupDetails clsDetails = new ProductGroupDetails();

                int id = Convert.ToInt16(Common.Decrypt(Request.QueryString["subgroupid"].ToString(), Session.SessionID));
                clsDetails = clsProductGroup.Details(id);

                clsProductGroup.CommitAndDispose();
                string groupCode = " for Product Sub Group Code : " + clsDetails.ProductGroupName;

				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Create New Product Sub Group Variation Combination";	
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Product Sub Group Variation Combination";
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Product Sub Group Variation Combinations List";		
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlList.Visible = true;
						break;
                    case "details":
                        stHeading = "Sub Group Variation Details";
                        SearchIndex = SearchCategoryID.ProductVariations;
                        ctrlDetails.Visible = true;
                        break; 
					default:	
						break;
				}

				LargeHeading.Text = stHeading + groupCode;
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
