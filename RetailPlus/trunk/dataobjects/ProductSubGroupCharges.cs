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
	public struct ProductSubGroupChargeDetails
	{
		public Int64	ChargeID;
		public Int64	SubGroupID;
		public Int32	ChargeTypeID;
		public string	ChargeType;
		public decimal	ChargeAmount;
		public bool		InPercent;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductSubGroupCharge
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

		public ProductSubGroupCharge()
		{
			
		}

		public ProductSubGroupCharge(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int64 Insert(ProductSubGroupChargeDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductSubGroupCharges (" +
								"SubGroupID, " + 
								"ChargeTypeID, " +
								"ChargeAmount, " +
								"InPercent " + 
								") VALUES (" + 
								"@SubGroupID, " + 
								"@ChargeTypeID, " +
								"@ChargeAmount, " +
								"@InPercent);";

				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = Details.SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmChargeTypeID = new MySqlParameter("@ChargeTypeID",MySqlDbType.Int32);			
				prmChargeTypeID.Value = Details.ChargeTypeID;
				cmd.Parameters.Add(prmChargeTypeID);

				MySqlParameter prmChargeAmount = new MySqlParameter("@ChargeAmount",MySqlDbType.Decimal);			
				prmChargeAmount.Value = Details.ChargeAmount;
				cmd.Parameters.Add(prmChargeAmount);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Byte);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

				cmd.ExecuteNonQuery();

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                Int64 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

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

		public void Update(ProductSubGroupChargeDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductSubGroupCharges SET " + 
								"ChargeTypeID	=	@ChargeTypeID, " +
								"ChargeAmount	=	@ChargeAmount, " +
								"InPercent		=	@InPercent " + 
							"WHERE SubGroupID		=	@SubGroupID " +
							"AND ChargeID		=	@ChargeID;";
				 
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChargeTypeID = new MySqlParameter("@ChargeTypeID",MySqlDbType.Int32);			
				prmChargeTypeID.Value = Details.ChargeTypeID;
				cmd.Parameters.Add(prmChargeTypeID);

				MySqlParameter prmChargeAmount = new MySqlParameter("@ChargeAmount",MySqlDbType.Decimal);			
				prmChargeAmount.Value = Details.ChargeAmount;
				cmd.Parameters.Add(prmChargeAmount);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Byte);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = Details.SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmChargeID = new MySqlParameter("@ChargeID",MySqlDbType.Int64);			
				prmChargeID.Value = Details.ChargeID;
				cmd.Parameters.Add(prmChargeID);

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

		public bool Delete(Int64 SubGroupID, string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductSubGroupCharges WHERE SubGroupID = @SubGroupID AND ChargeID IN (" + IDs + ");";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

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

		public ProductSubGroupChargeDetails Details(Int64 ChargeID)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
							"FROM tblProductSubGroupCharges a " +
							"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
							"WHERE ChargeID = @ChargeID ";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmChargeID = new MySqlParameter("@ChargeID",MySqlDbType.Int64);			
				prmChargeID.Value = ChargeID;
				cmd.Parameters.Add(prmChargeID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader();
				
				ProductSubGroupChargeDetails Details = new ProductSubGroupChargeDetails();

				while (myReader.Read())
				{
					Details.ChargeID = myReader.GetInt64("ChargeID");
					Details.SubGroupID = myReader.GetInt64("SubGroupID");
					Details.ChargeTypeID = myReader.GetInt32("ChargeTypeID");
					Details.ChargeType = "" + myReader["ChargeType"].ToString();
					Details.ChargeAmount = myReader.GetDecimal("ChargeAmount");
					Details.InPercent = myReader.GetBoolean("InPercent");
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

		public MySqlDataReader List(Int64 SubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
						"FROM tblProductSubGroupCharges a " +
						"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
						"WHERE SubGroupID = @SubGroupID " + 
						"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

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

		public MySqlDataReader Search(Int64 SubGroupID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
						"FROM tblProductSubGroupCharges a " +
						"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
						"WHERE SubGroupID = @SubGroupID " + 
						"AND ChargeType LIKE @SearchKey " + 
						"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "% " + SearchKey + "%";
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

		public MySqlDataReader AvailableCharges(Int64 SubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT ChargeTypeID, ChargeTypeCode, ChargeType FROM tblChargeType " + 
					"WHERE ChargeTypeID NOT IN (SELECT ChargeTypeID FROM tblProductSubGroupCharges WHERE SubGroupID = @SubGroupID) " + 
					"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

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


		#endregion
	}
}

