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
//    /// Summary description for KeyGen.
//    /// </summary>
//    public class CheckDIgit
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

//                // ALTER TABLE tblProductPackage ADD `CheckDigitOk` TINYINT(1) NOT NULL DEFAULT 0;
//                string SQL = "SELECT PackageID, BarCode1 FROM tblProductPackage WHERE LEFT(Barcode1,6) = '400000' AND LENGTH(BarCode1) = 13 LIMIT " + iLimit.ToString() + ";";
//                cmd.CommandText = SQL;

//                System.Data.DataTable dt = new System.Data.DataTable("tblProductPackage");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame=0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    string Barcode1 = dr["BarCode1"].ToString();
//                    Int64 PackageID = Int64.Parse(dr["PackageID"].ToString());

//                    AceSoft.BarcodeHelper ean13 = new AceSoft.BarcodeHelper("40", "0000", Barcode1.Substring(6, 6));

//                    string newBarcode1 = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;

//                    if (Barcode1 != newBarcode1)
//                    {
//                        SQL = "UPDATE tblProductPackage SET CheckDigitOk=1, Barcode1 = @Barcode1 WHERE CheckDigitOk=0 AND PackageID = @PackageID;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@Barcode1", newBarcode1);
//                        cmd.Parameters.AddWithValue("@PackageID", PackageID);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Barcode change from: " + Barcode1 + " to " + newBarcode1);
//                        iNotTheSame++;
//                    }
//                    else
//                    {
//                        SQL = "UPDATE tblProductPackage SET CheckDigitOk=1 WHERE CheckDigitOk=0 AND PackageID = @PackageID AND Barcode1 = @Barcode1;";

                        
//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@Barcode1", newBarcode1);
//                        cmd.Parameters.AddWithValue("@PackageID", PackageID);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("[" + iCtr.ToString() + "/" + iCount.ToString() + "]Barcode: " + Barcode1 + " is the same.");
//                        iTheSame++;
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
