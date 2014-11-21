using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ContactCreditAddWnd.
	/// </summary>
	public class ContactCreditWnd : System.Windows.Forms.Form
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
        private Label label2;
        private TextBox txtCreditLimit;
        private Label label1;
        private TextBox txtCreditCardNo;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label label5;
        private TextBox txtCreditCardExpiryDate;
		private string mstHeader;
        private Label label3;
        private TextBox txtRemarks;
        private CreditCardStatus mCreditCardStatus;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

		public string Header
		{
			get {	return mstHeader;	}
			set {	mstHeader = value;	}
		}
        public CreditCardStatus CreditCardStatus
        {
            get { return mCreditCardStatus; }
            set { mCreditCardStatus = value; }
        }

        public Data.ContactDetails Guarantor  { get; set; }
        public Data.CardTypeDetails CardTypeDetails  { get; set; }
		public Data.ContactDetails ContactDetails
		{
			get {	return mContactDetails;	}
			set {	mContactDetails = value;	}
		}


		#region Constructors And Desctructors
		public ContactCreditWnd()
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtCreditCardNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCreditCardExpiryDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCreditLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.txtCreditCardNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCreditCardExpiryDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCreditLimit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 318);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(71, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(296, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Please enter remarks or any additional information";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(114, 221);
            this.txtRemarks.MaxLength = 150;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(839, 68);
            this.txtRemarks.TabIndex = 11;
            // 
            // txtCreditCardNo
            // 
            this.txtCreditCardNo.AcceptsReturn = true;
            this.txtCreditCardNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCreditCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardNo.Enabled = false;
            this.txtCreditCardNo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditCardNo.Location = new System.Drawing.Point(114, 70);
            this.txtCreditCardNo.MaxLength = 15;
            this.txtCreditCardNo.Name = "txtCreditCardNo";
            this.txtCreditCardNo.Size = new System.Drawing.Size(560, 33);
            this.txtCreditCardNo.TabIndex = 0;
            this.txtCreditCardNo.Text = "2011-0000000000000001";
            this.txtCreditCardNo.GotFocus += new System.EventHandler(this.txtCreditCardNo_GotFocus);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(71, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(297, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Please enter Credit Card Expiry Date (yyyy-MM-dd)";
            // 
            // txtCreditCardExpiryDate
            // 
            this.txtCreditCardExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditCardExpiryDate.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreditCardExpiryDate.Location = new System.Drawing.Point(114, 143);
            this.txtCreditCardExpiryDate.MaxLength = 10;
            this.txtCreditCardExpiryDate.Name = "txtCreditCardExpiryDate";
            this.txtCreditCardExpiryDate.Size = new System.Drawing.Size(282, 33);
            this.txtCreditCardExpiryDate.TabIndex = 1;
            this.txtCreditCardExpiryDate.Text = "2011-11-02";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(513, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Please Enter Credit Limit";
            // 
            // txtCreditLimit
            // 
            this.txtCreditLimit.BackColor = System.Drawing.SystemColors.Window;
            this.txtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreditLimit.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtCreditLimit.Location = new System.Drawing.Point(554, 143);
            this.txtCreditLimit.MaxLength = 10;
            this.txtCreditLimit.Name = "txtCreditLimit";
            this.txtCreditLimit.Size = new System.Drawing.Size(200, 33);
            this.txtCreditLimit.TabIndex = 2;
            this.txtCreditLimit.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(71, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Credit Card Number (this is an autogenerated card no by the system)";
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
            // ContactCreditWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ContactCreditWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactCreditAddWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactCreditAddWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods
		private void ContactCreditAddWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
						clsEvent.AddEventLn("ERROR!!! Saving customer details. Err Description: " + ex.Message);
						MessageBox.Show("Sorry the customer name is already in the database. Please type another customer name." ,"RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					break;
			}
		}

		private void ContactCreditAddWnd_Load(object sender, System.EventArgs e)
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

			this.lblHeader.Text = mstHeader;
            this.LoadRecord();
            
		}
		
		#endregion

        #region Windows Control Methods

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox.Name == txtCreditLimit.Name)
                txtCreditLimit.Focus();
            else if (txtSelectedTexBox.Name == txtCreditCardNo.Name)
                txtCreditCardNo.Focus();
            else if (txtSelectedTexBox.Name == txtCreditCardExpiryDate.Name)
                txtCreditCardExpiryDate.Focus();

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
                clsEvent.AddEventLn("ERROR!!! Saving Credit Card details. Err Description: " + ex.Message);
                MessageBox.Show("Sorry the Credit Card No is already in the database. Please type another credit card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCreditCardNo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        #endregion

		#region Private Methods
		private bool SaveRecord()
		{
            bool boRetValue = false;

            if (string.IsNullOrEmpty(txtCreditCardNo.Text.Trim()))
            {
                MessageBox.Show("Sorry please enter a valid credit card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }
            DateTime dteExpiryDate = DateTime.MinValue;
            try { dteExpiryDate = DateTime.Parse(txtCreditCardExpiryDate.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid expiry date in the ff format: YYYY-MM-dd ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }
            decimal decCreditLimit = 0;
            try { decCreditLimit = decimal.Parse(txtCreditLimit.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid credit limit amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }

            if (mCreditCardStatus == CreditCardStatus.New)
            {
                if (MessageBox.Show("Are you sure you want to issue a new credit card to " + mContactDetails.ContactName + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mCreditCardStatus == CreditCardStatus.Lost || mCreditCardStatus == CreditCardStatus.Expired)
            {
                if (MessageBox.Show("Are you sure you want to declare card no: " + txtCreditCardNo.Text + " as " + mCreditCardStatus.ToString("G") + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mCreditCardStatus == CreditCardStatus.Replaced_Lost || mCreditCardStatus == CreditCardStatus.Replaced_Expired)
            {
                if (MessageBox.Show("Are you sure you want to replace existing credit card w/ new card no?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mCreditCardStatus == CreditCardStatus.Reactivated_Lost)
            {
                if (MessageBox.Show("Are you sure you want to reactivate existing credit card no: " + txtCreditCardNo.Text + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mCreditCardStatus == CreditCardStatus.ReNew)
            {
                if (MessageBox.Show("Are you sure you want to renew card no: " + txtCreditCardNo.Text + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }

                if (dteExpiryDate < DateTime.Now)
                {
                    MessageBox.Show("Expiry date must not be less than date today. Please enter a valid expiry date. ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return boRetValue;
                }
            }

            Data.ContactCreditCardInfos clsContactCreditCardInfos = new Data.ContactCreditCardInfos();
            Data.ContactCreditCardInfoDetails clsContactCreditCardInfoDetails = clsContactCreditCardInfos.Details(mContactDetails.ContactID);
            if (clsContactCreditCardInfoDetails.CustomerID != Constants.ZERO)
            {
                txtCreditCardNo.Text = clsContactCreditCardInfoDetails.CreditCardNo;
                txtCreditCardExpiryDate.Text = clsContactCreditCardInfoDetails.ExpiryDate.ToString("yyyy-MM-dd");
            }
            if (((mCreditCardStatus == CreditCardStatus.New && clsContactCreditCardInfoDetails.CustomerID == Constants.ZERO) ||
                    mCreditCardStatus == CreditCardStatus.Replaced_Lost) && (CardTypeDetails.WithGuarantor))
            {
                Data.ERPConfig clsERPConfig = new Data.ERPConfig(clsContactCreditCardInfos.Connection, clsContactCreditCardInfos.Transaction);
                BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.GroupCreditCard_Country_Code, BarcodeHelper.GroupCreditCard_ManufacturerCode, clsERPConfig.get_LastGroupCreditCardNo());
                txtCreditCardNo.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
            }
            else if ((mCreditCardStatus == CreditCardStatus.New && clsContactCreditCardInfoDetails.CustomerID == Constants.ZERO) ||
                    mCreditCardStatus == CreditCardStatus.Replaced_Lost)
            {
                Data.ERPConfig clsERPConfig = new Data.ERPConfig(clsContactCreditCardInfos.Connection, clsContactCreditCardInfos.Transaction);
                BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.CreditCard_Country_Code, BarcodeHelper.CreditCard_ManufacturerCode, clsERPConfig.get_LastCreditCardNo());
                txtCreditCardNo.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
            }
            clsContactCreditCardInfos.CommitAndDispose();
            
            Data.ContactCreditCardInfoDetails clsDetails = new Data.ContactCreditCardInfoDetails();
            clsDetails.CustomerID = mContactDetails.ContactID;
            clsDetails.CardTypeDetails = CardTypeDetails;
            clsDetails.CreditCardNo = txtCreditCardNo.Text;
            clsDetails.CreditAwardDate = DateTime.Now;
            clsDetails.ExpiryDate = dteExpiryDate;
            clsDetails.CreditLimit = decCreditLimit;

            clsContactCreditCardInfos = new Data.ContactCreditCardInfos();
            clsContactCreditCardInfoDetails = clsContactCreditCardInfos.Details(txtCreditCardNo.Text);

            if (clsContactCreditCardInfoDetails.CustomerID != Constants.ZERO)
            {
                if (mCreditCardStatus == CreditCardStatus.New || (mCreditCardStatus != CreditCardStatus.New && clsContactCreditCardInfoDetails.CustomerID != mContactDetails.ContactID))
                {
                    clsContactCreditCardInfos.CommitAndDispose();

                    MessageBox.Show("Credit Card No: " + clsContactCreditCardInfoDetails.CreditCardNo + " was already issued on " + clsContactCreditCardInfoDetails.CreditAwardDate.ToString("MMM dd, yyyy") + " to another customer." +
                                    Environment.NewLine + "Please enter another Credit Card No.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtCreditCardNo.Focus();
                    txtCreditCardNo.SelectAll();
                    return boRetValue;
                }
            }

            clsDetails.GuarantorID = clsContactCreditCardInfoDetails.GuarantorID;
            clsDetails.CardTypeDetails = clsContactCreditCardInfoDetails.CardTypeDetails;
            if (mCreditCardStatus == CreditCardStatus.New)
            {
                clsDetails.CreditActive = true;
                // override if new
                clsDetails.GuarantorID = Guarantor.ContactID;
                clsDetails.CardTypeDetails = CardTypeDetails;
            }
            else if (mCreditCardStatus == CreditCardStatus.Lost || mCreditCardStatus == CreditCardStatus.Expired)
            {
                clsDetails.CreditActive = false;
            }
            else if (mCreditCardStatus == CreditCardStatus.Replaced_Lost || 
                mCreditCardStatus == CreditCardStatus.Replaced_Expired ||
                mCreditCardStatus == CreditCardStatus.Reactivated_Lost ||
                mCreditCardStatus == CreditCardStatus.ReNew)
            {
                clsDetails.CreditActive = true;
            }
            clsDetails.CreditCardStatus = mCreditCardStatus;

            boRetValue = clsContactCreditCardInfos.Update(clsDetails);

            Data.Contacts clsContacts = new Data.Contacts(clsContactCreditCardInfos.Connection, clsContactCreditCardInfos.Transaction);

            clsContacts.UpdateRemarks(clsDetails.CustomerID, txtRemarks.Text, "");

            mContactDetails.CreditDetails = clsDetails;

            clsContactCreditCardInfos.CommitAndDispose();
            
            boRetValue = true;

            return boRetValue;
		}
        private void LoadRecord()
        {
            lblHeader.Text = mstHeader + " for customer : " + mContactDetails.ContactName;
            if ((CardTypeDetails.WithGuarantor) || (Guarantor.ContactID != 0 && Guarantor.ContactID != mContactDetails.ContactID)) lblHeader.Text += " --- Guarantor: " + Guarantor.ContactName + (Guarantor.CreditDetails.ExpiryDate == DateTime.MinValue ? "" : " [Expiry:" + Guarantor.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd") + "]");

            txtCreditCardNo.Text = mContactDetails.ContactCode;
             
            if ((CardTypeDetails.WithGuarantor) || (Guarantor.ContactID != 0 && Guarantor.ContactID != mContactDetails.ContactID))
                if (Guarantor.CreditDetails.ExpiryDate.AddMonths(6) <= DateTime.Now)
                    txtCreditCardExpiryDate.Text = (new DateTime(DateTime.Now.AddYears(2).Year, DateTime.Now.AddYears(2).Month, DateTime.DaysInMonth(DateTime.Now.AddYears(2).Year, DateTime.Now.AddYears(2).Month))).ToString("yyyy-MM-dd");
                else
                    txtCreditCardExpiryDate.Text = Guarantor.CreditDetails.ExpiryDate.ToString("yyyy-MM-dd");
            else
                txtCreditCardExpiryDate.Text = (new DateTime(DateTime.Now.AddYears(1).Year, DateTime.Now.AddYears(1).Month, DateTime.DaysInMonth(DateTime.Now.AddYears(1).Year, DateTime.Now.AddYears(1).Month))).ToString("yyyy-MM-dd");

            txtCreditLimit.Text = mContactDetails.CreditLimit.ToString("#,##0.#0");
            txtRemarks.Text = mContactDetails.Remarks;

            switch (mCreditCardStatus)
            {
                case CreditCardStatus.New:
                    txtCreditCardNo.Text = "WILL BE ASSIGNED AFTER SAVING";
                    break;
                case CreditCardStatus.Lost:
                case CreditCardStatus.Expired:
                case CreditCardStatus.Replaced_Lost:
                case CreditCardStatus.Replaced_Expired:
                case CreditCardStatus.ReNew:
                case CreditCardStatus.Reactivated_Lost:
                case CreditCardStatus.ManualDeactivated:
                    txtCreditCardNo.Text = mContactDetails.CreditDetails.CreditCardNo;
                    break;
                default:
                    txtCreditCardNo.Text = "WILL BE ASSIGNED AFTER SAVING";
                    break;
            }

            Data.ContactCreditCardInfos clsContactCreditCardInfos = new Data.ContactCreditCardInfos();
            Data.ContactCreditCardInfoDetails clsContactCreditDetails = clsContactCreditCardInfos.Details(mContactDetails.ContactID);
            
            clsContactCreditCardInfos.CommitAndDispose();

            if (mCreditCardStatus == CreditCardStatus.New && clsContactCreditDetails.CustomerID != Constants.ZERO)
            {
                txtCreditCardExpiryDate.Enabled = false; this.Refresh(); txtCreditCardNo.Focus();
                MessageBox.Show("Credit Card No: " + clsContactCreditDetails.CreditCardNo + " was already issued last " + clsContactCreditDetails.CreditAwardDate.ToString("MMM dd, yyyy") + " to " + mContactDetails.ContactName + "." +
                                Environment.NewLine + "Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (mCreditCardStatus == CreditCardStatus.Lost || mCreditCardStatus == CreditCardStatus.Expired)
            {
                txtCreditCardExpiryDate.Enabled = false; this.Refresh(); txtCreditCardNo.Focus();
            }
            else if (mCreditCardStatus == CreditCardStatus.Replaced_Lost || mCreditCardStatus == CreditCardStatus.Replaced_Expired)
            {
                txtCreditCardExpiryDate.Enabled = true; txtCreditCardExpiryDate.Focus();
            }
            else if (mCreditCardStatus == CreditCardStatus.ReNew)
            {
                txtCreditCardExpiryDate.Enabled = true; txtCreditCardExpiryDate.Focus();
            }
            else if (mCreditCardStatus == CreditCardStatus.Reactivated_Lost)
            {
                txtCreditCardNo.Enabled = true; txtCreditCardExpiryDate.Enabled = true; txtCreditLimit.Enabled = true; txtCreditCardNo.Focus();
            }
        }

		#endregion

    }
}
