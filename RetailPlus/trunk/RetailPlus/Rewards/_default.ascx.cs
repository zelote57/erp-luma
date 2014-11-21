using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Rewards
{
    public partial  class __Default : System.Web.UI.UserControl
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


        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            // LoadMembers();
        }
        private void LoadMembers()
        {
            ContactReward clsContactReward = new ContactReward();
            DataTable dtActiveStatisticsReport = clsContactReward.ActiveStatisticsReport(DateTime.Now.AddMonths(-1), DateTime.Now);
            clsContactReward.CommitAndDispose();

            if (dtActiveStatisticsReport.Rows.Count == 0)
            {
                Chart1.Visible = false;
            }
            else
            {
                Chart1.Visible = true; 
                Chart1.DataSource = dtActiveStatisticsReport.DefaultView;

                Chart1.Series["Series1"].XValueMember = "RewardAwardDate";
                Chart1.Series["Series1"].YValueMembers = "TotalNoOfActiveRewards";

                Chart1.Series["Series2"].XValueMember = "RewardAwardDate";
                Chart1.Series["Series2"].YValueMembers = "NoOfActiveRewards";

                Chart1.Series["Series3"].XValueMember = "RewardAwardDate";
                Chart1.Series["Series3"].YValueMembers = "TotalNoOfInActiveRewards";

                Chart1.Series["Series4"].XValueMember = "RewardAwardDate";
                Chart1.Series["Series4"].YValueMembers = "NoOfInActiveRewards";

                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
                
                Chart1.Series["Series1"].LegendText = "Total No. Of ActiveRewards";
                Chart1.Series["Series2"].LegendText = "No. Of New ActiveRewards";
                Chart1.Series["Series3"].LegendText = "Total No. Of InActiveRewards";
                Chart1.Series["Series4"].LegendText = "No. Of InActiveRewards";

                Chart1.DataBind();
            }
        }
       
        #endregion
        
    }
}
