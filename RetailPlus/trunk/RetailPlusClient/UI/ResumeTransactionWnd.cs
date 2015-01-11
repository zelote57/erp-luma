using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    public class ResumeTransactionWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgItems;
        private System.Windows.Forms.DataGridTableStyle dgStyle;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionID;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionNo;
        private System.Windows.Forms.DataGridTextBoxColumn CustomerID;
        private System.Windows.Forms.DataGridTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionDate;
        private System.Windows.Forms.DataGridTextBoxColumn DateSuspended;
        private System.Windows.Forms.DataGridTextBoxColumn TransactionStatus;
        private System.Windows.Forms.DataGridTextBoxColumn TransDiscount;
        private System.Windows.Forms.DataGridTextBoxColumn TransDiscountType;
        private System.Windows.Forms.DataGridTextBoxColumn WaiterID;
        private System.Windows.Forms.DataGridTextBoxColumn WaiterName;
        private System.Windows.Forms.DataGridTextBoxColumn ChargeAmount;
        private System.Windows.Forms.DataGridTextBoxColumn ChargeCode;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.PictureBox imgIcon;
        private TextBox txtSearch;

        #region public Properties

        private Int64 mCashierID;
        public Int64 CashierID
        {
            set
            {
                mCashierID = value;
            }
        }

        private DialogResult dialog;
        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        private Data.SalesTransactionDetails mDetails = new Data.SalesTransactionDetails();
        public Data.SalesTransactionDetails Details
        {
            get
            {
                return mDetails;
            }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        #endregion

        #region Constructors and Destructors

        public ResumeTransactionWnd()
        {
            InitializeComponent();

            if (Common.isTerminalMultiInstanceEnabled())
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.dgItems = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.TransactionID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CustomerID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionDate = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DateSuspended = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransactionStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransDiscount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.TransDiscountType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.WaiterID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.WaiterName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ChargeAmount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ChargeCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(67, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "<- Press the icon to close the window or Enter search criteria.";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.TabIndex = 53;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // dgItems
            // 
            this.dgItems.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.BackColor = System.Drawing.Color.White;
            this.dgItems.BackgroundColor = System.Drawing.Color.White;
            this.dgItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgItems.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgItems.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgItems.CaptionVisible = false;
            this.dgItems.DataMember = "";
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgItems.FlatMode = true;
            this.dgItems.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgItems.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.HeaderForeColor = System.Drawing.Color.White;
            this.dgItems.Location = new System.Drawing.Point(0, 67);
            this.dgItems.Name = "dgItems";
            this.dgItems.PreferredRowHeight = 50;
            this.dgItems.ReadOnly = true;
            this.dgItems.RowHeadersVisible = false;
            this.dgItems.RowHeaderWidth = 5;
            this.dgItems.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgItems.SelectionForeColor = System.Drawing.Color.White;
            this.dgItems.Size = new System.Drawing.Size(1022, 699);
            this.dgItems.TabIndex = 54;
            this.dgItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgItems.TabStop = false;
            this.dgItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgItems_MouseDown);
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgItems;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.TransactionID,
            this.TransactionNo,
            this.CustomerID,
            this.CustomerName,
            this.TransactionDate,
            this.DateSuspended,
            this.TransactionStatus,
            this.TransDiscount,
            this.TransDiscountType,
            this.WaiterID,
            this.WaiterName,
            this.ChargeAmount,
            this.ChargeCode});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblSuspendedTransactions";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 40;
            this.dgStyle.ReadOnly = true;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // TransactionID
            // 
            this.TransactionID.Format = "";
            this.TransactionID.FormatInfo = null;
            this.TransactionID.MappingName = "TransactionID";
            this.TransactionID.NullText = "";
            this.TransactionID.ReadOnly = true;
            this.TransactionID.Width = 0;
            // 
            // TransactionNo
            // 
            this.TransactionNo.Format = "";
            this.TransactionNo.FormatInfo = null;
            this.TransactionNo.HeaderText = "Transaction No";
            this.TransactionNo.MappingName = "TransactionNo";
            this.TransactionNo.NullText = "";
            this.TransactionNo.ReadOnly = true;
            this.TransactionNo.Width = 180;
            // 
            // CustomerID
            // 
            this.CustomerID.Format = "";
            this.CustomerID.FormatInfo = null;
            this.CustomerID.MappingName = "CustomerID";
            this.CustomerID.NullText = "";
            this.CustomerID.ReadOnly = true;
            this.CustomerID.Width = 0;
            // 
            // CustomerName
            // 
            this.CustomerName.Format = "";
            this.CustomerName.FormatInfo = null;
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.MappingName = "CustomerName";
            this.CustomerName.NullText = "";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 180;
            // 
            // TransactionDate
            // 
            this.TransactionDate.Format = "";
            this.TransactionDate.FormatInfo = null;
            this.TransactionDate.HeaderText = "TransactionDate";
            this.TransactionDate.MappingName = "TransactionDate";
            this.TransactionDate.NullText = "";
            this.TransactionDate.ReadOnly = true;
            this.TransactionDate.Width = 0;
            // 
            // DateSuspended
            // 
            this.DateSuspended.Format = "";
            this.DateSuspended.FormatInfo = null;
            this.DateSuspended.HeaderText = "Date Suspended";
            this.DateSuspended.MappingName = "DateSuspended";
            this.DateSuspended.NullText = "";
            this.DateSuspended.ReadOnly = true;
            this.DateSuspended.Width = 180;
            // 
            // TransactionStatus
            // 
            this.TransactionStatus.Format = "";
            this.TransactionStatus.FormatInfo = null;
            this.TransactionStatus.HeaderText = "TransactionStatus";
            this.TransactionStatus.MappingName = "TransactionStatus";
            this.TransactionStatus.NullText = "";
            this.TransactionStatus.ReadOnly = true;
            this.TransactionStatus.Width = 0;
            // 
            // TransDiscount
            // 
            this.TransDiscount.Format = "";
            this.TransDiscount.FormatInfo = null;
            this.TransDiscount.MappingName = "TransDiscount";
            this.TransDiscount.NullText = "";
            this.TransDiscount.ReadOnly = true;
            this.TransDiscount.Width = 0;
            // 
            // TransDiscountType
            // 
            this.TransDiscountType.Format = "";
            this.TransDiscountType.FormatInfo = null;
            this.TransDiscountType.MappingName = "TransDiscountType";
            this.TransDiscountType.NullText = "";
            this.TransDiscountType.ReadOnly = true;
            this.TransDiscountType.Width = 0;
            // 
            // WaiterID
            // 
            this.WaiterID.Format = "";
            this.WaiterID.FormatInfo = null;
            this.WaiterID.MappingName = "WaiterID";
            this.WaiterID.ReadOnly = true;
            this.WaiterID.Width = 0;
            // 
            // WaiterName
            // 
            this.WaiterName.Format = "";
            this.WaiterName.FormatInfo = null;
            this.WaiterName.MappingName = "WaiterName";
            this.WaiterName.ReadOnly = true;
            this.WaiterName.Width = 0;
            // 
            // ChargeAmount
            // 
            this.ChargeAmount.Format = "";
            this.ChargeAmount.FormatInfo = null;
            this.ChargeAmount.MappingName = "ChargeAmount";
            this.ChargeAmount.ReadOnly = true;
            this.ChargeAmount.Width = 0;
            // 
            // ChargeCode
            // 
            this.ChargeCode.Format = "";
            this.ChargeCode.FormatInfo = null;
            this.ChargeCode.MappingName = "ChargeCode";
            this.ChargeCode.ReadOnly = true;
            this.ChargeCode.Width = 0;
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
            // ResumeTransactionWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ResumeTransactionWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ResumeTransactionWnd_Load);
            this.Resize += new System.EventHandler(this.ResumeTransactionWnd_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResumeTransactionWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #endregion

        #region Windows Form Methods

        private void ResumeTransactionWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                    if (dgItems.CurrentRowIndex < 0)
                    {
                        dialog = DialogResult.Cancel;
                        this.Hide();
                    }
                    else
                    {
                        dialog = DialogResult.OK;
                        if (CreateDetails(dgItems.CurrentRowIndex))
                        {   this.Hide();    }
                    }
                    
                    break;

                case Keys.Up:
                    dt = (System.Data.DataTable)dgItems.DataSource;
                    if (dgItems.CurrentRowIndex > 0)
                    {
                        index = dgItems.CurrentRowIndex;
                        dgItems.CurrentRowIndex -= 1;
                        dgItems.Select(dgItems.CurrentRowIndex);
                        dgItems.UnSelect(index);
                    }
                    break;

                case Keys.Down:
                    dt = (System.Data.DataTable)dgItems.DataSource;
                    if (dgItems.CurrentRowIndex < dt.Rows.Count - 1)
                    {
                        index = dgItems.CurrentRowIndex;

                        dgItems.CurrentRowIndex += 1;
                        dgItems.Select(dgItems.CurrentRowIndex);
                        dgItems.UnSelect(index);
                    }
                    break;
            }
        }

        private void ResumeTransactionWnd_Resize(object sender, System.EventArgs e)
        {
            dgStyle.GridColumnStyles["TransactionNo"].Width = 180;
            dgStyle.GridColumnStyles["CustomerName"].Width = 180;
            dgStyle.GridColumnStyles["DateSuspended"].Width = this.Width - 370;
        }

        private void ResumeTransactionWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ResumeTransaction.jpg"); }
            catch { }
            LoadOptions();
            LoadData();
            txtSearch.Focus();
        }

        #endregion

        #region Windows Control Methods

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            System.Data.DataTable dt = (System.Data.DataTable)dgItems.DataSource;

            for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
            {
                try
                {
                    if (dgItems[iRow, 3].ToString().Substring(0, txtSearch.Text.Length).ToLower() == txtSearch.Text.ToLower())
                    {
                        dgItems.UnSelect(dgItems.CurrentRowIndex);
                        dgItems.Select(iRow);
                        dgItems.CurrentRowIndex = iRow;
                        return;
                    }
                }
                catch { }
            }
        }

        private void dgItems_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGrid dgItems = (DataGrid)sender;
            System.Windows.Forms.DataGrid.HitTestInfo hti = dgItems.HitTest(e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    {
                        dgItems.Select(hti.Row);
                        if (CreateDetails(hti.Row))
                        {
                            dialog = DialogResult.OK;
                            this.Hide();
                        }
                        break;
                    }
            }
        }

        private void txtSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
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

        #region Private Modifiers

        private void LoadOptions()
        {
            dgStyle.GridColumnStyles["TransactionNo"].Width = 200;
            dgStyle.GridColumnStyles["CustomerName"].Width = this.Width - 510;
            dgStyle.GridColumnStyles["DateSuspended"].Width = 300;
        }

        private void LoadData()
        {
            try
            {
                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();

                Int64 iCashierID = TerminalDetails.ShowOneTerminalSuspendedTransactions ? mCashierID : 0;
                System.Data.DataTable dt = clsTransactions.ListSuspendedDataTable(TerminalDetails.BranchID, TerminalDetails.TerminalNo, iCashierID, TerminalDetails.ShowOnlyPackedTransactions);
                clsTransactions.CommitAndDispose();

                this.dgStyle.MappingName = dt.TableName;
                dgItems.DataSource = dt;
                dgItems.Select(0);
                dgItems.CurrentRowIndex = 0;
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CreateDetails(int iRow)
        {
            try
            {
                bool boRetValue = false;

                mDetails = new Data.SalesTransactionDetails();

                mDetails.TransactionID = Convert.ToInt64(dgItems[iRow, 0]);
                mDetails.TransactionNo = dgItems[iRow, 1].ToString();

                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                mDetails = clsTransactions.Details(mDetails.TransactionNo, TerminalDetails.TerminalNo, TerminalDetails.BranchID);
                clsTransactions.Resume(mDetails.TransactionID);

                Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems(clsTransactions.Connection, clsTransactions.Transaction);
                mDetails.TransactionItems = clsItems.Details(mDetails.TransactionID, mDetails.TransactionDate);

                clsTransactions.CommitAndDispose();

                boRetValue = true;

                return boRetValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

    }
}
