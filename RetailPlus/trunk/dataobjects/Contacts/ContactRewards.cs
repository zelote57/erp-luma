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
	public class ContactReward : POSConnection
    {
		#region Constructors and Destructors

		public ContactReward()
            : base(null, null)
        {
        }

        public ContactReward(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public bool Insert(ContactRewardDetails Details)
		{
			try  
			{
				string SQL = "CALL procContactRewardModify(@lngCustomerID, @strRewardCardNo, @intRewardActive, @decRewardPoints, @dteRewardAwardDate, @intRewardCardStatus, @dteExpiryDate, @dteBirthDate);";

				MySqlCommand cmd = new MySqlCommand();
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
				if (base.ExecuteNonQuery(cmd) > 0) bolRetValue = true;
				return bolRetValue;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
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
				throw base.ThrowException(ex);
			}	
		}

		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblContacts WHERE ContactID IN (" + IDs + ");";
				  
				
				
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
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
				  
				
				
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@ContactID", ContactID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public ContactRewardDetails Details(string RewardCardNo)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE RewardCardNo = @RewardCardNo;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				cmd.Parameters.AddWithValue("@RewardCardNo", RewardCardNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);

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
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
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

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
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

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

			    string SQL = SQLSelect() + "WHERE 1=1  ";
                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "RewardCardNo") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

			    return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
		}
		public DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
		{
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

			    string SQL = SQLSelect() + "WHERE (RewardCardNo LIKE @SearchKey or RewardActive LIKE @SearchKey) ";

                SQL += "ORDER BY " + (!string.IsNullOrEmpty(SortField) ? SortField : "RewardCardNo") + " ";
                SQL += SortOrder == SortOption.Ascending ? "ASC " : "DESC ";

                cmd.Parameters.AddWithValue("@SearchKey", SearchKey + "%");

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }

		}

		#endregion

		#region Public Modifiers

		public void AddPurchase(long ContactID, decimal Amount)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL procContactRewardsAddPurchase(@ContactID, @Amount);";

				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@Amount", Amount);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}
		public void AddPoints(long ContactID, decimal RewardPoint)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL procContactRewardsAddPoint(@ContactID, @RewardPoint);";
				
				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@RewardPoint", RewardPoint);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
		public void DeductPoints(long ContactID, decimal RewardPoint)
		{
			try 
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL procContactRewardsDeductPoint(@ContactID, @RewardPoint);";

				cmd.Parameters.AddWithValue("@ContactID", ContactID);
				cmd.Parameters.AddWithValue("@RewardPoint", RewardPoint);

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void AddMovement(long lngCustomerID, DateTime dteRewardDate, decimal decRewardPointsBefore, decimal decRewardPointsAdjustment, decimal decRewardPointsAfter, DateTime dteRewardExpiryDate, string strRewardReason, string strTerminalNo, string strCashierName, string strTransactionNo)
		{
			try
			{
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

				string SQL = "CALL procContactRewardsMovementInsert(@lngCustomerID, @dteRewardDate, @decRewardPointsBefore, @decRewardPointsAdjustment, @decRewardPointsAfter, @dteRewardExpiryDate, @strRewardReason, @strTerminalNo, @strCashierName, @strTransactionNo);";

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

                cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}
		}

		#endregion

        #region reports

        public System.Data.DataTable ActiveStatisticsReport(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT  SUM(IF(RewardActive=0,1,0)) TotalNoOfInActiveRewards " +
                                    ",SUM(IF(RewardActive=1,1,0)) TotalNoOfActiveRewards " +
                                "FROM tblContactRewards CREW ";

                
                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int32 intTotalNoOfInActiveRewards = 0;
                Int32 intTotalNoOfActiveRewards = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    intTotalNoOfInActiveRewards = Int32.TryParse(dr["TotalNoOfInActiveRewards"].ToString(), out intTotalNoOfInActiveRewards) ? intTotalNoOfInActiveRewards : 0;
                    intTotalNoOfActiveRewards = Int32.TryParse(dr["TotalNoOfActiveRewards"].ToString(), out intTotalNoOfActiveRewards) ? intTotalNoOfActiveRewards : 0; 
                }

                SQL = "SELECT " +
                                    "CALD.CalDate RewardAwardDate " +
                                    ",SUM(IF(RewardActive=0,1,0)) NoOfInActiveRewards " +
                                    ",SUM(IF(RewardActive=1,1,0)) NoOfActiveRewards " +
                                    "," + intTotalNoOfInActiveRewards + " TotalNoOfInActiveRewards " +
                                    "," + intTotalNoOfActiveRewards + " TotalNoOfActiveRewards " +
                                "FROM tblCalDate CALD " +
                                "LEFT OUTER JOIN tblContactRewards CREW ON CALD.CalDate = DATE_FORMAT(CREW.RewardAwardDate, '%Y-%m-%d') " +
                                "WHERE " +
                                    "CALD.CalDate BETWEEN DATE_FORMAT('" + StartDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')  AND " +
                                    "DATE_FORMAT('" + EndDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d') " +
                                "GROUP BY CALD.CalDate " +
                                "ORDER BY CALD.CalDate";

                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = SQL;
                dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable RewardsMovement(DateTime StartDate, DateTime EndDate, Int64 CustomerID = Constants.ZERO)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                    "RewardDate " +
                                    ",RewardPointsBefore " +
                                    ",RewardPointsAdjustment " +
                                    ",RewardPointsAfter " +
                                    ",RewardReason " +
                                    ",TerminalNo " +
                                    ",CashierName " +
                                    ",TransactionNo " +
                                "FROM tblContactRewardsMovement " +
                                "WHERE " +
                                    "RewardDate BETWEEN DATE_FORMAT('" + StartDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d')  AND " +
                                    "DATE_FORMAT('" + EndDate.ToString("yyyy-MM-dd") + "', '%Y-%m-%d') ";

                if (CustomerID != Constants.ZERO)
                {
                    SQL += "AND CustomerID = @CustomerID ";
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                }

                SQL += "ORDER BY CustomerID, RewardDate";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable RewardsSummary(DateTime StartDate, DateTime EndDate, Int64 CustomerID = Constants.ZERO)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "SELECT " +
                                "cntct.ContactID, " +
                                "cntct.ContactCode, " +
                                "cntct.ContactName, " +
                                "rwd.RewardCardNo, " +
                                "rwd.RewardPoints, " +
                                "rwdm.RewardDate, " +
                                "SUM(CASE WHEN rwdm.RewardPointsAdjustment < 0 THEN rwdm.RewardPointsAdjustment ELSE 0 END) RedeemedPoints, " +
                                "SUM(CASE WHEN rwdm.RewardPointsAdjustment > 0 THEN rwdm.RewardPointsAdjustment ELSE 0 END) AddedPoints " +
                            "FROM tblContactRewardsMovement rwdm " +
                            "INNER JOIN tblContactRewards rwd ON rwd.CustomerID = rwdm.CustomerID " +
                            "INNER JOIN tblContacts cntct ON cntct.ContactID = rwd.CustomerID " +
                            "WHERE RewardDate BETWEEN DATE_FORMAT('" + Common.ToShortDateString(StartDate) + "', '%Y-%m-%d') AND " +
                            "                         DATE_FORMAT('" + Common.ToShortDateString(EndDate) + "', '%Y-%m-%d') ";

                if (CustomerID != Constants.ZERO)
                {
                    SQL += "AND rwdm.CustomerID = @CustomerID ";
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                }
                SQL += "GROUP BY " +
                                "cntct.ContactID, " +
                                "cntct.ContactCode, " +
                                "cntct.ContactName, " +
                                "rwd.RewardCardNo, " +
                                "rwd.RewardPoints, " +
                                "rwdm.RewardDate ";
                SQL += "ORDER BY ContactName, RewardDate";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable SummarizedStatistics()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string SQL = "CALL procRewardsSummarizedStatistics();";

                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }

        #endregion

    }
}