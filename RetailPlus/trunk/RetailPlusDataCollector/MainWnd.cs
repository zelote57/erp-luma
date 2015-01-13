using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.DataCollector
{
    public partial class MainWnd : Form
    {
        private System.ComponentModel.BackgroundWorker bgwSavetoDB = new System.ComponentModel.BackgroundWorker();

        private System.ComponentModel.BackgroundWorker bgwZeroOutInv = new System.ComponentModel.BackgroundWorker();
        private System.ComponentModel.BackgroundWorker bgwZeroOutInvNeg = new System.ComponentModel.BackgroundWorker();

        private Event mclsEvent = new Event();
        private System.Data.DataTable mdtItems;

        private InvLog mclsInvLog = new InvLog();
        private Data.BranchDetails mclsBranchDetails;

        private string mstStatus;

        public MainWnd()
        {
            InitializeComponent();

            bgwSavetoDB.WorkerReportsProgress = true;
            bgwSavetoDB.DoWork += new DoWorkEventHandler(bgwSavetoDB_DoWork);
            bgwSavetoDB.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwSavetoDB_RunWorkerCompleted);
            bgwSavetoDB.ProgressChanged += new ProgressChangedEventHandler(bgwSavetoDB_ProgressChanged);

            bgwZeroOutInv.WorkerReportsProgress = true;
            bgwZeroOutInv.DoWork += new DoWorkEventHandler(bgwZeroOutInv_DoWork);
            bgwZeroOutInv.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwZeroOutInv_RunWorkerCompleted);
            bgwZeroOutInv.ProgressChanged += new ProgressChangedEventHandler(bgwZeroOutInv_ProgressChanged);

            bgwZeroOutInvNeg.WorkerReportsProgress = true;
            bgwZeroOutInvNeg.DoWork += new DoWorkEventHandler(bgwZeroOutInvNeg_DoWork);
            bgwZeroOutInvNeg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwZeroOutInvNeg_RunWorkerCompleted);
            bgwZeroOutInvNeg.ProgressChanged += new ProgressChangedEventHandler(bgwZeroOutInvNeg_ProgressChanged);
        }

        private void bgwSavetoDB_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SaveToDB();
            }
            catch { throw; }
        }

        // This event handler deals with the results of the 
        // background operation. 
        private void bgwSavetoDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set progress bar to 100% in case it's not already there.
            prgBar.Value = 100;

            // First, handle the case where an exception was thrown. 
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled  
                // the operation. 
                // Note that due to a race condition in  
                // the DoWork event handler, the Cancelled 
                // flag may not have been set, even though 
                // CancelAsync was called.
                lblStatus.Text = "Canceled";
            }
            else
            {
                grpSaveToDB.Visible = false;
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Inventory has been uploaded.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // This event handler updates the progress bar. 
        private void bgwSavetoDB_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgBar.Value = e.ProgressPercentage;

            this.lblStatus.Text = mstStatus;
        }

        private void bgwZeroOutInv_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mstStatus = "Connecting to database...";
                bgwZeroOutInv.ReportProgress(10);

                AceSoft.RetailPlus.Client.MasterDB clsMasterConnection = new AceSoft.RetailPlus.Client.MasterDB();
                clsMasterConnection.GetConnection();

                mstStatus = "Updating product quantity to zero...";
                bgwZeroOutInv.ReportProgress(20);
                Data.ProductInventories clsProductInventories = new Data.ProductInventories(clsMasterConnection.Connection, clsMasterConnection.Transaction);
                clsProductInventories.ZeroOutInventoryByBranch(mclsBranchDetails.BranchID);

                mstStatus = "Commiting to database...";
                bgwZeroOutInv.ReportProgress(90);
                clsMasterConnection.CommitAndDispose();

                bgwZeroOutInv.ReportProgress(100);
            }
            catch { throw; }
        }

        // This event handler deals with the results of the 
        // background operation. 
        private void bgwZeroOutInv_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set progress bar to 100% in case it's not already there.
            prgBar.Value = 100;

            // First, handle the case where an exception was thrown. 
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled  
                // the operation. 
                // Note that due to a race condition in  
                // the DoWork event handler, the Cancelled 
                // flag may not have been set, even though 
                // CancelAsync was called.
                lblStatus.Text = "Canceled";
            }
            else
            {
                grpSaveToDB.Visible = false;
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Quantity has been zero out.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // This event handler updates the progress bar. 
        private void bgwZeroOutInv_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgBar.Value = e.ProgressPercentage;

            this.lblStatus.Text = mstStatus;
        }

        private void bgwZeroOutInvNeg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mstStatus = "Connecting to database...";
                bgwZeroOutInvNeg.ReportProgress(10);

                AceSoft.RetailPlus.Client.MasterDB clsMasterConnection = new AceSoft.RetailPlus.Client.MasterDB();
                clsMasterConnection.GetConnection();

                mstStatus = "Updating product negative quantity to zero...";
                bgwZeroOutInvNeg.ReportProgress(20);
                Data.ProductInventories clsProductInventories = new Data.ProductInventories(clsMasterConnection.Connection, clsMasterConnection.Transaction);
                clsProductInventories.ZeroOutNegativeInventorygByBranch(mclsBranchDetails.BranchID);

                mstStatus = "Commiting to database...";
                bgwZeroOutInvNeg.ReportProgress(90);
                clsMasterConnection.CommitAndDispose();

                bgwZeroOutInvNeg.ReportProgress(100);
            }
            catch { throw; }
        }

        // This event handler deals with the results of the 
        // background operation. 
        private void bgwZeroOutInvNeg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set progress bar to 100% in case it's not already there.
            prgBar.Value = 100;

            // First, handle the case where an exception was thrown. 
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled  
                // the operation. 
                // Note that due to a race condition in  
                // the DoWork event handler, the Cancelled 
                // flag may not have been set, even though 
                // CancelAsync was called.
                lblStatus.Text = "Canceled";
            }
            else
            {
                grpSaveToDB.Visible = false;
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Negative Quantity has been zero out.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // This event handler updates the progress bar. 
        private void bgwZeroOutInvNeg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prgBar.Value = e.ProgressPercentage;

            this.lblStatus.Text = mstStatus;
        }

        private void cmdBranchSelect_Click(object sender, EventArgs e)
        {
            try
            {
                BranchWnd clsBranchWnd = new BranchWnd();
                clsBranchWnd.ShowDialog(this);
                mclsBranchDetails = clsBranchWnd.BranchDetails;
                clsBranchWnd.Close();
                clsBranchWnd.Dispose();

                if (clsBranchWnd.Result == System.Windows.Forms.DialogResult.OK)
                {
                    cmdBranchSelect.Text = mclsBranchDetails.BranchCode;
                    LoadFile();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ReadBarCode();
            }
            catch (Exception ex)
            {
                mclsEvent.AddErrorEvent(ex);
                throw;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (mclsBranchDetails.BranchID == 0)
                {
                    MessageBox.Show("Please select the branch to inventory.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmdBranchSelect.Focus();
                    return;
                }
                decimal decQuantity = 0;
                if (txtQuantity.Text.IndexOf("*") == -1)
                {
                    if (!decimal.TryParse(txtQuantity.Text, out decQuantity))
                    {
                        MessageBox.Show("Please enter a valid numeric quantity.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (txtQuantity.Text.IndexOf("*") != -1)
                {
                    string[] Qty = txtQuantity.Text.Split('*');
                    if (!decimal.TryParse(Qty[0], out decQuantity))
                    {
                        MessageBox.Show("Please enter a valid numeric quantity such as {No of Boxes} * {Qty per Box}.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQuantity.Focus(); txtQuantity.SelectAll();
                        return;
                    }
                    if (!decimal.TryParse(Qty[1], out decQuantity))
                    {
                        MessageBox.Show("Please enter a valid numeric quantity such as {No of Boxes} * {Qty per Box}.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQuantity.Focus(); txtQuantity.SelectAll();
                        return;
                    }
                    decQuantity = (decimal.Parse(Qty[0]) * decimal.Parse(Qty[1]));
                }

                // add the item in file
                AddItem(txtBarCode.Text, decQuantity, txtUnitCode.Text, lblProductDesc.Tag.ToString());

                if (mdtItems == null || mdtItems.Rows.Count == 0)   // do a new load for the cloning
                {
                    LoadFile();
                }
                else
                {
                    DataGridViewRow row = (DataGridViewRow)dgvItems.Rows[0].Clone();
                    row.Cells[0].Value = txtBarCode.Text;
                    row.Cells[1].Value = decQuantity.ToString("#,##0.#0");
                    row.Cells[2].Value = txtUnitCode.Text;
                    row.Cells[3].Value = lblProductDesc.Tag.ToString();

                    //remvoe this
                    //dgvItems.Rows.Add(row);
                    //dgvItems.Rows[dgvItems.Rows.Count - 1].Selected = true;

                    mdtItems.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value);
                    dgvItems.Rows[dgvItems.Rows.Count - 1].Selected = true;

                    dgvItems.FirstDisplayedScrollingRowIndex = dgvItems.Rows.Count - 1;
                }
                txtBarCode.Text = "";
                txtQuantity.Text = "0.00";
                txtBarCode.Focus();
            }
            catch (Exception ex)
            {
                mclsEvent.AddErrorEvent(ex);
                throw;
            }
            
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/MainIcon.jpg"); }
            catch { }

            LoadOptions();
        }

        private void Main_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F2:   // clear inventory file
                    if (mclsBranchDetails.BranchID == 0)
                    {
                        MessageBox.Show("Please select the branch to clear the inventory file.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmdBranchSelect.Focus();
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to clear the inventory file of branch " + mclsBranchDetails.BranchCode + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Data.BranchDetails clsBranchDetails = mclsBranchDetails;
                        if (!Directory.Exists("invfiles/backups/")) Directory.CreateDirectory("invfiles/backups/");
                        System.IO.File.Copy(mclsInvLog.LogFile, "invfiles/backups/" + mclsBranchDetails.BranchCode + "_" + DateTime.Now.ToString("yyyyddMMhhmmss") + ".inv");
                        System.IO.File.Delete(mclsInvLog.LogFile);
                        mclsInvLog = new InvLog();
                        LoadOptions();
                        LoadFile();
                        mclsBranchDetails = clsBranchDetails;
                        cmdBranchSelect.Text = mclsBranchDetails.BranchCode;
                    }
                    break;
                case Keys.F3:
                    grpSaveToDB.Visible = false;
                    if (mclsBranchDetails.BranchID == 0)
                    {
                        MessageBox.Show("Please select the branch to zero out the inventory.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmdBranchSelect.Focus();
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to ZERO out the inventory of branch " + mclsBranchDetails.BranchCode + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        grpSaveToDB.Visible = true;
                        Cursor.Current = Cursors.WaitCursor;

                        this.bgwZeroOutInv.RunWorkerAsync();

                        // Wait for the BackgroundWorker to finish the download.
                        while (this.bgwZeroOutInv.IsBusy)
                        {
                            //prgBar.Increment(1);
                            // Keep UI messages moving, so the form remains 
                            // responsive during the asynchronous operation.
                            Application.DoEvents();
                        }
                    }
                    break;
                case Keys.F4:
                    grpSaveToDB.Visible = false;
                    if (mclsBranchDetails.BranchID == 0)
                    {
                        MessageBox.Show("Please select the branch to upload the inventory.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmdBranchSelect.Focus();
                        return;
                    }
                    if (mdtItems == null || mdtItems.Rows.Count == 0)
                    {
                        MessageBox.Show("No rows to upload, please make sure the inventory file is loaded.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to UPLOAD the inventory of branch " + mclsBranchDetails.BranchCode + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        grpSaveToDB.Visible = true;
                        Cursor.Current = Cursors.WaitCursor;

                        this.bgwSavetoDB.RunWorkerAsync();

                        // Wait for the BackgroundWorker to finish the download.
                        while (this.bgwSavetoDB.IsBusy)
                        {
                            //prgBar.Increment(1);
                            // Keep UI messages moving, so the form remains 
                            // responsive during the asynchronous operation.
                            Application.DoEvents();
                        }
                    }
                    break;
                case Keys.F5:
                    grpSaveToDB.Visible = false;
                    if (mclsBranchDetails.BranchID == 0)
                    {
                        MessageBox.Show("Please select the branch to zero out the NEGATIVE inventory.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmdBranchSelect.Focus();
                        return;
                    }
                    if (MessageBox.Show("Are you sure you want to ZERO out the NEGATIVE  inventory of branch " + mclsBranchDetails.BranchCode + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        grpSaveToDB.Visible = true;
                        Cursor.Current = Cursors.WaitCursor;

                        this.bgwZeroOutInvNeg.RunWorkerAsync();

                        // Wait for the BackgroundWorker to finish the download.
                        while (this.bgwZeroOutInvNeg.IsBusy)
                        {
                            //prgBar.Increment(1);
                            // Keep UI messages moving, so the form remains 
                            // responsive during the asynchronous operation.
                            Application.DoEvents();
                        }
                    }
                    break;
                case Keys.Escape:
                    if (MessageBox.Show("Are you sure you want to exit?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void txtBarCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    txtQuantity.Focus(); txtQuantity.SelectAll();
                    break;
            }
        }

        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    cmdSave.Focus();
                    break;
            }
        }

        private void LoadOptions()
        {
            cmdBranchSelect.Text = "press to select";
            txtBarCode.Text = "";
            txtQuantity.Text = "0.00";
            lblProductDesc.Text = "PLEASE SCAN AN ITEM.";
            dgvItems.DataSource = null;
            txtBarCode.Focus();
            txtBarCode.Select();

            mclsBranchDetails = new Data.BranchDetails();
            mdtItems = null;
        }

        private void ReadBarCode()
        {
            try
            {
                if (string.IsNullOrEmpty(txtBarCode.Text))
                {
                    return;
                }

                Data.Products clsProducts = new Data.Products();
                Data.ProductDetails clsProductDetails = clsProducts.Details(txtBarCode.Text);
                clsProducts.CommitAndDispose();

                if (clsProductDetails.ProductID == 0)
                {
                    lblProductDesc.Text = "Item not found in db";
                    lblProductDesc.Tag = "Item not found in db";
                    txtUnitCode.Text = "PCS";
                }
                else
                {
                    if (clsProductDetails.ProductCode != clsProductDetails.ProductDesc)
                        lblProductDesc.Text = clsProductDetails.ProductCode + ": " + clsProductDetails.ProductDesc;
                    else
                        lblProductDesc.Text = clsProductDetails.ProductDesc;
                    lblProductDesc.Tag = clsProductDetails.ProductCode;
                    txtUnitCode.Text = clsProductDetails.BaseUnitCode;
                }
            }
            catch 
            {
 
            }
        }

        private void LoadFile()
        {
            Cursor.Current = Cursors.WaitCursor;

            string invfile = "invfiles/" + mclsBranchDetails.BranchCode + ".inv"; //mclsInvLog.LogFile;

            if (!System.IO.File.Exists(invfile))
            {
                Data.BranchDetails clsBranchDetails = mclsBranchDetails;
                mclsInvLog = new InvLog();
                LoadOptions();
                mclsBranchDetails = clsBranchDetails;
                cmdBranchSelect.Text = mclsBranchDetails.BranchCode;
            }
            else if (System.IO.File.Exists(invfile))
            {
                mdtItems = new System.Data.DataTable("tblInvItems");
                mdtItems.Columns.Add("BarCode");
                mdtItems.Columns.Add("Quantity");
                mdtItems.Columns.Add("Unit");
                mdtItems.Columns.Add("Description");

                using (var reader = new StreamReader(invfile))
                {
                    string line; int iCtr = 0; 
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (iCtr >= 4)
                        {
                            string[] item = line.Split('|');
                            mdtItems.Rows.Add(item[0], item[1], item[2], item[3]);
                        }
                        iCtr++;
                    }
                }

                dgvItems.MultiSelect = false;
                dgvItems.AutoGenerateColumns = true;
                dgvItems.AutoSize = true;
                dgvItems.ScrollBars = ScrollBars.Vertical;
                dgvItems.DataSource = mdtItems.TableName;
                dgvItems.DataSource = mdtItems;

                dgvItems.Columns["BarCode"].Visible = true;
                dgvItems.Columns["Quantity"].Visible = true;
                dgvItems.Columns["Unit"].Visible = true;
                dgvItems.Columns["Description"].Visible = true;

                dgvItems.Columns["BarCode"].HeaderText = "BarCode";
                dgvItems.Columns["Quantity"].HeaderText = "Quantity";
                dgvItems.Columns["Unit"].HeaderText = "Unit";
                dgvItems.Columns["Description"].HeaderText = "Description";

                dgvItems.Columns["BarCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvItems.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvItems.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgvItems.Columns["BarCode"].Width = 230;
                dgvItems.Columns["Quantity"].Width = 100;
                dgvItems.Columns["Unit"].Width = 100;
                dgvItems.Columns["Description"].Width = dgvItems.Width - 450;

                dgvItems.Rows[dgvItems.Rows.Count-1].Selected = true;
                dgvItems.FirstDisplayedScrollingRowIndex = dgvItems.Rows.Count - 1;

                mclsInvLog.LogFile = invfile;
            }
            Cursor.Current = Cursors.Default;
        }

        private void AddItem(string BarCode, decimal decQuantity, string BaseUnitCode, string ProductCode)
        {
            try
            {
                if (mclsInvLog.BranchDetails.BranchCode != mclsBranchDetails.BranchCode)
                {
                    mclsInvLog.BranchDetails = mclsBranchDetails;
                }
                mclsInvLog.AddItem(BarCode, decQuantity, BaseUnitCode, ProductCode);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("being used by another process"))
                {
                    MessageBox.Show("An error has occured while reading inventory file. To avoid damage in the file, system will not exit. Please re-open the system again.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
        }

        private void SaveToDB()
        {
            AceSoft.RetailPlus.Client.MasterDB clsMasterConnection;
			Data.Products clsProducts;
            Data.Inventory clsInventory;
            Data.Database clsDatabase;

            Data.ERPConfig clsERPConfig = new Data.ERPConfig();
            Data.ERPConfigDetails clsERPConfigDetails = clsERPConfig.Details();
            string strReferenceNo = Constants.CLOSE_INVENTORY_CODE + CompanyDetails.BECompanyCode + DateTime.Now.Year.ToString() + clsERPConfig.get_LastClosingNo();
            clsERPConfig.CommitAndDispose();

            Data.ProductDetails clsProductDetails;
            Data.InventoryDetails clsInventoryDetails;

            DateTime dtePostingDate = DateTime.Now;

            if (!Directory.Exists("invfiles/backups/")) Directory.CreateDirectory("invfiles/backups/");
            if (File.Exists("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + ".inv"))
            {
                if (MessageBox.Show("You have already loaded the inventory for this branch today. Please verify the file you are loading. Would you like to continue?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    bgwSavetoDB.ReportProgress(100);
                    return;
                }
            }
            else
            {
                System.IO.File.Copy("invfiles/" + mclsBranchDetails.BranchCode + ".inv", "invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + ".inv");
            }

            InvExLog clsInvExLog = new InvExLog();
            clsInvExLog.BranchDetails = mclsBranchDetails;
            if (File.Exists("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_exc.inv"))
            {
                System.IO.File.Copy("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_exc.inv", "invfiles/backups/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_exc.inv" + "_" + DateTime.Now.ToString("yyyyddMMhhmmss"));
                System.IO.File.Delete("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_exc.inv");
            }

            InvLoadedLog clsInvLoadedLog = new InvLoadedLog();
            clsInvLoadedLog.BranchDetails = mclsBranchDetails;
            if (File.Exists("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_saved.inv"))
            {
                System.IO.File.Copy("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_saved.inv", "invfiles/backups/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_saved.inv" + "_" + DateTime.Now.ToString("yyyyddMMhhmmss"));
                System.IO.File.Delete("invfiles/" + mclsBranchDetails.BranchCode + DateTime.Now.ToString("yyyyMMdd") + "_saved.inv");
            }

            decimal iCtr = 1, iRows = Decimal.Parse(mdtItems.Rows.Count.ToString());
            foreach (System.Data.DataRow dr in mdtItems.Rows)
			{
                string strBarCode = dr["BarCode"].ToString();
                decimal decQuantity = decimal.Parse(dr["Quantity"].ToString());
                string strUnit = dr["Unit"].ToString();
                string strDescription = dr["Description"].ToString();

                mstStatus = "[" + iCtr.ToString() + "/" + iRows + "]Saving " + strBarCode + strDescription;
                bgwSavetoDB.ReportProgress(int.Parse(Math.Ceiling(iCtr / iRows * 100).ToString()));
                iCtr++;

        back:
                clsMasterConnection = new AceSoft.RetailPlus.Client.MasterDB();
                try
                {
                    clsMasterConnection.GetConnection();

                    clsProducts = new Data.Products(clsMasterConnection.Connection, clsMasterConnection.Transaction);
                    clsInventory = new Data.Inventory(clsMasterConnection.Connection, clsMasterConnection.Transaction);

                    clsProductDetails = clsProducts.Details(mclsBranchDetails.BranchID, strBarCode);
                    if (clsProductDetails.ProductID == 0)
                    {
                        clsInvExLog.AddItem(strBarCode, decQuantity, strUnit, strDescription);
                    }
                    else
                    {
                        clsInvLoadedLog.AddItem(strBarCode, decQuantity, strUnit, strDescription);

                        /*******************************************
                        * Add to Inventory
                        * ****************************************/
                        //clsProduct.AddQuantity(lngProductID, decQuantity);
                        //if (lngVariationMatrixID != 0) { clsProductVariationsMatrix.AddQuantity(lngVariationMatrixID, decQuantity); }
                        // July 26, 2011: change the above codes to the following
                        clsProducts.AddQuantity(mclsBranchDetails.BranchID, clsProductDetails.ProductID, 0, decQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_INVENTORY_BY_BRANCH) + " /" + clsProductDetails.BaseUnitCode, DateTime.Now, strReferenceNo, "System");


                        //-- STEP 1: Insert to tblInventory for reporting purposes
                        /*******************************************
                            * Add to Inventory Analysis
                            * ****************************************/
                        clsInventoryDetails = new Data.InventoryDetails();
                        clsInventoryDetails.BranchID = mclsBranchDetails.BranchID;
                        clsInventoryDetails.PostingDateFrom = clsERPConfigDetails.PostingDateFrom;
                        clsInventoryDetails.PostingDateTo = clsERPConfigDetails.PostingDateTo;
                        clsInventoryDetails.PostingDate = dtePostingDate;
                        clsInventoryDetails.ReferenceNo = strReferenceNo;
                        clsInventoryDetails.ContactID = clsProductDetails.SupplierID;
                        clsInventoryDetails.ContactCode = clsProductDetails.SupplierCode;
                        clsInventoryDetails.ProductID = clsProductDetails.ProductID;
                        clsInventoryDetails.ProductCode = clsProductDetails.ProductCode;
                        clsInventoryDetails.VariationMatrixID = 0;
                        clsInventoryDetails.MatrixDescription = "";
                        clsInventoryDetails.ClosingQuantity = clsProductDetails.Quantity;
                        clsInventoryDetails.ClosingActualQuantity = decQuantity + clsProductDetails.Quantity;
                        clsInventoryDetails.ClosingCost = (decQuantity + clsProductDetails.Quantity) * clsProductDetails.PurchasePrice;
                        clsInventoryDetails.ClosingVAT = (decQuantity + clsProductDetails.Quantity) * clsProductDetails.PurchasePrice * decimal.Parse("0.12");	// Purchase Cost with VAT
                        clsInventoryDetails.PurchasePrice = clsProductDetails.PurchasePrice;

                        clsInventory.Insert(clsInventoryDetails);
                    }
                    
                    clsMasterConnection.CommitAndDispose();
                }
                catch (Exception ex) 
                {
                    if (ex.Message.Contains("Deadlock found when trying to get lock; try restarting transaction"))
                    { try { clsMasterConnection.ThrowException(ex); } catch { } clsDatabase = new Data.Database(); clsDatabase.FlushHosts(); clsDatabase.CommitAndDispose(); goto back; }
                    else if (ex.InnerException.Message.Contains("Deadlock found when trying to get lock; try restarting transaction"))
                    { try { clsMasterConnection.ThrowException(ex); } catch { } clsDatabase = new Data.Database(); clsDatabase.FlushHosts(); clsDatabase.CommitAndDispose(); goto back; }
                }
			}
            
            bgwSavetoDB.ReportProgress(100);
        }
    }
}
