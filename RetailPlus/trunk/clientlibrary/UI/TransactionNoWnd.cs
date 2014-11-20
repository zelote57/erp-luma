using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
    public class TransactionNoWnd : System.Windows.Forms.Form
    {

        private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Label lblHeader;
        private DialogResult dialog;
        private System.Windows.Forms.PictureBox imgIcon;

        private GroupBox groupBox1;
        private Label lblTerminalNo;
        private TextBox txtTransactionNo;
        private TextBox txtSelectedTextBox;
        private AceSoft.KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private Button cmdCancel;
        private Label label1;
        private TextBox txtTerminalNo;

        #region public Properties

        private int miTransactionNoLength;
        public int TransactionNoLength
        {
            set { miTransactionNoLength = value; }
        }
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private string mstrTerminalNo;
        public string TerminalNo
        {
            get
            {
                return mstrTerminalNo;
            }
            set
            {
                mstrTerminalNo = value;
            }
        }


        private string mstrTransactionNo;
        public string TransactionNo
        {
            get
            {
                return mstrTransactionNo;
            }
            set
            {
                mstrTransactionNo = value;
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

        public TransactionNoWnd()
        {
            InitializeComponent();
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTerminalNo = new System.Windows.Forms.TextBox();
            this.lblTerminalNo = new System.Windows.Forms.Label();
            this.txtTransactionNo = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
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
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(124, 13);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "Enter TransactionNo.";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label1);
            
            this.groupBox1.Controls.Add(this.txtTerminalNo);
            this.groupBox1.Controls.Add(this.lblTerminalNo);
            this.groupBox1.Controls.Add(this.txtTransactionNo);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 533);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(200, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter Transaction No.";
            // 
            // txtTerminalNo
            // 
            this.txtTerminalNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerminalNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalNo.Location = new System.Drawing.Point(78, 80);
            this.txtTerminalNo.MaxLength = 16;
            this.txtTerminalNo.Name = "txtTerminalNo";
            this.txtTerminalNo.Size = new System.Drawing.Size(73, 30);
            this.txtTerminalNo.TabIndex = 2;
            this.txtTerminalNo.Text = "01";
            this.txtTerminalNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTerminalNo
            // 
            this.lblTerminalNo.AutoSize = true;
            this.lblTerminalNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalNo.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblTerminalNo.Location = new System.Drawing.Point(40, 58);
            this.lblTerminalNo.Name = "lblTerminalNo";
            this.lblTerminalNo.Size = new System.Drawing.Size(110, 13);
            this.lblTerminalNo.TabIndex = 3;
            this.lblTerminalNo.Text = "Enter Terminal No.";
            // 
            // txtTransactionNo
            // 
            this.txtTransactionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionNo.Location = new System.Drawing.Point(225, 80);
            this.txtTransactionNo.MaxLength = 16;
            this.txtTransactionNo.Name = "txtTransactionNo";
            this.txtTransactionNo.Size = new System.Drawing.Size(237, 30);
            this.txtTransactionNo.TabIndex = 0;
            this.txtTransactionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(877, 618);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 83);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // TransactionNoWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransactionNoWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TransactionNoWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransactionNoWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #endregion

        #region Windows Form Methods
        private void TransactionNoWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
                    TransactionStatus status = clsSalesTransactions.Status(txtTransactionNo.Text);
                    clsSalesTransactions.CommitAndDispose();

                    if (status == TransactionStatus.NotYetApplied)
                    {
                        MessageBox.Show("Sorry you have entered an invalid Transaction No." +
                            "Please type a valid Transaction No.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (status == TransactionStatus.Closed || status == TransactionStatus.Released || status == TransactionStatus.Refund || status == TransactionStatus.CreditPayment)
                    {
                        mstrTransactionNo = txtTransactionNo.Text.PadLeft(miTransactionNoLength - 1, '0');
                        mstrTerminalNo = txtTerminalNo.Text;
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sorry the Transaction No. you entered is " + status.ToString("G") + ". " +
                            "Please enter a valid Transaction No.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
            }
        }

        private void TransactionNoWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/TransactionNo.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }

            txtTerminalNo.Text = mstrTerminalNo;

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();

                // 
                // keyboardNoControl1
                // 
                this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
                this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Default;
                this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Default;
                this.keyboardNoControl1.Location = new System.Drawing.Point(561, 20);
                this.keyboardNoControl1.Name = "keyboardNoControl1";
                this.keyboardNoControl1.Size = new System.Drawing.Size(199, 202);
                this.keyboardNoControl1.TabIndex = 1;
                this.keyboardNoControl1.TabStop = false;
                this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);

                this.groupBox1.Controls.Add(this.keyboardNoControl1);

                keyboardNoControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
            }
        }

        #endregion

        #region Windows Control Methods

        private void txtTerminalNo_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
            }
        }

        private void txtTransactionNo_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedTextBox = (TextBox)sender;

            if (mclsTerminalDetails.WithRestaurantFeatures)
            {
                keyboardNoControl1.Visible = mclsTerminalDetails.WithRestaurantFeatures;
            }
        }

        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTextBox == null)
                txtTransactionNo.Focus();
            if (txtSelectedTextBox.Name == txtTerminalNo.Name)
                txtTerminalNo.Focus();
            else if (txtSelectedTextBox.Name == txtTransactionNo.Name)
                txtTransactionNo.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion
        
    }
}
