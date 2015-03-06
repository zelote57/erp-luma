using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for CreditBillWGuaWnd.
	/// </summary>
    public class CreditBillWGuaWnd : System.Windows.Forms.Form
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
        //private TextBox txtSelectedTexBox;
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
        private Label label2;
        private TextBox txtCurrentDue;
        private Label label1;
        private TextBox txtBeginningBalance;
        private Label label12;
        private TextBox txtEndingBalance;
        private Label label11;
        private TextBox txtTotalAmountDue;
        private Label label10;
        private TextBox txtAmountDue;
        private Label label9;
        private TextBox txtPurchases;
        private Label label8;
        private TextBox txtPenalty;
        private Label label6;
        private TextBox txtBalance;
        private Label label3;
        private TextBox txtPayments;
        private GroupBox grpCurrBilling;
        private Label label13;
        private TextBox txtCurrEndingBalance;
        private Label label14;
        private TextBox txtCurrTotalAmountDue;
        private Label label15;
        private TextBox txtCurrAmountDue;
        private Label label16;
        private TextBox txtCurrPurchases;
        private Label label17;
        private TextBox txtCurrPenalty;
        private Label label18;
        private TextBox txtCurrBalance;
        private Label label19;
        private TextBox txtCurrPayments;
        private Label label20;
        private TextBox txtCurrCurrentDue;
        private Label label21;
        private TextBox txtCurrBeginningBalance;

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

		public CreditBillWGuaWnd()
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
            this.label12 = new System.Windows.Forms.Label();
            this.txtEndingBalance = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalAmountDue = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAmountDue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPurchases = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPenalty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPayments = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCurrentDue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBeginningBalance = new System.Windows.Forms.TextBox();
            this.grpCurrBilling = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCurrEndingBalance = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCurrTotalAmountDue = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCurrAmountDue = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCurrPurchases = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtCurrPenalty = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCurrBalance = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCurrPayments = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCurrCurrentDue = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtCurrBeginningBalance = new System.Windows.Forms.TextBox();
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
            this.txtCustomerName.TabIndex = 1;
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
            this.txtAvailableCredit.TabIndex = 9;
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
            this.txtCreditLimit.TabIndex = 7;
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
            this.txtCredit.TabIndex = 8;
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
            // grpBillingDetails
            // 
            this.grpBillingDetails.BackColor = System.Drawing.Color.White;
            this.grpBillingDetails.Controls.Add(this.label12);
            this.grpBillingDetails.Controls.Add(this.txtEndingBalance);
            this.grpBillingDetails.Controls.Add(this.label11);
            this.grpBillingDetails.Controls.Add(this.txtTotalAmountDue);
            this.grpBillingDetails.Controls.Add(this.label10);
            this.grpBillingDetails.Controls.Add(this.txtAmountDue);
            this.grpBillingDetails.Controls.Add(this.label9);
            this.grpBillingDetails.Controls.Add(this.txtPurchases);
            this.grpBillingDetails.Controls.Add(this.label8);
            this.grpBillingDetails.Controls.Add(this.txtPenalty);
            this.grpBillingDetails.Controls.Add(this.label6);
            this.grpBillingDetails.Controls.Add(this.txtBalance);
            this.grpBillingDetails.Controls.Add(this.label3);
            this.grpBillingDetails.Controls.Add(this.txtPayments);
            this.grpBillingDetails.Controls.Add(this.label2);
            this.grpBillingDetails.Controls.Add(this.txtCurrentDue);
            this.grpBillingDetails.Controls.Add(this.label1);
            this.grpBillingDetails.Controls.Add(this.txtBeginningBalance);
            this.grpBillingDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBillingDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpBillingDetails.Location = new System.Drawing.Point(7, 175);
            this.grpBillingDetails.Name = "grpBillingDetails";
            this.grpBillingDetails.Size = new System.Drawing.Size(445, 364);
            this.grpBillingDetails.TabIndex = 13;
            this.grpBillingDetails.TabStop = false;
            this.grpBillingDetails.Text = "Billing Details: Nov 20,2014";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Window;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(79, 321);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Ending Balance";
            // 
            // txtEndingBalance
            // 
            this.txtEndingBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtEndingBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEndingBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndingBalance.Location = new System.Drawing.Point(198, 321);
            this.txtEndingBalance.MaxLength = 75;
            this.txtEndingBalance.Name = "txtEndingBalance";
            this.txtEndingBalance.Size = new System.Drawing.Size(169, 30);
            this.txtEndingBalance.TabIndex = 32;
            this.txtEndingBalance.Text = "0.00";
            this.txtEndingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(79, 285);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Total Amt. Due";
            // 
            // txtTotalAmountDue
            // 
            this.txtTotalAmountDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtTotalAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmountDue.Location = new System.Drawing.Point(198, 285);
            this.txtTotalAmountDue.MaxLength = 75;
            this.txtTotalAmountDue.Name = "txtTotalAmountDue";
            this.txtTotalAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtTotalAmountDue.TabIndex = 30;
            this.txtTotalAmountDue.Text = "0.00";
            this.txtTotalAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(79, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Amount Due";
            // 
            // txtAmountDue
            // 
            this.txtAmountDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmountDue.Location = new System.Drawing.Point(198, 249);
            this.txtAmountDue.MaxLength = 75;
            this.txtAmountDue.Name = "txtAmountDue";
            this.txtAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtAmountDue.TabIndex = 28;
            this.txtAmountDue.Text = "0.00";
            this.txtAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(79, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Purchases";
            // 
            // txtPurchases
            // 
            this.txtPurchases.BackColor = System.Drawing.SystemColors.Window;
            this.txtPurchases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPurchases.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchases.Location = new System.Drawing.Point(198, 213);
            this.txtPurchases.MaxLength = 75;
            this.txtPurchases.Name = "txtPurchases";
            this.txtPurchases.Size = new System.Drawing.Size(169, 30);
            this.txtPurchases.TabIndex = 26;
            this.txtPurchases.Text = "0.00";
            this.txtPurchases.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Window;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(79, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Penalty";
            // 
            // txtPenalty
            // 
            this.txtPenalty.BackColor = System.Drawing.SystemColors.Window;
            this.txtPenalty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPenalty.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPenalty.Location = new System.Drawing.Point(198, 177);
            this.txtPenalty.MaxLength = 75;
            this.txtPenalty.Name = "txtPenalty";
            this.txtPenalty.Size = new System.Drawing.Size(169, 30);
            this.txtPenalty.TabIndex = 24;
            this.txtPenalty.Text = "0.00";
            this.txtPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(79, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Balance";
            // 
            // txtBalance
            // 
            this.txtBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.Location = new System.Drawing.Point(198, 141);
            this.txtBalance.MaxLength = 75;
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(169, 30);
            this.txtBalance.TabIndex = 22;
            this.txtBalance.Text = "0.00";
            this.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(79, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Payments";
            // 
            // txtPayments
            // 
            this.txtPayments.BackColor = System.Drawing.SystemColors.Window;
            this.txtPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPayments.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayments.Location = new System.Drawing.Point(198, 105);
            this.txtPayments.MaxLength = 75;
            this.txtPayments.Name = "txtPayments";
            this.txtPayments.Size = new System.Drawing.Size(169, 30);
            this.txtPayments.TabIndex = 20;
            this.txtPayments.Text = "0.00";
            this.txtPayments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPayments.TextChanged += new System.EventHandler(this.txtPayments_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(79, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Current Due";
            // 
            // txtCurrentDue
            // 
            this.txtCurrentDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrentDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentDue.Location = new System.Drawing.Point(198, 69);
            this.txtCurrentDue.MaxLength = 75;
            this.txtCurrentDue.Name = "txtCurrentDue";
            this.txtCurrentDue.Size = new System.Drawing.Size(169, 30);
            this.txtCurrentDue.TabIndex = 19;
            this.txtCurrentDue.Text = "0.00";
            this.txtCurrentDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrentDue.TextChanged += new System.EventHandler(this.txtCurrentDue_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(79, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Beginning Balance";
            // 
            // txtBeginningBalance
            // 
            this.txtBeginningBalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtBeginningBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBeginningBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBeginningBalance.Location = new System.Drawing.Point(198, 33);
            this.txtBeginningBalance.MaxLength = 75;
            this.txtBeginningBalance.Name = "txtBeginningBalance";
            this.txtBeginningBalance.Size = new System.Drawing.Size(169, 30);
            this.txtBeginningBalance.TabIndex = 17;
            this.txtBeginningBalance.Text = "0.00";
            this.txtBeginningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpCurrBilling
            // 
            this.grpCurrBilling.BackColor = System.Drawing.Color.White;
            this.grpCurrBilling.Controls.Add(this.label13);
            this.grpCurrBilling.Controls.Add(this.txtCurrEndingBalance);
            this.grpCurrBilling.Controls.Add(this.label14);
            this.grpCurrBilling.Controls.Add(this.txtCurrTotalAmountDue);
            this.grpCurrBilling.Controls.Add(this.label15);
            this.grpCurrBilling.Controls.Add(this.txtCurrAmountDue);
            this.grpCurrBilling.Controls.Add(this.label16);
            this.grpCurrBilling.Controls.Add(this.txtCurrPurchases);
            this.grpCurrBilling.Controls.Add(this.label17);
            this.grpCurrBilling.Controls.Add(this.txtCurrPenalty);
            this.grpCurrBilling.Controls.Add(this.label18);
            this.grpCurrBilling.Controls.Add(this.txtCurrBalance);
            this.grpCurrBilling.Controls.Add(this.label19);
            this.grpCurrBilling.Controls.Add(this.txtCurrPayments);
            this.grpCurrBilling.Controls.Add(this.label20);
            this.grpCurrBilling.Controls.Add(this.txtCurrCurrentDue);
            this.grpCurrBilling.Controls.Add(this.label21);
            this.grpCurrBilling.Controls.Add(this.txtCurrBeginningBalance);
            this.grpCurrBilling.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCurrBilling.ForeColor = System.Drawing.Color.Blue;
            this.grpCurrBilling.Location = new System.Drawing.Point(505, 175);
            this.grpCurrBilling.Name = "grpCurrBilling";
            this.grpCurrBilling.Size = new System.Drawing.Size(445, 364);
            this.grpCurrBilling.TabIndex = 14;
            this.grpCurrBilling.TabStop = false;
            this.grpCurrBilling.Text = "Billing Details: Nov 20,2014";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Window;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(79, 321);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Ending Balance";
            // 
            // txtCurrEndingBalance
            // 
            this.txtCurrEndingBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCurrEndingBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrEndingBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrEndingBalance.Location = new System.Drawing.Point(198, 321);
            this.txtCurrEndingBalance.MaxLength = 75;
            this.txtCurrEndingBalance.Name = "txtCurrEndingBalance";
            this.txtCurrEndingBalance.Size = new System.Drawing.Size(169, 30);
            this.txtCurrEndingBalance.TabIndex = 32;
            this.txtCurrEndingBalance.Text = "0.00";
            this.txtCurrEndingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Window;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(79, 285);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Total Amt. Due";
            // 
            // txtCurrTotalAmountDue
            // 
            this.txtCurrTotalAmountDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrTotalAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrTotalAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrTotalAmountDue.Location = new System.Drawing.Point(198, 285);
            this.txtCurrTotalAmountDue.MaxLength = 75;
            this.txtCurrTotalAmountDue.Name = "txtCurrTotalAmountDue";
            this.txtCurrTotalAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtCurrTotalAmountDue.TabIndex = 30;
            this.txtCurrTotalAmountDue.Text = "0.00";
            this.txtCurrTotalAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MediumBlue;
            this.label15.Location = new System.Drawing.Point(79, 249);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Amount Due";
            // 
            // txtCurrAmountDue
            // 
            this.txtCurrAmountDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrAmountDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrAmountDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrAmountDue.Location = new System.Drawing.Point(198, 249);
            this.txtCurrAmountDue.MaxLength = 75;
            this.txtCurrAmountDue.Name = "txtCurrAmountDue";
            this.txtCurrAmountDue.Size = new System.Drawing.Size(169, 30);
            this.txtCurrAmountDue.TabIndex = 28;
            this.txtCurrAmountDue.Text = "0.00";
            this.txtCurrAmountDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Window;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MediumBlue;
            this.label16.Location = new System.Drawing.Point(79, 213);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Purchases";
            // 
            // txtCurrPurchases
            // 
            this.txtCurrPurchases.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrPurchases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrPurchases.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrPurchases.Location = new System.Drawing.Point(198, 213);
            this.txtCurrPurchases.MaxLength = 75;
            this.txtCurrPurchases.Name = "txtCurrPurchases";
            this.txtCurrPurchases.Size = new System.Drawing.Size(169, 30);
            this.txtCurrPurchases.TabIndex = 26;
            this.txtCurrPurchases.Text = "0.00";
            this.txtCurrPurchases.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Window;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.MediumBlue;
            this.label17.Location = new System.Drawing.Point(79, 177);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Penalty";
            // 
            // txtCurrPenalty
            // 
            this.txtCurrPenalty.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrPenalty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrPenalty.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrPenalty.Location = new System.Drawing.Point(198, 177);
            this.txtCurrPenalty.MaxLength = 75;
            this.txtCurrPenalty.Name = "txtCurrPenalty";
            this.txtCurrPenalty.Size = new System.Drawing.Size(169, 30);
            this.txtCurrPenalty.TabIndex = 24;
            this.txtCurrPenalty.Text = "0.00";
            this.txtCurrPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.Window;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.MediumBlue;
            this.label18.Location = new System.Drawing.Point(79, 141);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "Balance";
            // 
            // txtCurrBalance
            // 
            this.txtCurrBalance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCurrBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrBalance.Location = new System.Drawing.Point(198, 141);
            this.txtCurrBalance.MaxLength = 75;
            this.txtCurrBalance.Name = "txtCurrBalance";
            this.txtCurrBalance.ReadOnly = true;
            this.txtCurrBalance.Size = new System.Drawing.Size(169, 30);
            this.txtCurrBalance.TabIndex = 22;
            this.txtCurrBalance.Text = "0.00";
            this.txtCurrBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Window;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.MediumBlue;
            this.label19.Location = new System.Drawing.Point(79, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Payments";
            // 
            // txtCurrPayments
            // 
            this.txtCurrPayments.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrPayments.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrPayments.Location = new System.Drawing.Point(198, 105);
            this.txtCurrPayments.MaxLength = 75;
            this.txtCurrPayments.Name = "txtCurrPayments";
            this.txtCurrPayments.Size = new System.Drawing.Size(169, 30);
            this.txtCurrPayments.TabIndex = 20;
            this.txtCurrPayments.Text = "0.00";
            this.txtCurrPayments.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrPayments.TextChanged += new System.EventHandler(this.txtCurrPayments_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Window;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.MediumBlue;
            this.label20.Location = new System.Drawing.Point(79, 69);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "Current Due";
            // 
            // txtCurrCurrentDue
            // 
            this.txtCurrCurrentDue.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrCurrentDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrCurrentDue.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrCurrentDue.Location = new System.Drawing.Point(198, 69);
            this.txtCurrCurrentDue.MaxLength = 75;
            this.txtCurrCurrentDue.Name = "txtCurrCurrentDue";
            this.txtCurrCurrentDue.Size = new System.Drawing.Size(169, 30);
            this.txtCurrCurrentDue.TabIndex = 19;
            this.txtCurrCurrentDue.Text = "0.00";
            this.txtCurrCurrentDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCurrCurrentDue.TextChanged += new System.EventHandler(this.txtCurrCurrentDue_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Window;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.MediumBlue;
            this.label21.Location = new System.Drawing.Point(79, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(109, 13);
            this.label21.TabIndex = 16;
            this.label21.Text = "Beginning Balance";
            // 
            // txtCurrBeginningBalance
            // 
            this.txtCurrBeginningBalance.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrBeginningBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrBeginningBalance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrBeginningBalance.Location = new System.Drawing.Point(198, 33);
            this.txtCurrBeginningBalance.MaxLength = 75;
            this.txtCurrBeginningBalance.Name = "txtCurrBeginningBalance";
            this.txtCurrBeginningBalance.Size = new System.Drawing.Size(169, 30);
            this.txtCurrBeginningBalance.TabIndex = 17;
            this.txtCurrBeginningBalance.Text = "0.00";
            this.txtCurrBeginningBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CreditBillWGuaWnd
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
            this.Name = "CreditBillWGuaWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditBillWGuaWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditBillWGuaWnd_KeyDown);
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

		private void CreditBillWGuaWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		private void CreditBillWGuaWnd_Load(object sender, System.EventArgs e)
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

        private void txtCurrentDue_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }

        private void txtPayments_TextChanged(object sender, EventArgs e)
        {
            ComputeBalance();
        }


        private void txtCurrCurrentDue_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        private void txtCurrPayments_TextChanged(object sender, EventArgs e)
        {
            ComputeCurrBalance();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
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
                txtBeginningBalance.Text = clsBillingDetails.BeginningBalance.ToString("#,##0.#0");
                txtCurrentDue.Text = clsBillingDetails.Prev1MoCurrentDueAmount.ToString("#,##0.#0");
                txtPayments.Text = clsBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");
                //if (clsBillingDetails.Prev1MoCurrentDueAmount <= 0)
                    
                //else
                //    txtPayments.Text = "-" + clsBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");

                txtBalance.Text = clsBillingDetails.RunningCreditAmt.ToString("#,##0.#0");
                txtPenalty.Text = clsBillingDetails.TotalBillCharges.ToString("#,##0.#0");
                txtPurchases.Text = clsBillingDetails.CurrentPurchaseAmt.ToString("#,##0.#0");
                txtAmountDue.Text = clsBillingDetails.CurrMonthCreditAmt.ToString("#,##0.#0");
                txtTotalAmountDue.Text = clsBillingDetails.CurrentDueAmount.ToString("#,##0.#0");
                txtEndingBalance.Text = clsBillingDetails.EndingBalance.ToString("#,##0.#0");
                grpBillingDetails.Visible = true;
            }
            else { grpBillingDetails.Visible = false; }

            grpCurrBilling.Tag = "0";
            if (clsCurrBillingDetails.BillingDate == CreditorDetails.CreditDetails.LastBillingDate)
            {
                grpCurrBilling.Tag = clsCurrBillingDetails.CreditBillHeaderID;
                txtCurrBeginningBalance.Text = clsCurrBillingDetails.BeginningBalance.ToString("#,##0.#0");
                txtCurrCurrentDue.Text = clsCurrBillingDetails.Prev1MoCurrentDueAmount.ToString("#,##0.#0");
                txtCurrPayments.Text = clsCurrBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");

                //if (clsCurrBillingDetails.Prev1MoCurrentDueAmount <= 0)
                //    txtCurrPayments.Text = clsCurrBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");
                //else
                //    txtCurrPayments.Text = "-" + clsCurrBillingDetails.CurrMonthAmountPaid.ToString("#,##0.#0");

                txtCurrBalance.Text = clsCurrBillingDetails.RunningCreditAmt.ToString("#,##0.#0");
                txtCurrPenalty.Text = clsCurrBillingDetails.TotalBillCharges.ToString("#,##0.#0");
                txtCurrPurchases.Text = clsCurrBillingDetails.CurrentPurchaseAmt.ToString("#,##0.#0");
                txtCurrAmountDue.Text = clsCurrBillingDetails.CurrMonthCreditAmt.ToString("#,##0.#0");
                txtCurrTotalAmountDue.Text = clsCurrBillingDetails.CurrentDueAmount.ToString("#,##0.#0");
                txtCurrEndingBalance.Text = clsCurrBillingDetails.EndingBalance.ToString("#,##0.#0");
                grpCurrBilling.Visible = true;
            }
            else { grpCurrBilling.Visible = false; }
        }

        private void LoadOptions()
        {
            
        }

        private void ComputeBalance()
        {
            decimal decCurrentDue = decimal.TryParse(txtCurrentDue.Text, out decCurrentDue) ? decCurrentDue : 0;
            decimal decPayments = decimal.TryParse(txtPayments.Text, out decPayments) ? decPayments : 0;

            txtBalance.Text = (decCurrentDue + decPayments).ToString("#,##0.#0");

            decimal decBeginningBalance = decimal.TryParse(txtBeginningBalance.Text, out decBeginningBalance) ? decBeginningBalance : 0;
            decimal decPenalty = decimal.TryParse(txtPenalty.Text, out decPenalty) ? decPenalty : 0;
            decimal decPurchases = decimal.TryParse(txtPurchases.Text, out decPurchases) ? decPurchases : 0;

            txtEndingBalance.Text = (decBeginningBalance + decPayments + decPenalty + decPurchases).ToString("#,##0.#0");
        }

        private void ComputeCurrBalance()
        {
            decimal decCurrentDue = 0;
            decimal decPayments = 0;

            decCurrentDue = decimal.TryParse(txtCurrCurrentDue.Text, out decCurrentDue) ? decCurrentDue : 0;
            decPayments = decimal.TryParse(txtCurrPayments.Text, out decPayments) ? decPayments : 0;

            txtCurrBalance.Text = (decCurrentDue + decPayments).ToString("#,##0.#0");

            decimal decCurrBeginningBalance = decimal.TryParse(txtCurrBeginningBalance.Text, out decCurrBeginningBalance) ? decCurrBeginningBalance : 0;
            decimal decCurrPenalty = decimal.TryParse(txtCurrPenalty.Text, out decCurrPenalty) ? decCurrPenalty : 0;
            decimal decCurrPurchases = decimal.TryParse(txtCurrPurchases.Text, out decCurrPurchases) ? decCurrPurchases : 0;

            txtCurrEndingBalance.Text = (decCurrBeginningBalance + decPayments + decCurrPenalty + decCurrPurchases).ToString("#,##0.#0");
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
                    clsCreditBillHeaderDetails.BeginningBalance = decimal.TryParse(txtBeginningBalance.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.Prev1MoCurrentDueAmount = decimal.TryParse(txtCurrentDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthAmountPaid = decimal.TryParse(txtPayments.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.RunningCreditAmt = decimal.TryParse(txtBalance.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.TotalBillCharges = decimal.TryParse(txtPenalty.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentPurchaseAmt = decimal.TryParse(txtPurchases.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthCreditAmt = decimal.TryParse(txtAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentDueAmount = decimal.TryParse(txtTotalAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.EndingBalance = decimal.TryParse(txtEndingBalance.Text, out decTemp) ? decTemp : 0;
                    // update the previous billing
                    clsCreditBillHeaders.OverWriteBilling(clsCreditBillHeaderDetails);
                    clsCreditBillHeaders.setIsBillPrinted(CreditorDetails.CreditDetails.GuarantorID, CreditorDetails.CreditDetails.Last2BillingDate, false);
                }

                CreditBillHeaderID = Int64.Parse(grpCurrBilling.Tag.ToString());
                if (CreditBillHeaderID != 0)
                {
                    clsCreditBillHeaderDetails = new Data.CreditBillHeaderDetails();
                    clsCreditBillHeaderDetails.CreditBillHeaderID = CreditBillHeaderID;
                    clsCreditBillHeaderDetails.BillingDate = CreditorDetails.CreditDetails.LastBillingDate;
                    clsCreditBillHeaderDetails.BeginningBalance = decimal.TryParse(txtCurrBeginningBalance.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.Prev1MoCurrentDueAmount = decimal.TryParse(txtCurrCurrentDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthAmountPaid = decimal.TryParse(txtCurrPayments.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.RunningCreditAmt = decimal.TryParse(txtCurrBalance.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.TotalBillCharges = decimal.TryParse(txtCurrPenalty.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentPurchaseAmt = decimal.TryParse(txtCurrPurchases.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrMonthCreditAmt = decimal.TryParse(txtCurrAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.CurrentDueAmount = decimal.TryParse(txtCurrTotalAmountDue.Text, out decTemp) ? decTemp : 0;
                    clsCreditBillHeaderDetails.EndingBalance = decimal.TryParse(txtCurrEndingBalance.Text, out decTemp) ? decTemp : 0;
                    
                    // update the previous billing
                    clsCreditBillHeaders.OverWriteBilling(clsCreditBillHeaderDetails);
                    clsCreditBillHeaders.setIsBillPrinted(CreditorDetails.CreditDetails.GuarantorID, CreditorDetails.CreditDetails.LastBillingDate, false);
                }
                clsCreditBillHeaders.CommitAndDispose();

                boRetValue = true;
            }
            return boRetValue;
        }
    }
}
