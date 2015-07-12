using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.FtpClient;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ItemAttachmentWnd : Form
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

        public Data.SalesTransactionItemDetails SalesTransactionItemDetails { get; set; }

        public Data.TerminalDetails TerminalDetails { get; set; }

        private Data.SysConfigDetails mclsSysConfigDetails;
        private Label label2;
        private Label label15;
        private Label label16;
        private Label label12;
    
        public Data.SysConfigDetails SysConfigDetails
        {
            set { mclsSysConfigDetails = value; }
        }
        public Int64 CashierID { get; set; }
        public string CashierName { get; set; }

        private bool mboDeleteTransactionItemAttachment;
        private Label label1;
        private Label label3;
        private Label label4;
        private bool mboViewDeletedTransactionItemAttachment;

		#region Constructors and Destuctors

		public ItemAttachmentWnd()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.grpBox1.Text = "Item Attachments";
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
            this.lblHeader1.Size = new System.Drawing.Size(133, 13);
            this.lblHeader1.TabIndex = 70;
            this.lblHeader1.Text = "Attachment details of:";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Red;
            this.lblHeader.Location = new System.Drawing.Point(202, 22);
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
            this.cmdEnter.Size = new System.Drawing.Size(125, 83);
            this.cmdEnter.TabIndex = 3;
            this.cmdEnter.Text = "OPEN FILE";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
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
            this.label16.Size = new System.Drawing.Size(176, 13);
            this.label16.TabIndex = 111;
            this.label16.Text = " to remove the posted attachment.";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(743, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 115;
            this.label1.Text = "[+]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(768, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 114;
            this.label3.Text = " to add an attachment.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(712, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Press";
            // 
            // ItemAttachmentWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblHeader1);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.grpBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "ItemAttachmentWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ItemAttachmentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemAttachmentWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		#endregion

		#region Windows Form Methods

		private void ItemAttachmentWnd_Load(object sender, System.EventArgs e)
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

            lblHeader.Text = SalesTransactionItemDetails.ProductCode;

            Security.AccessRights clsAccessRights = new Security.AccessRights();
            Security.AccessRightsDetails clsDetails = new Security.AccessRightsDetails();

            clsDetails = clsAccessRights.Details(CashierID, (Int16)AccessTypes.TransactionItemAttachmentDelete);
            mboDeleteTransactionItemAttachment = clsDetails.Write;

            clsDetails = clsAccessRights.Details(CashierID, (Int16)AccessTypes.TransactionItemAttachmentViewDeleted);
            mboViewDeletedTransactionItemAttachment = clsDetails.Write;

            clsAccessRights.CommitAndDispose();

			LoadOptions();
			LoadData();
		}

		private void ItemAttachmentWnd_KeyDown(object sender, KeyEventArgs e)
		{
            System.Diagnostics.Debug.WriteLine(e.KeyData);

            if (Control.ModifierKeys == Keys.Shift)
                switch (e.KeyCode)
                {
                    case Keys.Add:
                    case Keys.Oemplus:
                        AttachFile();
                        break;
                }
            else
            {
                switch (e.KeyData)
                {
                    case Keys.Escape:
                        this.Hide();
                        break;

                    case Keys.Enter:
                        OpenAttachment();
                        break;

                    case Keys.Add:
                    case Keys.Oemplus:
                        AttachFile();
                        break;

                    case Keys.Up:
                    case Keys.Down:
                        if (!dgvItems.Focused) dgvItems.Focus();
                        break;

                    case Keys.Back:
                    case Keys.Delete:
                        DeleteAttachment();
                        break;
                }
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
            OpenAttachment();
		}
        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void dgvItems_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

        }

		#endregion

		#region Private Modifiers

		private void LoadOptions()
		{

        }
		
		private void LoadData()
		{	
			try
			{
                //DateTime dteRetValue = DateTime.MinValue;
                //Data.CreditPaymentCashDetails clsCreditPaymentCashDetails = new Data.CreditPaymentCashDetails();
                //clsCreditPaymentCashDetails.BranchDetails = new Data.BranchDetails();
                //clsCreditPaymentCashDetails.TerminalNo = "";
                //clsCreditPaymentCashDetails.PaymentDateFrom = DateTime.TryParse(txtTrxStartDate.Text + " 00:00:00", out dteRetValue) ? dteRetValue : DateTime.Now.AddDays(-60);
                //clsCreditPaymentCashDetails.PaymentDateTo = DateTime.TryParse(txtTrxEndDate.Text + " 23:59:59", out dteRetValue) ? dteRetValue : DateTime.Now;
                //clsCreditPaymentCashDetails.CreditType = CreditType.Both;
                //clsCreditPaymentCashDetails.CreditCardTypeID = 0;
                //clsCreditPaymentCashDetails.ContactID = mclsCustomerDetails.ContactID;

                Data.SalesTransactionItemAttachments clsSalesTransactionItemAttachments = new Data.SalesTransactionItemAttachments();
                System.Data.DataTable dt = clsSalesTransactionItemAttachments.ListAsDataTable(new Data.SalesTransactionItemAttachmentDetails() { TransactionItemsID = SalesTransactionItemDetails.TransactionItemsID });
                clsSalesTransactionItemAttachments.CommitAndDispose();

                //System.Data.DataView dv = dt.DefaultView;
                //dv.Sort = "TransactionDate";
                //dt = dv.ToTable();

                dgvItems.MultiSelect = false;
                dgvItems.AutoGenerateColumns = true;
                dgvItems.AutoSize = false;
                dgvItems.DataSource = dt.TableName;
                dgvItems.DataSource = dt;

                foreach (DataGridViewColumn dc in dgvItems.Columns)
                {
                    dc.Visible = false;
                }
                dgvItems.Columns["OrigFileName"].Visible = true;
                dgvItems.Columns["UploadedByName"].Visible = true;
                dgvItems.Columns["LastUpdatedByName"].Visible = true;
                dgvItems.Columns["CreatedOn"].Visible = true;

                dgvItems.Columns["OrigFileName"].Width = dgvItems.Width - 570;
                dgvItems.Columns["UploadedByName"].Width = 200;
                dgvItems.Columns["LastUpdatedByName"].Width = 200;
                dgvItems.Columns["CreatedOn"].Width = 150;

                dgvItems.Columns["OrigFileName"].HeaderCell.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft };
                dgvItems.Columns["UploadedByName"].HeaderCell.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft };
                dgvItems.Columns["LastUpdatedByName"].HeaderCell.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft };
                dgvItems.Columns["CreatedOn"].HeaderCell.Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleCenter };

                dgvItems.Columns["OrigFileName"].HeaderText = "File Name";
                dgvItems.Columns["UploadedByName"].HeaderText = "Uploaded By";
                dgvItems.Columns["LastUpdatedByName"].HeaderText = "Last Updated By";
                dgvItems.Columns["CreatedOn"].HeaderText = "Upload Date";

                dgvItems.Columns["CreatedOn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvItems.Columns["CreatedOn"].DefaultCellStyle.Format = "yyyy-MMM-dd hh:mm tt";

                dgvItems.ReadOnly = true;
                dgvItems.Select();

                grpBox1.Text = "Attachments of: " + SalesTransactionItemDetails.ProductCode + ".";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

        private void AttachFile()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = System.IO.Path.GetFileName(ofd.FileName);
                    string filePath = System.IO.Path.GetFullPath(ofd.FileName);

                    string strNewFileName = SalesTransactionItemDetails.TransactionID.ToString() + "_" + SalesTransactionItemDetails.TransactionItemsID + "_" + fileName;

                    Data.SalesTransactionItemAttachmentDetails clsDetails = new Data.SalesTransactionItemAttachmentDetails();
                    clsDetails.TransactionItemAttachmentsID = 0;
                    clsDetails.TransactionItemsID = SalesTransactionItemDetails.TransactionItemsID;
                    clsDetails.TransactionID = SalesTransactionItemDetails.TransactionID;
                    clsDetails.OrigFileName = fileName;
                    clsDetails.FileName = strNewFileName;
                    clsDetails.UploadedByName = CashierName;
                    clsDetails.CreatedOn = DateTime.Now;
                    clsDetails.LastModified = clsDetails.CreatedOn;

                    Data.SalesTransactionItemAttachments clsSalesTransactionItemAttachments = new Data.SalesTransactionItemAttachments();
                    clsSalesTransactionItemAttachments.Insert(clsDetails);
                    clsSalesTransactionItemAttachments.CommitAndDispose();

                    try
                    {
                        string strServer = "127.0.0.1";
                        if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"] != null)
                        {
                            try { strServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
                            catch { }
                        }

                        int intPort = 21;
                        if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                        {
                            try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                            catch { }
                        }

                        string strUserName = "ftprbsuser";
                        string strPassword = "ftprbspwd";
                        string strFTPDirectory = "attachment";

                        string destinationDirectory = Application.StartupPath;
                        //string strConstantRemarks = "Please contact your system administrator immediately.";

                        FtpClient ftpClient = new FtpClient();
                        ftpClient.Host = strServer;
                        ftpClient.Port = intPort;
                        ftpClient.Credentials = new NetworkCredential(strUserName, strPassword);

                        IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size);


                        Event clsEvent = new Event();

                        bool bolIsFileExist = false;
                        clsEvent.AddEventLn("Checking file if already exist...", true);
                        try
                        {
                            foreach (FtpListItem ftpListItem in lstFtpListItem)
                            {
                                if (ftpListItem.Name.ToUpper() == Path.GetFileName(strNewFileName).ToUpper())
                                { bolIsFileExist = true; break; }
                            }
                        }
                        catch (Exception excheck)
                        {
                            clsEvent.AddEventLn("checking error..." + excheck.Message, true);
                        }

                        if (bolIsFileExist)
                        {
                            clsEvent.AddEventLn("exist...", true);
                            clsEvent.AddEventLn("uploading now...", true);
                        }
                        else
                        {
                            clsEvent.AddEventLn("does not exist...", true);
                            clsEvent.AddEventLn("uploading now...", true);
                        }

                        using (var fileStream = File.OpenRead(filePath))
                        using (var ftpStream = ftpClient.OpenWrite(string.Format("{0}/{1}", strFTPDirectory, Path.GetFileName(strNewFileName))))
                        {
                            var buffer = new byte[8 * 1024];
                            int count;
                            while ((count = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ftpStream.Write(buffer, 0, count);
                            }
                            clsEvent.AddEventLn("Upload Complete: TotalBytes: " + buffer.ToString(), true);
                        }

                        ftpClient.Disconnect();
                        ftpClient.Dispose();
                        ftpClient = null;
                    }
                    catch { }

                    LoadData();

                    MessageBox.Show("The attachment: " + fileName + " has been uploaded.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteAttachment()
        {
            if (!mboDeleteTransactionItemAttachment)
            {
                MessageBox.Show("Sorry you are not allowed to delete posted attachments, please consult your system administrator.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                if (dgvItems.SelectedRows.Count == 1)
                {
                    if (MessageBox.Show("Are you sure you want to delete the attachment.", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                        {
                            string strOrigFileName = dr.Cells["OrigFileName"].Value.ToString();
                            Int64 iTransactionItemAttachmentsID = Int64.Parse(dr.Cells["TransactionItemAttachmentsID"].Value.ToString());

                            Data.SalesTransactionItemAttachments clsSalesTransactionItemAttachments = new Data.SalesTransactionItemAttachments();
                            clsSalesTransactionItemAttachments.Delete(iTransactionItemAttachmentsID.ToString());
                            clsSalesTransactionItemAttachments.CommitAndDispose();

                            LoadData();

                            MessageBox.Show("The attachment: " + strOrigFileName +" has been deleted.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void OpenAttachment()
        {
            try
            {
                string strTempPath = Application.StartupPath + "/temp/";
                string strFile = "";

                if (dgvItems.SelectedRows.Count == 1)
                {
                    if (!Directory.Exists(strTempPath)) Directory.CreateDirectory(strTempPath);

                    foreach (DataGridViewRow dr in dgvItems.SelectedRows)
                    {
                        strFile = strTempPath + dr.Cells["FileName"].Value.ToString();

                        if (System.IO.File.Exists(strFile))
                        try { System.IO.File.Delete(strFile); }
                        catch { }

                        #region Download

                        try
                        {
                            string strServer = "127.0.0.1";
                            if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"] != null)
                            {
                                try { strServer = System.Configuration.ConfigurationManager.AppSettings["VersionFTPIPAddress"].ToString(); }
                                catch { }
                            }

                            int intPort = 21;
                            if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
                            {
                                try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                                catch { }
                            }

                            string strUserName = "ftprbsuser";
                            string strPassword = "ftprbspwd";
                            string strFTPDirectory = "attachment";

                            FtpClient ftpClient = new FtpClient();
                            ftpClient.Host = strServer;
                            ftpClient.Port = intPort;
                            ftpClient.Credentials = new NetworkCredential(strUserName, strPassword);

                            //IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size)
                            //        .Where(ftpListItem => string.Equals(Path.GetExtension(ftpListItem.Name), ".dll"));

                            IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(strFTPDirectory, FtpListOption.Modify | FtpListOption.Size);

                            Int32 iCount = lstFtpListItem.Count();
                            Int32 iCtr = 1;

                            // List all files with a .txt extension
                            foreach (FtpListItem ftpListItem in lstFtpListItem)
                            {
                                if (ftpListItem.Name.ToLower() == dr.Cells["FileName"].Value.ToString().ToLower())
                                {
                                    // Report progress as a percentage of the total task. 
                                    decimal x = ((decimal.Parse(iCtr.ToString()) / decimal.Parse(iCount.ToString()) * decimal.Parse("100")) - decimal.Parse("1"));
                                    iCtr++;

                                    strFile = string.Format(@"{0}{1}", strTempPath, ftpListItem.Name);

                                    using (var ftpStream = ftpClient.OpenRead(ftpListItem.FullName))
                                    using (var fileStream = File.Create(strFile, (int)ftpStream.Length))
                                    {
                                        var buffer = new byte[8 * 1024];
                                        int count;
                                        while ((count = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            fileStream.Write(buffer, 0, count);
                                        }
                                    }

                                    //System.Diagnostics.Process.Start(strFile);

                                    try
                                    { Common.OpenFileToDOS(strFile); }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error opening file. You may directly open the file @: " + strFile + Environment.NewLine + "Error details: " + ex.InnerException.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error opening file, the system cannot connect to FTP server. Please consult your System Administrator." + Environment.NewLine + "Error details: " + ex.InnerException.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


		#endregion

	}
}
