using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using RetailPlus.Datasets;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Reports
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial  class __ContactsReport : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack && Visible)
			{
				lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
				LoadOptions();
                Session["ReportDocument"] = null;
                Session["ReportType"] = "contacts";
            }
        }

        protected void Page_Init(object sender, System.EventArgs e)
        {
            if (Session["ReportDocument"] != null && Session["ReportType"] != null)
            {
                if (Session["ReportType"].ToString() == "contacts")
                    CRViewer.ReportSource = (ReportDocument)Session["ReportDocument"];
            }
        }
		private void LoadOptions()
		{
			DataClass clsDataClass = new DataClass();

			Contacts clsContact = new Contacts();

			cboContactCode.DataTextField = "ContactCode";
			cboContactCode.DataValueField = "ContactCode";
			cboContactCode.DataSource = clsContact.ListAsDataTable("ContactCode",SortOption.Ascending);
			cboContactCode.DataBind();
			cboContactCode.Items.Add( new ListItem("All Codes","0"));
			cboContactCode.SelectedIndex = cboContactCode.Items.Count - 1;

			cboContactName.DataTextField = "ContactName";
			cboContactName.DataValueField = "ContactName";
			cboContactName.DataSource = clsContact.ListAsDataTable("ContactName",SortOption.Ascending);
			cboContactName.DataBind();
			cboContactName.Items.Add( new ListItem("All Contacts","0"));
			cboContactName.SelectedIndex = cboContactName.Items.Count - 1;
			
			clsContact.CommitAndDispose();
			
			ContactGroups clsContactGroup = new ContactGroups();
			cboGroup.DataTextField = "ContactGroupName";
			cboGroup.DataValueField = "ContactGroupID";
			cboGroup.DataSource = clsDataClass.DataReaderToDataTable(clsContactGroup.List("ContactGroupName",SortOption.Ascending));
			cboGroup.DataBind();
			cboGroup.Items.Add( new ListItem("All Contact Groups","0"));
			cboGroup.SelectedIndex = cboGroup.Items.Count - 1;

			clsContactGroup.CommitAndDispose();

			cboDeleted.SelectedIndex = cboDeleted.Items.Count - 1;
		}

        private ReportDocument getReportDocument()
        {
            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath(Constants.ROOT_DIRECTORY + "/Reports/_ContactsReport.rpt"));

            return rpt;
        }

        #region Export

        private void Export(ExportFormatType pvtExportFormatType)
        {
            ReportDocument rpt = getReportDocument();

            SetDataSource(rpt);

            if (pvtExportFormatType == ExportFormatType.WordForWindows || pvtExportFormatType == ExportFormatType.Excel || pvtExportFormatType == ExportFormatType.PortableDocFormat)
            {
                string strFileName = Session["UserName"].ToString() + "_contacts";
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
            ContactGroupCategory enumContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), cboContactGroupCategory.SelectedValue);

			ReportDataset rptds = new ReportDataset();

			Contacts clsContact = new Contacts();
            System.Data.DataTable dt = clsContact.AdvanceSearchDataTable(enumContactGroupCategory, cboContactCode.SelectedItem.Value, cboContactName.SelectedItem.Value, Convert.ToInt16(cboDeleted.SelectedItem.Value), Convert.ToInt32(cboGroup.SelectedItem.Value), false, "ContactID", SortOption.Ascending);
			clsContact.CommitAndDispose();

            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = rptds.Contacts.NewRow();

                foreach (DataColumn dc in rptds.Contacts.Columns)
                    drNew[dc] = dr[dc.ColumnName];

                rptds.Contacts.Rows.Add(drNew);
            }

			Report.SetDataSource(rptds); 
			SetParameters(Report);
		}

		#endregion

		#region SetParameters
		private void SetParameters (ReportDocument Report)
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
