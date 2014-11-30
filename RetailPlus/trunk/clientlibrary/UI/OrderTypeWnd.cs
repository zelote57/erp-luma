using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    public partial class OrderTypeWnd : Form
    {
        private DialogResult dialog;
        private OrderTypes mOrderType;

        #region Public Get/Set Properties

        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }
        public OrderTypes orderType
        {
            get
            {
                return mOrderType;
            }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        #endregion

        public OrderTypeWnd()
        {
            InitializeComponent();

            if (Common.isTerminalMultiInstanceEnabled())
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
        }

        private void cmdF1_Click(object sender, EventArgs e)
        {
            AssignValues(OrderTypes.DineIn);
        }

        private void cmdF2_Click(object sender, EventArgs e)
        {
            AssignValues(OrderTypes.TakeOut);
        }

        private void cmdF3_Click(object sender, EventArgs e)
        {
            AssignValues(OrderTypes.Delivery);
        }

        private void LoadOptions()
        {

        }

        private void AssignValues(OrderTypes pvtOrderType)
        {
            dialog = DialogResult.OK;
            mOrderType = pvtOrderType;
            this.Hide();
        }

        private void OrderTypeWnd_Load(object sender, EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Reports.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            {
                this.cmdF1.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF2.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
                this.cmdF3.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_yellow.jpg");
            }
            catch { }

            LoadOptions();
        }

        private void OrderTypeWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    AssignValues(OrderTypes.DineIn);
                    break;

                case Keys.F2:
                    AssignValues(OrderTypes.TakeOut);
                    break;

                case Keys.F3:
                    AssignValues(OrderTypes.Delivery);
                    break;

                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
    }
}