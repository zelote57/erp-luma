using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Management;

using AceSoft.RetailPlus.Reports;
using AceSoft.RetailPlus.Security;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;

namespace AceSoft.RetailPlus.Client.UI
{
    public class MainWndExtension : Form
    {
        public System.Windows.Forms.Timer tmr;
        public System.Data.DataTable ItemDataTable;

        #region Public Variables

        public string mstrBeginningTransactionNo;
        public string mCashierName;
        public MySqlConnection mConnection;
        public MySqlTransaction mTransaction;
        
        public DateTime mdteOverRidingPrintDate;
        public DateTime mdtCurrentDateTime;

        public bool mboIsRefund;
        public bool mboIsItemHeaderPrinted;
        public bool mboIsInTransaction;
        public bool mboCreditCardSwiped;
        public bool mboRewardCardSwiped;
        public bool mboLocked;
        public bool mbodgItemRowClick;
        public bool mboIsCashCountInitialized;
        public bool mboDoNotPrintTransactionDate;
        
        public Data.SysConfigDetails mclsSysConfigDetails;
        public Data.TerminalDetails mclsTerminalDetails;
        public Data.SalesTransactionDetails mclsSalesTransactionDetails;
        public Data.ContactDetails mclsContactDetails;

        public Event clsEvent = new Event();
        public FilePrinter mclsFilePrinter = new FilePrinter();
        public EJournalPrinter mclsEJournal = new EJournalPrinter();
        public StringBuilder msbToPrint = new StringBuilder();
        public StringBuilder msbEJournalToPrint = new StringBuilder();

        #endregion

        #region Commands
        

        #endregion

        #region Private Modifiers

        

        public void SendStringToTurret(string szString)
        {
            //RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.TurretName, "\f" + szString, "RetailPlus Turret Disp: " + lblTransNo.Text);
            RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.TurretName, "\f" + szString, "RetailPlus Turret Disp: " + mclsSalesTransactionDetails.TransactionNo);
        }

        public Data.SalesTransactionItemDetails ApplyPromo(Data.SalesTransactionItemDetails Details)
        {
            try
            {
                Details.Amount = (Details.Price * Details.Quantity);

                decimal AppliedQuantity = 0;
                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                {
                    if (dr["TransactionItemsID"].ToString() != Details.TransactionItemsID.ToString())
                    {
                        if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && dr["ProductID"].ToString() == Details.ProductID.ToString())
                        {
                            AppliedQuantity += Convert.ToDecimal(dr["Quantity"]);
                        }
                    }
                    if (Details.ItemNo == dr["ItemNo"].ToString())
                    {
                        break;
                    }
                }

                PromoTypes PromoType = PromoTypes.NotApplicable;
                decimal PromoQuantity = 0;
                decimal PromoValue = 0;
                bool PromoInPercent = false;

                Data.PromoItems clsPromoItems = new Data.PromoItems(mConnection, mTransaction);
                mConnection = clsPromoItems.Connection; mTransaction = clsPromoItems.Transaction;

                //bool IsPromoApplied = clsPromoItems.ApplyPromoValue(Convert.ToInt64(lblCustomer.Tag), Details.ProductID, Details.VariationsMatrixID, out PromoType, out PromoQuantity, out PromoValue, out PromoInPercent);
                bool IsPromoApplied = clsPromoItems.ApplyPromoValue(mclsSalesTransactionDetails.CustomerID, Details.ProductID, Details.VariationsMatrixID, out PromoType, out PromoQuantity, out PromoValue, out PromoInPercent);
                clsPromoItems.CommitAndDispose();

                Details.PromoValue = PromoValue;
                Details.PromoQuantity = PromoQuantity;
                Details.PromoInPercent = PromoInPercent;
                Details.PromoType = PromoType;
                Details.PromoApplied = 0;

                if (IsPromoApplied && PromoType != PromoTypes.NotApplicable)
                {
                    Details.PromoApplied = GetPromoApplied(PromoType, Details.Price, Details.Quantity, PromoQuantity, PromoValue, PromoInPercent, AppliedQuantity);
                }
                Details.Amount = Details.Amount - Details.Discount - Details.PromoApplied;
                Details.Commision = Details.Amount * (Details.PercentageCommision / 100);

                return Details;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Applying promo. TRACE: ");
                throw ex;
            }
        }
        public decimal GetPromoApplied(PromoTypes PromoType, decimal Price, decimal Quantity, decimal PromoQuantity, decimal PromoValue, bool InPercent, decimal AppliedQuantity)
        {
            try
            {
                // This is the PromoApplied
                decimal decRetValue = 0;

                int ApplicableQuantity = (int)((Quantity + (AppliedQuantity % PromoQuantity)) / PromoQuantity);

                switch (PromoType)
                {
                    case PromoTypes.ValueOffAfterQtyReached:
                        if (!InPercent)
                        { decRetValue = ApplicableQuantity * PromoValue; }
                        break;
                    case PromoTypes.PercentOffAfterQtyReached:
                        if (InPercent)
                        { decRetValue = ApplicableQuantity * Price * PromoQuantity * (PromoValue / 100); }
                        break;
                }

                return decRetValue;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Computing applicable promo. TRACE: ");
                throw ex;
            }
        }

        public Int64 AddItemToDB(Data.SalesTransactionItemDetails Details)
        {
            try
            {
                Details.TransactionID = mclsSalesTransactionDetails.TransactionID;
                Details.TransactionDate = mclsSalesTransactionDetails.TransactionDate;

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                Int64 TransactionItemsID = clsSalesTransactions.AddItem(mclsSalesTransactionDetails, Details);
                clsSalesTransactions.CommitAndDispose();

                clsEvent.AddEventLn("Adding item no: " + Details.ItemNo + " Barcode".PadRight(15) + ":" + Details.BarCode + " ProductCode".PadRight(15) + ":" + Details.ProductCode + " Price".PadRight(15) + ":" + Details.Price, true);

                return TransactionItemsID;
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Adding sales item to database. TRACE: ");
                throw ex;
            }
        }

        public void ReservedAndCommitItem(Data.SalesTransactionItemDetails Details, TransactionItemStatus _previousTransactionItemStatus)
        {
            // Sep 24, 2011      Lemuel E. Aceron
            // Added order slip wherein all punch items will not change sales and inventory
            // a customer named ORDER SLIP should be defined in contacts
            if (mclsSalesTransactionDetails.CustomerName.ToUpper() != Constants.C_RETAILPLUS_ORDER_SLIP_CUSTOMER && Details.TransactionItemStatus != TransactionItemStatus.Return)
            {
                // Added May 7, 2011 to Cater Reserved and Commit functionality
                //if (mclsTerminalDetails.ReservedAndCommit)
                //{
                Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                Data.ProductUnit clsProductUnit = new Data.ProductUnit(mConnection, mTransaction);
                mConnection = clsProductUnit.Connection; mTransaction = clsProductUnit.Transaction;

                Data.ProductVariationsMatrix clsProductVariationsMatrix = new Data.ProductVariationsMatrix(mConnection, mTransaction);
                decimal decNewQuantity = 0;

                // both refund and normal transaction

                // Sep 14, 2013: remove the reserved and commit for refund. 
                // refund quantity should only reflect for refund after closing of transaction.
                // 
                // return is also not applicable for reserved an commit.
                // return quantity should only reflect for refund after closing of transaction.
                if (!mboIsRefund)
                {

                    // SOLD items that are VOID
                    if (Details.TransactionItemStatus == TransactionItemStatus.Void && _previousTransactionItemStatus != TransactionItemStatus.Return)
                    {
                        decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);

                        clsProduct.SubtractReservedQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_QTY_RESERVE_AND_COMMIT_VOID_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);

                        // Sep 14, 2013: remove
                        // clsProduct.AddQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.ADD_RESERVE_AND_COMMIT_VOID_ITEM), DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                        // remove the ff codes for a change in Jul 26, 2011
                        // clsProduct.AddQuantity(Details.ProductID, decNewQuantity);
                        // 
                        // if (Details.VariationsMatrixID != 0)
                        //     clsProductVariationsMatrix.AddQuantity(Details.VariationsMatrixID, decNewQuantity);
                    }
                    // SOLD items
                    else if (Details.TransactionItemStatus != TransactionItemStatus.Void)
                    {
                        decNewQuantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity * Details.PackageQuantity);
                        if (mclsTerminalDetails.IsParkingTerminal)
                            clsProduct.AddReservedQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.PARKING_IN) + " @ " + (Details.Amount / decNewQuantity).ToString("#,##0.#0") + " Buying: " + Details.PurchasePrice.ToString("#,##0.#0") + " Orig Selling: " + Details.Price.ToString("#,##0.#0") + " Discount: " + (Details.Price - (Details.Amount / decNewQuantity)).ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                        else
                            clsProduct.AddReservedQuantity(Constants.TerminalBranchID, Details.ProductID, Details.VariationsMatrixID, decNewQuantity, Data.Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(Data.PRODUCT_INVENTORY_MOVEMENT.DEDUCT_SOLD_RETAIL) + " @ " + (Details.Amount / decNewQuantity).ToString("#,##0.#0") + " Buying: " + Details.PurchasePrice.ToString("#,##0.#0") + " Orig Selling: " + Details.Price.ToString("#,##0.#0") + " Discount: " + (Details.Price - (Details.Amount / decNewQuantity)).ToString("#,##0.#0") + " to " + mclsSalesTransactionDetails.CustomerName, DateTime.Now, mclsSalesTransactionDetails.TransactionNo, mclsSalesTransactionDetails.CashierName);
                        // remove the ff codes for a change in Jul 26, 2011
                        // clsProduct.SubtractQuantity(Details.ProductID, decNewQuantity);
                        // 
                        // if (Details.VariationsMatrixID != 0)
                        //     clsProductVariationsMatrix.SubtractQuantity(Details.VariationsMatrixID, decNewQuantity);
                    }
                }
                //}
            }
        }
        public void ComputeSubTotal()
        {
            try
            {
                clsEvent.AddEvent("Computing Amounts...");

                decimal decSubTotalDiscount = 0;
                decimal decTransDiscountApplied = mclsSalesTransactionDetails.TransDiscount;
                DiscountTypes DiscountType = mclsSalesTransactionDetails.TransDiscountType;
                decimal decSubTotal = 0;
                decimal decSubTotalDiscountableAmount = 0;
                decimal decItemAmount = 0; //use for the amount with discount applied
                decimal decItemDiscount = 0;
                decimal decVAT = 0;
                decimal decVATableAmount = 0;
                decimal decNonVatableAmount = 0;
                decimal decVatExempt = 0;
                decimal decEVAT = 0;
                decimal decEVATableAmount = 0;
                decimal decNonEVatableAmount = 0;
                decimal decLocalTax = 0;
                decimal decItemSold = 0;
                decimal decQuantitySold = 0;

                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                {
                    DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString());
                    string itemDiscountCode = dr["DiscountCode"].ToString();
                    decimal itemTrueAmt = Convert.ToDecimal(dr["Amount"]);
                    decimal itemVAT = Convert.ToDecimal(dr["VAT"]);
                    decItemDiscount += Convert.ToDecimal(dr["Discount"]);

                    // Sep 4, 2014 remove this. subtotal should reflect the VAT
                    //// feb 8, 2014
                    //// overwrite the itemtrueAmount if it's a SNR and no discount per item yet
                    //if ((mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode || 
                    //     mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode) &&
                    //     string.IsNullOrEmpty(itemDiscountCode))
                    //{
                    //    itemTrueAmt = itemTrueAmt / (1 + (mclsTerminalDetails.VAT / 100));
                    //}
                    decItemAmount = itemTrueAmt;

                    if (dr["Quantity"].ToString().IndexOf("RETURN") != -1) //2. check if the item is return
                    {
                        decItemAmount = decItemAmount * -1;
                        decItemDiscount = decItemDiscount * -1;
                        itemTrueAmt = itemTrueAmt * -1;
                    }
                    else if (dr["Quantity"].ToString().IndexOf("VOID") == -1)
                    {
                        decItemSold += 1;
                        decQuantitySold += Convert.ToDecimal(dr["Quantity"]);
                    }

                    if (DiscountType != DiscountTypes.NotApplicable)
                    {
                        if (ItemDiscountType == DiscountTypes.NotApplicable && dr["Quantity"].ToString().IndexOf("RETURN") == -1 && bool.Parse(dr["IncludeInSubtotalDiscount"].ToString()))
                        {
                            decSubTotalDiscountableAmount += itemTrueAmt;
                        }
                    }

                    decSubTotal += itemTrueAmt;

                    // compute the vat, evat and local tax
                    // march 31, 2006 remove "&& dr["Quantity"].ToString().IndexOf("RETURN") == -1)
                    // june 8, 2006 added && dr["Quantity"].ToString().IndexOf("RETURN") == -1) again.
                    // june 27, 2007 remove "&& dr["Quantity"].ToString().IndexOf("RETURN") == -1) again
                    // Sep 2, 2010 added to include Senior Citizen Discount 
                    if (dr["DiscountCode"].ToString() == mclsTerminalDetails.SeniorCitizenDiscountCode)
                    {
                        decVatExempt += itemTrueAmt + decItemDiscount;
                    }
                    else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode && decItemDiscount == 0)
                    {
                        decVatExempt += itemTrueAmt / (1 + (mclsTerminalDetails.VAT / 100));
                    }
                    else if (Convert.ToDecimal(dr["VAT"]) != 0)
                    {
                        decVATableAmount += decItemAmount;
                    }
                    else if (Convert.ToDecimal(dr["VAT"]) == 0 && dr["Quantity"].ToString().IndexOf("VOID") == -1)
                    {
                        decNonVatableAmount += decItemAmount;
                    }
                    else if (dr["Quantity"].ToString().IndexOf("VOID") != -1)
                    {
//                        string void1 = "void";
                    }
                    else MessageBox.Show("Computation of VAT Warning. Please call the System Administrator immediately.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // compute the EVAT if enabled
                    if (mclsTerminalDetails.EnableEVAT && dr["DiscountCode"].ToString() == mclsTerminalDetails.SeniorCitizenDiscountCode)
                    {
                        decNonEVatableAmount += itemTrueAmt + decItemDiscount;
                    }
                    else if (mclsTerminalDetails.EnableEVAT && mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode && decItemDiscount == 0)
                    {
                        decNonEVatableAmount += itemTrueAmt / (1 + (mclsTerminalDetails.VAT / 100));
                    }
                    else if (mclsTerminalDetails.EnableEVAT && Convert.ToDecimal(dr["VAT"]) != 0)
                    {
                        decEVATableAmount += decItemAmount;
                    }
                    else if (mclsTerminalDetails.EnableEVAT && Convert.ToDecimal(dr["VAT"]) == 0 && dr["Quantity"].ToString().IndexOf("VOID") == -1)
                    {
                        decNonEVatableAmount += decItemAmount;
                    }

                    if (Convert.ToDecimal(dr["LocalTax"]) != 0)
                        decLocalTax += decItemAmount;
                }

                if (DiscountType == DiscountTypes.NotApplicable)
                {
                    decSubTotalDiscount = 0;
                }
                else if (DiscountType == DiscountTypes.FixedValue)
                {
                    decSubTotalDiscount = mclsSalesTransactionDetails.TransDiscount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                {
                    decSubTotalDiscount = (decSubTotalDiscountableAmount / (1 + (mclsTerminalDetails.VAT / 100)) * (mclsSalesTransactionDetails.TransDiscount / 100));
                    mclsSalesTransactionDetails.SNRDiscount = decSubTotalDiscount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode)
                {
                    decSubTotalDiscount = (decSubTotalDiscountableAmount * (mclsSalesTransactionDetails.TransDiscount / 100));
                    mclsSalesTransactionDetails.PWDDiscount = decSubTotalDiscount;
                }
                else if (DiscountType == DiscountTypes.Percentage)
                {
                    decSubTotalDiscount = (decSubTotalDiscountableAmount * (mclsSalesTransactionDetails.TransDiscount / 100));
                }
                //	gross sales = net sales + discount;
                //	net sales = gross sales - vat - discount

                if (!mclsTerminalDetails.IsVATInclusive)
                {
                    if (decVATableAmount >= decSubTotalDiscount) decVATableAmount = decVATableAmount - decSubTotalDiscount; else decVATableAmount = 0;
                    if (decEVATableAmount >= decSubTotalDiscount) decEVATableAmount = decEVATableAmount - decSubTotalDiscount; else decEVATableAmount = 0;
                    if (decLocalTax >= decSubTotalDiscount) decLocalTax = decLocalTax - decSubTotalDiscount; else decLocalTax = 0;
                }
                else
                {
                    if (decVATableAmount >= decSubTotalDiscount) decVATableAmount = (decVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decVATableAmount = 0;
                    if (decEVATableAmount >= decSubTotalDiscount) decEVATableAmount = (decEVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decEVATableAmount = 0;
                    if (decLocalTax >= decSubTotalDiscount) decLocalTax = (decLocalTax - decSubTotalDiscount) / (1 + (mclsTerminalDetails.LocalTax / 100)); else decLocalTax = 0;

                    //// Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
                    //if (decNONVATableAmount >= decSubTotalDiscount) decNONVATableAmount = (decNONVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decNONVATableAmount = 0;
                    //if (decNONEVATableAmount >= decSubTotalDiscount) decNONEVATableAmount = (decNONEVATableAmount - decSubTotalDiscount) / (1 + (mclsTerminalDetails.VAT / 100)); else decNONEVATableAmount = 0;
                }

                // Sep 2, 2010 added to include Senior Citizen Discount as nonvatable
                if (decNonVatableAmount >= decSubTotalDiscount) decNonVatableAmount = decNonVatableAmount - decSubTotalDiscount; else decNonVatableAmount = 0;
                if (decNonEVatableAmount >= decSubTotalDiscount) decNonEVatableAmount = decNonEVatableAmount - decSubTotalDiscount; else decNonEVatableAmount = 0;

                decVAT = decVATableAmount * (mclsTerminalDetails.VAT / 100);
                decEVAT = decEVATableAmount * (mclsTerminalDetails.EVAT / 100);
                decLocalTax = decLocalTax * (mclsTerminalDetails.LocalTax / 100);

                if (!mclsTerminalDetails.IsVATInclusive) decSubTotal += decVAT + decLocalTax;
                if (!mclsTerminalDetails.EnableEVAT) decSubTotal += decEVAT;


                // ****** BYPASS ****//
                long lngCount = 7;
                try { lngCount = long.Parse(System.Configuration.ConfigurationManager.AppSettings["CustomerCount"]); }
                catch { }

                if (ItemDataTable.Rows.Count >= lngCount)
                {
                    try
                    {
                        string[] strCustomerIDs = System.Configuration.ConfigurationManager.AppSettings["CustomerIDs"].ToLower().Split(':');
                        foreach (string strCustomerID in strCustomerIDs)
                        {
                            decimal decDiscount = decimal.Parse("0.30");

                            try { decDiscount = decimal.Parse(System.Configuration.ConfigurationManager.AppSettings["CustomerIDsX"]); }
                            catch { }

                            if (strCustomerID == mclsSalesTransactionDetails.CustomerID.ToString())
                            {
                                decSubTotal = decSubTotal * (decimal.Parse("1") - decDiscount);
                                decSubTotalDiscount = decSubTotalDiscount * (decimal.Parse("1") - decDiscount);
                                decSubTotalDiscountableAmount = decSubTotalDiscountableAmount * (decimal.Parse("1") - decDiscount);
                                decItemDiscount = decItemDiscount * (decimal.Parse("1") - decDiscount);
                                decVAT = decVAT * (decimal.Parse("1") - decDiscount);
                                decVATableAmount = decVATableAmount * (decimal.Parse("1") - decDiscount);
                                decVatExempt = decVatExempt * (decimal.Parse("1") - decDiscount);
                                decNonVatableAmount = decNonVatableAmount * (decimal.Parse("1") - decDiscount);
                                decEVAT = decEVAT * (decimal.Parse("1") - decDiscount);
                                decEVATableAmount = decEVATableAmount * (decimal.Parse("1") - decDiscount);
                                decNonEVatableAmount = decNonEVatableAmount * (decimal.Parse("1") - decDiscount);
                                decLocalTax = decLocalTax * (decimal.Parse("1") - decDiscount);

                                break;
                            }
                        }
                    }
                    catch { }
                }

                if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.NotApplicable)
                {
                    mclsSalesTransactionDetails.Charge = 0;
                    mclsSalesTransactionDetails.ChargeAmount = 0;
                }
                else if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.FixedValue)
                {
                    mclsSalesTransactionDetails.Charge = mclsSalesTransactionDetails.ChargeAmount;
                }
                else if (mclsSalesTransactionDetails.ChargeType == ChargeTypes.Percentage)
                {
                    mclsSalesTransactionDetails.Charge = decSubTotal * (mclsSalesTransactionDetails.ChargeAmount / 100);
                }

                decimal decSubtotalVAT = 0;
                if ((mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode) && mclsSalesTransactionDetails.DiscountableAmount != 0)
                {
                    // recompute coz VAT is zero
                    decSubtotalVAT = (decSubTotalDiscountableAmount / (1 + (mclsTerminalDetails.VAT / 100))) * (mclsTerminalDetails.VAT / 100);
                }
                else
                {
                    decSubtotalVAT = mclsSalesTransactionDetails.VAT;
                }

                mclsSalesTransactionDetails.AmountDue = decSubTotal - decSubtotalVAT - decSubTotalDiscount + mclsSalesTransactionDetails.Charge + mclsSalesTransactionDetails.VAT;
                mclsSalesTransactionDetails.NetSales = decSubTotal - (decVatExempt * decimal.Parse("0.12")) - decSubTotalDiscount;
                mclsSalesTransactionDetails.SubTotal = decSubTotal;
                mclsSalesTransactionDetails.Discount = decSubTotalDiscount;
                mclsSalesTransactionDetails.DiscountableAmount = decSubTotalDiscountableAmount;
                mclsSalesTransactionDetails.ItemsDiscount = decItemDiscount;
                mclsSalesTransactionDetails.VATExempt = decVatExempt;
                mclsSalesTransactionDetails.NonVATableAmount = decNonVatableAmount;
                mclsSalesTransactionDetails.VATableAmount = decVATableAmount;
                mclsSalesTransactionDetails.VAT = decVAT;
                mclsSalesTransactionDetails.EVAT = decEVAT;
                mclsSalesTransactionDetails.EVATableAmount = decEVATableAmount;
                mclsSalesTransactionDetails.NonEVATableAmount = decNonEVatableAmount;
                mclsSalesTransactionDetails.LocalTax = decLocalTax;
                mclsSalesTransactionDetails.ItemSold = decItemSold;
                mclsSalesTransactionDetails.QuantitySold = decQuantitySold;

                clsEvent.AddEventLn("Done computing amount for transaction #".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionNo);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Computing sales subtotal. TRACE: ");
            }
        }

        public DialogResult GetWriteAccessAndLogin(Int64 UID, AccessTypes AccessType, string OverridingHeader = "")
        {
            DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessType);

            if (loginresult == DialogResult.None)
            {
                string strHeader = OverridingHeader;

                if (string.IsNullOrEmpty(strHeader))
                {
                    switch (AccessType)
                    {
                        case AccessTypes.PrintTransactionHeader: strHeader = "Print Transaction Header"; break;
                        case AccessTypes.ChangeQuantity: strHeader = "Change Quantity"; break;
                        case AccessTypes.ChangePrice: strHeader = "Change Price"; break;
                        case AccessTypes.ReturnItem: strHeader = "Return Item Access"; break;
                        case AccessTypes.ApplyItemDiscount: strHeader = "Apply Item Discount"; break;
                        case AccessTypes.Contacts: strHeader = "Update customer information"; break;
                        case AccessTypes.SuspendTransaction: strHeader = "Suspend Transaction No. " + mclsSalesTransactionDetails.TransactionNo; break;
                        case AccessTypes.ResumeTransaction: strHeader = "Resume Suspended Transaction"; break;
                        case AccessTypes.VoidTransaction: strHeader = "Void Transaction No. " + mclsSalesTransactionDetails.TransactionNo; break;
                        case AccessTypes.Withhold: strHeader = "WithHold Amount"; break;
                        case AccessTypes.Disburse: strHeader = "Disburse Amount"; break;
                        case AccessTypes.PaidOut: strHeader = "Paid-Out Amount"; break;
                        case AccessTypes.MallForwarder: strHeader = "Mall Data Forwarder"; break;
                        case AccessTypes.VoidItem: strHeader = "Void Item"; break;
                        case AccessTypes.CashCount: strHeader = "Issue Cash Count"; break;
                        case AccessTypes.EnterFloat: strHeader = "Enter Float or Beginning Balance"; break;
                        case AccessTypes.InitializeZRead: strHeader = "Initialize Z-Read"; break;
                        case AccessTypes.CreateTransaction: strHeader = "Create Transaction"; break;
                        case AccessTypes.CloseTransaction: strHeader = "Close Transaction"; break;
                        case AccessTypes.ReleaseItems: strHeader = "Release Items"; break;
                        case AccessTypes.LogoutFE: strHeader = "Logout"; break;
                        case AccessTypes.ApplyTransDiscount: strHeader = "Apply Transaction Discount"; break;
                        case AccessTypes.ChargeType: strHeader = "Apply Transaction Charge"; break;
                        case AccessTypes.OpenDrawer: strHeader = "Open Drawer"; break;
                        case AccessTypes.CreditPayment: strHeader = "Enter Credit Payment"; break;
                        case AccessTypes.RefundTransaction: strHeader = "Refund Transaction"; break;
                        case AccessTypes.RewardCardIssuance: strHeader = "Issue new Reward Card"; break;
                        case AccessTypes.RewardCardChange: strHeader = "Reward Card Replacement"; break;
                        case AccessTypes.CreditCardIssuance: strHeader = "Issue new Credit Card"; break;
                        case AccessTypes.CreditCardChange: strHeader = "Credit Card Replacement"; break;
                        case AccessTypes.PackUnpackTransaction: strHeader = "Pack/Unpack Transaction Access Validation"; break;
                        case AccessTypes.ReprintTransaction: strHeader = "Reprint Transaction Access Validation"; break;
                        case AccessTypes.PrintZRead: strHeader = "Print ZRead Access Validation"; break;
                        case AccessTypes.PrintXRead: strHeader = "Print XRead Access Validation"; break;
                        case AccessTypes.PrintHourlyReport: strHeader = "Print Hourly Report Access Validation"; break;
                        case AccessTypes.PrintGroupReport: strHeader = "Print Group/Dept. Report Access Validation"; break;
                        case AccessTypes.PrintPLUReport: strHeader = "Print PLU Report Access Validation"; break;
                        case AccessTypes.PrintElectronicJournal: strHeader = "Print EJournal Report Access Validation"; break;
                    }
                }
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.ResumeTransaction;
                login.Header = strHeader;
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();
            }
            return loginresult;
        }
        private DialogResult GetWriteAccess(Int64 UID, AccessTypes accesstype)
        {
            DialogResult resRetValue = DialogResult.None;

            AccessRights clsAccessRights = new AccessRights(mConnection, mTransaction);
            AccessRightsDetails clsDetails = new AccessRightsDetails();

            clsDetails = clsAccessRights.Details(UID, (Int16)accesstype);

            if (clsDetails.Write)
            {
                resRetValue = DialogResult.OK;
            }

            clsAccessRights.CommitAndDispose();

            return resRetValue;
        }
        public void SavePayments(ArrayList arrCashPaymentDetails, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrCreditPaymentDetails, ArrayList arrDebitPaymentDetails)
        {
            //need to remove this eventually
            mclsSalesTransactionDetails.PaymentDetails = AssignArrayListPayments(arrCashPaymentDetails, arrChequePaymentDetails, arrCreditCardPaymentDetails, arrCreditPaymentDetails, arrDebitPaymentDetails);

            if (mboIsRefund)
            {
                // Lemu 2011-06-09 : Added saving of debit payments as deposit if refund. Requested by Frank.
                Data.Deposits clsDeposit = new Data.Deposits(mConnection, mTransaction);
                mConnection = clsDeposit.Connection; mTransaction = clsDeposit.Transaction;

                Data.DepositDetails clsDepositDetails = new Data.DepositDetails();
                foreach (Data.DebitPaymentDetails clsDebitPaymentDetails in mclsSalesTransactionDetails.PaymentDetails.arrDebitPaymentDetails)
                {
                    clsDepositDetails = new Data.DepositDetails();
                    clsDepositDetails.Amount = clsDebitPaymentDetails.Amount;
                    clsDepositDetails.PaymentType = PaymentTypes.Debit;
                    clsDepositDetails.DateCreated = mclsSalesTransactionDetails.TransactionDate;
                    clsDepositDetails.BranchDetails = new Data.BranchDetails
                    {
                        BranchID = mclsTerminalDetails.BranchID
                    };
                    clsDepositDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
                    clsDepositDetails.CashierID = mclsSalesTransactionDetails.CashierID;
                    clsDepositDetails.ContactID = mclsSalesTransactionDetails.CustomerID;
                    clsDepositDetails.ContactName = mclsSalesTransactionDetails.CustomerName;
                    clsDepositDetails.Remarks = "Added during refund of transaction #: " + mclsSalesTransactionDetails.TransactionNo;

                    clsDeposit.Insert(clsDepositDetails);
                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    clsContact.AddDebit(clsDepositDetails.ContactID, clsDepositDetails.Amount);
                }
                clsDeposit.CommitAndDispose();

                InsertAuditLog(AccessTypes.Deposit, "Deposit: type='" + clsDepositDetails.PaymentType.ToString("G") + "' amount='" + clsDepositDetails.Amount.ToString(",##0.#0") + "'" + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);

                // Remove Debit Payments so that it wont be saved in the debit payment table
                mclsSalesTransactionDetails.PaymentDetails.arrDebitPaymentDetails = new Data.DebitPaymentDetails[0];
            }

            Data.Payment clsPayment = new Data.Payment(mConnection, mTransaction);
            mConnection = clsPayment.Connection; mTransaction = clsPayment.Transaction;

            clsPayment.Insert(mclsSalesTransactionDetails.PaymentDetails);

            // CreditCardPaymentDetails : Inhouse
        }

        public Data.PaymentDetails AssignArrayListPayments(ArrayList arrCashPaymentDetails, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrCreditPaymentDetails, ArrayList arrDebitPaymentDetails)
        {
            Data.CashPaymentDetails[] CashPaymentDetails = new Data.CashPaymentDetails[0];
            if (arrCashPaymentDetails.Count > 0)
            {
                CashPaymentDetails = new Data.CashPaymentDetails[arrCashPaymentDetails.Count];
                arrCashPaymentDetails.CopyTo(CashPaymentDetails);
            }

            Data.ChequePaymentDetails[] ChequePaymentDetails = new Data.ChequePaymentDetails[0];
            if (arrChequePaymentDetails.Count > 0)
            {
                ChequePaymentDetails = new Data.ChequePaymentDetails[arrChequePaymentDetails.Count];
                arrChequePaymentDetails.CopyTo(ChequePaymentDetails);
            }

            Data.CreditCardPaymentDetails[] CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[0];
            if (arrCreditCardPaymentDetails.Count > 0)
            {
                CreditCardPaymentDetails = new Data.CreditCardPaymentDetails[arrCreditCardPaymentDetails.Count];
                arrCreditCardPaymentDetails.CopyTo(CreditCardPaymentDetails);
            }

            Data.CreditPaymentDetails[] CreditPaymentDetails = new Data.CreditPaymentDetails[0];
            if (arrCreditPaymentDetails != null && arrCreditPaymentDetails.Count > 0)
            {
                CreditPaymentDetails = new Data.CreditPaymentDetails[arrCreditPaymentDetails.Count];
                arrCreditPaymentDetails.CopyTo(CreditPaymentDetails);
            }

            Data.DebitPaymentDetails[] DebitPaymentDetails = new Data.DebitPaymentDetails[0];
            if (arrDebitPaymentDetails != null && arrDebitPaymentDetails.Count > 0)
            {
                DebitPaymentDetails = new Data.DebitPaymentDetails[arrDebitPaymentDetails.Count];
                arrDebitPaymentDetails.CopyTo(DebitPaymentDetails);
            }

            Data.PaymentDetails Details = new Data.PaymentDetails();
            Details.TransactionID = mclsSalesTransactionDetails.TransactionID;
            Details.arrCashPaymentDetails = CashPaymentDetails;
            Details.arrChequePaymentDetails = ChequePaymentDetails;
            Details.arrCreditCardPaymentDetails = CreditCardPaymentDetails;
            Details.arrCreditPaymentDetails = CreditPaymentDetails;
            Details.arrDebitPaymentDetails = DebitPaymentDetails;

            return Details;
        }

        public void AddToReprintedTransaction(string strTransactionNo, string pstrTerminalNo)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEvent("Adding To no of reprinted transaction : " + strTransactionNo);

                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                clsTerminalReport.UpdateReprintedTransaction(mclsTerminalDetails.BranchID, pstrTerminalNo, strTransactionNo);

                clsTerminalReport.CommitAndDispose();

                clsEvent.AddEventLn("Done");
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! adding to reprinted transaction. TRACE: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #region UpdateTerminalReport

        //public delegate void UpdateTerminalReportDelegate(TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VATableAmount, decimal NonVATableAmount, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, PaymentTypes PaymentType);
        public void UpdateTerminalReport(TransactionStatus TransStatus, decimal ItemSold, decimal QuantitySold, decimal SubTotal, decimal Discount, decimal ItemsDiscount, decimal Charge, decimal VAT, decimal VATableAmount, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, PaymentTypes PaymentType)
        {
            Int32 intNoOfCashTransactions = 0;
            Int32 intNoOfChequeTransactions = 0;
            Int32 intNoOfCreditCardTransactions = 0;
            Int32 intNoOfCreditTransactions = 0;
            Int32 intNoOfDebitTransactions = 0;
            Int32 intNoOfCombinationPaymentTransactions = 0;
            Int32 intNoOfDiscountedTransactions = 0;
            Int32 intNoOfRewardPointsPayment = 0;
            decimal decPromotionalItems = 0;
            
            foreach (System.Data.DataRow dr in ItemDataTable.Rows)
            {

                decimal ItemQuantity = 0;
                try { ItemQuantity = Convert.ToDecimal(dr["Quantity"]); }
                catch
                {
                    try { ItemQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace("RETURN", "").Trim()); }
                    catch { }
                }

                decPromotionalItems += Convert.ToDecimal(dr["PromoApplied"]);

                DiscountTypes ItemDiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), dr["ItemDiscountType"].ToString().ToString());
            }

            switch (PaymentType)
            {
                case PaymentTypes.Cash: { intNoOfCashTransactions = 1; break; }
                case PaymentTypes.Cheque: { intNoOfChequeTransactions = 1; break; }
                case PaymentTypes.CreditCard: { intNoOfCreditCardTransactions = 1; break; }
                case PaymentTypes.Credit: { intNoOfCreditTransactions = 1; break; }
                case PaymentTypes.Debit: { intNoOfDebitTransactions = 1; break; }
                case PaymentTypes.RewardPoints: { intNoOfRewardPointsPayment = 1; break; }
                case PaymentTypes.Combination: { intNoOfCombinationPaymentTransactions = 1; break; }
                default: { intNoOfCashTransactions = 1; break; }
            }
            

            Data.TerminalReportDetails clsTerminalReportDetails = new Data.TerminalReportDetails();

            if (TransStatus == TransactionStatus.Closed)
            {
                clsTerminalReportDetails.ItemSold = ItemSold;
                clsTerminalReportDetails.QuantitySold = QuantitySold;
                clsTerminalReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
                clsTerminalReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
                clsTerminalReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
                clsTerminalReportDetails.NoOfCreditTransactions = intNoOfCreditTransactions;
                clsTerminalReportDetails.NoOfDebitPaymentTransactions = intNoOfDebitTransactions;
                clsTerminalReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
                clsTerminalReportDetails.NoOfRewardPointsPayment = intNoOfRewardPointsPayment;
                clsTerminalReportDetails.NoOfClosedTransactions = 1;
                clsTerminalReportDetails.CashInDrawer = CashPayment;
                clsTerminalReportDetails.CashSales = CashPayment;
                clsTerminalReportDetails.ChequeSales = ChequePayment;
                clsTerminalReportDetails.CreditCardSales = CreditCardPayment;
                clsTerminalReportDetails.CreditSales = CreditPayment;
                clsTerminalReportDetails.DebitPayment = DebitPayment;
                clsTerminalReportDetails.RewardPointsPayment = RewardPointsPayment;
                clsTerminalReportDetails.RewardConvertedPayment = RewardConvertedPayment;

                if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                {
                    clsTerminalReportDetails.SNRDiscount = Discount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode)
                {
                    clsTerminalReportDetails.PWDDiscount = Discount;
                }
                else
                {
                    clsTerminalReportDetails.OtherDiscount = Discount;
                }
                clsTerminalReportDetails.TotalDiscount = ItemsDiscount + Discount;
                clsTerminalReportDetails.ItemsDiscount = ItemsDiscount;
                clsTerminalReportDetails.SubTotalDiscount = Discount;

                clsTerminalReportDetails.GrossSales = SubTotal;
                clsTerminalReportDetails.NetSales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;
                clsTerminalReportDetails.GroupSales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;
                clsTerminalReportDetails.DailySales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;

                clsTerminalReportDetails.TotalCharge = Charge;
                clsTerminalReportDetails.VAT = VAT;
                clsTerminalReportDetails.VATableAmount = VATableAmount;
                clsTerminalReportDetails.NonVATableAmount = NonVATableAmount;
                clsTerminalReportDetails.VATExempt = VATExempt;
                clsTerminalReportDetails.EVAT = EVAT;
                clsTerminalReportDetails.EVATableAmount = EVATableAmount;
                clsTerminalReportDetails.NonEVATableAmount = NonEVATableAmount;
                clsTerminalReportDetails.LocalTax = LocalTax;

                // march 19, 2009
                clsTerminalReportDetails.NoOfDiscountedTransactions = intNoOfDiscountedTransactions;
                clsTerminalReportDetails.CreditSalesTax = clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
                clsTerminalReportDetails.PromotionalItems = decPromotionalItems;
            }
            else if (TransStatus == TransactionStatus.Void)
            {
                clsTerminalReportDetails.ItemSold = 0;
                clsTerminalReportDetails.QuantitySold = 0;
                clsTerminalReportDetails.NoOfVoidTransactions = 1;
                clsTerminalReportDetails.VoidSales = SubTotal;
                clsTerminalReportDetails.DailySales = 0; // change refund
                clsTerminalReportDetails.GrossSales = SubTotal;
                clsTerminalReportDetails.NetSales = 0;
            }
            else if (TransStatus == TransactionStatus.Refund)
            {
                clsTerminalReportDetails.ItemSold = -ItemSold;
                clsTerminalReportDetails.QuantitySold = -QuantitySold;
                clsTerminalReportDetails.NoOfRefundTransactions = 1;
                // should be net which is also the same as CashPayment + ChequePayment + CreditCardPayment
                clsTerminalReportDetails.RefundSales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount); //SubTotal; 
                clsTerminalReportDetails.CashInDrawer = -CashPayment;

                if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                {
                    clsTerminalReportDetails.SNRDiscount = -Discount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode)
                {
                    clsTerminalReportDetails.PWDDiscount = -Discount;
                }
                else
                {
                    clsTerminalReportDetails.OtherDiscount = -Discount;
                }
                clsTerminalReportDetails.TotalDiscount = -(ItemsDiscount + Discount);
                clsTerminalReportDetails.ItemsDiscount = -ItemsDiscount;
                clsTerminalReportDetails.SubTotalDiscount = -Discount;

                clsTerminalReportDetails.GrossSales = -SubTotal;
                clsTerminalReportDetails.NetSales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);
                clsTerminalReportDetails.GroupSales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);
                clsTerminalReportDetails.DailySales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);

                clsTerminalReportDetails.TotalCharge = -Charge;
                clsTerminalReportDetails.VAT = -VAT;
                clsTerminalReportDetails.VATableAmount = -VATableAmount;
                clsTerminalReportDetails.NonVATableAmount = -NonVATableAmount;
                clsTerminalReportDetails.VATExempt = -VATExempt;
                clsTerminalReportDetails.EVAT = -EVAT;
                clsTerminalReportDetails.EVATableAmount = -EVATableAmount;
                clsTerminalReportDetails.NonEVATableAmount = -NonEVATableAmount;
                clsTerminalReportDetails.LocalTax = -LocalTax;

                // march 19, 2009
                clsTerminalReportDetails.CreditSalesTax = -(clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100));
            }
            else if (TransStatus == TransactionStatus.CreditPayment)
            {
                // apr 12, 2014 put all to 0
                //clsTerminalReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
                //clsTerminalReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
                //clsTerminalReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
                //clsTerminalReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
                clsTerminalReportDetails.NoOfCreditPaymentTransactions = 1;

                //clsTerminalReportDetails.CashInDrawer = CashPayment;
                //clsTerminalReportDetails.CashSales = CashPayment;
                //clsTerminalReportDetails.ChequeSales = ChequePayment;
                //clsTerminalReportDetails.CreditCardSales = CreditCardPayment;
                //clsTerminalReportDetails.DebitPayment = DebitPayment;
                clsTerminalReportDetails.CashInDrawer = CashPayment;
                clsTerminalReportDetails.CreditPaymentCash = CashPayment;
                clsTerminalReportDetails.CreditPaymentCheque = ChequePayment;
                clsTerminalReportDetails.CreditPaymentCreditCard = CreditCardPayment;
                clsTerminalReportDetails.CreditPaymentDebit = DebitPayment;
                clsTerminalReportDetails.CreditPayment = CashPayment + ChequePayment + CreditCardPayment + DebitPayment; // SubTotal + Charge; Apr 12, 2014 change to new line

                // march 19, 2009
                // Apr 12, 2014 change to 0 coz its already taxed when goods are sold
                clsTerminalReportDetails.CreditSalesTax = 0; // clsTerminalReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100); 
                clsTerminalReportDetails.PromotionalItems = decPromotionalItems;

                /// may 28, 2006 remove from daily sales and gross sales 
                /// since it was already declared during the credit payment of transaction
                //				clsTerminalReportDetails.DailySales = SubTotal;
                //				clsTerminalReportDetails.GrossSales = SubTotal + ItemsDiscount;
            }
            clsTerminalReportDetails.NoOfTotalTransactions = 1;

            clsTerminalReportDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
            clsTerminalReportDetails.BranchID = mclsTerminalDetails.BranchID;

            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            clsTerminalReport.UpdateTransactionSales(clsTerminalReportDetails);
            //clsTerminalReport.CommitAndDispose(); --Remove this. Should be commited by the calling object
        }

        #endregion

        #region UpdateCashierReport

        //public delegate void UpdateCashierReportDelegate(TransactionStatus TransStatus, decimal SubTotal, decimal Discount, decimal Charge, decimal VAT, decimal VATableAmount, decimal NonVATableAmount, decimal EVAT, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, PaymentTypes PaymentType);
        public void UpdateCashierReport(TransactionStatus TransStatus, decimal ItemSold, decimal QuantitySold, decimal SubTotal, decimal Discount, decimal ItemsDiscount, decimal Charge, decimal VAT, decimal VATableAmount, decimal NonVATableAmount, decimal VATExempt, decimal EVAT, decimal EVATableAmount, decimal NonEVATableAmount, decimal LocalTax, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, PaymentTypes PaymentType)
        {
            Int32 intNoOfCashTransactions = 0;
            Int32 intNoOfChequeTransactions = 0;
            Int32 intNoOfCreditCardTransactions = 0;
            Int32 intNoOfCreditTransactions = 0;
            Int32 intNoOfDebitTransactions = 0;
            Int32 intNoOfCombinationPaymentTransactions = 0;
            Int32 intNoOfDiscountedTransactions = 0;
            Int32 intNoOfRewardPointsPayment = 0;
            
            decimal decPromotionalItems = 0;

            foreach (System.Data.DataRow dr in ItemDataTable.Rows)
            {

                decimal ItemQuantity = 0;
                try { ItemQuantity = Convert.ToDecimal(dr["Quantity"]); }
                catch
                {
                    try { ItemQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace("RETURN", "").Trim()); }
                    catch { }
                }

                decPromotionalItems += Convert.ToDecimal(dr["PromoApplied"]);
            }
            switch (PaymentType)
            {
                case PaymentTypes.Cash: { intNoOfCashTransactions = 1; break; }
                case PaymentTypes.Cheque: { intNoOfChequeTransactions = 1; break; }
                case PaymentTypes.CreditCard: { intNoOfCreditCardTransactions = 1; break; }
                case PaymentTypes.Credit: { intNoOfCreditTransactions = 1; break; }
                case PaymentTypes.Debit: { intNoOfDebitTransactions = 1; break; }
                case PaymentTypes.RewardPoints: { intNoOfRewardPointsPayment = 1; break; }
                case PaymentTypes.Combination: { intNoOfCombinationPaymentTransactions = 1; break; }
                default: { intNoOfCashTransactions = 1; break; }
            }

            Data.CashierReportDetails clsCashierReportDetails = new Data.CashierReportDetails();

            if (TransStatus == TransactionStatus.Closed)
            {
                clsCashierReportDetails.ItemSold = ItemSold;
                clsCashierReportDetails.QuantitySold = QuantitySold;
                clsCashierReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
                clsCashierReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
                clsCashierReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
                clsCashierReportDetails.NoOfCreditTransactions = intNoOfCreditTransactions;
                clsCashierReportDetails.NoOfDebitPaymentTransactions = intNoOfDebitTransactions;
                clsCashierReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
                clsCashierReportDetails.NoOfRewardPointsPayment = intNoOfRewardPointsPayment;
                clsCashierReportDetails.NoOfClosedTransactions = 1;
                clsCashierReportDetails.CashSales = CashPayment;
                clsCashierReportDetails.ChequeSales = ChequePayment;
                clsCashierReportDetails.CreditCardSales = CreditCardPayment;
                clsCashierReportDetails.CreditSales = CreditPayment;
                clsCashierReportDetails.DebitPayment = DebitPayment;
                clsCashierReportDetails.RewardPointsPayment = RewardPointsPayment;
                clsCashierReportDetails.RewardConvertedPayment = RewardConvertedPayment;

                if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                {
                    clsCashierReportDetails.SNRDiscount = Discount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode)
                {
                    clsCashierReportDetails.PWDDiscount = Discount;
                }
                else
                {
                    clsCashierReportDetails.OtherDiscount = Discount;
                }
                clsCashierReportDetails.TotalDiscount = ItemsDiscount + Discount;
                clsCashierReportDetails.ItemsDiscount = ItemsDiscount;
                clsCashierReportDetails.SubTotalDiscount = Discount;

                clsCashierReportDetails.GrossSales = SubTotal;
                clsCashierReportDetails.NetSales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;
                clsCashierReportDetails.GroupSales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;
                clsCashierReportDetails.DailySales = SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount;

                clsCashierReportDetails.TotalCharge = Charge;
                clsCashierReportDetails.VAT = VAT;
                clsCashierReportDetails.VATableAmount = VATableAmount;
                clsCashierReportDetails.NonVATableAmount = NonVATableAmount;
                clsCashierReportDetails.VATExempt = VATExempt;
                clsCashierReportDetails.EVAT = EVAT;
                clsCashierReportDetails.EVATableAmount = EVATableAmount;
                clsCashierReportDetails.NonEVATableAmount = NonEVATableAmount;
                clsCashierReportDetails.LocalTax = LocalTax;

                // march 19, 2009
                clsCashierReportDetails.NoOfDiscountedTransactions = intNoOfDiscountedTransactions;
                clsCashierReportDetails.CreditSalesTax = clsCashierReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
                clsCashierReportDetails.PromotionalItems = decPromotionalItems;
            }
            else if (TransStatus == TransactionStatus.Void)
            {
                clsCashierReportDetails.ItemSold = 0;
                clsCashierReportDetails.QuantitySold = 0;
                clsCashierReportDetails.NoOfVoidTransactions = 1;
                clsCashierReportDetails.VoidSales = SubTotal;
            }
            else if (TransStatus == TransactionStatus.Refund)
            {
                clsCashierReportDetails.ItemSold = -ItemSold;
                clsCashierReportDetails.QuantitySold = -QuantitySold;
                clsCashierReportDetails.NoOfRefundTransactions = 1;
                clsCashierReportDetails.RefundSales = SubTotal;
                clsCashierReportDetails.CashInDrawer = -CashPayment;
                // Sep 4, 2014 remove no effect on sales
                //clsCashierReportDetails.CashSales = -CashPayment;
                //clsCashierReportDetails.ChequeSales = -ChequePayment;
                //clsCashierReportDetails.CreditCardSales = -CreditCardPayment;
                //clsCashierReportDetails.CreditSales = -CreditPayment;
                //clsCashierReportDetails.DebitPayment = -DebitPayment;

                if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.SeniorCitizenDiscountCode)
                {
                    clsCashierReportDetails.SNRDiscount = -Discount;
                }
                else if (mclsSalesTransactionDetails.DiscountCode == mclsTerminalDetails.PWDDiscountCode)
                {
                    clsCashierReportDetails.PWDDiscount = -Discount;
                }
                else
                {
                    clsCashierReportDetails.OtherDiscount = -Discount;
                }
                clsCashierReportDetails.TotalDiscount = -(ItemsDiscount + Discount);
                clsCashierReportDetails.ItemsDiscount = -ItemsDiscount;
                clsCashierReportDetails.SubTotalDiscount = -Discount;

                clsCashierReportDetails.GrossSales = -SubTotal;
                clsCashierReportDetails.NetSales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);
                clsCashierReportDetails.GroupSales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);
                clsCashierReportDetails.DailySales = -(SubTotal - (VATExempt * mclsTerminalDetails.VAT / 100) - Discount);

                clsCashierReportDetails.TotalCharge = -Charge;
                clsCashierReportDetails.VAT = -VAT;
                clsCashierReportDetails.VATableAmount = -VATableAmount;
                clsCashierReportDetails.NonVATableAmount = -NonVATableAmount;
                clsCashierReportDetails.VATExempt = -VATExempt;
                clsCashierReportDetails.EVAT = -EVAT;
                clsCashierReportDetails.EVATableAmount = -EVATableAmount;
                clsCashierReportDetails.NonEVATableAmount = -NonEVATableAmount;
                clsCashierReportDetails.LocalTax = -LocalTax;
            }
            else if (TransStatus == TransactionStatus.CreditPayment)
            {
                // Apr 12, 2014 put this all to zero
                //clsCashierReportDetails.NoOfCashTransactions = intNoOfCashTransactions;
                //clsCashierReportDetails.NoOfChequeTransactions = intNoOfChequeTransactions;
                //clsCashierReportDetails.NoOfCreditCardTransactions = intNoOfCreditCardTransactions;
                //clsCashierReportDetails.NoOfCombinationPaymentTransactions = intNoOfCombinationPaymentTransactions;
                clsCashierReportDetails.NoOfCreditPaymentTransactions = 1;

                // Apr 12, 2014 put this all to zero
                //clsCashierReportDetails.CashSales = CashPayment;
                //clsCashierReportDetails.ChequeSales = ChequePayment;
                //clsCashierReportDetails.CreditCardSales = CreditCardPayment;
                //clsCashierReportDetails.DebitPayment = DebitPayment;
                clsCashierReportDetails.CashInDrawer = CashPayment;
                clsCashierReportDetails.CreditPaymentCash = CashPayment;
                clsCashierReportDetails.CreditPaymentCheque = ChequePayment;
                clsCashierReportDetails.CreditPaymentCreditCard = CreditCardPayment;
                clsCashierReportDetails.CreditPaymentDebit = DebitPayment;
                clsCashierReportDetails.CreditPayment = CashPayment + ChequePayment + CreditCardPayment + DebitPayment;

                
                // march 19, 2009
                // apr 12, 2014 remove this coz this is alredy declared before
                //clsCashierReportDetails.CreditSalesTax = clsCashierReportDetails.CreditPayment * (mclsTerminalDetails.VAT / 100);
                clsCashierReportDetails.PromotionalItems = decPromotionalItems;
            }
            clsCashierReportDetails.NoOfTotalTransactions = 1;

            clsCashierReportDetails.TerminalNo = mclsTerminalDetails.TerminalNo;
            clsCashierReportDetails.BranchID = mclsTerminalDetails.BranchID;
            clsCashierReportDetails.CashierID = mclsSalesTransactionDetails.CashierID;

            Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
            mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

            clsCashierReport.UpdateTransactionSales(clsCashierReportDetails);
        }

        #endregion

        #endregion

        #region Printing

        #region CenterString, CutPrinterPaper, SendOrderSlipToPrinter, GetReceiptFormatParameter

        public string CenterString(string Value, int TotalLengthOfCenter)
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
        public void CutPrinterPaper()
        {
            try
            {
                //string command = Convert.ToChar(29).ToString() + Convert.ToChar(86).ToString() + Convert.ToChar(49).ToString();   // cut the paper  Chr$(86)
                RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.PrinterName, RawPrinterHelper.escCutFull, "RetailPlus Paper Cutter.");
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! in auto-cutting the paper from printer. Err Description: ");
            }
        }
        public bool SendOrderSlipToPrinter(string szString, string PrinterName)
        {
            try
            {
                clsEvent.AddEventReceipt(szString);
                RawPrinterHelper.SendStringToPrinter(PrinterName, szString + "\f", "RetailPlus Trx. No: " + mclsSalesTransactionDetails.TransactionNo);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public string GetReceiptFormatParameter(string stReceiptFormat, bool IsReceipt, DateTime OverRidingPrintDate)
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
                if (!IsReceipt)
                    stRetValue = "";
                else
                    stRetValue = mclsSalesTransactionDetails.TransactionNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.CheckCounter)
            {
                try
                {
                    stRetValue = Int32.Parse(mclsSalesTransactionDetails.TransactionNo).ToString();
                }
                catch
                {
                    stRetValue = mclsSalesTransactionDetails.TransactionNo;
                }
            }
            else if (stReceiptFormat == ReceiptFieldFormats.ORNo)
            {
                if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Void)
                    stRetValue = "N/A:Void".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionNo;
                else if (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.CreditPayment)
                    stRetValue = "N/A:CRP".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionNo;
                else if (!IsReceipt)
                    stRetValue = "";
                else
                    stRetValue = mclsSalesTransactionDetails.ORNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.DateNow)
            {
                if (!mboDoNotPrintTransactionDate)
                {
                    if (OverRidingPrintDate != DateTime.MinValue)
                        stRetValue = OverRidingPrintDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                    else
                        stRetValue = DateTime.Now.ToString("MMM. dd, yyyy hh:mm:ss tt");
                    //stRetValue = DateTime.Now.ToLocalTime().ToString("MMM. dd, yyyy hh:mm:ss tt");
                }
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TransactionDate)
            {
                if (!mboDoNotPrintTransactionDate)
                {
                    //stRetValue = mclsSalesTransactionDetails.TransactionDate.ToLocalTime().ToString("MMM. dd, yyyy hh:mm:ss tt");
                    stRetValue = mclsSalesTransactionDetails.TransactionDate.ToString("MMM. dd, yyyy hh:mm:ss tt");
                }
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Cashier)
            {
                stRetValue = mclsSalesTransactionDetails.CashierName;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TerminalNo)
            {
                stRetValue = mclsTerminalDetails.TerminalNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.MachineSerialNo)
            {
                stRetValue = CONFIG.MachineSerialNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.AccreditationNo)
            {
                stRetValue = CONFIG.AccreditationNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.SubTotal)
            {
                stRetValue = mclsSalesTransactionDetails.SubTotal.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.OtherCharges)
            {
                stRetValue = mclsSalesTransactionDetails.Charge.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.CreditChargeAmount)
            {
                stRetValue = mclsSalesTransactionDetails.CreditChargeAmount.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Discount)
            {
                stRetValue = mclsSalesTransactionDetails.Discount.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.AmountDue)
            {
                stRetValue = mclsSalesTransactionDetails.AmountDue.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.AmountTender)
            {
                stRetValue = mclsSalesTransactionDetails.AmountPaid.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.Change)
            {
                stRetValue = mclsSalesTransactionDetails.ChangeAmount.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.VATZeroRated)
            {
                stRetValue = "0.00";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.NonVATableAmount)
            {
                stRetValue = mclsSalesTransactionDetails.NonVATableAmount.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.VATExempt)
            {
                stRetValue = mclsSalesTransactionDetails.VATExempt.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.VATableAmount)
            {
                stRetValue = mclsSalesTransactionDetails.VATableAmount.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.VAT)
            {
                stRetValue = mclsSalesTransactionDetails.VAT.ToString("#,##0.#0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TotalItemSold)
            {
                stRetValue = mclsSalesTransactionDetails.ItemSold.ToString("#,##0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.TotalQtySold)
            {
                stRetValue = mclsSalesTransactionDetails.QuantitySold.ToString("#,##0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.CustomerName)
            {
                stRetValue = mclsSalesTransactionDetails.CustomerName;// lblCustomer.Text;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.WaiterName)
            {
                stRetValue = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0,11).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16);
            }
            else if (stReceiptFormat == ReceiptFieldFormats.BaggerName)
            {
                stRetValue = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0,11).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16);
            }
            else if (stReceiptFormat == ReceiptFieldFormats.OrderType)
            {
                stRetValue = mclsSalesTransactionDetails.OrderType.ToString("G").ToUpper();
            }
            else if (stReceiptFormat == ReceiptFieldFormats.CheckOutBillFooter)
            {
                PrintCheckOutBillFooter();
                stRetValue = "";
            }
            else if (stReceiptFormat == ReceiptFieldFormats.DiscountCode)
            {
                switch (mclsSalesTransactionDetails.TransDiscountType)
                {
                    case DiscountTypes.FixedValue:
                        stRetValue = mclsSalesTransactionDetails.DiscountCode + "(" + mclsSalesTransactionDetails.TransDiscount.ToString("##.##") + ")";
                        break;
                    case DiscountTypes.Percentage:
                        stRetValue = mclsSalesTransactionDetails.DiscountCode + "(" + mclsSalesTransactionDetails.TransDiscount.ToString("##.##") + "%)";
                        break;
                }
            }
            else if (stReceiptFormat == ReceiptFieldFormats.DiscountRemarks)
            {
                stRetValue = mclsSalesTransactionDetails.DiscountRemarks;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.ChargeCode)
            {
                stRetValue = mclsSalesTransactionDetails.ChargeCode;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.ChargeRemarks)
            {
                stRetValue = mclsSalesTransactionDetails.ChargeRemarks;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardCardNo)
            {
                stRetValue = mclsSalesTransactionDetails.RewardCardNo;
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardPreviousPoints)
            {
                stRetValue = mclsSalesTransactionDetails.RewardPreviousPoints.ToString("#,##0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardEarnedPoints)
            {
                stRetValue = mclsSalesTransactionDetails.RewardEarnedPoints.ToString("#,##0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardCurrentPoints)
            {
                stRetValue = mclsSalesTransactionDetails.RewardCurrentPoints.ToString("#,##0");
            }
            else if (stReceiptFormat == ReceiptFieldFormats.RewardsPermitNo)
            {
                stRetValue = mclsTerminalDetails.RewardPointsDetails.RewardsPermitNo;
            }
            else
            {
                stRetValue = stReceiptFormat;
            }

            if (stRetValue == null) stRetValue = "";

            return stRetValue;
        }

        #endregion

        #region Prerequisites

        public void PrintReportValue(Reports.ReceiptDetails clsReceiptDetails, bool IsReceipt, DateTime OverRidingPrintDate, bool isCheckOutBill=false)
        {
            if (clsReceiptDetails.Value == ReceiptFieldFormats.CustomerName && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.WaiterName && mclsSalesTransactionDetails.WaiterID == Constants.C_RETAILPLUS_WAITERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.DiscountCode && (mclsSalesTransactionDetails.DiscountCode == null || mclsSalesTransactionDetails.DiscountCode == string.Empty))
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.DiscountRemarks && (mclsSalesTransactionDetails.DiscountRemarks == null || mclsSalesTransactionDetails.DiscountRemarks == string.Empty))
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.ChargeCode && (mclsSalesTransactionDetails.ChargeCode == null || mclsSalesTransactionDetails.ChargeCode == string.Empty))
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.ChargeRemarks && (mclsSalesTransactionDetails.ChargeRemarks == null || mclsSalesTransactionDetails.ChargeRemarks == string.Empty))
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardCardNo && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardPreviousPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardEarnedPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.RewardCurrentPoints && mclsSalesTransactionDetails.CustomerID == Constants.C_RETAILPLUS_CUSTOMERID)
                return;
            else if (clsReceiptDetails.Value == ReceiptFieldFormats.ORNo && (mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.Void || mclsSalesTransactionDetails.TransactionStatus == TransactionStatus.CreditPayment))
                return;
            else if (clsReceiptDetails.Text.IndexOf("OFFICIAL RECEIPT") > -1 && clsReceiptDetails.Value == ReceiptFieldFormats.ORNo
                && string.IsNullOrEmpty(mclsSalesTransactionDetails.ORNo))
                return;
            else if (clsReceiptDetails.Text.IndexOf("OFFICIAL RECEIPT") > -1 && isCheckOutBill)
                return;

            if (!string.IsNullOrEmpty(clsReceiptDetails.Text) || !string.IsNullOrEmpty(clsReceiptDetails.Value))
            {
                switch (clsReceiptDetails.Orientation)
                {
                    case ReportFormatOrientation.Justify:
                        if (string.IsNullOrEmpty(clsReceiptDetails.Text))
                            msbToPrint.Append(GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate) + Environment.NewLine);
                        else if (string.IsNullOrEmpty(clsReceiptDetails.Value))
                            msbToPrint.Append(GetReceiptFormatParameter(clsReceiptDetails.Text, IsReceipt, OverRidingPrintDate) + Environment.NewLine);
                        else
                        {
                            if (clsReceiptDetails.Value == ReceiptFieldFormats.AmountDue && !mclsTerminalDetails.IsPrinterDotMatrix)
                                msbToPrint.Append(clsReceiptDetails.Text.PadRight(10) + RawPrinterHelper.escAlignRight + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 10) + Environment.NewLine);
                            else if (clsReceiptDetails.Value == ReceiptFieldFormats.Change && !mclsTerminalDetails.IsPrinterDotMatrix)
                                msbToPrint.Append(RawPrinterHelper.escEmphasizedOn + clsReceiptDetails.Text.PadRight(10) + RawPrinterHelper.escAlignRight + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 10) + RawPrinterHelper.escEmphasizedOff + Environment.NewLine);
                            else
                                msbToPrint.Append(clsReceiptDetails.Text.PadRight(15) + ":" + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate).PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                        }
                        break;
                    case ReportFormatOrientation.Center:
                        if (string.IsNullOrEmpty(clsReceiptDetails.Text))
                            msbToPrint.Append(CenterString(GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        else if (string.IsNullOrEmpty(clsReceiptDetails.Value))
                            msbToPrint.Append(CenterString(GetReceiptFormatParameter(clsReceiptDetails.Text, IsReceipt, OverRidingPrintDate), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        else
                            if (clsReceiptDetails.Value == ReceiptFieldFormats.AmountDue && !mclsTerminalDetails.IsPrinterDotMatrix)
                                msbToPrint.Append(RawPrinterHelper.escAlignCenter + clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate) + RawPrinterHelper.escAlignLeft + Environment.NewLine);
                            else if (clsReceiptDetails.Value == ReceiptFieldFormats.Change && !mclsTerminalDetails.IsPrinterDotMatrix)
                                msbToPrint.Append(RawPrinterHelper.escEmphasizedOn + RawPrinterHelper.escAlignCenter + clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate) + RawPrinterHelper.escAlignLeft + RawPrinterHelper.escEmphasizedOff + Environment.NewLine);
                            else
                                msbToPrint.Append(CenterString(clsReceiptDetails.Text + " : " + GetReceiptFormatParameter(clsReceiptDetails.Value, IsReceipt, OverRidingPrintDate), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);

                        break;
                }
            }
        }
        public static void PrintProps(ManagementObject o, string prop)
        {
            try { Console.WriteLine(prop + "|" + o[prop]); }
            catch (Exception e) { Console.Write(e.ToString()); }
        }
        public bool isPrinterOnline(string objPrinterName)
        {
            bool boretValue = false;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    string printerName = printer["Name"].ToString().ToLower();

                    if (printerName == objPrinterName.ToLower())
                    {
                        boretValue = true;
                        break;
                    }
                    //Console.WriteLine("Printer".PadRight(15) + ":" + printerName);

                    //PrintProps(printer, "Caption");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "Availability");
                    //PrintProps(printer, "Default");
                    //PrintProps(printer, "DetectedErrorState");
                    //PrintProps(printer, "ExtendedDetectedErrorState");
                    //PrintProps(printer, "ExtendedPrinterStatus");
                    //PrintProps(printer, "LastErrorCode");
                    //PrintProps(printer, "PrinterState");
                    //PrintProps(printer, "PrinterStatus");
                    //PrintProps(printer, "Status");
                    //PrintProps(printer, "WorkOffline");
                    //PrintProps(printer, "Local");
                }
            }
            catch { }
            return boretValue;
        }

        public void PrintCheckOutBill()
        {
            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
            {
                MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }
            DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

            if (loginresult == DialogResult.None)
            {
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.CloseTransaction;
                login.Header = "Print Check-Out Bill Access Validation";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();
            }
            if (loginresult == DialogResult.OK)
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                /*****
                 * Put a separate ReportHeader for Check-OutBill
                 * Due to security reasons that check-out bill is use as a receipt.
                 * January 14, 2008
                 * ***/
                PrintCheckOutBillHeader();

                /*** end of Jan 14 ***/

                foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                {
                    if (dr["Quantity"].ToString().IndexOf("RETURN") != -1)
                    {
                        string stItemNo = "" + dr["ItemNo"].ToString();
                        string stProductCode = "" + dr["ProductCode"].ToString() + "-RET";
                        string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                        if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                        decimal decQuantity = Convert.ToDecimal(dr["Quantity"].ToString().Replace(" - RETURN", "").Trim());
                        decimal decPrice = Convert.ToDecimal(dr["Price"]);
                        decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                        decimal decAmount = Convert.ToDecimal(dr["Amount"]) * -1;
                        decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                        decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                        decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);

                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                    }
                    else if (dr["Quantity"].ToString() != "VOID")
                    {
                        string stItemNo = "" + dr["ItemNo"].ToString();
                        string stProductCode = "" + dr["ProductCode"].ToString();
                        if (dr["MatrixDescription"].ToString() != string.Empty && dr["MatrixDescription"].ToString() != null) stProductCode += "-" + dr["MatrixDescription"].ToString();
                        string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                        decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);
                        decimal decPrice = Convert.ToDecimal(dr["Price"]);
                        decimal decDiscount = Convert.ToDecimal(dr["Discount"]);
                        decimal decAmount = Convert.ToDecimal(dr["Amount"]);
                        decimal decVAT = Convert.ToDecimal(dr["VAT"]);
                        decimal decEVAT = Convert.ToDecimal(dr["EVAT"]);
                        decimal decPromoApplied = Convert.ToDecimal(dr["PromoApplied"]);

                        PrintItem(stItemNo, stProductCode, stProductUnitCode, decQuantity, decPrice, decDiscount, decPromoApplied, decAmount, decVAT, decEVAT);
                    }
                }

                PrintReportFooterSection(true, TransactionStatus.Closed, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                mboIsItemHeaderPrinted = false;
                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
            }
        }
        private void PrintCheckOutBillHeader()
        {
            PrintReportHeaderSection(true, DateTime.MinValue);
            mboIsItemHeaderPrinted = true;

            if (mclsTerminalDetails.IsPrinterDotMatrix)
            {
                msbToPrint.Append(CenterString(mclsSysConfigDetails.CheckOutBillHeaderLabel, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString(Constants.C_FE_NOT_VALID_AS_RECEIPT, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else
            {
                msbToPrint.Append(RawPrinterHelper.escEmphasizedOn + CenterString(mclsSysConfigDetails.CheckOutBillHeaderLabel, mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escEmphasizedOff + Environment.NewLine);
                msbToPrint.Append(RawPrinterHelper.escEmphasizedOn + CenterString(Constants.C_FE_NOT_VALID_AS_RECEIPT, mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escEmphasizedOff + Environment.NewLine);
            }
            PrintReportPageHeaderSection(true, true);
        }
        public void PrintCheckOutBillFooter()
        {
            if (mclsSalesTransactionDetails.OrderType == OrderTypes.Delivery)
            {
                Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                Data.ContactDetails clsContactDetails = clsContact.Details(mclsSalesTransactionDetails.CustomerID);
                clsContact.CommitAndDispose();

                if (clsContactDetails.BusinessName != string.Empty)
                    msbToPrint.Append("Delivered to".PadRight(15) + ":" + clsContactDetails.BusinessName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                if (clsContactDetails.TelephoneNo != string.Empty)
                    msbToPrint.Append("Tel #".PadRight(15) + ":" + clsContactDetails.TelephoneNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                if (clsContactDetails.Address != string.Empty)
                    msbToPrint.Append("Address".PadRight(15) + ":" + clsContactDetails.Address.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

            }

        }

        public void PrintOrderSlip(bool WillReprintAll, bool WillShowMessage = true)
        {
            if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
            {
                MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }
            if (!mboIsInTransaction)
            {
                MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }
            DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

            if (loginresult == DialogResult.None)
            {
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.CloseTransaction;
                login.Header = "Print Order Slip Validation";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();
            }
            if (loginresult == DialogResult.OK)
            {
                if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoice)
                {
                    PrintSalesInvoice();
                }
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.DeliveryReceipt)
                {
                    PrintDeliveryReceipt();
                }
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceAndDR)
                {
                    PrintSalesInvoice();
                    PrintDeliveryReceipt();
                }
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300Printer)
                {
                    PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300Printer);
                }
                //Added May 11, 2010
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceOrDR)
                {
                    PrintDeliveryReceipt();
                }
                //Added January 17, 2011
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                {
                    PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusPrinter);
                }
                //Added February 22, 2011
                else if (mclsTerminalDetails.ReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                {
                    PrintSalesInvoiceToLX(TerminalReceiptType.SalesInvoiceForLX300PlusAmazon); //8.5inc x 7inch
                }
                else
                {
                    /*************************
                     * Check if will reprint all items for ALT + S
                     * December 18, 2008
                     * **********************/

                    bool bolRetailPlusOSPrinter1HeaderPrinted = false;
                    bool bolRetailPlusOSPrinter2HeaderPrinted = false;
                    bool bolRetailPlusOSPrinter3HeaderPrinted = false;
                    bool bolRetailPlusOSPrinter4HeaderPrinted = false;
                    bool bolRetailPlusOSPrinter5HeaderPrinted = false;

                    Data.ProductComposition clsProductComposition = new Data.ProductComposition(mConnection, mTransaction);
                    mConnection = clsProductComposition.Connection; mTransaction = clsProductComposition.Transaction;

                    Data.Products clsProduct = new Data.Products(mConnection, mTransaction);
                    mConnection = clsProduct.Connection; mTransaction = clsProduct.Transaction;

                    Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                    mConnection = clsSalesTransactionItems.Connection; mTransaction = clsSalesTransactionItems.Transaction;

                    // print order slip items in each printer
                    foreach (System.Data.DataRow dr in ItemDataTable.Rows)
                    {
                        bool OrderSlipPrinted = Convert.ToBoolean(dr["OrderSlipPrinted"]);
                        if (!OrderSlipPrinted || WillReprintAll)
                        {
                            /****************************************
                             * Update items that are already printed
                             *  December 18, 2008
                            ****************************************/
                            Int64 iTransactionItemsID = Convert.ToInt64(dr["TransactionItemsID"]);
                            clsSalesTransactionItems.UpdateOrderSlipPrinted(true, iTransactionItemsID, mclsSalesTransactionDetails.TransactionDate);

                            if (dr["Quantity"].ToString() != "VOID" && dr["Quantity"].ToString().IndexOf("RETURN") == -1)
                            {
                                //string stItemNo = "" + dr["ItemNo"].ToString();
                                long lProductID = Convert.ToInt64(dr["ProductID"]);

                                bool bolWillPrintProductComposition = clsProduct.WillPrintProductComposition(lProductID);

                                string stProductCode = "" + dr["ProductCode"].ToString();
                                string stProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                                decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);

                                AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                                string stPrinterName = orderSlipPrinter.ToString("G");

                                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1 && !bolRetailPlusOSPrinter1HeaderPrinted)
                                { bolRetailPlusOSPrinter1HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G"), WillReprintAll); }
                                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2 && !bolRetailPlusOSPrinter2HeaderPrinted)
                                { bolRetailPlusOSPrinter2HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G"), WillReprintAll); }
                                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3 && !bolRetailPlusOSPrinter3HeaderPrinted)
                                { bolRetailPlusOSPrinter3HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G"), WillReprintAll); }
                                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4 && !bolRetailPlusOSPrinter4HeaderPrinted)
                                { bolRetailPlusOSPrinter4HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G"), WillReprintAll); }
                                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5 && !bolRetailPlusOSPrinter5HeaderPrinted)
                                { bolRetailPlusOSPrinter5HeaderPrinted = true; PrintOrderSlipHeader(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G"), WillReprintAll); }

                                // print product composition first: 
                                // if there are product compositions no need to print product

                                if (bolWillPrintProductComposition)
                                {
                                    if (!PrintOrderSlipComposition(lProductID, stProductCode, stProductUnitCode, decQuantity, bolWillPrintProductComposition))
                                    {
                                        // if there are no product composition
                                        // print the product only
                                        PrintItemForKitchen(stProductCode, stProductUnitCode, decQuantity, stPrinterName, true);
                                    }
                                }
                                else
                                {
                                    // if there are no product composition
                                    // print the product only
                                    PrintItemForKitchen(stProductCode, stProductUnitCode, decQuantity, stPrinterName, true);
                                }
                            }

                            /****************************************
                             * Update items that are already printed
                             *  December 18, 2008
                            ****************************************/
                            dr["OrderSlipPrinted"] = true.ToString();

                        }
                    }
                    clsProductComposition.CommitAndDispose();
                    // print order slip footer in each printer
                    if (bolRetailPlusOSPrinter1HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G")); }
                    if (bolRetailPlusOSPrinter2HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G")); }
                    if (bolRetailPlusOSPrinter3HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G")); }
                    if (bolRetailPlusOSPrinter4HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G")); }
                    if (bolRetailPlusOSPrinter5HeaderPrinted) { PrintOrderSlipFooter(AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G")); }

                    if (WillShowMessage)
                    {
                        if (WillReprintAll)
                            MessageBox.Show("Order's has been re-send to Kitchen/Bar printer's.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        else
                            MessageBox.Show("Order's has been sent to Kitchen/Bar printer's.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    }
                }
            }
        }
        public bool PrintOrderSlipCountCompositionHeader(long ProductID, int iRetailPlusOSPrinter1Ctr, int iRetailPlusOSPrinter2Ctr, int iRetailPlusOSPrinter3Ctr, int iRetailPlusOSPrinter4Ctr, int iRetailPlusOSPrinter5Ctr, out int RetailPlusOSPrinter1Ctr, out int RetailPlusOSPrinter2Ctr, out int RetailPlusOSPrinter3Ctr, out int RetailPlusOSPrinter4Ctr, out int RetailPlusOSPrinter5Ctr)
        {
            // returns 
            //  false if no product composition
            //  true if with product composition

            bool boRetValue = false;
            Data.ProductComposition clsProductComposition = new Data.ProductComposition(mConnection, mTransaction);
            mConnection = clsProductComposition.Connection; mTransaction = clsProductComposition.Transaction;

            System.Data.DataTable dt = clsProductComposition.ListAsDataTable(ProductID);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                switch (orderSlipPrinter)
                {
                    case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1: { iRetailPlusOSPrinter1Ctr++; break; }
                    case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2: { iRetailPlusOSPrinter2Ctr++; break; }
                    case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3: { iRetailPlusOSPrinter3Ctr++; break; }
                    case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4: { iRetailPlusOSPrinter4Ctr++; break; }
                    case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5: { iRetailPlusOSPrinter5Ctr++; break; }
                }

                long lProductID = Convert.ToInt64(dr["ProductID"]);
                PrintOrderSlipCountCompositionHeader(lProductID, iRetailPlusOSPrinter1Ctr, iRetailPlusOSPrinter2Ctr, iRetailPlusOSPrinter3Ctr, iRetailPlusOSPrinter4Ctr, iRetailPlusOSPrinter5Ctr, out RetailPlusOSPrinter1Ctr, out RetailPlusOSPrinter2Ctr, out RetailPlusOSPrinter3Ctr, out RetailPlusOSPrinter4Ctr, out RetailPlusOSPrinter5Ctr);

                boRetValue = true;
            }

            RetailPlusOSPrinter1Ctr = iRetailPlusOSPrinter1Ctr;
            RetailPlusOSPrinter2Ctr = iRetailPlusOSPrinter2Ctr;
            RetailPlusOSPrinter3Ctr = iRetailPlusOSPrinter3Ctr;
            RetailPlusOSPrinter4Ctr = iRetailPlusOSPrinter4Ctr;
            RetailPlusOSPrinter5Ctr = iRetailPlusOSPrinter5Ctr;

            return boRetValue;
        }
        public bool PrintOrderSlipComposition(long ProductID, string ProductCode, string ProductUnitCode, decimal Quantity, bool bolWillPrintProductComposition)
        {
            bool boRetValue = false;

            bool bolRetailPlusOSPrinter1ItemHeaderPrinted = false;
            bool bolRetailPlusOSPrinter2ItemHeaderPrinted = false;
            bool bolRetailPlusOSPrinter3ItemHeaderPrinted = false;
            bool bolRetailPlusOSPrinter4ItemHeaderPrinted = false;
            bool bolRetailPlusOSPrinter5ItemHeaderPrinted = false;


            Data.ProductComposition clsProductComposition = new Data.ProductComposition(mConnection, mTransaction);
            mConnection = clsProductComposition.Connection; mTransaction = clsProductComposition.Transaction;

            System.Data.DataTable dt = clsProductComposition.ListAsDataTable(ProductID);
            clsProductComposition.CommitAndDispose();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                AceSoft.RetailPlus.OrderSlipPrinter orderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                string stPrinterName = orderSlipPrinter.ToString("G");

                long lProductID = Convert.ToInt64(dr["ProductID"]);
                string stProductCode = "" + dr["ProductCode"].ToString();
                string stProductUnitCode = "" + dr["UnitCode"].ToString();
                decimal decQuantity = Convert.ToDecimal(dr["Quantity"]);

                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1 && !bolRetailPlusOSPrinter1ItemHeaderPrinted)
                { bolRetailPlusOSPrinter1ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1.ToString("G"), bolWillPrintProductComposition); }
                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2 && !bolRetailPlusOSPrinter2ItemHeaderPrinted)
                { bolRetailPlusOSPrinter2ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2.ToString("G"), bolWillPrintProductComposition); }
                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3 && !bolRetailPlusOSPrinter3ItemHeaderPrinted)
                { bolRetailPlusOSPrinter3ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3.ToString("G"), bolWillPrintProductComposition); }
                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4 && !bolRetailPlusOSPrinter4ItemHeaderPrinted)
                { bolRetailPlusOSPrinter4ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4.ToString("G"), bolWillPrintProductComposition); }
                if (orderSlipPrinter == AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5 && !bolRetailPlusOSPrinter5ItemHeaderPrinted)
                { bolRetailPlusOSPrinter5ItemHeaderPrinted = true; PrintItemForKitchen(ProductCode, ProductUnitCode, Quantity, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5.ToString("G"), bolWillPrintProductComposition); }

                if (!PrintOrderSlipComposition(lProductID, stProductCode, stProductUnitCode, decQuantity, bolWillPrintProductComposition))
                {
                    // if there are no product composition
                    // print the product only
                    PrintItemForKitchen("   " + stProductCode, stProductUnitCode, decQuantity, stPrinterName);
                }

                boRetValue = true;
            }

            return boRetValue;
        }
        public void PrintOrderSlipHeader(string PrinterName, bool WillReprintAll)
        {
            // print page header spacer
            for (int i = 0; i < 3; i++)
            { SendOrderSlipToPrinter(Environment.NewLine, PrinterName); }

            SendOrderSlipToPrinter(CenterString("Trx #: " + mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine, PrinterName);
            if (mclsTerminalDetails.IsPrinterDotMatrix)
            { SendOrderSlipToPrinter(CenterString("ORDER SLIP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine, PrinterName); }
            else { SendOrderSlipToPrinter(RawPrinterHelper.escFontHeightX2On + CenterString("ORDER SLIP", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escFontHeightX2Off + Environment.NewLine, PrinterName); }
            
            //
            if (WillReprintAll)
            { SendOrderSlipToPrinter(CenterString("-reprinted: check if already prepared-", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine, PrinterName); }

            SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("Customer : " + mclsSalesTransactionDetails.CustomerName + Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("DESC".PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
            SendOrderSlipToPrinter("UNIT".PadRight(6), PrinterName);
            SendOrderSlipToPrinter("QTY".PadLeft(6), PrinterName);
            SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
        }
        public void PrintOrderSlipFooter(string PrinterName)
        {

            SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("Served by: " + mclsSalesTransactionDetails.WaiterName + Environment.NewLine, PrinterName);
            SendOrderSlipToPrinter("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine, PrinterName);

            // print page footer spacer
            for (int i = 0; i < 6; i++)
            { SendOrderSlipToPrinter(Environment.NewLine, PrinterName); }

            if (mclsTerminalDetails.IsPrinterAutoCutter) CutPrinterPaper();
        }

        public delegate void PrintOfficialReceiptDelegate();
        public void PrintOfficialReceipt()
        {
            try
            {
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (!mboIsInTransaction)
                {
                    MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

                if (loginresult == DialogResult.None)
                {
                    LogInWnd login = new LogInWnd();

                    login.AccessType = AccessTypes.CloseTransaction;
                    login.Header = "Print Sales Invoice Validation";
                    login.TerminalDetails = mclsTerminalDetails;
                    login.ShowDialog(this);
                    loginresult = login.Result;
                    login.Close();
                    login.Dispose();
                }
                if (loginresult == DialogResult.OK)
                {
                    CRSReports.OR rpt = new CRSReports.OR();

                    AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
                    System.Data.DataRow drNew;

                    /****************************report logo *****************************/
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/images/ReportLogo.jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "/images/ReportLogo.jpg");

                        byte[] propimg = new byte[fi.Length];
                        fs.Read(propimg, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        drNew = rptds.CompanyLogo.NewRow(); drNew["Picture"] = propimg;
                        rptds.CompanyLogo.Rows.Add(drNew);
                    }
                    catch { }

                    /****************************sales transaction *****************************/
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

                    if (clsSalesTransactionDetails.isExist)
                    {
                        /****************************sales transaction details*****************************/
                        drNew = rptds.Transactions.NewRow();

                        drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
                        drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
                        drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
                        drNew["CustomerAddress"] = clsContactDetails.Address;
                        drNew["CustomerTerms"] = clsContactDetails.Terms;
                        drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
                        drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
                        drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
                        drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
                        drNew["CreatedByName"] = clsSalesTransactionDetails.CreatedByName;
                        drNew["AgentName"] = clsSalesTransactionDetails.AgentName;
                        drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
                        drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
                        drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
                        drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
                        drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
                        drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Discount"] = clsSalesTransactionDetails.Discount;
                        drNew["VAT"] = clsSalesTransactionDetails.VAT;
                        drNew["VATableAmount"] = clsSalesTransactionDetails.VATableAmount;
                        drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
                        drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
                        drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
                        drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
                        drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
                        drNew["CreditPayment"] = clsSalesTransactionDetails.CreditPayment;
                        drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
                        drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
                        drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
                        drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Charge"] = clsSalesTransactionDetails.Charge;
                        drNew["isConsignment"] = clsSalesTransactionDetails.isConsignment;

                        rptds.Transactions.Rows.Add(drNew);

                        /****************************sales transaction items*****************************/
                        Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            drNew = rptds.SalesTransactionItems.NewRow();

                            foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
                                drNew[dc] = dr[dc.ColumnName];

                            rptds.SalesTransactionItems.Rows.Add(drNew);
                        }
                    }

                    clsSalesTransactions.CommitAndDispose();

                    rpt.SetDataSource(rptds);

                    CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
                    CrystalDecisions.Shared.ParameterValues currentValues;
                    CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

                    paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.CompanyName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.CashierName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0, 11);
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.Address1 +
                                            Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
                                            Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #".PadRight(15) + ":" + CompanyDetails.FaxPhone +
                                            Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg." +
                                            Environment.NewLine + "BIR Acc #: " + CONFIG.AccreditationNo + " SN#: " + CONFIG.MachineSerialNo;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    //foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
                    //{
                    //    if (objPic.Name.ToUpper() == "PICLOGO1")
                    //    {
                    //        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
                    //    }
                    //}

                    //CRViewer.Visible = true;
                    //CRViewer.ReportSource = rpt;
                    //CRViewer.Show();

                    try
                    {
                        DateTime logdate = DateTime.Now;
                        string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                        if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                        {
                            Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                        }
                        string logFile = logsdir + logdate.ToString("MMM") + "/OR_" + clsSalesTransactionDetails.TransactionNo + logdate.ToString("yyyyMMddhhmmss") + ".doc";

                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
                    }
                    catch { }

                    if (isPrinterOnline(mclsTerminalDetails.SalesInvoicePrinterName))
                    {
                        rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        clsEvent.AddEventLn("will not print sales invoice. printer is offline.", true, mclsSysConfigDetails.WillWriteSystemLog);
                    }

                    rpt.Close();
                    rpt.Dispose();

                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
                MessageBox.Show("Sorry an error was encountered during printing, please reprint again." + Environment.NewLine + "Details: " + ex.Message, "RetailPlus");
            }
        }
        public delegate void PrintSalesInvoiceDelegate();
        public void PrintSalesInvoice()
        {
            try
            {
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (!mboIsInTransaction)
                {
                    MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

                if (loginresult == DialogResult.None)
                {
                    LogInWnd login = new LogInWnd();

                    login.AccessType = AccessTypes.CloseTransaction;
                    login.Header = "Print Sales Invoice Validation";
                    login.TerminalDetails = mclsTerminalDetails;
                    login.ShowDialog(this);
                    loginresult = login.Result;
                    login.Close();
                    login.Dispose();
                }
                if (loginresult == DialogResult.OK)
                {
                    CRSReports.SI rpt = new CRSReports.SI();

                    AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
                    System.Data.DataRow drNew;

                    /****************************report logo *****************************/
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/images/ReportLogo.jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        System.IO.FileInfo fi = new System.IO.FileInfo(Application.StartupPath + "/images/ReportLogo.jpg");

                        byte[] propimg = new byte[fi.Length];
                        fs.Read(propimg, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        drNew = rptds.CompanyLogo.NewRow(); drNew["Picture"] = propimg;
                        rptds.CompanyLogo.Rows.Add(drNew);
                    }
                    catch { }

                    /****************************sales transaction *****************************/
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

                    if (clsSalesTransactionDetails.isExist)
                    {
                        /****************************sales transaction details*****************************/
                        drNew = rptds.Transactions.NewRow();

                        drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
                        drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
                        drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
                        drNew["CustomerAddress"] = clsContactDetails.Address;
                        drNew["CustomerTerms"] = clsContactDetails.Terms;
                        drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
                        drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
                        drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
                        drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
                        drNew["CreatedByName"] = clsSalesTransactionDetails.CreatedByName;
                        drNew["AgentName"] = clsSalesTransactionDetails.AgentName;
                        drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
                        drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
                        drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
                        drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
                        drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
                        drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Discount"] = clsSalesTransactionDetails.Discount;
                        drNew["VAT"] = clsSalesTransactionDetails.VAT;
                        drNew["VATableAmount"] = clsSalesTransactionDetails.VATableAmount;
                        drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
                        drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
                        drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
                        drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
                        drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
                        drNew["CreditPayment"] = clsSalesTransactionDetails.CreditPayment;
                        drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
                        drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
                        drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
                        drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Charge"] = clsSalesTransactionDetails.Charge;
                        drNew["isConsignment"] = clsSalesTransactionDetails.isConsignment;

                        rptds.Transactions.Rows.Add(drNew);

                        /****************************sales transaction items*****************************/
                        Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                        mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                        System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            drNew = rptds.SalesTransactionItems.NewRow();

                            foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
                                drNew[dc] = dr[dc.ColumnName];

                            rptds.SalesTransactionItems.Rows.Add(drNew);
                        }
                    }

                    clsSalesTransactions.CommitAndDispose();

                    rpt.SetDataSource(rptds);

                    CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
                    CrystalDecisions.Shared.ParameterValues currentValues;
                    CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

                    paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.CompanyName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.CashierName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0, 11);
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.Address1 +
                                            Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
                                            Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #".PadRight(15) + ":" + CompanyDetails.FaxPhone +
                                            Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg." +
                                            Environment.NewLine + "BIR Acc #: " + CONFIG.AccreditationNo + " SN#: " + CONFIG.MachineSerialNo;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    //foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
                    //{
                    //    if (objPic.Name.ToUpper() == "PICLOGO1")
                    //    {
                    //        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
                    //    }
                    //}

                    //CRViewer.Visible = true;
                    //CRViewer.ReportSource = rpt;
                    //CRViewer.Show();

                    try
                    {
                        DateTime logdate = DateTime.Now;
                        string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                        if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                        {
                            Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                        }
                        string logFile = logsdir + logdate.ToString("MMM") + "/OR_" + clsSalesTransactionDetails.TransactionNo + logdate.ToString("yyyyMMddhhmmss") + ".doc";

                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
                    }
                    catch { }

                    if (isPrinterOnline(mclsTerminalDetails.SalesInvoicePrinterName))
                    {
                        rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        clsEvent.AddEventLn("will not print sales invoice. printer is offline.", true, mclsSysConfigDetails.WillWriteSystemLog);
                    }

                    rpt.Close();
                    rpt.Dispose();

                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
                MessageBox.Show("Sorry an error was encountered during printing, please reprint again." + Environment.NewLine + "Details: " + ex.Message, "RetailPlus");
            }
        }
        public void PrintSalesInvoiceToLX(TerminalReceiptType pclsTerminalReceiptType)
        {
            try
            {
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (!mboIsInTransaction)
                {
                    MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

                if (loginresult == DialogResult.None)
                {
                    LogInWnd login = new LogInWnd();

                    login.AccessType = AccessTypes.CloseTransaction;
                    login.Header = "Print LX 300+ Sales Invoice Validation";
                    login.TerminalDetails = mclsTerminalDetails;
                    login.ShowDialog(this);
                    loginresult = login.Result;
                    login.Close();
                    login.Dispose();
                }
                if (loginresult == DialogResult.OK)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportClass rpt = null;
                    if (pclsTerminalReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusAmazon)
                        rpt = new CRSReports.ORLX300PlusAmazon();
                    else if (pclsTerminalReceiptType == TerminalReceiptType.SalesInvoiceForLX300PlusPrinter)
                        rpt = new CRSReports.ORLX300Plus();
                    else
                        rpt = new CRSReports.ORLX();

                    AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
                    System.Data.DataRow drNew;

                    /****************************sales transaction *****************************/
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    mConnection = clsContact.Connection; mTransaction = clsContact.Transaction;

                    Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

                    if (clsSalesTransactionDetails.isExist)
                    {
                        drNew = rptds.Transactions.NewRow();

                        drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
                        drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
                        drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
                        drNew["CustomerAddress"] = clsContactDetails.Address;
                        drNew["CustomerTerms"] = clsContactDetails.Terms;
                        drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
                        drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
                        drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
                        drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
                        drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
                        drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
                        drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
                        drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
                        drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
                        drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Discount"] = clsSalesTransactionDetails.Discount;
                        drNew["VAT"] = clsSalesTransactionDetails.VAT;
                        drNew["VATableAmount"] = clsSalesTransactionDetails.VATableAmount;
                        drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
                        drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
                        drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
                        drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
                        drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
                        drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
                        drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
                        drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
                        drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Charge"] = clsSalesTransactionDetails.Charge;

                        rptds.Transactions.Rows.Add(drNew);

                        /****************************sales transaction items*****************************/
                        Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                        mConnection = clsSalesTransactionItems.Connection; mTransaction = clsSalesTransactionItems.Transaction;

                        System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            drNew = rptds.SalesTransactionItems.NewRow();

                            foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
                                drNew[dc] = dr[dc.ColumnName];

                            rptds.SalesTransactionItems.Rows.Add(drNew);
                        }
                    }

                    clsSalesTransactions.CommitAndDispose();

                    rpt.SetDataSource(rptds);

                    CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
                    CrystalDecisions.Shared.ParameterValues currentValues;
                    CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

                    paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.CompanyName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.CashierName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0, 11);
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.Address1 +
                                            Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
                                            Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #".PadRight(15) + ":" + CompanyDetails.FaxPhone +
                                            Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg." +
                                            Environment.NewLine + "BIR Acc #: " + CONFIG.AccreditationNo + " SN#: " + CONFIG.MachineSerialNo;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["ORHeader"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSysConfigDetails.ORHeader;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    //foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
                    //{
                    //    if (objPic.Name.ToUpper() == "PICLOGO1")
                    //    {
                    //        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
                    //    }
                    //}

                    //CRViewer.Visible = true;
                    //CRViewer.ReportSource = rpt;
                    //CRViewer.Show();

                    try
                    {
                        DateTime logdate = DateTime.Now;
                        string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                        if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                        {
                            Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                        }
                        string logFile = logsdir + logdate.ToString("MMM") + "/OR_" + clsSalesTransactionDetails.TransactionNo + logdate.ToString("yyyyMMddhhmmss") + ".doc";

                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
                    }
                    catch { }

                    System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                    int i;
                    int rawKind = 0;

                    for (i = 0; i < printDoc.PrinterSettings.PaperSizes.Count; i++)
                    {
                        if (printDoc.PrinterSettings.PaperSizes[i].PaperName == "RetailPlusLXHalfSize")
                        {
                            rawKind = (int)GetField(printDoc.PrinterSettings.PaperSizes[i], "kind");
                            break;
                        }
                    }

                    if (isPrinterOnline(mclsTerminalDetails.SalesInvoicePrinterName))
                    {
                        rpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawKind;
                        rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        clsEvent.AddEventLn("will not print sales invoice to LX. printer is offline.", true, mclsSysConfigDetails.WillWriteSystemLog);
                    }


                    rpt.Close();
                    rpt.Dispose();

                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
            }
        }
        private object GetField(Object obj, String fieldName)
        {
            System.Reflection.FieldInfo fi = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            return fi.GetValue(obj);
        }

        public delegate void PrintDeliveryReceiptDelegate();
        public void PrintDeliveryReceipt()
        {
            try
            {
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (!mboIsInTransaction)
                {
                    MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

                if (loginresult == DialogResult.None)
                {
                    LogInWnd login = new LogInWnd();

                    login.AccessType = AccessTypes.CloseTransaction;
                    login.Header = "Print Delivery Receipt";
                    login.TerminalDetails = mclsTerminalDetails;
                    login.ShowDialog(this);
                    loginresult = login.Result;
                    login.Close();
                    login.Dispose();
                }
                if (loginresult == DialogResult.OK)
                {
                    CRSReports.DR rpt = new CRSReports.DR();

                    AceSoft.RetailPlus.Client.ReportDataset rptds = new AceSoft.RetailPlus.Client.ReportDataset();
                    System.Data.DataRow drNew;

                    /****************************sales transaction *****************************/
                    Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                    mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                    Data.SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(mclsSalesTransactionDetails.TransactionNo, mclsTerminalDetails.TerminalNo, mclsTerminalDetails.BranchID);

                    Data.Contacts clsContact = new Data.Contacts(mConnection, mTransaction);
                    Data.ContactDetails clsContactDetails = clsContact.Details(clsSalesTransactionDetails.CustomerID);

                    if (clsSalesTransactionDetails.isExist)
                    {
                        drNew = rptds.Transactions.NewRow();

                        drNew["TransactionID"] = clsSalesTransactionDetails.TransactionID;
                        drNew["TransactionNo"] = clsSalesTransactionDetails.TransactionNo;
                        drNew["CustomerName"] = clsSalesTransactionDetails.CustomerName;
                        drNew["CustomerAddress"] = clsContactDetails.Address;
                        drNew["CustomerTerms"] = clsContactDetails.Terms;
                        drNew["CustomerModeOfterms"] = clsContactDetails.ModeOfTerms;
                        drNew["CustomerBusinessName"] = clsContactDetails.BusinessName;
                        drNew["CustomerTelNo"] = clsContactDetails.TelephoneNo;
                        drNew["CashierName"] = clsSalesTransactionDetails.CashierName;
                        drNew["CreatedByName"] = clsSalesTransactionDetails.CreatedByName;
                        drNew["AgentName"] = clsSalesTransactionDetails.AgentName;
                        drNew["TerminalNo"] = clsSalesTransactionDetails.TerminalNo;
                        drNew["TransactionDate"] = clsSalesTransactionDetails.TransactionDate;
                        drNew["DateSuspended"] = clsSalesTransactionDetails.DateSuspended.ToString();
                        drNew["DateResumed"] = clsSalesTransactionDetails.DateResumed;
                        drNew["TransactionStatus"] = clsSalesTransactionDetails.TransactionStatus;
                        drNew["SubTotal"] = clsSalesTransactionDetails.SubTotal;
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Discount"] = clsSalesTransactionDetails.Discount;
                        drNew["VAT"] = clsSalesTransactionDetails.VAT;
                        drNew["VATableAmount"] = clsSalesTransactionDetails.VATableAmount;
                        drNew["LocalTax"] = clsSalesTransactionDetails.LocalTax;
                        drNew["AmountPaid"] = clsSalesTransactionDetails.AmountPaid;
                        drNew["CashPayment"] = clsSalesTransactionDetails.CashPayment;
                        drNew["ChequePayment"] = clsSalesTransactionDetails.ChequePayment;
                        drNew["CreditCardPayment"] = clsSalesTransactionDetails.CreditCardPayment;
                        drNew["CreditPayment"] = clsSalesTransactionDetails.CreditPayment;
                        drNew["BalanceAmount"] = clsSalesTransactionDetails.BalanceAmount;
                        drNew["ChangeAmount"] = clsSalesTransactionDetails.ChangeAmount;
                        drNew["DateClosed"] = clsSalesTransactionDetails.DateClosed;
                        drNew["PaymentType"] = clsSalesTransactionDetails.PaymentType.ToString("d");
                        drNew["ItemsDiscount"] = clsSalesTransactionDetails.ItemsDiscount;
                        drNew["Charge"] = clsSalesTransactionDetails.Charge;
                        drNew["isConsignment"] = clsSalesTransactionDetails.isConsignment;

                        rptds.Transactions.Rows.Add(drNew);

                        /****************************sales transaction items*****************************/
                        Data.SalesTransactionItems clsSalesTransactionItems = new Data.SalesTransactionItems(mConnection, mTransaction);
                        mConnection = clsSalesTransactionItems.Connection; mTransaction = clsSalesTransactionItems.Transaction;

                        System.Data.DataTable dt = clsSalesTransactionItems.List(clsSalesTransactionDetails.TransactionID, clsSalesTransactionDetails.TransactionDate, "TransactionItemsID", SortOption.Ascending);

                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            drNew = rptds.SalesTransactionItems.NewRow();

                            foreach (System.Data.DataColumn dc in rptds.SalesTransactionItems.Columns)
                                drNew[dc] = dr[dc.ColumnName];

                            rptds.SalesTransactionItems.Rows.Add(drNew);
                        }
                    }

                    clsSalesTransactions.CommitAndDispose();

                    rpt.SetDataSource(rptds);

                    CrystalDecisions.CrystalReports.Engine.ParameterFieldDefinition paramField;
                    CrystalDecisions.Shared.ParameterValues currentValues;
                    CrystalDecisions.Shared.ParameterDiscreteValue discreteParam;

                    paramField = rpt.DataDefinition.ParameterFields["CompanyName"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.CompanyName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PrintedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.CashierName;
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["PackedBy"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = mclsSalesTransactionDetails.WaiterName; // grpItems.Text.Remove(0, 11);
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    paramField = rpt.DataDefinition.ParameterFields["CompanyAddress"];
                    discreteParam = new CrystalDecisions.Shared.ParameterDiscreteValue();
                    discreteParam.Value = CompanyDetails.Address1 +
                                            Environment.NewLine + CompanyDetails.Address2 + ", " + CompanyDetails.City + " " + CompanyDetails.Country +
                                            Environment.NewLine + "Tel #: " + CompanyDetails.OfficePhone + " Fax #".PadRight(15) + ":" + CompanyDetails.FaxPhone +
                                            Environment.NewLine + "TIN : " + CompanyDetails.TIN + "      VAT Reg.";
                    currentValues = new CrystalDecisions.Shared.ParameterValues();
                    currentValues.Add(discreteParam);
                    paramField.ApplyCurrentValues(currentValues);

                    //foreach (CrystalDecisions.CrystalReports.Engine.ReportObject objPic in rpt.Section1.ReportObjects)
                    //{
                    //    if (objPic.Name.ToUpper() == "PICLOGO1")
                    //    {
                    //        objPic = new Bitmap(Application.StartupPath + "/images/ReportLogo.jpg");
                    //    }
                    //}

                    //CRViewer.Visible = true;
                    //CRViewer.ReportSource = rpt;
                    //CRViewer.Show();

                    try
                    {
                        DateTime logdate = DateTime.Now;
                        string logsdir = System.Configuration.ConfigurationManager.AppSettings["logsdir"].ToString();

                        if (!Directory.Exists(logsdir + logdate.ToString("MMM")))
                        {
                            Directory.CreateDirectory(logsdir + logdate.ToString("MMM"));
                        }
                        string logFile = logsdir + logdate.ToString("MMM") + "/DR_" + clsSalesTransactionDetails.TransactionNo + logdate.ToString("yyyyMMddhhmmss") + ".doc";

                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, logFile);
                    }
                    catch { }

                    if (isPrinterOnline(mclsTerminalDetails.SalesInvoicePrinterName))
                    {
                        rpt.PrintOptions.PrinterName = mclsTerminalDetails.SalesInvoicePrinterName;
                        rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    else
                    {
                        clsEvent.AddEventLn("will not print delivery receipt. printer is offline.", true, mclsSysConfigDetails.WillWriteSystemLog);
                    }

                    rpt.Close();
                    rpt.Dispose();

                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
            }
        }

        public void PrintReportPageHeaderSection(bool isReceipt, bool isCheckOutBill = false)
        {

            Receipt clsReceipt = new Receipt(mConnection, mTransaction);
            ReceiptDetails clsReceiptDetails;

            // print Page Header
            int iCtr = 0;
            string stModule = "";
            for (iCtr = 1; iCtr <= 10; iCtr++)
            {
                stModule = "PageHeader" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, isReceipt, DateTime.MinValue, isCheckOutBill);
            }

            PrintItemHeader();

            clsReceipt.CommitAndDispose();
        }
        public void PrintReportHeadersSection(bool IsReceipt)
        {
            PrintReportHeaderSection(IsReceipt, mdteOverRidingPrintDate);
            PrintReportPageHeaderSectionChecked(IsReceipt);
            mdteOverRidingPrintDate = DateTime.MinValue;
        }
        public void PrintReportHeadersSection(bool IsReceipt, DateTime OverRidingPrintDate)
        {
            PrintReportHeaderSection(IsReceipt, OverRidingPrintDate);
            PrintReportPageHeaderSectionChecked(IsReceipt);
        }
        public void PrintReportHeaderSection(bool IsReceipt, DateTime OverRidingPrintDate)
        {
            //PosExplorer posExplorer = new PosExplorer();
            //DeviceInfo deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, mclsTerminalDetails.PrinterName);
            //m_Printer = (PosPrinter) posExplorer.CreateInstance(deviceInfo);

            //m_Printer.Open();
            ////Then the device is disable from other application
            //m_Printer.Claim(1000);
            ////Enable the device.
            //m_Printer.DeviceEnabled = true;

            ////'Output by the high quality mode

            //m_Printer.RecLetterQuality = true;

            ////'Release the exclusive control right

            //m_Printer.Release();

            ////m_Printer.SetBitmap(1, PrinterStation.Receipt, strFilePath, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);

            msbToPrint.Clear(); // reset the transaction to print in POSPrinter
            msbToPrint = new StringBuilder(); // reset the transaction to print in POSPrinter

            Reports.Receipt clsReceipt = new Reports.Receipt(mConnection, mTransaction);
            Reports.ReceiptDetails clsReceiptDetails;

            clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportHeaderSpacer);

            // print Report Header Spacer
            for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
            { msbToPrint.Append(Environment.NewLine); }

            mclsFilePrinter = new FilePrinter();
            if (string.IsNullOrEmpty(mclsSalesTransactionDetails.TransactionNo) || mclsSalesTransactionDetails.TransactionNo == "READY..." || mclsSalesTransactionDetails.TransactionNo == mclsFilePrinter.FileName)
                mclsFilePrinter.FileName = DateTime.Now.ToString("yyyyMMddhhmmss");
            else
                mclsFilePrinter.FileName = mclsSalesTransactionDetails.TransactionNo;

            if (mclsTerminalDetails.IsPrinterDotMatrix) msbToPrint.Append(CenterString(CompanyDetails.CompanyCode, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            else msbToPrint.Append(RawPrinterHelper.escBoldHWOn + RawPrinterHelper.escAlignCenter + CompanyDetails.CompanyCode + RawPrinterHelper.escAlignDef + RawPrinterHelper.escBoldHWOff + Environment.NewLine);

            // print Report Header
            int iCtr = 0;
            string stModule = "";
            for (iCtr = 1; iCtr <= 10; iCtr++)
            {
                stModule = "ReportHeader" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, IsReceipt, OverRidingPrintDate);
            }
            clsReceipt.CommitAndDispose();
        }

        public void PrintReportPageHeaderSectionChecked(bool IsReceipt)
        {
            if (IsReceipt)
            {
                // print page header <-- second transaction header
                int iCtr = 0;
                string stModule = "";

                Receipt clsReceipt = new Receipt(mConnection, mTransaction);
                ReceiptDetails clsReceiptDetails;

                for (iCtr = 1; iCtr <= 10; iCtr++)
                {
                    stModule = "PageHeader" + iCtr;
                    clsReceiptDetails = clsReceipt.Details(stModule);

                    PrintReportValue(clsReceiptDetails, IsReceipt, DateTime.MinValue);
                }

                PrintItemHeader();

                clsReceipt.CommitAndDispose();
            }
        }

        public void PrintItemHeader()
        {
            msbToPrint.Append(Environment.NewLine);
            msbToPrint.Append("DESC".PadRight(mclsTerminalDetails.MaxReceiptWidth - 29));
            msbToPrint.Append("QTY".PadLeft(6));
            msbToPrint.Append("PRICE".PadLeft(10));
            msbToPrint.Append("AMOUNT".PadLeft(13));
            msbToPrint.Append(Environment.NewLine);
            msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
        }
        public void PrintItemForKitchen(string Description, string stProductUnitCode, decimal Quantity, string PrinterName)
        {
            // description
            string stDescription = Description;
            try
            { stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
            catch { }
            try
            { stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
            catch { }

            SendOrderSlipToPrinter(stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
            SendOrderSlipToPrinter(stProductUnitCode.PadRight(6), PrinterName);

            string stQuantity = Quantity.ToString("#,##0.#0");
            if (Quantity == 1 || Quantity == -1)
                stQuantity = "1";
            else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
            { stQuantity = Quantity.ToString("#,##0"); }

            SendOrderSlipToPrinter(stQuantity.PadLeft(6), PrinterName);
            SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
        }
        public void PrintItemForKitchen(string Description, string stProductUnitCode, decimal Quantity, string PrinterName, bool bolBIG)
        {
            // description
            string stDescription = Description;
            try
            { stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
            catch { }
            try
            { stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
            catch { }

            string stQuantity = Quantity.ToString("#,##0.#0");
            if (Quantity == 1 || Quantity == -1)
                stQuantity = "1";
            else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
            { stQuantity = Quantity.ToString("#,##0"); }

            if (bolBIG)
            {
                if (mclsTerminalDetails.IsPrinterDotMatrix) SendOrderSlipToPrinter(stQuantity + "x" + stProductUnitCode + " " + stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
                else SendOrderSlipToPrinter(RawPrinterHelper.escFontHeightX2On + stQuantity + "x" + stProductUnitCode + " " + stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12) + RawPrinterHelper.escFontHeightX2Off, PrinterName);
            }
            else
            {
                SendOrderSlipToPrinter(stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth - 12), PrinterName);
                SendOrderSlipToPrinter(stProductUnitCode.PadRight(6), PrinterName);
                SendOrderSlipToPrinter(stQuantity.PadLeft(6), PrinterName);
            }

            SendOrderSlipToPrinter(Environment.NewLine, PrinterName);
        }

        public delegate void PrintItemDelegate(string ItemNo, string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT);
        public void PrintItem(string ItemNo, string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT)
        {
            if (!mboIsItemHeaderPrinted)
            {
                PrintReportHeadersSection(true);

                mboIsItemHeaderPrinted = true;
            }

            // description
            string stDescription = Description;
            try
            { stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
            catch { }
            try
            { stDescription = stDescription.Substring(0, mclsTerminalDetails.MaxReceiptWidth); }
            catch { }

            // discount and promo
            string AddedString = "";
            if (Discount != 0)
            { AddedString += "@" + Discount.ToString("#,##0.#0") + "disc "; }
            if (PromoApplied != 0)
            { AddedString += "@" + PromoApplied.ToString("#,##0.#0") + "promo "; }

            // price
            string stPrice = Price.ToString("#,##0.#0");
            if (Price == 1 || Price == -1)
                stPrice = "1";
            else if (Decimal.Compare(Price, Decimal.Floor(Price)) == 0)
            { stPrice = Price.ToString("#,##0"); }

            // quantity
            string stQuantity = Quantity.ToString("#,##0.#0");
            if (Quantity == 1 || Quantity == -1)
                stQuantity = "1";
            else if (Decimal.Compare(Quantity, Decimal.Floor(Quantity)) == 0)
            { stQuantity = Quantity.ToString("#,##0"); }

            // evat and vat
            bool isVATable = false;
            if (VAT > 0)
            { isVATable = true; }

            string stAmount = Amount.ToString("#,##0.#0");
            //			if (Decimal.Compare(Amount, Decimal.Floor(Amount)) == 0)
            //			{	stAmount = Amount.ToString("#,##0");	}

            if (mclsSalesTransactionDetails.DiscountCode != mclsTerminalDetails.SeniorCitizenDiscountCode)
            {
                if (isVATable)
                { stAmount += "V "; }
                else
                { stAmount += "NV"; }
            }

            if (stDescription.Length <= 14 && AddedString.Length == 0)
                msbToPrint.Append(stDescription.PadRight(14));
            else
                msbToPrint.Append(stDescription.PadRight(mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);

            if (AddedString.Length <= 11 && AddedString.Length != 0)
                msbToPrint.Append(AddedString.PadRight(11));
            else if (AddedString.Length > 11)
                msbToPrint.Append(AddedString.PadRight(mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);

            if (stQuantity.Length <= 4 && stDescription.Length <= 14 && AddedString.Length == 0)
                msbToPrint.Append(stQuantity.PadLeft(3));
            else if (stQuantity.Length <= 6 && AddedString.Length == 0)
                msbToPrint.Append(stQuantity.PadLeft(17));
            else if (stQuantity.Length <= 6 && AddedString.Length > 11)
                msbToPrint.Append(stQuantity.PadLeft(17));
            else if (stQuantity.Length <= 6 && AddedString.Length != 0)
                msbToPrint.Append(stQuantity.PadLeft(6));
            else if (stQuantity.Length <= 17 && AddedString.Length == 0)
                msbToPrint.Append(stQuantity.PadLeft(17));
            else if (stQuantity.Length <= 17 && AddedString.Length != 0)
                msbToPrint.Append(stQuantity.PadLeft(17));
            else
                msbToPrint.Append(stQuantity.PadLeft(stQuantity.Length));

            if (stPrice.Length <= 10)
                msbToPrint.Append(stPrice.PadLeft(10));
            else
                msbToPrint.Append(Environment.NewLine + stPrice.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 13));

            if (stAmount.Length <= 13)
                msbToPrint.Append(stAmount.PadLeft(13));
            else
                msbToPrint.Append(Environment.NewLine + stAmount.PadLeft(mclsTerminalDetails.MaxReceiptWidth));

            msbToPrint.Append(Environment.NewLine);
        }

        public void PrintPageFooterASection()
        {
            int iCtr = 0;
            string stModule = "";
            Receipt clsReceipt = new Receipt(mConnection, mTransaction);
            ReceiptDetails clsReceiptDetails;

            // print page footer
            for (iCtr = 1; iCtr <= 20; iCtr++)
            {
                stModule = "PageFooterA" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, false, DateTime.MinValue);
            }

            clsReceipt.CommitAndDispose();
        }
        public void PrintPageFooterBSection()
        {
            int iCtr = 0;
            string stModule = "";
            Receipt clsReceipt = new Receipt(mConnection, mTransaction);
            ReceiptDetails clsReceiptDetails;

            // print page footer
            for (iCtr = 1; iCtr <= 5; iCtr++)
            {
                stModule = "PageFooterB" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, false, DateTime.MinValue);
            }

            clsReceipt.CommitAndDispose();
        }
        public void PrintReportFooter(bool IsReceipt, bool boPrintTermsAndConditions = false)
        {
            Receipt clsReceipt = new Receipt(mConnection, mTransaction);
            ReceiptDetails clsReceiptDetails;

            // print report footer
            int iCtr = 0;
            string stModule = "";
            for (iCtr = 1; iCtr <= 5; iCtr++)
            {
                stModule = "ReportFooter" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, IsReceipt, DateTime.MinValue);
            }

            if (mclsTerminalDetails.IncludeTermsAndConditions & boPrintTermsAndConditions)
                PrintTermsAndConditions();

            // Sep 14, 2014 get the msbToPrint to remove the spacers.
            msbEJournalToPrint = new StringBuilder(msbToPrint.ToString());

            // print report footer Spacer
            clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportFooterSpacer);
            for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
            { msbToPrint.Append(Environment.NewLine); }
            clsReceipt.CommitAndDispose();

            // do the actual print
            if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
            {
                mclsFilePrinter.Write(msbToPrint);
                RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, mclsFilePrinter.FileName, "RetailPlus " + mclsFilePrinter.FileName);
                mclsFilePrinter.DeleteFile();

                //cut the paper if printer is auto cutter
                if (mclsTerminalDetails.IsPrinterAutoCutter)
                    CutPrinterPaper();
            }

            // Sep 14, 2014 add electronic journal only for all valid transactions with transaction no
            if (mclsSalesTransactionDetails.TransactionID != 0)
            {
                msbEJournalToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                
                mclsEJournal = new EJournalPrinter();
                if (string.IsNullOrEmpty(mstrBeginningTransactionNo))
                {
                    Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                    mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;
                    mstrBeginningTransactionNo = clsTerminalReport.Details(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo).BeginningTransactionNo;
                    clsTerminalReport.CommitAndDispose();
                }
                mclsEJournal.FileName = mstrBeginningTransactionNo;
                mclsEJournal.Write(msbEJournalToPrint);
                
            }
        }
        public void PrintPageAndReportFooterSection(bool IsReceipt, DateTime OverRidingPrintDate, bool boPrintTermsAndConditions = false)
        {
            //if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
            //{
            int iCtr = 0;
            string stModule = "";
            Receipt clsReceipt = new Receipt(mConnection, mTransaction);
            ReceiptDetails clsReceiptDetails;

            if (IsReceipt)
            {
                // print page footer
                for (iCtr = 1; iCtr <= 20; iCtr++)
                {
                    stModule = "PageFooterA" + iCtr;
                    clsReceiptDetails = clsReceipt.Details(stModule);

                    PrintReportValue(clsReceiptDetails, IsReceipt, OverRidingPrintDate);
                }
                // print page footer
                for (iCtr = 1; iCtr <= 5; iCtr++)
                {
                    stModule = "PageFooterB" + iCtr;
                    clsReceiptDetails = clsReceipt.Details(stModule);

                    PrintReportValue(clsReceiptDetails, IsReceipt, OverRidingPrintDate);
                }
            }

            // print report footer
            for (iCtr = 1; iCtr <= 5; iCtr++)
            {
                stModule = "ReportFooter" + iCtr;
                clsReceiptDetails = clsReceipt.Details(stModule);

                PrintReportValue(clsReceiptDetails, IsReceipt, OverRidingPrintDate);
            }

            if (mclsTerminalDetails.IncludeTermsAndConditions & boPrintTermsAndConditions)
                PrintTermsAndConditions();

            // Sep 14, 2014 get the msbToPrint to remove the spacers.
            msbEJournalToPrint = msbToPrint;

            // print report footer Spacer
            clsReceiptDetails = clsReceipt.Details(ReportFormatModule.ReportFooterSpacer);
            for (int i = 0; i < Convert.ToInt32(clsReceiptDetails.Value); i++)
            { msbToPrint.Append(Environment.NewLine); }
            clsReceipt.CommitAndDispose();

            // do the actual print
            if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
            {
                mclsFilePrinter.Write(msbToPrint);
                RawPrinterHelper.SendFileToPrinter(mclsTerminalDetails.PrinterName, mclsFilePrinter.FileName, "RetailPlus " + mclsFilePrinter.FileName);
                mclsFilePrinter.DeleteFile();

                //print the first part of transaction header if autocutter
                if (mclsTerminalDetails.IsPrinterAutoCutter)
                    //cut the paper if printer is auto cutter
                    CutPrinterPaper();
            }

            // Sep 14, 2014 add electronic journal only for all valid transactions with transaction no
            if (mclsSalesTransactionDetails.TransactionID != 0)
            {
                msbEJournalToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                mclsEJournal = new EJournalPrinter();
                if (string.IsNullOrEmpty(mstrBeginningTransactionNo))
                {
                    Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                    mstrBeginningTransactionNo = clsTerminalReport.Details(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo).BeginningTransactionNo;
                    clsTerminalReport.CommitAndDispose();
                }
                mclsEJournal.FileName = mstrBeginningTransactionNo;
                mclsEJournal.Write(msbEJournalToPrint);
            }
        }
        public void PrintTermsAndConditions()
        {
            msbToPrint.Append(Environment.NewLine);
            msbToPrint.Append("Terms and Conditions".PadRight(15) + ":" + Environment.NewLine + Environment.NewLine);
            msbToPrint.Append("1.Items not claimed w/in 30days will  be" + Environment.NewLine);
            msbToPrint.Append("  charged double the cost of  service." + Environment.NewLine);
            msbToPrint.Append("2.Items  not claimed w/in 60days will be" + Environment.NewLine);
            msbToPrint.Append("  disposed of to   cover   the  cost  of" + Environment.NewLine);
            msbToPrint.Append("  services rendered." + Environment.NewLine);
            msbToPrint.Append("3.We are not  liable   for  any  damages" + Environment.NewLine);
            msbToPrint.Append("  incurred due to the natural  effect of" + Environment.NewLine);
            msbToPrint.Append("  washer and  dryer  to the garments. To" + Environment.NewLine);
            msbToPrint.Append("  avoid  such  incidence, clients should" + Environment.NewLine);
            msbToPrint.Append("  declare  the   fragility  of  items to" + Environment.NewLine);
            msbToPrint.Append("  washer, dryer and pressing." + Environment.NewLine);
            msbToPrint.Append("4.We are not liable for any  damages  in" + Environment.NewLine);
            msbToPrint.Append("  case of fire,flood and other unforseen" + Environment.NewLine);
            msbToPrint.Append("  events or loss through  force majeure." + Environment.NewLine);
            msbToPrint.Append("5.We are  not  liable  for  any  changes" + Environment.NewLine);
            msbToPrint.Append("  resulting from normal washing process," + Environment.NewLine);
            msbToPrint.Append("  loss of buttons,  anything left in the" + Environment.NewLine);
            msbToPrint.Append("  pockets including shrinkage and fading" + Environment.NewLine);
            msbToPrint.Append("6.Liability of  loss is  limited  to  an" + Environment.NewLine);
            msbToPrint.Append("  amount not  exceeding  three (3) times" + Environment.NewLine);
            msbToPrint.Append("  the laundry charges." + Environment.NewLine);
            msbToPrint.Append("7.We serve the right to confirm accuracy" + Environment.NewLine);
            msbToPrint.Append("  of the items for laundry & inform the" + Environment.NewLine);
            msbToPrint.Append("  customers of  any  discrepancy  within" + Environment.NewLine);
            msbToPrint.Append("  24 hours." + Environment.NewLine);
            msbToPrint.Append("8.Complaints  will  only be  entertained" + Environment.NewLine);
            msbToPrint.Append("  within 24hours from date of  delivery." + Environment.NewLine);
            msbToPrint.Append(Environment.NewLine);
            msbToPrint.Append(Environment.NewLine);

        }
        public void PrintReportFooterSection(bool IsReceipt, TransactionStatus status, decimal TotalItemSold, decimal TotalQuantitySold, decimal SubTotal, decimal Discount, decimal Charge, decimal AmountPaid, decimal CashPayment, decimal ChequePayment, decimal CreditCardPayment, decimal CreditPayment, decimal DebitPayment, decimal RewardPointsPayment, decimal RewardConvertedPayment, decimal ChangeAmount, ArrayList arrChequePaymentDetails, ArrayList arrCreditCardPaymentDetails, ArrayList arrCreditPaymentDetails, ArrayList arrDebitPaymentDetails, bool boPrintTermsAndConditions = false)
        {
            //if (mclsTerminalDetails.AutoPrint != PrintingPreference.AskFirst)
            //{
            if ((status == TransactionStatus.ParkingTicket && IsReceipt) || status != TransactionStatus.ParkingTicket)
                PrintPageFooterASection();

            if (status == TransactionStatus.Refund)
            {
                if (CashPayment != 0)
                    msbToPrint.Append("Cash Refund".PadRight(15) + ":" + CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                if (ChequePayment != 0)
                {
                    msbToPrint.Append("Cheque Refund".PadRight(15) + ":" + ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                    if (arrChequePaymentDetails != null)
                    {
                        foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
                        {
                            //print cheque details
                            msbToPrint.Append("Cheque No.".PadRight(15) + ":" + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Amount".PadRight(15) + ":" + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Validity Date".PadRight(15) + ":" + chequepaymentdet.ValidityDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }
                }

                if (CreditCardPayment != 0)
                {
                    msbToPrint.Append("Credit Card Refund: " + CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                    if (arrCreditCardPaymentDetails != null)
                    {
                        foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
                        {
                            //print credit card details
                            msbToPrint.Append("Card Type".PadRight(15) + ":" + cardpaymentdet.CardTypeCode.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Card No.".PadRight(15) + ":" + cardpaymentdet.CardNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Member Name".PadRight(15) + ":" + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Amount".PadRight(15) + ":" + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Validity Date".PadRight(15) + ":" + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }
                }
            }
            else if (status == TransactionStatus.Closed || status == TransactionStatus.Reprinted || status == TransactionStatus.Open || status == TransactionStatus.CreditPayment)
            {
                if (CashPayment != 0)
                    msbToPrint.Append("Cash Payment".PadRight(15) + ":" + CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                if (ChequePayment != 0)
                {
                    msbToPrint.Append("Cheque Payment".PadRight(15) + ":" + ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                    if (arrChequePaymentDetails != null)
                    {
                        foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
                        {
                            //print checque details
                            msbToPrint.Append("Cheque No.".PadRight(15) + ":" + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Amount".PadRight(15) + ":" + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Validity Date".PadRight(15) + ":" + chequepaymentdet.ValidityDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }
                }

                if (CreditCardPayment != 0)
                {
                    msbToPrint.Append("C.Card Paymnt".PadRight(15) + ":" + CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    if (arrCreditCardPaymentDetails != null)
                    {
                        foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
                        {
                            //print credit card details
                            msbToPrint.Append("Card Type".PadRight(15) + ":" + cardpaymentdet.CardTypeCode.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Card No.".PadRight(15) + ":" + cardpaymentdet.CardNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Member Name".PadRight(15) + ":" + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Amount".PadRight(15) + ":" + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Validity Date".PadRight(15) + ":" + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }
                }
                if (CreditPayment != 0)
                {
                    msbToPrint.Append("Credit Paymnt".PadRight(15) + ":" + CreditPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    if (arrCreditPaymentDetails != null)
                    {
                        foreach (Data.CreditPaymentDetails creditpaymentdet in arrCreditPaymentDetails)
                        {
                            //print credit details
                            msbToPrint.Append("Amount".PadRight(15) + ":" + creditpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Remarks".PadRight(15) + ":" + creditpaymentdet.Remarks + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }


                }
                if (DebitPayment != 0)
                {
                    msbToPrint.Append("Debit  Paymnt".PadRight(15) + ":" + DebitPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    if (arrDebitPaymentDetails != null)
                    {
                        foreach (Data.DebitPaymentDetails debitpaymentdet in arrDebitPaymentDetails)
                        {
                            //print credit details
                            msbToPrint.Append("Amount".PadRight(15) + ":" + debitpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                            msbToPrint.Append("Remarks".PadRight(15) + ":" + debitpaymentdet.Remarks + Environment.NewLine);
                            msbToPrint.Append(Environment.NewLine);
                        }
                    }
                }
                if (RewardConvertedPayment != 0)
                {
                    msbToPrint.Append("Reward Payment".PadRight(15) + ":" + RewardConvertedPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                }
            }

            if (status == TransactionStatus.Suspended)
            {
                msbToPrint.Append(CenterString("*****THIS TRANSACTION IS SUSPENDED*****", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else if (status == TransactionStatus.Void)
            {
                msbToPrint.Append(CenterString("*******THIS TRANSACTION IS VOID*******", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else if (status == TransactionStatus.Reprinted && !mclsSysConfigDetails.WillNotPrintReprintMessage)
            {
                msbToPrint.Append(CenterString("**THIS TRANSACTION IS REPRINTED AS OF**", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else if (status == TransactionStatus.Refund)
            {
                msbToPrint.Append(CenterString("*****THIS TRANSACTION IS A REFUND*****", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else if (status == TransactionStatus.CreditPayment)
            {
                if (mclsSysConfigDetails.WillPrintCreditPaymentHeader)
                    msbToPrint.Append(CenterString("------CREDIT PAYMENT--------", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }
            else if (status == TransactionStatus.ParkingTicket)
            {
                msbToPrint.Append(CenterString("------PARKING TICKET--------", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
            }

            PrintPageFooterBSection();
            PrintReportFooter(IsReceipt, boPrintTermsAndConditions);

            ////cut the paper if printer is auto cutter
            //if (mclsTerminalDetails.IsPrinterAutoCutter)
            //    CutPrinterPaper();

        }

        public void PrintParkingTicket()
        {
            try
            {
                if (mclsTerminalDetails.AutoPrint == PrintingPreference.Auto)
                {
                    MessageBox.Show("Sorry this option is not applicable for Auto-Print receipt.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                if (!mboIsInTransaction)
                {
                    MessageBox.Show("No active transaction is found! Please transact first.", "RetailPlus", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    return;
                }
                DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.CloseTransaction);

                if (loginresult == DialogResult.None)
                {
                    LogInWnd login = new LogInWnd();

                    login.AccessType = AccessTypes.CloseTransaction;
                    login.Header = "Print Parking Ticket Access Validation";
                    login.TerminalDetails = mclsTerminalDetails;
                    login.ShowDialog(this);
                    loginresult = login.Result;
                    login.Close();
                    login.Dispose();
                }
                if (loginresult == DialogResult.OK)
                {
                    PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    PrintReportHeaderSection(true, DateTime.MinValue);
                    mboIsItemHeaderPrinted = true;

                    AceSoft.BarcodePrinter clsBarcodePrinter = new BarcodePrinter();
                    if (mclsTerminalDetails.IsPrinterDotMatrix)
                    {
                        msbToPrint.Append(clsBarcodePrinter.GenerateBarCode(mclsSalesTransactionDetails.TransactionNo, AceSoft.printerModel.EpsonTest, barcodeType.EAN13) + Environment.NewLine);
                        msbToPrint.Append(CenterString("-/- PARKING TICKET -/-", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        msbToPrint.Append(CenterString("NOT VALID AS RECEIPT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    else
                    {
                        msbToPrint.Append(clsBarcodePrinter.GenerateBarCode(mclsSalesTransactionDetails.TransactionNo, AceSoft.printerModel.Epson, barcodeType.EAN13) + Environment.NewLine);
                        msbToPrint.Append(RawPrinterHelper.escBoldHOn + CenterString("-/- PARKING TICKET -/-", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escBoldHOff + Environment.NewLine);
                        msbToPrint.Append(RawPrinterHelper.escBoldHOn + CenterString("NOT VALID AS RECEIPT", mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escBoldHOff + Environment.NewLine);
                    }
                    PrintReportPageHeaderSection(true);

                    msbToPrint.Append(Environment.NewLine + RawPrinterHelper.escEmphasizedOn + CenterString("TIME IN: " + mclsSalesTransactionDetails.TransactionDate.ToString("MM/dd/yyyy hh:mm tt"), mclsTerminalDetails.MaxReceiptWidth) + RawPrinterHelper.escEmphasizedOff + Environment.NewLine + Environment.NewLine);

                    PrintReportFooterSection(false, TransactionStatus.ParkingTicket, mclsSalesTransactionDetails.ItemSold, mclsSalesTransactionDetails.QuantitySold, mclsSalesTransactionDetails.SubTotal, mclsSalesTransactionDetails.Discount, mclsSalesTransactionDetails.Charge, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

                    mboIsItemHeaderPrinted = false;
                    mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing Parking Slip: ");
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// This is to print the Internal Credit Card used for payment
        /// </summary>
        /// <param name="clsChargeSlipType"></param>
        public void PrintChargeSlip(ChargeSlipType clsChargeSlipType)
        {
            try
            {
                decimal decInternalCreditCardPayment = 0;
                Data.ContactDetails clsCreditorDetails = new Data.ContactDetails();
                Data.ContactDetails clsGuarantorDetails = new Data.ContactDetails();
                Data.CardTypeDetails clsCreditCardTypeDetails = new Data.CardTypeDetails();

                foreach (Data.CreditCardPaymentDetails clsCreditCardPaymentDetails in mclsSalesTransactionDetails.PaymentDetails.arrCreditCardPaymentDetails)
                {
                    if (clsCreditCardPaymentDetails.CardTypeDetails.CreditCardType == CreditCardTypes.Internal)
                    {
                        decInternalCreditCardPayment += clsCreditCardPaymentDetails.Amount;
                        clsCreditCardTypeDetails = clsCreditCardPaymentDetails.CardTypeDetails;
                        clsCreditorDetails = clsCreditCardPaymentDetails.CreditorDetails;

                        Data.Contacts clsContacts = new Data.Contacts(mConnection, mTransaction);
                        mConnection = clsContacts.Connection; mTransaction = clsContacts.Transaction;

                        if (clsCreditCardTypeDetails.WithGuarantor && clsCreditorDetails.CreditDetails.GuarantorID != 0)
                        {
                            clsGuarantorDetails = new Data.Contacts(mConnection, mTransaction).Details(clsCreditorDetails.CreditDetails.GuarantorID);
                        }
                        clsContacts.CommitAndDispose();
                    }
                }

                if (decInternalCreditCardPayment != 0)
                {
                    PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    PrintReportHeaderSection(false, DateTime.MinValue);

                    if (!string.IsNullOrEmpty(mclsSysConfigDetails.ChargeSlipHeaderLabel))
                    {
                        msbToPrint.Append(CenterString(mclsSysConfigDetails.ChargeSlipHeaderLabel, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        msbToPrint.Append(CenterString(Constants.C_FE_NOT_VALID_AS_RECEIPT, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }

                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("Trans. Date".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("OR #".PadRight(15) + ":" + mclsSalesTransactionDetails.ORNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    
                    // print the guarantor if with guarantor
                    if (clsCreditCardTypeDetails.WithGuarantor)
                    {
                        msbToPrint.Append(CenterString(clsGuarantorDetails.ContactCode, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                        msbToPrint.Append(CenterString("Guarantor", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    msbToPrint.Append(Environment.NewLine);

                    // print the charge slip header
                    if (string.IsNullOrEmpty(clsCreditCardTypeDetails.CardTypeName))
                    { msbToPrint.Append(CenterString("CHARGE SLIP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine); }
                    else { msbToPrint.Append(CenterString(clsCreditCardTypeDetails.CardTypeName + " CHARGE SLIP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine); }

                    
                    // print the amount and agreement
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("Amount of Purchase".PadRight(15) + ":" + decInternalCreditCardPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    if (mclsTerminalDetails.IncludeCreditChargeAgreement)
                    {
                        msbToPrint.Append("I hereby agree  to pay the total  amount" + Environment.NewLine);
                        msbToPrint.Append("stated herein including any charges  due" + Environment.NewLine);
                        msbToPrint.Append("thereon  subject   to    the   pertinent" + Environment.NewLine);
                        msbToPrint.Append("contract   governing  the use of    this" + Environment.NewLine);
                        msbToPrint.Append("Credit Card." + Environment.NewLine);
                        msbToPrint.Append(Environment.NewLine);
                        msbToPrint.Append(Environment.NewLine);
                    }
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString(mclsSalesTransactionDetails.CustomerName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString(mclsSalesTransactionDetails.CashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(CenterString("Cashier", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    switch (clsChargeSlipType)
                    {
                        case ChargeSlipType.Customer:
                            msbToPrint.Append(CenterString("Customer's Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                            break;
                        case ChargeSlipType.Original:
                            msbToPrint.Append(CenterString("Original Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                            break;
                        case ChargeSlipType.Guarantor:
                            msbToPrint.Append(CenterString("Guarantor's Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                            break;
                    }

                    PrintPageAndReportFooterSection(false, DateTime.MinValue);

                    mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                    InsertAuditLog(AccessTypes.PrintTransactionHeader, "Print Charge Slip: " + clsChargeSlipType.ToString("G") + " TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing Charge Slip: " + clsChargeSlipType.ToString("G"));
            }
            Cursor.Current = Cursors.Default;
        }
        public void PrintRewardsRedemptionSlip()
        {
            // this should comes before earning of points otherwise this will be wrong.
            try
            {
                if (mclsSalesTransactionDetails.RewardPointsPayment != 0)
                {
                    PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    PrintReportHeaderSection(false, DateTime.MinValue);
                    if (GetReceiptFormatParameter(ReceiptFieldFormats.RewardsPermitNo, false, DateTime.MinValue) != string.Empty) msbToPrint.Append(CenterString("BIR Permit No." + GetReceiptFormatParameter(ReceiptFieldFormats.RewardsPermitNo, false, DateTime.MinValue), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);

                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("Transaction Date".PadRight(15) + ":" + mclsSalesTransactionDetails.TransactionDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(CenterString("R E D E M P T I O N   S L I P", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("Rewards Card No.".PadRight(15) + ":" + mclsContactDetails.RewardDetails.RewardCardNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("Total Points".PadRight(15) + ":" + mclsSalesTransactionDetails.RewardPreviousPoints.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("Redeemed Points".PadRight(15) + ":" + mclsSalesTransactionDetails.RewardPointsPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("Balance Points".PadRight(15) + ":" + mclsSalesTransactionDetails.RewardCurrentPoints.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString(mclsSalesTransactionDetails.CustomerName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString(mclsSalesTransactionDetails.CashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(CenterString("Cashier", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(CenterString("Original Copy", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);

                    PrintPageAndReportFooterSection(false, DateTime.MinValue);

                    mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                    InsertAuditLog(AccessTypes.PrintTransactionHeader, "Print Rewards Redeemption Slip: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing Rewards Redeemption Slip. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }
        public void PrintCreditVerificationSlip(Data.ContactDetails CreditorDetails)
        {
            // this should comes before earning of points otherwise this will be wrong.
            try
            {
                if (CreditorDetails.ContactID != 0)
                {
                    PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                    mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                    PrintReportHeaderSection(false, DateTime.MinValue);
                    if (!string.IsNullOrEmpty(mclsSysConfigDetails.CreditVerificationSlipHeaderLabel))
                    {
                        msbToPrint.Append(CenterString(mclsSysConfigDetails.CreditVerificationSlipHeaderLabel.Replace("{CardTypeCode}", CreditorDetails.CreditDetails.CardTypeDetails.CardTypeCode).Replace("{CardTypeName}", CreditorDetails.CreditDetails.CardTypeDetails.CardTypeName), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }

                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(CenterString("V E R I F I C A T I O N   S L I P", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("Validty Date".PadRight(15) + ":" + DateTime.Now.ToString("yyyy-MMM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("Credit Card No".PadRight(15) + ":" + CreditorDetails.CreditDetails.CreditCardNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("Name".PadRight(15) + ":" + CreditorDetails.ContactName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("AvailableCredit".PadRight(15) + ":" + (CreditorDetails.CreditLimit - CreditorDetails.Credit).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString("Verified By:" + mclsSalesTransactionDetails.CashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("Grocery" + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("Department Store" + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);
                    msbToPrint.Append("-".PadLeft(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("Appliance Center" + Environment.NewLine);
                    msbToPrint.Append(Environment.NewLine);

                    PrintPageAndReportFooterSection(false, DateTime.MinValue);

                    mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                    InsertAuditLog(AccessTypes.PrintTransactionHeader, "Print Credit VerificationSlip: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                }
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing Rewards Redeemption Slip. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintZRead
        public void PrintZRead(bool pvtWillPreviewReport)
        {
            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
            clsTerminalReport.CommitAndDispose();

            //put the trustfund as zero during zread 
            //to show the correct zread
            Details.TrustFund = 0;

            DialogResult result = DialogResult.OK;

            if (pvtWillPreviewReport)
            {
                TerminalReportWnd clsTerminalReportWnd = new TerminalReportWnd();
                clsTerminalReportWnd.SysConfigDetails = mclsSysConfigDetails;
                clsTerminalReportWnd.TerminalDetails = mclsTerminalDetails;
                clsTerminalReportWnd.Details = Details;
                clsTerminalReportWnd.CashierName = mCashierName;
                clsTerminalReportWnd.TerminalReportType = TerminalReportType.ZRead;
                clsTerminalReportWnd.ShowDialog(this);
                result = clsTerminalReportWnd.Result;
                clsTerminalReportWnd.Close();
                clsTerminalReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                PrintZRead(true, Details);
            }
        }
        //public delegate void PrintZReadDelegate(bool pvtWillOpenDrawer, Data.TerminalReportDetails Details);
        public void PrintZRead(bool pvtWillOpenDrawer, Data.TerminalReportDetails Details)
        {
            if (pvtWillOpenDrawer)
            {
                Cursor.Current = Cursors.WaitCursor;
                OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                Invoke(opendrawerDel);
            }

            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                DateTime dteEffectiveDate = Convert.ToDateTime(Details.DateLastInitializedToDisplay.ToString("MMM. dd, yyyy") + " " + Details.DateLastInitialized.ToString("hh:mm:ss tt"));
                PrintReportHeadersSection(false, dteEffectiveDate);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("ZRead Report : " + Details.ZReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintTerminalReportDetails(Details);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintZRead, "Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing ZRead report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        //public delegate void RePrintZReadDelegate(Data.TerminalReportDetails Details);
        public void RePrintZRead(Data.TerminalReportDetails Details)
        {
            Cursor.Current = Cursors.WaitCursor;
            OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
            Invoke(opendrawerDel);

            try
            {
                // override the Trustfund coz its a reprint
                // Nov 20, 2013 put this coz sometimes the TF is zero
                decimal oldTrustFund = mclsTerminalDetails.TrustFund;
                mclsTerminalDetails.TrustFund = Details.TrustFund;

                // override the cashiername to be printed in the receipt
                mclsSalesTransactionDetails.CashierName = Details.InitializedBy;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                DateTime dteEffectiveDate = Convert.ToDateTime(Details.DateLastInitializedToDisplay.ToString("MMM. dd, yyyy") + " " + Details.DateLastInitialized.ToString("hh:mm:ss tt"));
                PrintReportHeadersSection(false, dteEffectiveDate);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("ZRead Report : " + Details.ZReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintTerminalReportDetails(Details);

                PrintPageAndReportFooterSection(false, dteEffectiveDate);

                // revert override the cashiername to be printed in the receipt
                mclsSalesTransactionDetails.CashierName = mCashierName;
                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
                mclsTerminalDetails.TrustFund = oldTrustFund;

                InsertAuditLog(AccessTypes.PrintZRead, "Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing ZRead report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region ReprintZRead
        public void ReprintZRead()
        {
            DialogResult result = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTerminalReport);

            if (result == DialogResult.None)
            {
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.PrintZRead;
                login.Header = "Re-Print ZREAD Report";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                result = login.Result;
                login.Close();
                login.Dispose();
            }

            if (result == DialogResult.OK)
            {
                TerminalHistoryDateWnd clsTerminalHistoryDateWnd = new TerminalHistoryDateWnd();
                clsTerminalHistoryDateWnd.TerminalNo = mclsTerminalDetails.TerminalNo;
                clsTerminalHistoryDateWnd.ShowDialog(this);
                DialogResult clsTerminalHistoryDateWndresult = clsTerminalHistoryDateWnd.Result;
                DateTime dtDateLastInitialized = clsTerminalHistoryDateWnd.DateLastInitialized;
                clsTerminalHistoryDateWnd.Close();
                clsTerminalHistoryDateWnd.Dispose();

                Data.TerminalReportHistory clsTerminalReportHistory = new Data.TerminalReportHistory(mConnection, mTransaction);
                mConnection = clsTerminalReportHistory.Connection; mTransaction = clsTerminalReportHistory.Transaction;

                Data.TerminalReportDetails Details = clsTerminalReportHistory.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, dtDateLastInitialized);
                //Data.TerminalReportDetails NextDetails = clsTerminalReportHistory.NextDetails(mclsTerminalDetails.TerminalNo, dtDateLastInitialized);
                clsTerminalReportHistory.CommitAndDispose();

                decimal OldTrustFund = mclsTerminalDetails.TrustFund;
                mclsTerminalDetails.TrustFund = Details.TrustFund;

                AceSoft.Common.SYSTEMTIME st = new AceSoft.Common.SYSTEMTIME();
                // set the sytem date to NextDetails DateLastInitialized
                st = AceSoft.Common.ConvertToSystemTime(Details.DateLastInitialized.AddSeconds(-2).ToUniversalTime());
                mdtCurrentDateTime = DateTime.Now;
                tmr.Enabled = true;
                tmr.Start();
                AceSoft.Common.SetSystemTime(ref st);

                TerminalReportWnd clsTerminalReportWnd = new TerminalReportWnd();
                clsTerminalReportWnd.SysConfigDetails = mclsSysConfigDetails;
                clsTerminalReportWnd.TerminalDetails = mclsTerminalDetails;
                clsTerminalReportWnd.Details = Details;
                clsTerminalReportWnd.CashierName = mCashierName;
                clsTerminalReportWnd.TerminalReportType = TerminalReportType.ZRead;
                clsTerminalReportWnd.ShowDialog(this);
                result = clsTerminalReportWnd.Result;
                clsTerminalReportWnd.Close();
                clsTerminalReportWnd.Dispose();

                if (result == DialogResult.OK)
                {
                    //PrintZReadDelegate printzreadDel = new PrintZReadDelegate(PrintZRead);
                    //printzreadDel.BeginInvoke(Details, null, null);
                    RePrintZRead(Details);
                    InsertAuditLog(AccessTypes.ReprintZRead, "Re-Print ZRead report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " DateLastInitialized=" + dtDateLastInitialized.ToString("yyyy-MM-dd hh:mm") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                }

                // set the sytem date to NextDetails DateLastInitialized
                st = AceSoft.Common.ConvertToSystemTime(mdtCurrentDateTime.ToUniversalTime());
                AceSoft.Common.SetSystemTime(ref st);
                tmr.Stop();
                tmr.Enabled = false;

                mclsTerminalDetails.TrustFund = OldTrustFund;
            }
        }

        #endregion

        #region PrintXRead

        public void PrintXRead()
        {
            mclsTerminalDetails.TrustFund = 0;

            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            clsTerminalReport.UpdateTrustFund(mclsTerminalDetails.BranchID, mclsTerminalDetails.TerminalNo, 0, mclsSalesTransactionDetails.CashierName, Constants.TRUSTFUND_UPDATE_REASON_XREAD);

            Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
            clsTerminalReport.CommitAndDispose();

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                TerminalReportWnd clsTerminalReportWnd = new TerminalReportWnd();
                clsTerminalReportWnd.SysConfigDetails = mclsSysConfigDetails;
                clsTerminalReportWnd.TerminalDetails = mclsTerminalDetails;
                clsTerminalReportWnd.Details = Details;
                clsTerminalReportWnd.CashierName = mCashierName;
                clsTerminalReportWnd.TerminalReportType = TerminalReportType.XRead;
                clsTerminalReportWnd.ShowDialog(this);
                result = clsTerminalReportWnd.Result;
                clsTerminalReportWnd.Close();
                clsTerminalReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                //PrintXReadDelegate printxreadDel = new PrintXReadDelegate(PrintXRead);
                PrintXRead(Details);
            }
        }
        public delegate void PrintXReadDelegate(Data.TerminalReportDetails Details);
        public void PrintXRead(Data.TerminalReportDetails Details)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                DateTime dteEffectiveDate = Convert.ToDateTime(Details.DateLastInitializedToDisplay.ToString("MMM. dd, yyyy") + " " + Details.DateLastInitialized.ToString("hh:mm:ss tt"));
                PrintReportHeadersSection(false, dteEffectiveDate);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("XRead Report : " + Details.XReadCount.ToString(), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintTerminalReportDetails(Details);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                clsTerminalReport.UpdateXReadCount(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                clsTerminalReport.UpdateTrustFund(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, 0, mclsSalesTransactionDetails.CashierName, Constants.TRUSTFUND_UPDATE_REASON_XREAD);

                clsTerminalReport.CommitAndDispose();

                InsertAuditLog(AccessTypes.PrintXRead, "Print XREAD report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing XREAD report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintTerminalReport
        public void PrintTerminalReport()
        {
            DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintTerminalReport);

            if (loginresult == DialogResult.None)
            {
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.PrintTerminalReport;
                login.Header = "Print Terminal Report";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();
            }

            if (loginresult == DialogResult.OK)
            {
                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

                Data.TerminalReportDetails Details = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
                clsTerminalReport.CommitAndDispose();
                
                // always set to zero for BIR purposes
                Details.TrustFund = 0;

                DialogResult result = DialogResult.OK;

                if (mclsTerminalDetails.PreviewTerminalReport)
                {
                    TerminalReportWnd clsTerminalReportWnd = new TerminalReportWnd();
                    clsTerminalReportWnd.SysConfigDetails = mclsSysConfigDetails;
                    clsTerminalReportWnd.TerminalDetails = mclsTerminalDetails;
                    clsTerminalReportWnd.Details = Details;
                    clsTerminalReportWnd.CashierName = mCashierName;
                    clsTerminalReportWnd.TerminalReportType = TerminalReportType.TerminalReport;
                    clsTerminalReportWnd.ShowDialog(this);
                    result = clsTerminalReportWnd.Result;
                    clsTerminalReportWnd.Close();
                    clsTerminalReportWnd.Dispose();
                }
                if (result == DialogResult.OK)
                {
                    //PrintTerminalReportDelegate terminalreportDel = new PrintTerminalReportDelegate(PrintTerminalReport);
                    //terminalreportDel.BeginInvoke(Details, null, null);
                    PrintTerminalReport(Details);
                }
            }
        }
        public delegate void PrintTerminalReportDelegate(Data.TerminalReportDetails Details);
        public void PrintTerminalReport(Data.TerminalReportDetails Details)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Terminal Report", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintTerminalReportDetails(Details);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintTerminalReport, "Print Terminal report: TerminalNo=" + Details.TerminalNo + " CashInDrawer=" + Details.CashInDrawer.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing terminal report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        public void PrintTerminalReportDetails(Data.TerminalReportDetails Details)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Data.SysConfig clsSysConfig = new Data.SysConfig(mConnection, mTransaction);
                mConnection = clsSysConfig.Connection; mTransaction = clsSysConfig.Transaction;

                msbToPrint.Append("Beginning OR No.".PadRight(21) + ":" + Details.BeginningORNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Ending    OR No.".PadRight(21) + ":" + Details.EndingORNo.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine + Environment.NewLine);
                msbToPrint.Append("Gross Sales".PadRight(21) + ":" + ((Details.GrossSales + Details.TotalCharge) * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("(-) Service Charge".PadRight(21) + ":" + (Details.TotalCharge * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Amount".PadRight(21) + ":" + (Details.GrossSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append(("(-) " + mclsTerminalDetails.VAT.ToString("##") + "% VAT Exempt").PadRight(21) + ":" + (Details.VATExempt * (mclsTerminalDetails.VAT / 100) * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("(-) Subtotal Discount".PadRight(21) + ":" + (Details.SubTotalDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Net Sales".PadRight(21) + ":" + (Details.NetSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                if (mclsTerminalDetails.WillPrintGrandTotal == true)
                {
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append("OLD GRAND TOTAL".PadRight(21) + ":" + (Details.OldGrandTotal).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                    msbToPrint.Append("This Total Amount".PadRight(21) + ":" + (Details.GrossSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                    msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                    msbToPrint.Append("NEW GRAND TOTAL".PadRight(21) + ":" + (Details.OldGrandTotal + (Details.GrossSales * ((100 - Details.TrustFund) / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                    mclsSysConfigDetails.WillDeductTFInTerminalReport = clsSysConfig.get_WillDeductTFInTerminalReport();
                }

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("VAT Exempt".PadRight(21) + ":" + (Details.VATExempt * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VAT Zero Rated".PadRight(21) + ":" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("NonVATable Amount".PadRight(21) + ":" + (Details.NonVATableAmount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VATable Amount".PadRight(21) + ":" + (Details.VATableAmount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VAT".PadRight(21) + ":" + (Details.VAT * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Local Tax".PadRight(21) + ":" + (Details.LocalTax * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash Sales".PadRight(21) + ":" + (Details.CashSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque Sales".PadRight(21) + ":" + (Details.ChequeSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card Sales".PadRight(21) + ":" + (Details.CreditCardSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit (Charge)".PadRight(21) + ":" + (Details.CreditSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Payment".PadRight(21) + ":" + (Details.CreditPayment * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("      Cash".PadRight(21) + ":" + (Details.CreditPaymentCash * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cheque".PadRight(21) + ":" + (Details.CreditPaymentCheque * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit Card".PadRight(21) + ":" + (Details.CreditPaymentCreditCard * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Debit".PadRight(21) + ":" + (Details.CreditPaymentDebit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("Debit  Sales".PadRight(21) + ":" + (Details.DebitPayment * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("RewardPoints Redeemd".PadRight(21) + ":" + Details.RewardPointsPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Cash Equivalent".PadRight(21) + ":" + (Details.RewardConvertedPayment * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Employee Acct.".PadRight(21) + ":" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Void Sales".PadRight(21) + ":" + (Details.VoidSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Refund Sales".PadRight(21) + ":" + (Details.RefundSales * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cash".PadRight(21) + ":" + (Details.RefundCash * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cheque".PadRight(21) + ":" + (Details.RefundCheque * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit Card".PadRight(21) + ":" + (Details.RefundCreditCard * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit".PadRight(21) + ":" + (Details.RefundCredit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Debit".PadRight(21) + ":" + (Details.RefundDebit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Items Discount".PadRight(21) + ":" + (Details.ItemsDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Subtotal Discount".PadRight(21) + ":" + (Details.SubTotalDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Senior Citizen".PadRight(21) + ":" + (Details.SNRDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     PWD".PadRight(21) + ":" + (Details.PWDDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Others".PadRight(21) + ":" + (Details.OtherDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Discounts".PadRight(21) + ":" + (Details.TotalDiscount * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.BranchID, Details.TerminalNo, Details.BeginningTransactionNo, Details.EndingTransactionNo);
                clsSysConfig.CommitAndDispose();

                if (dt.Rows.Count > 0)
                {
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    foreach (System.Data.DataRow dr in dt.Rows)
                    { msbToPrint.Append(dr["DiscountCode"].ToString().PadRight(21) + ":" + (Convert.ToDecimal(dr["Discount"].ToString()) * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine); }
                }

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Beginning Balance".PadRight(21) + ":" + (Details.BeginningBalance).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cash-In-Drawer".PadRight(21) + ":" + (Details.BeginningBalance + ((Details.CashInDrawer - Details.BeginningBalance) * ((100 - Details.TrustFund) / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Paid Out".PadRight(21) + ":" + (Details.TotalPaidOut * ((100 - Details.TrustFund) / 100)).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashDisburse * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeDisburse * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardDisburse * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashWithHold * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeWithHold * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardWithHold * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashDeposit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeDeposit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardDeposit * ((100 - Details.TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash Transactions".PadRight(21) + ":" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque Transactions".PadRight(21) + ":" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card Trans.".PadRight(21) + ":" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Transactions".PadRight(21) + ":" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Debit Payment Trans.".PadRight(21) + ":" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Refund Transactions".PadRight(21) + ":" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Void Transactions".PadRight(21) + ":" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Combination Tran".PadRight(21) + ":" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Payment Trans".PadRight(21) + ":" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Reward Points  Trans".PadRight(21) + ":" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                //				msbToPrint.Append("Employees Acct Trans".PadRight(21) + ":" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22)  + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Transactions".PadRight(21) + ":" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing terminal report details. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintHourlyReport
        public void PrintHourlyReport()
        {
            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            Data.TerminalReportDetails clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

            System.Data.DataTable dtHourlyReport = clsTerminalReport.HourlyReport(clsTerminalReportDetails.BeginningTransactionNo, clsTerminalReportDetails.EndingTransactionNo, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
            clsTerminalReport.CommitAndDispose();

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                HourlyReportWnd clsHourlyReportWnd = new HourlyReportWnd();
                clsHourlyReportWnd.TerminalDetails = mclsTerminalDetails;
                clsHourlyReportWnd.CashierName = mCashierName;
                clsHourlyReportWnd.dtHourlyReport = dtHourlyReport;
                clsHourlyReportWnd.ShowDialog(this);
                result = clsHourlyReportWnd.Result;
                clsHourlyReportWnd.Close();
                clsHourlyReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                //PrintHourlyReportDelegate hourlyreportDel = new PrintHourlyReportDelegate(PrintHourlyReport);
                PrintHourlyReport(dtHourlyReport);
            }
        }
        public delegate void PrintHourlyReportDelegate(System.Data.DataTable dtHourlyReport);
        public void PrintHourlyReport(System.Data.DataTable dtHourlyReport)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                decimal TotalTranCount = 0;
                decimal TotalAmount = 0;
                decimal TotalDiscount = 0;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("HOURLY REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("TIME  TranCnt           Amount  Discount" + Environment.NewLine);

                foreach (System.Data.DataRow dr in dtHourlyReport.Rows)
                {
                    string Time = dr["Time"].ToString();
                    msbToPrint.Append(Time.PadRight(6));

                    string TranCount = Convert.ToDecimal(dr["TranCount"].ToString()).ToString("##0");
                    msbToPrint.Append(TranCount.PadLeft(7));

                    string Amount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");
                    msbToPrint.Append(Amount.PadLeft(17));

                    string Discount = Convert.ToDecimal(dr["Discount"].ToString()).ToString("#,##0.#0");
                    msbToPrint.Append(Discount.PadLeft(10));
                    msbToPrint.Append(Environment.NewLine);

                    TotalTranCount += Convert.ToDecimal(dr["TranCount"]);
                    TotalAmount += Convert.ToDecimal(dr["Amount"]);
                    TotalDiscount += Convert.ToDecimal(dr["Discount"]);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Total:" + TotalTranCount.ToString("##0").PadLeft(7));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append(TotalDiscount.ToString("#,##0.#0").PadLeft(10) + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintHourlyReport, "Print hourly report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing hourly report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintGroupReport
        public void PrintGroupReport()
        {
            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            Data.TerminalReportDetails clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
            System.Data.DataTable dtGroupReport = clsTerminalReport.GroupReport(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);
            clsTerminalReport.CommitAndDispose();

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                GroupReportWnd clsGroupReportWnd = new GroupReportWnd();
                clsGroupReportWnd.TerminalDetails = mclsTerminalDetails;
                clsGroupReportWnd.CashierName = mCashierName;
                clsGroupReportWnd.dtGroupReport = dtGroupReport;
                clsGroupReportWnd.TerminalReportDetail = clsTerminalReportDetails;
                clsGroupReportWnd.ShowDialog(this);
                result = clsGroupReportWnd.Result;
                clsGroupReportWnd.Close();
                clsGroupReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                //PrintGroupReportDelegate groupreportDel = new PrintGroupReportDelegate(PrintGroupReport);
                PrintGroupReport(dtGroupReport, clsTerminalReportDetails);
            }
        }

        public delegate void PrintGroupReportDelegate(System.Data.DataTable dtGroupReport, Data.TerminalReportDetails clsTerminalReportDetails);
        public void PrintGroupReport(System.Data.DataTable dtGroupReport, Data.TerminalReportDetails clsTerminalReportDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                decimal TotalTranCount = 0;
                decimal TotalAmount = 0;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("GROUP REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("GROUP       QTY           Amount Prcntg." + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                foreach (System.Data.DataRow dr in dtGroupReport.Rows)
                {
                    string ProductGroup = dr["ProductGroup"].ToString();
                    string TranCount = Convert.ToDecimal(dr["TranCount"].ToString()).ToString("#,##0");
                    string Amount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");
                    string Percentage = dr["Percentage"].ToString();

                    msbToPrint.Append(ProductGroup + Environment.NewLine);
                    msbToPrint.Append(TranCount.PadLeft(15));
                    msbToPrint.Append(Amount.PadLeft(17));
                    msbToPrint.Append(Percentage.PadLeft(8));
                    msbToPrint.Append(Environment.NewLine);
                    //					}

                    TotalTranCount += Convert.ToDecimal(dr["TranCount"]);
                    TotalAmount += Convert.ToDecimal(dr["Amount"]);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(TotalTranCount.ToString("#,##0").PadLeft(15));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append("100%".PadLeft(8));
                msbToPrint.Append(Environment.NewLine);
                //msbToPrint.Append("Plus Discount :".PadRight(15));
                //msbToPrint.Append(clsTerminalReportDetails.SubTotalDiscount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15));
                //msbToPrint.Append(Environment.NewLine);
                //msbToPrint.Append("Plus Charges  :".PadRight(15));
                //msbToPrint.Append(clsTerminalReportDetails.TotalCharge.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15));
                //msbToPrint.Append(Environment.NewLine);
                //if (!mclsTerminalDetails.IsVATInclusive)
                //{
                //    msbToPrint.Append("Plus 12% VAT  :".PadRight(15));
                //    msbToPrint.Append(clsTerminalReportDetails.VAT.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15));
                //    msbToPrint.Append(Environment.NewLine);
                //}

                //decimal GrandTotal = clsTerminalReportDetails.NetSales + clsTerminalReportDetails.TotalDiscount + clsTerminalReportDetails.TotalCharge + (clsTerminalReportDetails.VATExempt * mclsTerminalDetails.VAT / 100);
                msbToPrint.Append("Grand Total   :".PadRight(15));
                msbToPrint.Append(clsTerminalReportDetails.GrossSales.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15));
                msbToPrint.Append(Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintGroupReport, "Print group report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing group report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintPLUReport
        public void PrintPLUReport()
        {
            Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
            mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

            Data.CashierReportDetails clsCashierReportDetails = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
            clsCashierReport.GeneratePLUReport(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, false, mCashierName);

            Data.PLUReport clsPLUReport = new Data.PLUReport(mConnection, mTransaction);
            mConnection = clsPLUReport.Connection; mTransaction = clsPLUReport.Transaction;

            System.Data.DataTable dtpluReport = clsPLUReport.ListAsDataTable(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

            clsCashierReport.CommitAndDispose();

            DateTime StartDate = clsCashierReportDetails.LastLoginDate;
            DateTime EndDate = DateTime.Now;

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                PLUReportWnd clsPLUReportWnd = new PLUReportWnd();
                clsPLUReportWnd.TerminalDetails = mclsTerminalDetails;
                clsPLUReportWnd.CashierName = mCashierName;
                clsPLUReportWnd.dtPLUReport = dtpluReport;
                clsPLUReportWnd.CashierReportDetail = clsCashierReportDetails;
                clsPLUReportWnd.StartDate = StartDate;
                clsPLUReportWnd.EndDate = EndDate;
                clsPLUReportWnd.ShowDialog(this);
                result = clsPLUReportWnd.Result;
                clsPLUReportWnd.Close();
                clsPLUReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                //PrintPLUReportDelegate plureportDel = new PrintPLUReportDelegate(PrintPLUReport);
                PrintPLUReport(dtpluReport, clsCashierReportDetails, StartDate, EndDate);
            }
        }

        public delegate void PrintPLUReportDelegate(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, DateTime StartDate, DateTime EndDate);
        public void PrintPLUReport(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, DateTime StartDate, DateTime EndDate)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                decimal TotalQuantity = 0;
                decimal TotalAmount = 0;
                //string stProductGroup = "";
                //string stProductGroupOld = "";

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PLU REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Start Date: " + StartDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString("End Date  : " + EndDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine + Environment.NewLine);
                msbToPrint.Append("Item           Quantity           Amount" + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                foreach (System.Data.DataRow dr in dtPLUReport.Rows)
                {
                    //stProductGroup = dr["ProductGroup"].ToString();
                    //if (stProductGroup != stProductGroupOld)
                    //{
                    //    stProductGroupOld = stProductGroup;

                    //    msbToPrint.Append(stProductGroupOld.PadRight(40));
                    //    msbToPrint.Append(Environment.NewLine);
                    //}

                    string stProductCode = dr["ProductCode"].ToString();
                    string stQuantity = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");
                    string stAmount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

                    if (stProductCode.Length <= 11)
                    {
                        msbToPrint.Append(stProductCode.PadRight(11));
                        msbToPrint.Append(stQuantity.PadLeft(12));
                        msbToPrint.Append(stAmount.PadLeft(17));
                        msbToPrint.Append(Environment.NewLine);
                    }
                    else
                    {
                        msbToPrint.Append(stProductCode + Environment.NewLine);
                        msbToPrint.Append(stQuantity.PadLeft(23));
                        msbToPrint.Append(stAmount.PadLeft(17));
                        msbToPrint.Append(Environment.NewLine);
                    }

                    TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
                    TotalAmount += Convert.ToDecimal(dr["Amount"]);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Total:".PadRight(6));
                msbToPrint.Append(TotalQuantity.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append(Environment.NewLine);
                msbToPrint.Append("Grand Total   :".PadRight(16));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(24));
                msbToPrint.Append(Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintPLUReport, "Print PLU report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing PLU report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintPLUReportPerGroup
        public void PrintPLUReportPerGroup()
        {
            Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
            mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

            Data.CashierReportDetails clsCashierReportDetails = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
            clsCashierReport.GeneratePLUReport(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, false, mCashierName);

            Data.PLUReport clsPLUReport = new Data.PLUReport(mConnection, mTransaction);
            mConnection = clsPLUReport.Connection; mTransaction = clsPLUReport.Transaction;

            System.Data.DataTable dtpluReport = clsPLUReport.ListAsDataTable(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, true, "ProductGroup");

            clsCashierReport.CommitAndDispose();

            DateTime StartDate = clsCashierReportDetails.LastLoginDate;
            DateTime EndDate = DateTime.Now;

            DialogResult result = DialogResult.OK;

            if (result == DialogResult.OK)
            {
                //PrintPLUReportPerGroupDelegate plureportDel = new PrintPLUReportPerGroupDelegate(PrintPLUReportPerGroup);
                PrintPLUReportPerGroup(dtpluReport, clsCashierReportDetails, StartDate, EndDate);
            }
        }

        public delegate void PrintPLUReportPerGroupDelegate(System.Data.DataTable dtPLUReportPerGroup, Data.CashierReportDetails clsCashierReportDetails, DateTime StartDate, DateTime EndDate);
        public void PrintPLUReportPerGroup(System.Data.DataTable dtPLUReportPerGroup, Data.CashierReportDetails clsCashierReportDetails, DateTime StartDate, DateTime EndDate)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                decimal TotalQuantity = 0;
                //decimal TotalAmount = 0;
                string stProductGroup = "";
                string stProductGroupOld = "";

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PLU INVENTORY", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Start Date: " + StartDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString("End Date  : " + EndDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine + Environment.NewLine);
                msbToPrint.Append("Item                            Quantity" + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                foreach (System.Data.DataRow dr in dtPLUReportPerGroup.Rows)
                {
                    stProductGroup = dr["ProductGroup"].ToString();
                    if (stProductGroup != stProductGroupOld)
                    {
                        stProductGroupOld = stProductGroup;

                        msbToPrint.Append(stProductGroupOld.PadRight(40));
                        msbToPrint.Append(Environment.NewLine);
                    }

                    string stProductCode = "   " + dr["ProductCode"].ToString();
                    string stQuantity = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");

                    if (stProductCode.Length <= 28)
                    {
                        msbToPrint.Append(stProductCode.PadRight(28));
                        msbToPrint.Append(stQuantity.PadLeft(12));
                        msbToPrint.Append(Environment.NewLine);
                    }
                    else
                    {
                        msbToPrint.Append(stProductCode + Environment.NewLine);
                        msbToPrint.Append(stQuantity.PadLeft(40));
                        msbToPrint.Append(Environment.NewLine);
                    }

                    TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Total:".PadRight(28));
                msbToPrint.Append(TotalQuantity.ToString("#,##0.#0").PadLeft(12));
                msbToPrint.Append(Environment.NewLine);
                //msbToPrint.Append("Less Discount :".PadRight(16));
                //msbToPrint.Append(clsCashierReportDetails.SubTotalDiscount.ToString("#,##0.#0").PadLeft(24));
                //msbToPrint.Append(Environment.NewLine);
                //msbToPrint.Append("Plus Charges  :".PadRight(16));
                //msbToPrint.Append(clsCashierReportDetails.TotalCharge.ToString("#,##0.#0").PadLeft(24));
                //msbToPrint.Append(Environment.NewLine);
                //if (!mclsTerminalDetails.IsVATInclusive)
                //{
                //    msbToPrint.Append("Plus 12% VAT  :".PadRight(16));
                //    msbToPrint.Append(clsCashierReportDetails.VAT.ToString("#,##0.#0").PadLeft(24));
                //    msbToPrint.Append(Environment.NewLine);
                //}

                //decimal GrandTotal = clsCashierReportDetails.DailySales + clsCashierReportDetails.VAT + clsCashierReportDetails.TotalCharge;
                //msbToPrint.Append("Grand Total   :".PadRight(16));
                //msbToPrint.Append(GrandTotal.ToString("#,##0.#0").PadLeft(24));
                //msbToPrint.Append(Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintPLUReport, "Print PLU report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing PLU report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintEJournalReport
        public void PrintEJournalReport()
        {
            Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
            mConnection = clsTerminalReport.Connection; mTransaction = clsTerminalReport.Transaction;

            Data.SalesTransactionDetails[] salesDetails = clsTerminalReport.EJournalReport(Constants.TerminalBranchID, mCashierName, mclsTerminalDetails.TerminalNo);
            clsTerminalReport.CommitAndDispose();

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                EJournalReportWnd clsEJournalReportWnd = new EJournalReportWnd();
                clsEJournalReportWnd.TerminalDetails = mclsTerminalDetails;
                clsEJournalReportWnd.CashierName = mCashierName;
                clsEJournalReportWnd.SalesDetails = salesDetails;
                clsEJournalReportWnd.ShowDialog(this);
                result = clsEJournalReportWnd.Result;
                clsEJournalReportWnd.Close();
                clsEJournalReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                //PrintEJournalReportDelegate ejournalreportDel = new PrintEJournalReportDelegate(PrintEJournalReport);
                PrintEJournalReport(salesDetails);
            }
        }

        public delegate void PrintEJournalReportDelegate(Data.SalesTransactionDetails[] salesDetails);
        public void PrintEJournalReport(Data.SalesTransactionDetails[] salesDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                mboIsItemHeaderPrinted = true;

                PrintReportHeaderSection(false, DateTime.MinValue);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("ELECTRONIC JOURNAL REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                Data.ChequePaymentDetails[] arrChequePaymentDetails;
                Data.CreditCardPaymentDetails[] arrCreditCardPaymentDetails;
                Data.CreditPaymentDetails[] arrCreditPaymentDetails;
                Data.DebitPaymentDetails[] arrDebitPaymentDetails;
                Data.Payment clspayment = new Data.Payment(mConnection, mTransaction);
                mConnection = clspayment.Connection; mTransaction = clspayment.Transaction;

                foreach (Data.SalesTransactionDetails trandetails in salesDetails)
                {
                    // set the details
                    mclsSalesTransactionDetails = trandetails;
                    /*** 
                     * Print the Headers
                     * OFFICIAL RECEIPT #:
                     * Transaction Date 
                     * Item Header
                     ***/

                    PrintReportPageHeaderSection(true);

                    /*** 
                     * Print the Items
                     ***/
                    int itemno = 0;
                    decimal TotalItemSold = 0;
                    decimal iTotalQuantitySold = 0;
                    foreach (Data.SalesTransactionItemDetails item in trandetails.TransactionItems)
                    {
                        itemno++;
                        TotalItemSold++;
                        iTotalQuantitySold += item.Quantity;
                        string stProductCode = item.Description;
                        if (item.MatrixDescription != string.Empty && item.MatrixDescription != null) stProductCode += "-" + item.MatrixDescription;
                        PrintItem(itemno.ToString(), stProductCode, item.ProductUnitCode, item.Quantity, item.Price, item.Discount, item.PromoApplied, item.Amount, item.VAT, item.EVAT);
                    }

                    /*** 
                     * Print the Footer
                     ***/
                    /*********************************************************************************/
                    PrintPageFooterASection();

                    if (trandetails.TransactionStatus == TransactionStatus.Refund)
                    {
                        if (trandetails.CashPayment != 0)
                            msbToPrint.Append("Cash Refund".PadRight(15) + ":" + trandetails.CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                        if (trandetails.ChequePayment != 0)
                        {
                            msbToPrint.Append("Cheque Refund".PadRight(15) + ":" + trandetails.ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                            arrChequePaymentDetails = new Data.ChequePayments(clspayment.Connection, clspayment.Transaction).Details(trandetails.BranchID, trandetails.TerminalNo, trandetails.TransactionID);

                            if (arrChequePaymentDetails != null)
                            {
                                foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
                                {
                                    //print cheque details
                                    msbToPrint.Append("Cheque No.".PadRight(15) + ":" + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Validity Date".PadRight(15) + ":" + chequepaymentdet.ValidityDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }
                        }

                        if (trandetails.CreditCardPayment != 0)
                        {
                            msbToPrint.Append("Credit Card Refund".PadRight(15) + ":" + trandetails.CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                            arrCreditCardPaymentDetails = new Data.CreditCardPayments(clspayment.Connection, clspayment.Transaction).Details(trandetails.BranchID, trandetails.TerminalNo, trandetails.TransactionID);
                            if (arrCreditCardPaymentDetails != null)
                            {
                                foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
                                {
                                    //print credit card details
                                    msbToPrint.Append("Member Name".PadRight(15) + ":" + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Validity Date".PadRight(15) + ":" + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }
                        }
                    }
                    else if (trandetails.TransactionStatus == TransactionStatus.Closed || trandetails.TransactionStatus == TransactionStatus.Reprinted || trandetails.TransactionStatus == TransactionStatus.Open || trandetails.TransactionStatus == TransactionStatus.CreditPayment)
                    {
                        msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                        if (trandetails.CashPayment != 0)
                            msbToPrint.Append("Cash Payment".PadRight(15) + ":" + trandetails.CashPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                        if (trandetails.ChequePayment != 0)
                        {
                            msbToPrint.Append("Cheque Payment".PadRight(15) + ":" + trandetails.ChequePayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                            arrChequePaymentDetails = new Data.ChequePayments(clspayment.Connection, clspayment.Transaction).Details(trandetails.BranchID, trandetails.TerminalNo, trandetails.TransactionID);
                            if (arrChequePaymentDetails != null)
                            {
                                foreach (Data.ChequePaymentDetails chequepaymentdet in arrChequePaymentDetails)
                                {
                                    //print checque details
                                    msbToPrint.Append("Cheque No.".PadRight(15) + ":" + chequepaymentdet.ChequeNo.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + chequepaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Validity Date".PadRight(15) + ":" + chequepaymentdet.ValidityDate.ToString("yyyy-MM-dd").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }
                        }

                        if (trandetails.CreditCardPayment != 0)
                        {
                            msbToPrint.Append("Credit Card Payment".PadRight(15) + ":" + trandetails.CreditCardPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);

                            arrCreditCardPaymentDetails = new Data.CreditCardPayments(clspayment.Connection, clspayment.Transaction).Details(trandetails.BranchID, trandetails.TerminalNo, trandetails.TransactionID);
                            if (arrCreditCardPaymentDetails != null)
                            {
                                foreach (Data.CreditCardPaymentDetails cardpaymentdet in arrCreditCardPaymentDetails)
                                {
                                    //print credit card details
                                    msbToPrint.Append("Member Name ".PadRight(15) + ":" + cardpaymentdet.CardHolder.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + cardpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append("Validity Date".PadRight(15) + ":" + cardpaymentdet.ValidityDates.Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }
                        }
                        if (trandetails.CreditPayment != 0)
                        {
                            msbToPrint.Append("Credit Payment".PadRight(15) + ":" + trandetails.CreditPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15) + Environment.NewLine);

                            arrCreditPaymentDetails = new Data.CreditPayments(clspayment.Connection, clspayment.Transaction).Details(trandetails.BranchID, trandetails.TerminalNo, trandetails.TransactionID);
                            if (arrCreditPaymentDetails != null)
                            {
                                foreach (Data.CreditPaymentDetails creditpaymentdet in arrCreditPaymentDetails)
                                {
                                    //print credit details
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + creditpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }

                        }
                        if (trandetails.DebitPayment != 0)
                        {
                            msbToPrint.Append("Debit  Payment".PadRight(15) + ":" + trandetails.DebitPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 15) + Environment.NewLine);

                            arrDebitPaymentDetails = clspayment.arrDebitPaymentDetails(trandetails.TransactionID);
                            if (arrDebitPaymentDetails != null)
                            {
                                foreach (Data.DebitPaymentDetails debitpaymentdet in arrDebitPaymentDetails)
                                {
                                    //print debit details
                                    msbToPrint.Append("Amount".PadRight(15) + ":" + debitpaymentdet.Amount.ToString("#,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                                    msbToPrint.Append(Environment.NewLine);
                                }
                            }
                        }
                        if (trandetails.RewardConvertedPayment != 0)
                        {
                            msbToPrint.Append("Reward Paymnt".PadRight(15) + ":" + trandetails.RewardConvertedPayment.ToString("###,##0.#0").Trim().PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                        }
                    }

                    if (trandetails.TransactionStatus == TransactionStatus.Suspended)
                    {
                        msbToPrint.Append(CenterString("This transaction is suspended", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    else if (trandetails.TransactionStatus == TransactionStatus.Void)
                    {
                        msbToPrint.Append(CenterString("This transaction is VOID", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    else if (trandetails.TransactionStatus == TransactionStatus.Reprinted)
                    {
                        msbToPrint.Append(CenterString("This transaction is reprinted as of ", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                        msbToPrint.Append(CenterString(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    else if (trandetails.TransactionStatus == TransactionStatus.Refund)
                    {
                        msbToPrint.Append(CenterString("This transaction is a REFUND", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }
                    else if (trandetails.TransactionStatus == TransactionStatus.CreditPayment)
                    {
                        if (mclsSysConfigDetails.WillPrintCreditPaymentHeader)
                            msbToPrint.Append(CenterString("------CREDIT PAYMENT--------", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    }

                    PrintPageFooterBSection();
                    /*********************************************************************************/
                    msbToPrint.Append(Environment.NewLine + "=".PadRight(mclsTerminalDetails.MaxReceiptWidth, '=') + Environment.NewLine);
                }

                clspayment.CommitAndDispose();

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintElectronicJournal, "Print Electronic Journal report: TerminalNo=" + mclsTerminalDetails.TerminalNo + " Cashier".PadRight(15) + ":" + mCashierName + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing Electronic Journal report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintPLUReportPerOrderSlipPrinter
        public void PrintPLUReportPerOrderSlipPrinter()
        {
            Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
            mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

            Data.CashierReportDetails clsCashierReportDetails = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
            clsCashierReport.GeneratePLUReport(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo, false, mCashierName);

            Data.PLUReport clsPLUReport = new Data.PLUReport(mConnection, mTransaction);
            System.Data.DataTable dtpluReport = clsPLUReport.ListAsDataTable(mclsTerminalDetails.BranchDetails.BranchID, mclsTerminalDetails.TerminalNo);

            clsCashierReport.CommitAndDispose();

            DateTime StartDate = clsCashierReportDetails.LastLoginDate;
            DateTime EndDate = DateTime.Now;

            DialogResult result = DialogResult.OK;

            if (mclsTerminalDetails.PreviewTerminalReport)
            {
                PLUReportWnd clsPLUReportWnd = new PLUReportWnd();
                clsPLUReportWnd.TerminalDetails = mclsTerminalDetails;
                clsPLUReportWnd.CashierName = mCashierName;
                clsPLUReportWnd.dtPLUReport = dtpluReport;
                clsPLUReportWnd.CashierReportDetail = clsCashierReportDetails;
                clsPLUReportWnd.StartDate = StartDate;
                clsPLUReportWnd.EndDate = EndDate;
                clsPLUReportWnd.ShowDialog(this);
                result = clsPLUReportWnd.Result;
                clsPLUReportWnd.Close();
                clsPLUReportWnd.Dispose();
            }

            if (result == DialogResult.OK)
            {
                // put variables in which printer to print
                int RetailPlusOSPrinter1Ctr = 0; int RetailPlusOSPrinter2Ctr = 0; int RetailPlusOSPrinter3Ctr = 0; int RetailPlusOSPrinter4Ctr = 0; int RetailPlusOSPrinter5Ctr = 0;

                foreach (System.Data.DataRow dr in dtpluReport.Rows)
                {
                    AceSoft.RetailPlus.OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());
                    switch (locOrderSlipPrinter)
                    {
                        case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1: { RetailPlusOSPrinter1Ctr++; break; }
                        case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2: { RetailPlusOSPrinter2Ctr++; break; }
                        case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3: { RetailPlusOSPrinter3Ctr++; break; }
                        case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4: { RetailPlusOSPrinter4Ctr++; break; }
                        case AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5: { RetailPlusOSPrinter5Ctr++; break; }
                    }
                }

                if (RetailPlusOSPrinter1Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter1);
                if (RetailPlusOSPrinter2Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter2);
                if (RetailPlusOSPrinter3Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter3);
                if (RetailPlusOSPrinter4Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter4);
                if (RetailPlusOSPrinter5Ctr > 0) PrintPLUReportPerOrderSlipPrinter(dtpluReport, clsCashierReportDetails, StartDate, EndDate, AceSoft.RetailPlus.OrderSlipPrinter.RetailPlusOSPrinter5);
            }
        }

        public void PrintPLUReportPerOrderSlipPrinter(System.Data.DataTable dtPLUReport, Data.CashierReportDetails clsCashierReportDetails, DateTime StartDate, DateTime EndDate, OrderSlipPrinter blockOrderSlipPrinter)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                decimal TotalQuantity = 0;
                decimal TotalAmount = 0;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PLU REPORT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString(blockOrderSlipPrinter.ToString("G"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Start Date: " + StartDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append(CenterString("End Date  : " + EndDate.ToString("MM/dd/yyyy hh:mm:ss tt"), mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine + Environment.NewLine);
                msbToPrint.Append("Item           Quantity           Amount" + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                foreach (System.Data.DataRow dr in dtPLUReport.Rows)
                {
                    OrderSlipPrinter enumOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    if (blockOrderSlipPrinter == enumOrderSlipPrinter)
                    {
                        string stProductCode = dr["ProductCode"].ToString();
                        string stQuantity = Convert.ToDecimal(dr["Quantity"].ToString()).ToString("#,##0.#0");
                        string stAmount = Convert.ToDecimal(dr["Amount"].ToString()).ToString("#,##0.#0");

                        if (stProductCode.Length <= 11)
                        {
                            msbToPrint.Append(stProductCode.PadRight(11));
                            msbToPrint.Append(stQuantity.PadLeft(12));
                            msbToPrint.Append(stAmount.PadLeft(17));
                            msbToPrint.Append(Environment.NewLine);
                        }
                        else
                        {
                            msbToPrint.Append(stProductCode + Environment.NewLine);
                            msbToPrint.Append(stQuantity.PadLeft(23));
                            msbToPrint.Append(stAmount.PadLeft(17));
                            msbToPrint.Append(Environment.NewLine);
                        }

                        TotalQuantity += Convert.ToDecimal(dr["Quantity"]);
                        TotalAmount += Convert.ToDecimal(dr["Amount"]);
                    }
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Total:".PadRight(6));
                msbToPrint.Append(TotalQuantity.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(17));
                msbToPrint.Append(Environment.NewLine);
                msbToPrint.Append("Grand Total   :".PadRight(16));
                msbToPrint.Append(TotalAmount.ToString("#,##0.#0").PadLeft(24));
                msbToPrint.Append(Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintPLUReport, "Print PLU report per OrderSlipprinter: " + blockOrderSlipPrinter.ToString("G") + " TerminalNo=" + mclsTerminalDetails.TerminalNo + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing PLU report. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintCashiersReport
        public void PrintCashiersReport()
        {
            DialogResult loginresult = GetWriteAccess(mclsSalesTransactionDetails.CashierID, AccessTypes.PrintCashierReport);

            if (loginresult == DialogResult.None)
            {
                LogInWnd login = new LogInWnd();

                login.AccessType = AccessTypes.PrintCashierReport;
                login.Header = "Print Cashier Report";
                login.TerminalDetails = mclsTerminalDetails;
                login.ShowDialog(this);
                loginresult = login.Result;
                login.Close();
                login.Dispose();
            }

            if (loginresult == DialogResult.OK)
            {
                DateTime dte = DateTime.Now;

                Data.CashierReports clsCashierReport = new Data.CashierReports(mConnection, mTransaction);
                mConnection = clsCashierReport.Connection; mTransaction = clsCashierReport.Transaction;

                Data.CashierReportDetails Details = clsCashierReport.Details(mclsSalesTransactionDetails.CashierID, Constants.TerminalBranchID, mclsTerminalDetails.TerminalNo);
                clsCashierReport.CommitAndDispose();

                DialogResult result = DialogResult.OK;

                if (mclsTerminalDetails.PreviewTerminalReport)
                {
                    CashierReportWnd clsCashierReportWnd = new CashierReportWnd();
                    clsCashierReportWnd.TerminalDetails = mclsTerminalDetails;
                    clsCashierReportWnd.Details = Details;
                    clsCashierReportWnd.CashierName = mCashierName;
                    clsCashierReportWnd.ShowDialog(this);
                    result = clsCashierReportWnd.Result;
                    clsCashierReportWnd.Close();
                    clsCashierReportWnd.Dispose();
                }

                if (result == DialogResult.OK)
                {
                    //PrintCashiersReportDelegate cashierreportDel = new PrintCashiersReportDelegate(PrintCashiersReport);
                    PrintCashiersReport(Details);
                }
            }

        }
        public delegate void PrintCashiersReportDelegate(Data.CashierReportDetails Details);
        public void PrintCashiersReport(Data.CashierReportDetails Details)
        {
            Cursor.Current = Cursors.WaitCursor;
            OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
            Invoke(opendrawerDel);

            try
            {
                decimal TrustFund = 0; //always put this as zero
                
                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Cashier's Report : " + mCashierName, mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                msbToPrint.Append("Gross Sales".PadRight(21) + ":" + ((Details.GrossSales + Details.TotalCharge) * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("(-) Service Charge".PadRight(21) + ":" + (Details.TotalCharge * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Amount".PadRight(21) + ":" + (Details.GrossSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append(("(-) " + mclsTerminalDetails.VAT.ToString("##") + "% VAT Exempt").PadRight(21) + ":" + (Details.VATExempt * (mclsTerminalDetails.VAT / 100) * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("(-) Subtotal Discount".PadRight(21) + ":" + (Details.SubTotalDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Net Sales".PadRight(21) + ":" + (Details.NetSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Taxables Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("VAT Exempt".PadRight(21) + ":" + (Details.VATExempt * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VAT Zero Rated".PadRight(21) + ":" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("NonVATable Amount".PadRight(21) + ":" + (Details.NonVATableAmount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VATable Amount".PadRight(21) + ":" + (Details.VATableAmount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("VAT".PadRight(21) + ":" + (Details.VAT * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Local Tax".PadRight(21) + ":" + (Details.LocalTax * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Total Amount Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash Sales".PadRight(21) + ":" + (Details.CashSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque Sales".PadRight(21) + ":" + (Details.ChequeSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card Sales".PadRight(21) + ":" + (Details.CreditCardSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit (Charge)".PadRight(21) + ":" + (Details.CreditSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Payment".PadRight(21) + ":" + (Details.CreditPayment * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("      Cash".PadRight(21) + ":" + (Details.CreditPaymentCash * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cheque".PadRight(21) + ":" + (Details.CreditPaymentCheque * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit Card".PadRight(21) + ":" + (Details.CreditPaymentCreditCard * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Debit".PadRight(21) + ":" + (Details.CreditPaymentDebit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("Debit  Sales".PadRight(21) + ":" + (Details.DebitPayment * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("RewardPoints Redeemd".PadRight(21) + ":" + Details.RewardPointsPayment.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Cash Equivalent".PadRight(21) + ":" + (Details.RewardConvertedPayment * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Employee Acct.".PadRight(21) + ":" + "0.00".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Void Sales".PadRight(21) + ":" + (Details.VoidSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Refund Sales".PadRight(21) + ":" + (Details.RefundSales * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cash".PadRight(21) + ":" + (Details.RefundCash * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Cheque".PadRight(21) + ":" + (Details.RefundCheque * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit Card".PadRight(21) + ":" + (Details.RefundCreditCard * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Credit".PadRight(21) + ":" + (Details.RefundCredit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("      Debit".PadRight(21) + ":" + (Details.RefundDebit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Discounts", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Items Discount".PadRight(21) + ":" + (Details.ItemsDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Subtotal Discount".PadRight(21) + ":" + (Details.SubTotalDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Senior Citizen".PadRight(21) + ":" + (Details.SNRDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     PWD".PadRight(21) + ":" + (Details.PWDDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("     Others".PadRight(21) + ":" + (Details.OtherDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Discounts".PadRight(21) + ":" + (Details.TotalDiscount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(mConnection, mTransaction);
                mConnection = clsSalesTransactions.Connection; mTransaction = clsSalesTransactions.Transaction;

                Data.TerminalReport clsTerminalReport = new Data.TerminalReport(mConnection, mTransaction);
                Data.TerminalReportDetails clsTerminalReportDetails = clsTerminalReport.Details(mclsTerminalDetails.BranchDetails.BranchID, Details.TerminalNo);
                System.Data.DataTable dt = clsSalesTransactions.Discounts(Details.BranchID, Details.TerminalNo, clsTerminalReportDetails.BeginningTransactionNo, clsTerminalReportDetails.EndingTransactionNo, Details.CashierID);
                clsSalesTransactions.CommitAndDispose();
                if (dt.Rows.Count > 0)
                {
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    msbToPrint.Append(CenterString("Subtotal Discounts Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                    msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                    foreach (System.Data.DataRow dr in dt.Rows)
                    { msbToPrint.Append(dr["DiscountCode"].ToString().PadRight(21) + ":" + (Convert.ToDecimal(dr["Discount"].ToString()) * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine); }
                }

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Drawer Information", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Beginning Balance".PadRight(21) + ":" + (Details.BeginningBalance).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cash-In-Drawer".PadRight(21) + ":" + (Details.BeginningBalance + ((Details.CashInDrawer - Details.BeginningBalance) * ((100 - TrustFund) / 100))).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Paid Out", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Paid Out".PadRight(21) + ":" + (Details.TotalPaidOut * ((100 - TrustFund) / 100)).ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PICK UP / Disburstment", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashDisburse * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeDisburse * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardDisburse * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Receive on Account", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashWithHold * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeWithHold * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardWithHold * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Customer Deposits", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash".PadRight(21) + ":" + (Details.CashDeposit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque".PadRight(21) + ":" + (Details.ChequeDeposit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card".PadRight(21) + ":" + (Details.CreditCardDeposit * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Transaction Count Breakdown", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash Transactions".PadRight(21) + ":" + Details.NoOfCashTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cheque Transactions".PadRight(21) + ":" + Details.NoOfChequeTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Card Trans.".PadRight(21) + ":" + Details.NoOfCreditCardTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Transactions".PadRight(21) + ":" + Details.NoOfCreditTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Debit Payment Trans.".PadRight(21) + ":" + Details.NoOfDebitPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Refund Transactions".PadRight(21) + ":" + Details.NoOfRefundTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Void Transactions".PadRight(21) + ":" + Details.NoOfVoidTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Combination Tran".PadRight(21) + ":" + Details.NoOfCombinationPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Credit Payment Trans".PadRight(21) + ":" + Details.NoOfCreditPaymentTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Reward Points  Trans".PadRight(21) + ":" + Details.NoOfRewardPointsPayment.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                //				msbToPrint.Append("Employees Acct Trans".PadRight(21) + ":" + "0".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22)  + Environment.NewLine);
                msbToPrint.Append("".PadRight(21) + ":" + "------------".PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22, ' ') + Environment.NewLine);
                msbToPrint.Append("Total Transactions".PadRight(21) + ":" + Details.NoOfTotalTransactions.ToString("#,##0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("Cash Count", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Cash In Drawer".PadRight(21) + ":" + (Details.CashInDrawer * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                msbToPrint.Append("Cash Count".PadRight(21) + ":" + (Details.CashCount * ((100 - TrustFund) / 100)).ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                if (Details.CashInDrawer > Details.CashCount)
                {
                    decimal decShort = Details.CashInDrawer - Details.CashCount;
                    msbToPrint.Append("Short".PadRight(21) + ":" + decShort.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                }
                else if (Details.CashCount > Details.CashInDrawer)
                {
                    decimal decOver = Details.CashCount - Details.CashInDrawer;
                    msbToPrint.Append("Over".PadRight(21) + ":" + decOver.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 22) + Environment.NewLine);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.PrintCashierReport, "Print cashier report: CashInDrawer=" + Details.CashInDrawer.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing cashier report data. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintCashCount

        public delegate void PrintCashCountDelegate(Data.CashCountDetails[] arrDetails);
        public void PrintCashCount(Data.CashCountDetails[] arrDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                OpenDrawerDelegate opendrawerDel = new OpenDrawerDelegate(OpenDrawer);
                Invoke(opendrawerDel);

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("CASH COUNT DECLARATION", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                decimal decTotalAmount = 0;
                foreach (Data.CashCountDetails Details in arrDetails)
                {
                    msbToPrint.Append(Details.DenominationValue.ToString("#,##0.#0").PadLeft(8));
                    msbToPrint.Append(" X ");
                    msbToPrint.Append(Details.DenominationCount.ToString("#,##0").PadRight(10));
                    msbToPrint.Append(" = ");
                    msbToPrint.Append(Details.DenominationAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 26) + Environment.NewLine);
                    decTotalAmount += Details.DenominationAmount;
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("TOTAL                     " + decTotalAmount.ToString("#,##0.#0").PadLeft(mclsTerminalDetails.MaxReceiptWidth - 28) + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.CashCount, "Print CASHCOUNT: " + decTotalAmount.ToString("#,##0.#0") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing cash count data. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintWithHold

        public delegate void PrintWithHoldDelegate(Data.WithholdDetails details);
        public void PrintWithHold(Data.WithholdDetails pvtWithHoldDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DateTime dteTransDate = mclsSalesTransactionDetails.TransactionDate;
                mclsSalesTransactionDetails.TransactionDate = pvtWithHoldDetails.DateCreated;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                //do not remove it here: this will revert the transaction date.
                mclsSalesTransactionDetails.TransactionDate = dteTransDate;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("WITHHOLD / RCV-ON-ACCOUNT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Amount	 : " + pvtWithHoldDetails.Amount.ToString("#,##0.#0") + Environment.NewLine);
                msbToPrint.Append("Wit. Type: " + pvtWithHoldDetails.PaymentType.ToString("G") + Environment.NewLine);
                if (pvtWithHoldDetails.Remarks != string.Empty && pvtWithHoldDetails.Remarks != null)
                    msbToPrint.Append("Remarks  : " + pvtWithHoldDetails.Remarks + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.Disburse, "Print WITHHOLD: " + pvtWithHoldDetails.Amount.ToString("#,##0.#0") + " " + pvtWithHoldDetails.PaymentType.ToString("G") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing wihhold data. Err Description: ");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintDisbursement

        public delegate void PrintDisbursementDelegate(Data.DisburseDetails pvtDisburseDetails);
        public void PrintDisbursement(Data.DisburseDetails pvtDisburseDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DateTime dteTransDate = mclsSalesTransactionDetails.TransactionDate;
                mclsSalesTransactionDetails.TransactionDate = pvtDisburseDetails.DateCreated;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                //do not remove it here: this will revert the transaction date.
                mclsSalesTransactionDetails.TransactionDate = dteTransDate;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("DISBURSEMENT / PICK-UP", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Amount	  : " + pvtDisburseDetails.Amount.ToString("#,##0.#0") + Environment.NewLine);
                msbToPrint.Append("Dis. Type : " + pvtDisburseDetails.PaymentType.ToString("G") + Environment.NewLine);
                if (pvtDisburseDetails.Remarks != string.Empty && pvtDisburseDetails.Remarks != null)
                    msbToPrint.Append("Remarks   : " + pvtDisburseDetails.Remarks + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;

                InsertAuditLog(AccessTypes.Disburse, "Print DISBURSEMENT: " + pvtDisburseDetails.Amount.ToString("#,##0.#0") + " " + pvtDisburseDetails.PaymentType.ToString("G") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing disbursement data. Err Description: ");
                Cursor.Current = Cursors.Default;
                throw ex;
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintPaidOut

        public delegate void PrintPaidOutDelegate(Data.PaidOutDetails details);
        public void PrintPaidOut(Data.PaidOutDetails pvtPaidOutDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DateTime dteTransDate = mclsSalesTransactionDetails.TransactionDate;
                mclsSalesTransactionDetails.TransactionDate = pvtPaidOutDetails.DateCreated;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                //do not remove it here: this will revert the transaction date.
                mclsSalesTransactionDetails.TransactionDate = dteTransDate;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("PAID-OUT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Amount	   : " + pvtPaidOutDetails.Amount.ToString("#,##0.#0") + Environment.NewLine);
                msbToPrint.Append("P-Out Type : " + pvtPaidOutDetails.PaymentType.ToString("G") + Environment.NewLine);
                if (pvtPaidOutDetails.Remarks != string.Empty && pvtPaidOutDetails.Remarks != null)
                    msbToPrint.Append("Remarks    : " + pvtPaidOutDetails.Remarks + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
                InsertAuditLog(AccessTypes.PaidOut, "Print PAID-OUT: " + pvtPaidOutDetails.Amount.ToString("#,##0.#0") + " " + pvtPaidOutDetails.PaymentType.ToString("G") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing paid-out data. Err Description: ");
                Cursor.Current = Cursors.Default;
                throw ex;
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PrintDeposit

        public delegate void PrintDepositDelegate(Data.DepositDetails pvtDepositDetails);
        public void PrintDeposit(Data.DepositDetails pvtDepositDetails)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DateTime dteTransDate = mclsSalesTransactionDetails.TransactionDate;
                mclsSalesTransactionDetails.TransactionDate = pvtDepositDetails.DateCreated;

                PrintingPreference oldCONFIG_AutoPrint = mclsTerminalDetails.AutoPrint;
                mclsTerminalDetails.AutoPrint = PrintingPreference.Normal;

                PrintReportHeadersSection(false);

                //do not remove it here: this will revert the transaction date.
                mclsSalesTransactionDetails.TransactionDate = dteTransDate;

                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append(CenterString("DEPOSIT AMOUNT", mclsTerminalDetails.MaxReceiptWidth) + Environment.NewLine);
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);
                msbToPrint.Append("Amount".PadRight(15) + ":" + pvtDepositDetails.Amount.ToString("#,##0.#0") + Environment.NewLine);
                msbToPrint.Append("Dis. Type".PadRight(15) + ":" + pvtDepositDetails.PaymentType.ToString("G") + Environment.NewLine);
                if (string.IsNullOrEmpty(pvtDepositDetails.Remarks))
                    msbToPrint.Append("Customer".PadRight(15) + ":" + pvtDepositDetails.ContactName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine + Environment.NewLine);
                else
                {
                    msbToPrint.Append("Customer".PadRight(15) + ":" + pvtDepositDetails.ContactName.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine);
                    msbToPrint.Append("Remarks".PadRight(15) + ":" + pvtDepositDetails.Remarks.PadLeft(mclsTerminalDetails.MaxReceiptWidth - 16) + Environment.NewLine + Environment.NewLine);
                }
                msbToPrint.Append("-".PadRight(mclsTerminalDetails.MaxReceiptWidth, '-') + Environment.NewLine);

                PrintPageAndReportFooterSection(false, DateTime.MinValue);

                mclsTerminalDetails.AutoPrint = oldCONFIG_AutoPrint;
                InsertAuditLog(AccessTypes.Deposit, "Print DEPOSIT: " + pvtDepositDetails.Amount.ToString("#,##0.#0") + " " + pvtDepositDetails.PaymentType.ToString("G") + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Printing deposit data. Err Description: ");
                Cursor.Current = Cursors.Default;
                throw ex;
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #endregion

        #region Turret

        public delegate void DisplayItemToTurretDelegate(string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT);
        public void DisplayItemToTurret(string Description, string stProductUnitCode, decimal Quantity, decimal Price, decimal Discount, decimal PromoApplied, decimal Amount, decimal VAT, decimal EVAT)
        {
            try
            {
                // clsEvent.AddEventLn("Displaying to turret...", true);

                string stDescription = Description;
                try
                { stDescription = Description.Split(Convert.ToChar(Environment.NewLine)).GetValue(0).ToString(); }
                catch { }
                if (stDescription.Length > 20)
                    stDescription = stDescription.Substring(0, 20);
                else
                    stDescription = stDescription.PadRight(20);

                string stAmount = Amount.ToString("#,##0.#0").PadLeft(20);
                SendStringToTurret(stDescription + stAmount + Environment.NewLine);

                //clsEvent.AddEventLn("Done!");
            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex);
            }
        }

        #endregion

        #region Open Drawer

        public delegate void OpenDrawerDelegate();
        public void OpenDrawer()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                clsEvent.AddEventLn("Opening cash drawer...", true);

                //Chr$(&H1B) + Chr$(&H70) + Chr$(&H0) + Chr$(&H2F) + Chr$(&H3F) '//drawer open
                //				string command = Convert.ToChar("&H1B").ToString() + Convert.ToChar("&H70").ToString() + Convert.ToChar("&H0").ToString() + Convert.ToChar("&H2F").ToString() + Convert.ToChar("&H3F").ToString();   // cut the paper  Chr$(86)
                string command = Convert.ToChar(27).ToString() + Convert.ToChar(112).ToString() + Convert.ToChar(0).ToString() + Convert.ToChar(47).ToString() + Convert.ToChar(63).ToString();   // cut the paper  Chr$(86)
                RawPrinterHelper.SendStringToPrinter(mclsTerminalDetails.CashDrawerName, command + "\f", "RetailPlus Drawer.");

                InsertAuditLog(AccessTypes.OpenDrawer, "Open cash drawer." + " @ Branch: " + mclsTerminalDetails.BranchDetails.BranchCode);
                clsEvent.AddEventLn("Done opening cash drawer!", true);

            }
            catch (Exception ex)
            {
                InsertErrorLogToFile(ex, "ERROR!!! Opening drawer.");
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Audit Logs

        public void InsertAuditLog(AccessTypes AccessType, string Remarks)
        {
            Methods.InsertAuditLog(mclsTerminalDetails, mCashierName, AccessType, Remarks);
        }

        public void InsertErrorLogToFile(Exception ex = null, string remarks = null)
        {
            if (remarks != null) clsEvent.AddEventLn(remarks, true);
            if (ex != null) clsEvent.AddErrorEventLn(ex);

            if (mConnection != null)
            {
                if (mConnection.State == System.Data.ConnectionState.Open)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                    clsEvent.AddEventLn("An open connnection has been found and closed.", true);
                }
            }

        }

        #endregion
    }
}
