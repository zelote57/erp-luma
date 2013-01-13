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
	public struct DiscountDetails
	{
		public int DiscountID;
		public string DiscountCode;
		public string DiscountType;
		public decimal DiscountPrice;
		public byte InPercent;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Discount : POSConnection
    {
		#region Constructors and Destructors

		public Discount()
            : base(null, null)
        {
        }

        public Discount(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(DiscountDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblDiscount (DiscountCode, DiscountType, DiscountPrice, InPercent) " +
							"VALUES (@DiscountCode, @DiscountType, @DiscountPrice, @InPercent);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDiscountCode = new MySqlParameter("@DiscountCode",MySqlDbType.String);			
				prmDiscountCode.Value = Details.DiscountCode;
				cmd.Parameters.Add(prmDiscountCode);

				MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.String);			
				prmDiscountType.Value = Details.DiscountType;
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmDiscountPrice = new MySqlParameter("@DiscountPrice",MySqlDbType.Decimal);			
				prmDiscountPrice.Value = Details.DiscountPrice;
				cmd.Parameters.Add(prmDiscountPrice);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Byte);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;
				
				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				Int32 iID = 0;

				while (myReader.Read()) 
				{
					iID = myReader.GetInt32(0);
				}

				myReader.Close();

				return iID;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public void Update(DiscountDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblDiscount SET " + 
							"DiscountCode = @DiscountCode, " +  
							"DiscountType = @DiscountType, " +  
							"DiscountPrice = @DiscountPrice, " +  
							"InPercent = @InPercent " +  
							"WHERE DiscountID = @DiscountID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmDiscountCode = new MySqlParameter("@DiscountCode",MySqlDbType.String);			
				prmDiscountCode.Value = Details.DiscountCode;
				cmd.Parameters.Add(prmDiscountCode);

				MySqlParameter prmDiscountType = new MySqlParameter("@DiscountType",MySqlDbType.String);			
				prmDiscountType.Value = Details.DiscountType;
				cmd.Parameters.Add(prmDiscountType);

				MySqlParameter prmDiscountPrice = new MySqlParameter("@DiscountPrice",MySqlDbType.Decimal);			
				prmDiscountPrice.Value = Details.DiscountPrice;
				cmd.Parameters.Add(prmDiscountPrice);

				MySqlParameter prmInPercent = new MySqlParameter("@InPercent",MySqlDbType.Byte);			
				prmInPercent.Value = Details.InPercent;
				cmd.Parameters.Add(prmInPercent);

				MySqlParameter prmDiscountID = new MySqlParameter("@DiscountID",MySqlDbType.Int16);			
				prmDiscountID.Value = Details.DiscountID;
				cmd.Parameters.Add(prmDiscountID);

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
				string SQL=	"DELETE FROM tblDiscount WHERE DiscountID IN (" + IDs + ");";
				  
				
	 			
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
                                "DiscountID, " +
                                "DiscountCode, " +
                                "DiscountType, " +
                                "DiscountPrice, " +
                                "InPercent " +
                            "FROM tblDiscount ";

            return stSQL;
        }

		#region Details

		public DiscountDetails Details(Int32 DiscountID)
		{
			try
			{
				string SQL=SQLSelect() + "WHERE DiscountID = @DiscountID;";
				  
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDiscountID = new MySqlParameter("@DiscountID",MySqlDbType.Int16);
				prmDiscountID.Value = DiscountID;
				cmd.Parameters.Add(prmDiscountID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				DiscountDetails Details = new DiscountDetails();

				while (myReader.Read()) 
				{
                    Details.DiscountID = myReader.GetInt32("DiscountID");
                    Details.DiscountCode = "" + myReader["DiscountCode"].ToString();
                    Details.DiscountType = "" + myReader["DiscountType"].ToString();
                    Details.DiscountPrice = myReader.GetDecimal("DiscountPrice");
                    Details.InPercent = myReader.GetByte("InPercent");
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

		public DiscountDetails Details(string DiscountCode)
		{
			try
			{
				string SQL=	SQLSelect() + "WHERE DiscountCode = @DiscountCode;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmDiscountCode = new MySqlParameter("@DiscountCode",MySqlDbType.String);
				prmDiscountCode.Value = DiscountCode;
				cmd.Parameters.Add(prmDiscountCode);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				DiscountDetails Details = new DiscountDetails();

				while (myReader.Read()) 
				{
					Details.DiscountID = myReader.GetInt32("DiscountID");
					Details.DiscountCode = "" + myReader["DiscountCode"].ToString();
					Details.DiscountType = "" + myReader["DiscountType"].ToString();
					Details.DiscountPrice = myReader.GetDecimal("DiscountPrice");
					Details.InPercent =  myReader.GetByte("InPercent");
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

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
            string SQL = SQLSelect() + "ORDER BY " + SortField;

            if (SortOrder == SortOption.Ascending)
                SQL += " ASC";
            else
                SQL += " DESC";

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            System.Data.DataTable dt = new System.Data.DataTable("tblDiscount");
            base.MySqlDataAdapterFill(cmd, dt);
            

			return dt;
		}
		
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
				string SQL = SQLSelect() + "WHERE DiscountType LIKE @SearchKey ORDER BY " + SortField;

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

