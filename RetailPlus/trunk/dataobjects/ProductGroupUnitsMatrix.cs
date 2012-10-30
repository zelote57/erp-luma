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
	public struct ProductGroupUnitsMatrixDetails
	{
		public Int64 MatrixID;
		public Int64 GroupID;
		public Int32 BaseUnitID;
		public string BaseUnitName;
		public decimal BaseUnitValue;
		public Int32 BottomUnitID;
		public string BottomUnitName;
		public decimal BottomUnitValue;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductGroupUnitsMatrix
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

		public ProductGroupUnitsMatrix()
		{
			
		}

		public ProductGroupUnitsMatrix(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(ProductGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblProductGroupUnitMatrix (" +
								"GroupID, " +
								"BaseUnitID, " +
								"BaseUnitValue, " +
								"BottomUnitID, " +
								"BottomUnitValue" +
								")VALUES(" +
								"@GroupID, " +
								"@BaseUnitID, " +
								"@BaseUnitValue, " +
								"@BottomUnitID, " +
								"@BottomUnitValue);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmBaseUnitValue = new MySqlParameter("@BaseUnitValue",MySqlDbType.Decimal);			
				prmBaseUnitValue.Value = Details.BaseUnitValue;
				cmd.Parameters.Add(prmBaseUnitValue);
				
				MySqlParameter prmBottomUnitID = new MySqlParameter("@BottomUnitID",MySqlDbType.Int32);			
				prmBottomUnitID.Value = Details.BottomUnitID;
				cmd.Parameters.Add(prmBottomUnitID);

				MySqlParameter prmBottomUnitValue = new MySqlParameter("@BottomUnitValue",MySqlDbType.Decimal);			
				prmBottomUnitValue.Value = Details.BottomUnitValue;
				cmd.Parameters.Add(prmBottomUnitValue);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				Int64 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt64(0);
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
	
		public void Update(ProductGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblProductGroupUnitMatrix SET " + 
								"BaseUnitID = @BaseUnitID, " +  
								"BaseUnitValue = @BaseUnitValue, " +
								"BottomUnitID = @BottomUnitID, " +
								"BottomUnitValue = @BottomUnitValue " +  
								"WHERE MatrixID = @MatrixID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.String);			
				prmMatrixID.Value = Details.MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmBaseUnitValue = new MySqlParameter("@BaseUnitValue",MySqlDbType.Decimal);			
				prmBaseUnitValue.Value = Details.BaseUnitValue;
				cmd.Parameters.Add(prmBaseUnitValue);
				
				MySqlParameter prmBottomUnitID = new MySqlParameter("@BottomUnitID",MySqlDbType.Int32);			
				prmBottomUnitID.Value = Details.BottomUnitID;
				cmd.Parameters.Add(prmBottomUnitID);

				MySqlParameter prmBottomUnitValue = new MySqlParameter("@BottomUnitValue",MySqlDbType.Decimal);			
				prmBottomUnitValue.Value = Details.BottomUnitValue;
				cmd.Parameters.Add(prmBottomUnitValue);

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

		#region Details

		public ProductGroupUnitsMatrixDetails Details(Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
								"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
								"FROM tblProductGroupUnitMatrix a INNER JOIN " +
								"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
								"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
								"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
								"WHERE a.MatrixID = @MatrixID;"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",System.Data.DbType.Int64);
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		public ProductGroupUnitsMatrixDetails UnitDetails(Int64 GroupID, Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.GroupID " +
					"WHERE a.GroupID = " + GroupID + " " +
					"AND a.MatrixID = " + MatrixID + ";"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

//				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int16);
//				prmGroupID.Value = GroupID;
//				cmd.Parameters.Add(prmGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		public ProductGroupUnitsMatrixDetails LastDetails(Int64 GroupID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
					"WHERE a.GroupID = @GroupID " +
					"ORDER BY a.MatrixID DESC LIMIT 1;"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductGroupUnitMatrix WHERE MatrixID IN (" + IDs + ");";
								
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

		#region Streams

		public MySqlDataReader List(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string	SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
					"WHERE a.GroupID = " + GroupID  + " ORDER BY " + SortField;

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
		public MySqlDataReader AvailableUnitsForProduct(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			
			try
			{
				string SQL = "SELECT * FROM tblUnit " +
					"WHERE UnitID NOT IN (SELECT BaseUnitID FROM tblProductGroupUnitMatrix WHERE GroupID = @GroupID) " +
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

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

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
				string SQL ="SELECT * FROM tblProductGroupUnitMatrix " +
							"WHERE GroupID LIKE @SearchKey " +
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
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

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
	}
}

