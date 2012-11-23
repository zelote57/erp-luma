using System;
using System.Data;
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
	#region Struct

	public struct ContactRewardDetails
	{
		public long ContactID;
		public string RewardCardNo;
		public bool RewardActive;
		public decimal RewardPoints;
		public DateTime RewardAwardDate;
		public decimal TotalPurchases;
		public decimal RedeemedPoints;
		public RewardCardStatus RewardCardStatus;
		public DateTime ExpiryDate;
		public DateTime BirthDate;
	}

	
	#endregion

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ContactReward
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

		public ContactReward()
		{
			
		}

		public ContactReward(MySqlConnection Connection, MySqlTransaction Transaction)
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

		public bool Insert(ContactRewardDetails Details)
		{
			try  
			{
				string SQL = "CALL procContactRewardModify(@lngCustomerID, @strRewardCardNo, @intRewardActive, @decRewardPoints, @dteRewardAwardDate, @intRewardCardStatus, @dteExpiryDate, @dteBirthDate);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngCustomerID", Details.ContactID);
				cmd.Parameters.AddWithValue("@strRewardCardNo", Details.RewardCardNo);
				cmd.Parameters.AddWithValue("@intRewardActive", Convert.ToInt16(Details.RewardActive));
				cmd.Parameters.AddWithValue("@decRewardPoints", Convert.ToDecimal(0)); // not working if decimal
				cmd.Parameters.AddWithValue("@dteRewardAwardDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				cmd.Parameters.AddWithValue("@intRewardCardStatus", Details.RewardCardStatus.ToString("d"));
				cmd.Parameters.AddWithValue("@dteExpiryDate", Details.ExpiryDate.ToString("yyyy-MM-dd"));
				cmd.Parameters.AddWithValue("@dteBirthDate", Details.BirthDate.ToString("yyyy-MM-dd"));

				bool bolRetValue = false;
				if (cmd.ExecuteNonQuery() > 0) bolRetValue = true;
				return bolRetValue;
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
		public bool Update(ContactRewardDetails Details)
		{
			try 
			{
				return Insert(Details);
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
				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";
				  
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
								"CustomerID, " +
								"RewardCardNo, " +
								"RewardActive, " +
								"RewardPoints, " +
								"RewardAwardDate, " +
								"TotalPurchases, " +
								"RedeemedPoints, " +
								"RewardCardStatus, " +
								"ExpiryDate, " +
								"BirthDate " +
							"FROM tblContactRewards ";
			return stSQL;
		}

		#region Details

		public ContactRewardDetails Details(long ContactID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE CustomerID = @ContactID;";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ContactID", ContactID);

				MySqlDataReader myReader = (MySqlDataReader) cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
				
				ContactRewardDetails Details = new ContactRewardDetails();

				while (myReader.Read()) 
				{
					Details.ContactID = myReader.GetInt64("CustomerID");
					Details.RewardCardNo = "" + myReader["RewardCardNo"].ToString();
					Details.RewardActive = myReader.GetBoolean("RewardActive");
					Details.RewardPoints = myReader.GetDecimal("RewardPoints");
					Details.RewardAwardDate = myReader.GetDateTime("RewardAwardDate");
					Details.TotalPurchases = myReader.GetDecimal("TotalPurchases");
					Details.RedeemedPoints = myReader.GetDecimal("RedeemedPoints");
					Details.RewardCardStatus = (RewardCardStatus)Enum.Parse(typeof(RewardCardStatus), myReader.GetString("RewardCardStatus"));
					Details.ExpiryDate = myReader.GetDateTime("ExpiryDate");
					Details.BirthDate = myReader.GetDateTime("BirthDate");
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
		public ContactRewardDetails Details(string RewardCardNo)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE RewardCardNo = @RewardCardNo;";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);

				MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

				ContactRewardDetails Details = new ContactRewardDetails();

				while (myReader.Read())
				{
					Details.ContactID = myReader.GetInt64("CustomerID");
					Details.RewardCardNo = "" + myReader["RewardCardNo"].ToString();
					Details.RewardActive = myReader.GetBoolean("RewardActive");
					Details.RewardPoints = myReader.GetDecimal("RewardPoints");
					Details.RewardAwardDate = myReader.GetDateTime("RewardAwardDate");
					Details.TotalPurchases = myReader.GetDecimal("TotalPurchases");
					Details.RedeemedPoints = myReader.GetDecimal("RedeemedPoints");
					Details.RewardCardStatus = (RewardCardStatus)Enum.Parse(typeof(RewardCardStatus), myReader.GetString("RewardCardStatus"));
					Details.ExpiryDate = myReader.GetDateTime("ExpiryDate");
					Details.BirthDate = myReader.GetDateTime("BirthDate");
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
				string SQL = SQLSelect() + "WHERE 1=1 ORDER BY " + SortField; 

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
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' " +
								"AND (ContactCode LIKE @SearchKey " +
								"OR ContactName LIKE @SearchKey) " +
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

		public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
			string SQL = SQLSelect() + "WHERE ORDER BY " + SortField;

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

			System.Data.DataTable dt = new System.Data.DataTable("tblContactRewards");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}
		public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
		{
			string SQL = SQLSelect() + "WHERE (RewardCardNo LIKE @SearchKey or RewardActive LIKE @SearchKey) ";

			SQL += "ORDER BY " + SortField;

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

			cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");

			System.Data.DataTable dt = new System.Data.DataTable("tblContactRewards");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		public DataTable ActiveStatisticsReport(DateTime StartDate, DateTime EndDate)
		{
			string SQL = "SELECT " +
								"CALD.CalDate RewardAwardDate " +
								",SUM(IF(RewardActive=0,1,0)) NoOfInActiveRewards " +
								",SUM(IF(RewardActive=1,1,0)) NoOfActiveRewards " +
							"FROM tblCalDate CALD " +
							"LEFT OUTER JOIN tblContactRewards CREW ON CALD.CalDate = DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d') " +
							"WHERE " +
								"CALD.CalDate BETWEEN DATE_FORMAT('" + StartDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')  AND " +
								"DATE_FORMAT('" + EndDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d') " +
							"GROUP BY CALD.CalDate " +
							"ORDER BY CALD.CalDate";

			MySqlConnection cn = GetConnection();

			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = cn;
			cmd.Transaction = mTransaction;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = SQL;

			System.Data.DataTable dt = new System.Data.DataTable("tblContactRewards");
			MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
			adapter.Fill(dt);

			return dt;
		}

		#endregion

		#region Public Modifiers

		public void AddPurchase(long ContactID, decimal Amount)
		{
			try
			{
				string SQL = "CALL procContactRewardsAddPurchase(@ContactID, @Amount);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@Amount", Amount);

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
		public void AddPoints(long ContactID, decimal RewardPoint)
		{
			try 
			{
				string SQL = "CALL procContactRewardsAddPoint(@ContactID, @RewardPoint);";
				  
				MySqlConnection cn = GetConnection();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@RewardPoint", RewardPoint);

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
		public void DeductPoints(long ContactID, decimal RewardPoint)
		{
			try 
			{
				string SQL = "CALL procContactRewardsDeductPoint(@ContactID, @RewardPoint);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@RewardPoint", RewardPoint);

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

		public void AddMovement(long lngCustomerID, DateTime dteRewardDate, decimal decRewardPointsBefore, decimal decRewardPointsAdjustment, decimal decRewardPointsAfter, DateTime dteRewardExpiryDate, string strRewardReason, string strTerminalNo, string strCashierName, string strTransactionNo)
		{
			try
			{
				string SQL = "CALL procContactRewardsMovementInsert(@lngCustomerID, @dteRewardDate, @decRewardPointsBefore, @decRewardPointsAdjustment, @decRewardPointsAfter, @dteRewardExpiryDate, @strRewardReason, @strTerminalNo, @strCashierName, @strTransactionNo);";

				MySqlConnection cn = GetConnection();

				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = cn;
				cmd.Transaction = mTransaction;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@lngCustomerID", lngCustomerID);
				cmd.Parameters.AddWithValue("@dteRewardDate", dteRewardDate);
				cmd.Parameters.AddWithValue("@decRewardPointsBefore", decRewardPointsBefore);
				cmd.Parameters.AddWithValue("@decRewardPointsAdjustment", decRewardPointsAdjustment);
				cmd.Parameters.AddWithValue("@decRewardPointsAfter", decRewardPointsAfter);
				cmd.Parameters.AddWithValue("@dteRewardExpiryDate", dteRewardExpiryDate);
				cmd.Parameters.AddWithValue("@strRewardReason", strRewardReason);
				cmd.Parameters.AddWithValue("@strTerminalNo", strTerminalNo);
				cmd.Parameters.AddWithValue("@strCashierName", strCashierName);
				cmd.Parameters.AddWithValue("@strTransactionNo", strTransactionNo);

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