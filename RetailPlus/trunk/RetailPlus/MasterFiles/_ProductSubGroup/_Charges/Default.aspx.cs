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

namespace AceSoft.RetailPlus.MasterFiles._ProductSubGroup._Charges
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page
	{



		protected void Page_Load(object sender, System.EventArgs e)
		{
			const string defaultHeading = "Master Files";
			string stHeading = defaultHeading;			

			const string defaultTitle = "List of Product Group Additional Charges";
			SiteTitle.Title = defaultTitle;

			const SearchCategoryID defaultSearchIndex = SearchCategoryID.Products;
			SearchCategoryID SearchIndex = defaultSearchIndex;			

			HorizontalNavBar.PageNavigatorid = HorizontalNavID.MasterFiles;
			
			ProductGroup clsProductGroup = new ProductGroup();
			ProductGroupDetails clsDetails = new ProductGroupDetails();
			
			Int32 id = Convert.ToInt32(Common.Decrypt(Request.QueryString["groupid"].ToString(),Session.SessionID));
			clsDetails = clsProductGroup.Details(id);

			clsProductGroup.CommitAndDispose();

			string groupCode = " for Group Code : " + clsDetails.ProductGroupName;

			if (Request.QueryString["task"]!=null)
			{
				string task = Common.Decrypt(Request.QueryString["task"].ToString(),Session.SessionID);
				switch(task)
				{
					case "add":
						stHeading = "Register New Additional Charge";	
						SearchIndex = SearchCategoryID.ProductGroupAdditionalCharges;
						ctrlInsert.Visible = true;
						break;
					case "edit":
						stHeading = "Modify Additional Charge";
						SearchIndex = SearchCategoryID.ProductGroupAdditionalCharges;
						ctrlUpdate.Visible = true;
						break;   
					case "list":
						stHeading = "Additional Charges List";		
						SearchIndex = SearchCategoryID.ProductGroupAdditionalCharges;
						ctrlList.Visible = true;
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
