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
//    /// Summary description for CC_B4S.
//    /// </summary>
//    public class CC_B4S
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 100;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT LPAD(CONCAT(CC_B4S_ID,TRACER),14,'0') TransactionNo, STR_TO_DATE(TRDATE, '%d/%m/%Y') TransactionDate, " +
//                                "(CONVERT(TRAMNT, DECIMAL(18,3)) + CONVERT(PAID_P, DECIMAL(18,3))) SubTotal, CONVERT(PAID_P, DECIMAL(18,3)) Penalty, " +
//                                "CONCAT('888880', LPAD(CC_B4S.ICRKEY,7,0)) ContactCode, CC_B4S_ID, TRACER TracerNo, ICDESC, TREFNO " +
//                             "FROM CC_B4S WHERE isProcessed = 0 LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                ProductDetails clsProductDetails = new Products(clsLocalConnection.Connection, clsLocalConnection.Transaction).DetailsByCode(1, "CREDIT PAYMENT");
//                SalesTransactions clsSalesTransactions = new SalesTransactions(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");
//                string strTerminalNo = "9997";

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    string TransactionNo = dr["TransactionNo"].ToString();
//                    Int64 CC_B4S_ID = Int64.Parse(dr["CC_B4S_ID"].ToString());

//                    string ORNo = dr["TransactionNo"].ToString();
//                    string TracerNo = dr["TracerNo"].ToString();
//                    string ICDESC = dr["ICDESC"].ToString();
//                    string TREFNO = dr["TREFNO"].ToString();

//                    DateTime TransactionDate = DateTime.Parse(dr["TransactionDate"].ToString());
//                    DateTime DateClosed = DateTime.Parse(dr["TransactionDate"].ToString());
//                    DateTime CreatedOn = DateTime.Parse(dr["TransactionDate"].ToString());
//                    DateTime ContactCheckInDate = DateTime.Parse(dr["TransactionDate"].ToString());

//                    decimal Penalty = decimal.Parse(dr["Penalty"].ToString());
//                    decimal SubTotal = decimal.Parse(dr["SubTotal"].ToString());

//                    string ContactCode = dr["ContactCode"].ToString();
//                    ContactDetails clsContactDetails = clsContacts.Details(ContactCode);

//                    if (clsContactDetails.ContactID == 0)
//                    {
//                        Console.WriteLine("b4s[" + iCtr.ToString() + "/" + iCount.ToString() + "]TransactionNo: " + TransactionNo + " has invalid customercode: " + ContactCode + ".");
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        SQL = "SELECT * FROM tblTransactions WHERE BranchID=1 AND TerminalNo = @TerminalNo AND TransactionNo = @TransactionNo LIMIT 1;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                        cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);

//                        cmd.CommandText = SQL;
//                        dtT = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                        if (dtT.Rows.Count > 0)
//                        {
//                            Console.WriteLine("b4s[" + iCtr.ToString() + "/" + iCount.ToString() + "]TransactionNo: " + TransactionNo + " already in the database.");
//                            iTheSame++;
//                        }
//                        else
//                        {
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
//                            cmd.Parameters.AddWithValue("@TransactionStatus", 7); //creditpayment
//                            cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
//                            cmd.Parameters.AddWithValue("@AmountPaid", SubTotal);
//                            cmd.Parameters.AddWithValue("@CashPayment", SubTotal);
//                            cmd.Parameters.AddWithValue("@ChequePayment", 0);
//                            cmd.Parameters.AddWithValue("@CreditCardPayment", 0);
//                            cmd.Parameters.AddWithValue("@CreditPayment", 0);
//                            cmd.Parameters.AddWithValue("@DateClosed", DateClosed);
//                            cmd.Parameters.AddWithValue("@PaymentType", 4); //cash
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
//                            cmd.Parameters.AddWithValue("@DataSource", "CC_B4S");
//                            cmd.Parameters.AddWithValue("@CustomerGroupName", "CC Members");
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@ORNo", ORNo);
//                            cmd.Parameters.AddWithValue("@NetSales", SubTotal);
//                            cmd.Parameters.AddWithValue("@ChargeType", 0);
//                            cmd.Parameters.AddWithValue("@ItemSold", 1);
//                            cmd.Parameters.AddWithValue("@QuantitySold", 1);
//                            cmd.Parameters.AddWithValue("@ContactCheckInDate", ContactCheckInDate);
//                            cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            //#endregion

//                            Console.WriteLine("b4s[" + iCtr.ToString() + "/" + iCount.ToString() + "]TransactionNo: " + TransactionNo + " has been inserted.");
//                            iNotTheSame++;
//                        }

//                        SalesTransactionDetails clsSalesTransactionDetails = clsSalesTransactions.Details(TransactionNo, strTerminalNo, 1);

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
//                            cmd.Parameters.AddWithValue("@DataSource", "CC_B4S");
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@GrossSales", SubTotal);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);
//                        }

//                        //#endw

//                        //#region Insert to tblCashPayment

//                        SQL = "SELECT * FROM tblCashPayment WHERE TransactionNo=@TransactionNo AND TerminalNo=@TerminalNo LIMIT 1;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@TransactionNo", clsSalesTransactionDetails.TransactionNo);
//                        cmd.Parameters.AddWithValue("@TerminalNo", clsSalesTransactionDetails.TerminalNo);

//                        cmd.CommandText = SQL;
//                        dtT = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                        if (dtT.Rows.Count == 0)
//                        {

//                            SQL = "INSERT INTO tblCashPayment(TransactionID, Amount, Remarks, TransactionNo, CreatedOn, TerminalNo, BranchID)VALUES(";

//                            SQL += "@TransactionID, @Amount, @Remarks, @TransactionNo, @CreatedOn, @TerminalNo, @BranchID)";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@TransactionID", clsSalesTransactionDetails.TransactionID);
//                            cmd.Parameters.AddWithValue("@Amount", SubTotal);
//                            cmd.Parameters.AddWithValue("@Remarks", "ICC payment tracer:" + TracerNo + "; Penalty:" + Penalty.ToString("#,##0.#0") + "; TREFNO:" + TREFNO);
//                            cmd.Parameters.AddWithValue("@TransactionNo", TransactionNo);
//                            cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
//                            cmd.Parameters.AddWithValue("@TerminalNo", strTerminalNo);
//                            cmd.Parameters.AddWithValue("@BranchID", 1);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);
//                        }

//                        //#endregion

//                        SQL = "UPDATE CC_B4S SET isProcessed=1 WHERE CC_B4S_ID = @CC_B4S_ID;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CC_B4S_ID", CC_B4S_ID);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);
//                    }

//                    iCtr++;
//                }

//                clsLocalConnection.CommitAndDispose();
//                Console.WriteLine("done and committed");
//                Console.WriteLine("Total: " + iCount.ToString());
//                Console.WriteLine("Already in db: " + iTheSame.ToString());
//                Console.WriteLine("Inserted: " + iNotTheSame.ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.ToString());
//            }
//        }
//    }
//}
