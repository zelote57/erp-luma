using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for CashierReportWnd.
	/// </summary>
	public class CashierReportWnd : System.Windows.Forms.Form
	{
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DialogResult dialog;
        private Data.CashierReportDetails mDetails;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PictureBox imgIcon;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblReportDesc;
		private System.Windows.Forms.Panel panReport;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.Label lblNoOfDebitPaymentTransactions;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblNoOfCreditPaymentTran;
		private System.Windows.Forms.Label lblNoOfCreditPaymentTran1;
		private System.Windows.Forms.Label lblNoOfCreditTran;
		private System.Windows.Forms.Label lblNoOfCreditTran1;
		private System.Windows.Forms.Label lblNoOfVoidTransactions;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label lblNoOfRefundTransactions;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label lblNoOfCombPayTran;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lblNoOfCreditCardTran;
		private System.Windows.Forms.Label lblNoOfCreditCardTran1;
		private System.Windows.Forms.Label lblNoOfChequeTransactions;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label lblNoOfCashTransactions;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label71;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label58;
		private System.Windows.Forms.Label lblBeginningBalance;
		private System.Windows.Forms.Label lblBeginningBalance1;
		private System.Windows.Forms.Label label50;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label lblTotalDiscounts;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label lblSubtotalDiscount;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label lblItemsDiscount;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.Label lblRefundSales;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label lblVoidSales;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label lblDebitPayment;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lblCreditPayment;
		private System.Windows.Forms.Label lblCreditPayment1;
		private System.Windows.Forms.Label lblCreditSales;
		private System.Windows.Forms.Label lblCreditSales1;
		private System.Windows.Forms.Label lblCreditCardSales;
		private System.Windows.Forms.Label lblCreditCardSales1;
		private System.Windows.Forms.Label lblChequeSales;
		private System.Windows.Forms.Label lblChequeSales1;
		private System.Windows.Forms.Label lblCashSales;
		private System.Windows.Forms.Label lblCashSales1;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label43;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label lblLocalTax;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.Label lblVAT1;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label lblTotalAmount;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label lblCharge;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label lblVAT;
		private System.Windows.Forms.Label label75;
		private System.Windows.Forms.Label lblNetSales;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label lblCreditCardDeposit;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblChequeDeposit;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblCashDeposit;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label lblPaidOut;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblCreditCardWithHold;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label lblChequeWithHold;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label lblCashWithHold;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label lblCreditCardDisburse;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.Label lblChequeDisburse;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.Label lblCashDisburse;
		private System.Windows.Forms.Label lblCashDisburse1;
		private System.Windows.Forms.Label lblCashInDrawer;
		private System.Windows.Forms.Label lblCashInDrawer1;
		private System.Windows.Forms.Label lblTotalDiscount;
		private System.Windows.Forms.Label label77;
		private System.Windows.Forms.Label lblGrossSales;
		private System.Windows.Forms.Label label79;
		private System.Windows.Forms.Label lblPanelTop;
		private System.Windows.Forms.Label lblReportFooter3;
		private System.Windows.Forms.Label lblReportFooter2;
		private System.Windows.Forms.Label lblReportFooter1;
		private System.Windows.Forms.Label lblPanelBot;
		private System.Windows.Forms.Label lblNoOfTotalTransactions;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label lblReceiptDesc;
		private System.Windows.Forms.Label lblReportHeader4;
		private System.Windows.Forms.Label lblReportHeader3;
		private System.Windows.Forms.Label lblReportHeader2;
		private System.Windows.Forms.Label lblReportHeader1;
		private System.Windows.Forms.Label lblCompany;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblShortOver;
		private System.Windows.Forms.Label lblCashCount;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblShortOverName;
        private Button cmdCancel;
        private Button cmdEnter;
		private string mCashierName;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

		public Data.CashierReportDetails Details
		{
			set 
			{	mDetails	=	value;	}
		}

		public string CashierName
		{
			set
			{	mCashierName	=	value;	}
        }

        #region Constructors and Destructors

        public CashierReportWnd()
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

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panReport = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblShortOver = new System.Windows.Forms.Label();
            this.lblShortOverName = new System.Windows.Forms.Label();
            this.lblCashCount = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.lblNoOfDebitPaymentTransactions = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNoOfCreditPaymentTran = new System.Windows.Forms.Label();
            this.lblNoOfCreditPaymentTran1 = new System.Windows.Forms.Label();
            this.lblNoOfCreditTran = new System.Windows.Forms.Label();
            this.lblNoOfCreditTran1 = new System.Windows.Forms.Label();
            this.lblNoOfVoidTransactions = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblNoOfRefundTransactions = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblNoOfCombPayTran = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblNoOfCreditCardTran = new System.Windows.Forms.Label();
            this.lblNoOfCreditCardTran1 = new System.Windows.Forms.Label();
            this.lblNoOfChequeTransactions = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblNoOfCashTransactions = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.lblBeginningBalance = new System.Windows.Forms.Label();
            this.lblBeginningBalance1 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblTotalDiscounts = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblSubtotalDiscount = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblItemsDiscount = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblRefundSales = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblVoidSales = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblDebitPayment = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCreditPayment = new System.Windows.Forms.Label();
            this.lblCreditPayment1 = new System.Windows.Forms.Label();
            this.lblCreditSales = new System.Windows.Forms.Label();
            this.lblCreditSales1 = new System.Windows.Forms.Label();
            this.lblCreditCardSales = new System.Windows.Forms.Label();
            this.lblCreditCardSales1 = new System.Windows.Forms.Label();
            this.lblChequeSales = new System.Windows.Forms.Label();
            this.lblChequeSales1 = new System.Windows.Forms.Label();
            this.lblCashSales = new System.Windows.Forms.Label();
            this.lblCashSales1 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblLocalTax = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lblVAT1 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblCharge = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.lblNetSales = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblCreditCardDeposit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblChequeDeposit = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCashDeposit = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPaidOut = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCreditCardWithHold = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblChequeWithHold = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblCashWithHold = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.lblCreditCardDisburse = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lblChequeDisburse = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.lblCashDisburse = new System.Windows.Forms.Label();
            this.lblCashDisburse1 = new System.Windows.Forms.Label();
            this.lblCashInDrawer = new System.Windows.Forms.Label();
            this.lblCashInDrawer1 = new System.Windows.Forms.Label();
            this.lblTotalDiscount = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.lblGrossSales = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.lblPanelTop = new System.Windows.Forms.Label();
            this.lblReportFooter3 = new System.Windows.Forms.Label();
            this.lblReportFooter2 = new System.Windows.Forms.Label();
            this.lblReportFooter1 = new System.Windows.Forms.Label();
            this.lblPanelBot = new System.Windows.Forms.Label();
            this.lblNoOfTotalTransactions = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblReceiptDesc = new System.Windows.Forms.Label();
            this.lblReportHeader4 = new System.Windows.Forms.Label();
            this.lblReportHeader3 = new System.Windows.Forms.Label();
            this.lblReportHeader2 = new System.Windows.Forms.Label();
            this.lblReportHeader1 = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 51;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
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
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cashier Report Details";
            // 
            // panReport
            // 
            this.panReport.AutoScroll = true;
            this.panReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panReport.Controls.Add(this.label7);
            this.panReport.Controls.Add(this.lblShortOver);
            this.panReport.Controls.Add(this.lblShortOverName);
            this.panReport.Controls.Add(this.lblCashCount);
            this.panReport.Controls.Add(this.label24);
            this.panReport.Controls.Add(this.label1);
            this.panReport.Controls.Add(this.label3);
            this.panReport.Controls.Add(this.label4);
            this.panReport.Controls.Add(this.label74);
            this.panReport.Controls.Add(this.lblNoOfDebitPaymentTransactions);
            this.panReport.Controls.Add(this.label10);
            this.panReport.Controls.Add(this.lblNoOfCreditPaymentTran);
            this.panReport.Controls.Add(this.lblNoOfCreditPaymentTran1);
            this.panReport.Controls.Add(this.lblNoOfCreditTran);
            this.panReport.Controls.Add(this.lblNoOfCreditTran1);
            this.panReport.Controls.Add(this.lblNoOfVoidTransactions);
            this.panReport.Controls.Add(this.label33);
            this.panReport.Controls.Add(this.lblNoOfRefundTransactions);
            this.panReport.Controls.Add(this.label37);
            this.panReport.Controls.Add(this.lblNoOfCombPayTran);
            this.panReport.Controls.Add(this.label18);
            this.panReport.Controls.Add(this.lblNoOfCreditCardTran);
            this.panReport.Controls.Add(this.lblNoOfCreditCardTran1);
            this.panReport.Controls.Add(this.lblNoOfChequeTransactions);
            this.panReport.Controls.Add(this.label14);
            this.panReport.Controls.Add(this.lblNoOfCashTransactions);
            this.panReport.Controls.Add(this.label16);
            this.panReport.Controls.Add(this.label22);
            this.panReport.Controls.Add(this.label71);
            this.panReport.Controls.Add(this.label72);
            this.panReport.Controls.Add(this.label57);
            this.panReport.Controls.Add(this.label65);
            this.panReport.Controls.Add(this.label70);
            this.panReport.Controls.Add(this.label64);
            this.panReport.Controls.Add(this.label66);
            this.panReport.Controls.Add(this.label68);
            this.panReport.Controls.Add(this.label60);
            this.panReport.Controls.Add(this.label62);
            this.panReport.Controls.Add(this.label63);
            this.panReport.Controls.Add(this.label54);
            this.panReport.Controls.Add(this.label56);
            this.panReport.Controls.Add(this.label58);
            this.panReport.Controls.Add(this.lblBeginningBalance);
            this.panReport.Controls.Add(this.lblBeginningBalance1);
            this.panReport.Controls.Add(this.label50);
            this.panReport.Controls.Add(this.label52);
            this.panReport.Controls.Add(this.label53);
            this.panReport.Controls.Add(this.lblTotalDiscounts);
            this.panReport.Controls.Add(this.label49);
            this.panReport.Controls.Add(this.label46);
            this.panReport.Controls.Add(this.lblSubtotalDiscount);
            this.panReport.Controls.Add(this.label21);
            this.panReport.Controls.Add(this.lblItemsDiscount);
            this.panReport.Controls.Add(this.label69);
            this.panReport.Controls.Add(this.label13);
            this.panReport.Controls.Add(this.label44);
            this.panReport.Controls.Add(this.label45);
            this.panReport.Controls.Add(this.lblRefundSales);
            this.panReport.Controls.Add(this.label19);
            this.panReport.Controls.Add(this.lblVoidSales);
            this.panReport.Controls.Add(this.label15);
            this.panReport.Controls.Add(this.lblDebitPayment);
            this.panReport.Controls.Add(this.label8);
            this.panReport.Controls.Add(this.lblCreditPayment);
            this.panReport.Controls.Add(this.lblCreditPayment1);
            this.panReport.Controls.Add(this.lblCreditSales);
            this.panReport.Controls.Add(this.lblCreditSales1);
            this.panReport.Controls.Add(this.lblCreditCardSales);
            this.panReport.Controls.Add(this.lblCreditCardSales1);
            this.panReport.Controls.Add(this.lblChequeSales);
            this.panReport.Controls.Add(this.lblChequeSales1);
            this.panReport.Controls.Add(this.lblCashSales);
            this.panReport.Controls.Add(this.lblCashSales1);
            this.panReport.Controls.Add(this.label42);
            this.panReport.Controls.Add(this.label43);
            this.panReport.Controls.Add(this.label41);
            this.panReport.Controls.Add(this.lblLocalTax);
            this.panReport.Controls.Add(this.label73);
            this.panReport.Controls.Add(this.lblVAT1);
            this.panReport.Controls.Add(this.label40);
            this.panReport.Controls.Add(this.label38);
            this.panReport.Controls.Add(this.label36);
            this.panReport.Controls.Add(this.label32);
            this.panReport.Controls.Add(this.lblTotalAmount);
            this.panReport.Controls.Add(this.label31);
            this.panReport.Controls.Add(this.label29);
            this.panReport.Controls.Add(this.lblCharge);
            this.panReport.Controls.Add(this.label27);
            this.panReport.Controls.Add(this.lblVAT);
            this.panReport.Controls.Add(this.label75);
            this.panReport.Controls.Add(this.lblNetSales);
            this.panReport.Controls.Add(this.label25);
            this.panReport.Controls.Add(this.label23);
            this.panReport.Controls.Add(this.lblCreditCardDeposit);
            this.panReport.Controls.Add(this.label5);
            this.panReport.Controls.Add(this.lblChequeDeposit);
            this.panReport.Controls.Add(this.label9);
            this.panReport.Controls.Add(this.lblCashDeposit);
            this.panReport.Controls.Add(this.label17);
            this.panReport.Controls.Add(this.lblPaidOut);
            this.panReport.Controls.Add(this.label6);
            this.panReport.Controls.Add(this.lblCreditCardWithHold);
            this.panReport.Controls.Add(this.label47);
            this.panReport.Controls.Add(this.lblChequeWithHold);
            this.panReport.Controls.Add(this.label51);
            this.panReport.Controls.Add(this.lblCashWithHold);
            this.panReport.Controls.Add(this.label55);
            this.panReport.Controls.Add(this.lblCreditCardDisburse);
            this.panReport.Controls.Add(this.label59);
            this.panReport.Controls.Add(this.lblChequeDisburse);
            this.panReport.Controls.Add(this.label61);
            this.panReport.Controls.Add(this.lblCashDisburse);
            this.panReport.Controls.Add(this.lblCashDisburse1);
            this.panReport.Controls.Add(this.lblCashInDrawer);
            this.panReport.Controls.Add(this.lblCashInDrawer1);
            this.panReport.Controls.Add(this.lblTotalDiscount);
            this.panReport.Controls.Add(this.label77);
            this.panReport.Controls.Add(this.lblGrossSales);
            this.panReport.Controls.Add(this.label79);
            this.panReport.Controls.Add(this.lblPanelTop);
            this.panReport.Controls.Add(this.lblReportFooter3);
            this.panReport.Controls.Add(this.lblReportFooter2);
            this.panReport.Controls.Add(this.lblReportFooter1);
            this.panReport.Controls.Add(this.lblPanelBot);
            this.panReport.Controls.Add(this.lblNoOfTotalTransactions);
            this.panReport.Controls.Add(this.label28);
            this.panReport.Controls.Add(this.lblReceiptDesc);
            this.panReport.Controls.Add(this.lblReportHeader4);
            this.panReport.Controls.Add(this.lblReportHeader3);
            this.panReport.Controls.Add(this.lblReportHeader2);
            this.panReport.Controls.Add(this.lblReportHeader1);
            this.panReport.Controls.Add(this.lblCompany);
            this.panReport.Controls.Add(this.label20);
            this.panReport.Controls.Add(this.label2);
            this.panReport.Location = new System.Drawing.Point(291, 12);
            this.panReport.Name = "panReport";
            this.panReport.Size = new System.Drawing.Size(385, 515);
            this.panReport.TabIndex = 109;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label7.Location = new System.Drawing.Point(9, 1174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(353, 5);
            this.label7.TabIndex = 426;
            this.label7.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShortOver
            // 
            this.lblShortOver.BackColor = System.Drawing.Color.Transparent;
            this.lblShortOver.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortOver.ForeColor = System.Drawing.Color.Red;
            this.lblShortOver.Location = new System.Drawing.Point(176, 1158);
            this.lblShortOver.Name = "lblShortOver";
            this.lblShortOver.Size = new System.Drawing.Size(174, 14);
            this.lblShortOver.TabIndex = 425;
            this.lblShortOver.Text = "0.00";
            this.lblShortOver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblShortOverName
            // 
            this.lblShortOverName.Location = new System.Drawing.Point(16, 1158);
            this.lblShortOverName.Name = "lblShortOverName";
            this.lblShortOverName.Size = new System.Drawing.Size(137, 14);
            this.lblShortOverName.TabIndex = 424;
            this.lblShortOverName.Text = "Short / Over";
            // 
            // lblCashCount
            // 
            this.lblCashCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCashCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashCount.ForeColor = System.Drawing.Color.Red;
            this.lblCashCount.Location = new System.Drawing.Point(176, 1142);
            this.lblCashCount.Name = "lblCashCount";
            this.lblCashCount.Size = new System.Drawing.Size(174, 14);
            this.lblCashCount.TabIndex = 423;
            this.lblCashCount.Text = "0.00";
            this.lblCashCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(16, 1142);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(137, 14);
            this.label24.TabIndex = 422;
            this.label24.Text = "Cash Count";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 1118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 14);
            this.label1.TabIndex = 421;
            this.label1.Text = "Cash Count";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label3.Location = new System.Drawing.Point(9, 1134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 5);
            this.label3.TabIndex = 420;
            this.label3.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label4.Location = new System.Drawing.Point(9, 1110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 5);
            this.label4.TabIndex = 419;
            this.label4.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label74
            // 
            this.label74.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label74.Location = new System.Drawing.Point(176, 1086);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(174, 5);
            this.label74.TabIndex = 418;
            this.label74.Text = "----------------------------------------------";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoOfDebitPaymentTransactions
            // 
            this.lblNoOfDebitPaymentTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfDebitPaymentTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfDebitPaymentTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfDebitPaymentTransactions.Location = new System.Drawing.Point(176, 1039);
            this.lblNoOfDebitPaymentTransactions.Name = "lblNoOfDebitPaymentTransactions";
            this.lblNoOfDebitPaymentTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfDebitPaymentTransactions.TabIndex = 417;
            this.lblNoOfDebitPaymentTransactions.Text = "0.00";
            this.lblNoOfDebitPaymentTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 1039);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 14);
            this.label10.TabIndex = 416;
            this.label10.Text = "Debit Payment Trans.";
            // 
            // lblNoOfCreditPaymentTran
            // 
            this.lblNoOfCreditPaymentTran.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfCreditPaymentTran.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfCreditPaymentTran.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfCreditPaymentTran.Location = new System.Drawing.Point(176, 1023);
            this.lblNoOfCreditPaymentTran.Name = "lblNoOfCreditPaymentTran";
            this.lblNoOfCreditPaymentTran.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfCreditPaymentTran.TabIndex = 415;
            this.lblNoOfCreditPaymentTran.Text = "0.00";
            this.lblNoOfCreditPaymentTran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoOfCreditPaymentTran1
            // 
            this.lblNoOfCreditPaymentTran1.Location = new System.Drawing.Point(16, 1023);
            this.lblNoOfCreditPaymentTran1.Name = "lblNoOfCreditPaymentTran1";
            this.lblNoOfCreditPaymentTran1.Size = new System.Drawing.Size(137, 14);
            this.lblNoOfCreditPaymentTran1.TabIndex = 414;
            this.lblNoOfCreditPaymentTran1.Text = "Credit Payment Tran";
            // 
            // lblNoOfCreditTran
            // 
            this.lblNoOfCreditTran.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfCreditTran.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfCreditTran.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfCreditTran.Location = new System.Drawing.Point(176, 991);
            this.lblNoOfCreditTran.Name = "lblNoOfCreditTran";
            this.lblNoOfCreditTran.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfCreditTran.TabIndex = 413;
            this.lblNoOfCreditTran.Text = "0.00";
            this.lblNoOfCreditTran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoOfCreditTran1
            // 
            this.lblNoOfCreditTran1.Location = new System.Drawing.Point(16, 991);
            this.lblNoOfCreditTran1.Name = "lblNoOfCreditTran1";
            this.lblNoOfCreditTran1.Size = new System.Drawing.Size(137, 14);
            this.lblNoOfCreditTran1.TabIndex = 412;
            this.lblNoOfCreditTran1.Text = "Credit Card Trans.";
            // 
            // lblNoOfVoidTransactions
            // 
            this.lblNoOfVoidTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfVoidTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfVoidTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfVoidTransactions.Location = new System.Drawing.Point(176, 1071);
            this.lblNoOfVoidTransactions.Name = "lblNoOfVoidTransactions";
            this.lblNoOfVoidTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfVoidTransactions.TabIndex = 411;
            this.lblNoOfVoidTransactions.Text = "0.00";
            this.lblNoOfVoidTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(16, 1071);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(137, 14);
            this.label33.TabIndex = 410;
            this.label33.Text = "Void Transactions";
            // 
            // lblNoOfRefundTransactions
            // 
            this.lblNoOfRefundTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfRefundTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfRefundTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfRefundTransactions.Location = new System.Drawing.Point(176, 1055);
            this.lblNoOfRefundTransactions.Name = "lblNoOfRefundTransactions";
            this.lblNoOfRefundTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfRefundTransactions.TabIndex = 409;
            this.lblNoOfRefundTransactions.Text = "0.00";
            this.lblNoOfRefundTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.Location = new System.Drawing.Point(16, 1055);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(137, 14);
            this.label37.TabIndex = 408;
            this.label37.Text = "Refund Transactions";
            // 
            // lblNoOfCombPayTran
            // 
            this.lblNoOfCombPayTran.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfCombPayTran.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfCombPayTran.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfCombPayTran.Location = new System.Drawing.Point(176, 1007);
            this.lblNoOfCombPayTran.Name = "lblNoOfCombPayTran";
            this.lblNoOfCombPayTran.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfCombPayTran.TabIndex = 407;
            this.lblNoOfCombPayTran.Text = "0.00";
            this.lblNoOfCombPayTran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(16, 1007);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(137, 14);
            this.label18.TabIndex = 406;
            this.label18.Text = "Comb. Payment Tran";
            // 
            // lblNoOfCreditCardTran
            // 
            this.lblNoOfCreditCardTran.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfCreditCardTran.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfCreditCardTran.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfCreditCardTran.Location = new System.Drawing.Point(176, 975);
            this.lblNoOfCreditCardTran.Name = "lblNoOfCreditCardTran";
            this.lblNoOfCreditCardTran.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfCreditCardTran.TabIndex = 405;
            this.lblNoOfCreditCardTran.Text = "0.00";
            this.lblNoOfCreditCardTran.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoOfCreditCardTran1
            // 
            this.lblNoOfCreditCardTran1.Location = new System.Drawing.Point(16, 975);
            this.lblNoOfCreditCardTran1.Name = "lblNoOfCreditCardTran1";
            this.lblNoOfCreditCardTran1.Size = new System.Drawing.Size(137, 14);
            this.lblNoOfCreditCardTran1.TabIndex = 404;
            this.lblNoOfCreditCardTran1.Text = "Credit Card Tran";
            // 
            // lblNoOfChequeTransactions
            // 
            this.lblNoOfChequeTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfChequeTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfChequeTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfChequeTransactions.Location = new System.Drawing.Point(176, 959);
            this.lblNoOfChequeTransactions.Name = "lblNoOfChequeTransactions";
            this.lblNoOfChequeTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfChequeTransactions.TabIndex = 403;
            this.lblNoOfChequeTransactions.Text = "0.00";
            this.lblNoOfChequeTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(16, 959);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 14);
            this.label14.TabIndex = 402;
            this.label14.Text = "Cheque Transactions";
            // 
            // lblNoOfCashTransactions
            // 
            this.lblNoOfCashTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfCashTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfCashTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfCashTransactions.Location = new System.Drawing.Point(176, 943);
            this.lblNoOfCashTransactions.Name = "lblNoOfCashTransactions";
            this.lblNoOfCashTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfCashTransactions.TabIndex = 401;
            this.lblNoOfCashTransactions.Text = "0.00";
            this.lblNoOfCashTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(16, 943);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(137, 14);
            this.label16.TabIndex = 400;
            this.label16.Text = "Cash Transactions";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(9, 918);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(353, 14);
            this.label22.TabIndex = 399;
            this.label22.Text = "Transaction Count Breakdown";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label71
            // 
            this.label71.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label71.Location = new System.Drawing.Point(9, 934);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(353, 5);
            this.label71.TabIndex = 398;
            this.label71.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label71.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label72
            // 
            this.label72.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label72.Location = new System.Drawing.Point(9, 910);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(353, 5);
            this.label72.TabIndex = 397;
            this.label72.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label57
            // 
            this.label57.Location = new System.Drawing.Point(9, 839);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(353, 14);
            this.label57.TabIndex = 396;
            this.label57.Text = "Customer Deposits";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label65.Location = new System.Drawing.Point(9, 855);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(353, 5);
            this.label65.TabIndex = 395;
            this.label65.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label70
            // 
            this.label70.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label70.Location = new System.Drawing.Point(9, 831);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(353, 5);
            this.label70.TabIndex = 394;
            this.label70.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label64
            // 
            this.label64.Location = new System.Drawing.Point(9, 758);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(353, 14);
            this.label64.TabIndex = 393;
            this.label64.Text = "Receive on Account";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label66
            // 
            this.label66.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label66.Location = new System.Drawing.Point(9, 774);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(353, 5);
            this.label66.TabIndex = 392;
            this.label66.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label68
            // 
            this.label68.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label68.Location = new System.Drawing.Point(9, 750);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(353, 5);
            this.label68.TabIndex = 391;
            this.label68.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label60
            // 
            this.label60.Location = new System.Drawing.Point(9, 678);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(353, 14);
            this.label60.TabIndex = 390;
            this.label60.Text = "PICK UP / Disburstment";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label62
            // 
            this.label62.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label62.Location = new System.Drawing.Point(9, 694);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(353, 5);
            this.label62.TabIndex = 389;
            this.label62.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label63
            // 
            this.label63.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label63.Location = new System.Drawing.Point(9, 670);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(353, 5);
            this.label63.TabIndex = 388;
            this.label63.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label54
            // 
            this.label54.Location = new System.Drawing.Point(9, 632);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(353, 14);
            this.label54.TabIndex = 387;
            this.label54.Text = "Paid Out";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label56
            // 
            this.label56.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label56.Location = new System.Drawing.Point(9, 648);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(353, 5);
            this.label56.TabIndex = 386;
            this.label56.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label58
            // 
            this.label58.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label58.Location = new System.Drawing.Point(9, 624);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(353, 5);
            this.label58.TabIndex = 385;
            this.label58.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBeginningBalance
            // 
            this.lblBeginningBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBeginningBalance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeginningBalance.ForeColor = System.Drawing.Color.Red;
            this.lblBeginningBalance.Location = new System.Drawing.Point(176, 594);
            this.lblBeginningBalance.Name = "lblBeginningBalance";
            this.lblBeginningBalance.Size = new System.Drawing.Size(174, 14);
            this.lblBeginningBalance.TabIndex = 384;
            this.lblBeginningBalance.Text = "0.00";
            this.lblBeginningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBeginningBalance1
            // 
            this.lblBeginningBalance1.Location = new System.Drawing.Point(16, 594);
            this.lblBeginningBalance1.Name = "lblBeginningBalance1";
            this.lblBeginningBalance1.Size = new System.Drawing.Size(137, 14);
            this.lblBeginningBalance1.TabIndex = 383;
            this.lblBeginningBalance1.Text = "Float";
            // 
            // label50
            // 
            this.label50.Location = new System.Drawing.Point(9, 571);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(353, 14);
            this.label50.TabIndex = 382;
            this.label50.Text = "Drawer Information";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label52
            // 
            this.label52.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label52.Location = new System.Drawing.Point(9, 587);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(353, 5);
            this.label52.TabIndex = 381;
            this.label52.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label53
            // 
            this.label53.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label53.Location = new System.Drawing.Point(9, 564);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(353, 5);
            this.label53.TabIndex = 380;
            this.label53.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalDiscounts
            // 
            this.lblTotalDiscounts.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDiscounts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDiscounts.ForeColor = System.Drawing.Color.Red;
            this.lblTotalDiscounts.Location = new System.Drawing.Point(176, 546);
            this.lblTotalDiscounts.Name = "lblTotalDiscounts";
            this.lblTotalDiscounts.Size = new System.Drawing.Size(174, 14);
            this.lblTotalDiscounts.TabIndex = 379;
            this.lblTotalDiscounts.Text = "0.00";
            this.lblTotalDiscounts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label49
            // 
            this.label49.Location = new System.Drawing.Point(16, 546);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(137, 14);
            this.label49.TabIndex = 378;
            this.label49.Text = "Total Discounts";
            // 
            // label46
            // 
            this.label46.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label46.Location = new System.Drawing.Point(176, 539);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(174, 5);
            this.label46.TabIndex = 377;
            this.label46.Text = "----------------------------------------------";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotalDiscount
            // 
            this.lblSubtotalDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalDiscount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblSubtotalDiscount.Location = new System.Drawing.Point(176, 525);
            this.lblSubtotalDiscount.Name = "lblSubtotalDiscount";
            this.lblSubtotalDiscount.Size = new System.Drawing.Size(174, 14);
            this.lblSubtotalDiscount.TabIndex = 376;
            this.lblSubtotalDiscount.Text = "0.00";
            this.lblSubtotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(16, 525);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(137, 14);
            this.label21.TabIndex = 375;
            this.label21.Text = "SubTotal Discount";
            // 
            // lblItemsDiscount
            // 
            this.lblItemsDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblItemsDiscount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblItemsDiscount.Location = new System.Drawing.Point(176, 509);
            this.lblItemsDiscount.Name = "lblItemsDiscount";
            this.lblItemsDiscount.Size = new System.Drawing.Size(174, 14);
            this.lblItemsDiscount.TabIndex = 374;
            this.lblItemsDiscount.Text = "0.00";
            this.lblItemsDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label69
            // 
            this.label69.Location = new System.Drawing.Point(16, 509);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(137, 14);
            this.label69.TabIndex = 373;
            this.label69.Text = "Item Discount";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(9, 486);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(353, 14);
            this.label13.TabIndex = 372;
            this.label13.Text = "Discounts";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label44.Location = new System.Drawing.Point(9, 502);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(353, 5);
            this.label44.TabIndex = 371;
            this.label44.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label45.Location = new System.Drawing.Point(9, 478);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(353, 5);
            this.label45.TabIndex = 370;
            this.label45.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRefundSales
            // 
            this.lblRefundSales.BackColor = System.Drawing.Color.Transparent;
            this.lblRefundSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefundSales.ForeColor = System.Drawing.Color.Red;
            this.lblRefundSales.Location = new System.Drawing.Point(176, 462);
            this.lblRefundSales.Name = "lblRefundSales";
            this.lblRefundSales.Size = new System.Drawing.Size(174, 14);
            this.lblRefundSales.TabIndex = 369;
            this.lblRefundSales.Text = "0.00";
            this.lblRefundSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(16, 462);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(137, 14);
            this.label19.TabIndex = 368;
            this.label19.Text = "Refund Sales";
            // 
            // lblVoidSales
            // 
            this.lblVoidSales.BackColor = System.Drawing.Color.Transparent;
            this.lblVoidSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoidSales.ForeColor = System.Drawing.Color.Red;
            this.lblVoidSales.Location = new System.Drawing.Point(176, 446);
            this.lblVoidSales.Name = "lblVoidSales";
            this.lblVoidSales.Size = new System.Drawing.Size(174, 14);
            this.lblVoidSales.TabIndex = 367;
            this.lblVoidSales.Text = "0.00";
            this.lblVoidSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(16, 446);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 14);
            this.label15.TabIndex = 366;
            this.label15.Text = "Void Sales";
            // 
            // lblDebitPayment
            // 
            this.lblDebitPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblDebitPayment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebitPayment.ForeColor = System.Drawing.Color.Red;
            this.lblDebitPayment.Location = new System.Drawing.Point(176, 430);
            this.lblDebitPayment.Name = "lblDebitPayment";
            this.lblDebitPayment.Size = new System.Drawing.Size(174, 14);
            this.lblDebitPayment.TabIndex = 365;
            this.lblDebitPayment.Text = "0.00";
            this.lblDebitPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 430);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 14);
            this.label8.TabIndex = 364;
            this.label8.Text = "Debit  Sales";
            // 
            // lblCreditPayment
            // 
            this.lblCreditPayment.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditPayment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditPayment.ForeColor = System.Drawing.Color.Red;
            this.lblCreditPayment.Location = new System.Drawing.Point(176, 414);
            this.lblCreditPayment.Name = "lblCreditPayment";
            this.lblCreditPayment.Size = new System.Drawing.Size(174, 14);
            this.lblCreditPayment.TabIndex = 363;
            this.lblCreditPayment.Text = "0.00";
            this.lblCreditPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreditPayment1
            // 
            this.lblCreditPayment1.Location = new System.Drawing.Point(16, 414);
            this.lblCreditPayment1.Name = "lblCreditPayment1";
            this.lblCreditPayment1.Size = new System.Drawing.Size(137, 14);
            this.lblCreditPayment1.TabIndex = 362;
            this.lblCreditPayment1.Text = "Credit Payment";
            // 
            // lblCreditSales
            // 
            this.lblCreditSales.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditSales.ForeColor = System.Drawing.Color.Red;
            this.lblCreditSales.Location = new System.Drawing.Point(176, 398);
            this.lblCreditSales.Name = "lblCreditSales";
            this.lblCreditSales.Size = new System.Drawing.Size(174, 14);
            this.lblCreditSales.TabIndex = 361;
            this.lblCreditSales.Text = "0.00";
            this.lblCreditSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreditSales1
            // 
            this.lblCreditSales1.Location = new System.Drawing.Point(16, 398);
            this.lblCreditSales1.Name = "lblCreditSales1";
            this.lblCreditSales1.Size = new System.Drawing.Size(137, 14);
            this.lblCreditSales1.TabIndex = 360;
            this.lblCreditSales1.Text = "Credit (Charge)";
            // 
            // lblCreditCardSales
            // 
            this.lblCreditCardSales.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCardSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCardSales.ForeColor = System.Drawing.Color.Red;
            this.lblCreditCardSales.Location = new System.Drawing.Point(176, 382);
            this.lblCreditCardSales.Name = "lblCreditCardSales";
            this.lblCreditCardSales.Size = new System.Drawing.Size(174, 14);
            this.lblCreditCardSales.TabIndex = 359;
            this.lblCreditCardSales.Text = "0.00";
            this.lblCreditCardSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreditCardSales1
            // 
            this.lblCreditCardSales1.Location = new System.Drawing.Point(16, 382);
            this.lblCreditCardSales1.Name = "lblCreditCardSales1";
            this.lblCreditCardSales1.Size = new System.Drawing.Size(137, 14);
            this.lblCreditCardSales1.TabIndex = 358;
            this.lblCreditCardSales1.Text = "Credit Card Sales";
            // 
            // lblChequeSales
            // 
            this.lblChequeSales.BackColor = System.Drawing.Color.Transparent;
            this.lblChequeSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeSales.ForeColor = System.Drawing.Color.Red;
            this.lblChequeSales.Location = new System.Drawing.Point(176, 366);
            this.lblChequeSales.Name = "lblChequeSales";
            this.lblChequeSales.Size = new System.Drawing.Size(174, 14);
            this.lblChequeSales.TabIndex = 357;
            this.lblChequeSales.Text = "0.00";
            this.lblChequeSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChequeSales1
            // 
            this.lblChequeSales1.Location = new System.Drawing.Point(16, 366);
            this.lblChequeSales1.Name = "lblChequeSales1";
            this.lblChequeSales1.Size = new System.Drawing.Size(137, 14);
            this.lblChequeSales1.TabIndex = 356;
            this.lblChequeSales1.Text = "Cheque Sales";
            // 
            // lblCashSales
            // 
            this.lblCashSales.BackColor = System.Drawing.Color.Transparent;
            this.lblCashSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashSales.ForeColor = System.Drawing.Color.Red;
            this.lblCashSales.Location = new System.Drawing.Point(176, 350);
            this.lblCashSales.Name = "lblCashSales";
            this.lblCashSales.Size = new System.Drawing.Size(174, 14);
            this.lblCashSales.TabIndex = 355;
            this.lblCashSales.Text = "0.00";
            this.lblCashSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCashSales1
            // 
            this.lblCashSales1.Location = new System.Drawing.Point(16, 350);
            this.lblCashSales1.Name = "lblCashSales1";
            this.lblCashSales1.Size = new System.Drawing.Size(137, 14);
            this.lblCashSales1.TabIndex = 354;
            this.lblCashSales1.Text = "Cash Sales";
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(8, 326);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(353, 14);
            this.label42.TabIndex = 353;
            this.label42.Text = "Total Amount Breakdown ";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label43
            // 
            this.label43.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label43.Location = new System.Drawing.Point(8, 342);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(353, 5);
            this.label43.TabIndex = 352;
            this.label43.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label41
            // 
            this.label41.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label41.Location = new System.Drawing.Point(8, 318);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(353, 5);
            this.label41.TabIndex = 351;
            this.label41.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLocalTax
            // 
            this.lblLocalTax.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalTax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalTax.ForeColor = System.Drawing.Color.Red;
            this.lblLocalTax.Location = new System.Drawing.Point(176, 298);
            this.lblLocalTax.Name = "lblLocalTax";
            this.lblLocalTax.Size = new System.Drawing.Size(174, 14);
            this.lblLocalTax.TabIndex = 350;
            this.lblLocalTax.Text = "0.00";
            this.lblLocalTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label73
            // 
            this.label73.Location = new System.Drawing.Point(16, 298);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(137, 14);
            this.label73.TabIndex = 349;
            this.label73.Text = "LocalTax";
            // 
            // lblVAT1
            // 
            this.lblVAT1.BackColor = System.Drawing.Color.Transparent;
            this.lblVAT1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVAT1.ForeColor = System.Drawing.Color.Red;
            this.lblVAT1.Location = new System.Drawing.Point(176, 282);
            this.lblVAT1.Name = "lblVAT1";
            this.lblVAT1.Size = new System.Drawing.Size(174, 14);
            this.lblVAT1.TabIndex = 348;
            this.lblVAT1.Text = "0.00";
            this.lblVAT1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.Location = new System.Drawing.Point(16, 282);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(137, 14);
            this.label40.TabIndex = 347;
            this.label40.Text = "VAT";
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(8, 257);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(353, 14);
            this.label38.TabIndex = 342;
            this.label38.Text = "Taxables Breakdown";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label36.Location = new System.Drawing.Point(8, 273);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(353, 5);
            this.label36.TabIndex = 341;
            this.label36.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label32.Location = new System.Drawing.Point(8, 249);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(353, 5);
            this.label32.TabIndex = 336;
            this.label32.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(173, 231);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(174, 14);
            this.lblTotalAmount.TabIndex = 333;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(16, 231);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(137, 14);
            this.label31.TabIndex = 332;
            this.label31.Text = "Total Amount";
            // 
            // label29
            // 
            this.label29.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label29.Location = new System.Drawing.Point(173, 225);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(174, 5);
            this.label29.TabIndex = 331;
            this.label29.Text = "----------------------------------------------";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharge
            // 
            this.lblCharge.BackColor = System.Drawing.Color.Transparent;
            this.lblCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharge.ForeColor = System.Drawing.Color.Red;
            this.lblCharge.Location = new System.Drawing.Point(173, 210);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(174, 14);
            this.lblCharge.TabIndex = 330;
            this.lblCharge.Text = "0.00";
            this.lblCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(16, 210);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(137, 14);
            this.label27.TabIndex = 329;
            this.label27.Text = "Other Charge";
            // 
            // lblVAT
            // 
            this.lblVAT.BackColor = System.Drawing.Color.Transparent;
            this.lblVAT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVAT.ForeColor = System.Drawing.Color.Red;
            this.lblVAT.Location = new System.Drawing.Point(173, 194);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(174, 14);
            this.lblVAT.TabIndex = 328;
            this.lblVAT.Text = "0.00";
            this.lblVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label75
            // 
            this.label75.Location = new System.Drawing.Point(16, 194);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(137, 14);
            this.label75.TabIndex = 327;
            this.label75.Text = "VAT";
            // 
            // lblNetSales
            // 
            this.lblNetSales.BackColor = System.Drawing.Color.Transparent;
            this.lblNetSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetSales.ForeColor = System.Drawing.Color.Red;
            this.lblNetSales.Location = new System.Drawing.Point(173, 178);
            this.lblNetSales.Name = "lblNetSales";
            this.lblNetSales.Size = new System.Drawing.Size(174, 14);
            this.lblNetSales.TabIndex = 326;
            this.lblNetSales.Text = "0.00";
            this.lblNetSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(16, 178);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(137, 14);
            this.label25.TabIndex = 325;
            this.label25.Text = "Net Sales";
            // 
            // label23
            // 
            this.label23.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label23.Location = new System.Drawing.Point(173, 170);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(174, 5);
            this.label23.TabIndex = 324;
            this.label23.Text = "----------------------------------------------";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreditCardDeposit
            // 
            this.lblCreditCardDeposit.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCardDeposit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCardDeposit.ForeColor = System.Drawing.Color.Red;
            this.lblCreditCardDeposit.Location = new System.Drawing.Point(176, 894);
            this.lblCreditCardDeposit.Name = "lblCreditCardDeposit";
            this.lblCreditCardDeposit.Size = new System.Drawing.Size(174, 14);
            this.lblCreditCardDeposit.TabIndex = 316;
            this.lblCreditCardDeposit.Text = "0.00";
            this.lblCreditCardDeposit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 894);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 14);
            this.label5.TabIndex = 315;
            this.label5.Text = "Credit Card";
            // 
            // lblChequeDeposit
            // 
            this.lblChequeDeposit.BackColor = System.Drawing.Color.Transparent;
            this.lblChequeDeposit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeDeposit.ForeColor = System.Drawing.Color.Red;
            this.lblChequeDeposit.Location = new System.Drawing.Point(176, 878);
            this.lblChequeDeposit.Name = "lblChequeDeposit";
            this.lblChequeDeposit.Size = new System.Drawing.Size(174, 14);
            this.lblChequeDeposit.TabIndex = 314;
            this.lblChequeDeposit.Text = "0.00";
            this.lblChequeDeposit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 878);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 14);
            this.label9.TabIndex = 313;
            this.label9.Text = "Cheque";
            // 
            // lblCashDeposit
            // 
            this.lblCashDeposit.BackColor = System.Drawing.Color.Transparent;
            this.lblCashDeposit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashDeposit.ForeColor = System.Drawing.Color.Red;
            this.lblCashDeposit.Location = new System.Drawing.Point(176, 862);
            this.lblCashDeposit.Name = "lblCashDeposit";
            this.lblCashDeposit.Size = new System.Drawing.Size(174, 14);
            this.lblCashDeposit.TabIndex = 312;
            this.lblCashDeposit.Text = "0.00";
            this.lblCashDeposit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(16, 862);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(137, 14);
            this.label17.TabIndex = 311;
            this.label17.Text = "Cash";
            // 
            // lblPaidOut
            // 
            this.lblPaidOut.BackColor = System.Drawing.Color.Transparent;
            this.lblPaidOut.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaidOut.ForeColor = System.Drawing.Color.Red;
            this.lblPaidOut.Location = new System.Drawing.Point(176, 654);
            this.lblPaidOut.Name = "lblPaidOut";
            this.lblPaidOut.Size = new System.Drawing.Size(174, 14);
            this.lblPaidOut.TabIndex = 286;
            this.lblPaidOut.Text = "0.00";
            this.lblPaidOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 654);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 14);
            this.label6.TabIndex = 285;
            this.label6.Text = "Paid-out";
            // 
            // lblCreditCardWithHold
            // 
            this.lblCreditCardWithHold.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCardWithHold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCardWithHold.ForeColor = System.Drawing.Color.Red;
            this.lblCreditCardWithHold.Location = new System.Drawing.Point(176, 814);
            this.lblCreditCardWithHold.Name = "lblCreditCardWithHold";
            this.lblCreditCardWithHold.Size = new System.Drawing.Size(174, 14);
            this.lblCreditCardWithHold.TabIndex = 284;
            this.lblCreditCardWithHold.Text = "0.00";
            this.lblCreditCardWithHold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(16, 814);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(137, 14);
            this.label47.TabIndex = 283;
            this.label47.Text = "Credit Card";
            // 
            // lblChequeWithHold
            // 
            this.lblChequeWithHold.BackColor = System.Drawing.Color.Transparent;
            this.lblChequeWithHold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeWithHold.ForeColor = System.Drawing.Color.Red;
            this.lblChequeWithHold.Location = new System.Drawing.Point(176, 798);
            this.lblChequeWithHold.Name = "lblChequeWithHold";
            this.lblChequeWithHold.Size = new System.Drawing.Size(174, 14);
            this.lblChequeWithHold.TabIndex = 282;
            this.lblChequeWithHold.Text = "0.00";
            this.lblChequeWithHold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(16, 798);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(137, 14);
            this.label51.TabIndex = 281;
            this.label51.Text = "Cheque";
            // 
            // lblCashWithHold
            // 
            this.lblCashWithHold.BackColor = System.Drawing.Color.Transparent;
            this.lblCashWithHold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashWithHold.ForeColor = System.Drawing.Color.Red;
            this.lblCashWithHold.Location = new System.Drawing.Point(176, 782);
            this.lblCashWithHold.Name = "lblCashWithHold";
            this.lblCashWithHold.Size = new System.Drawing.Size(174, 14);
            this.lblCashWithHold.TabIndex = 280;
            this.lblCashWithHold.Text = "0.00";
            this.lblCashWithHold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label55
            // 
            this.label55.Location = new System.Drawing.Point(16, 782);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(137, 14);
            this.label55.TabIndex = 279;
            this.label55.Text = "Cash";
            // 
            // lblCreditCardDisburse
            // 
            this.lblCreditCardDisburse.BackColor = System.Drawing.Color.Transparent;
            this.lblCreditCardDisburse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditCardDisburse.ForeColor = System.Drawing.Color.Red;
            this.lblCreditCardDisburse.Location = new System.Drawing.Point(176, 734);
            this.lblCreditCardDisburse.Name = "lblCreditCardDisburse";
            this.lblCreditCardDisburse.Size = new System.Drawing.Size(174, 14);
            this.lblCreditCardDisburse.TabIndex = 276;
            this.lblCreditCardDisburse.Text = "0.00";
            this.lblCreditCardDisburse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label59
            // 
            this.label59.Location = new System.Drawing.Point(16, 734);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(137, 14);
            this.label59.TabIndex = 275;
            this.label59.Text = "Credit Card";
            // 
            // lblChequeDisburse
            // 
            this.lblChequeDisburse.BackColor = System.Drawing.Color.Transparent;
            this.lblChequeDisburse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeDisburse.ForeColor = System.Drawing.Color.Red;
            this.lblChequeDisburse.Location = new System.Drawing.Point(176, 718);
            this.lblChequeDisburse.Name = "lblChequeDisburse";
            this.lblChequeDisburse.Size = new System.Drawing.Size(174, 14);
            this.lblChequeDisburse.TabIndex = 274;
            this.lblChequeDisburse.Text = "0.00";
            this.lblChequeDisburse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(16, 718);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(137, 14);
            this.label61.TabIndex = 273;
            this.label61.Text = "Cheque";
            // 
            // lblCashDisburse
            // 
            this.lblCashDisburse.BackColor = System.Drawing.Color.Transparent;
            this.lblCashDisburse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashDisburse.ForeColor = System.Drawing.Color.Red;
            this.lblCashDisburse.Location = new System.Drawing.Point(176, 702);
            this.lblCashDisburse.Name = "lblCashDisburse";
            this.lblCashDisburse.Size = new System.Drawing.Size(174, 14);
            this.lblCashDisburse.TabIndex = 272;
            this.lblCashDisburse.Text = "0.00";
            this.lblCashDisburse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCashDisburse1
            // 
            this.lblCashDisburse1.Location = new System.Drawing.Point(16, 702);
            this.lblCashDisburse1.Name = "lblCashDisburse1";
            this.lblCashDisburse1.Size = new System.Drawing.Size(137, 14);
            this.lblCashDisburse1.TabIndex = 271;
            this.lblCashDisburse1.Text = "Cash";
            // 
            // lblCashInDrawer
            // 
            this.lblCashInDrawer.BackColor = System.Drawing.Color.Transparent;
            this.lblCashInDrawer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashInDrawer.ForeColor = System.Drawing.Color.Red;
            this.lblCashInDrawer.Location = new System.Drawing.Point(176, 610);
            this.lblCashInDrawer.Name = "lblCashInDrawer";
            this.lblCashInDrawer.Size = new System.Drawing.Size(174, 14);
            this.lblCashInDrawer.TabIndex = 268;
            this.lblCashInDrawer.Text = "0.00";
            this.lblCashInDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCashInDrawer1
            // 
            this.lblCashInDrawer1.Location = new System.Drawing.Point(16, 610);
            this.lblCashInDrawer1.Name = "lblCashInDrawer1";
            this.lblCashInDrawer1.Size = new System.Drawing.Size(137, 14);
            this.lblCashInDrawer1.TabIndex = 267;
            this.lblCashInDrawer1.Text = "Cash-In-Drawer";
            // 
            // lblTotalDiscount
            // 
            this.lblTotalDiscount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDiscount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDiscount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalDiscount.Location = new System.Drawing.Point(173, 154);
            this.lblTotalDiscount.Name = "lblTotalDiscount";
            this.lblTotalDiscount.Size = new System.Drawing.Size(174, 14);
            this.lblTotalDiscount.TabIndex = 243;
            this.lblTotalDiscount.Text = "0.00";
            this.lblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label77
            // 
            this.label77.Location = new System.Drawing.Point(16, 154);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(137, 14);
            this.label77.TabIndex = 242;
            this.label77.Text = "Total Discount (-)";
            // 
            // lblGrossSales
            // 
            this.lblGrossSales.BackColor = System.Drawing.Color.Transparent;
            this.lblGrossSales.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossSales.ForeColor = System.Drawing.Color.Red;
            this.lblGrossSales.Location = new System.Drawing.Point(173, 138);
            this.lblGrossSales.Name = "lblGrossSales";
            this.lblGrossSales.Size = new System.Drawing.Size(174, 14);
            this.lblGrossSales.TabIndex = 241;
            this.lblGrossSales.Text = "0.00";
            this.lblGrossSales.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label79
            // 
            this.label79.Location = new System.Drawing.Point(16, 138);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(137, 14);
            this.label79.TabIndex = 240;
            this.label79.Text = "Gross Sales";
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
            this.lblReportFooter3.Location = new System.Drawing.Point(20, 1223);
            this.lblReportFooter3.Name = "lblReportFooter3";
            this.lblReportFooter3.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter3.TabIndex = 227;
            this.lblReportFooter3.Text = "ReportFooter3";
            this.lblReportFooter3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter2
            // 
            this.lblReportFooter2.Location = new System.Drawing.Point(20, 1207);
            this.lblReportFooter2.Name = "lblReportFooter2";
            this.lblReportFooter2.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter2.TabIndex = 226;
            this.lblReportFooter2.Text = "ReportFooter1";
            this.lblReportFooter2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportFooter1
            // 
            this.lblReportFooter1.Location = new System.Drawing.Point(20, 1191);
            this.lblReportFooter1.Name = "lblReportFooter1";
            this.lblReportFooter1.Size = new System.Drawing.Size(331, 15);
            this.lblReportFooter1.TabIndex = 225;
            this.lblReportFooter1.Text = "ReportFooter1";
            this.lblReportFooter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPanelBot
            // 
            this.lblPanelBot.Location = new System.Drawing.Point(20, 1239);
            this.lblPanelBot.Name = "lblPanelBot";
            this.lblPanelBot.Size = new System.Drawing.Size(331, 15);
            this.lblPanelBot.TabIndex = 168;
            this.lblPanelBot.Text = "-/-";
            this.lblPanelBot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoOfTotalTransactions
            // 
            this.lblNoOfTotalTransactions.BackColor = System.Drawing.Color.Transparent;
            this.lblNoOfTotalTransactions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfTotalTransactions.ForeColor = System.Drawing.Color.Red;
            this.lblNoOfTotalTransactions.Location = new System.Drawing.Point(176, 1094);
            this.lblNoOfTotalTransactions.Name = "lblNoOfTotalTransactions";
            this.lblNoOfTotalTransactions.Size = new System.Drawing.Size(174, 14);
            this.lblNoOfTotalTransactions.TabIndex = 161;
            this.lblNoOfTotalTransactions.Text = "0.00";
            this.lblNoOfTotalTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(16, 1094);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(137, 14);
            this.label28.TabIndex = 160;
            this.label28.Text = "Total Transactions";
            // 
            // lblReceiptDesc
            // 
            this.lblReceiptDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptDesc.Location = new System.Drawing.Point(16, 112);
            this.lblReceiptDesc.Name = "lblReceiptDesc";
            this.lblReceiptDesc.Size = new System.Drawing.Size(331, 15);
            this.lblReceiptDesc.TabIndex = 122;
            this.lblReceiptDesc.Text = "Cashier\'s Report";
            this.lblReceiptDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportHeader4
            // 
            this.lblReportHeader4.Location = new System.Drawing.Point(16, 88);
            this.lblReportHeader4.Name = "lblReportHeader4";
            this.lblReportHeader4.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader4.TabIndex = 117;
            this.lblReportHeader4.Text = "ReportHeader4";
            this.lblReportHeader4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader3
            // 
            this.lblReportHeader3.ForeColor = System.Drawing.Color.Blue;
            this.lblReportHeader3.Location = new System.Drawing.Point(16, 72);
            this.lblReportHeader3.Name = "lblReportHeader3";
            this.lblReportHeader3.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader3.TabIndex = 116;
            this.lblReportHeader3.Text = "ReportHeader3";
            this.lblReportHeader3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader2
            // 
            this.lblReportHeader2.Location = new System.Drawing.Point(16, 56);
            this.lblReportHeader2.Name = "lblReportHeader2";
            this.lblReportHeader2.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader2.TabIndex = 115;
            this.lblReportHeader2.Text = "ReportHeader2";
            this.lblReportHeader2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblReportHeader1
            // 
            this.lblReportHeader1.Location = new System.Drawing.Point(16, 40);
            this.lblReportHeader1.Name = "lblReportHeader1";
            this.lblReportHeader1.Size = new System.Drawing.Size(331, 15);
            this.lblReportHeader1.TabIndex = 114;
            this.lblReportHeader1.Text = "ReportHeader1";
            this.lblReportHeader1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCompany
            // 
            this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.Location = new System.Drawing.Point(16, 16);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(331, 15);
            this.lblCompany.TabIndex = 113;
            this.lblCompany.Text = "AceSoft RetailPlus ™";
            this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label20.Location = new System.Drawing.Point(8, 128);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(353, 5);
            this.label20.TabIndex = 322;
            this.label20.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(8, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 5);
            this.label2.TabIndex = 323;
            this.label2.Text = "---------------------------------------------------------------------------------" +
                "---------";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReportDesc
            // 
            this.lblReportDesc.AutoSize = true;
            this.lblReportDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblReportDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDesc.ForeColor = System.Drawing.Color.White;
            this.lblReportDesc.Location = new System.Drawing.Point(67, 22);
            this.lblReportDesc.Name = "lblReportDesc";
            this.lblReportDesc.Size = new System.Drawing.Size(135, 13);
            this.lblReportDesc.TabIndex = 3;
            this.lblReportDesc.Text = "XRead Report Window.";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(762, 41);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(252, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Tag = "";
            this.lblDescription.Text = "Press Enter Key to print the current viewed report.";
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
            // CashierReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblReportDesc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CashierReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CashierReportWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CashierReportWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panReport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods
        
        private void CashierReportWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CashierReport.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			lblReportDesc.Text = mCashierName + " Cashier Report Window.";
			groupBox1.Text = "Cashier's Report Details";
			lblReceiptDesc.Text = "Cashier's Report : " + mCashierName;

			PopulateTerminalReport();
		}

		private void CashierReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

				case Keys.PageDown:
					ScrollIntoView(e.KeyData);
					break;

				case Keys.PageUp:
					ScrollIntoView(e.KeyData);
					break;
			}
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

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Private Methods

        private void ScrollIntoView(Keys KeyData)
		{
			if (KeyData == Keys.PageUp)
			{
				panReport.ScrollControlIntoView(this.lblPanelTop);
			}
			else if (KeyData == Keys.PageDown)
			{
				panReport.ScrollControlIntoView(this.lblPanelBot);
			}
		}

		private void PopulateTerminalReport()
		{	
			lblCompany.Text = CompanyDetails.CompanyCode;

			Reports.ReceiptFormat clsReceiptFormat = new Reports.ReceiptFormat();
			Reports.ReceiptFormatDetails clsDetails = clsReceiptFormat.Details();
			clsReceiptFormat.CommitAndDispose();

			lblReportHeader1.Text = GetReceiptFormatParameter(clsDetails.ReportHeader1);
			lblReportHeader2.Text = GetReceiptFormatParameter(clsDetails.ReportHeader2);
			lblReportHeader3.Text = GetReceiptFormatParameter(clsDetails.ReportHeader3);
			lblReportHeader4.Text = GetReceiptFormatParameter(clsDetails.ReportHeader4);

			lblGrossSales.Text				= mDetails.GrossSales.ToString("#,##0.00");
			lblTotalDiscount.Text			= mDetails.TotalDiscount.ToString("#,##0.00");
			
			lblNetSales.Text				= mDetails.DailySales.ToString("#,##0.00");
			lblVAT.Text						= mDetails.VAT.ToString("#,##0.00");
			lblCharge.Text					= mDetails.TotalCharge.ToString("#,##0.00");
			decimal TotalAmount				= mDetails.DailySales + mDetails.VAT + mDetails.EVAT + mDetails.LocalTax + mDetails.TotalCharge;
			lblTotalAmount.Text				= TotalAmount.ToString("#,##0.00");

			lblVAT1.Text					= mDetails.VAT.ToString("#,##0.#0");
			lblLocalTax.Text				= mDetails.LocalTax.ToString("#,##0.#0");

			lblCashSales.Text				= mDetails.CashSales.ToString("#,##0.#0");
			lblChequeSales.Text				= mDetails.ChequeSales.ToString("#,##0.#0");
			lblCreditCardSales.Text			= mDetails.CreditCardSales.ToString("#,##0.#0");
			lblCreditSales.Text				= mDetails.CreditSales.ToString("#,##0.#0");
			lblCreditPayment.Text			= mDetails.CreditPayment.ToString("#,##0.#0");
			lblDebitPayment.Text			= mDetails.DebitPayment.ToString("#,##0.#0");
			lblVoidSales.Text				= mDetails.VoidSales.ToString("#,##0.#0");
			lblRefundSales.Text				= mDetails.RefundSales.ToString("#,##0.#0");

			lblItemsDiscount.Text			= mDetails.ItemsDiscount.ToString("#,##0.#0");
			lblSubtotalDiscount.Text		= mDetails.SubTotalDiscount.ToString("#,##0.#0");
			lblTotalDiscounts.Text			= mDetails.TotalDiscount.ToString("#,##0.#0");

			lblBeginningBalance.Text		= mDetails.BeginningBalance.ToString("#,##0.#0");
			lblCashInDrawer.Text			= mDetails.CashInDrawer.ToString("#,##0.#0");

			lblPaidOut.Text					= mDetails.TotalPaidOut.ToString("#,##0.#0");
			lblCashDisburse.Text			= mDetails.CashDisburse.ToString("#,##0.#0");
			lblChequeDisburse.Text			= mDetails.ChequeDisburse.ToString("#,##0.#0");
			lblCreditCardDisburse.Text		= mDetails.CreditCardDisburse.ToString("#,##0.#0");

			lblCashWithHold.Text			= mDetails.CashWithHold.ToString("#,##0.#0");
			lblChequeWithHold.Text			= mDetails.ChequeWithHold.ToString("#,##0.#0");
			lblCreditCardWithHold.Text		= mDetails.CreditCardWithHold.ToString("#,##0.#0");

			lblCashDeposit.Text				= mDetails.CashDeposit.ToString("#,##0.#0");
			lblChequeDeposit.Text			= mDetails.ChequeDeposit.ToString("#,##0.#0");
			lblCreditCardDeposit.Text		= mDetails.CreditCardDeposit.ToString("#,##0.#0");

			lblNoOfCashTransactions.Text	= mDetails.NoOfCashTransactions.ToString("#,##0");
			lblNoOfChequeTransactions.Text	= mDetails.NoOfChequeTransactions.ToString("#,##0");
			lblNoOfCreditCardTran.Text		= mDetails.NoOfCreditCardTransactions.ToString("#,##0");
			lblNoOfCreditTran.Text			= mDetails.NoOfCreditTransactions.ToString("#,##0");
			lblNoOfCombPayTran.Text			= mDetails.NoOfCombinationPaymentTransactions.ToString("#,##0");
			lblNoOfCreditPaymentTran.Text	= mDetails.NoOfCreditPaymentTransactions.ToString("#,##0");
			lblNoOfDebitPaymentTransactions.Text	= mDetails.NoOfDebitPaymentTransactions.ToString("#,##0");
			lblNoOfRefundTransactions.Text	= mDetails.NoOfRefundTransactions.ToString("#,##0");
			lblNoOfVoidTransactions.Text	= mDetails.NoOfVoidTransactions.ToString("#,##0");
			lblNoOfTotalTransactions.Text	= mDetails.NoOfTotalTransactions.ToString("#,##0");
			
			lblCashCount.Text				= mDetails.CashCount.ToString("#,##0.#0");

			if (mDetails.CashInDrawer > mDetails.CashCount)
			{
				lblShortOverName.Text = "Short";
				decimal decShort = mDetails.CashInDrawer - mDetails.CashCount;
				lblShortOver.Text = decShort.ToString("#,##0.#0");
			}
			else if (mDetails.CashCount > mDetails.CashInDrawer)
			{
				lblShortOverName.Text = "Over";
				decimal decOver = mDetails.CashCount - mDetails.CashInDrawer;
				lblShortOver.Text = decOver.ToString("#,##0.#0");
			}
			else
			{
				lblShortOverName.Text = "";
				lblShortOver.Text = "";
			}

			lblReportFooter1.Text = GetReceiptFormatParameter(clsDetails.ReportFooter1);
			lblReportFooter2.Text = GetReceiptFormatParameter(clsDetails.ReportFooter2);
			lblReportFooter3.Text = GetReceiptFormatParameter(clsDetails.ReportFooter3);

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
				stRetValue = "Invoice #: " + "N/A";
			}
			else
			{
				stRetValue = stReceiptFormat;
			}

			return stRetValue;
		}

        #endregion

	}
}