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
    public struct DisburseDetails
    {
        public Int64 DisburseID;
        public decimal Amount;
        public PaymentTypes PaymentType;
        public DateTime DateCreated;
        public string TerminalNo;
        public int BranchID;
        public Int64 CashierID;
        public string CashierName;
        public string Remarks;

        public DateTime StartTransactionDate;
        public DateTime EndTransactionDate;
    }

    public struct DisburseColumns
    {
        public bool DisburseID;
        public bool Amount;
        public bool PaymentType;
        public bool DateCreated;
        public bool TerminalNo;
        public bool BranchID;
        public bool CashierID;
        public bool CashierName;
        public bool Remarks;
    }

    public struct DisburseColumnNames
    {
        public const string DisburseID = "DisburseID";
        public const string Amount = "Amount";
        public const string PaymentType = "PaymentType";
        public const string DateCreated = "DateCreated";
        public const string TerminalNo = "TerminalNo";
        public const string BranchID = "BranchID";
        public const string CashierID = "CashierID";
        public const string CashierName = "CashierName";
        public const string Remarks = "Remarks";
    }

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class Disburse
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


        #region Constructors & Destructors

        public Disburse()
        {

        }

        public Disburse(MySqlConnection Connection, MySqlTransaction Transaction)
        {
            this.mConnection = Connection;
            this.mTransaction = Transaction;
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
                IsInTransaction = true;
            }

            return mConnection;
        }


        #endregion

        #region Insert and Update

        public Int64 Insert(DisburseDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblDisburse (" +
                                    "Amount, " +
                                    "PaymentType, " +
                                    "DateCreated, " +
                                    "TerminalNo, " +
                                    "CashierID, " +
                                    "BranchID, " +
                                    "BranchCode, " +
                                    "Remarks " +
                                ")VALUES (" +
                                    "@Amount, " +
                                    "@PaymentType, " +
                                    "@DateCreated, " +
                                    "@TerminalNo, " +
                                    "@CashierID, " +
                                    "@BranchID, " +
                                    "(SELECT BranchCode FROM tblBranch WHERE BranchID = @BranchID), " +
                                    "@Remarks " +
                                ");";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("@DateCreated", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
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

                TerminalReport clsTerminalReport = new TerminalReport(cn, mTransaction);
                clsTerminalReport.UpdateDisburse(Details);

                CashierReport clsCashierReport = new CashierReport(cn, mTransaction);
                clsCashierReport.UpdateDisburse(Details);

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

        public void Update(DisburseDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblDisburse SET " +
                                "Amount			=	@Amount, " +
                                "PaymentType	=	@PaymentType, " +
                                "DateCreated	=	@DateCreated, " +
                                "TerminalNo		=	TerminalNo, " +
                                "CashierID		=	@CashierID, " +
                                "BranchID		=	@BranchID, " +
                                "BranchCode     =   (SELECT BranchCode FROM tblBranch WHERE BranchID = @BranchID), " +
                                "Remarks		=	@Remarks " +
                            "WHERE DisburseID	=	@DisburseID;";


                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("@DateCreated", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@DisburseID", Details.DisburseID);

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
                MySqlConnection cn = GetConnection();
                MySqlCommand cmd;

                string SQL = "DELETE FROM tblDisburse WHERE DisburseID IN (" + IDs + ");";
                cmd = new MySqlCommand();
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

        public string SQLSelect()
        {
            string SQL = "SELECT " +
                            "a.DisburseID, " +
                            "a.Amount, " +
                            "a.PaymentType, " +
                            "a.DateCreated, " +
                            "a.TerminalNo, " +
                            "a.CashierID, " +
                            "b.Name AS CashierName, " +
                            "a.BranchID, " +
                            "a.Remarks " +
                        "FROM tblDisburse a INNER JOIN sysAccessUserDetails b ON a.CashierID=b.UID ";

            return SQL;
        }

        private string SQLSelect(DisburseColumns clsDisburseColumns)
        {
            string stSQL = "SELECT ";

            if (clsDisburseColumns.Amount) stSQL += "tblDisburse.Amount, ";
            if (clsDisburseColumns.PaymentType) stSQL += "tblDisburse.PaymentType, ";
            if (clsDisburseColumns.DateCreated) stSQL += "tblDisburse.DateCreated, ";
            if (clsDisburseColumns.TerminalNo) stSQL += "tblDisburse.TerminalNo, ";
            if (clsDisburseColumns.CashierID) stSQL += "tblDisburse.CashierID, ";
            if (clsDisburseColumns.CashierName) stSQL += "sysAccessUserDetails.Name 'CashierName', ";
            if (clsDisburseColumns.BranchID) stSQL += "tblDisburse.BranchID, ";
            if (clsDisburseColumns.Remarks) stSQL += "tblDisburse.Remarks, ";

            stSQL += "tblDisburse.DisburseID FROM tblDisburse ";

            if (clsDisburseColumns.CashierName)
                stSQL += "INNER JOIN sysAccessUserDetails ON sysAccessUserDetails.UID = tblDisburse.CashierID ";

            return stSQL;
        }

        #region Details

        public DisburseDetails Details(Int32 DisburseID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE DisburseID = @DisburseID;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DisburseID", DisburseID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                DisburseDetails Details = new DisburseDetails();

                while (myReader.Read())
                {
                    Details.DisburseID = myReader.GetInt64("DisburseID");
                    Details.Amount = myReader.GetDecimal("Amount");
                    Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), myReader.GetString("PaymentType"));
                    Details.DateCreated = myReader.GetDateTime("DateCreated");
                    Details.CashierID = myReader.GetInt64("CashierID");
                    Details.CashierName = "" + myReader["CashierName"].ToString();
                    Details.BranchID = myReader.GetInt32("BranchID");
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

        //public MySqlDataReader List(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL =	SQLSelect() + "WHERE 1=1 ORDER BY " + SortField; 

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlConnection cn = GetConnection();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();

        //        return myReader;			
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionFailed = true;
        //        if (IsInTransaction)
        //        {
        //            mTransaction.Rollback();
        //            mTransaction.Dispose(); 
        //            mConnection.Close();
        //            mConnection.Dispose();
        //        }

        //        throw ex;
        //    }	
        //}
        //public MySqlDataReader List(DateTime StartTransactionDate, DateTime EndTransactionDate, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();

        //        string SQL = SQLSelect();

        //        if (StartTransactionDate != DateTime.MinValue)
        //        {
        //            SQL += "AND a.DateCreated >= @StartTransactionDate ";
        //            cmd.Parameters.AddWithValue("@StartTransactionDate", StartTransactionDate);
        //        }
        //        if (EndTransactionDate != DateTime.MinValue)
        //        {
        //            SQL += "AND a.DateCreated <= @EndTransactionDate ";
        //            cmd.Parameters.AddWithValue("@EndTransactionDate", EndTransactionDate);
        //        }

        //        SQL += " ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlConnection cn = GetConnection();
        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;


        //        MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

        //        return myReader;
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionFailed = true;
        //        if (IsInTransaction)
        //        {
        //            mTransaction.Rollback();
        //            mTransaction.Dispose();
        //            mConnection.Close();
        //            mConnection.Dispose();
        //        }

        //        throw ex;
        //    }
        //}
        //public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = SQLSelect() + "WHERE TerminalNo LIKE @SearchKey " +
        //                    "ORDER BY " + SortField; 

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlConnection cn = GetConnection();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
        //        prmSearchKey.Value = "%" + SearchKey + "%";
        //        cmd.Parameters.Add(prmSearchKey);

        //        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();

        //        return myReader;			
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionFailed = true;
        //        if (IsInTransaction)
        //        {
        //            mTransaction.Rollback();
        //            mTransaction.Dispose(); 
        //            mConnection.Close();
        //            mConnection.Dispose();
        //        }

        //        throw ex;
        //    }	
        //}

        public System.Data.DataTable ListAsDataTable(DisburseColumns clsDisburseColumns, DisburseDetails clsSearchKey, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect(clsDisburseColumns) + "WHERE 1=1 ";
                if (clsSearchKey.BranchID != 0)
                {
                    SQL += "AND tblDisburse.BranchID = @BranchID ";
                    MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                    prmBranchID.Value = clsSearchKey.BranchID;
                    cmd.Parameters.Add(prmBranchID);
                }
                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND tblDisburse.CashierID = @CashierID ";
                    MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                    prmCashierID.Value = clsSearchKey.CashierID;
                    cmd.Parameters.Add(prmCashierID);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblDisburse.DateCreated >= @StartTransactionDate ";
                    MySqlParameter prmStartTransactionDate = new MySqlParameter("@StartTransactionDate",MySqlDbType.DateTime);
                    prmStartTransactionDate.Value = clsSearchKey.StartTransactionDate;
                    cmd.Parameters.Add(prmStartTransactionDate);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblDisburse.DateCreated <= @EndTransactionDate ";
                    MySqlParameter prmEndTransactionDate = new MySqlParameter("@EndTransactionDate",MySqlDbType.DateTime);
                    prmEndTransactionDate.Value = clsSearchKey.EndTransactionDate;
                    cmd.Parameters.Add(prmEndTransactionDate);
                }
                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                MySqlConnection cn = GetConnection();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblDisburse");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
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

