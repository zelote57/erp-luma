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
	public struct PositionDetails
	{
		public Int16 PositionID;
		public string PositionCode;
		public string PositionName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Position : POSConnection
    {
        public const string DEFAULT_ALL_POSITIONS = "All Positions";

		#region Constructors and Destructors

		public Position()
            : base(null, null)
        {
        }

        public Position(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int16 Insert(PositionDetails Details)
		{
			try 
			{
                string SQL = "CALL procPositionInsert(@PositionCode, @PositionName);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmPositionCode = new MySqlParameter("@PositionCode",MySqlDbType.String);
                prmPositionCode.Value = Details.PositionCode;
                cmd.Parameters.Add(prmPositionCode);

                MySqlParameter prmPositionName = new MySqlParameter("@PositionName",MySqlDbType.String);
                prmPositionName.Value = Details.PositionName;
                cmd.Parameters.Add(prmPositionName);

                base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
                base.MySqlDataAdapterFill(cmd, dt);
                

                Int16 iID = 0;
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    iID = Int16.Parse(dr[0].ToString());
                }

				return iID;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}

		public void Update(PositionDetails Details)
		{
			try 
			{
                string SQL = "CALL procPositionUpdate(@PositionID, @PositionCode, @PositionName);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PositionID", Details.PositionID);
                cmd.Parameters.AddWithValue("@PositionCode", Details.PositionCode);
                cmd.Parameters.AddWithValue("@PositionName", Details.PositionName);

                base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "PositionID, " +
                                "PositionCode, " +
                                "PositionName " +
                            "FROM tblPositions ";
            return stSQL;
        }

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblPositions WHERE PositionID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);

				return true;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

		#region Details

		public PositionDetails Details(Int16 PositionID)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE PositionID = @PositionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PositionID", PositionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PositionDetails Details = new PositionDetails();

				while (myReader.Read()) 
				{
					Details.PositionID = myReader.GetInt16("PositionID");
					Details.PositionCode = "" + myReader["PositionCode"].ToString();
					Details.PositionName = "" + myReader["PositionName"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}
		public PositionDetails Details(string PositionName)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE PositionName = @PositionName;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@PositionName", PositionName);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PositionDetails Details = new PositionDetails();

				while (myReader.Read()) 
				{
					Details.PositionID = myReader.GetInt16("PositionID");
					Details.PositionCode = "" + myReader["PositionCode"].ToString();
					Details.PositionName = "" + myReader["PositionName"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}

		#endregion

		#region Streams

        public MySqlDataReader List(string SortField, SortOption SortOrder, Int32 Limit)
		{
			try
			{
                string SQL = SQLSelect() + "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}
        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder, Int32 Limit)
        {
            try
            {
                if (SortField == null || SortField == string.Empty) SortField = "PositionName";

                string SQL = SQLSelect() + "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                if (Limit != 0)
                    SQL += "LIMIT " + Limit + " ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblPositions");
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }
        public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE (PositionCode LIKE @SearchKey " +
                                            "OR PositionName LIKE @SearchKey) ";

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

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}
        public System.Data.DataTable SearchDataTable(string SearchKey, string SortField, SortOption SortOrder, Int32 Limit)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE (PositionCode LIKE @SearchKey " +
                                            "OR PositionName LIKE @SearchKey) ";

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

                System.Data.DataTable dt = new System.Data.DataTable("tblPositions");
                base.MySqlDataAdapterFill(cmd, dt);
                

                return dt;
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }
		
		#endregion
	}
}

