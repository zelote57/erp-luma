using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class DebitPaymentWnd : Form
	{
		private Label lblBalanceAmount;
		private Label lblHeader;
		private GroupBox groupBox1;
		private Label lblRemarks;
		private TextBox txtRemarks;
		private TextBox txtAmount;
		private Label lblDebitOrDeposit;
		private PictureBox imgIcon;
		private Label lblDebit;
		private Label lblAllowedDebit;
		private Label label2;
		//private TextBox txtSelectedTexBox;
		private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
		private Button cmdCancel;
		private Button cmdEnter;
		private System.ComponentModel.Container components = null;
		
		private decimal mdecAllowedDebit = 0;
		private DialogResult dialog;
		private decimal mdecBalanceAmount;
		private AceSoft.KeyBoardHook.KeyboardNoControl keyboardNoControl1;
		private bool mboIsRefund = false;
		private Data.ContactDetails mclsCustomerDetails;
		private Data.DebitPaymentDetails mDetails = new Data.DebitPaymentDetails();
		Data.SalesTransactionDetails mclsSalesTransactionDetails;

		public bool IsRefund
		{
			set { mboIsRefund = value; }
		}
		public decimal AllowedDebit 
		{
			set {	mdecAllowedDebit = value;	}
		}
		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}
		public Data.SalesTransactionDetails SalesTransactionDetails 
		{
			set { mclsSalesTransactionDetails = value; }
		}
		public Data.ContactDetails CustomerDetails
		{
			set { mclsCustomerDetails = value; }
		}
		public decimal BalanceAmount
		{
			set
			{
				mdecBalanceAmount = value;
			}
		}
		public Data.DebitPaymentDetails Details
		{
			get
			{	return mDetails;	}
		}

		public DebitPaymentWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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
			this.lblRemarks = new System.Windows.Forms.Label();
			this.txtRemarks = new System.Windows.Forms.TextBox();
			this.lblDebit = new System.Windows.Forms.Label();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.lblDebitOrDeposit = new System.Windows.Forms.Label();
			this.imgIcon = new System.Windows.Forms.PictureBox();
			this.lblAllowedDebit = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdEnter = new System.Windows.Forms.Button();
			this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// lblBalanceAmount
			// 
			this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
			this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
			this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
			this.lblBalanceAmount.Name = "lblBalanceAmount";
			this.lblBalanceAmount.Size = new System.Drawing.Size(184, 32);
			this.lblBalanceAmount.TabIndex = 86;
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
			this.lblHeader.Size = new System.Drawing.Size(128, 13);
			this.lblHeader.TabIndex = 83;
			this.lblHeader.Text = "Tender Debit Amount";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.lblRemarks);
			this.groupBox1.Controls.Add(this.txtRemarks);
			this.groupBox1.Controls.Add(this.lblDebit);
			this.groupBox1.Controls.Add(this.txtAmount);
			this.groupBox1.Controls.Add(this.lblBalanceAmount);
			this.groupBox1.Controls.Add(this.lblDebitOrDeposit);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Blue;
			this.groupBox1.Location = new System.Drawing.Point(9, 67);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1008, 237);
			this.groupBox1.TabIndex = 84;
			this.groupBox1.TabStop = false;
			// 
			// lblRemarks
			// 
			this.lblRemarks.AutoSize = true;
			this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblRemarks.Location = new System.Drawing.Point(361, 134);
			this.lblRemarks.Name = "lblRemarks";
			this.lblRemarks.Size = new System.Drawing.Size(267, 13);
			this.lblRemarks.TabIndex = 17;
			this.lblRemarks.Text = "Add an optional 255 character remarks below.";
			// 
			// txtRemarks
			// 
			this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(118, 153);
			this.txtRemarks.MaxLength = 255;
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(723, 56);
			this.txtRemarks.TabIndex = 1;
			this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
			// 
			// lblDebit
			// 
			this.lblDebit.AutoSize = true;
			this.lblDebit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDebit.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblDebit.Location = new System.Drawing.Point(434, 58);
			this.lblDebit.Name = "lblDebit";
			this.lblDebit.Size = new System.Drawing.Size(120, 13);
			this.lblDebit.TabIndex = 15;
			this.lblDebit.Text = "Debit Amount (PHP)";
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
			// lblDebitOrDeposit
			// 
			this.lblDebitOrDeposit.AutoSize = true;
			this.lblDebitOrDeposit.BackColor = System.Drawing.Color.Transparent;
			this.lblDebitOrDeposit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDebitOrDeposit.ForeColor = System.Drawing.Color.LightSlateGray;
			this.lblDebitOrDeposit.Location = new System.Drawing.Point(801, 17);
			this.lblDebitOrDeposit.Name = "lblDebitOrDeposit";
			this.lblDebitOrDeposit.Size = new System.Drawing.Size(201, 19);
			this.lblDebitOrDeposit.TabIndex = 85;
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
			// lblAllowedDebit
			// 
			this.lblAllowedDebit.BackColor = System.Drawing.Color.Transparent;
			this.lblAllowedDebit.Font = new System.Drawing.Font("Tahoma", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAllowedDebit.ForeColor = System.Drawing.Color.DarkSalmon;
			this.lblAllowedDebit.Location = new System.Drawing.Point(620, 48);
			this.lblAllowedDebit.Name = "lblAllowedDebit";
			this.lblAllowedDebit.Size = new System.Drawing.Size(184, 16);
			this.lblAllowedDebit.TabIndex = 88;
			this.lblAllowedDebit.Text = "0.00";
			this.lblAllowedDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
			this.label2.Location = new System.Drawing.Point(811, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 16);
			this.label2.TabIndex = 87;
			this.label2.Text = "Allowed debit amount.";
			// 
			// keyboardSearchControl1
			// 
			this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
			this.keyboardSearchControl1.Location = new System.Drawing.Point(95, 323);
			this.keyboardSearchControl1.Name = "keyboardSearchControl1";
			this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
			this.keyboardSearchControl1.TabIndex = 4;
			this.keyboardSearchControl1.TabStop = false;
			this.keyboardSearchControl1.Tag = "";
			this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
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
			this.cmdCancel.TabIndex = 3;
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
			this.cmdEnter.TabIndex = 2;
			this.cmdEnter.Text = "ENTER";
			this.cmdEnter.UseVisualStyleBackColor = true;
			this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
			// 
			// keyboardNoControl1
			// 
			this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
			this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Up;
			this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Down;
			this.keyboardNoControl1.Location = new System.Drawing.Point(400, 323);
			this.keyboardNoControl1.Name = "keyboardNoControl1";
			this.keyboardNoControl1.Size = new System.Drawing.Size(202, 176);
			this.keyboardNoControl1.TabIndex = 89;
			this.keyboardNoControl1.TabStop = false;
			this.keyboardNoControl1.Visible = false;
			this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
			// 
			// DebitPaymentWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1022, 766);
			this.ControlBox = false;
			this.Controls.Add(this.keyboardNoControl1);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdEnter);
			this.Controls.Add(this.keyboardSearchControl1);
			this.Controls.Add(this.lblAllowedDebit);
			this.Controls.Add(this.lblHeader);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.imgIcon);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DebitPaymentWnd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.DebitPaymentWnd_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebitPaymentWnd_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		#region Windows Form Methods
		
		private void DebitPaymentWnd_KeyDown(object sender, KeyEventArgs e)
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
		private void DebitPaymentWnd_Load(object sender, System.EventArgs e)
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

			lblAllowedDebit.Text = mdecAllowedDebit.ToString("#,##0.#0");
			lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
			txtAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
			lblDebit.Text = "Debit Amount (" + CompanyDetails.Currency + ")";
			if (mboIsRefund)
				lblDebitOrDeposit.Text = "Current Balance to be deposited.";
			else
				lblDebitOrDeposit.Text = "Current Balance to be paid.";
		}

		#endregion

		#region Windows Control Methods

		private void txtAmount_GotFocus(object sender, System.EventArgs e)
		{
			keyboardNoControl1.Visible = true;
			keyboardSearchControl1.Visible = false;
		}
		private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			Methods clsMethods = new Methods();
			e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
		}
		private void txtRemarks_GotFocus(object sender, System.EventArgs e)
		{
			keyboardNoControl1.Visible = false;
			keyboardSearchControl1.Visible = true;
		}
		private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
		{
			txtRemarks.Focus();
			SendKeys.Send(e.KeyboardKeyPressed);
		}
		private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
		{
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
				MessageBox.Show("Sorry you have entered an invalid amount for debit payment." +
					"Please type a valid debit amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (!mboIsRefund)
			{
				if (Amount > mdecAllowedDebit)
				{
					txtAmount.Focus();
					MessageBox.Show("Amount must be less than the debit limit (" + mdecAllowedDebit.ToString("#,##0.#0") + "). Please enter a lower amount for debit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				MessageBox.Show("Amount must be greater than zero. Please enter a higher amount for debit payment.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			mDetails.TransactionID = mclsSalesTransactionDetails.TransactionID;
			mDetails.TransactionNo = mclsSalesTransactionDetails.TransactionNo;
			mDetails.CustomerDetails = mclsCustomerDetails;
			mDetails.Amount = Amount;
			mDetails.Remarks = txtRemarks.Text;

			return true;
		}

		#endregion
	}
}
