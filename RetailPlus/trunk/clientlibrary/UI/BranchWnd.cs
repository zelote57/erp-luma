using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    public partial class BranchWnd : Form
    {
        public Data.TerminalDetails TerminalDetails { get; set; }
        public Data.BranchDetails BranchDetails { get; set; }
        public DialogResult Result { get; set; }

        public BranchWnd()
        {
            InitializeComponent();
        }

        private void BranchWnd_Load(object sender, EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/MainIcon.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            LoadOptions();
        }

        private void BranchWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    cmdCancel_Click(null, null);
                    break;

                case Keys.Enter:
                    cmdEnter_Click(null, null);
                    break;
            }
        }

        private void txtTerminalNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 iTerminalNo = Int32.Parse(txtTerminalNo.Text);
                if (iTerminalNo > 0)
                {
                    Data.Terminal clsTerminal = new Data.Terminal();
                    TerminalDetails = clsTerminal.Details(txtTerminalNo.Text);
                    BranchDetails = TerminalDetails.BranchDetails;
                    Int32 iBranchCount = clsTerminal.BranchCount(txtTerminalNo.Text);

                    cboBranch.Items.Clear();
                    Data.Branch clsBranch = new Data.Branch(clsTerminal.Connection, clsTerminal.Transaction);
                    foreach (System.Data.DataRow dr in clsBranch.ListAsDataTable(TerminalNo: txtTerminalNo.Text).Rows)
                    {
                        cboBranch.Items.Add(dr["BranchCode"]);
                    }
                    clsTerminal.CommitAndDispose();

                    if (iBranchCount == 0)
                        cboBranch.Items.Add("No Applicable Branch");
                    else if (iBranchCount == 1)
                    {
                        cboBranch.SelectedIndex = cboBranch.Items.IndexOf(BranchDetails.BranchCode);
                        cboBranch.Enabled = false;
                    }
                    else
                    {
                        cboBranch.SelectedIndex = cboBranch.Items.IndexOf(BranchDetails.BranchCode);
                        cboBranch.Enabled = true;
                    }

                    txtTerminalName.Text = TerminalDetails.TerminalName;
                }
            }
            catch { }
        }

        private void LoadOptions()
        {
            cboBranch.Items.Clear();
            Data.Branch clsBranch = new Data.Branch();
            foreach (System.Data.DataRow dr in clsBranch.ListAsDataTable(TerminalNo: TerminalDetails.TerminalNo).Rows)
            {
                cboBranch.Items.Add(dr["BranchCode"]);
            }

            Int32 iBranchCount = new Data.Terminal(clsBranch.Connection, clsBranch.Transaction).BranchCount(txtTerminalNo.Text);

            clsBranch.CommitAndDispose();

            if (iBranchCount == 0)
            {
                cboBranch.Items.Add("No Applicable Branch");
            }
            else if (iBranchCount != 0)
            {
                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(BranchDetails.BranchCode);
                cboBranch.Enabled = false;
            }
            else
            {
                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(BranchDetails.BranchCode);
                cboBranch.Enabled = true;
            }

            txtTerminalName.Text = TerminalDetails.TerminalName;
            txtTerminalNo.Text = TerminalDetails.TerminalNo;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            Data.Terminal clsTerminal = new Data.Terminal();
            TerminalDetails = clsTerminal.Details(txtTerminalNo.Text);

            BranchDetails = TerminalDetails.BranchDetails;

            clsTerminal.CommitAndDispose();

            if (MessageBox.Show("This computer will be configured as TerminalNo: " + txtTerminalNo.Text + " Branch:" + BranchDetails.BranchCode + "-" + BranchDetails.BranchName + ". Sales will be added to " + TerminalDetails.TerminalName + " and Inventory will be deducted to this as well." + Environment.NewLine +
                "Are you sure you want to continue?", "RetailPlus", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (TerminalDetails.TerminalID == 0)
            {
                MessageBox.Show("Sorry the TerminalNo does not belong to branch: " + BranchDetails.BranchCode + "-" + BranchDetails.BranchName + ". Please consult your system administrator", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Result = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }

    }
}
