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
	public struct ProductAttributeDetails
	{
		public int ProductAttributeID;
		public string ProductAttributeCode;
		public string ProductAttributeName;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductAttribute : POSConnection
    {
		#region Constructors and Destructors

		public ProductAttribute()
            : base(null, null)
        {
        }

        public ProductAttribute(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion
		
		#region Insert and Update

		public Int32 Insert(ProductAttributeDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductAttribute (ProductAttributeCode, ProductAttributeName) VALUES (@ProductAttributeCode, @ProductAttributeName);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductAttributeCode = new MySqlParameter("@ProductAttributeCode",MySqlDbType.String);			
				prmProductAttributeCode.Value = Details.ProductAttributeCode;
				cmd.Parameters.Add(prmProductAttributeCode);

				MySqlParameter prmProductAttributeName = new MySqlParameter("@ProductAttributeName",MySqlDbType.String);			
				prmProductAttributeName.Value = Details.ProductAttributeName;
				cmd.Parameters.Add(prmProductAttributeName);

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
				
				
					

				
				
				

				throw ex;
			}	
		}

		public void Update(ProductAttributeDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblProductAttribute SET " + 
							"ProductAttributeCode = @ProductAttributeCode, " +  
							"ProductAttributeName = @ProductAttributeName " +  
							"WHERE ProductAttributeID = @ProductAttributeID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductAttributeCode = new MySqlParameter("@ProductAttributeCode",MySqlDbType.String);			
				prmProductAttributeCode.Value = Details.ProductAttributeCode;
				cmd.Parameters.Add(prmProductAttributeCode);

				MySqlParameter prmProductAttributeName = new MySqlParameter("@ProductAttributeName",MySqlDbType.String);			
				prmProductAttributeName.Value = Details.ProductAttributeName;
				cmd.Parameters.Add(prmProductAttributeName);

				MySqlParameter prmProductAttributeID = new MySqlParameter("@ProductAttributeID",MySqlDbType.Int16);			
				prmProductAttributeID.Value = Details.ProductAttributeID;
				cmd.Parameters.Add(prmProductAttributeID);

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
				string SQL=	"DELETE FROM tblProductAttribute WHERE ProductAttributeID IN (" + IDs + ");";
				  
				
	 			
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

		public ProductAttributeDetails Details(Int32 ProductAttributeID)
		{
			try
			{
				string SQL=	"SELECT " +
								"ProductAttributeID, " +
								"ProductAttributeCode, " +
								"ProductAttributeName " +
							"FROM tblProductAttribute " +
							"WHERE ProductAttributeID = @ProductAttributeID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmProductAttributeID = new MySqlParameter("@ProductAttributeID",MySqlDbType.Int16);
				prmProductAttributeID.Value = ProductAttributeID;
				cmd.Parameters.Add(prmProductAttributeID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductAttributeDetails Details = new ProductAttributeDetails();

				while (myReader.Read()) 
				{
					Details.ProductAttributeID = ProductAttributeID;
					Details.ProductAttributeCode = myReader.GetString(1);
					Details.ProductAttributeName = myReader.GetString(2);
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

		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
					"ProductAttributeID, " +
					"ProductAttributeCode, " +
					"ProductAttributeName " +
					"FROM tblProductAttribute " +
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
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductAttribute");

			dt.Columns.Add("ProductAttributeID");
			dt.Columns.Add("ProductAttributeCode");
			dt.Columns.Add("ProductAttributeName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductAttributeID"] = myReader.GetInt16(0);
				dr["ProductAttributeCode"] = myReader.GetString(1);
				dr["ProductAttributeName"] = myReader.GetString(2);
					
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
								"ProductAttributeID, " +
								"ProductAttributeCode, " +
								"ProductAttributeName " +
							"FROM tblProductAttribute " +
							"WHERE ProductAttributeName LIKE @SearchKey " +
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
			
			System.Data.DataTable dt = new System.Data.DataTable("tblProductAttribute");

			dt.Columns.Add("ProductAttributeID");
			dt.Columns.Add("ProductAttributeCode");
			dt.Columns.Add("ProductAttributeName");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["ProductAttributeID"] = myReader.GetInt16(0);
				dr["ProductAttributeCode"] = myReader.GetString(1);
				dr["ProductAttributeName"] = myReader.GetString(2);
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}

		#endregion
	}
}

