using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
	public class TableSelectWnd : System.Windows.Forms.Form
	{
		private Label label1;
		private PictureBox imgIcon;
		private System.ComponentModel.Container components = null;

		private DialogResult dialog;
		private Data.ContactDetails mDetails;
		private bool mboShowAvailableTableOnly;
		private Data.TerminalDetails mclsTerminalDetails;
		private GroupBox grpItems;
		private TableLayoutPanel tblLayout;
		private Button cmdTableRight;
		private Button cmdCancel;
        private Button cmdMergeTable;
		private Button cmdTableLeft;

        public bool isMergeTable { get; set; }
		public Data.TerminalDetails TerminalDetails
		{
			set { mclsTerminalDetails = value; }
		}
		public bool ShowAvailableTableOnly
		{
			set {	mboShowAvailableTableOnly = value;	}
		}
		public DialogResult Result
		{
			get {	return dialog;	}
		}
		public ContactDetails Details
		{
			get {	return mDetails;	}
		}

        public Int64 CashierID { get; set; }

        public Data.ContactGroupCategory ContactGroupCategory { get; set; }

		#region Constructors and Destructors

		public TableSelectWnd()
		{
			InitializeComponent();

            if (mclsTerminalDetails.MultiInstanceEnabled)
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = mclsTerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
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
            this.label1 = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cmdTableRight = new System.Windows.Forms.Button();
            this.cmdTableLeft = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdMergeTable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "<- Press the image to cancel selecting table.";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 0;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.White;
            this.grpItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.grpItems.Controls.Add(this.tblLayout);
            this.grpItems.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.grpItems.ForeColor = System.Drawing.Color.Blue;
            this.grpItems.Location = new System.Drawing.Point(9, 67);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(1007, 545);
            this.grpItems.TabIndex = 53;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Select TABLE to dine.";
            // 
            // tblLayout
            // 
            this.tblLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLayout.BackColor = System.Drawing.Color.Transparent;
            this.tblLayout.CausesValidation = false;
            this.tblLayout.ColumnCount = 4;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tblLayout.Location = new System.Drawing.Point(9, 20);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 5;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblLayout.Size = new System.Drawing.Size(992, 519);
            this.tblLayout.TabIndex = 101;
            // 
            // cmdTableRight
            // 
            this.cmdTableRight.BackColor = System.Drawing.Color.Gold;
            this.cmdTableRight.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdTableRight.FlatAppearance.BorderSize = 0;
            this.cmdTableRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdTableRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdTableRight.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTableRight.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTableRight.Location = new System.Drawing.Point(964, 26);
            this.cmdTableRight.Name = "cmdTableRight";
            this.cmdTableRight.Size = new System.Drawing.Size(50, 28);
            this.cmdTableRight.TabIndex = 125;
            this.cmdTableRight.Text = ">";
            this.cmdTableRight.UseVisualStyleBackColor = false;
            this.cmdTableRight.Click += new System.EventHandler(this.cmdTableRight_Click);
            // 
            // cmdTableLeft
            // 
            this.cmdTableLeft.BackColor = System.Drawing.Color.Gold;
            this.cmdTableLeft.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdTableLeft.FlatAppearance.BorderSize = 0;
            this.cmdTableLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdTableLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdTableLeft.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTableLeft.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTableLeft.Location = new System.Drawing.Point(908, 26);
            this.cmdTableLeft.Name = "cmdTableLeft";
            this.cmdTableLeft.Size = new System.Drawing.Size(50, 28);
            this.cmdTableLeft.TabIndex = 126;
            this.cmdTableLeft.Text = "<";
            this.cmdTableLeft.UseVisualStyleBackColor = false;
            this.cmdTableLeft.Click += new System.EventHandler(this.cmdTableLeft_Click);
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
            this.cmdCancel.TabIndex = 127;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdMergeTable
            // 
            this.cmdMergeTable.BackColor = System.Drawing.Color.Gold;
            this.cmdMergeTable.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdMergeTable.FlatAppearance.BorderSize = 0;
            this.cmdMergeTable.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.cmdMergeTable.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.cmdMergeTable.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMergeTable.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdMergeTable.Location = new System.Drawing.Point(690, 26);
            this.cmdMergeTable.Name = "cmdMergeTable";
            this.cmdMergeTable.Size = new System.Drawing.Size(164, 28);
            this.cmdMergeTable.TabIndex = 128;
            this.cmdMergeTable.Text = "Merge Table";
            this.cmdMergeTable.UseVisualStyleBackColor = false;
            this.cmdMergeTable.Click += new System.EventHandler(this.cmdMergeTable_Click);
            // 
            // TableSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1026, 764);
            this.ControlBox = false;
            this.Controls.Add(this.cmdMergeTable);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdTableLeft);
            this.Controls.Add(this.cmdTableRight);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "TableSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TableSelectWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TableSelectWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpItems.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Windows Form Methods

		private void TableSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            //System.Data.DataTable dt;
            //int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;
			}
		}

		private void TableSelectWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ContactSelect.jpg");	}
			catch{}
			try
			{ this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
			catch { }
            
			LoadOptions();
			LoadContactData(System.Data.SqlClient.SortOrder.Ascending);
		}

		#endregion

        #region Windows Control Methods

		private void imgIcon_Click(object sender, EventArgs e)
		{
			dialog = DialogResult.Cancel;
			this.Hide();
		}

        private void cmdTableLeft_Click(object sender, EventArgs e)
        {
            LoadContactData(System.Data.SqlClient.SortOrder.Descending);
        }

        private void cmdTableRight_Click(object sender, EventArgs e)
        {
            LoadContactData(System.Data.SqlClient.SortOrder.Ascending);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

		#endregion

		#region Private Methods

		private void LoadOptions()
		{

		}
		private void LoadContactData(System.Data.SqlClient.SortOrder SequenceSortOrder)
		{
			try
			{
				tblLayout.Controls.Clear();

				Int64 intSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { intSequenceNoStart = long.Parse(cmdTableLeft.Tag.ToString()); }
					catch { }
				else
					try { intSequenceNoStart = long.Parse(cmdTableRight.Tag.ToString()); }
					catch { }

                // Sep 24, 2014 put an override if cmdSubGroupLeft.Tag = 0
                // always do an asceding coz its already the end.
                if (intSequenceNoStart < Constants.C_RESTOPLUS_MAX_TABLES) intSequenceNoStart = 0; //reset to 0 if it's 1
                if (intSequenceNoStart == 0) SequenceSortOrder = System.Data.SqlClient.SortOrder.Ascending;

				ContactColumns clsContactColumns = new ContactColumns();
				clsContactColumns.ContactCode = true;
                clsContactColumns.LastCheckInDate = true;

				ContactColumns clsSearchColumns = new ContactColumns();

				Contacts clsContact = new Contacts();

                System.Data.DataTable dtContact;

                if (ContactGroupCategory == Data.ContactGroupCategory.TABLES)
                    dtContact = clsContact.Tables(clsContactColumns, intSequenceNoStart, SequenceSortOrder, clsSearchColumns, string.Empty, SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending ? Constants.C_RESTOPLUS_MAX_TABLES : Constants.C_RESTOPLUS_MAX_TABLES + 1, false, "SequenceNo", SequenceSortOrder);
                else
                    dtContact = clsContact.Customers(clsContactColumns, intSequenceNoStart, SequenceSortOrder, clsSearchColumns, string.Empty, SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending ? Constants.C_RESTOPLUS_MAX_TABLES : Constants.C_RESTOPLUS_MAX_TABLES + 1, false, "SequenceNo", SequenceSortOrder);

                // re-order the products by sequence no
                if (dtContact.Rows.Count > 0)
                {
                    System.Data.DataView dv = dtContact.DefaultView;
                    dv.Sort = "SequenceNo";
                    dtContact = dv.ToTable();
                }

				int iRow = 0;
				int iCol = 0;
				int iCtr = 1;
                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsContact.Connection, clsContact.Transaction);
				Data.SalesTransactionDetails clsSalesTransactionDetails = new Data.SalesTransactionDetails();

                Data.MergeTable clsMergeTable = new Data.MergeTable(clsContact.Connection, clsContact.Transaction);
                Data.MergeTableDetails clsMergeTableDetails = new Data.MergeTableDetails();

				if (dtContact.Rows.Count == 0)
				{
					cmdTableLeft.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
					cmdTableRight.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
				}

				foreach (System.Data.DataRow dr in dtContact.Rows)
				{
                    //if (iCol == 5) { iCol = 0; iRow++; }

					#region Sequence # Counter
                    if (iCtr > Constants.C_RESTOPLUS_MAX_TABLES) break;

                    if (iCtr == 1) cmdTableLeft.Tag = dr[Data.ContactColumnNames.SequenceNo].ToString();
                    if (iCtr >= 1 && dtContact.Rows.Count > Constants.C_RESTOPLUS_MAX_TABLES) cmdTableRight.Tag = dr[Data.ContactColumnNames.SequenceNo].ToString();

                    #endregion

					ProductButton cmdTable = new ProductButton();

					cmdTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
					cmdTable.BackColor = System.Drawing.Color.Red;
					cmdTable.Dock = System.Windows.Forms.DockStyle.Fill;
					cmdTable.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
					cmdTable.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
					cmdTable.ForeColor = System.Drawing.SystemColors.ControlText;
					cmdTable.GradientBottom = System.Drawing.Color.DarkRed;
					cmdTable.GradientTop = System.Drawing.Color.Red;
					cmdTable.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
					cmdTable.Location = new System.Drawing.Point(3, 3);
					cmdTable.Size = new System.Drawing.Size(245, 90);
					cmdTable.TabIndex = 118;
					cmdTable.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					cmdTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
					cmdTable.UseVisualStyleBackColor = false;

					cmdTable.Name = "cmdTable" + iCtr.ToString();
                    cmdTable.Text = dr[Data.ContactColumnNames.ContactCode].ToString();
                    cmdTable.Tag = dr[Data.ContactColumnNames.ContactID].ToString();
                    cmdTable.Click += new System.EventHandler(cmdTable_Click);

                    if (DateTime.Parse(dr[Data.ContactColumnNames.LastCheckInDate].ToString()) != Constants.C_DATE_MIN_VALUE)
                    {
                        TimeSpan iLapse = DateTime.Now - DateTime.Parse(dr[Data.ContactColumnNames.LastCheckInDate].ToString());

                        Label lblLastCheckInDate = new System.Windows.Forms.Label();
                        lblLastCheckInDate.AutoSize = true;
                        lblLastCheckInDate.BackColor = System.Drawing.Color.Transparent;
                        lblLastCheckInDate.Font = new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblLastCheckInDate.ForeColor = System.Drawing.Color.Blue;
                        lblLastCheckInDate.Location = new System.Drawing.Point(5, 5);
                        lblLastCheckInDate.Name = "lblLastCheckInDate" + iCtr.ToString();
                        lblLastCheckInDate.TabIndex = 1;
                        lblLastCheckInDate.Text = "";
                        lblLastCheckInDate.Text = "CheckIn: " + DateTime.Parse(dr[Data.ContactColumnNames.LastCheckInDate].ToString()).ToString("dd-MMM hh:mm tt") + "   [" + iLapse.Hours.ToString("0#") + "hrs " + iLapse.Minutes.ToString("0#") + "mins]";
                        cmdTable.Controls.Add(lblLastCheckInDate);
                    }

                    string stTransactionNo = clsSalesTransactions.getSuspendedTransactionNo(long.Parse(dr[Data.ContactColumnNames.ContactID].ToString()), mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);
					if (stTransactionNo != string.Empty)
					{
						clsSalesTransactionDetails = clsSalesTransactions.Details(stTransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);
                        cmdTable.Text = dr[Data.ContactColumnNames.ContactCode].ToString();

						decimal decAmountDue = Convert.ToDecimal(clsSalesTransactionDetails.SubTotal + clsSalesTransactionDetails.Charge - clsSalesTransactionDetails.Discount);
						cmdTable.Text += Environment.NewLine + Environment.NewLine + "Amount Due:" + decAmountDue.ToString("#,###.#0");

                        Label lblNoOfPax = new System.Windows.Forms.Label();
                        lblNoOfPax.AutoSize = true;
                        lblNoOfPax.BackColor = System.Drawing.Color.Transparent;
                        lblNoOfPax.Font = new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lblNoOfPax.ForeColor = System.Drawing.Color.White;
                        lblNoOfPax.Location = new System.Drawing.Point(210, 75);
                        lblNoOfPax.Name = "lblNoOfPax" + iCtr.ToString();
                        lblNoOfPax.TabIndex = 1;
                        lblNoOfPax.Text = "";
                        lblNoOfPax.Text = clsSalesTransactionDetails.PaxNo.ToString() + "Pax";
                        cmdTable.Controls.Add(lblNoOfPax);

                        // Jan 31, 2015 : Lemu
                        // Added disabling of Suspended Transactions. 
                        // Put the SuspendedOpen Status to in LoadTransaction
                        if (mboShowAvailableTableOnly || clsSalesTransactionDetails.TransactionStatus == TransactionStatus.SuspendedOpen)
						{
							cmdTable.BackColor = System.Drawing.Color.DarkGray;
                            cmdTable.GradientBottom = System.Drawing.Color.DarkGray;
                            cmdTable.GradientTop = System.Drawing.Color.DarkGray;
							cmdTable.Enabled = false;

                            if (clsSalesTransactionDetails.TransactionStatus == TransactionStatus.SuspendedOpen)
                            {
                                cmdTable.BackColor = System.Drawing.Color.Gray;
                                cmdTable.GradientBottom = System.Drawing.Color.Gray;
                                cmdTable.GradientTop = System.Drawing.Color.Gray;
                                cmdTable.Enabled = true;
                                cmdTable.Text += Environment.NewLine + "(open in other terminal)";
                            }
						}
						else
						{
							cmdTable.BackColor = System.Drawing.Color.DarkBlue;
							cmdTable.GradientBottom = System.Drawing.Color.DarkBlue;
							cmdTable.GradientTop = System.Drawing.Color.LightBlue;
						}
					}

                    Label lblMerge = new System.Windows.Forms.Label();
                    lblMerge.AutoSize = true;
                    lblMerge.BackColor = System.Drawing.Color.Transparent;
                    lblMerge.Font = new System.Drawing.Font("Tahoma", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblMerge.ForeColor = System.Drawing.Color.White;
                    lblMerge.Location = new System.Drawing.Point(5, 75);
                    lblMerge.Name = "lblMerge" + iCtr.ToString();
                    lblMerge.TabIndex = 1;
                    lblMerge.Text = "Merged";
                    lblMerge.Visible = false;
                    cmdTable.Controls.Add(lblMerge);

                    clsMergeTableDetails = clsMergeTable.Details(dr[Data.ContactColumnNames.ContactCode].ToString());
                    if (clsMergeTableDetails.ChildTableCode == dr[Data.ContactColumnNames.ContactCode].ToString() &&
                        clsMergeTableDetails.MainTableCode != dr[Data.ContactColumnNames.ContactCode].ToString())
                    {
                        cmdTable.BackColor = System.Drawing.Color.DarkGray;
                        cmdTable.GradientBottom = System.Drawing.Color.DarkGray;
                        cmdTable.GradientTop = System.Drawing.Color.LightGray;
                        cmdTable.Enabled = false;
                        cmdTable.Controls["lblMerge" + cmdTable.Name.Replace("cmdTable", "")].Visible = true;
                        cmdTable.Controls["lblMerge" + cmdTable.Name.Replace("cmdTable", "")].Text = "Merged to " + clsMergeTableDetails.MainTableCode;
                    }

					tblLayout.Controls.Add(cmdTable, iCol, iRow);
                    
					iCol++; iCtr++;
				}
				clsContact.CommitAndDispose();
			}
			catch (IndexOutOfRangeException){}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}

		private void cmdTable_Click(object sender, EventArgs e)
		{
			try
			{
				ProductButton cmdTable = (ProductButton)sender;

				Data.Contacts clsContact = new Contacts();
				mDetails = clsContact.Details(long.Parse(cmdTable.Tag.ToString()));

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsContact.Connection, clsContact.Transaction);
                string stTransactionNo = clsSalesTransactions.getSuspendedTransactionNo(mDetails.ContactID, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);
                Data.SalesTransactionDetails clsSalesTransactionDetails = new SalesTransactionDetails();
                if (!string.IsNullOrEmpty(stTransactionNo))
                {
                    clsSalesTransactionDetails = clsSalesTransactions.Details(stTransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);
                }
                clsContact.CommitAndDispose();

                if (!string.IsNullOrEmpty(stTransactionNo) && clsSalesTransactionDetails.TransactionStatus == TransactionStatus.SuspendedOpen)
                {
                    if (MessageBox.Show("This transaction is already open in another terminal. Please suspend in the other terminal first before opening." + Environment.NewLine + "Would you like to force open this transaction?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        //LoadContactData(System.Data.SqlClient.SortOrder.Ascending);
                        return;
                    }
                    else
                    {
                        DialogResult resResumeSuspendedOpenTransaction = GetWriteAccessAndLogin(CashierID, AccessTypes.ResumeSuspendedOpenTransaction);

                        if (resResumeSuspendedOpenTransaction != System.Windows.Forms.DialogResult.OK)
                        {
                            //LoadContactData(System.Data.SqlClient.SortOrder.Ascending);
                            return;
                        }
                    }
                }
				dialog = DialogResult.OK;
				this.Hide();
			}
			catch { }
		}

		#endregion

        private void cmdMergeTable_Click(object sender, EventArgs e)
        {
            if (!isMergeTable)
            {
                isMergeTable = true;
                grpItems.Text = "Select the main table to merge.";
                cmdMergeTable.Text = "Select Table";
            }
            else {
                isMergeTable = false;
                grpItems.Text = "Select table to dine.";
                cmdMergeTable.Text = "Merge Table";
            }
        }

        public DialogResult GetWriteAccessAndLogin(Int64 UID, AccessTypes AccessType, string OverridingHeader = "")
        {
            DialogResult loginresult = GetWriteAccess(CashierID, AccessType);

            if (loginresult == DialogResult.None)
            {
                string strHeader = OverridingHeader;

                if (string.IsNullOrEmpty(strHeader))
                {
                    switch (AccessType)
                    {
                        case AccessTypes.PrintTransactionHeader: strHeader = "Print Transaction Header"; break;
                        case AccessTypes.ChangeQuantity: strHeader = "Change Quantity"; break;
                        case AccessTypes.ChangePrice: strHeader = "Change Price"; break;
                        case AccessTypes.ReturnItem: strHeader = "Return Item Access"; break;
                        case AccessTypes.ApplyItemDiscount: strHeader = "Apply Item Discount"; break;
                        case AccessTypes.Contacts: strHeader = "Update customer information"; break;
                        case AccessTypes.SuspendTransaction: strHeader = "Suspend Transaction No. "; break;
                        case AccessTypes.ResumeTransaction: strHeader = "Resume Suspended Transaction"; break;
                        case AccessTypes.ResumeSuspendedOpenTransaction: strHeader = "Resume Suspended Open Transaction"; break;
                        case AccessTypes.VoidTransaction: strHeader = "Void Transaction No. "; break;
                        case AccessTypes.Withhold: strHeader = "WithHold Amount"; break;
                        case AccessTypes.Disburse: strHeader = "Disburse Amount"; break;
                        case AccessTypes.PaidOut: strHeader = "Paid-Out Amount"; break;
                        case AccessTypes.MallForwarder: strHeader = "Mall Data Forwarder"; break;
                        case AccessTypes.VoidItem: strHeader = "Void Item"; break;
                        case AccessTypes.CashCount: strHeader = "Issue Cash Count"; break;
                        case AccessTypes.EnterFloat: strHeader = "Enter Float or Beginning Balance"; break;
                        case AccessTypes.InitializeZRead: strHeader = "Initialize Z-Read"; break;
                        case AccessTypes.CreateTransaction: strHeader = "Create Transaction"; break;
                        case AccessTypes.CloseTransaction: strHeader = "Close Transaction"; break;
                        case AccessTypes.ReleaseItems: strHeader = "Release Items"; break;
                        case AccessTypes.LogoutFE: strHeader = "Logout"; break;
                        case AccessTypes.ApplyTransDiscount: strHeader = "Apply Transaction Discount"; break;
                        case AccessTypes.ChargeType: strHeader = "Apply Transaction Charge"; break;
                        case AccessTypes.OpenDrawer: strHeader = "Open Drawer"; break;
                        case AccessTypes.CreditPayment: strHeader = "Enter Credit Payment"; break;
                        case AccessTypes.RefundTransaction: strHeader = "Refund Transaction"; break;
                        case AccessTypes.RewardCardIssuance: strHeader = "Issue new Reward Card"; break;
                        case AccessTypes.RewardCardChange: strHeader = "Reward Card Replacement"; break;
                        case AccessTypes.CreditCardIssuance: strHeader = "Issue new Credit Card"; break;
                        case AccessTypes.CreditCardChange: strHeader = "Credit Card Replacement"; break;
                        case AccessTypes.PackUnpackTransaction: strHeader = "Pack/Unpack Transaction Access Validation"; break;
                        case AccessTypes.ReprintTransaction: strHeader = "Reprint Transaction Access Validation"; break;
                        case AccessTypes.PrintZRead: strHeader = "Print ZRead Access Validation"; break;
                        case AccessTypes.PrintXRead: strHeader = "Print XRead Access Validation"; break;
                        case AccessTypes.PrintHourlyReport: strHeader = "Print Hourly Report Access Validation"; break;
                        case AccessTypes.PrintGroupReport: strHeader = "Print Group/Dept. Report Access Validation"; break;
                        case AccessTypes.PrintPLUReport: strHeader = "Print PLU Report Access Validation"; break;
                        case AccessTypes.PrintElectronicJournal: strHeader = "Print EJournal Report Access Validation"; break;
                        default: strHeader = AccessType.ToString(); break;
                    }
                }
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessType;
                login.Header = strHeader;
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();

                if (loginresult != System.Windows.Forms.DialogResult.OK)
                    loginresult = System.Windows.Forms.DialogResult.Cancel;

            }
            return loginresult;
        }

        public DialogResult GetWriteAccess(Int64 UID, AccessTypes accesstype)
        {
            DialogResult resRetValue = DialogResult.None;

            AccessRights clsAccessRights = new AccessRights();
            AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (Int16)accesstype);

            if (clsDetails.Write)
            {
                resRetValue = DialogResult.OK;
            }

            clsAccessRights.CommitAndDispose();

            return resRetValue;
        }
	}
}
