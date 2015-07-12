using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using AceSoft.RetailPlus.Reports;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace AceSoft.RetailPlus.Client.UI
{
    /// <summary>
    /// Summary description for EJournalReportWnd.
    /// </summary>
    public class EJournalReportWnd : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblReportDesc;
        private System.Windows.Forms.PictureBox imgIcon;
        private System.ComponentModel.Container components = null;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer CRViewer;
        private Button cmdCancel;
        private Button cmdEnter;
        

        #region Public Get/Set Properties

        private string mCashierName;
        public string CashierName
        {
            set { mCashierName = value; }
        }

        private DialogResult dialog;
        private Label lblPress;
        private Label lblF2;
        private Label lblAddNewCustomer;
    
        public DialogResult Result
        {
            get { return dialog; }
        }

        private Data.SalesTransactionDetails[] salesDetails;
        public Data.SalesTransactionDetails[] SalesDetails
        {
            set { salesDetails = value; }
        }

        public Data.TerminalDetails TerminalDetails { get; set; }

        #endregion

        #region Constructors and Destructors
        public EJournalReportWnd()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            if (TerminalDetails.MultiInstanceEnabled)
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; }
            else
            { this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; }
            this.ShowInTaskbar = TerminalDetails.FORM_Behavior == FORM_Behavior.NON_MODAL; 
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CRViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.lblReportDesc = new System.Windows.Forms.Label();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdEnter = new System.Windows.Forms.Button();
            this.lblPress = new System.Windows.Forms.Label();
            this.lblF2 = new System.Windows.Forms.Label();
            this.lblAddNewCustomer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.CRViewer);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(9, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 533);
            this.groupBox1.TabIndex = 92;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Electronic Journal Report Details";
            // 
            // CRViewer
            // 
            this.CRViewer.ActiveViewIndex = -1;
            this.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRViewer.Location = new System.Drawing.Point(9, 17);
            this.CRViewer.Name = "CRViewer";
            this.CRViewer.SelectionFormula = "";
            this.CRViewer.ShowCloseButton = false;
            this.CRViewer.ShowExportButton = false;
            this.CRViewer.ShowGotoPageButton = false;
            this.CRViewer.ShowPrintButton = false;
            this.CRViewer.ShowRefreshButton = false;
            this.CRViewer.ShowTextSearchButton = false;
            this.CRViewer.Size = new System.Drawing.Size(990, 510);
            this.CRViewer.TabIndex = 247;
            this.CRViewer.ViewTimeSelectionFormula = "";
            // 
            // lblReportDesc
            // 
            this.lblReportDesc.AutoSize = true;
            this.lblReportDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblReportDesc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportDesc.ForeColor = System.Drawing.Color.White;
            this.lblReportDesc.Location = new System.Drawing.Point(66, 18);
            this.lblReportDesc.Name = "lblReportDesc";
            this.lblReportDesc.Size = new System.Drawing.Size(199, 13);
            this.lblReportDesc.TabIndex = 93;
            this.lblReportDesc.Text = "Electronic Journal Report Window.";
            // 
            // imgIcon
            // 
            this.imgIcon.BackColor = System.Drawing.Color.Blue;
            this.imgIcon.Location = new System.Drawing.Point(9, 5);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(49, 49);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgIcon.TabIndex = 91;
            this.imgIcon.TabStop = false;
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
            this.cmdCancel.TabIndex = 1;
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
            this.cmdEnter.TabIndex = 0;
            this.cmdEnter.Text = "PRINT";
            this.cmdEnter.UseVisualStyleBackColor = true;
            this.cmdEnter.Click += new System.EventHandler(this.cmdEnter_Click);
            // 
            // lblPress
            // 
            this.lblPress.AutoSize = true;
            this.lblPress.BackColor = System.Drawing.Color.Transparent;
            this.lblPress.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblPress.Location = new System.Drawing.Point(764, 41);
            this.lblPress.Name = "lblPress";
            this.lblPress.Size = new System.Drawing.Size(33, 13);
            this.lblPress.TabIndex = 127;
            this.lblPress.Text = "Press";
            // 
            // lblF2
            // 
            this.lblF2.AutoSize = true;
            this.lblF2.BackColor = System.Drawing.Color.Transparent;
            this.lblF2.ForeColor = System.Drawing.Color.Red;
            this.lblF2.Location = new System.Drawing.Point(798, 41);
            this.lblF2.Name = "lblF2";
            this.lblF2.Size = new System.Drawing.Size(41, 13);
            this.lblF2.TabIndex = 126;
            this.lblF2.Text = "[Enter]";
            // 
            // lblAddNewCustomer
            // 
            this.lblAddNewCustomer.AutoSize = true;
            this.lblAddNewCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblAddNewCustomer.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAddNewCustomer.Location = new System.Drawing.Point(838, 41);
            this.lblAddNewCustomer.Name = "lblAddNewCustomer";
            this.lblAddNewCustomer.Size = new System.Drawing.Size(176, 13);
            this.lblAddNewCustomer.TabIndex = 125;
            this.lblAddNewCustomer.Text = " to print the current viewed report.";
            // 
            // EJournalReportWnd
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 766);
            this.ControlBox = false;
            this.Controls.Add(this.lblPress);
            this.Controls.Add(this.lblF2);
            this.Controls.Add(this.lblAddNewCustomer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdEnter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblReportDesc);
            this.Controls.Add(this.imgIcon);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "EJournalReportWnd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EJournalReportWnd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EJournalReportWnd_KeyDown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        #endregion

        #region Windows Form Methods
        private void EJournalReportWnd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    dialog = DialogResult.OK;
                    this.Hide();
                    break;
            }
        }

        private void EJournalReportWnd_Load(object sender, System.EventArgs e)
        {
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/EJournalReport.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_red.jpg"); }
            catch { }
            try
            { this.cmdEnter.Image = new Bitmap(Application.StartupPath + "/images/blank_medium_dark_green.jpg"); }
            catch { }

            this.CRViewer.Zoom(1);

            PopulateEJournalReport();
        }

        #endregion

        #region Private Methods

        private string getHeaderFooter(Reports.ReceiptDetails clsReceiptDetails)
        {
            string stRetValue = "";

            if ((clsReceiptDetails.Text != "" && clsReceiptDetails.Text != null) || (clsReceiptDetails.Value != "" && clsReceiptDetails.Value != null))
            {
                switch (clsReceiptDetails.Orientation)
                {
                    case ReportFormatOrientation.Justify:
                        if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null)
                            stRetValue = GetReceiptFormatParameter(clsReceiptDetails.Value);
                        else
                            stRetValue = clsReceiptDetails.Text.PadRight(13) + ":" + GetReceiptFormatParameter(clsReceiptDetails.Value).PadLeft(TerminalDetails.MaxReceiptWidth - 14);
                        break;
                    case ReportFormatOrientation.Center:
                        if (clsReceiptDetails.Text == "" || clsReceiptDetails.Text == null)
                            stRetValue = CenterString(GetReceiptFormatParameter(clsReceiptDetails.Value), TerminalDetails.MaxReceiptWidth);
                        else
                            stRetValue = CenterString(clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value), TerminalDetails.MaxReceiptWidth);
                        break;
                }
            }

            return stRetValue;
        }
        private string GetReceiptFormatParameter(string stReceiptFormat)
        {
            string stRetValue = "";

            if (stReceiptFormat == ReceiptFieldFormats.Blank)
            {
                stRetValue = "";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Spacer)
            {
                stRetValue = " ";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.InvoiceNo)
            {
                stRetValue = "";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.DateNow)
            {
                stRetValue = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Cashier)
            {
                stRetValue = mCashierName;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TerminalNo)
            {
                stRetValue = TerminalDetails.TerminalNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.MachineSerialNo)
            {
                stRetValue = CONFIG.MachineSerialNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.AccreditationNo)
            {
                stRetValue = CONFIG.AccreditationNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardsPermitNo)
            {
                stRetValue = TerminalDetails.RewardPointsDetails.RewardsPermitNo;
            }
            else
            {
                stRetValue = stReceiptFormat;
            }

            if (stRetValue == null) stRetValue = "";

            return stRetValue;
        }

        private string CenterString(string Value, int TotalLengthOfCenter)
        {
            string stRetValue = Value;
            Int32 lenvalue = Value.Length;

            if (lenvalue <= TotalLengthOfCenter)
            {
                Int32 padding = (int)(TotalLengthOfCenter - lenvalue) / 2;

                for (int i = 0; i < padding; i++)
                { stRetValue = " " + stRetValue + " "; }

                if (((TotalLengthOfCenter - lenvalue) % 2) != 0)
                    stRetValue += " ";
            }
            else
            {
                stRetValue = Value.Substring(0, TotalLengthOfCenter);
            }
            return stRetValue;
        }

        private void PopulateEJournalReport()
        {
            CRSReports.EJournalReport rpt = new CRSReports.EJournalReport();

            SetDataSource(rpt);
            SetParameters(rpt);

            CRViewer.ReportSource = rpt;
            CRViewer.Show();

        }

        #endregion

        #region SetDataSource

        private void SetDataSource(ReportDocument Report)
        {
            AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();

            foreach (Data.SalesTransactionDetails details in salesDetails)
            {
                DataRow drNew = rptds.Transactions.NewRow();

                drNew["TransactionID"] = details.TransactionID;
                drNew["TransactionNo"] = details.TransactionNo;
                drNew["CustomerID"] = details.CustomerID;
                drNew["CashierID"] = details.CashierID;
                drNew["CashierName"] = details.CashierName;
                drNew["TerminalNo"] = details.TerminalNo;
                drNew["TransactionDate"] = details.TransactionDate;
                drNew["DateSuspended"] = details.DateSuspended;
                drNew["DateResumed"] = details.DateResumed;
                drNew["TransactionStatus"] = details.TransactionStatus;
                drNew["SubTotal"] = details.SubTotal;
                drNew["Discount"] = details.Discount;
                drNew["TransDiscount"] = details.TransDiscount;
                drNew["TransDiscountType"] = details.TransDiscountType;
                drNew["VAT"] = details.VAT;
                drNew["VatableAmount"] = details.VATableAmount;
                drNew["EVAT"] = details.EVAT;
                drNew["EVatableAmount"] = details.EVATableAmount;
                drNew["LocalTax"] = details.LocalTax;
                drNew["AmountPaid"] = details.AmountPaid;
                drNew["CashPayment"] = details.CashPayment;
                drNew["ChequePayment"] = details.ChequePayment;
                drNew["CreditCardPayment"] = details.CreditCardPayment;
                drNew["CreditPayment"] = details.CreditPayment;
                drNew["BalanceAmount"] = details.BalanceAmount;
                drNew["ChangeAmount"] = details.ChangeAmount;
                drNew["DateClosed"] = details.DateClosed;
                drNew["PaymentType"] = details.PaymentType;

                rptds.Transactions.Rows.Add(drNew);

                foreach (Data.SalesTransactionItemDetails item in details.TransactionItems)
                {
                    DataRow drNewItem = rptds.SalesTransactionItems.NewRow();

                    drNewItem["TransactionItemsID"] = item.TransactionItemsID;
                    drNewItem["TransactionID"] = item.TransactionID;
                    drNewItem["ProductID"] = item.ProductID;
                    drNewItem["ProductCode"] = item.ProductCode;
                    drNewItem["BarCode"] = item.BarCode;
                    drNewItem["Description"] = item.Description;
                    drNewItem["ProductUnitID"] = item.ProductUnitID;
                    drNewItem["ProductUnitCode"] = item.ProductUnitCode;
                    drNewItem["Quantity"] = item.Quantity;
                    drNewItem["Price"] = item.Price;
                    drNewItem["Discount"] = item.Discount;
                    drNewItem["ItemDiscount"] = item.ItemDiscount;
                    drNewItem["ItemDiscountType"] = item.ItemDiscountType;
                    //					if (item.TransactionItemStatus == TransactionItemStatus.Return)
                    //						drNewItem["Amount"]				= - item.Amount;
                    //					else if (item.TransactionItemStatus == TransactionItemStatus.Refund)
                    //						drNewItem["Amount"]				= - item.Amount;
                    //					else if (item.TransactionItemStatus == TransactionItemStatus.Void)
                    //						drNewItem["Amount"]				= 0;
                    //					else
                    drNewItem["Amount"] = item.Amount;
                    drNewItem["VAT"] = item.VAT;
                    drNewItem["EVAT"] = item.EVAT;
                    drNewItem["LocalTax"] = item.LocalTax;
                    drNewItem["VariationsMatrixID"] = item.VariationsMatrixID;
                    drNewItem["MatrixDescription"] = item.MatrixDescription;
                    drNewItem["ProductGroup"] = item.ProductGroup;
                    drNewItem["ProductSubGroup"] = item.ProductSubGroup;
                    drNewItem["TransactionDate"] = item.TransactionDate;
                    drNewItem["TransactionItemStatus"] = item.TransactionItemStatus;
                    drNewItem["DiscountCode"] = item.DiscountCode;
                    drNewItem["DiscountRemarks"] = item.DiscountRemarks;
                    drNewItem["ProductPackageID"] = item.ProductPackageID;
                    drNewItem["MatrixPackageID"] = item.MatrixPackageID;
                    drNewItem["PackageQuantity"] = item.PackageQuantity;
                    drNewItem["PromoQuantity"] = item.PromoQuantity;
                    drNewItem["PromoValue"] = item.PromoValue;
                    drNewItem["PromoInPercent"] = item.PromoInPercent;
                    drNewItem["PromoType"] = item.PromoType;
                    drNewItem["PromoApplied"] = item.PromoApplied;
                    drNewItem["PurchasePrice"] = item.PurchasePrice;
                    drNewItem["PurchaseAmount"] = item.PurchaseAmount;
                    drNewItem["IncludeInSubtotalDiscount"] = item.IncludeInSubtotalDiscount;

                    rptds.SalesTransactionItems.Rows.Add(drNewItem);
                }
            }
            Report.SetDataSource(rptds);

        }

        #endregion

        #region SetParameters

        private void SetParameters(ReportDocument Report)
        {
            int iCtr = 0;
            string stModule = "";
            Reports.Receipt clsReceipt = new Reports.Receipt();
            //			Reports.ReceiptDetails clsReceiptDetails;

            Reports.ReceiptDetails[] ReportHeader = new Reports.ReceiptDetails[6];

            // print report footer
            for (iCtr = 1; iCtr <= 5; iCtr++)
            {
                stModule = "ReportHeader" + iCtr;
                ReportHeader[iCtr] = clsReceipt.Details(stModule);
            }

            Reports.ReceiptDetails[] ReportFooter = new Reports.ReceiptDetails[6];
            // print report footer
            for (iCtr = 1; iCtr <= 5; iCtr++)
            {
                stModule = "ReportFooter" + iCtr;
                ReportFooter[iCtr] = clsReceipt.Details(stModule);
            }

            clsReceipt.CommitAndDispose();

            ParameterFieldDefinition paramField;
            ParameterValues currentValues;
            ParameterDiscreteValue discreteParam;

            paramField = Report.DataDefinition.ParameterFields["CompanyCode"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = CompanyDetails.CompanyCode;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportHeader1"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportHeader[1]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportHeader2"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportHeader[2]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportHeader3"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportHeader[3]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportHeader4"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportHeader[4]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["CONFIG_ENABLEEVAT"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = TerminalDetails.EnableEVAT;
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportFooter1"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportFooter[1]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportFooter2"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportFooter[2]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

            paramField = Report.DataDefinition.ParameterFields["ReportFooter3"];
            discreteParam = new ParameterDiscreteValue();
            discreteParam.Value = getHeaderFooter(ReportFooter[3]);
            currentValues = new ParameterValues();
            currentValues.Add(discreteParam);
            paramField.ApplyCurrentValues(currentValues);

        }

        #endregion

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }

        private void cmdEnter_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.OK;
            this.Hide();
        }


    }
}
