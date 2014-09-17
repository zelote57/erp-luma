using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

using AceSoft.RetailPlus.Reports;

namespace AceSoft.RetailPlus.Client.UI
{
    public class HourlyReportWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblReportDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panReport;
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
        private System.Windows.Forms.DataGrid dgHourlyReport;
        private System.Windows.Forms.DataGridTableStyle dgStyle;
        private System.Windows.Forms.DataGridTextBoxColumn TIME;
        private System.Windows.Forms.DataGridTextBoxColumn TRAN;
        private System.Windows.Forms.DataGridTextBoxColumn AMOUNT;
        private System.Windows.Forms.DataGridTextBoxColumn DISC;
        private System.Windows.Forms.Label lblTotalTran;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalDiscount;
        private System.ComponentModel.Container components = null;
        private Button cmdCancel;
        private Button cmdEnter;

        #region Public Properties

        private string mCashierName;
        public string CashierName
        {
            set { mCashierName = value; }
        }

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private System.Data.DataTable mdtHourlyReport;
        public System.Data.DataTable dtHourlyReport
        {
            set
            {
                mdtHourlyReport = value;
            }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set
            {
                mclsTerminalDetails = value;
            }
        }

        #endregion

        #region Constructors and Destructors

        public HourlyReportWnd()
        {
            InitializeComponent();
        }

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

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panReport = new System.Windows.Forms.Panel();
            this.lblTotalTran = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.dgHourlyReport = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TIME = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TRAN = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AMOUNT = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DISC = new System.Windows.Forms.DataGridTextBoxColumn();
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgHourlyReport)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 0;
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
            this.lblDescription.TabIndex = 90;
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
            this.lblReportDesc.Size = new System.Drawing.Size(136, 13);
            this.lblReportDesc.TabIndex = 89;
            this.lblReportDesc.Text = "Hourly Report Window.";
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
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hourly Report Details";
            // 
            // panReport
            // 
            this.panReport.AutoScroll = true;
            this.panReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panReport.Controls.Add(this.lblTotalTran);
            this.panReport.Controls.Add(this.lblTotalAmount);
            this.panReport.Controls.Add(this.lblTotalDiscount);
            this.panReport.Controls.Add(this.dgHourlyReport);
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
            // lblTotalTran
            // 
            this.lblTotalTran.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotalTran.Location = new System.Drawing.Point(75, 372);
            this.lblTotalTran.Name = "lblTotalTran";
            this.lblTotalTran.Size = new System.Drawing.Size(50, 15);
            this.lblTotalTran.TabIndex = 242;
            this.lblTotalTran.Text = "0";
            this.lblTotalTran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotalAmount.Location = new System.Drawing.Point(126, 372);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(146, 15);
            this.lblTotalAmount.TabIndex = 241;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.BackColor = System.Drawing.Color.DarkGray;
            this.lblTotalDiscount.Location = new System.Drawing.Point(273, 372);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(80, 15);
            this.lblTotalDiscount.TabIndex = 240;
            this.lblTotalDiscount.Text = "0.00";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgHourlyReport
            // 
            this.dgHourlyReport.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgHourlyReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgHourlyReport.BackColor = System.Drawing.Color.White;
            this.dgHourlyReport.BackgroundColor = System.Drawing.Color.White;
            this.dgHourlyReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgHourlyReport.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgHourlyReport.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgHourlyReport.CaptionVisible = false;
            this.dgHourlyReport.DataMember = "";
            this.dgHourlyReport.Enabled = false;
            this.dgHourlyReport.FlatMode = true;
            this.dgHourlyReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgHourlyReport.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgHourlyReport.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgHourlyReport.HeaderForeColor = System.Drawing.Color.White;
            this.dgHourlyReport.Location = new System.Drawing.Point(22, 139);
            this.dgHourlyReport.Name = "dgHourlyReport";
            this.dgHourlyReport.ReadOnly = true;
            this.dgHourlyReport.RowHeadersVisible = false;
            this.dgHourlyReport.RowHeaderWidth = 5;
            this.dgHourlyReport.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgHourlyReport.SelectionForeColor = System.Drawing.Color.White;
            this.dgHourlyReport.Size = new System.Drawing.Size(319, 516);
            this.dgHourlyReport.TabIndex = 239;
            this.dgHourlyReport.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgHourlyReport.TabStop = false;
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgStyle.ColumnHeadersVisible = false;
            this.dgStyle.DataGrid = this.dgHourlyReport;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.TIME,
            this.TRAN,
            this.AMOUNT,
            this.DISC});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblHourlyReport";
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 5;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // TIME
            // 
            this.TIME.Format = "0#:00";
            this.TIME.FormatInfo = null;
            this.TIME.HeaderText = "TIME";
            this.TIME.MappingName = "Time";
            this.TIME.ReadOnly = true;
            this.TIME.Width = 50;
            // 
            // TRAN
            // 
            this.TRAN.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.TRAN.Format = "#,##0.#0";
            this.TRAN.FormatInfo = null;
            this.TRAN.HeaderText = "TRAN";
            this.TRAN.MappingName = "TranCount";
            this.TRAN.ReadOnly = true;
            this.TRAN.Width = 30;
            // 
            // AMOUNT
            // 
            this.AMOUNT.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.AMOUNT.Format = "#,##0.#0";
            this.AMOUNT.FormatInfo = null;
            this.AMOUNT.HeaderText = "AMOUNT";
            this.AMOUNT.MappingName = "Amount";
            this.AMOUNT.ReadOnly = true;
            this.AMOUNT.Width = 70;
            // 
            // DISC
            // 
            this.DISC.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.DISC.Format = "#,##0.#0";
            this.DISC.FormatInfo = null;
            this.DISC.HeaderText = "DISCOUNT";
            this.DISC.MappingName = "Discount";
            this.DISC.ReadOnly = true;
            this.DISC.Width = 70;
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
            this.lblReportFooter3.Location = new System.Drawing.Point(22, 431);
            this.lblReportFooter3.Name = "lblReportFooter3";
            this.lblReportFooter3.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter3.TabIndex = 227;
            this.lblReportFooter3.Text = "ReportFooter3";
            this.lblReportFooter3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter2
            // 
            this.lblReportFooter2.Location = new System.Drawing.Point(22, 415);
            this.lblReportFooter2.Name = "lblReportFooter2";
            this.lblReportFooter2.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter2.TabIndex = 226;
            this.lblReportFooter2.Text = "ReportFooter1";
            this.lblReportFooter2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter1
            // 
            this.lblReportFooter1.Location = new System.Drawing.Point(22, 399);
            this.lblReportFooter1.Name = "lblReportFooter1";
            this.lblReportFooter1.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter1.TabIndex = 225;
            this.lblReportFooter1.Text = "ReportFooter1";
            this.lblReportFooter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelBot
            // 
            this.lblPanelBot.Location = new System.Drawing.Point(22, 455);
            this.lblPanelBot.Name = "lblPanelBot";
            this.lblPanelBot.Size = new System.Drawing.Size(331, 15);
            this.lblPanelBot.TabIndex = 168;
            this.lblPanelBot.Text = "-/-";
            this.lblPanelBot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(22, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 5);
            this.label2.TabIndex = 129;
            this.label2.Text = "----------------------------------------------------------------------------";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReceiptDesc
            // 
            this.lblReceiptDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptDesc.Location = new System.Drawing.Point(22, 115);
            this.lblReceiptDesc.Name = "lblReceiptDesc";
            this.lblReceiptDesc.Size = new System.Drawing.Size(331, 15);
            this.lblReceiptDesc.TabIndex = 122;
            this.lblReceiptDesc.Text = "Hourly Report";
            this.lblReceiptDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label23.Location = new System.Drawing.Point(22, 107);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(331, 5);
            this.label23.TabIndex = 121;
            this.label23.Text = "----------------------------------------------------------------------------";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportHeader4
            // 
            this.lblReportHeader4.Location = new System.Drawing.Point(22, 88);
            this.lblReportHeader4.Name = "lblReportHeader4";
            this.lblReportHeader4.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader4.TabIndex = 117;
            this.lblReportHeader4.Text = "ReportHeader4";
            this.lblReportHeader4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader3
            // 
            this.lblReportHeader3.ForeColor = System.Drawing.Color.Blue;
            this.lblReportHeader3.Location = new System.Drawing.Point(22, 72);
            this.lblReportHeader3.Name = "lblReportHeader3";
            this.lblReportHeader3.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader3.TabIndex = 116;
            this.lblReportHeader3.Text = "ReportHeader3";
            this.lblReportHeader3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader2
            // 
            this.lblReportHeader2.Location = new System.Drawing.Point(22, 56);
            this.lblReportHeader2.Name = "lblReportHeader2";
            this.lblReportHeader2.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader2.TabIndex = 115;
            this.lblReportHeader2.Text = "ReportHeader2";
            this.lblReportHeader2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader1
            // 
            this.lblReportHeader1.Location = new System.Drawing.Point(22, 40);
            this.lblReportHeader1.Name = "lblReportHeader1";
            this.lblReportHeader1.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader1.TabIndex = 114;
            this.lblReportHeader1.Text = "ReportHeader1";
            this.lblReportHeader1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(22, 16);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(331, 15);
            this.lblCompany.TabIndex = 113;
            this.lblCompany.Text = "AceSoft RetailPlus ™";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // HourlyReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblReportDesc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "HourlyReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.HourlyReport_Load);
            this.Resize += new System.EventHandler(this.HourlyReport_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HourlyReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgHourlyReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Windows Form Methods

        private void HourlyReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                    dt = (System.Data.DataTable)dgHourlyReport.DataSource;
                    if (dgHourlyReport.CurrentRowIndex > 0)
                    {
                        index = dgHourlyReport.CurrentRowIndex;
                        dgHourlyReport.CurrentRowIndex -= 1;
                        dgHourlyReport.Select(dgHourlyReport.CurrentRowIndex);
                        dgHourlyReport.UnSelect(index);
                    }
                    break;

                case Keys.Down:
                    dt = (System.Data.DataTable)dgHourlyReport.DataSource;
                    if (dgHourlyReport.CurrentRowIndex < dt.Rows.Count - 1)
                    {
                        index = dgHourlyReport.CurrentRowIndex;

                        dgHourlyReport.CurrentRowIndex += 1;
                        dgHourlyReport.Select(dgHourlyReport.CurrentRowIndex);
                        dgHourlyReport.UnSelect(index);
                    }
                    break;
            }
        }

        private void HourlyReport_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/HourlyReport.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            PopulateHourlyReport();
        }

        private void HourlyReport_Resize(object sender, System.EventArgs e)
        {
            SetGridItemsWidth();
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
        private void SetGridItemsWidth()
        {
            dgStyle.GridColumnStyles["Time"].Width = 50;
            dgStyle.GridColumnStyles["TranCount"].Width = 50;
            dgStyle.GridColumnStyles["Amount"].Width = dgHourlyReport.Width - 180;
            dgStyle.GridColumnStyles["Discount"].Width = 80;
        }

        private void PopulateHourlyReport()
        {
            lblCompany.Text = CompanyDetails.CompanyCode;

            Receipt clsReceipt = new Receipt();

            lblReportHeader1.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader1").Value);
            lblReportHeader2.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader2").Value);
            lblReportHeader3.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader3").Value);
            lblReportHeader4.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportHeader4").Value);

            this.dgStyle.MappingName = mdtHourlyReport.TableName;
            dgHourlyReport.DataSource = mdtHourlyReport;
            decimal TotalTranCount = 0;
            decimal TotalAmount = 0;
            decimal TotalDiscount = 0;
            try
            {
                dgHourlyReport.Select(0);
                foreach (System.Data.DataRow dr in mdtHourlyReport.Rows)
                {
                    TotalTranCount += Convert.ToDecimal(dr["TranCount"]);
                    TotalAmount += Convert.ToDecimal(dr["Amount"]);
                    TotalDiscount += Convert.ToDecimal(dr["Discount"]);
                }
            }
            catch { }
            lblTotalTran.Text = TotalTranCount.ToString("#,##0");
            lblTotalAmount.Text = TotalAmount.ToString("#,##0.#0");
            lblTotalDiscount.Text = TotalDiscount.ToString("#,##0.#0");

            lblReportFooter1.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter1").Value);
            lblReportFooter2.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter2").Value);
            lblReportFooter3.Text = GetReceiptFormatParameter(clsReceipt.Details("ReportFooter3").Value);

            clsReceipt.CommitAndDispose();

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

        #endregion

        
    }
}
