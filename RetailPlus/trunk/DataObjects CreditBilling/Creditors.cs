using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace AceSoft.RetailPlus.Data
{

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class Creditors : POSConnection
    {
        #region Constructors and Destructors

		public Creditors()
            : base(null, null)
        {
        }

        public Creditors(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        /// <summary>
        /// Returns true : all have purchases
        /// Returns false : some doesnt have purchases
        /// </summary>
        /// <param name="clsContactDetails"></param>
        /// <param name="decCredit"></param>
        /// <returns></returns>
        public bool AutoAdjustCredit(Data.ContactDetails clsContactDetails, decimal decCredit)
        {
            bool boRetValue = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

                Int64 intContactID = clsContactDetails.ContactID;

                // pay all the purchases, we will do the reverse to arrive at equal credit in IC_ICC
                string SQL = "UPDATE tblCreditPayment SET AmountPaid = Amount WHERE ContactID=@ContactID;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ContactID", intContactID);
                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                if (decCredit == 0) return true; // return true

                SQL = "UPDATE tblContacts SET Credit = @Credit WHERE ContactID=@ContactID;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ContactID", intContactID);
                cmd.Parameters.AddWithValue("@Credit", decCredit);
                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                Data.Contacts clsContacts = new Data.Contacts(base.Connection, base.Transaction);
                Data.ProductDetails clsProductDetails = new Data.Products(base.Connection, base.Transaction).DetailsByCode(1, "IC IMPORTED TRX");
                Data.SalesTransactions clsSalesTransactions = new Data.SalesTransactions(base.Connection, base.Transaction);
                Data.CreditCardPayments clsCreditCardPayments = new Data.CreditCardPayments(base.Connection, base.Transaction);
                Data.SalesTransactionDetails clsSalesTransactionDetails;

                string strTerminalNo = "9995"; Int32 intBranchID = 1;
                string TransactionNo = DateTime.Now.ToString("yyyyMmddHHmmss").PadLeft(14, '0');
                DateTime CreatedOn = new DateTime(2014, 01, 01);
                DateTime TransactionDate = new DateTime(2014, 01, 01);
                DateTime DateClosed = new DateTime(2014, 01, 01);

                SQL = "SELECT * FROM tblCreditPayment WHERE ContactID=@ContactID ORDER BY CreditDate DESC;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ContactID", intContactID);

                cmd.CommandText = SQL;
                dtT = new System.Data.DataTable("tblTemp");
                base.MySqlDataAdapterFill(cmd, dtT);
                Int32 dtTRowsCount = dtT.Rows.Count;    //use this when reinsert is called below

            ReInsert:
                if (dtTRowsCount == 0)
                {

                    // no purchases but with credit
                    decimal SubTotal = decCredit;
                    TransactionNo = DateTime.Now.ToString("yyyyMmddHHmmss").PadLeft(14, '0');

                    //#region Insert to tblTransactions

                    SQL = "INSERT INTO tblTransactions(TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, BranchID, BranchCode, TransactionDate, " +
                            "DateSuspended, DateResumed, TransactionStatus, SubTotal, " +
                            "AmountPaid, CashPayment, ChequePayment, " +
                            "CreditCardPayment, CreditPayment, DateClosed, PaymentType, " +
                            "WaiterID, WaiterName, AgentID, AgentName, CreatedByID, CreatedByName, " +
                            "AgentDepartmentName, AgentPositionName, ReleasedDate, RewardPointsPayment, " +
                            "RewardConvertedPayment, PaxNo, CreditChargeAmount, TransactionType, isConsignment, " +
                            "DataSource, CustomerGroupName, CreatedOn, ORNo, " +
                            "NetSales, ChargeType, ItemSold, QuantitySold,  " +
                            "ContactCheckInDate, GrossSales)VALUES(";

                    SQL += "@TransactionNo, @CustomerID, @CustomerName, @CashierID, @CashierName, @TerminalNo, @BranchID, @BranchCode, @TransactionDate, " +
                            "@DateSuspended, @DateResumed, @TransactionStatus, @SubTotal, " +
                            "@AmountPaid, @CashPayment, @ChequePayment, " +
                            "@CreditCardPayment, @CreditPayment, @DateClosed, @PaymentType, " +
                            "@WaiterID, @WaiterName, @AgentID, @AgentName, @CreatedByID, @CreatedByName, " +
                            "@AgentDepartmentName, @AgentPositionName, @ReleasedDate, @RewardPointsPayment, " +
                            "@RewardConvertedPayment, @PaxNo, @CreditChargeAmount, @TransactionType, @isConsignment, " +
                            "@DataSource, @CustomerGroupName, @CreatedOn, @ORNo, " +
                            "@NetSales, @ChargeType, @ItemSold, @QuantitySold,  " +
                            "@ContactCheckInDate, @GrossSales)";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                    cmd.Parameters.AddWithValue("@CustomerID", clsContactDetails.ContactID);
                    cmd.Parameters.AddWithValue("@CustomerName", clsContactDetails.ContactName);
                    cmd.Parameters.AddWithValue("@CashierID", 1);
                    cmd.Parameters.AddWithValue("@CashierName", "Auto AdjUser");
                    cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
                    cmd.Parameters.AddWithValue("@BranchID", intBranchID);
                    cmd.Parameters.AddWithValue("@BranchCode", "Main");
                    cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                    cmd.Parameters.AddWithValue("@DateSuspended", Constants.C_DATE_MIN_VALUE);
                    cmd.Parameters.AddWithValue("@DateResumed", Constants.C_DATE_MIN_VALUE);
                    cmd.Parameters.AddWithValue("@TransactionStatus", 1);
                    cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
                    cmd.Parameters.AddWithValue("@AmountPaid", SubTotal);
                    cmd.Parameters.AddWithValue("@CashPayment", 0);
                    cmd.Parameters.AddWithValue("@ChequePayment", 0);
                    cmd.Parameters.AddWithValue("@CreditCardPayment", SubTotal);
                    cmd.Parameters.AddWithValue("@CreditPayment", 0);
                    cmd.Parameters.AddWithValue("@DateClosed", DateClosed);
                    cmd.Parameters.AddWithValue("@PaymentType", 2);
                    cmd.Parameters.AddWithValue("@WaiterID", 2);
                    cmd.Parameters.AddWithValue("@WaiterName", "RetailPlus Default");
                    cmd.Parameters.AddWithValue("@AgentID", 1);
                    cmd.Parameters.AddWithValue("@AgentName", "RetailPlus Agent ™");
                    cmd.Parameters.AddWithValue("@CreatedByID", 1);
                    cmd.Parameters.AddWithValue("@CreatedByName", "Auto AdjUser");
                    cmd.Parameters.AddWithValue("@AgentDepartmentName", "System Default Department");
                    cmd.Parameters.AddWithValue("@AgentPositionName", "System Default Position");
                    cmd.Parameters.AddWithValue("@ReleasedDate", Constants.C_DATE_MIN_VALUE);
                    cmd.Parameters.AddWithValue("@RewardPointsPayment", 0);
                    cmd.Parameters.AddWithValue("@RewardConvertedPayment", 0);
                    cmd.Parameters.AddWithValue("@PaxNo", 1);
                    cmd.Parameters.AddWithValue("@CreditChargeAmount", 0);
                    cmd.Parameters.AddWithValue("@TransactionType", 0);
                    cmd.Parameters.AddWithValue("@isConsignment", 0);
                    cmd.Parameters.AddWithValue("@DataSource", "IC_NO_ITN");
                    cmd.Parameters.AddWithValue("@CustomerGroupName", clsContactDetails.ContactGroupName);
                    cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    cmd.Parameters.AddWithValue("@ORNo", TransactionNo);
                    cmd.Parameters.AddWithValue("@NetSales", SubTotal);
                    cmd.Parameters.AddWithValue("@ChargeType", 0);
                    cmd.Parameters.AddWithValue("@ItemSold", 1);
                    cmd.Parameters.AddWithValue("@QuantitySold", 1);
                    cmd.Parameters.AddWithValue("@ContactCheckInDate", CreatedOn);
                    cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

                    cmd.CommandText = SQL;
                    base.ExecuteNonQuery(cmd);

                    //#endregion

                    clsSalesTransactionDetails = clsSalesTransactions.Details(TransactionNo, strTerminalNo, intBranchID);

                    //#region Insert to tblTransactionItems

                    SQL = "SELECT * FROM tblTransactionItems WHERE TransactionID=@TransactionID LIMIT 1;";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);

                    cmd.CommandText = SQL;
                    dtT = new System.Data.DataTable("tblTemp");
                    base.MySqlDataAdapterFill(cmd, dtT);

                    if (dtT.Rows.Count == 0)
                    {
                        SQL = "INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, " +
                                    "Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, " +
                                    "ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, " +
                                    "PackageQuantity, DataSource, CreatedOn, GrossSales)VALUES(";

                        SQL += "@TransactionID, @ProductID, @ProductCode, @BarCode, @Description, @ProductUnitID, @ProductUnitCode, " +
                                    "@Quantity, @Price, @SellingPrice, @Discount, @ItemDiscount, @ItemDiscountType, @Amount, " +
                                    "@ProductGroup, @ProductSubGroup, @TransactionItemStatus, @DiscountCode, @DiscountRemarks, @ProductPackageID, " +
                                    "@PackageQuantity, @DataSource, @CreatedOn, @GrossSales)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
                        cmd.Parameters.AddWithValue("@ProductID", clsProductDetails.ProductID);
                        cmd.Parameters.AddWithValue("@ProductCode", clsProductDetails.ProductCode);
                        cmd.Parameters.AddWithValue("@BarCode", clsProductDetails.BarCode);
                        cmd.Parameters.AddWithValue("@Description", clsProductDetails.ProductDesc);
                        cmd.Parameters.AddWithValue("@ProductUnitID", clsProductDetails.BaseUnitID);
                        cmd.Parameters.AddWithValue("@ProductUnitCode", clsProductDetails.BaseUnitCode);
                        cmd.Parameters.AddWithValue("@Quantity", 1);
                        cmd.Parameters.AddWithValue("@Price", SubTotal);
                        cmd.Parameters.AddWithValue("@SellingPrice", SubTotal);
                        cmd.Parameters.AddWithValue("@Discount", 0);
                        cmd.Parameters.AddWithValue("@ItemDiscount", 0);
                        cmd.Parameters.AddWithValue("@ItemDiscountType", 0);
                        cmd.Parameters.AddWithValue("@Amount", SubTotal);
                        cmd.Parameters.AddWithValue("@ProductGroup", clsProductDetails.ProductGroupName);
                        cmd.Parameters.AddWithValue("@ProductSubGroup", clsProductDetails.ProductSubGroupCode);
                        cmd.Parameters.AddWithValue("@TransactionItemStatus", 0);
                        cmd.Parameters.AddWithValue("@DiscountCode", "");
                        cmd.Parameters.AddWithValue("@DiscountRemarks", "");
                        cmd.Parameters.AddWithValue("@ProductPackageID", clsProductDetails.PackageID);
                        cmd.Parameters.AddWithValue("@PackageQuantity", 1);
                        cmd.Parameters.AddWithValue("@DataSource", "AutoAdjust");
                        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                        cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

                        cmd.CommandText = SQL;
                        base.ExecuteNonQuery(cmd);
                    }

                    //#endregion

                    //#region Insert to tblCreditCardPayment

                    SQL = "SELECT * FROM tblCreditCardPayment WHERE BranchID=@BranchID AND TransactionNo=@TransactionNo AND TerminalNo=@TerminalNo LIMIT 1;";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
                    cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);
                    cmd.Parameters.AddWithValue("@BranchID", clsSalesTransactionDetails.BranchID);

                    cmd.CommandText = SQL;
                    dtT = new System.Data.DataTable("tblTemp");
                    base.MySqlDataAdapterFill(cmd, dtT);

                    if (dtT.Rows.Count == 0)
                    {

                        SQL = "INSERT INTO tblCreditCardPayment(TransactionID, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, " +
                                    "Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID, AdditionalCharge, " +
                                    "ContactID, GuarantorID, TransactionDate, CashierName)VALUES(";

                        SQL += "@TransactionID, @Amount, @CardTypeID, @CardTypeCode, @CardTypeName, @CardNo, @CardHolder, @ValidityDates, " +
                                    "@Remarks, @TransactionNo, @CreatedOn, @TerminalNo, @BranchID, @AdditionalCharge, " +
                                    "@ContactID, @GuarantorID, @TransactionDate, @CashierName)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
                        cmd.Parameters.AddWithValue("@Amount", SubTotal);
                        cmd.Parameters.AddWithValue("@CardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
                        cmd.Parameters.AddWithValue("@CardTypeCode", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeCode);
                        cmd.Parameters.AddWithValue("@CardTypeName", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeName);
                        cmd.Parameters.AddWithValue("@CardNo", clsContactDetails.CreditDetails.CreditCardNo);
                        cmd.Parameters.AddWithValue("@CardHolder", clsContactDetails.ContactName);
                        cmd.Parameters.AddWithValue("@ValidityDates", clsContactDetails.CreditDetails.ExpiryDate.ToString("MMddyy"));
                        cmd.Parameters.AddWithValue("@Remarks", "");
                        cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                        cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
                        cmd.Parameters.AddWithValue("@BranchID", intBranchID);
                        cmd.Parameters.AddWithValue("@AdditionalCharge", 0);
                        cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
                        cmd.Parameters.AddWithValue("@GuarantorID", clsContactDetails.CreditDetails.GuarantorID);
                        cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
                        cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);

                        cmd.CommandText = SQL;
                        base.ExecuteNonQuery(cmd);
                    }

                    //#endregion

                    //#region Insert to tblCreditPayment

                    SQL = "SELECT * FROM tblCreditPayment WHERE BranchID=@BranchID AND TransactionNo=@TransactionNo AND TerminalNo = @TerminalNo LIMIT 1;";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
                    cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);
                    cmd.Parameters.AddWithValue("@BranchID", clsSalesTransactionDetails.BranchID);

                    cmd.CommandText = SQL;
                    dtT = new System.Data.DataTable("tblTemp");
                    base.MySqlDataAdapterFill(cmd, dtT);

                    if (dtT.Rows.Count == 0)
                    {
                        CreditCardPaymentDetails[] clsCreditCardPaymentDetails = clsCreditCardPayments.Details(1, strTerminalNo, clsSalesTransactionDetails.TransactionID);

                        SQL = "INSERT INTO tblCreditPayment(TransactionID, Amount, ContactID, Remarks, AmountPaid, " +
                                    "TransactionNo, CreditDate, CreditBefore, CreditAfter, " +
                                    "CreditReason, TerminalNo, CashierName, AmountPaidCuttOffMonth, " +
                                    "CreatedOn, BranchID, CreditCardPaymentID, CreditCardTypeID, CreditReasonID)VALUES(";

                        SQL += "@TransactionID, @Amount, @ContactID, @Remarks, @AmountPaid, " +
                                    "@TransactionNo, @CreditDate, @CreditBefore, @CreditAfter, " +
                                    "@CreditReason, @TerminalNo, @CashierName, @AmountPaidCuttOffMonth, " +
                                    "@CreatedOn, @BranchID, @CreditCardPaymentID, @CreditCardTypeID, @CreditReasonID)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
                        cmd.Parameters.AddWithValue("@Amount", SubTotal);
                        cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
                        cmd.Parameters.AddWithValue("@Remarks", "Deliquent purchases before 2014May ");
                        cmd.Parameters.AddWithValue("@AmountPaid", 0);
                        cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
                        cmd.Parameters.AddWithValue("@CreditDate", CreatedOn);
                        cmd.Parameters.AddWithValue("@CreditBefore", 0);
                        cmd.Parameters.AddWithValue("@CreditAfter", SubTotal);
                        cmd.Parameters.AddWithValue("@CreditReason", "Deliquent purchases before 2014May ");
                        cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
                        cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);
                        cmd.Parameters.AddWithValue("@AmountPaidCuttOffMonth", 0);
                        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                        cmd.Parameters.AddWithValue("@BranchID", intBranchID);
                        cmd.Parameters.AddWithValue("@CreditCardPaymentID", clsCreditCardPaymentDetails[0].CreditCardPaymentID);
                        cmd.Parameters.AddWithValue("@CreditCardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
                        cmd.Parameters.AddWithValue("@CreditReasonID", 0);

                        cmd.CommandText = SQL;
                        base.ExecuteNonQuery(cmd);

                        boRetValue = false;
                    }
                }
                else
                {
                    foreach (System.Data.DataRow drCredit in dtT.Rows)
                    {
                        decimal decTrxCredit = decimal.Parse(drCredit["Amount"].ToString());
                        Int64 CreditPaymentID = Int64.Parse(drCredit["CreditPaymentID"].ToString());

                        if (decCredit > decTrxCredit)
                        {
                            SQL = "UPDATE tblCreditPayment SET AmountPaid=(AmountPaid - @AmountPaid) WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@AmountPaid", decTrxCredit);
                            cmd.Parameters.AddWithValue("@ContactID", intContactID);
                            cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);

                            cmd.CommandText = SQL;
                            base.ExecuteNonQuery(cmd);

                            decCredit -= decTrxCredit;
                        }
                        else
                        {
                            SQL = "UPDATE tblCreditPayment SET AmountPaid= (AmountPaid - @AmountPaid) WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@AmountPaid", decCredit);
                            cmd.Parameters.AddWithValue("@ContactID", intContactID);
                            cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);

                            cmd.CommandText = SQL;
                            base.ExecuteNonQuery(cmd);

                            boRetValue = true;
                            decCredit = 0;
                            break;
                        }
                    }

                    // meaning there is still credit but no purchases
                    if (decCredit > 0)
                    {
                        dtTRowsCount = 0;

                        goto ReInsert;
                        //#endregion
                    }
                }
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
            return boRetValue;
        }

        public void DeleteCreditTransaction(Int32 BranchID, string TerminalNo, Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "";

                SQL = "DELETE FROM tblCreditPaymentCash " +
                      "WHERE TerminalNo=@TerminalNo AND BranchID=@BranchID AND TransactionID=@TransactionID;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SQL = "UPDATE tblTransactionItems SET TransactionItemStatus=@TransactionItemStatus, Barcode='CancelledCreditPayment', ProductCode='CancelledCreditPayment', Description='CancelledCreditPayment' " +
                      "WHERE TransactionID=@TransactionID;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TransactionItemStatus", TransactionItemStatus.CancelledCreditPayment);
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                SQL = "UPDATE tblTransactions SET TransactionStatus=@TransactionStatus " +
                      "WHERE TerminalNo=@TerminalNo AND BranchID=@BranchID AND TransactionID=@TransactionID;";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TransactionStatus", TransactionStatus.CancelledCreditPayment);
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@TerminalNo", TerminalNo);

                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
    }
}
