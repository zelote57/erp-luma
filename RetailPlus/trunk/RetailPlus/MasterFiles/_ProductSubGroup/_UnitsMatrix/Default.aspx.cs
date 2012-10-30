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

namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._UnitsMatrix
{
	public partial class _Default : System.Web.UI.Page
	{

		#region Web Form Control

		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Product SubGroup Unit Matrix";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.ProductSubGroups;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			if (Request.QueryString["task"]!=null)
			{
                ProductSubGroup clsProductSubGroup = new ProductSubGroup();
                ProductSubGroupDetails clsDetails = new ProductSubGroupDetails();

                int id = Convert.ToInt16(Common.Decrypt(Request.QueryString["subgroupid"].ToString(), Session.SessionID));
                clsDetails = clsProductSubGroup.Details(id);

                clsProductSubGroup.CommitAndDispose();

                string groupCode = " for Product SubGroup : " + clsDetails.ProductSubGroupName;

				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Register New Unit Matrix";	
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Unit Matrix";
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Unit Matrix List";		
						SearchIndex = SearchCategoryID.ProductVariations;
						ctrlList.Visible = true;
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
