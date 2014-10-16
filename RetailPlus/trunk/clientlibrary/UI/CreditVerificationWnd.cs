using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
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
        private DialogResult dialog;
        private GroupBox grpContactDetails;
        private Label label3;
        private TextBox txtTelNo;
        private Label label1;
        private TextBox txtAddress;
        private Label lblCaption;
        private TextBox txtCustomerName;
        private TextBox txtSelectedTexBox;
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
        private Label label10;
        private DataGridView dgvItems;
        private Label lblBalanceName;
        private Label lblBalance;
        private Label lblAddNewCustomer;
        private Label label6;
		private string mstCaption;

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
            this.txtCreditCardStatus = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.grpContactDetails.Controls.Add(this.txtCreditCardStatus);
            this.grpContactDetails.Controls.Add(this.label10);
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
            this.grpContactDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpContactDetails.ForeColor = System.Drawing.Color.Blue;
            this.grpContactDetails.Location = new System.Drawing.Point(9, 113);
            this.grpContactDetails.Name = "grpContactDetails";
            this.grpContactDetails.Size = new System.Drawing.Size(1008, 182);
            this.grpContactDetails.TabIndex = 0;
            this.grpContactDetails.TabStop = false;
            this.grpContactDetails.Text = "Creditor Details";
            this.grpContactDetails.Visible = false;
            // 
            // txtCreditCardStatus
            // 
            this.txtCreditCardStatus.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreditCardStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardStatus.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditCardStatus.Location = new System.Drawing.Point(174, 96);
            this.txtCreditCardStatus.MaxLength = 25;
            this.txtCreditCardStatus.Name = "txtCreditCardStatus";
            this.txtCreditCardStatus.ReadOnly = true;
            this.txtCreditCardStatus.Size = new System.Drawing.Size(439, 30);
            this.txtCreditCardStatus.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(71, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Credit Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(627, 17);
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
            this.txtTelNo.Location = new System.Drawing.Point(731, 15);
            this.txtTelNo.MaxLength = 75;
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.ReadOnly = true;
            this.txtTelNo.Size = new System.Drawing.Size(169, 30);
            this.txtTelNo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(73, 51);
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
            this.txtAddress.Size = new System.Drawing.Size(726, 39);
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
            this.txtCustomerName.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(73, 132);
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
            this.txtAvailableCredit.Location = new System.Drawing.Point(731, 132);
            this.txtAvailableCredit.MaxLength = 75;
            this.txtAvailableCredit.Name = "txtAvailableCredit";
            this.txtAvailableCredit.ReadOnly = true;
            this.txtAvailableCredit.Size = new System.Drawing.Size(169, 30);
            this.txtAvailableCredit.TabIndex = 8;
            this.txtAvailableCredit.Text = "0.00";
            this.txtAvailableCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MediumBlue;
            this.label7.Location = new System.Drawing.Point(627, 132);
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
            this.txtCreditLimit.Location = new System.Drawing.Point(174, 132);
            this.txtCreditLimit.MaxLength = 75;
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.ReadOnly = true;
            this.txtCreditLimit.Size = new System.Drawing.Size(169, 30);
            this.txtCreditLimit.TabIndex = 1;
            this.txtCreditLimit.Text = "0.00";
            this.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(356, 132);
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
            this.txtCredit.Location = new System.Drawing.Point(444, 132);
            this.txtCredit.MaxLength = 75;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(169, 30);
            this.txtCredit.TabIndex = 2;
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
            // grpPurchases
            // 
            this.grpPurchases.BackColor = System.Drawing.Color.White;
            this.grpPurchases.Controls.Add(this.lblBalanceName);
            this.grpPurchases.Controls.Add(this.lblBalance);
            this.grpPurchases.Controls.Add(this.dgvItems);
            this.grpPurchases.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPurchases.ForeColor = System.Drawing.Color.Blue;
            this.grpPurchases.Location = new System.Drawing.Point(7, 301);
            this.grpPurchases.Name = "grpPurchases";
            this.grpPurchases.Size = new System.Drawing.Size(1010, 301);
            this.grpPurchases.TabIndex = 13;
            this.grpPurchases.TabStop = false;
            this.grpPurchases.Text = "Current Purchases";
            this.grpPurchases.Visible = false;
            // 
            // lblBalanceName
            // 
            this.lblBalanceName.AutoSize = true;
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(441, 263);
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
            this.lblBalance.Location = new System.Drawing.Point(733, 265);
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
            this.dgvItems.Size = new System.Drawing.Size(996, 238);
            this.dgvItems.TabIndex = 57;
            // 
            // txtScan
            // 
            this.txtScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScan.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScan.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtScan.Location = new System.Drawing.Point(183, 67);
            this.txtScan.MaxLength = 0;
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(726, 30);
            this.txtScan.TabIndex = 96;
            this.txtScan.Text = "put the cursor here to scan credit card";
            this.txtScan.TextChanged += new System.EventHandler(this.txtScan_TextChanged);
            this.txtScan.GotFocus += new System.EventHandler(this.txtScan_GotFocus);
            this.txtScan.LostFocus += new System.EventHandler(this.txtScan_LostFocus);
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
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(737, 22);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(225, 13);
            this.lblAddNewCustomer.TabIndex = 98;
            this.lblAddNewCustomer.Text = "Click or Press  [Enter]  to print verification slip";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(807, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 99;
            this.label6.Text = "[Enter]";
            // 
            // CreditVerificationWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
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
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					try
					{
						if (SaveRecord() == true)
						{							
							dialog = DialogResult.OK; 
							this.Hide();
						}
					}
					catch (Exception ex)
					{
						Event clsEvent = new Event();
                        clsEvent.AddErrorEventLn(ex);
						MessageBox.Show("Sorry the customer name is already in the database. Please type another customer name." ,"RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					break;
			}
		}

		private void CreditVerificationWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Balance.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			this.lblHeader.Text = !string.IsNullOrEmpty(mstCaption) ? mstCaption : CompanyDetails.CompanyCode + " Credit Verification";

            LoadOptions();
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
            try
            {
                if (SaveRecord() == true)
                {
                    dialog = DialogResult.OK;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR!!! Saving customer details. Err Description: " + ex.Message);
                MessageBox.Show("Sorry the customer name is already in the database. Please type another customer name.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtScan_LostFocus(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtScan.Text))
            {
                txtScan.Text = "put the cursor here to scan credit card";
            }
            else if (txtScan.Text != "put the cursor here to scan credit card" && mContactDetails.ContactID == 0)
            {
                txtScan.Text = "put the cursor here to scan credit card";
            }
        }

        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScan.Text))
            {
                Data.Contacts clsContacts = new Data.Contacts();
                mContactDetails = clsContacts.DetailsByCreditCardNo(txtScan.Text);

                if (mContactDetails.ContactID == 0)
                {
                    mContactDetails = clsContacts.DetailsByCreditCardNo(txtScan.Text.Remove(txtScan.Text.Length - 1));
                }
                clsContacts.CommitAndDispose();


                if (mContactDetails.ContactID == 0)
                {
                    grpContactDetails.Visible = false;
                    grpPurchases.Visible = false;
                }
                else
                {
                    grpContactDetails.Visible = true;
                    grpPurchases.Visible = true;

                    txtCustomerName.Text = mContactDetails.ContactName;
                    txtTelNo.Text = mContactDetails.TelephoneNo;
                    txtAddress.Text = mContactDetails.Address;

                    txtCreditCardStatus.Text = mContactDetails.CreditDetails.CreditCardStatus.ToString("G") + " ";
                    txtCreditCardStatus.Text += mContactDetails.CreditDetails.CreditActive ? " (Active) " : " (InActive) ";
                    txtCreditLimit.Text = mContactDetails.CreditLimit.ToString("#,##0.#0");
                    txtCredit.Text = mContactDetails.Credit.ToString("#,##0.#0");
                    txtCreditLimit.Text = (mContactDetails.CreditLimit - mContactDetails.Credit).ToString("#,##0.#0");

                    LoadPurchases();
                    lblBalance.Text = mContactDetails.Credit.ToString("#,##0.#0");
                }
            }
        }

        private void txtScan_GotFocus(object sender, System.EventArgs e)
        {
            txtScan.Text = "";
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            txtScan.Focus();
        }

        private bool SaveRecord()
        {
            //Data.ContactDetails clsDetails = new Data.ContactDetails();
            //clsDetails = mContactDetails;

            //clsDetails.ContactCode = txtCustomerName.Text;
            //clsDetails.ContactName = txtCustomerName.Text;
            //clsDetails.Address = txtAddress.Text;
            //clsDetails.TelephoneNo = txtTelNo.Text;

            //// Jul 22, 2014
            //clsDetails.Debit = mContactDetails.Debit;
            //clsDetails.Credit = mContactDetails.Credit;
            //clsDetails.IsCreditAllowed = chkIsCreditAllowed.Checked;
            //clsDetails.CreditLimit = decimal.Parse(txtCreditLimit.Text);
            //clsDetails.Terms = int.Parse(txtTerms.Text);
            //clsDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), cboTerms.SelectedItem.ToString());

            //Data.Contacts clsContact = new Data.Contacts();
            //if (mContactDetails.ContactID == 0)
            //{
            //    if (mstCaption == "Please enter customer name for deposit.")
            //    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_DEPOSIT; }
            //    else if (mstCaption == "Quickly add new customer")
            //    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_QUICKLY_ADDED_FROM_FE; }
            //    else if (mContactDetails.ContactID == 0) // means not edit
            //    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT; }

            //    clsDetails.ContactGroupID = Constants.CONTACT_GROUP_CUSTOMER;
            //    clsDetails.PositionID = Constants.C_RETAILPLUS_AGENT_POSITIONID;
            //    clsDetails.DepartmentID = Constants.C_RETAILPLUS_AGENT_DEPARTMENTID;
            //    clsDetails.ContactID = clsContact.Insert(clsDetails);
            //}
            //else
            //{
            //    clsDetails.ContactCode = mContactDetails.ContactCode;
            //    clsDetails.ContactGroupID = mContactDetails.ContactGroupID;
            //    clsDetails.ContactGroupName = mContactDetails.ContactGroupName;
            //    clsContact.Update(clsDetails);
            //}
            //clsContact.CommitAndDispose();

            //mContactDetails = clsDetails;

            return true;
        }

        private void LoadPurchases()
        {
            try
            {
                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                System.Data.DataTable dt = clsTransactions.ListForPaymentDataTable(mContactDetails.ContactID);
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
                dgvItems.Columns["Credit"].Visible = true;
                dgvItems.Columns["CreditPaid"].Visible = true;
                dgvItems.Columns["Balance"].Visible = true;

                dgvItems.Columns["TransactionNo"].Width = 120;
                dgvItems.Columns["TransactionDate"].Width = 120;
                dgvItems.Columns["CreditReason"].Width = 240;
                int iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["CreditReason"].Width) / 3;
                dgvItems.Columns["Credit"].Width = iWidth;
                dgvItems.Columns["CreditPaid"].Width = iWidth;
                dgvItems.Columns["Balance"].Width = iWidth;

                dgvItems.Columns["TransactionNo"].HeaderText = "Trans. No";
                dgvItems.Columns["TransactionDate"].HeaderText = "Trans. Date";
                dgvItems.Columns["CreditReason"].HeaderText = "Description";
                dgvItems.Columns["Credit"].HeaderText = "Credit";
                dgvItems.Columns["CreditPaid"].HeaderText = "Credit Paid";
                dgvItems.Columns["Balance"].HeaderText = "Balance";

                dgvItems.Columns["Credit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["CreditPaid"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Balance"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvItems.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["CreditPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvItems.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt";
                dgvItems.Columns["Credit"].DefaultCellStyle.Format = "#,##0.#0";
                dgvItems.Columns["CreditPaid"].DefaultCellStyle.Format = "#,##0.#0";
                dgvItems.Columns["Balance"].DefaultCellStyle.Format = "#,##0.#0";

                dgvItems.ReadOnly = true;
                dgvItems.Select();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}
