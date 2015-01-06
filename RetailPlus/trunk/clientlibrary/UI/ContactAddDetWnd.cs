using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ContactAddDetWnd.
	/// </summary>
	public class ContactAddDetWnd : System.Windows.Forms.Form
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
        private TextBox txtMobileNo;
        private Label label2;
        private TextBox txtFirstName;
        private Label label1;
        private TextBox txtAddress;
        private Label lblCaption;
        private TextBox txtContactCode;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label label16;
        private TextBox txtRemarks;
        private Label label15;
        private TextBox txtTelephoneNo;
        private Label label14;
        private TextBox txtBusinessName;
        private Label label13;
        private Label label12;
        private TextBox txtBirthDate;
        private Label label11;
        private Label label10;
        private TextBox txtLastName;
        private TextBox txtMiddleName;
        private ComboBox cboSalutation;
        private KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
        private Label label4;
        private TextBox txtContactName;
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
		public ContactAddDetWnd()
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
            this.cboSalutation = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTelephoneNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBusinessName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBirthDate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtContactCode = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtContactName);
            this.groupBox1.Controls.Add(this.cboSalutation);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtTelephoneNo);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtBusinessName);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtBirthDate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.txtMiddleName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMobileNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.lblCaption);
            this.groupBox1.Controls.Add(this.txtContactCode);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contact Details";
            // 
            // cboSalutation
            // 
            this.cboSalutation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSalutation.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSalutation.Location = new System.Drawing.Point(114, 102);
            this.cboSalutation.Name = "cboSalutation";
            this.cboSalutation.Size = new System.Drawing.Size(80, 31);
            this.cboSalutation.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.MediumBlue;
            this.label16.Location = new System.Drawing.Point(71, 263);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(114, 281);
            this.txtRemarks.MaxLength = 150;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(839, 51);
            this.txtRemarks.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.MediumBlue;
            this.label15.Location = new System.Drawing.Point(695, 206);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "TelePhone No";
            // 
            // txtTelephoneNo
            // 
            this.txtTelephoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelephoneNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephoneNo.Location = new System.Drawing.Point(698, 224);
            this.txtTelephoneNo.MaxLength = 75;
            this.txtTelephoneNo.Name = "txtTelephoneNo";
            this.txtTelephoneNo.Size = new System.Drawing.Size(255, 30);
            this.txtTelephoneNo.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(71, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "BusinessName";
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusinessName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusinessName.Location = new System.Drawing.Point(114, 162);
            this.txtBusinessName.MaxLength = 75;
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Size = new System.Drawing.Size(313, 30);
            this.txtBusinessName.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(500, 144);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "YYYY-MM-DD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.MediumBlue;
            this.label12.Location = new System.Drawing.Point(432, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(157, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Birth Date ( YYYY-MM-DD )";
            // 
            // txtBirthDate
            // 
            this.txtBirthDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBirthDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBirthDate.Location = new System.Drawing.Point(435, 162);
            this.txtBirthDate.MaxLength = 10;
            this.txtBirthDate.Name = "txtBirthDate";
            this.txtBirthDate.Size = new System.Drawing.Size(257, 30);
            this.txtBirthDate.TabIndex = 7;
            this.txtBirthDate.Text = "1900-01-01";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MediumBlue;
            this.label11.Location = new System.Drawing.Point(695, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Last Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.MediumBlue;
            this.label10.Location = new System.Drawing.Point(432, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Middle Name";
            // 
            // txtLastName
            // 
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(696, 103);
            this.txtLastName.MaxLength = 85;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(257, 30);
            this.txtLastName.TabIndex = 5;
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMiddleName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMiddleName.Location = new System.Drawing.Point(433, 103);
            this.txtMiddleName.MaxLength = 85;
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(257, 30);
            this.txtMiddleName.TabIndex = 4;
            this.txtMiddleName.TextChanged += new System.EventHandler(this.txtMiddleName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(695, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mobile No";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(698, 162);
            this.txtMobileNo.MaxLength = 75;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(255, 30);
            this.txtMobileNo.TabIndex = 8;
            this.txtMobileNo.GotFocus += new System.EventHandler(this.txtTelNo_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(71, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Salutation / First Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(195, 102);
            this.txtFirstName.MaxLength = 85;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(232, 30);
            this.txtFirstName.TabIndex = 3;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            this.txtFirstName.GotFocus += new System.EventHandler(this.txtBusinessName_GotFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(71, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Customer Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(114, 224);
            this.txtAddress.MaxLength = 150;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(578, 30);
            this.txtAddress.TabIndex = 9;
            this.txtAddress.GotFocus += new System.EventHandler(this.txtAddress_GotFocus);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCaption.Location = new System.Drawing.Point(71, 25);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(167, 13);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Please enter Customer Code";
            // 
            // txtContactCode
            // 
            this.txtContactCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactCode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactCode.Location = new System.Drawing.Point(114, 43);
            this.txtContactCode.MaxLength = 25;
            this.txtContactCode.Name = "txtContactCode";
            this.txtContactCode.Size = new System.Drawing.Size(313, 30);
            this.txtContactCode.TabIndex = 0;
            this.txtContactCode.GotFocus += new System.EventHandler(this.txtCustomerName_GotFocus);
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
            this.cmdCancel.TabIndex = 14;
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
            this.cmdEnter.TabIndex = 13;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(112, 422);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
            this.keyboardSearchControl1.TabIndex = 12;
            this.keyboardSearchControl1.TabStop = false;
            this.keyboardSearchControl1.Tag = "";
            this.keyboardSearchControl1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(432, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Please enter Customer Name";
            // 
            // txtContactName
            // 
            this.txtContactName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactName.Location = new System.Drawing.Point(435, 43);
            this.txtContactName.MaxLength = 25;
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(518, 30);
            this.txtContactName.TabIndex = 1;
            // 
            // ContactAddDetWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ContactAddDetWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactAddDetWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactAddDetWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods
		private void ContactAddDetWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		private void ContactAddDetWnd_Load(object sender, System.EventArgs e)
		{
			this.lblHeader.Text = mstCaption;

            LoadOption();
		}
		
		#endregion

		#region Windows Control Methods

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox == null)
                txtContactCode.Focus();
            else if (txtSelectedTexBox.Name == txtContactCode.Name)
                txtContactCode.Focus();
            else if (txtSelectedTexBox.Name == txtFirstName.Name)
                txtFirstName.Focus();
            else if (txtSelectedTexBox.Name == txtMobileNo.Name)
                txtMobileNo.Focus();
            else if (txtSelectedTexBox.Name == txtAddress.Name)
                txtAddress.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

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

        private void txtTerms_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtTerms_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContactName.Text)) txtContactName.Text = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text;
        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContactName.Text)) txtContactName.Text = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text;
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContactName.Text)) txtContactName.Text = txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text;
        }

        #endregion

        #region Private Methods

        private void LoadOption()
        {
            Data.Salutation clsSalutation = new Data.Salutation();
            System.Data.DataTable dt = clsSalutation.ListAsDataTable();
            clsSalutation.CommitAndDispose();

            cboSalutation.ValueMember = "SalutationCode";
            cboSalutation.DisplayMember = "SalutationName";
            cboSalutation.DataSource = dt.DefaultView;
            cboSalutation.SelectedIndex = 0;
            cboSalutation.SelectedValue = "MR";

            if (mContactDetails.ContactID != 0)
            {
                //txtContactCode.Enabled = false;
                txtContactCode.Text = mContactDetails.ContactCode;
                txtContactName.Text = mContactDetails.ContactName;
                txtAddress.Text = mContactDetails.Address;
                txtBusinessName.Text = mContactDetails.BusinessName;
                txtTelephoneNo.Text = mContactDetails.TelephoneNo;
                txtRemarks.Text = mContactDetails.Remarks;

                //txtDebit.Text = mContactDetails.Debit.ToString("###0.#0");
                //chkIsCreditAllowed.Checked = mContactDetails.IsCreditAllowed;
                //cboDepartment.SelectedIndex = cboDepartment.Items.IndexOf(cboDepartment.Items.FindByValue(mContactDetails.DepartmentID.ToString()));
                //cboPosition.SelectedIndex = cboPosition.Items.IndexOf(cboPosition.Items.FindByValue(mContactDetails.PositionID.ToString()));

                //txtCreditCardNo.Text = mContactDetails.CreditDetails.CreditCardNo;
                //cboCreditCardType.SelectedIndex = cboCreditCardType.Items.IndexOf(cboCreditCardType.Items.FindByValue(mContactDetails.CreditDetails.CardTypeDetails.CardTypeID.ToString()));
                //txtCreditAwardDate.Text = mContactDetails.CreditDetails.CreditAwardDate.ToString("yyyy-MMM-dd");
                //txtExpiryDate.Text = mContactDetails.CreditDetails.ExpiryDate.ToString("yyyy-MMM-dd");
                //cboCreditCardStatus.SelectedIndex = cboCreditCardStatus.Items.IndexOf(cboCreditCardStatus.Items.FindByValue(mContactDetails.CreditDetails.CreditCardStatus.ToString("d")));
                //lblCreditCardActive.Text = mContactDetails.CreditDetails.CreditActive ? "Active" : "InActive (Hold/Suspended)";
                //txtCreditLimit.Text = mContactDetails.CreditLimit.ToString("###0.#0");
                //txtCredit.Text = mContactDetails.Credit.ToString("###0.#0");
                //txtPaidAmount.Text = "0.00";
                //txtCurrentBalance.Text = (mContactDetails.CreditLimit - mContactDetails.Credit).ToString("###0.#0");
                //lblLastBillingDate.Text = "Last Billing Date:" + mContactDetails.CreditDetails.LastBillingDate.ToString("yyyy-MMM-dd");

                if (!string.IsNullOrEmpty(mContactDetails.AdditionalDetails.Salutation))
                {
                    cboSalutation.SelectedValue = mContactDetails.AdditionalDetails.Salutation;
                    txtFirstName.Text = mContactDetails.AdditionalDetails.FirstName;
                    txtMiddleName.Text = mContactDetails.AdditionalDetails.MiddleName;
                    txtLastName.Text = mContactDetails.AdditionalDetails.LastName;
                    txtBirthDate.Text = mContactDetails.AdditionalDetails.BirthDate.ToString("yyyy-MM-dd");
                    txtMobileNo.Text = mContactDetails.AdditionalDetails.MobileNo;
                }
            }
            else
            {
                Data.ERPConfig clsERPConfig = new Data.ERPConfig();
                BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.CustomerCode_Country_Code, BarcodeHelper.CustomerCode_ManufacturerCode, clsERPConfig.get_LastCustomerCode());
                txtContactCode.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                clsERPConfig.CommitAndDispose();
            }

            if (mstCaption == "Please enter customer name for deposit.")
            { txtRemarks.Text = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_DEPOSIT; }
            else if (mstCaption == "Quickly add new customer")
            { txtRemarks.Text = Data.Contacts.DEFAULT_REMARKS_FOR_QUICKLY_ADDED_FROM_FE; }
            else if (mContactDetails.ContactID == 0) // means not edit
            { txtRemarks.Text = Data.Contacts.DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT; }
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
                clsDetails.ContactName = txtContactName.Text;
                clsDetails.ContactGroupID = Convert.ToInt32(Constants.CONTACT_GROUP_CUSTOMER);
                clsDetails.ModeOfTerms = ModeOfTerms.Days;
                clsDetails.Terms = Convert.ToInt32("0");
                clsDetails.Address = txtAddress.Text;
                clsDetails.BusinessName = txtBusinessName.Text;
                clsDetails.TelephoneNo = txtTelephoneNo.Text;
                clsDetails.Remarks = txtRemarks.Text;
                //clsDetails.Debit = Convert.ToDecimal("0");
                //clsDetails.Credit = Convert.ToDecimal(txtCredit.Text);
                //clsDetails.IsCreditAllowed = chkIsCreditAllowed.Checked;
                //clsDetails.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text);
                //clsDetails.DepartmentID = Convert.ToInt16(cboDepartment.SelectedItem.Value);
                //clsDetails.PositionID = Convert.ToInt16(cboPosition.SelectedItem.Value);

                Data.ContactAddOnDetails clsAddOnDetails = new Data.ContactAddOnDetails();
                clsAddOnDetails.ContactID = clsDetails.ContactID;
                clsAddOnDetails.Salutation = cboSalutation.SelectedValue.ToString();
                clsAddOnDetails.FirstName = txtFirstName.Text;
                clsAddOnDetails.MiddleName = txtMiddleName.Text;
                clsAddOnDetails.LastName = txtLastName.Text;
                clsAddOnDetails.SpouseName = "";
                DateTime dteBirthDate = Constants.C_DATE_MIN_VALUE;
                dteBirthDate = DateTime.TryParse(txtBirthDate.Text, out dteBirthDate) ? dteBirthDate : Constants.C_DATE_MIN_VALUE;
                clsAddOnDetails.BirthDate = dteBirthDate;
                clsAddOnDetails.SpouseBirthDate = Constants.C_DATE_MIN_VALUE;
                clsAddOnDetails.AnniversaryDate = Constants.C_DATE_MIN_VALUE;
                clsAddOnDetails.Address1 = txtAddress.Text;
                clsAddOnDetails.Address2 = string.Empty;
                clsAddOnDetails.City = string.Empty;
                clsAddOnDetails.State = string.Empty;
                clsAddOnDetails.ZipCode = string.Empty;
                clsAddOnDetails.CountryID = Constants.C_DEF_COUNTRY_ID;
                clsAddOnDetails.CountryCode = Constants.C_DEF_COUNTRY_CODE;
                clsAddOnDetails.BusinessPhoneNo = txtTelephoneNo.Text;
                clsAddOnDetails.HomePhoneNo = string.Empty;
                clsAddOnDetails.MobileNo = txtMobileNo.Text;
                clsAddOnDetails.FaxNo = string.Empty;
                clsAddOnDetails.EmailAddress = string.Empty;

                clsDetails.AdditionalDetails = clsAddOnDetails;

                Data.Contacts clsContact = new Data.Contacts();
                if (mContactDetails.ContactID == 0)
                {
                    clsDetails.ContactGroupID = Constants.CONTACT_GROUP_CUSTOMER;
                    clsDetails.PositionID = Constants.C_RETAILPLUS_AGENT_POSITIONID;
                    clsDetails.DepartmentID = Constants.C_RETAILPLUS_AGENT_DEPARTMENTID;
                    clsDetails.ContactID = clsContact.Insert(clsDetails);
                }
                else
                {
                    //clsDetails.ContactCode = mContactDetails.ContactCode;
                    clsDetails.ContactGroupID = mContactDetails.ContactGroupID;
                    clsDetails.ContactGroupName = mContactDetails.ContactGroupName;
                    clsContact.Update(clsDetails);
                }
                clsContact.CommitAndDispose();

                mContactDetails = clsDetails;

                return true;
            }
        }

        #endregion

        
    }
}
