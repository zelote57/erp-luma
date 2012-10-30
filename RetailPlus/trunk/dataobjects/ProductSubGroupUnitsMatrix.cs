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
	public struct ProductSubGroupUnitsMatrixDetails
	{
		public Int64 MatrixID;
		public Int64 SubGroupID;
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
	public class ProductSubGroupUnitsMatrix
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

		public ProductSubGroupUnitsMatrix()
		{
			
		}

		public ProductSubGroupUnitsMatrix(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(ProductSubGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblProductSubGroupUnitMatrix (" +
								"SubGroupID, " +
								"BaseUnitID, " +
								"BaseUnitValue, " +
								"BottomUnitID, " +
								"BottomUnitValue" +
								")VALUES(" +
								"@SubGroupID, " +
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
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = Details.SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

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
	
		public void Update(ProductSubGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblProductSubGroupUnitMatrix SET " + 
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

		public ProductSubGroupUnitsMatrixDetails Details(Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.SubGroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
								"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
								"FROM tblProductSubGroupUnitMatrix a INNER JOIN " +
								"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
								"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
								"tblProductSubGroup d ON a.SubGroupID = d.ProductSubGroupID " +
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
				
				ProductSubGroupUnitsMatrixDetails Details = new ProductSubGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64(0);
					Details.SubGroupID = myReader.GetInt16(1);;
					Details.BaseUnitID = myReader.GetInt16(2);
					Details.BaseUnitName = myReader.GetString(3);
					Details.BaseUnitValue = myReader.GetDecimal(4);
					Details.BottomUnitID = myReader.GetInt16(5);
					Details.BottomUnitName = myReader.GetString(6);
					Details.BottomUnitValue = myReader.GetDecimal(7);
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

		public ProductSubGroupUnitsMatrixDetails UnitDetails(Int64 SubGroupID, Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.SubGroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductSubGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductSubGroup d ON a.SubGroupID = d.SubGroupID " +
					"WHERE a.SubGroupID = " + SubGroupID + " " +
					"AND a.MatrixID = " + MatrixID + ";"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

//				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int16);
//				prmSubGroupID.Value = SubGroupID;
//				cmd.Parameters.Add(prmSubGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductSubGroupUnitsMatrixDetails Details = new ProductSubGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64(0);
					Details.SubGroupID = SubGroupID;
					Details.BaseUnitID = myReader.GetInt16(2);
					Details.BaseUnitName = myReader.GetString(3);
					Details.BaseUnitValue = myReader.GetDecimal(4);
					Details.BottomUnitID = myReader.GetInt16(5);
					Details.BottomUnitName = myReader.GetString(6);
					Details.BottomUnitValue = myReader.GetDecimal(7);
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

		public ProductSubGroupUnitsMatrixDetails LastDetails(Int64 SubGroupID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.SubGroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductSubGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductSubGroup d ON a.SubGroupID = d.ProductSubGroupID " +
					"WHERE a.SubGroupID = @SubGroupID " +
					"ORDER BY a.MatrixID DESC LIMIT 1;"; 
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",System.Data.DbType.Int64);
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductSubGroupUnitsMatrixDetails Details = new ProductSubGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64(0);
					Details.SubGroupID = myReader.GetInt16(1);;
					Details.BaseUnitID = myReader.GetInt16(2);
					Details.BaseUnitName = myReader.GetString(3);
					Details.BaseUnitValue = myReader.GetDecimal(4);
					Details.BottomUnitID = myReader.GetInt16(5);
					Details.BottomUnitName = myReader.GetString(6);
					Details.BottomUnitValue = myReader.GetDecimal(7);
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
				string SQL=	"DELETE FROM tblProductSubGroupUnitMatrix WHERE MatrixID IN (" + IDs + ");";
								
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

		public MySqlDataReader List(Int64 SubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string	SQL =	"SELECT a.MatrixID, a.SubGroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductSubGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductSubGroup d ON a.SubGroupID = d.ProductSubGroupID " +
					"WHERE a.SubGroupID = " + SubGroupID  + " ORDER BY " + SortField;

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
		public MySqlDataReader AvailableUnitsForProduct(int SubGroupID, string SortField, SortOption SortOrder)
		{
			
			try
			{
				string SQL = "SELECT * FROM tblUnit " +
					"WHERE UnitID NOT IN (SELECT BaseUnitID FROM tblProductSubGroupUnitMatrix WHERE SubGroupID = @SubGroupID) " +
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

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

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
				string SQL ="SELECT * FROM tblProductSubGroupUnitMatrix " +
							"WHERE SubGroupID LIKE @SearchKey " +
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

		#region Inheritance

		public void InheritGroupUnitMatrix(int ProductGroupID, int ProductSubGroupID)
		{
			try 
			{	
				string SQL =	"INSERT INTO tblProductSubGroupUnitMatrix (" +
								"SubGroupID, " +
								"BaseUnitID, " +
								"BaseUnitValue, " +
								"BottomUnitID, " +
								"BottomUnitValue" +
						")SELECT " +
								"@SubGroupID, " +
								"BaseUnitID, " +
								"BaseUnitValue, " +
								"BottomUnitID, " +
								"BottomUnitValue  FROM tblProductGroupUnitMatrix " +
						"WHERE GroupID = @GroupID;";

				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = ProductSubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmGroupID);

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
	}
}

