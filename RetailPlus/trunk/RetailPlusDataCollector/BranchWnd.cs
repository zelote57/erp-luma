using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.DataCollector
{
    public partial class BranchWnd : Form
    {
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
                    cmdOK_Click(null, null);
                    break;
            }
        }

        private void LoadOptions()
        {
            cboBranch.Items.Clear();
            Data.Branch clsBranch = new Data.Branch();
            foreach (System.Data.DataRow dr in clsBranch.ListAsDataTable().Rows)
            {
                cboBranch.Items.Add(dr["BranchCode"]);
            }
            clsBranch.CommitAndDispose();

            if (cboBranch.Items.Count == 0)
                cboBranch.Items.Add("Please Reload");
            else if (BranchDetails.BranchID != 0)
                cboBranch.SelectedIndex = cboBranch.Items.IndexOf(BranchDetails.BranchCode);
            else
                cboBranch.SelectedIndex = 0;

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Data.Branch clsBranch = new Data.Branch();
            BranchDetails = clsBranch.Details(cboBranch.Text);
            clsBranch.CommitAndDispose();
            Result = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Cancel;
            this.Hide();
        }
    }
}
