using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    /// <summary>
    /// Summary description for PaidOutWnd.
    /// </summary>
    public class PaidOutWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboType;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.PictureBox imgIcon;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;

        #region Property Get/Set

        private Data.PaidOutDetails mclsPaidOutDetails;
        public Data.PaidOutDetails PaidOutDetails
        {
            get
            {
                return mclsPaidOutDetails;
            }
        }

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private Int64 mCashierID;
        public Int64 CashierID
        {
            set
            {
                mCashierID = value;
            }
        }

        private Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set
            {
                mclsTerminalDetails = value;
            }
        }

        #endregion

        #region Constructors and Destructors

        public PaidOutWnd()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

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


        #endregion

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
            this.lblCash = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
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
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Paid Out.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(35, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "Enter";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.lblRemarks);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblCash);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Press Enter to resume Transaction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(380, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 13);
            this.label2.TabIndex = 19;
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
            this.cboType.Location = new System.Drawing.Point(252, 102);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(425, 31);
            this.cboType.TabIndex = 1;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRemarks.Location = new System.Drawing.Point(331, 144);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(267, 13);
            this.lblRemarks.TabIndex = 17;
            this.lblRemarks.Text = "Add an optional 255 character remarks below.";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(196, 162);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(536, 37);
            this.txtRemarks.TabIndex = 2;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCash.Location = new System.Drawing.Point(421, 16);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(87, 13);
            this.lblCash.TabIndex = 15;
            this.lblCash.Text = "Amount (PHP)";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(364, 38);
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
            this.cmdCancel.TabIndex = 4;
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
            this.cmdEnter.TabIndex = 3;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(112, 316);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
            this.keyboardSearchControl1.TabIndex = 81;
            this.keyboardSearchControl1.TabStop = false;
            this.keyboardSearchControl1.Tag = "";
            // 
            // PaidOutWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "PaidOutWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PaidOutWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PaidOutWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Windows Form Methods

        private void PaidOutWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Disburse.jpg"); }
            catch { }
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

        }

        private void PaidOutWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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

        #region Windows Control Methods

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
            if (txtSelectedTexBox == null)
                txtAmount.Focus();
            else if (txtSelectedTexBox.Name == txtAmount.Name)
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
                MessageBox.Show("Sorry, the amount you entered is not valid. Please check the amount you entered.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return boRetValue;
            }

            if (Convert.ToDecimal(txtAmount.Text) > 0)
            {
                mclsPaidOutDetails.Amount = Convert.ToDecimal(txtAmount.Text);
                mclsPaidOutDetails.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), cboType.Text, true);
                mclsPaidOutDetails.Remarks = txtRemarks.Text;
                mclsPaidOutDetails.DateCreated = DateTime.Now;
                mclsPaidOutDetails.TerminalNo = CompanyDetails.TerminalNo;
                mclsPaidOutDetails.BranchDetails = mclsTerminalDetails.BranchDetails;
                mclsPaidOutDetails.CashierID = mCashierID;

                Data.CashierReports clsCashierReport = new Data.CashierReports();
                if (!clsCashierReport.IsPaidOutAmountValid(mclsPaidOutDetails))
                {
                    MessageBox.Show("Sorry, the amount you entered is greater than the " + cboType.Text + " sales." +
                        "Please check the amount you entered.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    boRetValue = true;
                }
                clsCashierReport.CommitAndDispose();
            }
            else
            {
                MessageBox.Show("Sorry, amount must be greater than zero." +
                    "Please enter amount that is greater than zero.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return boRetValue;
        }


        #endregion


    }
}
