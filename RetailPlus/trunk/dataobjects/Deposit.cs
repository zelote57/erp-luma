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
	public struct DepositDetails
	{
        public string TerminalNo;
        public Int64 SyncID;
		public Int64 DepositID;
		public decimal Amount;
		public PaymentTypes PaymentType;
		public DateTime DateCreated;
		public Int64 CashierID;
        public string CashierName;
        public Int64 ContactID;
        public string ContactName;
        public string Remarks;

        public DateTime StartTransactionDate;
        public DateTime EndTransactionDate;

        public BranchDetails BranchDetails; 
        public DateTime CreatedOn;
        public DateTime LastModified;
	}

    public struct DepositColumns
    {
        public bool DepositID;
        public bool Amount;
        public bool PaymentType;
        public bool DateCreated;
        public bool TerminalNo;
        public bool BranchDetails;
        public bool CashierID;
        public bool CashierName;
        public bool ContactID;
        public bool ContactName;
        public bool Remarks;
    }

    public struct DepositColumnNames
    {
        public const string DepositID = "DepositID";
        public const string Amount = "Amount";
        public const string PaymentType = "PaymentType";
        public const string DateCreated = "DateCreated";
        public const string TerminalNo = "TerminalNo";
        public const string BranchDetails = "BranchDetails";
        public const string CashierID = "CashierID";
        public const string CashierName = "CashierName";
        public const string ContactID = "ContactID";
        public const string ContactName = "ContactName";
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
	public class Deposits : POSConnection
    {
		#region Constructors and Destructors

		public Deposits()
            : base(null, null)
        {
        }

        public Deposits(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(DepositDetails Details)
		{
			try 
			{
                Save(Details);

                Int64 iID = Int64.Parse(base.getLAST_INSERT_ID(this));

                TerminalReport clsTerminalReport = new TerminalReport(base.Connection, base.Transaction);
				clsTerminalReport.UpdateDeposit(Details);

				CashierReports clsCashierReport = new CashierReports(base.Connection, base.Transaction);
				clsCashierReport.UpdateDeposit(Details);

				return iID;
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(DepositDetails Details)
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

        public Int32 Save(DepositDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveDeposit(@BranchID, @TerminalNo, @SyncID, @DepositID, @Amount, @PaymentType, @DateCreated, " +
                                                  "@CashierID, @ContactID, @Remarks, @BranchCode, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("DepositID", Details.DepositID);
                cmd.Parameters.AddWithValue("Amount", Details.Amount);
                cmd.Parameters.AddWithValue("PaymentType", Details.PaymentType.ToString("d"));
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("ContactID", Details.ContactID);
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

				string SQL=	"DELETE FROM tblDeposit WHERE DepositID IN (" + IDs + ");";
				
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
                            "a.DepositID, " +
                            "a.Amount, " +
                            "a.PaymentType, " +
                            "a.DateCreated, " +
                            "a.TerminalNo, " +
                            "a.CashierID, " +
                            "b.Name AS CashierName, " +
                            "a.ContactID, " +
                            "c.ContactName, " +
                            "a.BranchID, " +
                            "a.BranchCode, " +
                            "a.Remarks " +
                        "FROM tblDeposit a INNER JOIN sysAccessUserDetails b ON a.CashierID=b.UID " +
                            "INNER JOIN tblContacts c ON a.ContactID = c.ContactID ";

            return SQL;
        }

        private string SQLSelect(DepositColumns clsDepositColumns)
        {
            string stSQL = "SELECT ";

            if (clsDepositColumns.Amount) stSQL += "tblDeposit.Amount, ";
            if (clsDepositColumns.PaymentType) stSQL += "tblDeposit.PaymentType, ";
            if (clsDepositColumns.DateCreated) stSQL += "tblDeposit.DateCreated, ";
            if (clsDepositColumns.TerminalNo) stSQL += "tblDeposit.TerminalNo, ";
            if (clsDepositColumns.CashierID) stSQL += "tblDeposit.CashierID, ";
            if (clsDepositColumns.CashierName) stSQL += "sysAccessUserDetails.Name 'CashierName', ";
            if (clsDepositColumns.BranchDetails) stSQL += "tblDeposit.BranchID, ";
            if (clsDepositColumns.Remarks) stSQL += "tblDeposit.Remarks, ";

            stSQL += "tblDeposit.DepositID FROM tblDeposit ";

            if (clsDepositColumns.CashierName)
                stSQL += "INNER JOIN sysAccessUserDetails ON sysAccessUserDetails.UID = tblDeposit.CashierID ";

            if (clsDepositColumns.ContactName)
                stSQL += "INNER JOIN tblContacts c ON tblContacts.ContactID = tblDeposit.ContactID  ";

            return stSQL;
        }

		#region Details

		public DepositDetails Details(Int64 DepositID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE DepositID = @DepositID;";

                cmd.Parameters.AddWithValue("@DepositID", DepositID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                DepositDetails Details = new DepositDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.DepositID = Int64.Parse(dr["DepositID"].ToString());
                    Details.Amount = decimal.Parse(dr["Amount"].ToString());
                    Details.PaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), dr["PaymentType"].ToString());
                    Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.CashierID = Int64.Parse(dr["CashierID"].ToString());
                    Details.CashierName = dr["CashierName"].ToString();
                    Details.ContactID = Int64.Parse(dr["ContactID"].ToString());
                    Details.ContactName = dr["ContactName"].ToString();
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

        public System.Data.DataTable ListAsDataTable(DepositColumns clsDepositColumns, DepositDetails clsSearchKey, string SortField = "DepositID", System.Data.SqlClient.SortOrder SortOrder = System.Data.SqlClient.SortOrder.Ascending, Int32 limit = 0)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect(clsDepositColumns) + "WHERE 1=1 ";

                if (clsSearchKey.BranchDetails.BranchID != 0)
                {
                    SQL += "AND tblDeposit.BranchID = @BranchID ";
                    cmd.Parameters.AddWithValue("BranchID", clsSearchKey.BranchDetails.BranchID);
                }
                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND tblDeposit.CashierID = @CashierID ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }
                if (clsSearchKey.ContactID != 0)
                {
                    SQL += "AND tblDeposit.ContactID = @ContactID ";
                    cmd.Parameters.AddWithValue("ContactID", clsSearchKey.ContactID);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblDeposit.DateCreated >= @StartTransactionDate ";
                    cmd.Parameters.AddWithValue("StartTransactionDate", clsSearchKey.StartTransactionDate);
                }
                if (clsSearchKey.StartTransactionDate != DateTime.MinValue)
                {
                    SQL += "AND tblDeposit.DateCreated <= @EndTransactionDate ";
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

