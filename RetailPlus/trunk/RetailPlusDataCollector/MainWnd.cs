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
        private Event mclsEvent = new Event();
        private System.Data.DataTable mdtItems;

        private InvLog mclsInvLog = new InvLog();
        private Data.BranchDetails mclsBranchDetails;

        public MainWnd()
        {
            InitializeComponent();
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
                    if (MessageBox.Show("Are you sure you want to clear the inventory file of branch " + mclsBranchDetails.BranchCode + "?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
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
                case Keys.F4:
                    MessageBox.Show("This feature is not yet implemented.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case Keys.Escape:
                    if (MessageBox.Show("Are you sure you want to exit?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Application.Exit();
                    }
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
    }
}
