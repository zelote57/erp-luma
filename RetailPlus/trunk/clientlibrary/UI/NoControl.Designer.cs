namespace AceSoft.RetailPlus.Client.UI
{
    partial class NoControl
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
            this.txtNo = new System.Windows.Forms.TextBox();
            this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
            this.SuspendLayout();
            // 
            // txtNo
            // 
            this.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNo.Location = new System.Drawing.Point(13, 12);
            this.txtNo.MaxLength = 9;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(271, 33);
            this.txtNo.TabIndex = 16;
            this.txtNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // keyboardNoControl1
            // 
            this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
            this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Clear;
            this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.SelectAll;
            this.keyboardNoControl1.Location = new System.Drawing.Point(42, 62);
            this.keyboardNoControl1.Name = "keyboardNoControl1";
            this.keyboardNoControl1.Size = new System.Drawing.Size(214, 173);
            this.keyboardNoControl1.TabIndex = 17;
            this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
            // 
            // NoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 246);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardNoControl1);
            this.Controls.Add(this.txtNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoControl";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NoControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NoControl_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNo;
        private KeyBoardHook.KeyboardNoControl keyboardNoControl1;
    }
}