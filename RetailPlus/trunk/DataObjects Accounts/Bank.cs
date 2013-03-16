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
	public struct BankDetails
	{
		public int BankID;
		public string BankCode;
		public string BankName;
        public string ChequeCode;
        public string ChequeCounter;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Bank : POSConnection
	{
		#region Bank and Destructors

        public Bank()
            : base(null, null)
        {
        }

        public Bank(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(BankDetails Details)
		{
			try 
			{
                string SQL = "INSERT INTO tblBank (BankCode, BankName, ChequeCode, ChequeCounter) " +
                                                    "VALUES (@BankCode, @BankName, @ChequeCode, @ChequeCounter);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);			
				prmBankCode.Value = Details.BankCode;
				cmd.Parameters.Add(prmBankCode);

                MySqlParameter prmBankName = new MySqlParameter("@BankName",MySqlDbType.String);
				prmBankName.Value = Details.BankName;
				cmd.Parameters.Add(prmBankName);

                MySqlParameter prmChequeCode = new MySqlParameter("@ChequeCode",MySqlDbType.String);
                prmChequeCode.Value = Details.ChequeCode;
                cmd.Parameters.Add(prmChequeCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = Details.ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);
     
				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				
				Int32 iID = 0;

				foreach(System.Data.DataRow dr in dt.Rows)
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
		public void Update(BankDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblBank SET " + 
								"BankCode		= @BankCode, " +
								"BankName		= @BankName, " +
                                "ChequeCode     = @ChequeCode, " +
                                "ChequeCounter  = @ChequeCounter " +
							"WHERE BankID = @BankID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);			
				prmBankCode.Value = Details.BankCode;
				cmd.Parameters.Add(prmBankCode);		

				MySqlParameter prmBankName = new MySqlParameter("@BankName",MySqlDbType.String);			
				prmBankName.Value = Details.BankName;
				cmd.Parameters.Add(prmBankName);

                MySqlParameter prmChequeCode = new MySqlParameter("@ChequeCode",MySqlDbType.String);
                prmChequeCode.Value = Details.ChequeCode;
                cmd.Parameters.Add(prmChequeCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = Details.ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

				MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);			
				prmBankID.Value = Details.BankID;
				cmd.Parameters.Add(prmBankID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					 
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
        public void UpdateChequeCounter(int BankID, string ChequeCounter)
        {
            try
            {
                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankID = @BankID;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

                base.ExecuteNonQuery(cmd);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public void UpdateChequeCounter(string BankCode, string ChequeCounter)
        {
            try
            {
                string SQL = "UPDATE tblBank SET " +
                                "ChequeCounter  = @ChequeCounter " +
                            "WHERE BankCode	    = @BankCode ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlParameter prmChequeCounter = new MySqlParameter("@ChequeCounter",MySqlDbType.String);
                prmChequeCounter.Value = ChequeCounter;
                cmd.Parameters.Add(prmChequeCounter);

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
				string SQL=	"DELETE FROM tblBank WHERE BankID IN (" + IDs + ");";
				  
				
	 			
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
                                "BankID, " +
                                "BankCode, " +
                                "BankName, " +
                                "ChequeCode, " +
                                "ChequeCounter " +
                            "FROM tblBank ";
            return stSQL;
        }

		#region Details

		public BankDetails Details(Int32 BankID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE BankID = @BankID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int16);
				prmBankID.Value = BankID;
				cmd.Parameters.Add(prmBankID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				BankDetails Details = new BankDetails();

				while (myReader.Read()) 
				{
					Details.BankID = BankID;
					Details.BankCode = "" + myReader["BankCode"].ToString();
					Details.BankName = "" + myReader["BankName"].ToString();
                    Details.ChequeCode = "" + myReader["ChequeCode"].ToString();
                    Details.ChequeCounter = "" + myReader["ChequeCounter"].ToString();
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
				string SQL =SQLSelect() + "ORDER BY " + SortField;

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

                System.Data.DataTable dt = new System.Data.DataTable("tblBank");
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
                string SQL = SQLSelect() + 
                            "WHERE BankCode LIKE @SearchKey " +
								"or BankName LIKE @SearchKey " +
                                "or ChequeCode LIKE @SearchKey " +
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
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() +
                            "WHERE BankCode LIKE @SearchKey " +
                                "or BankName LIKE @SearchKey " +
                                "or ChequeCode LIKE @SearchKey " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblBank");
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

        public string getChequeNo()
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = 1";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(int BankID)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankID ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(string BankCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankCode ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                string stRetValue = "";

                while (myReader.Read())
                {
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = 1";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(int BankID, out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankID ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankID = new MySqlParameter("@BankID",MySqlDbType.Int32);
                prmBankID.Value = BankID;
                cmd.Parameters.Add(prmBankID);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }
        public string getChequeNo(string BankCode, out string ChequeCode)
        {
            try
            {
                string SQL = "SELECT " +
                                    "ChequeCode, " +
                                    "ChequeCounter " +
                                "FROM tblBank WHERE BankID = @BankCode ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBankCode = new MySqlParameter("@BankCode",MySqlDbType.String);
                prmBankCode.Value = BankCode;
                cmd.Parameters.Add(prmBankCode);

                MySqlDataReader myReader = (MySqlDataReader)cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                ChequeCode = "";
                string stRetValue = "";

                while (myReader.Read())
                {
                    ChequeCode = "" + myReader["ChequeCode"].ToString();
                    long lChequeCounter = myReader.GetInt64("ChequeCounter") + 1;
                    stRetValue = "" + myReader["ChequeCounter"].ToString();
                    stRetValue = lChequeCounter.ToString().PadLeft(stRetValue.Length, '0');
                }

                return stRetValue;
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

