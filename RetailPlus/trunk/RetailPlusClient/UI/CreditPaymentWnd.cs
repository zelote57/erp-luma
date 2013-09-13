using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    public class CreditPaymentWnd : Form
    {
        private Label lblBalanceAmount;
        private Label lblHeader;
        private GroupBox groupBox1;
        private Label lblRemarks;
        private TextBox txtRemarks;
        private TextBox txtAmount;
        private Label lblBalance;
        private PictureBox imgIcon;
        private Label lblCredit;
        private System.ComponentModel.Container components = null;
        private Label lblAllowedCredit;
        private Label lblAllowedCreditLabel;
        private Button cmdCancel;
        private Button cmdEnter;

        private decimal mdecAllowedCredit = 0;
        private DialogResult dialog;
        private decimal mdecBalanceAmount;
        private Data.ContactDetails mclsCustomerDetails;
        private Data.CreditPaymentDetails mDetails = new Data.CreditPaymentDetails();
        private Data.SalesTransactionDetails mclsSalesTransactionDetails;

        public decimal AllowedCredit
        {
            set { mdecAllowedCredit = value; }
        }
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }
        public Data.SalesTransactionDetails SalesTransactionDetails
        {
            set
            {
                mclsSalesTransactionDetails  = value;
            }
        }
        public Data.ContactDetails CustomerDetails
        {
            set { mclsCustomerDetails = value; }
        }
        public decimal BalanceAmount
        {
            set
            {
                mdecBalanceAmount = value;
            }
        }
        public Data.CreditPaymentDetails Details
        {
            get
            { return mDetails; }
        }

        public CreditPaymentWnd()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        #region Windows Form Designer generated code

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
        private void InitializeComponent()
        {
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblAllowedCredit = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblAllowedCreditLabel = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(118, 31);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(649, 32);
            this.lblBalanceAmount.TabIndex = 5;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(132, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Tender Credit Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblAllowedCredit);
            this.groupBox1.Controls.Add(this.lblCredit);
            this.groupBox1.Controls.Add(this.lblAllowedCreditLabel);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.lblBalance);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(361, 134);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(267, 13);
            this.lblRemarks.TabIndex = 3;
            this.lblRemarks.Text = "Add an optional 255 character remarks below.";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(118, 153);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(723, 56);
            this.txtRemarks.TabIndex = 1;
            // 
            // lblAllowedCredit
            // 
            this.lblAllowedCredit.BackColor = System.Drawing.Color.Transparent;
            this.lblAllowedCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllowedCredit.ForeColor = System.Drawing.Color.DarkSalmon;
            this.lblAllowedCredit.Location = new System.Drawing.Point(118, 7);
            this.lblAllowedCredit.Name = "lblAllowedCredit";
            this.lblAllowedCredit.Size = new System.Drawing.Size(649, 32);
            this.lblAllowedCredit.TabIndex = 6;
            this.lblAllowedCredit.Text = "0.00";
            this.lblAllowedCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredit.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCredit.Location = new System.Drawing.Point(434, 58);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(124, 13);
            this.lblCredit.TabIndex = 2;
            this.lblCredit.Text = "Credit Amount (PHP)";
            // 
            // lblAllowedCreditLabel
            // 
            this.lblAllowedCreditLabel.AutoSize = true;
            this.lblAllowedCreditLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblAllowedCreditLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllowedCreditLabel.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblAllowedCreditLabel.Location = new System.Drawing.Point(775, 14);
            this.lblAllowedCreditLabel.Name = "lblAllowedCreditLabel";
            this.lblAllowedCreditLabel.Size = new System.Drawing.Size(225, 19);
            this.lblAllowedCreditLabel.TabIndex = 8;
            this.lblAllowedCreditLabel.Text = "Allowed consignment amount.";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(392, 80);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblBalance.Location = new System.Drawing.Point(776, 38);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(201, 19);
            this.lblBalance.TabIndex = 7;
            this.lblBalance.Text = "Current Balance to be paid.";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 82;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
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
            this.cmdCancel.TabIndex = 2;
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
            this.cmdEnter.TabIndex = 1;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CreditPaymentWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditPaymentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditPaymentWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Windows Form Methods

        private void CreditPaymentWnd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.PageUp:
                    SendKeys.Send("+{TAB}");
                    break;

                case Keys.PageDown:
                    SendKeys.Send("{TAB}");
                    break;

                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    if (isValuesAssigned())
                    {
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    break;
            }
        }
        private void CreditPaymentWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CreditPayment.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            lblAllowedCredit.Text = mdecAllowedCredit.ToString("#,##0.#0");
            lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            lblCredit.Text = "Credit Amount (" + CompanyDetails.Currency + ")";

            if (mclsSalesTransactionDetails.isConsignment) {
                lblHeader.Text = "Tender Consignment Amount";
                lblAllowedCreditLabel.Text = "Allowed consignment amount.";
            }
            else {
                lblHeader.Text = "Tender Credit Amount";
                lblAllowedCreditLabel.Text = "Allowed credit amount.";
            }
        }

        #endregion

        #region Windows Control Methods

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (isValuesAssigned())
            {
                dialog = DialogResult.OK;
                this.Hide();
            }
        }

        #endregion

        #region Private Methods

        private bool isValuesAssigned()
        {
            decimal Amount;
            try
            {
                Amount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                txtAmount.Focus();
                MessageBox.Show("Sorry you have entered an invalid amount for credit payment." +
                    "Please type a valid credit amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Amount > mdecAllowedCredit)
            {
                txtAmount.Focus();
                MessageBox.Show("Amount must be less than the credit limit (" + mdecAllowedCredit.ToString("#,##0.#0") + "). Please enter a lower amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Amount > mdecBalanceAmount)
            {
                txtAmount.Focus();
                MessageBox.Show("Amount must be less than the balance amount (" + mdecBalanceAmount.ToString("#,##0.#0") + "). Please enter a lower or equal amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Amount <= 0)
            {
                txtAmount.Focus();
                MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
            mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;
            mDetails.CashierName = mclsSalesTransactionDetails.CashierName;
            mDetails.TerminalNo = mclsSalesTransactionDetails.TerminalNo;
            mDetails.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
            mDetails.CustomerDetails = mclsCustomerDetails;
            mDetails.Amount = Amount;
            mDetails.Remarks = txtRemarks.Text;

            return true;
        }

        #endregion

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
    }
}
