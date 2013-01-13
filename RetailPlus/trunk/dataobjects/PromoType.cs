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
	public struct PromoTypeDetails
	{
		public int PromoTypeID;
		public string PromoTypeCode;
		public string PromoTypeName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class PromoType : POSConnection
	{
		
		#region Constructors and Destructors

		public PromoType()
            : base(null, null)
        {
        }

        public PromoType(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(PromoTypeDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblPromoType (" + 
								"PromoTypeCode, " +
								"PromoTypeName " +
								")VALUES (" +
								"@PromoTypeCode, " +
								"@PromoTypeName);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPromoTypeCode = new MySqlParameter("@PromoTypeCode",MySqlDbType.String);			
				prmPromoTypeCode.Value = Details.PromoTypeCode;
				cmd.Parameters.Add(prmPromoTypeCode);

				MySqlParameter prmPromoTypeName = new MySqlParameter("@PromoTypeName",MySqlDbType.String);	
				prmPromoTypeName.Value = Details.PromoTypeName;
				cmd.Parameters.Add(prmPromoTypeName);
				
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

		public void Update(PromoTypeDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblPromoType SET " + 
							"PromoTypeCode = @PromoTypeCode, " +  
							"PromoTypeName = @PromoTypeName " +  
							"WHERE PromoTypeID = @PromoTypeID;";
							
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoTypeCode = new MySqlParameter("@PromoTypeCode",MySqlDbType.String);			
				prmPromoTypeCode.Value = Details.PromoTypeCode;
				cmd.Parameters.Add(prmPromoTypeCode);

				MySqlParameter prmPromoTypeName = new MySqlParameter("@PromoTypeName",MySqlDbType.String);	
				prmPromoTypeName.Value = Details.PromoTypeName;
				cmd.Parameters.Add(prmPromoTypeName);

				MySqlParameter prmPromoTypeID = new MySqlParameter("@PromoTypeID",MySqlDbType.Int32);	
				prmPromoTypeID.Value = Details.PromoTypeID;
				cmd.Parameters.Add(prmPromoTypeID);

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
				
				MySqlCommand cmd;

				string SQL=	"DELETE FROM tblPromoType WHERE PromoTypeID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				
				
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

		#region Details

		public PromoTypeDetails Details(Int32 PromoTypeID)
		{
			try
			{
				string SQL=	"SELECT " +
					"PromoTypeID, " +
					"PromoTypeCode, " +
					"PromoTypeName " +
					"FROM tblPromoType a " +
					"WHERE a.PromoTypeID = @PromoTypeID;"; 
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoTypeID = new MySqlParameter("@PromoTypeID",System.Data.DbType.Int32);
				prmPromoTypeID.Value = PromoTypeID;
				cmd.Parameters.Add(prmPromoTypeID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PromoTypeDetails Details = new PromoTypeDetails();

				while (myReader.Read()) 
				{
					Details.PromoTypeID = myReader.GetInt32(0);
					Details.PromoTypeCode = myReader.GetString(1);
					Details.PromoTypeName = myReader.GetString(2);
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
				string SQL =	"SELECT " +
					"PromoTypeID, " +
					"PromoTypeCode, " +
					"PromoTypeName " +
					"FROM tblPromoType " +
					"WHERE 1=1 ORDER BY " + SortField; 

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
				string SQL =	"SELECT " +
								"PromoTypeID, " +
								"PromoTypeCode, " +
								"PromoTypeName " +
							"FROM tblPromoType " +
							"WHERE PromoTypeCode LIKE @SearchKey " +
								"OR PromoTypeName LIKE @SearchKey " +
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

