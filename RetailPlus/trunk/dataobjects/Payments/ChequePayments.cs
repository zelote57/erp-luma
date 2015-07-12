using System;
using System.Collections;
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
	public struct ChequePaymentDetails
	{
        public BranchDetails BranchDetails;
        public string TerminalNo;
        public Int64 SyncID;
        public Int64 ChequePaymentID;
		public Int64 TransactionID;
        public string TransactionNo;
        public string ChequeNo;
		public decimal Amount;
        public DateTime ValidityDate;
		public string Remarks;

        public DateTime CreatedOn;
        public DateTime LastModified;

        // 09Jul2015 : Added For Searches
        public DateTime ValidityDateFrom;
        public DateTime ValidityDateTo;
        public string CustomerGroupName;
        public string CustomerName;
        public string CashierName;
        public DateTime TransactionDateFrom;
        public DateTime TransactionDateTo;
        public TransactionStatus TransactionStatus;
        public PaymentTypes PaymentType;
        public string AgentName;

	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ChequePayments : POSConnection
    {
		#region Constructors and Destructors

		public ChequePayments()
            : base(null, null)
        {
        }

        public ChequePayments(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(ChequePaymentDetails Details)
		{
			try  
			{
                Save(Details);

                return Int64.Parse(base.getLAST_INSERT_ID(this));
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ChequePaymentDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveChequePayment(@BranchID, @TerminalNo, @SyncID, @ChequePaymentID, @TransactionID, @ChequeNo, @Amount, @ValidityDate, @Remarks, @TransactionNo, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("ChequePaymentID", Details.ChequePaymentID);
                cmd.Parameters.AddWithValue("TransactionID", Details.TransactionID);
                cmd.Parameters.AddWithValue("ChequeNo", Details.ChequeNo);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("ValidityDate", Details.ValidityDate);
                cmd.Parameters.AddWithValue("Remarks", Details.Remarks);
                cmd.Parameters.AddWithValue("TransactionNo", Details.TransactionNo);
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

                string SQL = "DELETE FROM tblChequePayment WHERE PaymentID IN (" + IDs + ");";
	 			
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

        private string SQLSelect(SQLSelectType SQLSelectType = SQLSelectType.SQLForList)
        {
            string stSQL = "";

            switch (SQLSelectType)
            {
                case SQLSelectType.SQLForReport:
                    stSQL = "SELECT chq.BranchID, brnch.BranchCode, chq.TerminalNo, chq.SyncID, chq.ChequePaymentID, chq.TransactionID, chq.ChequeNo, chq.Amount, chq.ValidityDate, chq.Remarks, chq.TransactionNo, chq.CreatedOn, chq.LastModified, trx.CustomerID, trx.CustomerName, trx.CashierName, trx.AgentName " +
                             "FROM tblChequePayment chq " +
                             "  INNER JOIN tblBranch brnch ON chq.BranchID = brnch.BranchID " +
                             "  INNER JOIN tblTransactions trx ON trx.BranchID = chq.BranchID AND trx.TerminalNo = chq.TerminalNo AND chq.TransactionID = trx.TransactionID ";
                    break;
                case SQLSelectType.SQLForList:
                default:
                    stSQL = "SELECT chq.BranchID, chq.TerminalNo, chq.SyncID, chq.ChequePaymentID, chq.TransactionID, chq.ChequeNo, chq.Amount, chq.ValidityDate, chq.Remarks, chq.TransactionNo, chq.CreatedOn, chq.LastModified " +
                             "FROM tblChequePayment chq ";
                    break;
            }

            return stSQL;
        }

		#region Details

        /// <summary>
        /// Get the Cheque Payment details of a certain Transaction.
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="TerminalNo"></param>
        /// <param name="SyncID">sabme as the TransactionID</param>
        /// <returns></returns>
        public ChequePaymentDetails[] Details(Int32 BranchID, string TerminalNo, Int64 TransactionID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE BranchID = @BranchID AND TerminalNo = @TerminalNo AND TransactionID = @TransactionID;";

                cmd.Parameters.AddWithValue("BranchID", BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", TerminalNo);
                cmd.Parameters.AddWithValue("TransactionID", TransactionID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                ArrayList items = new ArrayList();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    items.Add(setDetails(dr));
                }

                ChequePaymentDetails[] arrCashDetails = new ChequePaymentDetails[0];

                if (items != null)
                {
                    arrCashDetails = new ChequePaymentDetails[items.Count];
                    items.CopyTo(arrCashDetails);
                }

                return arrCashDetails;
            }

            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public ChequePaymentDetails setDetails(System.Data.DataRow dr)
        {
            Data.ChequePaymentDetails Details = new Data.ChequePaymentDetails();

            Details.BranchDetails = new Branch(base.Connection, base.Transaction).Details(Int32.Parse(dr["BranchID"].ToString()));
            Details.TerminalNo = dr["TerminalNo"].ToString();
            Details.SyncID = Int64.Parse(dr["SyncID"].ToString());
            Details.ChequePaymentID = Int64.Parse(dr["ChequePaymentID"].ToString());
            Details.TransactionID = Int64.Parse(dr["TransactionID"].ToString());
            Details.Amount = decimal.Parse(dr["Amount"].ToString());
            Details.Remarks = dr["Remarks"].ToString();
            Details.TransactionNo = dr["TransactionNo"].ToString();

            Details.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
            Details.LastModified = DateTime.Parse(dr["LastModified"].ToString());

            return Details;
        }

		#endregion
		
		#region Streams

        public System.Data.DataTable List(ChequePaymentDetails SearchKeys = new ChequePaymentDetails(), string SortField = "ChequePaymentID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                #region Search
                if (SearchKeys.BranchDetails.BranchID != 0)
                {
                    SQL += "AND BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("@BranchID", SearchKeys.BranchDetails.BranchID);
                }
                if (!string.IsNullOrEmpty(SearchKeys.TerminalNo))
                {
                    SQL += "AND TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("@TerminalNo", SearchKeys.TerminalNo);
                }
                if (!string.IsNullOrEmpty(SearchKeys.TransactionNo))
                {
                    SQL += "AND TransactionNo = @TransactionNo ";
                    cmd.Parameters.AddWithValue("@TransactionNo", SearchKeys.TransactionNo);
                }
                if (!string.IsNullOrEmpty(SearchKeys.ChequeNo))
                {
                    SQL += "AND ChequeNo = @ChequeNo ";
                    cmd.Parameters.AddWithValue("@ChequeNo", SearchKeys.ChequeNo);
                }
                if (SearchKeys.ValidityDateFrom != DateTime.MinValue)
                {
                    SQL += "AND ValidityDate >= @ValidityDateFrom ";
                    cmd.Parameters.AddWithValue("@ValidityDateFrom", SearchKeys.ValidityDateFrom);
                }
                if (SearchKeys.ValidityDateTo != DateTime.MinValue)
                {
                    SQL += "AND ValidityDate <= @ValidityDateTo ";
                    cmd.Parameters.AddWithValue("@ValidityDateTo", SearchKeys.ValidityDateTo);
                }
                #endregion

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ChequePaymentID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
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

        public System.Data.DataTable ListAsReport(ChequePaymentDetails SearchKeys = new ChequePaymentDetails(), string SortField = "ChequePaymentID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect(SQLSelectType.SQLForReport) + "WHERE 1=1 ";

                #region Search
                if (SearchKeys.BranchDetails.BranchID != 0)
                {
                    SQL += "AND chq.BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("@BranchID", SearchKeys.BranchDetails.BranchID);
                }
                if (!string.IsNullOrEmpty(SearchKeys.TerminalNo))
                {
                    SQL += "AND chq.TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("@TerminalNo", SearchKeys.TerminalNo);
                }
                if (!string.IsNullOrEmpty(SearchKeys.TransactionNo))
                {
                    SQL += "AND chq.TransactionNo = @TransactionNo ";
                    cmd.Parameters.AddWithValue("@TransactionNo", SearchKeys.TransactionNo);
                }
                if (!string.IsNullOrEmpty(SearchKeys.CustomerName))
                {
                    SQL += "AND trx.CustomerName = @CustomerName ";
                    cmd.Parameters.AddWithValue("@CustomerName", SearchKeys.CustomerName);
                }
                if (!string.IsNullOrEmpty(SearchKeys.CustomerGroupName))
                {
                    SQL += "AND trx.CustomerGroupName = @CustomerGroupName ";
                    cmd.Parameters.AddWithValue("@CustomerGroupName", SearchKeys.CustomerGroupName);
                }
                if (!string.IsNullOrEmpty(SearchKeys.AgentName))
                {
                    SQL += "AND trx.AgentName = @AgentName ";
                    cmd.Parameters.AddWithValue("@AgentName", SearchKeys.AgentName);
                }
                if (!string.IsNullOrEmpty(SearchKeys.AgentName))
                {
                    SQL += "AND trx.AgentName = @AgentName ";
                    cmd.Parameters.AddWithValue("@AgentName", SearchKeys.AgentName);
                }
                if (!string.IsNullOrEmpty(SearchKeys.ChequeNo))
                {
                    SQL += "AND chq.ChequeNo = @ChequeNo ";
                    cmd.Parameters.AddWithValue("@ChequeNo", SearchKeys.ChequeNo);
                }
                if (SearchKeys.ValidityDateFrom != DateTime.MinValue)
                {
                    SQL += "AND chq.ValidityDate >= @ValidityDateFrom ";
                    cmd.Parameters.AddWithValue("@ValidityDateFrom", SearchKeys.ValidityDateFrom);
                }
                if (SearchKeys.ValidityDateTo != DateTime.MinValue)
                {
                    SQL += "AND chq.ValidityDate <= @ValidityDateTo ";
                    cmd.Parameters.AddWithValue("@ValidityDateTo", SearchKeys.ValidityDateTo);
                }
                if (SearchKeys.ValidityDateFrom != DateTime.MinValue)
                {
                    SQL += "AND trx.TransactionDate >= @TransactionDateFrom ";
                    cmd.Parameters.AddWithValue("@TransactionDateFrom", SearchKeys.TransactionDateFrom);
                }
                if (SearchKeys.ValidityDateTo != DateTime.MinValue)
                {
                    SQL += "AND trx.TransactionDate <= @TransactionDateTo ";
                    cmd.Parameters.AddWithValue("@TransactionDateTo", SearchKeys.TransactionDateTo);
                }

                #endregion

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "ChequePaymentID") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";
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

