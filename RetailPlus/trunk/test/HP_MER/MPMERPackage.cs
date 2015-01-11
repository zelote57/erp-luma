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
//    public class MPMERPackage
//    {
//        public static void Main(string[] args)
//        {
//            try
//            {
//                Int64 iLimit = 100;

//                if (args.Length >= 1) iLimit = Int64.Parse(args[0].ToString());

//                AceSoft.RetailPlus.Client.LocalDB clsLocalConnection = new AceSoft.RetailPlus.Client.LocalDB();
//                clsLocalConnection.GetConnection();

//                Console.Write("Conencted to " + clsLocalConnection.Connection.ConnectionString + ". Press ok to continue or CTRL +C to abort.");
//                Console.ReadLine();

//                MySqlCommand cmd = new MySqlCommand();
//                cmd.CommandType = System.Data.CommandType.Text;

//                // ALTER TABLE tblProductPackage ADD `MPMERPackageOk` TINYINT(1) NOT NULL DEFAULT 0;
//                string SQL = "SELECT MEDESC ProductCode, MEDESC ProductDesc, CLRKEY ProductSubGroupCode, MEPCK3 UnitCode, CONCAT('SUP:',SURKEY ) SupplierCode, " +
//                             "    MP_MER.MERETP Price, MP_MER.MECOS2 PurchasePrice, " +
//                             "	  MP_MER.MERETP WSPrice, " +
//                             "	  CASE WHEN IFNULL(SUSTOK,'') <> '' THEN SUSTOK " +
//                             "		   WHEN IFNULL(MEAN13,'') <> '' THEN MEAN13 " +
//                             "		   ELSE CONCAT('400000',MERKEY) " +
//                             "	  END Barcode1, SUSTK2 Barcode2, BARCD1 Barcode3, CONCAT('9999',MERKEY) Barcode4, MP_MER_ID " +
//                             "FROM MP_MER WHERE isProcessed=0 LIMIT " + iLimit.ToString() + ";";

//                cmd.CommandText = SQL;

//                ProductSubGroup clsProductSubGroup = new ProductSubGroup(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                Unit clsUnit = new Unit(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                Products clsProducts = new Products(clsLocalConnection.Connection, clsLocalConnection.Transaction);
//                Contacts clsContacts = new Contacts(clsLocalConnection.Connection, clsLocalConnection.Transaction);

//                System.Data.DataTable dt = new System.Data.DataTable("tblTemp");
//                clsLocalConnection.MySqlDataAdapterFill(cmd, dt);

//                Int64 iCount = dt.Rows.Count;
//                Int64 iCtr = 1, iTheSame = 0, iNotTheSame = 0;
//                foreach (System.Data.DataRow dr in dt.Rows)
//                {
//                    Int64 MP_MER_ID = Int64.Parse(dr["MP_MER_ID"].ToString());
//                    string ProductCode = dr["ProductCode"].ToString();
//                    string ProductDesc = dr["ProductDesc"].ToString();
//                    string ProductSubGroupCode = dr["ProductSubGroupCode"].ToString();
//                    string UnitCode = dr["UnitCode"].ToString();
//                    string SupplierCode = dr["SupplierCode"].ToString();
//                    decimal Price = decimal.Parse(dr["Price"].ToString());
//                    decimal PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
//                    decimal WSPrice = decimal.Parse(dr["WSPrice"].ToString());
//                    string Barcode1 = dr["BarCode1"].ToString();
//                    string Barcode2 = dr["Barcode2"].ToString();
//                    string Barcode3 = dr["BarCode3"].ToString();
//                    string Barcode4 = dr["BarCode4"].ToString();

//                Step1:
//                    SQL = "SELECT * FROM tblProducts WHERE ProductCode = @ProductCode LIMIT 1;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

//                    cmd.CommandText = SQL;
//                    System.Data.DataTable dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (Barcode1.Substring(0, 6) == "400000")
//                    {
//                        string x = "";
//                    }

//                    if (dtT.Rows.Count == 0)
//                    {
//                        //no need to insert just update

//                        SQL = "INSERT INTO tblProducts(ProductCode, ProductDesc, ProductSubGroupID, BaseUnitID, DateCreated, IncludeInSubtotalDiscount, " +
//                                                       "SupplierID, IsItemSold, Active, CreatedOn, LastModified, SequenceNo)VALUES(" +
//                                                       "@ProductCode, @ProductDesc, @ProductSubGroupID, @BaseUnitID, @DateCreated, @IncludeInSubtotalDiscount, " +
//                                                       "@SupplierID, @IsItemSold, @Active, NOW(), NOW(), SequenceNo)";

//                        Int64 SupplierID = clsContacts.Details(SupplierCode).ContactID;
//                        SupplierID = SupplierID == 0 ? 2 : SupplierID;

//                        Int32 UnitID = clsUnit.Details(UnitCode).UnitID;
//                        UnitID = UnitID == 0 ? 1 : UnitID;

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
//                        cmd.Parameters.AddWithValue("@ProductDesc", ProductDesc);
//                        cmd.Parameters.AddWithValue("@ProductSubGroupID", clsProductSubGroup.Details(ProductSubGroupCode).ProductSubGroupID);
//                        cmd.Parameters.AddWithValue("@BaseUnitID", UnitID);
//                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
//                        cmd.Parameters.AddWithValue("@IncludeInSubtotalDiscount", 1);
//                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
//                        cmd.Parameters.AddWithValue("@IsItemSold", 1);
//                        cmd.Parameters.AddWithValue("@Active", 1);
//                        cmd.Parameters.AddWithValue("@SequenceNo", MP_MER_ID);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        goto Step1;
//                    }

//                    Int64 ProductID = Int64.Parse(dtT.Rows[0]["ProductID"].ToString());
//                    Int32 BaseUnitID = Int32.Parse(dtT.Rows[0]["BaseUnitID"].ToString());

//                    SQL = "SELECT * FROM tblProductPackage WHERE ProductID = @ProductID AND UnitID=@UnitID LIMIT 1;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
//                    cmd.Parameters.AddWithValue("@UnitID", BaseUnitID);

//                    cmd.CommandText = SQL;
//                    dtT = new System.Data.DataTable("tblTemp");
//                    clsLocalConnection.MySqlDataAdapterFill(cmd, dtT);

//                    if (dtT.Rows.Count > 0)
//                    {
//                        // do an update
//                        SQL = "UPDATE tblProductPackage SET " +
//                                    "Price = @Price, PurchasePrice = @PurchasePrice, WSPrice = @WSPrice " +
//                              "WHERE ProductID = @ProductID AND UnitID=@UnitID;";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
//                        cmd.Parameters.AddWithValue("@UnitID", BaseUnitID);
//                        cmd.Parameters.AddWithValue("@Price", Price);
//                        cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
//                        cmd.Parameters.AddWithValue("@WSPrice", WSPrice);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("mer[" + iCtr.ToString() + "/" + iCount.ToString() + "]Barcode: " + Barcode1 + " already in the database.");
//                        iTheSame++;
//                    }
//                    else
//                    {
//                        if (Barcode1.Substring(0, 6) == "400000" && Barcode1.Length == 13)
//                        {
//                            AceSoft.BarcodeHelper ean13 = new AceSoft.BarcodeHelper("40", "0000", Barcode1.Substring(6, 6));
//                            Barcode1 = ean13.CountryCode + ean13.ManufacturerCode + ean13.ProductCode + ean13.ChecksumDigit;
//                        }

//                        SQL = "INSERT INTO tblProductPackage(ProductID, UnitID, Price, PurchasePrice, Quantity, VAT, WSPrice, " +
//                                                            "Barcode1, Barcode2, Barcode3, Barcode4, CreatedOn, LastModified)VALUES(" +
//                                                            "@ProductID, @UnitID, @Price, @PurchasePrice, @Quantity, @VAT, @WSPrice, " +
//                                                            "@Barcode1, @Barcode2, @Barcode3, @Barcode4, NOW(), NOW())";

//                        cmd.Parameters.Clear();
//                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
//                        cmd.Parameters.AddWithValue("@UnitID", BaseUnitID);
//                        cmd.Parameters.AddWithValue("@Price", Price);
//                        cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);
//                        cmd.Parameters.AddWithValue("@Quantity", 1);
//                        cmd.Parameters.AddWithValue("@VAT", 12);
//                        cmd.Parameters.AddWithValue("@WSPrice", WSPrice);
//                        cmd.Parameters.AddWithValue("@Barcode1", Barcode1);
//                        cmd.Parameters.AddWithValue("@Barcode2", Barcode2);
//                        cmd.Parameters.AddWithValue("@Barcode3", Barcode3);
//                        cmd.Parameters.AddWithValue("@Barcode4", Barcode4);

//                        cmd.CommandText = SQL;
//                        clsLocalConnection.ExecuteNonQuery(cmd);

//                        Console.WriteLine("mer[" + iCtr.ToString() + "/" + iCount.ToString() + "]Barcode: " + Barcode1 + " has been inserted.");
//                        iNotTheSame++;
//                    }

//                    SQL = "UPDATE MP_MER SET isProcessed=1 WHERE MP_MER_ID = @MP_MER_ID;";

//                    cmd.Parameters.Clear();
//                    cmd.Parameters.AddWithValue("@MP_MER_ID", MP_MER_ID);

//                    cmd.CommandText = SQL;
//                    clsLocalConnection.ExecuteNonQuery(cmd);

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
