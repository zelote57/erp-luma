using System;
using System.Security.Permissions;
using MySql.Data.MySqlClient;
using System.Collections;

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
	public struct StockItemDetails
	{
		public Int64 StockItemID;
		public Int64 StockID;
		public Int64 ProductID;
		public Int64 VariationMatrixID;
		public Int32 ProductUnitID;
		public Int16 StockTypeID;
		public DateTime StockDate;
		public decimal Quantity;
		public string Remarks;
        public decimal PurchasePrice;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class StockItem : POSConnection
	{
		
		#region Constructors and Destructors

		public StockItem()
            : base(null, null)
        {
        }

        public StockItem(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(StockItemDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblStockItems (" +
								"StockID, " + 
								"ProductID, " + 
								"VariationMatrixID, " + 
								"ProductUnitID, " +  
								"StockTypeID, " + 
								"StockDate, " + 
								"Quantity, " +
								"Remarks, " +
                                "PurchasePrice" +
								") VALUES (" +
								"@StockID, " + 
								"@ProductID, " + 
								"@VariationMatrixID, " + 
								"@ProductUnitID, " +  
								"@StockTypeID, " + 
								"@StockDate, " + 
								"@Quantity, " +
								"@Remarks, " +
                                "@PurchasePrice);"; 

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = Details.StockID;
				cmd.Parameters.Add(prmStockID);

				MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);	
				prmProductID.Value = Details.ProductID;
				cmd.Parameters.Add(prmProductID);
				
				MySqlParameter prmVariationMatrixID = new MySqlParameter("@VariationMatrixID",MySqlDbType.Int64);	
				prmVariationMatrixID.Value = Details.VariationMatrixID;
				cmd.Parameters.Add(prmVariationMatrixID);
				
				MySqlParameter prmProductUnitID = new MySqlParameter("@ProductUnitID",MySqlDbType.Int32);	
				prmProductUnitID.Value = Details.ProductUnitID;
				cmd.Parameters.Add(prmProductUnitID);

				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
	
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmQuantity = new MySqlParameter("@Quantity",MySqlDbType.Decimal);
				prmQuantity.Value = Details.Quantity;
				cmd.Parameters.Add(prmQuantity);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

                MySqlParameter prmPurchasePrice  = new MySqlParameter("@PurchasePrice",MySqlDbType.Decimal);
                prmPurchasePrice.Value = Details.PurchasePrice;
                cmd.Parameters.Add(prmPurchasePrice);

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

				throw base.ThrowException(ex);
			}	
		}


		#endregion

		#region Delete

		public bool Delete(string IDs)
		{
			try 
			{
				string SQL=	"DELETE FROM tblStockItems WHERE StockItemID IN (" + IDs + ");";
				  
				
	 			
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

		public bool Delete(Int64 StockID)
		{
			try 
			{
				string SQL=	"DELETE FROM tblStockItems WHERE StockID = " + StockID + ";";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

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

		public StockItemDetails[] Details(Int64 StockID)
		{
			try
			{
				System.Data.DataTable dt = ListAsDataTable(StockID, "StockItemID", SortOption.Ascending);
				
				ArrayList items = new ArrayList();

				foreach(System.Data.DataRow dr in dt.Rows)
				{
					StockItemDetails itemDetails = new StockItemDetails();

                    itemDetails.StockItemID = Int64.Parse(dr["StockItemID"].ToString());
					itemDetails.StockID = StockID;
					itemDetails.ProductID = Int64.Parse(dr["ProductID"].ToString());
					itemDetails.VariationMatrixID = Int64.Parse(dr["VariationMatrixID"].ToString());
					itemDetails.ProductUnitID = Int32.Parse(dr["ProductUnitID"].ToString());
					itemDetails.StockTypeID = Int16.Parse(dr["StockTypeID"].ToString());
					itemDetails.StockDate = DateTime.Parse(dr["StockDate"].ToString());
					itemDetails.Quantity = decimal.Parse(dr["Quantity"].ToString());
					itemDetails.Remarks = "" + dr["Remarks"].ToString();
                    itemDetails.PurchasePrice = decimal.Parse(dr["PurchasePrice"].ToString());
					items.Add(itemDetails);
				}

				StockItemDetails[] StockItems = new StockItemDetails[0];

				if (items != null)
				{
					StockItems = new StockItemDetails[items.Count];
					items.CopyTo(StockItems);
				}
				
				return StockItems;
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
			string stSQL = "SELECT " +
									"StockItemID, " +
									"StockID, " +
									"a.ProductID, " +
									"ProductCode, " +
									"VariationMatrixID, " + 
									"c.Description AS BaseVariationDescription, " +
									"ProductUnitID, " +
									"d.UnitCode, " +
									"d.UnitName, " +
									"a.StockTypeID, " +
									"e.Description AS StockTypeDescription, " +
									"StockDate, " +
									"a.Quantity, " +
									"a.Remarks, " +
                                    "a.PurchasePrice " +
								"FROM (((tblStockItems a " +
								    "LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID) " +
								    "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID) " +  
								    "LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID) " +
								    "LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID " +
								"WHERE 1=1 ";
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

				throw base.ThrowException(ex);
			}	
		}
		
		public MySqlDataReader List(Int64 StockID, string SortField, SortOption SortOrder)
		{
			try
			{
				string SQL = SQLSelect() + "AND StockID = @StockID " +
								"ORDER BY " + SortField; 

				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

				
				
				return base.ExecuteReader(cmd);			
			}
			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw base.ThrowException(ex);
			}	
		}
		
        public System.Data.DataTable ListAsDataTable(Int64 StockID, string SortField = "StockItemID", SortOption SortOrder = SortOption.Desscending)
		{
			try
			{
				string SQL = SQLSelect() + "AND StockID = @StockID " +
								"ORDER BY " + SortField; 
				
				if (SortOrder == SortOption.Ascending)
					SQL += " ASC";
				else
					SQL += " DESC";

				MySqlCommand cmd = new MySqlCommand();
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

                System.Data.DataTable dt = new System.Data.DataTable("StockItems");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				return dt;			
			}
			catch (Exception ex)
			{
				throw base.ThrowException(ex);
			}	
		}
        //public MySqlDataReader ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
        //{
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand();

        //        string SQL = "SELECT " +
        //                        "StockItemID, " +
        //                        "IFNULL(c.Description, b.ProductCode) AS MatrixDescription, " +
        //                        "CASE StockDirection " +
        //                        "	WHEN 0 THEN a.Quantity " +
        //                        "	WHEN 1 THEN -a.Quantity " +
        //                        "END AS Quantity, " +
        //                        "d.UnitCode, " +
        //                        "CONCAT(e.Description, ':' , a.Remarks) AS Remarks, " +
        //                        "a.StockDate AS TransactionDate, " +
        //                        "TransactionNo " +
        //                    "FROM (((tblStockItems a " +
        //                    "INNER JOIN tblStock f ON a.StockID = f.StockID " +
        //                    "LEFT OUTER JOIN tblProducts b ON a.ProductID = b.ProductID) " +
        //                    "LEFT OUTER JOIN tblProductBaseVariationsMatrix c ON a.VariationMatrixID = c.MatrixID) " +
        //                    "LEFT OUTER JOIN tblUnit d ON a.ProductUnitID = d.UnitID) " +
        //                    "LEFT OUTER JOIN tblStockType e ON a.StockTypeID = e.StockTypeID " +
        //                    "WHERE 1=1 ";

        //        SQL += "AND a.StockDate >= @StartDate ";

        //        MySqlParameter prmStartDate = new MySqlParameter("@StartDate",MySqlDbType.DateTime);
        //        prmStartDate.Value = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmStartDate); 

        //        SQL += "AND a.StockDate <= @EndDate ";

        //        MySqlParameter prmEndDate = new MySqlParameter("@EndDate",MySqlDbType.DateTime);
        //        prmEndDate.Value = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
        //        cmd.Parameters.Add(prmEndDate); 

        //        if (ProductID != 0)
        //        {	
        //            SQL += "AND a.ProductID = @ProductID ";

        //            MySqlParameter prmProductID = new MySqlParameter("@ProductID",MySqlDbType.Int64);
        //            prmProductID.Value = ProductID;
        //            cmd.Parameters.Add(prmProductID); 
        //        }

        //        SQL += "ORDER BY a.StockDate DESC";

        //        

        //        
        //        
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.CommandText = SQL;
				
        //        
				
        //        return base.ExecuteReader(cmd);			
        //    }
        //    catch (Exception ex)
        //    {
        //        
        //        
        //        {
        //            
        //            
        //            
        //            
        //        }

        //        throw base.ThrowException(ex);
        //    }	
        //}
        public MySqlDataReader ProductHistoryReport(long ProductID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                string SQL = "CALL procGenerateProductHistory(@SessionID, @StartTransactionDate, @EndTransactionDate, @ProductID);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                Random clsRandom = new Random();
                MySqlParameter prmSessionID = new MySqlParameter("@SessionID", clsRandom.Next(1234567, 99999999));

                cmd.Parameters.Add(prmSessionID);
                cmd.Parameters.AddWithValue("@StartTransactionDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@EndTransactionDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@ProductID", ProductID);

                base.ExecuteNonQuery(cmd);

                SQL = "SELECT " +
                            "HistoryID, " +
                            "MatrixDescription, " +
                            "Quantity, " +
                            "UnitCode, " +
                            "Remarks, " +
                            "TransactionDate, " +
                            "TransactionNo " +
                    "FROM tblProductHistory " +
                    "WHERE SessionID = @SessionID " +
                    "ORDER BY TransactionDate;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                

                SQL = "DELETE FROM tblProductHistory WHERE SessionID = @SessionID;";

                cmd.CommandText = SQL;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(prmSessionID);
                base.ExecuteNonQuery(cmd);

                return base.ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                }
                throw base.ThrowException(ex);
            }
        }

        public System.Data.DataTable ProductMovementReport(long ProductID, long MatrixID, DateTime StartDate, DateTime EndDate, int intBranchID = 0)
        {
            try
            {
                // note the parameter name must be the same as paramter in the proc.
                // this is more efficient then the call.
                // need to adjust all proc like this

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                string SQL = "procProductMovementSelect";

                cmd.Parameters.AddWithValue("@intProductID", ProductID);
                cmd.Parameters.AddWithValue("@intMatrixID", MatrixID);
                cmd.Parameters.AddWithValue("@dteStartTransactionDate", StartDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@dteEndTransactionDate", EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@intBranchID", intBranchID);
                
                cmd.CommandText = SQL;
                string strDataTableName = "tbl" + this.GetType().FullName.Split(new Char[] { '.' })[this.GetType().FullName.Split(new Char[] { '.' }).Length - 1]; System.Data.DataTable dt = new System.Data.DataTable(strDataTableName);
                base.MySqlDataAdapterFill(cmd, dt);

                return dt;
            }
            catch (Exception ex)
            {              
                throw base.ThrowException(ex);
            }
        }
		
		#endregion
	}
}

