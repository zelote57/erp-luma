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
	public struct BranchDetails
	{
		public int BranchID;
		public string BranchCode;
		public string BranchName;
		public string DBIP;
		public string DBPort;
		public string Address;
		public string Remarks;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Branch
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

		public Branch()
		{
			
		}

		public Branch(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int16 Insert(BranchDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblBranch (BranchCode, BranchName, DBIP, DBPort, Address, Remarks) " +
					"VALUES (@BranchCode, @BranchName, @DBIP, @DBPort, @Address, @Remarks);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchCode = new MySqlParameter("@BranchCode",MySqlDbType.String);			
				prmBranchCode.Value = Details.BranchCode;
				cmd.Parameters.Add(prmBranchCode);

				MySqlParameter prmBranchName = new MySqlParameter("@BranchName",MySqlDbType.String);			
				prmBranchName.Value = Details.BranchName;
				cmd.Parameters.Add(prmBranchName);

				MySqlParameter prmDBIP = new MySqlParameter("@DBIP",MySqlDbType.String);			
				prmDBIP.Value = Details.DBIP;
				cmd.Parameters.Add(prmDBIP);

				MySqlParameter prmDBPort = new MySqlParameter("@DBPort",MySqlDbType.String);			
				prmDBPort.Value = Details.DBPort;
				cmd.Parameters.Add(prmDBPort);

				MySqlParameter prmAddress = new MySqlParameter("@Address",MySqlDbType.String);			
				prmAddress.Value = Details.Address;
				cmd.Parameters.Add(prmAddress);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

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
				{
					mTransaction.Rollback();
					mTransaction.Dispose(); 
					mConnection.Close();
					mConnection.Dispose();
				}

				throw ex;
			}	
		}

		public void Update(BranchDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblBranch SET " + 
								"BranchCode = @BranchCode, " +  
								"BranchName = @BranchName, " +  
								"DBIP = @DBIP, " +  
								"DBPort = @DBPort, " +  
								"Address = @Address, " +  
								"Remarks = @Remarks " +  
							"WHERE BranchID = @BranchID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);			
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmBranchCode = new MySqlParameter("@BranchCode",MySqlDbType.String);			
				prmBranchCode.Value = Details.BranchCode;
				cmd.Parameters.Add(prmBranchCode);

				MySqlParameter prmBranchName = new MySqlParameter("@BranchName",MySqlDbType.String);			
				prmBranchName.Value = Details.BranchName;
				cmd.Parameters.Add(prmBranchName);

				MySqlParameter prmDBIP = new MySqlParameter("@DBIP",MySqlDbType.String);			
				prmDBIP.Value = Details.DBIP;
				cmd.Parameters.Add(prmDBIP);

				MySqlParameter prmDBPort = new MySqlParameter("@DBPort",MySqlDbType.String);			
				prmDBPort.Value = Details.DBPort;
				cmd.Parameters.Add(prmDBPort);

				MySqlParameter prmAddress = new MySqlParameter("@Address",MySqlDbType.String);			
				prmAddress.Value = Details.Address;
				cmd.Parameters.Add(prmAddress);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);			
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

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
				string SQL=	"DELETE FROM tblBranch WHERE BranchID IN (" + IDs + ");";
				  
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
            string stSQL = "SELECT " +
					            "BranchID, " +
					            "BranchCode, " +
					            "BranchName, " + 
					            "DBIP, " +
					            "DBPort, " +
					            "Address, " +
					            "Remarks " +
					        "FROM tblBranch ";

            return stSQL;
        }

		#region Details

		public BranchDetails Details(Int16 BranchID)
		{
			try
			{
				string SQL = "SELECT " +
								"BranchID, " +
								"BranchCode, " +
								"BranchName, " + 
								"DBIP, " +
								"DBPort, " +
								"Address, " +
								"Remarks " +
							"FROM tblBranch " +
							"WHERE BranchID = @BranchID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
				prmBranchID.Value = BranchID;
				cmd.Parameters.Add(prmBranchID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				BranchDetails Details = new BranchDetails();

				while (myReader.Read()) 
				{
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.DBIP = "" + myReader["DBIP"].ToString();
                    Details.DBPort = "" + myReader["DBPort"].ToString();
                    Details.Address = "" + myReader["Address"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();
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
        public BranchDetails Details(string BranchCode)
        {
            try
            {
                string SQL = "SELECT " +
                                "BranchID, " +
                                "BranchCode, " +
                                "BranchName, " +
                                "DBIP, " +
                                "DBPort, " +
                                "Address, " +
                                "Remarks " +
                            "FROM tblBranch " +
                            "WHERE BranchCode = @BranchCode;";

                MySqlConnection cn = GetConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cn;
                cmd.Transaction = mTransaction;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@BranchCode", BranchCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                BranchDetails Details = new BranchDetails();

                while (myReader.Read())
                {
                    Details.BranchID = myReader.GetInt32("BranchID");
                    Details.BranchCode = "" + myReader["BranchCode"].ToString();
                    Details.BranchName = "" + myReader["BranchName"].ToString();
                    Details.DBIP = "" + myReader["DBIP"].ToString();
                    Details.DBPort = "" + myReader["DBPort"].ToString();
                    Details.Address = "" + myReader["Address"].ToString();
                    Details.Remarks = "" + myReader["Remarks"].ToString();
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

        //public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
        //{
        //    string SQL = SQLSelect() + "ORDER BY " + SortField;

        //    if (SortOrder == SortOption.Ascending)
        //        SQL += " ASC";
        //    else
        //        SQL += " DESC";

        //    MySqlConnection cn = GetConnection();

        //    MySqlCommand cmd = new MySqlCommand();
        //    cmd.Connection = cn;
        //    cmd.Transaction = mTransaction;
        //    cmd.CommandType = System.Data.CommandType.Text;
        //    cmd.CommandText = SQL;

        //    System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
        //    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //    adapter.Fill(dt);

        //    return dt;
        //}
		
        //public MySqlDataReader List(string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //            "BranchID, " +
        //            "BranchCode, " +
        //            "BranchName, " + 
        //            "DBIP, " +
        //            "DBPort, " +
        //            "Address, " +
        //            "Remarks " +
        //            "FROM tblBranch " +
        //            "ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlConnection cn = GetConnection();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
        //        return myReader;
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionFailed = true;
        //        if (IsInTransaction)
        //        {
        //            mTransaction.Rollback();
        //            mTransaction.Dispose(); 
        //            mConnection.Close();
        //            mConnection.Dispose();
        //        }

        //        throw ex;
        //    }	
        //}
		
        //public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
        //{
        //    try
        //    {
        //        string SQL = "SELECT " +
        //                        "BranchID, " +
        //                        "BranchCode, " +
        //                        "BranchName, " + 
        //                        "DBIP, " +
        //                        "DBPort, " +
        //                        "Address, " +
        //                        "Remarks " +
        //                    "FROM tblBranch " +
        //                    "WHERE BranchName LIKE @SearchKey ORDER BY " + SortField;

        //        if (SortOrder == SortOption.Ascending)
        //            SQL += " ASC";
        //        else
        //            SQL += " DESC";

        //        MySqlConnection cn = GetConnection();

        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.Connection = cn;
        //        cmd.Transaction = mTransaction;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
        //        prmSearchKey.Value = "%" + SearchKey + "%";
        //        cmd.Parameters.Add(prmSearchKey);

        //        MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
        //        return myReader;			
        //    }
        //    catch (Exception ex)
        //    {
        //        TransactionFailed = true;
        //        if (IsInTransaction)
        //        {
        //            mTransaction.Rollback();
        //            mTransaction.Dispose(); 
        //            mConnection.Close();
        //            mConnection.Dispose();
        //        }

        //        throw ex;
        //    }	
        //}

        public System.Data.DataTable ListAsDataTable(string SortField = "BranchCode", SortOption SortOrder=SortOption.Ascending)
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

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE (BranchCode LIKE @SearchKey or BranchName LIKE @SearchKey) ORDER BY " + SortField;

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

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey);

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

		#endregion
	}
}

