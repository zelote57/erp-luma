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
//                Int64 iLimit = 100;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                // pay all the purchases, we will do the reverse to arrive at equal credit in IC_ICC
//                string SQL = "UPDATE tblCreditPayment SET AmountPaid = Amount;";
//                cmd.CommandText = SQL;
//                clsLocalConnection.ExecuteNonQuery(cmd);

//                // get all the contacts with credit
//                SQL = "SELECT ContactID, ContactCode, Credit FROM tblContacts WHERE Credit > 0;";
//                cmd.CommandText = SQL;
//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 ContactID = Int64.Parse(dr["ContactID"].ToString());
//                    string ContactCode = dr["ContactCode"].ToString();
//                    decimal decCredit = decimal.Parse(dr["Credit"].ToString());

//                    ContactDetails clsContactDetails = clsContacts.Details(ContactID);

//                    SQL = "SELECT * FROM tblCreditPayment WHERE ContactID=@ContactID ORDER BY CreditDate DESC;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@ContactID", ContactID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (dtT.Rows.Count == 0)
//                    {
//                        // no purchases but with credit
//                        Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases.");
//                        iTheSame++;
//                        break;
//                    }
//                    else
//                    {
//                        foreach (System.Data.DataRow drCredit in dtT.Rows)
//                        {
//                            decimal decTrxCredit = decimal.Parse(drCredit["Amount"].ToString());
//                            Int64 CreditPaymentID = Int64.Parse(drCredit["CreditPaymentID"].ToString());

//                            if (decCredit > decTrxCredit)
//                            {
//                                SQL = "UPDATE tblCreditPayment SET AmountPaid=@AmountPaid WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

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
//                                SQL = "UPDATE tblCreditPayment SET AmountPaid=@AmountPaid WHERE ContactID=@ContactID AND CreditPaymentID=@CreditPaymentID;";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@AmountPaid", decCredit);
//                                cmd.Parameters.AddWithValue("@ContactID", ContactID);
//                                cmd.Parameters.AddWithValue("@CreditPaymentID", CreditPaymentID);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);

//                                Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " purchases updated.");
//                                iNotTheSame++;
//                                break;
//                            }
//                        }

//                        // meaning there is still credit but no purchases
//                        if (decCredit > 0)
//                        {
//                            Console.WriteLine("credit[" + iCtr.ToString() + "/" + iCount.ToString() + "]Contact: " + clsContactDetails.CreditDetails.CreditCardNo + " still have credits but without purchases.");
//                            iTheSame++;
//                            break;
//                        }
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
