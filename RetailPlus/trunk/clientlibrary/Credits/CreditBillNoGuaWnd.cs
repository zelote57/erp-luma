using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for CreditBillNoGuaWnd.
	/// </summary>
    public class CreditBillNoGuaWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private DialogResult dialog;
        private GroupBox grpContactDetails;
        private Label lblCaption;
        private TextBox txtCustomerName;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private GroupBox grpBillingDetails;
        private Label label7;
        private TextBox txtAvailableCredit;
        private Label label4;
        private TextBox txtCredit;
        private Label label5;
        private TextBox txtCreditLimit;
        private string mstCaption;
        private GroupBox grpCurrBilling;
        private Label label13;
        private Label label14;
        private TextBox txtCurrentDueAmount;
        private Label label15;
        private TextBox txtMinimumAmountDue;
        private TextBox txtCurrentPurchaseAmt;
        private Label label17;
        private TextBox txtTotalBillCharges;
        private Label label19;
        private TextBox txtCurrMonthAmountPaid;
        private Label label20;
        private TextBox txtPrev1MoCurrentDueAmount;
        private Label label21;
        private Label label1;
        private Label label2;
        private TextBox txtLastCurrentDueAmount;
        private Label label3;
        private TextBox txtLastMinimumAmountDue;
        private TextBox txtLastCurrentPurchaseAmt;
        private Label label6;
        private TextBox txtLastTotalBillCharges;
        private Label label8;
        private TextBox txtLastCurrMonthAmountPaid;
        private Label label9;
        private TextBox txtLastPrev1MoCurrentDueAmount;
        private Label label10;
        private TextBox txtLastPrev2MoCurrentDueAmount;
        private Label label12;
        private TextBox txtLastPreviousBalance;
        private Label label11;
        private TextBox txtPreviousBalance;
        private Button cmdLastEqual;
        private Button cmdEqual;
        private TextBox txtPrev2MoCurrentDueAmount;
        private bool mboIsLoading;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

		public string Caption
		{
			get {	return mstCaption;	}
			set {	mstCaption = value;	}
		}

        public Data.ContactDetails CreditorDetails { get; set; }

        public Data.ContactDetails GuarantorDetails { get; set; }

        public Data.TerminalDetails TerminalDetails { get; set; }

		#region Constructors And Desctructors

		public CreditBillNoGuaWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Balance.jpg"); }
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpContactDetails = new System.Windows.Forms.GroupBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAvailableCredit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.grpBillingDetails = new System.Windows.Forms.GroupBox();
            this.cmdLastEqual = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLastPreviousBalance = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLastCurrentDueAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastMinimumAmountDue = new System.Windows.Forms.TextBox();
            this.txtLastCurrentPurchaseAmt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLastTotalBillCharges = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLastCurrMonthAmountPaid = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLastPrev1MoCurrentDueAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastPrev2MoCurrentDueAmount = new System.Windows.Forms.TextBox();
            this.grpCurrBilling = new System.Windows.Forms.GroupBox();
            this.cmdEqual = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPreviousBalance = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCurrentDueAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMinimumAmountDue = new System.Windows.Forms.TextBox();
            this.txtCurrentPurchaseAmt = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalBillCharges = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCurrMonthAmountPaid = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPrev1MoCurrentDueAmount = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPrev2MoCurrentDueAmount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpContactDetails.SuspendLayout();
            this.grpBillingDetails.SuspendLayout();
            this.grpCurrBilling.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(108, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Credit Verification";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 12;
            this.imgIcon.TabStop = false;
            // 
            // grpContactDetails
            // 
            this.grpContactDetails.BackColor = System.Drawing.Color.White;
            this.grpContactDetails.Controls.Add(this.lblCaption);
            this.grpContactDetails.Controls.Add(this.txtCustomerName);
            this.grpContactDetails.Controls.Add(this.label5);
            this.grpContactDetails.Controls.Add(this.txtAvailableCredit);
            this.grpContactDetails.Controls.Add(this.label7);
            this.grpContactDetails.Controls.Add(this.txtCreditLimit);
            this.grpContactDetails.Controls.Add(this.label4);
            this.grpContactDetails.Controls.Add(this.txtCredit);
            this.grpContactDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContactDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpContactDetails.Location = new System.Drawing.Point(9, 66);
            this.grpContactDetails.Name = "grpContactDetails";
            this.grpContactDetails.Size = new System.Drawing.Size(1008, 90);
            this.grpContactDetails.TabIndex = 0;
            this.grpContactDetails.TabStop = false;
            this.grpContactDetails.Text = "Creditor Details";
            this.grpContactDetails.Visible = false;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCaption.Location = new System.Drawing.Point(71, 17);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(97, 13);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Customer Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(174, 15);
            this.txtCustomerName.MaxLength = 25;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(439, 30);
            this.txtCustomerName.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(71, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Credit limit";
            // 
            // txtAvailableCredit
            // 
            this.txtAvailableCredit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAvailableCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvailableCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvailableCredit.Location = new System.Drawing.Point(731, 52);
            this.txtAvailableCredit.MaxLength = 75;
            this.txtAvailableCredit.Name = "txtAvailableCredit";
            this.txtAvailableCredit.ReadOnly = true;
            this.txtAvailableCredit.Size = new System.Drawing.Size(169, 30);
            this.txtAvailableCredit.TabIndex = 21;
            this.txtAvailableCredit.Text = "0.00";
            this.txtAvailableCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MediumBlue;
            this.label7.Location = new System.Drawing.Point(627, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Available Credit ";
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditLimit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditLimit.Location = new System.Drawing.Point(174, 52);
            this.txtCreditLimit.MaxLength = 75;
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.ReadOnly = true;
            this.txtCreditLimit.Size = new System.Drawing.Size(169, 30);
            this.txtCreditLimit.TabIndex = 19;
            this.txtCreditLimit.Text = "0.00";
            this.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(356, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current Credit";
            // 
            // txtCredit
            // 
            this.txtCredit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredit.Location = new System.Drawing.Point(444, 52);
            this.txtCredit.MaxLength = 75;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(169, 30);
            this.txtCredit.TabIndex = 20;
            this.txtCredit.Text = "0.00";
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.cmdCancel.TabIndex = 17;
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
            this.cmdEnter.TabIndex = 16;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // grpBillingDetails
            // 
            this.grpBillingDetails.BackColor = System.Drawing.Color.White;
            this.grpBillingDetails.Controls.Add(this.cmdLastEqual);
            this.grpBillingDetails.Controls.Add(this.label12);
            this.grpBillingDetails.Controls.Add(this.txtLastPreviousBalance);
            this.grpBillingDetails.Controls.Add(this.label1);
            this.grpBillingDetails.Controls.Add(this.label2);
            this.grpBillingDetails.Controls.Add(this.txtLastCurrentDueAmount);
            this.grpBillingDetails.Controls.Add(this.label3);
            this.grpBillingDetails.Controls.Add(this.txtLastMinimumAmountDue);
            this.grpBillingDetails.Controls.Add(this.txtLastCurrentPurchaseAmt);
            this.grpBillingDetails.Controls.Add(this.label6);
            this.grpBillingDetails.Controls.Add(this.txtLastTotalBillCharges);
            this.grpBillingDetails.Controls.Add(this.label8);
            this.grpBillingDetails.Controls.Add(this.txtLastCurrMonthAmountPaid);
            this.grpBillingDetails.Controls.Add(this.label9);
            this.grpBillingDetails.Controls.Add(this.txtLastPrev1MoCurrentDueAmount);
            this.grpBillingDetails.Controls.Add(this.label10);
            this.grpBillingDetails.Controls.Add(this.txtLastPrev2MoCurrentDueAmount);
            this.grpBillingDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBillingDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpBillingDetails.Location = new System.Drawing.Point(7, 175);
            this.grpBillingDetails.Name = "grpBillingDetails";
            this.grpBillingDetails.Size = new System.Drawing.Size(445, 346);
            this.grpBillingDetails.TabIndex = 13;
            this.grpBillingDetails.TabStop = false;
            this.grpBillingDetails.Text = "Billing Details: Nov 20,2014";
            // 
            // cmdLastEqual
            // 
            this.cmdLastEqual.Location = new System.Drawing.Point(380, 249);
            this.cmdLastEqual.Name = "cmdLastEqual";
            this.cmdLastEqual.Size = new System.Drawing.Size(47, 32);
            this.cmdLastEqual.TabIndex = 6;
            this.cmdLastEqual.Text = " = ";
            this.cmdLastEqual.UseVisualStyleBackColor = true;
            this.cmdLastEqual.Click += new System.EventHandler(this.cmdLastEqual_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Window;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(62, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 49;
            this.label12.Text = "Previous Balance";
            // 
            // txtLastPreviousBalance
            // 
            this.txtLastPreviousBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLastPreviousBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastPreviousBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastPreviousBalance.Location = new System.Drawing.Point(207, 105);
            this.txtLastPreviousBalance.MaxLength = 75;
            this.txtLastPreviousBalance.Name = "txtLastPreviousBalance";
            this.txtLastPreviousBalance.ReadOnly = true;
            this.txtLastPreviousBalance.Size = new System.Drawing.Size(169, 30);
            this.txtLastPreviousBalance.TabIndex = 2;
            this.txtLastPreviousBalance.Text = "0.00";
            this.txtLastPreviousBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(60, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Total Amt. Due";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(60, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Minimum Amount Due";
            // 
            // txtLastCurrentDueAmount
            // 
            this.txtLastCurrentDueAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLastCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastCurrentDueAmount.Location = new System.Drawing.Point(205, 285);
            this.txtLastCurrentDueAmount.MaxLength = 75;
            this.txtLastCurrentDueAmount.Name = "txtLastCurrentDueAmount";
            this.txtLastCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtLastCurrentDueAmount.TabIndex = 7;
            this.txtLastCurrentDueAmount.Text = "0.00";
            this.txtLastCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(60, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Purchases";
            // 
            // txtLastMinimumAmountDue
            // 
            this.txtLastMinimumAmountDue.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLastMinimumAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastMinimumAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastMinimumAmountDue.Location = new System.Drawing.Point(205, 249);
            this.txtLastMinimumAmountDue.MaxLength = 75;
            this.txtLastMinimumAmountDue.Name = "txtLastMinimumAmountDue";
            this.txtLastMinimumAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtLastMinimumAmountDue.TabIndex = 6;
            this.txtLastMinimumAmountDue.Text = "0.00";
            this.txtLastMinimumAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLastCurrentPurchaseAmt
            // 
            this.txtLastCurrentPurchaseAmt.BackColor = System.Drawing.SystemColors.Window;
            this.txtLastCurrentPurchaseAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastCurrentPurchaseAmt.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastCurrentPurchaseAmt.Location = new System.Drawing.Point(205, 213);
            this.txtLastCurrentPurchaseAmt.MaxLength = 75;
            this.txtLastCurrentPurchaseAmt.Name = "txtLastCurrentPurchaseAmt";
            this.txtLastCurrentPurchaseAmt.Size = new System.Drawing.Size(169, 30);
            this.txtLastCurrentPurchaseAmt.TabIndex = 5;
            this.txtLastCurrentPurchaseAmt.Text = "0.00";
            this.txtLastCurrentPurchaseAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastCurrentPurchaseAmt.TextChanged += new System.EventHandler(this.txtLastCurrentPurchaseAmt_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(60, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 26);
            this.label6.TabIndex = 41;
            this.label6.Text = "Finance Charge + \r\n  Late Payment Charge";
            // 
            // txtLastTotalBillCharges
            // 
            this.txtLastTotalBillCharges.BackColor = System.Drawing.SystemColors.Window;
            this.txtLastTotalBillCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastTotalBillCharges.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastTotalBillCharges.Location = new System.Drawing.Point(206, 177);
            this.txtLastTotalBillCharges.MaxLength = 75;
            this.txtLastTotalBillCharges.Name = "txtLastTotalBillCharges";
            this.txtLastTotalBillCharges.Size = new System.Drawing.Size(169, 30);
            this.txtLastTotalBillCharges.TabIndex = 4;
            this.txtLastTotalBillCharges.Text = "0.00";
            this.txtLastTotalBillCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastTotalBillCharges.TextChanged += new System.EventHandler(this.txtLastTotalBillCharges_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Window;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(61, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Payments";
            // 
            // txtLastCurrMonthAmountPaid
            // 
            this.txtLastCurrMonthAmountPaid.BackColor = System.Drawing.SystemColors.Window;
            this.txtLastCurrMonthAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastCurrMonthAmountPaid.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastCurrMonthAmountPaid.Location = new System.Drawing.Point(207, 141);
            this.txtLastCurrMonthAmountPaid.MaxLength = 75;
            this.txtLastCurrMonthAmountPaid.Name = "txtLastCurrMonthAmountPaid";
            this.txtLastCurrMonthAmountPaid.Size = new System.Drawing.Size(169, 30);
            this.txtLastCurrMonthAmountPaid.TabIndex = 3;
            this.txtLastCurrMonthAmountPaid.Text = "0.00";
            this.txtLastCurrMonthAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastCurrMonthAmountPaid.TextChanged += new System.EventHandler(this.txtLastCurrMonthAmountPaid_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(61, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "30 Days";
            // 
            // txtLastPrev1MoCurrentDueAmount
            // 
            this.txtLastPrev1MoCurrentDueAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtLastPrev1MoCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastPrev1MoCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastPrev1MoCurrentDueAmount.Location = new System.Drawing.Point(207, 69);
            this.txtLastPrev1MoCurrentDueAmount.MaxLength = 75;
            this.txtLastPrev1MoCurrentDueAmount.Name = "txtLastPrev1MoCurrentDueAmount";
            this.txtLastPrev1MoCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtLastPrev1MoCurrentDueAmount.TabIndex = 1;
            this.txtLastPrev1MoCurrentDueAmount.Text = "0.00";
            this.txtLastPrev1MoCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastPrev1MoCurrentDueAmount.TextChanged += new System.EventHandler(this.txtLastPrev1MoCurrentDueAmount_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(61, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "60 Days and Over";
            // 
            // txtLastPrev2MoCurrentDueAmount
            // 
            this.txtLastPrev2MoCurrentDueAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtLastPrev2MoCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastPrev2MoCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastPrev2MoCurrentDueAmount.Location = new System.Drawing.Point(207, 33);
            this.txtLastPrev2MoCurrentDueAmount.MaxLength = 75;
            this.txtLastPrev2MoCurrentDueAmount.Name = "txtLastPrev2MoCurrentDueAmount";
            this.txtLastPrev2MoCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtLastPrev2MoCurrentDueAmount.TabIndex = 0;
            this.txtLastPrev2MoCurrentDueAmount.Text = "0.00";
            this.txtLastPrev2MoCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLastPrev2MoCurrentDueAmount.TextChanged += new System.EventHandler(this.txtLastPrev2MoCurrentDueAmount_TextChanged);
            // 
            // grpCurrBilling
            // 
            this.grpCurrBilling.BackColor = System.Drawing.Color.White;
            this.grpCurrBilling.Controls.Add(this.cmdEqual);
            this.grpCurrBilling.Controls.Add(this.label11);
            this.grpCurrBilling.Controls.Add(this.txtPreviousBalance);
            this.grpCurrBilling.Controls.Add(this.label13);
            this.grpCurrBilling.Controls.Add(this.label14);
            this.grpCurrBilling.Controls.Add(this.txtCurrentDueAmount);
            this.grpCurrBilling.Controls.Add(this.label15);
            this.grpCurrBilling.Controls.Add(this.txtMinimumAmountDue);
            this.grpCurrBilling.Controls.Add(this.txtCurrentPurchaseAmt);
            this.grpCurrBilling.Controls.Add(this.label17);
            this.grpCurrBilling.Controls.Add(this.txtTotalBillCharges);
            this.grpCurrBilling.Controls.Add(this.label19);
            this.grpCurrBilling.Controls.Add(this.txtCurrMonthAmountPaid);
            this.grpCurrBilling.Controls.Add(this.label20);
            this.grpCurrBilling.Controls.Add(this.txtPrev1MoCurrentDueAmount);
            this.grpCurrBilling.Controls.Add(this.label21);
            this.grpCurrBilling.Controls.Add(this.txtPrev2MoCurrentDueAmount);
            this.grpCurrBilling.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCurrBilling.ForeColor = System.Drawing.Color.Blue;
            this.grpCurrBilling.Location = new System.Drawing.Point(505, 175);
            this.grpCurrBilling.Name = "grpCurrBilling";
            this.grpCurrBilling.Size = new System.Drawing.Size(445, 346);
            this.grpCurrBilling.TabIndex = 14;
            this.grpCurrBilling.TabStop = false;
            this.grpCurrBilling.Text = "Billing Details: Nov 20,2014";
            // 
            // cmdEqual
            // 
            this.cmdEqual.Location = new System.Drawing.Point(370, 247);
            this.cmdEqual.Name = "cmdEqual";
            this.cmdEqual.Size = new System.Drawing.Size(47, 32);
            this.cmdEqual.TabIndex = 36;
            this.cmdEqual.Text = " = ";
            this.cmdEqual.UseVisualStyleBackColor = true;
            this.cmdEqual.Click += new System.EventHandler(this.cmdEqual_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(53, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Previous Balance";
            // 
            // txtPreviousBalance
            // 
            this.txtPreviousBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPreviousBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreviousBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviousBalance.Location = new System.Drawing.Point(198, 105);
            this.txtPreviousBalance.MaxLength = 75;
            this.txtPreviousBalance.Name = "txtPreviousBalance";
            this.txtPreviousBalance.ReadOnly = true;
            this.txtPreviousBalance.Size = new System.Drawing.Size(169, 30);
            this.txtPreviousBalance.TabIndex = 10;
            this.txtPreviousBalance.Text = "0.00";
            this.txtPreviousBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Window;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(50, 285);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Total Amt. Due";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Window;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(50, 249);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Minimum Amount Due";
            // 
            // txtCurrentDueAmount
            // 
            this.txtCurrentDueAmount.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentDueAmount.Location = new System.Drawing.Point(195, 285);
            this.txtCurrentDueAmount.MaxLength = 75;
            this.txtCurrentDueAmount.Name = "txtCurrentDueAmount";
            this.txtCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtCurrentDueAmount.TabIndex = 15;
            this.txtCurrentDueAmount.Text = "0.00";
            this.txtCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MediumBlue;
            this.label15.Location = new System.Drawing.Point(50, 213);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Purchases";
            // 
            // txtMinimumAmountDue
            // 
            this.txtMinimumAmountDue.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMinimumAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinimumAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinimumAmountDue.Location = new System.Drawing.Point(195, 249);
            this.txtMinimumAmountDue.MaxLength = 75;
            this.txtMinimumAmountDue.Name = "txtMinimumAmountDue";
            this.txtMinimumAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtMinimumAmountDue.TabIndex = 14;
            this.txtMinimumAmountDue.Text = "0.00";
            this.txtMinimumAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCurrentPurchaseAmt
            // 
            this.txtCurrentPurchaseAmt.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrentPurchaseAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPurchaseAmt.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPurchaseAmt.Location = new System.Drawing.Point(195, 213);
            this.txtCurrentPurchaseAmt.MaxLength = 75;
            this.txtCurrentPurchaseAmt.Name = "txtCurrentPurchaseAmt";
            this.txtCurrentPurchaseAmt.Size = new System.Drawing.Size(169, 30);
            this.txtCurrentPurchaseAmt.TabIndex = 13;
            this.txtCurrentPurchaseAmt.Text = "0.00";
            this.txtCurrentPurchaseAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrentPurchaseAmt.TextChanged += new System.EventHandler(this.txtCurrentPurchaseAmt_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Window;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.MediumBlue;
            this.label17.Location = new System.Drawing.Point(50, 177);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(135, 26);
            this.label17.TabIndex = 25;
            this.label17.Text = "Finance Charge + \r\n  Late Payment Charge";
            // 
            // txtTotalBillCharges
            // 
            this.txtTotalBillCharges.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalBillCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalBillCharges.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBillCharges.Location = new System.Drawing.Point(196, 177);
            this.txtTotalBillCharges.MaxLength = 75;
            this.txtTotalBillCharges.Name = "txtTotalBillCharges";
            this.txtTotalBillCharges.Size = new System.Drawing.Size(169, 30);
            this.txtTotalBillCharges.TabIndex = 12;
            this.txtTotalBillCharges.Text = "0.00";
            this.txtTotalBillCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalBillCharges.TextChanged += new System.EventHandler(this.txtTotalBillCharges_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Window;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.MediumBlue;
            this.label19.Location = new System.Drawing.Point(51, 141);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Payments";
            // 
            // txtCurrMonthAmountPaid
            // 
            this.txtCurrMonthAmountPaid.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrMonthAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrMonthAmountPaid.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrMonthAmountPaid.Location = new System.Drawing.Point(197, 141);
            this.txtCurrMonthAmountPaid.MaxLength = 75;
            this.txtCurrMonthAmountPaid.Name = "txtCurrMonthAmountPaid";
            this.txtCurrMonthAmountPaid.Size = new System.Drawing.Size(169, 30);
            this.txtCurrMonthAmountPaid.TabIndex = 11;
            this.txtCurrMonthAmountPaid.Text = "0.00";
            this.txtCurrMonthAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrMonthAmountPaid.TextChanged += new System.EventHandler(this.txtCurrMonthAmountPaid_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Window;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.MediumBlue;
            this.label20.Location = new System.Drawing.Point(52, 69);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "30 Days";
            // 
            // txtPrev1MoCurrentDueAmount
            // 
            this.txtPrev1MoCurrentDueAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrev1MoCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrev1MoCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrev1MoCurrentDueAmount.Location = new System.Drawing.Point(198, 69);
            this.txtPrev1MoCurrentDueAmount.MaxLength = 75;
            this.txtPrev1MoCurrentDueAmount.Name = "txtPrev1MoCurrentDueAmount";
            this.txtPrev1MoCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtPrev1MoCurrentDueAmount.TabIndex = 9;
            this.txtPrev1MoCurrentDueAmount.Text = "0.00";
            this.txtPrev1MoCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrev1MoCurrentDueAmount.TextChanged += new System.EventHandler(this.txtPrev1MoCurrentDueAmount_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Window;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.MediumBlue;
            this.label21.Location = new System.Drawing.Point(52, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(106, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "60 Days and Over";
            // 
            // txtPrev2MoCurrentDueAmount
            // 
            this.txtPrev2MoCurrentDueAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrev2MoCurrentDueAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrev2MoCurrentDueAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrev2MoCurrentDueAmount.Location = new System.Drawing.Point(198, 33);
            this.txtPrev2MoCurrentDueAmount.MaxLength = 75;
            this.txtPrev2MoCurrentDueAmount.Name = "txtPrev2MoCurrentDueAmount";
            this.txtPrev2MoCurrentDueAmount.Size = new System.Drawing.Size(169, 30);
            this.txtPrev2MoCurrentDueAmount.TabIndex = 8;
            this.txtPrev2MoCurrentDueAmount.Text = "0.00";
            this.txtPrev2MoCurrentDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrev2MoCurrentDueAmount.TextChanged += new System.EventHandler(this.txtPrev2MoCurrentDueAmount_TextChanged);
            // 
            // CreditBillNoGuaWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.grpCurrBilling);
            this.Controls.Add(this.grpBillingDetails);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.grpContactDetails);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CreditBillNoGuaWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditBillNoGuaWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditBillNoGuaWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpContactDetails.ResumeLayout(false);
            this.grpContactDetails.PerformLayout();
            this.grpBillingDetails.ResumeLayout(false);
            this.grpBillingDetails.PerformLayout();
            this.grpCurrBilling.ResumeLayout(false);
            this.grpCurrBilling.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods

		private void CreditBillNoGuaWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

                case Keys.Enter:
                    if (SaveRecord())
                    {
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    break;
			}
		}

		private void CreditBillNoGuaWnd_Load(object sender, System.EventArgs e)
		{
            this.lblHeader.Text = "Adjust current billing statement of: " + CreditorDetails.ContactName;
            this.grpBillingDetails.Text = "Last Billing Details: " + CreditorDetails.CreditDetails.Last2BillingDate.ToString("MMM dd, yyyy");

            this.grpCurrBilling.Text = "Current Billing Details: " + CreditorDetails.CreditDetails.LastBillingDate.ToString("MMM dd, yyyy");

            LoadOptions();


            LoadData();
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
            if (SaveRecord())
            {
                dialog = DialogResult.OK;
                this.Hide();
            }
        }

        private void txtLastPrev2MoCurrentDueAmount_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        private void txtLastPrev1MoCurrentDueAmount_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        private void txtLastCurrMonthAmountPaid_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        private void txtLastTotalBillCharges_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        private void txtLastCurrentPurchaseAmt_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            mboIsLoading = true;

            grpContactDetails.Visible = true;
            grpBillingDetails.Visible = true;

            txtCustomerName.Text = CreditorDetails.ContactName;

            txtCreditLimit.Text = CreditorDetails.CreditLimit.ToString("#,##0.#0");
            txtCredit.Text = CreditorDetails.Credit.ToString("#,##0.#0");
            txtAvailableCredit.Text = (CreditorDetails.CreditLimit - CreditorDetails.Credit).ToString("#,##0.#0");

            Data.Billing clsBilling = new Data.Billing();
            Data.BillingDetails clsBillingDetails = clsBilling.Details(CreditorDetails.ContactID, CreditorDetails.CreditDetails.Last2BillingDate);
            Data.BillingDetails clsCurrBillingDetails = clsBilling.Details(CreditorDetails.ContactID, CreditorDetails.CreditDetails.LastBillingDate);
            clsBilling.CommitAndDispose();

            grpBillingDetails.Tag = "0";
            if (clsBillingDetails.BillingDate == CreditorDetails.CreditDetails.Last2BillingDate)
            {
                grpBillingDetails.Tag = clsBillingDetails.CreditBillHeaderID;
                txtLastPrev2MoCurrentDueAmount.Text = clsBillingDetails.Prev2MoCurrentDueAmount.ToString("#,##0.#0");
                txtLastPrev1MoCurrentDueAmount.Text = clsBillingDetails.Prev1MoCurrentDueAmount.ToString("#,##0.#0");
                txtLastPreviousBalance.Text = (clsBillingDetails.Prev2MoCurrentDueAmount + clsBillingDetails.Prev1MoCurrentDueAmount).ToString("#,##0.#0");
                txtLastCurrMonthAmountPaid.Text = clsBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");

                //txtBalance.Text = clsBillingDetails.RunningCreditAmt.ToString("#,##0.#0");
                txtLastTotalBillCharges.Text = clsBillingDetails.TotalBillCharges.ToString("#,##0.#0");
                txtLastCurrentPurchaseAmt.Text = clsBillingDetails.CurrentPurchaseAmt.ToString("#,##0.#0");
                txtLastMinimumAmountDue.Text = clsBillingDetails.MinimumAmountDue.ToString("#,##0.#0");
                txtLastCurrentDueAmount.Text = clsBillingDetails.CurrentDueAmount.ToString("#,##0.#0");
                grpBillingDetails.Visible = true;
            }
            else { grpBillingDetails.Visible = false; }

            grpCurrBilling.Tag = "0";
            if (clsCurrBillingDetails.BillingDate == CreditorDetails.CreditDetails.LastBillingDate)
            {
                grpCurrBilling.Tag = clsCurrBillingDetails.CreditBillHeaderID;
                txtPrev2MoCurrentDueAmount.Text = clsCurrBillingDetails.Prev2MoCurrentDueAmount.ToString("#,##0.#0");
                txtPrev1MoCurrentDueAmount.Text = clsCurrBillingDetails.Prev1MoCurrentDueAmount.ToString("#,##0.#0");
                txtPreviousBalance.Text = (clsCurrBillingDetails.Prev2MoCurrentDueAmount + clsCurrBillingDetails.Prev1MoCurrentDueAmount).ToString("#,##0.#0");
                txtCurrMonthAmountPaid.Text = clsCurrBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");

                //txtBalance.Text = clsCurrBillingDetails.RunningCreditAmt.ToString("#,##0.#0");
                txtTotalBillCharges.Text = clsCurrBillingDetails.TotalBillCharges.ToString("#,##0.#0");
                txtCurrentPurchaseAmt.Text = clsCurrBillingDetails.CurrentPurchaseAmt.ToString("#,##0.#0");
                txtMinimumAmountDue.Text = clsCurrBillingDetails.MinimumAmountDue.ToString("#,##0.#0");
                txtCurrentDueAmount.Text = clsCurrBillingDetails.CurrentDueAmount.ToString("#,##0.#0");
                grpCurrBilling.Visible = true;
            }
            else { grpCurrBilling.Visible = false; }

            mboIsLoading = false;
        }

        private void LoadOptions()
        {
            
        }

        private void ComputeBalance()
        {
            if (!mboIsLoading)
            {
                decimal decPrev2MoCurrentDueAmount = decimal.TryParse(txtLastPrev2MoCurrentDueAmount.Text, out decPrev2MoCurrentDueAmount) ? decPrev2MoCurrentDueAmount : 0;
                decimal decPrev1MoCurrentDueAmount = decimal.TryParse(txtLastPrev1MoCurrentDueAmount.Text, out decPrev1MoCurrentDueAmount) ? decPrev1MoCurrentDueAmount : 0;
                decimal decCurrMonthAmountPaid = decimal.TryParse(txtLastCurrMonthAmountPaid.Text, out decCurrMonthAmountPaid) ? decCurrMonthAmountPaid : 0;

                //txtBalance.Text = (decCurrentDue + decPayments).ToString("#,##0.#0");

                decimal decTotalBillCharges = decimal.TryParse(txtLastTotalBillCharges.Text, out decTotalBillCharges) ? decTotalBillCharges : 0;
                decimal decCurrentPurchaseAmt = decimal.TryParse(txtLastCurrentPurchaseAmt.Text, out decCurrentPurchaseAmt) ? decCurrentPurchaseAmt : 0;

                txtLastPreviousBalance.Text = (decPrev2MoCurrentDueAmount + decPrev1MoCurrentDueAmount).ToString("#,##0.#0");

                decimal decCurrentDueAmt = decPrev2MoCurrentDueAmount + decPrev1MoCurrentDueAmount + decCurrMonthAmountPaid + decTotalBillCharges + decCurrentPurchaseAmt;
                txtLastCurrentDueAmount.Text = decCurrentDueAmt.ToString("#,##0.#0");

                if (decCurrentDueAmt < decimal.Parse("500"))
                    txtLastMinimumAmountDue.Text = decCurrentDueAmt.ToString("#,##0.#0");
                if (decCurrentDueAmt * decimal.Parse("0.10") < decimal.Parse("500"))
                    txtLastMinimumAmountDue.Text = "500.00";
                else
                    txtLastMinimumAmountDue.Text = (decCurrentDueAmt * decimal.Parse("0.10")).ToString("#,##0.#0");
            }
        }

        private void ComputeCurrBalance()
        {
            if (!mboIsLoading)
            {
                decimal decPrev2MoCurrentDueAmount = decimal.TryParse(txtPrev2MoCurrentDueAmount.Text, out decPrev2MoCurrentDueAmount) ? decPrev2MoCurrentDueAmount : 0;
                decimal decPrev1MoCurrentDueAmount = decimal.TryParse(txtPrev1MoCurrentDueAmount.Text, out decPrev1MoCurrentDueAmount) ? decPrev1MoCurrentDueAmount : 0;
                decimal decCurrMonthAmountPaid = decimal.TryParse(txtCurrMonthAmountPaid.Text, out decCurrMonthAmountPaid) ? decCurrMonthAmountPaid : 0;

                //txtBalance.Text = (decCurrentDue + decPayments).ToString("#,##0.#0");

                decimal decTotalBillCharges = decimal.TryParse(txtTotalBillCharges.Text, out decTotalBillCharges) ? decTotalBillCharges : 0;
                decimal decCurrentPurchaseAmt = decimal.TryParse(txtCurrentPurchaseAmt.Text, out decCurrentPurchaseAmt) ? decCurrentPurchaseAmt : 0;

                txtPreviousBalance.Text = (decPrev2MoCurrentDueAmount + decPrev1MoCurrentDueAmount).ToString("#,##0.#0");

                decimal decCurrentDueAmt = decPrev2MoCurrentDueAmount + decPrev1MoCurrentDueAmount + decCurrMonthAmountPaid + decTotalBillCharges + decCurrentPurchaseAmt;
                txtCurrentDueAmount.Text = decCurrentDueAmt.ToString("#,##0.#0");

                if (decCurrentDueAmt < decimal.Parse("500"))
                    txtMinimumAmountDue.Text = decCurrentDueAmt.ToString("#,##0.#0");
                if (decCurrentDueAmt * decimal.Parse("0.10") < decimal.Parse("500"))
                    txtMinimumAmountDue.Text = "500.00";
                else
                    txtMinimumAmountDue.Text = (decCurrentDueAmt * decimal.Parse("0.10")).ToString("#,##0.#0");
            }
        }

        #endregion

        private bool SaveRecord()
        {
            bool boRetValue = false;

            if (MessageBox.Show("Are you sure you want to adjust the billing details?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                decimal decTemp = 0;
                Data.CreditBillHeaderDetails clsCreditBillHeaderDetails;
                Data.CreditBillHeaders clsCreditBillHeaders = new Data.CreditBillHeaders();

                Int64 CreditBillHeaderID = Int64.Parse(grpBillingDetails.Tag.ToString());
                if (CreditBillHeaderID != 0)
                {
                    clsCreditBillHeaderDetails = new Data.CreditBillHeaderDetails();
                    clsCreditBillHeaderDetails.CreditBillHeaderID = CreditBillHeaderID;
                    clsCreditBillHeaderDetails.BillingDate = CreditorDetails.CreditDetails.Last2BillingDate;
                    clsCreditBillHeaderDetails.Prev2MoCurrentDueAmount = decimal.TryParse(txtLastPrev2MoCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.Prev1MoCurrentDueAmount = decimal.TryParse(txtLastPrev1MoCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthAmountPaid = decimal.TryParse(txtLastCurrMonthAmountPaid.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.TotalBillCharges = decimal.TryParse(txtLastTotalBillCharges.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentPurchaseAmt = decimal.TryParse(txtLastCurrentPurchaseAmt.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.MinimumAmountDue = decimal.TryParse(txtLastMinimumAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentDueAmount = decimal.TryParse(txtLastCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    // update the previous billing
                    clsCreditBillHeaders.OverWriteBillingNoG(clsCreditBillHeaderDetails);
                    clsCreditBillHeaders.setIsBillPrintedNoG(CreditorDetails.ContactID, CreditorDetails.CreditDetails.Last2BillingDate, false);
                }

                CreditBillHeaderID = Int64.Parse(grpCurrBilling.Tag.ToString());
                if (CreditBillHeaderID != 0)
                {
                    clsCreditBillHeaderDetails = new Data.CreditBillHeaderDetails();
                    clsCreditBillHeaderDetails.CreditBillHeaderID = CreditBillHeaderID;
                    clsCreditBillHeaderDetails.BillingDate = CreditorDetails.CreditDetails.LastBillingDate;
                    clsCreditBillHeaderDetails.Prev2MoCurrentDueAmount = decimal.TryParse(txtPrev2MoCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.Prev1MoCurrentDueAmount = decimal.TryParse(txtPrev1MoCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthAmountPaid = decimal.TryParse(txtCurrMonthAmountPaid.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.TotalBillCharges = decimal.TryParse(txtTotalBillCharges.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentPurchaseAmt = decimal.TryParse(txtCurrentPurchaseAmt.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.MinimumAmountDue = decimal.TryParse(txtMinimumAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentDueAmount = decimal.TryParse(txtCurrentDueAmount.Text, out decTemp) ? decTemp : 0;
                    // update the previous billing
                    clsCreditBillHeaders.OverWriteBillingNoG(clsCreditBillHeaderDetails);
                    clsCreditBillHeaders.setIsBillPrintedNoG(CreditorDetails.ContactID, CreditorDetails.CreditDetails.Last2BillingDate, false);
                }
                clsCreditBillHeaders.CommitAndDispose();

                boRetValue = true;
            }
            return boRetValue;
        }

        private void txtPrev2MoCurrentDueAmount_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void txtPrev1MoCurrentDueAmount_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void txtCurrMonthAmountPaid_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void txtTotalBillCharges_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void txtCurrentPurchaseAmt_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void cmdLastEqual_Click(object sender, EventArgs e)
        {
            if (cmdLastEqual.Text == " = ")
            {
                txtLastMinimumAmountDue.Text = txtLastCurrentDueAmount.Text;
                cmdLastEqual.Text = "10%";
            }
            else
            {
                txtLastMinimumAmountDue.Text = (decimal.Parse(txtLastCurrentDueAmount.Text) * decimal.Parse("0.10")).ToString("#,##0.#0");
                cmdLastEqual.Text = " = ";
            }
        }

        private void cmdEqual_Click(object sender, EventArgs e)
        {
            if (cmdEqual.Text == " = ")
            {
                txtMinimumAmountDue.Text = txtCurrentDueAmount.Text;
                cmdEqual.Text = "10%";
            }
            else
            {
                txtMinimumAmountDue.Text = (decimal.Parse(txtCurrentDueAmount.Text) * decimal.Parse("0.10")).ToString("#,##0.#0");
                cmdEqual.Text = " = ";
            }
        }
    }
}
