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
	public struct ProductUnitDetails
	{
		public int ProductUnitID;
		public string ProductUnitCode;
		public string ProductUnitName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductUnit : POSConnection
	{
		#region Constructors and Destructors

		public ProductUnit()
            : base(null, null)
        {
        }

        public ProductUnit(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int32 Insert(ProductUnitDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductUnit (ProductUnitCode, ProductUnitName) VALUES (@ProductUnitCode, @ProductUnitName);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);			
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);

				MySqlParameter prmProductUnitName = new MySqlParameter("@ProductUnitName",MySqlDbType.String);			
				prmProductUnitName.Value = Details.ProductUnitName;
				cmd.Parameters.Add(prmProductUnitName);

				base.ExecuteNonQuery(cmd);

				SQL = "SELECT LAST_INSERT_ID();";
				
				cmd.Parameters.Clear(); 
				cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("ProdUnit");
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
				
				
					

				
				
				

				throw ex;
			}	
		}

		public void Update(ProductUnitDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductUnit SET " + 
							"ProductUnitCode = @ProductUnitCode, " +  
							"ProductUnitName = @ProductUnitName " +  
							"WHERE ProductUnitID = @ProductUnitID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductUnitCode = new MySqlParameter("@ProductUnitCode",MySqlDbType.String);			
				prmProductUnitCode.Value = Details.ProductUnitCode;
				cmd.Parameters.Add(prmProductUnitCode);

				MySqlParameter prmProductUnitName = new MySqlParameter("@ProductUnitName",MySqlDbType.String);			
				prmProductUnitName.Value = Details.ProductUnitName;
				cmd.Parameters.Add(prmProductUnitName);

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int16);			
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductUnit WHERE ProductUnitID IN (" + IDs + ");";
				  
				
	 			
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

		public ProductUnitDetails Details(Int32 ProductUnitID)
		{
			try
			{
				string SQL=	"SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
							"WHERE ProductUnitID = @ProductUnitID;";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",System.Data.DbType.Int32);
				prmProductUnitID.Value = ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				ProductUnitDetails Details = new ProductUnitDetails();

                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);
				foreach(System.Data.DataRow dr in dt.Rows)
				{
					Details.ProductUnitID = ProductUnitID;
					Details.ProductUnitCode = "" + dr["ProductUnitCode"].ToString();
                    Details.ProductUnitName = "" + dr["ProductUnitName"].ToString();
				}
				
                return Details;
			}

			catch (Exception ex)
			{
				throw ex;
			}	
		}


		#endregion

		#region Streams

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
							"ORDER BY " + SortField;

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
				
				
					

				
				
				

				throw ex;
			}	
		}

		public System.Data.DataTable DataList(string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductUnit");

			dt.Columns.Add("ProductUnitID");
			dt.Columns.Add("ProductUnitCode");
			dt.Columns.Add("ProductUnitName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductUnitID"] = myReader.GetInt16(0);
				dr["ProductUnitCode"] = myReader.GetString(1);
				dr["ProductUnitName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		public MySqlDataReader Search(string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"ProductUnitID, " +
								"ProductUnitCode, " +
								"ProductUnitName " +
							"FROM tblProductUnit " +
							"WHERE ProductUnitName LIKE @SearchKey " +
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
				
				
					

				
				
				

				throw ex;
			}	
		}
		
		public System.Data.DataTable DataSearch(string SearchKey, string SortField, SortOption SortOrder)
		{
			MySqlDataReader myReader = Search(SearchKey,SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductUnit");

			dt.Columns.Add("ProductUnitID");
			dt.Columns.Add("ProductUnitCode");
			dt.Columns.Add("ProductUnitName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductUnitID"] = myReader.GetInt16(0);
				dr["ProductUnitCode"] = myReader.GetString(1);
				dr["ProductUnitName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		#endregion

		#region Public Modifiers

		public decimal GetBaseUnitValue(Int64 ProductID, Int32 UnitIDToConvert, decimal Quantity)
		{
			try
			{
				Products clsProduct = new Products(base.Connection, base.Transaction);
                Int32 BaseUnitID = clsProduct.Details(ProductID).BaseUnitID;
				
				Int32 origUnitIDToConvert = UnitIDToConvert;

				decimal ConvertedUnit = Quantity;

                while (BaseUnitID != UnitIDToConvert)
                {
                    string SQL = "SELECT " +
                                    "BaseUnitID, " +
                                    "BottomUnitValue, " +
                                    "BaseUnitValue " +
                                "FROM tblProductUnitMatrix " +
                                "WHERE ProductID = @ProductID " +
                                "AND BottomUnitID = @UnitIDToConvert ";

                    MySqlCommand cmd = new MySqlCommand();
                    
                    
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = SQL;

                    MySqlParameter prmProductID = new MySqlParameter("@ProductID", System.Data.DbType.Int64);
                    prmProductID.Value = ProductID;
                    cmd.Parameters.Add(prmProductID);

                    MySqlParameter prmUnitIDToConvert = new MySqlParameter("@UnitIDToConvert", System.Data.DbType.Int32);
                    prmUnitIDToConvert.Value = UnitIDToConvert;
                    cmd.Parameters.Add(prmUnitIDToConvert);

                    System.Data.DataTable dt = new System.Data.DataTable("tblProductUnit");
                    base.MySqlDataAdapterFill(cmd, dt);
                    

                    int iCtr = 0;
                    foreach(System.Data.DataRow dr in dt.Rows)
                    {
                        iCtr++;
                        UnitIDToConvert = Int32.Parse(dr["BaseUnitID"].ToString());
                        ConvertedUnit = Decimal.Parse(dr["BaseUnitValue"].ToString()) * ConvertedUnit / Decimal.Parse(dr["BottomUnitValue"].ToString());
                    }
                    if (iCtr == 0) break;
                }

				return ConvertedUnit;
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw ex;
			}	
		}


		#endregion
	}
}

