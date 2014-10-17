using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class CreditsItemizeWnd : Form
	{
		private PictureBox imgIcon;
		private GroupBox groupBox1;
		private Label lblBalanceName;
        private Label lblBalance;
		private Label lblHeader1;
		private Label lblHeader;
		private Button cmdCancel;
		private Button cmdEnter;
		private System.ComponentModel.Container components = null;
        private DataGridView dgvItems;
        private Label label1;
        private Label lblBalanceSelected;

        private decimal mdecAmountPaid;
        private decimal mdecCashPayment;
        private decimal mdecChequePayment;
        private decimal mdecCreditCardPayment;
        private decimal mdecDebitPayment;
        private decimal mdecBalanceAmount;
        private decimal mdecChangeAmount;

        public decimal AmountPayment
        {
            get { return mdecAmountPaid; }
        }
        public decimal CashPayment
        {
            get { return mdecCashPayment; }
        }
        public decimal ChequePayment
        {
            get { return mdecChequePayment; }
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

        private PaymentTypes mPaymentType = PaymentTypes.NotYetAssigned;
        public PaymentTypes PaymentType
        {
            get
            { return mPaymentType; }
        }

        private ArrayList marrCashPaymentDetails = new ArrayList();
        public ArrayList CashPaymentDetails
        {
            get
            {
                return marrCashPaymentDetails;
            }
        }

        private ArrayList marrChequePaymentDetails = new ArrayList();
        public ArrayList ChequePaymentDetails
        {
            get
            {
                return marrChequePaymentDetails;
            }
        }

        private ArrayList marrCreditCardPaymentDetails = new ArrayList();
        public ArrayList CreditCardPaymentDetails
        {
            get { return marrCreditCardPaymentDetails; }
        }

        private ArrayList marrDebitPaymentDetails = new ArrayList();
        public ArrayList DebitPaymentDetails
        {
            get { return marrDebitPaymentDetails; }
        }

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private Data.ContactDetails mclsCustomerDetails;
        public Data.ContactDetails CustomerDetails
        {
            set { mclsCustomerDetails = value; }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set { mclsTerminalDetails = value; }
        }

		#region Constructors and Destuctors

		public CreditsItemizeWnd()
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBalanceSelected = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
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
            this.groupBox1.Controls.Add(this.dgvItems);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1009, 391);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select all transactions to pay. Use {Arrow Keys} for single selection or {Shift +" +
    " Space Bar} and {Ctrl + Click} for multiple selection.";
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItems.ColumnHeadersHeight = 24;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItems.GridColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(8, 24);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(994, 361);
            this.dgvItems.TabIndex = 56;
            this.dgvItems.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvItems_RowStateChanged);
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
            this.lblBalanceName.Location = new System.Drawing.Point(446, 475);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(255, 29);
            this.lblBalanceName.TabIndex = 86;
            this.lblBalanceName.Text = "TOTAL CREDIT BALANCE";
            this.lblBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(667, 477);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(351, 25);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(590, 29);
            this.label1.TabIndex = 89;
            this.label1.Text = "AMOUNT TO PAY NOW (SUM OF SELECTED TRANSACTIONS)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalanceSelected
            // 
            this.lblBalanceSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceSelected.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceSelected.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalanceSelected.Location = new System.Drawing.Point(667, 514);
            this.lblBalanceSelected.Name = "lblBalanceSelected";
            this.lblBalanceSelected.Size = new System.Drawing.Size(351, 25);
            this.lblBalanceSelected.TabIndex = 90;
            this.lblBalanceSelected.Text = "0.00";
            this.lblBalanceSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CreditsItemizeWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBalanceSelected);
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
            this.Name = "CreditsItemizeWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditsItemizeWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditsItemizeWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		#endregion

		#region Windows Form Methods

		private void CreditsItemizeWnd_Load(object sender, System.EventArgs e)
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
		private void CreditsItemizeWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					this.Hide(); 
					break;

				case Keys.Enter:
					if (Convert.ToDecimal(lblBalanceSelected.Text) > 0)
					{
                        Data.Products clsProducts = new Data.Products();
                        if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_CREDITPAYMENT_PRODUCT();
                            Methods.InsertAuditLog(mclsTerminalDetails, "System Administrator", AccessTypes.EnterCreditPayment, "CREDIT PAYMENT product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();
                        
                        dialog = ShowPayment();
                        if (dialog == DialogResult.OK)
                            this.Hide();
					}
					else
						this.Hide();

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
			if (Convert.ToDecimal(lblBalanceSelected.Text) > 0)
			{
				dialog = ShowPayment();
				if (dialog == DialogResult.OK)
					this.Hide();
			}
			else
				this.Hide();
		}
        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void dgvItems_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            decimal decTotalPayable = 0;
            decimal decTemp = 0;

            foreach (DataGridViewRow dr in dgvItems.SelectedRows)
            {
                decTemp = 0;
                decimal.TryParse(dr.Cells["Balance"].Value.ToString(), out decTemp);
                decTotalPayable += decTemp;
            }
            lblBalanceSelected.Text = decTotalPayable.ToString("#,##0.#0");
        }

		#endregion

		#region Private Modifiers

		private void LoadOptions()
		{

        }
		
		private void LoadData()
		{	
			try
			{
				Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt = clsTransactions.ListForPaymentDataTable(mclsCustomerDetails.ContactID);

				clsTransactions.CommitAndDispose();

                dgvItems.MultiSelect = true;
                dgvItems.AutoGenerateColumns = true;
                dgvItems.AutoSize = false;
                dgvItems.DataSource = dt.TableName;
                dgvItems.DataSource = dt;

                foreach (DataGridViewTextBoxColumn dc in dgvItems.Columns)
                {
                    dc.Visible = false;
                }
                dgvItems.Columns["TransactionNo"].Visible = true;
                dgvItems.Columns["TransactionDate"].Visible = true;
                dgvItems.Columns["CreditReason"].Visible = true;
                dgvItems.Columns["SubTotal"].Visible = true;
                dgvItems.Columns["Credit"].Visible = true;
                dgvItems.Columns["CreditPaid"].Visible = true;
                dgvItems.Columns["Balance"].Visible = true;

                dgvItems.Columns["TransactionNo"].Width = 120;
                dgvItems.Columns["TransactionDate"].Width = 120;
                dgvItems.Columns["CreditReason"].Width = 240;
                int iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["CreditReason"].Width) / 4;
                dgvItems.Columns["SubTotal"].Width = iWidth;
                dgvItems.Columns["Credit"].Width = iWidth;
                dgvItems.Columns["CreditPaid"].Width = iWidth;
                dgvItems.Columns["Balance"].Width = iWidth;

                dgvItems.Columns["TransactionNo"].HeaderText = "Trans. No";
                dgvItems.Columns["TransactionDate"].HeaderText = "Trans. Date";
                dgvItems.Columns["CreditReason"].HeaderText = "Description";
                dgvItems.Columns["SubTotal"].HeaderText = "Subtotal";
                dgvItems.Columns["Credit"].HeaderText = "Credit";
                dgvItems.Columns["CreditPaid"].HeaderText = "Credit Paid";
                dgvItems.Columns["Balance"].HeaderText = "Balance";

                dgvItems.Columns["SubTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Credit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["CreditPaid"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Balance"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvItems.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["CreditPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvItems.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt";
                dgvItems.Columns["SubTotal"].DefaultCellStyle.Format = "#,##0.#0";
                dgvItems.Columns["Credit"].DefaultCellStyle.Format = "#,##0.#0";
                dgvItems.Columns["CreditPaid"].DefaultCellStyle.Format = "#,##0.#0";
                dgvItems.Columns["Balance"].DefaultCellStyle.Format = "#,##0.#0";

                dgvItems.ReadOnly = true;
                dgvItems.Select();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}
		private DialogResult ShowPayment()
		{
			DialogResult paymentResult = DialogResult.Cancel;

			if (Convert.ToDecimal(lblBalanceSelected.Text) > 0)
			{
                Data.SalesTransactionDetails clsSalesTransactionDetails = new Data.SalesTransactionDetails();
                clsSalesTransactionDetails.SubTotal = Convert.ToDecimal(lblBalanceSelected.Text);

                PaymentsWnd payment = new PaymentsWnd();
                payment.TerminalDetails = mclsTerminalDetails;
                payment.CustomerDetails = mclsCustomerDetails;
                payment.SalesTransactionDetails = clsSalesTransactionDetails;
                payment.CreditCardSwiped = false;
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
		private void SaveCashPayment(Data.Payment clsPayment, ArrayList pvtarrCashPaymentDetails)
		{
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

                foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                {
                    Int64 lngTransactionID = Convert.ToInt64(dr.Cells["TransactionID"].Value.ToString());
                    string strTransactionNo = dr.Cells["TransactionNo"].Value.ToString();
                    decimal decBalance = Convert.ToDecimal(dr.Cells["Balance"].Value.ToString());

                    if (decRemainingAmountPaid >= decBalance)
                    {
                        InsertCashPayment(clsPayment, lngTransactionID, strTransactionNo, decBalance, strRemarks);

                        dr.Cells["CreditPaid"].Value = decBalance;
                        dr.Cells["Balance"].Value = 0;

                        decRemainingAmountPaid -= decBalance;
                    }
                    else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
                    {
                        InsertCashPayment(clsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, strRemarks);

                        //dgvItems.Select(itemIndex);
                        dr.Cells["CreditPaid"].Value = decRemainingAmountPaid;
                        dr.Cells["Balance"].Value = decBalance - decRemainingAmountPaid;
                        decRemainingAmountPaid = 0;
                        break;
                    }
                }
			}
		}
		private void InsertCashPayment(Data.Payment clsPayment, Int64 intTransactionID, string strTransactionNo, decimal decAmount, string strRemarks)
		{
			Data.CashPaymentDetails Details = new Data.CashPaymentDetails();
            Details.BranchDetails = mclsTerminalDetails.BranchDetails;
            Details.TerminalNo = mclsTerminalDetails.TerminalNo;
			Details.TransactionID = intTransactionID;
			Details.TransactionNo = strTransactionNo;
			Details.Amount = decAmount;
			Details.Remarks = strRemarks;

            new Data.CashPayments(clsPayment.Connection, clsPayment.Transaction).Insert(Details);
			clsPayment.UpdateCredit(mclsCustomerDetails.ContactID, intTransactionID, strTransactionNo, decAmount, strRemarks);
		}
		private void SaveChequePayment(Data.Payment clsPayment, ArrayList pvtarrChequePaymentDetails)
		{
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
				
				foreach(DataGridViewRow dr in dgvItems.SelectedRows) 
				{
					Int64 lngTransactionID = Convert.ToInt64(dr.Cells["TransactionID"].Value.ToString());
					string strTransactionNo = dr.Cells["TransactionNo"].Value.ToString();
					decimal decBalance = Convert.ToDecimal(dr.Cells["Balance"].Value.ToString());

					if (decRemainingAmountPaid >= decBalance)
					{
						InsertChequePayment(clsPayment, lngTransactionID, strTransactionNo, det.ChequeNo, decBalance, det.ValidityDate, strRemarks);

						dr.Cells["CreditPaid"].Value = decBalance;
						dr.Cells["Balance"].Value = 0;

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
						InsertChequePayment(clsPayment, lngTransactionID, strTransactionNo, det.ChequeNo, decRemainingAmountPaid, det.ValidityDate, strRemarks);

                        //dgItems.Select(itemIndex);
						dr.Cells["CreditPaid"].Value = decRemainingAmountPaid;
						dr.Cells["Balance"].Value = decBalance - decRemainingAmountPaid;
						decRemainingAmountPaid =0;
						break;
					}
				}
			}
		}
		private void InsertChequePayment(Data.Payment clsPayment, Int64 intTransactionID, string strTransactionNo, string strChequeNo, decimal decAmount, DateTime dteValidityDate, string strRemarks)
		{
			Data.ChequePaymentDetails Details = new Data.ChequePaymentDetails();
            Details.BranchDetails = mclsTerminalDetails.BranchDetails;
            Details.TerminalNo = mclsTerminalDetails.TerminalNo;
			Details.TransactionID = intTransactionID;
			Details.TransactionNo = strTransactionNo;
			Details.ChequeNo = strChequeNo;
			Details.Amount = decAmount;
			Details.ValidityDate = dteValidityDate;
			Details.Remarks = strRemarks;

            new Data.ChequePayments(clsPayment.Connection, clsPayment.Transaction).Insert(Details);
			clsPayment.UpdateCredit(mclsCustomerDetails.ContactID, intTransactionID, strTransactionNo, decAmount, strRemarks);
		}
		private void SaveCreditCardPayment(Data.Payment clsPayment, ArrayList pvtarrCreditCardPaymentDetails)
		{
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
				
				foreach(DataGridViewRow dr in dgvItems.SelectedRows) 
				{
					Int64 lngTransactionID = Convert.ToInt64(dr.Cells["TransactionID"].Value.ToString());
					string strTransactionNo = dr.Cells["TransactionNo"].Value.ToString();
					decimal decBalance = Convert.ToDecimal(dr.Cells["Balance"].Value.ToString());

					if (decRemainingAmountPaid >= decBalance)
					{
						InsertCreditCardPayment(clsPayment, lngTransactionID, strTransactionNo, decBalance, det.CardTypeID, det.CardNo, det.CardHolder, det.ValidityDates, strRemarks);

                        //dgItems.Select(itemIndex);
						dr.Cells["CreditPaid"].Value = decBalance;
						dr.Cells["Balance"].Value = 0;

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
						InsertCreditCardPayment(clsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, det.CardTypeID, det.CardNo, det.CardHolder, det.ValidityDates, strRemarks);

                        //dgItems.Select(itemIndex);
						dr.Cells["CreditPaid"].Value = decRemainingAmountPaid;
						dr.Cells["Balance"].Value = decBalance - decRemainingAmountPaid;
						decRemainingAmountPaid =0;
						break;
					}
				}
			}
		}
		private void InsertCreditCardPayment(Data.Payment clsPayment, Int64 intTransactionID, string strTransactionNo,  decimal decAmount, Int16 intCardTypeID, string strCardNo, string strCardHolder, string strValidityDates, string strRemarks)
		{
			Data.CardType clsCardType = new Data.CardType(clsPayment.Connection, clsPayment.Transaction);
			Data.CardTypeDetails clsCardTypeDetails = clsCardType.Details(intCardTypeID);

			Data.CreditCardPaymentDetails Details = new Data.CreditCardPaymentDetails();
            Details.BranchDetails = mclsTerminalDetails.BranchDetails;
            Details.TerminalNo = mclsTerminalDetails.TerminalNo;
			Details.TransactionID = intTransactionID;
			Details.TransactionNo = strTransactionNo;
			Details.Amount = decAmount;
			Details.CardTypeID = intCardTypeID;
			Details.CardNo = strCardNo;
			Details.CardTypeCode = clsCardTypeDetails.CardTypeCode;
			Details.CardTypeName = clsCardTypeDetails.CardTypeName;
			Details.CardHolder = strCardHolder;
			Details.ValidityDates = strValidityDates;
			Details.Remarks = strRemarks;

            new Data.CreditCardPayments(clsPayment.Connection, clsPayment.Transaction).Insert(Details);
			clsPayment.UpdateCredit(mclsCustomerDetails.ContactID, intTransactionID, strTransactionNo, decAmount, strRemarks);
		}
		private void SaveDebitPayment(Data.Payment clsPayment, ArrayList pvtarrDebitPaymentDetails)
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

                foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                {
                    Int64 lngTransactionID = Convert.ToInt64(dr.Cells["TransactionID"].Value.ToString());
                    string strTransactionNo = dr.Cells["TransactionNo"].Value.ToString();
                    decimal decBalance = Convert.ToDecimal(dr.Cells["Balance"].Value.ToString());

					if (decRemainingAmountPaid >= decBalance)
					{
						InsertDebitPayment(clsPayment, lngTransactionID, strTransactionNo, decBalance, strRemarks);

                        //dgItems.Select(itemIndex);
						dr.Cells["CreditPaid"].Value = decBalance;
						dr.Cells["Balance"].Value = 0;

						decRemainingAmountPaid -= decBalance;
					}
					else if (decRemainingAmountPaid > 0 && decRemainingAmountPaid < decBalance)
					{
						InsertDebitPayment(clsPayment, lngTransactionID, strTransactionNo, decRemainingAmountPaid, strRemarks);

                        //dgItems.Select(itemIndex);
						dr.Cells["CreditPaid"].Value = decRemainingAmountPaid;
						dr.Cells["Balance"].Value = decBalance - decRemainingAmountPaid;
						decRemainingAmountPaid =0;
						break;
					}
					itemIndex++;
				}

                //this.dgStyle.MappingName = dt.TableName;
                //dgItems.DataSource = dt;
                //if (dt.Rows.Count > 0)
                //{
                //    dgItems.Select(0);
                //    dgItems.CurrentRowIndex=0;
                //}
			}
		}
		private void InsertDebitPayment(Data.Payment clsPayment, Int64 intTransactionID, string strTransactionNo, decimal decAmount, string strRemarks)
		{
			Data.DebitPaymentDetails Details = new Data.DebitPaymentDetails();
			Details.TransactionID = intTransactionID;
			Details.TransactionNo = strTransactionNo;
			Details.Amount = decAmount;
			Details.CustomerDetails = mclsCustomerDetails;
			Details.Remarks = strRemarks;

			clsPayment.InsertDebitPayment(Details);
			clsPayment.UpdateDebit(mclsCustomerDetails.ContactID, intTransactionID, strTransactionNo, decAmount, strRemarks);
		}
		
		#endregion

	}
}
