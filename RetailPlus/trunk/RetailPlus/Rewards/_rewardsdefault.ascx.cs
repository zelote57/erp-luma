using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Rewards
{
    public partial  class __RewardsDefault : System.Web.UI.UserControl
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


        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            LoadMembers();
        }
        private void LoadMembers()
        {
            ContactReward clsContactReward = new ContactReward();
            DataTable dtActiveStatisticsReport = clsContactReward.ActiveStatisticsReport(DateTime.Parse("2012-01-01"), DateTime.Parse("2012-02-28"));
            clsContactReward.CommitAndDispose();

            //cboCustomer.DataTextField = "ContactName";
            //cboCustomer.DataValueField = "ContactID";

            //string SearchKey = "%" + txtCustomer.Text;
            Chart1.DataSource = dtActiveStatisticsReport.DefaultView;
            Chart1.Series["Series1"].XValueMember = "RewardAwardDate";
            Chart1.Series["Series1"].YValueMembers = "NoOfInActiveRewards";

            Chart1.Series["Series2"].XValueMember = "RewardAwardDate";
            Chart1.Series["Series2"].YValueMembers = "NoOfActiveRewards";
            Chart1.DataBind();
            //cboCustomer.DataBind();
            

            //if (cboCustomer.Items.Count == 0) cboCustomer.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            //cboCustomer.SelectedIndex = 0;
        }
        private void LoadProduct()
        {
            Data.ProductColumns clsProductColumns = new Data.ProductColumns();
            clsProductColumns.ProductID = true;
            clsProductColumns.ProductCode = true;

            string strSearchKey = ""; // txtProductCode.Text.Trim();
            Data.ProductDetails clsSearchKeys = new Data.ProductDetails();
            clsSearchKeys.BarCode = strSearchKey;
            clsSearchKeys.BarCode2 = strSearchKey;
            clsSearchKeys.BarCode3 = strSearchKey;
            clsSearchKeys.ProductCode = strSearchKey;

            //Data.Product clsProduct = new Data.Product();
            //cboProductCode.DataTextField = "ProductCode";
            //cboProductCode.DataValueField = "ProductID";
            //cboProductCode.DataSource = clsProduct.ListAsDataTable(clsProductColumns, clsSearchKeys, ProductListFilterType.ShowActiveAndInactive, 0, System.Data.SqlClient.SortOrder.Ascending, 100, false, "ProductCode", SortOption.Ascending);
            //cboProductCode.DataBind();
            //clsProduct.CommitAndDispose();

            //if (cboProductCode.Items.Count == 0) cboProductCode.Items.Insert(0, new ListItem(Constants.PLEASE_SELECT, Constants.ZERO_STRING));
            //cboProductCode.SelectedIndex = 0;
        }
        private void SaveRecord()
        {
            
            
        }

        #endregion
        
    }
}
