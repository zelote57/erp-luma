using System;
using System.Collections;
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
	public struct RemoteBranchInventoryDetails
	{
		public long BranchInventoryID;
		public long ProductID;
		public string ProductCode;
		public long VariationMatrixID;
		public string MatrixDescription;
		public decimal Quantity;
		public int BranchID;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class RemoteBranchInventory : POSConnection
	{
		
		#region Constructors and Destructors

		public RemoteBranchInventory()
            : base(null, null)
        {
        }

        public RemoteBranchInventory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public void Insert(string stSQL)
		{
			try 
			{
                string SQL = stSQL;
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

        public long Insert(RemoteBranchInventoryDetails Details)
        {
            try
            {
                string SQL = "INSERT INTO tblRemoteBranchInventory (ProductID, ProductCode, VariationMatrixID, MatrixDescription, Quantity, BranchID) " +
                    "VALUES (@ProductID, @ProductCode, @VariationMatrixID, @MatrixDescription, @Quantity, @BranchID);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
                prmProductID.Value = Details.ProductID;
                cmd.Parameters.Add(prmProductID);

                MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);
                prmProductCode.Value = Details.ProductCode;
                cmd.Parameters.Add(prmProductCode);

                MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);
                prmVariationMatrixID.Value = Details.VariationMatrixID;
                cmd.Parameters.Add(prmVariationMatrixID);

                MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);
                prmMatrixDescription.Value = Details.MatrixDescription;
                cmd.Parameters.Add(prmMatrixDescription);

                MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);
                prmQuantity.Value = Details.Quantity;
                cmd.Parameters.Add(prmQuantity);

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

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

		public void Update(RemoteBranchInventoryDetails Details)
		{
			try 
			{
				string SQL=	"UPDATE tblRemoteBranchInventory SET " + 
								"ProductID = @ProductID, " +  
								"ProductCode = @ProductCode, " +  
								"VariationMatrixID = @VariationMatrixID, " +  
								"MatrixDescription = @MatrixDescription, " +  
								"Quantity = @Quantity, " +
								"BranchID = @BranchID " +  
							"WHERE BranchInventoryID = @BranchInventoryID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchInventoryID = new MySqlParameter("@BranchInventoryID",MySqlDbType.Int64);
                prmBranchInventoryID.Value = Details.BranchInventoryID;
                cmd.Parameters.Add(prmBranchInventoryID);

                MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.String);
                prmProductID.Value = Details.ProductID;
                cmd.Parameters.Add(prmProductID);

				MySqlParameter prmProductCode = new MySqlParameter("@ProductCode",MySqlDbType.String);			
				prmProductCode.Value = Details.ProductCode;
				cmd.Parameters.Add(prmProductCode);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.String);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmMatrixDescription = new MySqlParameter("@MatrixDescription",MySqlDbType.String);			
				prmMatrixDescription.Value = Details.MatrixDescription;
				cmd.Parameters.Add(prmMatrixDescription);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.String);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.String);			
				prmBranchID.Value = Details.BranchID;
				cmd.Parameters.Add(prmBranchID);

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
				string SQL=	"DELETE FROM tblRemoteBranchInventory WHERE BranchInventoryID IN (" + IDs + ");";
				  
				
	 			
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

        public bool Delete(int BranchID)
        {
            try
            {
                string SQL = "DELETE FROM tblRemoteBranchInventory WHERE BranchID = " +  BranchID + ";";

                

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

		#region Details

        public RemoteBranchInventoryDetails Details(long BranchInventoryID)
		{
			try
			{
				string SQL = "SELECT " +
								"BranchInventoryID, " +
								"ProductID, " +
								"ProductCode, " + 
								"VariationMatrixID, " +
								"MatrixDescription, " +
								"Quantity, " +
								"BranchID " +
							"FROM tblRemoteBranchInventory " +
							"WHERE BranchInventoryID = @BranchInventoryID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmBranchID = new MySqlParameter("@BranchInventoryID",MySqlDbType.Int16);
				prmBranchID.Value = BranchInventoryID;
				cmd.Parameters.Add(prmBranchID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				RemoteBranchInventoryDetails Details = new RemoteBranchInventoryDetails();

				while (myReader.Read()) 
				{
					Details.BranchInventoryID = BranchInventoryID;
                    Details.ProductID = myReader.GetInt64("ProductID");
                    Details.ProductCode = "" + myReader["ProductCode"].ToString();
                    Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
                    Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
                    Details.Quantity = myReader.GetDecimal("Quantity");
                    Details.BranchID = myReader.GetInt16("BranchID");
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
			MySqlDataReader myReader = List(SortField,SortOption.Ascending);
			
			System.Data.DataTable dt = new System.Data.DataTable("tblRemoteBranchInventory");

			dt.Columns.Add("BranchInventoryID");
			dt.Columns.Add("ProductID");
			dt.Columns.Add("ProductCode");
			dt.Columns.Add("VariationMatrixID");
			dt.Columns.Add("MatrixDescription");
			dt.Columns.Add("Quantity");
            dt.Columns.Add("UnitID");
            dt.Columns.Add("UnitCode");
			dt.Columns.Add("BranchID");
            dt.Columns.Add("BranchCode");
				
			while (myReader.Read())
			{
				System.Data.DataRow dr = dt.NewRow();

				dr["BranchInventoryID"] = myReader.GetInt64("BranchInventoryID");
                dr["ProductID"] = myReader.GetInt64("ProductID");
                dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
                dr["VariationMatrixID"] = myReader.GetInt64("VariationMatrixID");
                dr["MatrixDescription"] = "" + myReader["MatrixDescription"].ToString();
                dr["Quantity"] = myReader.GetDecimal("Quantity");
                dr["UnitID"] = myReader.GetInt32("UnitID");
                dr["UnitCode"] = "" + myReader["UnitCode"].ToString();
                dr["BranchID"] = myReader.GetInt16("BranchID");
                dr["BranchCode"] = "" + myReader["BranchCode"].ToString();
					
				dt.Rows.Add(dr);
			}
			
			myReader.Close();

			return dt;
		}
        public System.Data.DataTable DataList(string ProductGroupName, string ProductSubGroupName, string ProductCode)
        {
            string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' ";

            if (ProductGroupName != "" && ProductGroupName != null)
            {
                SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
            }
            if (ProductSubGroupName != "" && ProductSubGroupName != null)
            {
                SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
            }

            if (ProductCode != "" && ProductCode != null)
            {
                string stSQL = "";
                foreach (string stProductCode in ProductCode.Split(';'))
                {
                    stSQL += "OR a.ProductCode LIKE '%" + stProductCode + "%' ";
                }
                SQL += "AND (" + stSQL.Remove(0, 2) + ")";
            }

            
            System.Data.DataTable dt = new System.Data.DataTable("ProductBranchInventory");
            base.MySqlDataAdapterFill(SQL, dt);
            

            return dt;

            //MySqlDataReader myReader = List(ProductGroupName, ProductSubGroupName, ProductCode);

            //System.Data.DataTable dt = new System.Data.DataTable("ProductBranchInventory");

            //dt.Columns.Add("BranchInventoryID");
            //dt.Columns.Add("ProductID");
            //dt.Columns.Add("ProductCode");
            //dt.Columns.Add("VariationMatrixID");
            //dt.Columns.Add("MatrixDescription");
            //dt.Columns.Add("Quantity");
            //dt.Columns.Add("UnitID");
            //dt.Columns.Add("UnitCode");
            //dt.Columns.Add("BranchID");
            //dt.Columns.Add("BranchCode");
            //dt.Columns.Add("ProductGroupCode");
            //dt.Columns.Add("ProductSubGroupCode");

            //while (myReader.Read())
            //{
            //    System.Data.DataRow dr = dt.NewRow();

            //    dr["BranchInventoryID"] = myReader.GetInt64("BranchInventoryID");
            //    dr["ProductID"] = myReader.GetInt64("ProductID");
            //    dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
            //    dr["VariationMatrixID"] = myReader.GetInt64("VariationMatrixID");
            //    dr["MatrixDescription"] = "" + myReader["MatrixDescription"].ToString();
            //    dr["Quantity"] = myReader.GetDecimal("Quantity");
            //    dr["UnitID"] = myReader.GetInt32("UnitID");
            //    dr["UnitCode"] = "" + myReader["UnitCode"].ToString();
            //    dr["BranchID"] = myReader.GetInt16("BranchID");
            //    dr["BranchCode"] = "" + myReader["BranchCode"].ToString();
            //    dr["ProductGroupCode"] = "" + myReader["ProductGroupCode"].ToString();
            //    dr["ProductSubGroupCode"] = "" + myReader["ProductSubGroupCode"].ToString();

            //    dt.Rows.Add(dr);
            //}

            //myReader.Close();

            //return dt;
        }

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                "BranchInventoryID, " +
                                "a.ProductID, " +
                                "a.ProductCode, " +
                                "a.VariationMatrixID, " +
                                "MatrixDescription, " +
                                "a.Quantity, " +
                                "c.BaseUnitID AS UnitID, " +
                                "UnitCode, " +
                                "a.BranchID, " +
                                "BranchCode, " +
                                "ProductGroupCode, " +
                                "ProductSubGroupCode " +
                            "FROM tblRemoteBranchInventory a " +
                                "INNER JOIN tblBranch b ON a.BranchID = b.BranchID " +
                                "INNER JOIN tblProducts c ON a.ProductID = c.ProductID " +
                                "LEFT JOIN tblProductSubGroup d ON c.ProductSubGroupID = d.ProductSubGroupID " +
                                "LEFT JOIN tblProductGroup e ON d.ProductGroupID = e.ProductGroupID " +
                                "LEFT JOIN tblUnit f ON c.BaseUnitID = f.UnitID ";
            return stSQL;
        }
		public MySqlDataReader List(string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "ORDER BY " + SortField;

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
        public MySqlDataReader List(string ProductGroupName, string ProductSubGroupName, string ProductCode)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 AND deleted = '0' ";
				
				if (ProductGroupName != "" && ProductGroupName != null)
				{
					SQL += "AND ProductGroupName = '" + ProductGroupName + "' ";
				}
				if (ProductSubGroupName != "" && ProductSubGroupName != null)
				{
					SQL += "AND ProductSubGroupName = '" + ProductSubGroupName + "' ";
				}

				if (ProductCode != "" && ProductCode != null)
				{
					string stSQL = "";
					foreach (string stProductCode in ProductCode.Split(';'))
					{
						stSQL += "OR a.ProductCode LIKE '%" + stProductCode + "%' ";
					}
					SQL += "AND (" + stSQL.Remove(0,2) + ")";
				}

                

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
                string SQL = SQLSelect() + "WHERE a.ProductCode LIKE @SearchKey ORDER BY " + SortField;

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

        public MySqlConnection GetConnectionToBranch(string ServerIP = "", string DBPort = "", string DBName = "")
        {
            return base.GetConnection(ServerIP, DBPort, DBName);
        }

        public string[] GetInsertList(int BranchID)
        {
            try
            {
                string SQL = "SELECT " +
                                "CONCAT('INSERT INTO tblRemoteBranchInventory ( ProductID, ProductCode, VariationMatrixID, MatrixDescription, Quantity, BranchID', " +
                                "') VALUES ( '" +
                                ", ProductID, ',''', REPLACE(ProductCode,'''',''''''''''''''),''',0,'''',', Quantity, ',@BranchID);') AS INSERTStatement " +
                            "FROM tblProducts WHERE Quantity <> 0;";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int16);
                prmBranchID.Value = BranchID;
                cmd.Parameters.Add(prmBranchID);

                ArrayList items = new ArrayList();

                MySqlDataReader myReader = base.ExecuteReader(cmd);
                while (myReader.Read())
                {
                    string INSERTStatement = "" + myReader["INSERTStatement"].ToString();

                    items.Add(INSERTStatement);
                }

                myReader.Close();

                string[] arrINSERTStatements = new string[0];

                if (items != null)
                {
                    arrINSERTStatements = new string[items.Count];
                    items.CopyTo(arrINSERTStatements);
                }

                return arrINSERTStatements;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}

