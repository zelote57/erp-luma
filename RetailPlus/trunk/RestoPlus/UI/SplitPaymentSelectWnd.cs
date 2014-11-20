using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace AceSoft.RetailPlus.Client.UI
{
    public class SplitPaymentSelectWnd : System.Windows.Forms.Form
    {
        private System.ComponentModel.Container components = null;

        #region public Properties

        public Int32 MainWndTop { get; set; }
        public Int32 MainWndLeft { get; set; }
        public DialogResult Result { get; set; }
        public SplitPaymentTypes SplitPaymentType { get; set; }
        private Data.SalesTransactionDetails mDetails = new Data.SalesTransactionDetails();
        private MenuButton cmd2;
        private MenuButton cmd1;
        private MenuButton cmd3;
        private MenuButton cmdCancel;
    
        public Data.SalesTransactionDetails Details
        {
            get
            {
                return mDetails;
            }
        }

        public Data.TerminalDetails mclsTerminalDetails;
        public Data.TerminalDetails TerminalDetails
        {
            set
            {
                mclsTerminalDetails = value;
            }
        }

        #endregion

        #region Constructors and Destructors

        public SplitPaymentSelectWnd()
        {
            InitializeComponent();
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
            this.cmdCancel = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd3 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd1 = new AceSoft.RetailPlus.Client.MenuButton();
            this.cmd2 = new AceSoft.RetailPlus.Client.MenuButton();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.Transparent;
            this.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.GradientBottom = System.Drawing.Color.DarkRed;
            this.cmdCancel.GradientTop = System.Drawing.Color.Red;
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(1, 506);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(92, 51);
            this.cmdCancel.TabIndex = 117;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmd3
            // 
            this.cmd3.BackColor = System.Drawing.Color.Transparent;
            this.cmd3.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd3.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd3.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd3.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd3.Location = new System.Drawing.Point(1, 455);
            this.cmd3.Name = "cmd3";
            this.cmd3.Size = new System.Drawing.Size(92, 51);
            this.cmd3.TabIndex = 116;
            this.cmd3.Text = "Split by \r\nAmount";
            this.cmd3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd3.UseVisualStyleBackColor = false;
            this.cmd3.Click += new System.EventHandler(this.cmd3_Click);
            // 
            // cmd1
            // 
            this.cmd1.BackColor = System.Drawing.Color.Transparent;
            this.cmd1.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd1.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd1.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd1.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd1.Location = new System.Drawing.Point(1, 351);
            this.cmd1.Name = "cmd1";
            this.cmd1.Size = new System.Drawing.Size(92, 51);
            this.cmd1.TabIndex = 115;
            this.cmd1.Text = "Split Evenly";
            this.cmd1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd1.UseVisualStyleBackColor = false;
            this.cmd1.Click += new System.EventHandler(this.cmd1_Click);
            // 
            // cmd2
            // 
            this.cmd2.BackColor = System.Drawing.Color.Transparent;
            this.cmd2.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.cmd2.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd2.GradientBottom = System.Drawing.Color.DarkGreen;
            this.cmd2.GradientTop = System.Drawing.Color.GreenYellow;
            this.cmd2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmd2.Location = new System.Drawing.Point(1, 403);
            this.cmd2.Name = "cmd2";
            this.cmd2.Size = new System.Drawing.Size(92, 51);
            this.cmd2.TabIndex = 114;
            this.cmd2.Text = "Split by Seat";
            this.cmd2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmd2.UseVisualStyleBackColor = false;
            this.cmd2.Click += new System.EventHandler(this.cmd2_Click);
            // 
            // SplitPaymentSelectWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(108, 680);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmd3);
            this.Controls.Add(this.cmd1);
            this.Controls.Add(this.cmd2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "SplitPaymentSelectWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplitPaymentWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SplitPaymentWnd_KeyDown);
            this.ResumeLayout(false);

        }
        #endregion

        #endregion

        #region Windows Form Methods

        private void SplitPaymentWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    this.Result = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    //if (dgItems.CurrentRowIndex < 0)
                    //{
                    //    dialog = DialogResult.Cancel;
                    //    this.Hide();
                    //}
                    //else
                    //{
                    //    dialog = DialogResult.OK;
                    //    if (CreateDetails(dgItems.CurrentRowIndex))
                    //    { this.Hide(); }
                    //}
                    this.Result = DialogResult.Cancel;
                    this.Hide();

                    break;

            }
        }
        
        private void SplitPaymentWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.Top = MainWndTop; }
            catch { }
            try
            { this.Left = MainWndLeft; }
            catch { }

            this.Width = 93;
            this.Height = 676;
        }

        #endregion

        #region Windows Control Methods

        private void cmd1_Click(object sender, EventArgs e)
        {
            this.SplitPaymentType = SplitPaymentTypes.Equally;
            this.Result = DialogResult.OK;
            this.Hide();
        }

        private void cmd2_Click(object sender, EventArgs e)
        {
            this.SplitPaymentType = SplitPaymentTypes.ByItem;
            this.Result = DialogResult.OK;
            this.Hide();
        }

        private void cmd3_Click(object sender, EventArgs e)
        {
            this.SplitPaymentType = SplitPaymentTypes.ByAmount;
            this.Result = DialogResult.OK;
            this.Hide();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Result = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Private Modifiers

        private void LoadOptions()
        {

        }

        private void LoadData()
        {

        }

        #endregion

        
    }
}
