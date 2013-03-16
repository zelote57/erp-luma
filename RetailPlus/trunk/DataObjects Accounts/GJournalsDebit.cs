
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
	public struct GJournalsDebitDetails
	{
		public long GJournalDebitID;
		public long GJournalID;
		public int ChartOfAccountID;
		public decimal Amount;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class GJournalsDebit : POSConnection
	{
		#region Constructors and Destructors

		public GJournalsDebit()
            : base(null, null)
        {
        }

        public GJournalsDebit(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public long Insert(GJournalsDebitDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblGJournalDebit (" +
								"GJournalID, " +
								"ChartOfAccountID, " +
								"Amount" +
							") VALUES (" +
								"@GJournalID, " +
								"@ChartOfAccountID, " +
								"@Amount" +
							");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = Details.GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int64);			
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);
     
				base.ExecuteNonQuery(cmd);

                SQL = "SELECT LAST_INSERT_ID();";

                cmd.Parameters.Clear();
                cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int64 iID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int64.Parse(dr[0].ToString());
                }

                return iID;
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void Update(GJournalsDebitDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblGJournalDebit SET " + 
								"GJournalID			=	@GJournalID, " +
								"ChartOfAccountID	=	@ChartOfAccountID, " +
								"Amount				=	@Amount " +
							"WHERE GJournalDebitID	=	@GJournalDebitID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = Details.GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int64);			
				prmChartOfAccountID.Value = Details.ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Details.Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlParameter prmGJournalDebitID = new MySqlParameter("@GJournalDebitID",MySqlDbType.Int64);				
				prmGJournalDebitID.Value = Details.GJournalDebitID;
				cmd.Parameters.Add(prmGJournalDebitID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblGJournalDebit WHERE GJournalDebitID IN (" + IDs + ");";
				  
				
	 			
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

		#region Details

		public GJournalsDebitDetails Details(long GJournalDebitID)
		{
			try
			{
				string SQL =	"SELECT " +
									"GJournalDebitID, " +
									"GJournalID, " +
									"a.ChartOfAccountID, " +
									"ChartOfAccountCode, " +
									"ChartOfAccountName, " +
									"Amount " +
								"FROM tblGJournalDebit a INNER JOIN tblChartOfAccount b " +
									"ON a.ChartOfAccountID = b.ChartOfAccountID " +
								"WHERE GJournalDebitID = @GJournalDebitID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGJournalDebitID = new MySqlParameter("@GJournalDebitID",MySqlDbType.Int64);			
				prmGJournalDebitID.Value = GJournalDebitID;
				cmd.Parameters.Add(prmGJournalDebitID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				GJournalsDebitDetails Details = new GJournalsDebitDetails();

				while (myReader.Read()) 
				{
					Details.GJournalDebitID = GJournalDebitID;
					Details.GJournalID = myReader.GetInt64("GJournalID");
					Details.ChartOfAccountID = myReader.GetInt32("ChartOfAccountID");
					Details.Amount = myReader.GetDecimal("Amount");
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
				string SQL ="SELECT " +
								"GJournalDebitID, " +
								"GJournalID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblGJournalDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(long GJournalID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"GJournalDebitID, " +
								"GJournalID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblGJournalDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE GJournalID = @GJournalID " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
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
				string SQL ="SELECT " +
								"GJournalDebitID, " +
								"GJournalID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblGJournalDebit a INNER JOIN tblChartOfAccount b " +
							"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE GJournalID LIKE @SearchKey " +
								"or ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		public MySqlDataReader Search(long GJournalID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"GJournalDebitID, " +
								"GJournalID, " +
								"a.ChartOfAccountID, " +
								"ChartOfAccountCode, " +
								"ChartOfAccountName, " +
								"Amount " +
							"FROM tblGJournalDebit a INNER JOIN tblChartOfAccount b " +
								"ON a.ChartOfAccountID = b.ChartOfAccountID " +
							"WHERE GJournalID = @GJournalID " +
								"AND (ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey) " +
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGJournalID = new MySqlParameter("@GJournalID",MySqlDbType.Int64);			
				prmGJournalID.Value = GJournalID;
				cmd.Parameters.Add(prmGJournalID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey +"%";
				cmd.Parameters.Add(prmSearchKey);

				MySqlDataReader myReader = base.ExecuteReader(cmd);
				
				return myReader;			
			}
			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}		

		#endregion

		public void Post(long GJournalID)
		{
			try 
			{
				GetConnection();
				ChartOfAccount clsChartOfAccount = new ChartOfAccount(Connection, Transaction);

				MySqlDataReader myReader = List(GJournalID, "GJournalDebitID",SortOption.Ascending);
				while (myReader.Read())
				{
					int ChartOfAccountID = myReader.GetInt32("ChartOfAccountID");
					decimal Amount = myReader.GetDecimal("Amount");

					clsChartOfAccount.UpdateDebit(ChartOfAccountID, Amount);
				}
				myReader.Close();

			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
	}
}

