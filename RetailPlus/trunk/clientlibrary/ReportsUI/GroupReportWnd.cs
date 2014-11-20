using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using MySql.Data.MySqlClient;

using AceSoft.RetailPlus.Reports;

namespace AceSoft.RetailPlus.Client.UI
{
    public class GroupReportWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblReportDesc;
        private System.ComponentModel.Container components = null;
        
        private Button cmdCancel;
        private Button cmdEnter;
        

        #region Public Properties

        private string mCashierName;
        public string CashierName
        {
            set { mCashierName = value; }
        }

        private System.Data.DataTable mdtGroupReport;
        public System.Data.DataTable dtGroupReport
        {
            set
            {
                mdtGroupReport = value;
            }
        }

        private DialogResult dialog;
        public DialogResult Result
        {
            get { return dialog; }
        }

        private Data.TerminalReportDetails mTerminalReportDetails;
        private GroupBox groupBox1;
        private Panel panel1;
        private DataGridView dgvItems;
        private Label lblReceiptDesc;
        private Label lblCompany;
    
        public Data.TerminalReportDetails TerminalReportDetails
        {
            set { mTerminalReportDetails = value; }
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
        public GroupReportWnd()
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblReceiptDesc = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
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
            this.lblReportDesc.Size = new System.Drawing.Size(168, 13);
            this.lblReportDesc.TabIndex = 89;
            this.lblReportDesc.Text = "Department Report Window.";
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
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dept. (Group) Report Details";
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItems.ColumnHeadersHeight = 24;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItems.GridColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle9;
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
            this.lblReceiptDesc.Text = "Group Report";
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
            // GroupReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblReportDesc);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "GroupReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.GroupReportWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GroupReportWnd_KeyDown);
            this.Resize += new System.EventHandler(this.GroupReportWnd_Resize);
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
        private void GroupReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

        private void GroupReportWnd_Load(object sender, System.EventArgs e)
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

            lblCompany.Text = CompanyDetails.CompanyName;

            PopulateGroupReport();
        }

        private void GroupReportWnd_Resize(object sender, System.EventArgs e)
        {

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

        private void PopulateGroupReport()
        {
            Receipt clsReceipt = new Receipt();
            
            // disabled the trustfund
            mTerminalReportDetails.TrustFund = 0;

            mclsTerminalDetails.MaxReceiptWidth = 94;

            System.Data.DataTable dt = new System.Data.DataTable("tblGroupReport");
            dt.Columns.Add("Module");
            dt.Columns.Add("Separator");
            dt.Columns.Add("Value");
            dt.Columns.Add("Prcntg");

            decimal TotalTranCount = 0;
            decimal TotalAmount = 0;
            try
            {
                foreach (System.Data.DataRow dr in mdtGroupReport.Rows)
                {
                    string ProductGroup = dr["ProductGroup"].ToString();
                    string TranCount = Convert.ToDecimal(dr["TranCount"].ToString()).ToString("#,##0");
                    string Amount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");
                    string Percentage = dr["Percentage"].ToString();

                    TotalTranCount += decimal.Parse(Math.Round(Convert.ToDecimal(TranCount) * ((100 - mTerminalReportDetails.TrustFund) / 100), 2).ToString());
                    TotalAmount += decimal.Parse(Math.Round(Convert.ToDecimal(Amount) * ((100 - mTerminalReportDetails.TrustFund) / 100), 2).ToString());
                    dt.Rows.Add(ProductGroup, TranCount, Amount, Percentage);
                }
            }
            catch { }
            dt.Rows.Add("Total", TotalTranCount.ToString("#,##0.#0"), "", TotalAmount.ToString("#,##0.#0"));
            dt.Rows.Add("(+) Items Disc/VatE", ":", "", ((mTerminalReportDetails.GrossSales - TotalAmount) * ((100 - mTerminalReportDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("-----------------------------------", "--", "----------", "----------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 80, '-'));
            dt.Rows.Add("Gross Sales", ":", "", ((mTerminalReportDetails.GrossSales + mTerminalReportDetails.TotalCharge) * ((100 - mTerminalReportDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Service Charge", ":", "", mTerminalReportDetails.TotalCharge.ToString("#,##0.#0"));
            dt.Rows.Add("-----------------------------------", "--", "----------", "----------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 80, '-'));
            dt.Rows.Add("Total Amount", ":", "", (mTerminalReportDetails.GrossSales * ((100 - mTerminalReportDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) " + mclsTerminalDetails.VAT.ToString("##") + "% VAT Exempt ", ":", "", (mTerminalReportDetails.VATExempt / (1 + (mclsTerminalDetails.VAT / 100)) * (mclsTerminalDetails.VAT / 100) * ((100 - mTerminalReportDetails.TrustFund) / 100)).ToString("#,##0.#0"));
            dt.Rows.Add("(-) Discount", ":", "", (mTerminalReportDetails.SubTotalDiscount + mTerminalReportDetails.ItemsDiscount).ToString("#,##0.#0"));
            dt.Rows.Add("-----------------------------------", "--", "----------", "----------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 80, '-'));
            dt.Rows.Add("Net Sales", ":", "", (mTerminalReportDetails.NetSales * ((100 - mTerminalReportDetails.TrustFund) / 100)).ToString("#,##0.#0"));

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
            dgvItems.Columns["Prcntg"].Visible = true;

            dgvItems.Columns["Module"].HeaderText = "Group";
            dgvItems.Columns["Separator"].HeaderText = "Qty";
            dgvItems.Columns["Value"].HeaderText = "Amount";
            dgvItems.Columns["Prcntg"].HeaderText = "Prcntg.";

            dgvItems.Columns["Module"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvItems.Columns["Separator"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvItems.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.Columns["Prcntg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvItems.Columns["Module"].Width = 200;
            dgvItems.Columns["Separator"].Width = 40;
            dgvItems.Columns["Value"].Width = (dgvItems.Width - 220 - 40) / 2;
            dgvItems.Columns["Prcntg"].Width = (dgvItems.Width - 220 - 40) / 2;

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