using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ContactSelectWnd : System.Windows.Forms.Form
	{
		private Label label1;
        private TextBox txtSearch;
		private DataGridTableStyle dgStyle;
		private DataGrid dgContacts;
		private DataGridTextBoxColumn ContactID;
		private DataGridTextBoxColumn ContactCode;
		private DataGridTextBoxColumn ContactName;
		private DataGridTextBoxColumn Debit;
		private DataGridTextBoxColumn Credit;
		private DataGridTextBoxColumn CreditLimit;
		private DataGridTextBoxColumn IsCreditAllowed;
        private DataGridTextBoxColumn PositionName;
        private DataGridTextBoxColumn DepartmentName;
		private PictureBox imgIcon;
        private Label lblAddNewCustomer;
		private System.ComponentModel.Container components = null;
        private AceSoft.KeyBoardHook.KeyboardSearchControl keyboardSearchControl1;

		private DialogResult dialog;
		private Data.ContactDetails mDetails;
		private bool mboHasCreditOnly;
        private ContactGroupCategory mContactGroupCategory;

        public bool HasCreditOnly
		{
			set {	mboHasCreditOnly = value;	}
		}
		public DialogResult Result
		{
			get {	return dialog;	}
		}
		public ContactDetails Details
		{
			get {	return mDetails;	}
		}
        public ContactGroupCategory ContactGroupCategory
        {
            set { mContactGroupCategory = value; }
        }

        #region Constructors and Destructors

        public ContactSelectWnd()
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgContacts = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.ContactID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ContactCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ContactName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CreditLimit = new System.Windows.Forms.DataGridTextBoxColumn();
            this.IsCreditAllowed = new System.Windows.Forms.DataGridTextBoxColumn();
            this.PositionName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.keyboardSearchControl1 = new AceSoft.KeyBoardHook.KeyboardSearchControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgContacts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(67, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter search criteria.";
            // 
            // dgContacts
            // 
            this.dgContacts.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgContacts.BackColor = System.Drawing.Color.White;
            this.dgContacts.BackgroundColor = System.Drawing.Color.White;
            this.dgContacts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgContacts.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgContacts.CaptionFont = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgContacts.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgContacts.CaptionVisible = false;
            this.dgContacts.DataMember = "";
            this.dgContacts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgContacts.FlatMode = true;
            this.dgContacts.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgContacts.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgContacts.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgContacts.HeaderForeColor = System.Drawing.Color.White;
            this.dgContacts.Location = new System.Drawing.Point(0, 198);
            this.dgContacts.Name = "dgContacts";
            this.dgContacts.PreferredRowHeight = 50;
            this.dgContacts.ReadOnly = true;
            this.dgContacts.RowHeadersVisible = false;
            this.dgContacts.RowHeaderWidth = 5;
            this.dgContacts.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgContacts.SelectionForeColor = System.Drawing.Color.White;
            this.dgContacts.Size = new System.Drawing.Size(802, 422);
            this.dgContacts.TabIndex = 5;
            this.dgContacts.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgContacts.TabStop = false;
            this.dgContacts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgContacts_MouseDown);
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgContacts;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.ContactID,
            this.ContactCode,
            this.ContactName,
            this.Debit,
            this.Credit,
            this.CreditLimit,
            this.IsCreditAllowed,
            this.PositionName,
            this.DepartmentName});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblContacts";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 40;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 5;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // ContactID
            // 
            this.ContactID.Format = "";
            this.ContactID.FormatInfo = null;
            this.ContactID.HeaderText = "ID";
            this.ContactID.MappingName = "ContactID";
            this.ContactID.NullText = "";
            this.ContactID.ReadOnly = true;
            this.ContactID.Width = 0;
            // 
            // ContactCode
            // 
            this.ContactCode.Format = "";
            this.ContactCode.FormatInfo = null;
            this.ContactCode.HeaderText = "Contact Code";
            this.ContactCode.MappingName = "ContactCode";
            this.ContactCode.NullText = "";
            this.ContactCode.ReadOnly = true;
            this.ContactCode.Width = 200;
            // 
            // ContactName
            // 
            this.ContactName.Format = "";
            this.ContactName.FormatInfo = null;
            this.ContactName.HeaderText = "Contact Name";
            this.ContactName.MappingName = "ContactName";
            this.ContactName.NullText = "";
            this.ContactName.ReadOnly = true;
            this.ContactName.Width = 200;
            // 
            // Debit
            // 
            this.Debit.Format = "";
            this.Debit.FormatInfo = null;
            this.Debit.HeaderText = "Debit";
            this.Debit.MappingName = "Debit";
            this.Debit.NullText = "";
            this.Debit.ReadOnly = true;
            this.Debit.Width = 0;
            // 
            // Credit
            // 
            this.Credit.Format = "";
            this.Credit.FormatInfo = null;
            this.Credit.HeaderText = "Credit";
            this.Credit.MappingName = "Credit";
            this.Credit.NullText = "";
            this.Credit.ReadOnly = true;
            this.Credit.Width = 0;
            // 
            // CreditLimit
            // 
            this.CreditLimit.Format = "";
            this.CreditLimit.FormatInfo = null;
            this.CreditLimit.HeaderText = "CreditLimit";
            this.CreditLimit.MappingName = "CreditLimit";
            this.CreditLimit.NullText = "";
            this.CreditLimit.ReadOnly = true;
            this.CreditLimit.Width = 0;
            // 
            // IsCreditAllowed
            // 
            this.IsCreditAllowed.Format = "";
            this.IsCreditAllowed.FormatInfo = null;
            this.IsCreditAllowed.HeaderText = "Allow Credit";
            this.IsCreditAllowed.MappingName = "IsCreditAllowed";
            this.IsCreditAllowed.NullText = "";
            this.IsCreditAllowed.ReadOnly = true;
            this.IsCreditAllowed.Width = 0;
            // 
            // PositionName
            // 
            this.PositionName.Format = "";
            this.PositionName.FormatInfo = null;
            this.PositionName.HeaderText = "PositionName";
            this.PositionName.MappingName = "PositionName";
            this.PositionName.NullText = "";
            this.PositionName.ReadOnly = true;
            this.PositionName.Width = 0;
            // 
            // DepartmentName
            // 
            this.DepartmentName.Format = "";
            this.DepartmentName.FormatInfo = null;
            this.DepartmentName.HeaderText = "DepartmentName";
            this.DepartmentName.MappingName = "DepartmentName";
            this.DepartmentName.NullText = "";
            this.DepartmentName.ReadOnly = true;
            this.DepartmentName.Width = 0;
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(586, 33);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(206, 13);
            this.lblAddNewCustomer.TabIndex = 89;
            this.lblAddNewCustomer.Text = "Click or Press <F2> to Add new customer";
            this.lblAddNewCustomer.Click += new System.EventHandler(this.lblAddNewCustomer_Click);
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
            // keyboardSearchControl1
            // 
            this.keyboardSearchControl1.BackColor = System.Drawing.Color.White;
            this.keyboardSearchControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.keyboardSearchControl1.Location = new System.Drawing.Point(0, 64);
            this.keyboardSearchControl1.Name = "keyboardSearchControl1";
            this.keyboardSearchControl1.Size = new System.Drawing.Size(802, 134);
            this.keyboardSearchControl1.TabIndex = 1;
            this.keyboardSearchControl1.TabStop = false;
            this.keyboardSearchControl1.Tag = "";
            this.keyboardSearchControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardSearchControl1_UserKeyPressed);
            // 
            // ContactSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardSearchControl1);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.dgContacts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ContactSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ContactSelectWnd_Load);
            this.Resize += new System.EventHandler(this.ContactSelectWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactSelectWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgContacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Windows Form Methods

		private void ContactSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			System.Data.DataTable dt;
			int index;

			switch (e.KeyData)
			{
				case Keys.Escape:
					dialog = DialogResult.Cancel;
					this.Hide(); 
					break;

				case Keys.Enter:
					if (dgContacts.CurrentRowIndex < 0)
					{
						dialog = DialogResult.Cancel;
					} 
					else 
					{
						CreateDetails(dgContacts.CurrentRowIndex);
						dialog = DialogResult.OK;
					}
					this.Hide(); 
					break;
				
				case Keys.Up:
					dt = (System.Data.DataTable) dgContacts.DataSource;
					if (dgContacts.CurrentRowIndex > 0) 
					{
						index = dgContacts.CurrentRowIndex;				
						dgContacts.CurrentRowIndex -= 1;
						dgContacts.Select(dgContacts.CurrentRowIndex);
						dgContacts.UnSelect(index);
					}
					break;

				case Keys.Down:
					dt = (System.Data.DataTable) dgContacts.DataSource;
					if (dgContacts.CurrentRowIndex < dt.Rows.Count-1) 
					{
						index = dgContacts.CurrentRowIndex;				

						dgContacts.CurrentRowIndex += 1;
						dgContacts.Select(dgContacts.CurrentRowIndex);
						dgContacts.UnSelect(index);
					}
					break;
                case Keys.F2:
                    ContactAdd();
                    break;
			}
		}

		private void ContactSelectWnd_Load(object sender, System.EventArgs e)
		{
			try
			{	this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg");	}
			catch{}
			try
			{	this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ContactSelect.jpg");	}
			catch{}

			LoadOptions();
			LoadContactData();
		}

		private void ContactSelectWnd_Resize(object sender, System.EventArgs e)
		{
            dgStyle.GridColumnStyles["ContactCode"].Width = 200;
            dgStyle.GridColumnStyles["ContactName"].Width = this.Width - 225;
		}
		
		#endregion

        #region Windows Control Methods

		private void txtSearch_TextChanged(object sender, System.EventArgs e)
		{
			LoadContactData();
		}

        private void dgContacts_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid dgContacts = (DataGrid)sender;
            System.Windows.Forms.DataGrid.HitTestInfo hti = dgContacts.HitTest(e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    dgContacts.Select(hti.Row);
                    CreateDetails(hti.Row);
                    dialog = DialogResult.OK;
                    this.Hide();
                    break;
            }
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void keyboardSearchControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            txtSearch.Focus();
            SendKeys.Send(e.KeyboardKeyPressed);
        }
		
        #endregion

		#region Private Methods

        private void CreateDetails(int iRow)
		{
			mDetails = new ContactDetails();
			
			//int iRow = dgContacts.CurrentRowIndex;

			mDetails.ContactID = Convert.ToInt64(dgContacts[iRow, 0].ToString());
			mDetails.ContactCode = dgContacts[iRow, 1].ToString();
			mDetails.ContactName = dgContacts[iRow, 2].ToString();
			mDetails.Debit = Convert.ToDecimal(dgContacts[iRow, 3].ToString());
			mDetails.Credit = Convert.ToDecimal(dgContacts[iRow, 4].ToString());
			mDetails.CreditLimit = Convert.ToDecimal(dgContacts[iRow, 5].ToString());
			mDetails.IsCreditAllowed = Convert.ToInt16(dgContacts[iRow, 6].ToString());
            mDetails.PositionName = dgContacts[iRow, 7].ToString();
            mDetails.DepartmentName = dgContacts[iRow, 8].ToString();
		}
		private void LoadOptions()
		{
            if (mContactGroupCategory == ContactGroupCategory.AGENT)
                lblAddNewCustomer.Visible = false;

		}
		private void LoadContactData()
		{	
			Contacts clsContact = new Contacts();

			try
			{
				string searchkey = "" + txtSearch.Text;

                System.Data.DataTable dt;
                if (mContactGroupCategory == ContactGroupCategory.AGENT)
                    dt = clsContact.AgentsAsDataTable(searchkey, 100, "ContactName", SortOption.Ascending); 
				else
                    dt = clsContact.CustomersDataTable(searchkey, 100, mboHasCreditOnly, "ContactName", SortOption.Ascending);

				clsContact.CommitAndDispose();
				
				dgContacts.DataSource = dt;
				dgContacts.Select(0);
				dgContacts.CurrentRowIndex=0;
			}
			catch (IndexOutOfRangeException){}
			catch (Exception ex)
			{
				clsContact.CommitAndDispose();
				MessageBox.Show(ex.Message,"RetailPlus",MessageBoxButtons.OK,MessageBoxIcon.Error); 
			}
		}
        private void ContactAdd()
        {
            ContactAddWnd addwnd = new ContactAddWnd();
            addwnd.Caption = "Quickly add new customer.";
            addwnd.ShowDialog(this);
            DialogResult addresult = addwnd.Result;
            Data.ContactDetails details = addwnd.ContactDetails;
            addwnd.Close();
            addwnd.Dispose();

            if (addresult == DialogResult.OK)
            {
                txtSearch.Text = details.ContactCode;
                LoadContactData();
            }

        }

		#endregion

        private void lblAddNewCustomer_Click(object sender, EventArgs e)
        {
            ContactAdd();
        }

	}
}
