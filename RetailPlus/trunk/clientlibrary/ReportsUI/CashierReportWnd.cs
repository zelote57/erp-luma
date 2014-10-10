using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using AceSoft.RetailPlus.Reports;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for CashierReportWnd.
	/// </summary>
	public class CashierReportWnd : System.Windows.Forms.Form
	{
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox imgIcon;
		private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblReportDesc;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label lblReceiptDesc;
        private Label lblCompany;

        #region Public Get/Set Properties

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private Data.CashierReportDetails mclsDetails;
        public Data.CashierReportDetails Details
        {
            set
            { mclsDetails = value; }
        }

        private string mCashierName;
        public string CashierName
        {
            set
            { mCashierName = value; }
        }

        private decimal mTrustFund;
        public decimal TrustFund
        {
            set
            { mTrustFund = value; }
        }

        private Data.SysConfigDetails mclsSysConfigDetails;
        public Data.SysConfigDetails SysConfigDetails
        {
            set
            {
                mclsSysConfigDetails = value;
            }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        private Panel panel1;
        private DataGridView dgvItems;
    
        public Data.TerminalDetails TerminalDetails
        {
            set
            {
                mclsTerminalDetails = value;
            }
        }

        private Reports.TerminalReportType mTerminalReportType;

        public Reports.TerminalReportType TerminalReportType
        {
            set
            {
                mTerminalReportType = value;
            }
        }

        #endregion

        #region Constructors and Destructors

        public CashierReportWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblReceiptDesc = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 51;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblReceiptDesc);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 533);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cashier Report Details";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvItems);
            this.panel1.Location = new System.Drawing.Point(311, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 458);
            this.panel1.TabIndex = 127;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CausesValidation = false;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeight = 24;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItems.GridColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(388, 458);
            this.dgvItems.TabIndex = 127;
            // 
            // lblReceiptDesc
            // 
            this.lblReceiptDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptDesc.Location = new System.Drawing.Point(308, 40);
            this.lblReceiptDesc.Name = "lblReceiptDesc";
            this.lblReceiptDesc.Size = new System.Drawing.Size(388, 22);
            this.lblReceiptDesc.TabIndex = 125;
            this.lblReceiptDesc.Text = "Cashier\'s Report";
            this.lblReceiptDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(308, 21);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(388, 19);
            this.lblCompany.TabIndex = 124;
            this.lblCompany.Text = "AceSoft RetailPlus ™";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportDesc
            // 
            this.lblReportDesc.AutoSize = true;
            this.lblReportDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblReportDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDesc.ForeColor = System.Drawing.Color.White;
            this.lblReportDesc.Location = new System.Drawing.Point(67, 22);
            this.lblReportDesc.Name = "lblReportDesc";
            this.lblReportDesc.Size = new System.Drawing.Size(135, 13);
            this.lblReportDesc.TabIndex = 3;
            this.lblReportDesc.Text = "XRead Report Window.";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(762, 41);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(252, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Tag = "";
            this.lblDescription.Text = "Press Enter Key to print the current viewed report.";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(765, 618);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 83);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdEnter
            // 
            this.cmdEnter.AutoSize = true;
            this.cmdEnter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEnter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnter.ForeColor = System.Drawing.Color.White;
            this.cmdEnter.Location = new System.Drawing.Point(877, 618);
            this.cmdEnter.Name = "cmdEnter";
            this.cmdEnter.Size = new System.Drawing.Size(106, 83);
            this.cmdEnter.TabIndex = 0;
            this.cmdEnter.Text = "PRINT";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CashierReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblReportDesc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CashierReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CashierReportWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CashierReportWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods
        
        private void CashierReportWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CashierReport.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			lblReportDesc.Text = mCashierName + " Cashier Report Window.";
			groupBox1.Text = "Cashier's Report Details";
			lblReceiptDesc.Text = "Cashier's Report : " + mCashierName;

			PopulateCashiersReport();
		}

		private void CashierReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
                    this.Hide(); 
					break;

				case Keys.Enter:
					dialog = DialogResult.OK; 
					this.Hide();
					break;
			}
        }

        #endregion

        #region Windows Control Methods

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.OK;
            this.Hide();
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Private Methods

        private void PopulateCashiersReport()
        {
            Receipt clsReceipt = new Receipt();
            clsReceipt.GetConnection();

            Data.TerminalReportDetails clsTerminalReportDetails = new Data.TerminalReport(clsReceipt.Connection, clsReceipt.Transaction).Details(mclsDetails.BranchID, mclsDetails.TerminalNo);

            mclsTerminalDetails.MaxReceiptWidth = 94;

            System.Data.DataTable dt = new System.Data.DataTable("tblTerminalReport");
            dt.Columns.Add("Module");
            dt.Columns.Add("Separator");
            dt.Columns.Add("Value");
            //dt.Rows.Add(CenterString(CompanyDetails.CompanyCode, mclsTerminalDetails.MaxReceiptWidth));
            //dt.Rows.Add("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-'));
            //dt.Rows.Add(CenterString("Terminal Report", mclsTerminalDetails.MaxReceiptWidth));
            //dt.Rows.Add("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-'));

            //string strReportHeader1 = clsReceipt.Details("ReportHeader1").Value;
            //string strReportHeader2 = clsReceipt.Details("ReportHeader2").Value;
            //string strReportHeader3 = clsReceipt.Details("ReportHeader3").Value;
            //string strReportHeader4 = clsReceipt.Details("ReportHeader4").Value;
            //if (!string.IsNullOrEmpty(strReportHeader1)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportHeader1), mclsTerminalDetails.MaxReceiptWidth));
            //if (!string.IsNullOrEmpty(strReportHeader2)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportHeader2), mclsTerminalDetails.MaxReceiptWidth));
            //if (!string.IsNullOrEmpty(strReportHeader3)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportHeader3), mclsTerminalDetails.MaxReceiptWidth));
            //if (!string.IsNullOrEmpty(strReportHeader4)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportHeader4), mclsTerminalDetails.MaxReceiptWidth));

            dt.Rows.Add("Gross Sales", ":", (mclsDetails.GrossSales + mclsDetails.TotalCharge).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Service Charge", ":", mclsDetails.TotalCharge.ToString("#,##0.#0"));
            dt.Rows.Add("", ":", "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("Total Amount", ":", mclsDetails.GrossSales.ToString("#,##0.#0"));
            dt.Rows.Add("(-) " + mclsTerminalDetails.VAT.ToString("##") + "% VAT Exempt ", ":", (mclsDetails.VATExempt * (mclsTerminalDetails.VAT / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Subtotal Discount", ":", mclsDetails.SubTotalDiscount.ToString("#,##0.#0"));
            dt.Rows.Add("", ":", "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("Net Sales", ":", mclsDetails.NetSales.ToString("#,##0.#0"));

            dt.Rows.Add("Taxables Breakdown", "", "");
            dt.Rows.Add("VAT Exempt", ":", mclsDetails.VATExempt.ToString("#,##0.00"));
            dt.Rows.Add("VAT Zero Rated", ":", mclsDetails.VATZeroRated.ToString("#,##0.00"));
            dt.Rows.Add("NonVATable Amount", ":", mclsDetails.NonVATableAmount.ToString("#,##0.00"));
            dt.Rows.Add("VATable Amount", ":", mclsDetails.VATableAmount.ToString("#,##0.00"));
            dt.Rows.Add(mclsTerminalDetails.VAT.ToString("##") + "% VAT", ":", mclsDetails.VAT.ToString("#,##0.00"));
            dt.Rows.Add("Local Tax", ":", mclsDetails.LocalTax.ToString("#,##0.00"));

            dt.Rows.Add("Total Amount Breakdown", "", "");
            dt.Rows.Add("Cash Sales", ":", mclsDetails.CashSales.ToString("#,##0.00"));
            dt.Rows.Add("Cheque Sales", ":", mclsDetails.ChequeSales.ToString("#,##0.00"));
            dt.Rows.Add("Credit Card Sales", ":", mclsDetails.CreditCardSales.ToString("#,##0.00"));
            dt.Rows.Add("Credit (Charge)", ":", mclsDetails.CreditSales.ToString("#,##0.00"));
            dt.Rows.Add("Credit Payment", ":", mclsDetails.CreditPayment.ToString("#,##0.00"));
            dt.Rows.Add("      Cash", ":", mclsDetails.CreditPaymentCash.ToString("#,##0.00"));
            dt.Rows.Add("      Cheque", ":", mclsDetails.CreditPaymentCheque.ToString("#,##0.00"));
            dt.Rows.Add("      Credit Card", ":", mclsDetails.CreditPaymentCreditCard.ToString("#,##0.00"));
            dt.Rows.Add("      Debit", ":", mclsDetails.CreditPaymentDebit.ToString("#,##0.00"));
            dt.Rows.Add("Debit Sales", ":", mclsDetails.DebitPayment.ToString("#,##0.00"));
            dt.Rows.Add("     Rewards Points Redeemed", ":", mclsDetails.RewardPointsPayment.ToString("#,##0.00"));
            dt.Rows.Add("Employee Acct.", ":", "0.00");
            dt.Rows.Add("Void Sales", ":", mclsDetails.VoidSales.ToString("#,##0.00"));
            dt.Rows.Add("Refund Sales", ":", mclsDetails.RefundSales.ToString("#,##0.00"));
            dt.Rows.Add("      Cash", ":", mclsDetails.RefundCash.ToString("#,##0.00"));
            dt.Rows.Add("      Cheque", ":", mclsDetails.RefundCheque.ToString("#,##0.00"));
            dt.Rows.Add("      Credit Card", ":", mclsDetails.RefundCreditCard.ToString("#,##0.00"));
            dt.Rows.Add("      Credit", ":", mclsDetails.RefundCredit.ToString("#,##0.00"));
            dt.Rows.Add("      Debit", ":", mclsDetails.RefundDebit.ToString("#,##0.00"));

            dt.Rows.Add("Discounts", "", "");
            dt.Rows.Add("Items Discount", ":", mclsDetails.ItemsDiscount.ToString("#,##0.00"));
            dt.Rows.Add("Subtotal Discount", ":", mclsDetails.SubTotalDiscount.ToString("#,##0.00"));
            dt.Rows.Add("     Senior Citizen", ":", mclsDetails.SNRDiscount.ToString("#,##0.00"));
            dt.Rows.Add("     PWD", ":", mclsDetails.PWDDiscount.ToString("#,##0.00"));
            dt.Rows.Add("     Others", ":", mclsDetails.OtherDiscount.ToString("#,##0.00"));
            dt.Rows.Add("Total Discounts", ":", mclsDetails.TotalDiscount.ToString("#,##0.00"));

            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsReceipt.Connection, clsReceipt.Transaction);

            System.Data.DataTable dtDiscounts = clsSalesTransactions.Discounts(mclsDetails.BranchID, mclsDetails.TerminalNo, clsTerminalReportDetails.BeginningTransactionNo, clsTerminalReportDetails.EndingTransactionNo);
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Add("-", "-", "-");
                dt.Rows.Add("Subtotal Discounts Breakdown", "", "");
                dt.Rows.Add("-", "-", "-");
                foreach (System.Data.DataRow dr in dtDiscounts.Rows)
                {
                    dt.Rows.Add(dr["DiscountCode"].ToString(), ":", decimal.Parse(dr["Discount"].ToString()).ToString("#,##0.00"));
                }
            }

            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Drawer Information", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Beginning Balance", ":", mclsDetails.BeginningBalance.ToString("#,##0.00"));
            dt.Rows.Add("Cash In Drawer", ":", mclsDetails.CashInDrawer.ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Paid Out", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Paid Out", ":", mclsDetails.TotalPaidOut.ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("PICK UP / Disburstment", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", mclsDetails.CashDisburse.ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", mclsDetails.ChequeDisburse.ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", mclsDetails.CreditCardDisburse.ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Receive on Account", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", mclsDetails.CashWithHold.ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", mclsDetails.ChequeWithHold.ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", mclsDetails.CreditCardWithHold.ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Customer Deposits", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", mclsDetails.CashDeposit.ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", mclsDetails.ChequeDeposit.ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", mclsDetails.CreditCardDeposit.ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Transaction Count Breakdown", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash Transactions", ":", mclsDetails.NoOfCashTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Cheque Transactions", ":", mclsDetails.NoOfChequeTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Credit Card Transactions", ":", mclsDetails.NoOfCreditCardTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Credit Transactions", ":", mclsDetails.NoOfCreditTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Debit Transactions", ":", mclsDetails.NoOfDebitPaymentTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Refund Transactions", ":", mclsDetails.NoOfRefundTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Void Transactions", ":", mclsDetails.NoOfVoidTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Combination Trans", ":", mclsDetails.NoOfCombinationPaymentTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Credit Payment Trans", ":", mclsDetails.NoOfCreditPaymentTransactions.ToString("#,##0.00"));
            dt.Rows.Add("Reward Points Trans", ":", mclsDetails.NoOfRewardPointsPayment.ToString("#,##0.00"));
            dt.Rows.Add("", ":", "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("Total Transactions", ":", mclsDetails.NoOfTotalTransactions.ToString("#,##0.00"));

            string strReportFooter1 = clsReceipt.Details("ReportFooter1").Value;
            string strReportFooter2 = clsReceipt.Details("ReportFooter2").Value;
            string strReportFooter3 = clsReceipt.Details("ReportFooter3").Value;
            if (!string.IsNullOrEmpty(strReportFooter1)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportFooter1), mclsTerminalDetails.MaxReceiptWidth));
            if (!string.IsNullOrEmpty(strReportFooter2)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportFooter2), mclsTerminalDetails.MaxReceiptWidth));
            if (!string.IsNullOrEmpty(strReportFooter3)) dt.Rows.Add(CenterString(GetReceiptFormatParameter(strReportFooter3), mclsTerminalDetails.MaxReceiptWidth));

            clsReceipt.CommitAndDispose();

            dgvItems.MultiSelect = true;
            dgvItems.AutoGenerateColumns = true;
            dgvItems.AutoSize = true;
            dgvItems.ScrollBars = ScrollBars.Vertical;
            dgvItems.DataSource = dt.TableName;
            dgvItems.DataSource = dt;

            //dgvItems.Columns["ReportValue"].Visible = true;
            //dgvItems.Columns["ReportValue"].HeaderText = "";
            //dgvItems.Columns["ReportValue"].Width = 388;

            dgvItems.Columns["Module"].Visible = true;
            dgvItems.Columns["Separator"].Visible = true;
            dgvItems.Columns["Value"].Visible = true;

            dgvItems.Columns["Module"].HeaderText = "";
            dgvItems.Columns["Separator"].HeaderText = "";
            dgvItems.Columns["Value"].HeaderText = "";

            dgvItems.Columns["Module"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvItems.Columns["Separator"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvItems.Columns["Module"].Width = 230;
            dgvItems.Columns["Separator"].Width = 20;
            dgvItems.Columns["Value"].Width = dgvItems.Width-250-20;

        }
        private string GetReceiptFormatParameter(string stReceiptFormat)
        {
            string stRetValue = "";

            if (stReceiptFormat == ReceiptFieldFormats.Blank)
            {
                stRetValue = "";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Spacer)
            {
                stRetValue = " ";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.InvoiceNo)
            {
                stRetValue = "";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.DateNow)
            {
                stRetValue = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Cashier)
            {
                stRetValue = mCashierName;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TerminalNo)
            {
                stRetValue = mclsTerminalDetails.TerminalNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.MachineSerialNo)
            {
                stRetValue = CONFIG.MachineSerialNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.AccreditationNo)
            {
                stRetValue = CONFIG.AccreditationNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardsPermitNo)
            {
                stRetValue = mclsTerminalDetails.RewardPointsDetails.RewardsPermitNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.InHouseIndividualCreditPermitNo)
            {
                stRetValue = mclsTerminalDetails.InHouseIndividualCreditPermitNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.InHouseGroupCreditPermitNo)
            {
                stRetValue = mclsTerminalDetails.InHouseGroupCreditPermitNo;
            }
            else
            {
                stRetValue = stReceiptFormat;
            }

            if (stRetValue == null) stRetValue = "";

            return stRetValue;
        }
        private string CenterString(string Value, int TotalLengthOfCenter)
        {
            string stRetValue = Value;
            Int32 lenvalue = Value.Length;

            if (lenvalue <= TotalLengthOfCenter)
            {
                Int32 padding = (int)(TotalLengthOfCenter - lenvalue) / 2;

                for (int i = 0; i < padding; i++)
                { stRetValue = " " + stRetValue + " "; }

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