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
//    /// Summary description for IC_ICC.
//    /// </summary>
//    public class IC_ICC
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

//                string SQL = "SELECT DISTINCT CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) AS ContactCode, ICDESC AS ContactName," +
//                                    "CONCAT(ICADR1,ICADR2) AS Address, ICLINE AS BusinessName, ICPONE AS TelephoneNo, " +
//                                    "CONCAT('gurkey:',GURKEY,'; status:',ICSTAT,'; ICBEGB:',ICBEGB,'; ICPURC',ICPURC) AS Remarks, " +
//                                    "ICLINE CreditLimit, ICSALE Credit,  " +
//                                    "ICBEGB, ICPURC, ICAF15, ICAF30, ICAF45, ICAF60, ICCBAL, ICCHRG, ICCDUE, ICPAYM, ICPAYC, ICENDB, ICSALE " +
//                             "FROM IC_ICC LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;


//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditBills clsCreditBills = new CreditBills(clsLocalConnection.Connection, clsLocalConnection.Transaction);

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

//                    ContactDetails Member = clsContacts.Details(ContactCode);

//                    if (Member.ContactID != 0)
//                    {
//                        SQL = "UPDATE tblContacts SET " +
//                                    "Credit = @Credit, CreditLimit = @CreditLimit " +
//                                    "WHERE ContactID = @ContactID;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.Parameters.AddWithValue("@Credit", Credit);
//                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("icc[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " already in the database.");
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
//                        cmd.Parameters.AddWithValue("@ContactGroupID", 4); // 'IC Members'
//                        cmd.Parameters.AddWithValue("@ModeOfTerms", 0);
//                        cmd.Parameters.AddWithValue("@Terms", 0);
//                        cmd.Parameters.AddWithValue("@Address", Address);
//                        cmd.Parameters.AddWithValue("@BusinessName", BusinessName);
//                        cmd.Parameters.AddWithValue("@TelephoneNo", TelephoneNo);
//                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
//                        cmd.Parameters.AddWithValue("@Debit", 0);
//                        cmd.Parameters.AddWithValue("@Credit", Credit);
//                        cmd.Parameters.AddWithValue("@CreditLimit", CreditLimit);
//                        cmd.Parameters.AddWithValue("@IsCreditAllowed", 1);
//                        cmd.Parameters.AddWithValue("@DepartmentID", 1);
//                        cmd.Parameters.AddWithValue("@PositionID", 1);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("icc[" + iCtr.ToString() + "/" + iCount.ToString() + "]ContactCode: " + ContactCode + " has been inserted.");
//                        iNotTheSame++;
//                    }

//                Step2:
//                    CreditBillDetails clsCreditBillDetails = clsCreditBills.Details(CreditType.Group, new DateTime(2014, 11, 20), Member.CreditDetails.CardTypeDetails.CardTypeID);

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
//                                "SELECT '2014-09-20' CreditPurcStartDateToProcess, '2014-11-19' CreditPurcEndDateToProcess, " +
//                                                            "'2014-11-20' BillingDate, '2014-11-30' CreditCutOffDate, '2014-11-30' CreditPaymentDueDate, " +
//                                                            "CardTypeID, CardTypeCode, CreditCardType, WithGuarantor, " +
//                                                            "CreditUseLastDayCutOffDate, CreditFinanceCharge, " +
//                                                            "CreditMinimumPercentageDue, CreditMinimumAmountDue, CreditLatePenaltyCharge, " +
//                                                            "CreditFinanceCharge15th, CreditMinimumPercentageDue15th, " +
//                                                            "CreditMinimumAmountDue15th, CreditLatePenaltyCharge15th, " +
//                                                            "NOW() CreatedOn, 1 CreatedByID, 'SysCWithGBiller' CreatedByName " +
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

//                    decimal ICBEGB = decimal.TryParse(dr["ICBEGB"].ToString(), out ICBEGB) ? ICBEGB : 0;
//                    decimal ICPURC = decimal.TryParse(dr["ICPURC"].ToString(), out ICPURC) ? ICPURC : 0;
//                    decimal ICAF15 = decimal.TryParse(dr["ICAF15"].ToString(), out ICAF15) ? ICAF15 : 0;
//                    decimal ICAF30 = decimal.TryParse(dr["ICAF30"].ToString(), out ICAF30) ? ICAF30 : 0;
//                    decimal ICAF45 = decimal.TryParse(dr["ICAF45"].ToString(), out ICAF45) ? ICAF45 : 0;
//                    decimal ICAF60 = decimal.TryParse(dr["ICAF60"].ToString(), out ICAF60) ? ICAF60 : 0;
//                    decimal ICCBAL = decimal.TryParse(dr["ICCBAL"].ToString(), out ICCBAL) ? ICCBAL : 0;
//                    decimal ICCHRG = decimal.TryParse(dr["ICCHRG"].ToString(), out ICCHRG) ? ICCHRG : 0;
//                    decimal ICCDUE = decimal.TryParse(dr["ICCDUE"].ToString(), out ICCDUE) ? ICCDUE : 0;
//                    decimal ICPAYM = decimal.TryParse(dr["ICPAYM"].ToString(), out ICPAYM) ? ICPAYM : 0;
//                    decimal ICPAYC = decimal.TryParse(dr["ICPAYC"].ToString(), out ICPAYC) ? ICPAYC : 0;
//                    decimal ICENDB = decimal.TryParse(dr["ICENDB"].ToString(), out ICENDB) ? ICENDB : 0;
//                    decimal ICSALE = decimal.TryParse(dr["ICSALE"].ToString(), out ICSALE) ? ICSALE : 0;

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

//                        Console.WriteLine("    creditbillheader inserted.");
//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CreditBillID", clsCreditBillDetails.CreditBillID);
//                        cmd.Parameters.AddWithValue("@ContactID", Member.ContactID);
//                        cmd.Parameters.AddWithValue("@GuarantorID", Member.CreditDetails.GuarantorID);
//                        cmd.Parameters.AddWithValue("@CreditLimit", Member.CreditLimit);
//                        cmd.Parameters.AddWithValue("@RunningCreditAmt", ICCBAL);
//                        cmd.Parameters.AddWithValue("@CurrMonthCreditAmt", ICAF15);
//                        cmd.Parameters.AddWithValue("@CurrMonthAmountPaid", ICPAYM);
//                        cmd.Parameters.AddWithValue("@BillingDate", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@BillingFile", "");
//                        cmd.Parameters.AddWithValue("@TotalBillCharges", ICCHRG);
//                        cmd.Parameters.AddWithValue("@CurrentDueAmount", ICAF15 + ICCHRG + ICCBAL);
//                        cmd.Parameters.AddWithValue("@MinimumAmountDue", ICAF15);
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrentDueAmount", ICCDUE);
//                        cmd.Parameters.AddWithValue("@Prev1MoMinimumAmountDue", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrMonthAmountPaid", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev2MoCurrentDueAmount", 0); //not needed
//                        cmd.Parameters.AddWithValue("@CurrentPurchaseAmt", ICPURC);
//                        cmd.Parameters.AddWithValue("@BeginningBalance", ICBEGB);
//                        cmd.Parameters.AddWithValue("@EndingBalance", ICENDB);
//                        cmd.Parameters.AddWithValue("@CreatedOn", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@CreatedByID", 1);
//                        cmd.Parameters.AddWithValue("@CreatedByName", "IC SysUser");
//                        cmd.Parameters.AddWithValue("@IsBillPrinted", 1);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);
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

//                        Console.WriteLine("    creditbillheader updated.");
//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CreditBillHeaderID", CreditBillHeaderID);
//                        cmd.Parameters.AddWithValue("@CreditLimit", Member.CreditLimit);
//                        cmd.Parameters.AddWithValue("@RunningCreditAmt", ICCBAL);
//                        cmd.Parameters.AddWithValue("@CurrMonthCreditAmt", ICAF15);
//                        cmd.Parameters.AddWithValue("@CurrMonthAmountPaid", ICPAYM);
//                        cmd.Parameters.AddWithValue("@BillingDate", clsCreditBillDetails.BillingDate);
//                        cmd.Parameters.AddWithValue("@BillingFile", "");
//                        cmd.Parameters.AddWithValue("@TotalBillCharges", ICCHRG);
//                        cmd.Parameters.AddWithValue("@CurrentDueAmount", ICAF15 + ICCHRG + ICCBAL);
//                        cmd.Parameters.AddWithValue("@MinimumAmountDue", ICAF15);
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrentDueAmount", ICCDUE);
//                        cmd.Parameters.AddWithValue("@Prev1MoMinimumAmountDue", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev1MoCurrMonthAmountPaid", 0); //not needed
//                        cmd.Parameters.AddWithValue("@Prev2MoCurrentDueAmount", 0); //not needed
//                        cmd.Parameters.AddWithValue("@CurrentPurchaseAmt", ICPURC);
//                        cmd.Parameters.AddWithValue("@BeginningBalance", ICBEGB);
//                        cmd.Parameters.AddWithValue("@EndingBalance", ICENDB);

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
