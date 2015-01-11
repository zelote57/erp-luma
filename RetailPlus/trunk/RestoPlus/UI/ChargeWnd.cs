using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ChargeWnd.
	/// </summary>
	public class ChargeWnd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox imgIcon;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboChargeType;
		private System.Windows.Forms.Label lblChargeTypes;
		private System.Windows.Forms.TextBox txtRemarks;
		private System.Windows.Forms.Label lblCash;
		private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DialogResult dialog;
		private ChargeTypes mChargeType;
		private decimal mdecBalanceAmount;
		private decimal mdecChargeAmount;
		private string mChargeCode;
        private bool mbolIsChargeEditable;

		private System.Windows.Forms.Label lblChargeType;
        private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
        private Button cmdCancel;
        private Button cmdEnter;
        private AceSoft.KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private Label lblDescription;
        private Label lblBalanceAmount;
		private string mRemarks;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

        public bool IsChargeEditable
        {
            set
            {
                mbolIsChargeEditable = value;
            }
        }

		public ChargeTypes ChargeType
		{
			get 
			{
				return mChargeType;
			}
			set 
			{
				mChargeType = value;
			}
		}

		public decimal ChargeAmount
		{
			get
			{
				return mdecChargeAmount;
			}
			set
			{
				mdecChargeAmount = value;;
			}
		}

		public decimal BalanceAmount
		{
			set
			{
				mdecBalanceAmount = value;
			}
		}

		public string ChargeCode
		{
			set
			{
				mChargeCode = value;
			}
			get 
			{
				return mChargeCode;
			}
		}

		public string Remarks
		{
			set
			{
				mRemarks = value;
			}
			get 
			{
				return mRemarks;
			}
		}

        public Data.TerminalDetails TerminalDetails { get; set; }

		public ChargeWnd()
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
		private void InitializeComponent()
		{
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboChargeType = new System.Windows.Forms.ComboBox();
            this.lblChargeType = new System.Windows.Forms.Label();
            this.lblChargeTypes = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
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
            this.imgIcon.TabIndex = 87;
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
            this.lblHeader.Size = new System.Drawing.Size(138, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Tender Charge Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboChargeType);
            this.groupBox1.Controls.Add(this.lblChargeType);
            this.groupBox1.Controls.Add(this.lblChargeTypes);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.lblCash);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(8, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Press ENTER key to apply charge.";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(801, 17);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(201, 19);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Current Balance to be paid.";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(184, 30);
            this.lblBalanceAmount.TabIndex = 8;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(346, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Add an optional 255 character remarks below.";
            // 
            // cboChargeType
            // 
            this.cboChargeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChargeType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChargeType.Location = new System.Drawing.Point(379, 35);
            this.cboChargeType.Name = "cboChargeType";
            this.cboChargeType.Size = new System.Drawing.Size(200, 31);
            this.cboChargeType.TabIndex = 0;
            this.cboChargeType.SelectedIndexChanged += new System.EventHandler(this.cboChargeType_SelectedIndexChanged);
            this.cboChargeType.GotFocus += new System.EventHandler(this.cboChargeType_GotFocus);
            // 
            // lblChargeType
            // 
            this.lblChargeType.AutoSize = true;
            this.lblChargeType.BackColor = System.Drawing.Color.Transparent;
            this.lblChargeType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeType.ForeColor = System.Drawing.Color.Red;
            this.lblChargeType.Location = new System.Drawing.Point(663, 109);
            this.lblChargeType.Name = "lblChargeType";
            this.lblChargeType.Size = new System.Drawing.Size(20, 13);
            this.lblChargeType.TabIndex = 6;
            this.lblChargeType.Text = "%";
            // 
            // lblChargeTypes
            // 
            this.lblChargeTypes.AutoSize = true;
            this.lblChargeTypes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeTypes.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblChargeTypes.Location = new System.Drawing.Point(421, 16);
            this.lblChargeTypes.Name = "lblChargeTypes";
            this.lblChargeTypes.Size = new System.Drawing.Size(116, 13);
            this.lblChargeTypes.TabIndex = 3;
            this.lblChargeTypes.Text = "Select Charge Type";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(118, 162);
            this.txtRemarks.MaxLength = 255;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(723, 56);
            this.txtRemarks.TabIndex = 2;
            this.txtRemarks.GotFocus += new System.EventHandler(this.txtRemarks_GotFocus);
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCash.Location = new System.Drawing.Point(416, 80);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(126, 13);
            this.lblCash.TabIndex = 4;
            this.lblCash.Text = "Charge to be applied.";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Enabled = false;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(302, 99);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(355, 30);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(35, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "ENTER";
            // 
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(95, 323);
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
            // keyboardNoControl1
            // 
            this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
            this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Up;
            this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Down;
            this.keyboardNoControl1.Location = new System.Drawing.Point(400, 323);
            this.keyboardNoControl1.Name = "keyboardNoControl1";
            this.keyboardNoControl1.Size = new System.Drawing.Size(208, 172);
            this.keyboardNoControl1.TabIndex = 88;
            this.keyboardNoControl1.TabStop = false;
            this.keyboardNoControl1.Visible = false;
            this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
            // 
            // ChargeWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardNoControl1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChargeWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ChargeWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChargeWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void ChargeWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
		private void ChargeWnd_Load(object sender, System.EventArgs e)
		{
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Charge.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			LoadOptions();
		}
        private void cboChargeType_GotFocus(object sender, System.EventArgs e)
        {
            keyboardNoControl1.Visible = false;
            keyboardSearchControl1.Visible = false;
        }
		private void txtAmount_GotFocus(object sender, System.EventArgs e)
		{
            keyboardNoControl1.Visible = true;
            keyboardSearchControl1.Visible = false;
		}
		private void txtAmount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			Methods clsMethods = new Methods();
			e.Handled = clsMethods.AllNumWithDecimal(Convert.ToInt32(e.KeyChar));
		}
		private void txtRemarks_GotFocus(object sender, System.EventArgs e)
		{
            keyboardNoControl1.Visible = false;
            keyboardSearchControl1.Visible = true;
		}


		#region Private Methods

		private bool isValuesAssigned()
		{
			try 
			{
				mdecChargeAmount = Convert.ToDecimal(txtAmount.Text);
				if (mdecChargeAmount ==  0)
				{	mChargeType = ChargeTypes.NotApplicable;	}

				mChargeCode = cboChargeType.Text;
				mRemarks = txtRemarks.Text;

				return true;
			}
			catch
			{
				MessageBox.Show("Sorry you have entered an invalid amount for Charge." +
					"Please type a valid Charge amount.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		private void LoadOptions() 
		{
			if (mChargeType == ChargeTypes.Percentage)
			{
				lblHeader.Text = "Percentage Charge";
				lblChargeType.Visible = true;
			}
			else
			{
				lblHeader.Text = "Amount Charge";
				lblChargeType.Visible = false;
			}

			txtAmount.Text = mdecChargeAmount.ToString("#,##0.#0");
			lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
			
			lblDescription.Text = "Current Balance to be paid.";

			cboChargeType.Items.Clear();
			Data.ChargeType clsCharge = new Data.ChargeType();
			foreach (System.Data.DataRow dr in clsCharge.ListAsDataTable().Rows)
			{
				cboChargeType.Items.Add(dr["ChargeTypeCode"]);
			}
			clsCharge.CommitAndDispose();

			if (mChargeCode != null & mChargeCode != "")
				cboChargeType.SelectedIndex = cboChargeType.Items.IndexOf(mChargeCode);
			else 
			{
				cboChargeType.SelectedIndex = cboChargeType.Items.Count - 1;
			}
			txtRemarks.Text = mRemarks;
            if (mbolIsChargeEditable == true) { txtAmount.Enabled = true; txtAmount.Focus(); }
		}

		#endregion

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

        private void cboChargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data.ChargeType clsCharge = new Data.ChargeType();
            Data.ChargeTypeDetails clsChargeTypeDetails = clsCharge.Details(cboChargeType.SelectedItem.ToString());
            clsCharge.CommitAndDispose();

            //if (clsChargeTypeDetails.ChargeAmount == 0)
            //{
            //    mChargeType = ChargeTypes.NotApplicable;
            //    lblHeader.Text = "Not Applicable";
            //    lblChargeType.Visible = false;
            //}
            //else 
            if (clsChargeTypeDetails.InPercent)
            {
                mChargeType = ChargeTypes.Percentage;
                lblHeader.Text = clsChargeTypeDetails.ChargeType + ": Percentage Charge";
                lblChargeType.Visible = true;
            }
            else
            {
                mChargeType = ChargeTypes.FixedValue;
                lblHeader.Text = clsChargeTypeDetails.ChargeType + ": Amount Charge";
                lblChargeType.Visible = false;
            }

            txtAmount.Text = clsChargeTypeDetails.ChargeAmount.ToString("#,##0.#0");
        }
	}
}
