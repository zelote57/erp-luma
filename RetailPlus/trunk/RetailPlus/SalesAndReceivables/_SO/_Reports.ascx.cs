using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.SalesAndReceivables._SO
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __Reports : System.Web.UI.UserControl
	{

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
                if (Request.QueryString["reporttype"] != null)
                    lblReportType.Text = Common.Decrypt(Request.QueryString["reporttype"].ToString(), Session.SessionID);
                else
                    lblReportType.Text = "SOReport";

                if (Visible)
                {
                    LoadOptions();
                    GenerateHTML();
                }
            }
        }

        private void LoadOptions()
        {
        }

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            string strReportType = lblReportType.Text;
            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/" + strReportType + ".rpt"));

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_salesorder";
                CRSHelper.GenerateReport(strFileName, rpt, this.updPrint, pvtExportFormatType);
            }
            else
            {
                CRViewer.ReportSource = rpt;
                Session["ReportDocument"] = rpt;
            }
        }

        #endregion

        #region GeneratePDF
        private void GeneratePDF() { Export(ExportFormatType.PortableDocFormat); }
        #endregion

        #region GenerateWord
        private void GenerateWord() { Export(ExportFormatType.WordForWindows); }
        #endregion
        
        #region GenerateExcel
        private void GenerateExcel() { Export(ExportFormatType.Excel); }
        #endregion

        #region GenerateHTML
        private void GenerateHTML() { Export(ExportFormatType.HTML40); }
        #endregion

        #region SetDataSource

        private void SetDataSource(ReportDocument Report)
        {
            long iID = 0;
            try
            {
                if (Request.QueryString["task"].ToString().ToLower() == "reportfromposted" && Request.QueryString["soid"].ToString() != null)
                { iID = Convert.ToInt64(Request.QueryString["soid"].ToString()); }
                else { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["soid"].ToString(), Session.SessionID)); }
                lblReferrer.ToolTip = iID.ToString();
            }
            catch { iID = long.Parse(lblReferrer.ToolTip); }

            ReportDataset rptds = new ReportDataset();

            SO clsSO = new SO();
            MySqlDataReader myreader = clsSO.List(iID, "SOID", SortOption.Ascending);
            
            while (myreader.Read())
            {
                DataRow drNew = rptds.SO.NewRow();

                foreach (DataColumn dc in rptds.SO.Columns)
                    drNew[dc] = "" + myreader[dc.ColumnName];

                rptds.SO.Rows.Add(drNew);
            }
            myreader.Close();

            SOItem clsSOItem = new SOItem(clsSO.Connection, clsSO.Transaction);
            System.Data.DataTable dt = clsSOItem.ListAsDataTable(iID);
            foreach(System.Data.DataRow dr in dt.Rows)
            {
                DataRow drNew = rptds.SOItems.NewRow();

                foreach (DataColumn dc in rptds.SOItems.Columns)
                    drNew[dc] = "" + dr[dc.ColumnName];

                rptds.SOItems.Rows.Add(drNew);
            }
            clsSO.CommitAndDispose();

            Report.SetDataSource(rptds);
            SetParameters(Report);
        }

        #endregion

        #region SetParameters
        private void SetParameters(ReportDocument Report)
        {
            ParameterFieldDefinition paramField;
            ParameterValues currentValues;
            ParameterDiscreteValue discreteParam;

            paramField = Report.DataDefinition.ParameterFields["CompanyName"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = CompanyDetails.CompanyName;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["PrintedBy"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = Session["Name"].ToString();
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);
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

        protected void imgView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            switch (Convert.ToInt16(cboReportOptions.SelectedItem.Value))
            {
                case 0:
                    GenerateHTML();
                    break;
                case 1:
                    GeneratePDF();
                    break;
                case 2:
                    GenerateWord();
                    break;
                case 3:
                    GenerateExcel();
                    break;
            }
        }
        protected void cmdView_Click(object sender, System.EventArgs e)
        {
            switch (Convert.ToInt16(cboReportOptions.SelectedItem.Value))
            {
                case 0:
                    GenerateHTML();
                    break;
                case 1:
                    GeneratePDF();
                    break;
                case 2:
                    GenerateWord();
                    break;
                case 3:
                    GenerateExcel();
                    break;
            }
        }
        protected void imgBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }
        protected void cmdBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(lblReferrer.Text);
        }

        #endregion

	}
}
