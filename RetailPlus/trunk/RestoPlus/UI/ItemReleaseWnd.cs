using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AceSoft.RetailPlus.Security;

namespace AceSoft.RetailPlus.Client.UI
{
    public partial class ItemReleaseWnd : Form
    {
        Event clsEvent = new Event();
        private Data.SalesTransactionDetails mclsSalesTransactionDetails;
        private System.Data.DataTable ItemDataTable;
        private DialogResult dialog;
        private long mlngReleaserID;
        private string mstrReleaserName;

        public DialogResult Result
        {
            get { return dialog; }
        }
        public long ReleaserID
        {
            set { mlngReleaserID = value; }
            get { return mlngReleaserID; }
        }
        public string ReleaserName
        {
            set { mstrReleaserName = value; }
            get { return mstrReleaserName; }
        }
        private string mstTerminalNo;
        public string TerminalNo
        {
            set { mstTerminalNo = value; }
        }

        public ItemReleaseWnd()
        {
            InitializeComponent();
        }

        #region Windows Form Methods

        private void ItemReleaseWnd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    dialog = DialogResult.Cancel;
                    this.Hide();
                    break;

                case Keys.Enter:
                    //  Release transaction if transaction is not empty and item is not selected
                    if (lblTransactionNo.Text != string.Empty && txtScan.Text.Trim() == string.Empty)
                    {
                        ReleaseTransaction();
                    }
                    //  Load transaction if no transaction is loaded yet
                    else if (lblTransactionNo.Text == string.Empty && txtScan.Text.Trim() != string.Empty)
                    {
                        try
                        {
                            LoadTransaction();
                        }
                        catch (Exception ex)
                        { 
                            MessageBox.Show("Sorry you have entered an invalid transaction number." + Environment.NewLine + "Err Desc: " + ex.Message, "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                    }
                    //  Check quantity & match to item if item is not empty
                    else if (lblTransactionNo.Text != string.Empty && txtScan.Text.Trim() != string.Empty)
                    {
                        MatchItem();
                    }
                    break;

                case Keys.Up:
                    MoveItemUp();
                    break;

                case Keys.Down:
                    MoveItemDown();
                    break;

                case Keys.F2:
                    ChangeQuantity();
                    break;

                case Keys.F5:
                    LoadOptions();
                    break;
            }
        }
        private void ItemReleaseWnd_Load(object sender, EventArgs e)
        {
            LoadOptions();
            
            try
            { this.BackgroundImage = new Bitmap(Application.StartupPath + "/images/Background.jpg"); }
            catch { }
            try
            { this.imgIcon.Image = new Bitmap(Application.StartupPath + "/images/ItemRelease.jpg"); }
            catch { }
            try
            { this.cmdCancel.Image = new Bitmap(Application.StartupPath + "/images/blank_small_dark_red.jpg"); }
            catch { }
        }

        #endregion

        #region Windows Control Methods

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void imgIcon_Click(object sender, EventArgs e)
        {
            dialog = DialogResult.Cancel;
            this.Hide();
        }
        private void lblF51_Click(object sender, EventArgs e)
        {
            LoadOptions();
        }
        private void lblF5_Click(object sender, EventArgs e)
        {
            LoadOptions();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Load all defaults, clear all objects.
        /// </summary>
        private void LoadOptions()
        {
            ItemDataTable = new System.Data.DataTable("tblProducts");
            ItemDataTable.Columns.Add("TransactionItemsID");
            ItemDataTable.Columns.Add("ItemNo");
            ItemDataTable.Columns.Add("ProductID");
            ItemDataTable.Columns.Add("ProductCode");
            ItemDataTable.Columns.Add("BarCode");
            ItemDataTable.Columns.Add("Description");
            ItemDataTable.Columns.Add("VariationsMatrixID");
            ItemDataTable.Columns.Add("MatrixDescription");
            ItemDataTable.Columns.Add("Quantity");
            ItemDataTable.Columns.Add("ProductUnitID");
            ItemDataTable.Columns.Add("ProductUnitCode");
            ItemDataTable.Columns.Add("Price");
            ItemDataTable.Columns.Add("Discount");
            ItemDataTable.Columns.Add("ItemDiscount");
            ItemDataTable.Columns.Add("ItemDiscountType");
            ItemDataTable.Columns.Add("Amount");
            ItemDataTable.Columns.Add("VAT");
            ItemDataTable.Columns.Add("EVAT");
            ItemDataTable.Columns.Add("LocalTax");
            ItemDataTable.Columns.Add("ProductGroup");
            ItemDataTable.Columns.Add("ProductSubGroup");
            ItemDataTable.Columns.Add("TransactionItemStat");
            ItemDataTable.Columns.Add("DiscountCode");
            ItemDataTable.Columns.Add("DiscountRemarks");
            ItemDataTable.Columns.Add("ProductPackageID");
            ItemDataTable.Columns.Add("MatrixPackageID");
            ItemDataTable.Columns.Add("PackageQuantity");
            ItemDataTable.Columns.Add("PromoQuantity");
            ItemDataTable.Columns.Add("PromoValue");
            ItemDataTable.Columns.Add("PromoInPercent");
            ItemDataTable.Columns.Add("PromoType");
            ItemDataTable.Columns.Add("PromoApplied");
            ItemDataTable.Columns.Add("PurchasePrice");
            ItemDataTable.Columns.Add("PurchaseAmount");
            ItemDataTable.Columns.Add("IncludeInSubtotalDiscount");
            ItemDataTable.Columns.Add("OrderSlipPrinter");
            ItemDataTable.Columns.Add("OrderSlipPrinted");
            ItemDataTable.Columns.Add("PercentageCommision");
            ItemDataTable.Columns.Add("Commision");
            ItemDataTable.Columns.Add("ScannedQty");
            ItemDataTable.Columns.Add("ScannedAmt");

            dgItems.DataSource = ItemDataTable;
            
            dgStyle.GridColumnStyles["ItemNo"].Width = 65;
            dgStyle.GridColumnStyles["Description"].Width = dgItems.Width - 500; //763 total witdh
            dgStyle.GridColumnStyles["MatrixDescription"].Width = 80;
            dgStyle.GridColumnStyles["Quantity"].Width = 65;
            dgStyle.GridColumnStyles["ProductUnitCode"].Width = 30;
            dgStyle.GridColumnStyles["Amount"].Width = 65;
            dgStyle.GridColumnStyles["ScannedQty"].Width = 90;
            dgStyle.GridColumnStyles["ScannedAmt"].Width = 100;

            lblTransactionNo.Text = string.Empty;
            lblCommand.Text = "Scan Transaction #:";
            lblTotalAmount.Text = "0.00";
            lblTotalScannedAmt.Text = "0.00";
            lblTotal.Text = "TOTAL..."; lblTotal.ForeColor = Color.Blue;

            //txtScan.Text = string.Empty;
            txtScan.Focus();
        }

        /// <summary>
        ///     Load the transaction using the transaction no. scanned in the txtScan.
        /// </summary>
        private void LoadTransaction()
        {
            try
            {
                LoadOptions();
                string strTransactionNo = txtScan.Text.Trim().PadLeft(14, '0');
                Data.SalesTransactions clsTransactions = new Data.SalesTransactions();
                mclsSalesTransactionDetails = clsTransactions.Details(strTransactionNo, mstTerminalNo, Constants.TerminalBranchID);

                if (mclsSalesTransactionDetails.TransactionNo != string.Empty && mclsSalesTransactionDetails.TransactionNo != null)
                {
                    if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Closed)
                    {
                        Data.SalesTransactionItems clsItems = new Data.SalesTransactionItems(clsTransactions.Connection, clsTransactions.Transaction);
                        mclsSalesTransactionDetails.TransactionItems = clsItems.Details(mclsSalesTransactionDetails.TransactionID, mclsSalesTransactionDetails.TransactionDate);
                        clsTransactions.CommitAndDispose();

                        lblTransactionNo.Text = "Transaction #: " + mclsSalesTransactionDetails.TransactionNo;
                        lblTransactionNo.Tag = mclsSalesTransactionDetails.TransactionID.ToString();
                        lblCommand.Tag = mclsSalesTransactionDetails.TransactionDate.ToString("MM/dd/yyyy hh:mm");
                        LoadResumedItems(mclsSalesTransactionDetails.TransactionItems);
                        getTotal();

                        lblCommand.Text = "Scan item:";
                        txtScan.Text = string.Empty;
                        txtScan.Focus();
                    }
                    else {
                        clsTransactions.CommitAndDispose();
                        txtScan.Text = string.Empty;
                        txtScan.Focus();

                        MessageBox.Show("Sorry, you cannot release transaction with status: " + mclsSalesTransactionDetails.TransactionStatus.ToString("G") + ".", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else { clsTransactions.CommitAndDispose(); }
            }
            catch {  }
        }

        /// <summary>
        ///     Set all the details of the SalesTransactionItemDetails in the grdList.
        /// </summary>
        /// <param name="Items"></param>
        private void LoadResumedItems(Data.SalesTransactionItemDetails[] Items)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ItemDataTable.Rows.Clear();

                foreach (Data.SalesTransactionItemDetails item in Items)
                {
                    System.Data.DataRow dr = ItemDataTable.NewRow();

                    dr = setCurrentRowItemDetails(dr, item);
                    dr["ItemNo"] = ItemDataTable.Rows.Count + 1;

                    ItemDataTable.Rows.Add(dr);

                }
                if (ItemDataTable.Rows.Count != 0)
                {
                    dgItems.CurrentRowIndex = 0; 
                    dgItems.Select(dgItems.CurrentRowIndex);
                }

                clsEvent.AddEventLn("Done loading transaction items.", true);

            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("ERROR! Loading transaction items. TRACE: " + ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void getTotal()
        {
            decimal decScannedAmt = 0;
            decimal decScannedQuantity = 0;
            decimal decPrice = 0;

            for (int iRow = 0; iRow < ItemDataTable.Rows.Count; iRow++)
            {
                //dgItems.CurrentRowIndex = iRow;
                decPrice = Convert.ToDecimal(dgItems[iRow, 11].ToString());

                if (dgItems[iRow, 8].ToString().IndexOf("RETURN") != -1)
                {   
                    decScannedQuantity = Convert.ToDecimal(dgItems[iRow, 39].ToString().Replace(" - RETURN", "").Trim());
                    decScannedQuantity = -decScannedQuantity;
                }
                else if (dgItems[iRow, 8].ToString().IndexOf("VOID") != -1)
                {decScannedQuantity = 0;}
                else
                {decScannedQuantity = Convert.ToDecimal(dgItems[iRow, 39].ToString());}

                decScannedAmt += decScannedQuantity * decPrice;
                // decScannedAmt += Convert.ToDecimal(ItemDataTable.Rows[iRow][40].ToString());
            }

            lblTotalAmount.Text = mclsSalesTransactionDetails.SubTotal.ToString("#,###.#0");
            lblTotalScannedAmt.Text = decScannedAmt.ToString("#,##0.#0");

            lblTotal.Text = "TOTAL..."; lblTotal.ForeColor = Color.Blue;
            if (mclsSalesTransactionDetails.SubTotal != decScannedAmt)
            { lblTotal.Text = "TOTAL is not yet equal..."; lblTotal.ForeColor = Color.Red; }

        }

        private Data.SalesTransactionItemDetails getCurrentRowItemDetails()
        {
            try
            {
                Int32 iRow = dgItems.CurrentRowIndex;

                Data.SalesTransactionItemDetails Details = new Data.SalesTransactionItemDetails();

                Details.TransactionID = mclsSalesTransactionDetails.TransactionID;
                Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;
                Details.TransactionItemsID = Convert.ToInt64(dgItems[iRow, 0].ToString());
                Details.ItemNo = dgItems[iRow, 1].ToString();
                Details.ProductID = Convert.ToInt32(dgItems[iRow, 2].ToString());
                Details.ProductCode = dgItems[iRow, 3].ToString();
                Details.BarCode = dgItems[iRow, 4].ToString();
                Details.Description = dgItems[iRow, 5].ToString();
                Details.VariationsMatrixID = Convert.ToInt64(dgItems[iRow, 6].ToString());
                Details.MatrixDescription = dgItems[iRow, 7].ToString();
                if (Details.Description.IndexOf(Environment.NewLine) > -1)
                {
                    Details.Description = Details.Description.Remove(Details.Description.IndexOf(Environment.NewLine), Details.Description.Length - Details.Description.IndexOf(Environment.NewLine));
                }
                if (dgItems[iRow, 8].ToString().IndexOf("RETURN") != -1)
                {
                    Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString().Replace(" - RETURN", "").Trim());
                    Details.ScannedQty = Convert.ToDecimal(dgItems[iRow, 39].ToString().Replace(" - RETURN", "").Trim());
                }
                else if (dgItems[iRow, 8].ToString().IndexOf("VOID") != -1)
                {
                    Details.Quantity = 0;
                    Details.ScannedQty = 0;
                }
                else
                {
                    Details.Quantity = Convert.ToDecimal(dgItems[iRow, 8].ToString());
                    Details.ScannedQty = Convert.ToDecimal(dgItems[iRow, 39].ToString());
                }
                Details.ProductUnitID = Convert.ToInt32(dgItems[iRow, 9].ToString());
                Details.ProductUnitCode = dgItems[iRow, 10].ToString();
                Details.Price = Convert.ToDecimal(dgItems[iRow, 11].ToString());
                Details.Discount = Convert.ToDecimal(dgItems[iRow, 12].ToString());
                Details.ItemDiscount = Convert.ToDecimal(dgItems[iRow, 13].ToString());
                Details.ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dgItems[iRow, 14].ToString());
                Details.Amount = Details.Quantity * Details.Price;//Convert.ToDecimal(dgItems[iRow, 13].ToString()) ;
                Details.ScannedAmt = Details.ScannedQty * Details.Price;//Convert.ToDecimal(dgItems[iRow, 40].ToString());
                Details.VAT = Convert.ToDecimal(dgItems[iRow, 16].ToString());
                Details.EVAT = Convert.ToDecimal(dgItems[iRow, 17].ToString());
                Details.LocalTax = Convert.ToDecimal(dgItems[iRow, 18].ToString());
                Details.ProductGroup = dgItems[iRow, 19].ToString();
                Details.ProductSubGroup = dgItems[iRow, 20].ToString();
                Details.TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dgItems[iRow, 21].ToString());
                Details.DiscountCode = dgItems[iRow, 22].ToString();
                Details.DiscountRemarks = dgItems[iRow, 23].ToString();
                Details.ProductPackageID = Convert.ToInt64(dgItems[iRow, 24].ToString());
                Details.MatrixPackageID = Convert.ToInt64(dgItems[iRow, 25].ToString());
                Details.PackageQuantity = Convert.ToDecimal(dgItems[iRow, 26].ToString());
                Details.PromoQuantity = Convert.ToDecimal(dgItems[iRow, 27].ToString());
                Details.PromoValue = Convert.ToDecimal(dgItems[iRow, 28].ToString());
                Details.PromoInPercent = Convert.ToInt16(dgItems[iRow, 29].ToString());
                Details.PromoType = (PromoTypes)Enum.Parse(typeof(PromoTypes), dgItems[iRow, 30].ToString());
                Details.PromoApplied = Convert.ToDecimal(dgItems[iRow, 31].ToString());
                Details.PurchasePrice = Convert.ToDecimal(dgItems[iRow, 32].ToString());
                Details.PurchaseAmount = Convert.ToDecimal(dgItems[iRow, 33].ToString());
                Details.IncludeInSubtotalDiscount = Convert.ToInt16(dgItems[iRow, 34].ToString());
                Details.OrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dgItems[iRow, 35].ToString());
                Details.OrderSlipPrinted = Convert.ToBoolean(dgItems[iRow, 36].ToString());
                Details.PercentageCommision = Convert.ToDecimal(dgItems[iRow, 37].ToString());
                Details.Commision = Convert.ToDecimal(dgItems[iRow, 38].ToString());
                

                return Details;
            }
            catch (Exception ex)
            {
                Event clsEvent = new Event();
                clsEvent.AddEventLn("ERROR! Getting current row item details. TRACE: " + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        ///     Set the details contained in dr parameter to a selected row in the grdList.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="Details"></param>
        /// <returns></returns>
        private System.Data.DataRow setCurrentRowItemDetails(System.Data.DataRow dr, Data.SalesTransactionItemDetails Details)
        {
            try
            {
                dr["TransactionItemsID"] = Details.TransactionItemsID; //0
                dr["ItemNo"] = Details.ItemNo; //1
                dr["ProductID"] = Details.ProductID; //2
                dr["ProductCode"] = Details.ProductCode; //3
                dr["BarCode"] = Details.BarCode; //4
                dr["Description"] = Details.Description; //5
                dr["VariationsMatrixID"] = Details.VariationsMatrixID; //6
                dr["MatrixDescription"] = Details.MatrixDescription; //7
                dr["Quantity"] = Details.Quantity;	//8
                dr["ProductUnitID"] = Details.ProductUnitID; //9
                dr["ProductUnitCode"] = Details.ProductUnitCode; //10
                dr["Price"] = Details.Price.ToString("###,##0.#0");	//11
                dr["Discount"] = Details.Discount.ToString("###,##0.#0"); //12
                dr["ItemDiscount"] = Details.ItemDiscount.ToString("###,##0.#0");//13
                dr["ItemDiscountType"] = Details.ItemDiscountType.ToString("d");//14
                dr["Amount"] = Details.Amount.ToString("###,##0.#0"); //15
                dr["VAT"] = Details.VAT.ToString("###,##0.#0"); //16
                dr["EVAT"] = Details.EVAT.ToString("###,##0.#0"); //17
                dr["LocalTax"] = Details.LocalTax.ToString("###,##0.#0"); //18
                dr["ProductGroup"] = Details.ProductGroup; //19
                dr["ProductSubGroup"] = Details.ProductSubGroup; //20
                dr["TransactionItemStat"] = Details.TransactionItemStatus.ToString("d"); //21
                dr["DiscountCode"] = Details.DiscountCode; //22
                dr["DiscountRemarks"] = Details.DiscountRemarks; //23
                dr["ProductPackageID"] = Details.ProductPackageID; //24
                dr["MatrixPackageID"] = Details.MatrixPackageID; //25
                dr["PackageQuantity"] = Details.PackageQuantity; //26
                dr["PromoQuantity"] = Details.PromoQuantity; //27
                dr["PromoValue"] = Details.PromoValue; //28
                dr["PromoInPercent"] = Details.PromoInPercent; //29
                dr["PromoType"] = Details.PromoType; //30
                dr["PromoApplied"] = Details.PromoApplied; //31
                dr["PurchasePrice"] = Details.PurchasePrice; //32
                dr["PurchaseAmount"] = Details.PurchaseAmount; //33
                dr["IncludeInSubtotalDiscount"] = Details.IncludeInSubtotalDiscount; //34
                dr["OrderSlipPrinter"] = Details.OrderSlipPrinter; //35
                dr["OrderSlipPrinted"] = Details.OrderSlipPrinted.ToString(); //36
                dr["PercentageCommision"] = Details.PercentageCommision; //37
                dr["Commision"] = Details.Amount * (Details.PercentageCommision / 100); //38
                dr["ScannedQty"] = Details.ScannedQty; //39
                dr["ScannedAmt"] = Convert.ToDecimal((Details.Price * Details.ScannedQty) - Details.Discount - Details.PromoApplied).ToString("#,##0.#0"); //40

                if (Details.TransactionItemStatus == TransactionItemStatus.Void)
                {
                    dr["Quantity"] = "VOID";
                    dr["Price"] = "0.00";
                    dr["Discount"] = "0.00";
                    dr["Amount"] = "0.00";
                    dr["VAT"] = "0.00";
                    dr["EVAT"] = "0.00";
                    dr["LocalTax"] = "0.00";

                    dr["PromoApplied"] = "0.00";
                    dr["PercentageCommision"] = "0.00";
                    dr["Commision"] = "0.00";
                }
                else if (Details.TransactionItemStatus == TransactionItemStatus.Return)
                {
                    dr["Quantity"] = Details.Quantity + " - RETURN";
                    dr["ScannedQty"] = Details.ScannedQty + " - RETURN"; //39
                    if (Details.Amount < 0)
                    {
                        dr["Amount"] = Convert.ToDecimal(-Details.Amount).ToString("###,##0.#0");
                        dr["ScannedAmt"] = Convert.ToDecimal(-((Details.Price * Details.ScannedQty) - Details.Discount - Details.PromoApplied)).ToString("#,##0.#0"); //40
                        dr["PercentageCommision"] = -Details.PercentageCommision;
                        dr["Commision"] = Convert.ToDecimal(Convert.ToDecimal(dr["Commision"]) * -1).ToString("###,##0.#0");
                    }
                }

                return dr;
            }
            catch (Exception ex)
            {
                clsEvent.AddEventLn("ERROR! Setting current row item details. TRACE: " + ex.Message);
                return dr;
            }
        }

        private void ReleaseTransaction()
        {
            decimal decScannedAmt = decimal.Parse(lblTotalScannedAmt.Text);
            if (mclsSalesTransactionDetails.SubTotal != decScannedAmt)
                MessageBox.Show("Transaction subtotal versus scanned amount is not yet equal, you cannot release this transaction.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else 
            {
                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions();
                clsSalesTransactions.Release(long.Parse(lblTransactionNo.Tag.ToString()), mlngReleaserID, mstrReleaserName);
                clsSalesTransactions.CommitAndDispose();
                LoadOptions();
                MessageBox.Show("Transaction has been released.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void MatchItem()
        {
            Data.SalesTransactionItemDetails _SalesTransactionItemDetails  = new Data.SalesTransactionItemDetails();
            int oldindex = dgItems.CurrentRowIndex;
            bool boFound = false;

            for (int iRow=0;iRow<ItemDataTable.Rows.Count;iRow++)
            {
                dgItems.CurrentRowIndex = iRow;
                _SalesTransactionItemDetails = getCurrentRowItemDetails();
                TransactionItemStatus _TransactionItemStatus = (TransactionItemStatus)Enum.Parse(typeof(TransactionItemStatus), dgItems[iRow, 21].ToString());
                if ((_SalesTransactionItemDetails.BarCode == txtScan.Text.Trim() || _SalesTransactionItemDetails.ProductCode.Contains(txtScan.Text.Trim()))  && _TransactionItemStatus == TransactionItemStatus.Valid)
                {
                    _SalesTransactionItemDetails.ScannedQty += 1;
                    ApplyChangeQuantityPriceAmountDetails(iRow, _SalesTransactionItemDetails);
                    
                    //decimal decScannedQty = Convert.ToDecimal(dgItems[iRow, 39].ToString());
                    //decimal decScannedAmt = Convert.ToDecimal(dgItems[iRow, 40].ToString());

                    try { dgItems.UnSelect(0); }
                    catch { }
                    dgItems.UnSelect(oldindex);
                    dgItems.Select(dgItems.CurrentRowIndex);
                    boFound = true;
                    break;

                    //if (decScannedQty == 0) { ApplyChangeQuantityPriceAmountDetails(iRow, Details); break; }
                }
            }
            txtScan.Text = string.Empty;
            if (!boFound ) dgItems.CurrentRowIndex = oldindex;
            getTotal();
        }

        private void ChangeQuantity()
        {
            int iRow = dgItems.CurrentRowIndex;

            if (iRow >= 0)
            {
                if (dgItems[iRow, 8].ToString() != "VOID")
                {
                    Data.SalesTransactionItemDetails Details = getCurrentRowItemDetails();

                    decimal decOldQuantity = Details.Quantity;
                    decimal decScannedQty = Details.ScannedQty;
                    ChangeQuantityWnd QtyWnd = new ChangeQuantityWnd();
                    QtyWnd.Details = Details;

                    QtyWnd.ShowDialog(this);
                    DialogResult result = QtyWnd.Result;
                    Details = QtyWnd.Details;

                    QtyWnd.Close();
                    QtyWnd.Dispose();

                    if (result == DialogResult.OK && decScannedQty != Details.Quantity)
                    {
                        // ChangeQuantityWnd referenced is Quantity column thus need to overwrite with the olf value.
                        Details.ScannedQty = Details.Quantity;
                        Details.Quantity = decOldQuantity;
                        ApplyChangeQuantityPriceAmountDetails(iRow, Details);
                    }
                }
            }
        }

        private void ApplyChangeQuantityPriceAmountDetails(int iRow, Data.SalesTransactionItemDetails Details)
        {
            System.Data.DataRow dr = (System.Data.DataRow)ItemDataTable.Rows[iRow];
            
            dr = setCurrentRowItemDetails(dr, Details);

            getTotal();
        }

        /// <summary>
        ///     Change the highlighted item in the rows if an up arrow is pressed.
        /// </summary>
        private void MoveItemUp()
        {
            if (ItemDataTable.Rows.Count > 0 && dgItems.CurrentRowIndex != 0)
            {
                int oldindex = dgItems.CurrentRowIndex;
                dgItems.CurrentRowIndex -= 1;
                try { dgItems.UnSelect(0); }
                catch { }
                dgItems.UnSelect(oldindex);
                dgItems.Select(dgItems.CurrentRowIndex);
            }
        }

        /// <summary>
        ///     Change the highlighted item in the rows if a down arrow is pressed.
        /// </summary>
        private void MoveItemDown()
        {
            if (dgItems.CurrentRowIndex + 1 < ItemDataTable.Rows.Count && dgItems.CurrentRowIndex + 1 != ItemDataTable.Rows.Count)
            {
                int oldindex = dgItems.CurrentRowIndex;

                dgItems.CurrentRowIndex += 1;
                try { dgItems.UnSelect(0); }
                catch { }
                dgItems.UnSelect(oldindex);
                dgItems.Select(dgItems.CurrentRowIndex);
            }
        }

        #endregion

    }
}