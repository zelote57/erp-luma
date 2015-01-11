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
//    /// Summary description for FE_DSC.
//    /// </summary>
//    public class FE_DSC
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

//                string SQL = "SELECT DSRKEY RewardCardNo, DSRKEY ContactCode, DS_PTS RewardPoints, NOW() RewardAwardDate, DS_AMT TotalPurchases, DS_RED RedeemedPoints, " +
//                             "                       CASE WHEN DSTATS = 'A' THEN 10 " +
//                            "	                        WHEN DSTATS = 'C' THEN 1 " +
//                            "	                        WHEN DSTATS = 'D' THEN 8 " +
//                            "	                        WHEN DSTATS = 'L' THEN 1 " +
//                            "	                        WHEN DSTATS = 'S' THEN 11 " +
//                            "	                        ELSE 11 " +
//                            "                       END RewardCardStatus, LEFT(DSADR2, 7) ExpiryDate, DSBDAY BirthDate " +
//                            "FROM FE_DSC;";

//                cmd.CommandText = SQL;

//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    string ContactCode = dr["ContactCode"].ToString();
//                    string RewardCardNo = dr["RewardCardNo"].ToString();
//                    decimal RewardPoints = decimal.TryParse(dr["RewardPoints"].ToString(), out RewardPoints) ? RewardPoints : 0;
//                    decimal TotalPurchases = decimal.TryParse(dr["TotalPurchases"].ToString(), out TotalPurchases) ? TotalPurchases : 0;
//                    decimal RedeemedPoints = decimal.TryParse(dr["RedeemedPoints"].ToString(), out RedeemedPoints) ? RedeemedPoints : 0;
//                    RewardCardStatus RewardCardStatus = (RewardCardStatus)Enum.Parse(typeof(RewardCardStatus), dr["RewardCardStatus"].ToString());
//                    string tmpString = dr["ExpiryDate"].ToString();
//                    DateTime ExpiryDate = DateTime.Now;

//                    if (tmpString.Trim().Length >= 7)
//                        try
//                        { ExpiryDate = new DateTime(Int32.Parse(tmpString.Substring(3, 4)), Int32.Parse(tmpString.Substring(0, 2)), DateTime.DaysInMonth(Int32.Parse(tmpString.Substring(3, 4)), Int32.Parse(tmpString.Substring(0, 2)))); }
//                        catch { ExpiryDate = DateTime.Now; }

//                    tmpString = dr["BirthDate"].ToString();
//                    DateTime BirthDate = DateTime.TryParse(tmpString, out BirthDate) ? BirthDate : Constants.C_DATE_MIN_VALUE;

//                    ContactDetails Member = clsContacts.Details(ContactCode);
//                    try
//                    {
//                        if (Member.RewardDetails.ContactID != 0)
//                        {
//                            //update the information here
//                            SQL = "UPDATE tblContactRewards SET " +
//                                        "RewardActive = @RewardActive, RewardPoints = @RewardPoints, TotalPurchases = @TotalPurchases, " +
//                                        "RedeemedPoints = @RedeemedPoints, RewardCardStatus = @RewardCardStatus, ExpiryDate = @ExpiryDate " +
//                                        "WHERE CustomerID = @CustomerID;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@CustomerID", Member.ContactID);
//                            cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);
//                            cmd.Parameters.AddWithValue("@RewardActive", Contacts.checkRewardActive(RewardCardStatus));
//                            cmd.Parameters.AddWithValue("@RewardPoints", RewardPoints - RedeemedPoints);
//                            cmd.Parameters.AddWithValue("@RewardAwardDate", DateTime.Now);
//                            cmd.Parameters.AddWithValue("@TotalPurchases", TotalPurchases);
//                            cmd.Parameters.AddWithValue("@RedeemedPoints", RedeemedPoints);
//                            cmd.Parameters.AddWithValue("@RewardCardStatus", RewardCardStatus);
//                            cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
//                            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
//                            cmd.Parameters.AddWithValue("@SoldByID", 1);
//                            cmd.Parameters.AddWithValue("@SoldByName", "SysUser");
//                            cmd.Parameters.AddWithValue("@ConfirmedByID", 1);
//                            cmd.Parameters.AddWithValue("@ConfirmedByName", "SysUser");

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " already in the database.");
//                            iTheSame++;
//                        }
//                        else
//                        {
//                            SQL = "INSERT INTO tblContactRewards(CustomerID, RewardCardNo, RewardActive, RewardPoints, RewardAwardDate, TotalPurchases, RedeemedPoints, " +
//                                                                "RewardCardStatus, ExpiryDate, BirthDate, SoldByID, SoldByName, " +
//                                                                "ConfirmedByID, ConfirmedByName ,CreatedOn, LastModified)VALUES(" +
//                                                                "@CustomerID, @RewardCardNo, @RewardActive, @RewardPoints, @RewardAwardDate, @TotalPurchases, @RedeemedPoints, " +
//                                                                "@RewardCardStatus, @ExpiryDate, @BirthDate, @SoldByID, @SoldByName, " +
//                                                                "@ConfirmedByID, @ConfirmedByName, NOW(), NOW())";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@CustomerID", Member.ContactID);
//                            cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);
//                            cmd.Parameters.AddWithValue("@RewardActive", Contacts.checkRewardActive(RewardCardStatus)); // HP CREDIT CARD
//                            cmd.Parameters.AddWithValue("@RewardPoints", RewardPoints - RedeemedPoints);
//                            cmd.Parameters.AddWithValue("@RewardAwardDate", DateTime.Now);
//                            cmd.Parameters.AddWithValue("@TotalPurchases", TotalPurchases);
//                            cmd.Parameters.AddWithValue("@RedeemedPoints", RedeemedPoints);
//                            cmd.Parameters.AddWithValue("@RewardCardStatus", RewardCardStatus);
//                            cmd.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
//                            cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
//                            cmd.Parameters.AddWithValue("@SoldByID", 1);
//                            cmd.Parameters.AddWithValue("@SoldByName", "SysUser");
//                            cmd.Parameters.AddWithValue("@ConfirmedByID", 1);
//                            cmd.Parameters.AddWithValue("@ConfirmedByName", "SysUser");

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Customer: " + Member.ContactCode + " has been inserted.");
//                            iNotTheSame++;
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
