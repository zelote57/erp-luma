using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Data;

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
	public struct VariationDetails
	{
		public int VariationID;
		public string VariationCode;
		public string VariationType;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Variation : POSConnection
	{
		
		#region Constructors and Destructors

		public Variation()
            : base(null, null)
        {
        }

        public Variation(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}
		
		#endregion
		
		#region Insert and Update

		public Int32 Insert(VariationDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblVariations (VariationCode, VariationType) VALUES (@VariationCode, @VariationType);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmVariationCode = new MySqlParameter("@VariationCode",MySqlDbType.String);			
				prmVariationCode.Value = Details.VariationCode;
				cmd.Parameters.Add(prmVariationCode);

				MySqlParameter prmVariationType = new MySqlParameter("@VariationType",MySqlDbType.String);			
				prmVariationType.Value = Details.VariationType;
				cmd.Parameters.Add(prmVariationType);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
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
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public void Update(VariationDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblVariations SET " + 
							"VariationCode = @VariationCode, " +  
							"VariationType = @VariationType " +  
							"WHERE VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmVariationCode = new MySqlParameter("@VariationCode",MySqlDbType.String);			
				prmVariationCode.Value = Details.VariationCode;
				cmd.Parameters.Add(prmVariationCode);

				MySqlParameter prmVariationType = new MySqlParameter("@VariationType",MySqlDbType.String);			
				prmVariationType.Value = Details.VariationType;
				cmd.Parameters.Add(prmVariationType);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int16);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

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
				string SQL=	"DELETE FROM tblVariations WHERE VariationID IN (" + IDs + ");";
				  
				
	 			
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
                                "VariationID, " +
                                "VariationCode, " +
                                "VariationType " +
                            "FROM tblVariations ";

            return stSQL;
        }

		#region Details

		public VariationDetails Details(Int32 VariationID)
		{
			try
			{
				string SQL=	"SELECT " +
								"VariationID, " +
								"VariationCode, " +
								"VariationType " +
							"FROM tblVariations " +
							"WHERE VariationID = @VariationID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int16);
				prmVariationID.Value = VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				VariationDetails Details = new VariationDetails();

				while (myReader.Read()) 
				{
                    Details.VariationID = myReader.GetInt32("VariationID");
					Details.VariationCode = "" + myReader["VariationCode"].ToString();
					Details.VariationType = "" + myReader["VariationType"].ToString();
				}

				myReader.Close();

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public VariationDetails Details(string VariationCode)
		{
			try
			{
				string SQL=	"SELECT " +
								"VariationID, " +
								"VariationCode, " +
								"VariationType " +
							"FROM tblVariations " +
							"WHERE VariationCode = @VariationCode;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmVariationCode = new MySqlParameter("@VariationCode",MySqlDbType.String);
				prmVariationCode.Value = VariationCode;
				cmd.Parameters.Add(prmVariationCode);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				VariationDetails Details = new VariationDetails();

				while (myReader.Read()) 
				{
					Details.VariationID = myReader.GetInt32("VariationID");
					Details.VariationCode = "" + myReader["VariationCode"].ToString();
					Details.VariationType = "" + myReader["VariationType"].ToString();
				}

				myReader.Close();

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
                if (SortField == null || SortField == string.Empty) SortField = "VariationID";

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
        public DataTable ListAsDataTable(string SortField, SortOption SortOrder)
        {
            if (SortField == null || SortField == string.Empty) SortField = "VariationID";

            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC ";
            else
                SQL += " DESC ";

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;


            System.Data.DataTable dt = new System.Data.DataTable("tblVariations");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"VariationID, " +
								"VariationCode, " +
								"VariationType " +
							"FROM tblVariations " +
							"WHERE VariationType LIKE @SearchKey " +
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
		

		#endregion

	}
}

