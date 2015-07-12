using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ChequesPaymentWnd : Form
	{
		private System.ComponentModel.Container components = null;
		
		private Label label8;
		private Label lblBalanceAmount;
		private GroupBox groupBox1;
		private Label lblRemarks;
		private TextBox txtRemarks;
		private TextBox txtAmount;
		private Label lblHeader;
		private Label label1;
		private Label label3;
		private Label label2;
		private PictureBox imgIcon;
		private TextBox txtChequeNo;
		private TextBox txtValidityDate;
        private Button cmdCancel;
        private Button cmdEnter;
        private TextBox txtSelectedTextBox;
        private Label lblChequeAmount;
        private KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;

		#region public Properties

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
			{	mdecBalanceAmount = value;	}
		}

        private Data.SalesTransactionDetails mclsSalesTransactionDetails; 
        public Data.SalesTransactionDetails SalesTransactionDetails
		{
			set { mclsSalesTransactionDetails = value; }
		}

        private Data.ChequePaymentDetails mDetails = new Data.ChequePaymentDetails();
        public Data.ChequePaymentDetails Details
		{
			get
			{	return mDetails;	}
		}

        public Data.TerminalDetails TerminalDetails { get; set; }

        public Data.SysConfigDetails SysConfigDetails { get; set; }
        
		#endregion

		#region Constructors and Destructors

		public ChequesPaymentWnd()
		{
			InitializeComponent();

            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ChequesPayment.jpg"); }
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
            this.label8 = new System.Windows.Forms.Label();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidityDate = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblChequeAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label8.Location = new System.Drawing.Point(801, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(201, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Current Balance to be paid.";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(184, 30);
            this.lblBalanceAmount.TabIndex = 5;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.txtChequeNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtValidityDate);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblChequeAmount);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(362, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "(Format: mmddyy)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(67, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cheque No.:";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChequeNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChequeNo.Location = new System.Drawing.Point(207, 42);
            this.txtChequeNo.MaxLength = 0;
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(200, 30);
            this.txtChequeNo.TabIndex = 0;
            this.txtChequeNo.GotFocus += new System.EventHandler(this.txtChequeNo_GotFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(67, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Validity Date:";
            // 
            // txtValidityDate
            // 
            this.txtValidityDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValidityDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidityDate.Location = new System.Drawing.Point(207, 122);
            this.txtValidityDate.MaxLength = 6;
            this.txtValidityDate.Name = "txtValidityDate";
            this.txtValidityDate.Size = new System.Drawing.Size(149, 30);
            this.txtValidityDate.TabIndex = 2;
            this.txtValidityDate.GotFocus += new System.EventHandler(this.txtValidityDate_GotFocus);
            this.txtValidityDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidityDate_KeyPress);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(67, 165);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(126, 13);
            this.lblRemarks.TabIndex = 7;
            this.lblRemarks.Text = "Remarks (optional) : ";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(207, 162);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(600, 50);
            this.txtRemarks.TabIndex = 3;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // lblChequeAmount
            // 
            this.lblChequeAmount.AutoSize = true;
            this.lblChequeAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChequeAmount.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblChequeAmount.Location = new System.Drawing.Point(67, 85);
            this.lblChequeAmount.Name = "lblChequeAmount";
            this.lblChequeAmount.Size = new System.Drawing.Size(135, 13);
            this.lblChequeAmount.TabIndex = 5;
            this.lblChequeAmount.Text = "Cheque Amount (PHP):";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(207, 82);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(140, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Tender Cheque Amount";
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
            // ChequesPaymentWnd
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
            this.Name = "ChequesPaymentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ChequesPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChequesPaymentWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

        #region Windows Form Methods

        private void ChequesPaymentWnd_KeyDown(object sender, KeyEventArgs e)
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
        private void ChequesPaymentWnd_Load(object sender, System.EventArgs e)
        {
            if (TerminalDetails.WithRestaurantFeatures)
            {
                this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
                this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();

                // 
                // keyboardSearchControl1
                // 
                this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
                this.keyboardSearchControl1.Location = new System.Drawing.Point(91, 310);
                this.keyboardSearchControl1.Name = "keyboardSearchControl1";
                this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
                this.keyboardSearchControl1.TabIndex = 91;
                this.keyboardSearchControl1.TabStop = false;
                this.keyboardSearchControl1.Tag = "";
                this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
                // 
                // keyboardNoControl1
                // 
                this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
                this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Clear;
                this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.SelectAll;
                this.keyboardNoControl1.Location = new System.Drawing.Point(412, 310);
                this.keyboardNoControl1.Name = "keyboardNoControl1";
                this.keyboardNoControl1.Size = new System.Drawing.Size(208, 172);
                this.keyboardNoControl1.TabIndex = 92;
                this.keyboardNoControl1.TabStop = false;
                this.keyboardNoControl1.Visible = false;
                this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);

                this.Controls.Add(this.keyboardSearchControl1);
                this.Controls.Add(this.keyboardNoControl1);

                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = TerminalDetails.WithRestaurantFeatures;
            }

            lblChequeAmount.Text = "Cheque Amount (" + CompanyDetails.Currency + "):";
            lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
            txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");

            
        }

        #endregion

        #region Windows Control Methods

		private void txtChequeNo_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = TerminalDetails.WithRestaurantFeatures;
            }
		}
		private void txtAmount_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
		}
		private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			Methods clsMethods = new Methods();
			e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
		}
		private void txtValidityDate_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = TerminalDetails.WithRestaurantFeatures;
                keyboardSearchControl1.Visible = false;
            }
		}
		private void txtValidityDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			Methods clsMethods = new Methods();
			e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
		}
		private void txtRemarks_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTextBox = (TextBox)sender;

            if (TerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = false;
                keyboardSearchControl1.Visible = TerminalDetails.WithRestaurantFeatures;
            }
		}
        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTextBox == null)
                txtChequeNo.Focus();
            if (txtSelectedTextBox.Name == txtChequeNo.Name)
                txtChequeNo.Focus();
            else if (txtSelectedTextBox.Name == txtAmount.Name)
                txtAmount.Focus();
            else if (txtSelectedTextBox.Name == txtValidityDate.Name)
                txtValidityDate.Focus();
            else if (txtSelectedTextBox.Name == txtRemarks.Name)
                txtRemarks.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }
        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTextBox.Name == txtAmount.Name ||
                txtSelectedTextBox.Name == txtValidityDate.Name)
            {
                txtSelectedTextBox.Focus();
                if (e.KeyboardKeyPressed == "{CLEAR}")
                    txtSelectedTextBox.Text = "";
                else if (e.KeyboardKeyPressed == "{SELECTALL}")
                    txtSelectedTextBox.SelectAll();
                else if (e.KeyboardKeyPressed == "." & txtSelectedTextBox.Text.IndexOf(".") < 0)
                    SendKeys.Send(e.KeyboardKeyPressed);
                else if (e.KeyboardKeyPressed != ".")
                    SendKeys.Send(e.KeyboardKeyPressed);
            }
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
        
        #region Private Modifiers

        private bool isValuesAssigned()
		{
			if (txtChequeNo.Text == null || txtChequeNo.Text == "")
			{
				MessageBox.Show("Please type a valid Cheque No.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				txtChequeNo.Focus();
				return false;
			}
			decimal Amount;
			try 
			{
				Amount = Convert.ToDecimal(txtAmount.Text);
			}
			catch
			{
				txtAmount.Focus();
				MessageBox.Show("Sorry you have entered an invalid amount for cheque payment." +
					"Please type a valid cheque amount.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (Amount <= 0)
			{
				txtAmount.Focus();
				MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for cheque payment","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			DateTime ValidityDate = DateTime.MinValue;
			if (txtValidityDate.Text == null || txtValidityDate.Text == "")
			{
				txtValidityDate.Focus();
				MessageBox.Show("Please type a valid Validity Date.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			else if (txtValidityDate.Text != null && txtValidityDate.Text != "")
			{
				
				try 
				{
					string Month = txtValidityDate.Text.Substring(0,2);
					string Day = txtValidityDate.Text.Substring(2,2);
					string Year = txtValidityDate.Text.Substring(4,2);
					ValidityDate = Convert.ToDateTime(Month + "/" + Day + "/" + Year);
				}
				catch
				{
					txtValidityDate.Focus();
					MessageBox.Show("Sorry you have entered an invalid date for validity date." +
						"Please type a valid validity date.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
                if (ValidityDate < DateTime.Now.AddDays(-SysConfigDetails.ChequePaymentAcceptableNoOfDays))
				{
					txtValidityDate.Focus();
					MessageBox.Show("Cheque has been expired, please ask for a valid cheque.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}
			}

            mDetails.BranchDetails = TerminalDetails.BranchDetails;
            mDetails.TerminalNo = TerminalDetails.TerminalNo;
            mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
            mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;
			mDetails.ChequeNo = txtChequeNo.Text;
			mDetails.Amount = Amount;
			mDetails.ValidityDate = ValidityDate;
			mDetails.Remarks = txtRemarks.Text;

			return true;
		}

		#endregion

        
	}
}
