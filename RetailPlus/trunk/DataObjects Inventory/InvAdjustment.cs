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

    #region InvAdjustmentDetails

    public struct InvAdjustmentDetails
    {
        public long InvAdjustmentID;
        public long UID;
        public DateTime InvAdjustmentDate;
        public long ProductID;
        public string ProductCode;
        public string Description;
        public long VariationMatrixID;
        public string MatrixDescription;
        public int UnitID;
        public string UnitCode;
        public decimal QuantityBefore;
        public decimal QuantityNow;
        public decimal MinThresholdBefore;
        public decimal MinThresholdNow;
        public decimal MaxThresholdBefore;
        public decimal MaxThresholdNow;
        public string Remarks;
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
    public class InvAdjustment
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

        public InvAdjustment()
        {

        }

        public InvAdjustment(MySqlConnection Connection, MySqlTransaction Transaction)
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

        public void Insert(InvAdjustmentDetails Details)
        {
            try
            {
                string SQL = "CALL procInvAdjustmentInsert(@UID, @InvAdjustmentDate, @ProductID, @ProductCode, @Description, @VariationMatrixID, " +
                                                        "@MatrixDescription, @UnitID, @UnitCode, @QuantityBefore, @QuantityNow, " +
                                                        "@MinThresholdBefore, @MinThresholdNow, @MaxThresholdBefore, @MaxThresholdNow, @Remarks);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@UID", Details.UID);
                cmd.Parameters.AddWithValue("@InvAdjustmentDate", Details.InvAdjustmentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("@ProductCode", Details.ProductCode);
                cmd.Parameters.AddWithValue("@Description", Details.Description);
                cmd.Parameters.AddWithValue("@VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("@MatrixDescription", Details.MatrixDescription);
                cmd.Parameters.AddWithValue("@UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("@UnitCode", Details.UnitCode);
                cmd.Parameters.AddWithValue("@QuantityBefore", Details.QuantityBefore);
                cmd.Parameters.AddWithValue("@QuantityNow", Details.QuantityNow);
                cmd.Parameters.AddWithValue("@MinThresholdBefore", Details.MinThresholdBefore);
                cmd.Parameters.AddWithValue("@MinThresholdNow", Details.MinThresholdNow);
                cmd.Parameters.AddWithValue("@MaxThresholdBefore", Details.MaxThresholdBefore);
                cmd.Parameters.AddWithValue("@MaxThresholdNow", Details.MaxThresholdNow);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

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
                string SQL = "DELETE FROM tblInvAdjustment WHERE InvAdjustmentID IN (" + IDs + ");";

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

        public bool DeleteMainProduct(long ProductID)
        {
            try
            {
                string SQL = "DELETE FROM tblInvAdjustment WHERE ProductID = @ProductID AND VariationMatrixID = 0 AND Remarks = 'newly added. beginning balance.';";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

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
                                "a.InvAdjustmentID, " +
                                "a.UID, " +
                                "e.Name, " +
                                "a.InvAdjustmentDate, "+
                                "a.ProductID, " +
                                "a.ProductCode, " +
                                "a.Description, " +
                                "a.VariationMatrixID, " +
                                "a.MatrixDescription, " +
                                "a.UnitID, " +
                                "a.UnitCode, " +
                                "a.QuantityBefore, " +
                                "a.QuantityNow, " +
                                "a.MinThresholdBefore, " +
                                "a.MinThresholdNow, " +
                                "a.MaxThresholdBefore, " +
                                "a.MaxThresholdNow " +
                            "FROM tblInvAdjustment a " +
                            "INNER JOIN sysAccessUserDetails e ON a.UID = e.UID ";

            return stSQL;
        }

        #region Streams

        public System.Data.DataTable List(DateTime StartChangeDate, DateTime EndChangeDate, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.InvAdjustmentDate >= @StartChangeDate " +
                                            "AND a.InvAdjustmentDate <= @EndChangeDate " +
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

                cmd.Parameters.AddWithValue("@StartChangeDate", StartChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndChangeDate", EndChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));

                System.Data.DataTable dtRetValue = new System.Data.DataTable("InvAdjustments");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtRetValue);

                return dtRetValue;

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
        public System.Data.DataTable List(long ProductID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE b.ProductID = @ProductID ORDER BY " + SortField;

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

                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                System.Data.DataTable dtRetValue = new System.Data.DataTable("InvAdjustments");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtRetValue);

                return dtRetValue;

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
        public System.Data.DataTable List(long ProductID, DateTime StartChangeDate, DateTime EndChangeDate, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE b.ProductID = @ProductID " +
                                            "AND a.InvAdjustmentDate >= @StartChangeDate " +
                                            "AND a.InvAdjustmentDate <= @EndChangeDate " +
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

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@StartChangeDate", StartChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndChangeDate", EndChangeDate.ToString("yyyy-MM-dd HH:mm:ss"));

                System.Data.DataTable dtRetValue = new System.Data.DataTable("InvAdjustments");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dtRetValue);

                return dtRetValue;

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