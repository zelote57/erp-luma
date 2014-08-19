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
	public struct AccountSummaryDetails
	{
		public Int32 AccountSummaryID;
		public string AccountSummaryCode;
		public string AccountSummaryName;
        public AccountClassificationDetails AccountClassificationDetails;

        public DateTime CreatedOn;
        public DateTime LastModified;

        //public short AccountClassificationID;
        //public string AccountClassificationCode;
        //public string AccountClassificationName;
        //public AccountClassificationType AccountClassificationType;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccountSummaries : POSConnection
	{
		#region Constructors and Destructors

		public AccountSummaries()
            : base(null, null)
        {
        }

        public AccountSummaries(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(AccountSummaryDetails Details)
		{
			try 
			{
                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				Int32 iID = 0;

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					iID = Int32.Parse(dr[0].ToString());
				}

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(AccountSummaryDetails Details)
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

        public Int32 Save(AccountSummaryDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveAccountSummary(@AccountSummaryID, @AccountClassificationID, @AccountSummaryCode, @AccountSummaryName, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("AccountSummaryID", Details.AccountSummaryID);
                cmd.Parameters.AddWithValue("AccountClassificationID", Details.AccountClassificationDetails.AccountClassificationID);
                cmd.Parameters.AddWithValue("AccountSummaryCode", Details.AccountSummaryCode);
                cmd.Parameters.AddWithValue("AccountSummaryName", Details.AccountSummaryName);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

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
				string SQL=	"DELETE FROM tblAccountSummary WHERE AccountSummaryID IN (" + IDs + ");";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
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
            string stSQL = "SELECT " +
                                "AccountSummaryID, " +
                                "AccountSummaryCode, " +
                                "AccountSummaryName, " +
                                "a.AccountClassificationID, " +
                                "AccountClassificationCode, " +
                                "AccountClassificationName, " +
                                "AccountClassificationType " +
                            "FROM tblAccountSummary a INNER JOIN " +
                                "tblAccountClassification b ON a.AccountClassificationID = b.AccountClassificationID ";
            return stSQL;
        }

		#region Details

		public AccountSummaryDetails Details(Int32 AccountSummaryID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE AccountSummaryID = @AccountSummaryID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);
				prmAccountSummaryID.Value = AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                AccountSummaryDetails Details = new AccountSummaryDetails();
                AccountClassifications clsAccountClassification = new AccountClassifications(this.Connection, this.Transaction);
 
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    Details.AccountSummaryID = AccountSummaryID;
                    Details.AccountSummaryCode = "" + dr["AccountSummaryCode"].ToString();
                    Details.AccountSummaryName = "" + dr["AccountSummaryName"].ToString();
                    Details.AccountClassificationDetails = clsAccountClassification.Details(Int16.Parse(dr["AccountClassificationID"].ToString()));
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + "ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
                string SQL = SQLSelect() + 
                            "WHERE AccountSummaryCode LIKE @SearchKey " +
								"or AccountSummaryName LIKE @SearchKey " +
                                "or AccountClassificationCode LIKE @SearchKey " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		

		#endregion
	}
}

