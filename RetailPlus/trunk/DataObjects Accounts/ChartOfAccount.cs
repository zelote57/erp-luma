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
	public struct ChartOfAccountDetails
	{
		public Int32 ChartOfAccountID;
		public string ChartOfAccountCode;
		public string ChartOfAccountName;
        //public Int32 AccountCategoryID;
        //public string AccountCategoryCode;
        //public string AccountCategoryName;
        //public Int32 AccountSummaryID;
        //public string AccountSummaryCode;
        //public string AccountSummaryName;
        public AccountCategoryDetails AccountCategoryDetails; 
        //public AccountSummaryDetails AccountSummaryDetails;
        //public AccountClassificationDetails AccountClassificationDetails;
        //public short AccountClassificationID;
        //public string AccountClassificationCode;
        //public string AccountClassificationName;
        //public AccountClassificationType AccountClassificationType;
		public decimal Credit;
		public decimal Debit;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ChartOfAccounts : POSConnection
	{
		#region Constructors and Destructors

		public ChartOfAccounts()
            : base(null, null)
        {
        }

        public ChartOfAccounts(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(ChartOfAccountDetails Details)
		{
			try 
			{
                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                Int32 iID = 0;

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int32.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ChartOfAccountDetails Details)
		{
			try 
			{
                Save(Details);
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ChartOfAccountDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveChartOfAccount(@ChartOfAccountID, @AccountCategoryID, @ChartOfAccountCode, @ChartOfAccountName, " +
                                                    "@Debit, @Credit, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ChartOfAccountID", Details.ChartOfAccountID);
                cmd.Parameters.AddWithValue("AccountCategoryID", Details.AccountCategoryDetails.AccountCategoryID);
                cmd.Parameters.AddWithValue("ChartOfAccountCode", Details.ChartOfAccountCode);
                cmd.Parameters.AddWithValue("ChartOfAccountName", Details.ChartOfAccountName);
                cmd.Parameters.AddWithValue("Debit", Details.Debit);
                cmd.Parameters.AddWithValue("Credit", Details.Credit);
                cmd.Parameters.AddWithValue("CreatedOn", Details.CreatedOn == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.CreatedOn);
                cmd.Parameters.AddWithValue("LastModified", Details.LastModified == DateTime.MinValue ? Constants.C_DATE_MIN_VALUE : Details.LastModified);

                return base.ExecuteNonQuery(cmd);
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
				string SQL=	"DELETE FROM tblChartOfAccount WHERE ChartOfAccountID IN (" + IDs + ");";
				  
				
	 			
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
                                    "ChartOfAccountID, " +
                                    "a.AccountCategoryID, " +
                                    "AccountCategoryCode, " +
                                    "AccountCategoryName, " +
                                    "b.AccountSummaryID, " +
                                    "AccountSummaryCode, " +
                                    "AccountSummaryName, " +
                                    "ChartOfAccountCode, " +
                                    "ChartOfAccountName, " +
                                    "c.AccountClassificationID, " +
                                    "AccountClassificationCode, " +
                                    "AccountClassificationName, " +
                                    "AccountClassificationType, " +
                                    "Debit, " +
                                    "Credit, " +
                                    "CONCAT(RPAD(ChartOfAccountCode,10,' '), ' ', RPAD(ChartOfAccountName,50,' '), '          *', AccountSummaryName) AS ChartOfAccountNameComplete " +
                                "FROM tblChartOfAccount a " +
                                    "INNER JOIN tblAccountCategory b ON a.AccountCategoryID = b.AccountCategoryID " +
                                    "INNER JOIN tblAccountSummary c ON b.AccountSummaryID = c.AccountSummaryID " +
                                    "INNER JOIN tblAccountClassification d ON c.AccountClassificationID = d.AccountClassificationID ";
            return stSQL;
        }

		#region Details

		public ChartOfAccountDetails Details(Int32 ChartOfAccountID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE ChartOfAccountID = @ChartOfAccountID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int16);
				prmChartOfAccountID.Value = ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

                System.Data.DataTable dt = new System.Data.DataTable("ChartOfAccount");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
				
				ChartOfAccountDetails Details = new ChartOfAccountDetails();

                AccountCategories clsAccountCategory = new AccountCategories(this.Connection, this.Transaction);
                AccountSummaries clsAccountSummary = new AccountSummaries(this.Connection, this.Transaction);
                AccountClassifications clsAccountClassification = new AccountClassifications(this.Connection, this.Transaction);

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					Details.ChartOfAccountID = ChartOfAccountID;
					Details.ChartOfAccountCode = "" + dr["ChartOfAccountCode"].ToString();
					Details.ChartOfAccountName = "" + dr["ChartOfAccountName"].ToString();
                    Details.Debit = decimal.Parse(dr["Debit"].ToString());
                    Details.Credit = decimal.Parse(dr["Credit"].ToString());
                    
                    Details.AccountCategoryDetails = clsAccountCategory.Details(Int32.Parse(dr["AccountCategoryID"].ToString()));
                    //Details.AccountSummaryDetails = clsAccountSummary.Details(Int32.Parse(dr["AccountSummaryID"].ToString()));
                    //Details.AccountClassificationDetails = clsAccountClassification.Details(Int16.Parse(dr["AccountClassificationID"].ToString()));
				}

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
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "ORDER BY " + SortField;


                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblChartOfAccounts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public MySqlDataReader List(int AccountCategoryID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.AccountCategoryID = @AccountCategoryID  ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@AccountCategoryID", AccountCategoryID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public System.Data.DataTable ListAsDataTable(int AccountCategoryID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE a.AccountCategoryID = @AccountCategoryID  ORDER BY " + SortField;


                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@AccountCategoryID", AccountCategoryID);

                System.Data.DataTable dt = new System.Data.DataTable("tblChartOfAccounts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
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
				string SQL =SQLSelect() + 
                            "WHERE ChartOfAccountCode LIKE @SearchKey " +
								"or ChartOfAccountName LIKE @SearchKey " +
								"or AccountCategoryCode LIKE @SearchKey " +
								"or AccountCategoryName LIKE @SearchKey " +
								"or AccountSummaryCode LIKE @SearchKey " +
								"or AccountSummaryName LIKE @SearchKey " +
                                 "or AccountClassificationName LIKE @SearchKey " +
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit, bool isQuantityGreaterThanZERO)
        {
            try
            {
                string SQL = SQLSelect() +
                            "WHERE ChartOfAccountCode LIKE @SearchKey " +
                                "or ChartOfAccountName LIKE @SearchKey " +
                                "or AccountCategoryCode LIKE @SearchKey " +
                                "or AccountCategoryName LIKE @SearchKey " +
                                "or AccountSummaryCode LIKE @SearchKey " +
                                "or AccountSummaryName LIKE @SearchKey " +
                                 "or AccountClassificationName LIKE @SearchKey " +
                            "ORDER BY " + SortField;

                if (isQuantityGreaterThanZERO == true)
                    SQL += "AND a.Quantity > 0 ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblChartOfAccounts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public MySqlDataReader BalanceSheet(string SortField, SortOption SortOrder)
        {
            try
            {
                // `AccountClassificationType` 
                //      0 = Balance Sheet
                //      1 = Income Statement
                string SQL = SQLSelect() +
                                    "WHERE AccountClassificationType = @AccountClassificationType " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@AccountClassificationType", AccountClassificationType.BalanceSheet.ToString("d"));

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public System.Data.DataTable BalanceSheetAsDataTable(string SortField, SortOption SortOrder)
        {
            try
            {
                // `AccountClassificationType` 
                //      0 = Balance Sheet
                //      1 = Income Statement
                string SQL = SQLSelect() +
                                    "WHERE AccountClassificationType = @AccountClassificationType " +
                            "ORDER BY " + SortField;


                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@AccountClassificationType", AccountClassificationType.BalanceSheet.ToString("d"));

                System.Data.DataTable dt = new System.Data.DataTable("tblChartOfAccounts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

        public MySqlDataReader IncomeStatement(string SortField, SortOption SortOrder)
        {
            try
            {
                // `AccountClassificationType` 
                //      0 = Balance Sheet
                //      1 = Income Statement
                string SQL = SQLSelect() +
                            "WHERE AccountClassificationType = @AccountClassificationType " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@AccountClassificationType", AccountClassificationType.IncomeStatement.ToString("d"));

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader();

                return myReader;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public System.Data.DataTable IncomeStatementAsDataTable(string SortField, SortOption SortOrder)
        {
            try
            {
                // `AccountClassificationType` 
                //      0 = Balance Sheet
                //      1 = Income Statement
                string SQL = SQLSelect() +
                            "WHERE AccountClassificationType = @AccountClassificationType " +
                            "ORDER BY " + SortField;


                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@AccountClassificationType", AccountClassificationType.IncomeStatement.ToString("d"));

                System.Data.DataTable dt = new System.Data.DataTable("tblChartOfAccounts");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }

		#endregion

		#region Public Modifiers

		public void UpdateDebit(int ChartOfAccountID, decimal Amount)
		{
			try 
			{
				string SQL = "UPDATE tblChartOfAccount SET " + 
								"Debit		= Debit + @Amount " +
							"WHERE ChartOfAccountID = @ChartOfAccountID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int16);			
				prmChartOfAccountID.Value = ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		public void UpdateCredit(int ChartOfAccountID, decimal Amount)
		{
			try 
			{
				string SQL = "UPDATE tblChartOfAccount SET " + 
								"Credit		= Credit + @Amount " +
							"WHERE ChartOfAccountID = @ChartOfAccountID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmAmount = new MySqlParameter("@Amount",MySqlDbType.Decimal);			
				prmAmount.Value = Amount;
				cmd.Parameters.Add(prmAmount);

				MySqlParameter prmChartOfAccountID = new MySqlParameter("@ChartOfAccountID",MySqlDbType.Int16);			
				prmChartOfAccountID.Value = ChartOfAccountID;
				cmd.Parameters.Add(prmChartOfAccountID);

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
	}
}

