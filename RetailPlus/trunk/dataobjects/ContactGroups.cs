using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{
	public enum ContactGroupCategory
	{
		CUSTOMER = 1,
		SUPPLIER = 2,
		BOTH = 3,
        AGENT = 4
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct ContactGroupDetails
	{
		public int ContactGroupID;
		public string ContactGroupCode;
		public string ContactGroupName;
		public ContactGroupCategory ContactGroupCategory;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ContactGroup
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

		public MySqlConnection Connection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction Transaction
		{
			get { return mTransaction;	}
		}


		#region Constructors and Destructors

		public ContactGroup()
		{
			
		}

		public ContactGroup(MySqlConnection Connection, MySqlTransaction Transaction)
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
			if (mConnection==null)
			{
				mConnection = new MySqlConnection(AceSoft.RetailPlus.DBConnection.ConnectionString());	
				mConnection.Open(); 
				
				mTransaction = (MySqlTransaction) mConnection.BeginTransaction();
				IsInTransaction = true;
			}

			return mConnection;
		} 
		

		#endregion

		#region Insert and Update

		public Int32 Insert(ContactGroupDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblContactGroup (ContactGroupCode, ContactGroupName, ContactGroupCategory) VALUES (@ContactGroupCode, @ContactGroupName, @ContactGroupCategory);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmContactGroupCode = new MySqlParameter("@ContactGroupCode",MySqlDbType.String);			
				prmContactGroupCode.Value = Details.ContactGroupCode;
				cmd.Parameters.Add(prmContactGroupCode);

				MySqlParameter prmContactGroupName = new MySqlParameter("@ContactGroupName",MySqlDbType.String);			
				prmContactGroupName.Value = Details.ContactGroupName;
				cmd.Parameters.Add(prmContactGroupName);
     
				MySqlParameter prmContactGroupCategory = new MySqlParameter("@ContactGroupCategory",MySqlDbType.Int16);			
				prmContactGroupCategory.Value = Details.ContactGroupCategory.ToString("d");
				cmd.Parameters.Add(prmContactGroupCategory);
				
				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int32 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt32(0);
				}

				myReader.Close();

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

		public void Update(ContactGroupDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblContactGroup SET " + 
					"ContactGroupCode		= @ContactGroupCode, " +
					"ContactGroupName		= @ContactGroupName, " +
					"ContactGroupCategory	= @ContactGroupCategory " +
					"WHERE ContactGroupID = @ContactGroupID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmContactGroupCode = new MySqlParameter("@ContactGroupCode",MySqlDbType.String);			
				prmContactGroupCode.Value = Details.ContactGroupCode;
				cmd.Parameters.Add(prmContactGroupCode);		

				MySqlParameter prmContactGroupName = new MySqlParameter("@ContactGroupName",MySqlDbType.String);			
				prmContactGroupName.Value = Details.ContactGroupName;
				cmd.Parameters.Add(prmContactGroupName);

				MySqlParameter prmContactGroupID = new MySqlParameter("@ContactGroupID",MySqlDbType.Int16);			
				prmContactGroupID.Value = Details.ContactGroupID;
				cmd.Parameters.Add(prmContactGroupID);

				MySqlParameter prmContactGroupCategory = new MySqlParameter("@ContactGroupCategory",MySqlDbType.Int16);			
				prmContactGroupCategory.Value = Details.ContactGroupCategory.ToString("d");
				cmd.Parameters.Add(prmContactGroupCategory);

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
				string SQL=	"DELETE FROM tblContactGroup WHERE ContactGroupID IN (" + IDs + ");";
				  
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


		#endregion

        private string SQLSelect()
        {
            string stSQL ="SELECT " +
					        "ContactGroupID, " +
					        "ContactGroupCode, " +
					        "ContactGroupName, " +
					        "ContactGroupCategory " +
                        "FROM tblContactGroup "; ;

            return stSQL;
        }

		#region Details

		public ContactGroupDetails Details(Int32 ContactGroupID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE ContactGroupID = @ContactGroupID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmContactGroupID = new MySqlParameter("@ContactGroupID",MySqlDbType.Int16);
				prmContactGroupID.Value = ContactGroupID;
				cmd.Parameters.Add(prmContactGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ContactGroupDetails Details = new ContactGroupDetails();

				while (myReader.Read()) 
				{
					Details.ContactGroupID = ContactGroupID;
					Details.ContactGroupCode = "" + myReader["ContactGroupCode"].ToString();
					Details.ContactGroupName = "" + myReader["ContactGroupName"].ToString();
                    Details.ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), myReader.GetString("ContactGroupCategory"));
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
        public ContactGroupDetails Details(string ContactGroupCode)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE ContactGroupCode = @ContactGroupCode;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ContactGroupCode", ContactGroupCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ContactGroupDetails Details = new ContactGroupDetails();

                while (myReader.Read())
                {
                    Details.ContactGroupID = myReader.GetInt32("ContactGroupID");
                    Details.ContactGroupCode = "" + myReader["ContactGroupCode"].ToString();
                    Details.ContactGroupName = "" + myReader["ContactGroupName"].ToString();
                    Details.ContactGroupCategory = (ContactGroupCategory)Enum.Parse(typeof(ContactGroupCategory), myReader.GetString("ContactGroupCategory"));
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
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
		public MySqlDataReader List(ContactGroupCategory Category, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE (ContactGroupCategory = @Category OR ContactGroupCategory = 3) " +
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

                cmd.Parameters.AddWithValue("@Category", Category.ToString("d"));

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
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
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ContactGroupName LIKE @SearchKey ORDER BY " + SortField;

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
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				return myReader;			
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

        #region DataTables

        public System.Data.DataTable ListAsDataTable(ContactGroupCategory Category = ContactGroupCategory.BOTH, string SearchKey = null, string SortField = "ContactGroupName", SortOption SortOrder = SortOption.Ascending)
        {
            MySqlCommand cmd = new MySqlCommand();

            string SQL = SQLSelect() + "WHERE 1=1 ";

            if (SearchKey != "")
            {
                SQL += "AND (ContactGroupCode LIKE @SearchKey  OR ContactGroupName LIKE @SearchKey) ";
                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");
            }
            if (Category != ContactGroupCategory.BOTH)
            {
                SQL += "AND (ContactGroupCategory = @Category OR ContactGroupCategory = 3) ";
                cmd.Parameters.AddWithValue("@Category", Category.ToString("d"));
            }
            
            SQL += "ORDER BY " + SortField;
            if (SortOrder == SortOption.Ascending)
                SQL += " ASC ";
            else
                SQL += " DESC ";

            MySqlConnection cn = GetConnection();

            cmd.Connection = cn;
            cmd.Transaction = mTransaction;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;
            
            System.Data.DataTable dt = new System.Data.DataTable("tblContactGroups");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        #endregion
	}
}
