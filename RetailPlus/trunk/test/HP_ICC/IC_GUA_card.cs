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
//                Int64 iLimit = 1000;
                
//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Connected to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                string SQL = "SELECT ContactID AS CustomerID, ContactID GuarantorID, " +
//                                     "                     CASE WHEN CONVERT(GUPENA, DECIMAL(18,2)) = 0 THEN 6 " + // -- 'HP SUPERCARD - 30' 
//                                     "						    ELSE 8 " + // -- HP SUPER CARD - 15/30 
//                                     "					   END CreditCardTypeID, " +
//                                     "						CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) CreditCardNo " +
//                            "FROM IC_GUA " +
//                            "INNER JOIN tblContacts cntct ON cntct.ContactCode = CONCAT('800000', LPAD(IC_GUA.GURKEY,7,0)) " +
//                            "WHERE cntct.ContactID NOT IN (SELECT DISTINCT CustomerID FROM tblContactCreditCardInfo) LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;

//                System.Data.DataTable dt = new System.Data.DataTable("tblContacts");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 CustomerID = Int64.Parse(dr["CustomerID"].ToString());
//                    Int64 GuarantorID = Int64.Parse(dr["GuarantorID"].ToString());
//                    Int32 CreditCardTypeID = Int32.Parse(dr["CreditCardTypeID"].ToString());
//                    string CreditCardNo = dr["CreditCardNo"].ToString();

//                    try
//                    {
//                        SQL = "INSERT INTO tblContactCreditCardInfo(CustomerID, GuarantorID, CreditCardTypeID, " +
//                                                "CreditCardNo, CreditAwardDate, CreditCardStatus, ExpiryDate)VALUES(" +
//                                                "@CustomerID, @GuarantorID, @CreditCardTypeID, " +
//                                                "@CreditCardNo, NOW(), @CreditCardStatus, DATE_ADD(NOW(), INTERVAL 2 YEAR))";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
//                        cmd.Parameters.AddWithValue("@GuarantorID", GuarantorID);
//                        cmd.Parameters.AddWithValue("@CreditCardTypeID", CreditCardTypeID);
//                        cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);
//                        cmd.Parameters.AddWithValue("@CreditCardStatus", 10); //SystemActivatedCreditCardStatus

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]CustomerID: " + CustomerID + " has been inserted.");
//                        iNotTheSame++;
//                    }
//                    catch
//                    {
//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]CustomerID: " + CustomerID + " already in the database.");
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
