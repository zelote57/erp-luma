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
	public struct ProductVariationDetails
	{
		public Int64 ProductID;
		public Int32 VariationID;
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
	public class ProductVariation : POSConnection
	{
		#region Constructors and Destructors

		public ProductVariation()
            : base(null, null)
        {
        }

        public ProductVariation(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int32 Insert(ProductVariationDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductVariations (ProductID, VariationID) VALUES (@ProductID, @VariationID);";

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

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

//		public void Update(ProductSubGroupDetails Details)
//		{
//			try 
//			{
//				string SQL = "UPDATE tblProductSubGroup SET " + 
//					"ProductGroupID = @ProductGroupID, " +
//					"ProductSubGroupCode = @ProductSubGroupCode, " +  
//					"ProductSubGroupName = @ProductSubGroupName " +  
//					"WHERE ProductSubGroupID = @ProductSubGroupID;";
//				  
//				
//	 			
//				MySqlCommand cmd = new MySqlCommand();
//				
//				
//				cmd.CommandType = System.Data.CommandType.Text;
//				cmd.CommandText = SQL;
//
//				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int16);			
//				prmProductGroupID.Value = Details.ProductGroupID;
//				cmd.Parameters.Add(prmProductGroupID);
//				
//				MySqlParameter prmProductSubGroupCode = new MySqlParameter("@ProductSubGroupCode",MySqlDbType.String);			
//				prmProductSubGroupCode.Value = Details.ProductSubGroupCode;
//				cmd.Parameters.Add(prmProductSubGroupCode);
//
//				MySqlParameter prmProductSubGroupName = new MySqlParameter("@ProductSubGroupName",MySqlDbType.String);			
//				prmProductSubGroupName.Value = Details.ProductSubGroupName;
//				cmd.Parameters.Add(prmProductSubGroupName);
//
//				MySqlParameter prmProductSubGroupID = new MySqlParameter("@ProductSubGroupID",MySqlDbType.Int16);			
//				prmProductSubGroupID.Value = Details.ProductSubGroupID;
//				cmd.Parameters.Add(prmProductSubGroupID);
//
//				base.ExecuteNonQuery(cmd);
//			}
//
//			catch (Exception ex)
//			{
//				
//				
//				{
//					
//					
//					
//					
//				}
//
//				throw ex;
//			}	
//		}

		public void Update(ProductVariationDetails Details, Int32 ProductVariationIDOld)
		{
			try 
			{
				string SQL = "UPDATE tblProductVariations SET " + 
								"VariationID = @VariationID " +
							"WHERE ProductID = @ProductID " +
							"and VariationID = @ProductVariationIDOld;";

				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlParameter prmProductVariationIDOld = new MySqlParameter("@ProductVariationIDOld",MySqlDbType.Int32);			
				prmProductVariationIDOld.Value = ProductVariationIDOld;
				cmd.Parameters.Add(prmProductVariationIDOld);

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

		public bool Delete(Int64 ProductID, string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductVariations WHERE ProductID = @ProductID AND VariationID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

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

        public VariationDetails Details(long ProductID, string VariationCode)
        {
            try
            {
                string SQL = "SELECT a.ProductID, " +
                                "a.VariationID, " +
                                "b.VariationCode, " +
                                "b.VariationType " +
                            "FROM tblProductVariations a " +
                            "LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
                            "WHERE ProductID = @ProductID AND VariationCode = @VariationCode;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@VariationCode", VariationCode);

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
                
                
                    

                
                
                

                throw ex;
            }
        }


        #endregion

		#region Streams

		public MySqlDataReader List(Int64 ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT a.ProductID, " +
								"a.VariationID, " +
								"b.VariationCode, " +
								"b.VariationType " +
							"FROM tblProductVariations a " +
							"LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
							"WHERE ProductID = @ProductID " + 
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
        public System.Data.DataTable ListAsDataTable(Int64 ProductID, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            try
            {
                string SQL = "SELECT a.ProductID, " +
                                "a.VariationID, " +
                                "b.VariationCode, " +
                                "b.VariationType " +
                            "FROM tblProductVariations a " +
                            "LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
                            "WHERE ProductID = @ProductID ";

                if (SortField != string.Empty && SortField != null)
                {
                    SQL += "ORDER BY " + SortField + " ";

                    if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                    else SQL += "DESC ";
                }

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblVariations");
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

		public MySqlDataReader Search(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT a.ProductID,a.VariationID,b.VariationCode,b.VariationType " +
							"FROM tblProductVariations a " +
							"LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
							"WHERE ProductID = @ProductID " + 
							"AND VariationType LIKE @SearchKey " + 
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

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

		#region AvailableVariations

		public MySqlDataReader AvailableVariations(Int64 ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT * FROM tblVariations " + 
								"WHERE VariationID NOT IN (SELECT VariationID FROM tblProductVariations WHERE ProductID = @ProductID) " + 
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				
			
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

        public System.Data.DataTable AvailableVariationsDataTable(long ProductID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = "SELECT * FROM tblVariations " +
                                "WHERE VariationID NOT IN (SELECT VariationID FROM tblProductVariations WHERE ProductID = @ProductID) " +
                            "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC;";
                else
                    SQL += " DESC;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                System.Data.DataTable dt = new System.Data.DataTable("tblVariations");
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

		#region isExist

		public bool isExist(ProductVariationDetails Details)
		{
			try 
			{
				string SQL = "SELECT COUNT(*) FROM tblProductVariations WHERE ProductID = @ProductID AND VariationID = @VariationID;";

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				bool boRetValue = false;

				while (myReader.Read())
				{
					boRetValue = true;
				}

				return boRetValue;
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

