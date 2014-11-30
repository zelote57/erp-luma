using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client.UI
{
	public class ContactSelectWnd : System.Windows.Forms.Form
	{
		private Label lblHeader;
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
        private System.ComponentModel.Container components = null;

        #region Public Properties

        public Data.TerminalDetails TerminalDetails { get; set; }

        public bool HasCreditOnly { get; set; }

        private DialogResult dialog;
		public DialogResult Result
		{
			get {	return dialog;	}
		}

        private Data.ContactDetails mDetails;
        private Label lblUpdateCustomer;
        private Label lblPress;
        private Label lblF6;
        private Label lblF2;
        private Label lblAddNewCustomer;
    
		public ContactDetails Details
		{
			get {	return mDetails;	}
		}

        private ContactGroupCategory mContactGroupCategory;
		public ContactGroupCategory ContactGroupCategory
		{
			set { mContactGroupCategory = value; }
		}

        public Keys keyCommand { get; set; }
        public string Header { get; set; }

        public bool EnableContactAddUpdate { get; set; }
        #endregion

        #region Constructors and Destructors

        public ContactSelectWnd()
		{
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ContactSelect.jpg"); }
            catch { }

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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
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
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblUpdateCustomer = new System.Windows.Forms.Label();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblF6 = new System.Windows.Forms.Label();
            this.lblF2 = new System.Windows.Forms.Label();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
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
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(125, 13);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "Enter search criteria.";
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
            this.dgContacts.Location = new System.Drawing.Point(0, 67);
            this.dgContacts.Name = "dgContacts";
            this.dgContacts.PreferredRowHeight = 50;
            this.dgContacts.ReadOnly = true;
            this.dgContacts.RowHeadersVisible = false;
            this.dgContacts.RowHeaderWidth = 5;
            this.dgContacts.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgContacts.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            this.dgContacts.Size = new System.Drawing.Size(1022, 699);
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
            // lblUpdateCustomer
            // 
            this.lblUpdateCustomer.AutoSize = true;
            this.lblUpdateCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblUpdateCustomer.Location = new System.Drawing.Point(845, 25);
            this.lblUpdateCustomer.Name = "lblUpdateCustomer";
            this.lblUpdateCustomer.Size = new System.Drawing.Size(147, 13);
            this.lblUpdateCustomer.TabIndex = 115;
            this.lblUpdateCustomer.Text = " to update selected customer";
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.BackColor = System.Drawing.Color.Transparent;
            this.lblPress.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblPress.Location = new System.Drawing.Point(771, 9);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(33, 13);
            this.lblPress.TabIndex = 114;
            this.lblPress.Text = "Press";
            // 
            // lblF6
            // 
            this.lblF6.BackColor = System.Drawing.Color.Transparent;
            this.lblF6.ForeColor = System.Drawing.Color.Red;
            this.lblF6.Location = new System.Drawing.Point(806, 25);
            this.lblF6.Name = "lblF6";
            this.lblF6.Size = new System.Drawing.Size(27, 13);
            this.lblF6.TabIndex = 113;
            this.lblF6.Text = "[F6]";
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.BackColor = System.Drawing.Color.Transparent;
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.Location = new System.Drawing.Point(805, 9);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(27, 13);
            this.lblF2.TabIndex = 112;
            this.lblF2.Text = "[F2]";
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(845, 9);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(111, 13);
            this.lblAddNewCustomer.TabIndex = 111;
            this.lblAddNewCustomer.Text = " to add new customer";
            // 
            // ContactSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.lblUpdateCustomer);
            this.Controls.Add(this.lblPress);
            this.Controls.Add(this.lblF6);
            this.Controls.Add(this.lblF2);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.dgContacts);
            this.Controls.Add(this.lblHeader);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ContactSelectWnd_KeyDown);
            this.Resize += new System.EventHandler(this.ContactSelectWnd_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgContacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Windows Form Methods

		private void ContactSelectWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            keyCommand = e.KeyData;
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
                    if (mContactGroupCategory != Data.ContactGroupCategory.AGENT) ContactAdd();
					break;

                case Keys.F6:
                    if (mContactGroupCategory != Data.ContactGroupCategory.AGENT) ContactUpdate();
                    break;
			}
		}

		private void ContactSelectWnd_Load(object sender, System.EventArgs e)
		{
			if (mContactGroupCategory == Data.ContactGroupCategory.AGENT)
                lblHeader.Text = "Enter agent code / name to search.";
            else
                lblHeader.Text = "Enter customer code/name/in-house credit card no to search.";

            lblHeader.Text = !string.IsNullOrEmpty(Header) ? Header : lblHeader.Text;


            if (mContactGroupCategory == ContactGroupCategory.AGENT)
            {
                lblPress.Visible = EnableContactAddUpdate;
                lblUpdateCustomer.Visible = EnableContactAddUpdate;
                lblAddNewCustomer.Visible = EnableContactAddUpdate;
                lblF2.Visible = false;
                lblF6.Visible = false;
            }
            else
            {
                lblPress.Visible = EnableContactAddUpdate;
                lblUpdateCustomer.Visible = EnableContactAddUpdate;
                lblAddNewCustomer.Visible = EnableContactAddUpdate;
                lblF2.Visible = EnableContactAddUpdate;
                lblF6.Visible = EnableContactAddUpdate;
            }

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

        private void lblAddNewCustomer_Click(object sender, EventArgs e)
        {
            ContactAdd();
        }

		#endregion

		#region Private Methods

		private void CreateDetails(int iRow)
		{
			mDetails = new ContactDetails();
			Customer clsCustomer = new Customer();
			mDetails = clsCustomer.Details(Convert.ToInt64(dgContacts[iRow, 0].ToString()));
			clsCustomer.CommitAndDispose();

			////int iRow = dgContacts.CurrentRowIndex;

			//mDetails.ContactID = Convert.ToInt64(dgContacts[iRow, 0].ToString());
			//mDetails.ContactCode = dgContacts[iRow, 1].ToString();
			//mDetails.ContactName = dgContacts[iRow, 2].ToString();
			//mDetails.Debit = Convert.ToDecimal(dgContacts[iRow, 3].ToString());
			//mDetails.Credit = Convert.ToDecimal(dgContacts[iRow, 4].ToString());
			//mDetails.CreditLimit = Convert.ToDecimal(dgContacts[iRow, 5].ToString());
			//mDetails.IsCreditAllowed = Convert.ToInt16(dgContacts[iRow, 6].ToString());
			//mDetails.PositionName = dgContacts[iRow, 7].ToString();
			//mDetails.DepartmentName = dgContacts[iRow, 8].ToString();
		}
		private void LoadOptions()
		{
            

		}
		private void LoadContactData()
		{	
			Contacts clsContact = new Contacts();

			try
			{
				string searchkey = "" + txtSearch.Text;

				System.Data.DataTable dt;
				if (mContactGroupCategory == ContactGroupCategory.AGENT)
					dt = clsContact.AgentsAsDataTable(searchkey, 50, "ContactName", SortOption.Ascending); 
				else
                    dt = clsContact.CustomersDataTable(searchkey, 50, HasCreditOnly, "ContactName", SortOption.Ascending);

                if (!TerminalDetails.ShowCustomerSelection && dt.Rows.Count == 0)
                {
                    Data.ContactDetails clsContactDetails = clsContact.DetailsByCreditCardNo(txtSearch.Text);

                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 7) clsContactDetails = clsContact.DetailsByCreditCardNo("888880" + txtSearch.Text);
                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 7) clsContactDetails = clsContact.DetailsByCreditCardNo("800000" + txtSearch.Text);
                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 9) clsContactDetails = clsContact.DetailsByCreditCardNo(BarcodeHelper.GroupCreditCard_Country_Code + BarcodeHelper.GroupCreditCard_ManufacturerCode + txtSearch.Text);
                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 9) clsContactDetails = clsContact.DetailsByCreditCardNo(BarcodeHelper.CustomerCode_Country_Code + BarcodeHelper.CustomerCode_ManufacturerCode + txtSearch.Text);
                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 9) clsContactDetails = clsContact.DetailsByCreditCardNo(BarcodeHelper.GroupCreditCard_Country_Code + BarcodeHelper.GroupCreditCard_ManufacturerCode_Manual + txtSearch.Text);
                    if (clsContactDetails.ContactID == 0 && txtSearch.Text.Length == 9) clsContactDetails = clsContact.DetailsByCreditCardNo(BarcodeHelper.CreditCard_Country_Code + BarcodeHelper.CreditCard_ManufacturerCode + txtSearch.Text);

                    if (clsContactDetails.ContactID != 0)
                    {
                        searchkey = clsContactDetails.ContactCode;

                        if (mContactGroupCategory == ContactGroupCategory.AGENT)
                            dt = clsContact.AgentsAsDataTable(searchkey, 100, "ContactName", SortOption.Ascending);
                        else
                            dt = clsContact.CustomersDataTable(searchkey, 100, HasCreditOnly, "ContactName", SortOption.Ascending);
                    }
                }
				clsContact.CommitAndDispose();

                this.dgStyle.MappingName = dt.TableName;
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
            if (!EnableContactAddUpdate) return;

            Data.ContactDetails details = new Data.ContactDetails();
            DialogResult addresult = System.Windows.Forms.DialogResult.Cancel;
            if (!TerminalDetails.ShowCustomerSelection)
            {
                ContactAddDetWnd addwnd = new ContactAddDetWnd();
                addwnd.Caption = "Quickly add new customer.";
                addwnd.TerminalDetails = TerminalDetails;
                addwnd.ShowDialog(this);
                addresult = addwnd.Result;
                details = addwnd.ContactDetails;
                addwnd.Close();
                addwnd.Dispose();
            }
            else
            {
			    ContactAddWnd addwnd = new ContactAddWnd();
			    addwnd.Caption = "Quickly add new customer.";
                addwnd.TerminalDetails = TerminalDetails;
			    addwnd.ShowDialog(this);
			    addresult = addwnd.Result;
			    details = addwnd.ContactDetails;
			    addwnd.Close();
			    addwnd.Dispose();
            }
            if (addresult == DialogResult.OK)
            {
                txtSearch.Text = details.ContactCode;
                LoadContactData();
            }
		}
        private void ContactUpdate()
        {
            try
            {
                if (!EnableContactAddUpdate) return;

                Int64 iContactID = Convert.ToInt64(dgContacts[dgContacts.CurrentRowIndex, 0].ToString());

                if (iContactID != 0 && iContactID != Constants.C_RETAILPLUS_CUSTOMERID)
                {
                    Data.Contacts clsContact = new Data.Contacts();
                    Data.ContactDetails details = clsContact.Details(iContactID);
                    clsContact.CommitAndDispose();

                    DialogResult addresult = System.Windows.Forms.DialogResult.Cancel;
                    if (!TerminalDetails.ShowCustomerSelection)
                    {
                        ContactAddDetWnd clsContactAddWnd = new ContactAddDetWnd();
                        clsContactAddWnd.Caption = "Update Customer [" + details.ContactName + "]";
                        clsContactAddWnd.ContactDetails = details;
                        clsContactAddWnd.TerminalDetails = TerminalDetails;
                        clsContactAddWnd.ShowDialog(this);
                        addresult = clsContactAddWnd.Result;
                        details = clsContactAddWnd.ContactDetails;
                        clsContactAddWnd.Close();
                        clsContactAddWnd.Dispose();
                    }
                    else
                    {
                        ContactAddWnd clsContactAddWnd = new ContactAddWnd();
                        clsContactAddWnd.Caption = "Update Customer [" + details.ContactName + "]";
                        clsContactAddWnd.ContactDetails = details;
                        clsContactAddWnd.TerminalDetails = TerminalDetails;
                        clsContactAddWnd.ShowDialog(this);
                        addresult = clsContactAddWnd.Result;
                        details = clsContactAddWnd.ContactDetails;
                        clsContactAddWnd.Close();
                        clsContactAddWnd.Dispose();
                    }
                    if (addresult == DialogResult.OK)
                    {
                        txtSearch.Text = details.ContactCode;
                        LoadContactData();
                    }
                }
            }
            catch { }
        }

		#endregion

	}
}
