using AceSoft.RetailPlus.Security;
using AceSoft.RetailPlus.Reports;

namespace AceSoft.RetailPlus._Reports_Format
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	public partial  class __Details : System.Web.UI.UserControl
	{
		
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Visible)
				{
					lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
					LoadOptions();
					LoadRecord();
				}
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

		#region Private Methods

		private void LoadOptions()
		{
			
		}

		private void LoadRecord()
		{
			lblCompanyName.Text = CompanyDetails.CompanyName;

			Label lblLabel;

			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			stModule = "ReportHeaderSpacer";
			clsReceiptDetails = clsReceipt.Details(stModule);
//			txtReportHeaderSpacer.Text = clsReceiptDetails.Value;

			for (iCtr=1;iCtr<=10;iCtr++)
			{
				lblLabel = (Label) this.FindControl("lblReportHeader" + iCtr);

				stModule = "ReportHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=10;iCtr++)
			{
				lblLabel = (Label) this.FindControl("lblPageHeader" + iCtr);
				
				stModule = "PageHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
				
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=20;iCtr++)
			{
				lblLabel = (Label) this.FindControl("lblPageFooterA" + iCtr);
				
				stModule = "PageFooterA" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				lblLabel = (Label) this.FindControl("lblPageFooterB" + iCtr);
				
				stModule = "PageFooterB" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				lblLabel = (Label) this.FindControl("lblReportFooter" + iCtr);
				
				stModule = "ReportFooter" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				ApplyFormat(clsReceiptDetails, lblLabel);
			}

			stModule = "ReportFooterSpacer";
			clsReceiptDetails = clsReceipt.Details(stModule);
//			txtReportFooterSpacer.Text = clsReceiptDetails.Value;

			clsReceipt.CommitAndDispose();
		}

		private void ApplyFormat(Reports.ReceiptDetails clsReceiptDetails,Label lblLabel)
		{
			int CONFIG_MAX_RECEIPT_WIDTH = 40;
			if ((clsReceiptDetails.Text != "" && clsReceiptDetails.Text != null) || (clsReceiptDetails.Value != "" && clsReceiptDetails.Value != null))
			{
				switch (clsReceiptDetails.Orientation)
				{
					case ReportFormatOrientation.Justify:
						if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null) 
							lblLabel.Text = clsReceiptDetails.Value;
						else
							lblLabel.Text = clsReceiptDetails.Text.PadRight(13) + ":" + clsReceiptDetails.Value.PadLeft(CONFIG_MAX_RECEIPT_WIDTH-14);
						break;
					case ReportFormatOrientation.Center:
						if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null) 
							lblLabel.Text = CenterString(clsReceiptDetails.Value, CONFIG_MAX_RECEIPT_WIDTH);
						else
							lblLabel.Text = CenterString(clsReceiptDetails.Text + " : " + clsReceiptDetails.Value, CONFIG_MAX_RECEIPT_WIDTH);
						break;
				}
			}	
		}
		private string CenterString(string Value, int TotalLengthOfCenter)
		{
			string stRetValue = Value;
			Int32 lenvalue = Value.Length;

			if (lenvalue <= TotalLengthOfCenter)
			{
				Int32 padding = (int) (TotalLengthOfCenter - lenvalue) / 2;

				for(int i=0;i<padding;i++)
				{	stRetValue = " " + stRetValue + " "; }

				if (((TotalLengthOfCenter - lenvalue) % 2) != 0)
					stRetValue += " ";
			}
			else
			{
				stRetValue = Value.Substring(0, TotalLengthOfCenter);
			}
			return stRetValue;
		}


		#endregion
	}
}
