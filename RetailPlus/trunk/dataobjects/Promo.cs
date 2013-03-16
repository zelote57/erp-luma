using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{
	public enum PromoStatus
	{
		InActive	= 0,
		Active		= 1
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public struct PromoDetails
	{
		public long PromoID;
		public string PromoCode;
		public string PromoName;
		public DateTime StartDate;
		public DateTime EndDate;
		public PromoStatus Status;
		public int PromoTypeID;
		//Used this fields when getting details
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
	public class Promo : POSConnection
	{
		#region Constructors and Destructors

		public Promo()
            : base(null, null)
        {
        }

        public Promo(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int64 Insert(PromoDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblPromo (" + 
									"PromoCode, " +
									"PromoName, " +
									"StartDate, " +
									"EndDate, " +
									"PromoTypeID" +
								")VALUES (" +
									"@PromoCode, " +
									"@PromoName, " +
									"@StartDate, " +
									"@EndDate, " +
									"@PromoTypeID);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmPromoCode = new MySqlParameter("@PromoCode",MySqlDbType.String);			
				prmPromoCode.Value = Details.PromoCode;
				cmd.Parameters.Add(prmPromoCode);

				MySqlParameter prmPromoName = new MySqlParameter("@PromoName",MySqlDbType.String);	
				prmPromoName.Value = Details.PromoName;
				cmd.Parameters.Add(prmPromoName);
				
				MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);	
				prmStartDate.Value = Details.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStartDate);

				MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);	
				prmEndDate.Value = Details.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmEndDate);

				MySqlParameter prmPromoTypeID = new MySqlParameter("@PromoTypeID",MySqlDbType.Int32);			
				prmPromoTypeID.Value = Details.PromoTypeID;
				cmd.Parameters.Add(prmPromoTypeID);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("LAST_INSERT_ID");
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

		public void Update(PromoDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblPromo SET " + 
								"PromoCode = @PromoCode, " +  
								"PromoName = @PromoName, " +  
								"StartDate = @StartDate, " +  
								"EndDate = @EndDate, " +  
								"PromoTypeID = @PromoTypeID " +  
							"WHERE PromoID = @PromoID;";
							
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoCode = new MySqlParameter("@PromoCode",MySqlDbType.String);			
				prmPromoCode.Value = Details.PromoCode;
				cmd.Parameters.Add(prmPromoCode);

				MySqlParameter prmPromoName = new MySqlParameter("@PromoName",MySqlDbType.String);	
				prmPromoName.Value = Details.PromoName;
				cmd.Parameters.Add(prmPromoName);

				MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);	
				prmStartDate.Value = Details.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStartDate);

				MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);	
				prmEndDate.Value = Details.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmEndDate);

				MySqlParameter prmPromoTypeID = new MySqlParameter("@PromoTypeID",MySqlDbType.Int32);			
				prmPromoTypeID.Value = Details.PromoTypeID;
				cmd.Parameters.Add(prmPromoTypeID);

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int64);	
				prmPromoID.Value = Details.PromoID;
				cmd.Parameters.Add(prmPromoID);

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
				
				MySqlCommand cmd;

				string SQL=	"DELETE FROM tblPromoItems WHERE PromoID IN (" + IDs + ");";
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblPromo WHERE PromoID IN (" + IDs + ");";
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

				throw base.ThrowException(ex);
			}	
		}

		
		#endregion

		#region Details

		public PromoDetails Details(Int64 PromoID)
		{
			try
			{
				string SQL=	"SELECT " +
								"PromoID, " +
								"PromoCode, " +
								"PromoName, " +
								"StartDate, " +
								"EndDate, " +
								"Status, " +
								"a.PromoTypeID, " +
								"PromoTypeCode, " +
								"PromoTypeName " +
							"FROM tblPromo a " +
							"INNER JOIN tblPromoType b ON a.PromoTypeID = b.PromoTypeID " +
							"WHERE a.PromoID = @PromoID;"; 
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int32);
				prmPromoID.Value = PromoID;
				cmd.Parameters.Add(prmPromoID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				PromoDetails Details = new PromoDetails();

				while (myReader.Read()) 
				{
					Details.PromoID = myReader.GetInt64("PromoID");
					Details.PromoCode = "" + myReader["PromoCode"].ToString();
					Details.PromoName = "" + myReader["PromoName"].ToString();
					Details.StartDate = myReader.GetDateTime("StartDate");
					Details.EndDate = myReader.GetDateTime("EndDate");
					Details.PromoTypeID = myReader.GetInt32("PromoTypeID");
					Details.PromoTypeCode = "" + myReader["PromoTypeCode"].ToString();
					Details.PromoTypeName = "" + myReader["PromoTypeName"].ToString();
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
				string SQL =	"SELECT " +
									"PromoID, " +
									"PromoCode, " +
									"PromoName, " +
									"StartDate, " +
									"EndDate, " +
									"Status, " +
									"a.PromoTypeID, " +
									"PromoTypeCode, " +
									"PromoTypeName " +
								"FROM tblPromo a INNER JOIN " +
								"tblPromoType b ON a.PromoTypeID = b.PromoTypeID " +
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

				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
									"PromoID, " +
									"PromoCode, " +
									"PromoName, " +
									"StartDate, " +
									"EndDate, " +
									"Status, " +
									"a.PromoTypeID, " +
									"PromoTypeCode, " +
									"PromoTypeName " +
								"FROM tblPromo a INNER JOIN " +
								"tblPromoType b ON a.PromoTypeID = b.PromoTypeID " +
								"WHERE PromoCode LIKE @SearchKey " +
									"OR PromoName LIKE @SearchKey " +
									"OR PromoTypeCode LIKE @SearchKey " +
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

				throw base.ThrowException(ex);
			}	
		}		

		
		#endregion

		#region Activate and Deactivate

		public void DeActivate(string IDs)
		{
			try 
			{
				string SQL=	"UPDATE tblPromo SET Status = 0 WHERE PromoID IN (" + IDs + ");";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void Activate(string IDs)
		{
			try 
			{
				string SQL=	"UPDATE tblPromo SET Status = 1 WHERE PromoID IN (" + IDs + ");";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void ActivateByID(Int64 PromoID)
		{
			try 
			{
				string SQL=	"UPDATE tblPromo SET " + 
					"Status = 1 " +  
					"WHERE PromoID = @PromoID;";
							
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmPromoID = new MySqlParameter("@PromoID",MySqlDbType.Int64);	
				prmPromoID.Value = PromoID;
				cmd.Parameters.Add(prmPromoID);

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

