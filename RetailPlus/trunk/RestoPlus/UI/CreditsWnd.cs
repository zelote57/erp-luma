using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class CreditsWnd : Form
	{
		private PictureBox imgIcon;
		private GroupBox groupBox1;
		private Label lblBalanceName;
		private Label lblBalance;
        private DataGrid dgItems;
        private DataGridTableStyle dgStyle;
        private DataGridTextBoxColumn TransactionID;
        private DataGridTextBoxColumn TransactionNo;
        private DataGridTextBoxColumn CustomerID;
        private DataGridTextBoxColumn CustomerName;
        private DataGridTextBoxColumn TransactionDate;
        private DataGridTextBoxColumn SubTotal;
        private DataGridTextBoxColumn Discount;
        private DataGridTextBoxColumn AmountPaid;
        private DataGridTextBoxColumn Credit;
        private DataGridTextBoxColumn CreditPaid;
        private DataGridTextBoxColumn Balance;
        private Label lblHeader1;
        private Label lblHeader;
        private Button cmdCancel;
        private Button cmdEnter;
        private System.ComponentModel.Container components = null;

		private DialogResult dialog;
        private Data.ContactDetails mclsCustomerDetails;
		private decimal mdecAmountPaid;
		private decimal mdecCashPayment;
		private decimal mdecChequePayment;
		private decimal mdecCreditCardPayment;
		private decimal mdecDebitPayment;
		private decimal mdecBalanceAmount;
		private decimal mdecChangeAmount;
		private PaymentTypes mPaymentType = PaymentTypes.NotYetAssigned;
		private ArrayList marrCashPaymentDetails = new ArrayList();
		private ArrayList marrChequePaymentDetails = new ArrayList();
		private ArrayList marrCreditCardPaymentDetails = new ArrayList();
		private ArrayList marrDebitPaymentDetails = new ArrayList();

		public decimal AmountPayment
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
		public ArrayList DebitPaymentDetails
		{
			get	{	return marrDebitPaymentDetails;	}
		}
		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}
        public Data.ContactDetails CustomerDetails
		{
            set { mclsCustomerDetails = value; }
		}

		#region Constructors and Destuctors

		public CreditsWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		private void InitializeComponent()
		{
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TransactionID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CustomerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AmountPaid = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CreditPaid = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 68;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.dgItems);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 391);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unpaid Transaction Details";
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgItems.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.DataMember = "";
            this.dgItems.Enabled = false;
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(8, 24);
            this.dgItems.Name = "dgItems";
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 5;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(770, 361);
            this.dgItems.TabIndex = 55;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.TransactionID,
            this.TransactionNo,
            this.CustomerID,
            this.CustomerName,
            this.TransactionDate,
            this.SubTotal,
            this.Discount,
            this.AmountPaid,
            this.Credit,
            this.CreditPaid,
            this.Balance});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblForPayment";
            this.dgStyle.PreferredColumnWidth = 0;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // TransactionID
            // 
            this.TransactionID.Format = "";
            this.TransactionID.FormatInfo = null;
            this.TransactionID.MappingName = "TransactionID";
            this.TransactionID.NullText = "";
            this.TransactionID.ReadOnly = true;
            this.TransactionID.Width = 0;
            // 
            // TransactionNo
            // 
            this.TransactionNo.Format = "";
            this.TransactionNo.FormatInfo = null;
            this.TransactionNo.HeaderText = "Trans. No";
            this.TransactionNo.MappingName = "TransactionNo";
            this.TransactionNo.NullText = "";
            this.TransactionNo.ReadOnly = true;
            this.TransactionNo.Width = 120;
            // 
            // CustomerID
            // 
            this.CustomerID.Format = "";
            this.CustomerID.FormatInfo = null;
            this.CustomerID.MappingName = "CustomerID";
            this.CustomerID.NullText = "";
            this.CustomerID.ReadOnly = true;
            this.CustomerID.Width = 0;
            // 
            // CustomerName
            // 
            this.CustomerName.Format = "";
            this.CustomerName.FormatInfo = null;
            this.CustomerName.MappingName = "CustomerName";
            this.CustomerName.NullText = "";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 0;
            // 
            // TransactionDate
            // 
            this.TransactionDate.Format = "";
            this.TransactionDate.FormatInfo = null;
            this.TransactionDate.HeaderText = "Trans. Date";
            this.TransactionDate.MappingName = "TransactionDate";
            this.TransactionDate.NullText = "";
            this.TransactionDate.ReadOnly = true;
            this.TransactionDate.Width = 90;
            // 
            // SubTotal
            // 
            this.SubTotal.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.SubTotal.Format = "";
            this.SubTotal.FormatInfo = null;
            this.SubTotal.HeaderText = "SubTotal";
            this.SubTotal.MappingName = "SubTotal";
            this.SubTotal.NullText = "";
            this.SubTotal.ReadOnly = true;
            this.SubTotal.Width = 0;
            // 
            // Discount
            // 
            this.Discount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Discount.Format = "";
            this.Discount.FormatInfo = null;
            this.Discount.HeaderText = "Discount";
            this.Discount.MappingName = "Discount";
            this.Discount.NullText = "";
            this.Discount.ReadOnly = true;
            this.Discount.Width = 0;
            // 
            // AmountPaid
            // 
            this.AmountPaid.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.AmountPaid.Format = "";
            this.AmountPaid.FormatInfo = null;
            this.AmountPaid.HeaderText = "Amnt. Paid";
            this.AmountPaid.MappingName = "AmountPaid";
            this.AmountPaid.NullText = "";
            this.AmountPaid.ReadOnly = true;
            this.AmountPaid.Width = 0;
            // 
            // Credit
            // 
            this.Credit.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Credit.Format = "";
            this.Credit.FormatInfo = null;
            this.Credit.HeaderText = "Credit";
            this.Credit.MappingName = "Credit";
            this.Credit.NullText = "";
            this.Credit.ReadOnly = true;
            this.Credit.Width = 0;
            // 
            // CreditPaid
            // 
            this.CreditPaid.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.CreditPaid.Format = "";
            this.CreditPaid.FormatInfo = null;
            this.CreditPaid.HeaderText = "Amt. Paid";
            this.CreditPaid.MappingName = "CreditPaid";
            this.CreditPaid.NullText = "";
            this.CreditPaid.ReadOnly = true;
            this.CreditPaid.Width = 0;
            // 
            // Balance
            // 
            this.Balance.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.Balance.Format = "";
            this.Balance.FormatInfo = null;
            this.Balance.HeaderText = "Balance";
            this.Balance.MappingName = "Balance";
            this.Balance.NullText = "";
            this.Balance.ReadOnly = true;
            this.Balance.Width = 0;
            // 
            // lblHeader1
            // 
            this.lblHeader1.AutoSize = true;
            this.lblHeader1.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.ForeColor = System.Drawing.Color.White;
            this.lblHeader1.Location = new System.Drawing.Point(67, 22);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(177, 13);
            this.lblHeader1.TabIndex = 70;
            this.lblHeader1.Text = "Current credit transactions of ";
            // 
            // lblBalanceName
            // 
            this.lblBalanceName.AutoSize = true;
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(262, 475);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(255, 29);
            this.lblBalanceName.TabIndex = 86;
            this.lblBalanceName.Text = "TOTAL CREDIT BALANCE";
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(538, 477);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(248, 25);
            this.lblBalance.TabIndex = 87;
            this.lblBalance.Text = "0.00";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Red;
            this.lblHeader.Location = new System.Drawing.Point(238, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(133, 13);
            this.lblHeader.TabIndex = 88;
            this.lblHeader.Text = "RetailPlus Customer ™";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(527, 503);
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
            this.cmdEnter.Location = new System.Drawing.Point(633, 503);
            this.cmdEnter.Name = "cmdEnter";
            this.cmdEnter.Size = new System.Drawing.Size(106, 83);
            this.cmdEnter.TabIndex = 0;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CreditsWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblBalanceName);
            this.Controls.Add(this.lblHeader1);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CreditsWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditsWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditsWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		#endregion

        #region Windows Form Methods

        private void CreditsWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Credits.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            lblHeader.Text = mclsCustomerDetails.ContactName;
            lblBalance.Text = mclsCustomerDetails.Credit.ToString("#,##0.#0");
			LoadOptions();
			LoadData();
		}
		private void CreditsWnd_KeyDown(object sender, KeyEventArgs e)
		{
			System.Data.DataTable dt;
			int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
					this.Hide(); 
					break;

				case Keys.Enter:
					if (Convert.ToDecimal(lblBalance.Text) > 0)
					{
						dialog = ShowPayment();
						if (dialog == DialogResult.OK)
							this.Hide();
					}
					else
						this.Hide();

					break;

				case Keys.Up:
					dt = (System.Data.DataTable) dgItems.DataSource;
					if (dgItems.CurrentRowIndex > 0) 
					{
						index = dgItems.CurrentRowIndex;				
						dgItems.CurrentRowIndex -= 1;
						dgItems.Select(dgItems.CurrentRowIndex);
						dgItems.UnSelect(index);
					}
					break;

				case Keys.Down:
					dt = (System.Data.DataTable) dgItems.DataSource;
					if (dgItems.CurrentRowIndex < dt.Rows.Count-1) 
					{
						index = dgItems.CurrentRowIndex;				

						dgItems.CurrentRowIndex += 1;
						dgItems.Select(dgItems.CurrentRowIndex);
						dgItems.UnSelect(index);
					}
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
            if (Convert.ToDecimal(lblBalance.Text) > 0)
            {
                dialog = ShowPayment();
                if (dialog == DialogResult.OK)
                    this.Hide();
            }
            else
                this.Hide();
        }

        #endregion

        #region Private Modifiers

        private void LoadOptions()
		{
			dgStyle.GridColumnStyles["TransactionNo"].Width = this.Width - 700;
			dgStyle.GridColumnStyles["TransactionDate"].Width = 120;
			dgStyle.GridColumnStyles["Subtotal"].Width = 90;
			dgStyle.GridColumnStyles["Discount"].Width = 90;
			dgStyle.GridColumnStyles["AmountPaid"].Width = 90;
			dgStyle.GridColumnStyles["Credit"].Width = 90;
			dgStyle.GridColumnStyles["CreditPaid"].Width = 90;
			dgStyle.GridColumnStyles["Balance"].Width = 90;
		}
		
		private void LoadData()
		{	
			try
			{
				Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                System.Data.DataTable dt = clsTransactions.ListForPaymentDataTable(mclsCustomerDetails.ContactID);

				clsTransactions.CommitAndDispose();
				dgItems.DataSource = dt;

				if (dt.Rows.Count > 0)
				{
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}
		private DialogResult ShowPayment()
		{
			DialogResult paymentResult = DialogResult.Cancel;

			if (Convert.ToDecimal(lblBalance.Text) > 0)
			{
				PaymentsWnd payment = new PaymentsWnd();
                
				payment.SalesTransactionDetails = new Data.SalesTransactionDetails();
                //payment.TransactionNo = string.Empty; //will get from the transction nos.
                payment.CustomerDetails = mclsCustomerDetails;
				payment.Discount = Convert.ToDecimal(0);
				payment.SubTotal = mclsCustomerDetails.Credit;
				payment.IsRefund = false;

				payment.ShowDialog(this);

				paymentResult = payment.Result;
			
				mdecAmountPaid = payment.AmountPaid;
				mdecCashPayment = payment.CashPayment;
				mdecChequePayment = payment.ChequePayment;
				mdecCreditCardPayment = payment.CreditCardPayment;
				mdecDebitPayment = payment.DebitPayment;
				mdecBalanceAmount = payment.BalanceAmount;
				mdecChangeAmount = payment.ChangeAmount;
				PaymentTypes mPaymentType = payment.PaymentType;
				marrCashPaymentDetails = payment.CashPaymentDetails;
				marrChequePaymentDetails = payment.ChequePaymentDetails;
				marrCreditCardPaymentDetails = payment.CreditCardPaymentDetails;
				marrDebitPaymentDetails = payment.DebitPaymentDetails;
				payment.Close();
				payment.Dispose();

				if (paymentResult == DialogResult.OK)
				{
					SavePayments(mdecAmountPaid, mdecCashPayment, mdecChequePayment, mdecCreditCardPayment, mdecDebitPayment,
						marrCashPaymentDetails, marrChequePaymentDetails, marrCreditCardPaymentDetails, marrDebitPaymentDetails);
				}
			}
			return paymentResult;
		}

		#endregion

		#region Save Payments

		private void SavePayments(decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal DebitPayment, ArrayList arrCashPaymentDetails, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrDebitPaymentDetails)
		{
			Data.Payment clsPayment = new Data.Payment();

			if (CashPayment > 0)
				SaveCashPayment(clsPayment, arrCashPaymentDetails);
			
			if (ChequePayment > 0)
				SaveChequePayment(clsPayment, arrChequePaymentDetails);

			if (CreditCardPayment > 0)
				SaveCreditCardPayment(clsPayment, arrCreditCardPaymentDetails);

			if (DebitPayment > 0)
				SaveDebitPayment(clsPayment, arrDebitPaymentDetails);

			clsPayment.CommitAndDispose();
		}
        private void SaveCashPayment(Data.Payment pvtclsPayment, ArrayList pvtarrCashPaymentDetails)
		{
			int itemIndex = 0;

			Data.CashPaymentDetails[] CashPaymentDetails = new Data.CashPaymentDetails[0];
			if (pvtarrCashPaymentDetails != null)
			{
				CashPaymentDetails = new Data.CashPaymentDetails[pvtarrCashPaymentDetails.Count];
				pvtarrCashPaymentDetails.CopyTo(CashPaymentDetails);
			}
			foreach (Data.CashPaymentDetails det in CashPaymentDetails)
			{
                string strRemarks = "PAID BY:" + mclsCustomerDetails.ContactName + "  PAYMENTTYPE:Cash DATE:" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                if (det.Remarks != null) strRemarks += Environment.NewLine + det.Remarks;

				decimal decRemainingAmountPaid = det.Amount;
				
				System.Data.DataTable dt = (System.Data.DataTable) dgItems.DataSource;
				foreach(System.Data.DataRow dr in dt.Rows) 
				{
					long lngTransactionID = Convert.ToInt64(dr["TransactionID"]);
                    string strTransactionNo = dr["TransactionNo"].ToString();
					decimal decBalance = Convert.ToDecimal(dr["Balance"]);

					if (decRemainingAmountPaid >= decBalance)
					{
                        InsertCashPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decBalance, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decBalance.ToString("#,##0.#0");
						dr["Balance"] = "0.00";

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
                        InsertCashPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decRemainingAmountPaid.ToString("#,##0.#0");
						dr["Balance"] = Convert.ToDecimal(decBalance - decRemainingAmountPaid).ToString("#,##0.#0");
						decRemainingAmountPaid =0;
						break;
					}
					itemIndex++;
				}
				dgItems.DataSource = dt;
				if (dt.Rows.Count > 0)
				{
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
				}
			}
		}
		private void InsertCashPayment(Data.Payment pvtclsPayment, long pvtlngTransactionID, string pvtstrTransactionNo, decimal pvtdecAmount, string pvtstrRemarks)
		{
			Data.CashPaymentDetails Details = new Data.CashPaymentDetails();
            Details.TransactionID = pvtlngTransactionID;
            Details.TransactionNo = pvtstrTransactionNo;
			Details.Amount = pvtdecAmount;
			Details.Remarks = pvtstrRemarks;

            pvtclsPayment.InsertCashPayment(Details);
            pvtclsPayment.UpdateCredit(mclsCustomerDetails.ContactID, pvtlngTransactionID, pvtstrTransactionNo, pvtdecAmount, pvtstrRemarks);
		}
        private void SaveChequePayment(Data.Payment pvtclsPayment, ArrayList pvtarrChequePaymentDetails)
		{
			int itemIndex = 0;

			Data.ChequePaymentDetails[] ChequePaymentDetails = new Data.ChequePaymentDetails[0];
			if (pvtarrChequePaymentDetails != null)
			{
				ChequePaymentDetails = new Data.ChequePaymentDetails[pvtarrChequePaymentDetails.Count];
				pvtarrChequePaymentDetails.CopyTo(ChequePaymentDetails);
			}
			foreach (Data.ChequePaymentDetails det in ChequePaymentDetails)
			{
                string strRemarks = "PAID BY:" + mclsCustomerDetails.ContactName + "  PAYMENTTYPE:Cheque DATE:" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                if (det.Remarks != null) strRemarks += Environment.NewLine + det.Remarks;

				decimal decRemainingAmountPaid = det.Amount;
				
				System.Data.DataTable dt = (System.Data.DataTable) dgItems.DataSource;
				foreach(System.Data.DataRow dr in dt.Rows) 
				{
					long lngTransactionID = Convert.ToInt64(dr["TransactionID"]);
                    string strTransactionNo = dr["TransactionNo"].ToString();
                    decimal decBalance = Convert.ToDecimal(dr["Balance"]);

					if (decRemainingAmountPaid >= decBalance)
					{
                        InsertChequePayment(pvtclsPayment, lngTransactionID, strTransactionNo, det.ChequeNo, decBalance, det.ValidityDate, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decBalance.ToString("#,##0.#0");
						dr["Balance"] = "0.00";

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
                        InsertChequePayment(pvtclsPayment, lngTransactionID, strTransactionNo, det.ChequeNo, decRemainingAmountPaid, det.ValidityDate, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decRemainingAmountPaid.ToString("#,##0.#0");
						dr["Balance"] = Convert.ToDecimal(decBalance - decRemainingAmountPaid).ToString("#,##0.#0");
						decRemainingAmountPaid =0;
						break;
					}
					itemIndex++;
				}
				dgItems.DataSource = dt;
				if (dt.Rows.Count > 0)
				{
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
				}
			}
		}
		private void InsertChequePayment(Data.Payment pvtclsPayment, long pvtlngTransactionID, string pvtstrTransactionNo, string pvtstrChequeNo, decimal pvtdecAmount, DateTime pvtdteValidityDate, string pvtstrRemarks)
		{
			Data.ChequePaymentDetails Details = new Data.ChequePaymentDetails();
			Details.TransactionID = pvtlngTransactionID;
            Details.TransactionNo = pvtstrTransactionNo;
			Details.ChequeNo = pvtstrChequeNo;
			Details.Amount = pvtdecAmount;
			Details.ValidityDate = pvtdteValidityDate;
			Details.Remarks = pvtstrRemarks;

            pvtclsPayment.InsertChequePayment(Details);
            pvtclsPayment.UpdateCredit(mclsCustomerDetails.ContactID, pvtlngTransactionID, pvtstrTransactionNo, pvtdecAmount, pvtstrRemarks);
		}
        private void SaveCreditCardPayment(Data.Payment pvtclsPayment, ArrayList pvtarrCreditCardPaymentDetails)
		{
			int itemIndex = 0;

			Data.CreditCardPaymentDetails[] CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[0];
			if (pvtarrCreditCardPaymentDetails != null)
			{
				CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[pvtarrCreditCardPaymentDetails.Count];
				pvtarrCreditCardPaymentDetails.CopyTo(CreditCardPaymentDetails);
			}
			foreach (Data.CreditCardPaymentDetails det in CreditCardPaymentDetails)
			{
                string strRemarks = "PAID BY:" + mclsCustomerDetails.ContactName + " PAYMENTTYPE:CreditCard DATE:" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                if (det.Remarks != null) strRemarks += Environment.NewLine + det.Remarks;

				decimal decRemainingAmountPaid = det.Amount;
				
				System.Data.DataTable dt = (System.Data.DataTable) dgItems.DataSource;
				foreach(System.Data.DataRow dr in dt.Rows) 
				{
                    long lngTransactionID = Convert.ToInt64(dr["TransactionID"]);
                    string strTransactionNo = dr["TransactionNo"].ToString();
                    decimal decBalance = Convert.ToDecimal(dr["Balance"]);

					if (decRemainingAmountPaid >= decBalance)
					{
                        InsertCreditCardPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decBalance, det.CardTypeID, det.CardNo, det.CardHolder, det.ValidityDates, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decBalance.ToString("#,##0.#0");
						dr["Balance"] = "0.00";

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
                        InsertCreditCardPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, det.CardTypeID, det.CardNo, det.CardHolder, det.ValidityDates, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decRemainingAmountPaid.ToString("#,##0.#0");
						dr["Balance"] = Convert.ToDecimal(decBalance - decRemainingAmountPaid).ToString("#,##0.#0");
						decRemainingAmountPaid =0;
						break;
					}
					itemIndex++;
				}
				dgItems.DataSource = dt;
				if (dt.Rows.Count > 0)
				{
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
				}
			}
		}
        private void InsertCreditCardPayment(Data.Payment pvtclsPayment, long pvtlngTransactionID, string pvtstrTransactionNo,  decimal pvtdecAmount, Int16 pvtintCardTypeID, string pvtstrCardNo, string pvtstrCardHolder, string pvtstrValidityDates, string pvtstrRemarks)
		{
            Data.CardType clsCardType = new Data.CardType(pvtclsPayment.Connection, pvtclsPayment.Transaction);
            Data.CardTypeDetails clsCardTypeDetails = clsCardType.Details(pvtintCardTypeID);

			Data.CreditCardPaymentDetails Details = new Data.CreditCardPaymentDetails();
            Details.TransactionID = pvtlngTransactionID;
            Details.TransactionNo = pvtstrTransactionNo;
			Details.Amount = pvtdecAmount;
			Details.CardTypeID = pvtintCardTypeID;
			Details.CardNo = pvtstrCardNo;
            Details.CardTypeCode = clsCardTypeDetails.CardTypeCode;
            Details.CardTypeName = clsCardTypeDetails.CardTypeName;
			Details.CardHolder = pvtstrCardHolder;
			Details.ValidityDates = pvtstrValidityDates;
			Details.Remarks = pvtstrRemarks;

            pvtclsPayment.InsertCreditCardPayment(Details);
            pvtclsPayment.UpdateCredit(mclsCustomerDetails.ContactID, pvtlngTransactionID, pvtstrTransactionNo, pvtdecAmount, pvtstrRemarks);
		}
		private void SaveDebitPayment(Data.Payment pvtclsPayment, ArrayList pvtarrDebitPaymentDetails)
		{
			int itemIndex = 0;

			Data.DebitPaymentDetails[] DebitPaymentDetails = new Data.DebitPaymentDetails[0];
			if (pvtarrDebitPaymentDetails != null)
			{
				DebitPaymentDetails = new Data.DebitPaymentDetails[pvtarrDebitPaymentDetails.Count];
				pvtarrDebitPaymentDetails.CopyTo(DebitPaymentDetails);
			}
			foreach (Data.DebitPaymentDetails det in DebitPaymentDetails)
			{
                string strRemarks = "PAID BY:" + mclsCustomerDetails.ContactName + " PAYMENTTYPE:Debit DATE:" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                if (det.Remarks != null) strRemarks += Environment.NewLine + det.Remarks;

				decimal decRemainingAmountPaid = det.Amount;
				
				System.Data.DataTable dt = (System.Data.DataTable) dgItems.DataSource;
				foreach(System.Data.DataRow dr in dt.Rows) 
				{
                    long lngTransactionID = Convert.ToInt64(dr["TransactionID"]);
                    string strTransactionNo = dr["TransactionNo"].ToString();
                    decimal decBalance = Convert.ToDecimal(dr["Balance"]);

					if (decRemainingAmountPaid >= decBalance)
					{
                        InsertDebitPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decBalance, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decBalance.ToString("#,##0.#0");
						dr["Balance"] = "0.00";

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
                        InsertDebitPayment(pvtclsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, strRemarks);

						dgItems.Select(itemIndex);
						dr["CreditPaid"] = decRemainingAmountPaid.ToString("#,##0.#0");
						dr["Balance"] = Convert.ToDecimal(decBalance - decRemainingAmountPaid).ToString("#,##0.#0");
						decRemainingAmountPaid =0;
						break;
					}
					itemIndex++;
				}
				dgItems.DataSource = dt;
				if (dt.Rows.Count > 0)
				{
					dgItems.Select(0);
					dgItems.CurrentRowIndex=0;
				}
			}
		}
        private void InsertDebitPayment(Data.Payment pvtclsPayment, long pvtlngTransactionID, string pvtstrTransactionNo, decimal pvtdecAmount, string pvtstrRemarks)
		{
			Data.DebitPaymentDetails Details = new Data.DebitPaymentDetails();
			Details.TransactionID = pvtlngTransactionID;
            Details.TransactionNo = pvtstrTransactionNo;
			Details.Amount = pvtdecAmount;
            Details.CustomerDetails = mclsCustomerDetails;
			Details.Remarks = pvtstrRemarks;

			pvtclsPayment.InsertDebitPayment(Details);
            pvtclsPayment.UpdateDebit(mclsCustomerDetails.ContactID, pvtlngTransactionID, pvtstrTransactionNo, pvtdecAmount, pvtstrRemarks);
		}
		
		#endregion

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        
	}
}
