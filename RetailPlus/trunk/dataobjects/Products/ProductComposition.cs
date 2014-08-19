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
	#region Struct

	public struct ProductCompositionDetails
	{
		public Int64 CompositionID;
        public Int64 MainProductID;
		public Int64 ProductID;
		public string ProductCode;
		public string ProductDesc;
		public Int64 VariationMatrixID;
		public string MatrixDescription;
		public decimal Quantity;
		public Int32 UnitID;
		public string UnitCode;
		public string UnitName;

        public DateTime CreatedOn;
        public DateTime LastModified;
	}

	
	#endregion

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class ProductComposition : POSConnection
    {
		#region Constructors and Destructors

		public ProductComposition()
            : base(null, null)
        {
        }

        public ProductComposition(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(ProductCompositionDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblProductComposition (" +
					"MainProductID, " +
					"ProductID, " + 
					"VariationMatrixID, " + 
					"UnitID, " +  
					"Quantity " +
					") VALUES (" +
					"@MainProductID, " +
					"@ProductID, " + 
					"@VariationMatrixID, " + 
					"@UnitID, " +  
					"@Quantity);"; 

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMainProductID = new MySqlParameter("@MainProductID",MySqlDbType.Int64);			
				prmMainProductID.Value = Details.MainProductID;
				cmd.Parameters.Add(prmMainProductID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.String);			
				prmUnitID.Value = Details.UnitID;
				cmd.Parameters.Add(prmUnitID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

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

		public void Update(ProductCompositionDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblProductComposition SET " +
					"MainProductID		=	@MainProductID, " +
					"ProductID			=	@ProductID, " + 
					"VariationMatrixID	=	@VariationMatrixID, " + 
					"UnitID				=	@UnitID, " +  
					"Quantity			=	@Quantity " +
					"WHERE CompositionID	=	@CompositionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMainProductID = new MySqlParameter("@MainProductID",MySqlDbType.Int64);			
				prmMainProductID.Value = Details.MainProductID;
				cmd.Parameters.Add(prmMainProductID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);			
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);

				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);			
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);

				MySqlParameter prmUnitID = new MySqlParameter("@UnitID",MySqlDbType.String);			
				prmUnitID.Value = Details.UnitID;
				cmd.Parameters.Add(prmUnitID);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);			
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmCompositionID = new MySqlParameter("@CompositionID",MySqlDbType.Int64);			
				prmCompositionID.Value = Details.CompositionID;
				cmd.Parameters.Add(prmCompositionID);

				base.ExecuteNonQuery(cmd);

			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}

        public Int32 Save(ProductCompositionDetails Details)
        {
            try
            {
                string SQL = "CALL procSaveProductComposition(@CompositionID, @MainProductID, @ProductID, @VariationMatrixID," +
                                                        "@UnitID, @Quantity, @CreatedOn, @LastModified);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("CompositionID", Details.CompositionID);
                cmd.Parameters.AddWithValue("MainProductID", Details.MainProductID);
                cmd.Parameters.AddWithValue("ProductID", Details.ProductID);
                cmd.Parameters.AddWithValue("VariationMatrixID", Details.VariationMatrixID);
                cmd.Parameters.AddWithValue("UnitID", Details.UnitID);
                cmd.Parameters.AddWithValue("Quantity", Details.Quantity);
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

		public bool Delete(string IDs)
		{
			try 
			{
				
				MySqlCommand cmd = new MySqlCommand();
				string SQL;

				SQL=	"DELETE FROM tblProductComposition WHERE CompositionID IN (" + IDs + ");";
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

		public ProductCompositionDetails Details(Int64 CompositionID)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE a.CompositionID = @CompositionID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmCompositionID = new MySqlParameter("@CompositionID",MySqlDbType.Int64);			
				prmCompositionID.Value = CompositionID;
				cmd.Parameters.Add(prmCompositionID);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				ProductCompositionDetails Details = new ProductCompositionDetails();

				while (myReader.Read()) 
				{
					Details.CompositionID = myReader.GetInt64("CompositionID");
					Details.MainProductID = myReader.GetInt64("MainProductID");
					Details.ProductID = myReader.GetInt64("ProductID");
					Details.ProductCode = "" + myReader["ProductCode"].ToString();
					Details.ProductDesc = "" + myReader["ProductDesc"].ToString();
					Details.VariationMatrixID = myReader.GetInt64("VariationMatrixID");
					Details.MatrixDescription = "" + myReader["MatrixDescription"].ToString();
					Details.Quantity = myReader.GetDecimal("Quantity");
					Details.UnitID = myReader.GetInt32("UnitID");
					Details.UnitCode = "" + myReader["UnitCode"].ToString();
					Details.UnitName = "" + myReader["UnitName"].ToString();
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

		private string SQLSelect()
		{
            string SQL = "SELECT " +
                                "CompositionID, " +
                                "MainProductID, " +
                                "a.ProductID, " +
                                "ProductCode, " +
                                "ProductDesc, " +
                                "a.VariationMatrixID, " +
                                "c.Description as MatrixDescription, " +
                                "a.Quantity, " +
                                "a.UnitID, " +
                                "d.UnitCode, " +
                                "d.UnitName, " +
                                "f.OrderSlipPrinter " +
                            "FROM tblProductComposition a " +
                            "INNER JOIN tblProducts b ON a.ProductID = b.ProductID " +
                            "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID " +
                            "INNER JOIN tblUnit d ON a.UnitID = d.UnitID " +
                            "INNER JOIN tblProductSubGroup e ON b.ProductSubGroupID = e.ProductSubGroupID " +
                            "INNER JOIN tblProductGroup f ON e.ProductGroupID = f.ProductGroupID ";
			return SQL;
		}

		public System.Data.DataTable dtList(Int64 MainProductID, string SortField, SortOption SortOrder)
		{
			try
			{
                if (SortField == string.Empty) SortField = "CompositionID";

				string SQL = SQLSelect() + "WHERE MainProductID = @MainProductID ORDER BY " + SortField; 
				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMainProductID = new MySqlParameter("@MainProductID",MySqlDbType.Int64);						
				prmMainProductID.Value = MainProductID;
				cmd.Parameters.Add(prmMainProductID);

				
				
				System.Data.DataTable dt = new System.Data.DataTable("tblCompositionList");

				dt.Columns.Add("CompositionID");
				dt.Columns.Add("MainProductID");
				dt.Columns.Add("ProductID");
				dt.Columns.Add("ProductCode");
				dt.Columns.Add("ProductDesc");
				dt.Columns.Add("VariationMatrixID");
				dt.Columns.Add("MatrixDescription");
				dt.Columns.Add("Quantity");
				dt.Columns.Add("UnitID");
				dt.Columns.Add("UnitCode");
				dt.Columns.Add("UnitName");
                dt.Columns.Add("OrderSlipPrinter");

                MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				while (myReader.Read())
				{
					System.Data.DataRow dr = dt.NewRow();

					dr["CompositionID"] = "" + myReader["CompositionID"].ToString();
					dr["MainProductID"] = "" + myReader["MainProductID"].ToString();
					dr["ProductID"] = "" + myReader["ProductID"].ToString();
					dr["ProductCode"] = "" + myReader["ProductCode"].ToString();
					dr["ProductDesc"] = "" + myReader["ProductDesc"].ToString();
					dr["VariationMatrixID"] = "" + myReader["VariationMatrixID"].ToString();
					dr["MatrixDescription"] = "" + myReader["MatrixDescription"].ToString();
					dr["Quantity"] = myReader.GetDecimal("Quantity").ToString("#,##0.#0");
					dr["UnitID"] = "" + myReader["UnitID"].ToString();
					dr["UnitCode"] = "" + myReader["UnitCode"].ToString();
					dr["UnitName"] = "" + myReader["UnitName"].ToString();
                    dr["OrderSlipPrinter"] = "" + myReader["OrderSlipPrinter"].ToString();
					dt.Rows.Add(dr);
				}

				myReader.Close();

				return dt;				
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(Int64 MainProductID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE MainProductID = @MainProductID ORDER BY " + SortField; 
				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMainProductID = new MySqlParameter("@MainProductID",MySqlDbType.Int64);						
				prmMainProductID.Value = MainProductID;
				cmd.Parameters.Add(prmMainProductID);

				
				
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
				string SQL = SQLSelect() + "WHERE 1=1 " +
					"AND (ProductCode LIKE @SearchKey " +
					"OR ProductDesc LIKE @SearchKey " +
					"OR c.Description) " +
					"ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				SQL += "LIMIT 100;";

				

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

		public MySqlDataReader Search(Int64 MainProductID,string SearchKey, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "WHERE MainProductID = @MainProductID " +
					"AND (ProductCode LIKE @SearchKey " +
					"OR ProductDesc LIKE @SearchKey " +
					"OR c.Description) " +
					"ORDER BY " + SortField; 

				if (SortOrder == SortOption.Ascending)
					SQL += " ASC ";
				else
					SQL += " DESC ";

				SQL += "LIMIT 100;";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmMainProductID = new MySqlParameter("@MainProductID",MySqlDbType.Int64);						
				prmMainProductID.Value = MainProductID;
				cmd.Parameters.Add(prmMainProductID);

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

        // returns True if a composition exist
        // returns False if a composition does not exist
        public bool GeneratePLUReport(string TerminalNo, long MainProductID, string MainProductCode, decimal Quantity)
		{
			try
			{
                
                bool boRetValue = false;

				System.Data.DataTable dtCompositionList = dtList(MainProductID, "CompositionID", SortOption.Ascending);

				Data.PLUReport clsPLUReport = new Data.PLUReport(base.Connection, base.Transaction);
				PLUReportDetails clsPLUReportDetails;

				foreach(System.Data.DataRow dr in dtCompositionList.Rows)
				{
					long lProductID = Convert.ToInt64(dr["ProductID"]);
                    string stProductCode = dr["ProductCode"].ToString();
					decimal decQuantity = Convert.ToDecimal(Convert.ToDecimal(dr["Quantity"]) * Quantity);
                    OrderSlipPrinter locOrderSlipPrinter = (OrderSlipPrinter)Enum.Parse(typeof(OrderSlipPrinter), dr["OrderSlipPrinter"].ToString());

                    clsPLUReportDetails = new PLUReportDetails();
                    clsPLUReportDetails.TerminalNo = TerminalNo;
                    clsPLUReportDetails.ProductID = lProductID;
                    clsPLUReportDetails.ProductCode = MainProductCode + "-" + stProductCode;
                    clsPLUReportDetails.Quantity = decQuantity;
                    clsPLUReportDetails.Amount = 0;
                    clsPLUReportDetails.OrderSlipPrinter = locOrderSlipPrinter;

                    clsPLUReport.Insert(clsPLUReportDetails);

                    GeneratePLUReport(TerminalNo, lProductID, stProductCode, decQuantity);

                    boRetValue = true;
				}

                return boRetValue;
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
				}
				throw base.ThrowException(ex);
			}	
		}
	}
}

