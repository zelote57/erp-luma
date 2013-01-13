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
	public struct ProductGroupUnitsMatrixDetails
	{
		public Int64 MatrixID;
		public Int64 GroupID;
		public Int32 BaseUnitID;
		public string BaseUnitName;
		public decimal BaseUnitValue;
		public Int32 BottomUnitID;
		public string BottomUnitName;
		public decimal BottomUnitValue;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductGroupUnitsMatrix : POSConnection
    {
		#region Constructors and Destructors

		public ProductGroupUnitsMatrix()
            : base(null, null)
        {
        }

        public ProductGroupUnitsMatrix(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(ProductGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"INSERT INTO tblProductGroupUnitMatrix (" +
								"GroupID, " +
								"BaseUnitID, " +
								"BaseUnitValue, " +
								"BottomUnitID, " +
								"BottomUnitValue" +
								")VALUES(" +
								"@GroupID, " +
								"@BaseUnitID, " +
								"@BaseUnitValue, " +
								"@BottomUnitID, " +
								"@BottomUnitValue);";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmBaseUnitValue = new MySqlParameter("@BaseUnitValue",MySqlDbType.Decimal);			
				prmBaseUnitValue.Value = Details.BaseUnitValue;
				cmd.Parameters.Add(prmBaseUnitValue);
				
				MySqlParameter prmBottomUnitID = new MySqlParameter("@BottomUnitID",MySqlDbType.Int32);			
				prmBottomUnitID.Value = Details.BottomUnitID;
				cmd.Parameters.Add(prmBottomUnitID);

				MySqlParameter prmBottomUnitValue = new MySqlParameter("@BottomUnitValue",MySqlDbType.Decimal);			
				prmBottomUnitValue.Value = Details.BottomUnitValue;
				cmd.Parameters.Add(prmBottomUnitValue);

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

				throw ex;
			}	
		}
	
		public void Update(ProductGroupUnitsMatrixDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblProductGroupUnitMatrix SET " + 
								"BaseUnitID = @BaseUnitID, " +  
								"BaseUnitValue = @BaseUnitValue, " +
								"BottomUnitID = @BottomUnitID, " +
								"BottomUnitValue = @BottomUnitValue " +  
								"WHERE MatrixID = @MatrixID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",MySqlDbType.String);			
				prmMatrixID.Value = Details.MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlParameter prmBaseUnitID = new MySqlParameter("@BaseUnitID",MySqlDbType.Int32);			
				prmBaseUnitID.Value = Details.BaseUnitID;
				cmd.Parameters.Add(prmBaseUnitID);

				MySqlParameter prmBaseUnitValue = new MySqlParameter("@BaseUnitValue",MySqlDbType.Decimal);			
				prmBaseUnitValue.Value = Details.BaseUnitValue;
				cmd.Parameters.Add(prmBaseUnitValue);
				
				MySqlParameter prmBottomUnitID = new MySqlParameter("@BottomUnitID",MySqlDbType.Int32);			
				prmBottomUnitID.Value = Details.BottomUnitID;
				cmd.Parameters.Add(prmBottomUnitID);

				MySqlParameter prmBottomUnitValue = new MySqlParameter("@BottomUnitValue",MySqlDbType.Decimal);			
				prmBottomUnitValue.Value = Details.BottomUnitValue;
				cmd.Parameters.Add(prmBottomUnitValue);

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

		#region Details

		public ProductGroupUnitsMatrixDetails Details(Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
								"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
								"FROM tblProductGroupUnitMatrix a INNER JOIN " +
								"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
								"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
								"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
								"WHERE a.MatrixID = @MatrixID;"; 
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmMatrixID = new MySqlParameter("@MatrixID",System.Data.DbType.Int64);
				prmMatrixID.Value = MatrixID;
				cmd.Parameters.Add(prmMatrixID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		public ProductGroupUnitsMatrixDetails UnitDetails(Int64 GroupID, Int64 MatrixID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.GroupID " +
					"WHERE a.GroupID = " + GroupID + " " +
					"AND a.MatrixID = " + MatrixID + ";"; 
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

//				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int16);
//				prmGroupID.Value = GroupID;
//				cmd.Parameters.Add(prmGroupID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		public ProductGroupUnitsMatrixDetails LastDetails(Int64 GroupID)
		{
			try
			{
				string SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
					"WHERE a.GroupID = @GroupID " +
					"ORDER BY a.MatrixID DESC LIMIT 1;"; 
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductGroupUnitsMatrixDetails Details = new ProductGroupUnitsMatrixDetails();

				while (myReader.Read()) 
				{
					Details.MatrixID = myReader.GetInt64("MatrixID");
					Details.GroupID = myReader.GetInt64("GroupID");
					Details.BaseUnitID = myReader.GetInt32("BaseUnitID");
					Details.BaseUnitName = "" + myReader["BaseUnitName"].ToString();
					Details.BaseUnitValue = myReader.GetDecimal("BaseUnitValue");
					Details.BottomUnitID = myReader.GetInt32("BottomUnitID");
					Details.BottomUnitName = "" + myReader["BottomUnitName"].ToString();
					Details.BottomUnitValue = myReader.GetDecimal("BottomUnitValue");
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

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductGroupUnitMatrix WHERE MatrixID IN (" + IDs + ");";
								
				
	 			
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

		#region Streams

		public MySqlDataReader List(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string	SQL =	"SELECT a.MatrixID, a.GroupID, a.BaseUnitID, b.UnitName 'BaseUnitName', " +
					"a.BaseUnitValue, a.BottomUnitID, c.UnitName 'BottomUnitName', a.BottomUnitValue " +
					"FROM tblProductGroupUnitMatrix a INNER JOIN " +
					"tblUnit b ON a.BaseUnitID = b.UnitID INNER JOIN " + 
					"tblUnit c ON a.BottomUnitID = c.UnitID LEFT OUTER JOIN " + 	
					"tblProductGroup d ON a.GroupID = d.ProductGroupID " +
					"WHERE a.GroupID = " + GroupID  + " ORDER BY " + SortField;

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
		public MySqlDataReader AvailableUnitsForProduct(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			
			try
			{
				string SQL = "SELECT * FROM tblUnit " +
					"WHERE UnitID NOT IN (SELECT BaseUnitID FROM tblProductGroupUnitMatrix WHERE GroupID = @GroupID) " +
					"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int64);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				
				
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
				string SQL ="SELECT * FROM tblProductGroupUnitMatrix " +
							"WHERE GroupID LIKE @SearchKey " +
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

