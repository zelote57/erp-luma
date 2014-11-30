using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    /// <summary>
    /// Summary description for ReportsWnd.
    /// </summary>
    public class ReportsWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private Label lblReprintZReadName;
        private Label lblPrintPLUReportPerOrderSlipPrinterName;

        private long mlCashierID;
        private Keys mKeyData;
        private Button cmdF4;
        private Button cmdF3;
        private Button cmdF2;
        private Button cmdF1;
        private Button cmdF10;
        private Button cmdF9;
        private Button cmdF8;
        private Button cmdF7;
        private Button cmdF6;
        private Button cmdF5;
        private Button cmdCancel;
        private Button cmdF11;
        private Label label5;
        private Button cmdF12;
        private Label label6;
        private Label lblPress;
        private Label lblF2;
        private Label lblAddNewCustomer;
        private DialogResult dialog;

        #region Public Get/Set Properties

        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }
        public Keys KeyData
        {
            get
            {
                return mKeyData;
            }
        }
        public long CashierID
        {
            set { mlCashierID = value; }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        #endregion

        #region Constructors and Destructors

        public ReportsWnd()
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdF12 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdF11 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdF10 = new System.Windows.Forms.Button();
            this.cmdF9 = new System.Windows.Forms.Button();
            this.cmdF8 = new System.Windows.Forms.Button();
            this.cmdF7 = new System.Windows.Forms.Button();
            this.cmdF6 = new System.Windows.Forms.Button();
            this.lblPrintPLUReportPerOrderSlipPrinterName = new System.Windows.Forms.Label();
            this.lblReprintZReadName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdF5 = new System.Windows.Forms.Button();
            this.cmdF4 = new System.Windows.Forms.Button();
            this.cmdF3 = new System.Windows.Forms.Button();
            this.cmdF2 = new System.Windows.Forms.Button();
            this.cmdF1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblF2 = new System.Windows.Forms.Label();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cmdF12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmdF11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmdF10);
            this.groupBox1.Controls.Add(this.cmdF9);
            this.groupBox1.Controls.Add(this.cmdF8);
            this.groupBox1.Controls.Add(this.cmdF7);
            this.groupBox1.Controls.Add(this.cmdF6);
            this.groupBox1.Controls.Add(this.lblPrintPLUReportPerOrderSlipPrinterName);
            this.groupBox1.Controls.Add(this.lblReprintZReadName);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmdF5);
            this.groupBox1.Controls.Add(this.cmdF4);
            this.groupBox1.Controls.Add(this.cmdF3);
            this.groupBox1.Controls.Add(this.cmdF2);
            this.groupBox1.Controls.Add(this.cmdF1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1007, 545);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Report Type To Print";
            // 
            // cmdF12
            // 
            this.cmdF12.AutoSize = true;
            this.cmdF12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF12.ForeColor = System.Drawing.Color.White;
            this.cmdF12.Location = new System.Drawing.Point(98, 339);
            this.cmdF12.Name = "cmdF12";
            this.cmdF12.Size = new System.Drawing.Size(78, 62);
            this.cmdF12.TabIndex = 90;
            this.cmdF12.Text = "F12";
            this.cmdF12.UseVisualStyleBackColor = true;
            this.cmdF12.Click += new System.EventHandler(this.cmdF12_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(218, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "Print PLU Inventory By Group";
            // 
            // cmdF11
            // 
            this.cmdF11.AutoSize = true;
            this.cmdF11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF11.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF11.ForeColor = System.Drawing.Color.White;
            this.cmdF11.Location = new System.Drawing.Point(553, 339);
            this.cmdF11.Name = "cmdF11";
            this.cmdF11.Size = new System.Drawing.Size(78, 62);
            this.cmdF11.TabIndex = 88;
            this.cmdF11.Text = "F11";
            this.cmdF11.UseVisualStyleBackColor = true;
            this.cmdF11.Click += new System.EventHandler(this.cmdF11_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(697, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "Re-Print Delivery Receipt";
            // 
            // cmdF10
            // 
            this.cmdF10.AutoSize = true;
            this.cmdF10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF10.ForeColor = System.Drawing.Color.White;
            this.cmdF10.Location = new System.Drawing.Point(553, 271);
            this.cmdF10.Name = "cmdF10";
            this.cmdF10.Size = new System.Drawing.Size(78, 62);
            this.cmdF10.TabIndex = 10;
            this.cmdF10.Text = "F10";
            this.cmdF10.UseVisualStyleBackColor = true;
            this.cmdF10.Click += new System.EventHandler(this.cmdF10_Click);
            // 
            // cmdF9
            // 
            this.cmdF9.AutoSize = true;
            this.cmdF9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF9.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF9.ForeColor = System.Drawing.Color.White;
            this.cmdF9.Location = new System.Drawing.Point(553, 208);
            this.cmdF9.Name = "cmdF9";
            this.cmdF9.Size = new System.Drawing.Size(78, 62);
            this.cmdF9.TabIndex = 9;
            this.cmdF9.Text = "F9";
            this.cmdF9.UseVisualStyleBackColor = true;
            this.cmdF9.Visible = false;
            this.cmdF9.Click += new System.EventHandler(this.cmdF9_Click);
            // 
            // cmdF8
            // 
            this.cmdF8.AutoSize = true;
            this.cmdF8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF8.ForeColor = System.Drawing.Color.White;
            this.cmdF8.Location = new System.Drawing.Point(553, 148);
            this.cmdF8.Name = "cmdF8";
            this.cmdF8.Size = new System.Drawing.Size(78, 62);
            this.cmdF8.TabIndex = 8;
            this.cmdF8.Text = "F8";
            this.cmdF8.UseVisualStyleBackColor = true;
            this.cmdF8.Click += new System.EventHandler(this.cmdF8_Click);
            // 
            // cmdF7
            // 
            this.cmdF7.AutoSize = true;
            this.cmdF7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF7.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF7.ForeColor = System.Drawing.Color.White;
            this.cmdF7.Location = new System.Drawing.Point(553, 88);
            this.cmdF7.Name = "cmdF7";
            this.cmdF7.Size = new System.Drawing.Size(78, 62);
            this.cmdF7.TabIndex = 7;
            this.cmdF7.Text = "F7";
            this.cmdF7.UseVisualStyleBackColor = true;
            this.cmdF7.Click += new System.EventHandler(this.cmdF7_Click);
            // 
            // cmdF6
            // 
            this.cmdF6.AutoSize = true;
            this.cmdF6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF6.ForeColor = System.Drawing.Color.White;
            this.cmdF6.Location = new System.Drawing.Point(553, 28);
            this.cmdF6.Name = "cmdF6";
            this.cmdF6.Size = new System.Drawing.Size(78, 62);
            this.cmdF6.TabIndex = 6;
            this.cmdF6.Text = "F6";
            this.cmdF6.UseVisualStyleBackColor = true;
            this.cmdF6.Click += new System.EventHandler(this.cmdF6_Click);
            // 
            // lblPrintPLUReportPerOrderSlipPrinterName
            // 
            this.lblPrintPLUReportPerOrderSlipPrinterName.AutoSize = true;
            this.lblPrintPLUReportPerOrderSlipPrinterName.ForeColor = System.Drawing.Color.Blue;
            this.lblPrintPLUReportPerOrderSlipPrinterName.Location = new System.Drawing.Point(697, 297);
            this.lblPrintPLUReportPerOrderSlipPrinterName.Name = "lblPrintPLUReportPerOrderSlipPrinterName";
            this.lblPrintPLUReportPerOrderSlipPrinterName.Size = new System.Drawing.Size(187, 13);
            this.lblPrintPLUReportPerOrderSlipPrinterName.TabIndex = 87;
            this.lblPrintPLUReportPerOrderSlipPrinterName.Text = "Print PLU Report per OrderSlip Printer";
            // 
            // lblReprintZReadName
            // 
            this.lblReprintZReadName.AutoSize = true;
            this.lblReprintZReadName.ForeColor = System.Drawing.Color.Blue;
            this.lblReprintZReadName.Location = new System.Drawing.Point(697, 234);
            this.lblReprintZReadName.Name = "lblReprintZReadName";
            this.lblReprintZReadName.Size = new System.Drawing.Size(96, 13);
            this.lblReprintZReadName.TabIndex = 85;
            this.lblReprintZReadName.Text = "Print z-read report";
            this.lblReprintZReadName.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(697, 174);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(116, 13);
            this.label18.TabIndex = 83;
            this.label18.Text = "Print Electronic Journal";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(697, 114);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 13);
            this.label16.TabIndex = 81;
            this.label16.Text = "Print PLU Report";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(218, 297);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 13);
            this.label12.TabIndex = 77;
            this.label12.Text = "Print Cashier\'s hourly report.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(697, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 13);
            this.label13.TabIndex = 76;
            this.label13.Text = "Print Dept. Sales Report";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(218, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(138, 13);
            this.label14.TabIndex = 75;
            this.label14.Text = "Print current XRead report.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Print current terminal report.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(218, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Print Cashier\'s report";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Reprint receipt.";
            // 
            // cmdF5
            // 
            this.cmdF5.AutoSize = true;
            this.cmdF5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF5.ForeColor = System.Drawing.Color.White;
            this.cmdF5.Location = new System.Drawing.Point(98, 271);
            this.cmdF5.Name = "cmdF5";
            this.cmdF5.Size = new System.Drawing.Size(78, 62);
            this.cmdF5.TabIndex = 5;
            this.cmdF5.Text = "F5";
            this.cmdF5.UseVisualStyleBackColor = true;
            this.cmdF5.Click += new System.EventHandler(this.cmdF5_Click);
            // 
            // cmdF4
            // 
            this.cmdF4.AutoSize = true;
            this.cmdF4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF4.ForeColor = System.Drawing.Color.White;
            this.cmdF4.Location = new System.Drawing.Point(98, 208);
            this.cmdF4.Name = "cmdF4";
            this.cmdF4.Size = new System.Drawing.Size(78, 62);
            this.cmdF4.TabIndex = 4;
            this.cmdF4.Text = "F4";
            this.cmdF4.UseVisualStyleBackColor = true;
            this.cmdF4.Click += new System.EventHandler(this.cmdF4_Click);
            // 
            // cmdF3
            // 
            this.cmdF3.AutoSize = true;
            this.cmdF3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF3.ForeColor = System.Drawing.Color.White;
            this.cmdF3.Location = new System.Drawing.Point(98, 148);
            this.cmdF3.Name = "cmdF3";
            this.cmdF3.Size = new System.Drawing.Size(78, 62);
            this.cmdF3.TabIndex = 3;
            this.cmdF3.Text = "F3";
            this.cmdF3.UseVisualStyleBackColor = true;
            this.cmdF3.Click += new System.EventHandler(this.cmdF3_Click);
            // 
            // cmdF2
            // 
            this.cmdF2.AutoSize = true;
            this.cmdF2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF2.ForeColor = System.Drawing.Color.White;
            this.cmdF2.Location = new System.Drawing.Point(98, 88);
            this.cmdF2.Name = "cmdF2";
            this.cmdF2.Size = new System.Drawing.Size(78, 62);
            this.cmdF2.TabIndex = 2;
            this.cmdF2.Text = "F2";
            this.cmdF2.UseVisualStyleBackColor = true;
            this.cmdF2.Click += new System.EventHandler(this.cmdF2_Click);
            // 
            // cmdF1
            // 
            this.cmdF1.AutoSize = true;
            this.cmdF1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdF1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdF1.ForeColor = System.Drawing.Color.White;
            this.cmdF1.Location = new System.Drawing.Point(98, 28);
            this.cmdF1.Name = "cmdF1";
            this.cmdF1.Size = new System.Drawing.Size(78, 62);
            this.cmdF1.TabIndex = 1;
            this.cmdF1.Text = "F1";
            this.cmdF1.UseVisualStyleBackColor = true;
            this.cmdF1.Click += new System.EventHandler(this.cmdF1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Reports Window";
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
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.BackColor = System.Drawing.Color.Transparent;
            this.lblPress.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblPress.Location = new System.Drawing.Point(764, 41);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(33, 13);
            this.lblPress.TabIndex = 127;
            this.lblPress.Text = "Press";
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.BackColor = System.Drawing.Color.Transparent;
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.Location = new System.Drawing.Point(798, 41);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(33, 13);
            this.lblF2.TabIndex = 126;
            this.lblF2.Text = "[Key]";
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(838, 41);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(139, 13);
            this.lblAddNewCustomer.TabIndex = 125;
            this.lblAddNewCustomer.Text = " to print the desired report.";
            // 
            // ReportsWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1026, 764);
            this.ControlBox = false;
            this.Controls.Add(this.lblPress);
            this.Controls.Add(this.lblF2);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "ReportsWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ReportsWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportsWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region Windows Form Methods

        private void ReportsWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Reports.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            {
                this.cmdF1.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF2.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF3.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF4.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF5.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF6.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF7.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF8.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF9.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF10.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF11.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF12.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
            }
            catch { }

            LoadOptions();
        }
        private void ReportsWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    if (cmdF1.Visible) AssignValues(Keys.F1);
                    break;

                case Keys.F2:
                    if (cmdF2.Visible) AssignValues(Keys.F2);
                    break;

                case Keys.F3:
                    if (cmdF3.Visible) AssignValues(Keys.F3);
                    break;

                case Keys.F4:
                    if (cmdF4.Visible) AssignValues(Keys.F4);
                    break;

                case Keys.F5:
                    if (cmdF5.Visible) AssignValues(Keys.F5);
                    break;

                case Keys.F6:
                    if (cmdF6.Visible) AssignValues(Keys.F6);
                    break;

                case Keys.F7:
                    if (cmdF7.Visible) AssignValues(Keys.F7);
                    break;

                case Keys.F8:
                    if (cmdF8.Visible) AssignValues(Keys.F8);
                    break;

                case Keys.F9:
                    if (cmdF9.Visible) AssignValues(Keys.F9);
                    break;

                case Keys.F10:
                    if (cmdF10.Visible) AssignValues(Keys.F10);
                    break;

                case Keys.F11:
                    if (cmdF11.Visible) AssignValues(Keys.F11);
                    break;

                case Keys.F12:
                    if (cmdF12.Visible) AssignValues(Keys.F12);
                    break;

                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

            }
        }

        #endregion

        private void LoadOptions()
        {
            //change to ctrL+ALT+F9
            Security.AccessRights clsAccessRights = new Security.AccessRights();
            Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

            clsDetails = clsAccessRights.Details(mlCashierID, (Int16)AccessTypes.PrintZRead);
            cmdF9.Visible = clsDetails.Read;
            lblReprintZReadName.Visible = clsDetails.Read;

            clsDetails = clsAccessRights.Details(mlCashierID, (Int16)AccessTypes.PLUReportPerOrderSlipPrinter);
            cmdF10.Visible = clsDetails.Read;
            lblPrintPLUReportPerOrderSlipPrinterName.Visible = clsDetails.Read;

            clsAccessRights.CommitAndDispose();
        }
        private void AssignValues(Keys key)
        {
            dialog = DialogResult.OK;
            mKeyData = key;
            this.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdF1_Click(object sender, EventArgs e)
        {
            if (cmdF1.Visible) AssignValues(Keys.F1);
        }

        private void cmdF2_Click(object sender, EventArgs e)
        {
            if (cmdF2.Visible) AssignValues(Keys.F2);
        }

        private void cmdF3_Click(object sender, EventArgs e)
        {
            if (cmdF3.Visible) AssignValues(Keys.F3);
        }

        private void cmdF4_Click(object sender, EventArgs e)
        {
            if (cmdF4.Visible) AssignValues(Keys.F4);
        }

        private void cmdF5_Click(object sender, EventArgs e)
        {
            if (cmdF5.Visible) AssignValues(Keys.F5);
        }

        private void cmdF6_Click(object sender, EventArgs e)
        {
            if (cmdF6.Visible) AssignValues(Keys.F6);
        }

        private void cmdF7_Click(object sender, EventArgs e)
        {
            if (cmdF7.Visible) AssignValues(Keys.F7);
        }

        private void cmdF8_Click(object sender, EventArgs e)
        {
            if (cmdF8.Visible) AssignValues(Keys.F8);
        }

        private void cmdF9_Click(object sender, EventArgs e)
        {
            if (cmdF9.Visible) AssignValues(Keys.F9);
        }

        private void cmdF10_Click(object sender, EventArgs e)
        {
            if (cmdF10.Visible) AssignValues(Keys.F10);
        }

        private void cmdF11_Click(object sender, EventArgs e)
        {
            if (cmdF11.Visible) AssignValues(Keys.F11);
        }

        private void cmdF12_Click(object sender, EventArgs e)
        {
            if (cmdF12.Visible) AssignValues(Keys.F12);
        }


    }
}


