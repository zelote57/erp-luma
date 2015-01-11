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
//                string SQL = "SELECT CustomerID, CreditCardNo FROM tblContactCreditCardInfo  WHERE LENGTH(CreditCardNo) =13 AND RIGHT(CreditCardNo,4) > 1000 LIMIT " + iLimit.ToString() + ";";
//                cmd.CommandText = SQL;

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 CustomerID = Int64.Parse(dr["CustomerID"].ToString());
//                    string CreditCardNo = dr["CreditCardNo"].ToString();
//                    string CountryCode = CreditCardNo.Substring(0, 2);
//                    string ManufacturerCode = CreditCardNo.Substring(2, 4);
//                    string CardNo = CreditCardNo.Substring(6, 6);

//                    AceSoft.BarcodeHelper ean13 = new AceSoft.BarcodeHelper(CountryCode, ManufacturerCode,CardNo);

//                    string newCreditCardNo = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

//                    SQL = "SELECT CustomerID, CreditCardNo FROM tblContactCreditCardInfo  WHERE CreditCardNo=@CreditCardNo AND CustomerID<>@CustomerID LIMIT 1;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@CreditCardNo", newCreditCardNo);
//                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (dtT.Rows.Count > 0)
//                    {
//                        //already exist update later
//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]CreditCardNo: " + newCreditCardNo + " already exisit in db.");
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        if (CreditCardNo != newCreditCardNo)
//                        {
//                            SQL = "UPDATE tblContactCreditCardInfo SET CreditCardNo=@newCreditCardNo WHERE CustomerID=@CustomerID AND CreditCardNo=@CreditCardNo;";

//                            cmd.Parameters.Clear();
//                            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
//                            cmd.Parameters.AddWithValue("@CreditCardNo", CreditCardNo);
//                            cmd.Parameters.AddWithValue("@newCreditCardNo", newCreditCardNo);

//                            cmd.CommandText = SQL;
//                            clsLocalConnection.ExecuteNonQuery(cmd);

//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]CreditCardNo change from: " + CreditCardNo + " to " + newCreditCardNo);
//                            iNotTheSame++;
//                        }
//                        else
//                        {
//                            Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]CreditCardNo: " + newCreditCardNo + " is the same.");
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
