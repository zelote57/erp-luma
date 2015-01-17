namespace AceSoft.RetailPlus.Client.UI
{
    partial class BranchWnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTerminalNo = new System.Windows.Forms.TextBox();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.lblDicountTypes = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTerminalName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.grpBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(1, 9);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 89;
            this.imgIcon.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(59, 31);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(152, 13);
            this.lblHeader.TabIndex = 88;
            this.lblHeader.Text = "Terminal re-configuration";
            // 
            // grpBox1
            // 
            this.grpBox1.BackColor = System.Drawing.Color.White;
            this.grpBox1.Controls.Add(this.label2);
            this.grpBox1.Controls.Add(this.txtTerminalName);
            this.grpBox1.Controls.Add(this.label1);
            this.grpBox1.Controls.Add(this.txtTerminalNo);
            this.grpBox1.Controls.Add(this.cboBranch);
            this.grpBox1.Controls.Add(this.lblDicountTypes);
            this.grpBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBox1.ForeColor = System.Drawing.Color.Blue;
            this.grpBox1.Location = new System.Drawing.Point(8, 60);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Padding = new System.Windows.Forms.Padding(1);
            this.grpBox1.Size = new System.Drawing.Size(1008, 334);
            this.grpBox1.TabIndex = 92;
            this.grpBox1.TabStop = false;
            this.grpBox1.Text = "Terminal Configuration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(435, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Enter New Terminal No.";
            // 
            // txtTerminalNo
            // 
            this.txtTerminalNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerminalNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalNo.Location = new System.Drawing.Point(455, 67);
            this.txtTerminalNo.MaxLength = 16;
            this.txtTerminalNo.Name = "txtTerminalNo";
            this.txtTerminalNo.Size = new System.Drawing.Size(98, 30);
            this.txtTerminalNo.TabIndex = 0;
            this.txtTerminalNo.Text = "01";
            this.txtTerminalNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTerminalNo.TextChanged += new System.EventHandler(this.txtTerminalNo_TextChanged);
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBranch.Location = new System.Drawing.Point(343, 234);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(368, 31);
            this.cboBranch.TabIndex = 1;
            // 
            // lblDicountTypes
            // 
            this.lblDicountTypes.AutoSize = true;
            this.lblDicountTypes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDicountTypes.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblDicountTypes.Location = new System.Drawing.Point(424, 218);
            this.lblDicountTypes.Name = "lblDicountTypes";
            this.lblDicountTypes.Size = new System.Drawing.Size(159, 13);
            this.lblDicountTypes.TabIndex = 96;
            this.lblDicountTypes.Text = "Branch of the new terminal";
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
            this.cmdCancel.TabStop = false;
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
            this.cmdEnter.TabStop = false;
            this.cmdEnter.Text = "ENTER";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(435, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 99;
            this.label2.Text = "Terminal Description";
            // 
            // txtTerminalName
            // 
            this.txtTerminalName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTerminalName.Enabled = false;
            this.txtTerminalName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalName.Location = new System.Drawing.Point(343, 156);
            this.txtTerminalName.MaxLength = 16;
            this.txtTerminalName.Name = "txtTerminalName";
            this.txtTerminalName.Size = new System.Drawing.Size(368, 30);
            this.txtTerminalName.TabIndex = 98;
            this.txtTerminalName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BranchWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 764);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.grpBox1);
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "BranchWnd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BranchWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BranchWnd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.grpBox1.ResumeLayout(false);
            this.grpBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTerminalNo;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.Label lblDicountTypes;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdEnter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTerminalName;
    }
}