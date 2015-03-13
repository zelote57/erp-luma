using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.MasterFiles._ContactGroup
{
    public partial class __ChangePriceLevel : System.Web.UI.UserControl
	{

		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
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
        protected void cmdGroup_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadProductGroup();
            cboGroup_SelectedIndexChanged(null, null);
        }
        protected void cboGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //LoadSubGroup();
        }
        
        protected void imgSavePriceLevel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveRecord(); 
        }
        protected void cmdSavePriceLevel_Click(object sender, EventArgs e)
        {
            SaveRecord(); 
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{
            cboGroup.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboGroup.SelectedIndex = 0;

            //string strproductcode = string.Empty;
            //try { strproductcode = Common.Decrypt(Request.QueryString["productcode"].ToString(), Session.SessionID); }
            //catch { }

            //if (strproductcode == string.Empty)
            //{
            //    cboProductCode.Items.Clear();
            //    cboProductCode.Items.Add(new ListItem(Constants.ALL, Constants.ZERO_STRING));
            //    cboProductCode.SelectedIndex = 0;
            //}
            //else{
            //    txtProductCode.Text = strproductcode;
            //    cmdProductCode_Click(null, null);
            //}

            //lblProductGroupID.Text = Constants.ZERO_STRING;
            //lblProductSubGroup.Text = Constants.ZERO_STRING;
            //lblProductID.Text = Constants.ZERO_STRING;
		}
        private void LoadProductGroup()
        {
            Data.ProductGroup clsProductGroup = new Data.ProductGroup();
            cboGroup.DataTextField = "ProductGroupName";
            cboGroup.DataValueField = "ProductGroupID";

            string stSearchKey = "%" + txtGroup.Text;
            cboGroup.DataSource = clsProductGroup.ListAsDataTable(stSearchKey, "ProductGroupName", SortOption.Ascending, 100);
            cboGroup.DataBind();
            clsProductGroup.CommitAndDispose();

            if (cboGroup.Items.Count == 0) cboGroup.Items.Insert(0, new ListItem(Constants.ALL, Constants.ZERO_STRING));
            cboGroup.SelectedIndex = 0;
        }
		private void SaveRecord()
		{
            //long lngProductGroupID = long.Parse(cboGroup.SelectedItem.Value);
            //long lngProductSubGroupID = long.Parse(cboProductSubGroup.SelectedItem.Value);
            //long lngProductID = long.Parse(cboProductCode.SelectedItem.Value);
            //decimal decRewardPoints = 0;
            //string javaScript;
            //try
            //{ decRewardPoints = decimal.Parse(txtRewardPoints.Text); }
            //catch 
            //{
            //    javaScript = "window.alert('Please enter a valid Reward Points.')";
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);
            //    return;
            //}

            //Products clsProduct = new Products();
            //clsProduct.UpdateRewardPoints(lngProductGroupID, lngProductSubGroupID, lngProductID, decRewardPoints);
            //clsProduct.CommitAndDispose();

            //javaScript = "window.alert('Reward Points has been updated.')";
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.updSave, this.updSave.GetType(), "openwindow", javaScript, true);

		}

		#endregion
        
    }
}
