using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

/******************************************************************************
	**		Auth: Lemuel E. Aceron
	**		Date: March 29, 2005
	***************************************************************************
	**		Change History
	***************************************************************************
	**		Date:			Author:				Description:
	**		--------		--------			-------------------------------
	**      
	***************************************************************************/

namespace AceSoft.RetailPlus.Security
{

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct AccessGroupRightsDetails
	{
		public int GroupID;
		public int TranTypeID;
		public bool Read;
		public bool Write;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct AllowedGroupRights
	{
		public bool Read;
		public bool Write;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class AccessGroupRights
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


		#region Contructors and Destructors
		
		public AccessGroupRights()
		{
			
		}

		public AccessGroupRights(MySqlConnection Connection, MySqlTransaction Transaction)
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


		#region DataList

		private MySqlDataReader ListAccessTypes(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT		TypeID, TypeName, Remarks " +
								"FROM		sysAccessTypes " + 
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

		public System.Data.DataTable DataList(int GroupID, string SortField, SortOption SortOrder)
		{
            AccessType clsAccessType = new AccessType(mConnection, mTransaction);
            System.Data.DataTable dtAccessType = clsAccessType.DataList("Category, SequenceNo", SortOption.Ascending);

            System.Data.DataTable dt = new System.Data.DataTable("sysAccessRights");

            dt.Columns.Add("TypeID");
            dt.Columns.Add("TypeName");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("SequenceNo");
            dt.Columns.Add("Read");
            dt.Columns.Add("Write");

            foreach (System.Data.DataRow dr in dtAccessType.Rows)
            {
                System.Data.DataRow drNew = dt.NewRow();

                drNew["TypeID"] = Convert.ToInt16(dr["TypeID"]);
                drNew["TypeName"] = "" + dr["TypeName"].ToString();
                drNew["Remarks"] = "" + dr["Remarks"].ToString();
                drNew["SequenceNo"] = "" + dr["SequenceNo"].ToString();

                AllowedRights rights = GetReadWrite(GroupID, Convert.ToInt16(drNew["TypeID"]));

                drNew["Read"] = rights.Read;
                drNew["Write"] = rights.Write;

                dt.Rows.Add(drNew);
            }
            return dt;
		}
        public System.Data.DataTable DataList(string Category, int GroupID, string SortField, SortOption SortOrder)
        {
            if (mConnection == null)
                GetConnection();

            AccessType clsAccessType = new AccessType(mConnection, mTransaction);
            System.Data.DataTable dtAccessType = clsAccessType.DataList(Category, "Category, SequenceNo", SortOption.Ascending);

            System.Data.DataTable dt = new System.Data.DataTable("sysAccessRights");

            dt.Columns.Add("TypeID");
            dt.Columns.Add("TypeName");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Enabled");
            dt.Columns.Add("SequenceNo");
            dt.Columns.Add("Read");
            dt.Columns.Add("Write");

            foreach (System.Data.DataRow dr in dtAccessType.Rows)
            {
                System.Data.DataRow drNew = dt.NewRow();

                drNew["TypeID"] = Convert.ToInt16(dr["TypeID"]);
                drNew["TypeName"] = "" + dr["TypeName"].ToString();
                drNew["Remarks"] = "" + dr["Remarks"].ToString();
                drNew["Enabled"] = "" + dr["Enabled"].ToString();
                drNew["SequenceNo"] = "" + dr["SequenceNo"].ToString();

                AllowedRights rights = GetReadWrite(GroupID, Convert.ToInt16(drNew["TypeID"]));

                drNew["Read"] = rights.Read;
                drNew["Write"] = rights.Write;

                dt.Rows.Add(drNew);
            }
            return dt;
        }

		public AllowedRights GetReadWrite(int GroupID,int TranTypeID)
		{
			try
			{
				string SQL ="SELECT IFNULL(AllowRead,0) as 'Read', IFNULL(AllowWrite,0) as 'Write' " +
							"FROM sysAccessGroupRights " +
							"WHERE GroupID = @GroupID AND TranTypeID = @TranTypeID";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
                cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@GroupID", GroupID);
				cmd.Parameters.AddWithValue("@TranTypeID", TranTypeID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				AllowedRights rights = new AllowedRights();

				while (myReader.Read())
				{
					rights.Read = myReader.GetBoolean("Read");
					rights.Write  = myReader.GetBoolean("Write");
				}
			
				myReader.Close();

				return rights;

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

		#region Update

		private bool IsExisting(int GroupID, int TranTypeID)
		{
			try
			{
				string SQL ="SELECT COUNT(*) FROM sysAccessGroupRights " +
							"WHERE GroupID = @GroupID AND TranTypeID = @TranTypeID";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int16);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				bool existing = false;
				
				while (myReader.Read())
				{
					if (myReader.GetInt32(0)>0)
						existing = true;
				}
			
				myReader.Close();

				return existing;
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

		public void Modify(AccessGroupRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				if (IsExisting(Details.GroupID,Details.TranTypeID))
				{
					Update(Details); 
				}
				else
				{
					Insert(Details);
				}
 
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}

		public void Insert(AccessGroupRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				SQL	=	"INSERT INTO sysAccessGroupRights " +
						"(GroupID, TranTypeID, AllowRead, AllowWrite) " +
						"VALUES (@GroupID, @TranTypeID, @Read, @Write)";
				 
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int16);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = Details.TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

				MySqlParameter prmRead = new MySqlParameter("@Read",MySqlDbType.String);			
				if (Details.Read)
					prmRead.Value = "1";
				else
					prmRead.Value = "0";
				cmd.Parameters.Add(prmRead);

				MySqlParameter prmWrite = new MySqlParameter("@Write",MySqlDbType.String);			
				if (Details.Write)
					prmWrite.Value = "1";
				else
					prmWrite.Value = "0";
				cmd.Parameters.Add(prmWrite);

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

		private void Update(AccessGroupRightsDetails Details)
		{
			try 
			{
				string SQL = string.Empty;

				SQL	=	"UPDATE sysAccessGroupRights SET " +
						"AllowRead	 = @Read, " +
						"AllowWrite = @Write " +
						"WHERE 1=1 " +
						"AND GroupID = @GroupID " +
						"AND TranTypeID = @TranTypeID";
				
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmRead = new MySqlParameter("@Read",MySqlDbType.String);			
				if (Details.Read)
					prmRead.Value = "1";
				else
					prmRead.Value = "0";
				cmd.Parameters.Add(prmRead);

				MySqlParameter prmWrite = new MySqlParameter("@Write",MySqlDbType.String);			
				if (Details.Write)
					prmWrite.Value = "1";
				else
					prmWrite.Value = "0";
				cmd.Parameters.Add(prmWrite);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int16);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmTranTypeID = new MySqlParameter("@TranTypeID",MySqlDbType.Int16);			
				prmTranTypeID.Value = Details.TranTypeID;
				cmd.Parameters.Add(prmTranTypeID);

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

		#region Details

		public AccessGroupRightsDetails Details(int GroupID, int TranTypeID)
		{
			try
			{
				AllowedRights rights = new AllowedRights();				
				rights = GetReadWrite(GroupID,TranTypeID);
 			
				AccessGroupRightsDetails Details = new AccessGroupRightsDetails();
	
				Details.GroupID = GroupID;
				Details.TranTypeID = TranTypeID;
				Details.Read = rights.Read;
				Details.Write = rights.Write; 

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
	}
}

