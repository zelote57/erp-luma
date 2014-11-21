using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class SplitPaymentAmountWnd : Form
	{
        System.Data.DataTable ItemDataTable = new System.Data.DataTable("tblSplitPayment");
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
        private Label lblHeader;
        private Label lblSubTotalName;
        private Label lblBalanceName;
        private Label lblAmountPaidName;
        private Label lblChangeName;
        private Label label1;
        private Label lblCharge;
        private Label label2;
        private Button cmdEnter;
        private Button cmdCancel;
		private System.ComponentModel.Container components = null;

        private Data.ContactDetails mclsCustomerDetails;
        private Data.TerminalDetails mclsTerminalDetails;
        private Data.SalesTransactionDetails mclsSalesTransactionDetails;
		private DialogResult dialog;
		
        //private decimal mdecAmountPaid;
        //private decimal mdecCashPayment;
        //private decimal mdecChequePayment;
        //private decimal mdecCreditCardPayment;
        //private decimal mdecCreditPayment;
        //private decimal mdecDebitPayment;
        //private decimal mdecBalanceAmount;
        //private decimal mdecChangeAmount;

        //private ArrayList marrPayments = new ArrayList();

        //private PaymentTypes mPaymentType = PaymentTypes.NotYetAssigned;

        //private ArrayList marrCashPaymentDetails = new ArrayList();
        //private ArrayList marrChequePaymentDetails = new ArrayList();
        //private ArrayList marrCreditCardPaymentDetails = new ArrayList();
        //private ArrayList marrCreditPaymentDetails = new ArrayList();
        //private ArrayList marrDebitPaymentDetails = new ArrayList();

		private decimal mdecAllowedCredit = 0;
        private decimal mdecAllowedDebit = 0;
        private bool mboIsRefund = false;
        private bool mboCreditCardSwiped = false;

        private bool mboIsCreditChargeExcluded;
        public bool IsCreditChargeExcluded { set { mboIsCreditChargeExcluded = value; } }

        public bool CreditCardSwiped
        {
            get { return mboCreditCardSwiped; }
            set { mboCreditCardSwiped = value; }
        }
        public Data.SalesTransactionDetails SalesTransactionDetails
        {
            get { return mclsSalesTransactionDetails; }
            set { mclsSalesTransactionDetails = value; }
        }
        public Data.TerminalDetails TerminalDetails
        {
            get { return mclsTerminalDetails; }
            set { mclsTerminalDetails = value; }
        }

        private Data.SysConfigDetails mclsSysConfigDetails;
        public Data.SysConfigDetails SysConfigDetails
        {
            get { return mclsSysConfigDetails; }
            set { mclsSysConfigDetails = value; }
        }

        private Data.SplitPaymentDetails[] marrSplitPaymentDetails;
        public Data.SplitPaymentDetails[] arrSplitPaymentDetails { get { return marrSplitPaymentDetails; } }

        public Int32 NoOfDiners { get; set; }
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
        public Data.ContactDetails CustomerDetails
		{
            set { mclsCustomerDetails = value; }
		}
		public DialogResult Result
		{
			get {	return dialog;	}
		}
        //public decimal AmountPaid
        //{
        //    get	{	return mdecAmountPaid;	}
        //}
        //public decimal CashPayment
        //{
        //    get	{	return mdecCashPayment;	}
        //}
        //public decimal ChequePayment
        //{
        //    get	{	return mdecChequePayment;	}
        //}
        //public decimal CreditCardPayment
        //{
        //    get
        //    {
        //        return mdecCreditCardPayment;
        //    }
        //}
        //public decimal CreditPayment
        //{
        //    get
        //    {
        //        return mdecCreditPayment;
        //    }
        //}
        //public decimal DebitPayment
        //{
        //    get
        //    {
        //        return mdecDebitPayment;
        //    }
        //}
        //public decimal BalanceAmount
        //{
        //    get
        //    {
        //        return mdecBalanceAmount;
        //    }
        //}
        //public decimal ChangeAmount
        //{
        //    get
        //    {
        //        return mdecChangeAmount;
        //    }
        //}
        //public PaymentTypes PaymentType
        //{
        //    get
        //    {	return mPaymentType;	}
        //}

        //public ArrayList CashPaymentDetails
        //{
        //    get 
        //    {
        //        return marrCashPaymentDetails;
        //    }
        //}
        //public ArrayList ChequePaymentDetails
        //{
        //    get 
        //    {
        //        return marrChequePaymentDetails;
        //    }
        //}
        //public ArrayList CreditCardPaymentDetails
        //{
        //    get	{	return marrCreditCardPaymentDetails;	}
        //}
        //public ArrayList CreditPaymentDetails
        //{
        //    get	{	return marrCreditPaymentDetails;	}
        //}
        //public ArrayList DebitPaymentDetails
        //{
        //    get	{	return marrDebitPaymentDetails;	}
        //}

        //private decimal mdecRewardPointsPayment;
        //public decimal RewardPointsPayment
        //{
        //    get { return mdecRewardPointsPayment; }
        //    set { mdecRewardPointsPayment = value; }
        //}

        //private decimal mdecRewardConvertedPayment;
        private Label label3;
        private Label label11;
        private Label lblAmountNetOfVAT;
        private Label lbllSubtotalVATLabel;
        private Label lblSubtotalVAT;
        private GroupBox groupBox3;
        private Label label4;
        private Label lblVAT;
        private Label lblDiscountType;
        private Label lblCash;
        private Label lblCheque;
        private Label lblCreditCard;
        private Label lblCredit;
        private Label lblDebit;
        private DataGridTableStyle dgStyle;
        private DataGrid dgItems;
    
        //public decimal RewardConvertedPayment
        //{
        //    get { return mdecRewardConvertedPayment; }
        //    set { mdecRewardConvertedPayment = value; }
        //}

        //private Data.ContactDetails mclsCreditorDetails;
        //public Data.ContactDetails CreditorDetails
        //{
        //    get { return mclsCreditorDetails; }
        //}

		#region Constructors and Destructors

		public SplitPaymentAmountWnd()
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
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAmountNetOfVAT = new System.Windows.Forms.Label();
            this.lbllSubtotalVATLabel = new System.Windows.Forms.Label();
            this.lblSubtotalVAT = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblCheque = new System.Windows.Forms.Label();
            this.lblCreditCard = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblDebit = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblSubTotal.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSubTotal.Location = new System.Drawing.Point(684, 70);
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
            this.lblSubTotalName.Location = new System.Drawing.Point(527, 70);
            this.lblSubTotalName.Name = "lblSubTotalName";
            this.lblSubTotalName.Size = new System.Drawing.Size(99, 24);
            this.lblSubTotalName.TabIndex = 65;
            this.lblSubTotalName.Text = "SUBTOTAL";
            this.lblSubTotalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.dgItems);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 690);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click the diner` to pay or click the \'Amount Due\' to change diner`s share";
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.White;
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgItems.CaptionBackColor = System.Drawing.Color.White;
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.CausesValidation = false;
            this.dgItems.DataMember = "";
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.GridLineColor = System.Drawing.Color.Blue;
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(3, 17);
            this.dgItems.Name = "dgItems";
            this.dgItems.ParentRowsBackColor = System.Drawing.Color.Blue;
            this.dgItems.PreferredRowHeight = 100;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeaderWidth = 40;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(445, 670);
            this.dgItems.TabIndex = 50;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            this.dgItems.Navigate += new System.Windows.Forms.NavigateEventHandler(this.dgItems_Navigate);
            this.dgItems.Click += new System.EventHandler(this.dgItems_Click);
            this.dgItems.LostFocus += new System.EventHandler(this.dgItems_LostFocus);
            this.dgItems.MouseLeave += new System.EventHandler(this.dgItems_MouseLeave);
            this.dgItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgItems_MouseMove);
            // 
            // dgStyle
            // 
            this.dgStyle.AllowSorting = false;
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridLineColor = System.Drawing.Color.Blue;
            this.dgStyle.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 10.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblSplitPayment";
            this.dgStyle.PreferredColumnWidth = 0;
            this.dgStyle.PreferredRowHeight = 50;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
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
            this.label9.Location = new System.Drawing.Point(527, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 24);
            this.label9.TabIndex = 69;
            this.label9.Text = "(-) DISCOUNT";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscount.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDiscount.Location = new System.Drawing.Point(684, 171);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(229, 23);
            this.lblDiscount.TabIndex = 70;
            this.lblDiscount.Text = "0.00";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Location = new System.Drawing.Point(527, 294);
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
            this.lblBalanceName.Location = new System.Drawing.Point(518, 312);
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
            this.lblBalance.Location = new System.Drawing.Point(690, 314);
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
            this.lblAmountPaidName.Location = new System.Drawing.Point(527, 269);
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
            this.lblAmountPaid.Location = new System.Drawing.Point(684, 269);
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
            this.label15.ForeColor = System.Drawing.Color.Red;
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
            this.lblChangeName.Location = new System.Drawing.Point(518, 355);
            this.lblChangeName.Name = "lblChangeName";
            this.lblChangeName.Size = new System.Drawing.Size(167, 38);
            this.lblChangeName.TabIndex = 80;
            this.lblChangeName.Text = "CHANGE";
            this.lblChangeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChangeName.Visible = false;
            // 
            // lblChange
            // 
            this.lblChange.BackColor = System.Drawing.Color.Transparent;
            this.lblChange.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.Firebrick;
            this.lblChange.Location = new System.Drawing.Point(690, 355);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(229, 38);
            this.lblChange.TabIndex = 81;
            this.lblChange.Text = "0.00";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblChange.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(527, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 24);
            this.label1.TabIndex = 85;
            this.label1.Text = "(+) OTHER CHARGES ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCharge
            // 
            this.lblCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblCharge.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCharge.ForeColor = System.Drawing.Color.Firebrick;
            this.lblCharge.Location = new System.Drawing.Point(684, 204);
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
            this.label2.Location = new System.Drawing.Point(641, 70);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label3.Location = new System.Drawing.Point(641, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 98;
            this.label3.Text = "(Net of VAT)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(527, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 24);
            this.label11.TabIndex = 97;
            this.label11.Text = "A M O U N T";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountNetOfVAT
            // 
            this.lblAmountNetOfVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountNetOfVAT.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblAmountNetOfVAT.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAmountNetOfVAT.Location = new System.Drawing.Point(684, 137);
            this.lblAmountNetOfVAT.Name = "lblAmountNetOfVAT";
            this.lblAmountNetOfVAT.Size = new System.Drawing.Size(229, 25);
            this.lblAmountNetOfVAT.TabIndex = 96;
            this.lblAmountNetOfVAT.Text = "0.00";
            this.lblAmountNetOfVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbllSubtotalVATLabel
            // 
            this.lbllSubtotalVATLabel.AutoSize = true;
            this.lbllSubtotalVATLabel.BackColor = System.Drawing.Color.Transparent;
            this.lbllSubtotalVATLabel.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lbllSubtotalVATLabel.Location = new System.Drawing.Point(527, 99);
            this.lbllSubtotalVATLabel.Name = "lbllSubtotalVATLabel";
            this.lbllSubtotalVATLabel.Size = new System.Drawing.Size(42, 24);
            this.lbllSubtotalVATLabel.TabIndex = 100;
            this.lbllSubtotalVATLabel.Text = "VAT";
            this.lbllSubtotalVATLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubtotalVAT
            // 
            this.lblSubtotalVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalVAT.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalVAT.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSubtotalVAT.Location = new System.Drawing.Point(684, 99);
            this.lblSubtotalVAT.Name = "lblSubtotalVAT";
            this.lblSubtotalVAT.Size = new System.Drawing.Size(229, 25);
            this.lblSubtotalVAT.TabIndex = 99;
            this.lblSubtotalVAT.Text = "0.00";
            this.lblSubtotalVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Location = new System.Drawing.Point(527, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 10);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(527, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 102;
            this.label4.Text = "(+) VAT";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVAT
            // 
            this.lblVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblVAT.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblVAT.ForeColor = System.Drawing.Color.Firebrick;
            this.lblVAT.Location = new System.Drawing.Point(684, 236);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(229, 25);
            this.lblVAT.TabIndex = 101;
            this.lblVAT.Text = "0.00";
            this.lblVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscountType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscountType.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDiscountType.Location = new System.Drawing.Point(641, 170);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(97, 13);
            this.lblDiscountType.TabIndex = 103;
            this.lblDiscountType.Text = "(Discount Type)";
            this.lblDiscountType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.BackColor = System.Drawing.Color.Transparent;
            this.lblCash.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCash.Location = new System.Drawing.Point(495, 518);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(68, 24);
            this.lblCash.TabIndex = 104;
            this.lblCash.Text = "lblCash";
            this.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCash.Visible = false;
            // 
            // lblCheque
            // 
            this.lblCheque.AutoSize = true;
            this.lblCheque.BackColor = System.Drawing.Color.Transparent;
            this.lblCheque.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCheque.Location = new System.Drawing.Point(610, 518);
            this.lblCheque.Name = "lblCheque";
            this.lblCheque.Size = new System.Drawing.Size(88, 24);
            this.lblCheque.TabIndex = 105;
            this.lblCheque.Text = "lblCheque";
            this.lblCheque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCheque.Visible = false;
            // 
            // lblCreditCard
            // 
            this.lblCreditCard.AutoSize = true;
            this.lblCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCard.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCreditCard.Location = new System.Drawing.Point(758, 518);
            this.lblCreditCard.Name = "lblCreditCard";
            this.lblCreditCard.Size = new System.Drawing.Size(111, 24);
            this.lblCreditCard.TabIndex = 106;
            this.lblCreditCard.Text = "lblCreditCard";
            this.lblCreditCard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCreditCard.Visible = false;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.BackColor = System.Drawing.Color.Transparent;
            this.lblCredit.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblCredit.Location = new System.Drawing.Point(489, 588);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(74, 24);
            this.lblCredit.TabIndex = 107;
            this.lblCredit.Text = "lblCredit";
            this.lblCredit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCredit.Visible = false;
            // 
            // lblDebit
            // 
            this.lblDebit.AutoSize = true;
            this.lblDebit.BackColor = System.Drawing.Color.Transparent;
            this.lblDebit.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold);
            this.lblDebit.Location = new System.Drawing.Point(621, 588);
            this.lblDebit.Name = "lblDebit";
            this.lblDebit.Size = new System.Drawing.Size(68, 24);
            this.lblDebit.TabIndex = 108;
            this.lblDebit.Text = "lblDebit";
            this.lblDebit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDebit.Visible = false;
            // 
            // SplitPaymentAmountWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.lblDebit);
            this.Controls.Add(this.lblCredit);
            this.Controls.Add(this.lblCreditCard);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblCheque);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblDiscountType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblVAT);
            this.Controls.Add(this.lbllSubtotalVATLabel);
            this.Controls.Add(this.lblSubtotalVAT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAmountNetOfVAT);
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
            this.Name = "SplitPaymentAmountWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SplitPaymentAmountWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SplitPaymentAmountWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Windows Form Methods

        private void dgItems_Click(object sender, EventArgs e)
        {
            try { dgItems.Select(dgItems.CurrentRowIndex); }
            catch { }

            if (ItemDataTable.Rows.Count > 0)
            {
                decimal decTotalPayableAmount = Convert.ToDecimal((Convert.ToDecimal(lblSubTotal.Text) - Convert.ToDecimal(lblSubtotalVAT.Text)) - Convert.ToDecimal(lblDiscount.Text) + Convert.ToDecimal(lblCharge.Text) + Convert.ToDecimal(lblVAT.Text));
                decimal iDinner = Int32.Parse(dgItems[dgItems.CurrentRowIndex, 0].ToString());
                decimal decAmountDue = decimal.Parse(dgItems[dgItems.CurrentRowIndex, 1].ToString());
                decimal decAmountPaid = decimal.Parse(dgItems[dgItems.CurrentRowIndex, 2].ToString());
                decimal decBalance = decimal.Parse(dgItems[dgItems.CurrentRowIndex, 3].ToString());
                decimal decChangeAmount = decimal.Parse(dgItems[dgItems.CurrentRowIndex, 4].ToString());

                if (dgItems.CurrentCell.ColumnNumber == 1) // AmountDue is clicked
                {
                    //if (ShowNoControl(this, out decRetValue, decimal.Parse("1"), "Enter no. of diners to pay.") == DialogResult.Cancel)
                Back:
                    NoControl clsNoControl = new NoControl();
                    clsNoControl.Caption = "Enter the share amount of diner` " + iDinner.ToString();
                    clsNoControl.NoValue = decAmountDue;
                    clsNoControl.ShowDialog(this);
                    decimal decNewAmountDue = decimal.TryParse(clsNoControl.NoValue.ToString(), out decNewAmountDue) ? decNewAmountDue : 0;
                    DialogResult result = clsNoControl.Result;
                    clsNoControl.Close();
                    clsNoControl.Dispose();

                    if (result == DialogResult.OK)
                    {
                        if (decNewAmountDue > decTotalPayableAmount)
                        {
                            MessageBox.Show("Sorry the amount you entered is higher than the total payable amount of " + decTotalPayableAmount.ToString("#,##0.#0") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto Back;
                        }
                        if (decAmountDue != decNewAmountDue) // this means that something has changed
                        {
                            Int32 iCurrentRowIndex = dgItems.CurrentRowIndex;

                            decimal decTotalAmountDue = decNewAmountDue;

                            // set the changes in the currentrowindex
                            setCurrentRowIndex(iCurrentRowIndex, decNewAmountDue, decAmountPaid);

                            // set the previous, DO NOT reset the decTotalAmountDue
                            for (Int32 iCtr = iCurrentRowIndex - 1; iCtr > 0; iCtr--)
                            {
                                iDinner = Int32.Parse(dgItems[iCtr, 0].ToString());
                                decAmountDue = decimal.Parse(dgItems[iCtr, 1].ToString());
                                decAmountPaid = decimal.Parse(dgItems[iCtr, 2].ToString());

                                // AmountDue is still covered -- do not do anything
                                if ((decTotalPayableAmount - decTotalAmountDue) >= decAmountDue)
                                    decAmountDue = decimal.Parse(dgItems[iCtr, 1].ToString());
                                // AmountDue is not covered anymore but with remaining amount
                                else if ((decTotalPayableAmount - decTotalAmountDue) > 0)
                                {
                                    decAmountDue = decTotalPayableAmount - decTotalAmountDue;
                                    decAmountPaid = 0;
                                    // reset the if decAmountPaid is changed
                                    marrSplitPaymentDetails[iCtr] = new Data.SplitPaymentDetails();
                                    setCurrentRowIndex(iCtr, decAmountDue, decAmountPaid);
                                }
                                // AmountDue is not covered anymore
                                else if ((decTotalPayableAmount - decTotalAmountDue) <= 0)
                                {
                                    decAmountDue = 0;
                                    decAmountPaid = 0;
                                    // reset the if decAmountPaid is changed
                                    marrSplitPaymentDetails[iCtr] = new Data.SplitPaymentDetails();
                                    setCurrentRowIndex(iCtr, decAmountDue, decAmountPaid);
                                }

                                decTotalAmountDue += decAmountDue;
                            }

                            // set the next, DO NOT reset the decTotalAmountDue
                            for (Int32 iCtr = iCurrentRowIndex + 1; iCtr < ItemDataTable.Rows.Count; iCtr++)
                            {
                                iDinner = Int32.Parse(dgItems[iCtr, 0].ToString());
                                decAmountDue = decimal.Parse(dgItems[iCtr, 1].ToString());
                                decAmountPaid = decimal.Parse(dgItems[iCtr, 2].ToString());

                                // AmountDue is still covered -- do not do anything
                                if ((decTotalPayableAmount - decTotalAmountDue) >= decAmountDue)
                                    decAmountDue = decimal.Parse(dgItems[iCtr, 1].ToString());
                                // AmountDue is not covered anymore but with remaining amount
                                else if ((decTotalPayableAmount - decTotalAmountDue) > 0)
                                {
                                    decAmountDue = decTotalPayableAmount - decTotalAmountDue;
                                    decAmountPaid = 0;
                                    // reset the if decAmountPaid is changed
                                    marrSplitPaymentDetails[iCtr] = new Data.SplitPaymentDetails();
                                    setCurrentRowIndex(iCtr, decAmountDue, decAmountPaid);
                                }
                                // AmountDue is not covered anymore
                                else if ((decTotalPayableAmount - decTotalAmountDue) <= 0)
                                {
                                    decAmountDue = 0;
                                    decAmountPaid = 0;
                                    // reset the if decAmountPaid is changed
                                    marrSplitPaymentDetails[iCtr] = new Data.SplitPaymentDetails();
                                    setCurrentRowIndex(iCtr, decAmountDue, decAmountPaid);
                                }

                                decTotalAmountDue += decAmountDue;
                            }

                            // set the remaining balance to the first payee
                            if (iCurrentRowIndex != 0)
                            {
                                decAmountDue = decimal.Parse(dgItems[0, 1].ToString());
                                decAmountPaid = decimal.Parse(dgItems[0, 2].ToString());
                                if ((decTotalPayableAmount - decTotalAmountDue) >= 0)
                                {
                                    decAmountDue = decTotalPayableAmount - decTotalAmountDue;
                                    setCurrentRowIndex(0, decAmountDue, decAmountPaid);

                                    decAmountPaid = 0;
                                    // reset the if decAmountPaid is changed
                                    marrSplitPaymentDetails[0] = new Data.SplitPaymentDetails();
                                }
                            }

                            lblAmountPaid.Text = "0.00";
                            ComputePayments();
                        }
                    }
                    return;
                }
                //insert payment details
                Data.SalesTransactionDetails clsSalesTransactionDetails = mclsSalesTransactionDetails;

                decimal decSplitPercentage = decAmountDue / decTotalPayableAmount;
                clsSalesTransactionDetails.CustomerDetails = new Data.ContactDetails();
                clsSalesTransactionDetails.SubTotal = mclsSalesTransactionDetails.SubTotal * decSplitPercentage;
                clsSalesTransactionDetails.DiscountableAmount = mclsSalesTransactionDetails.DiscountableAmount * decSplitPercentage;
                clsSalesTransactionDetails.Discount = mclsSalesTransactionDetails.Discount * decSplitPercentage;
                clsSalesTransactionDetails.Charge = mclsSalesTransactionDetails.Charge * decSplitPercentage;
                clsSalesTransactionDetails.VAT = mclsSalesTransactionDetails.VAT * decSplitPercentage;
                clsSalesTransactionDetails.VATableAmount = mclsSalesTransactionDetails.VATableAmount * decSplitPercentage;
                clsSalesTransactionDetails.EVAT = mclsSalesTransactionDetails.EVAT * decSplitPercentage;
                clsSalesTransactionDetails.EVATableAmount = mclsSalesTransactionDetails.EVATableAmount * decSplitPercentage;

                PaymentsWnd payment = new PaymentsWnd();
                payment.TerminalDetails = mclsTerminalDetails;
                payment.SysConfigDetails = mclsSysConfigDetails;
                payment.CustomerDetails = mclsCustomerDetails;
                payment.SalesTransactionDetails = clsSalesTransactionDetails;
                payment.CreditCardSwiped = mboCreditCardSwiped;
                payment.IsRefund = mboIsRefund;
                payment.IsCreditChargeExcluded = mboIsCreditChargeExcluded;

                payment.ShowDialog(this);

                DialogResult paymentResult = payment.Result;

                decimal AmountPaid = payment.AmountPaid;
                decimal CashPayment = payment.CashPayment;
                decimal ChequePayment = payment.ChequePayment;
                decimal CreditCardPayment = payment.CreditCardPayment;
                decimal CreditPayment = payment.CreditPayment;
                decimal DebitPayment = payment.DebitPayment;
                decimal CreditChargeAmount = payment.SalesTransactionDetails.CreditChargeAmount;

                decimal BalanceAmount = payment.BalanceAmount;
                decimal ChangeAmount = payment.ChangeAmount;
                PaymentTypes PaymentType = payment.PaymentType;
                ArrayList arrCashPaymentDetails = payment.CashPaymentDetails;
                ArrayList arrChequePaymentDetails = payment.ChequePaymentDetails;
                ArrayList arrCreditCardPaymentDetails = payment.CreditCardPaymentDetails;
                ArrayList arrCreditPaymentDetails = payment.CreditPaymentDetails;
                ArrayList arrDebitPaymentDetails = payment.DebitPaymentDetails;
                decimal RewardPointsPayment = payment.RewardPointsPayment;
                decimal RewardConvertedPayment = payment.RewardConvertedPayment;
                Data.ContactDetails clsCreditorDetails = payment.CreditorDetails;
                payment.Close();
                payment.Dispose();

                if (paymentResult == DialogResult.OK)
                {
                    ItemDataTable.Rows[dgItems.CurrentRowIndex][2] = AmountPaid.ToString("#,##0.#0");
                    ItemDataTable.Rows[dgItems.CurrentRowIndex][3] = "0.00";
                    ItemDataTable.Rows[dgItems.CurrentRowIndex][4] = ChangeAmount.ToString("#,##0.#0");

                    decAmountPaid = 0;
                    foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                    {
                        decAmountPaid += decimal.Parse(dr["AmountPaid"].ToString()) - decimal.Parse(dr["Change"].ToString());
                    }

                    lblAmountPaid.Text = decAmountPaid.ToString("#,##0.#0");
                    ComputePayments();

                    Data.SplitPaymentDetails clsSplitPaymentDetails = new Data.SplitPaymentDetails();

                    clsSplitPaymentDetails.AmountDue = clsSalesTransactionDetails.SubTotal - clsSalesTransactionDetails.Discount + clsSalesTransactionDetails.Charge; ;
                    clsSplitPaymentDetails.AmountPaid = AmountPaid;
                    clsSplitPaymentDetails.CashPayment = CashPayment;
                    clsSplitPaymentDetails.ChequePayment = ChequePayment;
                    clsSplitPaymentDetails.CreditCardPayment = CreditCardPayment;
                    clsSplitPaymentDetails.CreditPayment = CreditPayment;
                    clsSplitPaymentDetails.DebitPayment = DebitPayment;
                    clsSplitPaymentDetails.CreditChargeAmount = CreditChargeAmount;

                    clsSplitPaymentDetails.BalanceAmount = BalanceAmount;
                    clsSplitPaymentDetails.ChangeAmount = ChangeAmount;
                    clsSplitPaymentDetails.PaymentType = PaymentType;
                    clsSplitPaymentDetails.arrCashPaymentDetails = arrCashPaymentDetails;
                    clsSplitPaymentDetails.arrChequePaymentDetails = arrChequePaymentDetails;
                    clsSplitPaymentDetails.arrCreditCardPaymentDetails = arrCreditCardPaymentDetails;
                    clsSplitPaymentDetails.arrCreditPaymentDetails = arrCreditPaymentDetails;
                    clsSplitPaymentDetails.arrDebitPaymentDetails = arrDebitPaymentDetails;
                    clsSplitPaymentDetails.RewardPointsPayment = RewardPointsPayment;
                    clsSplitPaymentDetails.RewardConvertedPayment = RewardConvertedPayment;
                    clsSplitPaymentDetails.clsCreditorDetails = clsCreditorDetails;

                    marrSplitPaymentDetails[dgItems.CurrentRowIndex] = clsSplitPaymentDetails;

                }
            }
        }

        private void setCurrentRowIndex(int RowIndex, decimal decAmountDue, decimal decAmountPaid)
        {
            ItemDataTable.Rows[RowIndex][1] = decAmountDue.ToString("#,##0.#0");
            ItemDataTable.Rows[RowIndex][2] = decAmountPaid.ToString("#,##0.#0");

            if (decAmountPaid == 0)
            {
                ItemDataTable.Rows[RowIndex][3] = decAmountDue.ToString("#,##0.#0");
                ItemDataTable.Rows[RowIndex][4] = "0.00";
            }
            else if (decAmountDue <= decAmountPaid)
            {
                ItemDataTable.Rows[RowIndex][3] = "0.00";
                ItemDataTable.Rows[RowIndex][4] = (decAmountDue - decAmountPaid).ToString("#,##0.#0"); ;
            }
            else
            {
                ItemDataTable.Rows[RowIndex][3] = (decAmountDue - decAmountPaid).ToString("#,##0.#0"); ;
                ItemDataTable.Rows[RowIndex][4] = "0.00";
            }
        }

		private void dgItems_Navigate(object sender, NavigateEventArgs ne)
		{
            try { dgItems.Select(dgItems.CurrentRowIndex); }
            catch { }
		}
        private void dgItems_LostFocus(object sender, System.EventArgs e)
        {
            try { dgItems.Select(dgItems.CurrentRowIndex); }
            catch { }
        }
        private void dgItems_MouseMove(object sender, MouseEventArgs e)
        {
            try { dgItems.Select(dgItems.CurrentRowIndex); }
            catch { }
        }
        private void dgItems_MouseLeave(object sender, EventArgs e)
        {
            try { dgItems.Select(dgItems.CurrentRowIndex); }
            catch { }
        }

		private void SplitPaymentAmountWnd_Load(object sender, System.EventArgs e)
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
                        
            if (mboIsRefund)
            {
                lblHeader.Text = "Enter payment types to refund.";
                lblSubTotalName.Text = "REFUND";
                lblAmountPaidName.Text = "REFUNDED AMT";
                lblChangeName.Text = "OVER";
            }

            switch (mclsSalesTransactionDetails.TransDiscountType)
            {
                case DiscountTypes.NotApplicable:
                    lblDiscountType.Text = "";
                    break;
                case DiscountTypes.FixedValue:
                    lblDiscountType.Text = "(" + mclsSalesTransactionDetails.DiscountCode + " : " + mclsSalesTransactionDetails.TransDiscount.ToString() + ")";
                    break;
                case DiscountTypes.Percentage:
                    lblDiscountType.Text = "(" + mclsSalesTransactionDetails.DiscountCode + " : " + mclsSalesTransactionDetails.TransDiscount.ToString() + "%)";
                    break;
            }

            lblSubTotal.Text = mclsSalesTransactionDetails.SubTotal.ToString("#,##0.#0");
            if ((mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode) && mclsSalesTransactionDetails.DiscountableAmount !=0)
            {
                // recompute coz VAT is zero
                lblSubtotalVAT.Text = ((mclsSalesTransactionDetails.DiscountableAmount / (1 + (mclsTerminalDetails.VAT / 100))) * (mclsTerminalDetails.VAT / 100)).ToString("#,##0.#0");
            }
            else
            {
                lblSubtotalVAT.Text = mclsSalesTransactionDetails.VAT.ToString("#,##0.#0");
            }
            lblAmountNetOfVAT.Text = (mclsSalesTransactionDetails.SubTotal - decimal.Parse(lblSubtotalVAT.Text)).ToString("#,##0.#0");
			lblDiscount.Text = mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
			lblCharge.Text = mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            lblVAT.Text = mclsSalesTransactionDetails.VAT.ToString("#,##0.#0");
            lblAmountPaid.Text = "0.00";

            ComputePayments();

            LoadData();

            marrSplitPaymentDetails = new Data.SplitPaymentDetails[NoOfDiners];
		}

		private void SplitPaymentAmountWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
                case Keys.Up:
                    MoveItemUp();
                    break;

                case Keys.Down:
                    MoveItemDown();
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
                    }
                    else
                    {
                        dgItems_Click(null, null);
                    }
                    break;
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
                dgItems_Click(null, null);
            }
        }
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        
        #endregion

        #region Computation

        private void ComputePayments()
		{
            decimal Balance = Convert.ToDecimal((Convert.ToDecimal(lblSubTotal.Text) - Convert.ToDecimal(lblSubtotalVAT.Text)) - Convert.ToDecimal(lblDiscount.Text) + Convert.ToDecimal(lblCharge.Text) + Convert.ToDecimal(lblVAT.Text) - Convert.ToDecimal(lblAmountPaid.Text));
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

        private void LoadData()
        {
            ItemDataTable.Columns.Add("Diner");
            ItemDataTable.Columns.Add("AmountDue");
            ItemDataTable.Columns.Add("AmountPaid");
            ItemDataTable.Columns.Add("Balance");
            ItemDataTable.Columns.Add("Change");

            decimal Balance = mclsSalesTransactionDetails.SubTotal - mclsSalesTransactionDetails.Discount + mclsSalesTransactionDetails.Charge;
            ItemDataTable.Rows.Add("1", Balance.ToString("#,##0.#0"), "0.00", Balance.ToString("#,##0.#0"), "0.00");

            for (Int32 iCtr = 2; iCtr < NoOfDiners + 1; iCtr++)
            {
                ItemDataTable.Rows.Add(iCtr.ToString(), "0.00", "0.00", "0.00", "0.00");
            }

            this.dgStyle.MappingName = ItemDataTable.TableName;
            dgItems.DataSource = ItemDataTable;
            dgItems.Select(0);

            dgStyle.GridColumnStyles["Diner"].HeaderText = "Dinner";
            dgStyle.GridColumnStyles["AmountDue"].HeaderText = "Amt. Due";
            dgStyle.GridColumnStyles["AmountPaid"].HeaderText = "Amt. Paid";
            dgStyle.GridColumnStyles["Balance"].HeaderText = "Balance";
            dgStyle.GridColumnStyles["Change"].HeaderText = "Change ";

            dgStyle.GridColumnStyles["Diner"].Width = 60;
            dgStyle.GridColumnStyles["AmountDue"].Width = (dgItems.Width - 60) / 4;
            dgStyle.GridColumnStyles["AmountPaid"].Width = (dgItems.Width - 60) / 4;
            dgStyle.GridColumnStyles["Balance"].Width = (dgItems.Width - 60) / 4;
            dgStyle.GridColumnStyles["Change"].Width = (dgItems.Width - 60) / 4;
            dgStyle.GridColumnStyles["Diner"].Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            dgStyle.GridColumnStyles["AmountDue"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            dgStyle.GridColumnStyles["AmountPaid"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            dgStyle.GridColumnStyles["Balance"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            dgStyle.GridColumnStyles["Change"].Alignment = System.Windows.Forms.HorizontalAlignment.Right;

        }

        #endregion

        #region Private Modifiers
        
		private void SendStringToTurret(string szString)
		{
			RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.TurretName, "\f" + szString, "RetailPlus Turret Disp: ");
		}

        private void MoveItemUp()
        {
            if (ItemDataTable.Rows.Count > 0 && dgItems.CurrentRowIndex != 0)
            {
                int oldindex = dgItems.CurrentRowIndex;
                dgItems.CurrentRowIndex -= 1;
                try { dgItems.UnSelect(0); }
                catch { }
                dgItems.UnSelect(oldindex);
                dgItems.Select(dgItems.CurrentRowIndex);
            }
        }

        private void MoveItemDown()
        {
            if (dgItems.CurrentRowIndex + 1 < ItemDataTable.Rows.Count && dgItems.CurrentRowIndex + 1 != ItemDataTable.Rows.Count)
            {
                int oldindex = dgItems.CurrentRowIndex;

                dgItems.CurrentRowIndex += 1;
                try { dgItems.UnSelect(0); }
                catch { }
                dgItems.UnSelect(oldindex);
                dgItems.Select(dgItems.CurrentRowIndex);
            }
        }

		#endregion

		#region Assign Values when OK

		private void AssignValues()
		{
            ////mdecSubTotal = Convert.ToDecimal(lblSubTotal.Text);
            ////mdecDiscount = Convert.ToDecimal(lblDiscount.Text);
            ////mdecCharge = Convert.ToDecimal(lblChange.Text);

            //mdecAmountPaid = Convert.ToDecimal(lblAmountPaid.Text);
            //mdecCashPayment = Convert.ToDecimal(lblCash.Tag);
            //mdecChequePayment = Convert.ToDecimal(lblCheque.Tag);
            //mdecCreditCardPayment = Convert.ToDecimal(lblCreditCard.Tag);
            //mdecCreditPayment = Convert.ToDecimal(lblCredit.Tag);
            //mdecDebitPayment = Convert.ToDecimal(lblDebit.Tag);
            //mdecBalanceAmount = Convert.ToDecimal(lblBalance.Text);
            //mdecChangeAmount = Convert.ToDecimal(lblChange.Text);

            //// mPaymentType = mPaymentType;
		}

		#endregion

	}
}