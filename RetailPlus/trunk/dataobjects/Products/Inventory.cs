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
	public struct ProductInventoryDetails
	{
		public Int64 InventoryID;
		public Int64 ProductID;
		public string ProductCode;
		public Int64 VariationMatrixID;
		public string VariationMatrixDescription;
		public Int32 ProductUnitID;
		public string UnitName;
		public Int32 Quantity;
		public Int32 MinThreshold;
		public Int32 MaxThreshold;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductInventory : POSConnection
    {
		#region Constructors and Destructors

		public ProductInventory()
            : base(null, null)
        {
        }

        public ProductInventory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(ProductInventoryDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblInventory (" + 
								"ProductID, " +
								"VariationMatrixID, " +
								"ProductUnitID, " +
								"Quantity, " +
								"MinThreshold" +
								")VALUES (" +
								"@ProductID, " +
								"@VariationMatrixID, " +
								"@ProductUnitID, " +
								"@Quantity, " +
								"@MinThreshold, " +
								"@MaxThreshold);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
				
				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int16);	
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);	
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Int32);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Int32);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

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
				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductInventoryDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " + 
							"ProductID = @ProductID, " +  
							"Quantity = @Quantity, " +  
							"ProductUnitID = @ProductUnitID, " +  
							"MinThreshold = @MinThreshold, " + 
							"MaxThreshold = @MaxThreshold " + 
							"WHERE InventoryID = @InventoryID;";
							
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);			
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmMinThreshold = new MySqlParameter("@MinThreshold",MySqlDbType.Int32);			
				prmMinThreshold.Value = Details.MinThreshold;
				cmd.Parameters.Add(prmMinThreshold);

				MySqlParameter prmMaxThreshold = new MySqlParameter("@MaxThreshold",MySqlDbType.Int32);			
				prmMaxThreshold.Value = Details.MaxThreshold;
				cmd.Parameters.Add(prmMaxThreshold);

				MySqlParameter prmInventoryID = new MySqlParameter("@InventoryID",MySqlDbType.Int32);	
				prmInventoryID.Value = Details.InventoryID;
				cmd.Parameters.Add(prmInventoryID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

		public void Update(Int64 ProductID, Int64 VariationMatrixID, Int32 Quantity)
		{
			try 
			{
				string SQL=	"UPDATE tblInventory SET " + 
								"Quantity = Quantity + @Quantity " +  
							"WHERE ProductID = @ProductID " +
							"AND VariationMatrixID = @VariationMatrixID;";
							
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Int32);	
				prmQuantity.Value = Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

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
				string SQL=	"DELETE FROM tblInventory WHERE InventoryID IN (" + IDs + ");";
				  
				
	 			
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

		#region Details

		public ProductInventoryDetails Details(Int64 ProductID)
		{
			try
			{
                System.Data.DataTable dt = ListAsDataTable(ProductID, "InventoryID", SortOption.Ascending);
				
				ProductInventoryDetails Details = new ProductInventoryDetails();

				foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.InventoryID = Int64.Parse(dr["InventoryID"].ToString());
					Details.ProductID = Int64.Parse(dr["ProductID"].ToString());
					Details.ProductCode = "" + dr["ProductCode"].ToString();
					Details.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
					Details.VariationMatrixDescription = "" + dr["VariationMatrixDescription"].ToString();
					Details.ProductUnitID = Int32.Parse(dr["ProductUnitID"].ToString());
					Details.UnitName = "" + dr["UnitName"].ToString();
					Details.Quantity = Int32.Parse(dr["Quantity"].ToString());
					Details.MinThreshold = Int32.Parse(dr["MinThreshold"].ToString());
                    Details.MaxThreshold = Int32.Parse(dr["MaxThreshold"].ToString());
				}

				return Details;
			}

			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Streams

		public MySqlDataReader List(Int64 ProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL =	"SELECT " +
					"InventoryID, " +
					"a.ProductID, " +
					"b.ProductCode, " +
					"a.VariationMatrixID, " +
					"c.Description as VariationMatrixDescription, " +
					"a.ProductUnitID, " +
					"d.UnitName, " +
					"a.Quantity, " +
					"MinThreshold, " +
					"MaxThreshold " +
					"FROM tblInventory a " +
					"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
					"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
					"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
					"WHERE a.ProductID = @ProductID ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);
				
				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public System.Data.DataTable ListAsDataTable(Int64 ProductID, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = "SELECT " +
                                    "InventoryID, " +
                                    "a.ProductID, " +
                                    "b.ProductCode, " +
                                    "a.VariationMatrixID, " +
                                    "c.Description as VariationMatrixDescription, " +
                                    "a.ProductUnitID, " +
                                    "d.UnitName, " +
                                    "a.Quantity, " +
                                    "MinThreshold, " +
                                    "MaxThreshold " +
                            "FROM tblInventory a " +
                            "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                            "LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
                            "INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
                            "WHERE a.ProductID = @ProductID ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC";
                else
                    SQL += " DESC";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID", MySqlDbType.Int32);
                prmProductID.Value = ProductID;
                cmd.Parameters.Add(prmProductID);

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw base.ThrowException(ex);
            }
        }
		
		public MySqlDataReader ListReport(Int64 ProductID, int ProductUnitID, int VariationMatrixID)
		{
			try
			{
				string SQL =	"SELECT " +
					"InventoryID, " +
					"a.ProductID, " +
					"b.ProductCode, " +
					"a.VariationMatrixID, " +
					"c.Description as Description, " +
					"a.ProductUnitID, " +
					"d.UnitName, " +
					"a.Quantity, " +
					"MinThreshold, " +
					"MaxThreshold " +
					"FROM tblInventory a " +
					"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
					"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
					"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
					"WHERE 1=1";

				if (ProductID != 0)
					SQL += " AND a.ProductID = @ProductID"; 
				if (ProductUnitID != 0)
					SQL += " AND a.ProductUnitID = @ProductUnitID";
				if (VariationMatrixID != 0)
					SQL += " AND a.VariationMatrixID = @VariationMatrixID";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);
				prmProductUnitID.Value = ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int32);
				prmVariationMatrixID.Value = VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader Search(Int64 ProductID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"InventoryID, " +
								"a.ProductID, " +
								"b.ProductCode, " +
								"a.VariationMatrixID, " +
								"c.Description as VariationMatrixDescription, " +
								"a.ProductUnitID, " +
								"d.UnitName, " +
								"a.Quantity, " +
								"MinThreshold, " +
								"MaxThreshold " +
								"FROM tblInventory a " +
								"INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
								"LEFT OUTER JOIN  tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
								"INNER JOIN tblUnit d ON a.ProductUnitID = d.ProductUnitID " +
								"WHERE a.ProductID = @ProductID " + 
								"AND (b.ProductCode LIKE @SearchKey " +
								"OR c.Description LIKE @SearchKey " + 
								"OR d.UnitName LIKE @SearchKey) " + 
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int32);			
				prmProductID.Value = ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);
				prmSearchKey.Value = "%" + SearchKey + "%";
				cmd.Parameters.Add(prmSearchKey);
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}		

		#endregion
	}
}

