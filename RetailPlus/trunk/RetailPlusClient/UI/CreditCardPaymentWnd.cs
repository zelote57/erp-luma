using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
    public class CreditCardPaymentWnd : Form
    {

        private System.ComponentModel.Container components = null;
        private Label lblHeader;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private Label lblRemarks;
        private TextBox txtRemarks;
        private TextBox txtAmount;
        private Label label3;
        private Label label4;
        private Label label5;
        private PictureBox imgIcon;
        private TextBox txtCardNo;
        private TextBox txtValidityDates;
        private ComboBox cboCardType;
        private Label lblCreditCard;
        private TextBox txtCardHolder;
        //private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label label8;
        private Label lblBalanceAmount;

        #region public Properties

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private decimal mdecBalanceAmount;
        public decimal BalanceAmount
        {
            set
            { mdecBalanceAmount = value; }
        }

        private Data.SalesTransactionDetails mclsSalesTransactionDetails;
        public Data.SalesTransactionDetails SalesTransactionDetails
        {
            set { mclsSalesTransactionDetails = value; }
        }

        private Data.CreditCardPaymentDetails mDetails = new Data.CreditCardPaymentDetails();
        public Data.CreditCardPaymentDetails Details
        {
            get
            { return mDetails; }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set { mclsTerminalDetails = value; }
        }

        #endregion

        #region Constructors and Destructors

        public CreditCardPaymentWnd()
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.cboCardType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCardHolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidityDates = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 7;
            this.imgIcon.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(161, 13);
            this.lblHeader.TabIndex = 8;
            this.lblHeader.Text = "Tender Credit Card Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.cboCardType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCardHolder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCardNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtValidityDates);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblCreditCard);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label8.Location = new System.Drawing.Point(801, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(201, 19);
            this.label8.TabIndex = 92;
            this.label8.Text = "Current Balance to be paid.";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(184, 30);
            this.lblBalanceAmount.TabIndex = 93;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCardType
            // 
            this.cboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCardType.Location = new System.Drawing.Point(203, 70);
            this.cboCardType.Name = "cboCardType";
            this.cboCardType.Size = new System.Drawing.Size(200, 31);
            this.cboCardType.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(67, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 91;
            this.label5.Text = "Card Holder:";
            // 
            // txtCardHolder
            // 
            this.txtCardHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardHolder.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardHolder.Location = new System.Drawing.Point(203, 104);
            this.txtCardHolder.MaxLength = 0;
            this.txtCardHolder.Name = "txtCardHolder";
            this.txtCardHolder.Size = new System.Drawing.Size(424, 30);
            this.txtCardHolder.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(410, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Card No.:";
            // 
            // txtCardNo
            // 
            this.txtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNo.Location = new System.Drawing.Point(467, 70);
            this.txtCardNo.MaxLength = 0;
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(160, 30);
            this.txtCardNo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(404, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "(Format: mmyy)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(67, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Card Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(67, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Expiration Date:";
            // 
            // txtValidityDates
            // 
            this.txtValidityDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValidityDates.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidityDates.Location = new System.Drawing.Point(203, 138);
            this.txtValidityDates.MaxLength = 4;
            this.txtValidityDates.Name = "txtValidityDates";
            this.txtValidityDates.Size = new System.Drawing.Size(200, 30);
            this.txtValidityDates.TabIndex = 4;
            this.txtValidityDates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidityDates_KeyPress);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(67, 175);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(126, 13);
            this.lblRemarks.TabIndex = 17;
            this.lblRemarks.Text = "Remarks (optional) : ";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(203, 172);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(585, 41);
            this.txtRemarks.TabIndex = 5;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.AutoSize = true;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCreditCard.Location = new System.Drawing.Point(67, 39);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(119, 13);
            this.lblCreditCard.TabIndex = 15;
            this.lblCreditCard.Text = "Card Amount (PHP):";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(203, 36);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
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
            this.cmdCancel.TabIndex = 7;
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
            this.cmdEnter.TabIndex = 6;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CreditCardPaymentWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreditCardPaymentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditCardPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditCardPaymentWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Windows Form Methods

        private void CreditCardPaymentWnd_KeyDown(object sender, KeyEventArgs e)
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
        private void CreditCardPaymentWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CreditCardPayment.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            LoadOptions();
        }

        #endregion

        #region Windows Control Methods

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
        }
        private void txtValidityDates_KeyPress(object sender, KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
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

        private void LoadOptions()
        {
            lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");

            txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            txtAmount.SelectAll();
            lblCreditCard.Text = "Card Amount (" + CompanyDetails.Currency + "):";

            cboCardType.Items.Clear();
            Data.CardType clsCardType = new Data.CardType();
            foreach (System.Data.DataRow dr in clsCardType.DataList("CardTypeID", SortOption.Ascending).Rows)
            {
                cboCardType.Items.Add(dr["CardTypeName"]);
            }
            clsCardType.CommitAndDispose();

            if (cboCardType.Items.Count > 0)
                cboCardType.SelectedIndex = 0;

        }
        private bool isValuesAssigned()
        {
            decimal mdecAmount = 0;
            try
            {
                mdecAmount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                txtAmount.Focus();
                MessageBox.Show("Sorry you have entered an invalid amount for credit card payment." +
                    "Please type a valid credit amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCardNo.Text == null || txtCardNo.Text == "")
            {
                txtCardNo.Focus();
                MessageBox.Show("Please type a valid Card No.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtCardHolder.Text == null || txtCardHolder.Text == "")
            {
                txtCardHolder.Focus();
                MessageBox.Show("Please type a valid Card Holder.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            DateTime ValidityDate = DateTime.MinValue;
            if (txtValidityDates.Text == null || txtValidityDates.Text == "")
            {
                txtValidityDates.Focus();
                MessageBox.Show("Please type a valid Validity Date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtValidityDates.Text != null && txtValidityDates.Text != "")
            {

                try
                {
                    string Month = txtValidityDates.Text.Substring(0, 2);
                    string Year = "20" + txtValidityDates.Text.Substring(2, 2);
                    string Day = DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month)).ToString();
                    ValidityDate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
                }
                catch
                {
                    txtValidityDates.Focus();
                    MessageBox.Show("Please type a valid Validity Date. Format must be mmyy", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (ValidityDate < DateTime.Now)
                {
                    txtValidityDates.Focus();
                    MessageBox.Show("Card has been expired, please ask for a valid credit card.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            Data.CardType clsCardType = new Data.CardType();
            Data.CardTypeDetails cardtypedetails = clsCardType.Details(cboCardType.Text);
            clsCardType.CommitAndDispose();

            mDetails.BranchDetails = mclsTerminalDetails.BranchDetails;
            mDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
            mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
            mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;
            mDetails.Amount = mdecAmount;
            mDetails.CardTypeID = cardtypedetails.CardTypeID;
            mDetails.CardTypeCode = cardtypedetails.CardTypeCode;
            mDetails.CardTypeName = cardtypedetails.CardTypeName;
            mDetails.CardNo = txtCardNo.Text;
            mDetails.CardHolder = txtCardHolder.Text;
            mDetails.ValidityDates = ValidityDate.ToString("MMddyy");
            mDetails.Remarks = txtRemarks.Text;

            return true;
        }

        #endregion

    }
}
