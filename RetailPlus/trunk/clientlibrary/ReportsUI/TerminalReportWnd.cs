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
    /// Summary description for TerminalReportWnd.
    /// </summary>
    public class TerminalReportWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblDescription;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label lblReportDesc;
        private System.Windows.Forms.Label lblCompany;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label lblReceiptDesc;

        #region Public Get/Set Properties

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }
        
        private Data.TerminalReportDetails mclsDetails;
        public Data.TerminalReportDetails Details
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
        public TerminalReportWnd()
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

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
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XRead Details";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvItems);
            this.panel1.Location = new System.Drawing.Point(311, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 463);
            this.panel1.TabIndex = 124;
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
            this.dgvItems.Size = new System.Drawing.Size(388, 463);
            this.dgvItems.TabIndex = 124;
            // 
            // lblReceiptDesc
            // 
            this.lblReceiptDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptDesc.Location = new System.Drawing.Point(308, 36);
            this.lblReceiptDesc.Name = "lblReceiptDesc";
            this.lblReceiptDesc.Size = new System.Drawing.Size(388, 22);
            this.lblReceiptDesc.TabIndex = 122;
            this.lblReceiptDesc.Text = "Terminal Report";
            this.lblReceiptDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(308, 17);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(388, 19);
            this.lblCompany.TabIndex = 113;
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
            this.lblReportDesc.TabIndex = 67;
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
            this.lblDescription.TabIndex = 86;
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
            // TerminalReportWnd
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
            this.Name = "TerminalReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TerminalReportWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalReportWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region Windows Form Methods

        private void TerminalReportWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Help.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            lblCompany.Text = CompanyDetails.CompanyName;

            switch (mTerminalReportType)
            {
                case Reports.TerminalReportType.TerminalReport:
                    lblReportDesc.Text = "Terminal " + CompanyDetails.TerminalNo + " Report Window.";
                    groupBox1.Text = "Terminal Report Details";
                    lblReceiptDesc.Text = "Terminal Report";
                    break;
                case Reports.TerminalReportType.XRead:
                    lblReportDesc.Text = "XRead Report Window.";
                    groupBox1.Text = "XRead Details";
                    lblReceiptDesc.Text = "XRead Report : " + mclsDetails.XReadCount.ToString("#,##0");
                    break;
                case Reports.TerminalReportType.ZRead:
                    lblReportDesc.Text = "ZRead Report Window.";
                    groupBox1.Text = "ZRead Details";
                    lblReceiptDesc.Text = "ZRead Report : " + mclsDetails.ZReadCount.ToString("#,##0");
                    break;
                case Reports.TerminalReportType.CashiersTerminalReport:
                    lblReportDesc.Text = "Cashiers Report Window.";
                    groupBox1.Text = "Cashiers Report Details";
                    lblReceiptDesc.Text = "Cashiers Report : " + mCashierName;
                    break;
            }

            PopulateTerminalReport();

        }

        private void TerminalReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

        #endregion

        #region Private Methods

        private void PopulateTerminalReport()
        {
            Receipt clsReceipt = new Receipt();

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

            if (Int64.Parse(mclsDetails.BeginningORNo) == 0)
                dt.Rows.Add("Beginning OR No.", ":", mclsDetails.BeginningORNo.Remove(mclsDetails.BeginningORNo.Length - 1) + "1");
            else
                dt.Rows.Add("Beginning OR No.", ":", mclsDetails.BeginningORNo);

            dt.Rows.Add("Ending OR No.", ":", mclsDetails.EndingORNo);
            dt.Rows.Add("", "", "");
            dt.Rows.Add("Gross Sales", ":", ((mclsDetails.GrossSales + mclsDetails.TotalCharge) * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Service Charge", ":", mclsDetails.TotalCharge.ToString("#,##0.#0"));
            dt.Rows.Add("",":","------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("Total Amount", ":", (mclsDetails.GrossSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) " + mclsTerminalDetails.VAT.ToString("##") + "% VAT Exempt ", ":", (mclsDetails.VATExempt / (1 + (mclsTerminalDetails.VAT / 100)) * (mclsTerminalDetails.VAT / 100) * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Discount", ":", (mclsDetails.SubTotalDiscount + mclsDetails.ItemsDiscount).ToString("#,##0.#0"));
            dt.Rows.Add("", ":", "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("Net Sales", ":", (mclsDetails.NetSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("OLD GRAND TOTAL", ":", (mclsDetails.OldGrandTotal).ToString("#,##0.#0"));
            dt.Rows.Add("This Total Amount", ":", (mclsDetails.GrossSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("", ":", "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 66, '-'));
            dt.Rows.Add("NEW GRAND TOTAL", ":", (mclsDetails.OldGrandTotal + (mclsDetails.GrossSales * ((100 - mclsDetails.TrustFund) / 100))).ToString("#,##0.#0"));

            dt.Rows.Add("Taxables Breakdown", "", "");
            dt.Rows.Add("VAT Exempt", ":", (mclsDetails.VATExempt * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("VAT Zero Rated", ":", (mclsDetails.VATZeroRated * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("NonVATable Amount", ":", (mclsDetails.NonVATableAmount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("VATable Amount", ":", (mclsDetails.VATableAmount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add(mclsTerminalDetails.VAT.ToString("##") + "% VAT", ":", (mclsDetails.VAT * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Local Tax", ":", (mclsDetails.LocalTax * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));

            dt.Rows.Add("Total Amount Breakdown", "", "");
            dt.Rows.Add("Cash Sales", ":", (mclsDetails.CashSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Cheque Sales", ":", (mclsDetails.ChequeSales* ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit Card Sales", ":", (mclsDetails.CreditCardSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit (Charge)", ":", (mclsDetails.CreditSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit Payment", ":", (mclsDetails.CreditPayment * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Cash", ":", (mclsDetails.CreditPaymentCash * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Cheque", ":", (mclsDetails.CreditPaymentCheque * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Credit Card", ":", (mclsDetails.CreditPaymentCreditCard * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Debit", ":", (mclsDetails.CreditPaymentDebit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Debit Sales", ":", (mclsDetails.DebitPayment * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("     Rewards Points Redeemed", ":", (mclsDetails.RewardPointsPayment * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Employee Acct.", ":", "0.00");
            dt.Rows.Add("Void Sales", ":", (mclsDetails.VoidSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Refund Sales", ":", (mclsDetails.RefundSales * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Cash", ":", (mclsDetails.RefundCash * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Cheque", ":", (mclsDetails.RefundCheque * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Credit Card", ":", (mclsDetails.RefundCreditCard * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Credit", ":", (mclsDetails.RefundCredit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("      Debit", ":", (mclsDetails.RefundDebit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));

            dt.Rows.Add("Discounts", "", "");
            dt.Rows.Add("Items Discount", ":", (mclsDetails.ItemsDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Subtotal Discount", ":", (mclsDetails.SubTotalDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("     Senior Citizen", ":", (mclsDetails.SNRDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("     PWD", ":", (mclsDetails.PWDDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("     Others", ":", (mclsDetails.OtherDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Total Discounts", ":", (mclsDetails.TotalDiscount * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));

            Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsReceipt.Connection, clsReceipt.Transaction);

            System.Data.DataTable dtDiscounts = clsSalesTransactions.Discounts(mclsDetails.BranchID, mclsDetails.TerminalNo, mclsDetails.BeginningTransactionNo, mclsDetails.EndingTransactionNo);
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Add("-", "-", "-");
                dt.Rows.Add("Subtotal Discounts Breakdown", "", "");
                dt.Rows.Add("-", "-", "-");
                foreach (System.Data.DataRow dr in dtDiscounts.Rows)
                {
                    dt.Rows.Add(dr["DiscountCode"].ToString(), ":", (decimal.Parse(dr["Discount"].ToString()) * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
                }
            }

            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Drawer Information", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Beginning Balance", ":", (mclsDetails.BeginningBalance * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Cash In Drawer", ":", (mclsDetails.CashInDrawer * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Paid Out", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Paid Out", ":", (mclsDetails.TotalPaidOut * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("PICK UP / Disburstment", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", (mclsDetails.CashDisburse * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", (mclsDetails.ChequeDisburse * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", (mclsDetails.CreditCardDisburse * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Receive on Account", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", (mclsDetails.CashWithHold * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", (mclsDetails.ChequeWithHold * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", (mclsDetails.CreditCardWithHold * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Customer Deposits", "", "");
            dt.Rows.Add("-", "-", "-");
            dt.Rows.Add("Cash", ":", (mclsDetails.CashDeposit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Cheque", ":", (mclsDetails.ChequeDeposit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
            dt.Rows.Add("Credit Card", ":", (mclsDetails.CreditCardDeposit * ((100 - mclsDetails.TrustFund) / 100)).ToString("#,##0.00"));
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
            dgvItems.Columns["Value"].Width = dgvItems.Width - 250 - 20;

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