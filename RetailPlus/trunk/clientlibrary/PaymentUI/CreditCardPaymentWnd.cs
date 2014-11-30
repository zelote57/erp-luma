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
        private TextBox txtSelectedTextBox;
        private Label label8;
        private Label lblBalanceAmount;
        private KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
        private KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private TextBox txtScan;
        private Label label6;

        Data.ContactDetails mclsGuarantorDetails; 
        Data.CardTypeDetails mclsCardTypeDetails;

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

        private Panel panCharge;
        private Label label7;
        private TextBox txtCreditCardCharge;
        private Label lblPlus;
        private Label label9;
        private Label label10;

        private bool mboIsCreditChargeExcluded;
        public bool IsCreditChargeExcluded { set {mboIsCreditChargeExcluded = value;} }

        public Data.TerminalDetails TerminalDetails { get; set; }

        private ArrayList marrCreditCardPaymentDetails = new ArrayList();
        public ArrayList arrCreditCardPaymentDetails
        {
            set { marrCreditCardPaymentDetails = value; }
        }

        public bool IsRefund { get; set; }

        private Data.ContactDetails mclsCreditorDetails;
        public Data.ContactDetails CreditorDetails
        {
            get { return mclsCreditorDetails; }
            set { mclsCreditorDetails = value; }
        }

        #endregion

        #region Constructors and Destructors

        public CreditCardPaymentWnd()
        {
            InitializeComponent();

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
            
            if (Common.isTerminalMultiInstanceEnabled())
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtCardHolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidityDates = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cboCardType = new System.Windows.Forms.ComboBox();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panCharge = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCreditCardCharge = new System.Windows.Forms.TextBox();
            this.lblPlus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panCharge.SuspendLayout();
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
            this.lblHeader.TabIndex = 81;
            this.lblHeader.Text = "Tender Credit Card Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCardHolder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCardNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtValidityDates);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblCreditCard);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.cboCardType);
            this.groupBox1.Controls.Add(this.txtScan);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.panCharge);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scan the card or Enter the required information below";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label8.Location = new System.Drawing.Point(802, 17);
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
            this.lblBalanceAmount.Location = new System.Drawing.Point(631, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(164, 30);
            this.lblBalanceAmount.TabIndex = 93;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(67, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 91;
            this.label5.Text = "Card Holder:";
            // 
            // txtCardHolder
            // 
            this.txtCardHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardHolder.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardHolder.Location = new System.Drawing.Point(203, 154);
            this.txtCardHolder.MaxLength = 0;
            this.txtCardHolder.Name = "txtCardHolder";
            this.txtCardHolder.Size = new System.Drawing.Size(585, 30);
            this.txtCardHolder.TabIndex = 5;
            this.txtCardHolder.GotFocus += new System.EventHandler(this.txtCardHolder_GotFocus);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(67, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Card No.:";
            // 
            // txtCardNo
            // 
            this.txtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNo.Location = new System.Drawing.Point(203, 121);
            this.txtCardNo.MaxLength = 0;
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(211, 30);
            this.txtCardNo.TabIndex = 3;
            this.txtCardNo.TextChanged += new System.EventHandler(this.txtCardNo_TextChanged);
            this.txtCardNo.GotFocus += new System.EventHandler(this.txtCardNo_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(423, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "(Format: mmyy)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(415, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Expiration Date:";
            // 
            // txtValidityDates
            // 
            this.txtValidityDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValidityDates.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidityDates.Location = new System.Drawing.Point(513, 123);
            this.txtValidityDates.MaxLength = 4;
            this.txtValidityDates.Name = "txtValidityDates";
            this.txtValidityDates.Size = new System.Drawing.Size(108, 30);
            this.txtValidityDates.TabIndex = 4;
            this.txtValidityDates.GotFocus += new System.EventHandler(this.txtValidityDates_GotFocus);
            this.txtValidityDates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidityDates_KeyPress);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(67, 192);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(126, 13);
            this.lblRemarks.TabIndex = 17;
            this.lblRemarks.Text = "Remarks (optional) : ";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(203, 189);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(585, 41);
            this.txtRemarks.TabIndex = 6;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.AutoSize = true;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCreditCard.Location = new System.Drawing.Point(67, 29);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(119, 13);
            this.lblCreditCard.TabIndex = 15;
            this.lblCreditCard.Text = "Card Amount (PHP):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(67, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Card Type:";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(203, 20);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(211, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // cboCardType
            // 
            this.cboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCardType.Location = new System.Drawing.Point(203, 86);
            this.cboCardType.Name = "cboCardType";
            this.cboCardType.Size = new System.Drawing.Size(211, 31);
            this.cboCardType.TabIndex = 2;
            this.cboCardType.SelectedIndexChanged += new System.EventHandler(this.cboCardType_SelectedIndexChanged);
            this.cboCardType.GotFocus += new System.EventHandler(this.cboCardType_GotFocus);
            // 
            // txtScan
            // 
            this.txtScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtScan.Location = new System.Drawing.Point(203, 52);
            this.txtScan.MaxLength = 0;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(418, 30);
            this.txtScan.TabIndex = 1;
            this.txtScan.Text = "put the cursor here to scan credit card";
            this.txtScan.TextChanged += new System.EventHandler(this.txtScan_TextChanged);
            this.txtScan.GotFocus += new System.EventHandler(this.txtScan_GotFocus);
            this.txtScan.LostFocus += new System.EventHandler(this.txtScan_LostFocus);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(67, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 16);
            this.label6.TabIndex = 95;
            this.label6.Text = "S C A N   H E R E . . ";
            // 
            // panCharge
            // 
            this.panCharge.Controls.Add(this.label7);
            this.panCharge.Controls.Add(this.txtCreditCardCharge);
            this.panCharge.Controls.Add(this.lblPlus);
            this.panCharge.Controls.Add(this.label9);
            this.panCharge.Controls.Add(this.label10);
            this.panCharge.Location = new System.Drawing.Point(425, 18);
            this.panCharge.Name = "panCharge";
            this.panCharge.Size = new System.Drawing.Size(205, 38);
            this.panCharge.TabIndex = 109;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label7.Location = new System.Drawing.Point(36, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 26);
            this.label7.TabIndex = 111;
            this.label7.Text = "Credit\r\nCharge";
            // 
            // txtCreditCardCharge
            // 
            this.txtCreditCardCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardCharge.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditCardCharge.Location = new System.Drawing.Point(88, 3);
            this.txtCreditCardCharge.MaxLength = 16;
            this.txtCreditCardCharge.Name = "txtCreditCardCharge";
            this.txtCreditCardCharge.ReadOnly = true;
            this.txtCreditCardCharge.Size = new System.Drawing.Size(108, 30);
            this.txtCreditCardCharge.TabIndex = 110;
            this.txtCreditCardCharge.Text = "0.00";
            this.txtCreditCardCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPlus
            // 
            this.lblPlus.AutoSize = true;
            this.lblPlus.BackColor = System.Drawing.Color.White;
            this.lblPlus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlus.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblPlus.Location = new System.Drawing.Point(4, 11);
            this.lblPlus.Name = "lblPlus";
            this.lblPlus.Size = new System.Drawing.Size(16, 13);
            this.lblPlus.TabIndex = 109;
            this.lblPlus.Text = "+";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label9.Location = new System.Drawing.Point(21, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 25);
            this.label9.TabIndex = 112;
            this.label9.Text = "{";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label10.Location = new System.Drawing.Point(70, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 25);
            this.label10.TabIndex = 113;
            this.label10.Text = "}";
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
            this.cmdCancel.TabIndex = 8;
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
            this.cmdEnter.TabIndex = 7;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CreditCardPaymentWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
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
            this.panCharge.ResumeLayout(false);
            this.panCharge.PerformLayout();
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
            if (TerminalDetails.WithRestaurantFeatures)
            {
                this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
                this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();

                // 
                // keyboardSearchControl1
                // 
                this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
                this.keyboardSearchControl1.Location = new System.Drawing.Point(91, 310);
                this.keyboardSearchControl1.Name = "keyboardSearchControl1";
                this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
                this.keyboardSearchControl1.TabIndex = 91;
                this.keyboardSearchControl1.TabStop = false;
                this.keyboardSearchControl1.Tag = "";
                this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
                // 
                // keyboardNoControl1
                // 
                this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
                this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Clear;
                this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.SelectAll;
                this.keyboardNoControl1.Location = new System.Drawing.Point(412, 310);
                this.keyboardNoControl1.Name = "keyboardNoControl1";
                this.keyboardNoControl1.Size = new System.Drawing.Size(208, 172);
                this.keyboardNoControl1.TabIndex = 92;
                this.keyboardNoControl1.TabStop = false;
                this.keyboardNoControl1.Visible = false;
                this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);

                this.Controls.Add(this.keyboardSearchControl1);
                this.Controls.Add(this.keyboardNoControl1);
            
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }

            LoadOptions();
        }

        #endregion

        #region Windows Control Methods

        private void txtAmount_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
        }

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

        private void txtScan_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTextBox = null;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = false;
            }
            txtScan.Text = "";
        }

        private void txtScan_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtScan.Text))
            {
                txtScan.Text = "put the cursor here to scan credit card";
            }
            //else if (txtScan.Text != "put the cursor here to scan credit card")
            //{
            //    setScannedCreditCardInfo();
            //    txtCardNo_TextChanged(null, null);
            //}
        }

        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (txtScan.Text != "put the cursor here to scan credit card")
            {
                setScannedCreditCardInfo();
                txtCardNo_TextChanged(null, null);
            }
        }

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTextBox == null)
                txtAmount.Focus();
            else if (txtSelectedTextBox.Name == txtAmount.Name)
                txtAmount.Focus();
            if (txtSelectedTextBox.Name == txtCardNo.Name)
                txtCardNo.Focus();
            else if (txtSelectedTextBox.Name == txtCardHolder.Name)
                txtCardHolder.Focus();
            else if (txtSelectedTextBox.Name == txtValidityDates.Name)
                txtValidityDates.Focus();
            else if (txtSelectedTextBox.Name == txtRemarks.Name)
                txtRemarks.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTextBox == null)
                txtAmount.Focus();
            else if (txtSelectedTextBox.Name == txtAmount.Name)
                txtAmount.Focus();
            if (txtSelectedTextBox.Name == txtCardNo.Name)
                txtCardNo.Focus();
            else if (txtSelectedTextBox.Name == txtCardHolder.Name)
                txtCardHolder.Focus();
            else if (txtSelectedTextBox.Name == txtValidityDates.Name)
                txtValidityDates.Focus();
            else if (txtSelectedTextBox.Name == txtRemarks.Name)
                txtRemarks.Focus();

            if (txtSelectedTextBox == null || 
                txtSelectedTextBox.Name == txtAmount.Name ||
                txtSelectedTextBox.Name == txtValidityDates.Name ||
                txtSelectedTextBox.Name == txtCardNo.Name)
            {
                txtSelectedTextBox.Focus();
                if (e.KeyboardKeyPressed == "{CLEAR}")
                    txtSelectedTextBox.Text = "";
                else if (e.KeyboardKeyPressed == "{SELECTALL}")
                    txtSelectedTextBox.SelectAll();
                else if (e.KeyboardKeyPressed == "." & txtSelectedTextBox.Text.IndexOf(".") < 0)
                    SendKeys.Send(e.KeyboardKeyPressed);
                else if (e.KeyboardKeyPressed != ".")
                    SendKeys.Send(e.KeyboardKeyPressed);
            }
        }

        private void txtCardNo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
        }

        private void cboCardType_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTextBox = null;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = false;
            }
        }

        private void txtCardHolder_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = TerminalDetails.WithRestaurantFeatures;
            }
        }

        private void txtValidityDates_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
        }

        private void txtRemarks_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = TerminalDetails.WithRestaurantFeatures;
            }
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
            foreach (System.Data.DataRow dr in clsCardType.ListAsDataTable(new Data.CardTypeDetails(CreditCardTypes.Both), "CardTypeName", SortOption.Ascending).Rows)
            {
                cboCardType.Items.Add(dr["CardTypeName"]);
            }
            clsCardType.CommitAndDispose();

            if (cboCardType.Items.Count > 0) cboCardType.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(mclsCreditorDetails.CreditDetails.CreditCardNo) && mclsCreditorDetails.IsCreditAllowed)
            {
                txtScan.Text = mclsCreditorDetails.CreditDetails.CreditCardNo;
                //txtScan_TextChanged(null, null);
            }
            else
            {
                txtScan.Text = "put the cursor here to scan credit card";
            }

            txtAmount.Focus();
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
                MessageBox.Show("Sorry you have entered an invalid amount for credit card payment. Please type a valid credit amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(cboCardType.Text))
            {
                cboCardType.Focus();
                MessageBox.Show("Please select a valid Card Type.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrEmpty(txtValidityDates.Text))
            {
                txtValidityDates.Focus();
                MessageBox.Show("Please type a valid Validity Date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!string.IsNullOrEmpty(txtValidityDates.Text))
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
            // make sure that only 1 INTERNAL CREDIT CARD can be use per transaction
            if (mclsCardTypeDetails.CreditCardType == CreditCardTypes.Internal)
            {
                foreach (Data.CreditCardPaymentDetails clsCreditCardPaymentDetails in marrCreditCardPaymentDetails)
                {
                    if (clsCreditCardPaymentDetails.CardTypeDetails.CreditCardType == CreditCardTypes.Internal)
                    {
                        MessageBox.Show("Sorry an INTERNAL CREDIT CARD has only been use to pay. Please use another credit card or another mode of payment (e.g. cash).", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            decimal decAdditionalCreditCharge = 0;
            if (mclsCreditorDetails.ContactID != 0)
            {
                mdecAmount += decimal.Parse(txtCreditCardCharge.Text);

                decimal mdecAllowedCredit = mclsCreditorDetails.CreditLimit - mclsCreditorDetails.Credit;
                if (mdecAmount > mdecAllowedCredit)
                {
                    MessageBox.Show("Amount must be less than the credit limit (" + mdecAllowedCredit.ToString("#,##0.#0") + "). Please enter a lower amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }
                if (mdecAmount <= 0)
                {
                    MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return false;
                }
                if (!mclsCreditorDetails.CreditDetails.CreditActive)
                {
                    MessageBox.Show("Sorry the credit card status is " + mclsCreditorDetails.CreditDetails.CreditCardStatus.ToString("G") + ". Please enter an active credit card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtScan.Focus();
                    return false;
                }
                if (mclsCardTypeDetails.WithGuarantor)
                {
                    Data.Contacts clsContacts = new Data.Contacts();
                    mclsGuarantorDetails = clsContacts.Details(mclsCreditorDetails.CreditDetails.GuarantorID);
                    clsContacts.CommitAndDispose();

                    if (!mclsGuarantorDetails.CreditDetails.CreditActive)
                    {
                        MessageBox.Show("Sorry the Guarantor's credit card status is " + mclsGuarantorDetails.CreditDetails.CreditCardStatus.ToString("G") + ". Please enter an active credit card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtScan.Focus();
                        return false;
                    }
                }

                if (mboIsCreditChargeExcluded) // exclude if it's an special item
                { decAdditionalCreditCharge = 0; }
                else if (mclsCardTypeDetails.WithGuarantor && TerminalDetails.GroupChargeType.ChargeTypeID != 0)
                {
                    if (TerminalDetails.GroupChargeType.InPercent)
                        decAdditionalCreditCharge = mdecBalanceAmount * (TerminalDetails.GroupChargeType.ChargeAmount / 100);
                    else
                        decAdditionalCreditCharge = mdecBalanceAmount + TerminalDetails.GroupChargeType.ChargeAmount;
                }
                else if (!mclsCardTypeDetails.WithGuarantor && TerminalDetails.PersonalChargeType.ChargeTypeID != 0)
                {
                    if (TerminalDetails.PersonalChargeType.InPercent)
                        decAdditionalCreditCharge = mdecBalanceAmount * (TerminalDetails.PersonalChargeType.ChargeAmount / 100);
                    else
                        decAdditionalCreditCharge = mdecBalanceAmount + TerminalDetails.PersonalChargeType.ChargeAmount;
                }
                if (mdecAmount > mdecBalanceAmount + decAdditionalCreditCharge)
                {
                    txtAmount.Focus();
                    MessageBox.Show("Amount must be less than the balance amount (" + mdecBalanceAmount.ToString("#,##0.#0") + "). Please enter a lower or equal amount for credit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (mclsCreditorDetails.ContactID == 0 && mclsCardTypeDetails.CreditCardType == CreditCardTypes.Internal)
            {
                MessageBox.Show("Please enter a valid card no for " + cboCardType.Text + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            mDetails.BranchDetails = TerminalDetails.BranchDetails;
            mDetails.TerminalNo = TerminalDetails.TerminalNo;
            mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
            mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;
            mDetails.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
            mDetails.CashierName = mclsSalesTransactionDetails.CashierName;

            mDetails.Amount = mdecAmount;
            mDetails.AdditionalCharge = decAdditionalCreditCharge;

            mDetails.CardTypeID = mclsCardTypeDetails.CardTypeID;
            mDetails.CardTypeCode = mclsCardTypeDetails.CardTypeCode;
            mDetails.CardTypeName = mclsCardTypeDetails.CardTypeName;
            mDetails.CardNo = txtCardNo.Text;
            mDetails.CardHolder = txtCardHolder.Text;
            
            mDetails.ValidityDates = ValidityDate.ToString("MMddyy");
            mDetails.Remarks = txtRemarks.Text;
            mDetails.CardTypeDetails = mclsCardTypeDetails;
            mDetails.CreditorDetails = mclsCreditorDetails;
            mDetails.IsRefund = IsRefund;

            return true;
        }

        private void setScannedCreditCardInfo()
        {
            string[] strCareditInfo = txtScan.Text.Split(',');

            if (strCareditInfo.Length == 1) //internal credit card
            {
                txtCardNo.Text = strCareditInfo[0];
            }
            else
            {
                if (strCareditInfo.Length >= 1) txtCardHolder.Text = strCareditInfo[0];
                if (strCareditInfo.Length >= 2) txtCardNo.Text = strCareditInfo[1];
                if (strCareditInfo.Length >= 3) txtValidityDates.Text = strCareditInfo[2];
            }
        }
        

        #endregion

        private void cboCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.CardType clsCardType = new Data.CardType();
            mclsCardTypeDetails = clsCardType.Details(cboCardType.Text);
            clsCardType.CommitAndDispose();
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            mclsCreditorDetails = new Data.ContactDetails();
            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                Data.Contacts clsContacts = new Data.Contacts();
                mclsCreditorDetails = clsContacts.DetailsByCreditCardNo(txtCardNo.Text);

                if (mclsCreditorDetails.ContactID == 0)
                {
                    mclsCreditorDetails = clsContacts.DetailsByCreditCardNo(txtCardNo.Text.Remove(txtCardNo.Text.Length - 1));
                }
                clsContacts.CommitAndDispose();
            }

            //this means that this uses an internal credit card
            setCreditor();

            if (mclsCreditorDetails.ContactID != 0)
                setCreditCardChargeAmount(); 
            else
                unsetCreditCardCharge();
        }

        private bool setCreditor()
        {
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                mclsCreditorDetails = new Data.ContactDetails();
            }
            if (!string.IsNullOrEmpty(txtCardNo.Text))
            {
                string strContactCardNo = txtCardNo.Text;

                Data.Contacts clsContacts = new Data.Contacts();
                mclsCreditorDetails = clsContacts.DetailsByCreditCardNo(strContactCardNo);

                if (mclsCreditorDetails.ContactID == 0)
                {
                    strContactCardNo = strContactCardNo.Remove(strContactCardNo.Length - 1);
                    mclsCreditorDetails = clsContacts.DetailsByCreditCardNo(strContactCardNo);
                }
                clsContacts.CommitAndDispose();

                if(mclsCreditorDetails.ContactID != 0)
                {
                    cboCardType.SelectedText = mclsCreditorDetails.CreditDetails.CardTypeDetails.CardTypeName;
                    cboCardType.Text = mclsCreditorDetails.CreditDetails.CardTypeDetails.CardTypeName;

                    cboCardType_SelectedIndexChanged(null, null);

                    txtCardHolder.Text = mclsCreditorDetails.ContactName;
                    if (mclsCreditorDetails.CreditDetails.ExpiryDate == Constants.C_DATE_MIN_VALUE)
                        txtValidityDates.Text = "";
                    else
                        txtValidityDates.Text = mclsCreditorDetails.CreditDetails.ExpiryDate.ToString("MMyy");
                }
            }
            return true;
        }
        private void setCreditCardChargeAmount()
        {
            if (mboIsCreditChargeExcluded )  // exclude if the ProductCode is exempted in charging
            {
                unsetCreditCardCharge();
                cboCardType.Enabled = false;
                txtValidityDates.Enabled = false;
                txtCardHolder.Enabled = false;
            }
            else if (mclsCardTypeDetails.ExemptInTerminalCharge)
            {
                unsetCreditCardCharge();
                cboCardType.Enabled = false;
                txtValidityDates.Enabled = false;
                txtCardHolder.Enabled = false;
            }
            else if (mclsCardTypeDetails.WithGuarantor && TerminalDetails.GroupChargeType.ChargeTypeID == 0)
            {
                unsetCreditCardCharge();
                cboCardType.Enabled = false;
                txtValidityDates.Enabled = false;
                txtCardHolder.Enabled = false;
            }
            else if (!mclsCardTypeDetails.WithGuarantor && TerminalDetails.PersonalChargeType.ChargeTypeID == 0)
            {
                unsetCreditCardCharge();
                cboCardType.Enabled = false;
                txtValidityDates.Enabled = false;
                txtCardHolder.Enabled = false;
            }
            else
            {
                decimal decBalance = 0;
                decBalance = decimal.TryParse(txtAmount.Text, out decBalance) ? decBalance : 0;
                decimal decAdditionalCreditCharge = 0;

                if (mboIsCreditChargeExcluded) // exclude if it's an special item
                { decAdditionalCreditCharge = 0; }
                else if (mclsCardTypeDetails.ExemptInTerminalCharge) // exclude if it's an exemption
                { decAdditionalCreditCharge = 0; }
                else if (mclsCardTypeDetails.WithGuarantor && TerminalDetails.GroupChargeType.ChargeTypeID != 0)
                {
                    if (TerminalDetails.GroupChargeType.InPercent)
                        decAdditionalCreditCharge = decBalance * (TerminalDetails.GroupChargeType.ChargeAmount / 100);
                    else
                        decAdditionalCreditCharge = decBalance + TerminalDetails.GroupChargeType.ChargeAmount;
                }
                else if (!mclsCardTypeDetails.WithGuarantor && TerminalDetails.PersonalChargeType.ChargeTypeID != 0)
                {
                    if (TerminalDetails.PersonalChargeType.InPercent)
                        decAdditionalCreditCharge = decBalance * (TerminalDetails.PersonalChargeType.ChargeAmount / 100);
                    else
                        decAdditionalCreditCharge = decBalance + TerminalDetails.PersonalChargeType.ChargeAmount;
                }
                txtCreditCardCharge.Text = decAdditionalCreditCharge.ToString("#,##0.#0");
                lblBalanceAmount.Text = (decBalance + decAdditionalCreditCharge).ToString("#,##0.#0");

                cboCardType.Enabled = false;
                txtValidityDates.Enabled = false;
                txtCardHolder.Enabled = false;
                panCharge.Visible = true;
            }
        }
        private void unsetCreditCardCharge()
        {
            decimal decAmount = 0;
            if (decimal.TryParse(txtAmount.Text, out decAmount))
            {
                if (decAmount > mdecBalanceAmount)
                    txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");

                lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");

                cboCardType.Enabled = true;
                panCharge.Visible = false;
                txtValidityDates.Enabled = true;
                txtCardHolder.Enabled = true;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (mclsCreditorDetails.ContactID != 0)
                setCreditCardChargeAmount();
            else
                unsetCreditCardCharge();
        }

    }
}
