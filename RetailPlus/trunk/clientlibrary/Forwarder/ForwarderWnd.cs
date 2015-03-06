using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System.Net;
using System.Net.FtpClient;
using System.Xml;
using System.Diagnostics;

namespace AceSoft.RetailPlus.Client.UI
{
	/// <summary>
	/// Summary description for ForwarderWnd.
	/// </summary>
	public class ForwarderWnd : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblCash;
		private System.Windows.Forms.TextBox txtDateFrom;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDate;
        private System.ComponentModel.Container components = null;

		private DialogResult dialog;
		private System.Windows.Forms.PictureBox imgIcon;
        private Label label3;
        private TextBox txtDateTo;
        private Button cmdGO;
        private TextBox txtSelectedTexBox;
        private Button cmdCancel;
        private Button cmdEnter;
        private GroupBox grpRLC;
        private ListView lstItems;
        private ColumnHeader Filename;
        private ColumnHeader FileSize;
        private ColumnHeader DateModified;
		private DateTime mdtDateLastInitialized;

		public DialogResult Result
		{
			get 
			{
				return dialog;
			}
		}
        public DateTime DateLastInitialized
		{
			get
			{
				return mdtDateLastInitialized;
			}
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        #region Constructors and Destructors

        public ForwarderWnd()
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
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdGO = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDateTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDate = new System.Windows.Forms.ComboBox();
            this.lblCash = new System.Windows.Forms.Label();
            this.txtDateFrom = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.grpRLC = new System.Windows.Forms.GroupBox();
            this.lstItems = new System.Windows.Forms.ListView();
            this.Filename = new System.Windows.Forms.ColumnHeader();
            this.FileSize = new System.Windows.Forms.ColumnHeader();
            this.DateModified = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpRLC.SuspendLayout();
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
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "Mall Forwarder Viewer";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label8.Location = new System.Drawing.Point(584, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(205, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Press Send button to resume Transaction";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(35, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "Send";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cmdGO);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDateTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDate);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.lblCash);
            this.groupBox1.Controls.Add(this.txtDateFrom);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1007, 230);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Press Send button to resume Transaction";
            // 
            // cmdGO
            // 
            this.cmdGO.AutoSize = true;
            this.cmdGO.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdGO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdGO.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGO.ForeColor = System.Drawing.Color.White;
            this.cmdGO.Location = new System.Drawing.Point(499, 51);
            this.cmdGO.Name = "cmdGO";
            this.cmdGO.Size = new System.Drawing.Size(78, 62);
            this.cmdGO.TabIndex = 2;
            this.cmdGO.Text = "SEARCH";
            this.cmdGO.UseVisualStyleBackColor = true;
            this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumBlue;
            this.label3.Location = new System.Drawing.Point(336, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "--";
            // 
            // txtDateTo
            // 
            this.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDateTo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateTo.Location = new System.Drawing.Point(363, 67);
            this.txtDateTo.MaxLength = 10;
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Size = new System.Drawing.Size(134, 30);
            this.txtDateTo.TabIndex = 1;
            this.txtDateTo.Text = "yyyy-mm-dd";
            this.txtDateTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDateTo.GotFocus += new System.EventHandler(this.txtDateTo_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(267, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Select date of report to forward.";
            // 
            // cboDate
            // 
            this.cboDate.CausesValidation = false;
            this.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDate.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDate.Location = new System.Drawing.Point(162, 149);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(415, 31);
            this.cboDate.TabIndex = 3;
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblCash.Location = new System.Drawing.Point(156, 46);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(337, 13);
            this.lblCash.TabIndex = 15;
            this.lblCash.Text = "Select covered dates of terminal report to forward to mall.";
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDateFrom.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateFrom.Location = new System.Drawing.Point(192, 67);
            this.txtDateFrom.MaxLength = 10;
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Size = new System.Drawing.Size(134, 30);
            this.txtDateFrom.TabIndex = 0;
            this.txtDateFrom.Text = "yyyy-mm-dd";
            this.txtDateFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDateFrom.GotFocus += new System.EventHandler(this.txtDateFrom_GotFocus);
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(772, 306);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 83);
            this.cmdCancel.TabIndex = 5;
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
            this.cmdEnter.Location = new System.Drawing.Point(878, 306);
            this.cmdEnter.Name = "cmdEnter";
            this.cmdEnter.Size = new System.Drawing.Size(106, 83);
            this.cmdEnter.TabIndex = 4;
            this.cmdEnter.Text = "Send";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // grpRLC
            // 
            this.grpRLC.BackColor = System.Drawing.Color.White;
            this.grpRLC.Controls.Add(this.lstItems);
            this.grpRLC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRLC.ForeColor = System.Drawing.Color.Blue;
            this.grpRLC.Location = new System.Drawing.Point(9, 395);
            this.grpRLC.Name = "grpRLC";
            this.grpRLC.Size = new System.Drawing.Size(1007, 221);
            this.grpRLC.TabIndex = 83;
            this.grpRLC.TabStop = false;
            this.grpRLC.Text = "RLC File Server Management: [DOUBLE CLICK TO RELOAD] :";
            this.grpRLC.Visible = false;
            this.grpRLC.DoubleClick += new System.EventHandler(this.grpRLC_DoubleClick);
            // 
            // lstItems
            // 
            this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.FileSize,
            this.DateModified});
            this.lstItems.FullRowSelect = true;
            this.lstItems.Location = new System.Drawing.Point(6, 20);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(995, 195);
            this.lstItems.TabIndex = 95;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.DoubleClick += new System.EventHandler(this.lstItems_DoubleClick);
            // 
            // Filename
            // 
            this.Filename.Text = "Filename";
            this.Filename.Width = 440;
            // 
            // FileSize
            // 
            this.FileSize.Text = "Size";
            this.FileSize.Width = 111;
            // 
            // DateModified
            // 
            this.DateModified.Text = "Date Modified";
            this.DateModified.Width = 117;
            // 
            // ForwarderWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.grpRLC);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "ForwarderWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ForwarderWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ForwarderWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpRLC.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Windows Form Methods

        private void ForwarderWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Withhold.jpg");	}
			catch{}
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }
            try
            { this.cmdGO.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg"); }
            catch { }

            txtDateFrom.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            LoadOptions();
			
		}
        
		private void ForwarderWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                    if (cboDate.SelectedText == "No valid date") 
                    {
                        MessageBox.Show("Sorry, no record is found on your searched date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
					try
					{
                        mdtDateLastInitialized = Convert.ToDateTime(cboDate.SelectedValue.ToString());
					}
					catch
					{
						MessageBox.Show("Sorry, the date you selected is not a valid date." +
							"Please check the date you select.","RetailPlus",MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
                    dialog = DialogResult.OK;
                    this.Hide();
                    break;
			}
        }

        private void grpRLC_DoubleClick(object sender, EventArgs e)
        {
            grpRLC.Text = "RLC File Server Management: Reloading files...";

            ShowRLCServerFileViewer();
        }

        private void lstItems_DoubleClick(object sender, EventArgs e)
        {
            grpRLC.Text = "RLC File Server Management: Reloading files...";

            ShowRLCServerFileViewer();
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Windows Control Methods

        private void txtDateFrom_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }
        
        private void txtDateTo_GotFocus(object sender, EventArgs e)
        {
            txtSelectedTexBox = (TextBox)sender;
        }
        
        private void cmdGO_Click(object sender, EventArgs e)
        {
            try
            {
                txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text).ToString("yyyy-MM-dd");
            }
            catch
            {
                MessageBox.Show("Sorry the date you entered is not valid. Please enter a valid date.", "Date From Invalid Date", MessageBoxButtons.OK);
                txtDateFrom.Select();
                return;
            }
            try
            {
                txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text).ToString("yyyy-MM-dd");
            }
            catch
            {
                MessageBox.Show("Sorry the date you entered is not valid. Please enter a valid date.", "Date To Invalid Date", MessageBoxButtons.OK);
                txtDateTo.Select();
                return;
            }
            LoadOptions();
        }
        
        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            if (txtSelectedTexBox == null)
                txtDateFrom.Focus();
            else if (txtSelectedTexBox.Name == txtDateFrom.Name)
                txtDateFrom.Focus();
            else if (txtSelectedTexBox.Name == txtDateTo.Name)
                txtDateTo.Focus();

            SendKeys.Send(e.KeyboardKeyPressed);
        }
        
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        
        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (cboDate.SelectedText == "No valid date")
            {
                MessageBox.Show("Sorry, no record is found on your searched date.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                mdtDateLastInitialized = Convert.ToDateTime(cboDate.SelectedValue.ToString());
            }
            catch
            {
                MessageBox.Show("Sorry, the date you selected is not a valid date." +
                    "Please check the date you select.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dialog = DialogResult.OK;
            this.Hide();
        }

        #endregion

        #region Private Methods

        private void LoadOptions()
        {
            Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory();
            System.Data.DataTable dt = clsTerminalReportHistory.DatesLastInitializedForRLC(TerminalDetails.BranchID, TerminalDetails.TerminalNo, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text));
            clsTerminalReportHistory.CommitAndDispose();

            cboDate.DataSource = null;
            cboDate.DisplayMember = "DateLastInitializedDisplay";
            cboDate.ValueMember = "DateLastInitialized";
            cboDate.DataSource = dt.DefaultView;

            if (cboDate.Items.Count <= 0)
            {
                cboDate.DataSource = null;
                cboDate.Items.Clear();
                cboDate.Items.Add("No valid date");
            }

            cboDate.SelectedIndex = 0;
            if (CONFIG.MallCode.ToUpper() == MallCodes.RLC)
                grpRLC.Visible = true;
        }

        private void ShowRLCServerFileViewer()
        {
            ListViewItem lvi;
            ListViewItem.ListViewSubItem lvsi;
            lstItems.Items.Clear();

            int intPort = 21;
            if (System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"] != null)
            {
                try { intPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["VersionFTPPort"]); }
                catch { }
            }

            FtpClient ftpClient = new FtpClient();
            ftpClient.Host = CONFIG.FTPIPAddress;
            ftpClient.Port = intPort;
            ftpClient.Credentials = new NetworkCredential(CONFIG.FTPUsername, CONFIG.FTPPassword);

            IEnumerable<FtpListItem> lstFtpListItem = ftpClient.GetListing(CONFIG.FTPDirectory, FtpListOption.Modify | FtpListOption.Size);
            
            grpRLC.Text = "RLC File Server Management: [DOUBLE CLICK TO RELOAD] : " + CONFIG.FTPDirectory;

            //Int32 iCount = lstFtpListItem.Count();
            //Int32 iCtr = 1;
            try
            {
                foreach (FtpListItem ftpListItem in lstFtpListItem)
                {
                    lvi = new ListViewItem();
                    lvi.Text = ftpListItem.Name;
                    //lvi.ImageIndex = 0;
                    lvi.Tag = ftpListItem.FullName;

                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = ftpListItem.Size.ToString() + " kb";
                    lvi.SubItems.Add(lvsi);

                    lvsi = new ListViewItem.ListViewSubItem();
                    lvsi.Text = ftpListItem.Created.ToString("MM/dd/yyyy hh:mm tt");
                    lvi.SubItems.Add(lvsi);

                    lstItems.Items.Add(lvi);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error encountered while loading file list. " + Environment.NewLine + "Err #: " + ex.Message, "RetailPlus", MessageBoxButtons.OK);
            }

            ftpClient.Disconnect();
            ftpClient.Dispose();
            ftpClient = null;
        }
        
        #endregion

    }
}