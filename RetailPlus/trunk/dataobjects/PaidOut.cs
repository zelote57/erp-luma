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
        public Int64 PaidOutID;
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

    public struct PaidOutColumns
    {
        public bool PaidOutID;
        public bool Amount;
        public bool PaymentType;
        public bool DateCreated;
        public bool TerminalNo;
        public bool BranchID;
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
                string SQL = "INSERT INTO tblPaidOut (" +
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

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("@DateCreated", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
                clsTerminalReport.UpdatePaidOut(Details);

                CashierReport clsCashierReport = new CashierReport(base.Connection, base.Transaction);
                clsCashierReport.UpdatePaidOut(Details);

                return iID;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }

        public void Update(PaidOutDetails Details)
        {
            try
            {
                string SQL = "UPDATE tblPaidOut SET " +
                                "Amount			=	@Amount, " +
                                "PaymentType	=	@PaymentType, " +
                                "DateCreated	=	@DateCreated, " +
                                "TerminalNo		=	TerminalNo, " +
                                "CashierID		=	@CashierID, " +
                                "BranchID		=	@BranchID, " +
                                "BranchCode     =   (SELECT BranchCode FROM tblBranch WHERE BranchID = @BranchID), " +
                                "Remarks		=	@Remarks " +
                            "WHERE PaidOutID	=	@PaidOutID;";


                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@Amount", Details.Amount);
                cmd.Parameters.AddWithValue("@PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("@DateCreated", Details.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("@CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("@BranchID", Details.BranchID);
                cmd.Parameters.AddWithValue("@Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("@PaidOutID", Details.PaidOutID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
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
                
                MySqlCommand cmd;

                string SQL = "DELETE FROM tblPaidOut WHERE PaidOutID IN (" + IDs + ");";
                cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                base.ExecuteNonQuery(cmd);

                return true;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
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
            if (clsPaidOutColumns.BranchID) stSQL += "tblPaidOut.BranchID, ";
            if (clsPaidOutColumns.Remarks) stSQL += "tblPaidOut.Remarks, ";

            stSQL += "tblPaidOut.PaidOutID FROM tblPaidOut ";

            if (clsPaidOutColumns.CashierName)
                stSQL += "INNER JOIN sysAccessUserDetails ON sysAccessUserDetails.UID = tblPaidOut.CashierID ";

            return stSQL;
        }

        #region Details

        public PaidOutDetails Details(Int32 PaidOutID)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE PaidOutID = @PaidOutID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PaidOutID", PaidOutID);

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

                PaidOutDetails Details = new PaidOutDetails();

                while (myReader.Read())
                {
                    Details.PaidOutID = myReader.GetInt64("PaidOutID");
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
                
                
                {
                    
                    
                    
                    
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

        //        throw ex;
        //    }	
        //}

        public System.Data.DataTable ListAsDataTable(PaidOutColumns clsPaidOutColumns, PaidOutDetails clsSearchKey, int Limit, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                string SQL = SQLSelect(clsPaidOutColumns) + "WHERE 1=1 ";
                if (clsSearchKey.BranchID != 0)
                {
                    SQL += "AND tblPaidOut.BranchID = @BranchID ";
                    MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                    prmBranchID.Value = clsSearchKey.BranchID;
                    cmd.Parameters.Add(prmBranchID);
                }
                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND tblPaidOut.CashierID = @CashierID ";
                    MySqlParameter prmCashierID = new MySqlParameter("@CashierID",MySqlDbType.Int64);
                    prmCashierID.Value = clsSearchKey.CashierID;
                    cmd.Parameters.Add(prmCashierID);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblPaidOut.DateCreated >= @StartTransactionDate ";
                    MySqlParameter prmStartTransactionDate = new MySqlParameter("@StartTransactionDate",MySqlDbType.DateTime);
                    prmStartTransactionDate.Value = clsSearchKey.StartTransactionDate;
                    cmd.Parameters.Add(prmStartTransactionDate);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblPaidOut.DateCreated <= @EndTransactionDate ";
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

                
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblPaidOut");
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }

        #endregion
    }
}

