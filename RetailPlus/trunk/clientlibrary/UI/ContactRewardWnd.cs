using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ContactRewardAddWnd.
	/// </summary>
	public class ContactRewardWnd : System.Windows.Forms.Form
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
        private TextBox txtDOB;
        private Label label1;
        private TextBox txtRewardCardNo;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private Label label5;
        private TextBox txtRewardCardExpiryDate;
		private string mstCaption;
        private RewardCardStatus mRewardCardStatus;

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
        public RewardCardStatus RewardCardStatus
        {
            get { return mRewardCardStatus; }
            set { mRewardCardStatus = value; }
        }
		public Data.ContactDetails ContactDetails
		{
			get {	return mContactDetails;	}
			set {	mContactDetails = value;	}
		}

        public Data.TerminalDetails TerminalDetails { get; set; }
        
		#region Constructors And Desctructors
		public ContactRewardWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.txtRewardCardNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRewardCardExpiryDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDOB = new System.Windows.Forms.TextBox();
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
            this.groupBox1.Controls.Add(this.txtRewardCardNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRewardCardExpiryDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDOB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Details";
            // 
            // txtRewardCardNo
            // 
            this.txtRewardCardNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtRewardCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRewardCardNo.Enabled = false;
            this.txtRewardCardNo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRewardCardNo.Location = new System.Drawing.Point(114, 70);
            this.txtRewardCardNo.MaxLength = 15;
            this.txtRewardCardNo.Name = "txtRewardCardNo";
            this.txtRewardCardNo.Size = new System.Drawing.Size(560, 33);
            this.txtRewardCardNo.TabIndex = 0;
            this.txtRewardCardNo.Text = "2011-0000000000000001";
            this.txtRewardCardNo.GotFocus += new System.EventHandler(this.txtRewardCardNo_GotFocus);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(71, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Please enter Rewards Card Expiry Date (yyyy-MM-dd)";
            // 
            // txtRewardCardExpiryDate
            // 
            this.txtRewardCardExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRewardCardExpiryDate.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRewardCardExpiryDate.Location = new System.Drawing.Point(114, 143);
            this.txtRewardCardExpiryDate.MaxLength = 10;
            this.txtRewardCardExpiryDate.Name = "txtRewardCardExpiryDate";
            this.txtRewardCardExpiryDate.Size = new System.Drawing.Size(282, 33);
            this.txtRewardCardExpiryDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(513, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Update the Date Of Birth (yyyy-MM-dd)";
            // 
            // txtDOB
            // 
            this.txtDOB.BackColor = System.Drawing.SystemColors.Window;
            this.txtDOB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDOB.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.txtDOB.Location = new System.Drawing.Point(554, 143);
            this.txtDOB.MaxLength = 10;
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(200, 33);
            this.txtDOB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(71, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reward Card Number (this is an autogenerated card no by the system)";
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
            // ContactRewardWnd
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
            this.Name = "ContactRewardWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactRewardAddWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactRewardAddWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods
		private void ContactRewardAddWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		private void ContactRewardAddWnd_Load(object sender, System.EventArgs e)
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

			this.lblHeader.Text = mstCaption;
            this.LoadRecord();
            
		}
		
		#endregion

        #region Windows Control Methods

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox.Name == txtDOB.Name)
                txtDOB.Focus();
            else if (txtSelectedTexBox.Name == txtRewardCardNo.Name)
                txtRewardCardNo.Focus();
            else if (txtSelectedTexBox.Name == txtRewardCardExpiryDate.Name)
                txtRewardCardExpiryDate.Focus();

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
                clsEvent.AddEventLn("ERROR!!! Saving Reward Card details. Err Description: " + ex.Message);
                MessageBox.Show("Sorry the Reward Card No is already in the database. Please type another reward card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtRewardCardNo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        #endregion

		#region Private Methods
		private bool SaveRecord()
		{
            bool boRetValue = false;

            if (txtRewardCardNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Sorry please enter a valid reward card no.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }
            DateTime dteExpiryDate = DateTime.MinValue;
            try { dteExpiryDate = DateTime.Parse(txtRewardCardExpiryDate.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid expiry date in the ff format: YYYY-MM-dd ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }

            DateTime dteBirthDate = DateTime.MinValue;
            try { dteBirthDate = DateTime.Parse(txtDOB.Text); }
            catch
            {
                MessageBox.Show("Please enter a valid date of birth in the ff format: YYYY-MM-dd ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }

            if (mRewardCardStatus == RewardCardStatus.New)
            {
                if (MessageBox.Show("Are you sure you want to issue card no: " + txtRewardCardNo.Text + " to " + mContactDetails.ContactName + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mRewardCardStatus == RewardCardStatus.Lost || mRewardCardStatus == RewardCardStatus.Expired)
            {
                if (MessageBox.Show("Are you sure you want to declare card no: " + txtRewardCardNo.Text + " as " + mRewardCardStatus.ToString("G") + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mRewardCardStatus == RewardCardStatus.Replaced_Lost || mRewardCardStatus == RewardCardStatus.Replaced_Expired)
            {
                if (MessageBox.Show("Are you sure you want to replace existing reward card w/ new card no: " + txtRewardCardNo.Text + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mRewardCardStatus == RewardCardStatus.Reactivated_Lost)
            {
                if (MessageBox.Show("Are you sure you want to reactivate reward card no: " + txtRewardCardNo.Text + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }
            }
            else if (mRewardCardStatus == RewardCardStatus.ReNew)
            {
                if (MessageBox.Show("Are you sure you want to renew card no: " + txtRewardCardNo.Text + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return boRetValue; }

                if (dteExpiryDate < DateTime.Now)
                {
                    MessageBox.Show("Expiry date must not be less than date today. Please enter a valid expiry date. ", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return boRetValue;
                }
            }

            Data.ContactRewardDetails clsDetails = new Data.ContactRewardDetails();
            clsDetails.ContactID = mContactDetails.ContactID;
            clsDetails.RewardCardNo = txtRewardCardNo.Text;
            clsDetails.RewardPoints = 0;
            clsDetails.RewardAwardDate = DateTime.Now;
            clsDetails.ExpiryDate = dteExpiryDate;
            clsDetails.BirthDate = dteBirthDate;

            Data.ContactReward clsContactReward = new Data.ContactReward();
            Data.ContactRewardDetails clsContactRewardDetails = clsContactReward.Details(txtRewardCardNo.Text);

            if (clsContactRewardDetails.ContactID != Constants.ZERO)
            {
                if (mRewardCardStatus == RewardCardStatus.New || (mRewardCardStatus != RewardCardStatus.New && clsContactRewardDetails.ContactID != mContactDetails.ContactID))
                {
                    clsContactReward.CommitAndDispose();

                    MessageBox.Show("Reward Card No: " + clsContactRewardDetails.RewardCardNo + " was already issued on " + clsContactRewardDetails.RewardAwardDate.ToString("MMM dd, yyyy") + " to another customer." +
                                    Environment.NewLine + "Please enter another Reward Card No.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtRewardCardNo.Focus();
                    txtRewardCardNo.SelectAll();
                    return boRetValue;
                }
            }

            if (mRewardCardStatus == RewardCardStatus.New)
            {
                clsDetails.RewardActive = true;
            }
            else if (mRewardCardStatus == RewardCardStatus.Lost || mRewardCardStatus == RewardCardStatus.Expired)
            {
                clsDetails.RewardActive = false;
            }
            else if (mRewardCardStatus == RewardCardStatus.Replaced_Lost ||
                mRewardCardStatus == RewardCardStatus.Replaced_Expired ||
                mRewardCardStatus == RewardCardStatus.Reactivated_Lost ||
                mRewardCardStatus == RewardCardStatus.ReNew)
            {
                clsDetails.RewardActive = true;
            }
            clsDetails.RewardCardStatus = mRewardCardStatus;

            boRetValue = clsContactReward.Update(clsDetails);
            mContactDetails.RewardDetails = clsDetails;

            clsContactReward.CommitAndDispose();

            boRetValue = true;

            return boRetValue;
		}
        private void LoadRecord()
        {
            lblHeader.Text = mstCaption + " for customer : " + mContactDetails.ContactName;

            txtDOB.Text = DateTime.Now.AddYears(-18).ToString("yyyy-MM-dd");
            txtRewardCardNo.Text = mContactDetails.ContactCode.Substring(0,15);
            txtRewardCardExpiryDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");

            Data.ContactReward clsContactReward = new Data.ContactReward();
            Data.ContactRewardDetails clsContactRewardDetails = clsContactReward.Details(mContactDetails.ContactID);
            if (clsContactRewardDetails.ContactID != Constants.ZERO)
            {
                txtRewardCardNo.Text = clsContactRewardDetails.RewardCardNo;
                txtDOB.Text = clsContactRewardDetails.BirthDate.ToString("yyyy-MM-dd");
                txtRewardCardExpiryDate.Text = clsContactRewardDetails.ExpiryDate.ToString("yyyy-MM-dd");
            }
            if (TerminalDetails.AutoGenerateRewardCardNo)
            {
                if ((mRewardCardStatus == RewardCardStatus.New && clsContactRewardDetails.ContactID == Constants.ZERO) ||
                        mRewardCardStatus == RewardCardStatus.Replaced_Lost)
                {
                    Data.ERPConfig clsERPConfig = new Data.ERPConfig(clsContactReward.Connection, clsContactReward.Transaction);

                    BarcodeHelper ean13 = new BarcodeHelper(BarcodeHelper.RewardCard_Country_Code, BarcodeHelper.RewardCard_ManufacturerCode, clsERPConfig.get_LastRewardCardNo());
                    txtRewardCardNo.Text = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
                }
            }
            else
            {
                txtRewardCardNo.Enabled = true;
            }
            clsContactReward.CommitAndDispose();

            if (mRewardCardStatus == RewardCardStatus.New && clsContactRewardDetails.ContactID != Constants.ZERO)
            {
                txtRewardCardExpiryDate.Enabled = false; this.Refresh(); txtRewardCardNo.Focus();
                MessageBox.Show("Reward Card No: " + clsContactRewardDetails.RewardCardNo + " was already issued last " + clsContactRewardDetails.RewardAwardDate.ToString("MMM dd, yyyy") + " to " + mContactDetails.ContactName + "." +
                                Environment.NewLine + "Please select another customer.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (mRewardCardStatus == RewardCardStatus.Lost || mRewardCardStatus == RewardCardStatus.Expired)
            {
                txtRewardCardExpiryDate.Enabled = false; txtDOB.Focus();
            }
            else if (mRewardCardStatus == RewardCardStatus.Replaced_Lost || mRewardCardStatus == RewardCardStatus.Replaced_Expired)
            {
                txtRewardCardExpiryDate.Enabled = true; txtRewardCardExpiryDate.Focus();
            }
            else if (mRewardCardStatus == RewardCardStatus.Reactivated_Lost)
            {
                txtRewardCardNo.Enabled = true; txtRewardCardExpiryDate.Enabled = true; txtRewardCardNo.Focus();
            }
            else if (mRewardCardStatus == RewardCardStatus.ReNew)
            {
                txtRewardCardExpiryDate.Enabled = true; txtRewardCardExpiryDate.Focus(); 
            }
            
        }

		#endregion

    }
}
