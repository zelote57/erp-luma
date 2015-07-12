using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Management;
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
        private Label labelBalanceSelected;
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

        private PaymentTypes mPaymentType;
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

        public Int64 CashierID { get; set; }
        public string CashierName { get; set; }

        private Data.ContactDetails mclsCustomerDetails;
        public Data.ContactDetails CustomerDetails
        {
            set { mclsCustomerDetails = value; }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        private Data.SysConfigDetails mclsSysConfigDetails;
        private Label labelAmountDue;
        private Label lblAmountDue;
        private Label labelF4;
        private Label labelF3;
        private Label label12;
        private Label lblF4;
        private Label lblF3;
        private Label label6;
        private Label lblAddNewCustomer;
        private Label labelF5;
        private Label lblF5;
        private GroupBox grpSearch;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label4;
        private Label label3;
        private TextBox txtTrxEndDate;
        private TextBox txtTrxStartDate;
        private Label labelF11;
        private Label lblF11;

        private Keys mKeyData;
        private Label labelF12;
        private Label lblF12;
    
        public Keys KeyData
        {
            get
            {
                return mKeyData;
            }
        }

        public string TransactionNoToReprint { get; set; }
        public string TerminalNoToReprint { get; set; }

        public Data.SysConfigDetails SysConfigDetails
        {
            set { mclsSysConfigDetails = value; }
        }

        private DataGridViewSelectedRowCollection mdgvItemsSelectedRows;
        public DataGridViewSelectedRowCollection dgvItemsSelectedRows
        {
            get { return mdgvItemsSelectedRows; }
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
            if (TerminalDetails.MultiInstanceEnabled)
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.labelBalanceSelected = new System.Windows.Forms.Label();
            this.lblBalanceSelected = new System.Windows.Forms.Label();
            this.labelAmountDue = new System.Windows.Forms.Label();
            this.lblAmountDue = new System.Windows.Forms.Label();
            this.labelF4 = new System.Windows.Forms.Label();
            this.labelF3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblF4 = new System.Windows.Forms.Label();
            this.lblF3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
            this.labelF5 = new System.Windows.Forms.Label();
            this.lblF5 = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTrxEndDate = new System.Windows.Forms.TextBox();
            this.txtTrxStartDate = new System.Windows.Forms.TextBox();
            this.labelF11 = new System.Windows.Forms.Label();
            this.lblF11 = new System.Windows.Forms.Label();
            this.labelF12 = new System.Windows.Forms.Label();
            this.lblF12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.grpSearch.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeight = 24;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItems.GridColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(8, 24);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle3;
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
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(116, 475);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(585, 29);
            this.lblBalanceName.TabIndex = 86;
            this.lblBalanceName.Text = "TOTAL CREDIT BALANCE";
            this.lblBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(700, 477);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(318, 25);
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
            // labelBalanceSelected
            // 
            this.labelBalanceSelected.AutoSize = true;
            this.labelBalanceSelected.BackColor = System.Drawing.Color.Transparent;
            this.labelBalanceSelected.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBalanceSelected.Location = new System.Drawing.Point(111, 512);
            this.labelBalanceSelected.Name = "labelBalanceSelected";
            this.labelBalanceSelected.Size = new System.Drawing.Size(590, 29);
            this.labelBalanceSelected.TabIndex = 89;
            this.labelBalanceSelected.Text = "AMOUNT TO PAY NOW (SUM OF SELECTED TRANSACTIONS)";
            this.labelBalanceSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalanceSelected
            // 
            this.lblBalanceSelected.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceSelected.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceSelected.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalanceSelected.Location = new System.Drawing.Point(707, 514);
            this.lblBalanceSelected.Name = "lblBalanceSelected";
            this.lblBalanceSelected.Size = new System.Drawing.Size(311, 25);
            this.lblBalanceSelected.TabIndex = 90;
            this.lblBalanceSelected.Text = "0.00";
            this.lblBalanceSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAmountDue
            // 
            this.labelAmountDue.BackColor = System.Drawing.Color.Transparent;
            this.labelAmountDue.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmountDue.Location = new System.Drawing.Point(116, 553);
            this.labelAmountDue.Name = "labelAmountDue";
            this.labelAmountDue.Size = new System.Drawing.Size(585, 29);
            this.labelAmountDue.TabIndex = 91;
            this.labelAmountDue.Text = "AMOUNT DUE";
            this.labelAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelAmountDue.Visible = false;
            // 
            // lblAmountDue
            // 
            this.lblAmountDue.BackColor = System.Drawing.Color.Transparent;
            this.lblAmountDue.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountDue.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAmountDue.Location = new System.Drawing.Point(705, 555);
            this.lblAmountDue.Name = "lblAmountDue";
            this.lblAmountDue.Size = new System.Drawing.Size(313, 25);
            this.lblAmountDue.TabIndex = 92;
            this.lblAmountDue.Text = "0.00";
            this.lblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAmountDue.Visible = false;
            // 
            // labelF4
            // 
            this.labelF4.AutoSize = true;
            this.labelF4.BackColor = System.Drawing.Color.Transparent;
            this.labelF4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF4.Location = new System.Drawing.Point(874, 41);
            this.labelF4.Name = "labelF4";
            this.labelF4.Size = new System.Drawing.Size(98, 13);
            this.labelF4.TabIndex = 111;
            this.labelF4.Text = " to show payments";
            // 
            // labelF3
            // 
            this.labelF3.AutoSize = true;
            this.labelF3.BackColor = System.Drawing.Color.Transparent;
            this.labelF3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF3.Location = new System.Drawing.Point(874, 25);
            this.labelF3.Name = "labelF3";
            this.labelF3.Size = new System.Drawing.Size(100, 13);
            this.labelF3.TabIndex = 110;
            this.labelF3.Text = " to show purchases";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(800, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 109;
            this.label12.Text = "Press";
            // 
            // lblF4
            // 
            this.lblF4.BackColor = System.Drawing.Color.Transparent;
            this.lblF4.ForeColor = System.Drawing.Color.Red;
            this.lblF4.Location = new System.Drawing.Point(835, 41);
            this.lblF4.Name = "lblF4";
            this.lblF4.Size = new System.Drawing.Size(27, 13);
            this.lblF4.TabIndex = 108;
            this.lblF4.Text = "[F4]";
            // 
            // lblF3
            // 
            this.lblF3.BackColor = System.Drawing.Color.Transparent;
            this.lblF3.ForeColor = System.Drawing.Color.Red;
            this.lblF3.Location = new System.Drawing.Point(835, 25);
            this.lblF3.Name = "lblF3";
            this.lblF3.Size = new System.Drawing.Size(27, 13);
            this.lblF3.TabIndex = 107;
            this.lblF3.Text = "[F3]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(834, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 106;
            this.label6.Text = "[Enter]";
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(874, 9);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(99, 13);
            this.lblAddNewCustomer.TabIndex = 105;
            this.lblAddNewCustomer.Text = " to enter payments";
            // 
            // labelF5
            // 
            this.labelF5.AutoSize = true;
            this.labelF5.BackColor = System.Drawing.Color.Transparent;
            this.labelF5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF5.Location = new System.Drawing.Point(657, 41);
            this.labelF5.Name = "labelF5";
            this.labelF5.Size = new System.Drawing.Size(74, 13);
            this.labelF5.TabIndex = 113;
            this.labelF5.Text = " Filters search";
            this.labelF5.Visible = false;
            // 
            // lblF5
            // 
            this.lblF5.BackColor = System.Drawing.Color.Transparent;
            this.lblF5.ForeColor = System.Drawing.Color.Red;
            this.lblF5.Location = new System.Drawing.Point(618, 41);
            this.lblF5.Name = "lblF5";
            this.lblF5.Size = new System.Drawing.Size(27, 13);
            this.lblF5.TabIndex = 112;
            this.lblF5.Text = "[F5]";
            this.lblF5.Visible = false;
            // 
            // grpSearch
            // 
            this.grpSearch.BackColor = System.Drawing.Color.White;
            this.grpSearch.Controls.Add(this.checkBox2);
            this.grpSearch.Controls.Add(this.checkBox1);
            this.grpSearch.Controls.Add(this.label4);
            this.grpSearch.Controls.Add(this.label3);
            this.grpSearch.Controls.Add(this.txtTrxEndDate);
            this.grpSearch.Controls.Add(this.txtTrxStartDate);
            this.grpSearch.ForeColor = System.Drawing.Color.Blue;
            this.grpSearch.Location = new System.Drawing.Point(9, 81);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(1009, 391);
            this.grpSearch.TabIndex = 114;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Press [Enter ] to search or [Esc] to hide this filter.";
            this.grpSearch.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(238, 112);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(117, 17);
            this.checkBox2.TabIndex = 115;
            this.checkBox2.Text = "Show Consignment";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(44, 112);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(159, 17);
            this.checkBox1.TabIndex = 115;
            this.checkBox1.Text = "Show Accounts Receivables";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(41, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Start transaction date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(41, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 112;
            this.label3.Text = "Ending transaction date :";
            // 
            // txtTrxEndDate
            // 
            this.txtTrxEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrxEndDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrxEndDate.Location = new System.Drawing.Point(238, 71);
            this.txtTrxEndDate.Name = "txtTrxEndDate";
            this.txtTrxEndDate.Size = new System.Drawing.Size(136, 23);
            this.txtTrxEndDate.TabIndex = 3;
            // 
            // txtTrxStartDate
            // 
            this.txtTrxStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrxStartDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrxStartDate.Location = new System.Drawing.Point(238, 42);
            this.txtTrxStartDate.Name = "txtTrxStartDate";
            this.txtTrxStartDate.Size = new System.Drawing.Size(136, 23);
            this.txtTrxStartDate.TabIndex = 2;
            // 
            // labelF11
            // 
            this.labelF11.AutoSize = true;
            this.labelF11.BackColor = System.Drawing.Color.Transparent;
            this.labelF11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF11.Location = new System.Drawing.Point(657, 25);
            this.labelF11.Name = "labelF11";
            this.labelF11.Size = new System.Drawing.Size(139, 13);
            this.labelF11.TabIndex = 116;
            this.labelF11.Text = "Print Statement Of Account";
            this.labelF11.Visible = false;
            // 
            // lblF11
            // 
            this.lblF11.BackColor = System.Drawing.Color.Transparent;
            this.lblF11.ForeColor = System.Drawing.Color.Red;
            this.lblF11.Location = new System.Drawing.Point(618, 25);
            this.lblF11.Name = "lblF11";
            this.lblF11.Size = new System.Drawing.Size(34, 13);
            this.lblF11.TabIndex = 115;
            this.lblF11.Text = "[F11]";
            this.lblF11.Visible = false;
            // 
            // labelF12
            // 
            this.labelF12.AutoSize = true;
            this.labelF12.BackColor = System.Drawing.Color.Transparent;
            this.labelF12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF12.Location = new System.Drawing.Point(657, 9);
            this.labelF12.Name = "labelF12";
            this.labelF12.Size = new System.Drawing.Size(142, 13);
            this.labelF12.TabIndex = 118;
            this.labelF12.Text = "Reprint selected transaction";
            this.labelF12.Visible = false;
            // 
            // lblF12
            // 
            this.lblF12.BackColor = System.Drawing.Color.Transparent;
            this.lblF12.ForeColor = System.Drawing.Color.Red;
            this.lblF12.Location = new System.Drawing.Point(618, 9);
            this.lblF12.Name = "lblF12";
            this.lblF12.Size = new System.Drawing.Size(34, 13);
            this.lblF12.TabIndex = 117;
            this.lblF12.Text = "[F12]";
            this.lblF12.Visible = false;
            // 
            // CreditsItemizeWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.labelF12);
            this.Controls.Add(this.lblF12);
            this.Controls.Add(this.labelF11);
            this.Controls.Add(this.lblF11);
            this.Controls.Add(this.labelF5);
            this.Controls.Add(this.lblF5);
            this.Controls.Add(this.labelF4);
            this.Controls.Add(this.labelF3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblF4);
            this.Controls.Add(this.lblF3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.labelAmountDue);
            this.Controls.Add(this.lblAmountDue);
            this.Controls.Add(this.labelBalanceSelected);
            this.Controls.Add(this.lblBalanceSelected);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblBalanceName);
            this.Controls.Add(this.lblHeader1);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.grpSearch);
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
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
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

			LoadOptions();
			LoadData();
		}
		private void CreditsItemizeWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
                    if (grpSearch.Visible)
                    {
                        grpSearch.Visible = false;
                        dgvItems.Focus();
                        cmdEnter.Text = "Enter";
                    }
                    else
                    {
                        this.Hide();
                    }
					break;

				case Keys.Enter:
                    if (grpSearch.Visible)
                    {
                        LoadData();
                    }
					else if (Convert.ToDecimal(lblBalanceSelected.Text) > 0)
					{
                        Data.Products clsProducts = new Data.Products();
                        if (clsProducts.Details(Data.Products.DEFAULT_CREDIT_PAYMENT_BARCODE).ProductID == 0)
                        {
                            clsProducts.CREATE_CREDITPAYMENT_PRODUCT();
                            Methods.InsertAuditLog(TerminalDetails, "System Administrator", AccessTypes.EnterCreditPayment, "CREDIT PAYMENT product has been created coz it's not configured");
                        }
                        clsProducts.CommitAndDispose();
                        
                        dialog = ShowPayment();
                        if (dialog == DialogResult.OK)
                        { mKeyData = Keys.Enter; this.Hide(); }
					}
					else
						this.Hide();

					break;

                case Keys.F3:
                    CreditSalesWnd clsCreditSalesWnd = new CreditSalesWnd();
                    clsCreditSalesWnd.TerminalDetails = TerminalDetails;
                    clsCreditSalesWnd.SysConfigDetails = mclsSysConfigDetails;
                    clsCreditSalesWnd.CustomerDetails = mclsCustomerDetails;
                    clsCreditSalesWnd.ShowDialog(this);
                    clsCreditSalesWnd.Close();
                    clsCreditSalesWnd.Dispose();
                    break;

                case Keys.F4:
                    CreditPaymentsWnd clsCreditPaymentsWnd = new CreditPaymentsWnd();
                    clsCreditPaymentsWnd.TerminalDetails = TerminalDetails;
                    clsCreditPaymentsWnd.SysConfigDetails = mclsSysConfigDetails;
                    clsCreditPaymentsWnd.CustomerDetails = mclsCustomerDetails;
                    clsCreditPaymentsWnd.CashierID = CashierID;
                    clsCreditPaymentsWnd.ShowDialog(this);
                    DialogResult result = clsCreditPaymentsWnd.Result;
                    mclsCustomerDetails = clsCreditPaymentsWnd.CustomerDetails;
                    clsCreditPaymentsWnd.Close();
                    clsCreditPaymentsWnd.Dispose();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadData();
                    }
                    break;

                case Keys.F5:
                    if (!lblF5.Visible) return;
                    grpSearch.Visible = !grpSearch.Visible;
                    if (grpSearch.Visible)
                    {
                        txtTrxStartDate.Focus();
                        cmdEnter.Text = "Search";
                    }
                    else
                    {
                        dgvItems.Focus();
                        cmdEnter.Text = "Enter";
                    }
                    break;
                case Keys.F11:
                    if (!lblF11.Visible) return;
                    if (dgvItems.Rows.Count > 0)
                    {
                        PrintSOA();
                    }
                    break;
                case Keys.F12:
                    if (!lblF12.Visible) return;
                    TransactionNoToReprint = ""; TerminalNoToReprint = "";
                    foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                    {
                        TransactionNoToReprint = dr.Cells["TransactionNo"].Value.ToString();
                        TerminalNoToReprint = dr.Cells["TerminalNo"].Value.ToString();
                        dialog = System.Windows.Forms.DialogResult.OK;
                        mKeyData = Keys.F12;
                        this.Hide();
                        break;
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
            if (grpSearch.Visible)
            {
                LoadData();
            }
			else if (Convert.ToDecimal(lblBalanceSelected.Text) > 0)
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
            lblF12.Visible = true; labelF12.Visible = true;
            switch (mclsSysConfigDetails.CreditPaymentType)
            {
                case CreditPaymentType.MPC:
                case CreditPaymentType.Normal:
                default:
                    lblF11.Visible = true; labelF11.Visible = true;
                    lblF5.Visible = true; labelF5.Visible = true;
                    txtTrxStartDate.Text = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd");
                    txtTrxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
            }
        }
		
		private void LoadData()
		{	
			try
			{
                DateTime dteRetValue = DateTime.MinValue;
                DateTime dtePaymentDateFrom = DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : DateTime.Now.AddYears(-2);
                DateTime dtePaymentDateTo = DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : DateTime.Now; 
                
                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
				System.Data.DataTable dt;

                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        dt = clsTransactions.ListForPaymentDataTable(mclsCustomerDetails.ContactID, "Balance, TransactionID", System.Data.SqlClient.SortOrder.Descending);
                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dt = clsTransactions.ListForPaymentDataTable(mclsCustomerDetails.ContactID, "TransactionNo", System.Data.SqlClient.SortOrder.Ascending, 0, dtePaymentDateFrom, dtePaymentDateTo);
                        break;
                }   
                //Data.Billing clsBilling = new Data.Billing(clsTransactions.Connection, clsTransactions.Transaction);
                //Data.BillingDetails clsBillingDetails = clsBilling.Details(mclsCustomerDetails.ContactID, false);

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
                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        dgvItems.Columns["CreditReason"].Visible = true;
                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dgvItems.Columns["Terms"].Visible = true;
                        dgvItems.Columns["ModeOfTermsCode"].Visible = true;
                        dgvItems.Columns["AgingDate"].Visible = true;
                        dgvItems.Columns["AgeTerms"].Visible = true;
                        break;
                }
                //
                dgvItems.Columns["SubTotal"].Visible = true;
                dgvItems.Columns["Credit"].Visible = true;
                dgvItems.Columns["CreditPaid"].Visible = true;
                dgvItems.Columns["Balance"].Visible = true;
                
                dgvItems.Columns["TransactionNo"].Width = 120;
                dgvItems.Columns["TransactionDate"].Width = 120;
                // do an override
                int iWidth = 100;
                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        if (dt.Rows.Count < 14) dgvItems.Columns["CreditReason"].Width = 240; else dgvItems.Columns["CreditReason"].Width = 210;
                        iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["CreditReason"].Width) / 4;
                        if (dt.Rows.Count >= 14) iWidth = iWidth - 5;

                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dgvItems.Columns["Terms"].Width = 60;
                        dgvItems.Columns["ModeOfTermsCode"].Width = 75;
                        dgvItems.Columns["AgingDate"].Width = 120;
                        if (dt.Rows.Count < 14) dgvItems.Columns["AgeTerms"].Width = 90; else dgvItems.Columns["AgeTerms"].Width = 60;

                        iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["Terms"].Width - dgvItems.Columns["ModeOfTermsCode"].Width - dgvItems.Columns["AgingDate"].Width - dgvItems.Columns["AgeTerms"].Width) / 4;
                        if (dt.Rows.Count >= 14) iWidth = iWidth - 5;
                        break;
                }
                dgvItems.Columns["SubTotal"].Width = iWidth;
                dgvItems.Columns["Credit"].Width = iWidth;
                dgvItems.Columns["CreditPaid"].Width = iWidth;
                dgvItems.Columns["Balance"].Width = iWidth;

                dgvItems.Columns["TransactionNo"].HeaderText = "Trans. No";
                dgvItems.Columns["TransactionDate"].HeaderText = "Trans. Date";
                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        dgvItems.Columns["CreditReason"].HeaderText = "Description";
                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dgvItems.Columns["Terms"].HeaderText = "Terms";
                        dgvItems.Columns["ModeOfTermsCode"].HeaderText = "ModeOfTerms";
                        dgvItems.Columns["AgingDate"].HeaderText = "Due Date";
                        dgvItems.Columns["AgeTerms"].HeaderText = "Age";

                        dgvItems.Columns["Terms"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvItems.Columns["Terms"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvItems.Columns["Terms"].DefaultCellStyle.Format = "#,##0";

                        dgvItems.Columns["AgingDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt";

                        dgvItems.Columns["AgeTerms"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvItems.Columns["AgeTerms"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvItems.Columns["AgeTerms"].DefaultCellStyle.Format = "#,##0";
                        break;
                }
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


                lblHeader.Text = mclsCustomerDetails.ContactName;
                lblBalance.Text = mclsCustomerDetails.Credit.ToString("#,##0.#0");

                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        // sort this so that the least amount will be on top
                        // for HP
                        dgvItems.Sort(dgvItems.Columns["Balance"], ListSortDirection.Descending);
                        dgvItems.SelectAll();
                        dgvItems_RowStateChanged(null, null);
                        dgvItems.Enabled = false;

                        Data.Billing clsBilling = new Data.Billing();
                        Data.BillingDetails clsBillingDetails = clsBilling.Details(mclsCustomerDetails.ContactID, mclsCustomerDetails.CreditDetails.LastBillingDate, false);
                        clsBilling.CommitAndDispose();

                        lblAmountDue.Visible = true;
                        labelAmountDue.Visible = true;
                        lblAmountDue.Text = "0.00";
                        if (clsBillingDetails.ContactID != 0)
                        {
                            if (mclsCustomerDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                            {
                                labelAmountDue.Text = "Amount Due";

                                if (decimal.Parse(lblBalance.Text) < clsBillingDetails.CurrentDueAmount)
                                {
                                    lblAmountDue.Text = lblBalance.Text;
                                }
                                else
                                {
                                    lblAmountDue.Text = clsBillingDetails.CurrentDueAmount.ToString("#,##0.#0");
                                }
                            }
                            else
                            {
                                labelAmountDue.Text = "Minimum Amount Due";
                                if (decimal.Parse(lblBalance.Text) < clsBillingDetails.MinimumAmountDue)
                                {
                                    lblAmountDue.Text = lblBalance.Text;
                                }
                                else
                                {
                                    lblAmountDue.Text = clsBillingDetails.MinimumAmountDue.ToString("#,##0.#0");
                                }
                            }
                        }
                        break;
                    case CreditPaymentType.Normal:
                    case CreditPaymentType.MPC:
                    default:
                        lblAmountDue.Visible = true;
                        labelAmountDue.Visible = true;

                        labelAmountDue.Text = "SubTotal Amount";
                        lblAmountDue.Text = mclsCustomerDetails.Credit.ToString("#,##0.#0");

                        decimal decTemp = 0;
                        decimal decTotalPayable = 0;
                        foreach (DataGridViewRow dr in dgvItems.Rows)
                        {
                            decTemp = 0;
                            decimal.TryParse(dr.Cells["Balance"].Value.ToString(), out decTemp);
                            decTotalPayable += decTemp;
                        }
                        lblAmountDue.Text = decTotalPayable.ToString("#,##0.#0");

                        labelAmountDue.Location = new Point(116, 475);
                        lblAmountDue.Location = new Point(700, 477);
                        lblBalanceName.Location = new Point(116, 553);
                        lblBalance.Location = new Point(705, 555);
                        break;
                }
                
                grpSearch.Visible = false;
                dgvItems.Focus();
                cmdEnter.Text = "Enter";
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
                switch (mclsSysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        clsSalesTransactionDetails.SubTotal = Convert.ToDecimal(lblAmountDue.Text);
                        break;
                    case CreditPaymentType.Normal:
                    case CreditPaymentType.MPC:
                    default:
                        clsSalesTransactionDetails.SubTotal = Convert.ToDecimal(lblBalanceSelected.Text);
                        break;
                }
                
                clsSalesTransactionDetails.TransactionStatus = TransactionStatus.CreditPayment;

                PaymentsWnd payment = new PaymentsWnd();
                payment.TerminalDetails = TerminalDetails;
                payment.SysConfigDetails = mclsSysConfigDetails;
                payment.CustomerDetails = mclsCustomerDetails;
                payment.SalesTransactionDetails = clsSalesTransactionDetails;
                payment.CreditCardSwiped = false;
                payment.IsRefund = false;
                payment.isFromCreditPayment = true;
                payment.ShowDialog(this);

                paymentResult = payment.Result;

                mdecAmountPaid = payment.AmountPaid;
                mdecCashPayment = payment.CashPayment;
                mdecChequePayment = payment.ChequePayment;
                mdecCreditCardPayment = payment.CreditCardPayment;
                mdecDebitPayment = payment.DebitPayment;
                mdecBalanceAmount = payment.BalanceAmount;
                mdecChangeAmount = payment.ChangeAmount;
                mPaymentType = payment.PaymentType;
                marrCashPaymentDetails = payment.CashPaymentDetails;
                marrChequePaymentDetails = payment.ChequePaymentDetails;
                marrCreditCardPaymentDetails = payment.CreditCardPaymentDetails;
                marrDebitPaymentDetails = payment.DebitPaymentDetails;
                payment.Close();
                payment.Dispose();

                if (paymentResult == DialogResult.OK)
                {
                    // Nov 2, 2014 do not save do the saving in MainWnd
                    // get the selected Transactions to be paid instead

                    //SavePayments(mdecAmountPaid, mdecCashPayment, mdecChequePayment, mdecCreditCardPayment, mdecDebitPayment,
                    //    marrCashPaymentDetails, marrChequePaymentDetails, marrCreditCardPaymentDetails, marrDebitPaymentDetails);

                    mdgvItemsSelectedRows = dgvItems.SelectedRows;
                }
			}
			return paymentResult;
		}

        private void PrintSOA()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                CRSReports.SOA rpt = new CRSReports.SOA();

                AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
                System.Data.DataRow drNew;

                /****************************report logo *****************************/
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/images/ReportLogo.jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "/images/ReportLogo.jpg");

                    byte[] propimg = new byte[fi.Length];
                    fs.Read(propimg, 0, Convert.ToInt32(fs.Length));
                    fs.Close();

                    drNew = rptds.CompanyLogo.NewRow(); drNew["Picture"] = propimg;
                    rptds.CompanyLogo.Rows.Add(drNew);
                }
                catch { }

                /****************************datatable*****************************/

                Data.Contacts clsContacts = new Data.Contacts();
                System.Data.DataTable dt = clsContacts.ListAsDataTable(ContactID: mclsCustomerDetails.ContactID);
                clsContacts.CommitAndDispose();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    drNew = rptds.Customer.NewRow();

                    foreach (System.Data.DataColumn dc in rptds.Customer.Columns)
                        drNew[dc] = dr[dc.ColumnName];

                    rptds.Customer.Rows.Add(drNew);
                }

                dt = (System.Data.DataTable) dgvItems.DataSource;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    drNew = rptds.CustomerCredit.NewRow();

                    foreach (System.Data.DataColumn dc in rptds.CustomerCredit.Columns)
                        drNew[dc] = dr[dc.ColumnName];

                    rptds.CustomerCredit.Rows.Add(drNew);
                }

                rpt.SetDataSource(rptds);

                CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
                CrystalDecisions.Shared.ParameterValues currentValues;
                CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

                paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
                discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                discreteParam.Value = CompanyDetails.CompanyName;
                currentValues = new CrystalDecisions.Shared.ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
                discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                discreteParam.Value = CashierName;
                currentValues = new CrystalDecisions.Shared.ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
                discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                discreteParam.Value = CashierName;
                currentValues = new CrystalDecisions.Shared.ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
                discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                discreteParam.Value = CompanyDetails.Address1 +
                    ((!string.IsNullOrEmpty(CompanyDetails.Address2) ? Environment.NewLine + CompanyDetails.Address2 + ", " : " ")) +
                    CompanyDetails.City + " " + CompanyDetails.Country +
                    ((!string.IsNullOrEmpty(CompanyDetails.OfficePhone) ? Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " " : " ")) +
                    ((!string.IsNullOrEmpty(CompanyDetails.OfficePhone) ? Environment.NewLine + "FaxPhone #: " + CompanyDetails.FaxPhone + " " : " "));
                currentValues = new CrystalDecisions.Shared.ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                paramField = rpt.DataDefinition.ParameterFields["BIRInfo"];
                discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                discreteParam.Value = "";
                currentValues = new CrystalDecisions.Shared.ParameterValues();
                currentValues.Add(discreteParam);
                paramField.ApplyCurrentValues(currentValues);

                //foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
                //{
                //    if (objPic.Name.ToUpper() == "PICLOGO1")
                //    {
                //        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
                //    }
                //}

                //CRViewer.Visible = true;
                //CRViewer.ReportSource = rpt;
                //CRViewer.Show();

                try
                {
                    DateTime logdate = DateTime.Now;
                    string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                    if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                    {
                        Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                    }
                    string logFile = logsdir + logdate.ToString("MMM") + "/SOA_" + logdate.ToString("yyyyMMddhhmmss") + ".doc";

                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
                }
                catch { }

                if (isPrinterOnline(TerminalDetails.SalesInvoicePrinterName))
                {
                    rpt.PrintOptions.PrinterName = TerminalDetails.SalesInvoicePrinterName;
                    rpt.PrintToPrinter(1, false, 0, 0);

                    rpt.Close();
                    rpt.Dispose();

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Statement Of Account for " + mclsCustomerDetails.ContactName + " has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    rpt.Close();
                    rpt.Dispose();

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Sorry, will not print sales invoice. printer is offline.", "RetailPlus");
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Sorry an error was encountered during printing, please reprint again." + Environment.NewLine + "Details: " + ex.Message, "RetailPlus");
            }
        }

        public bool isPrinterOnline(string objPrinterName)
        {
            bool boretValue = false;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    string printerName = printer["Name"].ToString().ToLower();

                    if (printerName == objPrinterName.ToLower())
                    {
                        boretValue = true;
                        break;  // exit the for loops
                    }
                    //Console.WriteLine("Printer".PadRight(15) + ":" + printerName);
                }
            }
            catch { }
            return boretValue;
        }

		#endregion

		#region Save Payments

        
		
		#endregion

	}
}
