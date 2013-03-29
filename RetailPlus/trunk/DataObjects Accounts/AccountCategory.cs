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
	public struct AccountCategoryDetails
	{
		public int AccountCategoryID;
		public string AccountCategoryCode;
		public string AccountCategoryName;
		public int AccountSummaryID;
		public string AccountSummaryCode;
		public string AccountSummaryName;
        public short AccountClassificationID;
        public string AccountClassificationCode;
        public string AccountClassificationName;
        public AccountClassificationType AccountClassificationType;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccountCategory : POSConnection
	{
		#region Constructors and Destructors

		public AccountCategory()
            : base(null, null)
        {
        }

        public AccountCategory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(AccountCategoryDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblAccountCategory (AccountSummaryID, AccountCategoryCode, AccountCategoryName) VALUES (@AccountSummaryID, @AccountCategoryCode, @AccountCategoryName);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);			
				prmAccountSummaryID.Value = Details.AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

				MySqlParameter prmAccountCategoryCode = new MySqlParameter("@AccountCategoryCode",MySqlDbType.String);			
				prmAccountCategoryCode.Value = Details.AccountCategoryCode;
				cmd.Parameters.Add(prmAccountCategoryCode);

				MySqlParameter prmAccountCategoryName = new MySqlParameter("@AccountCategoryName",MySqlDbType.String);			
				prmAccountCategoryName.Value = Details.AccountCategoryName;
				cmd.Parameters.Add(prmAccountCategoryName);
     
				base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int32 iID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
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

		public void Update(AccountCategoryDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblAccountCategory SET " + 
								"AccountSummaryID			= @AccountSummaryID, " +
								"AccountCategoryCode		= @AccountCategoryCode, " +
								"AccountCategoryName		= @AccountCategoryName " +
							"WHERE AccountCategoryID = @AccountCategoryID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountSummaryID = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int16);			
				prmAccountSummaryID.Value = Details.AccountSummaryID;
				cmd.Parameters.Add(prmAccountSummaryID);

				MySqlParameter prmAccountCategoryCode = new MySqlParameter("@AccountCategoryCode",MySqlDbType.String);			
				prmAccountCategoryCode.Value = Details.AccountCategoryCode;
				cmd.Parameters.Add(prmAccountCategoryCode);		

				MySqlParameter prmAccountCategoryName = new MySqlParameter("@AccountCategoryName",MySqlDbType.String);			
				prmAccountCategoryName.Value = Details.AccountCategoryName;
				cmd.Parameters.Add(prmAccountCategoryName);

				MySqlParameter prmAccountCategoryID = new MySqlParameter("@AccountCategoryID",MySqlDbType.Int16);			
				prmAccountCategoryID.Value = Details.AccountCategoryID;
				cmd.Parameters.Add(prmAccountCategoryID);

				base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblAccountCategory WHERE AccountCategoryID IN (" + IDs + ");";
				  
				
	 			
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

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                    "AccountCategoryID, " +
                                    "a.AccountSummaryID, " +
                                    "AccountSummaryCode, " +
                                    "AccountSummaryName, " +
                                    "AccountCategoryCode, " +
                                    "AccountCategoryName, " +
                                    "b.AccountClassificationID, " +
                                    "AccountClassificationCode, " +
                                    "AccountClassificationName, " +
                                    "AccountClassificationType " +
                                "FROM tblAccountCategory a " +
                                "INNER JOIN tblAccountSummary b ON a.AccountSummaryID = b.AccountSummaryID " +
                                "INNER JOIN tblAccountClassification c ON b.AccountClassificationID = c.AccountClassificationID "; 
            return stSQL;
        }

		#region Details

		public AccountCategoryDetails Details(Int32 AccountCategoryID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE AccountCategoryID = @AccountCategoryID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountCategoryID = new MySqlParameter("@AccountCategoryID",MySqlDbType.Int16);
				prmAccountCategoryID.Value = AccountCategoryID;
				cmd.Parameters.Add(prmAccountCategoryID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				AccountCategoryDetails Details = new AccountCategoryDetails();

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					Details.AccountCategoryID = AccountCategoryID;
					Details.AccountCategoryCode = "" + dr["AccountCategoryCode"].ToString();
					Details.AccountCategoryName = "" + dr["AccountCategoryName"].ToString();
					Details.AccountSummaryID = Int32.Parse(dr["AccountSummaryID"].ToString());
					Details.AccountSummaryCode = "" + dr["AccountSummaryCode"].ToString();
					Details.AccountSummaryName = "" + dr["AccountSummaryName"].ToString();
                    Details.AccountClassificationID = Int16.Parse(dr["AccountClassificationID"].ToString());
                    Details.AccountClassificationCode = "" + dr["AccountClassificationCode"].ToString();
                    Details.AccountClassificationName = "" + dr["AccountClassificationName"].ToString();
                    Details.AccountClassificationType = (AccountClassificationType)Enum.Parse(typeof(AccountClassificationType), dr["AccountClassificationType"].ToString());
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
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
        public MySqlDataReader List(int AccountSummaryID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.AccountSummaryID = @AccountSummaryID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmAccountSummaryID  = new MySqlParameter("@AccountSummaryID",MySqlDbType.Int32);
                prmAccountSummaryID.Value = AccountSummaryID;
                cmd.Parameters.Add(prmAccountSummaryID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

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
                            "WHERE AccountCategoryCode LIKE @SearchKey " +
								"or AccountCategoryName LIKE @SearchKey " +
								"or AccountSummaryCode LIKE @SearchKey " +
								"or AccountSummaryName LIKE @SearchKey " +
                                "or AccountClassificationName LIKE @SearchKey " +
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

