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
	public struct AccountClassificationDetails
	{
		public int AccountClassificationID;
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
	public class AccountClassification : POSConnection
	{

		#region Constructors and Destructors

		public AccountClassification()
            : base(null, null)
        {
        }

        public AccountClassification(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(AccountClassificationDetails Details)
		{
			try 
			{
                string SQL = "INSERT INTO tblAccountClassification (AccountClassificationCode, AccountClassificationName, AccountClassificationType) VALUES (@AccountClassificationCode, @AccountClassificationName, @AccountClassificationType);";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmAccountClassificationCode = new MySqlParameter("@AccountClassificationCode",MySqlDbType.String);			
				prmAccountClassificationCode.Value = Details.AccountClassificationCode;
				cmd.Parameters.Add(prmAccountClassificationCode);

                MySqlParameter prmAccountClassificationName = new MySqlParameter("@AccountClassificationName",MySqlDbType.String);
                prmAccountClassificationName.Value = Details.AccountClassificationName;
                cmd.Parameters.Add(prmAccountClassificationName);

                MySqlParameter prmAccountClassificationType = new MySqlParameter("@AccountClassificationType",MySqlDbType.Int16);
                prmAccountClassificationType.Value = Details.AccountClassificationType.ToString("d");
                cmd.Parameters.Add(prmAccountClassificationType);
     
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

		public void Update(AccountClassificationDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblAccountClassification SET " + 
								"AccountClassificationCode		= @AccountClassificationCode, " +
                                "AccountClassificationName		= @AccountClassificationName, " +
								"AccountClassificationType		= @AccountClassificationType " +
							"WHERE AccountClassificationID = @AccountClassificationID;";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountClassificationCode = new MySqlParameter("@AccountClassificationCode",MySqlDbType.String);			
				prmAccountClassificationCode.Value = Details.AccountClassificationCode;
				cmd.Parameters.Add(prmAccountClassificationCode);

                MySqlParameter prmAccountClassificationName = new MySqlParameter("@AccountClassificationName",MySqlDbType.String);
                prmAccountClassificationName.Value = Details.AccountClassificationName;
                cmd.Parameters.Add(prmAccountClassificationName);

                MySqlParameter prmAccountClassificationType = new MySqlParameter("@AccountClassificationType",MySqlDbType.Int16);
                prmAccountClassificationType.Value = Details.AccountClassificationType.ToString("d");
                cmd.Parameters.Add(prmAccountClassificationType);

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
				string SQL=	"DELETE FROM tblAccountClassification WHERE AccountClassificationID IN (" + IDs + ");";
	 			
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
                                    "AccountClassificationID, " +
                                    "AccountClassificationCode, " +
                                    "AccountClassificationName, " +
                                    "AccountClassificationType " +
                                "FROM tblAccountClassification ";
            return stSQL;
        }

		#region Details

		public AccountClassificationDetails Details(Int32 AccountClassificationID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE AccountClassificationID = @AccountClassificationID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAccountClassificationID = new MySqlParameter("@AccountClassificationID",MySqlDbType.Int16);
				prmAccountClassificationID.Value = AccountClassificationID;
				cmd.Parameters.Add(prmAccountClassificationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				AccountClassificationDetails Details = new AccountClassificationDetails();

				while (myReader.Read()) 
				{
					Details.AccountClassificationID = AccountClassificationID;
					Details.AccountClassificationCode = "" + myReader["AccountClassificationCode"].ToString();
                    Details.AccountClassificationName = "" + myReader["AccountClassificationName"].ToString();
					Details.AccountClassificationType = (AccountClassificationType) Enum.Parse(typeof(AccountClassificationType), myReader.GetString("AccountClassificationType"));
				}

				myReader.Close();

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
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE AccountClassificationName LIKE @SearchKey " +
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

