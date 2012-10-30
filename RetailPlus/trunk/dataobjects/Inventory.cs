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
	public struct ProductInventoryDetails
	{
		public Int64 InventoryID;
		public Int64 ProductID;
		public string ProductCode;
		public Int64 VariationMatrixID;
		public string VariationMatrixDescription;
		public Int32 ProductUnitID;
		public string UnitName;
		public Int32 Quantity;
		public Int32 MinThreshold;
		public Int32 MaxThreshold;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductInventory
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

		public ProductInventory()
		{
			
		}

		public ProductInventory(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(ProductInventoryDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblInventory (" + 
								"ProductID, " +
								"VariationMatrixID, " +
								"ProductUnitID, " +
								"Quantity, " +
								"MinThreshold" +
								")VALUES (" +
								"@ProductID, " +
								"@VariationMatrixID, " +
								"@ProductUnitID, " +
								"@Quantity, " +
								"@MinThreshold, " +
								"@MaxThreshold);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
				
				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int16);	
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);	
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Int32);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Int32);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

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

		public void Update(ProductInventoryDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " + 
							"ProductID = @ProductID, " +  
							"Quantity = @Quantity, " +  
							"ProductUnitID = @ProductUnitID, " +  
							"MinThreshold = @MinThreshold, " + 
							"MaxThreshold = @MaxThreshold " + 
							"WHERE InventoryID = @InventoryID;";
							
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);			
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Int32);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Int32);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

				MySqlParameter prmInventoryID = new MySqlParameter("@InventoryID",MySqlDbType.Int32);	
				prmInventoryID.Value = Details.InventoryID;
				cmd.Parameters.Add(prmInventoryID);

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

		public void Update(Int64 ProductID, Int64 VariationMatrixID, Int32 Quantity)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " + 
								"Quantity = Quantity + @Quantity " +  
							"WHERE ProductID = @ProductID " +
							"AND VariationMatrixID = @VariationMatrixID;";
							
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

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
				string SQL=	"DELETE FROM tblInventory WHERE InventoryID IN (" + IDs + ");";
				  
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

		#region Details

		public ProductInventoryDetails Details(Int64 ProductID)
		{
			try
			{
				MySqlDataReader myReader = List(ProductID, "InvenrotyID", SortOption.Ascending);
				
				ProductInventoryDetails Details = new ProductInventoryDetails();

				while (myReader.Read()) 
				{
					Details.InventoryID = myReader.GetInt64("InventoryID");
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.VariationMatrixDescription = "" + myReader["VariationMatrixDescription"].ToString();
					Details.ProductUnitID = myReader.GetInt32("ProductUnitID");
					Details.UnitName = "" + myReader["UnitName"].ToString();
					Details.Quantity = myReader.GetInt32("Quantity");
					Details.MinThreshold = myReader.GetInt32("MinThreshold");
					Details.MaxThreshold = myReader.GetInt32("MaxThreshold");
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

		public MySqlDataReader List(Int64 ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
					"InventoryID, " +
					"a.ProductID, " +
					"b.ProductCode, " +
					"a.VariationMatrixID, " +
					"c.Description as VariationMatrixDescription, " +
					"a.ProductUnitID, " +
					"d.UnitName, " +
					"a.Quantity, " +
					"MinThreshold, " +
					"MaxThreshold " +
					"FROM tblInventory a " +
					"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
					"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
					"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
					"WHERE a.ProductID = @ProductID ORDER BY " + SortField; 

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

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);
				
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
		
		public MySqlDataReader ListReport(Int64 ProductID, int ProductUnitID, int VariationMatrixID)
		{
			try
			{
				string SQL =	"SELECT " +
					"InventoryID, " +
					"a.ProductID, " +
					"b.ProductCode, " +
					"a.VariationMatrixID, " +
					"c.Description as Description, " +
					"a.ProductUnitID, " +
					"d.UnitName, " +
					"a.Quantity, " +
					"MinThreshold, " +
					"MaxThreshold " +
					"FROM tblInventory a " +
					"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
					"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
					"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
					"WHERE 1=1";

				if (ProductID != 0)
					SQL += " AND a.ProductID = @ProductID"; 
				if (ProductUnitID != 0)
					SQL += " AND a.ProductUnitID = @ProductUnitID";
				if (VariationMatrixID != 0)
					SQL += " AND a.VariationMatrixID = @VariationMatrixID";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);
				prmProductUnitID.Value = ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int32);
				prmVariationMatrixID.Value = VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);
				
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

		public MySqlDataReader Search(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"InventoryID, " +
								"a.ProductID, " +
								"b.ProductCode, " +
								"a.VariationMatrixID, " +
								"c.Description as VariationMatrixDescription, " +
								"a.ProductUnitID, " +
								"d.UnitName, " +
								"a.Quantity, " +
								"MinThreshold, " +
								"MaxThreshold " +
								"FROM tblInventory a " +
								"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
								"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
								"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
								"WHERE a.ProductID = @ProductID " + 
								"AND (b.ProductCode LIKE @SearchKey " +
								"OR c.Description LIKE @SearchKey " + 
								"OR d.UnitName LIKE @SearchKey) " + 
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
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

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

