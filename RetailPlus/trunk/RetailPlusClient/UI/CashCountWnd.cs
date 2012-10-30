using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using AceSoft.RetailPlus.Data;

namespace AceSoft.RetailPlus.Client.UI
{
    public class CashCountWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.DataGrid dgCashCount;
        private System.Windows.Forms.DataGridTableStyle dgStyle;
        private System.Windows.Forms.DataGridTextBoxColumn DenominationID;
        private System.Windows.Forms.DataGridTextBoxColumn ImagePath;
        private System.Windows.Forms.DataGridTextBoxColumn DenominationValue;
        private System.Windows.Forms.DataGridTextBoxColumn DenominationCount;
        private System.Windows.Forms.DataGridTextBoxColumn DenominationAmount;
        private System.Windows.Forms.DataGridTextBoxColumn DenominationCode;

        private System.Windows.Forms.PictureBox imgIcon;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox imgImagePath;
        private System.Windows.Forms.TextBox txtDenominationCount;

        private DialogResult dialog;
        private System.ComponentModel.Container components = null;

        private CashCountDetails[] marrCashCountDetails;
        private Int64 mCashierID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblCashCount;
        private string mCashierName;
        private System.Windows.Forms.Label lblDescription;
        private Button cmdCancel;
        private Button cmdEnter;
        private AceSoft.KeyBoardHook.KeyboardNoControl keyboardNoControl1;
        private decimal mdecAmount;
        private bool mboIsTouchScreen;
        public CashCountDetails[] Details
        {
            get { return marrCashCountDetails; }
        }
        public Int64 CashierID
        {
            set { mCashierID = value; }
        }

        public string CashierName
        {
            set { mCashierName = value; }
        }

        public decimal Amount
        {
            get { return mdecAmount; }
        }

        public DialogResult Result
        {
            get
            {
                return dialog;
            }
        }

        public bool IsTouchScreen
        {
            set
            {
                mboIsTouchScreen = value;
            }
        }

        #region Constructors and Destructors

        public CashCountWnd()
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

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.dgCashCount = new System.Windows.Forms.DataGrid();
            this.dgStyle = new System.Windows.Forms.DataGridTableStyle();
            this.DenominationID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ImagePath = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DenominationValue = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DenominationCount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DenominationAmount = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DenominationCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.imgImagePath = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtDenominationCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCashCount = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.keyboardNoControl1 = new AceSoft.KeyBoardHook.KeyboardNoControl();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCashCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgImagePath)).BeginInit();
            this.SuspendLayout();
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 0;
            this.imgIcon.TabStop = false;
            this.imgIcon.Click += new System.EventHandler(this.imgIcon_Click);
            // 
            // dgCashCount
            // 
            this.dgCashCount.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgCashCount.BackColor = System.Drawing.Color.White;
            this.dgCashCount.BackgroundColor = System.Drawing.Color.White;
            this.dgCashCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgCashCount.CaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgCashCount.CaptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgCashCount.CaptionForeColor = System.Drawing.Color.Blue;
            this.dgCashCount.CaptionVisible = false;
            this.dgCashCount.DataMember = "";
            this.dgCashCount.Enabled = false;
            this.dgCashCount.FlatMode = true;
            this.dgCashCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgCashCount.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(153)))));
            this.dgCashCount.HeaderFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgCashCount.HeaderForeColor = System.Drawing.Color.White;
            this.dgCashCount.Location = new System.Drawing.Point(0, 60);
            this.dgCashCount.Name = "dgCashCount";
            this.dgCashCount.PreferredRowHeight = 50;
            this.dgCashCount.ReadOnly = true;
            this.dgCashCount.RowHeadersVisible = false;
            this.dgCashCount.RowHeaderWidth = 5;
            this.dgCashCount.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.dgCashCount.SelectionForeColor = System.Drawing.Color.White;
            this.dgCashCount.Size = new System.Drawing.Size(722, 526);
            this.dgCashCount.TabIndex = 4;
            this.dgCashCount.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dgStyle});
            this.dgCashCount.TabStop = false;
            // 
            // dgStyle
            // 
            this.dgStyle.AlternatingBackColor = System.Drawing.Color.White;
            this.dgStyle.BackColor = System.Drawing.Color.White;
            this.dgStyle.DataGrid = this.dgCashCount;
            this.dgStyle.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.DenominationID,
            this.ImagePath,
            this.DenominationValue,
            this.DenominationCount,
            this.DenominationAmount,
            this.DenominationCode});
            this.dgStyle.HeaderBackColor = System.Drawing.Color.DarkOrange;
            this.dgStyle.HeaderFont = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgStyle.HeaderForeColor = System.Drawing.Color.White;
            this.dgStyle.MappingName = "tblDenomination";
            this.dgStyle.PreferredColumnWidth = 180;
            this.dgStyle.PreferredRowHeight = 30;
            this.dgStyle.RowHeadersVisible = false;
            this.dgStyle.RowHeaderWidth = 5;
            this.dgStyle.SelectionBackColor = System.Drawing.Color.Green;
            this.dgStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // DenominationID
            // 
            this.DenominationID.Format = "";
            this.DenominationID.FormatInfo = null;
            this.DenominationID.MappingName = "DenominationID";
            this.DenominationID.NullText = "";
            this.DenominationID.ReadOnly = true;
            this.DenominationID.Width = 0;
            // 
            // ImagePath
            // 
            this.ImagePath.Format = "";
            this.ImagePath.FormatInfo = null;
            this.ImagePath.MappingName = "ImagePath";
            this.ImagePath.NullText = "";
            this.ImagePath.ReadOnly = true;
            this.ImagePath.Width = 20;
            // 
            // DenominationValue
            // 
            this.DenominationValue.Format = "#,##0.#0";
            this.DenominationValue.FormatInfo = null;
            this.DenominationValue.HeaderText = "Value";
            this.DenominationValue.MappingName = "DenominationValue";
            this.DenominationValue.ReadOnly = true;
            this.DenominationValue.Width = 0;
            // 
            // DenominationCount
            // 
            this.DenominationCount.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.DenominationCount.Format = "#,##0";
            this.DenominationCount.FormatInfo = null;
            this.DenominationCount.HeaderText = "Enter Deno. Count";
            this.DenominationCount.MappingName = "DenominationCount";
            this.DenominationCount.NullText = "";
            this.DenominationCount.Width = 75;
            // 
            // DenominationAmount
            // 
            this.DenominationAmount.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.DenominationAmount.Format = "#,##0.#0";
            this.DenominationAmount.FormatInfo = null;
            this.DenominationAmount.HeaderText = "Amount";
            this.DenominationAmount.MappingName = "DenominationAmount";
            this.DenominationAmount.NullText = "";
            this.DenominationAmount.ReadOnly = true;
            this.DenominationAmount.Width = 75;
            // 
            // DenominationCode
            // 
            this.DenominationCode.Format = "";
            this.DenominationCode.FormatInfo = null;
            this.DenominationCode.HeaderText = "Denomination";
            this.DenominationCode.MappingName = "DenominationCode";
            this.DenominationCode.NullText = "";
            this.DenominationCode.ReadOnly = true;
            this.DenominationCode.Width = 200;
            // 
            // imgImagePath
            // 
            this.imgImagePath.BackColor = System.Drawing.Color.Blue;
            this.imgImagePath.Location = new System.Drawing.Point(0, 0);
            this.imgImagePath.Name = "imgImagePath";
            this.imgImagePath.Size = new System.Drawing.Size(49, 49);
            this.imgImagePath.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgImagePath.TabIndex = 0;
            this.imgImagePath.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(67, 22);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(103, 13);
            this.lblHeader.TabIndex = 5;
            this.lblHeader.Text = "Enter Cash Count";
            // 
            // txtDenominationCount
            // 
            this.txtDenominationCount.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtDenominationCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDenominationCount.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDenominationCount.ForeColor = System.Drawing.Color.White;
            this.txtDenominationCount.Location = new System.Drawing.Point(728, 206);
            this.txtDenominationCount.MaxLength = 16;
            this.txtDenominationCount.Name = "txtDenominationCount";
            this.txtDenominationCount.Size = new System.Drawing.Size(282, 36);
            this.txtDenominationCount.TabIndex = 0;
            this.txtDenominationCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDenominationCount.Visible = false;
            this.txtDenominationCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDenominationCount_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(592, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 29);
            this.label13.TabIndex = 6;
            this.label13.Text = "CASH COUNT";
            // 
            // lblCashCount
            // 
            this.lblCashCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCashCount.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashCount.ForeColor = System.Drawing.Color.Firebrick;
            this.lblCashCount.Location = new System.Drawing.Point(760, 10);
            this.lblCashCount.Name = "lblCashCount";
            this.lblCashCount.Size = new System.Drawing.Size(170, 25);
            this.lblCashCount.TabIndex = 7;
            this.lblCashCount.Text = "0.00";
            this.lblCashCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(458, 38);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(473, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Tag = "";
            this.lblDescription.Text = "Select the denomination you want to enter, then press the number to enter no. of " +
                "denomination.";
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
            // keyboardNoControl1
            // 
            this.keyboardNoControl1.BackColor = System.Drawing.Color.White;
            this.keyboardNoControl1.commandBlank1 = AceSoft.KeyBoardHook.CommandBlank1.Up;
            this.keyboardNoControl1.commandBlank2 = AceSoft.KeyBoardHook.CommandBlank2.Down;
            this.keyboardNoControl1.Location = new System.Drawing.Point(781, 248);
            this.keyboardNoControl1.Name = "keyboardNoControl1";
            this.keyboardNoControl1.Size = new System.Drawing.Size(202, 176);
            this.keyboardNoControl1.TabIndex = 1;
            this.keyboardNoControl1.TabStop = false;
            this.keyboardNoControl1.UserKeyPressed += new AceSoft.KeyBoardHook.KeyboardDelegate(this.keyboardNoControl1_UserKeyPressed);
            // 
            // CashCountWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.keyboardNoControl1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblCashCount);
            this.Controls.Add(this.txtDenominationCount);
            this.Controls.Add(this.dgCashCount);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "CashCountWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CashCount_Load);
            this.Resize += new System.EventHandler(this.CashCount_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CashCount_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCashCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgImagePath)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Windows Form Methods

        private void CashCount_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                    if (isValuesAssigned())
                    {
                        dialog = DialogResult.OK;
                        this.Hide();
                    }
                    break;

                case Keys.Up:
                    dgCashCount.Focus();
                    dt = (System.Data.DataTable)dgCashCount.DataSource;
                    if (dgCashCount.CurrentRowIndex > 0)
                    {
                        Int32 iDenominationCount = 0;
                        try { iDenominationCount = Convert.ToInt32(txtDenominationCount.Text); }
                        catch { }
                        dgCashCount[dgCashCount.CurrentRowIndex, 3] = iDenominationCount.ToString("#,##0");
                        dgCashCount[dgCashCount.CurrentRowIndex, 4] = Convert.ToDecimal(Convert.ToDecimal(dgCashCount[dgCashCount.CurrentRowIndex, 2]) * iDenominationCount).ToString("#,##0.#0");
                        txtDenominationCount.Text = "";
                        SetCashCount();
                        index = dgCashCount.CurrentRowIndex;

                        dgCashCount.CurrentRowIndex -= 1;
                        dgCashCount.Select(dgCashCount.CurrentRowIndex);
                        dgCashCount.UnSelect(index);
                        txtDenominationCount.Text = dgCashCount[dgCashCount.CurrentRowIndex, 3].ToString().Replace(",", "");
                        txtDenominationCount.SelectAll();
                    }
                    break;

                case Keys.Down:
                    dgCashCount.Focus();
                    dt = (System.Data.DataTable)dgCashCount.DataSource;
                    if (dgCashCount.CurrentRowIndex < dt.Rows.Count - 1)
                    {
                        Int32 iDenominationCount = 0;
                        try { iDenominationCount = Convert.ToInt32(txtDenominationCount.Text); }
                        catch { }
                        dgCashCount[dgCashCount.CurrentRowIndex, 3] = iDenominationCount.ToString("#,##0");
                        dgCashCount[dgCashCount.CurrentRowIndex, 4] = Convert.ToDecimal(Convert.ToDecimal(dgCashCount[dgCashCount.CurrentRowIndex, 2]) * iDenominationCount).ToString("#,##0.#0");

                        SetCashCount();
                        index = dgCashCount.CurrentRowIndex;

                        dgCashCount.CurrentRowIndex += 1;
                        dgCashCount.Select(dgCashCount.CurrentRowIndex);
                        dgCashCount.UnSelect(index);
                        txtDenominationCount.Text = dgCashCount[dgCashCount.CurrentRowIndex, 3].ToString().Replace(",", "");
                        txtDenominationCount.SelectAll();
                    }
                    //SendKeys.SendWait("{DOWN}");
                    break;
                //case Keys.D0:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("0");
                //    }
                //    break;
                //case Keys.D1:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("1");
                //    }
                //    break;
                //case Keys.D2:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("2");
                //    }
                //    break;
                //case Keys.D3:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("3");
                //    }
                //    break;
                //case Keys.D4:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("4");
                //    }
                //    break;
                //case Keys.D5:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("5");
                //    }
                //    break;
                //case Keys.D6:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("6");
                //    }
                //    break;
                //case Keys.D7:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("7");
                //    }
                //    break;
                //case Keys.D8:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("8");
                //    }
                //    break;
                //case Keys.D9:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("9");
                //    }
                //    break;
                //case Keys.NumPad0:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("0");
                //    }
                //    break;
                //case Keys.NumPad1:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("1");
                //    }
                //    break;
                //case Keys.NumPad2:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("2");
                //    }
                //    break;
                //case Keys.NumPad3:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("3");
                //    }
                //    break;
                //case Keys.NumPad4:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("4");
                //    }
                //    break;
                //case Keys.NumPad5:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("5");
                //    }
                //    break;
                //case Keys.NumPad6:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("6");
                //    }
                //    break;
                //case Keys.NumPad7:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("7");
                //    }
                //    break;
                //case Keys.NumPad8:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("8");
                //    }
                //    break;
                //case Keys.NumPad9:
                //    if (txtDenominationCount.Visible == false)
                //    {
                //        txtDenominationCount.Visible = true;
                //        txtDenominationCount.Focus();
                //        SendKeys.Send("9");
                //    }
                //    break;
            }
        }

        private void CashCount_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/CashCount.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            LoadOptions();
            LoadDenominationData();
        }

        private void CashCount_Resize(object sender, System.EventArgs e)
        {
            SetGridItemsWidth();
        }

        private void txtDenominationCount_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Methods clsMethods = new Methods();
            e.Handled = clsMethods.AllNum(Convert.ToInt32(e.KeyChar));
        }

        private void keyboardNoControl1_UserKeyPressed(object sender, AceSoft.KeyBoardHook.KeyboardEventArgs e)
        {
            System.Data.DataTable dt;
            int index;

            if (e.KeyboardKeyPressed.ToString() == "{UP}")
            {
                dgCashCount.Focus();
                dt = (System.Data.DataTable)dgCashCount.DataSource;
                if (dgCashCount.CurrentRowIndex > 0)
                {
                    if (txtDenominationCount.Visible == true)
                    {
                        Int32 iDenominationCount = 0;
                        try { iDenominationCount = Convert.ToInt32(txtDenominationCount.Text); }
                        catch { }
                        dgCashCount[dgCashCount.CurrentRowIndex, 3] = iDenominationCount.ToString("#,##0");
                        dgCashCount[dgCashCount.CurrentRowIndex, 4] = Convert.ToDecimal(Convert.ToDecimal(dgCashCount[dgCashCount.CurrentRowIndex, 2]) * iDenominationCount).ToString("#,##0.#0");
                        txtDenominationCount.Text = "";
                        //txtDenominationCount.Visible = false;
                        SetCashCount();
                    }
                    index = dgCashCount.CurrentRowIndex;

                    dgCashCount.CurrentRowIndex -= 1;
                    dgCashCount.Select(dgCashCount.CurrentRowIndex);
                    dgCashCount.UnSelect(index);
                }
            }
            if (e.KeyboardKeyPressed.ToString() == "{DOWN}")
            {
                dgCashCount.Focus();
                dt = (System.Data.DataTable)dgCashCount.DataSource;
                if (dgCashCount.CurrentRowIndex < dt.Rows.Count - 1)
                {
                    if (txtDenominationCount.Visible == true)
                    {
                        Int32 iDenominationCount = 0;
                        try { iDenominationCount = Convert.ToInt32(txtDenominationCount.Text); }
                        catch { }
                        dgCashCount[dgCashCount.CurrentRowIndex, 3] = iDenominationCount.ToString("#,##0");
                        dgCashCount[dgCashCount.CurrentRowIndex, 4] = Convert.ToDecimal(Convert.ToDecimal(dgCashCount[dgCashCount.CurrentRowIndex, 2]) * iDenominationCount).ToString("#,##0.#0");
                        txtDenominationCount.Text = "";
                        //txtDenominationCount.Visible = false;
                        SetCashCount();
                    }
                    index = dgCashCount.CurrentRowIndex;

                    dgCashCount.CurrentRowIndex += 1;
                    dgCashCount.Select(dgCashCount.CurrentRowIndex);
                    dgCashCount.UnSelect(index);
                }
            }
            else { txtDenominationCount.Focus(); }

            SendKeys.Send(e.KeyboardKeyPressed);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            if (isValuesAssigned())
            {
                dialog = DialogResult.OK;
                this.Hide();
            }
        }

        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        #endregion

        #region Windows Control Methods

        #endregion

        #region Private Methods

        private bool SaveRecord()
        {
            System.Data.DataTable dt = (System.Data.DataTable)dgCashCount.DataSource;

            ArrayList arrCashCountDetails = new ArrayList();
            CashCountDetails clsDetails;
            CashCount clsCashCount = new CashCount();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                clsDetails = new CashCountDetails();
                clsDetails.CashierID = mCashierID;
                clsDetails.CashierName = mCashierName;
                clsDetails.TerminalNo = CompanyDetails.TerminalNo;
                clsDetails.BranchID = Constants.TerminalBranchID;
                clsDetails.DateCreated = DateTime.Now;
                clsDetails.DenominationID = Convert.ToInt32(dr["DenominationID"].ToString().Replace(",", ""));
                clsDetails.DenominationCount = Convert.ToInt32(dr["DenominationCount"].ToString().Replace(",", ""));
                clsDetails.DenominationValue = Convert.ToDecimal(dr["DenominationValue"].ToString().Replace(",", ""));
                clsDetails.DenominationAmount = Convert.ToDecimal(dr["DenominationAmount"].ToString().Replace(",", ""));
                arrCashCountDetails.Add(clsDetails);
            }
            if (arrCashCountDetails != null)
            {
                CashCountDetails[] arrDetails = new CashCountDetails[arrCashCountDetails.Count];
                arrCashCountDetails.CopyTo(arrDetails);

                clsCashCount.GetConnection();
                Terminal clsTerminal = new Terminal(clsCashCount.Connection, clsCashCount.Transaction);
                clsTerminal.UpdateIsCashCountInitialized(Constants.TerminalBranchID, CompanyDetails.TerminalNo, mCashierID, true);

                clsCashCount.Insert(arrDetails);
                clsCashCount.CommitAndDispose();

                marrCashCountDetails = arrDetails;
            }
            MessageBox.Show("Cash Count has been initialized...", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;

        }

        private void LoadOptions()
        {
            txtDenominationCount.Visible = true;
            txtDenominationCount.Focus();

            if (mboIsTouchScreen)
            {
                keyboardNoControl1.Visible = true;
                dgCashCount.Height = 380;
            }
            else
            {
                keyboardNoControl1.Visible = false;
                dgCashCount.Height = 450;
            }

        }

        private void LoadDenominationData()
        {
            try
            {
                Data.Denomination clsDenomination = new Data.Denomination();

                System.Data.DataTable dt = clsDenomination.ListForCashCount("DenominationID", SortOption.Ascending);

                clsDenomination.CommitAndDispose();
                dgCashCount.DataSource = dt;
                dgCashCount.Select(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetGridItemsWidth()
        {
            dgStyle.GridColumnStyles["DenominationID"].Width = 0;

            dgStyle.GridColumnStyles["ImagePath"].Width = 50;
            dgStyle.GridColumnStyles["DenominationValue"].Width = 80;
            dgStyle.GridColumnStyles["DenominationCount"].Width = 130;
            dgStyle.GridColumnStyles["DenominationAmount"].Width = 120;
            dgStyle.GridColumnStyles["DenominationCode"].Width = dgCashCount.Width - 380;
        }

        private void SetCashCount()
        {
            System.Data.DataTable dt = (System.Data.DataTable)dgCashCount.DataSource;

            decimal Amount = 0;
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                try
                {
                    Amount += Convert.ToDecimal(dr["DenominationAmount"]);
                }
                catch { }
            }
            lblCashCount.Text = Amount.ToString("#,##0.#0");
            mdecAmount = Amount;

        }

        private bool isValuesAssigned()
        {
            try
            {
                if (txtDenominationCount.Text.Trim() != string.Empty)
                {
                    Int32 iDenominationCount = 0;
                    try { iDenominationCount = Convert.ToInt32(txtDenominationCount.Text); }
                    catch { }
                    dgCashCount.Focus();
                    dgCashCount[dgCashCount.CurrentRowIndex, 3] = iDenominationCount.ToString("#,##0");
                    dgCashCount[dgCashCount.CurrentRowIndex, 4] = Convert.ToDecimal(Convert.ToDecimal(dgCashCount[dgCashCount.CurrentRowIndex, 2]) * iDenominationCount).ToString("#,##0.#0");
                    SetCashCount();
                }
                if (MessageBox.Show("Warning!!! Cash Count will be intialized...Press OK to continue.", "RetailPlus", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return SaveRecord();
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Sorry you have entered an invalid amount." +
                    "Please type a valid amount.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        #endregion

        private void keyboardNoControl1_Load(object sender, EventArgs e)
        {

        }

    }
}
