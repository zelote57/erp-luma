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
	public struct CashCountDetails
	{
        public BranchDetails BranchDetails;
        public string TerminalNo;
        public Int64 SyncID;
		public Int64 CashCountID;
		public Int64 CashierID;
		public string CashierName;
		public DateTime DateCreated;
        public DenominationDetails DenominationDetails;
        
        /// <summary>
        /// DenominationValue saved in the cashcount to check if it's the same in the DenominationDetails
        /// </summary>
        public decimal DenominationValue;
		public Int32 DenominationCount;
		public decimal DenominationAmount;
        
        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CashCounts : POSConnection
    {
		#region Constructors and Destructors

		public CashCounts()
            : base(null, null)
        {
        }

        public CashCounts(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		private Int64 Insert(CashCountDetails Details)
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

		public void Insert(CashCountDetails[] Details)
		{
			try 
			{
				if (Details.Length > 0)
				{
                    decimal Amount = 0;
					foreach(CashCountDetails details in Details)
					{
						Insert(details);	
						Amount += details.DenominationAmount;
					}
					CashierReports clsCashierReport = new CashierReports(base.Connection, base.Transaction);
                    clsCashierReport.UpdateCashCount(Details[0].BranchDetails.BranchID, Details[0].CashierID, Details[0].TerminalNo, Amount);
				}
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(CashCountDetails Details)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procSaveCashCount(@BranchID, @TerminalNo, @SyncID, @CashCountID, @CashierID, @CashierName, @DateCreated, @DenominationID, @DenominationValue, @DenominationCount, @DenominationAmount, @BranchCode, @CreatedOn, @LastModified);";

                cmd.Parameters.AddWithValue("BranchID", Details.BranchDetails.BranchID);
                cmd.Parameters.AddWithValue("TerminalNo", Details.TerminalNo);
                cmd.Parameters.AddWithValue("SyncID", Details.SyncID);
                cmd.Parameters.AddWithValue("CashCountID", Details.CashCountID);
                cmd.Parameters.AddWithValue("CashierID", Details.CashierID);
                cmd.Parameters.AddWithValue("CashierName", Details.CashierName);
                cmd.Parameters.AddWithValue("DateCreated", Details.DateCreated);
                cmd.Parameters.AddWithValue("DenominationID", Details.DenominationDetails.DenominationID);
                cmd.Parameters.AddWithValue("DenominationValue", Details.DenominationDetails.DenominationValue);
                cmd.Parameters.AddWithValue("DenominationCount", Details.DenominationCount);
                cmd.Parameters.AddWithValue("DenominationAmount", Details.DenominationAmount);
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

				string SQL=	"DELETE FROM tblCashCount WHERE CashCountID IN (" + IDs + ");";
				
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

        private string SQLSelect()
        {
            string stSQL = "SELECT CashCountID, " +
                                "CashierID, " +
                                "CashierName, " +
                                "BranchID," +
                                "BranchCode," +
                                "TerminalNo," +
                                "DateCreated, " +
                                "a.DenominationID, " +
                                "DenominationCode, " +
                                "a.DenominationValue, " +
                                "ImagePath, " +
                                "DenominationCount, " +
                                "a.CreatedOn, " +
                                "a.LastModified " +
                            "FROM tblCashCount a " +
                            "INNER JOIN tblDenomination b ON a.DenominationID = b.DenominationID ";

            return stSQL;
        }

		#region Details

		public CashCountDetails Details(Int64 CashCountID)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
				
				string SQL=	SQLSelect() + "WHERE CashCountID = @CashCountID ";

                cmd.Parameters.AddWithValue("@CashCountID", CashCountID);

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				CashCountDetails Details = new CashCountDetails();
                foreach (System.Data.DataRow dr in dt.Rows)
				{
                    Details.BranchDetails = new Branch(base.Connection, base.Transaction).Details(Int32.Parse(dr["BranchID"].ToString()));
                    Details.TerminalNo = dr["TerminalNo"].ToString();
                    Details.SyncID = Int64.Parse(dr["SyncID"].ToString());
                    Details.CashCountID = Int64.Parse(dr["CashCountID"].ToString());
					Details.CashierID = Int64.Parse(dr["CashierID"].ToString());
					Details.CashierName = dr["CashierName"].ToString();
					Details.DateCreated = DateTime.Parse(dr["DateCreated"].ToString());
                    Details.DenominationDetails = new Denominations(base.Connection, base.Transaction).Details(Int32.Parse(dr["DenominationID"].ToString()));
                    Details.DenominationValue = decimal.Parse(dr["DenominationValue"].ToString());
                    Details.DenominationCount = Int32.Parse(dr["DenominationCount"].ToString());
                    Details.DenominationAmount = Int32.Parse(dr["DenominationAmount"].ToString());
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

        public System.Data.DataTable ListAsDataTable(CashCountDetails clsSearchKey, string SortField = "CashCountID", SortOption SortOrder = SortOption.Ascending, Int32 limit = 0)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (clsSearchKey.CashierID != 0)
                {
                    SQL += "AND CashierID = @CashierID ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }
                if (!string.IsNullOrEmpty(clsSearchKey.TerminalNo))
                {
                    SQL += "AND TerminalNo = @TerminalNo ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }
                if (!string.IsNullOrEmpty(clsSearchKey.CashierName))
                {
                    SQL += "AND CashierName = @CashierName ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }
                if (!string.IsNullOrEmpty(clsSearchKey.DenominationDetails.DenominationCode))
                {
                    SQL += "AND DenominationCode = @DenominationCode ";
                    cmd.Parameters.AddWithValue("CashierID", clsSearchKey.CashierID);
                }

                SQL += "ORDER BY " + SortField + " ";
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

