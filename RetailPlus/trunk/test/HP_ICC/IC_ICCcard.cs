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
//    /// Summary description for IC_GUA.
//    /// </summary>
//    public class IC_GUA
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) CreditCardNo, CONCAT('800000', LPAD(IC_ICC.ICRKEY,7,0)) ContactCode, " +
//                             "                       CASE WHEN ICSTAT = 'A' THEN 10 " +
//                            "	                        WHEN ICSTAT = 'C' THEN 1 " +
//                            "	                        WHEN ICSTAT = 'D' THEN 8 " +
//                            "	                        WHEN ICSTAT = 'L' THEN 1 " +
//                            "	                        WHEN ICSTAT = 'S' THEN 11 " +
//                            "	                        ELSE 11 " +
//                            "                       END CreditCardStatus, " +
//                            "           CONCAT('800000', LPAD(IC_ICC.GURKEY,7,0)) GuarantorCode " +
//                            "FROM IC_ICC;";

//                cmd.CommandText = SQL;

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    string GuarantorCode = dr["GuarantorCode"].ToString();
//                    string ContactCode = dr["ContactCode"].ToString();
//                    string CreditCardNo = dr["CreditCardNo"].ToString();
//                    Int16 CreditCardStatus = Int16.Parse(dr["CreditCardStatus"].ToString());

//                    ContactDetails Member = clsContacts.Details(ContactCode);
//                    try
//                    {
//                        if (Member.CreditDetails.CardTypeDetails.CardTypeID != 0)
//                        {
//                            //update the information here

//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " already in the database.");
//                            iTheSame++;
//                        }
//                        else
//                        {
//                            ContactDetails guarantor = clsContacts.Details(GuarantorCode);
//                            if (guarantor.ContactID == 0)
//                            {
//                                Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " guarantor: " + GuarantorCode + " not found.");
//                                iTheSame++;
//                            }
//                            else
//                            {
//                                SQL = "INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, " +
//                                                        "CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate)VALUES(" +
//                                                        "@CustomerID, @GuarantorID, @CreditCardTypeID, " +
//                                                        "@CreditCardNo, NOW(), @CreditCardStatus, DATE_ADD(NOW(), INTERVAL 2 YEAR))";

//                                cmd.Parameters.Clear();
//                                cmd.Parameters.AddWithValue("@CustomerID", Member.ContactID);
//                                cmd.Parameters.AddWithValue("@GuarantorID", guarantor.ContactID);
//                                cmd.Parameters.AddWithValue("@CreditCardTypeID", guarantor.CreditDetails.CardTypeDetails.CardTypeID);
//                                cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);
//                                cmd.Parameters.AddWithValue("@CreditCardStatus", CreditCardStatus);

//                                cmd.CommandText = SQL;
//                                clsLocalConnection.ExecuteNonQuery(cmd);

//                                Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " has been inserted.");
//                                iNotTheSame++;
//                            }
//                        }
//                    }
//                    catch
//                    {
//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " already in the database.");
//                        iTheSame++;
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
