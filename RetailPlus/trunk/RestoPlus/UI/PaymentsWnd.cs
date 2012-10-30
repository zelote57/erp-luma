using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class PaymentsWnd : Form
	{
		private Label lblSubTotal;
        private GroupBox groupBox1;
        private Label label9;
		private GroupBox groupBox2;
		private Label lblDiscount;
		private Label lblBalance;
		private Label lblAmountPaid;
		private Label label8;
		private Label label15;
		private Label lblChange;
		private PictureBox imgIcon;
        private Label lblCheque;
        private Label lblCreditCard;
        private Label lblCash;
        private Label lblHeader;
        private Label lblSubTotalName;
        private Label lblBalanceName;
        private Label lblAmountPaidName;
        private Label lblChangeName;
        private GroupBox grpDebit;
        private Label lblDebit;
        private Label label1;
        private Label lblCharge;
        private Label label2;
        private Button cmdEnter;
        private Button cmdCancel;
        private Button cmdF4;
        private Button cmdF3;
        private Button cmdF2;
        private Button cmdF1;
        private Button cmdF5;
		private System.ComponentModel.Container components = null;

        private Data.TerminalDetails mclsTerminalDetails;
        private Data.SalesTransactionDetails mclsSalesTransactionDetails;
        private Data.ContactDetails mclsCustomerDetails;
		private DialogResult dialog;
		private decimal mdecSubTotal;
		private decimal mdecDiscount;
		private decimal mdecCharge;
		private decimal mdecAmountPaid;
		private decimal mdecCashPayment;
		private decimal mdecChequePayment;
		private decimal mdecCreditCardPayment;
		private decimal mdecCreditPayment;
		private decimal mdecDebitPayment;
		private decimal mdecBalanceAmount;
		private decimal mdecChangeAmount;
		private PaymentTypes mPaymentType = PaymentTypes.NotYetAssigned;

		private ArrayList marrCashPaymentDetails = new ArrayList();
		private ArrayList marrChequePaymentDetails = new ArrayList();
		private ArrayList marrCreditCardPaymentDetails = new ArrayList();
		private ArrayList marrCreditPaymentDetails = new ArrayList();
		private ArrayList marrDebitPaymentDetails = new ArrayList();

		private decimal mdecAllowedCredit = 0;
		private bool mboIsCreditAllowed = false;
		private decimal mdecAllowedDebit = 0;
		private bool mboIsDebitAllowed = false;
		private Label lblCredit;
        private bool mboIsRefund = false;
        private GroupBox grpRewardCard;
        private Label lblRewardPointsAmount;
        private Button cmdF6;

        public Data.SalesTransactionDetails SalesTransactionDetails
        {
            get { return mclsSalesTransactionDetails; }
            set { mclsSalesTransactionDetails = value; }
        }
        public Data.ContactDetails CustomerDetails
        {
            get { return mclsCustomerDetails; }
            set { mclsCustomerDetails = value; }
        }
        public Data.TerminalDetails TerminalDetails
        {
            get { return mclsTerminalDetails; }
            set { mclsTerminalDetails = value; }
        }
		public decimal AllowedCredit
		{
			get {	return mdecAllowedCredit;	}
		}
		public decimal AllowedDebit
		{
			get {	return mdecAllowedDebit;	}
		}
		public bool IsRefund
		{
			set {	mboIsRefund = value;	}
		}
		public bool IsCreditAllowed
		{
			set {	mboIsCreditAllowed = value;	}
		}
		public bool IsDebitAllowed
		{
			set {	mboIsDebitAllowed = value;	}
		}
		public DialogResult Result
		{
			get {	return dialog;	}
		}
		public decimal SubTotal
		{
			get {	return mdecSubTotal;	}
			set {	mdecSubTotal = value;	}
		}
		public decimal Discount
		{
			get	{	return mdecDiscount;	}
			set {	mdecDiscount = value;	}
		}
		public decimal Charge
		{
			get	{	return mdecCharge;	}
			set {	mdecCharge = value;	}
		}
		public decimal AmountPaid
		{
			get	{	return mdecAmountPaid;	}
		}
		public decimal CashPayment
		{
			get	{	return mdecCashPayment;	}
		}
		public decimal ChequePayment
		{
			get	{	return mdecChequePayment;	}
		}
		public decimal CreditCardPayment
		{
			get
			{
				return mdecCreditCardPayment;
			}
		}
		public decimal CreditPayment
		{
			get
			{
				return mdecCreditPayment;
			}
		}
		public decimal DebitPayment
		{
			get
			{
				return mdecDebitPayment;
			}
		}
		public decimal BalanceAmount
		{
			get
			{
				return mdecBalanceAmount;
			}
		}
		public decimal ChangeAmount
		{
			get
			{
				return mdecChangeAmount;
			}
		}
		public PaymentTypes PaymentType
		{
			get
			{	return mPaymentType;	}
		}

		public ArrayList CashPaymentDetails
		{
			get 
			{
				return marrCashPaymentDetails;
			}
		}
		public ArrayList ChequePaymentDetails
		{
			get 
			{
				return marrChequePaymentDetails;
			}
		}
		public ArrayList CreditCardPaymentDetails
		{
			get	{	return marrCreditCardPaymentDetails;	}
		}
		public ArrayList CreditPaymentDetails
		{
			get	{	return marrCreditPaymentDetails;	}
		}
		public ArrayList DebitPaymentDetails
		{
			get	{	return marrDebitPaymentDetails;	}
		}

        private decimal mdecRewardPointsPayment;
        public decimal RewardPointsPayment
        {
            get { return mdecRewardPointsPayment; }
            set { mdecRewardPointsPayment = value; }
        }

        private decimal mdecRewardConvertedPayment;
        private Label lblRewardPointsPayment;
    
        public decimal RewardConvertedPayment
        {
            get { return mdecRewardConvertedPayment; }
            set { mdecRewardConvertedPayment = value; }
        }

		string mCONFIG_TURRET_NAME;

		public string CONFIG_TURRET_NAME
		{
			set {	mCONFIG_TURRET_NAME = value ;	}
		}

		#region Constructors and Destructors

		public PaymentsWnd()
		{
			InitializeComponent();
		}
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
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblSubTotalName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblCheque = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.cmdF4 = new System.Windows.Forms.Button();
            this.cmdF3 = new System.Windows.Forms.Button();
            this.cmdF2 = new System.Windows.Forms.Button();
            this.cmdF1 = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAmountPaidName = new System.Windows.Forms.Label();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblChangeName = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblDebit = new System.Windows.Forms.Label();
            this.grpDebit = new System.Windows.Forms.GroupBox();
            this.cmdF5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpRewardCard = new System.Windows.Forms.GroupBox();
            this.lblRewardPointsPayment = new System.Windows.Forms.Label();
            this.lblRewardPointsAmount = new System.Windows.Forms.Label();
            this.cmdF6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpDebit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpRewardCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblSubTotal.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSubTotal.Location = new System.Drawing.Point(690, 90);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(229, 25);
            this.lblSubTotal.TabIndex = 64;
            this.lblSubTotal.Text = "0.00";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalName
            // 
            this.lblSubTotalName.AutoSize = true;
            this.lblSubTotalName.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotalName.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblSubTotalName.Location = new System.Drawing.Point(527, 90);
            this.lblSubTotalName.Name = "lblSubTotalName";
            this.lblSubTotalName.Size = new System.Drawing.Size(99, 24);
            this.lblSubTotalName.TabIndex = 65;
            this.lblSubTotalName.Text = "SUBTOTAL";
            this.lblSubTotalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblCredit);
            this.groupBox1.Controls.Add(this.lblCheque);
            this.groupBox1.Controls.Add(this.lblCreditCard);
            this.groupBox1.Controls.Add(this.lblCash);
            this.groupBox1.Controls.Add(this.cmdF4);
            this.groupBox1.Controls.Add(this.cmdF3);
            this.groupBox1.Controls.Add(this.cmdF2);
            this.groupBox1.Controls.Add(this.cmdF1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 267);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Payment Type";
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredit.ForeColor = System.Drawing.Color.Blue;
            this.lblCredit.Location = new System.Drawing.Point(112, 225);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(168, 23);
            this.lblCredit.TabIndex = 73;
            this.lblCredit.Text = "IN-HOUSE CREDIT";
            // 
            // lblCheque
            // 
            this.lblCheque.AutoSize = true;
            this.lblCheque.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheque.Location = new System.Drawing.Point(112, 102);
            this.lblCheque.Name = "lblCheque";
            this.lblCheque.Size = new System.Drawing.Size(92, 23);
            this.lblCheque.TabIndex = 70;
            this.lblCheque.Text = "CHEQUES";
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.AutoSize = true;
            this.lblCreditCard.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCard.Location = new System.Drawing.Point(112, 163);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(139, 23);
            this.lblCreditCard.TabIndex = 68;
            this.lblCreditCard.Text = "CREDIT CARDS";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.Location = new System.Drawing.Point(112, 45);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(56, 23);
            this.lblCash.TabIndex = 0;
            this.lblCash.Text = "CASH";
            // 
            // cmdF4
            // 
            this.cmdF4.AutoSize = true;
            this.cmdF4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF4.ForeColor = System.Drawing.Color.White;
            this.cmdF4.Location = new System.Drawing.Point(27, 200);
            this.cmdF4.Name = "cmdF4";
            this.cmdF4.Size = new System.Drawing.Size(78, 62);
            this.cmdF4.TabIndex = 5;
            this.cmdF4.Text = "F4";
            this.cmdF4.UseVisualStyleBackColor = true;
            this.cmdF4.Click += new System.EventHandler(this.cmdF4_Click);
            // 
            // cmdF3
            // 
            this.cmdF3.AutoSize = true;
            this.cmdF3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF3.ForeColor = System.Drawing.Color.White;
            this.cmdF3.Location = new System.Drawing.Point(27, 140);
            this.cmdF3.Name = "cmdF3";
            this.cmdF3.Size = new System.Drawing.Size(78, 62);
            this.cmdF3.TabIndex = 4;
            this.cmdF3.Text = "F3";
            this.cmdF3.UseVisualStyleBackColor = true;
            this.cmdF3.Click += new System.EventHandler(this.cmdF3_Click);
            // 
            // cmdF2
            // 
            this.cmdF2.AutoSize = true;
            this.cmdF2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF2.ForeColor = System.Drawing.Color.White;
            this.cmdF2.Location = new System.Drawing.Point(27, 80);
            this.cmdF2.Name = "cmdF2";
            this.cmdF2.Size = new System.Drawing.Size(78, 62);
            this.cmdF2.TabIndex = 3;
            this.cmdF2.Text = "F2";
            this.cmdF2.UseVisualStyleBackColor = true;
            this.cmdF2.Click += new System.EventHandler(this.cmdF2_Click);
            // 
            // cmdF1
            // 
            this.cmdF1.AutoSize = true;
            this.cmdF1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF1.ForeColor = System.Drawing.Color.White;
            this.cmdF1.Location = new System.Drawing.Point(27, 20);
            this.cmdF1.Name = "cmdF1";
            this.cmdF1.Size = new System.Drawing.Size(78, 62);
            this.cmdF1.TabIndex = 2;
            this.cmdF1.Text = "F1";
            this.cmdF1.UseVisualStyleBackColor = true;
            this.cmdF1.Click += new System.EventHandler(this.cmdF1_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(100, 13);
            this.lblHeader.TabIndex = 67;
            this.lblHeader.Text = "Enter Payments.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(527, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 24);
            this.label9.TabIndex = 69;
            this.label9.Text = "DISCOUNT (-)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscount.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDiscount.Location = new System.Drawing.Point(690, 158);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(229, 23);
            this.lblDiscount.TabIndex = 70;
            this.lblDiscount.Text = "0.00";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Location = new System.Drawing.Point(521, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 10);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            // 
            // lblBalanceName
            // 
            this.lblBalanceName.AutoSize = true;
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(519, 237);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(183, 38);
            this.lblBalanceName.TabIndex = 72;
            this.lblBalanceName.Text = "BALANCE";
            this.lblBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(699, 240);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(229, 35);
            this.lblBalance.TabIndex = 73;
            this.lblBalance.Text = "0.00";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountPaidName
            // 
            this.lblAmountPaidName.AutoSize = true;
            this.lblAmountPaidName.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountPaidName.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblAmountPaidName.Location = new System.Drawing.Point(526, 192);
            this.lblAmountPaidName.Name = "lblAmountPaidName";
            this.lblAmountPaidName.Size = new System.Drawing.Size(125, 24);
            this.lblAmountPaidName.TabIndex = 76;
            this.lblAmountPaidName.Text = "AMOUNT PAID";
            this.lblAmountPaidName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountPaid.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblAmountPaid.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAmountPaid.Location = new System.Drawing.Point(690, 191);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(229, 25);
            this.lblAmountPaid.TabIndex = 77;
            this.lblAmountPaid.Text = "0.00";
            this.lblAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label8.Location = new System.Drawing.Point(742, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Press F6 to toogle payment details.";
            this.label8.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MistyRose;
            this.label15.Location = new System.Drawing.Point(726, 41);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "F6";
            this.label15.Visible = false;
            // 
            // lblChangeName
            // 
            this.lblChangeName.AutoSize = true;
            this.lblChangeName.BackColor = System.Drawing.Color.Transparent;
            this.lblChangeName.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangeName.Location = new System.Drawing.Point(519, 280);
            this.lblChangeName.Name = "lblChangeName";
            this.lblChangeName.Size = new System.Drawing.Size(167, 38);
            this.lblChangeName.TabIndex = 80;
            this.lblChangeName.Text = "CHANGE";
            this.lblChangeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChange
            // 
            this.lblChange.BackColor = System.Drawing.Color.Transparent;
            this.lblChange.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.Firebrick;
            this.lblChange.Location = new System.Drawing.Point(701, 280);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(229, 36);
            this.lblChange.TabIndex = 81;
            this.lblChange.Text = "0.00";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebit
            // 
            this.lblDebit.AutoSize = true;
            this.lblDebit.BackColor = System.Drawing.Color.White;
            this.lblDebit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebit.ForeColor = System.Drawing.Color.Blue;
            this.lblDebit.Location = new System.Drawing.Point(112, 45);
            this.lblDebit.Name = "lblDebit";
            this.lblDebit.Size = new System.Drawing.Size(126, 23);
            this.lblDebit.TabIndex = 0;
            this.lblDebit.Text = "USE DEPOSIT";
            // 
            // grpDebit
            // 
            this.grpDebit.BackColor = System.Drawing.Color.White;
            this.grpDebit.Controls.Add(this.lblDebit);
            this.grpDebit.Controls.Add(this.cmdF5);
            this.grpDebit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDebit.ForeColor = System.Drawing.Color.Blue;
            this.grpDebit.Location = new System.Drawing.Point(9, 335);
            this.grpDebit.Name = "grpDebit";
            this.grpDebit.Size = new System.Drawing.Size(431, 89);
            this.grpDebit.TabIndex = 82;
            this.grpDebit.TabStop = false;
            this.grpDebit.Text = "Use customer deposit or debit.";
            this.grpDebit.Visible = false;
            // 
            // cmdF5
            // 
            this.cmdF5.AutoSize = true;
            this.cmdF5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF5.ForeColor = System.Drawing.Color.White;
            this.cmdF5.Location = new System.Drawing.Point(27, 20);
            this.cmdF5.Name = "cmdF5";
            this.cmdF5.Size = new System.Drawing.Size(78, 62);
            this.cmdF5.TabIndex = 6;
            this.cmdF5.Text = "F5";
            this.cmdF5.UseVisualStyleBackColor = true;
            this.cmdF5.Click += new System.EventHandler(this.cmdF5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(527, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 24);
            this.label1.TabIndex = 85;
            this.label1.Text = "OTHER CHARGES (+)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharge
            // 
            this.lblCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblCharge.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCharge.ForeColor = System.Drawing.Color.Firebrick;
            this.lblCharge.Location = new System.Drawing.Point(690, 125);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(229, 23);
            this.lblCharge.TabIndex = 86;
            this.lblCharge.Text = "0.00";
            this.lblCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(647, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "(w/ VAT)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
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
            // grpRewardCard
            // 
            this.grpRewardCard.BackColor = System.Drawing.Color.White;
            this.grpRewardCard.Controls.Add(this.lblRewardPointsPayment);
            this.grpRewardCard.Controls.Add(this.lblRewardPointsAmount);
            this.grpRewardCard.Controls.Add(this.cmdF6);
            this.grpRewardCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRewardCard.ForeColor = System.Drawing.Color.Blue;
            this.grpRewardCard.Location = new System.Drawing.Point(9, 431);
            this.grpRewardCard.Name = "grpRewardCard";
            this.grpRewardCard.Size = new System.Drawing.Size(431, 89);
            this.grpRewardCard.TabIndex = 89;
            this.grpRewardCard.TabStop = false;
            this.grpRewardCard.Text = "Use customer reward points";
            this.grpRewardCard.Visible = false;
            // 
            // lblRewardPointsPayment
            // 
            this.lblRewardPointsPayment.AutoSize = true;
            this.lblRewardPointsPayment.BackColor = System.Drawing.Color.White;
            this.lblRewardPointsPayment.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRewardPointsPayment.ForeColor = System.Drawing.Color.Blue;
            this.lblRewardPointsPayment.Location = new System.Drawing.Point(171, 17);
            this.lblRewardPointsPayment.Name = "lblRewardPointsPayment";
            this.lblRewardPointsPayment.Size = new System.Drawing.Size(195, 23);
            this.lblRewardPointsPayment.TabIndex = 7;
            this.lblRewardPointsPayment.Text = "RewardPointsPayment";
            this.lblRewardPointsPayment.Visible = false;
            // 
            // lblRewardPointsAmount
            // 
            this.lblRewardPointsAmount.AutoSize = true;
            this.lblRewardPointsAmount.BackColor = System.Drawing.Color.White;
            this.lblRewardPointsAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRewardPointsAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblRewardPointsAmount.Location = new System.Drawing.Point(112, 45);
            this.lblRewardPointsAmount.Name = "lblRewardPointsAmount";
            this.lblRewardPointsAmount.Size = new System.Drawing.Size(137, 23);
            this.lblRewardPointsAmount.TabIndex = 0;
            this.lblRewardPointsAmount.Text = "USE REWARDS";
            // 
            // cmdF6
            // 
            this.cmdF6.AutoSize = true;
            this.cmdF6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF6.ForeColor = System.Drawing.Color.White;
            this.cmdF6.Location = new System.Drawing.Point(27, 20);
            this.cmdF6.Name = "cmdF6";
            this.cmdF6.Size = new System.Drawing.Size(78, 62);
            this.cmdF6.TabIndex = 6;
            this.cmdF6.Text = "F6";
            this.cmdF6.UseVisualStyleBackColor = true;
            this.cmdF6.Click += new System.EventHandler(this.cmdF6_Click);
            // 
            // PaymentsWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.grpRewardCard);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblChangeName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblAmountPaidName);
            this.Controls.Add(this.lblBalanceName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblSubTotalName);
            this.Controls.Add(this.lblCharge);
            this.Controls.Add(this.grpDebit);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblAmountPaid);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "PaymentsWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PaymentsWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaymentsWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDebit.ResumeLayout(false);
            this.grpDebit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpRewardCard.ResumeLayout(false);
            this.grpRewardCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Windows Form Methods

		private void dgItems_Navigate(object sender, NavigateEventArgs ne)
		{
		
		}

		private void PaymentsWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Payments.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }
            try
            {
                this.cmdF1.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF2.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF3.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF4.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF5.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF6.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg"); 
            }
            catch { }

            if (mclsSalesTransactionDetails.TransactionID != 0)
            {
                mdecAllowedCredit = mclsCustomerDetails.CreditLimit - mclsCustomerDetails.Credit;
                mboIsCreditAllowed = Convert.ToBoolean(mclsCustomerDetails.IsCreditAllowed);
                mdecAllowedDebit = mclsCustomerDetails.Debit;
                if (mboIsRefund) mboIsDebitAllowed = mboIsCreditAllowed;
                else { if (mdecAllowedDebit > 0) mboIsDebitAllowed = true; else { mboIsDebitAllowed = false; } }
                
                grpDebit.Visible = mboIsDebitAllowed;

                if (mclsTerminalDetails.RewardPointsDetails.EnableRewardPointsAsPayment == true)
                {
                    if (mboIsRefund == false && mclsCustomerDetails.RewardDetails.RewardPoints != 0 && mclsCustomerDetails.ContactID != Constants.C_RETAILPLUS_CUSTOMERID)
                    {
                        grpRewardCard.Visible = mclsCustomerDetails.RewardDetails.RewardActive;
                        grpRewardCard.Text = "Use customer reward points: " + mclsCustomerDetails.RewardDetails.RewardPoints.ToString("#,##0.#0");
                    }
                    if (!grpDebit.Visible && grpRewardCard.Visible) grpRewardCard.Location = new Point(9, 335);
                }
            }

			if (mboIsRefund)
			{
				lblHeader.Text = "Enter payment types to refund.";
				lblSubTotalName.Text = "REFUND";
				lblAmountPaidName.Text = "REFUNDED AMT";
				lblChangeName.Text = "OVER";
				cmdF4.Visible = false;
				lblCredit.Visible = false;
			}
			else
			{
				if (mdecAllowedCredit <= 0 && mboIsCreditAllowed)
				{
					cmdF4.Enabled = false;
					lblCredit.Enabled = false;
					mboIsCreditAllowed = false;
					grpDebit.Visible = mboIsDebitAllowed;
				}
				else
				{
					cmdF4.Visible = mboIsCreditAllowed;
					lblCredit.Visible = mboIsCreditAllowed;
					grpDebit.Visible = mboIsDebitAllowed;
				}
			}
			lblCash.Tag = "0.00";
			lblCheque.Tag = "0.00";
			lblCreditCard.Tag = "0.00";
			lblCredit.Tag = "0.00";
			lblDebit.Tag = "0.00";
            lblRewardPointsAmount.Tag = "0.00";
            lblRewardPointsPayment.Text = "0.00";

			lblDiscount.Text = mdecDiscount.ToString("#,##0.#0");
			lblCharge.Text = mdecCharge.ToString("#,##0.#0");
			lblSubTotal.Text = mdecSubTotal.ToString("#,##0.#0");
			ComputePayments();
		}

		private void PaymentsWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.F1:
					ShowCashPaymentWindow();
					break;

				case Keys.F2:
					ShowChequePaymentWindow();
					break;

				case Keys.F3:
					ShowCreditCardPaymentWindow();
					break;

				case Keys.F4:
					ShowCreditPaymentWindow();
					break;

				case Keys.F5:
					ShowDebitPaymentWindow();
					break;

                case Keys.F6:
                    ShowRewardPointsPaymentWindow();
                    break;

				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (Convert.ToDecimal(lblBalance.Text) <= 0 || (mclsSalesTransactionDetails.TransactionID == 0 && Convert.ToDecimal(lblAmountPaid.Text) > 0))
					{
						AssignValues();

						dialog = DialogResult.OK; 
						this.Hide();
						break;
					}
					else
					{
						ShowCashPaymentWindow();
						break;
					}
			}
		}

		
		#endregion

        #region Windows Control Methods

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblBalance.Text) <= 0 || (mclsSalesTransactionDetails.TransactionID == 0 && Convert.ToDecimal(lblAmountPaid.Text) > 0))
            {
                AssignValues();

                dialog = DialogResult.OK;
                this.Hide();
            }
            else
            {
                ShowCashPaymentWindow();
            }
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void cmdF1_Click(object sender, EventArgs e)
        {
            ShowCashPaymentWindow();
        }
        private void cmdF2_Click(object sender, EventArgs e)
        {
            ShowChequePaymentWindow();
        }
        private void cmdF3_Click(object sender, EventArgs e)
        {
            ShowCreditCardPaymentWindow();
        }
        private void cmdF4_Click(object sender, EventArgs e)
        {
            ShowCreditPaymentWindow();
        }
        private void cmdF5_Click(object sender, EventArgs e)
        {
            ShowDebitPaymentWindow();
        }
        private void cmdF6_Click(object sender, EventArgs e)
        {
            ShowRewardPointsPaymentWindow();
        }
        #endregion

        #region Computation

        private void ComputePayments()
		{
			decimal Balance = Convert.ToDecimal((Convert.ToDecimal(lblSubTotal.Text) + Convert.ToDecimal(lblCharge.Text)) - (Convert.ToDecimal(lblAmountPaid.Text) + Convert.ToDecimal(lblDiscount.Text)));
			if (Balance > 0 )
			{	
				lblBalance.Text = Balance.ToString("#,##0.#0");
				lblChange.Text = "0.00";
				if (mboIsRefund)
					SendStringToTurret("Refund Amt:".PadRight(20) + lblBalance.Text.PadLeft(20));
				else
					SendStringToTurret("Amount Due:".PadRight(20) + lblBalance.Text.PadLeft(20));
			} 
			else if (Balance <= 0 )
			{	
				lblBalance.Text = "0.00";
				Balance = Balance * -1;
				lblChange.Text = Balance.ToString("#,##0.#0");
				if (mboIsRefund)
					SendStringToTurret("Refunded Amount:".PadRight(20) + lblAmountPaid.Text.PadLeft(20));
				else
					SendStringToTurret("Tendered: " + lblAmountPaid.Text.PadLeft(10) + "Change  : " + lblChange.Text.PadLeft(10));
			}
			
		}

		
		#endregion

		#region Private Modifiers
		
		private void ShowCashPaymentWindow()
		{
			CashPaymentWnd cashshortcut = new CashPaymentWnd();
			cashshortcut.SalesTransactionDetails = mclsSalesTransactionDetails;
			cashshortcut.BalanceAmount = Convert.ToDecimal(lblBalance.Text);
			cashshortcut.ShowDialog(this);
			DialogResult result = cashshortcut.Result;
			Data.CashPaymentDetails cashDetails = cashshortcut.Details;
			cashshortcut.Close();
			cashshortcut.Dispose();

			if (result == DialogResult.OK)
			{
				lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + cashDetails.Amount).ToString("#,##0.#0");
				marrCashPaymentDetails.Add(cashDetails);
				ComputePayments();
				lblCash.Tag = Convert.ToDecimal(Convert.ToDecimal(lblCash.Tag) + cashDetails.Amount - Convert.ToDecimal(lblChange.Text)).ToString("#,##0.#0");

				if (mPaymentType == PaymentTypes.NotYetAssigned)
					mPaymentType = PaymentTypes.Cash;
				else
					mPaymentType = PaymentTypes.Combination;

			}
			
		}
		private void ShowChequePaymentWindow()
		{
			ChequesPaymentWnd cheque = new ChequesPaymentWnd();
			cheque.SalesTransactionDetails = mclsSalesTransactionDetails;
			cheque.BalanceAmount = Convert.ToDecimal(lblBalance.Text);
			cheque.ShowDialog(this);
			DialogResult result = cheque.Result;
			Data.ChequePaymentDetails chequeDetails = cheque.Details;
			cheque.Close();
			cheque.Dispose();

			if (result == DialogResult.OK)
			{
				lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + chequeDetails.Amount).ToString("#,##0.#0");
				marrChequePaymentDetails.Add(chequeDetails);
				ComputePayments();
				lblCheque.Tag = Convert.ToDecimal(Convert.ToDecimal(lblCheque.Tag) + chequeDetails.Amount - Convert.ToDecimal(lblChange.Text)).ToString("#,##0.#0");

				if (mPaymentType == PaymentTypes.NotYetAssigned)
					mPaymentType = PaymentTypes.Cheque;
				else
					mPaymentType = PaymentTypes.Combination;

			}
			
		}
		private void ShowCreditCardPaymentWindow()
		{
			CreditCardPaymentWnd creditcard = new CreditCardPaymentWnd();
			creditcard.SalesTransactionDetails = mclsSalesTransactionDetails;
			creditcard.BalanceAmount = Convert.ToDecimal(lblBalance.Text); 
			creditcard.ShowDialog(this);
			DialogResult result = creditcard.Result;
			Data.CreditCardPaymentDetails creditcardDetails = creditcard.Details;
			creditcard.Close();
			creditcard.Dispose();

			if (result == DialogResult.OK)
			{
				lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + creditcardDetails.Amount).ToString("#,##0.#0");
				marrCreditCardPaymentDetails.Add(creditcardDetails);
				ComputePayments();
				lblCreditCard.Tag = Convert.ToDecimal(Convert.ToDecimal(lblCreditCard.Tag) + creditcardDetails.Amount - Convert.ToDecimal(lblChange.Text)).ToString("#,##0.#0");

				if (mPaymentType == PaymentTypes.NotYetAssigned)
					mPaymentType = PaymentTypes.CreditCard;
				else
					mPaymentType = PaymentTypes.Combination;

			}
			
		}
		private void ShowCreditPaymentWindow()
		{
			if (mboIsCreditAllowed)
			{
				CreditPaymentWnd credit = new CreditPaymentWnd();
                credit.SalesTransactionDetails = mclsSalesTransactionDetails;
                credit.CustomerDetails = mclsCustomerDetails;
				credit.AllowedCredit = mdecAllowedCredit;
				credit.BalanceAmount = Convert.ToDecimal(lblBalance.Text);
				
				credit.ShowDialog(this);
				DialogResult result = credit.Result;
				Data.CreditPaymentDetails creditDetails = credit.Details;
				credit.Close();
				credit.Dispose();

				if (result == DialogResult.OK)
				{
					lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + creditDetails.Amount).ToString("#,##0.#0");
					marrCreditPaymentDetails.Add(creditDetails);

                    // Add 
					ComputePayments();
					lblCredit.Tag = Convert.ToDecimal(Convert.ToDecimal(lblCredit.Tag) + creditDetails.Amount - Convert.ToDecimal(lblChange.Text)).ToString("#,##0.#0");

					if (mPaymentType == PaymentTypes.NotYetAssigned)
						mPaymentType = PaymentTypes.Credit;
					else
						mPaymentType = PaymentTypes.Combination;
				}
				
			}
		}
		private void ShowDebitPaymentWindow()
		{
			if (mboIsDebitAllowed)
			{
				DebitPaymentWnd debit = new DebitPaymentWnd();
				debit.SalesTransactionDetails = mclsSalesTransactionDetails;
                debit.CustomerDetails = mclsCustomerDetails;
				debit.AllowedDebit = mdecAllowedDebit;
				debit.BalanceAmount = Convert.ToDecimal(lblBalance.Text);
                debit.IsRefund = mboIsRefund;

				debit.ShowDialog(this);
				DialogResult result = debit.Result;
				Data.DebitPaymentDetails debitDetails = debit.Details;
				debit.Close();
				debit.Dispose();

				if (result == DialogResult.OK)
				{
					lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + debitDetails.Amount).ToString("#,##0.#0");
					marrDebitPaymentDetails.Add(debitDetails);
					ComputePayments();
					lblDebit.Tag = Convert.ToDecimal(Convert.ToDecimal(lblDebit.Tag) + debitDetails.Amount - Convert.ToDecimal(lblChange.Text)).ToString("#,##0.#0");

					if (mPaymentType == PaymentTypes.NotYetAssigned)
						mPaymentType = PaymentTypes.Debit;
					else
						mPaymentType = PaymentTypes.Combination;
				}
				
			}
		}
        private void ShowRewardPointsPaymentWindow()
        {
            RewardPointPaymentWnd clsRewardPointPaymentWnd = new RewardPointPaymentWnd();
            clsRewardPointPaymentWnd.RewardPointsPayment = decimal.Parse(lblRewardPointsPayment.Text);
            clsRewardPointPaymentWnd.RewardConvertedPayment = Convert.ToDecimal(lblRewardPointsAmount.Tag);
            clsRewardPointPaymentWnd.BalanceAmount = Convert.ToDecimal(lblBalance.Text);
            clsRewardPointPaymentWnd.IsRefund = mboIsRefund;
            clsRewardPointPaymentWnd.ContactDetails = mclsCustomerDetails;
            clsRewardPointPaymentWnd.TerminalDetails = mclsTerminalDetails;

            clsRewardPointPaymentWnd.ShowDialog(this);
            DialogResult result = clsRewardPointPaymentWnd.Result;
            decimal decRewardPointsPayment = clsRewardPointPaymentWnd.RewardPointsPayment;
            decimal decRewardConvertedPayment = clsRewardPointPaymentWnd.RewardConvertedPayment;
            decimal decAmount = clsRewardPointPaymentWnd.Amount;
            clsRewardPointPaymentWnd.Close();
            clsRewardPointPaymentWnd.Dispose();

            if (result == DialogResult.OK)
            {
                lblAmountPaid.Text = Convert.ToDecimal(Convert.ToDecimal(lblAmountPaid.Text) + decAmount).ToString("#,##0.#0");
                ComputePayments();
                lblRewardPointsAmount.Tag = Convert.ToDecimal(Convert.ToDecimal(lblRewardPointsAmount.Tag) + decAmount).ToString("#,##0.#0");
                lblRewardPointsPayment.Text = Convert.ToDecimal(Convert.ToDecimal(lblRewardPointsPayment.Tag) + decRewardPointsPayment).ToString("#,##0.#0");

                if (mPaymentType == PaymentTypes.NotYetAssigned)
                    mPaymentType = PaymentTypes.RewardPoints;
                else
                    mPaymentType = PaymentTypes.Combination;
            }
        }
		private void SendStringToTurret(string szString)
		{
			RawPrinterHelper.SendStringToPrinter(mCONFIG_TURRET_NAME, "\f" + szString, "RetailPlus Turret Disp: ");
		}

		#endregion

		#region Assign Values when OK

		private void AssignValues()
		{
			mdecSubTotal = Convert.ToDecimal(lblSubTotal.Text);
			mdecDiscount = Convert.ToDecimal(lblDiscount.Text);
			mdecCharge = Convert.ToDecimal(lblChange.Text);
			mdecAmountPaid = Convert.ToDecimal(lblAmountPaid.Text);
			mdecCashPayment = Convert.ToDecimal(lblCash.Tag);
			mdecChequePayment = Convert.ToDecimal(lblCheque.Tag);
			mdecCreditCardPayment = Convert.ToDecimal(lblCreditCard.Tag);
			mdecCreditPayment = Convert.ToDecimal(lblCredit.Tag);
			mdecDebitPayment = Convert.ToDecimal(lblDebit.Tag);
			mdecBalanceAmount = Convert.ToDecimal(lblBalance.Text);
			mdecChangeAmount = Convert.ToDecimal(lblChange.Text);
            mdecRewardConvertedPayment = Convert.ToDecimal(lblRewardPointsAmount.Tag);
            mdecRewardPointsPayment = Convert.ToDecimal(lblRewardPointsPayment.Text);
			// mPaymentType = mPaymentType;
		}

		#endregion

	}
}