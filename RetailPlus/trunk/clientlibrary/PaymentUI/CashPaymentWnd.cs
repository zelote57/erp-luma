using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class CashPaymentWnd : Form
	{
		private System.ComponentModel.Container components = null;
		private Label lblHeader;
        private GroupBox groupBox1;
		private Label lblCash;
		private TextBox txtAmount;
		private Label lblBalance;
		private Label lblBalanceAmount;
        private PictureBox imgIcon;
		private Label lblRemarks;
        private TextBox txtRemarks;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;

        #region public Properties

        public bool mboIsRefund;
        public bool IsRefund { set { mboIsRefund = value; } }

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
            set
            { mdecBalanceAmount = value; }
        }

        private Data.SalesTransactionDetails mclsSalesTransactionDetails;
        public Data.SalesTransactionDetails SalesTransactionDetails
        {
            set { mclsSalesTransactionDetails = value; }
        }

        private Data.CashPaymentDetails mDetails;
        public Data.CashPaymentDetails CashPaymentDetails
        {
            get { return mDetails; }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set { mclsTerminalDetails = value; }
        }

        #endregion

        #region Constructors and Destructors
        
        public CashPaymentWnd()
		{
			InitializeComponent();
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

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 7;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(125, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Tender Cash Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.lblCash);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.lblBalance);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
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
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(361, 134);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(267, 13);
            this.lblRemarks.TabIndex = 3;
            this.lblRemarks.Text = "Add an optional 255 character remarks below.";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCash.Location = new System.Drawing.Point(434, 58);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(117, 13);
            this.lblCash.TabIndex = 2;
            this.lblCash.Text = "Cash Amount (PHP)";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(118, 153);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(723, 56);
            this.txtRemarks.TabIndex = 1;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(392, 80);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblBalance.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblBalance.Location = new System.Drawing.Point(801, 17);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(201, 19);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Current Balance to be paid.";
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
            this.cmdCancel.TabStop = false;
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
            this.cmdEnter.TabStop = false;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // CashPaymentWnd
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
            this.Name = "CashPaymentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CashPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CashPaymentWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods
        
        private void CashPaymentWnd_KeyDown(object sender, KeyEventArgs e)
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

        private void CashPaymentWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CashPayment.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
                this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
                // 
                // keyboardNoControl1
                // 
                this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
                this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Up;
                this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Down;
                this.keyboardNoControl1.Location = new System.Drawing.Point(412, 310);
                this.keyboardNoControl1.Name = "keyboardNoControl1";
                this.keyboardNoControl1.Size = new System.Drawing.Size(202, 176);
                this.keyboardNoControl1.TabIndex = 86;
                this.keyboardNoControl1.TabStop = false;
                this.keyboardNoControl1.Visible = false;
                this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
                // 
                // keyboardSearchControl1
                // 
                this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
                this.keyboardSearchControl1.Location = new System.Drawing.Point(91, 310);
                this.keyboardSearchControl1.Name = "keyboardSearchControl1";
                this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
                this.keyboardSearchControl1.TabIndex = 53;
                this.keyboardSearchControl1.TabStop = false;
                this.keyboardSearchControl1.Tag = "";
                this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);

                this.Controls.Add(this.keyboardSearchControl1);
                this.Controls.Add(this.keyboardNoControl1);

                keyboardNoControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
            }

            lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            lblCash.Text = "Cash Amount (" + CompanyDetails.Currency + ")";

            if (mboIsRefund)
            {
                lblHeader.Text = "Tender Cash Amount to refund";
                lblBalance.Text = "Current Balance to be refunded.";
            }
		}

        #endregion

        #region Windows Control Methods

        private void txtAmount_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
        }

		private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			Methods clsMethods = new Methods();
			e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
		}
		
        private void txtRemarks_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTexBox = (TextBox)sender;

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
            }
		}

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox.Name == txtAmount.Name)
                txtAmount.Focus();
            else
                txtRemarks.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
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

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Private Methods

        private bool isValuesAssigned()
		{
			decimal Amount;
			try { Amount = Convert.ToDecimal(txtAmount.Text); }
			catch
			{
				txtAmount.Focus();
				MessageBox.Show("Sorry you have entered an invalid amount for cash payment." +
					"Please type a valid cash amount.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (Amount <= 0)
			{
				txtAmount.Focus();
				MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for cash payment","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

            mDetails.BranchDetails = mclsTerminalDetails.BranchDetails;
            mDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
			mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
			mDetails.Amount = Amount;
			mDetails.Remarks = txtRemarks.Text;
            mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;

			return true;
        }

        #endregion

    }
}
