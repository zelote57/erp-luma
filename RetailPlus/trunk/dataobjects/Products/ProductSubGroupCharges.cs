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
	public struct ProductSubGroupChargeDetails
	{
		public Int64	ChargeID;
		public Int64	SubGroupID;
		public Int32	ChargeTypeID;
		public string	ChargeType;
		public decimal	ChargeAmount;
		public bool		InPercent;

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
	public class ProductSubGroupCharges : POSConnection
	{
		#region Constructors and Destructors

		public ProductSubGroupCharges()
            : base(null, null)
        {
        }

        public ProductSubGroupCharges(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
			
		#region Insert and Update

		public Int64 Insert(ProductSubGroupChargeDetails Details)
		{
			try 
			{
                Save(Details);

                string SQL = "SELECT LAST_INSERT_ID();";
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
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
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductSubGroupChargeDetails Details)
		{
			try 
			{
				string SQL = "UPDATE tblProductSubGroupCharges SET " + 
								"ChargeTypeID	=	@ChargeTypeID, " +
								"ChargeAmount	=	@ChargeAmount, " +
								"InPercent		=	@InPercent " + 
							"WHERE SubGroupID		=	@SubGroupID " +
							"AND ChargeID		=	@ChargeID;";
				 
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ChargeTypeID", Details.ChargeTypeID);
                cmd.Parameters.AddWithValue("@ChargeAmount", Details.ChargeAmount);
                cmd.Parameters.AddWithValue("@InPercent", Details.InPercent);
                cmd.Parameters.AddWithValue("@SubGroupID", Details.SubGroupID);
                cmd.Parameters.AddWithValue("@ChargeID", Details.ChargeID);

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ProductSubGroupChargeDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductSubGroupCharges(@ChargeID, @SubGroupID, @ChargeTypeID, @ChargeAmount, @InPercent, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("ChargeID", Details.ChargeID);
                cmd.Parameters.AddWithValue("SubGroupID", Details.SubGroupID);
                cmd.Parameters.AddWithValue("ChargeTypeID", Details.ChargeTypeID);
                cmd.Parameters.AddWithValue("ChargeAmount", Details.ChargeAmount);
                cmd.Parameters.AddWithValue("InPercent", Details.InPercent);
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

		public bool Delete(Int64 SubGroupID, string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductSubGroupCharges WHERE SubGroupID = @SubGroupID AND ChargeID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

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

		public ProductSubGroupChargeDetails Details(Int64 ChargeID)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
							"FROM tblProductSubGroupCharges a " +
							"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
							"WHERE ChargeID = @ChargeID ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmChargeID = new MySqlParameter("@ChargeID",MySqlDbType.Int64);			
				prmChargeID.Value = ChargeID;
				cmd.Parameters.Add(prmChargeID);

				ProductSubGroupChargeDetails Details = new ProductSubGroupChargeDetails();

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
                while (myReader.Read())
				{
					Details.ChargeID = myReader.GetInt64("ChargeID");
					Details.SubGroupID = myReader.GetInt64("SubGroupID");
					Details.ChargeTypeID = myReader.GetInt32("ChargeTypeID");
					Details.ChargeType = "" + myReader["ChargeType"].ToString();
					Details.ChargeAmount = myReader.GetDecimal("ChargeAmount");
					Details.InPercent = myReader.GetBoolean("InPercent");
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

		public MySqlDataReader List(Int64 SubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
						"FROM tblProductSubGroupCharges a " +
						"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
						"WHERE SubGroupID = @SubGroupID " + 
						"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader Search(Int64 SubGroupID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"a.ChargeID, " +
								"a.SubGroupID, " +
								"a.ChargeTypeID, " +
								"b.ChargeType, " +
								"a.ChargeAmount, " +
								"a.InPercent " +
						"FROM tblProductSubGroupCharges a " +
						"LEFT JOIN tblChargeType b ON a.ChargeTypeID = b.ChargeTypeID " +
						"WHERE SubGroupID = @SubGroupID " + 
						"AND ChargeType LIKE @SearchKey " + 
						"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "% " + SearchKey + "%";
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

		public MySqlDataReader AvailableCharges(Int64 SubGroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT ChargeTypeID, ChargeTypeCode, ChargeType FROM tblChargeType " + 
					"WHERE ChargeTypeID NOT IN (SELECT ChargeTypeID FROM tblProductSubGroupCharges WHERE SubGroupID = @SubGroupID) " + 
					"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmSubGroupID = new MySqlParameter("@SubGroupID",MySqlDbType.Int64);			
				prmSubGroupID.Value = SubGroupID;
				cmd.Parameters.Add(prmSubGroupID);

				
			
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
	}
}

