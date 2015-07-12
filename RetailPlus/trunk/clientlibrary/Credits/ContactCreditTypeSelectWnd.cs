using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ContactCreditTypeSelectWnd : System.Windows.Forms.Form
	{
		
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.PictureBox imgIcon;

		private DialogResult dialog;
        private GroupBox groupBox1;
        private Label lblCurrency;
        private Button cmdCancel;
        private ComboBox cboCardType;
        private GroupBox grpCardInfo;
        private Label lblMinimumPercentageDue;
        private Label lblPenaltyCharge;
        private Label lblFinanceCharge;
        private Label lblCardTypeCode;
        private Label label1;
        private Label WithGuarantor;
        private Label label2;
        private Label LatePenaltyCharge;
        private Label FinanceCharge;
        private Label CardTypeCode;
        private Label lblMinimumAmountDue;
        private CheckBox chkWithGuarantor;
        private Label label4;
        private Label lblMinimumPercentageDue15th;
        private Label lblMinimumAmountDue15th;
        private Label lblPenaltyCharge15th;
        private Label lblFinanceCharge15th;
        private Label labelMinimumAmountDue15th;
        private Label labelMinimumPercentageDue15th;
        private Label labelPenaltyCharge15th;
        private Label labelFinanceCharge15th;

		public DialogResult Result
		{
			get {	return dialog;	}
		}

        public Data.CardTypeDetails CardTypeDetails { get; set; }
        //private Data.ContactDetails mclsGuarantor;

        public Data.TerminalDetails TerminalDetails { get; set; }

		#region Constructors And Desctructors

		public ContactCreditTypeSelectWnd()
		{
			InitializeComponent();

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

        #endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactCreditTypeSelectWnd));
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpCardInfo = new System.Windows.Forms.GroupBox();
            this.lblMinimumPercentageDue15th = new System.Windows.Forms.Label();
            this.lblMinimumAmountDue15th = new System.Windows.Forms.Label();
            this.lblPenaltyCharge15th = new System.Windows.Forms.Label();
            this.lblFinanceCharge15th = new System.Windows.Forms.Label();
            this.labelMinimumAmountDue15th = new System.Windows.Forms.Label();
            this.labelMinimumPercentageDue15th = new System.Windows.Forms.Label();
            this.labelPenaltyCharge15th = new System.Windows.Forms.Label();
            this.labelFinanceCharge15th = new System.Windows.Forms.Label();
            this.chkWithGuarantor = new System.Windows.Forms.CheckBox();
            this.lblMinimumAmountDue = new System.Windows.Forms.Label();
            this.lblMinimumPercentageDue = new System.Windows.Forms.Label();
            this.lblPenaltyCharge = new System.Windows.Forms.Label();
            this.lblFinanceCharge = new System.Windows.Forms.Label();
            this.lblCardTypeCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WithGuarantor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LatePenaltyCharge = new System.Windows.Forms.Label();
            this.FinanceCharge = new System.Windows.Forms.Label();
            this.CardTypeCode = new System.Windows.Forms.Label();
            this.cboCardType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpCardInfo.SuspendLayout();
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
            this.lblHeader.Size = new System.Drawing.Size(110, 13);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Select Credit Type";
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.grpCardInfo);
            this.groupBox1.Controls.Add(this.cboCardType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblCurrency);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 299);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grpCardInfo
            // 
            this.grpCardInfo.Controls.Add(this.lblMinimumPercentageDue15th);
            this.grpCardInfo.Controls.Add(this.lblMinimumAmountDue15th);
            this.grpCardInfo.Controls.Add(this.lblPenaltyCharge15th);
            this.grpCardInfo.Controls.Add(this.lblFinanceCharge15th);
            this.grpCardInfo.Controls.Add(this.labelMinimumAmountDue15th);
            this.grpCardInfo.Controls.Add(this.labelMinimumPercentageDue15th);
            this.grpCardInfo.Controls.Add(this.labelPenaltyCharge15th);
            this.grpCardInfo.Controls.Add(this.labelFinanceCharge15th);
            this.grpCardInfo.Controls.Add(this.chkWithGuarantor);
            this.grpCardInfo.Controls.Add(this.lblMinimumAmountDue);
            this.grpCardInfo.Controls.Add(this.lblMinimumPercentageDue);
            this.grpCardInfo.Controls.Add(this.lblPenaltyCharge);
            this.grpCardInfo.Controls.Add(this.lblFinanceCharge);
            this.grpCardInfo.Controls.Add(this.lblCardTypeCode);
            this.grpCardInfo.Controls.Add(this.label1);
            this.grpCardInfo.Controls.Add(this.WithGuarantor);
            this.grpCardInfo.Controls.Add(this.label2);
            this.grpCardInfo.Controls.Add(this.LatePenaltyCharge);
            this.grpCardInfo.Controls.Add(this.FinanceCharge);
            this.grpCardInfo.Controls.Add(this.CardTypeCode);
            this.grpCardInfo.Location = new System.Drawing.Point(59, 101);
            this.grpCardInfo.Name = "grpCardInfo";
            this.grpCardInfo.Size = new System.Drawing.Size(828, 175);
            this.grpCardInfo.TabIndex = 10;
            this.grpCardInfo.TabStop = false;
            this.grpCardInfo.Text = "HP Super Card information";
            this.grpCardInfo.Visible = false;
            // 
            // lblMinimumPercentageDue15th
            // 
            this.lblMinimumPercentageDue15th.AutoSize = true;
            this.lblMinimumPercentageDue15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumPercentageDue15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMinimumPercentageDue15th.Location = new System.Drawing.Point(566, 138);
            this.lblMinimumPercentageDue15th.Name = "lblMinimumPercentageDue15th";
            this.lblMinimumPercentageDue15th.Size = new System.Drawing.Size(29, 13);
            this.lblMinimumPercentageDue15th.TabIndex = 21;
            this.lblMinimumPercentageDue15th.Text = "0.00";
            // 
            // lblMinimumAmountDue15th
            // 
            this.lblMinimumAmountDue15th.AutoSize = true;
            this.lblMinimumAmountDue15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumAmountDue15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMinimumAmountDue15th.Location = new System.Drawing.Point(728, 138);
            this.lblMinimumAmountDue15th.Name = "lblMinimumAmountDue15th";
            this.lblMinimumAmountDue15th.Size = new System.Drawing.Size(29, 13);
            this.lblMinimumAmountDue15th.TabIndex = 20;
            this.lblMinimumAmountDue15th.Text = "0.00";
            // 
            // lblPenaltyCharge15th
            // 
            this.lblPenaltyCharge15th.AutoSize = true;
            this.lblPenaltyCharge15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPenaltyCharge15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblPenaltyCharge15th.Location = new System.Drawing.Point(359, 138);
            this.lblPenaltyCharge15th.Name = "lblPenaltyCharge15th";
            this.lblPenaltyCharge15th.Size = new System.Drawing.Size(29, 13);
            this.lblPenaltyCharge15th.TabIndex = 19;
            this.lblPenaltyCharge15th.Text = "0.00";
            // 
            // lblFinanceCharge15th
            // 
            this.lblFinanceCharge15th.AutoSize = true;
            this.lblFinanceCharge15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinanceCharge15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblFinanceCharge15th.Location = new System.Drawing.Point(147, 138);
            this.lblFinanceCharge15th.Name = "lblFinanceCharge15th";
            this.lblFinanceCharge15th.Size = new System.Drawing.Size(29, 13);
            this.lblFinanceCharge15th.TabIndex = 18;
            this.lblFinanceCharge15th.Text = "0.00";
            // 
            // labelMinimumAmountDue15th
            // 
            this.labelMinimumAmountDue15th.AutoSize = true;
            this.labelMinimumAmountDue15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimumAmountDue15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelMinimumAmountDue15th.Location = new System.Drawing.Point(642, 125);
            this.labelMinimumAmountDue15th.Name = "labelMinimumAmountDue15th";
            this.labelMinimumAmountDue15th.Size = new System.Drawing.Size(88, 26);
            this.labelMinimumAmountDue15th.TabIndex = 17;
            this.labelMinimumAmountDue15th.Text = "15th Minimum\r\nAmount Due:";
            // 
            // labelMinimumPercentageDue15th
            // 
            this.labelMinimumPercentageDue15th.AutoSize = true;
            this.labelMinimumPercentageDue15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimumPercentageDue15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelMinimumPercentageDue15th.Location = new System.Drawing.Point(434, 125);
            this.labelMinimumPercentageDue15th.Name = "labelMinimumPercentageDue15th";
            this.labelMinimumPercentageDue15th.Size = new System.Drawing.Size(126, 26);
            this.labelMinimumPercentageDue15th.TabIndex = 16;
            this.labelMinimumPercentageDue15th.Text = "15th Minimum \r\nPercentage Due (%):";
            // 
            // labelPenaltyCharge15th
            // 
            this.labelPenaltyCharge15th.AutoSize = true;
            this.labelPenaltyCharge15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPenaltyCharge15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelPenaltyCharge15th.Location = new System.Drawing.Point(228, 125);
            this.labelPenaltyCharge15th.Name = "labelPenaltyCharge15th";
            this.labelPenaltyCharge15th.Size = new System.Drawing.Size(125, 26);
            this.labelPenaltyCharge15th.TabIndex = 15;
            this.labelPenaltyCharge15th.Text = "15th Late\r\nPenalty Charge  (%):";
            // 
            // labelFinanceCharge15th
            // 
            this.labelFinanceCharge15th.AutoSize = true;
            this.labelFinanceCharge15th.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFinanceCharge15th.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelFinanceCharge15th.Location = new System.Drawing.Point(48, 125);
            this.labelFinanceCharge15th.Name = "labelFinanceCharge15th";
            this.labelFinanceCharge15th.Size = new System.Drawing.Size(79, 26);
            this.labelFinanceCharge15th.TabIndex = 14;
            this.labelFinanceCharge15th.Text = "15th Finance\r\nCharge (%) :";
            // 
            // chkWithGuarantor
            // 
            this.chkWithGuarantor.AutoSize = true;
            this.chkWithGuarantor.Location = new System.Drawing.Point(742, 40);
            this.chkWithGuarantor.Name = "chkWithGuarantor";
            this.chkWithGuarantor.Size = new System.Drawing.Size(15, 14);
            this.chkWithGuarantor.TabIndex = 13;
            this.chkWithGuarantor.UseVisualStyleBackColor = true;
            // 
            // lblMinimumAmountDue
            // 
            this.lblMinimumAmountDue.AutoSize = true;
            this.lblMinimumAmountDue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumAmountDue.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMinimumAmountDue.Location = new System.Drawing.Point(728, 90);
            this.lblMinimumAmountDue.Name = "lblMinimumAmountDue";
            this.lblMinimumAmountDue.Size = new System.Drawing.Size(29, 13);
            this.lblMinimumAmountDue.TabIndex = 12;
            this.lblMinimumAmountDue.Text = "0.00";
            // 
            // lblMinimumPercentageDue
            // 
            this.lblMinimumPercentageDue.AutoSize = true;
            this.lblMinimumPercentageDue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumPercentageDue.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblMinimumPercentageDue.Location = new System.Drawing.Point(566, 90);
            this.lblMinimumPercentageDue.Name = "lblMinimumPercentageDue";
            this.lblMinimumPercentageDue.Size = new System.Drawing.Size(29, 13);
            this.lblMinimumPercentageDue.TabIndex = 11;
            this.lblMinimumPercentageDue.Text = "0.00";
            // 
            // lblPenaltyCharge
            // 
            this.lblPenaltyCharge.AutoSize = true;
            this.lblPenaltyCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPenaltyCharge.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblPenaltyCharge.Location = new System.Drawing.Point(359, 90);
            this.lblPenaltyCharge.Name = "lblPenaltyCharge";
            this.lblPenaltyCharge.Size = new System.Drawing.Size(29, 13);
            this.lblPenaltyCharge.TabIndex = 10;
            this.lblPenaltyCharge.Text = "0.00";
            // 
            // lblFinanceCharge
            // 
            this.lblFinanceCharge.AutoSize = true;
            this.lblFinanceCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinanceCharge.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblFinanceCharge.Location = new System.Drawing.Point(147, 90);
            this.lblFinanceCharge.Name = "lblFinanceCharge";
            this.lblFinanceCharge.Size = new System.Drawing.Size(29, 13);
            this.lblFinanceCharge.TabIndex = 9;
            this.lblFinanceCharge.Text = "0.00";
            // 
            // lblCardTypeCode
            // 
            this.lblCardTypeCode.AutoSize = true;
            this.lblCardTypeCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardTypeCode.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCardTypeCode.Location = new System.Drawing.Point(147, 39);
            this.lblCardTypeCode.Name = "lblCardTypeCode";
            this.lblCardTypeCode.Size = new System.Drawing.Size(92, 13);
            this.lblCardTypeCode.TabIndex = 8;
            this.lblCardTypeCode.Text = "Card Type Code: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(642, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 7;
            this.label1.Text = "Minimum\r\nAmount Due:";
            // 
            // WithGuarantor
            // 
            this.WithGuarantor.AutoSize = true;
            this.WithGuarantor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WithGuarantor.ForeColor = System.Drawing.Color.MediumBlue;
            this.WithGuarantor.Location = new System.Drawing.Point(642, 39);
            this.WithGuarantor.Name = "WithGuarantor";
            this.WithGuarantor.Size = new System.Drawing.Size(100, 13);
            this.WithGuarantor.TabIndex = 6;
            this.WithGuarantor.Text = "With Guarantor :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(434, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Minimum \r\nPercentage Due (%):";
            // 
            // LatePenaltyCharge
            // 
            this.LatePenaltyCharge.AutoSize = true;
            this.LatePenaltyCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatePenaltyCharge.ForeColor = System.Drawing.Color.MediumBlue;
            this.LatePenaltyCharge.Location = new System.Drawing.Point(228, 77);
            this.LatePenaltyCharge.Name = "LatePenaltyCharge";
            this.LatePenaltyCharge.Size = new System.Drawing.Size(125, 26);
            this.LatePenaltyCharge.TabIndex = 4;
            this.LatePenaltyCharge.Text = "Late\r\nPenalty Charge  (%):";
            // 
            // FinanceCharge
            // 
            this.FinanceCharge.AutoSize = true;
            this.FinanceCharge.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinanceCharge.ForeColor = System.Drawing.Color.MediumBlue;
            this.FinanceCharge.Location = new System.Drawing.Point(48, 77);
            this.FinanceCharge.Name = "FinanceCharge";
            this.FinanceCharge.Size = new System.Drawing.Size(91, 26);
            this.FinanceCharge.TabIndex = 3;
            this.FinanceCharge.Text = "Finance\r\nCharge (%)     :";
            // 
            // CardTypeCode
            // 
            this.CardTypeCode.AutoSize = true;
            this.CardTypeCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardTypeCode.ForeColor = System.Drawing.Color.MediumBlue;
            this.CardTypeCode.Location = new System.Drawing.Point(48, 39);
            this.CardTypeCode.Name = "CardTypeCode";
            this.CardTypeCode.Size = new System.Drawing.Size(101, 13);
            this.CardTypeCode.TabIndex = 2;
            this.CardTypeCode.Text = "Card Type Code: ";
            // 
            // cboCardType
            // 
            this.cboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCardType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCardType.Location = new System.Drawing.Point(276, 47);
            this.cboCardType.Name = "cboCardType";
            this.cboCardType.Size = new System.Drawing.Size(311, 31);
            this.cboCardType.TabIndex = 9;
            this.cboCardType.SelectedIndexChanged += new System.EventHandler(this.cboCardType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(362, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Credit Card";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCurrency.Location = new System.Drawing.Point(277, 27);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(207, 13);
            this.lblCurrency.TabIndex = 1;
            this.lblCurrency.Text = "Select type of  Credit Card  to issue.";
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
            this.cmdCancel.TabStop = false;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // ContactCreditTypeSelectWnd
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContactCreditTypeSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactCreditTypeSelectWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactCreditTypeSelectWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpCardInfo.ResumeLayout(false);
            this.grpCardInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		

		#region Windows Form Methods

		private void ContactCreditTypeSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.F1:
                case Keys.Enter:
                    //mCreditType = CreditType.Individual;
                    dialog = DialogResult.OK;
                    this.Hide();
					break;

                case Keys.F2:
                    //mCreditType = CreditType.Group;
                    dialog = DialogResult.OK;
                    this.Hide();
					break;
			}
		}
		private void ContactCreditTypeSelectWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CreditCardType.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }

            LoadOptions();
		}

		#endregion

		#region Windows Control Methods

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

		#endregion

        private void cmdCreditCardTypeIndividual_Click(object sender, EventArgs e)
        {
            //mCreditType= CreditType.Individual;
        }

        private void cmdCreditCardTypeGroup_Click(object sender, EventArgs e)
        {
            //mCreditType= CreditType.Group;
        }

		#region Private Methods

        private void LoadOptions()
        {
            cboCardType.Items.Clear();
            Data.CardType clsCardType = new Data.CardType();
            foreach (System.Data.DataRow dr in clsCardType.ListAsDataTable(new Data.CardTypeDetails(CreditCardTypes.Internal), "CardTypeName", SortOption.Ascending).Rows)
            {
                cboCardType.Items.Add(dr["CardTypeName"]);
            }
            clsCardType.CommitAndDispose();

            if (cboCardType.Items.Count > 0)
                cboCardType.SelectedIndex = 0;
            else
                cboCardType.Items.Add("No available cards to issue");

            cboCardType_SelectedIndexChanged(null, null);
        }

		#endregion

        private void cboCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCardType.Text == "No available cards to issue")
                grpCardInfo.Visible = false;
            else
            {
                Data.CardType clsCardType = new Data.CardType();
                Data.CardTypeDetails clsCardTypeDetails = clsCardType.Details(cboCardType.Text);
                clsCardType.CommitAndDispose();

                if (clsCardTypeDetails.CardTypeID == 0)
                {
                    grpCardInfo.Visible = false;
                    CardTypeDetails = new Data.CardTypeDetails();
                }
                else
                {
                    grpCardInfo.Visible = true;
                    grpCardInfo.Text = cboCardType.Text + " Information";
                    lblCardTypeCode.Text = clsCardTypeDetails.CardTypeCode;
                    lblFinanceCharge.Text = clsCardTypeDetails.CreditFinanceCharge.ToString("##0.#0");
                    lblPenaltyCharge.Text = clsCardTypeDetails.CreditLatePenaltyCharge.ToString("##0.#0");
                    lblMinimumPercentageDue.Text = clsCardTypeDetails.CreditMinimumPercentageDue.ToString("##0.#0");
                    lblMinimumAmountDue.Text = clsCardTypeDetails.CreditMinimumAmountDue.ToString("##0.#0");

                    lblFinanceCharge15th.Text = clsCardTypeDetails.CreditFinanceCharge15th.ToString("##0.#0");
                    lblPenaltyCharge15th.Text = clsCardTypeDetails.CreditLatePenaltyCharge15th.ToString("##0.#0");
                    lblMinimumPercentageDue15th.Text = clsCardTypeDetails.CreditMinimumPercentageDue15th.ToString("##0.#0");
                    lblMinimumAmountDue15th.Text = clsCardTypeDetails.CreditMinimumAmountDue15th.ToString("##0.#0");

                    chkWithGuarantor.Checked = clsCardTypeDetails.WithGuarantor;

                    CardTypeDetails = clsCardTypeDetails;

                    if (!clsCardTypeDetails.WithGuarantor)
                    {
                        groupBox1.Height = 248;
                        grpCardInfo.Height = 125; //this height should hide the 15th charges

                        //mclsGuarantor = new Data.ContactDetails();
                    }
                    else
                    {
                        groupBox1.Height = 300;
                        grpCardInfo.Height = 180;
                        
                    }
                }
            }
        }
    }
}
