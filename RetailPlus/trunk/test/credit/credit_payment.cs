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
//    /// Summary description for CreditPayment.
//    /// </summary>
//    public class CreditPayment
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 100;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("This will re-create pruchases and re-pay for none balance credit payment transactions." + Environment.NewLine);
//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;


//                string SQL = "select trx.transactionid, trx.terminalno, trx.customername, trx.transactiondate, " +
//                             "       trx.transactionno, trx.Customerid, trx.cashiername, " +
//                             "       trx.subtotal, pay.amountpaid " +
//                             "   from tbltransactions trx " +
//                             "   inner join ( " +
//                             "       select SUM(amount) amountpaid, transactionid, terminalno " +
//                             "       from tblcreditpaymentcash  " +
//                             "       group by transactionid, terminalno " +
//                             "   ) pay on trx.transactionid = pay.transactionid and trx.terminalno = pay.terminalno " +
//                             "   where trx.transactionstatus = 7 " +
//                             "   and trx.subtotal <> pay.amountpaid;";

//                cmd.CommandText = SQL;
//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                ContactDetails clsContactDetails;
//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditPayments clsCreditPayments = new CreditPayments(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditPaymentCash clsCreditPaymentCash = new CreditPaymentCash(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                Payment clsPayment = new Payment(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 ContactID = Int64.Parse(dr["Customerid"].ToString());
//                    decimal decsubtotal = decimal.Parse(dr["subtotal"].ToString());
//                    decimal decamountpaid = decimal.Parse(dr["amountpaid"].ToString());
//                    decimal decAmount = decsubtotal - decamountpaid;

//                    string terminalno = dr["terminalno"].ToString();
//                    Int64 transactionid = Int64.Parse(dr["transactionid"].ToString());
//                    string transactionno = dr["transactionno"].ToString();
//                    string cashiername = dr["cashiername"].ToString();
//                    DateTime transactiondate = DateTime.Parse(dr["transactiondate"].ToString());

//                    clsContactDetails = clsContacts.Details(ContactID);

//                    CreditPaymentDetails clsCreditPaymentDetails = new CreditPaymentDetails();
//                    clsCreditPaymentDetails.BranchDetails = new BranchDetails() { BranchID = 1 };
//                    clsCreditPaymentDetails.TerminalNo = terminalno;
//                    clsCreditPaymentDetails.TransactionID = transactionid;
//                    clsCreditPaymentDetails.TransactionNo = transactionno;
//                    clsCreditPaymentDetails.IsRefund = false;
//                    clsCreditPaymentDetails.TransactionDate = new DateTime(2014, 12, 1); //this should be Dec1,2014 so that it wont include in Billing
//                    clsCreditPaymentDetails.CashierName = cashiername;
//                    clsCreditPaymentDetails.Amount = decAmount;
//                    clsCreditPaymentDetails.CustomerDetails = clsContactDetails;
//                    clsCreditPaymentDetails.Remarks = "pay-no purchase: deposit " + transactiondate.ToString("yyyy-MM-dd HH:mm:ss");

//                    clsCreditPaymentDetails.CreditReason = "Deposit @ Ter#:" + terminalno + " Br#:1 trx: " + transactionno + " date:" + transactiondate.ToString("yyyy-MM-dd HH:mm");
//                    clsCreditPaymentDetails.CreditCardTypeID = clsContactDetails.CreditDetails.CardTypeDetails.CardTypeID;

//                    clsCreditPaymentDetails.CreditPaymentID = clsCreditPayments.Insert(clsCreditPaymentDetails);

//                    CreditPaymentCashDetails clsCreditPaymentCashDetails = new CreditPaymentCashDetails();
//                    clsCreditPaymentCashDetails.BranchDetails = new BranchDetails() { BranchID = 1 };
//                    clsCreditPaymentCashDetails.TerminalNo = terminalno;
//                    clsCreditPaymentCashDetails.CreditPaymentID = clsCreditPaymentDetails.CreditPaymentID;
//                    clsCreditPaymentCashDetails.CPRefBranchID = clsCreditPaymentDetails.BranchDetails.BranchID;
//                    clsCreditPaymentCashDetails.CPRefTerminalNo = clsCreditPaymentDetails.TerminalNo;
//                    clsCreditPaymentCashDetails.TransactionID = clsCreditPaymentDetails.TransactionID;
//                    clsCreditPaymentCashDetails.TransactionNo = clsCreditPaymentDetails.TransactionNo;
//                    clsCreditPaymentCashDetails.Amount = decAmount;
//                    clsCreditPaymentCashDetails.Remarks = "pay-no purchase: deposit";
//                    clsCreditPaymentCashDetails.CreatedOn = transactiondate;
//                    clsCreditPaymentCashDetails.LastModified = transactiondate;
                    
//                    clsCreditPaymentCash.Insert(clsCreditPaymentCashDetails);

//                    clsPayment.UpdateCredit(clsCreditPaymentDetails.BranchDetails.BranchID, clsCreditPaymentDetails.TerminalNo, ContactID, clsCreditPaymentDetails.CreditPaymentID, decAmount, clsCreditPaymentCashDetails.Remarks);

//                    Console.WriteLine("payadj[" + iCtr.ToString() + "/" + iCount.ToString() + "]Trxno: " + transactionno + " subtotal: " + decsubtotal.ToString("#,##0.#0") + " payment:" + decamountpaid.ToString("#,##0.#0"));

//                    iTheSame++;
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
