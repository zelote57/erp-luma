using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

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
		private Button cmdTableLeft;

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

		#region Constructors and Destructors

		public TableSelectWnd()
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
			// TableSelectWnd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1026, 788);
			this.ControlBox = false;
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
				long lngSequenceNoStart = 0;

				if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
					try { lngSequenceNoStart = long.Parse(cmdTableLeft.Tag.ToString()); }
					catch { }
				else
					try { lngSequenceNoStart = long.Parse(cmdTableRight.Tag.ToString()); }
					catch { }

				ContactColumns clsContactColumns = new ContactColumns();
				clsContactColumns.ContactCode = true;
                clsContactColumns.LastCheckInDate = true;

				ContactColumns clsSearchColumns = new ContactColumns();

				Contacts clsContact = new Contacts();
				System.Data.DataTable dtContact = clsContact.Customers(clsContactColumns, lngSequenceNoStart, SequenceSortOrder, clsSearchColumns, string.Empty, 0, false, string.Empty, SequenceSortOrder);

				int iRow = 0;
				int iCol = 0;
				int iCtr = 1;
                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(clsContact.Connection, clsContact.Transaction);
				Data.SalesTransactionDetails clsSalesTransactionDetails = new Data.SalesTransactionDetails();

				if (dtContact.Rows.Count == 0)
				{
					cmdTableLeft.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
					cmdTableRight.Tag = "0".ToString(); // reset the sequenceno to 0 if no record
				}

				foreach (System.Data.DataRow dr in dtContact.Rows)
				{
					if (iCol == 5) { iCol = 0; iRow++; }

					#region Sequence # Counter
					if (iCtr > Constants.C_RESTOPLUS_MAX_TABLES) break;

					if (iCtr == Constants.C_RESTOPLUS_MAX_TABLES && dtContact.Rows.Count > Constants.C_RESTOPLUS_MAX_TABLES)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
							try { if (iCtr == 1) cmdTableLeft.Tag = dr[Data.ContactColumnNames.ContactID].ToString(); }
							catch { }
						else
							try { cmdTableRight.Tag = dr[Data.ContactColumnNames.ContactID].ToString(); }
							catch { }
					}
					else if (dtContact.Rows.Count > 0 && dtContact.Rows.Count <= Constants.C_RESTOPLUS_MAX_TABLES)
					{
						if (SequenceSortOrder == System.Data.SqlClient.SortOrder.Descending)
						{
							try { if (iCtr == 1) cmdTableRight.Tag = dr[Data.ContactColumnNames.ContactID].ToString(); }
							catch { }
							cmdTableLeft.Tag = "0".ToString();
						}
						else
						{
							try { if (iCtr == 1) cmdTableLeft.Tag = dr[Data.ContactColumnNames.ContactID].ToString(); }
							catch { }
							// cmdTableRight.Tag = cmdTableRight.Tag; // do not reset
						}
					}
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

                        if (mboShowAvailableTableOnly)
						{
							cmdTable.BackColor = System.Drawing.Color.DarkGray;
							cmdTable.GradientBottom = System.Drawing.Color.DarkGray;
							cmdTable.GradientTop = System.Drawing.Color.LightGray;
							cmdTable.Enabled = false;
						}
						else
						{
							cmdTable.BackColor = System.Drawing.Color.DarkBlue;
							cmdTable.GradientBottom = System.Drawing.Color.DarkBlue;
							cmdTable.GradientTop = System.Drawing.Color.LightBlue;
						}
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
				clsContact.CommitAndDispose();

				dialog = DialogResult.OK;
				this.Hide();
			}
			catch { }
		}

		#endregion

	}
}
