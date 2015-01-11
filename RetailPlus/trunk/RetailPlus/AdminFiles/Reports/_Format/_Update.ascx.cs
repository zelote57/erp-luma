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
	
	public partial  class __Update : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList Dropdownlist3;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist4;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist5;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist6;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist7;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist8;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist9;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist10;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist11;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist12;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist13;
		
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
			this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);

		}
		#endregion
		
		#region Web Control Methods

		#region ReportHeader

		protected void cboReportHeader1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader1, lblReportHeader1);
		}

		protected void cboReportHeader2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader2, lblReportHeader2);
		}

		protected void cboReportHeader3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader3, lblReportHeader3);
		}

		protected void cboReportHeader4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader4, lblReportHeader4);
		}

		protected void cboReportHeader5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader5, lblReportHeader5);
		}

		protected void cboReportHeader6_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader6, lblReportHeader6);
		}

		protected void cboReportHeader7_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader7, lblReportHeader7);
		}

		protected void cboReportHeader8_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader8, lblReportHeader8);
		}

		protected void cboReportHeader9_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader9, lblReportHeader9);
		}

		protected void cboReportHeader10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportHeader10, lblReportHeader10);
		}

		#endregion

		#region PageHeader

		protected void cboPageHeader1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader1, lblPageHeader1);
		}

		protected void cboPageHeader2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader2, lblPageHeader2);
		}

		protected void cboPageHeader3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader3, lblPageHeader3);
		}

		protected void cboPageHeader4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader4, lblPageHeader4);
		}

		protected void cboPageHeader5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader5, lblPageHeader5);
		}

		protected void cboPageHeader6_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader6, lblPageHeader6);
		}

		protected void cboPageHeader7_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader7, lblPageHeader7);
		}

		protected void cboPageHeader8_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader8, lblPageHeader8);
		}

		protected void cboPageHeader9_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader9, lblPageHeader9);
		}

		protected void cboPageHeader10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageHeader10, lblPageHeader10);
		}

		#endregion

		#region PageFooterA

		protected void cboPageFooterA1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA1, lblPageFooterA1);
		}

		protected void cboPageFooterA2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA2, lblPageFooterA2);
		}

		protected void cboPageFooterA3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA3, lblPageFooterA3);
		}

		protected void cboPageFooterA4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA4, lblPageFooterA4);
		}

		protected void cboPageFooterA5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA5, lblPageFooterA5);
		}

		protected void cboPageFooterA6_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA6, lblPageFooterA6);
		}

		protected void cboPageFooterA7_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA7, lblPageFooterA7);
		}

		protected void cboPageFooterA8_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA8, lblPageFooterA8);
		}

		protected void cboPageFooterA9_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA9, lblPageFooterA9);
		}

		protected void cboPageFooterA10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA10, lblPageFooterA10);
		}

		protected void cboPageFooterA11_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA11, lblPageFooterA11);
		}

		protected void cboPageFooterA12_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA12, lblPageFooterA12);
		}

		protected void cboPageFooterA13_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA13, lblPageFooterA13);
		}

		protected void cboPageFooterA14_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA14, lblPageFooterA14);
		}

		protected void cboPageFooterA15_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA15, lblPageFooterA15);
		}

		protected void cboPageFooterA16_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA16, lblPageFooterA16);
		}

		protected void cboPageFooterA17_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA17, lblPageFooterA17);
		}

		protected void cboPageFooterA18_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA18, lblPageFooterA18);
		}

		protected void cboPageFooterA19_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA19, lblPageFooterA19);
		}

		protected void cboPageFooterA20_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterA20, lblPageFooterA20);
		}

		#endregion

		#region PageFooterB

		protected void cboPageFooterB1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterB1, lblPageFooterB1);
		}

		protected void cboPageFooterB2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterB2, lblPageFooterB2);
		}

		protected void cboPageFooterB3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterB3, lblPageFooterB3);
		}

		protected void cboPageFooterB4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterB4, lblPageFooterB4);
		}

		protected void cboPageFooterB5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboPageFooterB5, lblPageFooterB5);
		}

		#endregion

		#region ReportFooter

		protected void cboReportFooter1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportFooter1, lblReportFooter1);
		}

		protected void cboReportFooter2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportFooter2, lblReportFooter2);
		}

		protected void cboReportFooter3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportFooter3, lblReportFooter3);
		}

		protected void cboReportFooter4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportFooter4, lblReportFooter4);
		}

		protected void cboReportFooter5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ApplyFormat(cboReportFooter5, lblReportFooter5);
		}

		#endregion

		private void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SaveRecord();
            Response.Redirect(Constants.ROOT_DIRECTORY + "/AdminFiles/Default.aspx");
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			SaveRecord();
            Response.Redirect(Constants.ROOT_DIRECTORY + "/AdminFiles/Default.aspx");
		}


		#endregion

		#region Private Methods

		private void SaveRecord()
		{
			DropDownList cboOrientationBox;
			DropDownList cboBox;
			TextBox txtBox;
			int iCtr = 1;

			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsDetails = new ReceiptDetails();

			clsDetails.Module = "ReportHeaderSpacer";
			clsDetails.Value = txtReportHeaderSpacer.Text;
			clsReceipt.Update(clsDetails);

			for (iCtr=1;iCtr<=10;iCtr++)
			{
				cboOrientationBox = (DropDownList) this.FindControl("cboReportHeaderOrientation" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboReportHeader" + iCtr);
				txtBox = (TextBox) this.FindControl("txtReportHeader" + iCtr);
				clsDetails.Module = "ReportHeader" + iCtr;
				clsDetails.Text = txtBox.Text;
				clsDetails.Value = cboBox.SelectedItem.Value.ToString();
				clsDetails.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation),cboOrientationBox.SelectedItem.Value.ToString());

				clsReceipt.Update(clsDetails);
			}
			for (iCtr=1;iCtr<=10;iCtr++)
			{
				cboOrientationBox = (DropDownList) this.FindControl("cboPageHeaderOrientation" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageHeader" + iCtr);
				txtBox = (TextBox) this.FindControl("txtPageHeader" + iCtr);

				clsDetails.Module = "PageHeader" + iCtr;
				clsDetails.Text = txtBox.Text;
				clsDetails.Value = cboBox.SelectedItem.Value.ToString();
				clsDetails.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation),cboOrientationBox.SelectedItem.Value.ToString());

				clsReceipt.Update(clsDetails);
			}
			for (iCtr=1;iCtr<=20;iCtr++)
			{
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterAOrientation" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageFooterA" + iCtr);
				txtBox = (TextBox) this.FindControl("txtPageFooterA" + iCtr);

				clsDetails.Module = "PageFooterA" + iCtr;
				clsDetails.Text = txtBox.Text;
				clsDetails.Value = cboBox.SelectedItem.Value.ToString();
				clsDetails.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation),cboOrientationBox.SelectedItem.Value.ToString());

				clsReceipt.Update(clsDetails);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterBOrientation" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageFooterB" + iCtr);
				txtBox = (TextBox) this.FindControl("txtPageFooterB" + iCtr);

				clsDetails.Module = "PageFooterB" + iCtr;
				clsDetails.Text = txtBox.Text;
				clsDetails.Value = cboBox.SelectedItem.Value.ToString();
				clsDetails.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation),cboOrientationBox.SelectedItem.Value.ToString());

				clsReceipt.Update(clsDetails);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				cboOrientationBox = (DropDownList) this.FindControl("cboReportFooterOrientation" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboReportFooter" + iCtr);
				txtBox = (TextBox) this.FindControl("txtReportFooter" + iCtr);

				clsDetails.Module = "ReportFooter" + iCtr;
				clsDetails.Text = txtBox.Text;
				clsDetails.Value = cboBox.SelectedItem.Value.ToString();
				clsDetails.Orientation = (ReportFormatOrientation) Enum.Parse(typeof(ReportFormatOrientation),cboOrientationBox.SelectedItem.Value.ToString());

				clsReceipt.Update(clsDetails);
			}

			clsDetails.Module = "ReportFooterSpacer";
			clsDetails.Value = txtReportFooterSpacer.Text;
			clsReceipt.Update(clsDetails);

			clsReceipt.CommitAndDispose();
		}

		private void LoadOptions()
		{
			int iCtr = 1;
			DropDownList cboBox;
			DropDownList cboOrientationBox;

			for (iCtr=1;iCtr<=10;iCtr++)
			{
				cboBox = (DropDownList) this.FindControl("cboReportHeader" + iCtr);
				LoadDropDownList(cboBox);
				cboOrientationBox = (DropDownList) this.FindControl("cboReportHeaderOrientation" + iCtr);
				LoadDropDownListOrientation(cboOrientationBox);
			}
			for (iCtr=1;iCtr<=10;iCtr++)
			{
				cboBox = (DropDownList) this.FindControl("cboPageHeader" + iCtr);
				LoadDropDownList(cboBox);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageHeaderOrientation" + iCtr);
				LoadDropDownListOrientation(cboOrientationBox);
			}
			for (iCtr=1;iCtr<=20;iCtr++)
			{
				cboBox = (DropDownList) this.FindControl("cboPageFooterA" + iCtr);
				LoadDropDownList(cboBox);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterAOrientation" + iCtr);
				LoadDropDownListOrientation(cboOrientationBox);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				cboBox = (DropDownList) this.FindControl("cboPageFooterB" + iCtr);
				LoadDropDownList(cboBox);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterBOrientation" + iCtr);
				LoadDropDownListOrientation(cboOrientationBox);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				cboBox = (DropDownList) this.FindControl("cboReportFooter" + iCtr);
				LoadDropDownList(cboBox);
				cboOrientationBox = (DropDownList) this.FindControl("cboReportFooterOrientation" + iCtr);
				LoadDropDownListOrientation(cboOrientationBox);
			}
		}

		private void LoadDropDownList(DropDownList cboBox)
		{
            cboBox.Items.Add(new ListItem("", ""));
            cboBox.Items.Add(new ListItem("0.00", "0.00"));
            cboBox.Items.Add(new ListItem("xxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxxxxxxxxxxx"));
            cboBox.Items.Add(new ListItem("----------------------------------------", "----------------------------------------"));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Address1, ReceiptFieldFormats.Address1));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Address2, ReceiptFieldFormats.Address2));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.OfficePhone, ReceiptFieldFormats.OfficePhone));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.DirectPhone, ReceiptFieldFormats.DirectPhone));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.FaxPhone, ReceiptFieldFormats.FaxPhone));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.MobilePhone, ReceiptFieldFormats.MobilePhone));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.EmailAddress, ReceiptFieldFormats.EmailAddress));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WebSite, ReceiptFieldFormats.WebSite));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.TIN, ReceiptFieldFormats.TIN));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Blank, ReceiptFieldFormats.Blank));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Spacer, ReceiptFieldFormats.Spacer));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Terminator, ReceiptFieldFormats.Terminator));

			if (ReceiptFieldFormats.WelcomeNote1 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WelcomeNote1, ReceiptFieldFormats.WelcomeNote1));
			if (ReceiptFieldFormats.WelcomeNote2 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WelcomeNote2, ReceiptFieldFormats.WelcomeNote2));
			if (ReceiptFieldFormats.WelcomeNote3 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WelcomeNote3, ReceiptFieldFormats.WelcomeNote3));
			if (ReceiptFieldFormats.WelcomeNote4 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WelcomeNote4, ReceiptFieldFormats.WelcomeNote4));
			if (ReceiptFieldFormats.WelcomeNote5 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WelcomeNote5, ReceiptFieldFormats.WelcomeNote5));

			if (ReceiptFieldFormats.ClosingNote1 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ClosingNote1, ReceiptFieldFormats.ClosingNote1));
			if (ReceiptFieldFormats.ClosingNote2 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ClosingNote2, ReceiptFieldFormats.ClosingNote2));
			if (ReceiptFieldFormats.ClosingNote3 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ClosingNote3, ReceiptFieldFormats.ClosingNote3));
			if (ReceiptFieldFormats.ClosingNote4 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ClosingNote4, ReceiptFieldFormats.ClosingNote4));
			if (ReceiptFieldFormats.ClosingNote5 != "") cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ClosingNote5, ReceiptFieldFormats.ClosingNote5));

			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.InvoiceNo, ReceiptFieldFormats.InvoiceNo));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ORNo, ReceiptFieldFormats.ORNo));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.CheckCounter, ReceiptFieldFormats.CheckCounter));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.DateNow, ReceiptFieldFormats.DateNow));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.TransactionDate, ReceiptFieldFormats.TransactionDate));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Cashier, ReceiptFieldFormats.Cashier));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.TerminalNo, ReceiptFieldFormats.TerminalNo));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.MachineSerialNo, ReceiptFieldFormats.MachineSerialNo));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.AccreditationNo, ReceiptFieldFormats.AccreditationNo));

			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Spacer2, ReceiptFieldFormats.Spacer2));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.SubTotal, ReceiptFieldFormats.SubTotal));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.OtherCharges, ReceiptFieldFormats.OtherCharges));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.CreditChargeAmount, ReceiptFieldFormats.CreditChargeAmount));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ChargeCode, ReceiptFieldFormats.ChargeCode));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ChargeRemarks, ReceiptFieldFormats.ChargeRemarks));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Discount, ReceiptFieldFormats.Discount));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.DiscountCode, ReceiptFieldFormats.DiscountCode));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.DiscountRemarks, ReceiptFieldFormats.DiscountRemarks));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.AmountDue, ReceiptFieldFormats.AmountDue));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.AmountTender, ReceiptFieldFormats.AmountTender));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.Change, ReceiptFieldFormats.Change));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.VATExempt, ReceiptFieldFormats.VATExempt));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.ZeroRatedSales, ReceiptFieldFormats.ZeroRatedSales));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.NonVATableAmount, ReceiptFieldFormats.NonVATableAmount));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.VATableAmount, ReceiptFieldFormats.VATableAmount));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.VAT, ReceiptFieldFormats.VAT));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.TotalItemSold, ReceiptFieldFormats.TotalItemSold));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.TotalQtySold, ReceiptFieldFormats.TotalQtySold));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.CustomerName, ReceiptFieldFormats.CustomerName));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.WaiterName, ReceiptFieldFormats.WaiterName));
			cboBox.Items.Add(new ListItem(ReceiptFieldFormats.BaggerName, ReceiptFieldFormats.BaggerName));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.OrderType, ReceiptFieldFormats.OrderType));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.CheckOutBillFooter, ReceiptFieldFormats.CheckOutBillFooter));

            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.RewardsCustomerName, ReceiptFieldFormats.RewardsCustomerName));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.RewardCardNo, ReceiptFieldFormats.RewardCardNo));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.RewardPreviousPoints, ReceiptFieldFormats.RewardPreviousPoints));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.RewardEarnedPoints, ReceiptFieldFormats.RewardEarnedPoints));
            cboBox.Items.Add(new ListItem(ReceiptFieldFormats.RewardCurrentPoints, ReceiptFieldFormats.RewardCurrentPoints));
            
            

			cboBox.SelectedIndex = cboBox.Items.Count - 1;
		}
		private void LoadDropDownListOrientation(DropDownList cboBox)
		{
			cboBox.Items.Add(new ListItem("Justify", "0"));
			cboBox.Items.Add(new ListItem("Center", "1"));
			cboBox.SelectedIndex = cboBox.Items.Count - 1;
		}
		private void LoadRecord()
		{

			lblCompanyName.Text = CompanyDetails.CompanyName;
			lblCompanyName1.Text = CompanyDetails.CompanyName;

			DropDownList cboOrientationBox;
			DropDownList cboBox;
			TextBox txtBox;
			Label lblLabel;

			int iCtr = 0;
			string stModule = "";
			Receipt clsReceipt = new Receipt();
			ReceiptDetails clsReceiptDetails;

			stModule = "ReportHeaderSpacer";
			clsReceiptDetails = clsReceipt.Details(stModule);
			txtReportHeaderSpacer.Text = clsReceiptDetails.Value;

			for (iCtr=1;iCtr<=10;iCtr++)
			{
				txtBox = (TextBox) this.FindControl("txtReportHeader" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboReportHeader" + iCtr);
				cboOrientationBox = (DropDownList) this.FindControl("cboReportHeaderOrientation" + iCtr);
				lblLabel = (Label) this.FindControl("lblReportHeader" + iCtr);

				stModule = "ReportHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				txtBox.Text = clsReceiptDetails.Text;
				cboBox.SelectedIndex = cboBox.Items.IndexOf(cboBox.Items.FindByValue(clsReceiptDetails.Value));
				cboOrientationBox.SelectedIndex = cboOrientationBox.Items.IndexOf(cboOrientationBox.Items.FindByValue(clsReceiptDetails.Orientation.ToString("d")));
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=10;iCtr++)
			{
				txtBox = (TextBox) this.FindControl("txtPageHeader" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageHeader" + iCtr);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageHeaderOrientation" + iCtr);
				lblLabel = (Label) this.FindControl("lblPageHeader" + iCtr);
				
				stModule = "PageHeader" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
				
				txtBox.Text = clsReceiptDetails.Text;
				cboBox.SelectedIndex = cboBox.Items.IndexOf(cboBox.Items.FindByValue(clsReceiptDetails.Value));
				cboOrientationBox.SelectedIndex = cboOrientationBox.Items.IndexOf(cboOrientationBox.Items.FindByValue(clsReceiptDetails.Orientation.ToString("d")));
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=20;iCtr++)
			{
				txtBox = (TextBox) this.FindControl("txtPageFooterA" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageFooterA" + iCtr);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterAOrientation" + iCtr);
				lblLabel = (Label) this.FindControl("lblPageFooterA" + iCtr);
				
				stModule = "PageFooterA" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				txtBox.Text = clsReceiptDetails.Text;
				cboBox.SelectedIndex = cboBox.Items.IndexOf(cboBox.Items.FindByValue(clsReceiptDetails.Value));
				cboOrientationBox.SelectedIndex = cboOrientationBox.Items.IndexOf(cboOrientationBox.Items.FindByValue(clsReceiptDetails.Orientation.ToString("d")));
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				txtBox = (TextBox) this.FindControl("txtPageFooterB" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboPageFooterB" + iCtr);
				cboOrientationBox = (DropDownList) this.FindControl("cboPageFooterBOrientation" + iCtr);
				lblLabel = (Label) this.FindControl("lblPageFooterB" + iCtr);
				
				stModule = "PageFooterB" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				txtBox.Text = clsReceiptDetails.Text;
				cboBox.SelectedIndex = cboBox.Items.IndexOf(cboBox.Items.FindByValue(clsReceiptDetails.Value));
				cboOrientationBox.SelectedIndex = cboOrientationBox.Items.IndexOf(cboOrientationBox.Items.FindByValue(clsReceiptDetails.Orientation.ToString("d")));
				ApplyFormat(clsReceiptDetails, lblLabel);
			}
			for (iCtr=1;iCtr<=5;iCtr++)
			{
				txtBox = (TextBox) this.FindControl("txtReportFooter" + iCtr);
				cboBox = (DropDownList) this.FindControl("cboReportFooter" + iCtr);
				cboOrientationBox = (DropDownList) this.FindControl("cboReportFooterOrientation" + iCtr);
				lblLabel = (Label) this.FindControl("lblReportFooter" + iCtr);
				
				stModule = "ReportFooter" + iCtr;
				clsReceiptDetails = clsReceipt.Details(stModule);
			
				txtBox.Text = clsReceiptDetails.Text;
				cboBox.SelectedIndex = cboBox.Items.IndexOf(cboBox.Items.FindByValue(clsReceiptDetails.Value));
				cboOrientationBox.SelectedIndex = cboOrientationBox.Items.IndexOf(cboOrientationBox.Items.FindByValue(clsReceiptDetails.Orientation.ToString("d")));
				ApplyFormat(clsReceiptDetails, lblLabel);
			}

			stModule = "ReportFooterSpacer";
			clsReceiptDetails = clsReceipt.Details(stModule);
			txtReportFooterSpacer.Text = clsReceiptDetails.Value;

			clsReceipt.CommitAndDispose();
		}

		private void ApplyFormat(DropDownList sender, Label receiver)
		{
			if (sender.SelectedItem.Text == ReceiptFieldFormats.Blank)
			{
				receiver.Text = "&nbsp";
			}
			else if (sender.SelectedItem.Text == ReceiptFieldFormats.DateNow)
			{
				receiver.Text = DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss");
			}
			else if (sender.SelectedItem.Text == ReceiptFieldFormats.Cashier)
			{
				receiver.Text = "Cashier: " + Session["Name"].ToString();
			}
			else if (sender.SelectedItem.Text == ReceiptFieldFormats.TerminalNo)
			{
				receiver.Text = "Terminal No.: " + CompanyDetails.TerminalNo;
			}
//			else if (sender.SelectedItem.Text == ReceiptFieldFormats.MachineSerialNo)
//			{
//				receiver.Text = "MIN: " + CompanyDetails.MachineSerialNo;
//			}
//			else if (sender.SelectedItem.Text == ReceiptFieldFormats.AccreditationNo)
//			{
//				receiver.Text = "Acc. No.: " + CompanyDetails.AccreditationNo;
//			}
			else
			{
				receiver.Text = sender.SelectedItem.Text;
			}
			
		}

		private void ApplyFormat(Reports.ReceiptDetails clsReceiptDetails,Label lblLabel)
		{
			int CONFIG_MAX_RECEIPT_WIDTH = 40;
			if (!string.IsNullOrEmpty(clsReceiptDetails.Text) || !string.IsNullOrEmpty(clsReceiptDetails.Value))
			{
				switch (clsReceiptDetails.Orientation)
				{
					case ReportFormatOrientation.Justify:
						if (string.IsNullOrEmpty(clsReceiptDetails.Text)) 
							lblLabel.Text = clsReceiptDetails.Value;
                        else if (string.IsNullOrEmpty(clsReceiptDetails.Value))
                            lblLabel.Text = clsReceiptDetails.Text;
						else
							lblLabel.Text = clsReceiptDetails.Text.PadRight(13) + ":" + clsReceiptDetails.Value.PadLeft(CONFIG_MAX_RECEIPT_WIDTH-14);
						break;
					case ReportFormatOrientation.Center:
                        if (string.IsNullOrEmpty(clsReceiptDetails.Text)) 
							lblLabel.Text = CenterString(clsReceiptDetails.Value, CONFIG_MAX_RECEIPT_WIDTH);
                        else if (string.IsNullOrEmpty(clsReceiptDetails.Value))
                            lblLabel.Text = CenterString(clsReceiptDetails.Text, CONFIG_MAX_RECEIPT_WIDTH);
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
