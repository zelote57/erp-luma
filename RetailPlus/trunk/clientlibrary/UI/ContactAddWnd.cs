using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ContactAddWnd.
	/// </summary>
	public class ContactAddWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private Data.ContactDetails mContactDetails;
        private DialogResult dialog;
        private GroupBox groupBox1;
        private Label label3;
        private TextBox txtTelNo;
        private Label label2;
        private TextBox txtBusinessName;
        private Label label1;
        private TextBox txtAddress;
        private Label lblCaption;
        private TextBox txtCustomerName;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private GroupBox groupBox2;
        private Label label7;
        private TextBox txtAvailableCredit;
        private Label label4;
        private TextBox txtCredit;
        private Label label5;
        private TextBox txtCreditLimit;
        private Label label8;
        private TextBox txtTerms;
        private Label label6;
        private ComboBox cboTerms;
        private Label lblDiscountType;
        private Label label9;
        private CheckBox chkIsCreditAllowed;
        private Label label10;
        private TextBox txtContactCode;
        private Label label16;
        private TextBox txtRemarks;
        private Label label11;
        private TextBox txtLTONo;
        private Label label12;
        private TextBox txtTINNo;
        private Button cmdGenerateCustomerCode;
        private Label label13;
        private TextBox txtPriceLevel;
        private Label label14;
        private ComboBox cboGroup;
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

		public Data.ContactDetails ContactDetails
		{
			get {	return mContactDetails;	}
			set {	mContactDetails = value;	}
		}

        public Data.TerminalDetails TerminalDetails { get; set; }

		#region Constructors And Desctructors
		public ContactAddWnd()
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboGroup = new System.Windows.Forms.ComboBox();
            this.txtPriceLevel = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdGenerateCustomerCode = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLTONo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTINNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtContactCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusinessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkIsCreditAllowed = new System.Windows.Forms.CheckBox();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTerms = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTerms = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAvailableCredit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(166, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Enter Customer Information";
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cboGroup);
            this.groupBox1.Controls.Add(this.txtPriceLevel);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cmdGenerateCustomerCode);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtLTONo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtTINNo);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtContactCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTelNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBusinessName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.lblCaption);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contact Details";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(73, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Select group";
            // 
            // cboGroup
            // 
            this.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGroup.Location = new System.Drawing.Point(114, 167);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.Size = new System.Drawing.Size(205, 27);
            this.cboGroup.TabIndex = 7;
            // 
            // txtPriceLevel
            // 
            this.txtPriceLevel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtPriceLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPriceLevel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPriceLevel.Location = new System.Drawing.Point(114, 246);
            this.txtPriceLevel.MaxLength = 75;
            this.txtPriceLevel.Name = "txtPriceLevel";
            this.txtPriceLevel.ReadOnly = true;
            this.txtPriceLevel.Size = new System.Drawing.Size(201, 30);
            this.txtPriceLevel.TabIndex = 18;
            this.txtPriceLevel.TabStop = false;
            this.txtPriceLevel.Text = "SRP";
            this.txtPriceLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumBlue;
            this.label13.Location = new System.Drawing.Point(73, 231);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Price Level";
            // 
            // cmdGenerateCustomerCode
            // 
            this.cmdGenerateCustomerCode.CausesValidation = false;
            this.cmdGenerateCustomerCode.Location = new System.Drawing.Point(292, 51);
            this.cmdGenerateCustomerCode.Name = "cmdGenerateCustomerCode";
            this.cmdGenerateCustomerCode.Size = new System.Drawing.Size(27, 30);
            this.cmdGenerateCustomerCode.TabIndex = 1;
            this.cmdGenerateCustomerCode.Text = "...";
            this.cmdGenerateCustomerCode.UseVisualStyleBackColor = true;
            this.cmdGenerateCustomerCode.Click += new System.EventHandler(this.cmdGenerateCustomerCode_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(349, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "LTO No (BFAD No)";
            // 
            // txtLTONo
            // 
            this.txtLTONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLTONo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLTONo.Location = new System.Drawing.Point(392, 108);
            this.txtLTONo.MaxLength = 75;
            this.txtLTONo.Name = "txtLTONo";
            this.txtLTONo.Size = new System.Drawing.Size(310, 30);
            this.txtLTONo.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(71, 90);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "TIN No (Tax Identification No)";
            // 
            // txtTINNo
            // 
            this.txtTINNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTINNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTINNo.Location = new System.Drawing.Point(114, 108);
            this.txtTINNo.MaxLength = 75;
            this.txtTINNo.Name = "txtTINNo";
            this.txtTINNo.Size = new System.Drawing.Size(205, 30);
            this.txtTINNo.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MediumBlue;
            this.label16.Location = new System.Drawing.Point(349, 228);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(392, 246);
            this.txtRemarks.MaxLength = 150;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(609, 49);
            this.txtRemarks.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(71, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Please enter Customer Code";
            // 
            // txtContactCode
            // 
            this.txtContactCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactCode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactCode.Location = new System.Drawing.Point(114, 51);
            this.txtContactCode.MaxLength = 25;
            this.txtContactCode.Name = "txtContactCode";
            this.txtContactCode.Size = new System.Drawing.Size(176, 30);
            this.txtContactCode.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(730, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Please enter contact tel. no.";
            // 
            // txtTelNo
            // 
            this.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelNo.Location = new System.Drawing.Point(771, 108);
            this.txtTelNo.MaxLength = 75;
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Size = new System.Drawing.Size(230, 30);
            this.txtTelNo.TabIndex = 6;
            this.txtTelNo.GotFocus += new System.EventHandler(this.txtTelNo_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(728, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Please enter customer contact person";
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusinessName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusinessName.Location = new System.Drawing.Point(771, 51);
            this.txtBusinessName.MaxLength = 75;
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Size = new System.Drawing.Size(230, 30);
            this.txtBusinessName.TabIndex = 3;
            this.txtBusinessName.GotFocus += new System.EventHandler(this.txtBusinessName_GotFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(349, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please enter customer address";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(392, 167);
            this.txtAddress.MaxLength = 150;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(609, 48);
            this.txtAddress.TabIndex = 8;
            this.txtAddress.GotFocus += new System.EventHandler(this.txtAddress_GotFocus);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCaption.Location = new System.Drawing.Point(349, 33);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(171, 13);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Please enter Customer Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(392, 51);
            this.txtCustomerName.MaxLength = 25;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(310, 30);
            this.txtCustomerName.TabIndex = 2;
            this.txtCustomerName.GotFocus += new System.EventHandler(this.txtCustomerName_GotFocus);
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
            this.cmdCancel.TabIndex = 15;
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
            this.cmdEnter.TabIndex = 14;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkIsCreditAllowed);
            this.groupBox2.Controls.Add(this.lblDiscountType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboTerms);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtTerms);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtAvailableCredit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCredit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCreditLimit);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(7, 382);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1008, 206);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credit Details";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.MediumBlue;
            this.label9.Location = new System.Drawing.Point(677, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Check to Allow Credit";
            // 
            // chkIsCreditAllowed
            // 
            this.chkIsCreditAllowed.AutoSize = true;
            this.chkIsCreditAllowed.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.chkIsCreditAllowed.ForeColor = System.Drawing.Color.MediumBlue;
            this.chkIsCreditAllowed.Location = new System.Drawing.Point(688, 136);
            this.chkIsCreditAllowed.Name = "chkIsCreditAllowed";
            this.chkIsCreditAllowed.Size = new System.Drawing.Size(240, 30);
            this.chkIsCreditAllowed.TabIndex = 13;
            this.chkIsCreditAllowed.Text = "( If uncheck credit will not be allowed \r\n  even if he/she has available credit b" +
    "alance)";
            this.chkIsCreditAllowed.UseVisualStyleBackColor = true;
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscountType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscountType.ForeColor = System.Drawing.Color.Red;
            this.lblDiscountType.Location = new System.Drawing.Point(96, 50);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(31, 13);
            this.lblDiscountType.TabIndex = 15;
            this.lblDiscountType.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(402, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Select Type of Terms";
            // 
            // cboTerms
            // 
            this.cboTerms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerms.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTerms.Location = new System.Drawing.Point(443, 136);
            this.cboTerms.Name = "cboTerms";
            this.cboTerms.Size = new System.Drawing.Size(185, 31);
            this.cboTerms.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.MediumBlue;
            this.label8.Location = new System.Drawing.Point(75, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(242, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Enter Term No of Days or Months or Years";
            // 
            // txtTerms
            // 
            this.txtTerms.BackColor = System.Drawing.SystemColors.Window;
            this.txtTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerms.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerms.Location = new System.Drawing.Point(116, 136);
            this.txtTerms.MaxLength = 75;
            this.txtTerms.Name = "txtTerms";
            this.txtTerms.Size = new System.Drawing.Size(257, 30);
            this.txtTerms.TabIndex = 11;
            this.txtTerms.Text = "0";
            this.txtTerms.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTerms.GotFocus += new System.EventHandler(this.txtTerms_GotFocus);
            this.txtTerms.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTerms_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MediumBlue;
            this.label7.Location = new System.Drawing.Point(677, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Available Credit Balance";
            // 
            // txtAvailableCredit
            // 
            this.txtAvailableCredit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAvailableCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvailableCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvailableCredit.Location = new System.Drawing.Point(688, 68);
            this.txtAvailableCredit.MaxLength = 75;
            this.txtAvailableCredit.Name = "txtAvailableCredit";
            this.txtAvailableCredit.ReadOnly = true;
            this.txtAvailableCredit.Size = new System.Drawing.Size(185, 30);
            this.txtAvailableCredit.TabIndex = 8;
            this.txtAvailableCredit.TabStop = false;
            this.txtAvailableCredit.Text = "0.00";
            this.txtAvailableCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(402, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current Credit Amount";
            // 
            // txtCredit
            // 
            this.txtCredit.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCredit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredit.Location = new System.Drawing.Point(443, 68);
            this.txtCredit.MaxLength = 75;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(185, 30);
            this.txtCredit.TabIndex = 2;
            this.txtCredit.TabStop = false;
            this.txtCredit.Text = "0.00";
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(71, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(298, 26);
            this.label5.TabIndex = 5;
            this.label5.Text = "Enter amount of credit limit\r\n      (0.00   means customer is not allowed for cre" +
    "dit)";
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditLimit.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditLimit.Location = new System.Drawing.Point(114, 68);
            this.txtCreditLimit.MaxLength = 75;
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(257, 30);
            this.txtCreditLimit.TabIndex = 10;
            this.txtCreditLimit.Text = "0.00";
            this.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCreditLimit.GotFocus += new System.EventHandler(this.txtCreditLimit_GotFocus);
            this.txtCreditLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditLimit_KeyPress);
            // 
            // ContactAddWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ContactAddWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactAddWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactAddWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods
		private void ContactAddWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (string.IsNullOrEmpty(txtCustomerName.Text))
                    {
                        MessageBox.Show("Please enter a valid customer name.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Int32 intTerms = 0;
                    if (!int.TryParse(txtTerms.Text, out intTerms))
                    {
                        MessageBox.Show("Please enter a valid whole number in TERMS.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal decCreditLimit = 0;
                    if (!decimal.TryParse(txtCreditLimit.Text, out decCreditLimit))
                    {
                        MessageBox.Show("Please enter a valid credit limit.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
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

		private void ContactAddWnd_Load(object sender, System.EventArgs e)
		{
			this.lblHeader.Text = mstCaption;

            LoadOption();
		}
		
		#endregion

		#region Windows Control Methods

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox == null)
                txtCustomerName.Focus();
            else if (txtSelectedTexBox.Name == txtCustomerName.Name)
                txtCustomerName.Focus();
            else if (txtSelectedTexBox.Name == txtBusinessName.Name)
                txtBusinessName.Focus();
            else if (txtSelectedTexBox.Name == txtTelNo.Name)
                txtTelNo.Focus();
            else if (txtSelectedTexBox.Name == txtAddress.Name)
                txtAddress.Focus();
            else if (txtSelectedTexBox.Name == txtCreditLimit.Name)
                txtCreditLimit.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Please enter a valid customer name.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Int32 intTerms = 0;
            if (!int.TryParse(txtTerms.Text, out intTerms))
            {
                MessageBox.Show("Please enter a valid whole number in TERMS.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal decCreditLimit = 0;
            if (!decimal.TryParse(txtCreditLimit.Text, out decCreditLimit))
            {
                MessageBox.Show("Please enter a valid credit limit.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

        private void txtCustomerName_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtBusinessName_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtTelNo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtAddress_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtCreditLimit_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtCreditLimit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));

            try
            {
                txtAvailableCredit.Text = Convert.ToString(decimal.Parse(txtCreditLimit.Text) - decimal.Parse(txtCredit.Text));
            }
            catch
            {
                txtAvailableCredit.Text = "0.00";
            }
        }

        private void txtTerms_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtTerms_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
        }

        #endregion

        #region Private Methods

        private void LoadOption()
        {
            cboTerms.Items.Clear();
            foreach (string str in Enum.GetNames(typeof(ModeOfTerms)))
            {
                cboTerms.Items.Add(str);
            }
            cboTerms.SelectedIndex = 0;

            Data.ContactGroups clsContactGroups = new Data.ContactGroups();
            System.Data.DataTable dtContactGroups = clsContactGroups.ListAsDataTable(Data.ContactGroupCategory.CUSTOMER);
            clsContactGroups.CommitAndDispose();

            bool boContactGroup = false;
            cboGroup.Items.Clear();
            foreach (System.Data.DataRow dr in dtContactGroups.Rows)
            {
                cboGroup.Items.Add(dr["ContactGroupName"].ToString());
                if (mContactDetails.ContactGroupName == dr["ContactGroupName"].ToString())
                {
                    cboGroup.SelectedIndex = cboGroup.Items.Count - 1; boContactGroup = true;
                }
            }

            if (!boContactGroup) cboGroup.SelectedIndex = 0;

            if (mContactDetails.ContactID != 0)
            {
                txtContactCode.Text = mContactDetails.ContactCode;
                txtCustomerName.Text = mContactDetails.ContactName;
                txtBusinessName.Text = mContactDetails.BusinessName;
                txtTelNo.Text = mContactDetails.TelephoneNo;
                txtAddress.Text = mContactDetails.Address;
                txtTINNo.Text = mContactDetails.TINNo;
                txtLTONo.Text = mContactDetails.LTONo;
                txtPriceLevel.Text = mContactDetails.PriceLevel.ToString("G").ToUpper();

                txtCreditLimit.Text = mContactDetails.CreditLimit.ToString("#,##0.#0");
                txtCredit.Text = mContactDetails.Credit.ToString("#,##0.#0");
                txtAvailableCredit.Text = (mContactDetails.CreditLimit - mContactDetails.Credit).ToString("#,##0.#0");
                txtTerms.Text = mContactDetails.Terms.ToString("#,##0");
                cboTerms.SelectedIndex = int.Parse(mContactDetails.ModeOfTerms.ToString("d"));
                chkIsCreditAllowed.Checked = mContactDetails.IsCreditAllowed;
            }
            else
            {
                Data.ERPConfig clsERPConfig = new Data.ERPConfig();
                BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.CustomerCode_Country_Code, BarcodeHelper.CustomerCode_ManufacturerCode, clsERPConfig.get_LastCustomerCode());
                txtContactCode.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                clsERPConfig.CommitAndDispose();
            }
        }

        private bool SaveRecord()
        {
            if (MessageBox.Show("Please validate the customer information details before proceeding. Are you sure you want to continue?", "RetailPlus ™", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return false;
            }
            else
            {
                Data.ContactDetails clsDetails = new Data.ContactDetails();
                clsDetails = mContactDetails;

                clsDetails.ContactCode = txtContactCode.Text;
                clsDetails.ContactName = txtCustomerName.Text;
                clsDetails.TINNo = txtTINNo.Text;
                clsDetails.LTONo = txtLTONo.Text;
                clsDetails.Address = txtAddress.Text;
                clsDetails.BusinessName = txtBusinessName.Text;
                clsDetails.TelephoneNo = txtTelNo.Text;

                // Jul 22, 2014
                clsDetails.Debit = mContactDetails.Debit;
                clsDetails.Credit = mContactDetails.Credit;
                clsDetails.IsCreditAllowed = chkIsCreditAllowed.Checked;
                clsDetails.CreditLimit = decimal.Parse(txtCreditLimit.Text);
                clsDetails.Terms = int.Parse(txtTerms.Text);
                clsDetails.ModeOfTerms = (ModeOfTerms)Enum.Parse(typeof(ModeOfTerms), cboTerms.SelectedItem.ToString());


                Data.Contacts clsContact = new Data.Contacts();
                Data.ContactGroups clsContactGroups = new Data.ContactGroups(clsContact.Connection, clsContact.Transaction);
                clsDetails.ContactGroupID = clsContactGroups.DetailsByName(cboGroup.SelectedItem.ToString()).ContactGroupID;
                
                if (mContactDetails.ContactID == 0)
                {
                    if (mstCaption == "Please enter customer name for deposit.")
                    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_DEPOSIT; }
                    else if (mstCaption == "Quickly add new customer")
                    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_QUICKLY_ADDED_FROM_FE; }
                    else if (mContactDetails.ContactID == 0) // means not edit
                    { clsDetails.Remarks = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT; }

                    clsDetails.PositionID = Constants.C_RETAILPLUS_AGENT_POSITIONID;
                    clsDetails.DepartmentID = Constants.C_RETAILPLUS_AGENT_DEPARTMENTID;
                    clsDetails.ContactID = clsContact.Insert(clsDetails);
                }
                else
                {
                    clsDetails.ContactGroupName = mContactDetails.ContactGroupName;
                    clsContact.Update(clsDetails);
                }
                clsContact.CommitAndDispose();

                mContactDetails = clsDetails;

                return true;
            }
        }

        #endregion

        private void cmdGenerateCustomerCode_Click(object sender, EventArgs e)
        {
            Data.ERPConfig clsERPConfig = new Data.ERPConfig();
            BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.CustomerCode_Country_Code, BarcodeHelper.CustomerCode_ManufacturerCode, clsERPConfig.get_LastCustomerCode());
            txtContactCode.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
            clsERPConfig.CommitAndDispose();
        }

    }
}
