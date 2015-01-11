using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.SalesAndReceivables._CreditMemo
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
            if (!IsPostBack && Visible)
            {
                lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
                LoadOptions();
                GenerateHTML();
            }
        }

        private void LoadOptions()
        {

        }

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/CreditMemoReport.rpt"));

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_creditmemo";
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
                if (Request.QueryString["task"].ToString().ToLower() == "reportfromposted" && Request.QueryString["retid"].ToString() != null)
                { iID = Convert.ToInt64(Request.QueryString["retid"].ToString()); }
                else
                { iID = Convert.ToInt64(Common.Decrypt(Request.QueryString["retid"].ToString(), Session.SessionID)); }
                lblReferrer.ToolTip = iID.ToString();
            }
            catch { iID = long.Parse(lblReferrer.ToolTip); }

            ReportDataset rptds = new ReportDataset();

            CreditMemos clsCreditMemos = new CreditMemos();
            MySqlDataReader myreader = clsCreditMemos.List(iID, "CreditMemoID", SortOption.Ascending);

            while (myreader.Read())
            {
                DataRow drNew = rptds.CreditMemo.NewRow();

                foreach (DataColumn dc in rptds.CreditMemo.Columns)
                    drNew[dc] = "" + myreader[dc.ColumnName];

                rptds.CreditMemo.Rows.Add(drNew);
            }
            myreader.Close();

            CreditMemoItems clsCreditMemoItems = new CreditMemoItems(clsCreditMemos.Connection, clsCreditMemos.Transaction);
            System.Data.DataTable dt = clsCreditMemoItems.ListAsDataTable(iID);
            foreach(System.Data.DataRow dr in dt.Rows)
            {
                DataRow drNew = rptds.CreditMemoItem.NewRow();

                foreach (DataColumn dc in rptds.CreditMemoItem.Columns)
                    drNew[dc] = "" + dr[dc.ColumnName];

                rptds.CreditMemoItem.Rows.Add(drNew);
            }
            clsCreditMemos.CommitAndDispose();

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
            InitializeComponent();
            base.OnInit(e);
        }

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
