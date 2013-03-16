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
	public struct ProductGroupVariationDetails
	{
		public int GroupID;
		public int VariationID;
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
	public class ProductGroupVariation : POSConnection
    {
		#region Constructors and Destructors

		public ProductGroupVariation()
            : base(null, null)
        {
        }

        public ProductGroupVariation(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int32 Insert(ProductGroupVariationDetails Details)
		{
			try 
			{
				string SQL = "INSERT INTO tblProductGroupVariations (GroupID, VariationID) VALUES (@GroupID, @VariationID);";

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int32);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

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

				throw base.ThrowException(ex);
			}	
		}

		public void Update(ProductGroupVariationDetails Details, Int32 VarIDToUpdate)
		{
			try 
			{
				string SQL = "UPDATE tblProductGroupVariations SET " + 
					"VariationID = @VariationID " +
					"WHERE GroupID = @GroupID " +
					"AND VariationID = @VarIDToUpdate;";
				 
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int32);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				MySqlParameter prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				MySqlParameter prmVarIDToUpdate = new MySqlParameter("@VarIDToUpdate",MySqlDbType.Int32);			
				prmVarIDToUpdate.Value = VarIDToUpdate;
				cmd.Parameters.Add(prmVarIDToUpdate);

				base.ExecuteNonQuery(cmd);

				SQL = "UPDATE tblProductGroupVariationsMatrix a, tblProductGroupBaseVariationsMatrix b SET " + 
					"VariationID = @VariationID " +
					"WHERE a.MatrixID = b.MatrixID " +
					"AND GroupID = @GroupID " +
					"AND VariationID = @VarIDToUpdate;";
				  
				cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int32);			
				prmGroupID.Value = Details.GroupID;
				cmd.Parameters.Add(prmGroupID);

				prmVariationID = new MySqlParameter("@VariationID",MySqlDbType.Int32);			
				prmVariationID.Value = Details.VariationID;
				cmd.Parameters.Add(prmVariationID);

				prmVarIDToUpdate = new MySqlParameter("@VarIDToUpdate",MySqlDbType.Int32);			
				prmVarIDToUpdate.Value = VarIDToUpdate;
				cmd.Parameters.Add(prmVarIDToUpdate);

				base.ExecuteNonQuery(cmd);
				
			}

			catch (Exception ex)
			{
				
				
					

				
				
				

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Delete

		public bool Delete(int GroupID, string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblProductGroupVariations WHERE GroupID = @GroupID AND VariationID IN (" + IDs + ");";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int32);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

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

		#region Streams

		public MySqlDataReader List(Int64 GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT " +
								"a.GroupID, " +
								"a.VariationID, " +
								"b.VariationType " +
								"FROM tblProductGroupVariations a " +
								"LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
								"WHERE GroupID = @GroupID " + 
								"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

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

				throw base.ThrowException(ex);
			}	
		}

		public MySqlDataReader Search(Int64 ProductGroupID, string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL ="SELECT " +
								"a.GroupID, " +
								"a.VariationID, " +
								"b.VariationType " +
							"FROM tblProductGroupVariations a " +
							"LEFT JOIN tblVariations b ON a.VariationID = b.VariationID " +
							"WHERE GroupID = @ProductGroupID " + 
							"AND VariationType LIKE @SearchKey " + 
							"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmProductGroupID = new MySqlParameter("@ProductGroupID",MySqlDbType.Int64);			
				prmProductGroupID.Value = ProductGroupID;
				cmd.Parameters.Add(prmProductGroupID);

				MySqlParameter prmSearchKey = new MySqlParameter("@SearchKey",MySqlDbType.String);			
				prmSearchKey.Value = "%" + SearchKey +"%";
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

		public MySqlDataReader AvailableVariations(int GroupID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = "SELECT * FROM tblVariations " + 
					"WHERE VariationID NOT IN (SELECT VariationID FROM tblProductGroupVariations WHERE GroupID = @GroupID) " + 
					"ORDER BY " + SortField;

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC;";
				else
					SQL += " DESC;";

				

				MySqlCommand cmd = new MySqlCommand();
				
					
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmGroupID = new MySqlParameter("@GroupID",MySqlDbType.Int32);			
				prmGroupID.Value = GroupID;
				cmd.Parameters.Add(prmGroupID);

				
			
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

