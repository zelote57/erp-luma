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
    public struct PaidOutDetails
    {
        public string TerminalNo;
        public Int64 SyncID;
        public Int64 PaidOutID;
        public decimal Amount;
        public PaymentTypes PaymentType;
        public DateTime DateCreated;
        public Int64 CashierID;
        public string CashierName;
        public string Remarks;

        public DateTime StartTransactionDate;
        public DateTime EndTransactionDate;

        public BranchDetails BranchDetails;
        public DateTime CreatedOn;
        public DateTime LastModified;
    }

    public struct PaidOutColumns
    {
        public bool PaidOutID;
        public bool Amount;
        public bool PaymentType;
        public bool DateCreated;
        public bool TerminalNo;
        public bool BranchDetails;
        public bool CashierID;
        public bool CashierName;
        public bool Remarks;
    }

    public struct PaidOutColumnNames
    {
        public const string PaidOutID = "PaidOutID";
        public const string Amount = "Amount";
        public const string PaymentType = "PaymentType";
        public const string DateCreated = "DateCreated";
        public const string TerminalNo = "TerminalNo";
        public const string BranchDetails = "BranchDetails";
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
    public class PaidOut : POSConnection
    {
		#region Constructors and Destructors

		public PaidOut()
            : base(null, null)
        {
        }

        public PaidOut(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public Int64 Insert(PaidOutDetails Details)
        {
            try
            {
                Save(Details);

                Int64 iID = Int64.Parse(base.getLAST_INSERT_ID(this));
                
                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
                clsTerminalReport.UpdatePaidOut(Details);

                CashierReports clsCashierReport = new CashierReports(base.Connection, base.Transaction);
                clsCashierReport.UpdatePaidOut(Details);

                return iID;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public void Update(PaidOutDetails Details)
        {
            try
            {
                Save(Details);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public Int32 Save(PaidOutDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSavePaidOut(@BranchID, @TerminalNo, @SyncID, @DepositID, @Amount, @PaymentType, @DateCreated, " +
                                                  "@CashierID, @Remarks, @BranchCode, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("DepositID", Details.PaidOutID);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("BranchCode", Details.BranchDetails.BranchCode);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                cmd.CommandText = SQL;
                return base.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

        #region Delete

        public bool Delete(string IDs)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "DELETE FROM tblPaidOut WHERE PaidOutID IN (" + IDs + ");";
                
                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return true;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }


        #endregion

        public string SQLSelect()
        {
            string SQL = "SELECT " +
                            "a.PaidOutID, " +
                            "a.Amount, " +
                            "a.PaymentType, " +
                            "a.DateCreated, " +
                            "a.TerminalNo, " +
                            "a.CashierID, " +
                            "b.Name AS CashierName, " +
                            "a.BranchID, " +
                            "a.Remarks " +
                        "FROM tblPaidOut a INNER JOIN sysAccessUserDetails b ON a.CashierID=b.UID ";

            return SQL;
        }

        private string SQLSelect(PaidOutColumns clsPaidOutColumns)
        {
            string stSQL = "SELECT ";

            if (clsPaidOutColumns.Amount) stSQL += "tblPaidOut.Amount, ";
            if (clsPaidOutColumns.PaymentType) stSQL += "tblPaidOut.PaymentType, ";
            if (clsPaidOutColumns.DateCreated) stSQL += "tblPaidOut.DateCreated, ";
            if (clsPaidOutColumns.TerminalNo) stSQL += "tblPaidOut.TerminalNo, ";
            if (clsPaidOutColumns.CashierID) stSQL += "tblPaidOut.CashierID, ";
            if (clsPaidOutColumns.CashierName) stSQL += "sysAccessUserDetails.Name 'CashierName', ";
            if (clsPaidOutColumns.BranchDetails) stSQL += "tblPaidOut.BranchID, ";
            if (clsPaidOutColumns.Remarks) stSQL += "tblPaidOut.Remarks, ";

            stSQL += "tblPaidOut.PaidOutID FROM tblPaidOut ";

            if (clsPaidOutColumns.CashierName)
                stSQL += "INNER JOIN sysAccessUserDetails ON sysAccessUserDetails.UID = tblPaidOut.CashierID ";

            return stSQL;
        }

        #region Details

        public PaidOutDetails Details(Int64 PaidOutID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE PaidOutID = @PaidOutID;";

                cmd.Parameters.AddWithValue("@PaidOutID", PaidOutID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                PaidOutDetails Details = new PaidOutDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.PaidOutID = Int64.Parse(dr["PaidOutID"].ToString());
                    Details.Amount = decimal.Parse(dr["Amount"].ToString());
                    Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString());
                    Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.CashierID = Int64.Parse(dr["CashierID"].ToString());
                    Details.CashierName = dr["CashierName"].ToString();
                    Details.BranchDetails = new Branch(base.Connection, base.Transaction).Details(Int32.Parse(dr["BranchID"].ToString()));
                    Details.Remarks = dr["Remarks"].ToString();
                }

                return Details;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
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

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        

        //        return base.ExecuteReader(cmd);			
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

        //        
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;


        //        

        //        return base.ExecuteReader(cmd);
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

        //        

        //        MySqlCommand cmd = new MySqlCommand();
        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;

        //        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
        //        prmSearchKey.Value = "%" + SearchKey + "%";
        //        cmd.Parameters.Add(prmSearchKey);

        //        

        //        return base.ExecuteReader(cmd);			
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

        public System.Data.DataTable ListAsDataTable(PaidOutColumns clsPaidOutColumns, PaidOutDetails clsSearchKey, string SortField = "DateCreated", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect(clsPaidOutColumns) + "WHERE 1=1 ";
                if (clsSearchKey.BranchDetails.BranchID != 0)
                {
                    SQL += "AND tblPaidOut.BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("BranchID", clsSearchKey.BranchDetails.BranchID);
                }
                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND tblPaidOut.CashierID = @CashierID ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblPaidOut.DateCreated >= @StartTransactionDate ";
                    cmd.Parameters.AddWithValue("StartTransactionDate", clsSearchKey.StartTransactionDate);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblPaidOut.DateCreated <= @EndTransactionDate ";
                    cmd.Parameters.AddWithValue("EndTransactionDate", clsSearchKey.EndTransactionDate);
                }

                SQL += "ORDER BY " + SortField + " ";
                SQL += SortOrder == System.Data.SqlClient.SortOrder.Ascending ? "ASC " : "DESC ";
                SQL += limit == 0 ? "" : "LIMIT " + limit.ToString() + " ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion
    }
}

