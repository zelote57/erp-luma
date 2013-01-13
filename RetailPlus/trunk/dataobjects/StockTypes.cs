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
	public struct StockTypesDetails
	{
		public Int16 StockTypeID;
		public string StockTypeCode;
		public string Description;
		public StockDirections StockDirection;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class StockTypes : POSConnection
	{
		
		#region Constructors and Destructors

		public StockTypes()
            : base(null, null)
        {
        }

        public StockTypes(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public short Insert(StockTypesDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblStockType (StockTypeCode, Description, StockDirection) VALUES (@StockTypeCode, @Description, @StockDirection);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockTypeCode = new MySqlParameter("@StockTypeCode",MySqlDbType.String);			
				prmStockTypeCode.Value = Details.StockTypeCode;
				cmd.Parameters.Add(prmStockTypeCode);

				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);			
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);

				MySqlParameter prmStockDirection = new MySqlParameter("@StockDirection",MySqlDbType.Byte);			
				prmStockDirection.Value = Details.StockDirection.ToString("d");
				cmd.Parameters.Add(prmStockDirection);

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
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public void Update(StockTypesDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblStockType SET " + 
							"StockTypeCode = @StockTypeCode, " +  
							"Description = @Description, " +
							"StockDirection = @StockDirection " +
							"WHERE StockTypeID = @StockTypeID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockTypeCode = new MySqlParameter("@StockTypeCode",MySqlDbType.String);			
				prmStockTypeCode.Value = Details.StockTypeCode;
				cmd.Parameters.Add(prmStockTypeCode);

				MySqlParameter prmDescription = new MySqlParameter("@Description",MySqlDbType.String);			
				prmDescription.Value = Details.Description;
				cmd.Parameters.Add(prmDescription);

				MySqlParameter prmStockDirection = new MySqlParameter("@StockDirection",MySqlDbType.Byte);			
				prmStockDirection.Value = Details.StockDirection.ToString("d");
				cmd.Parameters.Add(prmStockDirection);

				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);			
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
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
				string SQL=	"DELETE FROM tblStockType WHERE StockTypeID IN (" + IDs + ");";
				  
				
	 			
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

				throw ex;
			}	
		}


		#endregion

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "StockTypeID, " +
                                "StockTypeCode, " +
                                "Description, " +
                                "StockDirection " +
                            "FROM tblStockType ";

            return stSQL;
        }

		#region Details

		public StockTypesDetails Details(Int16 StockTypeID)
		{
			try
			{
				string SQL=	"SELECT " +
								"StockTypeID, " +
								"StockTypeCode, " +
								"Description, " +
								"StockDirection " +
							"FROM tblStockType " +
							"WHERE StockTypeID = @StockTypeID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);
				prmStockTypeID.Value = StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);

                System.Data.DataTable dt = new System.Data.DataTable("StockType");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				StockTypesDetails Details = new StockTypesDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.StockTypeID = StockTypeID;
					Details.StockTypeCode = "" + dr["StockTypeCode"].ToString();
					Details.Description = "" + dr["Description"].ToString();
                    Details.StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), Convert.ToInt16(dr["StockDirection"]).ToString());
				}

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
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
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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

				throw ex;
			}	
		}
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =SQLSelect() + "WHERE Description LIKE @SearchKey " +
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

				throw ex;
			}	
		}

        public System.Data.DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("tblSTockTypes");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }
        public System.Data.DataTable SearchAsDataTable(string SearchKey, string SortField, SortOption SortOrder)
        {
            string SQL = SQLSelect() + "WHERE (StockTypeCode LIKE @SearchKey or Description LIKE @SearchKey) ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            cmd.Parameters.AddWithValue("@SearchKey", SearchKey);

            System.Data.DataTable dt = new System.Data.DataTable("tblSTockTypes");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }

		#endregion
	}
}

