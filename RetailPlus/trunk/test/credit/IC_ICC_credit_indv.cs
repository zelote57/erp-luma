//using System;
//using System.Management;
//using AceSoft.RetailPlus;
//using MySql.Data.MySqlClient;
//using System.Data;
//using AceSoft.RetailPlus.Data;
//using AceSoft.RetailPlus.Reports;
//using AceSoft.RetailPlus.Security;

//namespace Test
//{
//    /******************************************************************************
//    **		Auth: Lemuel E. Aceron
//    **		Date: May 21, 2006
//    *******************************************************************************
//    **		Change History
//    *******************************************************************************
//    **		Date:		Author:				Description:
//    **		--------		--------				-------------------------------------------
//    **    
//    *******************************************************************************/

//    /// <summary>
//    /// Summary description for IC_NO_ITN.
//    /// </summary>
//    public class IC_NO_ITN
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 100;


//                Console.WriteLine("RetailPlus: Credit bill adjustment");

//                Console.WriteLine();
//                Console.Write("Enter CreditCard No: ");
//                string CreditCardNo = Console.ReadLine();

//                if (string.IsNullOrEmpty(CreditCardNo))
//                {
//                    Console.Write("Sorry the credit card no should be entered. Press the enter key to exit.");
//                    Console.ReadLine();
//                    return;
//                }

//                Console.WriteLine();
//                Console.Write("Enter the correct Current Credit Amount: ");
//                string strCreditLimit = Console.ReadLine();

//                decimal Credit = 0;
//                if (string.IsNullOrEmpty(strCreditLimit))
//                {
//                    Console.Write("Sorry the credit limit should be entered. Press the enter key to exit.");
//                    Console.ReadLine();
//                    return;
//                }
//                else {
//                    try {
//                        Credit = decimal.Parse(strCreditLimit);
//                    }
//                    catch {
//                        Console.Write("Sorry the credit limit entered should be a number. Press the enter key to exit.");
//                        Console.ReadLine();
//                        return;
//                    }
//                }

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                // get all the contacts with credit
//                string SQL = "SELECT ContactID, ContactName, Credit FROM tblContactCreditCardInfo cci INNER JOIN tblContacts cntct ON cci.CustomerID=cntct.ContactID  WHERE CreditCardNo=@CreditCardNo;";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);
//                cmd.CommandText = SQL;
//                dtT = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                Int64 ContactID = 0;
//                string ContactName = "";
//                string strCurrentCredit = "0";
//                if (dtT.Rows.Count == 0)
//                {
//                    clsLocalConnection.CommitAndDispose();
//                    Console.Write("Sorry the credit card no you entered is not found in the system. Please double check then press the enter key to exit.");
//                    Console.ReadLine();
//                    return;
//                }
//                else
//                {
//                    ContactID = Int64.Parse(dtT.Rows[0]["ContactID"].ToString());
//                    ContactName = dtT.Rows[0]["ContactName"].ToString();
//                    strCurrentCredit = dtT.Rows[0]["Credit"].ToString();
//                }

//                if (ContactID == 0)
//                {
//                    clsLocalConnection.CommitAndDispose();
//                    Console.Write("Sorry an invalid contact has been found. Please double check then press the enter key to exit.");
//                    Console.ReadLine();
//                    return;
//                }

//                Console.WriteLine();
//                Console.WriteLine("Connected to " + clsLocalConnection.Connection.ConnectionString);
//                Console.WriteLine();
//                Console.WriteLine("This will update customer: " + ContactName + " with creditcardno: " + CreditCardNo + " current due from " + strCurrentCredit + " to " + strCreditLimit + ".");
//                Console.Write("Press any key to continue to CTRL + C to abort.");
//                Console.ReadLine();

//                // pay all the purchases, we will do the reverse to arrive at equal credit in IC_ICC
//                SQL = "UPDATE tblCreditPayment SET AmountPaid = Amount WHERE ContactID=@ContactID;";

//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@ContactID", ContactID);
//                cmd.CommandText = SQL;
//                clsLocalConnection.ExecuteNonQuery(cmd);

//                SQL = "UPDATE tblContacts SET Credit = @Credit WHERE ContactID=@ContactID;";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@ContactID", ContactID);
//                cmd.Parameters.AddWithValue("@Credit", Credit);
//                cmd.CommandText = SQL;
//                clsLocalConnection.ExecuteNonQuery(cmd);

//                // get all the contacts with credit
//                SQL = "SELECT ContactID, ContactCode, Credit FROM tblContacts WHERE Credit > 0 AND ContactID=@ContactID;";
//                cmd.Parameters.Clear();
//                cmd.Parameters.AddWithValue("@ContactID", ContactID);

//                cmd.CommandText = SQL;
//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                ProductDetails clsProductDetails = new Products(clsLocalConnection.Connection, clsLocalConnection.Transaction).DetailsByCode(1, "IC IMPORTED TRX");
//                SalesTransactions clsSalesTransactions = new SalesTransactions(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditCardPayments clsCreditCardPayments = new CreditCardPayments(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                ContactDetails clsContactDetails;
//                SalesTransactionDetails clsSalesTransactionDetails;

//                string strTerminalNo = "9995";
//                string TransactionNo = DateTime.Now.ToString("yyyyMmddHHmmss").PadLeft(14, '0');
//                DateTime CreatedOn = new DateTime(2014, 01, 01);
//                DateTime TransactionDate = new DateTime(2014, 01, 01);
//                DateTime DateClosed = new DateTime(2014, 01, 01);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    ContactID = Int64.Parse(dr["ContactID"].ToString());
//                    string ContactCode = dr["ContactCode"].ToString();
//                    decimal decCredit = decimal.Parse(dr["Credit"].ToString());

//                    clsContactDetails = clsContacts.Details(ContactID);

//                    SQL = "SELECT * FROM tblCreditPayment WHERE ContactID=@ContactID ORDER BY CreditDate DESC;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@ContactID", ContactID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (dtT.Rows.Count == 0)
//                    {
//                        // no purchases but with credit
//                        decimal SubTotal = decCredit;
//                        TransactionNo = DateTime.Now.ToString("yyyyMmddHHmmss").PadLeft(14, '0');

//                        //#region Insert to tblTransactions

//                        SQL = "INSERT INTO tblTransactions(TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, BranchID, BranchCode, TransactionDate, " +
//                                "DateSuspended, DateResumed, TransactionStatus, SubTotal, " +
//                                "AmountPaid, CashPayment, ChequePayment, " +
//                                "CreditCardPayment, CreditPayment, DateClosed, PaymentType, " +
//                                "WaiterID, WaiterName, AgentID, AgentName, CreatedByID, CreatedByName, " +
//                                "AgentDepartmentName, AgentPositionName, ReleasedDate, RewardPointsPayment, " +
//                                "RewardConvertedPayment, PaxNo, CreditChargeAmount, TransactionType, isConsignment, " +
//                                "DataSource, CustomerGroupName, CreatedOn, ORNo, " +
//                                "NetSales, ChargeType, ItemSold, QuantitySold,  " +
//                                "ContactCheckInDate, GrossSales)VALUES(";

//                        SQL += "@TransactionNo, @CustomerID, @CustomerName, @CashierID, @CashierName, @TerminalNo, @BranchID, @BranchCode, @TransactionDate, " +
//                                "@DateSuspended, @DateResumed, @TransactionStatus, @SubTotal, " +
//                                "@AmountPaid, @CashPayment, @ChequePayment, " +
//                                "@CreditCardPayment, @CreditPayment, @DateClosed, @PaymentType, " +
//                                "@WaiterID, @WaiterName, @AgentID, @AgentName, @CreatedByID, @CreatedByName, " +
//                                "@AgentDepartmentName, @AgentPositionName, @ReleasedDate, @RewardPointsPayment, " +
//                                "@RewardConvertedPayment, @PaxNo, @CreditChargeAmount, @TransactionType, @isConsignment, " +
//                                "@DataSource, @CustomerGroupName, @CreatedOn, @ORNo, " +
//                                "@NetSales, @ChargeType, @ItemSold, @QuantitySold,  " +
//                                "@ContactCheckInDate, @GrossSales)";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                        cmd.Parameters.AddWithValue("@CustomerID", clsContactDetails.ContactID);
//                        cmd.Parameters.AddWithValue("@CustomerName", clsContactDetails.ContactName);
//                        cmd.Parameters.AddWithValue("@CashierID", 1);
//                        cmd.Parameters.AddWithValue("@CashierName", "ICC SysUser");
//                        cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                        cmd.Parameters.AddWithValue("@BranchID", 1);
//                        cmd.Parameters.AddWithValue("@BranchCode", "Main");
//                        cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
//                        cmd.Parameters.AddWithValue("@DateSuspended", Constants.C_DATE_MIN_VALUE);
//                        cmd.Parameters.AddWithValue("@DateResumed", Constants.C_DATE_MIN_VALUE);
//                        cmd.Parameters.AddWithValue("@TransactionStatus", 1);
//                        cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
//                        cmd.Parameters.AddWithValue("@AmountPaid", SubTotal);
//                        cmd.Parameters.AddWithValue("@CashPayment", 0);
//                        cmd.Parameters.AddWithValue("@ChequePayment", 0);
//                        cmd.Parameters.AddWithValue("@CreditCardPayment", SubTotal);
//                        cmd.Parameters.AddWithValue("@CreditPayment", 0);
//                        cmd.Parameters.AddWithValue("@DateClosed", DateClosed);
//                        cmd.Parameters.AddWithValue("@PaymentType", 2);
//                        cmd.Parameters.AddWithValue("@WaiterID", 2);
//                        cmd.Parameters.AddWithValue("@WaiterName", "RetailPlus Default");
//                        cmd.Parameters.AddWithValue("@AgentID", 1);
//                        cmd.Parameters.AddWithValue("@AgentName", "RetailPlus Agent ™");
//                        cmd.Parameters.AddWithValue("@CreatedByID", 1);
//                        cmd.Parameters.AddWithValue("@CreatedByName", "ICC SysUser");
//                        cmd.Parameters.AddWithValue("@AgentDepartmentName", "System Default Department");
//                        cmd.Parameters.AddWithValue("@AgentPositionName", "System Default Position");
//                        cmd.Parameters.AddWithValue("@ReleasedDate", Constants.C_DATE_MIN_VALUE);
//                        cmd.Parameters.AddWithValue("@RewardPointsPayment", 0);
//                        cmd.Parameters.AddWithValue("@RewardConvertedPayment", 0);
//                        cmd.Parameters.AddWithValue("@PaxNo", 1);
//                        cmd.Parameters.AddWithValue("@CreditChargeAmount", 0);
//                        cmd.Parameters.AddWithValue("@TransactionType", 0);
//                        cmd.Parameters.AddWithValue("@isConsignment", 0);
//                        cmd.Parameters.AddWithValue("@DataSource", "IC_NO_ITN");
//                        cmd.Parameters.AddWithValue("@CustomerGroupName", clsContactDetails.ContactGroupName);
//                        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                        cmd.Parameters.AddWithValue("@ORNo", TransactionNo);
//                        cmd.Parameters.AddWithValue("@NetSales", SubTotal);
//                        cmd.Parameters.AddWithValue("@ChargeType", 0);
//                        cmd.Parameters.AddWithValue("@ItemSold", 1);
//                        cmd.Parameters.AddWithValue("@QuantitySold", 1);
//                        cmd.Parameters.AddWithValue("@ContactCheckInDate", CreatedOn);
//                        cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        //#endregion

//                        clsSalesTransactionDetails = clsSalesTransactions.Details(TransactionNo, strTerminalNo, 1);

//                        //#region Insert to tblTransactionItems

//                        SQL = "SELECT * FROM tblTransactionItems WHERE TransactionID=@TransactionID LIMIT 1;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);

//                        cmd.CommandText = SQL;
//                        dtT = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                        if (dtT.Rows.Count == 0)
//                        {
//                            SQL = "INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, " +
//                                        "Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, " +
//                                        "ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, " +
//                                        "PackageQuantity, DataSource, CreatedOn, GrossSales)VALUES(";

//                            SQL += "@TransactionID, @ProductID, @ProductCode, @BarCode, @Description, @ProductUnitID, @ProductUnitCode, " +
//                                        "@Quantity, @Price, @SellingPrice, @Discount, @ItemDiscount, @ItemDiscountType, @Amount, " +
//                                        "@ProductGroup, @ProductSubGroup, @TransactionItemStatus, @DiscountCode, @DiscountRemarks, @ProductPackageID, " +
//                                        "@PackageQuantity, @DataSource, @CreatedOn, @GrossSales)";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                            cmd.Parameters.AddWithValue("@ProductID", clsProductDetails.ProductID);
//                            cmd.Parameters.AddWithValue("@ProductCode", clsProductDetails.ProductCode);
//                            cmd.Parameters.AddWithValue("@BarCode", clsProductDetails.BarCode);
//                            cmd.Parameters.AddWithValue("@Description", clsProductDetails.ProductDesc);
//                            cmd.Parameters.AddWithValue("@ProductUnitID", clsProductDetails.BaseUnitID);
//                            cmd.Parameters.AddWithValue("@ProductUnitCode", clsProductDetails.BaseUnitCode);
//                            cmd.Parameters.AddWithValue("@Quantity", 1);
//                            cmd.Parameters.AddWithValue("@Price", SubTotal);
//                            cmd.Parameters.AddWithValue("@SellingPrice", SubTotal);
//                            cmd.Parameters.AddWithValue("@Discount", 0);
//                            cmd.Parameters.AddWithValue("@ItemDiscount", 0);
//                            cmd.Parameters.AddWithValue("@ItemDiscountType", 0);
//                            cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                            cmd.Parameters.AddWithValue("@ProductGroup", clsProductDetails.ProductGroupName);
//                            cmd.Parameters.AddWithValue("@ProductSubGroup", clsProductDetails.ProductSubGroupCode);
//                            cmd.Parameters.AddWithValue("@TransactionItemStatus", 0);
//                            cmd.Parameters.AddWithValue("@DiscountCode", "");
//                            cmd.Parameters.AddWithValue("@DiscountRemarks", "");
//                            cmd.Parameters.AddWithValue("@ProductPackageID", clsProductDetails.PackageID);
//                            cmd.Parameters.AddWithValue("@PackageQuantity", 1);
//                            cmd.Parameters.AddWithValue("@DataSource", "IC_NO_ITN");
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);
//                        }

//                        //#endregion

//                        //#region Insert to tblCreditCardPayment

//                        SQL = "SELECT * FROM tblCreditCardPayment WHERE TransactionNo=@TransactionNo AND TerminalNo=@TerminalNo LIMIT 1;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
//                        cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);

//                        cmd.CommandText = SQL;
//                        dtT = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                        if (dtT.Rows.Count == 0)
//                        {

//                            SQL = "INSERT INTO tblCreditCardPayment(TransactionID, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, " +
//                                        "Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID, AdditionalCharge, " +
//                                        "ContactID, GuarantorID, TransactionDate, CashierName)VALUES(";

//                            SQL += "@TransactionID, @Amount, @CardTypeID, @CardTypeCode, @CardTypeName, @CardNo, @CardHolder, @ValidityDates, " +
//                                        "@Remarks, @TransactionNo, @CreatedOn, @TerminalNo, @BranchID, @AdditionalCharge, " +
//                                        "@ContactID, @GuarantorID, @TransactionDate, @CashierName)";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                            cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                            cmd.Parameters.AddWithValue("@CardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
//                            cmd.Parameters.AddWithValue("@CardTypeCode", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeCode);
//                            cmd.Parameters.AddWithValue("@CardTypeName", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeName);
//                            cmd.Parameters.AddWithValue("@CardNo", clsContactDetails.CreditDetails.CreditCardNo);
//                            cmd.Parameters.AddWithValue("@CardHolder", clsContactDetails.ContactName);
//                            cmd.Parameters.AddWithValue("@ValidityDates", clsContactDetails.CreditDetails.ExpiryDate.ToString("MMddyy"));
//                            cmd.Parameters.AddWithValue("@Remarks", "");
//                            cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                            cmd.Parameters.AddWithValue("@BranchID", 1);
//                            cmd.Parameters.AddWithValue("@AdditionalCharge", 0);
//                            cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
//                            cmd.Parameters.AddWithValue("@GuarantorID", clsContactDetails.CreditDetails.GuarantorID);
//                            cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
//                            cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);
//                        }

//                        //#endregion

//                        //#region Insert to tblCreditPayment

//                        SQL = "SELECT * FROM tblCreditPayment WHERE TransactionNo=@TransactionNo AND TerminalNo = @TerminalNo LIMIT 1;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
//                        cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);

//                        cmd.CommandText = SQL;
//                        dtT = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                        if (dtT.Rows.Count == 0)
//                        {
//                            CreditCardPaymentDetails[] clsCreditCardPaymentDetails = clsCreditCardPayments.Details(1, strTerminalNo, clsSalesTransactionDetails.TransactionID);

//                            SQL = "INSERT INTO tblCreditPayment(TransactionID, Amount, ContactID, Remarks, AmountPaid, " +
//                                        "TransactionNo, CreditDate, CreditBefore, CreditAfter, " +
//                                        "CreditReason, TerminalNo, CashierName, AmountPaidCuttOffMonth, " +
//                                        "CreatedOn, BranchID, CreditCardPaymentID, CreditCardTypeID, CreditReasonID)VALUES(";

//                            SQL += "@TransactionID, @Amount, @ContactID, @Remarks, @AmountPaid, " +
//                                        "@TransactionNo, @CreditDate, @CreditBefore, @CreditAfter, " +
//                                        "@CreditReason, @TerminalNo, @CashierName, @AmountPaidCuttOffMonth, " +
//                                        "@CreatedOn, @BranchID, @CreditCardPaymentID, @CreditCardTypeID, @CreditReasonID)";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                            cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                            cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
//                            cmd.Parameters.AddWithValue("@Remarks", "Deliquent purchases before 2014May ");
//                            cmd.Parameters.AddWithValue("@AmountPaid", 0);
//                            cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                            cmd.Parameters.AddWithValue("@CreditDate", CreatedOn);
//                            cmd.Parameters.AddWithValue("@CreditBefore", 0);
//                            cmd.Parameters.AddWithValue("@CreditAfter", SubTotal);
//                            cmd.Parameters.AddWithValue("@CreditReason", "Deliquent purchases before 2014May ");
//                            cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                            cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);
//                            cmd.Parameters.AddWithValue("@AmountPaidCuttOffMonth", 0);
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@BranchID", 1);
//                            cmd.Parameters.AddWithValue("@CreditCardPaymentID", clsCreditCardPaymentDetails[0].CreditCardPaymentID);
//                            cmd.Parameters.AddWithValue("@CreditCardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
//                            cmd.Parameters.AddWithValue("@CreditReasonID", 0);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);
//                        }

//                        //#endregion

//                        Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases. " + clsSalesTransactionDetails.TransactionNo);
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        foreach (System.Data.DataRow drCredit in dtT.Rows)
//                        {
//                            decimal decTrxCredit = decimal.Parse(drCredit["Amount"].ToString());
//                            Int64 CreditPaymentID = Int64.Parse(drCredit["CreditPaymentID"].ToString());

//                            if (decCredit > decTrxCredit)
//                            {
//                                SQL = "UPDATE tblCreditPayment SET AmountPaid=(AmountPaid - @AmountPaid) WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@AmountPaid", decTrxCredit);
//                                cmd.Parameters.AddWithValue("@ContactID", ContactID);
//                                cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);

//                                decCredit -= decTrxCredit;
//                            }
//                            else
//                            {
//                                SQL = "UPDATE tblCreditPayment SET AmountPaid= (AmountPaid - @AmountPaid) WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@AmountPaid", decCredit);
//                                cmd.Parameters.AddWithValue("@ContactID", ContactID);
//                                cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);

//                                Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " purchases updated.");
//                                iNotTheSame++;

//                                decCredit = 0;
//                                break;
//                            }
//                        }

//                        // meaning there is still credit but no purchases
//                        if (decCredit > 0)
//                        {
//                            decimal SubTotal = decCredit;
//                            TransactionNo = DateTime.Now.ToString("yyyyMmddHHmmss").PadLeft(14, '0');

//                            //#region Insert to tblTransactions

//                            SQL = "INSERT INTO tblTransactions(TransactionNo, CustomerID, CustomerName, CashierID, CashierName, TerminalNo, BranchID, BranchCode, TransactionDate, " +
//                                    "DateSuspended, DateResumed, TransactionStatus, SubTotal, " +
//                                    "AmountPaid, CashPayment, ChequePayment, " +
//                                    "CreditCardPayment, CreditPayment, DateClosed, PaymentType, " +
//                                    "WaiterID, WaiterName, AgentID, AgentName, CreatedByID, CreatedByName, " +
//                                    "AgentDepartmentName, AgentPositionName, ReleasedDate, RewardPointsPayment, " +
//                                    "RewardConvertedPayment, PaxNo, CreditChargeAmount, TransactionType, isConsignment, " +
//                                    "DataSource, CustomerGroupName, CreatedOn, ORNo, " +
//                                    "NetSales, ChargeType, ItemSold, QuantitySold,  " +
//                                    "ContactCheckInDate, GrossSales)VALUES(";

//                            SQL += "@TransactionNo, @CustomerID, @CustomerName, @CashierID, @CashierName, @TerminalNo, @BranchID, @BranchCode, @TransactionDate, " +
//                                    "@DateSuspended, @DateResumed, @TransactionStatus, @SubTotal, " +
//                                    "@AmountPaid, @CashPayment, @ChequePayment, " +
//                                    "@CreditCardPayment, @CreditPayment, @DateClosed, @PaymentType, " +
//                                    "@WaiterID, @WaiterName, @AgentID, @AgentName, @CreatedByID, @CreatedByName, " +
//                                    "@AgentDepartmentName, @AgentPositionName, @ReleasedDate, @RewardPointsPayment, " +
//                                    "@RewardConvertedPayment, @PaxNo, @CreditChargeAmount, @TransactionType, @isConsignment, " +
//                                    "@DataSource, @CustomerGroupName, @CreatedOn, @ORNo, " +
//                                    "@NetSales, @ChargeType, @ItemSold, @QuantitySold,  " +
//                                    "@ContactCheckInDate, @GrossSales)";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                            cmd.Parameters.AddWithValue("@CustomerID", clsContactDetails.ContactID);
//                            cmd.Parameters.AddWithValue("@CustomerName", clsContactDetails.ContactName);
//                            cmd.Parameters.AddWithValue("@CashierID", 1);
//                            cmd.Parameters.AddWithValue("@CashierName", "ICC SysUser");
//                            cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                            cmd.Parameters.AddWithValue("@BranchID", 1);
//                            cmd.Parameters.AddWithValue("@BranchCode", "Main");
//                            cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
//                            cmd.Parameters.AddWithValue("@DateSuspended", Constants.C_DATE_MIN_VALUE);
//                            cmd.Parameters.AddWithValue("@DateResumed", Constants.C_DATE_MIN_VALUE);
//                            cmd.Parameters.AddWithValue("@TransactionStatus", 1);
//                            cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
//                            cmd.Parameters.AddWithValue("@AmountPaid", SubTotal);
//                            cmd.Parameters.AddWithValue("@CashPayment", 0);
//                            cmd.Parameters.AddWithValue("@ChequePayment", 0);
//                            cmd.Parameters.AddWithValue("@CreditCardPayment", SubTotal);
//                            cmd.Parameters.AddWithValue("@CreditPayment", 0);
//                            cmd.Parameters.AddWithValue("@DateClosed", DateClosed);
//                            cmd.Parameters.AddWithValue("@PaymentType", 2);
//                            cmd.Parameters.AddWithValue("@WaiterID", 2);
//                            cmd.Parameters.AddWithValue("@WaiterName", "RetailPlus Default");
//                            cmd.Parameters.AddWithValue("@AgentID", 1);
//                            cmd.Parameters.AddWithValue("@AgentName", "RetailPlus Agent ™");
//                            cmd.Parameters.AddWithValue("@CreatedByID", 1);
//                            cmd.Parameters.AddWithValue("@CreatedByName", "ICC SysUser");
//                            cmd.Parameters.AddWithValue("@AgentDepartmentName", "System Default Department");
//                            cmd.Parameters.AddWithValue("@AgentPositionName", "System Default Position");
//                            cmd.Parameters.AddWithValue("@ReleasedDate", Constants.C_DATE_MIN_VALUE);
//                            cmd.Parameters.AddWithValue("@RewardPointsPayment", 0);
//                            cmd.Parameters.AddWithValue("@RewardConvertedPayment", 0);
//                            cmd.Parameters.AddWithValue("@PaxNo", 1);
//                            cmd.Parameters.AddWithValue("@CreditChargeAmount", 0);
//                            cmd.Parameters.AddWithValue("@TransactionType", 0);
//                            cmd.Parameters.AddWithValue("@isConsignment", 0);
//                            cmd.Parameters.AddWithValue("@DataSource", "IC_NO_ITN");
//                            cmd.Parameters.AddWithValue("@CustomerGroupName", clsContactDetails.ContactGroupName);
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@ORNo", TransactionNo);
//                            cmd.Parameters.AddWithValue("@NetSales", SubTotal);
//                            cmd.Parameters.AddWithValue("@ChargeType", 0);
//                            cmd.Parameters.AddWithValue("@ItemSold", 1);
//                            cmd.Parameters.AddWithValue("@QuantitySold", 1);
//                            cmd.Parameters.AddWithValue("@ContactCheckInDate", CreatedOn);
//                            cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            //#endregion

//                            clsSalesTransactionDetails = clsSalesTransactions.Details(TransactionNo, strTerminalNo, 1);

//                            //#region Insert to tblTransactionItems

//                            SQL = "SELECT * FROM tblTransactionItems WHERE TransactionID=@TransactionID LIMIT 1;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);

//                            cmd.CommandText = SQL;
//                            dtT = new System.Data.DataTable("tblTemp");
//                            clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                            if (dtT.Rows.Count == 0)
//                            {
//                                SQL = "INSERT INTO tblTransactionItems(TransactionID, ProductID, ProductCode, BarCode, Description, ProductUnitID, ProductUnitCode, " +
//                                            "Quantity, Price, SellingPrice, Discount, ItemDiscount, ItemDiscountType, Amount, " +
//                                            "ProductGroup, ProductSubGroup, TransactionItemStatus, DiscountCode, DiscountRemarks, ProductPackageID, " +
//                                            "PackageQuantity, DataSource, CreatedOn, GrossSales)VALUES(";

//                                SQL += "@TransactionID, @ProductID, @ProductCode, @BarCode, @Description, @ProductUnitID, @ProductUnitCode, " +
//                                            "@Quantity, @Price, @SellingPrice, @Discount, @ItemDiscount, @ItemDiscountType, @Amount, " +
//                                            "@ProductGroup, @ProductSubGroup, @TransactionItemStatus, @DiscountCode, @DiscountRemarks, @ProductPackageID, " +
//                                            "@PackageQuantity, @DataSource, @CreatedOn, @GrossSales)";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                                cmd.Parameters.AddWithValue("@ProductID", clsProductDetails.ProductID);
//                                cmd.Parameters.AddWithValue("@ProductCode", clsProductDetails.ProductCode);
//                                cmd.Parameters.AddWithValue("@BarCode", clsProductDetails.BarCode);
//                                cmd.Parameters.AddWithValue("@Description", clsProductDetails.ProductDesc);
//                                cmd.Parameters.AddWithValue("@ProductUnitID", clsProductDetails.BaseUnitID);
//                                cmd.Parameters.AddWithValue("@ProductUnitCode", clsProductDetails.BaseUnitCode);
//                                cmd.Parameters.AddWithValue("@Quantity", 1);
//                                cmd.Parameters.AddWithValue("@Price", SubTotal);
//                                cmd.Parameters.AddWithValue("@SellingPrice", SubTotal);
//                                cmd.Parameters.AddWithValue("@Discount", 0);
//                                cmd.Parameters.AddWithValue("@ItemDiscount", 0);
//                                cmd.Parameters.AddWithValue("@ItemDiscountType", 0);
//                                cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                                cmd.Parameters.AddWithValue("@ProductGroup", clsProductDetails.ProductGroupName);
//                                cmd.Parameters.AddWithValue("@ProductSubGroup", clsProductDetails.ProductSubGroupCode);
//                                cmd.Parameters.AddWithValue("@TransactionItemStatus", 0);
//                                cmd.Parameters.AddWithValue("@DiscountCode", "");
//                                cmd.Parameters.AddWithValue("@DiscountRemarks", "");
//                                cmd.Parameters.AddWithValue("@ProductPackageID", clsProductDetails.PackageID);
//                                cmd.Parameters.AddWithValue("@PackageQuantity", 1);
//                                cmd.Parameters.AddWithValue("@DataSource", "IC_NO_ITN");
//                                cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                                cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);
//                            }

//                            //#endregion

//                            //#region Insert to tblCreditCardPayment

//                            SQL = "SELECT * FROM tblCreditCardPayment WHERE TransactionNo=@TransactionNo AND TerminalNo=@TerminalNo LIMIT 1;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
//                            cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);

//                            cmd.CommandText = SQL;
//                            dtT = new System.Data.DataTable("tblTemp");
//                            clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                            if (dtT.Rows.Count == 0)
//                            {

//                                SQL = "INSERT INTO tblCreditCardPayment(TransactionID, Amount, CardTypeID, CardTypeCode, CardTypeName, CardNo, CardHolder, ValidityDates, " +
//                                            "Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID, AdditionalCharge, " +
//                                            "ContactID, GuarantorID, TransactionDate, CashierName)VALUES(";

//                                SQL += "@TransactionID, @Amount, @CardTypeID, @CardTypeCode, @CardTypeName, @CardNo, @CardHolder, @ValidityDates, " +
//                                            "@Remarks, @TransactionNo, @CreatedOn, @TerminalNo, @BranchID, @AdditionalCharge, " +
//                                            "@ContactID, @GuarantorID, @TransactionDate, @CashierName)";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                                cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                                cmd.Parameters.AddWithValue("@CardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
//                                cmd.Parameters.AddWithValue("@CardTypeCode", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeCode);
//                                cmd.Parameters.AddWithValue("@CardTypeName", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeName);
//                                cmd.Parameters.AddWithValue("@CardNo", clsContactDetails.CreditDetails.CreditCardNo);
//                                cmd.Parameters.AddWithValue("@CardHolder", clsContactDetails.ContactName);
//                                cmd.Parameters.AddWithValue("@ValidityDates", clsContactDetails.CreditDetails.ExpiryDate.ToString("MMddyy"));
//                                cmd.Parameters.AddWithValue("@Remarks", "");
//                                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                                cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                                cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                                cmd.Parameters.AddWithValue("@BranchID", 1);
//                                cmd.Parameters.AddWithValue("@AdditionalCharge", 0);
//                                cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
//                                cmd.Parameters.AddWithValue("@GuarantorID", clsContactDetails.CreditDetails.GuarantorID);
//                                cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
//                                cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);
//                            }

//                            //#endregion

//                            //#region Insert to tblCreditPayment

//                            SQL = "SELECT * FROM tblCreditPayment WHERE TransactionNo=@TransactionNo AND TerminalNo = @TerminalNo LIMIT 1;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
//                            cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);

//                            cmd.CommandText = SQL;
//                            dtT = new System.Data.DataTable("tblTemp");
//                            clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                            if (dtT.Rows.Count == 0)
//                            {
//                                CreditCardPaymentDetails[] clsCreditCardPaymentDetails = clsCreditCardPayments.Details(1, strTerminalNo, clsSalesTransactionDetails.TransactionID);

//                                SQL = "INSERT INTO tblCreditPayment(TransactionID, Amount, ContactID, Remarks, AmountPaid, " +
//                                            "TransactionNo, CreditDate, CreditBefore, CreditAfter, " +
//                                            "CreditReason, TerminalNo, CashierName, AmountPaidCuttOffMonth, " +
//                                            "CreatedOn, BranchID, CreditCardPaymentID, CreditCardTypeID, CreditReasonID)VALUES(";

//                                SQL += "@TransactionID, @Amount, @ContactID, @Remarks, @AmountPaid, " +
//                                            "@TransactionNo, @CreditDate, @CreditBefore, @CreditAfter, " +
//                                            "@CreditReason, @TerminalNo, @CashierName, @AmountPaidCuttOffMonth, " +
//                                            "@CreatedOn, @BranchID, @CreditCardPaymentID, @CreditCardTypeID, @CreditReasonID)";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                                cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                                cmd.Parameters.AddWithValue("@ContactID", clsContactDetails.ContactID);
//                                cmd.Parameters.AddWithValue("@Remarks", "Deliquent purchases before 2014May ");
//                                cmd.Parameters.AddWithValue("@AmountPaid", 0);
//                                cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                                cmd.Parameters.AddWithValue("@CreditDate", CreatedOn);
//                                cmd.Parameters.AddWithValue("@CreditBefore", 0);
//                                cmd.Parameters.AddWithValue("@CreditAfter", SubTotal);
//                                cmd.Parameters.AddWithValue("@CreditReason", "Deliquent purchases before 2014May ");
//                                cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                                cmd.Parameters.AddWithValue("@CashierName", clsSalesTransactionDetails.CashierName);
//                                cmd.Parameters.AddWithValue("@AmountPaidCuttOffMonth", 0);
//                                cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                                cmd.Parameters.AddWithValue("@BranchID", 1);
//                                cmd.Parameters.AddWithValue("@CreditCardPaymentID", clsCreditCardPaymentDetails[0].CreditCardPaymentID);
//                                cmd.Parameters.AddWithValue("@CreditCardTypeID", clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID);
//                                cmd.Parameters.AddWithValue("@CreditReasonID", 0);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);
//                            }

//                            //#endregion

//                            Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases. " + clsSalesTransactionDetails.TransactionNo);
//                            iTheSame++;
//                        }
//                    }

//                    iCtr++;
//                }
//                clsLocalConnection.CommitAndDispose();
//                Console.WriteLine("Credit has been adjusted. Press any key to continue.");
//                Console.ReadLine();
//                //Console.WriteLine("done and committed");
//                //Console.WriteLine("Total: " + iCount.ToString());
//                //Console.WriteLine("Already in db: " + iTheSame.ToString());
//                //Console.WriteLine("Inserted: " + iNotTheSame.ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.ToString());
//            }
//        }
//    }
//}
