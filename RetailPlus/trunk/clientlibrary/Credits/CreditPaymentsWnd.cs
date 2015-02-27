using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
	public class CreditPaymentsWnd : Form
	{
		private PictureBox imgIcon;
        private GroupBox grpBox1;
		private Label lblHeader1;
		private Label lblHeader;
		private Button cmdCancel;
		private Button cmdEnter;
		private System.ComponentModel.Container components = null;
        private DataGridView dgvItems;

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private Data.ContactDetails mclsCustomerDetails;
        public Data.ContactDetails CustomerDetails
        {
            set { mclsCustomerDetails = value; }
            get { return mclsCustomerDetails; }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        private Data.SysConfigDetails mclsSysConfigDetails;
        private TextBox txtTrxStartDate;
        private TextBox txtTrxEndDate;
        private Label label1;
        private Label label2;
        private Label lblBalanceName;
        private Label lblTotal;
        private Label label15;
        private Label label16;
        private Label label12;
    
        public Data.SysConfigDetails SysConfigDetails
        {
            set { mclsSysConfigDetails = value; }
        }
        public Int64 CashierID { get; set; }

        private bool mboCreditPaymentReversal;

		#region Constructors and Destuctors

		public CreditPaymentsWnd()
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.txtTrxStartDate = new System.Windows.Forms.TextBox();
            this.txtTrxEndDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBalanceName = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 68;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // grpBox1
            // 
            this.grpBox1.BackColor = System.Drawing.Color.White;
            this.grpBox1.Controls.Add(this.dgvItems);
            this.grpBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox1.ForeColor = System.Drawing.Color.Blue;
            this.grpBox1.Location = new System.Drawing.Point(9, 60);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(1009, 509);
            this.grpBox1.TabIndex = 69;
            this.grpBox1.TabStop = false;
            this.grpBox1.Text = "Credit Purchases";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CausesValidation = false;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.ColumnHeadersHeight = 24;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvItems.GridColor = System.Drawing.Color.White;
            this.dgvItems.Location = new System.Drawing.Point(8, 24);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(994, 464);
            this.dgvItems.StandardTab = true;
            this.dgvItems.TabIndex = 2;
            this.dgvItems.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvItems_RowStateChanged);
            // 
            // lblHeader1
            // 
            this.lblHeader1.AutoSize = true;
            this.lblHeader1.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.ForeColor = System.Drawing.Color.White;
            this.lblHeader1.Location = new System.Drawing.Point(67, 22);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(118, 13);
            this.lblHeader1.TabIndex = 70;
            this.lblHeader1.Text = "Credit Payments of ";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Red;
            this.lblHeader.Location = new System.Drawing.Point(192, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(133, 13);
            this.lblHeader.TabIndex = 88;
            this.lblHeader.Text = "RetailPlus Customer ™";
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
            // txtTrxStartDate
            // 
            this.txtTrxStartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrxStartDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrxStartDate.Location = new System.Drawing.Point(714, 31);
            this.txtTrxStartDate.Name = "txtTrxStartDate";
            this.txtTrxStartDate.Size = new System.Drawing.Size(136, 23);
            this.txtTrxStartDate.TabIndex = 0;
            // 
            // txtTrxEndDate
            // 
            this.txtTrxEndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTrxEndDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrxEndDate.Location = new System.Drawing.Point(880, 31);
            this.txtTrxEndDate.Name = "txtTrxEndDate";
            this.txtTrxEndDate.Size = new System.Drawing.Size(136, 23);
            this.txtTrxEndDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(526, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Enter Transaction Date From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(856, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = " - ";
            // 
            // lblBalanceName
            // 
            this.lblBalanceName.AutoSize = true;
            this.lblBalanceName.BackColor = System.Drawing.Color.Transparent;
            this.lblBalanceName.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceName.Location = new System.Drawing.Point(623, 579);
            this.lblBalanceName.Name = "lblBalanceName";
            this.lblBalanceName.Size = new System.Drawing.Size(85, 29);
            this.lblBalanceName.TabIndex = 91;
            this.lblBalanceName.Text = "TOTAL:";
            this.lblBalanceName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTotal.Location = new System.Drawing.Point(700, 581);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(318, 25);
            this.lblTotal.TabIndex = 92;
            this.lblTotal.Text = "0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(743, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 112;
            this.label15.Text = "[Del]";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(768, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(157, 13);
            this.label16.TabIndex = 111;
            this.label16.Text = " to cancel the posted payment.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(712, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 110;
            this.label12.Text = "Press";
            // 
            // CreditPaymentsWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lblBalanceName);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTrxEndDate);
            this.Controls.Add(this.txtTrxStartDate);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblHeader1);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.grpBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "CreditPaymentsWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CreditPaymentsWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreditPaymentsWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		#endregion

		#region Windows Form Methods

		private void CreditPaymentsWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Credits.jpg");	}
			catch{}
			try
			{ this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
			catch { }
			try
			{ this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
			catch { }

			lblHeader.Text = mclsCustomerDetails.ContactName;

            Security.AccessRights clsAccessRights = new Security.AccessRights();
            Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

            clsDetails = clsAccessRights.Details(CashierID, (Int16)AccessTypes.CreditPaymentReversal);
            mboCreditPaymentReversal = clsDetails.Write;

            clsAccessRights.CommitAndDispose();


			LoadOptions();
			LoadData();
		}

		private void CreditPaymentsWnd_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					this.Hide(); 
					break;

				case Keys.Enter:
                    LoadData();
					break;

                case Keys.Up:
                case Keys.Down:
                    if (!dgvItems.Focused) dgvItems.Focus();
                    break;

                case Keys.Back:
                case Keys.Delete:
                    DeletePayment();
                    break;
			}
		}

		#endregion

		#region Windows Control Methods

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			dialog = DialogResult.Cancel;
			this.Hide();
		}
		private void cmdEnter_Click(object sender, EventArgs e)
		{
            LoadData();
		}
        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void dgvItems_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //decimal decTotalPayable = 0;
            //decimal decTemp = 0;

            //foreach (DataGridViewRow dr in dgvItems.SelectedRows)
            //{
            //    decTemp = 0;
            //    decimal.TryParse(dr.Cells["Balance"].Value.ToString(), out decTemp);
            //    decTotalPayable += decTemp;
            //}
            //lblBalanceSelected.Text = decTotalPayable.ToString("#,##0.#0");
        }

		#endregion

		#region Private Modifiers

		private void LoadOptions()
		{
            txtTrxStartDate.Text = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");
            txtTrxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
		
		private void LoadData()
		{	
			try
			{
                DateTime dteRetValue = DateTime.MinValue;
                Data.CreditPaymentCashDetails clsCreditPaymentCashDetails = new Data.CreditPaymentCashDetails();
                clsCreditPaymentCashDetails.BranchDetails = new Data.BranchDetails();
                clsCreditPaymentCashDetails.TerminalNo = "";
                clsCreditPaymentCashDetails.PaymentDateFrom = DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : DateTime.Now.AddDays(-60);
                clsCreditPaymentCashDetails.PaymentDateTo = DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : DateTime.Now;
                clsCreditPaymentCashDetails.CreditType = CreditType.Both;
                clsCreditPaymentCashDetails.CreditCardTypeID = 0;
                clsCreditPaymentCashDetails.ContactID = mclsCustomerDetails.ContactID;

                Data.Contacts clsContacts = new Data.Contacts();
                System.Data.DataTable dt = clsContacts.CreditPaymentCashAsDataTable(clsCreditPaymentCashDetails, "trx.CreatedOn");
				clsContacts.CommitAndDispose();

                System.Data.DataView dv = dt.DefaultView;
                dv.Sort = "TransactionDate";
                dt = dv.ToTable();

                dgvItems.MultiSelect = false;
                dgvItems.AutoGenerateColumns = true;
                dgvItems.AutoSize = false;
                dgvItems.DataSource = dt.TableName;
                dgvItems.DataSource = dt;

                foreach (DataGridViewTextBoxColumn dc in dgvItems.Columns)
                {
                    dc.Visible = false;
                }
                dgvItems.Columns["TransactionNo"].Visible = true;
                dgvItems.Columns["TransactionDate"].Visible = true;
                dgvItems.Columns["CreditReason"].Visible = true;
                dgvItems.Columns["Amount"].Visible = true;

                dgvItems.Columns["TransactionNo"].Width = 150;
                dgvItems.Columns["TransactionDate"].Width = 150;
                if (dt.Rows.Count < 16) dgvItems.Columns["CreditReason"].Width = 350; else dgvItems.Columns["CreditReason"].Width = 320;
                int iWidth = (dgvItems.Width - dgvItems.Columns["TransactionNo"].Width - dgvItems.Columns["TransactionDate"].Width - dgvItems.Columns["CreditReason"].Width) / 1;
                if (dt.Rows.Count >= 16) iWidth = iWidth - 20;
                dgvItems.Columns["Amount"].Width = iWidth;

                dgvItems.Columns["TransactionNo"].HeaderText = "Transaction No";
                dgvItems.Columns["TransactionDate"].HeaderText = "Transaction Date";
                dgvItems.Columns["CreditReason"].HeaderText = "Description";
                dgvItems.Columns["Amount"].HeaderText = "Amt. Paid";

                dgvItems.Columns["Amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvItems.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm tt";
                dgvItems.Columns["Amount"].DefaultCellStyle.Format = "#,##0.#0";

                dgvItems.ReadOnly = true;
                dgvItems.Select();

                decimal decTotalPayable = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    decimal decTemp = decimal.TryParse(dr["Amount"].ToString(), out decTemp) ? decTemp : 0;
                    decTotalPayable += decTemp;
                }
                lblTotal.Text = decTotalPayable.ToString("#,##0.#0");

                grpBox1.Text = "Payments from: " + clsCreditPaymentCashDetails.PaymentDateFrom.ToString("MMM dd, yyyy") + " to " + clsCreditPaymentCashDetails.PaymentDateTo.ToString("MMM dd, yyyy");

                txtTrxStartDate.SelectAll();
                txtTrxStartDate.Select();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

        private void DeletePayment()
        {
            if (!mboCreditPaymentReversal)
            {
                MessageBox.Show("Sorry you are not allowed to reverse payments, please consult your system administrator.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                if (dgvItems.SelectedRows.Count == 1)
                {
                    foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                    {
                        DateTime TransactionDate = DateTime.Parse(dr.Cells["TransactionDate"].Value.ToString());
                        if (TransactionDate < mclsCustomerDetails.CreditDetails.LastBillingDate)
                        {
                            MessageBox.Show("Sorry you cannot reverse payment's that are already included in billing statement last: " + mclsCustomerDetails.CreditDetails.LastBillingDate.ToString("MMM dd, yyyy"), "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            break;
                        }
                        else
                        {
                            decimal AmountPaid = decimal.Parse(dr.Cells["Amount"].Value.ToString());

                            if (MessageBox.Show("Are you sure you want to delete this Payment? " +
                                        "If you delete this payment, the current credit of " + mclsCustomerDetails.ContactName + " will be: " + Environment.NewLine + "   " + mclsCustomerDetails.Credit.ToString("#,##0.#0") + " + " + AmountPaid.ToString("#,##0.#0") + " = " + (mclsCustomerDetails.Credit + AmountPaid).ToString("#,##0.#0"), "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            {
                                Int32 BranchID = Int32.Parse(dr.Cells["BranchID"].Value.ToString());
                                string TerminalNo = dr.Cells["TerminalNo"].Value.ToString();
                                string TransactionNo = dr.Cells["TransactionNo"].Value.ToString();
                                Int64 TransactionID = Int64.Parse(dr.Cells["TransactionID"].Value.ToString());

                                // put the automatic adjustment outside

                                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
                                clsLocalConnection.GetConnection();

                                Data.Creditors clsCreditors = new Data.Creditors(clsLocalConnection.Connection, clsLocalConnection.Transaction);

                                switch (mclsSysConfigDetails.CreditPaymentType)
                                {
                                    case CreditPaymentType.Houseware:
                                        clsCreditors.DeleteCreditTransaction(BranchID, TerminalNo, TransactionID);
                                        clsCreditors.AutoAdjustCredit(mclsCustomerDetails, mclsCustomerDetails.Credit + AmountPaid);
                                        break;
                                    case CreditPaymentType.Normal:
                                    case CreditPaymentType.MPC:
                                    default:
                                        clsCreditors.AdjustCredit(BranchID, TerminalNo, TransactionID, mclsCustomerDetails, mclsCustomerDetails.Credit + AmountPaid, AmountPaid);
                                        clsCreditors.DeleteCreditTransaction(BranchID, TerminalNo, TransactionID);
                                        break;
                                }

                                clsLocalConnection.CommitAndDispose();
                                mclsCustomerDetails.Credit = mclsCustomerDetails.Credit + AmountPaid;
                                this.Hide();
                                dialog = System.Windows.Forms.DialogResult.OK;
                            }
                        }

                    }
                }
            }
        }

		#endregion

	}
}
