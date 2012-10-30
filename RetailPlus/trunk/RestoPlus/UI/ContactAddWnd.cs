using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ContactAddWnd.
	/// </summary>
	public class ContactAddWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgIcon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private Data.ContactDetails mContactDetails;
        private DialogResult dialog;
        private GroupBox groupBox1;
        private Label label3;
        private TextBox txtTelNo;
        private Label label2;
        private TextBox txtBusinessName;
        private Label label1;
        private TextBox txtAddress;
        private Label lblCaption;
        private TextBox txtCustomerName;
        private TextBox txtSelectedTexBox;
        private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
        private Button cmdCancel;
        private Button cmdEnter;
		private string mstCaption;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

		public string Caption
		{
			get {	return mstCaption;	}
			set {	mstCaption = value;	}
		}

		public Data.ContactDetails ContactDetails
		{
			get {	return mContactDetails;	}
			set {	mContactDetails = value;	}
		}


		#region Constructors And Desctructors
		public ContactAddWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTelNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusinessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
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
            this.lblHeader.Size = new System.Drawing.Size(166, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Enter Customer Information";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 12;
            this.imgIcon.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTelNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBusinessName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.lblCaption);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contact Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(402, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Please enter contact tel. no.";
            // 
            // txtTelNo
            // 
            this.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelNo.Location = new System.Drawing.Point(442, 102);
            this.txtTelNo.MaxLength = 75;
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Size = new System.Drawing.Size(200, 30);
            this.txtTelNo.TabIndex = 2;
            this.txtTelNo.GotFocus += new System.EventHandler(this.txtTelNo_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(42, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Please enter customer contact person";
            // 
            // txtBusinessName
            // 
            this.txtBusinessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBusinessName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusinessName.Location = new System.Drawing.Point(82, 102);
            this.txtBusinessName.MaxLength = 75;
            this.txtBusinessName.Name = "txtBusinessName";
            this.txtBusinessName.Size = new System.Drawing.Size(282, 30);
            this.txtBusinessName.TabIndex = 1;
            this.txtBusinessName.GotFocus += new System.EventHandler(this.txtBusinessName_GotFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(42, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please enter customer address";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(82, 161);
            this.txtAddress.MaxLength = 150;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(560, 39);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.GotFocus += new System.EventHandler(this.txtAddress_GotFocus);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCaption.Location = new System.Drawing.Point(42, 25);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(170, 13);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Please Enter Customer Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.Location = new System.Drawing.Point(82, 43);
            this.txtCustomerName.MaxLength = 25;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(282, 30);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.GotFocus += new System.EventHandler(this.txtCustomerName_GotFocus);
            // 
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(2, 298);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
            this.keyboardSearchControl1.TabIndex = 3;
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
            // ContactAddWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ContactAddWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactAddWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactAddWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		#endregion

		#region Windows Form Methods
		private void ContactAddWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (txtCustomerName.Text == "" || txtCustomerName.Text == null)
					{
						MessageBox.Show("Please enter a valid customer name.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					try
					{
						if (SaveRecord() == true)
						{							
							dialog = DialogResult.OK; 
							this.Hide();
						}
					}
					catch (Exception ex)
					{
						Event clsEvent = new Event();
						clsEvent.AddEventLn("ERROR!!! Saving customer details. Err Description: " + ex.Message);
						MessageBox.Show("Sorry the customer name is already in the database. Please type another customer name." ,"RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					break;
			}
		}

		private void ContactAddWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Balance.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			this.lblHeader.Text = mstCaption;
		}
		
		#endregion

		#region Private Methods
		private bool SaveRecord()
		{
			Data.ContactDetails clsDetails = new Data.ContactDetails();
			clsDetails.ContactCode = txtCustomerName.Text;
			clsDetails.ContactName = txtCustomerName.Text;
			clsDetails.ContactGroupID = Constants.CONTACT_GROUP_CUSTOMER;
			clsDetails.ModeOfTerms =  0;
			clsDetails.Terms = 0;
			clsDetails.Address = txtAddress.Text;
			clsDetails.BusinessName = txtBusinessName.Text;
			clsDetails.TelephoneNo = txtTelNo.Text;
			if (mstCaption == "Please enter customer name for deposit.")
			{	clsDetails.Remarks = Data.Contact.DEFAULT_REMARKS_FOR_ADDED_FROM_DEPOSIT; }
            else if (mstCaption=="Quickly add new customer")
            {   clsDetails.Remarks = Data.Contact.DEFAULT_REMARKS_FOR_QUICKLY_ADDED_FROM_FE; }
			else
			{	clsDetails.Remarks = Data.Contact.DEFAULT_REMARKS_FOR_ADDED_FROM_CLIENT; }
			clsDetails.Debit = 0;
			clsDetails.Credit = 0;
			clsDetails.IsCreditAllowed = 0;
			clsDetails.CreditLimit = 0;
            clsDetails.PositionID = Constants.C_RETAILPLUS_AGENT_POSITIONID;
            clsDetails.DepartmentID = Constants.C_RETAILPLUS_AGENT_DEPARTMENTID;

			Data.Contact clsContact = new Data.Contact();
			clsDetails.ContactID = clsContact.Insert(clsDetails);
			clsContact.CommitAndDispose();

			mContactDetails = clsDetails;

			return true;
		}

		#endregion

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox == null)
                txtCustomerName.Focus();
            else if (txtSelectedTexBox.Name == txtCustomerName.Name)
                txtCustomerName.Focus();
            else if (txtSelectedTexBox.Name == txtBusinessName.Name)
                txtBusinessName.Focus();
            else if (txtSelectedTexBox.Name == txtTelNo.Name)
                txtTelNo.Focus();
            else if (txtSelectedTexBox.Name == txtAddress.Name)
                txtAddress.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text == "" || txtCustomerName.Text == null)
            {
                MessageBox.Show("Please enter a valid customer name.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (SaveRecord() == true)
                {
                    dialog = DialogResult.OK;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR!!! Saving customer details. Err Description: " + ex.Message);
                MessageBox.Show("Sorry the customer name is already in the database. Please type another customer name.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtCustomerName_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtBusinessName_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtTelNo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }

        private void txtAddress_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }
		
	}
}
