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

    #region DebitMemoItemDetails

    public struct DebitMemoItemDetails
    {
        public long DebitMemoItemID;
        public long DebitMemoID;
        public long ProductID;
        public string ProductCode;
        public string BarCode;
        public string Description;
        public int ProductUnitID;
        public string ProductUnitCode;
        public decimal Quantity;
        public decimal PrevUnitCost;
        public decimal UnitCost;
        public decimal Discount;
        public decimal DiscountApplied;
        public DiscountTypes DiscountType;
        public decimal Amount;
        public decimal VAT;
        public decimal VatableAmount;
        public decimal EVAT;
        public decimal EVatableAmount;
        public decimal LocalTax;
        public bool isVATInclusive;
        public long VariationMatrixID;
        public string MatrixDescription;
        public string ProductGroup;
        public string ProductSubGroup;
        public DebitMemoItemStatus ItemStatus;
        public bool IsVatable;
        public string Remarks;
        public int ChartOfAccountIDPurchase;
        public int ChartOfAccountIDTaxPurchase;
        public int ChartOfAccountIDInventory;
    }

    #endregion

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class DebitMemoItems
    {
        MySqlConnection mConnection;
        MySqlTransaction mTransaction;
        bool IsInTransaction = false;
        bool TransactionFailed = false;

        public MySqlConnection Connection
        {
            get { return mConnection; }
        }

        public MySqlTransaction Transaction
        {
            get { return mTransaction; }
        }


        #region Constructors and Destructors

        public DebitMemoItems()
        {

        }

        public DebitMemoItems(MySqlConnection Connection, MySqlTransaction Transaction)
        {
            mConnection = Connection;
            mTransaction = Transaction;

        }

        public void CommitAndDispose()
        {
            if (!TransactionFailed)
            {
                if (IsInTransaction)
                {
                    mTransaction.Commit();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }
            }
        }

        public MySqlConnection GetConnection()
        {
            if (mConnection == null)
            {
                mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());
                mConnection.Open();

                mTransaction = (MySqlTransaction)mConnection.BeginTransaction();
            }

            IsInTransaction = true;
            return mConnection;
        }


        #endregion

        #region Insert and Update

        public long Insert(DebitMemoItemDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblPODebitMemoItems (" +
                                "DebitMemoID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
                                "PrevUnitCost, " +
                                "UnitCost, " +
                                "Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "EVatableAmount, " +
                                "LocalTax, " +
                                "isVATInclusive, " +
                                "VariationMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "ItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDPurchase, " +
                                "ChartOfAccountIDTaxPurchase, " +
                                "ChartOfAccountIDInventory" +
                            ") VALUES (" +
                                "@DebitMemoID, " +
                                "@ProductID, " +
                                "@ProductCode, " +
                                "@BarCode, " +
                                "@Description, " +
                                "@ProductUnitID, " +
                                "@ProductUnitCode, " +
                                "@Quantity, " +
                                "@PrevUnitCost, " +
                                "@UnitCost, " +
                                "@Discount, " +
                                "@DiscountApplied, " +
                                "@DiscountType, " +
                                "@Amount, " +
                                "@VAT, " +
                                "@VatableAmount, " +
                                "@EVAT, " +
                                "@EVatableAmount, " +
                                "@LocalTax, " +
                                "@isVATInclusive, " +
                                "@VariationMatrixID, " +
                                "@MatrixDescription, " +
                                "@ProductGroup, " +
                                "@ProductSubGroup, " +
                                "@ItemStatus, " +
                                "@IsVatable, " +
                                "@Remarks, " +
                                "(SELECT ChartOfAccountIDPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDTaxPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "(SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID) " +
                            ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DebitMemoID", Details.DebitMemoID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@BarCode", Details.BarCode);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@ProductUnitID", Details.ProductUnitID);
                cmd.Parameters.AddWithValue("@ProductUnitCode", Details.ProductUnitCode);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@PrevUnitCost", Details.PrevUnitCost);
                cmd.Parameters.AddWithValue("@UnitCost", Details.UnitCost);
                cmd.Parameters.AddWithValue("@Discount", Details.Discount);
                cmd.Parameters.AddWithValue("@DiscountApplied", Details.DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", (int)Details.DiscountType);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@VatableAmount", Details.VatableAmount);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@EVatableAmount", Details.EVatableAmount);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@isVATInclusive", Convert.ToInt16(Details.isVATInclusive));
                cmd.Parameters.AddWithValue("@VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@ItemStatus", Details.ItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                cmd.ExecuteNonQuery();

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                Int64 iID = 0;

                while (myReader.Read())
                {
                    iID = myReader.GetInt64(0);
                }

                myReader.Close();

                DebitMemos clsDebitMemos = new DebitMemos(Connection, Transaction);
                clsDebitMemos.SynchronizeAmount(Details.DebitMemoID);

                return iID;
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public void Update(DebitMemoItemDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemoItems SET " +
                                "DebitMemoID					=	@DebitMemoID, " +
                                "ProductID				=	@ProductID, " +
                                "ProductCode			=	@ProductCode, " +
                                "BarCode				=	@BarCode, " +
                                "Description			=	@Description, " +
                                "ProductUnitID			=	@ProductUnitID, " +
                                "ProductUnitCode		=	@ProductUnitCode, " +
                                "Quantity				=	@Quantity, " +
                                "PrevUnitCost			=	@PrevUnitCost, " +
                                "UnitCost				=	@UnitCost, " +
                                "Discount				=	@Discount, " +
                                "DiscountApplied		=	@DiscountApplied, " +
                                "DiscountType			=	@DiscountType, " +
                                "Amount					=	@Amount, " +
                                "VAT					=	@VAT, " +
                                "VatableAmount			=	@VatableAmount, " +
                                "EVAT					=	@EVAT, " +
                                "EVatableAmount			=	@EVatableAmount, " +
                                "LocalTax				=	@LocalTax, " +
                                "isVATInclusive			=	@isVATInclusive, " +
                                "VariationMatrixID		=	@VariationMatrixID, " +
                                "MatrixDescription		=	@MatrixDescription, " +
                                "ProductGroup			=	@ProductGroup, " +
                                "ProductSubGroup		=	@ProductSubGroup, " +
                                "ItemStatus			    =	@ItemStatus, " +
                                "IsVatable				=	@IsVatable, " +
                                "Remarks				=	@Remarks, " +
                                "ChartOfAccountIDPurchase       = (SELECT ChartOfAccountIDPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDTaxPurchase    = (SELECT ChartOfAccountIDTaxPurchase FROM tblProducts WHERE ProductID = @ProductID), " +
                                "ChartOfAccountIDInventory      = (SELECT ChartOfAccountIDInventory FROM tblProducts WHERE ProductID = @ProductID) " +
                            "WHERE DebitMemoItemID = @DebitMemoItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DebitMemoID", Details.DebitMemoID);
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@BarCode", Details.BarCode);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@ProductUnitID", Details.ProductUnitID);
                cmd.Parameters.AddWithValue("@ProductUnitCode", Details.ProductUnitCode);
                cmd.Parameters.AddWithValue("@Quantity", Details.Quantity);
                cmd.Parameters.AddWithValue("@PrevUnitCost", Details.PrevUnitCost);
                cmd.Parameters.AddWithValue("@UnitCost", Details.UnitCost);
                cmd.Parameters.AddWithValue("@Discount", Details.Discount);
                cmd.Parameters.AddWithValue("@DiscountApplied", Details.DiscountApplied);
                cmd.Parameters.AddWithValue("@DiscountType", (int)Details.DiscountType);
                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@VAT", Details.VAT);
                cmd.Parameters.AddWithValue("@VatableAmount", Details.VatableAmount);
                cmd.Parameters.AddWithValue("@EVAT", Details.EVAT);
                cmd.Parameters.AddWithValue("@EVatableAmount", Details.EVatableAmount);
                cmd.Parameters.AddWithValue("@LocalTax", Details.LocalTax);
                cmd.Parameters.AddWithValue("@isVATInclusive", Convert.ToInt16(Details.isVATInclusive));
                cmd.Parameters.AddWithValue("@VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@ProductGroup", Details.ProductGroup);
                cmd.Parameters.AddWithValue("@ProductSubGroup", Details.ProductSubGroup);
                cmd.Parameters.AddWithValue("@ItemStatus", Details.ItemStatus.ToString("d"));
                cmd.Parameters.AddWithValue("@IsVatable", Convert.ToInt16(Details.IsVatable));
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@DebitMemoItemID", Details.DebitMemoItemID);

                cmd.ExecuteNonQuery();

                DebitMemos clsDebitMemos = new DebitMemos(Connection, Transaction);
                clsDebitMemos.SynchronizeAmount(Details.DebitMemoID);
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public void Post(long DebitMemoID)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemoItems SET " +
                                "ItemStatus			=	@ItemStatus " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ItemStatus", DebitMemoItemStatus.Posted.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public void Cancel(long DebitMemoID)
        {
            try
            {
                string SQL = "UPDATE tblPODebitMemoItems SET " +
                                "ItemStatus			=	@ItemStatus " +
                            "WHERE DebitMemoID = @DebitMemoID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ItemStatus", DebitMemoItemStatus.Cancelled.ToString("d"));
                cmd.Parameters.AddWithValue("@DebitMemoID", DebitMemoID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        #endregion

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                string SQL = "DELETE FROM tblPODebitMemoItems WHERE DebitMemoItemID IN (" + IDs + ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.ExecuteNonQuery();

                return true;
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }


        #endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "DebitMemoItemID, " +
                                "DebitMemoID, " +
                                "ProductID, " +
                                "ProductCode, " +
                                "BarCode, " +
                                "Description, " +
                                "ProductUnitID, " +
                                "ProductUnitCode, " +
                                "Quantity, " +
                                "PrevUnitCost, " +
                                "UnitCost, " +
                                "Discount, " +
                                "DiscountApplied, " +
                                "DiscountType, " +
                                "Amount, " +
                                "VAT, " +
                                "VatableAmount, " +
                                "EVAT, " +
                                "EVatableAmount, " +
                                "LocalTax, " +
                                "isVATInclusive, " +
                                "VariationMatrixID, " +
                                "MatrixDescription, " +
                                "ProductGroup, " +
                                "ProductSubGroup, " +
                                "ItemStatus, " +
                                "IsVatable, " +
                                "Remarks, " +
                                "ChartOfAccountIDPurchase, " +
                                "ChartOfAccountIDTaxPurchase, " +
                                "ChartOfAccountIDInventory " +
                            "FROM tblPODebitMemoItems ";
            return stSQL;
        }

        #region Details

        public DebitMemoItemDetails Details(long DebitMemoItemID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE DebitMemoItemID = @DebitMemoItemID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoItemID = new MySqlParameter("@DebitMemoItemID",MySqlDbType.Int64);
                prmDebitMemoItemID.Value = DebitMemoItemID;
                cmd.Parameters.Add(prmDebitMemoItemID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                DebitMemoItemDetails Details = new DebitMemoItemDetails();

                while (myReader.Read())
                {
                    Details.DebitMemoItemID = DebitMemoItemID;
                    Details.DebitMemoID = myReader.GetInt64("DebitMemoID");
                    Details.ProductID = myReader.GetInt64("ProductID");
                    Details.ProductCode = "" + myReader["ProductCode"].ToString();
                    Details.BarCode = "" + myReader["BarCode"].ToString();
                    Details.Description = "" + myReader["Description"].ToString();
                    Details.ProductUnitID = myReader.GetInt16("ProductUnitID");
                    Details.ProductUnitCode = "" + myReader["ProductUnitCode"].ToString();
                    Details.Quantity = myReader.GetDecimal("Quantity");
                    Details.PrevUnitCost = myReader.GetDecimal("PrevUnitCost");
                    Details.UnitCost = myReader.GetDecimal("UnitCost");
                    Details.Discount = myReader.GetDecimal("Discount");
                    Details.DiscountApplied = myReader.GetDecimal("DiscountApplied");
                    Details.DiscountType = (DiscountTypes)Enum.Parse(typeof(DiscountTypes), myReader.GetString("DiscountType"));
                    Details.Amount = myReader.GetDecimal("Amount");
                    Details.VAT = myReader.GetDecimal("VAT");
                    Details.VatableAmount = myReader.GetDecimal("VatableAmount");
                    Details.EVAT = myReader.GetDecimal("EVAT");
                    Details.EVatableAmount = myReader.GetDecimal("EVatableAmount");
                    Details.LocalTax = myReader.GetDecimal("LocalTax");
                    Details.isVATInclusive = myReader.GetBoolean("isVATInclusive");
                    Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
                    Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
                    Details.ProductGroup = "" + myReader["ProductGroup"].ToString();
                    Details.ProductSubGroup = "" + myReader["ProductSubGroup"].ToString();
                    Details.ItemStatus = (DebitMemoItemStatus)Enum.Parse(typeof(DebitMemoItemStatus), myReader.GetString("ItemStatus"));
                    if (myReader["IsVatable"].ToString() == "1") Details.IsVatable = true;
                    Details.Remarks = "" + myReader["Remarks"].ToString();
                }

                myReader.Close();

                return Details;
            }

            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }


        #endregion

        #region Streams

        public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
        {
            if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            MySqlConnection cn = GetConnection();
            System.Data.DataTable dt = new System.Data.DataTable("tblPODebitMemoItems");
            MySqlDataAdapter adapter = new MySqlDataAdapter(SQL, cn);
            adapter.Fill(dt);

            return dt;

        }

        public MySqlDataReader List(string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

                string SQL = SQLSelect() + "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public MySqlDataReader List(long DebitMemoID, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

                string SQL = SQLSelect() + "WHERE DebitMemoID = @DebitMemoID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoID = new MySqlParameter("@DebitMemoID",MySqlDbType.Int64);
                prmDebitMemoID.Value = DebitMemoID;
                cmd.Parameters.Add(prmDebitMemoID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public MySqlDataReader List(DebitMemoItemStatus DebitMemoItemstatus, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

                string SQL = SQLSelect() + "WHERE ItemStatus = @ItemStatus " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmDebitMemoItemStatus.Value = DebitMemoItemstatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoItemStatus);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                        "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                        "or Remarks LIKE @SearchKey) " +
                                "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        public MySqlDataReader Search(DebitMemoItemStatus DebitMemoItemstatus, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                if (SortField == string.Empty || SortField == null) SortField = "DebitMemoItemID";

                string SQL = SQLSelect() + "WHERE (ProductCode LIKE @SearchKey or BarCode LIKE @SearchKey or Description LIKE @SearchKey " +
                                    "or MatrixDescription LIKE @SearchKey or ProductGroup LIKE @SearchKey or ProductSubGroup LIKE @SearchKey " +
                                    "or Remarks LIKE @SearchKey) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmDebitMemoItemStatus = new MySqlParameter("@ItemStatus",MySqlDbType.Int16);
                prmDebitMemoItemStatus.Value = DebitMemoItemstatus.ToString("d");
                cmd.Parameters.Add(prmDebitMemoItemStatus);

                MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
                prmSearchKey.Value = "%" + SearchKey + "%";
                cmd.Parameters.Add(prmSearchKey);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                TransactionFailed = true;
                if (IsInTransaction)
                {
                    mTransaction.Rollback();
                    mTransaction.Dispose();
                    mConnection.Close();
                    mConnection.Dispose();
                }

                throw ex;
            }
        }

        #endregion
    }
}