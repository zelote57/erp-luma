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
//    /// Summary description for CheckDigitCCard.
//    /// </summary>
//    public class CheckDigitCCard
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 1000;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Conencted to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                // ALTER TABLE tblProductPackage ADD `CheckDigitCCardOk` TINYINT(1) NOT NULL DEFAULT 0;
//                string SQL = "SELECT CustomerID, RewardCardNo FROM tblContactRewards  WHERE LENGTH(RewardCardNo) =13 AND RIGHT(RewardCardNo,4) > 1000 LIMIT " + iLimit.ToString() + ";";
//                cmd.CommandText = SQL;

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 CustomerID = Int64.Parse(dr["CustomerID"].ToString());
//                    string RewardCardNo = dr["RewardCardNo"].ToString();
//                    string CountryCode = RewardCardNo.Substring(0, 2);
//                    string ManufacturerCode = RewardCardNo.Substring(2, 4);
//                    string CardNo = RewardCardNo.Substring(6, 6);

//                    AceSoft.BarcodeHelper ean13 = new AceSoft.BarcodeHelper(CountryCode, ManufacturerCode,CardNo);

//                    string newRewardCardNo = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

//                    SQL = "SELECT CustomerID, RewardCardNo FROM tblContactRewards  WHERE RewardCardNo=@RewardCardNo AND CustomerID<>@CustomerID LIMIT 1;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@RewardCardNo", newRewardCardNo);
//                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (dtT.Rows.Count > 0)
//                    {
//                        //already exist update later
//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]RewardCardNo: " + newRewardCardNo + " already exisit in db.");
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        if (RewardCardNo != newRewardCardNo)
//                        {
//                            SQL = "UPDATE tblContactRewards SET RewardCardNo=@newRewardCardNo WHERE CustomerID=@CustomerID AND RewardCardNo=@RewardCardNo;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
//                            cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);
//                            cmd.Parameters.AddWithValue("@newRewardCardNo", newRewardCardNo);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]RewardCardNo change from: " + RewardCardNo + " to " + newRewardCardNo);
//                            iNotTheSame++;
//                        }
//                        else
//                        {
//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]RewardCardNo: " + newRewardCardNo + " is the same.");
//                            iTheSame++;
//                        }
//                    }
//                    iCtr++;
//                }
//                clsLocalConnection.CommitAndDispose();
//                Console.WriteLine("done and committed");
//                Console.WriteLine("Total: " + iCount.ToString());
//                Console.WriteLine("TheSame: " + iTheSame.ToString());
//                Console.WriteLine("NotTheSame: " + iNotTheSame.ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                Console.WriteLine(ex.ToString());
//            }
//        }
//    }
//}
