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
	public struct ProductUnitDetails
	{
		public int ProductUnitID;
		public string ProductUnitCode;
		public string ProductUnitName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductUnit
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

		public ProductUnit()
		{
			
		}

		public ProductUnit(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int32 Insert(ProductUnitDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductUnit (ProductUnitCode, ProductUnitName) VALUES (@ProductUnitCode, @ProductUnitName);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);			
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);

				MySqlParameter prmProductUnitName = new MySqlParameter("@ProductUnitName",MySqlDbType.String);			
				prmProductUnitName.Value = Details.ProductUnitName;
				cmd.Parameters.Add(prmProductUnitName);

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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}

		public void Update(ProductUnitDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductUnit SET " + 
							"ProductUnitCode = @ProductUnitCode, " +  
							"ProductUnitName = @ProductUnitName " +  
							"WHERE ProductUnitID = @ProductUnitID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);			
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);

				MySqlParameter prmProductUnitName = new MySqlParameter("@ProductUnitName",MySqlDbType.String);			
				prmProductUnitName.Value = Details.ProductUnitName;
				cmd.Parameters.Add(prmProductUnitName);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);			
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductUnit WHERE ProductUnitID IN (" + IDs + ");";
				  
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

		public ProductUnitDetails Details(Int32 ProductUnitID)
		{
			try
			{
				string SQL=	"SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
							"WHERE ProductUnitID = @ProductUnitID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",System.Data.DbType.Int32);
				prmProductUnitID.Value = ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ProductUnitDetails Details = new ProductUnitDetails();

				while (myReader.Read()) 
				{
					Details.ProductUnitID = ProductUnitID;
					Details.ProductUnitCode = myReader.GetString(1);
					Details.ProductUnitName = myReader.GetString(2);
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
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

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductUnit");

			dt.Columns.Add("ProductUnitID");
			dt.Columns.Add("ProductUnitCode");
			dt.Columns.Add("ProductUnitName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductUnitID"] = myReader.GetInt16(0);
				dr["ProductUnitCode"] = myReader.GetString(1);
				dr["ProductUnitName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
							"WHERE ProductUnitName LIKE @SearchKey " +
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
					mTransaction.Rollback();

				mTransaction.Dispose(); 
				mConnection.Close();
				mConnection.Dispose();

				throw ex;
			}	
		}
		
		public System.Data.DataTable DataSearch(string SearchKey, string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = Search(SearchKey,SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductUnit");

			dt.Columns.Add("ProductUnitID");
			dt.Columns.Add("ProductUnitCode");
			dt.Columns.Add("ProductUnitName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductUnitID"] = myReader.GetInt16(0);
				dr["ProductUnitCode"] = myReader.GetString(1);
				dr["ProductUnitName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		#endregion

		#region Public Modifiers

		public decimal GetBaseUnitValue(Int64 ProductID, Int32 UnitIDToConvert, decimal Quantity)
		{
			try
			{
				MySqlConnection cn = GetConnection();
	 			
				Product clsProduct = new Product(cn, mTransaction);
				Int32 BaseUnitID = clsProduct.Details(ProductID).BaseUnitID;
				
				Int32 origUnitIDToConvert = UnitIDToConvert;

				decimal ConvertedUnit = Quantity;

				while (BaseUnitID != UnitIDToConvert)
				{
					string SQL=	"SELECT " +
									"BaseUnitID, " +
									"BottomUnitValue, " +
									"BaseUnitValue " +
								"FROM tblProductUnitMatrix " +
								"WHERE ProductID = @ProductID " +
								"AND BottomUnitID = @UnitIDToConvert ";
				  
					MySqlCommand cmd = new MySqlCommand();
					cmd.Connection = cn;
					cmd.Transaction = mTransaction;
					cmd.CommandType = System.Data.CommandType.Text;
					cmd.CommandText = SQL;

					MySqlParameter prmProductID = new MySqlParameter("@ProductID",System.Data.DbType.Int64);
					prmProductID.Value = ProductID;
					cmd.Parameters.Add(prmProductID);

					MySqlParameter prmUnitIDToConvert = new MySqlParameter("@UnitIDToConvert",System.Data.DbType.Int32);
					prmUnitIDToConvert.Value = UnitIDToConvert;
					cmd.Parameters.Add(prmUnitIDToConvert);

					MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

					int iCtr = 0;
					while (myReader.Read()) 
					{
						iCtr++;
						UnitIDToConvert = myReader.GetInt32("BaseUnitID");
						ConvertedUnit = myReader.GetDecimal("BaseUnitValue") * ConvertedUnit / myReader.GetDecimal("BottomUnitValue");
					}
					myReader.Close();
					if (iCtr==0) break;
				}

				return ConvertedUnit;
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

