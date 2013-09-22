using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public struct MatrixPackageDetails
    {
        public Int64 PackageID;
        public Int64 MatrixID;
        public Int32 UnitID;
        public string UnitCode;
        public string UnitName;
        public decimal Price;
        public decimal WSPrice;
        public decimal PurchasePrice;
        public decimal Quantity;
        public decimal VAT;
        public decimal EVAT;
        public decimal LocalTax;
    }

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class MatrixPackage : POSConnection
    {
        #region Constructors and Destructors

        public MatrixPackage()
            : base(null, null)
        {
        }

        public MatrixPackage(MySqlConnection Connection, MySqlTransaction Transaction)
            : base(Connection, Transaction)
        {

        }

        #endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "a.PackageID, " +
                                "a.MatrixID, " +
                                "Description, " +
                                "a.UnitID, " +
                                "c.UnitCode, " +
                                "c.UnitName, " +
                                "a.Price, " +
                                "a.WSPrice, " +
                                "a.PurchasePrice, " +
                                "a.Quantity, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax " +
                            "FROM tblMatrixPackage a " +
                            "INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID " +
                            "INNER JOIN tblUnit c ON a.UnitID = c.UnitID ";

            return stSQL;
        }

        #region Streams

        public MySqlDataReader List(string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;



                return base.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, long ProductID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE b.ProductID = @ProductID ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);


                return dt;
            }
            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public MySqlDataReader List(string SortField, SortOption SortOrder, string MatrixIDs)
        {
            try
            {
                string SQL = "SELECT " +
                                "a.PackageID, " +
                                "a.MatrixID, " +
                                "Description, " +
                                "a.UnitID, " +
                                "c.UnitCode, " +
                                "c.UnitName, " +
                                "a.Price, " +
                                "a.WSPrice, " +
                                "a.PurchasePrice, " +
                                "a.Quantity, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax " +
                            "FROM tblMatrixPackage a " +
                            "INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID " +
                            "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
                            "WHERE 1=1 ";

                if (MatrixIDs != "" && MatrixIDs != null)
                    SQL += "AND a.MatrixID in (" + MatrixIDs + ") ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;



                return base.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public MySqlDataReader List(Int64 MatrixID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = "SELECT " +
                                "a.PackageID, " +
                                "a.MatrixID, " +
                                "Description, " +
                                "a.UnitID, " +
                                "c.UnitCode, " +
                                "c.UnitName, " +
                                "a.Price, " +
                                "a.WSPrice, " +
                                "a.PurchasePrice, " +
                                "a.Quantity, " +
                                "a.VAT, " +
                                "a.EVAT, " +
                                "a.LocalTax " +
                            "FROM tblMatrixPackage a " +
                            "INNER JOIN tblProductBaseVariationsMatrix b ON a.MatrixID = b.MatrixID " +
                            "INNER JOIN tblUnit c ON a.UnitID = c.UnitID " +
                            "WHERE 1=1 AND a.MatrixID = @MatrixID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = MatrixID;
                cmd.Parameters.Add(prmMatrixID);



                return base.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ListAsDataTable(Int64 MatrixID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 AND a.MatrixID = @MatrixID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);


                return dt;
            }
            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Insert and Update

        public void Insert(MatrixPackageDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblMatrixPackage (" +
                                "MatrixID, " +
                                "UnitID, " +
                                "Price, " +
                                "WSPrice, " +
                                "PurchasePrice, " +
                                "Quantity, " +
                                "VAT, " +
                                "EVAT, " +
                                "LocalTax" +
                            ") VALUES (" +
                                "@MatrixID, " +
                                "@UnitID, " +
                                "@Price, " +
                                "@WSPrice, " +
                                "@PurchasePrice, " +
                                "@Quantity, " +
                                "@VAT, " +
                                "@EVAT, " +
                                "@LocalTax" +
                            ");";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = Details.MatrixID;
                cmd.Parameters.Add(prmMatrixID);

                MySqlParameter prmUnitID = new MySqlParameter("@UnitID", MySqlDbType.Int32);
                prmUnitID.Value = Details.UnitID;
                cmd.Parameters.Add(prmUnitID);

                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity", MySqlDbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmVAT = new MySqlParameter("@VAT", MySqlDbType.Decimal);
                prmVAT.Value = Details.VAT;
                cmd.Parameters.Add(prmVAT);

                MySqlParameter prmEVAT = new MySqlParameter("@EVAT", MySqlDbType.Decimal);
                prmEVAT.Value = Details.EVAT;
                cmd.Parameters.Add(prmEVAT);

                MySqlParameter prmLocalTax = new MySqlParameter("@LocalTax", MySqlDbType.Decimal);
                prmLocalTax.Value = Details.LocalTax;
                cmd.Parameters.Add(prmLocalTax);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public void Update(MatrixPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
        {
            try
            {


                // Update MatrixPackagePriceHistory first to get the history
                MatrixPackagePriceHistoryDetails clsMatrixPackagePriceHistoryDetails = new MatrixPackagePriceHistoryDetails();
                clsMatrixPackagePriceHistoryDetails.UID = pvtUID;
                clsMatrixPackagePriceHistoryDetails.PackageID = Details.PackageID;
                clsMatrixPackagePriceHistoryDetails.ChangeDate = pvtChangeDate;
                clsMatrixPackagePriceHistoryDetails.PurchasePrice = Details.PurchasePrice;
                clsMatrixPackagePriceHistoryDetails.Price = Details.Price;
                clsMatrixPackagePriceHistoryDetails.VAT = Details.VAT;
                clsMatrixPackagePriceHistoryDetails.EVAT = Details.EVAT;
                clsMatrixPackagePriceHistoryDetails.LocalTax = Details.LocalTax;
                clsMatrixPackagePriceHistoryDetails.Remarks = pvtHistoryRemarks;

                MatrixPackagePriceHistory clsMatrixPackagePriceHistory = new MatrixPackagePriceHistory(base.Connection, base.Transaction);
                clsMatrixPackagePriceHistory.Insert(clsMatrixPackagePriceHistoryDetails);

                string SQL = "CALL procMatrixPackageUpdate(@PackageID, @MatrixID, @UnitID, @Price, @WSPrice, @PurchasePrice, @Quantity, @VAT, @EVAT, @LocalTax);";

                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PackageID", Details.PackageID);
                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@Price", Details.Price);
                cmd.Parameters.AddWithValue("@WSPrice", Details.WSPrice);
                cmd.Parameters.AddWithValue("@PurchasePrice", Details.PurchasePrice);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);

                base.ExecuteNonQuery(cmd);

                if (Details.Quantity == 1)
                {
                    ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
                    clsProductVariationsMatrix.UpdateByPackage(Details.MatrixID, Details.UnitID, Details.Price, Details.WSPrice, Details.PurchasePrice, Details.VAT, Details.EVAT, Details.LocalTax);
                }
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateByMatrixIDUnitIDAndQuantity(MatrixPackageDetails Details, long pvtUID, DateTime pvtChangeDate, string pvtHistoryRemarks)
        {
            try
            {
                string SQL = "SELECT PackageID FROM tblMatrixPackage WHERE MatrixID = @MatrixID " +
                                "AND UnitID		=	@UnitID " +
                                "AND Quantity	=	@Quantity;";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", Details.MatrixID);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                while (myReader.Read())
                { Details.PackageID = myReader.GetInt64("PackageID"); }

                myReader.Close();

                Update(Details, pvtUID, pvtChangeDate, pvtHistoryRemarks);
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdatePurchasing(long MatrixID, int UnitID, decimal Quantity, decimal PurchasePrice)
        {
            try
            {
                string SQL = "CALL procMatrixPackageUpdatePurchasing(@MatrixID, @UnitID, @Quantity, @PurchasePrice);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@PurchasePrice", PurchasePrice);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public void UpdateSelling(long MatrixID, int UnitID, decimal Quantity, decimal Price)
        {
            try
            {
                string SQL = "CALL procMatrixPackageUpdateSelling(@MatrixID, @UnitID, @Quantity, @Price);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Price", Price);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSellingWSPrice(long MatrixID, int UnitID, decimal Quantity, decimal WholesalePrice)
        {
            try
            {
                string SQL = "CALL procMatrixPackageUpdateSellingWSPrice(@MatrixID, @UnitID, @Quantity, @WSPrice);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@WSPrice", WholesalePrice);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateSellingWithSameQuantityAndUnit(long ProductID, int UnitID, decimal Quantity, decimal Price)
        {
            try
            {
                string SQL = "CALL procMatrixPackageUpdateSellingUsingProductID(@ProductID, @UnitID, @Quantity, @Price);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Price", Price);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        public void UpdateSellingWithSameQuantityAndUnitWSPrice(long ProductID, int UnitID, decimal Quantity, decimal WholesalePrice)
        {
            try
            {
                string SQL = "CALL procMatrixPackageUpdateSellingUsingProductIDWSPrice(@ProductID, @UnitID, @Quantity, @WSPrice);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = GetConnection();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@WSPrice", WholesalePrice);

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                string SQL = "DELETE FROM tblMatrixPackage WHERE PackageID IN (" + IDs + ");";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                base.ExecuteNonQuery(cmd);

                return true;
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }


        #endregion

        #region Details

        public long GetPackageID(long MatrixID, int UnitID)
        {
            try
            {
                string SQL = "SELECT PackageID FROM tblMatrixPackage WHERE MatrixID = @MatrixID AND UnitID = @UnitID AND Quantity = 1;";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@MatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@UnitID", UnitID);

                System.Data.DataTable dt = new System.Data.DataTable("MatrixPackage");
                base.MySqlDataAdapterFill(cmd, dt);


                long lngRetValue = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    lngRetValue = Int64.Parse(dr["PackageID"].ToString());
                }

                return lngRetValue;
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public MatrixPackageDetails Details(Int64 MatrixPackageID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 AND a.PackageID = @PackageID;";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPackageID = new MySqlParameter("@PackageID", MySqlDbType.Int64);
                prmPackageID.Value = MatrixPackageID;
                cmd.Parameters.Add(prmPackageID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                MatrixPackageDetails clsDetails = new MatrixPackageDetails(); ;

                while (myReader.Read())
                {
                    clsDetails.PackageID = myReader.GetInt64("PackageID");
                    clsDetails.MatrixID = myReader.GetInt64("MatrixID");
                    clsDetails.UnitID = myReader.GetInt32("UnitID");
                    clsDetails.UnitCode = "" + myReader["UnitCode"].ToString();
                    clsDetails.UnitName = "" + myReader["UnitName"].ToString();
                    clsDetails.Price = myReader.GetDecimal("Price");
                    clsDetails.WSPrice = myReader.GetDecimal("WSPrice");
                    clsDetails.PurchasePrice = myReader.GetDecimal("PurchasePrice");
                    clsDetails.Quantity = myReader.GetDecimal("Quantity");
                    clsDetails.VAT = myReader.GetDecimal("VAT");
                    clsDetails.EVAT = myReader.GetDecimal("EVAT");
                    clsDetails.LocalTax = myReader.GetDecimal("LocalTax");
                }

                myReader.Close();

                return clsDetails;
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }
        public MatrixPackageDetails DetailsByMatrixID(Int64 MatrixID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 AND a.MatrixID = @MatrixID LIMIT 1;";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = MatrixID;
                cmd.Parameters.Add(prmMatrixID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                MatrixPackageDetails clsDetails = new MatrixPackageDetails(); ;

                while (myReader.Read())
                {
                    clsDetails.PackageID = myReader.GetInt64("PackageID");
                    clsDetails.MatrixID = myReader.GetInt64("MatrixID");
                    clsDetails.UnitID = myReader.GetInt32("UnitID");
                    clsDetails.UnitCode = "" + myReader["UnitCode"].ToString();
                    clsDetails.UnitName = "" + myReader["UnitName"].ToString();
                    clsDetails.Price = myReader.GetDecimal("Price");
                    clsDetails.WSPrice = myReader.GetDecimal("WSPrice");
                    clsDetails.PurchasePrice = myReader.GetDecimal("PurchasePrice");
                    clsDetails.Quantity = myReader.GetDecimal("Quantity");
                    clsDetails.VAT = myReader.GetDecimal("VAT");
                    clsDetails.EVAT = myReader.GetDecimal("EVAT");
                    clsDetails.LocalTax = myReader.GetDecimal("LocalTax");
                }

                myReader.Close();

                return clsDetails;
            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region CountPackage

        public Int32 CountPackage(Int64 MatrixID)
        {
            try
            {
                string SQL = "SELECT Count(MatrixID) MatrixIDCount FROM tblMatrixPackage WHERE MatrixID = @MatrixID ";


                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID", MySqlDbType.Int64);
                prmMatrixID.Value = MatrixID;
                cmd.Parameters.Add(prmMatrixID);

                System.Data.DataTable dt = new System.Data.DataTable("tblPackage");
                base.MySqlDataAdapterFill(cmd, dt);

                int iRetValue = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iRetValue = Int32.Parse(dr["MatrixIDCount"].ToString());
                }

                return iRetValue;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }


        #endregion

        #region Change VAT, EVAT and Localtax

        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeVAT(decimal OldVAT, decimal NewVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblMatrixPackage SET " +
        //                            "VAT		= @NewVAT " +
        //                        "WHERE VAT		= @OldVAT;";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT",MySqlDbType.Decimal);			
        //        prmNewVAT.Value = NewVAT;
        //        cmd.Parameters.Add(prmNewVAT);

        //        MySqlParameter prmOldVAT = new MySqlParameter("@OldVAT",MySqlDbType.Decimal);			
        //        prmOldVAT.Value = OldVAT;
        //        cmd.Parameters.Add(prmOldVAT);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeEVAT(decimal OldEVAT, decimal NewEVAT)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblMatrixPackage SET " +
        //                            "EVAT		= @NewEVAT " +
        //                        "WHERE EVAT		= @OldEVAT;";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT",MySqlDbType.Decimal);			
        //        prmNewEVAT.Value = NewEVAT;
        //        cmd.Parameters.Add(prmNewEVAT);

        //        MySqlParameter prmOldEVAT = new MySqlParameter("@OldEVAT",MySqlDbType.Decimal);			
        //        prmOldEVAT.Value = OldEVAT;
        //        cmd.Parameters.Add(prmOldEVAT);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
        //// Dec 10, 2011 : Obsolete, change to ChangeTax
        //public void ChangeLocalTax(decimal OldLocalTax, decimal NewLocalTax)
        //{
        //    try 
        //    {
        //        string SQL =	"UPDATE tblMatrixPackage SET " +
        //                            "LocalTax		= @NewLocalTax " +
        //                        "WHERE LocalTax		= @OldLocalTax;";

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax",MySqlDbType.Decimal);			
        //        prmNewLocalTax.Value = NewLocalTax;
        //        cmd.Parameters.Add(prmNewLocalTax);

        //        MySqlParameter prmOldLocalTax = new MySqlParameter("@OldLocalTax",MySqlDbType.Decimal);			
        //        prmOldLocalTax.Value = OldLocalTax;
        //        cmd.Parameters.Add(prmOldLocalTax);

        //        base.ExecuteNonQuery(cmd);
        //    }

        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}

        public void ChangeTax(long ProductGroupID, long ProductSubGroupID, long ProductID, decimal NewVAT, decimal NewEVAT, decimal NewLocalTax)
        {
            try
            {
                string SQL = "UPDATE tblMatrixPackage SET " +
                                    "VAT		= @NewVAT, " +
                                    "EVAT		= @NewEVAT, " +
                                    "LocalTax	= @NewLocalTax ";

                if (ProductID != 0) SQL += "WHERE MatrixID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE ProductID = @ProductID);";
                else if (ProductSubGroupID != 0) SQL += "WHERE MatrixID IN (SELECT DISTINCT(ProductID) FROM tblProductBaseVariationsMatrix WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID = @ProductSubGroupID));";
                else if (ProductGroupID != 0) SQL += "WHERE MatrixID IN (SELECT DISTINCT(MatrixID) FROM tblProductBaseVariationsMatrix WHERE ProductID IN (SELECT DISTINCT(ProductID) FROM tblProducts WHERE ProductSubGroupID IN (SELECT DISTINCT(ProductSubGroupID) FROM tblProductSubGroup WHERE ProductGroupID = @ProductGroupID)));";



                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmNewVAT = new MySqlParameter("@NewVAT", MySqlDbType.Decimal);
                prmNewVAT.Value = NewVAT;
                cmd.Parameters.Add(prmNewVAT);

                MySqlParameter prmNewEVAT = new MySqlParameter("@NewEVAT", MySqlDbType.Decimal);
                prmNewEVAT.Value = NewEVAT;
                cmd.Parameters.Add(prmNewEVAT);

                MySqlParameter prmNewLocalTax = new MySqlParameter("@NewLocalTax", MySqlDbType.Decimal);
                prmNewLocalTax.Value = NewLocalTax;
                cmd.Parameters.Add(prmNewLocalTax);

                if (ProductID != 0)
                {
                    MySqlParameter prmProductID = new MySqlParameter("@ProductID", MySqlDbType.Int64);
                    prmProductID.Value = ProductID;
                    cmd.Parameters.Add(prmProductID);
                }
                else if (ProductSubGroupID != 0)
                {
                    MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID", MySqlDbType.Int64);
                    prmProductSubGroupID.Value = ProductSubGroupID;
                    cmd.Parameters.Add(prmProductSubGroupID);
                }
                else if (ProductGroupID != 0)
                {
                    MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID", MySqlDbType.Int64);
                    prmProductGroupID.Value = ProductGroupID;
                    cmd.Parameters.Add(prmProductGroupID);
                }

                base.ExecuteNonQuery(cmd);

            }

            catch (Exception ex)
            {


                {




                }

                throw base.ThrowException(ex);
            }
        }

        #endregion
    }
}

