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
//    /// Summary description for CC_B2S.
//    /// </summary>
//    public class CC_B2S
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

//                string SQL = "SELECT DISTINCT CONCAT('888880', LPAD(CC_B2S.ICRKEY,7,0)) AS ContactCode, ICDESC AS ContactName," +
//                                    "CONCAT(ICADR1,ICADR2) AS Address, GURKEY AS BusinessName, ICPONE AS TelephoneNo, " +
//                                    "CONCAT('gurkey:',GURKEY,'; status:',ICSTAT,'; ICBEGB:') AS Remarks, " +
//                                    "ICLINE CreditLimit, ICSALE Credit, " +
//                                    "OVER90, OVER60, OVER30, CURENT, PAYMNT, CHGPAY, CHGOVR, TOTDUE, MINDUE " +
//                             "FROM CC_B2S LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditBills clsCreditBills = new CreditBills(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dtAmt = new System.Data.DataTable("tblTemp");
//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");
//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    string ContactCode = dr["ContactCode"].ToString();
//                    string ContactName = dr["ContactName"].ToString();
//                    string Address = dr["Address"].ToString();
//                    string BusinessName = dr["BusinessName"].ToString();
//                    string TelephoneNo = dr["TelephoneNo"].ToString();
//                    string Remarks = dr["Remarks"].ToString();
//                    decimal Credit = decimal.Parse(dr["Credit"].ToString());
//                    decimal CreditLimit = decimal.Parse(dr["CreditLimit"].ToString());
//                    decimal TOTDUE = decimal.TryParse(dr["TOTDUE"].ToString(), out TOTDUE) ? TOTDUE : 0;

//                    ContactDetails Member = clsContacts.Details(ContactCode);

//                    if (Member.ContactID != 0)
//                    {
//                        // get the purchases after Dec 1, 2014
//                        SQL = "SELECT IFNULL(SUM(Amount),0) Amount FROM tblCreditPayment WHERE ContactID = @ContactID AND CreditDate >= '2014-12-01';";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.CommandText = SQL;
//                        dtAmt = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtAmt);
//                        decimal decAdditionalPurchases = 0;
//                        foreach (System.Data.DataRow drPOAmt in dtAmt.Rows)
//                        {
//                            decAdditionalPurchases = decimal.Parse(drPOAmt["Amount"].ToString());
//                        }

//                        // get the payments after Dec 1, 2014
//                        SQL = "SELECT IFNULL(SUM(SubTotal),0) Amount FROM tblTransactions WHERE CustomerID=@ContactID AND TransactionStatus=7 AND TransactionDate >= '2014-12-01';";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.CommandText = SQL;
//                        dtAmt = new System.Data.DataTable("tblTemp");
//                        clsLocalConnection.MySqlDataAdapterFill(cmd, dtAmt);
//                        decimal decAdditionalPayments = 0;
//                        foreach (System.Data.DataRow drPaidAmt in dtAmt.Rows)
//                        {
//                            decAdditionalPayments = decimal.Parse(drPaidAmt["Amount"].ToString());
//                        }

//                        // set the credit limit
//                        SQL = "UPDATE tblContacts SET " +
//                                    "Credit = @Credit, CreditLimit = @CreditLimit " +
//                                    "WHERE ContactID = @ContactID;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.Parameters.AddWithValue("@Credit", TOTDUE + decAdditionalPurchases - decAdditionalPayments);
//                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " already in the database.");
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        SQL = "INSERT INTO tblContacts (ContactCode ,ContactName ,ContactGroupID ,ModeOfTerms ,Terms " +
//                                    ",Address ,BusinessName ,TelephoneNo ,Remarks ,Debit ,Credit " +
//                                    ",CreditLimit ,IsCreditAllowed ,DateCreated ,DepartmentID ,PositionID)VALUES(" +
//                                    "@ContactCode ,@ContactName ,@ContactGroupID ,@ModeOfTerms ,@Terms " +
//                                    ",@Address ,@BusinessName ,@TelephoneNo ,@Remarks ,@Debit ,@Credit  " +
//                                    ",@CreditLimit ,@IsCreditAllowed ,NOW() ,@DepartmentID ,@PositionID)";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ContactCode", ContactCode);
//                        cmd.Parameters.AddWithValue("@ContactName", ContactName);
//                        cmd.Parameters.AddWithValue("@ContactGroupID", 5); // 'CC Members'
//                        cmd.Parameters.AddWithValue("@ModeOfTerms", 0);
//                        cmd.Parameters.AddWithValue("@Terms", 0);
//                        cmd.Parameters.AddWithValue("@Address", Address);
//                        cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
//                        cmd.Parameters.AddWithValue("@TelephoneNo", TelephoneNo);
//                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
//                        cmd.Parameters.AddWithValue("@Debit", 0);
//                        cmd.Parameters.AddWithValue("@Credit", 0);
//                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
//                        cmd.Parameters.AddWithValue("@IsCreditAllowed", 1);
//                        cmd.Parameters.AddWithValue("@DepartmentID", 1);
//                        cmd.Parameters.AddWithValue("@PositionID", 1);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " has been inserted.");
//                        iNotTheSame++;
//                    }

//                Step2:
//                    CreditBillDetails clsCreditBillDetails = clsCreditBills.Details(CreditType.Individual, new DateTime(2014, 11, 10), Member.CreditDetails.CardTypeDetails.CardTypeID);

//                    //insert a new if none
//                    if (clsCreditBillDetails.CreditBillID == 0)
//                    {
//                        SQL = "INSERT INTO tblCreditBills(CreditPurcStartDateToProcess, CreditPurcEndDateToProcess, " +
//                                                            "BillingDate, CreditCutOffDate, CreditPaymentDueDate, " +
//                                                            "CreditCardTypeID, CardTypeCode, CreditCardType, WithGuarantor, " +
//                                                            "CreditUseLastDayCutOffDate, CreditFinanceCharge, " +
//                                                            "CreditMinimumPercentageDue, CreditMinimumAmountDue, CreditLatePenaltyCharge, " +
//                                                            "CreditFinanceCharge15th, CreditMinimumPercentageDue15th, " +
//                                                            "CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th, " +
//                                                            "CreatedOn, CreatedByID, CreatedByName) " +
//                                "SELECT '2014-10-10' CreditPurcStartDateToProcess, '2014-11-09' CreditPurcEndDateToProcess, " +
//                                                            "'2014-11-10' BillingDate, '2014-10-30' CreditCutOffDate, '2014-10-30' CreditPaymentDueDate, " +
//                                                            "CardTypeID, CardTypeCode, CreditCardType, WithGuarantor, " +
//                                                            "CreditUseLastDayCutOffDate, CreditFinanceCharge, " +
//                                                            "CreditMinimumPercentageDue, CreditMinimumAmountDue, CreditLatePenaltyCharge, " +
//                                                            "CreditFinanceCharge15th, CreditMinimumPercentageDue15th, " +
//                                                            "CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th, " +
//                                                            "NOW() CreatedOn, 1 CreatedByID, 'SysCWoutGBiller' CreatedByName " +
//                                "FROM tblCardTypes WHERE CardTypeID=@CardTypeID;";
//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CardTypeID", Member.CreditDetails.CardTypeDetails.CardTypeID);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        goto Step2;
//                    }

//                    //insert into CreditBillHeader as the start
//                    SQL = "SELECT CreditBillHeaderID FROM tblCreditBillHeader WHERE ContactID=@ContactID LIMIT 1;";
//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    decimal OVER90 = decimal.TryParse(dr["OVER90"].ToString(), out OVER90) ? OVER90 : 0;
//                    decimal OVER60 = decimal.TryParse(dr["OVER60"].ToString(), out OVER60) ? OVER60 : 0;
//                    decimal OVER30 = decimal.TryParse(dr["OVER30"].ToString(), out OVER30) ? OVER30 : 0;
//                    decimal CURENT = decimal.TryParse(dr["CURENT"].ToString(), out CURENT) ? CURENT : 0;
//                    decimal PAYMNT = decimal.TryParse(dr["PAYMNT"].ToString(), out PAYMNT) ? PAYMNT : 0;
//                    decimal CHGPAY = decimal.TryParse(dr["CHGPAY"].ToString(), out CHGPAY) ? CHGPAY : 0;
//                    decimal CHGOVR = decimal.TryParse(dr["CHGOVR"].ToString(), out CHGOVR) ? CHGOVR : 0;
//                    TOTDUE = decimal.TryParse(dr["TOTDUE"].ToString(), out TOTDUE) ? TOTDUE : 0;
//                    decimal MINDUE = decimal.TryParse(dr["MINDUE"].ToString(), out MINDUE) ? MINDUE : 0;

//                    if (dtT.Rows.Count == 0)
//                    {
//                        SQL = "INSERT INTO tblCreditBillHeader(CreditBillID, ContactID, GuarantorID, CreditLimit, RunningCreditAmt, " +
//                                                            "CurrMonthCreditAmt, CurrMonthAmountPaid, BillingDate, BillingFile, TotalBillCharges, " +
//                                                            "CurrentDueAmount, MinimumAmountDue, Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue, " +
//                                                            "Prev1MoCurrMonthAmountPaid, Prev2MoCurrentDueAmount, CurrentPurchaseAmt, " +
//                                                            "BeginningBalance, EndingBalance,  " +
//                                                            "CreatedOn, CreatedByID, CreatedByName, IsBillPrinted)VALUES(";
//                        SQL += "@CreditBillID, @ContactID, @GuarantorID, @CreditLimit, @RunningCreditAmt, " +
//                                                            "@CurrMonthCreditAmt, @CurrMonthAmountPaid, @BillingDate, @BillingFile, @TotalBillCharges, " +
//                                                            "@CurrentDueAmount, @MinimumAmountDue, @Prev1MoCurrentDueAmount, @Prev1MoMinimumAmountDue, " +
//                                                            "@Prev1MoCurrMonthAmountPaid, @Prev2MoCurrentDueAmount, @CurrentPurchaseAmt, " +
//                                                            "@BeginningBalance, @EndingBalance,  " +
//                                                            "@CreatedOn, @CreatedByID, @CreatedByName, @IsBillPrinted)";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CreditBillID", clsCreditBillDetails.CreditBillID);
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.Parameters.AddWithValue("@GuarantorID", Member.CreditDetails.GuarantorID);
//                        cmd.Parameters.AddWithValue("@CreditLimit", Member.CreditLimit);
//                        cmd.Parameters.AddWithValue("@RunningCreditAmt", 0); //not set
//                        cmd.Parameters.AddWithValue("@CurrMonthCreditAmt", 0);
//                        cmd.Parameters.AddWithValue("@CurrMonthAmountPaid", -PAYMNT); //not set
//                        cmd.Parameters.AddWithValue("@BillingDate", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@BillingFile", "");
//                        cmd.Parameters.AddWithValue("@TotalBillCharges", CHGPAY + CHGOVR);
//                        cmd.Parameters.AddWithValue("@CurrentDueAmount", TOTDUE);
//                        cmd.Parameters.AddWithValue("@MinimumAmountDue", MINDUE);
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrentDueAmount", OVER30);
//                        cmd.Parameters.AddWithValue("@Prev1MoMinimumAmountDue", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrMonthAmountPaid", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev2MoCurrentDueAmount", OVER90 + OVER60); //not needed
//                        cmd.Parameters.AddWithValue("@CurrentPurchaseAmt", CURENT);
//                        cmd.Parameters.AddWithValue("@BeginningBalance", 0);
//                        cmd.Parameters.AddWithValue("@EndingBalance", 0);
//                        cmd.Parameters.AddWithValue("@CreatedOn", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@CreatedByID", 1);
//                        cmd.Parameters.AddWithValue("@CreatedByName", "IC SysUser");
//                        cmd.Parameters.AddWithValue("@IsBillPrinted", 1);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);
//                        Console.WriteLine("    creditbillheader inserted.");
//                    }
//                    else
//                    {
//                        Int64 CreditBillHeaderID = Int64.Parse(dtT.Rows[0]["CreditBillHeaderID"].ToString());

//                        SQL = "UPDATE tblCreditBillHeader SET CreditLimit=@CreditLimit, RunningCreditAmt=@RunningCreditAmt, " +
//                                                            "CurrMonthCreditAmt=@CurrMonthCreditAmt, CurrMonthAmountPaid=@CurrMonthAmountPaid, BillingDate=@BillingDate, BillingFile=@BillingFile, TotalBillCharges=@TotalBillCharges, " +
//                                                            "CurrentDueAmount=@CurrentDueAmount, MinimumAmountDue=@MinimumAmountDue, Prev1MoCurrentDueAmount=@Prev1MoCurrentDueAmount, Prev1MoMinimumAmountDue=@Prev1MoMinimumAmountDue, " +
//                                                            "Prev1MoCurrMonthAmountPaid=@Prev1MoCurrMonthAmountPaid, Prev2MoCurrentDueAmount=@Prev2MoCurrentDueAmount, CurrentPurchaseAmt=@CurrentPurchaseAmt, " +
//                                                            "BeginningBalance=@BeginningBalance, EndingBalance=@EndingBalance  " +
//                                "WHERE CreditBillHeaderID=@CreditBillHeaderID ";

                        
//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);
//                        cmd.Parameters.AddWithValue("@CreditBillID", clsCreditBillDetails.CreditBillID);
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.Parameters.AddWithValue("@GuarantorID", Member.CreditDetails.GuarantorID);
//                        cmd.Parameters.AddWithValue("@CreditLimit", Member.CreditLimit);
//                        cmd.Parameters.AddWithValue("@RunningCreditAmt", 0); //not set
//                        cmd.Parameters.AddWithValue("@CurrMonthCreditAmt", 0);
//                        cmd.Parameters.AddWithValue("@CurrMonthAmountPaid", -PAYMNT);
//                        cmd.Parameters.AddWithValue("@BillingDate", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@BillingFile", "");
//                        cmd.Parameters.AddWithValue("@TotalBillCharges", CHGPAY + CHGOVR);
//                        cmd.Parameters.AddWithValue("@CurrentDueAmount", TOTDUE);
//                        cmd.Parameters.AddWithValue("@MinimumAmountDue", MINDUE);
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrentDueAmount", OVER30);
//                        cmd.Parameters.AddWithValue("@Prev1MoMinimumAmountDue", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrMonthAmountPaid", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev2MoCurrentDueAmount", OVER90 + OVER60);
//                        cmd.Parameters.AddWithValue("@CurrentPurchaseAmt", CURENT);
//                        cmd.Parameters.AddWithValue("@BeginningBalance", 0);
//                        cmd.Parameters.AddWithValue("@EndingBalance", 0);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);
//                        Console.WriteLine("    creditbillheader updated.");
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
