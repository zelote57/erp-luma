using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class RewardPointPaymentWnd : Form
	{
		private Label lblBalanceAmount;
		private Label lblHeader;
		private GroupBox groupBox1;
        private Label lblConversion;
		private TextBox txtAmount;
		private Label lblDebitOrDeposit;
		private PictureBox imgIcon;
		private Label lblRewards;
		private Label lblRewardPoints;
		private Label lblRewardPointsLabel;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private System.ComponentModel.Container components = null;
		
		private decimal mdecAllowedRewardsPointsCashEquivalent = 0;

        #region Public Properties

        private bool mboIsRefund = false;
        public bool IsRefund
        {
            set { mboIsRefund = value; }
        }

        private DialogResult dialog;
		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

        private decimal mdecBalanceAmount;
        public decimal BalanceAmount
		{
			set { mdecBalanceAmount = value; }
		}

        private Data.ContactDetails mclsContactDetails = new Data.ContactDetails();
        public Data.ContactDetails ContactDetails
        {
            get { return mclsContactDetails; }
            set { mclsContactDetails = value; }
        }

        private Data.TerminalDetails mclsTerminalDetails = new Data.TerminalDetails();
        public Data.TerminalDetails TerminalDetails
        {
            set { mclsTerminalDetails = value; }
        }

        private decimal mdecAmount;
        public decimal Amount
        {
            get { return mdecAmount; }
        }

        private decimal mdecRewardPointsPayment;
        public decimal RewardPointsPayment
        {
            get { return mdecRewardPointsPayment; }
            set { mdecRewardPointsPayment = value; }
        }

        private decimal mdecRewardConvertedPayment;
        public decimal RewardConvertedPayment
        {
            get { return mdecRewardConvertedPayment; }
            set { mdecRewardConvertedPayment = value; }
        }

        #endregion

        #region Constructors and Destructors

        public RewardPointPaymentWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

        #endregion

        #region Windows Form Designer generated code

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
		private void InitializeComponent()
		{
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblConversion = new System.Windows.Forms.Label();
            this.lblRewards = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblRewardPoints = new System.Windows.Forms.Label();
            this.lblRewardPointsLabel = new System.Windows.Forms.Label();
            this.lblDebitOrDeposit = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(184, 32);
            this.lblBalanceAmount.TabIndex = 4;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(185, 13);
            this.lblHeader.TabIndex = 83;
            this.lblHeader.Text = "Tender Reward Points Payment";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblConversion);
            this.groupBox1.Controls.Add(this.lblRewards);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.lblDebitOrDeposit);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.lblRewardPoints);
            this.groupBox1.Controls.Add(this.lblRewardPointsLabel);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblConversion
            // 
            this.lblConversion.BackColor = System.Drawing.Color.Transparent;
            this.lblConversion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblConversion.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblConversion.Location = new System.Drawing.Point(386, 155);
            this.lblConversion.Name = "lblConversion";
            this.lblConversion.Size = new System.Drawing.Size(266, 20);
            this.lblConversion.TabIndex = 2;
            this.lblConversion.Text = "1 Point = 0.50 cents";
            this.lblConversion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRewards
            // 
            this.lblRewards.AutoSize = true;
            this.lblRewards.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRewards.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRewards.Location = new System.Drawing.Point(434, 58);
            this.lblRewards.Name = "lblRewards";
            this.lblRewards.Size = new System.Drawing.Size(171, 13);
            this.lblRewards.TabIndex = 1;
            this.lblRewards.Text = "Reward Points Amount (PHP)";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(417, 75);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblRewardPoints
            // 
            this.lblRewardPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblRewardPoints.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRewardPoints.ForeColor = System.Drawing.Color.DarkSalmon;
            this.lblRewardPoints.Location = new System.Drawing.Point(514, 129);
            this.lblRewardPoints.Name = "lblRewardPoints";
            this.lblRewardPoints.Size = new System.Drawing.Size(184, 16);
            this.lblRewardPoints.TabIndex = 5;
            this.lblRewardPoints.Text = "0.00";
            this.lblRewardPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRewardPointsLabel
            // 
            this.lblRewardPointsLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblRewardPointsLabel.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblRewardPointsLabel.Location = new System.Drawing.Point(346, 126);
            this.lblRewardPointsLabel.Name = "lblRewardPointsLabel";
            this.lblRewardPointsLabel.Size = new System.Drawing.Size(165, 23);
            this.lblRewardPointsLabel.TabIndex = 6;
            this.lblRewardPointsLabel.Text = "Available Reward Points";
            this.lblRewardPointsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDebitOrDeposit
            // 
            this.lblDebitOrDeposit.AutoSize = true;
            this.lblDebitOrDeposit.BackColor = System.Drawing.Color.Transparent;
            this.lblDebitOrDeposit.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblDebitOrDeposit.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDebitOrDeposit.Location = new System.Drawing.Point(801, 17);
            this.lblDebitOrDeposit.Name = "lblDebitOrDeposit";
            this.lblDebitOrDeposit.Size = new System.Drawing.Size(201, 19);
            this.lblDebitOrDeposit.TabIndex = 7;
            this.lblDebitOrDeposit.Text = "Current Balance to be paid.";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 82;
            this.imgIcon.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(533, 477);
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
            this.cmdEnter.Location = new System.Drawing.Point(639, 477);
            this.cmdEnter.Name = "cmdEnter";
            this.cmdEnter.Size = new System.Drawing.Size(106, 83);
            this.cmdEnter.TabIndex = 1;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // RewardPointPaymentWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RewardPointPaymentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RewardPointPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RewardPointPaymentWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        #region Windows Form Methods
        
        private void RewardPointPaymentWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.PageUp:
					SendKeys.Send("+{TAB}");
					break;

				case Keys.PageDown:
					SendKeys.Send("{TAB}");
					break;

				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (isValuesAssigned())
					{
						dialog = DialogResult.OK; 
						this.Hide();
					}
					break;
			}
		}
		private void RewardPointPaymentWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/DebitPayment.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            lblConversion.Text = mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue.ToString("#,##0.#0") + " points = " + mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent.ToString("#,##0.#0") + CompanyDetails.Currency;

            // put the mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue to zero if its 1 to avoid error
            mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue = mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue == 0 ? 1 : mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue;
            mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent = mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent == 0 ? 1 : mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent;

            mdecAllowedRewardsPointsCashEquivalent = (mclsContactDetails.RewardDetails.RewardPoints / mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue * mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent) - mdecRewardConvertedPayment;
            lblRewardPoints.Text = mdecAllowedRewardsPointsCashEquivalent.ToString("#,##0.#0");
            lblRewardPointsLabel.Text = Convert.ToDecimal(mclsContactDetails.RewardDetails.RewardPoints - mdecRewardPointsPayment).ToString("#,##0.#0") + " available points = ";
            lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            lblRewards.Text = "Reward Points Amount (" + CompanyDetails.Currency + ")";

        }

        #endregion

        #region Windows Control Methods

        private void txtAmount_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
        }
        private void txtRemarks_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }
        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox == null)
                txtAmount.Focus();
            else if (txtSelectedTexBox.Name == txtAmount.Name)
                txtAmount.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (isValuesAssigned())
            {
                dialog = DialogResult.OK;
                this.Hide();
            }
        }

        #endregion

        #region Private Methods

        private bool isValuesAssigned()
        {
            decimal Amount;
            try
            {
                Amount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                txtAmount.Focus();
                MessageBox.Show("Sorry you have entered an invalid amount for rewards payment." +
                    "Please type a valid rewards amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!mboIsRefund)
            {
                if (Amount > mdecAllowedRewardsPointsCashEquivalent)
                {
                    txtAmount.Focus();
                    MessageBox.Show("Amount must be less than the reward points limit (" + mdecAllowedRewardsPointsCashEquivalent.ToString("#,##0.#0") + "). Please enter a lower amount for debit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (Amount > mdecBalanceAmount)
            {
                txtAmount.Focus();
                MessageBox.Show("Amount must be less than the balance amount (" + mdecBalanceAmount.ToString("#,##0.#0") + "). Please enter a lower or equal amount for debit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Amount <= 0)
            {
                txtAmount.Focus();
                MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for reward points payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            mdecRewardPointsPayment += Amount / mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentCashEquivalent * mclsTerminalDetails.RewardPointsDetails.RewardPointsPaymentValue;
            mdecRewardConvertedPayment += Amount;
            mdecAmount = Amount;

            return true;
        }

        #endregion
    }
}
