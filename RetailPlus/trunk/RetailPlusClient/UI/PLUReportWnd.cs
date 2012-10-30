using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    /// <summary>
    /// Summary description for PLUReportWnd.
    /// </summary>
    public class PLUReportWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panReport;
        private System.Windows.Forms.DataGridTableStyle dgStyle;
        private System.Windows.Forms.DataGridTextBoxColumn ITEM;
        private System.Windows.Forms.DataGridTextBoxColumn QTY;
        private System.Windows.Forms.DataGridTextBoxColumn AMOUNT;
        private System.Windows.Forms.Label lblPanelTop;
        private System.Windows.Forms.Label lblReportFooter3;
        private System.Windows.Forms.Label lblReportFooter2;
        private System.Windows.Forms.Label lblReportFooter1;
        private System.Windows.Forms.Label lblPanelBot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReceiptDesc;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblReportHeader4;
        private System.Windows.Forms.Label lblReportHeader3;
        private System.Windows.Forms.Label lblReportHeader2;
        private System.Windows.Forms.Label lblReportHeader1;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblReportDesc;
        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalQuantity;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGrid dgPLUReport;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;

        private System.Data.DataTable mdtPLUReport;
        private string mCashierName;
        private DialogResult dialog;
        private Reports.ReceiptFormatDetails mclsReceiptFormatDetails;
        private DateTime mEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGrandTotal;
        private DateTime mStartDate;
        private System.Windows.Forms.Label lblVATName;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCharge;
        private Data.CashierReportDetails mCashierReportDetails;
        private Button cmdCancel;
        private Button cmdEnter;
        private bool mboIsVATInclusive;

        #region Public Get/Set Properties

        public System.Data.DataTable dtPLUReport
        {
            set { mdtPLUReport = value; }
        }
        public Reports.ReceiptFormatDetails ReceiptFormatDetails
        {
            set { mclsReceiptFormatDetails = value; }
        }
        public DateTime StartDate
        {
            set { mStartDate = value; }
        }
        public DateTime EndDate
        {
            set { mEndDate = value; }
        }
        public DialogResult Result
        {
            get { return dialog; }
        }
        public string CashierName
        {
            set { mCashierName = value; }
        }
        public Data.CashierReportDetails CashierReportDetail
        {
            set { mCashierReportDetails = value; }
        }
        public bool IsVATInclusive
        {
            set { mboIsVATInclusive = value; }
        }

        #endregion

        #region Constructors and Destructors
        public PLUReportWnd()
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
            System.Windows.Forms.DataGridTextBoxColumn TERMINALNO;
            System.Windows.Forms.DataGridTextBoxColumn ORDERSLIPPRINTER;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panReport = new System.Windows.Forms.Panel();
            this.lblVATName = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalQuantity = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.dgPLUReport = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.ITEM = new System.Windows.Forms.DataGridTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AMOUNT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblPanelTop = new System.Windows.Forms.Label();
            this.lblReportFooter3 = new System.Windows.Forms.Label();
            this.lblReportFooter2 = new System.Windows.Forms.Label();
            this.lblReportFooter1 = new System.Windows.Forms.Label();
            this.lblPanelBot = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReceiptDesc = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblReportHeader4 = new System.Windows.Forms.Label();
            this.lblReportHeader3 = new System.Windows.Forms.Label();
            this.lblReportHeader2 = new System.Windows.Forms.Label();
            this.lblReportHeader1 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            TERMINALNO = new System.Windows.Forms.DataGridTextBoxColumn();
            ORDERSLIPPRINTER = new System.Windows.Forms.DataGridTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPLUReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // TERMINALNO
            // 
            TERMINALNO.Format = "";
            TERMINALNO.FormatInfo = null;
            TERMINALNO.MappingName = "TerminalNo";
            TERMINALNO.ReadOnly = true;
            TERMINALNO.Width = 0;
            // 
            // ORDERSLIPPRINTER
            // 
            ORDERSLIPPRINTER.Format = "";
            ORDERSLIPPRINTER.FormatInfo = null;
            ORDERSLIPPRINTER.MappingName = "OrderSlipPrinter";
            ORDERSLIPPRINTER.NullText = "";
            ORDERSLIPPRINTER.ReadOnly = true;
            ORDERSLIPPRINTER.Width = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panReport);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 533);
            this.groupBox1.TabIndex = 92;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLU Report Details";
            // 
            // panReport
            // 
            this.panReport.AutoScroll = true;
            this.panReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panReport.Controls.Add(this.lblVATName);
            this.panReport.Controls.Add(this.lblVAT);
            this.panReport.Controls.Add(this.label7);
            this.panReport.Controls.Add(this.lblCharge);
            this.panReport.Controls.Add(this.label4);
            this.panReport.Controls.Add(this.lblGrandTotal);
            this.panReport.Controls.Add(this.label1);
            this.panReport.Controls.Add(this.lblDiscount);
            this.panReport.Controls.Add(this.lblEndDate);
            this.panReport.Controls.Add(this.lblStartDate);
            this.panReport.Controls.Add(this.lblTotal);
            this.panReport.Controls.Add(this.lblTotalQuantity);
            this.panReport.Controls.Add(this.lblTotalAmount);
            this.panReport.Controls.Add(this.dgPLUReport);
            this.panReport.Controls.Add(this.lblPanelTop);
            this.panReport.Controls.Add(this.lblReportFooter3);
            this.panReport.Controls.Add(this.lblReportFooter2);
            this.panReport.Controls.Add(this.lblReportFooter1);
            this.panReport.Controls.Add(this.lblPanelBot);
            this.panReport.Controls.Add(this.label2);
            this.panReport.Controls.Add(this.lblReceiptDesc);
            this.panReport.Controls.Add(this.label23);
            this.panReport.Controls.Add(this.lblReportHeader4);
            this.panReport.Controls.Add(this.lblReportHeader3);
            this.panReport.Controls.Add(this.lblReportHeader2);
            this.panReport.Controls.Add(this.lblReportHeader1);
            this.panReport.Controls.Add(this.lblCompany);
            this.panReport.Location = new System.Drawing.Point(210, 12);
            this.panReport.Name = "panReport";
            this.panReport.Size = new System.Drawing.Size(385, 515);
            this.panReport.TabIndex = 107;
            // 
            // lblVATName
            // 
            this.lblVATName.BackColor = System.Drawing.Color.DarkGray;
            this.lblVATName.Location = new System.Drawing.Point(25, 413);
            this.lblVATName.Name = "lblVATName";
            this.lblVATName.Size = new System.Drawing.Size(104, 15);
            this.lblVATName.TabIndex = 264;
            this.lblVATName.Text = "Plus 12% VAT";
            this.lblVATName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVAT
            // 
            this.lblVAT.BackColor = System.Drawing.Color.DarkGray;
            this.lblVAT.Location = new System.Drawing.Point(130, 413);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(227, 15);
            this.lblVAT.TabIndex = 263;
            this.lblVAT.Text = "0.00";
            this.lblVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DarkGray;
            this.label7.Location = new System.Drawing.Point(25, 397);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 15);
            this.label7.TabIndex = 262;
            this.label7.Text = "Plus Charges";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharge
            // 
            this.lblCharge.BackColor = System.Drawing.Color.DarkGray;
            this.lblCharge.Location = new System.Drawing.Point(130, 397);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(227, 15);
            this.lblCharge.TabIndex = 261;
            this.lblCharge.Text = "0.00";
            this.lblCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(25, 429);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 248;
            this.label4.Text = "Grand Total";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.BackColor = System.Drawing.Color.DarkGray;
            this.lblGrandTotal.Location = new System.Drawing.Point(130, 429);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(227, 15);
            this.lblGrandTotal.TabIndex = 247;
            this.lblGrandTotal.Text = "0.00";
            this.lblGrandTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(25, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 246;
            this.label1.Text = "Less Discount";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.BackColor = System.Drawing.Color.DarkGray;
            this.lblDiscount.Location = new System.Drawing.Point(130, 381);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(227, 15);
            this.lblDiscount.TabIndex = 245;
            this.lblDiscount.Text = "0.00";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(26, 160);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(331, 15);
            this.lblEndDate.TabIndex = 244;
            this.lblEndDate.Text = "End Date  : mm/dd/yyyy hh:mm AM/PM";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStartDate
            // 
            this.lblStartDate.ForeColor = System.Drawing.Color.Blue;
            this.lblStartDate.Location = new System.Drawing.Point(26, 144);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(331, 15);
            this.lblStartDate.TabIndex = 243;
            this.lblStartDate.Text = "Start Date: mm/dd/yyyy hh:mm AM/PM";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotal.Location = new System.Drawing.Point(25, 364);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(104, 15);
            this.lblTotal.TabIndex = 242;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalQuantity
            // 
            this.lblTotalQuantity.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotalQuantity.Location = new System.Drawing.Point(130, 364);
            this.lblTotalQuantity.Name = "lblTotalQuantity";
            this.lblTotalQuantity.Size = new System.Drawing.Size(146, 15);
            this.lblTotalQuantity.TabIndex = 241;
            this.lblTotalQuantity.Text = "0.00";
            this.lblTotalQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotalAmount.Location = new System.Drawing.Point(277, 364);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(80, 15);
            this.lblTotalAmount.TabIndex = 240;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgPLUReport
            // 
            this.dgPLUReport.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPLUReport.BackColor = System.Drawing.Color.White;
            this.dgPLUReport.BackgroundColor = System.Drawing.Color.White;
            this.dgPLUReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPLUReport.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgPLUReport.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgPLUReport.CaptionVisible = false;
            this.dgPLUReport.DataMember = "";
            this.dgPLUReport.Enabled = false;
            this.dgPLUReport.FlatMode = true;
            this.dgPLUReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPLUReport.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgPLUReport.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPLUReport.HeaderForeColor = System.Drawing.Color.White;
            this.dgPLUReport.Location = new System.Drawing.Point(27, 184);
            this.dgPLUReport.Name = "dgPLUReport";
            this.dgPLUReport.ReadOnly = true;
            this.dgPLUReport.RowHeadersVisible = false;
            this.dgPLUReport.RowHeaderWidth = 5;
            this.dgPLUReport.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgPLUReport.SelectionForeColor = System.Drawing.Color.White;
            this.dgPLUReport.Size = new System.Drawing.Size(330, 177);
            this.dgPLUReport.TabIndex = 239;
            this.dgPLUReport.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgPLUReport.TabStop = false;
            // 
            // dgStyle
            // 
            this.dgStyle.AllowSorting = false;
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgStyle.ColumnHeadersVisible = false;
            this.dgStyle.DataGrid = this.dgPLUReport;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            TERMINALNO,
            ORDERSLIPPRINTER,
            this.ITEM,
            this.QTY,
            this.AMOUNT});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblPLUReport";
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 0;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // ITEM
            // 
            this.ITEM.Format = "";
            this.ITEM.FormatInfo = null;
            this.ITEM.HeaderText = "ITEM";
            this.ITEM.MappingName = "ProductCode";
            this.ITEM.ReadOnly = true;
            this.ITEM.Width = 75;
            // 
            // QTY
            // 
            this.QTY.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.QTY.Format = "#,##0.#0";
            this.QTY.FormatInfo = null;
            this.QTY.HeaderText = "QTY";
            this.QTY.MappingName = "Quantity";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 75;
            // 
            // AMOUNT
            // 
            this.AMOUNT.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.AMOUNT.Format = "#,##0.#0";
            this.AMOUNT.FormatInfo = null;
            this.AMOUNT.HeaderText = "Amount";
            this.AMOUNT.MappingName = "Amount";
            this.AMOUNT.ReadOnly = true;
            this.AMOUNT.Width = 75;
            // 
            // lblPanelTop
            // 
            this.lblPanelTop.Location = new System.Drawing.Point(24, -8);
            this.lblPanelTop.Name = "lblPanelTop";
            this.lblPanelTop.Size = new System.Drawing.Size(331, 15);
            this.lblPanelTop.TabIndex = 238;
            this.lblPanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter3
            // 
            this.lblReportFooter3.Location = new System.Drawing.Point(26, 485);
            this.lblReportFooter3.Name = "lblReportFooter3";
            this.lblReportFooter3.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter3.TabIndex = 227;
            this.lblReportFooter3.Text = "ReportFooter3";
            this.lblReportFooter3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter2
            // 
            this.lblReportFooter2.Location = new System.Drawing.Point(26, 469);
            this.lblReportFooter2.Name = "lblReportFooter2";
            this.lblReportFooter2.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter2.TabIndex = 226;
            this.lblReportFooter2.Text = "ReportFooter1";
            this.lblReportFooter2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter1
            // 
            this.lblReportFooter1.Location = new System.Drawing.Point(26, 453);
            this.lblReportFooter1.Name = "lblReportFooter1";
            this.lblReportFooter1.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter1.TabIndex = 225;
            this.lblReportFooter1.Text = "ReportFooter1";
            this.lblReportFooter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelBot
            // 
            this.lblPanelBot.Location = new System.Drawing.Point(26, 509);
            this.lblPanelBot.Name = "lblPanelBot";
            this.lblPanelBot.Size = new System.Drawing.Size(331, 15);
            this.lblPanelBot.TabIndex = 168;
            this.lblPanelBot.Text = "-/-";
            this.lblPanelBot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(26, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 5);
            this.label2.TabIndex = 129;
            this.label2.Text = "----------------------------------------------------------------------------";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReceiptDesc
            // 
            this.lblReceiptDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptDesc.Location = new System.Drawing.Point(26, 115);
            this.lblReceiptDesc.Name = "lblReceiptDesc";
            this.lblReceiptDesc.Size = new System.Drawing.Size(331, 15);
            this.lblReceiptDesc.TabIndex = 122;
            this.lblReceiptDesc.Text = "PLU REPORT";
            this.lblReceiptDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label23.Location = new System.Drawing.Point(26, 107);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(331, 5);
            this.label23.TabIndex = 121;
            this.label23.Text = "----------------------------------------------------------------------------";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportHeader4
            // 
            this.lblReportHeader4.Location = new System.Drawing.Point(26, 88);
            this.lblReportHeader4.Name = "lblReportHeader4";
            this.lblReportHeader4.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader4.TabIndex = 117;
            this.lblReportHeader4.Text = "ReportHeader4";
            this.lblReportHeader4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader3
            // 
            this.lblReportHeader3.ForeColor = System.Drawing.Color.Blue;
            this.lblReportHeader3.Location = new System.Drawing.Point(26, 72);
            this.lblReportHeader3.Name = "lblReportHeader3";
            this.lblReportHeader3.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader3.TabIndex = 116;
            this.lblReportHeader3.Text = "ReportHeader3";
            this.lblReportHeader3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader2
            // 
            this.lblReportHeader2.Location = new System.Drawing.Point(26, 56);
            this.lblReportHeader2.Name = "lblReportHeader2";
            this.lblReportHeader2.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader2.TabIndex = 115;
            this.lblReportHeader2.Text = "ReportHeader2";
            this.lblReportHeader2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader1
            // 
            this.lblReportHeader1.Location = new System.Drawing.Point(26, 40);
            this.lblReportHeader1.Name = "lblReportHeader1";
            this.lblReportHeader1.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader1.TabIndex = 114;
            this.lblReportHeader1.Text = "ReportHeader1";
            this.lblReportHeader1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(26, 16);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(331, 15);
            this.lblCompany.TabIndex = 113;
            this.lblCompany.Text = "AceSoft RetailPlus ™";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 91;
            this.imgIcon.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(762, 41);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(252, 13);
            this.lblDescription.TabIndex = 94;
            this.lblDescription.Tag = "";
            this.lblDescription.Text = "Press Enter Key to print the current viewed report.";
            // 
            // lblReportDesc
            // 
            this.lblReportDesc.AutoSize = true;
            this.lblReportDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblReportDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDesc.ForeColor = System.Drawing.Color.White;
            this.lblReportDesc.Location = new System.Drawing.Point(66, 18);
            this.lblReportDesc.Name = "lblReportDesc";
            this.lblReportDesc.Size = new System.Drawing.Size(120, 13);
            this.lblReportDesc.TabIndex = 93;
            this.lblReportDesc.Text = "PLU Report Window.";
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
            // PLUReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblReportDesc);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PLUReportWnd";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PLUReportWnd_Load);
            this.Resize += new System.EventHandler(this.PLUReportWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PLUReportWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.panReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPLUReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region Windows Form Methods
        private void PLUReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Data.DataTable dt;
            int index;

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

                case Keys.Up:
                    dt = (System.Data.DataTable)dgPLUReport.DataSource;
                    if (dgPLUReport.CurrentRowIndex > 0)
                    {
                        index = dgPLUReport.CurrentRowIndex;
                        dgPLUReport.CurrentRowIndex -= 1;
                        dgPLUReport.Select(dgPLUReport.CurrentRowIndex);
                        dgPLUReport.UnSelect(index);
                    }
                    break;

                case Keys.Down:
                    dt = (System.Data.DataTable)dgPLUReport.DataSource;
                    if (dgPLUReport.CurrentRowIndex < dt.Rows.Count - 1)
                    {
                        index = dgPLUReport.CurrentRowIndex;

                        dgPLUReport.CurrentRowIndex += 1;
                        dgPLUReport.Select(dgPLUReport.CurrentRowIndex);
                        dgPLUReport.UnSelect(index);
                    }
                    break;
            }
        }

        private void PLUReportWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/GroupReport.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            PopulatePLUReport();
            SetGridItemsWidth();
        }

        private void PLUReportWnd_Resize(object sender, System.EventArgs e)
        {
            SetGridItemsWidth();
        }

        #endregion

        #region Private Methods

        private void SetGridItemsWidth()
        {
            dgStyle.GridColumnStyles["TerminalNo"].Width = 0;
            dgStyle.GridColumnStyles["OrderSlipPrinter"].Width = 0;
            dgStyle.GridColumnStyles["ProductCode"].Width = dgPLUReport.Width - 190;
            dgStyle.GridColumnStyles["Quantity"].Width = 82;
            dgStyle.GridColumnStyles["Amount"].Width = 90;
        }
        private void PopulatePLUReport()
        {
            lblCompany.Text = CompanyDetails.CompanyCode;

            lblStartDate.Text = "Start Date: " + mStartDate.ToString("MM/dd/yyyy hh:mm:ss tt") + Environment.NewLine;
            lblEndDate.Text = "End Date  : " + mEndDate.ToString("MM/dd/yyyy hh:mm:ss tt") + Environment.NewLine + Environment.NewLine;

            lblReportHeader1.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportHeader1);
            lblReportHeader2.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportHeader2);
            lblReportHeader3.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportHeader3);
            lblReportHeader4.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportHeader4);

            dgPLUReport.DataSource = mdtPLUReport;
            decimal TotalQuantity = 0;
            decimal TotalAmount = 0;
            try
            {
                foreach (System.Data.DataRow dr in mdtPLUReport.Rows)
                {
                    TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
                    TotalAmount += Convert.ToDecimal(dr["Amount"]);
                }
                dgPLUReport.Select(0);
            }
            catch { }
            lblTotalQuantity.Text = TotalQuantity.ToString("#,##0.#0");
            lblTotalAmount.Text = TotalAmount.ToString("#,##0.#0");

            lblDiscount.Text = mCashierReportDetails.SubTotalDiscount.ToString("#,##0.#0");
            lblCharge.Text = mCashierReportDetails.TotalCharge.ToString("#,##0.#0");
            lblVAT.Text = mCashierReportDetails.VAT.ToString("#,##0.#0");

            lblVAT.Visible = !mboIsVATInclusive;
            lblVATName.Visible = !mboIsVATInclusive;

            decimal GrandTotal = mCashierReportDetails.DailySales + mCashierReportDetails.VAT + mCashierReportDetails.TotalCharge;
            lblGrandTotal.Text = GrandTotal.ToString("#,##0.#0");

            lblReportFooter1.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportFooter1);
            lblReportFooter2.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportFooter2);
            lblReportFooter3.Text = GetReceiptFormatParameter(mclsReceiptFormatDetails.ReportFooter3);

        }
        private string GetReceiptFormatParameter(string stReceiptFormat)
        {
            string stRetValue = "";

            if (stReceiptFormat == Reports.ReceiptFieldFormats.Blank)
            {
                stRetValue = "";
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.Spacer)
            {
                stRetValue = Environment.NewLine;
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.DateNow)
            {
                stRetValue = DateTime.Now.ToString("MMM dd yyyy hh:mh tt");
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.Cashier)
            {
                stRetValue = "Cashier: " + mCashierName;
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.TerminalNo)
            {
                stRetValue = "Terminal No.: " + CompanyDetails.TerminalNo;
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.MachineSerialNo)
            {
                stRetValue = "MIN: " + CONFIG.MachineSerialNo;
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.AccreditationNo)
            {
                stRetValue = "Acc. No.: " + CONFIG.AccreditationNo;
            }
            else if (stReceiptFormat == Reports.ReceiptFieldFormats.InvoiceNo)
            {
                stRetValue = "OFFICIAL RECEIPT #: " + "N/A";
            }
            else
            {
                stRetValue = stReceiptFormat;
            }

            return stRetValue;
        }

        #endregion

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

    }
}