using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Management;
using System.Windows.Forms;


namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for CreditVerificationWnd.
	/// </summary>
	public class CreditVerificationWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private Data.ContactDetails mContactDetails;
        private Data.ContactDetails mGuarantorDetails;
        private DialogResult dialog;
        private GroupBox grpContactDetails;
        private Label label3;
        private TextBox txtTelNo;
        private Label label1;
        private TextBox txtAddress;
        private Label lblCaption;
        private TextBox txtCustomerName;
        //private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private GroupBox grpPurchases;
        private Label label7;
        private TextBox txtAvailableCredit;
        private Label label4;
        private TextBox txtCredit;
        private Label label5;
        private TextBox txtCreditLimit;
        private TextBox txtScan;
        private Label label2;
        private TextBox txtCreditCardStatus;
        private Label labelCreditStatus;
        private DataGridView dgvItems;
        private Label lblBalanceName;
        private Label lblBalance;
        private Label lblAddNewCustomer;
        private Label label6;
        private Label label8;
        private TextBox txtRemarks;
        private Label label9;
        private TextBox txtMobileNo;
        private TextBox txtGuarantor;
        private Label labelGuarantor;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label lblF8;
        private Label lblF8Desc;
        private Label lblF8Press;
		private string mstCaption;
        private Label lblF7;
        private Label lblF7Desc;
        private Label lblF7Press;
        private Label labelF11;
        private Label lblF11;
        private bool mboCreditAmountDueAdjustment;

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

		public Data.ContactDetails CreditorDetails
		{
			get {	return mContactDetails;	}
		}

        public Data.ContactDetails GuarantorDetails
        {
            get { return mGuarantorDetails; }
        }

        public Keys keyCommand { get; set; }
        public Data.TerminalDetails TerminalDetails { get; set; }
        public Data.SysConfigDetails SysConfigDetails { get; set; }

        public Int64 CashierID { get; set; }
        public string CashierName { get; set; }

		#region Constructors And Desctructors
		public CreditVerificationWnd()
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

            if (TerminalDetails.MultiInstanceEnabled)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpContactDetails = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAvailableCredit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.txtCreditCardStatus = new System.Windows.Forms.TextBox();
            this.labelCreditStatus = new System.Windows.Forms.Label();
            this.txtGuarantor = new System.Windows.Forms.TextBox();
            this.labelGuarantor = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.grpPurchases = new System.Windows.Forms.GroupBox();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblF8 = new System.Windows.Forms.Label();
            this.lblF8Desc = new System.Windows.Forms.Label();
            this.lblF8Press = new System.Windows.Forms.Label();
            this.lblF7 = new System.Windows.Forms.Label();
            this.lblF7Desc = new System.Windows.Forms.Label();
            this.lblF7Press = new System.Windows.Forms.Label();
            this.labelF11 = new System.Windows.Forms.Label();
            this.lblF11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpContactDetails.SuspendLayout();
            this.grpPurchases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
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
            this.grpContactDetails.Controls.Add(this.label9);
            this.grpContactDetails.Controls.Add(this.txtMobileNo);
            this.grpContactDetails.Controls.Add(this.label8);
            this.grpContactDetails.Controls.Add(this.txtRemarks);
            this.grpContactDetails.Controls.Add(this.label3);
            this.grpContactDetails.Controls.Add(this.txtTelNo);
            this.grpContactDetails.Controls.Add(this.label1);
            this.grpContactDetails.Controls.Add(this.txtAddress);
            this.grpContactDetails.Controls.Add(this.lblCaption);
            this.grpContactDetails.Controls.Add(this.txtCustomerName);
            this.grpContactDetails.Controls.Add(this.label5);
            this.grpContactDetails.Controls.Add(this.txtAvailableCredit);
            this.grpContactDetails.Controls.Add(this.label7);
            this.grpContactDetails.Controls.Add(this.txtCreditLimit);
            this.grpContactDetails.Controls.Add(this.label4);
            this.grpContactDetails.Controls.Add(this.txtCredit);
            this.grpContactDetails.Controls.Add(this.txtCreditCardStatus);
            this.grpContactDetails.Controls.Add(this.labelCreditStatus);
            this.grpContactDetails.Controls.Add(this.txtGuarantor);
            this.grpContactDetails.Controls.Add(this.labelGuarantor);
            this.grpContactDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContactDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpContactDetails.Location = new System.Drawing.Point(9, 113);
            this.grpContactDetails.Name = "grpContactDetails";
            this.grpContactDetails.Size = new System.Drawing.Size(1008, 208);
            this.grpContactDetails.TabIndex = 0;
            this.grpContactDetails.TabStop = false;
            this.grpContactDetails.Text = "Creditor Details";
            this.grpContactDetails.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(627, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Mobile no.";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(731, 15);
            this.txtMobileNo.MaxLength = 75;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.ReadOnly = true;
            this.txtMobileNo.Size = new System.Drawing.Size(169, 30);
            this.txtMobileNo.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(71, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(174, 88);
            this.txtRemarks.MaxLength = 150;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ReadOnly = true;
            this.txtRemarks.Size = new System.Drawing.Size(726, 39);
            this.txtRemarks.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(627, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Telephone no.";
            // 
            // txtTelNo
            // 
            this.txtTelNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelNo.Location = new System.Drawing.Point(731, 48);
            this.txtTelNo.MaxLength = 75;
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.ReadOnly = true;
            this.txtTelNo.Size = new System.Drawing.Size(169, 30);
            this.txtTelNo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(71, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(174, 51);
            this.txtAddress.MaxLength = 150;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(439, 30);
            this.txtAddress.TabIndex = 3;
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
            this.label5.Location = new System.Drawing.Point(71, 169);
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
            this.txtAvailableCredit.Location = new System.Drawing.Point(731, 169);
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
            this.label7.Location = new System.Drawing.Point(627, 169);
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
            this.txtCreditLimit.Location = new System.Drawing.Point(174, 169);
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
            this.label4.Location = new System.Drawing.Point(356, 169);
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
            this.txtCredit.Location = new System.Drawing.Point(444, 169);
            this.txtCredit.MaxLength = 75;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(169, 30);
            this.txtCredit.TabIndex = 8;
            this.txtCredit.Text = "0.00";
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCreditCardStatus
            // 
            this.txtCreditCardStatus.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreditCardStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardStatus.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditCardStatus.Location = new System.Drawing.Point(731, 133);
            this.txtCreditCardStatus.MaxLength = 25;
            this.txtCreditCardStatus.Name = "txtCreditCardStatus";
            this.txtCreditCardStatus.ReadOnly = true;
            this.txtCreditCardStatus.Size = new System.Drawing.Size(269, 30);
            this.txtCreditCardStatus.TabIndex = 6;
            // 
            // labelCreditStatus
            // 
            this.labelCreditStatus.AutoSize = true;
            this.labelCreditStatus.BackColor = System.Drawing.SystemColors.Window;
            this.labelCreditStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreditStatus.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelCreditStatus.Location = new System.Drawing.Point(627, 143);
            this.labelCreditStatus.Name = "labelCreditStatus";
            this.labelCreditStatus.Size = new System.Drawing.Size(81, 13);
            this.labelCreditStatus.TabIndex = 10;
            this.labelCreditStatus.Text = "Credit Status";
            // 
            // txtGuarantor
            // 
            this.txtGuarantor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtGuarantor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGuarantor.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGuarantor.Location = new System.Drawing.Point(174, 133);
            this.txtGuarantor.MaxLength = 25;
            this.txtGuarantor.Name = "txtGuarantor";
            this.txtGuarantor.ReadOnly = true;
            this.txtGuarantor.Size = new System.Drawing.Size(439, 30);
            this.txtGuarantor.TabIndex = 18;
            // 
            // labelGuarantor
            // 
            this.labelGuarantor.AutoSize = true;
            this.labelGuarantor.BackColor = System.Drawing.SystemColors.Window;
            this.labelGuarantor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGuarantor.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelGuarantor.Location = new System.Drawing.Point(71, 133);
            this.labelGuarantor.Name = "labelGuarantor";
            this.labelGuarantor.Size = new System.Drawing.Size(65, 13);
            this.labelGuarantor.TabIndex = 19;
            this.labelGuarantor.Text = "Guarantor";
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
            // grpPurchases
            // 
            this.grpPurchases.BackColor = System.Drawing.Color.White;
            this.grpPurchases.Controls.Add(this.lblBalanceName);
            this.grpPurchases.Controls.Add(this.lblBalance);
            this.grpPurchases.Controls.Add(this.dgvItems);
            this.grpPurchases.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPurchases.ForeColor = System.Drawing.Color.Blue;
            this.grpPurchases.Location = new System.Drawing.Point(7, 327);
            this.grpPurchases.Name = "grpPurchases";
            this.grpPurchases.Size = new System.Drawing.Size(1010, 275);
            this.grpPurchases.TabIndex = 13;
            this.grpPurchases.TabStop = false;
            this.grpPurchases.Text = "Current Un-Paid Purchases";
            this.grpPurchases.Visible = false;
            // 
            // lblBalanceName
            // 
            this.lblBalanceName.AutoSize = true;
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(441, 241);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(255, 29);
            this.lblBalanceName.TabIndex = 88;
            this.lblBalanceName.Text = "TOTAL CREDIT BALANCE";
            this.lblBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(733, 243);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(269, 25);
            this.lblBalance.TabIndex = 89;
            this.lblBalance.Text = "0.00";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dgvItems.Location = new System.Drawing.Point(6, 20);
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
            this.dgvItems.Size = new System.Drawing.Size(996, 220);
            this.dgvItems.TabIndex = 10;
            // 
            // txtScan
            // 
            this.txtScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtScan.Location = new System.Drawing.Point(183, 67);
            this.txtScan.MaxLength = 0;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(726, 30);
            this.txtScan.TabIndex = 0;
            this.txtScan.Text = "put the cursor here to scan credit card";
            this.txtScan.TextChanged += new System.EventHandler(this.txtScan_TextChanged);
            this.txtScan.GotFocus += new System.EventHandler(this.txtScan_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(36, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 97;
            this.label2.Text = "Scan Credit Card No:";
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(829, 9);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(119, 13);
            this.lblAddNewCustomer.TabIndex = 98;
            this.lblAddNewCustomer.Text = " to print verification slip";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(790, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 99;
            this.label6.Text = "[F12]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(790, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 100;
            this.label10.Text = "[F3]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(790, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 101;
            this.label11.Text = "[F4]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(579, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 102;
            this.label12.Text = "Press";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(829, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 13);
            this.label13.TabIndex = 103;
            this.label13.Text = " to show purchases";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label14.Location = new System.Drawing.Point(829, 41);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 13);
            this.label14.TabIndex = 104;
            this.label14.Text = " to show payments";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(610, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 106;
            this.label15.Text = "[F6]";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(634, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(142, 13);
            this.label16.TabIndex = 105;
            this.label16.Text = " to select customer by name";
            // 
            // lblF8
            // 
            this.lblF8.AutoSize = true;
            this.lblF8.BackColor = System.Drawing.Color.Transparent;
            this.lblF8.ForeColor = System.Drawing.Color.Red;
            this.lblF8.Location = new System.Drawing.Point(611, 41);
            this.lblF8.Name = "lblF8";
            this.lblF8.Size = new System.Drawing.Size(27, 13);
            this.lblF8.TabIndex = 109;
            this.lblF8.Text = "[F8]";
            this.lblF8.Visible = false;
            // 
            // lblF8Desc
            // 
            this.lblF8Desc.AutoSize = true;
            this.lblF8Desc.BackColor = System.Drawing.Color.Transparent;
            this.lblF8Desc.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblF8Desc.Location = new System.Drawing.Point(635, 41);
            this.lblF8Desc.Name = "lblF8Desc";
            this.lblF8Desc.Size = new System.Drawing.Size(121, 13);
            this.lblF8Desc.TabIndex = 108;
            this.lblF8Desc.Text = " to adjust current credit";
            this.lblF8Desc.Visible = false;
            // 
            // lblF8Press
            // 
            this.lblF8Press.AutoSize = true;
            this.lblF8Press.BackColor = System.Drawing.Color.Transparent;
            this.lblF8Press.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblF8Press.Location = new System.Drawing.Point(580, 41);
            this.lblF8Press.Name = "lblF8Press";
            this.lblF8Press.Size = new System.Drawing.Size(33, 13);
            this.lblF8Press.TabIndex = 107;
            this.lblF8Press.Text = "Press";
            this.lblF8Press.Visible = false;
            // 
            // lblF7
            // 
            this.lblF7.AutoSize = true;
            this.lblF7.BackColor = System.Drawing.Color.Transparent;
            this.lblF7.ForeColor = System.Drawing.Color.Red;
            this.lblF7.Location = new System.Drawing.Point(611, 25);
            this.lblF7.Name = "lblF7";
            this.lblF7.Size = new System.Drawing.Size(27, 13);
            this.lblF7.TabIndex = 112;
            this.lblF7.Text = "[F7]";
            this.lblF7.Visible = false;
            // 
            // lblF7Desc
            // 
            this.lblF7Desc.AutoSize = true;
            this.lblF7Desc.BackColor = System.Drawing.Color.Transparent;
            this.lblF7Desc.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblF7Desc.Location = new System.Drawing.Point(635, 25);
            this.lblF7Desc.Name = "lblF7Desc";
            this.lblF7Desc.Size = new System.Drawing.Size(106, 13);
            this.lblF7Desc.TabIndex = 111;
            this.lblF7Desc.Text = " to adjust current bill";
            this.lblF7Desc.Visible = false;
            // 
            // lblF7Press
            // 
            this.lblF7Press.AutoSize = true;
            this.lblF7Press.BackColor = System.Drawing.Color.Transparent;
            this.lblF7Press.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblF7Press.Location = new System.Drawing.Point(580, 25);
            this.lblF7Press.Name = "lblF7Press";
            this.lblF7Press.Size = new System.Drawing.Size(33, 13);
            this.lblF7Press.TabIndex = 110;
            this.lblF7Press.Text = "Press";
            this.lblF7Press.Visible = false;
            // 
            // labelF11
            // 
            this.labelF11.AutoSize = true;
            this.labelF11.BackColor = System.Drawing.Color.Transparent;
            this.labelF11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelF11.Location = new System.Drawing.Point(832, 57);
            this.labelF11.Name = "labelF11";
            this.labelF11.Size = new System.Drawing.Size(139, 13);
            this.labelF11.TabIndex = 118;
            this.labelF11.Text = "Print Statement Of Account";
            this.labelF11.Visible = false;
            // 
            // lblF11
            // 
            this.lblF11.BackColor = System.Drawing.Color.Transparent;
            this.lblF11.ForeColor = System.Drawing.Color.Red;
            this.lblF11.Location = new System.Drawing.Point(790, 57);
            this.lblF11.Name = "lblF11";
            this.lblF11.Size = new System.Drawing.Size(34, 13);
            this.lblF11.TabIndex = 117;
            this.lblF11.Text = "[F11]";
            this.lblF11.Visible = false;
            // 
            // CreditVerificationWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.labelF11);
            this.Controls.Add(this.lblF11);
            this.Controls.Add(this.lblF7);
            this.Controls.Add(this.lblF8);
            this.Controls.Add(this.lblF8Desc);
            this.Controls.Add(this.lblF7Desc);
            this.Controls.Add(this.lblF7Press);
            this.Controls.Add(this.lblF8Press);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpPurchases);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.grpContactDetails);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CreditVerificationWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditVerificationWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditVerificationWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpContactDetails.ResumeLayout(false);
            this.grpContactDetails.PerformLayout();
            this.grpPurchases.ResumeLayout(false);
            this.grpPurchases.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods

		private void CreditVerificationWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            DialogResult result; 
            keyCommand = e.KeyData;
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

                case Keys.F3:
                    if (mContactDetails.ContactID == 0) return;
                    CreditSalesWnd clsCreditSalesWnd = new CreditSalesWnd();
                    clsCreditSalesWnd.TerminalDetails = TerminalDetails;
                    clsCreditSalesWnd.SysConfigDetails = SysConfigDetails;
                    clsCreditSalesWnd.CustomerDetails = mContactDetails;
                    clsCreditSalesWnd.ShowDialog(this);
                    clsCreditSalesWnd.Close();
                    clsCreditSalesWnd.Dispose();
                    break;

                case Keys.F4:
                    if (mContactDetails.ContactID == 0) return;
                    CreditPaymentsWnd clsCreditPaymentsWnd = new CreditPaymentsWnd();
                    clsCreditPaymentsWnd.TerminalDetails = TerminalDetails;
                    clsCreditPaymentsWnd.SysConfigDetails = SysConfigDetails;
                    clsCreditPaymentsWnd.CustomerDetails = mContactDetails;
                    clsCreditPaymentsWnd.CashierID = CashierID;
                    clsCreditPaymentsWnd.ShowDialog(this);
                    result = clsCreditPaymentsWnd.Result;
                    clsCreditPaymentsWnd.Close();
                    clsCreditPaymentsWnd.Dispose();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadData();
                        txtScan.Text = mContactDetails.CreditDetails.CreditCardNo;
                    }
                    break;

                case Keys.F6:
                    Data.ContactDetails details;
				    ContactSelectWnd clsContactWnd = new ContactSelectWnd();
                    clsContactWnd.EnableContactAddUpdate = true;
                    clsContactWnd.TerminalDetails = TerminalDetails;
				    clsContactWnd.ContactGroupCategory = Data.ContactGroupCategory.CUSTOMER;
				    clsContactWnd.ShowDialog(this);
				    details = clsContactWnd.Details;
				    result = clsContactWnd.Result;
				    clsContactWnd.Close();
				    clsContactWnd.Dispose();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        switch (SysConfigDetails.CreditPaymentType)
                        {
                            case CreditPaymentType.Houseware:
                                LoadData();
                                txtScan.Text = details.CreditDetails.CreditCardNo;
                                break;
                            case CreditPaymentType.Normal:
                            case CreditPaymentType.MPC:
                            default:
                                txtScan.Text = details.ContactCode;
                                LoadData();
                                txtScan.Text = details.ContactCode;
                                break;
                        }
                        
                    }
                    break;

                case Keys.F7:
                    if (mContactDetails.ContactID != 0 && mboCreditAmountDueAdjustment)
                    {
                        if (mContactDetails.CreditDetails.CardTypeDetails.WithGuarantor)
                        {
                            CreditBillWGuaWnd clsCreditBillWGuaWnd = new CreditBillWGuaWnd();
                            clsCreditBillWGuaWnd.CreditorDetails = mContactDetails;
                            clsCreditBillWGuaWnd.GuarantorDetails = mGuarantorDetails;
                            clsCreditBillWGuaWnd.TerminalDetails = TerminalDetails;
                            clsCreditBillWGuaWnd.ShowDialog(this);
                            result = clsCreditBillWGuaWnd.Result;
                            clsCreditBillWGuaWnd.Close();
                            clsCreditBillWGuaWnd.Dispose();
                        }
                        else 
                        {
                            CreditBillNoGuaWnd clsCreditBillNoGuaWnd = new CreditBillNoGuaWnd();
                            clsCreditBillNoGuaWnd.CreditorDetails = mContactDetails;
                            clsCreditBillNoGuaWnd.GuarantorDetails = mGuarantorDetails;
                            clsCreditBillNoGuaWnd.TerminalDetails = TerminalDetails;
                            clsCreditBillNoGuaWnd.ShowDialog(this);
                            result = clsCreditBillNoGuaWnd.Result;
                            clsCreditBillNoGuaWnd.Close();
                            clsCreditBillNoGuaWnd.Dispose();
                        }
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            Methods.InsertAuditLog(TerminalDetails, CashierName, AccessTypes.CreditAmountDueAdjustment, "Adjust the billing statement of " + mContactDetails.ContactName + ".");

                            MessageBox.Show("Billing has been adjusted.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            txtScan.Text = mContactDetails.CreditDetails.CreditCardNo;
                        }
                    }
                    break;
                case Keys.F8:
                    if (mContactDetails.ContactID != 0 && mboCreditAmountDueAdjustment)
                    {
                        NoControl clsNoControl = new NoControl();
                        clsNoControl.Caption = "Enter new current Credit amount.";
                        clsNoControl.NoValue = mContactDetails.Credit;
                        clsNoControl.ShowDialog(this);
                        result = clsNoControl.Result;
                        decimal decAmount = clsNoControl.NoValue;
                        clsNoControl.Close();
                        clsNoControl.Dispose();
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            if (MessageBox.Show("Are you sure you want adjust the credit balance of " + mContactDetails.ContactName + " from: " + mContactDetails.Credit.ToString("#,##0.#0") + " to " + decAmount.ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            {
                                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
                                clsLocalConnection.GetConnection();

                                Data.Creditors clsCreditors = new Data.Creditors(clsLocalConnection.Connection, clsLocalConnection.Transaction);

                                clsCreditors.AutoAdjustCredit(mContactDetails, decAmount);

                                clsLocalConnection.CommitAndDispose();

                                Methods.InsertAuditLog(TerminalDetails, CashierName, AccessTypes.CreditAmountDueAdjustment, "Adjust the credit balance of " + mContactDetails.ContactName + " from: " + mContactDetails.Credit.ToString("#,##0.#0") + " to " + decAmount.ToString("#,##0.#0"));

                                MessageBox.Show("Current credit has been adjusted.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadData();
                                txtScan.Text = mContactDetails.CreditDetails.CreditCardNo;
                            }
                        }
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
                    if (mContactDetails.ContactID == 0)
                    {
                        MessageBox.Show("Please select a valid card to print the verification slip.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (mContactDetails.CreditDetails.CardTypeDetails.WithGuarantor && !mGuarantorDetails.CreditDetails.CreditActive)
                    {
                        MessageBox.Show("Sorry printing verification slip is not allowed. Your GUARANTOR credit status InActive.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (mContactDetails.CreditDetails.ExpiryDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23,59,59))
                    {
                        MessageBox.Show("Sorry printing verification slip is not allowed. Your card is already expired.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!mContactDetails.CreditDetails.CreditActive)
                    {
                        MessageBox.Show("Sorry printing verification slip is not allowed for InActive Credit Cards.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
					break;
			}
		}

		private void CreditVerificationWnd_Load(object sender, System.EventArgs e)
		{
            this.lblHeader.Text = !string.IsNullOrEmpty(mstCaption) ? mstCaption : CompanyDetails.CompanyCode + " Credit Verification";

            Security.AccessRights clsAccessRights = new Security.AccessRights();
            Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

            clsDetails = clsAccessRights.Details(CashierID, (Int16)AccessTypes.CreditAmountDueAdjustment);

            lblF7Press.Visible = clsDetails.Write;
            lblF7.Visible = clsDetails.Write;
            lblF7Desc.Visible = clsDetails.Write;

            lblF8Press.Visible = clsDetails.Write;
            lblF8.Visible = clsDetails.Write;
            lblF8Desc.Visible = clsDetails.Write;

            mboCreditAmountDueAdjustment = clsDetails.Write;

            clsAccessRights.CommitAndDispose();

            LoadOptions();
		}
		
		#endregion

		#region Windows Control Methods

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            keyCommand = Keys.Cancel;
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            keyCommand = Keys.Enter;
            dialog = DialogResult.OK;
            this.Hide();
        }

        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScan.Text) && 
                txtScan.Text != "put the cursor here to scan credit card" 
                && txtScan.Text.Length >= 7
                && txtScan.Text != mContactDetails.CreditDetails.CreditCardNo)
            {
                LoadData();
            }
        }

        private void txtScan_GotFocus(object sender, System.EventArgs e)
        {
            txtScan.SelectAll();
        }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            mContactDetails = new Data.ContactDetails();
            mGuarantorDetails = new Data.ContactDetails();

            Data.Contacts clsContacts = new Data.Contacts();
            switch (SysConfigDetails.CreditPaymentType)
            {
                case CreditPaymentType.Houseware:
                    mContactDetails = clsContacts.DetailsByCreditCardNo(txtScan.Text);
                    break;
                case CreditPaymentType.Normal:
                case CreditPaymentType.MPC:
                default:
                    mContactDetails = clsContacts.Details(txtScan.Text);
                    break;
            }

            if (mContactDetails.ContactID == 0)
            {
                //mContactDetails = clsContacts.DetailsByCreditCardNo(txtScan.Text.Remove(txtScan.Text.Length - 1));

                if (mContactDetails.ContactID == 0 && txtScan.Text.Length == 7) mContactDetails = clsContacts.DetailsByCreditCardNo("888880" + txtScan.Text);
                if (mContactDetails.ContactID == 0 && txtScan.Text.Length == 7) mContactDetails = clsContacts.DetailsByCreditCardNo("800000" + txtScan.Text);
                if (mContactDetails.ContactID == 0 && txtScan.Text.Length == 9) mContactDetails = clsContacts.DetailsByCreditCardNo(BarcodeHelper.GroupCreditCard_Country_Code + BarcodeHelper.GroupCreditCard_ManufacturerCode + txtScan.Text);
                if (mContactDetails.ContactID == 0 && txtScan.Text.Length == 9) mContactDetails = clsContacts.DetailsByCreditCardNo(BarcodeHelper.CreditCard_Country_Code + BarcodeHelper.CreditCard_ManufacturerCode + txtScan.Text);
            }

            //get the guarantor details if with details
            if (mContactDetails.ContactID != 0)
                if (mContactDetails.ContactID == mContactDetails.CreditDetails.GuarantorID)
                    mGuarantorDetails = mContactDetails;
                else if (mGuarantorDetails.ContactID != mContactDetails.CreditDetails.GuarantorID)
                    mGuarantorDetails = clsContacts.Details(mContactDetails.CreditDetails.GuarantorID);

            clsContacts.CommitAndDispose();


            if (mContactDetails.ContactID == 0)
            {
                grpContactDetails.Visible = false;
                grpPurchases.Visible = false;
                labelGuarantor.Visible = false;
                txtGuarantor.Visible = false;
            }
            else
            {
                grpContactDetails.Visible = true;
                grpPurchases.Visible = true;

                txtScan.Text = mContactDetails.CreditDetails.CreditCardNo;
                txtCustomerName.Text = mContactDetails.ContactName;
                txtMobileNo.Text = mContactDetails.AdditionalDetails.MobileNo;
                txtTelNo.Text = mContactDetails.TelephoneNo;
                txtAddress.Text = mContactDetails.Address;
                txtRemarks.Text = mContactDetails.Remarks;

                txtCreditCardStatus.Text = mContactDetails.CreditDetails.CreditActive ? "Active" : "InActive";
                txtCreditCardStatus.Text += "(" + mContactDetails.CreditDetails.CreditCardStatus.ToString("G") + ")";
                txtCreditLimit.Text = mContactDetails.CreditLimit.ToString("#,##0.#0");
                txtCredit.Text = mContactDetails.Credit.ToString("#,##0.#0");
                txtAvailableCredit.Text = (mContactDetails.CreditLimit - mContactDetails.Credit).ToString("#,##0.#0");

                if (!mContactDetails.CreditDetails.CardTypeDetails.WithGuarantor) //no guarantor
                {
                    labelCreditStatus.Location = new Point(71, 133);
                    txtCreditCardStatus.Location = new Point(174, 133);
                    txtCreditCardStatus.Size = new Size(439, 30);
                    labelGuarantor.Visible = false;
                    txtGuarantor.Visible = false;
                }
                else if (mContactDetails.CreditDetails.CardTypeDetails.WithGuarantor) //with guarantor
                {
                    labelCreditStatus.Location = new Point(627, 133);
                    txtCreditCardStatus.Location = new Point(731, 133);
                    txtCreditCardStatus.Size = new Size(269, 30);
                    labelGuarantor.Visible = true;
                    txtGuarantor.Visible = true;

                    txtGuarantor.Text = "[" + (mGuarantorDetails.CreditDetails.CreditActive ? "Active" : "InActive") + "] " + mGuarantorDetails.ContactCode + ":" + mGuarantorDetails.ContactName;
                }

                LoadPurchases();
                lblBalance.Text = mContactDetails.Credit.ToString("#,##0.#0");
            }
        }

        private void LoadOptions()
        {
            mContactDetails = new Data.ContactDetails();
            mGuarantorDetails = new Data.ContactDetails();
            txtScan.Focus();

            txtScan.Size = new Size(726, 30);
            switch (SysConfigDetails.CreditPaymentType)
            {
                case CreditPaymentType.MPC:
                case CreditPaymentType.Normal:
                default:
                    lblF11.Visible = true; labelF11.Visible = true;
                    txtScan.Size = new Size(558, 30);
                    break;
            }
        }

        private void LoadPurchases()
        {
            try
            {
                DateTime dteRetValue = DateTime.MinValue;
                DateTime dtePaymentDateFrom = DateTime.MinValue;    // DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : DateTime.Now.AddYears(-2);
                DateTime dtePaymentDateTo = DateTime.MinValue;      //DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : DateTime.Now;

                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                System.Data.DataTable dt;

                switch (SysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        dt = clsTransactions.ListForPaymentDataTable(mContactDetails.ContactID, "Balance, TransactionID", System.Data.SqlClient.SortOrder.Descending);
                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dt = clsTransactions.ListForPaymentDataTable(mContactDetails.ContactID, "TransactionNo", System.Data.SqlClient.SortOrder.Ascending, 0, dtePaymentDateFrom, dtePaymentDateTo);
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
                switch (SysConfigDetails.CreditPaymentType)
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
                switch (SysConfigDetails.CreditPaymentType)
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
                switch (SysConfigDetails.CreditPaymentType)
                {
                    case CreditPaymentType.Houseware:
                        dgvItems.Columns["CreditReason"].HeaderText = "Description";
                        break;
                    case CreditPaymentType.MPC:
                    case CreditPaymentType.Normal:
                    default:
                        dgvItems.Columns["Terms"].HeaderText = "Terms";
                        dgvItems.Columns["ModeOfTermsCode"].HeaderText = "ModeOfTerms";
                        dgvItems.Columns["AgingDate"].HeaderText = "AgingDate";
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
                //Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                //System.Data.DataTable dt = clsTransactions.ListForPaymentDataTable(mContactDetails.ContactID);
                //clsTransactions.CommitAndDispose();

                //dgvItems.MultiSelect = true;
                //dgvItems.AutoGenerateColumns = true;
                //dgvItems.AutoSize = false;
                //dgvItems.DataSource = dt.TableName;
                //dgvItems.DataSource = dt;

                //foreach (DataGridViewTextBoxColumn dc in dgvItems.Columns)
                //{
                //    dc.Visible = false;
                //}
                //dgvItems.Columns["TransactionNo"].Visible = true;
                //dgvItems.Columns["TransactionDate"].Visible = true;
                //dgvItems.Columns["CreditReason"].Visible = true;
                //dgvItems.Columns["SubTotal"].Visible = true;
                //dgvItems.Columns["Credit"].Visible = true;
                //dgvItems.Columns["CreditPaid"].Visible = true;
                //dgvItems.Columns["Balance"].Visible = true;

                //dgvItems.Columns["TransactionNo"].Width = 120;
                //dgvItems.Columns["TransactionDate"].Width = 120;
                //if (dt.Rows.Count < 8) dgvItems.Columns["CreditReason"].Width = 240; else dgvItems.Columns["CreditReason"].Width = 210;
                //int iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["CreditReason"].Width) / 4;
                //if (dt.Rows.Count >= 8) iWidth = iWidth - 5;
                //dgvItems.Columns["SubTotal"].Width = iWidth;
                //dgvItems.Columns["Credit"].Width = iWidth;
                //dgvItems.Columns["CreditPaid"].Width = iWidth;
                //dgvItems.Columns["Balance"].Width = iWidth;

                //dgvItems.Columns["TransactionNo"].HeaderText = "Trans. No";
                //dgvItems.Columns["TransactionDate"].HeaderText = "Trans. Date";
                //dgvItems.Columns["CreditReason"].HeaderText = "Description";
                //dgvItems.Columns["SubTotal"].HeaderText = "Subtotal";
                //dgvItems.Columns["Credit"].HeaderText = "Credit";
                //dgvItems.Columns["CreditPaid"].HeaderText = "Credit Paid";
                //dgvItems.Columns["Balance"].HeaderText = "Balance";

                //dgvItems.Columns["SubTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["Credit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["CreditPaid"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["Balance"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgvItems.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["CreditPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvItems.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //dgvItems.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt";
                //dgvItems.Columns["SubTotal"].DefaultCellStyle.Format = "#,##0.#0";
                //dgvItems.Columns["Credit"].DefaultCellStyle.Format = "#,##0.#0";
                //dgvItems.Columns["CreditPaid"].DefaultCellStyle.Format = "#,##0.#0";
                //dgvItems.Columns["Balance"].DefaultCellStyle.Format = "#,##0.#0";

                //dgvItems.ReadOnly = true;
                //dgvItems.Select();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                System.Data.DataTable dt = clsContacts.ListAsDataTable(ContactID: mContactDetails.ContactID);
                clsContacts.CommitAndDispose();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    drNew = rptds.Customer.NewRow();

                    foreach (System.Data.DataColumn dc in rptds.Customer.Columns)
                        drNew[dc] = dr[dc.ColumnName];

                    rptds.Customer.Rows.Add(drNew);
                }

                dt = (System.Data.DataTable)dgvItems.DataSource;
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
                    MessageBox.Show("Statement Of Account for " + mContactDetails.ContactName + " has been printed.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
