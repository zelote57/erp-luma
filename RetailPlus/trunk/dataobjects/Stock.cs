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
	public struct StockDetails
	{
		public long StockID;
        public int BranchID;
		public string TransactionNo;
		public short StockTypeID;
		public string StockTypeCode;
		public string StockTypeDescription;
		public StockDirections StockDirection;
		public DateTime StockDate;
		public long SupplierID;
		public string SupplierCode;
		public string SupplierName;
		public string Remarks;
		public StockItemDetails[] StockItems;
	}

	[StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
		 PublicKey = "002400000480000094000000060200000024000" +
		 "052534131000400000100010053D785642F9F960B43157E0380" +
		 "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
		 "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
		 "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
		 "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
		 "FF52834EAFB5A7A1FDFD5851A3")]
	public class Stock : POSConnection
	{
		
		#region Constructors and Destructors

		public Stock()
            : base(null, null)
        {
        }

        public Stock(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

		#region Insert and Update

		public Int64 Insert(StockDetails Details)
		{
			try  
			{
				string SQL =	"INSERT INTO tblStock (" +
                                    "BranchID, " +
									"TransactionNo, " +
									"StockTypeID, " +
									"StockDate, " + 
									"SupplierID, " + 
									"Remarks " +
								") VALUES (" +
                                    "@BranchID, " +
									"@TransactionNo, " +
									"@StockTypeID, " +
									"@StockDate, " + 
									"@SupplierID, " + 
									"@Remarks);"; 

				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

                MySqlParameter prmBranchID = new MySqlParameter("@BranchID",MySqlDbType.Int32);
                prmBranchID.Value = Details.BranchID;
                cmd.Parameters.Add(prmBranchID);

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);	
				prmTransactionNo.Value = Details.TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);
				
				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);	
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
				
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);	
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);	
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);
	
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
		public void Update(StockDetails Details)
		{
			try 
			{
				string SQL =	"UPDATE tblStock SET " +
									"StockTypeID		= @StockTypeID, " + 
									"StockDate			= @StockDate, " +  
									"SupplierID			= @SupplierID, " +  
									"Remarks			= @Remarks " +
								"WHERE StockID = @StockID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);	
				prmTransactionNo.Value = Details.TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);
				
				MySqlParameter prmStockTypeID = new MySqlParameter("@StockTypeID",MySqlDbType.Int16);	
				prmStockTypeID.Value = Details.StockTypeID;
				cmd.Parameters.Add(prmStockTypeID);
				
				MySqlParameter prmStockDate = new MySqlParameter("@StockDate",MySqlDbType.DateTime);	
				prmStockDate.Value = Details.StockDate.ToString("yyyy-MM-dd HH:mm:ss");
				cmd.Parameters.Add(prmStockDate);

				MySqlParameter prmSupplierID = new MySqlParameter("@SupplierID",MySqlDbType.Int64);	
				prmSupplierID.Value = Details.SupplierID;
				cmd.Parameters.Add(prmSupplierID);

				MySqlParameter prmRemarks = new MySqlParameter("@Remarks",MySqlDbType.String);
				prmRemarks.Value = Details.Remarks;
				cmd.Parameters.Add(prmRemarks);

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);	
				prmStockID.Value = Details.StockID;
				cmd.Parameters.Add(prmStockID);

				base.ExecuteNonQuery(cmd);
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}
        public void TagActive(long StockID)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                TagActiveInActive(StockID, true);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }
        public void TagInactive(long StockID)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                TagActiveInActive(StockID, false);
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw ex;
            }
        }
        private void TagActiveInActive(long StockID, bool bolActive)
        {
            // Added March 10, 2010 to monitor if transaction if Active or Inactive
            try
            {
                string SQL = "CALL procStockTagActiveInactive(@StockID, @TransactionListFilterType);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@StockID", StockID);
                cmd.Parameters.AddWithValue("@TransactionListFilterType", Convert.ToInt16(bolActive));

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
				

				string SQL=	"DELETE FROM tblStockItems WHERE StockID IN (" + IDs + ");";

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				base.ExecuteNonQuery(cmd);

				SQL=	"DELETE FROM tblStock WHERE StockID IN (" + IDs + ");";
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

        private string SQLSelect()
        {
            string stSQL = "SELECT " +
                                    "StockID, " +
                                    "BranchID, " +
                                    "TransactionNo, " +
                                    "a.StockTypeID, " +
                                    "StockTypeCode, " +
                                    "Description AS StockTypeDescription, " +
                                    "StockDirection, " +
                                    "StockDate, " +
                                    "SupplierID, " +
                                    "ContactCode AS SupplierCode, " +
                                    "ContactName AS SupplierName, " +
                                    "a.Remarks, " +
                                    "a.Active " +
                                "FROM tblStock a " +
                                "INNER JOIN tblStockType b ON a.StockTypeID = b.StockTypeID " +
                                "INNER JOIN tblContacts c ON a.SupplierID = c.ContactID ";
            return stSQL;
        }

		#region Details

		public StockDetails Details(Int64 StockID)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE StockID = @StockID;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmStockID = new MySqlParameter("@StockID",MySqlDbType.Int64);
				prmStockID.Value = StockID;
				cmd.Parameters.Add(prmStockID);

                System.Data.DataTable dt = new System.Data.DataTable("Stock");
                base.MySqlDataAdapterFill(cmd, dt);
                
				
				StockDetails Details = new StockDetails();

                foreach (System.Data.DataRow dr in dt.Rows)
				{
					Details.StockID = StockID;
                    Details.BranchID = Int32.Parse(dr["BranchID"].ToString());
					Details.TransactionNo = "" + dr["TransactionNo"].ToString();
					Details.StockTypeID = Int16.Parse(dr["StockTypeID"].ToString());
					Details.StockTypeCode = "" + dr["StockTypeCode"].ToString();
					Details.StockTypeDescription = "" + dr["StockTypeDescription"].ToString();
                    Details.StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), Convert.ToInt16(dr["StockDirection"]).ToString());
					Details.StockDate = DateTime.Parse(dr["StockDate"].ToString());
                    Details.SupplierID = Int64.Parse(dr["SupplierID"].ToString());
					Details.SupplierCode = "" + dr["SupplierCode"].ToString();
					Details.SupplierName = "" + dr["SupplierName"].ToString();
					Details.Remarks = "" + dr["Remarks"].ToString();
				}

				return Details;
			}

			catch (Exception ex)
			{
				
				
				{
					
					
					
					
				}

				throw ex;
			}	
		}

		public StockDetails Details(string TransactionNo)
		{
			try
			{
                string SQL = SQLSelect() + "WHERE TransactionNo = @TransactionNo;";
				  
				
	 			
				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;

				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

				MySqlDataReader myReader = base.ExecuteReader(cmd, System.Data.CommandBehavior.SingleResult);
				
				StockDetails Details = new StockDetails();

				while (myReader.Read()) 
				{
					Details.StockID = myReader.GetInt64("StockID");
                    Details.BranchID = myReader.GetInt32("BranchID");
					Details.TransactionNo = "" + myReader["TransactionNo"].ToString();
					Details.StockTypeID = myReader.GetInt16("StockTypeID");
					Details.StockTypeCode = "" + myReader["StockTypeCode"].ToString();
					Details.StockTypeDescription = "" + myReader["StockTypeDescription"].ToString();
                    Details.StockDirection = (StockDirections)Enum.Parse(typeof(StockDirections), myReader.GetString("StockDirection"));
					Details.StockDate = myReader.GetDateTime("StockDate");
					Details.SupplierID = myReader.GetInt64("SupplierID");
					Details.SupplierCode = "" + myReader["SupplierCode"].ToString();
					Details.SupplierName = "" + myReader["SupplierName"].ToString();
					Details.Remarks = "" + myReader["Remarks"].ToString();
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

		public MySqlDataReader Search(string TransactionNo)
		{
			try
			{
				string SQL =	SQLSelect() + "WHERE TransactionNo = @TransactionNo ";

				

				MySqlCommand cmd = new MySqlCommand();
				
				
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = SQL;
				
				MySqlParameter prmTransactionNo = new MySqlParameter("@TransactionNo",MySqlDbType.String);
				prmTransactionNo.Value = TransactionNo;
				cmd.Parameters.Add(prmTransactionNo);

				
				
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
				string SQL =	SQLSelect() + "(TransactionNo LIKE @SearchKey " +
								"OR StockTypeCode LIKE @SearchKey " +
								"OR a.Remarks LIKE @SearchKey) " +
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

        public System.Data.DataTable ListAsDataTableActiveInactive(TransactionListFilterType clsTransactionListFilterType, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 ";

                if (clsTransactionListFilterType == TransactionListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
                else if (clsTransactionListFilterType == TransactionListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                System.Data.DataTable dt = new System.Data.DataTable("tblStock");
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
        public System.Data.DataTable SearchDataTableActiveInactive(TransactionListFilterType clsTransactionListFilterType, string SearchKey, string SortField, SortOption SortOrder)
        {
            try
            {
                string SQL = SQLSelect() + "WHERE 1=1 " +
                                                "AND (TransactionNo LIKE @SearchKey " +
                                                "OR StockTypeCode LIKE @SearchKey " +
                                                "OR a.Remarks LIKE @SearchKey) ";

                if (clsTransactionListFilterType == TransactionListFilterType.ShowActiveOnly) SQL += "AND a.Active = 1 ";
                if (clsTransactionListFilterType == TransactionListFilterType.ShowInactiveOnly) SQL += "AND a.Active = 0 ";

                SQL += "ORDER BY " + SortField;

                if (SortOrder == SortOption.Ascending)
                    SQL += " ASC ";
                else
                    SQL += " DESC ";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@SearchKey", "%" + SearchKey + "%");

                System.Data.DataTable dt = new System.Data.DataTable("tblStock");
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

		#region AddItem

        /// <summary>
        /// Jul 28, 2011 : Lemu
        //  - Remove the adding/subtracting and SynchronizeQuantity in clsProductVariationsMatrix,
        //    already included in the new AddQuantity and SubtractQuantity.
        /// </summary>
        /// <param name="Details"></param>
        /// <param name="StockDirection"></param>
        /// <returns></returns>
		public Int64 AddItem(int BranchID, string TransactionNo, string CreatedBy, StockItemDetails Details, StockDirections StockDirection)
		{
			try  
			{
				
	 			
				StockItem clsStockItem = new StockItem(base.Connection, base.Transaction);
				Int64 StockItemID = clsStockItem.Insert(Details);

				ProductUnit clsProductUnit = new ProductUnit(base.Connection, base.Transaction);
				decimal Quantity = clsProductUnit.GetBaseUnitValue(Details.ProductID, Details.ProductUnitID, Details.Quantity);
                string strRemarks = string.Empty;

				Products clsProduct = new Products(base.Connection, base.Transaction);
				if (StockDirection == StockDirections.Decrement)
                {
                    strRemarks = Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.DEDUCT_STOCK_INVENTORY) + " " + Details.Remarks;
                    clsProduct.SubtractQuantity(BranchID, Details.ProductID, Details.VariationMatrixID, Quantity, strRemarks, Details.StockDate, TransactionNo, CreatedBy);	
                }
				else
                {
                    strRemarks = Products.getPRODUCT_INVENTORY_MOVEMENT_VALUE(PRODUCT_INVENTORY_MOVEMENT.ADD_STOCK_INVENTORY) + " " + Details.Remarks;
                    clsProduct.AddQuantity(BranchID, Details.ProductID, Details.VariationMatrixID, Quantity, strRemarks, Details.StockDate, TransactionNo, CreatedBy);	
                }

                // Removed
                //ProductVariationsMatrix clsProductVariationsMatrix = new ProductVariationsMatrix(base.Connection, base.Transaction);
                //if (Details.VariationMatrixID != 0)
                //{
                //    if (StockDirection == StockDirections.Decrement)
                //    {	clsProductVariationsMatrix.SubtractQuantity(Details.VariationMatrixID, Quantity);	}
                //    else
                //    {	clsProductVariationsMatrix.AddQuantity(Details.VariationMatrixID, Quantity);	}

                //    clsProductVariationsMatrix.SynchronizeQuantity(Details.ProductID);
                //}
				
				return StockItemID;
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

