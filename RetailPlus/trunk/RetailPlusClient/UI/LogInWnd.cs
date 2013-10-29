using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
    public class LogInWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtUserName;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private DialogResult dialog;
        private Int64 mintUserID;
        private AccessTypes mAccessType;
        private System.Windows.Forms.PictureBox imgIcon;
        private GroupBox groupBox1;
        private Button cmdCancel;
        private Button cmdEnter;
        private string mstrHeader;
        private TextBox txtSelectedtextBox = new TextBox();

        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        public Int64 UserID
        {
            get
            {
                return mintUserID;
            }
            set
            {
                mintUserID = value;
            }
        }

        public string Header
        {
            get
            {
                return lblHeader.Text;
            }
            set
            {
                mstrHeader = value;
            }
        }


        public AccessTypes AccessType
        {
            set
            {
                this.mAccessType = value;
            }
        }


        public LogInWnd()
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.AcceptsTab = true;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.txtPassword.Location = new System.Drawing.Point(319, 123);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = 'l';
            this.txtPassword.Size = new System.Drawing.Size(277, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.GotFocus += new System.EventHandler(this.txtPassword_GotFocus);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(168, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 23);
            this.label2.TabIndex = 19;
            this.label2.Text = "Password";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.MediumBlue;
            this.label14.Location = new System.Drawing.Point(168, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 23);
            this.label14.TabIndex = 17;
            this.label14.Text = "User Name";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.CausesValidation = false;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(94, 24);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(91, 13);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "System access";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(319, 84);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(277, 27);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.GotFocus += new System.EventHandler(this.txtUserName_GotFocus);
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(36, 8);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 14;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 237);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please swipe your access card or Enter user name and password to login.";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AutoSize = true;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(765, 618);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 83);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdEnter
            // 
            this.cmdEnter.AutoSize = true;
            this.cmdEnter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmdEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEnter.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnter.ForeColor = System.Drawing.Color.White;
            this.cmdEnter.Location = new System.Drawing.Point(877, 618);
            this.cmdEnter.Name = "cmdEnter";
            this.cmdEnter.Size = new System.Drawing.Size(106, 83);
            this.cmdEnter.TabIndex = 2;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // LogInWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LogInWnd_Load);
            this.Activated += new System.EventHandler(this.LogInWnd_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogInWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private void LogInWnd_Activated(object sender, System.EventArgs e)
        {
            txtUserName.Focus();
        }
        private void LogInWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.PageUp:
                    SendKeys.Send("+{TAB}");
                    break;

                case Keys.PageDown:
                    SendKeys.Send("{TAB}");
                    break;

                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    Int64 id = LoginUser();
                    if (id != 0)
                    {
                        mintUserID = id;
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    break;
            }
        }

        private Int64 LoginUser()
        {
            string strUserName = txtUserName.Text;
            string strPassword = txtPassword.Text;

            if (strUserName == string.Empty) { txtUserName.Focus(); return 0; }
            else if (strPassword == string.Empty && !strUserName.Contains("|")) { txtPassword.Focus(); return 0; }

            string strName = string.Empty;
            AccessUser clsAccessUser = new AccessUser();
            if (strPassword == string.Empty)
            {
                if (strUserName.Contains("|"))
                {
                    string[] strSplit = strUserName.Split('|');
                    strPassword = strSplit[1].ToString();
                    strUserName = strSplit[0].ToString();
                }
            }

            Int64 iUID = clsAccessUser.Login(strUserName, strPassword, mAccessType, out strName);

            AuditTrail clsAuditTrail = new AuditTrail(clsAccessUser.Connection, clsAccessUser.Transaction);
            AuditTrailDetails[] clsAuditTrailDetails = clsAuditTrail.DetailedList(DateTime.Today, DateTime.MinValue, strName, AccessTypes.None, "FE:%", 1, "ActivityDate", SortOption.Desscending);
            clsAccessUser.CommitAndDispose();

            if (mintUserID != 0)
            {
                if (iUID != mintUserID)
                {
                    switch (iUID)
                    {
                        case 0:
                            iUID = 0; txtUserName.Text = string.Empty; txtPassword.Text = string.Empty;
                            MessageBox.Show("Invalid user name and/or password.", "RetailPlus", MessageBoxButtons.OK);
                            break;
                            
                        default:
                            iUID = 0; txtUserName.Text = string.Empty; txtPassword.Text = string.Empty;
                            MessageBox.Show("Invalid user name and/or password.", "RetailPlus", MessageBoxButtons.OK);
                            break;
                    }
                }
            }
            else
            {
                if (iUID == 0)
                {
                    iUID = 0; txtUserName.Text = string.Empty; txtPassword.Text = string.Empty; txtUserName.Focus();
                    MessageBox.Show("Invalid user name and/or password.", "RetailPlus", MessageBoxButtons.OK);
                }
            }

            if (iUID != 0 && mintUserID == 0 && clsAuditTrailDetails.Length > 0 && mAccessType == AccessTypes.LoginFE)
            {
                if (clsAuditTrailDetails[0].Activity != AccessTypes.LogoutFE.ToString("G") && clsAuditTrailDetails[0].Activity != "Close Terminal")
                {
                    if (clsAuditTrailDetails[0].IPAddress != System.Net.Dns.GetHostName())
                    {
                        if (clsAuditTrailDetails[0].ActivityDate >= DateTime.Now.AddMinutes(-10))
                        {
                            iUID = 0; txtUserName.Text = string.Empty; txtPassword.Text = string.Empty; txtUserName.Focus();
                            MessageBox.Show("You are still doing transaction at " + clsAuditTrailDetails[0].IPAddress + "." + Environment.NewLine +
                                            "Please logout from that terminal first or wait for 1 hour(s) for automatic logout.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            return iUID;
        }

        private void LogInWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/Login.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            if (mstrHeader != string.Empty)
                lblHeader.Text = mstrHeader;
        }
        private void txtUserName_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedtextBox = (TextBox) sender;
        }
        private void txtPassword_GotFocus(object sender, System.EventArgs e)
        {
            txtSelectedtextBox = (TextBox) sender;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            Int64 id = LoginUser();
            if (id != 0)
            {
                mintUserID = id;
                dialog = DialogResult.OK;
                this.Hide();
            }
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

    }
}