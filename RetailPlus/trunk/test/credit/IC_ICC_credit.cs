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
//    /// Summary description for IC_ITN.
//    /// </summary>
//    public class IC_ITN
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 1000;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                // get all the contacts with credit, 
//                // all purchases of zero(0) are already paid above
//                string SQL = "SELECT cntct.ContactID, cntct.ContactCode, cntct.Credit FROM tblContacts cntct " +
//                       "WHERE ContactID IN (SELECT CustomerID FROM tblContactCreditCardInfo WHERE LEFT(CreditCardNo,6) = '888880') LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;
//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                ProductDetails clsProductDetails = new Products(clsLocalConnection.Connection, clsLocalConnection.Transaction).DetailsByCode(1, "IC IMPORTED TRX");
//                SalesTransactions clsSalesTransactions = new SalesTransactions(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                CreditCardPayments clsCreditCardPayments = new CreditCardPayments(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                ContactDetails clsContactDetails;
//                Creditors clsCreditors = new Creditors(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 ContactID = Int64.Parse(dr["ContactID"].ToString());
//                    string ContactCode = dr["ContactCode"].ToString();
//                    decimal decCredit = decimal.Parse(dr["Credit"].ToString());

//                    clsContactDetails = clsContacts.Details(ContactID);

//                    bool boRetValue = clsCreditors.AutoAdjustCredit(clsContactDetails, decCredit);

//                    if (boRetValue)
//                    {
//                        Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " purchases updated.");
//                        iTheSame++; 
//                    }
//                    else
//                    {
//                        Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases. ");
//                        iNotTheSame++;
//                    }

//                    //SQL = "SELECT * FROM tblCreditPayment WHERE ContactID=@ContactID ORDER BY CreditDate DESC;";
//                    //cmd.Parameters.Clear();
//                    //cmd.Parameters.AddWithValue("@ContactID", ContactID);

//                    //cmd.CommandText = SQL;
//                    //dtT = new System.Data.DataTable("tblTemp");
//                    //clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);
//                    //Int32 dtTRowsCount = dtT.Rows.Count;

//                    //if (dtTRowsCount == 0)
//                    //{
//                    //    clsCreditors.AutoAdjustCredit(clsContactDetails, decCredit);

//                    //    Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases. ");
//                    //    iTheSame++;
//                    //}
//                    //else
//                    //{
//                    //    clsCreditors.AutoAdjustCredit(clsContactDetails, decCredit);

//                    //    Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " purchases updated.");
//                    //    iNotTheSame++;
//                    //}
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
