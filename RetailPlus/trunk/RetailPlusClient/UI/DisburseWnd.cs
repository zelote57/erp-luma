using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for DisburseWnd.
	/// </summary>
	public class DisburseWnd : System.Windows.Forms.Form
	{
        private Label label1;
		private Label label15;
		private GroupBox groupBox1;
		private Label lblRemarks;
		private TextBox txtRemarks;
		private TextBox txtAmount;
		private Label label2;
		private ComboBox cboType;
        private PictureBox imgIcon;
        private Label lblCurrency;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
		private System.ComponentModel.Container components = null;

        #region Property Get/Set

        Data.TerminalDetails mclsTerminalDetails = new Data.TerminalDetails();
        public Data.TerminalDetails TerminalDetails
        {
            set {   mclsTerminalDetails = value;    }

        }
        
        private DialogResult dialog;
		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

        private Data.DisburseDetails mclsDisburseDetails;
        public Data.DisburseDetails DisburseDetails
		{
			get
			{
                return mclsDisburseDetails;
			}
		}

        private Int64 mintCashierID;
		public Int64 CashierID
		{
			set
			{
				mintCashierID = value;
			}
		}

        #endregion

        #region Constructors and Destructors

        public DisburseWnd()
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
            this.ShowInTaskbar = mclsTerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
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
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
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
            this.imgIcon.TabIndex = 51;
            this.imgIcon.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Disburse or Pick-up amount .";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(38, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Enter";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblCurrency);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Press Enter to resume Transaction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(380, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select type of accountability";
            // 
            // cboType
            // 
            this.cboType.CausesValidation = false;
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboType.Items.AddRange(new object[] {
            "CASH",
            "CHEQUES",
            "CREDIT CARDS"});
            this.cboType.Location = new System.Drawing.Point(263, 100);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(402, 31);
            this.cboType.TabIndex = 1;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(331, 142);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(267, 13);
            this.lblRemarks.TabIndex = 5;
            this.lblRemarks.Text = "Add an optional 255 character remarks below.";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(196, 161);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(536, 38);
            this.txtRemarks.TabIndex = 2;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCurrency.Location = new System.Drawing.Point(421, 16);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(87, 13);
            this.lblCurrency.TabIndex = 3;
            this.lblCurrency.Text = "Amount (PHP)";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(364, 39);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 30);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
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
            // DisburseWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "DisburseWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DisburseWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisburseWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Window Form Methods

		private void DisburseWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Disburse.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			cboType.Items.Clear();
			cboType.Items.Add(PaymentTypes.Cash.ToString("G"));
			cboType.Items.Add(PaymentTypes.Cheque.ToString("G"));
			cboType.Items.Add(PaymentTypes.CreditCard.ToString("G"));
			cboType.SelectedIndex = 0;

			lblCurrency.Text = "Amount (" + CompanyDetails.Currency + ")";
			
		}

		private void DisburseWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

		#endregion

		#region Window Control Methods

		private void txtAmount_GotFocus(object sender, System.EventArgs e)
		{
            txtSelectedTexBox = (TextBox)sender;
		}
		private void txtAmount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
            if (txtSelectedTexBox.Name == txtAmount.Name)
                txtAmount.Focus();
            else if (txtSelectedTexBox.Name == txtRemarks.Name)
                txtRemarks.Focus();

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
            bool boRetValue = false;
            try { Convert.ToDecimal(txtAmount.Text); }
            catch
            {
                MessageBox.Show("Sorry, the amount you entered is not valid." +
                    "Please check the amount you entered.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToDecimal(txtAmount.Text) > 0)
            {
                mclsDisburseDetails.Amount = Convert.ToDecimal(txtAmount.Text);
                mclsDisburseDetails.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), cboType.Text, true);
                mclsDisburseDetails.DateCreated = DateTime.Now;
                mclsDisburseDetails.TerminalNo = CompanyDetails.TerminalNo;
                mclsDisburseDetails.CashierID = mintCashierID;
                mclsDisburseDetails.BranchDetails = mclsTerminalDetails.BranchDetails;
                mclsDisburseDetails.Remarks = txtRemarks.Text;

                Data.CashierReports clsCashierReport = new Data.CashierReports();
                if (!clsCashierReport.IsDisburseAmountValid(mclsDisburseDetails))
                {
                    MessageBox.Show("Sorry, the amount you entered is greater than the " + cboType.Text + " sales." +
                        "Please check the amount you entered.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    boRetValue = false;
                }
                else
                { boRetValue = true; }
                clsCashierReport.CommitAndDispose();
            }
            else
            {
                MessageBox.Show("Sorry, amount must be greater than zero." +
                    "Please enter amount that is greater than zero.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                boRetValue = false;
            }
            return boRetValue;
        }

        #endregion
    }
}
