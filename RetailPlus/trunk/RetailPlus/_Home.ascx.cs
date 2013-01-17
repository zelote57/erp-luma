using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus
{
    public partial  class __Home : System.Web.UI.UserControl
    {

        #region Web Form Methods

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack && Visible)
            {
                //lblReferrer.Text = Request.UrlReferrer.ToString();
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
            //LoadMembers();
        }
        //private void LoadMembers()
        //{
        //    DateTime DateFrom = Convert.ToDateTime(DateTime.Today.ToShortDateString() + " 00:00");
        //    DateTime DateTo = Convert.ToDateTime(DateTime.Today.ToShortDateString() + " 23:59");

        //    Data.TerminalReport clsTerminalReport = new Data.TerminalReport();
        //    System.Data.DataTable dtHourlyReport = clsTerminalReport.HourlyReport(Constants.TerminalBranchID, Constants.ALL);

        //    SalesTransactionItems clsSalesTransactionItemsMost = new SalesTransactionItems(clsTerminalReport.Connection, clsTerminalReport.Transaction);
        //    System.Data.DataTable dtMostSaleable = clsSalesTransactionItemsMost.MostSalableItems(DateFrom, DateTo, 10);

        //    clsTerminalReport.CommitAndDispose();

        //    if (dtHourlyReport.Rows.Count == 0)
        //    {
        //        Chart1.Visible = false;
        //    }
        //    else
        //    {
        //        Chart1.Visible = true;
        //        Chart1.DataSource = dtHourlyReport.DefaultView;
        //        Chart1.Series["Series1"].XValueMember = "Time";
        //        Chart1.Series["Series1"].YValueMembers = "TranCount";

        //        Chart1.Series["Series2"].XValueMember = "TransactionDate";
        //        Chart1.Series["Series2"].YValueMembers = "Amount";

        //        Chart1.Series["Series3"].XValueMember = "Time";
        //        Chart1.Series["Series3"].YValueMembers = "Discount";

        //        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
        //        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;

        //        Chart1.Series["Series1"].LegendText = "No. Of Transactions";
        //        Chart1.Series["Series2"].LegendText = "Sales Amount";
        //        Chart1.Series["Series3"].LegendText = "Discounts";

        //        Chart1.DataBind();
        //    }

        //    if (dtMostSaleable.Rows.Count == 0)
        //    {
        //        Chart2.Visible = false;
        //    }
        //    else
        //    {
        //        Chart2.DataSource = dtMostSaleable.DefaultView;
        //        Chart2.Series["Series1"].XValueMember = "ProductCode";
        //        Chart2.Series["Series1"].YValueMembers = "Count";
        //        Chart2.Series["Series1"].Label = "#PERCENT{P0}";
        //        Chart2.Series["Series1"].LegendText = "#VALX : #VALY";

        //        Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
        //        Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;

        //        Chart2.DataBind();
        //    }
        //}
       
        #endregion
        
    }
}
