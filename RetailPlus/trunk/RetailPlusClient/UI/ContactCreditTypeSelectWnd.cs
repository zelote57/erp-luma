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
        private Label label1;
        private Button cmdCreditCardTypeGroup;
        private Button cmdCreditCardTypeIndividual;
        private Label label4;
        private Label label5;
        private Label label2;
        private Label label3;
        private Label label7;
        private Label label6;
		private CreditType mCreditType;

		public DialogResult Result
		{
			get {	return dialog;	}
		}

		public CreditType CreditType
		{
            get {	return mCreditType;	}
			set {	mCreditType = value;	}
		}

		#region Constructors And Desctructors
		public ContactCreditTypeSelectWnd()
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCreditCardTypeGroup = new System.Windows.Forms.Button();
            this.cmdCreditCardTypeIndividual = new System.Windows.Forms.Button();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdCreditCardTypeGroup);
            this.groupBox1.Controls.Add(this.cmdCreditCardTypeIndividual);
            this.groupBox1.Controls.Add(this.lblCurrency);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(511, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Enter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MediumBlue;
            this.label6.Location = new System.Drawing.Point(488, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "or [ Enter ]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(459, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "F2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MediumBlue;
            this.label5.Location = new System.Drawing.Point(451, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "[ F1 ]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(459, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "F1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(292, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Group Credit Card Type";
            // 
            // cmdCreditCardTypeGroup
            // 
            this.cmdCreditCardTypeGroup.AutoSize = true;
            this.cmdCreditCardTypeGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdCreditCardTypeGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCreditCardTypeGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCreditCardTypeGroup.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreditCardTypeGroup.ForeColor = System.Drawing.Color.White;
            this.cmdCreditCardTypeGroup.Location = new System.Drawing.Point(200, 107);
            this.cmdCreditCardTypeGroup.Name = "cmdCreditCardTypeGroup";
            this.cmdCreditCardTypeGroup.Size = new System.Drawing.Size(52, 52);
            this.cmdCreditCardTypeGroup.TabIndex = 3;
            this.cmdCreditCardTypeGroup.TabStop = false;
            this.cmdCreditCardTypeGroup.UseVisualStyleBackColor = true;
            this.cmdCreditCardTypeGroup.Click += new System.EventHandler(this.cmdCreditCardTypeGroup_Click);
            // 
            // cmdCreditCardTypeIndividual
            // 
            this.cmdCreditCardTypeIndividual.AutoSize = true;
            this.cmdCreditCardTypeIndividual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdCreditCardTypeIndividual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCreditCardTypeIndividual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCreditCardTypeIndividual.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCreditCardTypeIndividual.ForeColor = System.Drawing.Color.White;
            this.cmdCreditCardTypeIndividual.Location = new System.Drawing.Point(200, 40);
            this.cmdCreditCardTypeIndividual.Name = "cmdCreditCardTypeIndividual";
            this.cmdCreditCardTypeIndividual.Size = new System.Drawing.Size(52, 52);
            this.cmdCreditCardTypeIndividual.TabIndex = 2;
            this.cmdCreditCardTypeIndividual.TabStop = false;
            this.cmdCreditCardTypeIndividual.UseVisualStyleBackColor = true;
            this.cmdCreditCardTypeIndividual.Click += new System.EventHandler(this.cmdCreditCardTypeIndividual_Click);
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCurrency.Location = new System.Drawing.Point(292, 61);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(160, 13);
            this.lblCurrency.TabIndex = 1;
            this.lblCurrency.Text = "Individual Credit Card Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(451, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "[ F1 ]";
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
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
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
					mCreditType = CreditType.Individual;
                    dialog = DialogResult.OK;
                    this.Hide();
					break;

                case Keys.F2:
					mCreditType = CreditType.Group;
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

            try
            { this.cmdCreditCardTypeIndividual.Image = new Bitmap(Application.StartupPath + "/images/CreditCardTypeIndividual.jpg"); }
            catch { }
            try
            { this.cmdCreditCardTypeGroup.Image = new Bitmap(Application.StartupPath + "/images/CreditCardTypeGroup.jpg"); }
            catch { }
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
            mCreditType= CreditType.Individual;
        }

        private void cmdCreditCardTypeGroup_Click(object sender, EventArgs e)
        {
            mCreditType= CreditType.Group;
        }

		#region Private Methods

		#endregion

	}
}
