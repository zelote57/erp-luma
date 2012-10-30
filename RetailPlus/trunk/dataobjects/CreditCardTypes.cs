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
	public struct CardTypeDetails
	{
		public Int16 CardTypeID;
		public string CardTypeCode;
		public string CardTypeName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class CardType
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

		public CardType()
		{
			
		}

		public CardType(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public Int16 Insert(CardTypeDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblCardTypes (CardTypeCode, CardTypeName) VALUES (@CardTypeCode, @CardTypeName);";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCardTypeCode = new MySqlParameter("@CardTypeCode",MySqlDbType.String);			
				prmCardTypeCode.Value = Details.CardTypeCode;
				cmd.Parameters.Add(prmCardTypeCode);

				MySqlParameter prmCardTypeName = new MySqlParameter("@CardTypeName",MySqlDbType.String);			
				prmCardTypeName.Value = Details.CardTypeName;
				cmd.Parameters.Add(prmCardTypeName);

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

		public void Update(CardTypeDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblCardTypes SET " + 
							"CardTypeCode = @CardTypeCode, " +  
							"CardTypeName = @CardTypeName " +  
							"WHERE CardTypeID = @CardTypeID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmCardTypeCode = new MySqlParameter("@CardTypeCode",MySqlDbType.String);			
				prmCardTypeCode.Value = Details.CardTypeCode;
				cmd.Parameters.Add(prmCardTypeCode);

				MySqlParameter prmCardTypeName = new MySqlParameter("@CardTypeName",MySqlDbType.String);			
				prmCardTypeName.Value = Details.CardTypeName;
				cmd.Parameters.Add(prmCardTypeName);

				MySqlParameter prmCardTypeID = new MySqlParameter("@CardTypeID",MySqlDbType.Int16);			
				prmCardTypeID.Value = Details.CardTypeID;
				cmd.Parameters.Add(prmCardTypeID);

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

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblCardTypes WHERE CardTypeID IN (" + IDs + ");";
				  
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

		public CardTypeDetails Details(Int16 CardTypeID)
		{
			try
			{
				string SQL=	"SELECT " +
								"CardTypeID, " +
								"CardTypeCode, " +
								"CardTypeName " +
							"FROM tblCardTypes " +
							"WHERE CardTypeID = @CardTypeID;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CardTypeID", CardTypeID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				CardTypeDetails Details = new CardTypeDetails();

				while (myReader.Read()) 
				{
					Details.CardTypeID = myReader.GetInt16("CardTypeID");
					Details.CardTypeCode = "" + myReader["CardTypeCode"].ToString();
					Details.CardTypeName = "" + myReader["CardTypeName"].ToString();
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
		public CardTypeDetails Details(string CardTypeName)
		{
			try
			{
				string SQL=	"SELECT " +
								"CardTypeID, " +
								"CardTypeCode, " +
								"CardTypeName " +
							"FROM tblCardTypes " +
							"WHERE CardTypeName = @CardTypeName;";
				  
				MySqlConnection cn = GetConnection();
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@CardTypeName", CardTypeName);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				CardTypeDetails Details = new CardTypeDetails();

				while (myReader.Read()) 
				{
					Details.CardTypeID = myReader.GetInt16("CardTypeID");
					Details.CardTypeCode = "" + myReader["CardTypeCode"].ToString();
					Details.CardTypeName = "" + myReader["CardTypeName"].ToString();
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
				string SQL = "SELECT " +
								"CardTypeID, " +
								"CardTypeCode, " +
								"CardTypeName " +
							"FROM tblCardTypes " +
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
			
			System.Data.DataTable dt = new System.Data.DataTable("tblCardTypes");

			dt.Columns.Add("CardTypeID");
			dt.Columns.Add("CardTypeCode");
			dt.Columns.Add("CardTypeName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["CardTypeID"] = myReader.GetInt16("CardTypeID");
				dr["CardTypeCode"] = "" + myReader["CardTypeCode"].ToString();
				dr["CardTypeName"] = "" + myReader["CardTypeName"].ToString();
					
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
								"CardTypeID, " +
								"CardTypeCode, " +
								"CardTypeName " +
							"FROM tblCardTypes " +
							"WHERE CardTypeName LIKE @SearchKey " +
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
			
			System.Data.DataTable dt = new System.Data.DataTable("tblCardTypes");

			dt.Columns.Add("CardTypeID");
			dt.Columns.Add("CardTypeCode");
			dt.Columns.Add("CardTypeName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["CardTypeID"] = myReader.GetInt16("CardTypeID");
				dr["CardTypeCode"] = "" + myReader["CardTypeCode"].ToString();
				dr["CardTypeName"] = "" + myReader["CardTypeName"].ToString();
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		
		#endregion
	}
}

