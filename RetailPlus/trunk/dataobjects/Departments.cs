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
	public struct DepartmentDetails
	{
		public Int16 DepartmentID;
		public string DepartmentCode;
		public string DepartmentName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Department
	{
		MySqlConnection mConnection;
		MySqlTransaction mTransaction;
		bool IsInTransaction = false;
		bool TransactionFailed = false;

        public const string DEFAULT_ALL_DEPARTMENTS = "All Departments";

		public MySqlConnection Connection
		{
			get { return mConnection;	}
		}

		public MySqlTransaction Transaction
		{
			get { return mTransaction;	}
		}


		#region Constructors and Destructors

		public Department()
		{
			
		}

		public Department(MySqlConnection Connection, MySqlTransaction Transaction)
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


		#endregion

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

		
		#region Insert and Update

		public Int16 Insert(DepartmentDetails Details)
		{
			try 
			{
                string SQL = "CALL procDepartmentInsert(@DepartmentCode, @DepartmentName);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prDepartmentCode = new MySqlParameter("@DepartmentCode",MySqlDbType.String);
                prDepartmentCode.Value = Details.DepartmentCode;
                cmd.Parameters.Add(prDepartmentCode);

                MySqlParameter prmDepartmentName = new MySqlParameter("@DepartmentName",MySqlDbType.String);
                prmDepartmentName.Value = Details.DepartmentName;
                cmd.Parameters.Add(prmDepartmentName);

                cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int16 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt16(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}

		public void Update(DepartmentDetails Details)
		{
			try 
			{
                string SQL = "CALL procDepartmentUpdate(@DepartmentID, @DepartmentCode, @DepartmentName);";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DepartmentID", Details.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentCode", Details.DepartmentCode);
                cmd.Parameters.AddWithValue("@DepartmentName", Details.DepartmentName);

                cmd.ExecuteNonQuery();
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "DepartmentID, " +
                                "DepartmentCode, " +
                                "DepartmentName " +
                            "FROM tblDepartments ";
            return stSQL;
        }

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblDepartments WHERE DepartmentID IN (" + IDs + ");";
				  
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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}


		#endregion

		#region Details

		public DepartmentDetails Details(Int16 DepartmentID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE DepartmentID = @DepartmentID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DepartmentID", DepartmentID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				DepartmentDetails Details = new DepartmentDetails();

				while (myReader.Read()) 
				{
					Details.DepartmentID = myReader.GetInt16("DepartmentID");
					Details.DepartmentCode = "" + myReader["DepartmentCode"].ToString();
					Details.DepartmentName = "" + myReader["DepartmentName"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
		public DepartmentDetails Details(string DepartmentName)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE DepartmentName = @DepartmentName;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@DepartmentName", DepartmentName);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				DepartmentDetails Details = new DepartmentDetails();

				while (myReader.Read()) 
				{
					Details.DepartmentID = myReader.GetInt16("DepartmentID");
					Details.DepartmentCode = "" + myReader["DepartmentCode"].ToString();
					Details.DepartmentName = "" + myReader["DepartmentName"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				TransactionFailed = true;
				if (IsInTransaction)
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}

		#endregion

		#region Streams

        public MySqlDataReader List(string SortField, SortOption SortOrder, Int32 Limit)
		{
			try
			{
                if (SortField == null || SortField == string.Empty) SortField = "DepartmentName";

                string SQL = SQLSelect() + "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, Int32 Limit)
        {
            try
            {
                if (SortField == null || SortField == string.Empty) SortField = "DepartmentName";

                string SQL = SQLSelect() + "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblDepartments");
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
        public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit)
		{
			try
			{
                if (SortField == null || SortField == string.Empty) SortField = "DepartmentName";

                string SQL = SQLSelect() + "WHERE (DepartmentCode LIKE @SearchKey " +
                                            "OR DepartmentName LIKE @SearchKey) ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE (DepartmentCode LIKE @SearchKey " +
                                            "OR DepartmentName LIKE @SearchKey) ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblDepartments");
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

