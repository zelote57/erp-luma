using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class DiscountWnd : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
        private DialogResult dialog;
		private GroupBox groupBox1;
		private TextBox txtRemarks;
		private Label lblCash;
		private TextBox txtAmount;
        private Label lblHeader;
		private PictureBox imgIcon;
		private Label label1;
		private Label lblDiscountType;
		private DiscountTypes mDiscountType;
		private decimal mdecBalanceAmount;
		private Label lblDicountTypes;
		private Label label2;
		private ComboBox cboDiscountType;
		private decimal mdecDiscountAmount;
		private string mDiscountCode;
        private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;
        private Button cmdCancel;
        private Button cmdEnter;
		private string mRemarks;
        private Label lblBalanceAmount;
        private Label lblDescription;
        private AceSoft.KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private bool mbolIsDiscountEditable;
        private string mstHeader;

        public string Header
        {
            set
            {
                mstHeader = value;
            }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }
        public bool IsDiscountEditable
        {
            set
            {
                mbolIsDiscountEditable = value;
            }
        }

        //private bool mDisableVATExempt;
        //public bool DisableVATExempt
        //{
        //    set
        //    {
        //        mDisableVATExempt = value;
        //    }
        //}

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}

		public DiscountTypes DiscountType
		{
			get 
			{
				return mDiscountType;
			}
			set 
			{
				mDiscountType = value;
			}
		}

		public decimal DiscountAmount
		{
			get
			{
				return mdecDiscountAmount;
			}
			set
			{
				mdecDiscountAmount = value;;
			}
		}

		public decimal BalanceAmount
		{
			set
			{
				mdecBalanceAmount = value;
			}
		}

		public string DiscountCode
		{
			set
			{
				mDiscountCode = value;
			}
			get 
			{
				return mDiscountCode;
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


		public DiscountWnd()
		{
			InitializeComponent();

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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDiscountType = new System.Windows.Forms.ComboBox();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.lblDicountTypes = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDiscountType);
            this.groupBox1.Controls.Add(this.lblDiscountType);
            this.groupBox1.Controls.Add(this.lblDicountTypes);
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
            this.groupBox1.Text = "Press ENTER key to apply discount.";
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.Red;
            this.lblBalanceAmount.Location = new System.Drawing.Point(611, 10);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(184, 30);
            this.lblBalanceAmount.TabIndex = 8;
            this.lblBalanceAmount.Text = "0.00";
            this.lblBalanceAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(801, 17);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(201, 19);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Current Balance to be paid.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(346, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Add an optional 255 character remarks below.";
            // 
            // cboDiscountType
            // 
            this.cboDiscountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiscountType.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDiscountType.Location = new System.Drawing.Point(379, 38);
            this.cboDiscountType.Name = "cboDiscountType";
            this.cboDiscountType.Size = new System.Drawing.Size(200, 31);
            this.cboDiscountType.TabIndex = 0;
            this.cboDiscountType.SelectedIndexChanged += new System.EventHandler(this.cboDiscountType_SelectedIndexChanged);
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscountType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscountType.ForeColor = System.Drawing.Color.Red;
            this.lblDiscountType.Location = new System.Drawing.Point(664, 112);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(20, 13);
            this.lblDiscountType.TabIndex = 5;
            this.lblDiscountType.Text = "%";
            // 
            // lblDicountTypes
            // 
            this.lblDicountTypes.AutoSize = true;
            this.lblDicountTypes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDicountTypes.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblDicountTypes.Location = new System.Drawing.Point(417, 18);
            this.lblDicountTypes.Name = "lblDicountTypes";
            this.lblDicountTypes.Size = new System.Drawing.Size(125, 13);
            this.lblDicountTypes.TabIndex = 3;
            this.lblDicountTypes.Text = "Select Discount Type";
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
            this.lblCash.Location = new System.Drawing.Point(412, 82);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(135, 13);
            this.lblCash.TabIndex = 4;
            this.lblCash.Text = "Discount to be applied.";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Enabled = false;
            this.txtAmount.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(301, 102);
            this.txtAmount.MaxLength = 16;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(357, 30);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.GotFocus += new System.EventHandler(this.txtAmount_GotFocus);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label1
            // 
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
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(147, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Tender Discount Amount";
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
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(95, 323);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(799, 134);
            this.keyboardSearchControl1.TabIndex = 2;
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
            // keyboardNoControl1
            // 
            this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
            this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Up;
            this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Down;
            this.keyboardNoControl1.Location = new System.Drawing.Point(400, 323);
            this.keyboardNoControl1.Name = "keyboardNoControl1";
            this.keyboardNoControl1.Size = new System.Drawing.Size(208, 172);
            this.keyboardNoControl1.TabIndex = 1;
            this.keyboardNoControl1.TabStop = false;
            this.keyboardNoControl1.Visible = false;
            this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
            // 
            // DiscountWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardNoControl1);
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
            this.MinimizeBox = false;
            this.Name = "DiscountWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DiscountWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DiscountWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void DiscountWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
		private bool isValuesAssigned()
		{
			try 
			{
				mdecDiscountAmount = Convert.ToDecimal(txtAmount.Text);
				if (mdecDiscountAmount ==  0)
				{	mDiscountType = DiscountTypes.NotApplicable;	}

				mDiscountCode = cboDiscountType.Text;
				mRemarks = txtRemarks.Text;

				return true;
			}
			catch
			{
				MessageBox.Show("Sorry you have entered an invalid amount for discount." +
					"Please type a valid discount amount.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		private void DiscountWnd_Load(object sender, System.EventArgs e)
		{
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Discount.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

			LoadOptions();
			
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

		private void LoadOptions() 
		{
            lblHeader.Text = mstHeader;

            if (mDiscountType == DiscountTypes.Percentage)
            {
                lblDiscountType.Visible = true;
            }
            else if (mDiscountType == DiscountTypes.FixedValue)
            {
                lblDiscountType.Visible = false;
            }
            else if (mDiscountType == DiscountTypes.NotApplicable)
            {
                lblDiscountType.Visible = false;
            }

			txtAmount.Text = mdecDiscountAmount.ToString("#,##0.#0");
			lblBalanceAmount.Text = mdecBalanceAmount.ToString("#,##0.#0");
			
			lblDescription.Text = "Current Balance to be paid.";

            //string strTmp = "";
            cboDiscountType.Items.Clear();
            Data.Discount clsDiscount = new Data.Discount();
            foreach (System.Data.DataRow dr in clsDiscount.ListAsDataTable().Rows)
            {
                //if (mDisableVATExempt && dr["DiscountCode"].ToString() == TerminalDetails.SeniorCitizenDiscountCode)
                //    strTmp = "";
                //else if (mDisableVATExempt && dr["DiscountCode"].ToString() == TerminalDetails.PWDDiscountCode)
                //    strTmp = "";
                //else
                    cboDiscountType.Items.Add(dr["DiscountCode"]);
            }
			clsDiscount.CommitAndDispose();

			if (mDiscountCode != null & mDiscountCode != "")
				cboDiscountType.SelectedIndex = cboDiscountType.Items.IndexOf(mDiscountCode);
			else 
			{
				cboDiscountType.SelectedIndex = cboDiscountType.Items.Count - 1;
			}
			txtRemarks.Text = mRemarks;
            if (mbolIsDiscountEditable == true) { txtAmount.Enabled = true; txtAmount.Focus(); }
		}

		private void cboDiscountType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadChangeDiscount();
		}

        private void LoadChangeDiscount()
        {
            if (cboDiscountType.Text != null & cboDiscountType.Text != "")
            {
                Data.Discount clsDiscount = new Data.Discount();
                Data.DiscountDetails clsDiscountDetails = clsDiscount.Details(cboDiscountType.Text);
                clsDiscount.CommitAndDispose();

                //if (clsDiscountDetails.DiscountPrice == 0)
                //{
                //    mDiscountType = DiscountTypes.NotApplicable;
                //    lblHeader.Text = "Not Applicable";
                //    lblDiscountType.Visible = false;
                //}
                //else 
                lblHeader.Text = mstHeader;
                if (clsDiscountDetails.InPercent)
                {
                    mDiscountType = DiscountTypes.Percentage;
                    lblDiscountType.Visible = true;
                }
                else
                {
                    mDiscountType = DiscountTypes.FixedValue;
                    lblDiscountType.Visible = false;
                }

                txtAmount.Text = clsDiscountDetails.DiscountPrice.ToString("#,##0.#0");
            }
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
	}
}